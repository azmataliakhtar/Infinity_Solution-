Imports INF.Web.UI.Shopping
Imports AjaxControlToolkit.HTMLEditor.ToolbarButton

Namespace Ajax
    Public Class GetShortBasketInfo
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            ltrTotalPrice.Text = FormatCurrency(BxShoppingCart.GetShoppingCart().GetTotal(), 2)
            ltrItemNbr.Text = BxShoppingCart.GetShoppingCart().Items.Count

        End Sub

    End Class
End Namespace