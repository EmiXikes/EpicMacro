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

Public Class CustWBlock

    Public Shared Function EpicWBlock(ByVal SavePath As String) As Integer

        SaveAs(FileExistIncrementer(Path.Combine(Path.GetDirectoryName(SavePath), "__oldFile.dwg")))
        WBlock("", SavePath)

    End Function

End Class
