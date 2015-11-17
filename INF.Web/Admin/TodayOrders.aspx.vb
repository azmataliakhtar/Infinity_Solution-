Imports System.Globalization
Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI
Imports INF.Web.Data.DAL.SqlClient

Namespace Admin
    Partial Class TodayOrders
        Inherits AdminPage

        Public _startDate As String = Date.Now.Month & "/" & Date.Now.Day & "/" & Date.Now.Year

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                StartDateOnTextBox.Text = _startDate
                EndDateOnTextBox.Text = _startDate
                LoadTodaysOrders()
            End If
        End Sub

        Private Sub LoadTodaysOrders()
            Dim ukCulture As CultureInfo = New CultureInfo("en-US")
            Dim startDate As DateTime = DateTime.Parse(StartDateOnTextBox.Text, ukCulture.DateTimeFormat)
            Dim endDate As DateTime = DateTime.Parse(EndDateOnTextBox.Text, ukCulture.DateTimeFormat)

            Dim listOrders = OrderProvider.Instance.GetOrdersToDays(startDate, endDate)

            If Not IsNothing(listOrders) Then
                ltrNumberOfOrders.Text = listOrders.Count.ToString()
                OrderDataGrid.DataSource = listOrders
                OrderDataGrid.DataBind()
            Else
                ltrNumberOfOrders.Text = "0"
            End If
        End Sub

        Protected Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
            LoadTodaysOrders()
        End Sub

        Protected Sub OrderDataGrid_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles OrderDataGrid.PageIndexChanged
            If e.NewPageIndex < OrderDataGrid.PageCount AndAlso e.NewPageIndex >= 0 Then
                OrderDataGrid.CurrentPageIndex = e.NewPageIndex
            End If
            LoadTodaysOrders()
        End Sub
    End Class
End Namespace