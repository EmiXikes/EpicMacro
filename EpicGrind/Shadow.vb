Imports CADHelper

Public Class ShadowEng
    Public Shared ShadX As New ShadowEngine
    Public Shared Sub StartShadows()
        If IsNothing(ShadX.logReader) Then
            ShadX.Start()
        Else
            ShadX.logReader.StopPolling = False
        End If
    End Sub

    Public Shared Sub SendShadows(ByVal DDE_ID As Object, ByVal com As String)
        ShadX.ddeClient.Send(ShadX.ids(0), com)
    End Sub

End Class
