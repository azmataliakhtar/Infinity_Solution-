
Imports System.Drawing
Imports INF.Web.Data.BLL
Imports INF.Web.My.Resources
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Namespace Admin

    Partial Class Themes
        Inherits AdminPage

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                ShowLogoAndSloganView_Click(Nothing, Nothing)
            End If
            'FooterText.BasePath = BaseUrl + "FCKeditor/"
        End Sub

        Protected Sub ShowLogoAndSloganView_Click(sender As Object, e As System.EventArgs) Handles ShowLogoAndSloganView.Click
            LoadLogoAndSloganSettings()
            ThemesMultiView.SetActiveView(LogoAndSloganView)
        End Sub

        Protected Sub ShowNavigationImagesView_Click(sender As Object, e As System.EventArgs) Handles ShowNavigationImagesView.Click

            LoadNavigationImagesSettings()

            ThemesMultiView.SetActiveView(NavigationImagesView)
        End Sub

        Protected Sub ShowHeaderView_Click(sender As Object, e As System.EventArgs) Handles ShowHeaderView.Click
            LoadHeaderSettings()
            ThemesMultiView.SetActiveView(HeaderView)
        End Sub

        Protected Sub ShowFooterView_Click(sender As Object, e As System.EventArgs) Handles ShowFooterView.Click
            LoadFooterSettings()
            'FooterText.BasePath = BaseUrl + "FCKeditor/"
            ThemesMultiView.SetActiveView(FooterView)
        End Sub

        Protected Sub ShowHomePageView_Click(sender As Object, e As System.EventArgs) Handles ShowHomePageView.Click
            LoadHomePageSettings()
            ThemesMultiView.SetActiveView(HomePageView)
        End Sub

        Protected Sub ShowMenuCategory_Click(sender As Object, e As System.EventArgs) Handles ShowMenuCategory.Click
            LoadMenuCategorySettings()
            ThemesMultiView.SetActiveView(MenuCategoryView)
        End Sub

        Protected Sub SaveThemesSettings_Click(sender As Object, e As System.EventArgs) Handles SaveThemesSettings.Click

            If ThemesMultiView.GetActiveView().Equals(LogoAndSloganView) Then
                SaveLogoAndSloganSettings()

            ElseIf ThemesMultiView.GetActiveView().Equals(NavigationImagesView) Then
                SaveNavigationImagesSettings()

            ElseIf ThemesMultiView.GetActiveView().Equals(HeaderView) Then
                SaveHeaderSettings()

            ElseIf ThemesMultiView.GetActiveView().Equals(FooterView) Then
                SaveFooterSettings()

            ElseIf ThemesMultiView.GetActiveView().Equals(HomePageView) Then
                SaveHomePageSettings()

            ElseIf ThemesMultiView.GetActiveView().Equals(MenuCategoryView) Then
                SaveMenuCategorySettings()
            End If
        End Sub

        Private Sub LoadLogoAndSloganSettings()
            Dim bll As ThemesSettingsBusinessLogic = New ThemesSettingsBusinessLogic()
            Dim logoAndSloganSettings As LogoAndSloganSettings = bll.LoadLogoAndSloganSettings()

            If logoAndSloganSettings IsNot Nothing Then
                With logoAndSloganSettings

                    Slogan.Text = .Slogan
                    Logo.ImageUrl = .LogoUrl

                    If IsNumeric(.LogoWidth) Then
                        Logo.Width = CInt(.LogoWidth)
                    End If

                    If IsNumeric(.LogoHeight) Then
                        Logo.Height = CInt(.LogoHeight)
                    End If

                    LogoImageWidth.Text = .LogoWidth
                    LogoImageHeight.Text = .LogoHeight
                End With
            End If
        End Sub

        Private Sub SaveLogoAndSloganSettings()
            Dim messages As New List(Of String)

            If Not IsNumeric(LogoImageWidth.Text.Trim()) Then
                messages.Add("[Logo With] must be a number")
            End If

            If Not IsNumeric(LogoImageHeight.Text.Trim()) Then
                messages.Add("[Logo Height] must be a number")
            End If

            If messages.Count > 0 Then
                BuildErrorMessagesControl(ErrorMessages, messages)
                Return
            End If

            Dim logoAndSlogan As New LogoAndSloganSettings()
            With logoAndSlogan

                .LogoWidth = LogoImageWidth.Text.Trim()
                .LogoHeight = LogoImageHeight.Text.Trim()
                .LogoUrl = Logo.ImageUrl

                .Slogan = Slogan.Text.Trim()

                Try
                    Dim bll As New ThemesSettingsBusinessLogic()
                    If bll.SaveLogoAndSlogan(logoAndSlogan) Then

                        BuildMessagesControl(ErrorMessages, "The Logo And Slogan settings have been saved successfully")
                    Else
                        messages.Add("Could not save the settings")
                        BuildErrorMessagesControl(ErrorMessages, messages)
                    End If
                Catch ex As Exception

                    _log.Error(ex)
                End Try
            End With
        End Sub

        Protected Sub LogoFileUploadButton_Click(sender As Object, e As System.EventArgs) Handles LogoFileUploadButton.Click
            UploadLogo()
        End Sub

        Private Sub UploadLogo()
            If LogoFileUpload.HasFile Then

                Dim messages As New List(Of String)

                If Not IsNumeric(LogoImageWidth.Text.Trim()) Then
                    messages.Add("[Logo With] must be a number")
                End If

                If Not IsNumeric(LogoImageHeight.Text.Trim()) Then
                    messages.Add("[Logo Height] must be a number")
                End If

                If messages.Count > 0 Then
                    messages.Insert(0, "Please set up size for this logo before uploading it.")
                    BuildErrorMessagesControl(ErrorMessages, messages)
                End If

                Dim url As String = ResizeAndUploadImage(LogoFileUpload, CInt(LogoImageWidth.Text.Trim()), CInt(LogoImageHeight.Text.Trim()))
                Logo.ImageUrl = url
                Logo.Width = CInt(LogoImageWidth.Text.Trim())
                Logo.Height = CInt(LogoImageHeight.Text.Trim())
            End If
        End Sub

        Private Sub LoadNavigationImagesSettings()
            Dim bll As ThemesSettingsBusinessLogic = New ThemesSettingsBusinessLogic()
            Dim nis As NavigationImagesSettings = bll.LoadNavigationImagesSettings()

            If nis IsNot Nothing Then
                With nis

                    NavigationImage.ImageUrl = .ImageUrl
                    If IsNumeric(.ImageWidth) Then
                        NavigationImage.Width = CInt(.ImageWidth)
                    End If

                    If IsNumeric(.ImageHeight) Then
                        NavigationImage.Height = CInt(.ImageHeight)
                    End If

                    NavigationImageWitdh.Text = .ImageWidth
                    NavigationImageHeight.Text = .ImageHeight

                    NavigationHoverImage.ImageUrl = .ImageHoverUrl
                    If IsNumeric(.ImageHoverWidth) Then
                        NavigationHoverImage.Width = CInt(.ImageHoverWidth)
                    End If

                    If IsNumeric(.ImageHoverHeight) Then
                        NavigationHoverImage.Height = CInt(.ImageHoverHeight)
                    End If

                    NavigationHoverImageWidth.Text = .ImageHoverWidth
                    NavigationHoverImageHeight.Text = .ImageHoverHeight

                End With
            End If
        End Sub

        Private Sub SaveNavigationImagesSettings()
            Dim messages As New List(Of String)

            If Not IsNumeric(NavigationImageWitdh.Text.Trim()) Then
                messages.Add("[Navigation image width] must be a number")
            End If

            If Not IsNumeric(NavigationImageHeight.Text.Trim()) Then
                messages.Add("[Navigation image height] must be a number")
            End If

            If Not IsNumeric(NavigationHoverImageWidth.Text.Trim()) Then
                messages.Add("[Navigation image (hover) width] must be a number")
            End If

            If Not IsNumeric(NavigationHoverImageHeight.Text.Trim()) Then
                messages.Add("[Navigation image (hover) height] must be a number")
            End If

            If messages.Count > 0 Then
                BuildErrorMessagesControl(ErrorMessages, messages)
                Return
            End If

            Dim nis As New NavigationImagesSettings()
            With nis

                .ImageUrl = NavigationImage.ImageUrl
                .ImageWidth = NavigationImageWitdh.Text.Trim()
                .ImageHeight = NavigationImageHeight.Text.Trim()

                .ImageHoverUrl = NavigationHoverImage.ImageUrl
                .ImageHoverWidth = NavigationHoverImageWidth.Text.Trim()
                .ImageHoverHeight = NavigationHoverImageHeight.Text.Trim()

                Dim bll As New ThemesSettingsBusinessLogic()
                Try
                    If (bll.SaveNavigationImagesSettings(nis)) Then
                        BuildMessagesControl(ErrorMessages, "The changes have been saved successfully")
                    Else
                        messages.Add("Could not save the changes")
                        BuildErrorMessagesControl(ErrorMessages, messages)
                    End If
                Catch ex As Exception
                    _log.Error(ex)
                End Try

            End With
        End Sub

        Protected Sub NavigationImageFileUploadButton_Click(sender As Object, e As System.EventArgs) Handles NavigationImageFileUploadButton.Click
            If NavigationImageFileUpload.HasFile Then

                Dim messages As New List(Of String)

                If Not IsNumeric(NavigationImageWitdh.Text.Trim()) Then
                    messages.Add("[Navigation image width] must be a number")
                End If

                If Not IsNumeric(NavigationImageHeight.Text.Trim()) Then
                    messages.Add("[Navigation image height] must be a number")
                End If

                If messages.Count > 0 Then
                    BuildErrorMessagesControl(ErrorMessages, messages)
                    Return
                End If

                Dim url As String = ResizeAndUploadImage(NavigationImageFileUpload, CInt(NavigationImageWitdh.Text.Trim()), CInt(NavigationImageHeight.Text.Trim()))
                NavigationImage.ImageUrl = url
                NavigationImage.Width = CInt(NavigationImageWitdh.Text.Trim())
                NavigationImage.Height = CInt(NavigationImageHeight.Text.Trim())
            End If
        End Sub

        Protected Sub NavigationHoverImageFileUploadButton_Click(sender As Object, e As System.EventArgs) Handles NavigationHoverImageFileUploadButton.Click

            If NavigationHoverImageFileUpload.HasFile Then
                Dim messages As New List(Of String)

                If Not IsNumeric(NavigationHoverImageWidth.Text.Trim()) Then
                    messages.Add("[Navigation image (hover) width] must be a number")
                End If

                If Not IsNumeric(NavigationHoverImageHeight.Text.Trim()) Then
                    messages.Add("[Navigation image (hover) height] must be a number")
                End If

                If messages.Count > 0 Then
                    BuildErrorMessagesControl(ErrorMessages, messages)
                    Return
                End If

                Dim url As String = ResizeAndUploadImage(NavigationHoverImageFileUpload, CInt(NavigationHoverImageWidth.Text.Trim()), CInt(NavigationHoverImageHeight.Text.Trim()))
                NavigationHoverImage.ImageUrl = url

            End If

        End Sub

        Private Sub SaveHeaderSettings()
            If ValidateHeaderSettings() Then
                Dim headerSettings As New HeaderSettings()
                With headerSettings
                    .ImageUrl = HeaderBgImage.ImageUrl
                    .ImageWidth = HeaderBgImageWidth.Text.Trim()
                    .ImageHeight = HeaderBgImageHeight.Text.Trim()
                End With

                Dim bll As New ThemesSettingsBusinessLogic()
                Try
                    If bll.SaveHeaderSettings(headerSettings) Then
                        BuildMessagesControl(ErrorMessages, "The changes have been saved successfully")
                    Else
                        Dim messages As New List(Of String)
                        messages.Add("Could not save the changes")
                        BuildErrorMessagesControl(ErrorMessages, messages)
                    End If
                Catch ex As Exception
                    _log.Error(ex)
                End Try
            End If
        End Sub

        Private Sub LoadHeaderSettings()
            Dim bll As New ThemesSettingsBusinessLogic()

            Try
                Dim headerSettings As HeaderSettings = bll.LoadHeaderSettings()
                If headerSettings IsNot Nothing Then

                    With headerSettings
                        HeaderBgImage.ImageUrl = .ImageUrl

                        If IsNumeric(.ImageWidth) Then
                            If CInt(.ImageWidth) > 500 Then
                                HeaderBgImage.Width = 500
                            Else
                                HeaderBgImage.Width = CInt(.ImageWidth)
                            End If
                        End If

                        If IsNumeric(.ImageHeight) Then
                            HeaderBgImage.Height = CInt(.ImageHeight)
                        End If

                        HeaderBgImageWidth.Text = .ImageWidth
                        HeaderBgImageHeight.Text = .ImageHeight
                    End With
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try

        End Sub

        Protected Sub HeaderBgImageFileUploadButton_Click(sender As Object, e As System.EventArgs) Handles HeaderBgImageFileUploadButton.Click
            If HeaderBgImageFileUpload.HasFile Then
                If ValidateHeaderSettings() Then

                    Dim url As String = ResizeAndUploadImage(HeaderBgImageFileUpload, CInt(HeaderBgImageWidth.Text.Trim()), CInt(HeaderBgImageHeight.Text.Trim()))
                    HeaderBgImage.ImageUrl = url
                    If CInt(HeaderBgImageWidth.Text.Trim()) > 500 Then
                        HeaderBgImage.Width = 500
                    Else
                        HeaderBgImage.Width = CInt(HeaderBgImageWidth.Text.Trim())
                    End If

                    HeaderBgImage.Height = CInt(HeaderBgImageHeight.Text.Trim())
                End If
            End If
        End Sub

        Private Function ValidateHeaderSettings() As Boolean
            Dim messages As New List(Of String)

            If Not IsNumeric(HeaderBgImageWidth.Text.Trim()) Then
                messages.Add("[Header background image width] must be a number")
            End If

            If Not IsNumeric(HeaderBgImageHeight.Text.Trim()) Then
                messages.Add("[Header background image height] must be a number")
            End If

            If messages.Count > 0 Then
                BuildErrorMessagesControl(ErrorMessages, messages)
                Return False
            End If

            Return True
        End Function

        Protected Sub FooterbgImageFileUploadButton_Click(sender As Object, e As System.EventArgs) Handles FooterbgImageFileUploadButton.Click
            If FooterbgImageFileUpload.HasFile Then
                If ValidateFooterSettings() Then

                    Dim url As String = ResizeAndUploadImage(FooterbgImageFileUpload, CInt(FooterbgImageWidth.Text.Trim()), CInt(FooterbgImageHeight.Text.Trim()))
                    FooterbgImage.ImageUrl = url
                    If CInt(FooterbgImageWidth.Text.Trim()) > 500 Then
                        FooterbgImage.Width = 500
                    Else
                        FooterbgImage.Width = CInt(FooterbgImageWidth.Text.Trim())
                    End If

                    FooterbgImage.Height = CInt(FooterbgImageHeight.Text.Trim())
                End If
            End If
        End Sub

        Private Sub SaveFooterSettings()
            If ValidateFooterSettings() Then
                Dim footerSettings As New FooterSettings()
                With footerSettings
                    .ImageUrl = FooterbgImage.ImageUrl
                    .ImageWidth = FooterbgImageWidth.Text.Trim()
                    .ImageHeight = FooterbgImageHeight.Text.Trim()
                End With

                Dim bll As New ThemesSettingsBusinessLogic()
                Try
                    If bll.SaveFooterSettings(footerSettings) Then
                        BuildMessagesControl(ErrorMessages, "The changes have been saved successfully")
                    Else
                        Dim messages As New List(Of String)
                        messages.Add("Could not save the changes")
                        BuildErrorMessagesControl(ErrorMessages, messages)
                    End If
                Catch ex As Exception
                    _log.Error(ex)
                End Try
            End If
        End Sub

        Private Sub LoadFooterSettings()
            Dim bll As New ThemesSettingsBusinessLogic()

            Try
                Dim footerSettings As FooterSettings = bll.LoadFooterSettings()
                If footerSettings IsNot Nothing Then

                    With footerSettings
                        FooterbgImage.ImageUrl = .ImageUrl

                        If IsNumeric(.ImageWidth) Then
                            If CInt(.ImageWidth) > 500 Then
                                FooterbgImage.Width = 500
                            Else
                                FooterbgImage.Width = CInt(.ImageWidth)
                            End If
                        End If

                        If IsNumeric(.ImageHeight) Then
                            FooterbgImage.Height = CInt(.ImageHeight)
                        End If

                        FooterbgImageWidth.Text = .ImageWidth
                        FooterbgImageHeight.Text = .ImageHeight
                    End With
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try

        End Sub

        Private Function ValidateFooterSettings() As Boolean
            Dim messages As New List(Of String)

            If Not IsNumeric(FooterbgImageWidth.Text.Trim()) Then
                messages.Add("[Footer background image width] must be a number")
            End If

            If Not IsNumeric(FooterbgImageHeight.Text.Trim()) Then
                messages.Add("[Footer background image height] must be a number")
            End If

            If messages.Count > 0 Then
                BuildErrorMessagesControl(ErrorMessages, messages)
                Return False
            End If

            Return True
        End Function

        Protected Sub HomePageBgImageFileUploadButton_Click(sender As Object, e As System.EventArgs) Handles HomePageBgImageFileUploadButton.Click
            If HomePageBgImageFileUpload.HasFile Then
                If ValidateHomePageSettings() Then

                    Dim url As String = ResizeAndUploadImage(HomePageBgImageFileUpload, CInt(HomePageBgImageWidth.Text.Trim()), CInt(HomePageBgImageHeight.Text.Trim()))
                    HomePageBgImage.ImageUrl = url
                    If CInt(HomePageBgImageWidth.Text.Trim()) > 500 Then
                        HomePageBgImage.Width = 500
                    Else
                        HomePageBgImage.Width = CInt(HomePageBgImageWidth.Text.Trim())
                    End If

                    If CInt(HomePageBgImageHeight.Text.Trim()) > 400 Then
                        HomePageBgImage.Height = 400
                    Else
                        HomePageBgImage.Height = CInt(HomePageBgImageHeight.Text.Trim())
                    End If
                End If
            End If
        End Sub

        Private Function ValidateHomePageSettings() As Boolean
            Dim messages As New List(Of String)

            If Not IsNumeric(HomePageBgImageWidth.Text.Trim()) Then
                messages.Add("[HomePage background image width] must be a number")
            End If

            If Not IsNumeric(HomePageBgImageHeight.Text.Trim()) Then
                messages.Add("[HomePage background image height] must be a number")
            End If

            If messages.Count > 0 Then
                BuildErrorMessagesControl(ErrorMessages, messages)
                Return False
            End If

            Return True
        End Function

        Private Sub SaveHomePageSettings()
            If ValidateHomePageSettings() Then
                Dim hpSettings As New HomePageSettings()
                With hpSettings
                    .ImageUrl = HomePageBgImage.ImageUrl
                    .ImageWidth = HomePageBgImageWidth.Text.Trim()
                    .ImageHeight = HomePageBgImageHeight.Text.Trim()
                End With

                Dim bll As New ThemesSettingsBusinessLogic()
                Try
                    If bll.SaveHomePageSettings(hpSettings) Then
                        BuildMessagesControl(ErrorMessages, "The changes have been saved successfully")
                    Else
                        Dim messages As New List(Of String)
                        messages.Add("Could not save the changes")
                        BuildErrorMessagesControl(ErrorMessages, messages)
                    End If
                Catch ex As Exception
                    _log.Error(ex)
                End Try
            End If
        End Sub

        Private Sub LoadHomePageSettings()
            Dim bll As New ThemesSettingsBusinessLogic()

            Try
                Dim hpSettings As HomePageSettings = bll.LoadHomePageSettings()
                If hpSettings IsNot Nothing Then

                    With hpSettings
                        HomePageBgImage.ImageUrl = .ImageUrl

                        If IsNumeric(.ImageWidth) Then
                            If CInt(.ImageWidth) > 500 Then
                                HomePageBgImage.Width = 500
                            Else
                                HomePageBgImage.Width = CInt(.ImageWidth)
                            End If
                        End If

                        If IsNumeric(.ImageHeight) Then
                            If CInt(.ImageHeight) > 400 Then
                                HomePageBgImage.Height = 400
                            Else
                                HomePageBgImage.Height = CInt(.ImageHeight)
                            End If
                        End If

                        HomePageBgImageWidth.Text = .ImageWidth
                        HomePageBgImageHeight.Text = .ImageHeight
                    End With
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try

        End Sub

        Private Sub LoadMenuCategorySettings()
            Dim vBusiness As New ThemesSettingsBusinessLogic()
            Try
                Dim menuCatSettings As MenuCategorySettings = vBusiness.LoadMenuCategorySettings()
                With menuCatSettings
                    MenuCategoryWidth.Text = .Width.ToString()
                    MenuCategoryHeight.Text = .Height.ToString()
                End With

                Dim vColors As ColorSettings = vBusiness.LoadColorSettings()
                If (vColors IsNot Nothing) Then
                    Dim vColorConverter As New ColorConverter()

                    If Not String.IsNullOrWhiteSpace(vColors.BaseColor) Then
                        BaseColorTextBox.Text = vColors.BaseColor
                        BaseColorSampleLabel.BackColor = CType(vColorConverter.ConvertFromString("#" & vColors.BaseColor), Color)
                    End If

                    If Not String.IsNullOrWhiteSpace(vColors.BackColor) Then
                        BackColorTextBox.Text = vColors.BackColor
                        BackColorSampleLabel.BackColor = CType(vColorConverter.ConvertFromString("#" & vColors.BackColor), Color)
                    End If

                    EditOrderImage.ImageUrl = vColors.EditOrderImageUrl
                    ConfirmOrderImage.ImageUrl = vColors.ConfirmOrderImageUrl
                    CheckOutImage.ImageUrl = vColors.CheckOutImageUrl
                    AddToCartImage.ImageUrl = vColors.AddToCartImageUrl
                End If

            Catch ex As Exception
                _log.Error(ex)
            End Try
        End Sub

        Private Sub SaveMenuCategorySettings()
            Dim messages As New List(Of String)

            If Not IsNumeric(MenuCategoryWidth.Text.Trim()) Then
                messages.Add("[Menu Category Width] must be a number")
            End If

            If Not IsNumeric(MenuCategoryHeight.Text.Trim()) Then
                messages.Add("[Menu Category Height] must be a number")
            End If

            If messages.Count > 0 Then
                BuildErrorMessagesControl(ErrorMessages, messages)
                Return
            End If

            Dim vBusiness As New ThemesSettingsBusinessLogic()
            Dim vMenuCat As New MenuCategorySettings()
            With vMenuCat
                .Width = MenuCategoryWidth.Text.Trim()
                .Height = MenuCategoryHeight.Text.Trim()
            End With

            Dim vColors As New ColorSettings()
            With vColors
                If Not String.IsNullOrWhiteSpace(BaseColorTextBox.Text) Then
                    .BaseColor = BaseColorTextBox.Text.Trim()
                End If
                If Not String.IsNullOrWhiteSpace(BackColorTextBox.Text) Then
                    .BackColor = BackColorTextBox.Text.Trim()
                End If

                If Not String.IsNullOrWhiteSpace(EditOrderImage.ImageUrl) Then
                    vColors.EditOrderImageUrl = EditOrderImage.ImageUrl
                End If

                If Not String.IsNullOrWhiteSpace(ConfirmOrderImage.ImageUrl) Then
                    vColors.ConfirmOrderImageUrl = ConfirmOrderImage.ImageUrl
                End If

                If Not String.IsNullOrWhiteSpace(CheckOutImage.ImageUrl) Then
                    vColors.CheckOutImageUrl = CheckOutImage.ImageUrl
                End If

                If Not String.IsNullOrWhiteSpace(AddToCartImage.ImageUrl) Then
                    vColors.AddToCartImageUrl = AddToCartImage.ImageUrl
                End If
            End With

            Try
                If vBusiness.SaveMenuCategorySettings(vMenuCat) Then
                    BuildMessagesControl(ErrorMessages, AdminConstanst.ChangesHasBeenSaved)
                End If

                If Not (String.IsNullOrWhiteSpace(vColors.BaseColor) AndAlso String.IsNullOrWhiteSpace(vColors.BackColor)) Then
                    vBusiness.SaveColorSettings(vColors)
                End If

            Catch ex As Exception
                messages.Add(AdminConstanst.CouldNotSaveTheChanges)
                BuildErrorMessagesControl(ErrorMessages, messages)
                _log.Error(ex)
            End Try
        End Sub

        Protected Sub EditOrderImageUploadButton_Click(sender As Object, e As System.EventArgs) Handles EditOrderImageUploadButton.Click
            Dim vUrl As String = UploadImageWithoutResize(EditOrderImageFileUpload, Nothing)
            EditOrderImage.ImageUrl = vUrl
        End Sub

        Protected Sub ConfirmOrderUploadButton_Click(sender As Object, e As System.EventArgs) Handles ConfirmOrderUploadButton.Click
            Dim vUrl As String = UploadImageWithoutResize(ConfirmOrderFileUpload, Nothing)
            ConfirmOrderImage.ImageUrl = vUrl
        End Sub

        Protected Sub CheckOutButton_Click(sender As Object, e As System.EventArgs) Handles CheckOutButton.Click
            Dim vUrl As String = UploadImageWithoutResize(CheckOutFileUpload, Nothing)
            CheckOutImage.ImageUrl = vUrl
        End Sub

        Protected Sub AddToCartButton_Click(sender As Object, e As System.EventArgs) Handles AddToCartButton.Click
            Dim vUrl As String = UploadImageWithoutResize(AddToCartFileUpload, Nothing)
            AddToCartImage.ImageUrl = vUrl
        End Sub
    End Class
End Namespace