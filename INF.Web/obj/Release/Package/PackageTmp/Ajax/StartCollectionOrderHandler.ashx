<%@ WebHandler Language="VB" Class="INF.Web.Ajax.StartCollectionOrderHandler" %>


Imports System.Web
Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Logging.Log4Net

Namespace INF.Web.Ajax
    Public Class StartCollectionOrderHandler : Implements IHttpHandler, IRequiresSessionState
    
        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()
        
        Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
            _log.Info("BEGIN")
            context.Response.ContentType = "text/plain"
            HttpContext.Current.Session(BxShoppingCart.SS_ORDER_TYPE) = BxShoppingCart.ORDER_TYPE_COLLECTION
            'WebUtil.StoreValueToCookies(context, ShoppingCart.CK_ORDER_TYPE, ShoppingCart.ORDER_TYPE_COLLECTION)
            context.Response.Write("OK")
            
            _log.Info("BEGIN")
        End Sub
 
        Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
            Get
                Return False
            End Get
        End Property

    End Class
End Namespace