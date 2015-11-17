Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Namespace Admin
    Partial Class ReportSettings
        Inherits AdminPage

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                Dim settingsContent As PlaceHolder = DirectCast(Page.Master.FindControl("SettingsPlaceHolder"), PlaceHolder)

                If settingsContent IsNot Nothing Then
                    settingsContent.Visible = False
                End If

                ServiceNameTextBox.Enabled = True
                If Request.QueryString("type") IsNot Nothing Then
                    If Request.QueryString("type").Equals("edit") Then
                        LoadFormEdit()
                    End If
                End If

                Call LoadServicesCharge()
            End If
        End Sub

        Protected Sub SaveSettingsButton_Click(sender As Object, e As EventArgs) Handles SaveSettingsButton.Click

        End Sub

        Private Sub LoadFormEdit()
            Dim vBusiness As New ReportingBusinessLogic()
            Try
                Dim csServicesCharge As CsServicesCharge = vBusiness.GetServiceCharge(Request.QueryString("id"))
                If csServicesCharge IsNot Nothing Then
                    ServiceNameTextBox.Text = csServicesCharge.Name
                    ServiceNameTextBox.Enabled = True
                    ServiceChargeTextBox.Text = csServicesCharge.Charge.ToString()
                    ServiceEnabledCheckBox.Checked = csServicesCharge.IsActived
                    chkChargeOnOrder.Checked = csServicesCharge.ChargeOnOrder
                End If
                Dim vServices As IEnumerable(Of CsServicesCharge) = vBusiness.GetAllServicesCharge()
                ServicesChargeDataGrid.DataSource = vServices
                ServicesChargeDataGrid.DataBind()
            Catch ex As Exception
                _log.Error(ex)
            End Try
        End Sub

        Private Sub LoadServicesCharge()
            Dim vBusiness As New ReportingBusinessLogic()
            Try
                Dim vServices As IEnumerable(Of CsServicesCharge) = vBusiness.GetAllServicesCharge()
                ServicesChargeDataGrid.DataSource = vServices
                ServicesChargeDataGrid.DataBind()
            Catch ex As Exception
                _log.Error(ex)
            End Try
        End Sub

        Protected Sub SaveServiceChargeButton_Click(sender As Object, e As EventArgs) Handles SaveServiceChargeButton.Click
            Dim vBusiness As New ReportingBusinessLogic()
            Try
                Dim vService As New CsServicesCharge()
                If "edit".Equals(Request.QueryString("type")) Then
                    With vService
                        .ID = Request.QueryString("id")
                        .Name = ServiceNameTextBox.Text.Trim()
                        .Charge = CDbl(ServiceChargeTextBox.Text.Trim())
                        .IsActived = ServiceEnabledCheckBox.Checked
                        .ChargeOnOrder = chkChargeOnOrder.Checked

                        If vBusiness.SaveServiceCharge(vService) IsNot Nothing Then
                            'LoadServicesCharge()
                            Response.RedirectTo("ReportSettings.aspx")
                        End If
                    End With
                Else
                    With vService
                        .Name = ServiceNameTextBox.Text.Trim()
                        .Charge = CDbl(ServiceChargeTextBox.Text.Trim())
                        .IsActived = ServiceEnabledCheckBox.Checked
                        .ChargeOnOrder = chkChargeOnOrder.Checked

                        If vBusiness.SaveServiceCharge(vService) IsNot Nothing Then
                            LoadServicesCharge()
                        End If
                    End With
                End If

            Catch ex As Exception
                _log.Error(ex)
            End Try
        End Sub


    End Class
End Namespace