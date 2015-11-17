
Imports INF.Web.UI
Imports INF.Web.UI.UserRights

Namespace Admin

    Partial Class GeneralSettingsUserControl
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

            '<div class="settings-block block">
            '    <h2>General Settings</h2>
            '    <ul>
            '        <li><a href="Info.aspx">Website Information</a></li>
            '        <li><a href="Settings.aspx">Restaurant Settings</a></li>
            '        <li><a href="Themes.aspx">Themes</a></li>
            '        <li><a href="StaticPages.aspx">Static Pages</a></li>
            '        <li><a href="Users.aspx">System Users</a></li>
            '    </ul>
            '</div>

            ''panel
            'Dim divWrapper As New HtmlGenericControl("div")
            'divWrapper.ID = "left_nav"
            'divWrapper.Attributes.Add("class", "panel panel-primary")

            ''panel->panel-heading
            'Dim divHeading As New HtmlGenericControl("div")
            'divHeading.Attributes.Add("class", "panel-heading")

            ''panel->panel-heading->panel-title
            'Dim header As New HtmlGenericControl("h3")
            'header.Attributes.Add("class", "panel-title")
            'header.InnerText = "General Settings"
            'divHeading.Controls.Add(header)

            'divWrapper.Controls.Add(divHeading)

            ''panel->panel-body
            'Dim divBody As New HtmlGenericControl("div")
            'divBody.Attributes.Add("class", "panel-body")
            'divBody.Attributes.Add("style", "padding-left:2px;padding-right:2px;")
            ''panel->panel-body->ul
            ''Dim ul As New HtmlGenericControl("ul")

            'Dim divListGroup As New HtmlGenericControl("div")
            'divListGroup.Attributes.Add("class", "list-group")

            'For Each userRight As PageUserRight In PageUserRights
            '    If Not IsNothing(userRight) Then

            '        'Dim li As New HtmlGenericControl("li")
            '        Dim anchor As New HtmlGenericControl("a")
            '        anchor.Attributes.Add("href", userRight.PageID)
            '        anchor.InnerText = userRight.Title
            '        anchor.Attributes.Add("class", "list-group-item")
            '        divListGroup.Controls.Add(anchor)
            '        'li.Controls.Add(anchor)
            '        'ul.Controls.Add(li)
            '    End If
            'Next

            'divBody.Controls.Add(divListGroup)
            'divWrapper.Controls.Add(divBody)

            'phGeneralSettingsMenuItems.Controls.Add(divWrapper)

            'panel
            'Dim divWrapper As New HtmlGenericControl("div")
            'divWrapper.ID = "left_nav"
            'divWrapper.Attributes.Add("class", "panel panel-primary")

            ''panel->panel-heading
            'Dim divHeading As New HtmlGenericControl("div")
            'divHeading.Attributes.Add("class", "panel-heading")

            ''panel->panel-heading->panel-title
            'Dim header As New HtmlGenericControl("h3")
            'header.Attributes.Add("class", "panel-title")
            'header.InnerText = "General Settings"
            'divHeading.Controls.Add(header)

            'divWrapper.Controls.Add(divHeading)

            ''panel->panel-body
            'Dim divBody As New HtmlGenericControl("div")
            'divBody.Attributes.Add("class", "panel-body bg-warning")

            ''panel->panel-body->ul
            'Dim ul As New HtmlGenericControl("ul")
            'ul.Attributes.Add("class", "nav nav-pills nav-stacked")
            'ul.Attributes.Add("style", "max-width: 200px;")
            'For Each userRight As PageUserRight In PageUserRights
            '    If Not IsNothing(userRight) Then

            '        Dim li As New HtmlGenericControl("li")
            '        Dim anchor As New HtmlGenericControl("a")
            '        anchor.Attributes.Add("href", userRight.PageID)
            '        anchor.InnerText = userRight.Title
            '        li.Attributes.Add("style", "width:100%;")
            '        li.Attributes.Add("onclick", "$(this).toggleClass('active');")
            '        li.Controls.Add(anchor)
            '        ul.Controls.Add(li)
            '    End If
            'Next

            'divBody.Controls.Add(ul)
            'divWrapper.Controls.Add(divBody)
            'phGeneralSettingsMenuItems.Controls.Add(divWrapper)

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

            phGeneralSettingsMenuItems.Controls.Add(ul)
        End Sub
    End Class
End Namespace