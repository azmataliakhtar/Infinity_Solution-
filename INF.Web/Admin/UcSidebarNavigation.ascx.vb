Imports INF.Web.UI
Imports INF.Web.UI.UserRights

Namespace Admin
    Public Class UcSidebarNavigation
        Inherits BaseUserControl

        Public Sub New()
        End Sub

        Public Sub New(vPageUserRights As List(Of PageUserRight))
            PageUserRights = vPageUserRights
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            LoadMenuItemsCorrespondToUserRole()
        End Sub

        Private Sub LoadMenuItemsCorrespondToUserRole()
            If IsNothing(PageUserRights) Then
                Return
            End If

            Dim ul As New HtmlGenericControl("ul")
            ul.Attributes.Add("class", "nav nav-sidebar")

            For Each userRight As PageUserRight In PageUserRights
                If Not IsNothing(userRight) AndAlso userRight.RenderAsNavigationItem Then

                    Dim li As New HtmlGenericControl("li")
                    Dim anchor As New HtmlGenericControl("a")
                    anchor.Attributes.Add("href", userRight.PageID)
                    anchor.InnerText = userRight.Title
                    li.Attributes.Add("onclick", "$(this).toggleClass('active');")
                    li.Attributes.Add("class", "active")
                    li.Controls.Add(anchor)
                    ul.Controls.Add(li)
                End If
            Next

            phSidebarNavItems.Controls.Add(ul)
        End Sub
    End Class
End Namespace