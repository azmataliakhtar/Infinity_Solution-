Imports INF.Web.Data
Imports INF.Database
Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports INF.Web.UI.Utils
Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Settings
Imports INF.Web.UI.Logging.Log4Net

Namespace Ajax
    Partial Class GetMinOrderPostCode
        Inherits System.Web.UI.Page

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            _log.Info("BEGIN")

            Try
                If Not IsNothing(Session(BxShoppingCart.SS_POST_CODE)) AndAlso Not String.IsNullOrEmpty(CStr(Session(BxShoppingCart.SS_POST_CODE))) Then
                    Dim postCode As String = CStr(Session(BxShoppingCart.SS_POST_CODE))
                    Dim minOrderValue As Decimal = GetMinOrderByPostCode(postCode)
                    If IsNothing(minOrderValue) OrElse minOrderValue = 0 Then
                        minOrderValue = WebsiteConfig.Instance.MinOrderValue
                    End If

                    Session(BxShoppingCart.SS_MINIMUM_ORDER_VALUE) = minOrderValue

                    Response.Write(minOrderValue)
                Else
                    Response.Write(0)
                End If

            Catch ex As Exception
                _log.Error(ex)
                Response.Write("Error")
            End Try

            _log.Info("END")
        End Sub

        Private Function GetMinOrderByPostCode(postCode As String) As Decimal
            _log.Info("BEGIN")

            Dim minOrder As Decimal = WebsiteConfig.Instance.MinOrderValue
            If String.IsNullOrEmpty(postCode) Then
                Return minOrder
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

            Dim sessionFactory As ISessionFactory = DataProvider.GetInstance().CreateSessionFactory()
            Try
                Using _session As ISession = sessionFactory.CreateSession()

                    Dim query = _session.CreateQuery(Of CsPostCodePrice)(sb.ToString)
                    Dim results = query.GetResults(Of CsPostCodePrice)().ToList()

                    If results.Count > 0 Then
                        minOrder = results(0).MinOrder
                    End If
                End Using
            Catch ex As Exception
                _log.Error(ex)
            End Try

            _log.Info("END")
            Return minOrder
        End Function

    End Class

End Namespace