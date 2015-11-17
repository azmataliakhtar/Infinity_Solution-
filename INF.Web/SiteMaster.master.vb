Imports INF.Web.Public.default
Imports INF.Web.Data.BLL
Imports INF.Web.UI
Imports System.IO
Imports INF.Web.UI.Settings
Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Logging.Log4Net

Partial Public Class SiteMaster
    Inherits MasterPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Protected ReadOnly Property RemainingTime() As Double
        Get
            Dim vBusiness As New RestaurantBusinessLogic()
            Try
                Return vBusiness.CheckRestaurentTiming(DateTime.Now.DayOfWeek)
            Catch ex As Exception
                _log.Error(ex)
            End Try

            Return 0
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'If Not IsNothing(Session(BxShoppingCart.SS_ORDER_TYPE)) Then
        '    ltrOrderType.Text = "Order Type: " & Session(BxShoppingCart.SS_ORDER_TYPE).ToString()
        '    ltrOrderType.Visible = True
        '    'phOrderTypeReset.Visible = True

        'Else
        '    ltrOrderType.Visible = False
        '    'phOrderTypeReset.Visible = False
        'End If

        'If Not IsNothing(Session(BxShoppingCart.SS_POST_CODE)) AndAlso Not String.IsNullOrEmpty(CStr(Session(BxShoppingCart.SS_POST_CODE))) Then
        '    ltrPostcode.Visible = True
        '    ltrPostcode.Text = "/ Postcode: " & CStr(Session(BxShoppingCart.SS_POST_CODE))
        'Else
        '    ltrPostcode.Visible = False
        'End If
        If Not Page.IsPostBack Then

            Dim ucHeader As Header = CType(phHeader.FindControl("ucHeader"), Header)
            If Not IsNothing(ucHeader) Then
                If Not IsNothing(Session(BxShoppingCart.SS_ORDER_TYPE)) Then
                    ucHeader.ServiceType.Text = "Order Service: " & Session(BxShoppingCart.SS_ORDER_TYPE).ToString()
                    ucHeader.ServiceType.Visible = True
                Else
                    ucHeader.ServiceType.Visible = False
                End If

                If Not IsNothing(Session(BxShoppingCart.SS_POST_CODE)) AndAlso Not String.IsNullOrEmpty(CStr(Session(BxShoppingCart.SS_POST_CODE))) Then
                    ucHeader.Postcode.Visible = True
                    ucHeader.Postcode.Text = "/ Postcode: " & CStr(Session(BxShoppingCart.SS_POST_CODE))
                Else
                    ucHeader.Postcode.Visible = False
                End If
            End If
        End If

    End Sub

    Protected Sub Header_LogOutClick(ByVal sender As Object, ByVal e As EventArgs)
        FormsAuthentication.SignOut()
        HttpContext.Current.Session("LoggedInUser") = Nothing
        Response.RedirectTo("Login.aspx")
    End Sub

    Protected Overrides Sub OnInit(ByVal e As EventArgs)
        MyBase.OnInit(e)

        phHeader.Controls.Clear()
        phFooter.Controls.Clear()

        'Create header
        Dim virtualPathHeader As String = "~/public/" + WebsiteConfig.Instance.INFTheme + "/Header.ascx"
        Dim physicalPathHeader As String = HttpContext.Current.Server.MapPath(virtualPathHeader)
        If Not File.Exists(physicalPathHeader) Then
            virtualPathHeader = "~/public/default/Header.ascx"
        End If

        Dim virtualPathFooter As String = "~/public/" + WebsiteConfig.Instance.INFTheme + "/Footer.ascx"
        Dim physicalPathFooter As String = HttpContext.Current.Server.MapPath(virtualPathFooter)
        If Not File.Exists(physicalPathFooter) Then
            virtualPathFooter = "~/public/default/Footer.ascx"
        End If

        Dim header As UserControl = LoadControl(virtualPathHeader)
        header.ID = "ucHeader"
        If Not IsNothing(header) Then
            phHeader.Controls.Add(header)

            If TypeOf header Is BaseHeaderUserControl Then
                Dim defaultHeader As BaseHeaderUserControl = CType(header, BaseHeaderUserControl)
                AddHandler defaultHeader.LogOutClick, AddressOf Header_LogOutClick
            End If
        End If

        'Create footer
        Dim footer As UserControl = LoadControl(virtualPathFooter)
        footer.ID = "ucFooter"
        If Not IsNothing(footer) Then
            phFooter.Controls.Add(footer)
        End If
    End Sub
End Class