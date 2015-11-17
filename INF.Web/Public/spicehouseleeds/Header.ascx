<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Header.ascx.vb" Inherits="INF.Web.Public.default.Header" %>
<div class="header1">
   
   <div class="container_12" style="height: 101px;
margin: 10px 10px 5px 10px;">


<div id="logo" class="grid_3 alpha">
<asp:Panel runat="server" ID="pnlOrderTypeAndPostCode" Width="1000px" Style="z-index: 1; background-color: transparent; margin-left: 0px;float:left">
            <div style="vertical-align: top; text-align: left; padding: 1px;">
                <p style="margin-top: 0px; margin-bottom: 0px; font-size: 11px; font-weight: bold; color: whitesmoke;">
                    <asp:Literal runat="server" ID="ltrOrderType"></asp:Literal>
                    <asp:Literal runat="server" ID="ltrPostcode"></asp:Literal>
                    <%--<asp:PlaceHolder runat="server" ID="phOrderTypeReset" Visible="False">--%>
                    <a id="reset_order_type" href="javascript:void(0);" class="reset_order_type">
                        <asp:Image ID="Image1" runat="server" AlternateText="Reset Order Type" SkinID="ResetOrderTypeImage"/>
                    </a>
                    <%--</asp:PlaceHolder>--%>
                </p>
            </div>
        </asp:Panel>
<a href="http://www.spicehouseleeds.com.hostinguk.co.uk/">

<img src="http://www.spicehouseleeds.com/images/spice_house_restaurant_logo.png" alt="Spice House Indian Restaurant Leeds" height="70" title=""></a></div>


<div id="menu" class="grid_9 omega">

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
                            document.getElementById("timelabel").innerHTML="<font size='2px' color='Black'>Shop Open at</font><br/>" + "<font size='3px' color='Red'>" + "12:00 PM" + "</font>";
                        } else {
                            var second = Math.floor(leave) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);

                            hour = hour < 10 ? "0" + hour : hour;
                            minute = minute < 10 ? "0" + minute : minute;
                            second = second < 10 ? "0" + second : second;

                            var remain = hour + ":" + minute + ":" + second;
                            leave = leave - 1;

                            document.getElementById("timelabel").innerHTML = "<font size='3px' color='white'>  </font><br/>" + "<font size='4px' color='Maroon'>" + remain + "</font>";
                        }
                    } 
                </script>
                <script type="text/javascript">
                    var leave = <%=RemainingTime%>
                    var interv = setInterval(CounterTimer, 1000);
                    CounterTimer();
                </script>
            
                    <% If Not (HttpContext.Current.User IsNot Nothing AndAlso HttpContext.Current.User.Identity IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))) Then%>
                    <a href="login.aspx" rel="sb1">
                        <asp:Image ID="lnkLogin" runat="server" SkinID="LoginImage" />
                    </a>
                    <% 
                    Else
                        Dim vFormIdentity As FormsIdentity = TryCast(HttpContext.Current.User.Identity, FormsIdentity)
                        Dim vFullName As String = String.Empty
                        If (vFormIdentity IsNot Nothing)Then
                            vFullName = vFormIdentity.Ticket.UserData
                        End If
                    %>
                    <span style="color: #fff;">Welcome&nbsp;<strong><%=vFullName%></strong> </span>
                    |
                                <asp:LinkButton ID="lnkEditProfile" ForeColor="White" runat="server" Text="Settings"
                                    Style="text-decoration: none; color: Maroon" PostBackUrl="Profile.aspx"></asp:LinkButton>
                    |
                                <asp:LinkButton ID="lnkLogout" ForeColor="Maroon" runat="server" Text="Log Out" Style="text-decoration: none; color: Maroon"
                                    CausesValidation="false"></asp:LinkButton>
                    <% End If%>
					
			<div id="timelabel" class="TimeBlock" style="margin-top: -14px;">
			
                </div>
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
                            document.getElementById("timelabel").innerHTML="<font size='2px' color='Black'>Shop Open at</font><br/>" + "<font size='3px' color='Red'>" + "12:00 PM" + "</font>";
                        } else {
                            var second = Math.floor(leave) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);

                            hour = hour < 10 ? "0" + hour : hour;
                            minute = minute < 10 ? "0" + minute : minute;
                            second = second < 10 ? "0" + second : second;

                            var remain = hour + ":" + minute + ":" + second;
                            leave = leave - 1;

                            document.getElementById("timelabel").innerHTML = "<font size='3px' color='white'>  </font><br/>" + "<font size='4px' color='Maroon'>" + remain + "</font>";
                        }
                    } 
                </script>
                <script type="text/javascript">
                    var leave = <%=RemainingTime%>
                    var interv = setInterval(CounterTimer, 1000);
                    CounterTimer();
                </script>
               
                   
         
  <ul id="nav">
	 <li><a href="Default.aspx" rel="sb1">Home</a> </li>
                            <li><a href="Menu.aspx" rel="sb3">Menu</a> </li>
                            <li><a href="Deals.aspx" rel="sb3">Deals</a> </li>
                            <li><a href="TrackOrder.aspx">Track</a> </li>
                            <li><a href="Feedback.aspx">Feedback</a> </li>
                            <li><a href="ContactUs.aspx">Contact Us</a> </li>
                            <li><a href="AboutUs.aspx">About Us</a> </li>
							</ul>

		</div></div>
   
   
       
 
                   
</div>