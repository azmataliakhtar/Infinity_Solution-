<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Header.ascx.vb" Inherits="INF.Web.Public.default.Header" %>
<%@ Import Namespace="INF.Web.UI.Settings" %>



<script type="text/javascript">
    
    // Time left for opening shop
    
   
    
    var timeleft = parseFloat(<%=RemainingTime%>);

   

        <%                        
    If (ConfigurationManager.AppSettings("shopOpenAt") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("shopOpenAt")))) Then
        %>  var shopOpenAt = '<%Response.Write(ConfigurationManager.AppSettings("shopOpenAt"))%>' <%
    Else
         %>  var shopOpenAt = '4:30 PM' <%
    End If
        %>

  

    /**
    * This function is to check whether the shop is available for placing order.
    **/               					
    function IsShopOpen() {
        var day = Math.floor(timeleft / (60 * 60 * 24));
        var hours = Math.floor(timeleft / 3600) - (day * 24);
        var minutes = Math.floor(timeleft / 60) - (day * 24 * 60) - (hours * 60);

        if ((hours <= 0 && minutes <= 0) || hours > 14.5) {
            return false;
        }
        return true;
    }

    function CounterTimer() {
        var day = Math.floor(leave / (60 * 60 * 24));
        var hour = Math.floor(leave / 3600) - (day * 24);
        var minute = Math.floor(leave / 60) - (day * 24 * 60) - (hour * 60);

        if ((hour <= 0 && minute <= 0) || hour > 14.5) {

            document.getElementById("timelabel").innerHTML = "<span class='firstSpan'>opening at</span><br/>" + "<span class='secondSpan'>" + shopOpenAt + "</span>";
            document.getElementById("timelabelMobile").innerHTML = "<span class='firstSpan'>opening at</span><br/>" + "<span class='secondSpan'>" + shopOpenAt + "</span>";

            //timelabelMobile


            $('#closedSign').css("display", "block");
            $('#closedSignMobile').css("display", "block");

            $('#openSign').css("display", "none");
            $('#openSignMobile').css("display", "none");

        } else {
            var second = Math.floor(leave) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);

            hour = hour < 10 ? "0" + hour : hour;
            minute = minute < 10 ? "0" + minute : minute;
            second = second < 10 ? "0" + second : second;

            var remain = hour + ":" + minute + ":" + second;
            leave = leave - 1;

            document.getElementById("timelabel").innerHTML = "<span class='firstSpan'>restaurant closing in </span><br/>" + "<span class='secondSpan'>" + remain + "<i  style='margin-left:5px;font-size:22px;' class='fa fa-clock-o'></i></span>";
            document.getElementById("timelabelMobile").innerHTML = "<span class='firstSpan'>restaurant closing in </span><br/>" + "<span class='secondSpan'>" + remain + "<i  style='margin-left:5px;font-size:22px;' class='fa fa-clock-o'></i></span>";


            $('#closedSign').css("display", "none");
            $('#closedSignMobile').css("display", "none");

            $('#openSign').css("display", "block");
            $('#openSignMobile').css("display", "block");
        }
    }

</script>


