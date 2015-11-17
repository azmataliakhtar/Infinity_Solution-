
Imports INF.Web.UI

Namespace Admin
    Partial Class Reporting
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Response.RedirectTo("BasicReport.aspx")
        End Sub
    End Class
End Namespace