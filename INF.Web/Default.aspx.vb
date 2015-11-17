Imports INF.Web.UI
Imports System.IO
Imports INF.Web.UI.Logging.Log4Net

Partial Class _Default
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        _log.Info("BEGIN")

        If Not Page.IsPostBack Then
        End If

        _log.Info("END")
    End Sub

    Protected Overrides Sub OnInit(ByVal e As EventArgs)
        _log.Info("BEGIN")

        MyBase.OnInit(e)

        If IsNothing(phDefaultPage) Then
            _log.Info("END")
            Exit Sub
        End If

        Try
            phDefaultPage.Controls.Clear()
            Dim virtualPathDefault As String = "~/public/" + INFTheme + "/DefaultPageUserControl.ascx"
            Dim physicalPathDefault As String = HttpContext.Current.Server.MapPath(virtualPathDefault)
            If Not File.Exists(physicalPathDefault) Then
                virtualPathDefault = "~/public/default/DefaultPageUserControl.ascx"
            End If

            Dim defaultPage As UserControl = LoadControl(virtualPathDefault)
            If Not IsNothing(defaultPage) Then
                phDefaultPage.Controls.Add(defaultPage)
            End If
        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try

        _log.Info("END")
    End Sub
End Class