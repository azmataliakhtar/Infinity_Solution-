
Imports INF.Web.UI
Imports INF.Web.UI.UserRights

Namespace Admin

    Partial Class MainNavigation
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

            '<div id="nav" class="clearfix">
            '        <ul>
            '            <li><a href="Settings.aspx">Settings</a></li>
            '            <li><a href="Category.aspx">Category</a></li>
            '            <li><a href="Customers.aspx">Customers</a></li>
            '            <li><a href="ReportSettings.aspx">Reporting</a></li>
            '        </ul>
            '    </div>

            'Dim divWrapper As New HtmlGenericControl("div")
            'divWrapper.ID = "nav"
            'divWrapper.Attributes.Add("class", "admin-main-nav clearfix")

            Dim ul As New HtmlGenericControl("ul")
            ul.Attributes.Add("class", "nav navbar-nav navbar-right")
            For Each userRight As PageUserRight In PageUserRights
                If Not IsNothing(userRight) Then

                    Dim li As New HtmlGenericControl("li")
                    Dim anchor As New HtmlGenericControl("a")
                    anchor.Attributes.Add("href", userRight.PageID)
                    anchor.InnerText = userRight.Title
                    li.Controls.Add(anchor)
                    ul.Controls.Add(li)
                End If
            Next

            'divWrapper.Controls.Add(ul)
            'phMainNavigation.Controls.Add(divWrapper)
            phMainNavigation.Controls.Add(ul)
        End Sub
    End Class
End Namespace