
Imports INF.Web.Data.BLL
Imports INF.Web.Data.DAL.SqlClient
Imports INF.Web.Data.Entities
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Namespace Admin
    Partial Class CustomerOrders
        Inherits AdminPage

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                InitializeFilter()
                LoadCustomerInfo(Me.CustomerId)
                LoadAllOrdersInWeek(Me.CustomerId)
            End If
        End Sub

        Private Sub InitializeFilter()
            TimePeriodOptions.Items.Add(New ListItem() With {.Value = "0", .Text = "- Anytime -"})
            TimePeriodOptions.Items.Add(New ListItem() With {.Value = "1", .Text = "Today"})
            TimePeriodOptions.Items.Add(New ListItem() With {.Value = "7", .Text = "In Week"})
            TimePeriodOptions.Items.Add(New ListItem() With {.Value = "30", .Text = "In Month"})
            TimePeriodOptions.Items.Add(New ListItem() With {.Value = "60", .Text = "2 Months"})
            TimePeriodOptions.Items.Add(New ListItem() With {.Value = "90", .Text = "3 Months"})
        End Sub

        Private Sub LoadCustomerInfo(ByVal vCustomerId As Integer)
            If vCustomerId <= 0 Then Return

            Dim business As New CustomerBusinessLogic()
            Dim customer As CsCustomer = business.GetCustomerByID(vCustomerId)
            If Not IsNothing(customer) Then
                ltrCustomer.Text = customer.FirstName & " " & customer.LastName
            End If
        End Sub

        Private Sub LoadAllOrdersInWeek(ByVal vCustomerId As Integer)
            If vCustomerId <= 0 Then Return

            Dim recentlyDays As Integer = 0
            If String.IsNullOrWhiteSpace(TimePeriodOptions.SelectedValue) Then
                recentlyDays = 0
            Else
                recentlyDays = CInt(TimePeriodOptions.SelectedValue)
            End If

            Dim vBusiness As New ShoppingBusinessLogic()
            Try
                Dim ordersInWeek As List(Of Order) = Nothing

                If recentlyDays > 0 Then
                    ordersInWeek = vBusiness.GetOrdersByCustomer(CInt(vCustomerId), recentlyDays)
                Else
                    ordersInWeek = vBusiness.GetOrdersByCustomer(CInt(vCustomerId))
                End If

                If Not IsNothing(ordersInWeek) Then
                    ltrNumberOfOrders.Text = ordersInWeek.Count.ToString() & " Orders!"
                    OrdersRepeater.DataSource = ordersInWeek
                    OrdersRepeater.DataBind()
                Else
                    ltrNumberOfOrders.Text = "0 Orders!"
                End If

            Catch ex As Exception
                _log.Error(ex)
            End Try
        End Sub

        Protected Sub OrdersRepeater_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles OrdersRepeater.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If Not (TypeOf e.Item.FindControl("OrderDetailsRepeater") Is Repeater) Then Return
                If Not (TypeOf e.Item.DataItem Is Order) Then Return

                Dim orderDetailsRepeater As Repeater = DirectCast(e.Item.FindControl("OrderDetailsRepeater"), Repeater)
                Dim boundOrder As Order = DirectCast(e.Item.DataItem, Order)
                If IsNothing(boundOrder) Then Return

                orderDetailsRepeater.DataSource = boundOrder.OrderDetails
                orderDetailsRepeater.DataBind()

                If TypeOf e.Item.FindControl("OrderSubTotalPrice") Is Literal Then
                    Dim ltrSubTotal As Literal = DirectCast(e.Item.FindControl("OrderSubTotalPrice"), Literal)
                    ltrSubTotal.Text = FormatNumber(boundOrder.TotalAmount - boundOrder.DeliveryCharges + boundOrder.Discount, 2)
                End If
            End If
        End Sub

        Protected Sub GoBack_Click(sender As Object, e As EventArgs) Handles GoBack.Click
            Response.RedirectTo("Customers.aspx")
        End Sub

        Protected Sub TimePeriodOptions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TimePeriodOptions.SelectedIndexChanged
            LoadAllOrdersInWeek(Me.CustomerId)
        End Sub
    End Class
End Namespace