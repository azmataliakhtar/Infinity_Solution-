<%@ Page Language="VB" AutoEventWireup="false" Inherits="INF.Web.Admin_Report_PrintSalesReport" Codebehind="PrintSalesReport.aspx.vb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript" language="javascript" src="/Scripts/jquery-1.8.2.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="ToolkitScriptManager1" />
        <div style="width: 100%;display:block;" id="rpt-container">
             <rsweb:ReportViewer ID="SalesReportViewer" AsyncRendering="false"  ShowToolBar="false"
                runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="100%">
                <LocalReport ReportPath="Admin\rdlc\SalesReport.rdlc">
            
                </LocalReport>
            </rsweb:ReportViewer>
        
            <asp:ScriptManagerProxy ID="proxy" runat="server">
                 <Scripts>
                   <asp:ScriptReference Path="/Scripts/fixReportViewer.js" />
                </Scripts>
           </asp:ScriptManagerProxy>
       </div>
       <script type="text/javascript">
           setTimeout(showreport, 300);
           function showreport() {
               window.print();
           }
       </script>
    </form>
</body>
</html>
