
Imports INF.Web.Data.BLL
Imports Microsoft.Reporting.WebForms

Partial Class Admin_Report_PrintSalesReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim vBusiness As New ReportingBusinessLogic()
        Dim _type As Integer = Request.QueryString("type")
        Dim reportName As String = GetTypeNameReport(_type)
        Title = String.Format("Basic {0} Report", reportName)
        Dim startDate As Date = Convert.ToDateTime(Request.QueryString("startDate"))
        Dim endDate As Date = Convert.ToDateTime(Request.QueryString("endDate"))
        Dim reportDataSource1 As ReportDataSource = New ReportDataSource()
        reportDataSource1.Name = "ReportPeriodDataSet"
        reportDataSource1.Value = vBusiness.GetReportPeriod(startDate, endDate)
        Dim reportDataSource2 As ReportDataSource = New ReportDataSource()
        reportDataSource2.Name = "MonthlySalesDataSet"
        Dim reportInfo As ReportModel() = vBusiness.GetRestaurantInfo()
        reportInfo(0).Title = String.Format("Basic {0} Report", reportName)
        reportDataSource2.Value = reportInfo
        Dim reportDataSource3 As ReportDataSource = New ReportDataSource()
        reportDataSource3.Name = "CustomersDataSet"
        reportDataSource3.Value = vBusiness.GetCustomersReport(startDate, endDate)
        Dim reportDataSource4 As ReportDataSource = New ReportDataSource()
        reportDataSource4.Name = "BestItemsSoldDataSet"
        reportDataSource4.Value = vBusiness.GetBestItemSold()
        Dim reportDataSource5 As ReportDataSource = New ReportDataSource()
        reportDataSource5.Name = "OrdersInStatusesDataSet"
        reportDataSource5.Value = vBusiness.GetOrdersInStatuses(startDate, endDate)
        Dim reportDataSource6 As ReportDataSource = New ReportDataSource()
        reportDataSource6.Name = "SalesComparisonDataSet"
        reportDataSource6.Value = vBusiness.GetSalesComparison(startDate, endDate)

        SalesReportViewer.LocalReport.DataSources.Clear()
        SalesReportViewer.LocalReport.DataSources.Add(reportDataSource1)
        SalesReportViewer.LocalReport.DataSources.Add(reportDataSource2)
        SalesReportViewer.LocalReport.DataSources.Add(reportDataSource3)
        SalesReportViewer.LocalReport.DataSources.Add(reportDataSource4)
        SalesReportViewer.LocalReport.DataSources.Add(reportDataSource5)
        SalesReportViewer.LocalReport.DataSources.Add(reportDataSource6)
        SalesReportViewer.DataBind()
        SalesReportViewer.LocalReport.Refresh()
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
                Exit Select
        End Select
    End Function

End Class
