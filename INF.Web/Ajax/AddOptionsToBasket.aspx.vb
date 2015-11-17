
Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Utils
Imports INF.Web.UI.Logging.Log4Net

Namespace Ajax
    Partial Class AddOptionsToBasket
        Inherits System.Web.UI.Page

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            _log.Info("BEGIN")
            Try
                Dim itemId As Integer = WebUtil.GetParameterValueAsInteger(Page.Request, "itemId")
                Dim subItemId As Integer = WebUtil.GetParameterValueAsInteger(Page.Request, "subItemId")
                Dim optionIds As String = WebUtil.GetParameterValueAsString(Page.Request, "optionIds")

                Dim strCartId As String = WebUtil.GetParameterValueAsString(Page.Request, "CartID")

                If Not String.IsNullOrWhiteSpace(strCartId) Then

                    If Not String.IsNullOrWhiteSpace(optionIds) Then
                        Dim ids = optionIds.Split(";"c)
                        Dim lsOptionIds As List(Of Integer) = (From idStr In ids
                                Where (Not String.IsNullOrWhiteSpace(idStr)) AndAlso (IsNumeric(idStr))
                                Select CInt(idStr)).ToList()

                        For Each optionId As Integer In lsOptionIds
                            BxShoppingCart.GetShoppingCart().AddItemWithOptions(strCartId, itemId, optionId)

                        Next
                    End If

                Else
                    Dim newCartId As String = String.Empty
                    If String.IsNullOrWhiteSpace(optionIds) Then
                        newCartId = BxShoppingCart.GetShoppingCart().AddItemWithOptions(itemId, subItemId, 0)
                    Else

                        newCartId = BxShoppingCart.GetShoppingCart().AddItemWithOptions(itemId, subItemId, 0)

                        Dim ids = optionIds.Split(";"c)
                        For Each idStr As String In ids
                            If Not String.IsNullOrWhiteSpace(idStr) AndAlso IsNumeric(idStr) Then
                                Dim opptionId As Integer = CInt(idStr)
                                BxShoppingCart.GetShoppingCart().AddItemWithOptions(strCartId, itemId, opptionId)
                            End If
                        Next
                    End If
                    Response.Write(newCartId)
                End If
            Catch ex As Exception
                _log.Error(ex)
                Response.Write("Error")
            End Try
            _log.Info("END")
        End Sub
    End Class
End Namespace