<header class="header">

     <div id="fb-root"></div>
        <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.0";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));
        </script>

   
     <asp:Panel runat="server" ID="pnlOrderTypeAndPostCode">

            <div>
                <p class="orderOrCollP">

                    <asp:Literal runat="server" ID="ltrOrderType"></asp:Literal>

                    <asp:Literal runat="server" ID="ltrPostcode"></asp:Literal>

                    <a id="reset_order_type" href="#" class="reset_order_type">
                        <img src="Images/Del.png" alt="reset" />
                    </a>
                </p>
            </div>

        </asp:Panel>



    <div class="headerRowMobi">
        <div class="firstHeaderRowMobi wbsColor">
            <a class="shopLogoMobi" href="/">&nbsp;</a>

           <div class="shopOpenClosed">
               <span id="openSignMobile"></span>
               <span id="closedSignMobile"></span> 
           </div>


           

            <div class="mobiNavLinks">

                 <button type="button" class="navbar-toggle collapsed navbarbtn" data-toggle="collapse" data-target="#mobinav_collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                  </button>


                 <div class="collapse navbar-collapse" id="mobinav_collapse">

                  <ul class="nav navbar-nav">
                                         
                   
                      <% If Not (HttpContext.Current.User IsNot Nothing AndAlso HttpContext.Current.User.Identity IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))) Then%>
                
                <% 
                Else
                    Dim vFormIdentity As FormsIdentity = TryCast(HttpContext.Current.User.Identity, FormsIdentity)
                    Dim vFullName As String = String.Empty
                    If (vFormIdentity IsNot Nothing) Then
                        vFullName = vFormIdentity.Ticket.UserData
                    End If
                %>

                       <li class=""><a href="Profile.aspx" rel="sb1" class="liHome"><i class="fa fa-gear"></i><%=vFullName%> - Settings</a></li>
                 
               
            <% End If%>

                    
                     <%                        
                 If (ConfigurationManager.AppSettings("liHome") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liHome")))) Then
                        %>
                            <li class=""><a href="Default.aspx" rel="sb1" class="liHome"><i class="fa fa-home"></i>Home</a></li>
                        <%
                    End If
                %>

             <%                        
                 If (ConfigurationManager.AppSettings("liMenu") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liMenu")))) Then
                        %>
                           <li class=""><a href="Menu.aspx" rel="sb3" class="liMenu"><i class="fa fa-cutlery"></i>Menu</a></li>
                        <%
                    End If
                %>


            <%                        
                If (ConfigurationManager.AppSettings("liDeals") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liDeals")))) Then
                        %>
                          <li class=""><a class="liDeals" href="Deals.aspx"><i class="fa fa-flag"></i>Deals</a></li>
                        <%
                    End If
                %>


              <%                        
                  If (ConfigurationManager.AppSettings("liTracking") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liTracking")))) Then
                        %>
                           <li class=""><a href="TrackOrder.aspx" class="liTrackOrder"><i class="fa fa-location-arrow"></i>Track Order</a></li>    
                        <%
                    End If
                %>

            
                <%                        
                    If (ConfigurationManager.AppSettings("liReservation") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liReservation")))) Then
                        %>
                           <li class=""><a href="Reservation.aspx" class="liReservation"><i class="fa fa-ticket"></i>Book a Table</a></li>    
                        <%
                    End If
                %>
                    
               
                <%                        
                    If (ConfigurationManager.AppSettings("liPhotoGallery") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liPhotoGallery")))) Then
                        %>
                           <li class=""><a href="PhotoGallery.aspx" class="liPhotoGallery"><i class="fa fa-photo"></i>Photo Gallery</a></li>    
                        <%
                    End If
                %>
            
              <%                        
                  If (ConfigurationManager.AppSettings("liTestimonials") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liTestimonials")))) Then
                        %>
                           <li class=""><a href="Testimonials.aspx" class="liTestimonials"><i class="fa fa-comments"></i>Testimonials</a></li>    
                        <%
                    End If
                %>
            
               <%                        
                  If (ConfigurationManager.AppSettings("liContactUs") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liContactUs")))) Then
                        %>
                            <li class=""><a href="ContactUs.aspx" class="liAboutUs"><i class="fa fa-newspaper-o"></i>Contact Us</a></li>   
                        <%
                    End If
                %>
             
                        
                            
                    <% If Not (HttpContext.Current.User IsNot Nothing AndAlso HttpContext.Current.User.Identity IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))) Then%>
                        <li class="loginElement pull-right">
                            
                                <a href="login.aspx" rel="sb1" class="liLoginRegister">Login / Register</a>
                           
                        </li>
                     <% 
                  Else
                      Dim vFormIdentity As FormsIdentity = TryCast(HttpContext.Current.User.Identity, FormsIdentity)
                      Dim vFullName As String = String.Empty
                        
                      If (vFormIdentity IsNot Nothing) Then
                          vFullName = vFormIdentity.Ticket.UserData
                      End If
                            %>
                                <li class="loginElement logout pull-right">

                                                               
                                        <asp:LinkButton ID="LinkButton4"  runat="server" Text="Log Out" Style="text-decoration: none;" CausesValidation="false"></asp:LinkButton>
                                   
                                </li>

                 <% End If%>
                    
                  </ul>
                </div><!-- /.navbar-collapse -->

            </div>


             <div class="basketMobi bsktColor">
               
               <i class="fa fa-shopping-cart"></i>

            </div>


        </div>

        <div class="secondHeaderRowMobi scndhm">
            <div class="firstSubRow">
                <span class="mapIcn mbhi"><i class="fa fa-map-marker"></i></span>
                
                <!--
                <span class="">
                    <%                        
                         If (ConfigurationManager.AppSettings("customerAddr1") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("customerAddr1")))) Then
                            %>                                
                                <%=ConfigurationManager.AppSettings("customerAddr1")%>
                            <%
                        End If
                   
                            If (ConfigurationManager.AppSettings("customerAddr2") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("customerAddr2")))) Then
                        %>                                
                            <br/><%=ConfigurationManager.AppSettings("customerAddr2")%>
                        <%
                        End If
                    %>

                </span>
                -->

                <span class="">17 Queensway, HP1 1LS<br>Hertfordshire, UK</span>
            </div>

             <%                        
                If (ConfigurationManager.AppSettings("customerPhone") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("customerPhone")))) Then
                    %>
                        <div class="secondSubRow">
                            <span class="phnIcn mbhi"><i class="fa fa-mobile"></i></span>
                            <!-- <span class=""><%=ConfigurationManager.AppSettings("customerPhone")%></span> -->
                            <a class="phoneNr" href="tel:01442 211 080">01442 211 080</a><br/><a class="phoneNr" href="tel:01442 259 515">01442 259 515</a>
                        </div>
                    <%
                End If
            %>

       

            <div class="clear"></div>

             <div class="thirdSubRow">
                 <span class="onlnIcn mbhi"><i class="fa fa-laptop"></i></span>
                 <a href="Menu.aspx">
                     
                     <%                        
                         If (ConfigurationManager.AppSettings("orderOnlineRow1") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("orderOnlineRow1")))) Then
                            %>                                
                                <%=ConfigurationManager.AppSettings("orderOnlineRow1")%>
                            <%
                        End If
                   
                            If (ConfigurationManager.AppSettings("orderOnlineRow2") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("orderOnlineRow2")))) Then
                        %>                                
                            <br/><%=ConfigurationManager.AppSettings("orderOnlineRow2")%>
                        <%
                        End If
                    %>

                 </a>
            </div>

            <div class="fourthSUbRow">
                 <div id="timelabelMobile" class=""></div>
            </div>

        </div>
    </div>

    <div class="firstHeaderRow">

      


        <div class="firstCellH">

            <div class="fchTop">
                <span class="addressIcon"><i class="fa fa-map-marker"></i></span>
                <span class="addressTxt">
                     <%                        
                         If (ConfigurationManager.AppSettings("customerAddr1") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("customerAddr1")))) Then
                            %>                                
                                <%=ConfigurationManager.AppSettings("customerAddr1")%>
                            <%
                        End If
                   
                            If (ConfigurationManager.AppSettings("customerAddr2") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("customerAddr2")))) Then
                        %>                                
                            , <%=ConfigurationManager.AppSettings("customerAddr2")%>
                        <%
                        End If
                    %>
                    
               </span>
            </div>

            <div class="fchBottom">
                <div class="fchbFirst">

                    <%                        
                        If (ConfigurationManager.AppSettings("customerPhone") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("customerPhone")))) Then
                            %>
                                <span class="phoneIcon"><i class="fa fa-mobile"></i></span>
                                <a class="phoneNr" href="tel:01442 211 080">01442 211 080</a><br/><a class="phoneNr" href="tel:01442 259 515">01442 259 515</a>
                            <%
                        End If
                    %>
                   
                
                </div>

                <div class="fchbSecond">
                    <span class="onlineIcon"><i class="fa fa-laptop"></i></span>
                  

                     <a href="Menu.aspx">
                     
                     <%                        
                         If (ConfigurationManager.AppSettings("orderOnlineRow1") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("orderOnlineRow1")))) Then
                            %>                                
                                <%=ConfigurationManager.AppSettings("orderOnlineRow1")%>
                            <%
                        End If
                   
                            If (ConfigurationManager.AppSettings("orderOnlineRow2") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("orderOnlineRow2")))) Then
                        %>                                
                            <br/><%=ConfigurationManager.AppSettings("orderOnlineRow2")%>
                        <%
                        End If
                    %>

                 </a>


                </div>
            </div>
            
        </div>

          <div class="secondCellH">
            <a class="shopLogo" href="/">&nbsp;</a>
        </div>
        
        <div class="thirdCellH">

            <div class="ifLoginContainer">
            <% If Not (HttpContext.Current.User IsNot Nothing AndAlso HttpContext.Current.User.Identity IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))) Then%>
                
                <% 
                Else
                    Dim vFormIdentity As FormsIdentity = TryCast(HttpContext.Current.User.Identity, FormsIdentity)
                    Dim vFullName As String = String.Empty
                    If (vFormIdentity IsNot Nothing) Then
                        vFullName = vFormIdentity.Ticket.UserData
                    End If
                %>

                 <span class="welcomeSettings">
                    <div class="pseudoSettings"><i class="fa fa-gears"></i></div>
                    <asp:LinkButton ID="LinkButton1" ForeColor="White" runat="server" Text="Settings" Style="text-decoration: none; display: none" PostBackUrl="~/Profile.aspx"></asp:LinkButton>
                </span>

                <span class="welcomeName">
                    Welcome&nbsp;
                    <strong><%=vFullName%></strong> 
                </span>

               
            <% End If%>
            </div>
         

              <div id="timelabel" class="TimeBlock tchFirst"></div>

                <script type="text/javascript">
                    var leave = parseFloat(<%=RemainingTime%>);
                    var interv = setInterval(CounterTimer, 1000);
                    CounterTimer();
                </script>



            <div class="tchSecond">
                <span id="openSign"></span>
                <span id="closedSign"></span>
            </div>
        </div>

    </div>

    <div class="secondHeaderRow">

            


    

         <ul class="navList <%=ConfigurationManager.AppSettings("enableShortNav")%>">




           
            
           

             <%                        
                 If (ConfigurationManager.AppSettings("liHome") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liHome")))) Then
                        %>
                            <li class="navliColor"><a href="Default.aspx" rel="sb1" class="liHome"><i class="fa fa-home"></i>Home</a></li>
                        <%
                    End If
                %>

             <%                        
                 If (ConfigurationManager.AppSettings("liMenu") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liMenu")))) Then
                        %>
                           <li class="navliColor"><a href="Menu.aspx" rel="sb3" class="liMenu"><i class="fa fa-cutlery"></i>Menu</a></li>
                        <%
                    End If
                %>


            <%                        
                If (ConfigurationManager.AppSettings("liDeals") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liDeals")))) Then
                        %>
                          <li class="navliColor"><a class="liDeals" href="Deals.aspx"><i class="fa fa-flag"></i>Deals</a></li>
                        <%
                    End If
                %>


              <%                        
                  If (ConfigurationManager.AppSettings("liTracking") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liTracking")))) Then
                        %>
                           <li class="navliColor"><a href="TrackOrder.aspx" class="liTrackOrder"><i class="fa fa-location-arrow"></i>Track Order</a></li>    
                        <%
                    End If
                %>

            
                <%                        
                    If (ConfigurationManager.AppSettings("liReservation") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liReservation")))) Then
                        %>
                           <li class="navliColor"><a href="Reservation.aspx" class="liReservation"><i class="fa fa-ticket"></i>Book a Table</a></li>    
                        <%
                    End If
                %>
                    
               
                <%                        
                    If (ConfigurationManager.AppSettings("liPhotoGallery") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liPhotoGallery")))) Then
                        %>
                           <li class="navliColor"><a href="PhotoGallery.aspx" class="liPhotoGallery"><i class="fa fa-photo"></i>Photo Gallery</a></li>    
                        <%
                    End If
                %>
            
              <%                        
                  If (ConfigurationManager.AppSettings("liTestimonials") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liTestimonials")))) Then
                        %>
                           <li class="navliColor"><a href="Testimonials.aspx" class="liTestimonials"><i class="fa fa-comments"></i>Testimonials</a></li>    
                        <%
                    End If
                %>
            
               <%                        
                  If (ConfigurationManager.AppSettings("liContactUs") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("liContactUs")))) Then
                        %>
                            <li class="navliColor"><a href="ContactUs.aspx" class="liAboutUs"><i class="fa fa-newspaper-o"></i>Contact Us</a></li>   
                        <%
                    End If
                %>
             
            
                        
                            
                    <% If Not (HttpContext.Current.User IsNot Nothing AndAlso HttpContext.Current.User.Identity IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))) Then%>
                        <li class="loginElement navliColor">
                            <div class="loginContainer">
                                <a href="login.aspx" rel="sb1" class="liLoginRegister">Login / Register</a>
                            </div>
                        </li>
                    <% 
                    Else
                        Dim vFormIdentity As FormsIdentity = TryCast(HttpContext.Current.User.Identity, FormsIdentity)
                        Dim vFullName As String = String.Empty
                        
                        If (vFormIdentity IsNot Nothing) Then
                            vFullName = vFormIdentity.Ticket.UserData
                        End If
                    %>
                        <li class="loginElement logout navliColor">

                            <div class="loginContainer logout">                              
                                <asp:LinkButton ID="lnkLogout" ForeColor="Maroon" runat="server" Text="Log Out" Style="text-decoration: none;" CausesValidation="false"></asp:LinkButton>
                            </div>
                        </li>

                    <% End If%>
        </ul>

    </div>

   
</header>
