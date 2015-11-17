Imports INF.Web.Data.BLL
Imports INF.Web.My.Resources
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Namespace Admin
    Partial Class Info
        Inherits AdminPage

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                LoadWebsiteInformation()
            End If
        End Sub

        Protected Sub SaveChanges_Click(sender As Object, e As System.EventArgs) Handles SaveChanges.Click
            SaveWebsiteInformation()
        End Sub

        Private Sub LoadWebsiteInformation()
            Dim bBusiness As New ThemesSettingsBusinessLogic()
            Dim websiteInfo As WebsiteInformation = bBusiness.LoadWebsiteInformation()
            If websiteInfo IsNot Nothing Then
                WebsiteName.Text = websiteInfo.Name
                WebsiteMeta.Text = websiteInfo.Meta
            End If
        End Sub

        Private Sub SaveWebsiteInformation()
            If Page.IsValid Then
                Dim vBusiness As New ThemesSettingsBusinessLogic()
                Dim vWebsiteInfo As New WebsiteInformation()

                With vWebsiteInfo
                    .Name = WebsiteName.Text.Trim()
                    .Meta = WebsiteMeta.Text.Trim()
                End With

                Try
                    If vBusiness.SaveWebsiteInformation(vWebsiteInfo) Then

                        LoadWebsiteInformation()
                        MessageLabel.Text = AdminConstanst.ChangesHasBeenSaved
                        MessageLabel.CssClass = "good-msg"
                        MessageLabel.Visible = True
                    End If
                Catch ex As Exception
                    MessageLabel.Text = AdminConstanst.CouldNotSaveTheChanges
                    MessageLabel.Visible = True
                    MessageLabel.CssClass = "bad-msg"
                    _log.Error(ex)
                End Try

            End If
        End Sub

    End Class
End Namespace