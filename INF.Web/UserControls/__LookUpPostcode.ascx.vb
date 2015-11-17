Imports INF.Web.UI.Settings
Imports INF.Web.UI.Utils
Imports INF.Web.UI

Namespace UserControls
    Public Class __LookUpPostcode
        Inherits BaseUserControl

        Private _backgroundCss As String
        Protected Property BackgroundCss() As String
            Set(ByVal value As String)
                _backgroundCss = value
            End Set
            Get
                Return _backgroundCss
            End Get
        End Property

        Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
            Dim vBaseColor As String = EPATheme.Current.Themes.BaseColor
            Dim vBackColor As String = EPATheme.Current.Themes.BackColor
            BackgroundCss = WebUtil.BuildCssGradientBackgound(vBaseColor, vBackColor)
        End Sub

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        End Sub

    End Class
End Namespace