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
Imports CADHelper
Imports System.Threading
Imports System.Reflection

Public Class CustomMacroCommands


    Public Shared Shad As New ShadowEngine

    Shared DEL_Frames As New List(Of DXFExtItemRef)



    Public Shared Function ReadDxf(ByVal DxfFileName As String) As DxfDocument
        Try
            Dim ThisDxf As DxfDocument = DxfDocument.Load(DxfFileName)
            If IsNothing(ThisDxf) = True Then Throw New Exception
            Return ThisDxf
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox("DXF nolasīšanas kļūda. Veikt Audit un Purge.")
        End Try

    End Function

    Public Shared Function BindxRef(ByVal RefName As String) As Boolean
        Dim isRefLoadDone As Boolean = False
        Dim com As String
        Dim esc As String = Chr(27)
        Dim er As String = Chr(10)
        Dim FilePath As String = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Macro_EOF.txt")

        Dim ReadCheck As String

        If IsNothing(Shad.logReader) Then
            ShadowEng.StartShadows()
        Else
            Shad.logReader.StopPolling = False
        End If


        com = "-xref" & er & "b" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)
        ''SendCommand(com)

        com = Chr(34) & RefName & Chr(34) & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)
        ''SendCommand(com)

        Dim rndch As String = "DXF_PROCESSED" & CADHelper.FileOPS.RandomString(10)
        AcadMacroEOF.NacroEOFValue = rndch
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "MACRO_EOF" & vbLf)

        Do Until ReadCheck = rndch
            Try
                Thread.Sleep(500)
                ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
            Catch ex As Exception

            End Try

        Loop

        '' SendWaitForCommand("isRefLoadDone")

        Return True
    End Function

    Public Shared Function DeleteLayerShad(ByVal LayerName As String) As Boolean
        Dim isRefLoadDone As Boolean = False
        Dim com As String
        Dim esc As String = Chr(27)
        Dim er As String = Chr(10)
        Dim FilePath As String = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Macro_EOF.txt")

        Dim ReadCheck As String

        If IsNothing(Shad.logReader) Then
            ShadowEng.StartShadows()
        Else
            Shad.logReader.StopPolling = False
        End If

        com = "-layer" & er & "s" & er & "0" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        com = esc & esc & esc
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        com = "-laydel" & er & "n" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        ''com = Chr(34) & LayerName & Chr(34) & er & er & "Y" & er
        com = LayerName & er & er & "Y" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        Dim rndch As String = "LYR_DELETED" & CADHelper.FileOPS.RandomString(10)
        AcadMacroEOF.NacroEOFValue = rndch
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "MACRO_EOF" & vbLf)

        Do Until ReadCheck = rndch
            Try
                Thread.Sleep(500)
                ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
            Catch ex As Exception

            End Try

        Loop

        '' SendWaitForCommand("isRefLoadDone")

        Return True
    End Function

    Public Shared Function EpicBindResolvedXREFS() As Integer

        Dim dpath As String = Export2DXF_Defaultx()
        Dim Response As Integer = 0

        'Dim d As DXFFile = ReadDXFFile(dpath)
        'For Each T In d.Xrefs
        '    If T.isResolved = False Then Response = 1
        '    If T.isResolved = True Then
        '        BindxRef(T.Name)
        '    End If
        'Next



        Dim dx As DxfDocument = ReadDxf(dpath)
        For Each T In dx.Inserts
            If T.Block.IsXRef = True Then
                BindxRef(T.Block.Name)
            End If

        Next

        Return Response
    End Function

    Public Shared Function DeleteFrozenLayers() As Integer

        Dim dpath As String = Export2DXF_Defaultx()
        Dim Response As Integer = 0

        Dim dx As DxfDocument = ReadDxf(dpath)
        For Each L In dx.Layers
            If L.IsFrozen = True Then
                DeleteLayerShad(L.Name)
            End If

        Next

        Return Response
    End Function


    Public Shared Function Export2DXF_Defaultx() As String
        ''Dim DxfFileName As String = "C:\Epic\Apps\dxfTEST.dxf"
        Dim isDxfExportReady As Boolean = False
        Dim isDXFcreated As Boolean = False
        Dim isFileInUse As Boolean = True
        '' Dim xDXF As New DxfDocument
        Dim com As String
        Dim esc As String = Chr(27)
        Dim er As String = Chr(10)
        Dim DxfFileName As String

        Dim fileExists As Boolean = True

        Dim dxfpath As String = My.Application.Info.DirectoryPath & "\dxfexport"

        If Not Directory.Exists(dxfpath) Then
            Directory.CreateDirectory(dxfpath)
        End If

        'If IsNothing(Shad.logReader) Then
        '     ShadowEng.StartShadows()
        'Else
        '    Shad.logReader.StopPolling = False
        'End If

        DxfFileName = dxfpath & "\dxfexport.dxf"

        If File.Exists(DxfFileName) Then
            Try
                File.Delete(DxfFileName)
            Catch ex As Exception
                DxfFileName = FileExistIncrementer(DxfFileName)
            End Try
        End If

        Dim FilePath As String = Path.Combine(My.Application.Info.DirectoryPath, "Macro_EOF.txt")



        com = "filedia" & er & "0" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        com = "dxfout" & er & Chr(34) & DxfFileName & Chr(34) & er & "v" & er & "2000(LT2000)" & er & "16" & er & "y" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        com = esc & esc & esc
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        com = "filedia" & er & "1" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)


        AcadMacroEOF.NacroEOFValue = "isDxfExportReady"
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "MACRO_EOF" & vbLf)

        Dim ReadCheck As String
        Do Until ReadCheck = "isDxfExportReady"
            Thread.Sleep(500)
            ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
        Loop


        Do Until isDXFcreated = True
            Thread.Sleep(25)
            isDXFcreated = File.Exists(DxfFileName)
            '' MsgBox("dxf exist: " & isDXFcreated)
        Loop

        Do Until isFileInUse = False
            Thread.Sleep(25)
            isFileInUse = IsFileOpen(DxfFileName)
            ''  MsgBox("dxf file in use: " & isFileInUse)
        Loop

        Return DxfFileName

    End Function

    Public Shared Function Export2DXF_ToPath(ByVal FileName As String) As String
        ''Dim DxfFileName As String = "C:\Epic\Apps\dxfTEST.dxf"
        Dim isDxfExportReady As Boolean = False
        Dim isDXFcreated As Boolean = False
        Dim isFileInUse As Boolean = True
        '' Dim xDXF As New DxfDocument
        Dim com As String
        Dim esc As String = Chr(27)
        Dim er As String = Chr(10)
        Dim DxfFileName As String

        Dim fileExists As Boolean = True

        'Dim dxfpath As String = My.Application.Info.DirectoryPath & "\dxfexport"

        'If Not Directory.Exists(dxfpath) Then
        '    Directory.CreateDirectory(dxfpath)
        'End If

        'If IsNothing(Shad.logReader) Then
        '     ShadowEng.StartShadows()
        'Else
        '    Shad.logReader.StopPolling = False
        'End If

        DxfFileName = FileName

        If File.Exists(DxfFileName) Then
            Try
                File.Delete(DxfFileName)
            Catch ex As Exception
                DxfFileName = FileExistIncrementer(DxfFileName)
            End Try
        End If

        Dim FilePath As String = Path.Combine(My.Application.Info.DirectoryPath, "Macro_EOF.txt")



        com = "filedia" & er & "0" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        com = "dxfout" & er & Chr(34) & DxfFileName & Chr(34) & er & "v" & er & "2000(LT2000)" & er & "16" & er & "y" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        com = esc & esc & esc
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        com = "filedia" & er & "1" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)


        AcadMacroEOF.NacroEOFValue = "isDxfExportReady"
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "MACRO_EOF" & vbLf)

        Dim ReadCheck As String
        Do Until ReadCheck = "isDxfExportReady"
            Thread.Sleep(500)
            ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
        Loop


        Do Until isDXFcreated = True
            Thread.Sleep(25)
            isDXFcreated = File.Exists(DxfFileName)
            '' MsgBox("dxf exist: " & isDXFcreated)
        Loop

        Do Until isFileInUse = False
            Thread.Sleep(25)
            isFileInUse = IsFileOpen(DxfFileName)
            ''  MsgBox("dxf file in use: " & isFileInUse)
        Loop

        Return DxfFileName

    End Function

End Class
