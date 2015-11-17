Imports System.Globalization
Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports System.IO
Imports INF.Web.UI.Logging.Log4Net
Imports Microsoft.Reporting.WebForms
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.Net.Mail
Imports INF.Web.UI

Partial Class Admin_BasicReport
    Inherits AdminPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Public _type As Integer = 1
    Public _startDate As String = Date.Now.Month & "/" & Date.Now.Day & "/" & Date.Now.Year
    Public _endDate As String = Date.Now.Month & "/" & Date.Now.Day & "/" & Date.Now.Year

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim settingsContent As PlaceHolder = DirectCast(Page.Master.FindControl("SettingsPlaceHolder"), PlaceHolder)
            LoadMonthlyReport()
            If settingsContent IsNot Nothing Then
                settingsContent.Visible = False
            End If
        End If
    End Sub

    Private Sub LoadMonthlyReport()
        Dim vBusiness As New ReportingBusinessLogic()
        Try
            'type report
            Dim types As New Dictionary(Of String, String)
            types.Add("1", "Report Day")
            'types.Add("2", "Report Week")
            'types.Add("3", "Report Month")
            'types.Add("4", "Report Year")
            'ddlTypeReport.DataSource = types
            'ddlTypeReport.DataTextField = "Value"
            'ddlTypeReport.DataValueField = "Key"
            'ddlTypeReport.DataBind()

            'month
            'Dim months As New List(Of String)()
            'For index = 1 To 12
            '    months.Add(index.ToString())
            'Next
            'ddlMonthlyReportMonth.DataSource = months
            ' ddlMonthlyReportMonth.DataBind()
            'ddlMonthlyReportMonth.Items.FindByValue(Date.Now.Month.ToString()).Selected = True

            'year
            'Dim startYear = Date.Now.AddYears(-10)
            'Dim endYear = Date.Now.AddYears(10)
            'Dim years As New List(Of String)()
            'For index = Date.Now.Year - 10 To Date.Now.Year + 10
            '    years.Add(index.ToString())
            'Next
            'ddlMonthlyReportYear.DataSource = years
            'ddlMonthlyReportYear.DataBind()
            'ddlMonthlyReportYear.Items.FindByValue(Date.Now.Year.ToString()).Selected = True

            StartDateOnTextBox.Text = _startDate
            EndDateOnTextBox.Text = _endDate
            BindData()
        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Sub

    Protected Sub ViewReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewReport.Click
        BindData()
    End Sub

    Protected Sub BindData()
        Dim vBusiness As New ReportingBusinessLogic()
        'vBusiness.SaveMonthlyReportMonth(ddlMonthlyReportMonth.SelectedValue.ToString())
        'vBusiness.SaveMonthlyReportYear(ddlMonthlyReportYear.SelectedValue.ToString())
        Dim ukCulture As CultureInfo = New CultureInfo("en-US")
        '_type = Convert.ToInt32(ddlTypeReport.SelectedValue)
        _type = 1
        Dim startDate As DateTime = DateTime.Parse(StartDateOnTextBox.Text, ukCulture.DateTimeFormat) ' Convert.ToDateTime(StartDateOnTextBox.Text)
        Dim endDate As DateTime = DateTime.Parse(EndDateOnTextBox.Text, ukCulture.DateTimeFormat) ' Convert.ToDateTime(EndDateOnTextBox.Text)
        _startDate = startDate.Month & "/" & startDate.Day & "/" & startDate.Year
        _endDate = endDate.Month & "/" & endDate.Day & "/" & endDate.Year
        Dim reportDataSource1 As ReportDataSource = New ReportDataSource()
        reportDataSource1.Name = "PeriodDataSet"
        reportDataSource1.Value = vBusiness.GetReportPeriod(startDate, endDate)

        Dim reportDataSource3 As ReportDataSource = New ReportDataSource()
        reportDataSource3.Name = "OrdersMonthlyDataSet"
        Dim ordersMonthly() As OrdersMonthlyModel = vBusiness.GetOrdersMonthly(GetTypeNameReport(_type), startDate, endDate)
        reportDataSource3.Value = ordersMonthly

        Dim reportDataSource2 As ReportDataSource = New ReportDataSource()
        reportDataSource2.Name = "EnabledChargesDataSet"
        Dim chargesReport() As ChargeModel = vBusiness.GetAvailableCharges()

        If Not IsNothing(ordersMonthly) AndAlso ordersMonthly.Length = 1 Then
            For Each ch As ChargeModel In chargesReport
                If Not IsNothing(ch) AndAlso ch.ChargeOnOrder Then
                    ch.ChargeAmount = ch.ChargeAmount * (ordersMonthly(0).OrdersPaidByCardQuantity + ordersMonthly(0).OrdersPaidByCashQuantity)
                End If
            Next
        End If

        reportDataSource2.Value = chargesReport

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
                Exit Select
        End Select
    End Function

    'Protected Sub btnSendMail_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSendMail.Click
    '    Try
    '        SendMail()
    '    Catch ex As Exception
    '        _log.Error(ex)
    '    End Try
    'End Sub

    Protected Sub RenderFilePrint()
        Dim vBusiness As New ReportingBusinessLogic()
        Try
            Dim warnings As Warning()
            Dim streamids As String()
            Dim mimeType As String
            Dim encoding As String
            Dim extension As String

            Dim bytes As Byte() = BasicMonthlyReportViewer.LocalReport.Render("PDF", Nothing, mimeType, encoding, extension, streamids, warnings)

            Dim fs As New FileStream(HttpContext.Current.Server.MapPath("Report/BasicMonthlyReportViewerOutput.pdf"), FileMode.Create)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()

            'Open exsisting pdf
            Dim document As New Document(PageSize.LETTER)
            Dim reader As New PdfReader(HttpContext.Current.Server.MapPath("Report/BasicMonthlyReportViewerOutput.pdf"))
            'Getting a instance of new pdf wrtiter
            Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(HttpContext.Current.Server.MapPath("Report/BasicMonthlyReportViewerPrint.pdf"), FileMode.Create))
            document.Open()
            Dim cb As PdfContentByte = writer.DirectContent

            Dim i As Integer = 0
            Dim p As Integer = 0
            Dim n As Integer = reader.NumberOfPages
            Dim psize As Rectangle = reader.GetPageSize(1)

            Dim width As Single = psize.Width
            Dim height As Single = psize.Height

            'Add Page to new document
            While i < n
                document.NewPage()
                p += 1
                i += 1

                Dim page1 As PdfImportedPage = writer.GetImportedPage(reader, i)
                cb.AddTemplate(page1, 0, 0)
            End While

            'Attach javascript to the document
            Dim jAction As PdfAction = PdfAction.JavaScript("this.print(true);" & vbCr, writer)
            writer.AddJavaScript(jAction)
            document.Close()

            'Attach pdf to the iframe
            'frmPrint.Attributes("src") = "/Admin/Report/BasicMonthlyReportViewerPrint.pdf"
            'frmPrint.Visible = True
        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Sub

    Private Sub SendMail()
        Dim vBusiness As New EmailSettingBusinessLogic()
        Dim vBusinessEmailSender As New EmailSenderBusinessLogic()
        Dim warnings As Warning()
        Dim streamids As String()
        Dim mimeType As String
        Dim encoding As String
        Dim extension As String

        Dim emailSetting As CsEmailSetting
        emailSetting = vBusiness.GetFirstEmailSetting()
        Dim emailSenders As IEnumerable(Of CsEmailSender) = vBusinessEmailSender.GetAll()

        If emailSetting IsNot Nothing And emailSenders.Count() > 0 Then
            For Each item As CsEmailSender In emailSenders
                '_type = Convert.ToInt32(ddlTypeReport.SelectedValue)
                _type = 1
                Dim subject As String = GetTypeNameReport(_type)
                subject = String.Format("Basic {0} Report", Title)

                Dim bytes As Byte() = BasicMonthlyReportViewer.LocalReport.Render("PDF", Nothing, mimeType, encoding, extension, streamids, warnings)

                Dim memoryStream As New MemoryStream(bytes)
                memoryStream.Seek(0, SeekOrigin.Begin)

                Dim message As New MailMessage()
                Dim attachment As New Attachment(memoryStream, subject & ".pdf")
                message.Attachments.Add(attachment)

                message.From = New MailAddress(emailSetting.Sender)
                message.[To].Add(New MailAddress(item.Email, item.FullName))

                message.Subject = subject
                message.IsBodyHtml = True
                message.Body = subject

                Dim smtp As New SmtpClient(emailSetting.Host)
                smtp.Credentials = New System.Net.NetworkCredential(emailSetting.AuthenticationUser, emailSetting.AuthenticationPassword)
                smtp.EnableSsl = True
                smtp.Port = emailSetting.Port
                smtp.Send(message)

                memoryStream.Close()
                memoryStream.Dispose()
            Next
        End If
    End Sub


End Class
