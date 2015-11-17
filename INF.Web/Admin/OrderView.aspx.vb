Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports INF.Web.UI
Imports INF.Web.Data.DAL.SqlClient
Imports AjaxControlToolkit.HTMLEditor.ToolbarButton

Public Class OrderView
    Inherits AdminPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadCustomerInfo(CustomerId)
            LoadOrderDetails(OrderID)
        End If
    End Sub

    Private Sub LoadCustomerInfo(ByVal vCustomerId As Integer)
        If vCustomerId <= 0 Then Return

        Dim business As New CustomerBusinessLogic()
        Dim customer As CsCustomer = business.GetCustomerByID(vCustomerId)
        If Not IsNothing(customer) Then
            ltrCustomer.Text = customer.FirstName & " " & customer.LastName
            Dim str = "<table><tr><td>Fullname </td><td>: " + customer.FirstName + " " + customer.LastName + "</td></tr>"
            str += "<tr><td>Telephone </td><td>: " + customer.Telephone + "</td></tr>"
            str += "<tr><td>Mobiphone </td><td>: " + customer.Mobile + "</td></tr>"
            str += "<tr><td>Email </td><td>: " + customer.Email + "</td></tr></table>"
            ltrCustomerInfo.Text = str
        End If
    End Sub

    Private Sub LoadOrderDetails(ByVal vOrderID As Integer)
        If vOrderID <= 0 Then Exit Sub

        Dim order = OrderProvider.Instance.GetOrderByID(vOrderID)

        If Not IsNothing(order) AndAlso Not IsNothing(order.OrderDetails) Then

            ltrOrderAt.Text = order.OrderDate.ToString("dd-MMM-yyyy HH:mm")
            ltrOrderStatus.Text = order.OrderStatus
            ltrOrderType.Text = order.OrderType
            ltrPaymentStatus.Text = order.PayStatus

            ltrSubTotal.Text = FormatNumber(order.TotalAmount - order.DeliveryCharges + order.Discount, 2)
            ltrOnlineDiscount.Text = FormatNumber(order.Discount, 2)
            ltrDeliveryCharges.Text = FormatNumber(order.DeliveryCharges, 2)
            ltrTotalAmount.Text = FormatNumber(order.TotalAmount, 2)

            OrderDetailsRepeater.DataSource = order.OrderDetails
            OrderDetailsRepeater.DataBind()
        End If
    End Sub

    Protected Sub GoBack_Click(sender As Object, e As EventArgs) Handles GoBack.Click
        Response.RedirectTo("TodayOrders.aspx")
    End Sub
End Class