Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports INF.Web.UI
Imports INF.Web.Data.DAL.SqlClient
Imports AjaxControlToolkit.HTMLEditor.ToolbarButton

Public Class OrderDetails
    Inherits AdminPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            InitializeFilter()
            LoadCustomerInfo(CustomerId)
            LoadOrderDetails(OrderID)
        End If
    End Sub

    Private Sub LoadOrderDetails(ByVal vOrderID As Integer)
        If vOrderID <= 0 Then Exit Sub

        Dim business As New BasketTempBusinessLogic()
        Dim pendingOrders As CsBasketTemp = business.GetBasketTempByID(vOrderID)
        
        If Not IsNothing(pendingOrders) AndAlso Not IsNothing(pendingOrders.Items) Then

            ltrOrderAt.Text = pendingOrders.OrderDate.ToString("dd-MMM-yyyy HH:mm")
            ltrOrderStatus.Text = pendingOrders.OrderStatus
            ltrOrderType.Text = pendingOrders.OrderType
            ltrPaymentStatus.Text = pendingOrders.PayStatus

            ltrSubTotal.Text = FormatNumber(pendingOrders.TotalAmount - pendingOrders.DeliveryCharge + pendingOrders.Discount, 2)
            ltrOnlineDiscount.Text = FormatNumber(pendingOrders.Discount, 2)
            ltrDeliveryCharges.Text = FormatNumber(pendingOrders.DeliveryCharge, 2)
            ltrTotalAmount.Text = FormatNumber(pendingOrders.TotalAmount, 2)

            OrderDetailsRepeater.DataSource = pendingOrders.Items
            OrderDetailsRepeater.DataBind()
        End If
    End Sub

    Private Sub InitializeFilter()
    End Sub

    Private Sub LoadCustomerInfo(ByVal vCustomerId As Integer)
        If vCustomerId <= 0 Then Return

        Dim business As New CustomerBusinessLogic()
        Dim customer As CsCustomer = business.GetCustomerByID(vCustomerId)
        If Not IsNothing(customer) Then
            ltrCustomer.Text = customer.FirstName & " " & customer.LastName
        End If
    End Sub

    Protected Sub GoBack_Click(sender As Object, e As EventArgs) Handles GoBack.Click
        Response.RedirectTo("PendingOrders.aspx")
    End Sub

End Class