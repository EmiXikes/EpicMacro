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

Public Class Cust_EpicSplit
    Public Class PlotPage
        Public ID As Integer
        Public FrameItem As DXFExtItemRef
        Public LayoutName As String
        Public DrawingName As String
        Public isPloted As Boolean
        Public isError As Boolean
        Public PDFsaveDPath As String
        Public DWGName As String
        Public CustomTAGValue As String

        Public PrefixAttributeString As String

    End Class

    Public Shared PlotPages As List(Of PlotPage)
    Public Shared LastID As Integer
    Public Shared dxfDoc As DxfDocument
    Public Shared dxfpath As String

    Shared DEL_Frames As New List(Of DXFExtItemRef)
    Shared FilePath As String = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Macro_EOF.txt")

    Public Shared Function EpicSplitToFiles(ByVal SavePath As String) As Integer

        Dim FullSavePath As String

        If LoadFramesFromEntireFile(, "ALL") = 1 Then

            MsgBox("DWG faila kļūda." & vbLf & "Kommanda EpicSplitToFiles apturēta." & vbLf & "Faila apstrāde nav izdevusies.")
            Return 1

        End If
        Dim ReadCheck As String

        Dim ACADLayouts As List(Of String) = (From I In dxfDoc.Blocks
                                              Where I.Name.Contains("*Paper_Space") Or I.Name = "*Model_Space"
                                              Select I.Record.Layout.Name).ToList

        For Each PlotPage In PlotPages

            FullSavePath = FileExistIncrementer(Path.Combine(SavePath, PlotPage.DrawingName & ".dwg").ToString)



            If PlotPage.LayoutName = "Model" Then
                ''Switch Layout
                AcadMacroCtab.LayoutName = PlotPage.LayoutName
                AcadMacroCtab.LayoutSwitchDone = "LayoutSwitched_" & PlotPage.LayoutName & "__" & CADHelper.FileOPS.RandomString(10)
                ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "xxctab" & vbLf)

                Do Until ReadCheck = AcadMacroCtab.LayoutSwitchDone
                    Try
                        Thread.Sleep(100)
                        ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
                    Catch ex As Exception
                    End Try
                Loop

                ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "select" & vbLf & "c" & vbLf)
                ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), GetSelectionWindow(PlotPage.FrameItem) & vbLf)
                ''SwitchToLayout(PlotPage.LayoutName)


                ''TODO_newDDE
                AcadMacroWblockSelected.DxfPath = FullSavePath
                AcadMacroWblockSelected.FileReadyMarker = "WBlock_Finished_" & PlotPage.DrawingName
                ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "xxWblock" & vbLf)

                Do Until ReadCheck = AcadMacroWblockSelected.FileReadyMarker
                    Try
                        Thread.Sleep(100)
                        ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
                    Catch ex As Exception
                    End Try
                Loop

            Else

                ''Switch Layout
                AcadMacroCtab.LayoutName = PlotPage.LayoutName
                AcadMacroCtab.LayoutSwitchDone = "LayoutSwitched_" & CADHelper.FileOPS.RandomString(10)
                ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "xxctab" & vbLf)

                Do Until ReadCheck = AcadMacroCtab.LayoutSwitchDone
                    Try
                        Thread.Sleep(100)
                        ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
                    Catch ex As Exception
                    End Try
                Loop

                ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "undo" & vbLf & "m" & vbLf)



                For Each ACADLayout In ACADLayouts
                    If ACADLayout <> PlotPage.LayoutName And ACADLayout <> "Model" Then
                        ''TODO_newDDE

                        AcadMacroDeleteLayout.LayoutName = ACADLayout
                        AcadMacroDeleteLayout.LayoutDeleteComplete = "LayoutDeleted_" & ACADLayout & "__" & CADHelper.FileOPS.RandomString(10)
                        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "xxLayDel" & vbLf)

                        Do Until ReadCheck = AcadMacroDeleteLayout.LayoutDeleteComplete
                            Try
                                Thread.Sleep(100)
                                ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
                            Catch ex As Exception
                            End Try
                        Loop

                    End If
                Next

                AcadMacroSaveAs.SaveAsFilePath = FullSavePath
                AcadMacroSaveAs.FileReadyMarker = "FileSaved_" & FullSavePath & "_" & CADHelper.FileOPS.RandomString(10)
                ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "xxSaveAs" & vbLf)

                Do Until ReadCheck = AcadMacroSaveAs.FileReadyMarker
                    Try
                        Thread.Sleep(100)
                        ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
                    Catch ex As Exception
                    End Try
                Loop


                ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "undo" & vbLf & "b" & vbLf)

            End If

        Next

        AcadMacroSaveAs.SaveAsFilePath = Path.Combine(SavePath, "__oldFile.dwg")
        AcadMacroSaveAs.FileReadyMarker = "FileSaved_" & CADHelper.FileOPS.RandomString(10)
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "xxSaveAs" & vbLf)

        ''MsgBox("test 01")

        Do Until ReadCheck = AcadMacroSaveAs.FileReadyMarker
            Try
                Thread.Sleep(500)
                ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
            Catch ex As Exception
            End Try
        Loop

        '' MsgBox("test 02")


    End Function

    Shared Function LoadFramesFromEntireFile(Optional DWGName As String = "", Optional SelLay As String = "ALL") As Integer
        Dim Taglist As New List(Of String)

        Dim ThisPlot As New PlotCADWindow

        Dim er As String = Chr(10)
        Dim LastID As Integer
        ''  Dim LayoutName As String = "Model"

        'make folder
        'Dim pdfexportPath As String = My.Application.Info.DirectoryPath & "\pdfExport"
        'If Not Directory.Exists(pdfexportPath) Then
        '    Directory.CreateDirectory(pdfexportPath)
        'End If

        ''     Try

        PlotPages = New List(Of PlotPage)

        Taglist = GetFrameTagList()

        Taglist.Add("_F_O_R_M_A_T_S_")



        ''DEL_Frames = AttributeExtractAll(Taglist)

        dxfpath = CustomMacroCommands.Export2DXF_Defaultx()
        dxfDoc = DxfDocument.Load(dxfpath)

        If IsNothing(dxfDoc) Then
            MsgBox("DWG faila kļūda." & vbLf & "Neizdevās ielādēt DXF. Mēģināt Audit un Purge.")
            Return 1
        End If

        Dim RawEntities = dxfHelper.dxfHelper.Read2dxfH(dxfDoc)
        Dim OnlyBlocksThatHaveAttributes = (From ee In RawEntities.dxfHEntities
                                            Where ee.entType = dxfHelper.dxfHelperItems.dxhEntityType.BlockReference Select ee Where ee.BlckRfAttributes.Count > 0).ToList
        Dim FilteredByTags = FilterBlockRefsByAttributeTags(OnlyBlocksThatHaveAttributes, Taglist)
        Dim GroupedBytags = GroupBlckRefsByAttributeTags(FilteredByTags, Taglist)

        DEL_Frames = New List(Of DXFExtItemRef)
        For Each T In FilteredByTags

            Dim fr As New DXFExtItemRef With {
                           .X = T.Location.X,
                           .Y = T.Location.Y,
                           .Z = T.Location.Z,
                           .Scale = T.Scale.X,
                           .Rotation = T.Rotation,
                           .Layer = T.Layer,
                           .Layout = T.Layout,
                           .Color = T.Color,
                           .Name = T.BlckRfName
                          }

            fr.Attributes = New List(Of ExtAttribute)

            For Each A In T.BlckRfAttributes
                fr.Attributes.Add(New ExtAttribute With {.Name = A.Key, .Value = A.Value})
            Next


            DEL_Frames.Add(fr)
        Next

        Dim ThisPage As New PlotPage
        Dim RasNr As Integer = 1

        For i = 0 To DEL_Frames.Count - 1
            If Strings.Trim(DEL_Frames.Item(i).GetAttValue("_F_O_R_M_A_T_S_")) <> "" Then

                ThisPage = New PlotPage
                ThisPage.FrameItem = DEL_Frames.Item(i)
                ThisPage.LayoutName = DEL_Frames.Item(i).Layout
                ThisPage.DrawingName = "Nr. " & RasNr & " (nav RL)"
                ThisPage.DWGName = DWGName

                If Strings.Trim(GetDrawingName(DEL_Frames.Item(i))) <> "" Then
                    ThisPage.DrawingName = GetDrawingName(DEL_Frames.Item(i)).Replace("%%", "")
                End If

                LastID = LastID + 1
                ThisPage.ID = LastID

                If SelLay = "ALL" Then
                    PlotPages.Add(ThisPage)
                Else
                    If SelLay = "Model" Then
                        If ThisPage.LayoutName = "Model" Then PlotPages.Add(ThisPage)
                    Else
                        If ThisPage.LayoutName <> "Model" Then PlotPages.Add(ThisPage)
                    End If
                End If

                RasNr = RasNr + 1
            End If

        Next

        Return 0

        'Catch ex As Exception
        '    Throw New Exception
        'End Try


    End Function

    Private Shared Function GetFrameTagList() As List(Of String)
        Dim Taglist As New List(Of String)
        Dim Tags As String()
        Tags = Split(My.Settings.S_RakstlTags, Chr(10)).ToArray

        For i = 0 To Tags.Length - 1
            If Strings.Trim(Tags(i)) <> "" Then
                Taglist.Add(Tags(i))
            End If
        Next

        Return Taglist
    End Function

    Private Shared Function GetDrawingName(ByVal Frame As DXFExtItemRef) As String

        Dim FramePosX As Double = Frame.X
        Dim FramePosY As Double = Frame.Y

        FramePosX = Math.Truncate(FramePosX)
        FramePosY = Math.Truncate(FramePosY)

        Dim Taglist As New List(Of String)

        Taglist = GetFrameTagList()

        Dim TagValue As String
        Dim NameTablePosX As Double
        Dim NameTablePosY As Double

        For DEL_FramesID = 0 To DEL_Frames.Count - 1


            For TagNameID = 0 To Taglist.Count - 1

                TagValue = Strings.Trim(DEL_Frames.Item(DEL_FramesID).GetAttValue(Taglist.Item(TagNameID)))

                If TagValue <> "" Then
                    ''  LogThis("Testing " & TagValue)

                    NameTablePosX = DEL_Frames.Item(DEL_FramesID).X
                    NameTablePosY = DEL_Frames.Item(DEL_FramesID).Y

                    NameTablePosX = Math.Truncate(NameTablePosX)
                    NameTablePosY = Math.Truncate(NameTablePosY)

                    ''   LogThis("Testing X " & DEL_FramesID & "-> " & FramePosX & "==" & NameTablePosX)
                    ''LogThis("Testing Y " & DEL_FramesID & "-> " & FramePosY & "==" & NameTablePosY)

                    'If FramePosX = NameTablePosX And
                    '        FramePosY = NameTablePosY Then
                    If isCoordinateMatch(FramePosX, NameTablePosX, My.Settings.S_FramePosDelta) = True And
                       isCoordinateMatch(FramePosY, NameTablePosY, My.Settings.S_FramePosDelta) = True Then

                        'End If
                        TagValue = Strings.Trim(TagValue)
                        If Strings.UCase(Strings.Right(TagValue, 4)) = ".DWG" Then
                            TagValue = Strings.Left(TagValue, Strings.Len(TagValue) - 4)
                        End If

                        If Frame.Layout = DEL_Frames.Item(DEL_FramesID).Layout Then
                            Return TagValue
                        End If


                    End If

                End If

            Next

        Next


    End Function

    Private Shared Function isCoordinateMatch(ByVal Coord1 As Double, ByVal Coord2 As Double, ByVal DeltaDist As Double) As Boolean
        If Coord2 > (Coord1 - DeltaDist) And Coord2 < (Coord1 + DeltaDist) Then
            Return True
        Else
            Return False
        End If

    End Function


    Public Shared Function AttributeExtractAll(ByVal TagNames As List(Of String), Optional WindowSelection As Boolean = False, Optional ByVal InsertMultiLineSeperator As Boolean = False) As List(Of DXFExtItemRef)
        Dim dx As New DxfDocument
        Dim AttExtractFile As String

        AttExtractFile = CustomMacroCommands.Export2DXF_Defaultx()

        dx = DxfDocument.Load(AttExtractFile)

        Return ReadBlockAttributesFromDXFDocument(dx, TagNames, InsertMultiLineSeperator)

    End Function

    Public Shared Function ReadBlockAttributesFromDXFDocument(ByVal dx As DxfDocument, Optional TagNames As List(Of String) = Nothing, Optional ByVal InsertMultiLineSeperator As Boolean = False) As List(Of DXFExtItemRef)

        Dim BlckRef As New DXFExtItemRef
        Dim AttRef As New ExtAttribute
        Dim ResultList As New List(Of DXFExtItemRef)
        Dim AttrTagName As String
        Dim AttrTagReComName As String

        Try

            If IsNothing(dx) = True Then Throw New Exception

            Dim RealTagName As String

            Dim AttrIndex As String
            Dim LegacyJoin As Boolean



            For Each InsItem In dx.Inserts

                BlckRef = New DXFExtItemRef
                BlckRef.Attributes = New List(Of ExtAttribute)

                For Each AttributeItem In InsItem.Attributes

                    RealTagName = AttributeItem.Tag
                    AttrTagName = AttributeItem.Tag
                    AttrTagReComName = AttributeItem.Tag
                    LegacyJoin = False

                    If Not IsNothing(TagNames) Then

                        For Each TagName In TagNames

                            If Strings.Right(TagName, 1) = "#" Then

                                ''Name without the # switch
                                RealTagName = Left(TagName, Strings.InStr(TagName, "#") - 1)

                                ''TagName_000
                                ''TagName_#

                                If RealTagName = Strings.Left(AttrTagReComName, Len(RealTagName)) And
                                    IsNumeric(Strings.Replace(Strings.Mid(AttrTagReComName, Len(RealTagName) + 1, Len(AttrTagReComName)), "_", "")) Then '' add other checks...

                                    AttrIndex = Strings.Mid(AttrTagReComName, Len(RealTagName) + 1, Len(AttrTagReComName))
                                    AttrIndex = AttrIndex.PadLeft(3, "0")

                                    AttrTagReComName = Strings.Left(AttrTagReComName, Len(RealTagName))
                                    AttrTagReComName = AttrTagReComName & "_" & AttrIndex

                                    AttrTagName = AttrTagReComName
                                    LegacyJoin = True
                                    ''MsgBox(AttrTagReComName)

                                End If
                            End If
                        Next
                    End If

                    If IsNothing(TagNames) Then
                        BlckRef.Attributes.Add(New ExtAttribute With {.Name = AttributeItem.Tag, .Value = AttributeItem.Value})
                        ''MsgBox(AttributeItem.Tag)
                    Else
                        If LegacyJoin = True Then
                            RealTagName = RealTagName & "#"
                        End If
                        '  MsgBox(AttrTagName)

                        If TagNames.Contains(RealTagName) Then
                            BlckRef.Attributes.Add(New ExtAttribute With {.Name = AttrTagReComName, .Value = AttributeItem.Value})
                        End If
                    End If
                Next

                BlckRef.Color = InsItem.Color.Index
                BlckRef.Layer = InsItem.Layer.Name
                BlckRef.Layout = InsItem.Owner.Record.Layout.Name
                BlckRef.X = InsItem.Position.X
                BlckRef.Y = InsItem.Position.Y
                BlckRef.Rotation = InsItem.Rotation
                BlckRef.Scale = InsItem.Scale.X

                If BlckRef.Attributes.Count > 0 Then
                    BlckRef = NormalizeMultilineAttributesV2x(BlckRef, InsertMultiLineSeperator)
                    ResultList.Add(BlckRef)
                End If

            Next


            Return ResultList

        Catch ex As Exception
            ''  MsgBox(ex.Message)
            MsgBox(ex.StackTrace & Chr(10) & ex.Message)
            Throw New Exception("Neizdevas nolasit DXF")

        End Try
    End Function

    Private Shared Function NormalizeMultilineAttributesV2x(ByVal BlckRef As DXFExtItemRef, Optional ByVal InsertMultiLineSeperator As Boolean = False) As DXFExtItemRef

        BlckRef.Attributes = BlckRef.Attributes.OrderBy((Function(x) x.Name)).ToList

        Dim TestAttrName As String
        Dim MultilineAtrrCombined As String
        Dim AttrReturnList As New List(Of ExtAttribute)
        Dim EnterSymbol As String = ChrW(9166)
        If InsertMultiLineSeperator = False Then EnterSymbol = ""
        Dim UsedTags As New List(Of String)
        Dim UnderscorePosition As Integer
        Dim ImpliedTagIndex As String = ""

        For Each Attr In BlckRef.Attributes

            If Len(Attr.Name) > 4 Then

                For i = Strings.Len(Attr.Name) - 1 To 1 Step -1
                    If Strings.Mid(Attr.Name, i, 1) = "_" Then
                        UnderscorePosition = i
                        ImpliedTagIndex = Strings.Mid(Attr.Name, UnderscorePosition + 1, 3)
                        If IsNumeric(ImpliedTagIndex) Then

                        Else
                            UnderscorePosition = 0
                            ImpliedTagIndex = ""
                        End If

                        ''  MsgBox(UnderscorePosition)
                    End If
                Next

                If UnderscorePosition > 0 Then
                    TestAttrName = Strings.Left(Attr.Name, UnderscorePosition)
                    If UsedTags.Contains(TestAttrName) = False Then
                        UsedTags.Add(TestAttrName)
                        MultilineAtrrCombined = ""
                        For Each Attr2 In BlckRef.Attributes
                            If TestAttrName = Strings.Left(Attr2.Name, Len(TestAttrName)) And Len(Attr2.Name) = Len(TestAttrName) + Len(ImpliedTagIndex) And IsNumeric(Strings.Mid(Attr2.Name, Len(TestAttrName) + 1, 3)) Then  ''add len check
                                MultilineAtrrCombined = MultilineAtrrCombined & EnterSymbol & Chr(13) & Attr2.Value
                            End If
                        Next
                        If Strings.Left(MultilineAtrrCombined, 1) = EnterSymbol Then
                            MultilineAtrrCombined = Strings.Right(MultilineAtrrCombined, Len(MultilineAtrrCombined) - 1)
                        End If
                        If Strings.Left(MultilineAtrrCombined, 1) = Chr(13) Then
                            MultilineAtrrCombined = Strings.Right(MultilineAtrrCombined, Len(MultilineAtrrCombined) - 1)
                        End If
                        TestAttrName = Strings.Left(TestAttrName, Len(TestAttrName) - 1)
                        AttrReturnList.Add(New ExtAttribute With {.Name = TestAttrName, .Value = MultilineAtrrCombined})
                    End If
                Else
                    AttrReturnList.Add(Attr)
                End If

            Else
                AttrReturnList.Add(Attr)
            End If

        Next

        BlckRef.Attributes = AttrReturnList
        Return BlckRef

    End Function

End Class
