<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/ReportingMaster.master" AutoEventWireup="false" Inherits="INF.Web.Admin.SalesReport" CodeBehind="SalesReport.aspx.vb" %>

<%@ Import Namespace="INF.Web.UI.Settings" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="Server">
    <%=EPATheme.Current.Themes.WebsiteName%> - Sales Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <script type="text/javascript">
        $(function () {
            $("#<%= StartDateOnTextBox.ClientID %>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                changeWeek: true,
                showWeek: true,
                dateFormat: 'mm/dd/yy',
                numberOfMonths: 1,
                onClose: function (selectedDate) {
                    $("#<%= EndDateOnTextBox.ClientID %>").datepicker("option", "minDate", selectedDate);
                 }

             });
            $("#<%= EndDateOnTextBox.ClientID %>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                showWeek: true,
                dateFormat: 'mm/dd/yy',
                numberOfMonths: 1,
                onClose: function (selectedDate) {
                    $("#<%= StartDateOnTextBox.ClientID %>").datepicker("option", "maxDate", selectedDate);
                 }
             });
        });
    </script>
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="ToolkitScriptManager1" />
    <div class="row">
        <h1 class="page-header page-title">Sales Report</h1>
    </div>
    <div class="row">
        <div class="form-group">
            <div class="col-md-8 form-inline">
                <label style="font-weight: bold;">Start Date:</label>&nbsp;&nbsp;
                <asp:TextBox runat="server" ID="StartDateOnTextBox" Width="120px" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                <label style="font-weight: bold;">End Date:</label>&nbsp;&nbsp;
                <asp:TextBox runat="server" ID="EndDateOnTextBox" Width="120px" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4 pull-right">
                <asp:Button runat="server" ID="ViewReport" Text="Report" CssClass="btn btn-danger" Width="110" />
            </div>
        </div>
    </div>
    <div class="row">
        <rsweb:ReportViewer ID="SalesReportViewer" AsyncRendering="true" runat="server" Width="100%" Font-Names="Arial" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="100%">
            <LocalReport ReportPath="Admin\rdlc\SalesReport.rdlc"></LocalReport>
        </rsweb:ReportViewer>
    </div>
    <asp:ScriptManagerProxy ID="proxy" runat="server">
        <Scripts>
            <asp:ScriptReference Path="/Scripts/fixReportViewer.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
</asp:Content>
