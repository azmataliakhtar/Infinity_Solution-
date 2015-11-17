Imports INF.Web.UI
Imports INF.Web.UI.Settings
Imports INF.Web.UI.Utils
Imports System.IO

Namespace UserControls
    Public Class LookUpPostcode
        Inherits BaseUserControl

        Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
            If IsNothing(phPostcodeLookUp) Then
                Exit Sub
            End If

            phPostcodeLookUp.Controls.Clear()
            
            Dim virtualPathDefault As String = "~/public/" + WebsiteConfig.Instance.INFTheme + "/LookUpPostcode.ascx"
            Dim physicalPathDefault As String = HttpContext.Current.Server.MapPath(virtualPathDefault)
            If Not File.Exists(physicalPathDefault) Then
                virtualPathDefault = "~/public/default/LookUpPostcode.ascx"
            End If

            Dim defaultPage As UserControl = LoadControl(virtualPathDefault)
            If Not IsNothing(defaultPage) Then
                phPostcodeLookUp.Controls.Add(defaultPage)
            End If
            
        End Sub

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        End Sub

    End Class
End Namespace