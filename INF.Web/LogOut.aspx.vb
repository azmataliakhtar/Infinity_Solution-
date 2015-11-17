Imports INF.Web.UI

Public Class LogOut
    Inherits EPAPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FormsAuthentication.SignOut()
        HttpContext.Current.Session("LoggedInUser") = Nothing
        Response.RedirectTo("Login.aspx")
    End Sub

End Class