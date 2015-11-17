
Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI.UserRights
Imports INF.Web.UI

Namespace Admin

    Partial Class MasterPage
        Inherits System.Web.UI.MasterPage

        'Protected Sub LogOutLink_Click(sender As Object, e As System.EventArgs) Handles LogOutLink.Click
        '    FormsAuthentication.SignOut()
        '    FormsAuthentication.RedirectToLoginPage()
        'End Sub

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            'If Not Page.IsPostBack Then
            Dim ucMainNavigation As MainNavigation = TryCast(LoadControl("~/Admin/MainNavigation.ascx"), MainNavigation)
            If Not IsNothing(ucMainNavigation) Then
                Dim userRights As List(Of PageUserRight) = AdminPanelMenus.DefinePageUserRights()
                Dim loggedInUser As CsUser = GetLoggedInUser()
                If IsNothing(loggedInUser) Then Return

                Dim mainPageUserRights As List(Of PageUserRight) = (From urp In userRights
                        Where String.IsNullOrWhiteSpace(urp.ParentPageID) AndAlso urp.UserRole = loggedInUser.UserRole AndAlso urp.Rights.AllowView = True
                        ).ToList()

                ucMainNavigation.PageUserRights = mainPageUserRights
                phMainNavigation.Controls.Add(ucMainNavigation)
            End If
            'End If
        End Sub

        Protected Function GetLoggedInUser() As CsUser
            Dim identity As FormsIdentity = TryCast(HttpContext.Current.User.Identity, FormsIdentity)
            If identity IsNot Nothing Then
                Dim userName As String = String.Empty
                If (identity IsNot Nothing) Then
                    userName = identity.Ticket.Name
                End If

                If Not (HttpContext.Current.User.Identity.IsAuthenticated) Then
                    Response.RedirectTo("~/Admin/Login.aspx")
                End If

                Dim userBusiness As New UserBusinessLogic
                Return userBusiness.GetUser(userName)
            End If
            Return Nothing
        End Function
    End Class
End Namespace