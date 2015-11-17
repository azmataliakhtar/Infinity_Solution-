
Imports INF.Web.UI

Namespace Admin
    Partial Class LogOut
        Inherits Page
        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            FormsAuthentication.SignOut()
            'FormsAuthentication.RedirectToLoginPage()
            Response.RedirectTo("LogIn.aspx")
        End Sub
    End Class
End Namespace