Public Class Cust_EpicSplitW

    Function GetFrameTagList() As List(Of String)
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim tmpTags As String

        ''Me.BringToFront()

        tmpTags = My.Settings.S_RakstlTags
        My.Settings.S_RakstlTags = SettingRakstlTags.Text

        Dim taglist As List(Of String)
        taglist = GetFrameTagList()

        Dim duplicates = taglist.GroupBy(Function(i) i) _
                            .Where(Function(g) g.Count() > 1) _
                            .[Select](Function(g) g.Key)

        If duplicates.Count > 0 Then
            MsgBox("Rakstlaukuma nr TAG sarakstā ir divi vai vairāki vienādi ieraksti. Lūdzu izlabot, lai katrā rindiņā būtu tikai unikāli ieraksti!" & Chr(10) & "Izmaiņas TAG sarakstā atceltas.")
            SettingRakstlTags.Text = tmpTags
            My.Settings.S_RakstlTags = tmpTags
            ''  Exit Sub
        End If

        Try
            My.Settings.S_FramePosDelta = CDbl(FramePosDelta.Text)
        Catch ex As Exception
            MsgBox("Nepareizs formāts!!")
            My.Settings.S_FramePosDelta = 100
            FramePosDelta.Text = "100"
        End Try

        Try
            My.Settings.S_DeltaWin = CDbl(TB_DeltaWin.Text)
        Catch ex As Exception
            MsgBox("Nepareizs formāts!!")
            My.Settings.S_DeltaWin = 5
            TB_DeltaWin.Text = "5"
        End Try

        Close()

    End Sub

    Private Sub SettingRakstlTags_TextChanged(sender As Object, e As EventArgs) Handles SettingRakstlTags.TextChanged

    End Sub

    Private Sub CustomComOpt_Split_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Strings.Trim(My.Settings.S_RakstlTags) <> "" Then
            SettingRakstlTags.Text = My.Settings.S_RakstlTags
        End If

        If My.Settings.S_FramePosDelta = 0 Then My.Settings.S_FramePosDelta = 100
        FramePosDelta.Text = My.Settings.S_FramePosDelta

        If My.Settings.S_DeltaWin = 0 Then My.Settings.S_DeltaWin = 5
        TB_DeltaWin.Text = My.Settings.S_DeltaWin

    End Sub
End Class