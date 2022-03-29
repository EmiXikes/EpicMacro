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

Public Class Cust_EpicPurge


    Public Shared Function EpicPurge(ByVal FilePath As String)
        Dim com As String
        Dim esc As String = Chr(27)
        Dim er As String = Chr(10)
        Dim ReadCheck As String


        Dim EofFilePath As String = Path.Combine(My.Application.Info.DirectoryPath, "Macro_EOF.txt")
        Dim OutputFileDirectory = Path.GetDirectoryName(FilePath)

        Dim DxfFileName As String = Path.Combine(OutputFileDirectory, "_tempFile.dxf")

        ''DxfOut
        If File.Exists(DxfFileName) Then
            Try
                File.Delete(DxfFileName)
            Catch ex As Exception
                DxfFileName = FileExistIncrementer(DxfFileName)
            End Try

        End If

        DxfFileName = CustomMacroCommands.Export2DXF_ToPath(DxfFileName)

        ''Close
        AcadMacroClose.Save = False
        AcadMacroClose.ClosedMarker = "MACRO_EOF"
        AcadMacroEOF.NacroEOFValue = "fileclosed_" & CADHelper.FileOPS.RandomString(10)
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "CLXX" & vbLf)

        Do Until ReadCheck = AcadMacroEOF.NacroEOFValue
            Thread.Sleep(500)
            Try
                ReadCheck = System.IO.File.ReadAllLines(EofFilePath)(0)
            Catch ex As Exception

            End Try

        Loop

        ''Opendxf
        AcadMacroEOF.NacroEOFValue = "openready" & CADHelper.FileOPS.RandomString(10)
        AcadMacroOpen.DWGFileName(ShadowEng.ShadX.ids(0)) = CADHelper.FileOPS.OpenScript(DxfFileName, "MACRO_EOF")
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "oppp" & vbLf)

        Do Until ReadCheck = AcadMacroEOF.NacroEOFValue
            Thread.Sleep(500)
            Try
                ReadCheck = System.IO.File.ReadAllLines(EofFilePath)(0)
            Catch ex As Exception

            End Try

        Loop

        ''Purge
        com = "-purge" & er & "all" & er & "*" & er & "n" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        ''Delete original
        If File.Exists(FilePath) Then
            Try
                File.Delete(FilePath)
            Catch ex As Exception
                FilePath = FileExistIncrementer(FilePath)
            End Try
        End If

        ''SaveAs
        AcadMacroSaveAs.SaveAsFilePath = Path.Combine(FilePath)
        AcadMacroSaveAs.FileReadyMarker = "FileSaved_" & CADHelper.FileOPS.RandomString(10)
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "xxSaveAs" & vbLf)

        Do Until ReadCheck = AcadMacroSaveAs.FileReadyMarker
            Try
                Thread.Sleep(100)
                ReadCheck = System.IO.File.ReadAllLines(EofFilePath)(0)
            Catch ex As Exception
            End Try
        Loop

        If File.Exists(DxfFileName) Then
            Try
                File.Delete(DxfFileName)
            Catch ex As Exception

            End Try

        End If


        'com = "rea" & er
        'ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

    End Function


    Public Shared Function EpicPurge2(ByVal FilePath As String)
        Dim com As String
        Dim esc As String = Chr(27)
        Dim er As String = Chr(10)
        Dim ReadCheck As String


        Dim EofFilePath As String = Path.Combine(My.Application.Info.DirectoryPath, "Macro_EOF.txt")
        Dim OutputFileDirectory = Path.GetDirectoryName(FilePath)

        Dim DxfFileName As String = Path.Combine(OutputFileDirectory, "_tempFile.dxf")

        Dim DwgTempFile As String = Path.Combine(OutputFileDirectory, "_tempFile.dwg")

        ''Delete temp
        If File.Exists(DwgTempFile) Then
            Try
                File.Delete(DwgTempFile)
            Catch ex As Exception
                DwgTempFile = FileExistIncrementer(DwgTempFile)
            End Try
        End If

        ''SaveAs
        AcadMacroSaveAs.SaveAsFilePath = Path.Combine(DwgTempFile)
        AcadMacroSaveAs.FileReadyMarker = "FileSaved_" & CADHelper.FileOPS.RandomString(10)
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "xxSaveAs" & vbLf)

        Do Until ReadCheck = AcadMacroSaveAs.FileReadyMarker
            Try
                Thread.Sleep(100)
                ReadCheck = System.IO.File.ReadAllLines(EofFilePath)(0)
            Catch ex As Exception
            End Try
        Loop


        ''Delete original
        If File.Exists(FilePath) Then
            Try
                File.Delete(FilePath)
            Catch ex As Exception
                FilePath = FileExistIncrementer(FilePath)
            End Try
        End If

        ''Export
        com = "-exporttoautocad" & er &
            "f" & er &
            "2010" & er &
            "B" & er &
            "N" & er & er &
            Chr(34) & FilePath & Chr(34) & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)
        AcadMacroEOF.NacroEOFValue = "exportready_" & CADHelper.FileOPS.RandomString(10)
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "MACRO_EOF" & vbLf)

        Do Until ReadCheck = AcadMacroEOF.NacroEOFValue
            Thread.Sleep(500)
            Try
                ReadCheck = System.IO.File.ReadAllLines(EofFilePath)(0)
            Catch ex As Exception

            End Try

        Loop


        ''Close
        AcadMacroClose.Save = False
        AcadMacroClose.ClosedMarker = "MACRO_EOF"
        AcadMacroEOF.NacroEOFValue = "fileclosed_" & CADHelper.FileOPS.RandomString(10)
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "CLXX" & vbLf)

        Do Until ReadCheck = AcadMacroEOF.NacroEOFValue
            Thread.Sleep(500)
            Try
                ReadCheck = System.IO.File.ReadAllLines(EofFilePath)(0)
            Catch ex As Exception

            End Try

        Loop

        ''Open New Exported
        AcadMacroEOF.NacroEOFValue = "openready" & CADHelper.FileOPS.RandomString(10)
        AcadMacroOpen.DWGFileName(ShadowEng.ShadX.ids(0)) = CADHelper.FileOPS.OpenScript(FilePath, "MACRO_EOF")
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "oppp" & vbLf)

        Do Until ReadCheck = AcadMacroEOF.NacroEOFValue
            Thread.Sleep(500)
            Try
                ReadCheck = System.IO.File.ReadAllLines(EofFilePath)(0)
            Catch ex As Exception

            End Try

        Loop

        ''Purge
        com = "-purge" & er & "all" & er & "*" & er & "n" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        ''Delete temp
        If File.Exists(DwgTempFile) Then
            Try
                File.Delete(DwgTempFile)
            Catch ex As Exception

            End Try
        End If


    End Function

    Public Shared Function EpicPurge3(ByVal FilePath As String)
        Dim com As String
        Dim esc As String = Chr(27)
        Dim er As String = Chr(10)
        Dim ReadCheck As String


        Dim EofFilePath As String = Path.Combine(My.Application.Info.DirectoryPath, "Macro_EOF.txt")
        Dim OutputFileDirectory = Path.GetDirectoryName(FilePath)

        Dim DxfFileName As String = Path.Combine(OutputFileDirectory, "_tempFile.dxf")

        Dim DwgTempFile As String = Path.Combine(OutputFileDirectory, "_tempFile.dwg")

        ''Delete temp
        If File.Exists(DwgTempFile) Then
            Try
                File.Delete(DwgTempFile)
            Catch ex As Exception
                DwgTempFile = FileExistIncrementer(DwgTempFile)
            End Try
        End If

        ''SaveAs
        AcadMacroSaveAs.SaveAsFilePath = Path.Combine(DwgTempFile)
        AcadMacroSaveAs.FileReadyMarker = "FileSaved_" & CADHelper.FileOPS.RandomString(10)
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "xxSaveAs" & vbLf)

        Do Until ReadCheck = AcadMacroSaveAs.FileReadyMarker
            Try
                Thread.Sleep(100)
                ReadCheck = System.IO.File.ReadAllLines(EofFilePath)(0)
            Catch ex As Exception
            End Try
        Loop


        ''Delete original
        If File.Exists(FilePath) Then
            Try
                File.Delete(FilePath)
            Catch ex As Exception
                FilePath = FileExistIncrementer(FilePath)
            End Try
        End If

        ''Export
        com = "-exporttoautocad" & er &
            "f" & er &
            "2010" & er &
            "B" & er &
            "Y" & er & er &
            Chr(34) & FilePath & Chr(34) & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)
        AcadMacroEOF.NacroEOFValue = "exportready_" & CADHelper.FileOPS.RandomString(10)
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "MACRO_EOF" & vbLf)

        Do Until ReadCheck = AcadMacroEOF.NacroEOFValue
            Thread.Sleep(500)
            Try
                ReadCheck = System.IO.File.ReadAllLines(EofFilePath)(0)
            Catch ex As Exception

            End Try

        Loop


        ''Close
        AcadMacroClose.Save = False
        AcadMacroClose.ClosedMarker = "MACRO_EOF"
        AcadMacroEOF.NacroEOFValue = "fileclosed_" & CADHelper.FileOPS.RandomString(10)
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "CLXX" & vbLf)

        Do Until ReadCheck = AcadMacroEOF.NacroEOFValue
            Thread.Sleep(500)
            Try
                ReadCheck = System.IO.File.ReadAllLines(EofFilePath)(0)
            Catch ex As Exception

            End Try

        Loop

        ''Open New Exported
        AcadMacroEOF.NacroEOFValue = "openready" & CADHelper.FileOPS.RandomString(10)
        AcadMacroOpen.DWGFileName(ShadowEng.ShadX.ids(0)) = CADHelper.FileOPS.OpenScript(FilePath, "MACRO_EOF")
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), "oppp" & vbLf)

        Do Until ReadCheck = AcadMacroEOF.NacroEOFValue
            Thread.Sleep(500)
            Try
                ReadCheck = System.IO.File.ReadAllLines(EofFilePath)(0)
            Catch ex As Exception

            End Try

        Loop

        ''Purge
        com = "-purge" & er & "all" & er & "*" & er & "n" & er
        ShadowEng.SendShadows(ShadowEng.ShadX.ids(0), com)

        ''Delete temp
        If File.Exists(DwgTempFile) Then
            Try
                File.Delete(DwgTempFile)
            Catch ex As Exception

            End Try
        End If


    End Function


End Class
