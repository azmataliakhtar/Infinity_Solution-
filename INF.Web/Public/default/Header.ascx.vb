Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports INF.Web.UI.Shopping
Imports INF.Web.UI
Imports INF.Web.UI.Settings

Namespace [Public].default
    Public Class Header
        Inherits BaseHeaderUserControl



        Private ReadOnly _logger As log4net.ILog = log4net.LogManager.GetLogger("Header")

        Protected ReadOnly Property RemainingTime() As Double
            Get
                Dim vBusiness As New RestaurantBusinessLogic()
                Try
                    Return vBusiness.CheckRestaurentTiming(DateTime.Now.DayOfWeek)
                Catch ex As Exception
                    _logger.Error("Message: " & ex.Message & vbNewLine & ex.StackTrace)
                End Try

                Return 0
            End Get
        End Property

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        End Sub

        Protected Sub LogOff(sender As Object, e As System.EventArgs) Handles lnkLogout.Click
            OnLogOutClick(Me, Nothing)
        End Sub

        Public ReadOnly Property ServiceType() As Literal
            Get
                Return ltrOrderType
            End Get
        End Property

        Public ReadOnly Property Postcode() As Literal
            Get
                Return ltrPostcode
            End Get
        End Property
    End Class
End Namespace