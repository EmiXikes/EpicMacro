Public Class CustomAttrResult
    Private Sub CustomAttrResult_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.DataSource = EpicMacroMainForm.CustomAttrInfo
    End Sub
End Class