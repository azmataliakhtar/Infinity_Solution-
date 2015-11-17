Imports INF.Web.UI

Namespace Admin
    Partial Class GeneralSettings
        Inherits AdminPage

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            Response.RedirectTo("Themes.aspx")
        End Sub
    End Class
End Namespace