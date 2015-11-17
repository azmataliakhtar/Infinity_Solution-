
Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Utils
Imports INF.Web.UI.Logging.Log4Net

Namespace Ajax

    Partial Class RemoveItemFromShopCart
        Inherits System.Web.UI.Page

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            _log.Info("BEGIN")
            Try

                Dim strCartId As String = WebUtil.GetParameterValueAsString(Page.Request, "CartID")
                If String.IsNullOrWhiteSpace(strCartId) Then Return

                Dim basket As BxShoppingCart = BxShoppingCart.GetShoppingCart()
                basket.RemoveItem(strCartId)

            Catch ex As Exception
                Response.Write("Error")
                _log.Error(ex)
            End Try
            _log.Info("END")
        End Sub
    End Class
End Namespace