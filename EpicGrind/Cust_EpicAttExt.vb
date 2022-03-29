Imports DEL_acadltlib_EM.DXF
Imports DEL_acadltlib_EM.CAD_commands
Imports DEL_acadltlib_EM.CAD_Plot
Imports EM_DxfReader.Dxf_Main
Imports DEL_acadltlib_EM.FileIO
Imports netDxf
Imports DEL_acadltlib_EM.DDE
Imports DEL_acadltlib_EM.CAD_attextract
Imports DEL_acadltlib_EM.CAD_command_items
Imports System.IO
Imports dxfHelper.dxfHelper
Imports CADHelper
Imports System.Threading
Imports System.Reflection

Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel

Public Class Cust_EpicAttExt

    Public Shared Function EpicAttExt(ByVal attTags As List(Of String), ByVal xlsFilename As String, ByRef OpenFileAtTheEndPath As String)


        Dim dxfpath = CustomMacroCommands.Export2DXF_Defaultx()
        Dim dxfDoc = DxfDocument.Load(dxfpath)

        If IsNothing(dxfDoc) Then
            MsgBox("DWG faila kļūda." & vbLf & "Neizdevās ielādēt DXF. Mēģināt Audit un Purge.")
            Return 1
        End If

        'Process special tags
        Dim specialTags = (From t In attTags Where t.StartsWith("#")).ToList()
        Dim filePaths = (From t In attTags Where t.StartsWith("$")).ToList()

        'get filepath
        Dim filepath As String
        If filePaths.Count > 0 Then
            filepath = filePaths(0).Substring(1, filePaths(0).Length - 1)
        Else
            filepath = Path.Combine("C:\", "Epic", "MacroAExt.xlsx")
        End If

        'remove special tags, so they don't interfere with regular tags
        For Each sT In specialTags
            attTags.Remove(sT)
        Next
        For Each p In filePaths
            attTags.Remove(p)
        Next

        'dxf extraction and filtering
        Dim RawEntities = dxfHelper.dxfHelper.Read2dxfH(dxfDoc)
        Dim OnlyBlocksThatHaveAttributes = (From ee In RawEntities.dxfHEntities
                                            Where ee.entType = dxfHelper.dxfHelperItems.dxhEntityType.BlockReference Select ee Where ee.BlckRfAttributes.Count > 0).ToList
        Dim FilteredByTags = FilterBlockRefsByAttributeTags(OnlyBlocksThatHaveAttributes, attTags)

        'generating data for xlsx
        Dim BlockRefs As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))

        For Each Bref In FilteredByTags
            Dim BrefEntry As New Dictionary(Of String, String)

            'processing special tags
            For Each specialTAG In specialTags
                Select Case specialTAG
                    Case "#DWGNAME"
                        BrefEntry.Add(specialTAG, Path.GetFileName(xlsFilename))
                    Case "#LAYOUT"
                        BrefEntry.Add(specialTAG, Bref.Layout)
                    Case "#BLOCKNAME"
                        BrefEntry.Add(specialTAG, Bref.BlckRfName)
                    Case "#LAYER"
                        BrefEntry.Add(specialTAG, Bref.Layer)
                    Case "#COLOR"
                        BrefEntry.Add(specialTAG, Bref.Color)
                End Select
            Next

            'processing regular tags
            For Each Tag In attTags
                If Bref.BlckRfAttributes.ContainsKey(Tag) Then
                    BrefEntry.Add(Tag, Bref.BlckRfAttributes(Tag))
                Else
                    BrefEntry.Add(Tag, "")
                End If

            Next

            BlockRefs.Add(BrefEntry)
        Next

        'file ops
        If Not (File.Exists(filepath)) Then
            CreateNewWorkbookFile(filepath)
        End If

        Dim workbook As IWorkbook

        Try

            'test if file works as an xlsx...
            Dim fs = New FileStream(filepath, FileMode.Open, FileAccess.ReadWrite)
            workbook = New XSSFWorkbook(fs)

            ' ...try to delete if not
        Catch ex As Exception
            Try
                File.Delete(filepath)
            Catch ex1 As Exception

                'change filename if nothing works
                filepath = FileExistIncrementer(filepath)
            End Try

            CreateNewWorkbookFile(filepath)

            Dim fs = New FileStream(filepath, FileMode.Open, FileAccess.ReadWrite)
            workbook = New XSSFWorkbook(fs)
        End Try

        'open the file and write data
        Dim sht = workbook.GetSheet("EpicMacro Attribute Extraction")

        For i = 0 To BlockRefs.Count - 1

            Dim AttributesInBlockref = BlockRefs(i)

            Dim RowNo As Integer = GetRowNo(sht)
            Dim NewRow As IRow = sht.CreateRow(RowNo)

            For Each Attribute In AttributesInBlockref
                Dim ColNo As Integer = GetOrCreateColNo(sht, Attribute.Key)
                NewRow.CreateCell(ColNo).SetCellValue(Attribute.Value)
            Next
        Next

        Dim sw As FileStream = File.Create(filepath)
        workbook.Write(sw)
        sw.Close()

        OpenFileAtTheEndPath = filepath

    End Function

    Private Shared Sub CreateNewWorkbookFile(filepath As String)
        Dim newfs = New FileStream(filepath, FileMode.Create, FileAccess.Write)
        Dim newWB = New XSSFWorkbook()
        newWB.CreateSheet("EpicMacro Attribute Extraction")
        newWB.Write(newfs)
        newfs.Close()
    End Sub

    Private Shared Function GetRowNo(sht As ISheet) As Integer
        Return sht.LastRowNum + 1
    End Function

    Private Shared Function GetOrCreateColNo(sht As ISheet, key As String) As Integer

        Dim FirstRow = sht.GetRow(0)

        'if empty file
        If FirstRow Is Nothing Then
            FirstRow = sht.CreateRow(0)
            Dim NewCell0 = FirstRow.CreateCell(0)
            NewCell0.SetCellValue(key)
            Return 0
        End If

        'find column with matching key in the header
        Dim LastCol = FirstRow.LastCellNum
        For cellNo = 0 To LastCol
            Dim cell = FirstRow.GetCell(cellNo)
            If Not (cell Is Nothing) Then
                If cell.StringCellValue = key Then
                    Return cellNo
                End If
            End If

        Next

        'create new if nothing is found
        Dim NewCell = FirstRow.CreateCell(LastCol)
        NewCell.SetCellValue(key)
        Return LastCol

    End Function
End Class
