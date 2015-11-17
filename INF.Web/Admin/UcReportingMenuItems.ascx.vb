Imports INF.Web.UI
Imports INF.Web.UI.UserRights

Namespace Admin

    Partial Class UcReportingMenuItems
        Inherits BaseUserControl

        Public Sub New()

        End Sub

        Public Sub New(vPageUserRights As List(Of PageUserRight))
            PageUserRights = vPageUserRights
        End Sub

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            'If Not Page.IsPostBack Then
            LoadMenuItemsCorrespondToUserRole()
            'End If
        End Sub

        Private Sub LoadMenuItemsCorrespondToUserRole()
            If IsNothing(PageUserRights) Then Return

            'Dim divWrapper As New HtmlGenericControl("div")
            'divWrapper.ID = "left_nav"
            'divWrapper.Attributes.Add("class", "settings-block block")

            'Dim header As New HtmlGenericControl("h2")
            'header.InnerText = "Reporting"
            'divWrapper.Controls.Add(header)

            Dim ul As New HtmlGenericControl("ul")
            ul.Attributes.Add("class", "nav nav-sidebar")
            For Each userRight As PageUserRight In PageUserRights
                If Not IsNothing(userRight) Then

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

            'divWrapper.Controls.Add(ul)
            'phReportinMenuItems.Controls.Add(divWrapper)

            phReportinMenuItems.Controls.Add(ul)
        End Sub
    End Class
End Namespace