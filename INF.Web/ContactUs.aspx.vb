Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Partial Class ContactUs
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadContactUsPage()
        End If
    End Sub

    Private Sub LoadContactUsPage()
        Try
            Dim vBusiness As New StaticPageBusinessLogic()
            Dim vPage As CsStaticPage = vBusiness.GetStaticPage("contact_us")
            If vPage IsNot Nothing Then
                MapImage.ImageUrl = vPage.Image
                ContactInfo.Text = vPage.Body
            End If
        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try
    End Sub
End Class
