Imports INF.Web.Helpers
Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Namespace Admin
    Partial Class Settings
        Inherits AdminPage

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then
                'LoadThemeList()
                LoadRestaurantInfo()

                MessageLabel.Text = String.Empty
                MessageLabel.Visible = False
                AlertStyles = ""
            End If

            'If Me.RoleOfLoggedInUser() = UserRoles.Administrator Then
            '    ddlThemeList.Enabled = True
            'Else
            '    ddlThemeList.Enabled = False
            'End If
        End Sub

        'Private Sub LoadThemeList()
        '    Dim themeList As List(Of Theme) = ThemeHelper.GetThemes()
        '    ddlThemeList.DataTextField = "DisplayName"
        '    ddlThemeList.DataValueField = "Name"
        '    ddlThemeList.DataSource = themeList
        '    ddlThemeList.DataBind()
        'End Sub

        Private Sub LoadRestaurantInfo()
            Dim resInfoBLL As New RestaurantBusinessLogic()

            Try
                Dim resInfo As CsRestaurant = resInfoBLL.GetRestaurantInfo()
                If resInfo IsNot Nothing Then
                    With resInfo
                        ShopName.Text = .ShopName
                        ShopNo.Text = .ShopNo
                        PostCode.Text = .PostCode
                        BuildingName.Text = .BuildingName
                        Street.Text = .Street
                        City.Text = .City
                        Telephone1.Text = .Telephone1
                        Mobile.Text = .Mobile
                        Fax.Text = .Fax
                        Email.Text = .Email

                        ' Shopping information
                        DeliveryCharge.Text = FormatNumber(.DeliveryCharge, 2)
                        ServiceCharge.Text = FormatNumber(.ServiceCharge, 2)
                        EnableCashPayments.Checked = .EnableCashPayments
                        EnableNochex.Checked = .EnableNochex

                        OnlineDiscountTextBox.Text = FormatNumber(.OnlineDiscount, 2)

                        ' Temporary close website
                        WebsiteStatus.Checked = .WebSiteStatus

                        RestaurantID.Value = .ID.ToString()
                        'ddlThemeList.SelectedValue = .SelectedTheme
                    End With
                End If

            Catch ex As Exception

                _log.Error(ex)
            End Try
        End Sub

        Protected Sub SaveRestautanSettings_Click(sender As Object, e As System.EventArgs) Handles SaveRestautanSettings.Click
            If Page.IsValid Then
                Dim restaurantToSave As CsRestaurant
                Dim resId As Integer = 0
                Dim bll As New RestaurantBusinessLogic()

                If Not String.IsNullOrWhiteSpace(RestaurantID.Value) AndAlso IsNumeric(RestaurantID.Value) Then
                    resId = CInt(RestaurantID.Value.Trim())
                End If

                restaurantToSave = bll.GetRestaurantByID(resId)
                If restaurantToSave Is Nothing Then
                    restaurantToSave = New CsRestaurant()
                End If

                With restaurantToSave
                    .ShopName = ShopName.Text
                    .ShopNo = ShopNo.Text
                    .PostCode = PostCode.Text
                    .BuildingName = BuildingName.Text
                    .Street = Street.Text
                    .City = City.Text
                    .Telephone1 = Telephone1.Text
                    .Mobile = Mobile.Text
                    .Fax = Fax.Text
                    .Email = Email.Text

                    If Not String.IsNullOrWhiteSpace(DeliveryCharge.Text) Then
                        .DeliveryCharge = CDec(DeliveryCharge.Text.Trim())
                    End If

                    If Not String.IsNullOrWhiteSpace(ServiceCharge.Text) Then
                        .ServiceCharge = CDec(ServiceCharge.Text.Trim())
                    End If

                    .EnableCashPayments = EnableCashPayments.Checked
                    .EnableNochex = EnableNochex.Checked

                    If Not String.IsNullOrWhiteSpace(OnlineDiscountTextBox.Text.Trim()) Then
                        .OnlineDiscount = CDec(OnlineDiscountTextBox.Text.Trim())
                    End If

                    .WebSiteStatus = WebsiteStatus.Checked

                    If Not String.IsNullOrWhiteSpace(RestaurantID.Value) AndAlso IsNumeric(RestaurantID.Value) Then
                        .ID = CInt(RestaurantID.Value.Trim())
                    End If

                    'If Not IsNothing(ddlThemeList.SelectedValue) AndAlso Not String.IsNullOrWhiteSpace(ddlThemeList.SelectedValue) Then
                    '    .SelectedTheme = ddlThemeList.SelectedValue
                    'End If
                End With

                Try
                    Dim savedRestaurant = bll.SaveRestaurantInfo(restaurantToSave)
                    If savedRestaurant IsNot Nothing Then
                        LoadRestaurantInfo()

                        'If Not String.IsNullOrEmpty(ddlThemeList.SelectedValue) Then
                        '    Session(SSN_SELECTED_THEME) = ddlThemeList.SelectedValue
                        'End If

                        MessageLabel.Text = "The changes have been saved!"
                        MessageLabel.CssClass = "good-msg"
                        MessageLabel.Visible = True
                        AlertStyles = "alert alert-success"
                    End If
                Catch ex As Exception
                    MessageLabel.Text = "There is an error. The changes could not be saved!"
                    MessageLabel.Visible = True
                    MessageLabel.CssClass = "bad-msg"
                    AlertStyles = "alert alert-danger"
                    _log.Error(ex)
                End Try
            End If
        End Sub

        'Protected Sub ddlThemeList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlThemeList.SelectedIndexChanged
        'End Sub

        Protected Property AlertStyles() As String
            Get
                If Not IsNothing(ViewState("Settings_VS_AlertStyles")) Then
                    Return CStr(ViewState("Settings_VS_AlertStyles"))
                End If
                Return ""
            End Get
            Set(value As String)
                ViewState("Settings_VS_AlertStyles") = value
            End Set
        End Property
    End Class
End Namespace