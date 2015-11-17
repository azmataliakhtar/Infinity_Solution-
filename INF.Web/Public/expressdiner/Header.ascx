<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="../default/Header.ascx.vb" Inherits="INF.Web.Public.default.Header" %>
<%@ Import Namespace="INF.Web.UI.Settings" %>
<script type="text/javascript">
    // Time left for opening shop
    var timeleft = <%=RemainingTime%>;
    /**
    * This function is to check whether the shop is available for placing order.
    **/               					
    function IsShopOpen() {
        var day = Math.floor(timeleft / (60 * 60 * 24));
        var hours = Math.floor(timeleft / 3600) - (day * 24);
        var minutes = Math.floor(timeleft / 60) - (day * 24 * 60) - (hours * 60);
                                					
        if((hours <= 0 && minutes <= 0) || hours > 14.5 ) {
            return false;
        }
        return true;
    }
        
    function CounterTimer() {
        var day = Math.floor(leave / (60 * 60 * 24));
        var hour = Math.floor(leave / 3600) - (day * 24);
        var minute = Math.floor(leave / 60) - (day * 24 * 60) - (hour * 60);
                        					
        if((hour<=0 && minute <=0 ) || hour > 14.5 ) {
            document.getElementById("timelabel").innerHTML="<div><font size='2px' color='Black'>Shop Open at</font></div>" + "<div><font size='3px' color='Red'>" + "12:00 PM" + "</font></div>";
        } else {
            var second = Math.floor(leave) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);

            hour = hour < 10 ? "0" + hour : hour;
            minute = minute < 10 ? "0" + minute : minute;
            second = second < 10 ? "0" + second : second;

            var remain = hour + ":" + minute + ":" + second;
            leave = leave - 1;

            document.getElementById("timelabel").innerHTML = "<div><font size='3px' color='Black'>Closing In</font></div>" + "<div><font size='4px' color='Maroon'>" + remain + "</font></div>";
        }
    } 
</script>
<header class="header col-md-12 col-xs-12" style="background-image: url('<%= EPATheme.Current.Themes.BackgroundHeader %>');">
    <div class="page-toolbat-top">
        <asp:Panel runat="server" ID="pnlOrderTypeAndPostCode" Width="1000px" Style="z-index: 1; background-color: transparent; margin-left: 0px;">
            <div style="vertical-align: top; text-align: left; padding: 1px;">
                <p style="margin-top: 0px; margin-bottom: 0px; font-size: 11px; font-weight: bold; color: whitesmoke;">
                    <asp:Literal runat="server" ID="ltrOrderType"></asp:Literal>
                    <asp:Literal runat="server" ID="ltrPostcode"></asp:Literal>
                    <a id="reset_order_type" href="javascript:void(0);" class="reset_order_type">
                        <img src="Images/Del.png" alt="reset" /></a>
                </p>
            </div>
        </asp:Panel>
        <%--<ajaxToolkit:AlwaysVisibleControlExtender runat="server" ID="avceOrderTypeAndPostCode"
            TargetControlID="pnlOrderTypeAndPostCode" VerticalSide="Top" VerticalOffset="0"
            HorizontalSide="Center" HorizontalOffset="10" ScrollEffectDuration=".1" />--%>
    </div>
    <div class="row" style="padding-top: 5px; padding-bottom: 5px;">
        <div class="col-md-12 col-xs-12">
            <div id="timelabel" class="TimeBlock">
            </div>
            <script type="text/javascript">
                var leave = <%=RemainingTime%>;
                var interv = setInterval(CounterTimer, 1000);
                CounterTimer();
            </script>
        </div>
    </div>
    <div class="row" style="padding-top: 5px; padding-bottom: 5px;">
        <div class="col-md-12 col-xs-12">
            <div class="text-right">
                <% If Not (HttpContext.Current.User IsNot Nothing AndAlso HttpContext.Current.User.Identity IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))) Then%>
                <a href="../../login.aspx" rel="sb1">
                    <asp:Image ID="lnkLogin" runat="server" ImageAlign="Middle" ImageUrl="/App_Themes/expressdiner/Images/Login.png" />
                </a>
                <% 
                Else
                    Dim vFormIdentity As FormsIdentity = TryCast(HttpContext.Current.User.Identity, FormsIdentity)
                    Dim vFullName As String = String.Empty
                    If (vFormIdentity IsNot Nothing) Then
                        vFullName = vFormIdentity.Ticket.UserData
                    End If
                %>
               <%-- <span style="color: #ffffff;font-size: 14px;">Welcome&nbsp;<strong><%=vFullName%></strong></span>&nbsp;
                |&nbsp;<asp:LinkButton ID="lnkEditProfile" ForeColor="White" runat="server" Text="Settings" Style="text-decoration: none; color: royalblue;font-size: 14px;" PostBackUrl="Profile.aspx"></asp:LinkButton>
                |&nbsp;<asp:LinkButton ID="lnkLogout" ForeColor="Maroon" runat="server" Text="Log Out" Style="text-decoration: none; color: royalblue;font-size: 14px;" CausesValidation="false"></asp:LinkButton>--%>
                <span style="color: #ffffff;font-size: 14px;">Welcome&nbsp;<strong><%=vFullName%></strong></span>&nbsp;
                |&nbsp;<a href="../../Profile.aspx" style="text-decoration: none; color: royalblue;font-size: 14px;">Settings</a>
                |&nbsp;<a href="../../LogOut.aspx" style="text-decoration: none; color: royalblue;font-size: 14px;">Log Out</a>
                <% End If%>
            </div>
        </div>
    </div>
    <div class="row" style="padding-top: 5px; padding-bottom: 5px;">
        <div class="col-md-12 col-xs-12">
            <div class="MenuContainer">
                <div id="ddtabs3" class="solidblockmenu">
                    <ul>
                        <li><a href="Default.aspx" rel="sb1">Home</a> </li>
                        <li><a href="Menu.aspx" rel="sb3">Menu</a> </li>
                        <li><a href="TrackOrder.aspx">Track</a> </li>
                        <li><a href="Feedback.aspx">Feedback</a> </li>
                        <li><a href="ContactUs.aspx">Contact Us</a> </li>
                        <li><a href="AboutUs.aspx">About Us</a> </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</header>
<script type="text/javascript">
    $(document).ready(function () {

        $('.solidblockmenu a').css('background-image', 'url(<%= EPATheme.Current.Themes.BackgroundNavigation %>)');

        $('.solidblockmenu a').hover(function () {
            $(this).css('background-image', 'url(<%= EPATheme.Current.Themes.BackgroundNavigationHover %>)');
        });

        $('.solidblockmenu a').mouseout(function () {
            $(this).css('background-image', 'url(<%= EPATheme.Current.Themes.BackgroundNavigation %>)');
        });

        $("#reset_order_type").click(function() {
            ResetOrderType();
        });
    });
    
    function ResetOrderType() {
        $.ajax({
            url:"/ajax/ResetOrderTypeHandler.ashx",
            type:"POST",
            data: {
                'order_type': 'COLLECTION'
            },
            dataType:'html',
            success: function(result) {
                window.location.href = window.location.origin;
            },
            error:function(error) {
            }
        });
    }
</script>