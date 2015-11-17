<%@ WebHandler Language="VB" Class="INF.Web.Ajax.ResetOrderTypeHandler" %>


Imports System.Web
Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Logging.Log4Net

Namespace INF.Web.Ajax
    Public Class ResetOrderTypeHandler : Implements IHttpHandler, IRequiresSessionState
    
        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()
        
        Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
            _log.Info("BEGIN")
            
            context.Response.ContentType = "text/plain"
            HttpContext.Current.Session(BxShoppingCart.SS_ORDER_TYPE) = Nothing
        
            HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE) = Nothing
            HttpContext.Current.Session(BxShoppingCart.SS_POST_CODE) = Nothing
            HttpContext.Current.Session(BxShoppingCart.SS_ORDER_TYPE) = Nothing
            
            'WebUtil.StoreValueToCookies(context, ShoppingCart.CK_POST_CODE, Nothing)
            'WebUtil.StoreValueToCookies(context, ShoppingCart.CK_POST_CODE_CHARGE, Nothing)
            'WebUtil.StoreValueToCookies(context, ShoppingCart.CK_ORDER_TYPE, Nothing)
            
            BxShoppingCart.GetShoppingCart().Clear()
            HttpContext.Current.Session("VoucherCode") = String.Empty
            HttpContext.Current.Session("SpecInstr") = Nothing
            HttpContext.Current.Session("IsCartEmpty") = True
            
            context.Response.Write("OK")
            
            _log.Info("END")
        End Sub
 
        Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
            Get
                Return False
            End Get
        End Property

    End Class
End Namespace