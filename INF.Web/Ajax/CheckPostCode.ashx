<%@ WebHandler Language="VB" Class="INF.Web.Ajax.CheckPostCode" %>

Imports System
Imports System.Web
Imports System.Web.SessionState
Imports System.Text
Imports INF.Web.Data.Entities
Imports INF.Web.Data
Imports INF.Database
Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Logging.Log4Net

Namespace INF.Web.Ajax
    ''' <summary>
    ''' Code behind for CheckPostCodes ajax page
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CheckPostCode : Implements IHttpHandler, IRequiresSessionState
    
        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()
        
        Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
            _log.Info("BEGIN")
            
            context.Response.ContentType = "text/plain"
            Dim postCode As String = String.Empty
            If Not context.Request("pcode") Is Nothing Then
                postCode = context.Request("pcode")
            End If
        
            Dim isAllowDelivery As Boolean = False
            Dim price As Decimal = Me.GetPriceByPostCode(postCode, isAllowDelivery)
        
            If Not isAllowDelivery Then
                context.Response.Write("-1")
            Else
                HttpContext.Current.Session(BxShoppingCart.POST_CODE_CHARGE) = price
                HttpContext.Current.Session(BxShoppingCart.SS_POST_CODE) = postCode
                HttpContext.Current.Session(BxShoppingCart.SS_ORDER_TYPE) = BxShoppingCart.ORDER_TYPE_DELIVERY
                          
                context.Response.Write("1")
            End If
            
            _log.Info("END")
        End Sub
 
        Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
            Get
                Return False
            End Get
        End Property

        Private Function GetPriceByPostCode(postCode As String, ByRef isAllowDelivery As Boolean) As Decimal
            _log.Info("BEGIN")
                        
            Dim price As Decimal = 0
            isAllowDelivery = False
            If String.IsNullOrEmpty(postCode) Then
                Return price
            End If

            Dim firstPartOfPostCode5 As String = String.Empty
            Dim firstPartOfPostCode4 As String = String.Empty
            Dim firstPartOfPostCode3 As String = String.Empty
            Dim firstPartOfPostCode2 As String = String.Empty

            If postCode.Length >= 5 Then
                firstPartOfPostCode5 = postCode.Substring(0, 5)
            End If

            If postCode.Length >= 4 Then
                firstPartOfPostCode4 = postCode.Substring(0, 4)
            End If

            If postCode.Length >= 3 Then
                firstPartOfPostCode3 = postCode.Substring(0, 3)
            End If

            If postCode.Length >= 2 Then
                firstPartOfPostCode2 = postCode.Substring(0, 2)
            End If

            Dim sb As StringBuilder = New StringBuilder()
            sb.AppendLine("WHERE")
            sb.AppendLine(" REPLACE([POST_CODE], ' ', '') = '" & firstPartOfPostCode2 & "'")

            If Not String.IsNullOrEmpty(firstPartOfPostCode3) Then
                sb.AppendLine(" OR REPLACE([POST_CODE], ' ', '') = '" & firstPartOfPostCode3 & "'")
            End If

            If Not String.IsNullOrEmpty(firstPartOfPostCode4) Then
                sb.AppendLine(" OR REPLACE([POST_CODE], ' ', '') = '" & firstPartOfPostCode4 & "'")
            End If

            If Not String.IsNullOrEmpty(firstPartOfPostCode5) Then
                sb.AppendLine(" OR REPLACE([POST_CODE], ' ', '') = '" & firstPartOfPostCode5 & "'")
            End If

            sb.AppendLine("ORDER BY LEN([POST_CODE]) DESC")
        
            Dim _sessionFactory As ISessionFactory = DataProvider.GetInstance().CreateSessionFactory()
            Try
                Using _session As ISession = _sessionFactory.CreateSession()
                
                    Dim query = _session.CreateQuery(Of CsPostCodePrice)(sb.ToString)
                    Dim results = query.GetResults(Of CsPostCodePrice)().ToList()
                
                    If results.Count > 0 Then
                        isAllowDelivery = results(0).AllowDelivery
                    
                        If isAllowDelivery Then
                            price = results(0).Price
                        End If
                    End If
                End Using
            Catch ex As Exception
                _log.Error(ex)
            Finally
                If _sessionFactory IsNot Nothing Then
                    _sessionFactory = Nothing
                End If
            End Try
        
            _log.Info("END")
            Return price
        End Function

    End Class
End Namespace