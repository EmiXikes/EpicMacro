Imports EpicGrind.CustomMacroCommands

Module GrindItems
    Public Class gv
        Public Shared isReloadSettings As Boolean
        Public Shared DWGFiles As List(Of DWGFile)
        Public Shared LastFileID As Integer
        Public Shared MacroList As List(Of String)
        Public Shared LastMacroID As Integer

        'Public Shared PlotPages As List(Of PlotPage)
        'Public Shared LastID As Integer

    End Class

    Public Class DWGFile
        Public ID As Integer
        Public FilePath As String
        Public isDone As Boolean

    End Class
End Module
