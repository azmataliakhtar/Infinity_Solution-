Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI.UserRights
Imports INF.Web.UI

Namespace Admin
    Public Class DashboardMaster
        Inherits System.Web.UI.MasterPage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim ucMainNavigation As UcSidebarNavigation = TryCast(LoadControl("~/Admin/UcSidebarNavigation.ascx"), UcSidebarNavigation)
            If Not IsNothing(ucMainNavigation) Then
                Dim userRights As List(Of PageUserRight) = AdminPanelMenus.DefinePageUserRights()
                Dim loggedInUser As CsUser = GetLoggedInUser()
                If IsNothing(loggedInUser) Then Return

                Dim mainPageUserRights As List(Of PageUserRight) = (From urp In userRights
                                                                        Where [String].Equals("Dashboard.aspx", (urp.ParentPageID), StringComparison.CurrentCultureIgnoreCase) _
                                                                            AndAlso urp.UserRole = loggedInUser.UserRole _
                                                                            AndAlso urp.Rights.AllowView = True
                                                                    ).ToList()

                ucMainNavigation.PageUserRights = mainPageUserRights
                SettingsPlaceHolder.Controls.Add(ucMainNavigation)
            End If
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