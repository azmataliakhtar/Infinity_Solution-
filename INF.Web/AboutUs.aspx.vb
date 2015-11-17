
Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Partial Class AboutUs
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call LoadAboutUsContent()
        End If
    End Sub

    Private Sub LoadAboutUsContent()
        Try
            Dim vBusiness As New StaticPageBusinessLogic()
            Dim vPage As CsStaticPage = vBusiness.GetStaticPage("about_us")
            If vPage IsNot Nothing Then
                AboutUsContent.Text = vPage.Body
            End If
        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try
    End Sub
End Class
