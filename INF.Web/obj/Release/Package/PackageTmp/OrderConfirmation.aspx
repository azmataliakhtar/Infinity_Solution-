<%@ Page Language="VB" AutoEventWireup="false" Inherits="INF.Web.OrderConfirmation" Codebehind="OrderConfirmation.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="Scripts/jquery-1.8.2.js"></script>
</head>
<body>
   <form runat="server" class="SagePayForm" name="SagePayForm" method="POST" id="frmSagePay">
       <input runat="server" type="hidden" name="VPSProtocol" value="" id="VPSProtocol"/>
       <input runat="server" type="hidden" name="TxType" value="" id="TxType"/>
       <input runat="server" type="hidden" name="Vendor" value="" id="Vendor"/>
       <input runat="server" type="hidden" name="Crypt" value="" id="Crypt"/>
       
   </form>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $(".SagePayForm").submit();
        });
    </script>
</body>
</html>
