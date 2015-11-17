Imports System.Web.SessionState
Imports System.Runtime.CompilerServices
Imports INF.Web.UI.Settings
Imports log4net
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Public Class GlobalAsax
    Inherits System.Web.HttpApplication

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        log4net.Config.XmlConfigurator.Configure()

        Try
            EPATheme.Current.LoadThemeSettings()
        Catch ex As Exception
            _log.Error(ex)

            'Try one again
            Try
                EPATheme.Current.LoadThemeSettings()
            Catch ex1 As Exception
                _log.Error(ex)
            End Try
        End Try
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
        '***************************************************************
        ' Uncomment these lines to enable the mobile version.
        '***************************************************************
        'Dim isMobileDevice As Boolean = HttpContext.Current.Request.Browser.IsMobileDevice
        'If isMobileDevice Then
        '    If Not HttpContext.Current.Request.FilePath.Contains("/Mobile") AndAlso Not HttpContext.Current.Request.FilePath.Contains("/Ajax") AndAlso (HttpContext.Current.Request.FilePath.Contains(".aspx") OrElse HttpContext.Current.Request.FilePath.Equals("/")) Then
        '        HttpContext.Current.Response.Redirect("~/Mobile" & HttpContext.Current.Request.FilePath)
        '    End If
        'End If
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
        Dim ex As Exception = Server.GetLastError()
        _log.Error(ex)

        Session("EPOS_ERROR_MESSAGE") = "If you are in progress of Checkout Order, then kindly contact the website owener."
        Server.ClearError()
        Response.RedirectTo("~/Error.aspx?msg=EPOS_ERROR_MESSAGE")
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class