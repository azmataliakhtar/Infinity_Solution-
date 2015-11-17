<%@ Control Language="vb" AutoEventWireup="false" Inherits="INF.Web.Public.default.Header" %>
<header>

    <div class="pageToolbatTop">
        <asp:Panel runat="server" ID="pnlOrderTypeAndPostCode">
            <p>
                <asp:Literal runat="server" ID="ltrOrderType"></asp:Literal>
                <asp:Literal runat="server" ID="ltrPostcode"></asp:Literal>
                <a id="reset_order_type" href="javascript:void(0);" class="reset_order_type">
                    <%--<img src="Images/Del.png" alt="reset" />--%>
                    <asp:Image runat="server" AlternateText="Reset Order Type" SkinID="ResetOrderTypeImage" />
                </a>

            </p>
        </asp:Panel>
        <ajaxToolkit:AlwaysVisibleControlExtender runat="server" ID="avceOrderTypeAndPostCode"
            TargetControlID="pnlOrderTypeAndPostCode" VerticalSide="Top" VerticalOffset="0"
            HorizontalSide="Center" HorizontalOffset="10" ScrollEffectDuration=".1" />
    </div>

    <div class="logo"></div>

    <div class="shopName"></div>

    <div id="timelabel" class="timeBlock"></div>
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
                document.getElementById("timelabel").innerHTML="Shop Opening <br/> at" + "12:00 PM";
            } else {
                var second = Math.floor(leave) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);

                hour = hour < 10 ? "0" + hour : hour;
                minute = minute < 10 ? "0" + minute : minute;
                second = second < 10 ? "0" + second : second;

                var remain = "Closing in <br/>" + hour + ":" + minute + ":" + second;
                leave = leave - 1;

                document.getElementById("timelabel").innerHTML = "<br/>" + "" + remain + "";
            }
        }
    </script>
    <script type="text/javascript">
        var leave =<%=RemainingTime%>;
        var interv = setInterval(CounterTimer, 1000);
        CounterTimer();
    </script>

    <div class="login">
        <% If Not (HttpContext.Current.User IsNot Nothing AndAlso HttpContext.Current.User.Identity IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))) Then%>
        <a href="login.aspx" rel="sb1">Login or Register Here</a>
        <% 
        Else
            Dim vFormIdentity As FormsIdentity = TryCast(HttpContext.Current.User.Identity, FormsIdentity)
            Dim vFullName As String = String.Empty
            If (vFormIdentity IsNot Nothing) Then
                vFullName = vFormIdentity.Ticket.UserData
            End If
        %>
        <span>Welcome&nbsp;<strong><%=vFullName%></strong> </span>|
            <asp:LinkButton ID="lnkEditProfile" runat="server" Text="Settings"
                PostBackUrl="Profile.aspx"></asp:LinkButton>
        |
            <asp:LinkButton ID="lnkLogout" runat="server" Text="Log Out"
                CausesValidation="false"></asp:LinkButton>
        <% End If%>
    </div>

    <nav id="ddtabs3" class="navButtons">
        <ul>
            <li class="nhome"><a href="Default.aspx">Home</a></li>
            <li class="nmenu"><a href="Menu.aspx">Menu</a></li>
            <li class="ntrack"><a href="Menu.aspx?CategoryId=10">Deals</a></li>
            <li class="ntrack"><a href="TrackOrder.aspx">Track Order</a></li>
            <li class="nfeedback"><a href="Feedback.aspx">Feedback</a></li>
        </ul>
    </nav>

</header>
