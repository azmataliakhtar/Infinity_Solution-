<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.master" CodeBehind="WaitingOrderConfirmation.aspx.vb" Inherits="INF.Web.WaitingOrderConfirmation" %>
<%@ Import Namespace="INF.Web.UI.Settings" %>

<asp:Content ID="GoogleContent" ContentPlaceHolderID="GooglePlaceHolder" runat="Server">
        
      <%
          If (ConfigurationManager.AppSettings("google-site-verification") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-plus-link")))) Then
        %>                                
             <meta name="google-site-verification" content="<%=ConfigurationManager.AppSettings("google-site-verification")%>"/>
        <%
        End If
    %>    
    
     <%
         If (ConfigurationManager.AppSettings("google-plus-link") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-plus-link")))) Then
        %>                                
             <link href="<%=ConfigurationManager.AppSettings("google-plus-link")%>" rel="publisher"/>
        <%
        End If
    %>   
   
     <%
         If (ConfigurationManager.AppSettings("geo-region") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-region")))) Then
        %>                                
             <meta name="geo.region" content="<%=ConfigurationManager.AppSettings("geo-region")%>" />
        <%
        End If
    %> 

    <%
        If (ConfigurationManager.AppSettings("geo-placename") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-placename")))) Then
        %>                                
             <meta name="geo.placename" content="<%=ConfigurationManager.AppSettings("geo-placename")%>" />
        <%
        End If
    %> 


     <%
         If (ConfigurationManager.AppSettings("geo-position") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-position")))) Then
        %>                                
             <meta name="geo.position" content="<%=ConfigurationManager.AppSettings("geo-position")%>" />
        <%
        End If
    %> 

     <%
         If (ConfigurationManager.AppSettings("google-icbm") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-icbm")))) Then
        %>                                
             <meta name="ICBM" content="<%=ConfigurationManager.AppSettings("google-icbm")%>" />
        <%
        End If
    %> 

</asp:Content>

<asp:Content runat="server" ID="LinkCanonicalContent" ContentPlaceHolderID="LinkCanonical">

    <%
        If (ConfigurationManager.AppSettings("linkCanonicalOrderWait") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalOrderWait")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalOrderWait")%>" />
        <%
        End If
    %>   

     <%
         If (ConfigurationManager.AppSettings("descOrderWait") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("descOrderWait")))) Then
        %>                                
            <meta name="description" content="<%=ConfigurationManager.AppSettings("descOrderWait")%>" />
        <%
        End If
    %>  


</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="Server">
  
     <%
         If (ConfigurationManager.AppSettings("titleOrderWait") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleOrderWait")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleOrderWait")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%> - Order Processing
            <%
        End If
    %>   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadExtra" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    
    <div class="cstmsection">
         <h3 class="page-header page-title"><i class="fa fa-clock-o"></i>Checkout Order Processing</h3>

        <div class="text-center">
            <asp:Timer runat="server" ID="UpdateTimer" Interval="60000" />
            <asp:UpdatePanel runat="server" ID="TimedPanel" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="UpdateTimer" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <asp:PlaceHolder runat="server" ID="UpdateProgressPlaceHolder" Visible="False">
                        <div class="form-group row">
                            <p></p>
                            <p></p>
                            <img src="App_Themes/default/images/progress.gif" alt="Please wait..." />
                            <span id="countdown" class="timer"></span>
                            <p style="font-size: 130%;color: #E62624;">Please Wait. We are sending your order to the restaurant.</p>
                            <p />

                             
                                <script>

                                    var timerText = ''
                                    var seconds = <%=ConfigurationManager.AppSettings("orderWaitSeconds")%>;
                                    function secondPassed() {
                                        var minutes = Math.round((seconds - 30) / 60);
                                        var remainingSeconds = seconds % 60;
                                        if (remainingSeconds < 10) {
                                            remainingSeconds = "0" + remainingSeconds;
                                        }

                                        document.getElementById('countdown').innerHTML = timerText + "<br/>" + minutes + ":" + remainingSeconds + "s";

                                        if (seconds == 0) {
                                            clearInterval(countdownTimer);
                                            document.getElementById('countdown').innerHTML = "<br/>Something went wrong. Please call.";
                                        } else {
                                            seconds--;
                                        }
                                    }

                                    var countdownTimer = setInterval('secondPassed()', 1000);
                                </script>
                        

                            <p style="font-size: 110%;">Please Stay On This Page</p>
                            <p />
                            <p style="font-size: 110%;">Until You Receive a Response From Us.</p>
                            <p />
                        </div>

                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="MessagePlaceHolder" Visible="False">
                        <div class="form-group row">
                            <p></p>
                            <p></p>

                            <img src="App_Themes/default/images/cart_accept.png" alt="Your order has been accepted" /><br />
                            <p style="font-size: 130%;color: #39B3D7;">Thank You for your Order!</p>
                            <p />
                            <p style="font-size: 110%;">We have received your order and a confirmation has been sent to your email adress.</p>
                            <p />
                            <p style="font-size: 110%;">
                                Your order will take up to <b><%=ConfigurationManager.AppSettings("deliveryTimeMin")%></b>, however during busy periods, this can be prolonged.
                            </p>
                            <p style="font-size: 120%;text-transform: uppercase;font-weight: bold;">
                                Delivery Time: <asp:Literal runat="server" ID="ltrDeliveryTime" Visible="false"></asp:Literal>
                            </p>

                            <br/>

                            <p class="orderTrckBtn"> Track your order here: <a href="TrackOrder.aspx">here</a><p>
                            <%--<p style="font-size: 110%;">Your order will take up to 45 minutes, however during busy periods, this can be prolonged.</p>--%>
                            <p />
                             <script type="text/javascript">
                                 setTimeout(function(){ document.location.href = '/TrackOrder.aspx'; }, 10000);                               
                            </script>
                        </div>
                    </asp:PlaceHolder>

                    <asp:PlaceHolder runat="server" ID="ErrorMessagePlaceHolder" Visible="False">
                        <div class="form-group row">
                            <p></p>
                            <p></p>
                            <img src="App_Themes/default/images/questions.png" alt="Your order has been accepted" /><br />
                            <p style="font-size: 130%;color: #E62624;">Thank You for your Order!</p>
                            <p />
                            <p style="font-size: 110%;">Your order has not been confirmed yet. Please call us for details.</p>
                            <p />
                            <p style="font-size: 110%;">
                                <asp:Literal runat="server" ID="MessageFromChef"></asp:Literal></p>
                            <p />

                            <script type="text/javascript">
                                setTimeout(function(){ document.location.href = '/TrackOrder.aspx'; }, 10000);                               
                            </script>
                        </div>
                    </asp:PlaceHolder>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
