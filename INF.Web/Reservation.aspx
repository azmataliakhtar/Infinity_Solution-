<%@ page title="" language="VB" masterpagefile="~/SiteMaster.master" autoeventwireup="false" Inherits="INF.Web.Reservation" Codebehind="Reservation.aspx.vb" %>
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
        If (ConfigurationManager.AppSettings("linkCanonicalReservation") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalReservation")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalReservation")%>" />
        <%
        End If
    %>   

     <%
         If (ConfigurationManager.AppSettings("descReservation") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("descReservation")))) Then
        %>                                
            <meta name="description" content="<%=ConfigurationManager.AppSettings("descReservation")%>" />
        <%
        End If
    %>   

</asp:Content>

<asp:Content runat="server" ID="PageTitleContent" ContentPlaceHolderID="PageTitle">
    
     <%
         If (ConfigurationManager.AppSettings("titleReservation") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleReservation")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleReservation")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%> - Reservation 
            <%
        End If
    %>   

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadExtra" runat="Server"></asp:Content>
<asp:Content ID="MainContentPlaceHolderContent" ContentPlaceHolderID="MainContent" runat="Server">

    <section class="cstmsection">
         <h3 class="page-header page-title"><i class="fa fa-ticket"></i>Book a Table</h3>
            
         <div id="reservationSuccess">
             <div class="reservSuccessActual">
                The Booking has been sent.<br/> You will be contacted by our staff for details. 
             </div>
         </div>
         

          <div class="reservationCnt">

              <div class="reservImgs">
                <img src="App_Themes/default/imgs/openingTimes.jpg" class="openingTimes"/>
              
             </div>
              <div class="reservationDiv">

                  

                  <div class="firstBLine">

                      <div class="fbld">
                          <label>Email:</label>
                          <input type="text" id="bookingEmail" placeholder="email ..."/>
                      </div>

                      <div class="fbld">
                          <label>Phone:</label>
                          <input type="text" id="bookingPhone" placeholder="phone ..."/>
                      </div>

                      <div class="fbld">
                          <label>First Name:</label>
                          <input type="text" id="bookingFirstName" placeholder="first name"/>
                      </div>

                      <div class="fbld">
                          <label>Last Name:</label>
                          <input type="text" id="bookingLastName" placeholder="last name ..."/>
                      </div>

                  </div>


            
                   <div class="secondBLine">
                       <div class="sbld">
                           <label>For how many persons ?</label>
                           <div class="selectbx">
                                <select id="selectPeople">
                                     <option value="1">1 person</option>
                                     <option value="2">2 persons</option>
                                     <option value="3">3 persons</option>
                                     <option value="4">4 persons</option>
                                     <option value="5">5 persons</option>
                                     <option value="6">6 persons</option>
                                     <option value="7">7 persons</option>
                                     <option value="8">8 persons</option>
                                     <option value="9">9 persons</option>
                                     <option value="10">10 persons</option>
                                     <option value="11">11 persons</option>
                                     <option value="12">12 persons</option>
                                    <option value="12+">12+ persons</option>
                                </select>


                            </div>
                        </div>

                        <div class="sbld">
                            <label>Please choose the date:</label>
                            <div class="selectbx datebx">
                               

                                <div class="selectbxDay">
                                    <select id="selectbxDay">
                                        <option value="0"  selected="selected">day</option>
                                        <option value="1">1st</option>
                                         <option value="2">2nd</option>
                                         <option value="3">3rd</option>
                                         <option value="4">4th</option>
                                         <option value="5">5th</option>
                                         <option value="6">6th</option>
                                         <option value="7">7th</option>
                                         <option value="8">8th</option>
                                         <option value="9">9th</option>
                                         <option value="10">10th</option>
                                         <option value="11">11th</option>
                                         <option value="12">12th</option>
                                         <option value="13">13th</option>
                                         <option value="14">14th</option>
                                         <option value="15">15th</option>
                                         <option value="16">16th</option>
                                         <option value="17">17th</option>
                                         <option value="18">18th</option>
                                         <option value="19">19th</option>
                                         <option value="20">20th</option>
                                         <option value="21">21st</option>
                                         <option value="22">22nd</option>
                                         <option value="23">23rd</option>
                                         <option value="24">24th</option>
                                         <option value="25">25th</option>
                                         <option value="26">26th</option>
                                         <option value="27">27th</option>
                                         <option value="28">28th</option>
                                         <option class="day29" value="29">29th</option>
                                         <option class="day30" value="30">30th</option>
                                         <option class="day31" value="31">31st</option>                                        
                                     </select>
                                 </div>

                                <div class="selectbxMonth">
                                    <select id="selectMonth">
                                        <option value="0"  selected="selected">month</option>
                                        <option value="January">January</option>
                                        <option value="February">February</option>  
                                        <option value="March">March</option>  
                                        <option value="April">April</option>  
                                        <option value="May">May</option>  
                                        <option value="June">June</option>  
                                        <option value="July">July</option>  
                                        <option value="August">August</option>  
                                        <option value="September">September</option>    
                                        <option value="October">October</option>   
                                        <option value="November">November</option> 
                                        <option value="December">December</option> 
                                    </select>    
                               </div>

                            </div>
                        </div>

                        <div class="sbld">
                             <label>At what time will your arrive ? <br/>


                             </label>
                            <div class="selectbxtime">
                                <select id="selHour">
                                    <option value="hh"  selected="selected">hour</option>
                                    <option value="01">01</option>
                                    <option value="02">02</option>
                                    <option value="03">03</option>
                                    <option value="04">04</option>
                                    <option value="05">05</option>
                                    <option value="06">06</option>
                                    <option value="07">07</option>
                                    <option value="08">08</option>
                                    <option value="09">09</option>
                                    <option value="10">10</option>
                                    <option value="11">11</option>
                                    <option value="12">12</option>
                                </select>

                                 <select id="selMin">
                                    <option value="mm"  selected="selected">min</option>
                                    <option value="00m">00</option>
                                    <option value="15m">15</option>
                                    <option value="30m">30</option>
                                    <option value="45m">45</option>
                                </select>

                                 <select id="selAMPM">
                                    <option value="AM">AM</option>
                                    <option value="PM">PM</option>
                                </select>

                            </div>
                        </div>
                  </div>

                  <div class="fourthBLine">
                        <label>Additional info:</label>
                        <textarea rows="4" cols="10" id="additInfoTxtarea"></textarea>
                  </div>
                  
                  <div class="errorLine">Please fill in all the fields !</div>

                   <div class="thirdBLine">
                       <input type="button" id="sendReservationForm" value="Send the Reservation"/>
                  </div>

              
              </div>



            

          </div>
        
          
    </section>

</asp:Content>
