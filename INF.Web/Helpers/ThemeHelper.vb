Imports INF.Web.UI
Imports System.IO

Namespace Helpers
    Public Class ThemeHelper

        Public Shared Function GetThemes() As List(Of Theme)
            Dim themesPath As String = System.Web.HttpContext.Current.Server.MapPath("~/App_Themes")
            Dim dirInfo As DirectoryInfo = New DirectoryInfo(themesPath)

            Dim dirs() As DirectoryInfo = dirInfo.GetDirectories()
            Return (From themeDir In dirs Select New Theme(themeDir.Name) With {.DisplayName = themeDir.Name}).ToList()
        End Function
    End Class
End Namespace