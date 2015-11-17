
Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Utils
Imports INF.Web.UI.Logging.Log4Net

Namespace Ajax

    Partial Class DecreaseQuantity
        Inherits System.Web.UI.Page

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            _log.Info("BEGIN")

            Try
                If Not Page.IsPostBack Then
                    Dim szCartID As String = WebUtil.GetParameterValueAsString(Page.Request, "CartID")
                    If String.IsNullOrWhiteSpace(szCartID) Then Return

                    Dim basket As BxShoppingCart = BxShoppingCart.GetShoppingCart()
                    basket.RemoveItem(szCartID)
                End If
            Catch ex As Exception
                _log.Error(ex)
                Response.Write("Error")
            End Try

            _log.Info("END")
        End Sub
    End Class
End Namespace