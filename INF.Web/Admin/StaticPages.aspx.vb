Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.My.Resources
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Namespace Admin

    Partial Class StaticPages
        Inherits AdminPage

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                Call ShowContactUsView()
            End If
        End Sub

        Protected Sub MapImageFileUploadButton_Click(sender As Object, e As System.EventArgs) Handles MapImageFileUploadButton.Click
            If MapImageFileUpload.HasFile Then
                Dim url As String = UploadImageWithoutResize(MapImageFileUpload, String.Empty)
                MapImage.ImageUrl = url
            End If
        End Sub

        Protected Sub SaveChanges_Click(sender As Object, e As System.EventArgs) Handles SaveChanges.Click

            If StaticPagesMultiView.GetActiveView().Equals(ContactUsView) Then
                Call SaveContactUsPage()
                Exit Sub
            End If

            If StaticPagesMultiView.GetActiveView().Equals(AboutUsView) Then
                Call SaveAboutUsPage()
                Exit Sub
            End If
        End Sub

        Private Sub ShowContactUsView()
            Call LoadContactUs()
            StaticPagesTitle.Text = "Contact Us"
            StaticPagesMultiView.SetActiveView(ContactUsView)
        End Sub

        Private Sub LoadContactUs()
            Try
                Dim vBusiness As New StaticPageBusinessLogic()
                Dim vPage As CsStaticPage = vBusiness.GetStaticPage("contact_us")
                If vPage IsNot Nothing Then
                    MapImage.ImageUrl = vPage.Image
                    ContactInfo.Text = vPage.Body

                    ContactUsID.Value = vPage.ID.ToString()
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try
        End Sub

        Private Sub SaveContactUsPage()
            Try
                Dim vPage As New CsStaticPage()
                With vPage
                    .PageName = "contact_us"
                    .Body = ContactInfo.Text
                    .Image = MapImage.ImageUrl

                    If Not String.IsNullOrWhiteSpace(ContactUsID.Value) AndAlso IsNumeric(ContactUsID.Value) Then
                        .ID = CInt(ContactUsID.Value)
                    End If
                End With

                Dim vBusiness As New StaticPageBusinessLogic()
                Dim savedPage As CsStaticPage = vBusiness.SaveStaticPage(vPage)
                If savedPage IsNot Nothing Then
                    BuildMessagesControl(ErrorMessages, AdminConstanst.ChangesHasBeenSaved)
                    LoadContactUs()
                End If

            Catch ex As Exception
                Dim errors As New List(Of String)
                errors.Add(AdminConstanst.CouldNotSaveTheChanges)
                BuildErrorMessagesControl(ErrorMessages, errors)
                _log.Error(ex)
            End Try
        End Sub

        Private Sub ShowAboutUsView()
            Call LoadAboutUs()
            StaticPagesTitle.Text = "About Us"
            StaticPagesMultiView.SetActiveView(AboutUsView)
        End Sub

        Private Sub LoadAboutUs()
            Try
                Dim vBusiness As New StaticPageBusinessLogic()
                Dim vPage As CsStaticPage = vBusiness.GetStaticPage("about_us")
                If vPage IsNot Nothing Then
                    AboutUsEditor.Text = vPage.Body
                    AboutUsID.Value = vPage.ID.ToString()
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try
        End Sub

        Private Sub SaveAboutUsPage()
            Try
                Dim vPage As New CsStaticPage()
                With vPage
                    .PageName = "about_us"
                    .Body = AboutUsEditor.Text

                    If Not String.IsNullOrWhiteSpace(AboutUsID.Value) AndAlso IsNumeric(AboutUsID.Value) Then
                        .ID = CInt(AboutUsID.Value)
                    End If
                End With

                Dim vBusiness As New StaticPageBusinessLogic()
                Dim savedPage As CsStaticPage = vBusiness.SaveStaticPage(vPage)
                If savedPage IsNot Nothing Then
                    BuildMessagesControl(ErrorMessages, AdminConstanst.ChangesHasBeenSaved)
                    LoadContactUs()
                End If

            Catch ex As Exception
                Dim errors As New List(Of String)
                errors.Add(AdminConstanst.CouldNotSaveTheChanges)
                BuildErrorMessagesControl(ErrorMessages, errors)
                _log.Error(ex)
            End Try
        End Sub

        Protected Sub ContactUsViewLink_Click(sender As Object, e As System.EventArgs) Handles ContactUsViewLink.Click
            Call ShowContactUsView()
        End Sub

        Protected Sub AboutUsViewLink_Click(sender As Object, e As System.EventArgs) Handles AboutUsViewLink.Click
            Call ShowAboutUsView()
        End Sub
    End Class
End Namespace