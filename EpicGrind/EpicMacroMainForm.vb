Imports Ookii.Dialogs
Imports DEL_acadltlib_EM.DDE
Imports DEL_acadltlib_EM.CAD_commands
Imports DEL_acadltlib_EM.AutoCADLTinfo
Imports DEL_acadltlib_EM.EpicProfiles
'Imports System.Threading
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading
Imports CADHelper
Imports CADHelper.FileOPS
Imports System.Reflection



''Test edit 2018.08.17

Public Class EpicMacroMainForm

    Public Shad As New ShadowEngine

    Public Shared OpenFileAtTheEnd As Boolean = False
    Public Shared OpenFileAtTheEndPath As String

    Public MacroProfiles As New SaveProfileSystem
    Public ProfileList As New List(Of SaveProfileItem)
    Dim DontRefresh As Boolean = False

    Dim ThisNewsBox As New NewsBox
    Dim LoadLock As Boolean

    Public Shared BCustomAttr As Boolean = False
    Public Shared AttrList As New List(Of String)(New String() {"RASEJUMA_NR", "RASEJUMA_NOSAUKUMS"})
    Public Shared CustomAttrInfo As New List(Of ValPair)

    '' Dim EnterSymbol As String = "<!ent>"
    Dim EnterSymbol As String = ChrW(9166)

    Public Class ValPair
        Public Property RasNr
        Public Property Nos
    End Class


    Public Function FetchOldSaves()

    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim DWGFileSelectDialog As New VistaOpenFileDialog
        Dim ThisDWGFile As New DWGFile



        DWGFileSelectDialog.Multiselect = True
        DWGFileSelectDialog.Filter = "AutoCAD files|*.dwg"
        If DWGFileSelectDialog.ShowDialog() = DialogResult.OK Then

            If DWGFileSelectDialog.FileNames.Count > 0 Then

                If IsNothing(gv.DWGFiles) Then
                    gv.DWGFiles = New List(Of DWGFile)
                End If

                For i = 0 To DWGFileSelectDialog.FileNames.Count - 1
                    ThisDWGFile = New DWGFile
                    ThisDWGFile.FilePath = DWGFileSelectDialog.FileNames(i)
                    ThisDWGFile.ID = gv.LastFileID
                    gv.DWGFiles.Add(ThisDWGFile)
                    gv.LastFileID = gv.LastFileID + 1
                Next



                UpdateFiletable()
            End If
        End If
    End Sub

    Sub UpdateFiletable()

        If InvokeRequired Then

        Else

            FileTable.AllowUserToAddRows = True

            FileTable.Rows.Clear()

            Dim LastRow As Integer

            For i = 0 To gv.DWGFiles.Count - 1
                FileTable.Rows.Add()
                LastRow = FileTable.Rows.Count - 2
                FileTable.Rows.Item(LastRow).Cells.Item(0).Value = Strings.Mid(gv.DWGFiles.Item(i).FilePath, InStrRev(gv.DWGFiles.Item(i).FilePath, "\") + 1)
                FileTable.Rows.Item(LastRow).Cells.Item(1).Value = gv.DWGFiles.Item(i).ID
                If gv.DWGFiles.Item(i).isDone = True Then
                    FileTable.Rows.Item(LastRow).Cells.Item(0).Style.ForeColor = Color.ForestGreen
                    FileTable.Rows.Item(LastRow).Cells.Item(1).Style.ForeColor = Color.ForestGreen
                    FileTable.Rows.Item(LastRow).Cells.Item(0).Style.BackColor = Color.LightGreen
                    FileTable.Rows.Item(LastRow).Cells.Item(1).Style.BackColor = Color.LightGreen
                    FileTable.Rows.Item(LastRow).Cells.Item(0).Style.Font = New Font("Arial", 10, FontStyle.Bold)
                    FileTable.Rows.Item(LastRow).Cells.Item(1).Style.Font = New Font("Arial", 10, FontStyle.Bold)
                Else
                    FileTable.Rows.Item(LastRow).Cells.Item(0).Style.ForeColor = Color.Black
                    FileTable.Rows.Item(LastRow).Cells.Item(0).Style.BackColor = Color.White
                    FileTable.Rows.Item(LastRow).Cells.Item(0).Style.Font = New Font("Arial", 9, FontStyle.Regular)
                End If

                FileTable.Refresh()
            Next

            FileTable.Refresh()
            FileTable.ClearSelection()
            FileTable.AllowUserToAddRows = False


        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FileTable.Rows.Clear()
        gv.DWGFiles = New List(Of DWGFile)
        gv.LastFileID = 0
        UpdateFiletable()
    End Sub

    Dim Orig_Height As Integer
    Dim Delta_Height As Integer

    Private Sub Form1_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        Delta_Height = Me.Height - Orig_Height

        FileTable.Height = FileTable.Height + Delta_Height
        MacroTable.Height = MacroTable.Height + Delta_Height
        MacroCommandBox.Height = MacroCommandBox.Height + Delta_Height
        Panel1.Top = Panel1.Top + Delta_Height
        MacroDescription.Top = MacroDescription.Top + Delta_Height
        'Button1.Top = Button1.Top + Delta_Height
        'Button2.Top = Button2.Top + Delta_Height
        'Button3.Top = Button3.Top + Delta_Height
        'Button4.Top = Button4.Top + Delta_Height
        'Button4.Top = Button4.Top + Delta_Height
        Me.Width = 570
    End Sub

    Private Sub Form1_ResizeBegin(sender As Object, e As EventArgs) Handles MyBase.ResizeBegin
        Orig_Height = Me.Height
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim R As Boolean
        Dim er As String = Chr(10)
        Dim com As String
        Dim prc As New System.Diagnostics.Process()

        OpenFileAtTheEnd = False

        If IsNothing(gv.DWGFiles) Then
            MsgBox("Nav pievienoti DWG faili.")
            Exit Sub
        End If

        If gv.DWGFiles.Count = 0 Then
            MsgBox("Nav pievienoti DWG faili.")
            Exit Sub
        End If

        Try
            My.Settings.LastProfileID = MacroTable.SelectedRows.Item(0).Index
        Catch ex As Exception
            MsgBox("Nav izvēlēts Macro.")
            Exit Sub
        End Try

        For i = 0 To gv.DWGFiles.Count - 1
            gv.DWGFiles.Item(i).isDone = False
        Next

        CountMessage.Text = ""

        If IsNothing(Shad.logReader) Then
            ShadowEng.StartShadows()
        Else
            Shad.logReader.StopPolling = False
        End If

        com = "LOGFILEPATH" & er & "C:\Epic\Log" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        ''initDDE()
        com = "sdi" & er & "0" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)
        ''SendCommand(com)
        com = "LAYEREVALCTL" & er & "0" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)
        ''SendCommand(com)

        '' -------------------------- Shadow engine stuff ------------------------------
        '' -----------------------------------------------------------------------------

        AcadMacroOpen.DWGFileName.Clear()

        Dim FilePath As String = IO.Path.Combine(IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Macro_EOF.txt")
        If IO.File.Exists(FilePath) = False Then IO.File.Create(FilePath).Dispose()
        Dim Lines() As String = System.IO.File.ReadAllLines(FilePath)
        If Lines.Length < 10 Then ReDim Preserve Lines(10)
        Lines(0) = ""
        System.IO.File.WriteAllLines(FilePath, Lines)


        AcadMacroEOF.NacroEOFValue = "nullfileopenready"
        AcadMacroOpen.DWGFileName.Add(ShadowEng.ShadX.ids(0), OpenScript(Path.Combine(Application.StartupPath, "nullFile.dwg"), "MACRO_EOF"))
        ''AcadMacroOpen.DWGFileName.Add(ShadowEng.ShadX.ids(0), OpenScript("C:\Epic\qwe3.dwg", "MACRO_EOF"))

        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "oppp" & vbLf)

        Dim ReadCheck As String
        ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)

        Do Until ReadCheck = "nullfileopenready"
            Thread.Sleep(500)
            Try
                ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
            Catch ex As Exception

            End Try

        Loop

        ''R = OpenDWG_2(Path.Combine(Application.StartupPath, "nullFile.dwg"))
        ''Thread.Sleep(500)
        ''initDDE()

        UpdateFiletable()



        For i = 0 To gv.DWGFiles.Count - 1

            'open
            AcadMacroEOF.NacroEOFValue = CStr(gv.DWGFiles(i).ID) & "openready"
            AcadMacroOpen.DWGFileName(ShadowEng.ShadX.ids(0)) = OpenScript(gv.DWGFiles(i).FilePath, "MACRO_EOF")
            ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "oppp" & vbLf)

            Do Until ReadCheck = CStr(gv.DWGFiles(i).ID) & "openready"
                Thread.Sleep(500)
                Try
                    ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
                Catch ex As Exception

                End Try

            Loop

            'execute
            R = ExecuteDWG(i)

            ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), Chr(27) & Chr(27) & Chr(27))

            AcadMacroEOF.NacroEOFValue = CStr(gv.DWGFiles(i).ID) & "fileexecuted"
            ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "MACRO_EOF" & vbLf)

            Do Until ReadCheck = CStr(gv.DWGFiles(i).ID) & "fileexecuted"
                Thread.Sleep(500)
                Try
                    ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
                Catch ex As Exception

                End Try

            Loop


            'close
            AcadMacroClose.Save = True
            AcadMacroClose.ClosedMarker = "MACRO_EOF"
            AcadMacroEOF.NacroEOFValue = CStr(gv.DWGFiles(i).ID) & "fileclosed"
            ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "CLXX" & vbLf)

            Do Until ReadCheck = CStr(gv.DWGFiles(i).ID) & "fileclosed"
                Thread.Sleep(500)
                Try
                    ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
                Catch ex As Exception

                End Try

            Loop

            CountMessage.Text = i + 1 & " / " & gv.DWGFiles.Count
            gv.DWGFiles.Item(i).isDone = True
            UpdateFiletable()

        Next

        ' Close NULL file
        AcadMacroClose.Save = False
        AcadMacroClose.ClosedMarker = "MACRO_EOF"
        AcadMacroEOF.NacroEOFValue = "nullfileclosed"
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "CLXX" & vbLf)

        'Do Until ReadCheck = "nullfileclosed"
        '    Thread.Sleep(500)
        '    ReadCheck = System.IO.File.ReadAllLines(FilePath)(0)
        'Loop

        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "filedia" & vbLf & "1" & vbLf)
        ''SetFileDia(True)

        'If My.Settings.S_SDI = True Then
        '    com = "sdi" & er & "1" & er
        '    SendCommand(com)
        '    com = "qsave" & er & "quit" & er
        '    SendCommand(com)

        '    If AutoCADonStatus() = False Then
        '        prc = Process.Start(AutoCADDefaultPath)
        '    End If
        'Else
        '    com = "qsave" & er & "close" & er
        '    SendCommand(com)
        'End If

        If OpenFileAtTheEnd = True Then
            Process.Start(OpenFileAtTheEndPath)
        End If

        If BCustomAttr = True Then
            CustomAttrResult.ShowDialog()
        End If

    End Sub

    Function ExecuteDWG(ByVal PlotFileID As Integer) As Boolean

        Dim er As String = Chr(10)
        Dim com As String

        ''R = OpenDWG_2(gv.DWGFiles.Item(PlotFileID).FilePath)

        ''SendWaitForCommand("isFileOpen", "True")


        ''CloseDDE()
        ''Thread.Sleep(500)
        ''initDDE()

        ''SetEnv("isFileExecute", False)

        com = MacroCommandBox.Text
        com = Replace(com, Chr(10), "")
        com = Replace(com, Chr(13), "")
        com = Replace(com, EnterSymbol, Chr(10))
        com = Replace(com, "<!Esc>", Chr(27))

        Dim ExecLines As String() = Strings.Split(com, Chr(10))

        If CheckBox_LayoutLoop.Checked = True Then
            Dim b As String
            Dim LayoutsinDWG As List(Of String)

            LayoutsinDWG = GetLayouts()

            For i = 0 To LayoutsinDWG.Count - 1
                b = SwitchToLayout(LayoutsinDWG.Item(i))
                ExecuteLines(ExecLines, gv.DWGFiles.Item(PlotFileID).FilePath)
                ''  SendCommand(com)
            Next

        Else
            ExecuteLines(ExecLines, gv.DWGFiles.Item(PlotFileID).FilePath)
            '' SendCommand(com)
        End If


        ''SendWaitForCommand("isFileExecute", "True")

        'Do Until isFilePlotDone = True

        '    Dim Retries As Integer
        '    SetEnv("isFileExecute", "True"
        '    Thread.Sleep(20)
        '    isFilePlotDone = GetEnv("isFileExecute")

        '    Retries = Retries + 1
        '    If Retries > 5 Then
        '        CloseDDE()
        '        Thread.Sleep(100)
        '        initDDE()
        '        Retries = 0
        '    End If
        'Loop

        ''R = CloseDWG_WithSave()

        Return True
    End Function

    Function ExecuteLines(ByVal ExecLines As String(), Optional origFilePath As String = "")

        For Each S In ExecLines
            If Strings.UCase(Strings.Left(S, 4)) = "EPIC" Then
                ''Custom Commands
                S = Strings.UCase(Strings.Trim(Replace(S, Chr(10), "")))
                Select Case S
                    Case "EPICBINDRESOLVEDXREFS"
                        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), Chr(27) & Chr(27) & Chr(27))
                        CustomMacroCommands.EpicBindResolvedXREFS()
                        '    'Case "EPICEXTRACTATTRINFO"
                        '    '    SendCommand(Chr(27) & Chr(27) & Chr(27))
                        '    '    CustomMacroCommands.EpicExtractAttrInfo(AttrList)
                    Case "EPICSPLITTOFILES"
                        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), Chr(27) & Chr(27) & Chr(27))
                        Cust_EpicSplit.EpicSplitToFiles(Path.GetDirectoryName(origFilePath))
                        '        'Case "EPICWBLOCK"
                        '        '    SendCommand(Chr(27) & Chr(27) & Chr(27))
                        '        '    CustWBlock.EpicWBlock(origFilePath)
                    Case "EPICPURGE"
                        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), Chr(27) & Chr(27) & Chr(27))
                        Cust_EpicPurge.EpicPurge(origFilePath)

                    Case "EPICPURGE2"
                        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), Chr(27) & Chr(27) & Chr(27))
                        Cust_EpicPurge.EpicPurge2(origFilePath)

                    Case "EPICPURGE3"
                        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), Chr(27) & Chr(27) & Chr(27))
                        Cust_EpicPurge.EpicPurge3(origFilePath)
                    Case "EPICDELETEFROZEN"
                        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), Chr(27) & Chr(27) & Chr(27))
                        CustomMacroCommands.DeleteFrozenLayers()

                End Select

                If S.Contains("EPICATTEXT") Then
                    If Not (S.Contains("(") And S.Contains(")")) Then
                        Continue For
                    End If

                    Dim argString = S.Split("(")(1).Split(")")(0)
                    Dim args = argString.Split(",").ToList()


                    Cust_EpicAttExt.EpicAttExt(args, origFilePath, OpenFileAtTheEndPath)
                    OpenFileAtTheEnd = True

                End If

                Else
                ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), S & vbLf)
                ''SendCommand(S & Chr(10))
            End If

        Next
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLock = True

        PopulateMacroList()
        Try
            MacroTable.Rows.Item(My.Settings.LastProfileID).Selected = True
        Catch ex As Exception
            MacroTable.ClearSelection()
        End Try

        CheckBox_SDI.Checked = My.Settings.S_SDI
        CheckBox_LayoutLoop.Checked = My.Settings.S_LayoutLoop



        Me.AllowDrop = True

        '' MsgBox(EnterSymbol)

        If My.Application.CommandLineArgs.Count = 0 Then
        Else
            ''MsgBox(My.Application.CommandLineArgs.Item(0))


            Dim ThisDWGFile As New DWGFile
            Dim lines() As String = System.IO.File.ReadAllLines(My.Application.CommandLineArgs.Item(0))


            If IsNothing(gv.DWGFiles) Then
                gv.DWGFiles = New List(Of DWGFile)
            End If

            For n = 0 To lines.Count - 1
                If Strings.Trim(lines(n)) <> "" Then
                    If IO.File.Exists(lines(n)) Then
                        ThisDWGFile = New DWGFile
                        ThisDWGFile.FilePath = lines(n)
                        ThisDWGFile.ID = gv.LastFileID
                        gv.DWGFiles.Add(ThisDWGFile)
                        gv.LastFileID = gv.LastFileID + 1
                    End If

                End If
            Next

            UpdateFiletable()

        End If

        Me.Text = Me.Text & " - " & ActiveAutoCADTitle()


        If My.Settings.ShowNews = True Then
            ThisNewsBox.ShowDialog()
        End If

        ShowNews.Checked = My.Settings.ShowNews

        LoadLock = False

    End Sub

    Private Sub PopulateMacroList()

        'Dim OldSaveFilePath As String = "C:\Epic\Apps\EpicMacro\SaveProfiles"
        'Dim SaveFilePathFile As String = Path.Combine(Application.StartupPath(), "SaveFilePath.txt")

        'If File.Exists(SaveFilePathFile) = False Then
        '    File.Create(SaveFilePathFile).Dispose()
        'End If

        'Dim SaveFileContent = File.ReadAllLines(SaveFilePathFile)
        'If SaveFileContent.Length < 1 Then ReDim Preserve SaveFileContent(1)
        'Dim SaveFilePath As String = SaveFileContent(0)

        If Directory.Exists(My.Settings.MacroFilePath) = False Then
            Directory.CreateDirectory(My.Settings.MacroFilePath)
        End If
        For Each F In Directory.GetFiles(Path.Combine(Application.StartupPath, "SaveProfiles"))
            If File.Exists(Path.Combine(My.Settings.MacroFilePath, Path.GetFileName(F))) = False Then
                File.Copy(F, Path.Combine(My.Settings.MacroFilePath, Path.GetFileName(F)), False)
            End If

        Next

        MacroTable.Rows.Clear()
        ''MacroProfiles.ProfileSavePath = My.Application.Info.DirectoryPath & "\MacroFiles"
        MacroProfiles.ProfileSavePath = My.Settings.MacroFilePath
        MacroProfiles.Ext = "mcr"

        ProfileList = MacroProfiles.GetSaveProfiles()

        For i = 0 To ProfileList.Count - 1
            MacroTable.Rows.Add()
            MacroTable.Rows.Item(i).Cells(0).Value = ProfileList.Item(i).Name
            MacroTable.Rows.Item(i).Cells(1).Value = i
        Next

        MacroTable.ClearSelection()
    End Sub

    ''Dim Enc As Boolean = True

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'If Enc = False Then
        UpdateCommandBox()
        '    Enc = True
        'Else
        '    Enc = False
        'End If

    End Sub

    Sub UpdateCommandBox()

        MacroCommandBox.Text = Replace(MacroCommandBox.Text, "<!ent>", EnterSymbol)
        MacroCommandBox.Text = Replace(MacroCommandBox.Text, EnterSymbol & Chr(10), Chr(10))
        MacroCommandBox.Text = Replace(MacroCommandBox.Text, EnterSymbol, Chr(10))
        MacroCommandBox.Text = Replace(MacroCommandBox.Text, ";", Chr(10))
        MacroCommandBox.Text = Replace(MacroCommandBox.Text, Chr(10), EnterSymbol & Chr(10))
        MacroCommandBox.Text = Replace(MacroCommandBox.Text, "<!Esc>", Chr(27))
        MacroCommandBox.Text = Replace(MacroCommandBox.Text, "^C", Chr(27))
        MacroCommandBox.Text = Replace(MacroCommandBox.Text, Chr(27), "<!Esc>")

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim sd As New SaveFileDialog

        UpdateCommandBox()

        sd.InitialDirectory = MacroProfiles.ProfileSavePath
        sd.Filter = "Epic Macro Profile|*.epicmcr"
        If sd.ShowDialog() = DialogResult.OK Then

            MacroProfiles.SaveSettingToProfile(IO.Path.GetFileNameWithoutExtension(sd.FileName), "EpicMacroCommands", MacroCommandBox.Text)

            MacroTable.ClearSelection()
            PopulateMacroList()
            UpdateCommandBox()
        End If

    End Sub

    Private Sub MacroTable_SelectionChanged(sender As Object, e As EventArgs) Handles MacroTable.SelectionChanged

        If DontRefresh = False Then

            Dim ID As String

            If MacroTable.SelectedRows.Count > 0 Then
                ID = MacroTable.SelectedRows.Item(0).Index
                MacroCommandBox.Text = MacroProfiles.GetSettingValue(ProfileList.Item(ID).Name, "EpicMacroCommands")
                UpdateCommandBox()
            Else
                MacroCommandBox.Text = ""
                UpdateCommandBox()
            End If

        End If

    End Sub

    Private Sub EpicMacroMainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            My.Settings.LastProfileID = MacroTable.SelectedRows.Item(0).Index
        Catch ex As Exception
            My.Settings.LastProfileID = -1
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        MacroCommandBox.Text = ""
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        Try
            IO.File.Delete(ProfileList.Item(MacroTable.SelectedRows.Item(0).Index).Path)
        Catch ex As Exception
        End Try

        MacroTable.ClearSelection()
        PopulateMacroList()
        UpdateCommandBox()

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim od As New VistaFolderBrowserDialog
        od.SelectedPath = My.Settings.MacroFilePath & "\"
        od.ShowDialog()


        If od.SelectedPath <> My.Settings.MacroFilePath & "\" Then
            My.Settings.MacroFilePath = od.SelectedPath

        End If

        PopulateMacroList()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim webAddress As String = "http://www.daina-el.lv/"
        Process.Start(webAddress)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim webAddress As String = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=GHEQRK6HTAQTL"
        Process.Start(webAddress)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub CheckBox_SDI_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_SDI.CheckedChanged
        My.Settings.S_SDI = sender.checked
    End Sub

    Private Sub EpicMacroMainForm_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        Dim ThisDWGFile As New DWGFile

        If IsNothing(gv.DWGFiles) Then
            gv.DWGFiles = New List(Of DWGFile)
        End If

        For Each path In files



            ''MsgBox(IO.Path.GetExtension(path))

            If System.IO.Directory.Exists(path) Then
                'MsgBox("it's a dir")
                Dim DWGfiles() As String
                DWGfiles = IO.Directory.GetFiles(path, "*.dwg")

                For i = 0 To DWGfiles.Count - 1
                    ThisDWGFile = New DWGFile
                    ThisDWGFile.FilePath = DWGfiles(i)

                    If Regex.IsMatch(ThisDWGFile.FilePath, "[ēŗūīōāšģķļžčņĒŖŪĪŌĀŠĢĶĻŽČŅ]") Then
                        MsgBox("Faila nosaukumā vai adresē nedrīkst būt garumzīmes vai mīkstinājumi! " & Chr(10) & "Ielāde atcelta.")
                        Close()
                    End If

                    ThisDWGFile.ID = gv.LastFileID
                    gv.DWGFiles.Add(ThisDWGFile)
                    gv.LastFileID = gv.LastFileID + 1
                Next

            ElseIf System.IO.File.Exists(path) Then
                ''MsgBox("it's a file")
                If Strings.UCase(IO.Path.GetExtension(path)) = ".DWG" Then
                    ThisDWGFile = New DWGFile
                    ThisDWGFile.FilePath = path
                    ThisDWGFile.ID = gv.LastFileID

                    If Regex.IsMatch(ThisDWGFile.FilePath, "[ēŗūīōāšģķļžčņĒŖŪĪŌĀŠĢĶĻŽČŅ]") Then
                        MsgBox("Faila nosaukumā vai adresē nedrīkst būt garumzīmes vai mīkstinājumi! " & Chr(10) & "Ielāde atcelta.")
                        Close()
                    End If

                    gv.DWGFiles.Add(ThisDWGFile)
                    gv.LastFileID = gv.LastFileID + 1
                End If

                If Strings.UCase(IO.Path.GetExtension(path)) = ".EPICFL" Then

                    Dim lines() As String = System.IO.File.ReadAllLines(path)

                    For n = 0 To lines.Count - 1
                        If Strings.Trim(lines(n)) <> "" Then
                            If IO.File.Exists(lines(n)) Then
                                ThisDWGFile = New DWGFile
                                ThisDWGFile.FilePath = lines(n)
                                ThisDWGFile.ID = gv.LastFileID

                                If Regex.IsMatch(ThisDWGFile.FilePath, "[ēŗūīōāšģķļžčņĒŖŪĪŌĀŠĢĶĻŽČŅ]") Then
                                    MsgBox("Faila nosaukumā vai adresē nedrīkst būt garumzīmes vai mīkstinājumi! " & Chr(10) & "Ielāde atcelta.")
                                    Close()
                                End If

                                gv.DWGFiles.Add(ThisDWGFile)
                                gv.LastFileID = gv.LastFileID + 1
                            End If

                        End If
                    Next


                End If
            End If


            '' MsgBox(path)
        Next
        UpdateFiletable()
    End Sub

    Private Sub EpicMacroMainForm_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub CheckBox_LayoutLoop_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_LayoutLoop.CheckedChanged
        My.Settings.S_LayoutLoop = sender.checked
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim Sd As New SaveFileDialog
        Dim FileListPath As String

        If IsNothing(gv.DWGFiles) Then
            MsgBox("Nav pievienoti DWG faili.")
            Exit Sub
        End If

        ''Sd.InitialDirectory = MacroProfiles.ProfileSavePath
        Sd.Filter = "Epic Macro File List|*.epicfl"
        Sd.ShowDialog()

        FileListPath = Sd.FileName

        If FileListPath <> "" Then
            File.Create(FileListPath).Dispose()

            Dim tmpLines(0) As String
            Dim ArrCounter As Integer

            For i = 0 To gv.DWGFiles.Count - 1

                tmpLines(ArrCounter) = gv.DWGFiles.Item(i).FilePath
                ArrCounter = ArrCounter + 1
                Array.Resize(tmpLines, ArrCounter + 1)

                If i < gv.DWGFiles.Count - 1 Then
                    Array.Resize(tmpLines, ArrCounter + 1)
                End If


            Next

            System.IO.File.WriteAllLines(FileListPath, tmpLines)

        Else
            '' MsgBox("canceled")
        End If



    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        'If FileTable.SelectedRows.Count > 0 Then
        '    gv.DWGFiles.RemoveAt(FileTable.SelectedRows.Item(0).Index)
        '    UpdateFiletable()
        'End If

        Dim SelecedID As Integer
        Dim SavedID As Integer


        If FileTable.SelectedRows.Count > 0 Then

            For i = 0 To FileTable.SelectedRows.Count - 1
                SelecedID = FileTable.SelectedRows.Item(i).Cells(1).Value

                For savedindex = 0 To gv.DWGFiles.Count - 1
                    SavedID = gv.DWGFiles.Item(savedindex).ID
                    If SelecedID = SavedID Then
                        gv.DWGFiles.RemoveAt(savedindex)
                        Exit For
                    End If
                Next
            Next
            UpdateFiletable()
        End If



    End Sub

    Private Sub MacroDescription_DoubleClick(sender As Object, e As EventArgs) Handles MacroDescription.DoubleClick
        MacroDescription.ReadOnly = False
        MacroDescription.Refresh()
        ''MsgBox("test")
    End Sub

    Private Sub MacroDescription_TextChanged(sender As Object, e As EventArgs) Handles MacroDescription.TextChanged

    End Sub

    Function ActiveAutoCADTitle() As String
        Dim result As String

        Try
            result = LastAutoCADProcess.MainWindowTitle
            Return result

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Sub Button12_Click(sender As Object, e As EventArgs)
        initDDE()
    End Sub

    Private Sub ShowNews_CheckedChanged(sender As Object, e As EventArgs) Handles ShowNews.CheckedChanged
        My.Settings.ShowNews = sender.checked


        If sender.checked = True And LoadLock = False Then
            ThisNewsBox.ShowDialog()
        End If

    End Sub

    Private Sub Button12_Click_1(sender As Object, e As EventArgs) Handles Button12.Click
        Dim d As New CustomComSettings
        d.ShowDialog()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs)
        initDDE()

        Cust_EpicSplit.EpicSplitToFiles("C:\Epic\test")

        ''CloseDDE()
    End Sub

    Private Sub Button13_Click_1(sender As Object, e As EventArgs) Handles Button13.Click
        ''initDDE()

        ShadowEng.StartShadows()
        Cust_EpicPurge.EpicPurge2("C:\Users\User\Desktop\Purgeeest\2017-117_EL-PL-35-1.dwg")

        ''Cust_EpicSplit.EpicSplitToFiles("C:\Epic\test")
        ''CloseDDE()
    End Sub

    Private Sub EpicMacroMainForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If isAllowedToRun() = False Then
            MsgBox("Autorizācija neveiksmīga. Programmu nav atļauts izmantot.")
            Me.Close()
        End If
    End Sub
End Class
