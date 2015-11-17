
Imports INF.Web.Data.BLL
Imports Microsoft.Reporting.WebForms

Partial Class Admin_Report_PrintReporting
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim vBusiness As New ReportingBusinessLogic()
        Dim _type As Integer = Request.QueryString("type")
        Dim reportName As String = GetTypeNameReport(_type)
        Dim startDate As Date = Convert.ToDateTime(Request.QueryString("startDate"))
        Dim endDate As Date = Convert.ToDateTime(Request.QueryString("endDate"))
        Dim reportDataSource1 As ReportDataSource = New ReportDataSource()
        reportDataSource1.Name = "PeriodDataSet"
        reportDataSource1.Value = vBusiness.GetReportPeriod(startDate, endDate)

        Dim reportDataSource3 As ReportDataSource = New ReportDataSource()
        reportDataSource3.Name = "OrdersMonthlyDataSet"
        Dim ordersMonthly() As OrdersMonthlyModel = vBusiness.GetOrdersMonthly(reportName, startDate, endDate)
        reportDataSource3.Value = ordersMonthly

        Dim reportDataSource2 As ReportDataSource = New ReportDataSource()
        reportDataSource2.Name = "EnabledChargesDataSet"
        Dim chargesReport() As ChargeModel = vBusiness.GetAvailableCharges()

        If Not IsNothing(ordersMonthly) AndAlso ordersMonthly.Length = 1 Then
            For Each ch As ChargeModel In chargesReport
                If Not IsNothing(ch) AndAlso ch.ChargeOnOrder Then
                    ch.ChargeAmount = ch.ChargeAmount * ordersMonthly(0).OrdersPaidByCardQuantity
                End If
            Next
        End If
        
        reportDataSource2.Value = chargesReport

        Title = String.Format("Basic {0} Report", reportName)

        BasicMonthlyReportViewer.LocalReport.DataSources.Clear()
        BasicMonthlyReportViewer.LocalReport.DataSources.Add(reportDataSource1)
        BasicMonthlyReportViewer.LocalReport.DataSources.Add(reportDataSource2)
        BasicMonthlyReportViewer.LocalReport.DataSources.Add(reportDataSource3)
        BasicMonthlyReportViewer.DataBind()
        BasicMonthlyReportViewer.LocalReport.Refresh()
    End Sub

    Private Function GetTypeNameReport(ByVal type As Integer) As String
        Select Case type
            Case 1
                Return "Daily"
            Case 2
                Return "Weekly"
            Case 3
                Return "Monthly"
            Case 4
                Return "Yearly"
            Case Else
                Return "Dayly"
        End Select
    End Function

End Class
