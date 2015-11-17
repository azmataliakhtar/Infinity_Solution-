
Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI
Imports INF.Web.Data.DAL.SqlClient
Imports INF.Web.UI.Logging.Log4Net

Partial Class TrackOrder
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsAuthenticated Then
            Response.RedirectTo("Login.aspx")
            Exit Sub
        End If

        If Not Page.IsPostBack Then
            Dim vCustomer As CsCustomer = GetLoggedInCustomer()
            If vCustomer IsNot Nothing Then
                HttpContext.Current.Session(SSN_CUSTOMER_ID) = vCustomer.ID

                LoadAllOrdersInWeek(vCustomer.ID)
            End If
        End If
    End Sub

    Private Sub LoadAllOrdersInWeek(ByVal vCustomerId As Decimal)
        Dim vBusiness As New ShoppingBusinessLogic()
        Try
            Dim ordersInWeek As List(Of Order) = vBusiness.GetOrdersByCustomer(CInt(vCustomerId))
            OrdersRepeater.DataSource = ordersInWeek
            OrdersRepeater.DataBind()
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
End Class
