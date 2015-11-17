<%@ Control Language="vb" AutoEventWireup="false" Inherits="INF.Web.Public.default.Header" %>
<header>
		    <div class="heading"><img src="/public/pizzaperfect/images/heading.png"></div>
		    <div class="page-toolbat-top">
                    <asp:Panel runat="server" ID="pnlOrderTypeAndPostCode" Width="1000px" Style="z-index: 1; background-color: transparent; margin-left: 0px;">
                        <div>
                            <p class="pageToolbatTop">
                                <asp:Literal runat="server" ID="ltrOrderType"></asp:Literal>
                                <asp:Literal runat="server" ID="ltrPostcode"></asp:Literal>
                                <%--<asp:PlaceHolder runat="server" ID="phOrderTypeReset" Visible="False">--%>
                                <a id="reset_order_type" href="javascript:void(0);" class="reset_order_type">
                                    <img src="/public/pizzaperfect/Images/Del.png" alt="reset" /></a>
                                <%--</asp:PlaceHolder>--%>
                            </p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:AlwaysVisibleControlExtender runat="server" ID="avceOrderTypeAndPostCode"
                        TargetControlID="pnlOrderTypeAndPostCode" VerticalSide="Top" VerticalOffset="0"
                        HorizontalSide="Center" HorizontalOffset="10" ScrollEffectDuration=".1" />
		    </div>
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
                            document.getElementById("timelabel").innerHTML="Shop Open at 15:00 PM";
                        } else {
                            var second = Math.floor(leave) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);

                            hour = hour < 10 ? "0" + hour : hour;
                            minute = minute < 10 ? "0" + minute : minute;
                            second = second < 10 ? "0" + second : second;

                            var remain = "Closing In " + hour + ":" + minute + ":" + second;
                            leave = leave - 1;

                            document.getElementById("timelabel").innerHTML = " " + " " + remain + " ";
                        }
                    } 
                </script>
                <script type="text/javascript">
                    var leave = <%=RemainingTime%>
                    var interv = setInterval(CounterTimer, 1000);
                    CounterTimer();
                </script>

		    <div class="login">
                                    <% If Not (HttpContext.Current.User IsNot Nothing AndAlso HttpContext.Current.User.Identity IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))) Then%>
                                    <a href="login.aspx" rel="sb1">
                                        <asp:Image ID="lnkLogin" runat="server" ImageUrl="/public/pizzaperfect/Images/Login.png" /></a>
                                    <% 
                                    Else
                                        Dim vFormIdentity As FormsIdentity = TryCast(HttpContext.Current.User.Identity, FormsIdentity)
                                        Dim vFullName As String = String.Empty
                                        If (vFormIdentity IsNot Nothing) Then
                                            vFullName = vFormIdentity.Ticket.UserData
                                        End If
                                    %>
                                    <span style="color: #363636;">Welcome&nbsp;<strong><%=vFullName%></strong> </span>
                                    |
                                <asp:LinkButton ID="lnkEditProfile" ForeColor="White" runat="server" Text="Settings"
                                    Style="text-decoration: none; color: Maroon" PostBackUrl="Profile.aspx"></asp:LinkButton>
                                    |
                                <asp:LinkButton ID="lnkLogout" ForeColor="Maroon" runat="server" Text="Log Out" Style="text-decoration: none; color: Maroon"
                                    CausesValidation="false"></asp:LinkButton>
                                    <% End If%>
                    </div>
                </header>
<div>
    <summary id="menu"></summary>
    <nav id="navMobile">
			<ul>
			    <a href="http://pizzaperfect.uk.com/"><li>Home</li></a>
			    <a href="http://pizzaperfect.uk.com/Menu.aspx?CategoryId=1"><li>Starters & Sides</li></a>
			    <a href="http://pizzaperfect.uk.com/Menu.aspx?CategoryId=2"><li>Pizza's</li></a>
			    <a href="http://pizzaperfect.uk.com/Menu.aspx?CategoryId=3"><li>Meal Deal</li></a>
			    <a href="http://pizzaperfect.uk.com/Menu.aspx?CategoryId=4"><li>Desserts</li></a>
			    <a href="http://pizzaperfect.uk.com/Menu.aspx?CategoryId=5"><li>Drinks</li></a>
			</ul>
		    </nav>
</div>
<nav id="ddtabs3" class="navButtons">
		    <ul>
			<li><a href="Default.aspx" rel="sb1">Home</a> </li>
			<li class="active"><a href="Menu.aspx" rel="sb3">Menu</a> </li>
			<li><a href="TrackOrder.aspx">Track</a> </li>
			<li><a href="Feedback.aspx">Feedback</a> </li>
			<li><a href="ContactUs.aspx">Contact Us</a> </li>
			<li><a href="AboutUs.aspx">About Us</a> </li>
		    </ul>
		</nav>
