Imports INF.Web.UI

Namespace Admin

    Public Class Dashboard
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Response.RedirectTo("TodayOrders.aspx")
        End Sub

    End Class
End Namespace