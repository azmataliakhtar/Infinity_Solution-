<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DefaultPageUserControl.ascx.vb" Inherits="INF.Web.Public.default.DefaultPageUserControl" %>



<div class="HomeContainer">

     

        <!-- Jssor Slider Begin -->
        <!-- You can move inline styles to css file or css block. -->
        <div id="slider1_container" style="position: relative; top: 0px; left: 0px; width: 984px;
            height: 402px; ">

        <!-- Slides Container -->
        <div u="slides" style="cursor: move; position: absolute; left: 0px; top: 0px; width: 984px; height: 402px;
            overflow: hidden;">
           
             <%
                 If (ConfigurationManager.AppSettings("sliderImages") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("sliderImages")))) Then
               
                     Dim slidesNr As Integer = FormatNumber(ConfigurationManager.AppSettings("sliderImages"))
                     
                     For numberSlides As Integer = 0 To slidesNr Step +1
                        
                            
                         If (numberSlides = 1) Then
                             %>  <div><a href="<%=ConfigurationManager.AppSettings("slide1Href")%>"><img u="image" src="App_Themes/default/imgs/sliders/first.jpg" /></a></div>  <%
                         ElseIf (numberSlides = 2) Then
                             %>  <div><a href="<%=ConfigurationManager.AppSettings("slide2Href")%>"><img u="image" src="App_Themes/default/imgs/sliders/second.jpg" /></a></div>  <%
                         ElseIf (numberSlides = 3) Then
                             %>  <div><a href="<%=ConfigurationManager.AppSettings("slide3Href")%>"><img u="image" src="App_Themes/default/imgs/sliders/third.jpg" /></a></div>  <%
                         ElseIf (numberSlides = 4) Then
                             %>  <div><a href="<%=ConfigurationManager.AppSettings("slide4Href")%>"><img u="image" src="App_Themes/default/imgs/sliders/fourth.jpg" /></a></div>  <%
                         ElseIf (numberSlides = 5) Then
                             %>  <div><a href="<%=ConfigurationManager.AppSettings("slide5Href")%>"><img u="image" src="App_Themes/default/imgs/sliders/fifth.jpg" /></a></div>  <%
                         ElseIf (numberSlides = 6) Then
                             %>  <div><a href="<%=ConfigurationManager.AppSettings("slide6Href")%>"><img u="image" src="App_Themes/default/imgs/sliders/sixth.jpg" /></a></div>  <%
                         ElseIf (numberSlides = 7) Then
                             %>  <div><a href="<%=ConfigurationManager.AppSettings("slide7Href")%>"><img u="image" src="App_Themes/default/imgs/sliders/seventh.jpg" /></a></div>  <%
                         End If
                         
                     Next
                     
               
                    
            End If
            %>

            <!--
            <div><img u="image" src="App_Themes/default/imgs/orbit/slides/first.jpg" /></div>
            <div><img u="image" src="App_Themes/default/imgs/orbit/slides/second.jpg" /></div>
            <div><img u="image" src="App_Themes/default/imgs/orbit/slides/third.jpg" /></div>
            -->
            
        </div>
        
        <!-- Arrow Navigator Skin Begin -->
        <style>
            /* jssor slider arrow navigator skin 03 css */
            /*
            .jssora03l              (normal)
            .jssora03r              (normal)
            .jssora03l:hover        (normal mouseover)
            .jssora03r:hover        (normal mouseover)
            .jssora03ldn            (mousedown)
            .jssora03rdn            (mousedown)
            */
            .jssora03l, .jssora03r, .jssora03ldn, .jssora03rdn
            {
            	position: absolute;
            	cursor: pointer;
            	display: block;
                background: url(/App_Themes/default/imgs/a03.png) no-repeat;
                overflow:hidden;
            }
            .jssora03l { background-position: -3px -33px; }
            .jssora03r { background-position: -63px -33px; }
            .jssora03l:hover { background-position: -123px -33px; }
            .jssora03r:hover { background-position: -183px -33px; }
            .jssora03ldn { background-position: -243px -33px; }
            .jssora03rdn { background-position: -303px -33px; }
        </style>
        <!-- Arrow Left -->
        <span u="arrowleft" class="jssora03l" style="width: 55px; height: 55px; top: 123px; left: 8px;">
        </span>
        <!-- Arrow Right -->
        <span u="arrowright" class="jssora03r" style="width: 55px; height: 55px; top: 123px; right: 8px">
        </span>
        <!-- Arrow Navigator Skin End -->
        
    </div>
    <!-- Jssor Slider End -->

     


     

       <div class="btmcont">

            <div class="firstTmb btmtmb">

                <a href="<%=ConfigurationManager.AppSettings("thumb1Url")%>">
                    <img alt="" title="" src="/App_Themes/default/imgs/tmbs/1.jpg" class=""/>
                     <%
                         If (ConfigurationManager.AppSettings("thumb1Text") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("thumb1Text")))) Then
                     %>
                        <span><%=ConfigurationManager.AppSettings("thumb1Text")%></span>
                     <%
                         End If
                    %>
                </a>
            </div>

            <div class="secondTmb btmtmb">
                <a href="<%=ConfigurationManager.AppSettings("thumb2Url")%>">
                    <img alt="" title="" src="/App_Themes/default/imgs/tmbs/2.jpg" class=""/>
                      <%
                          If (ConfigurationManager.AppSettings("thumb2Text") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("thumb2Text")))) Then
                     %>
                        <span><%=ConfigurationManager.AppSettings("thumb2Text")%></span>
                     <%
                         End If
                    %>
                </a>
            </div>

            <div class="thirdTmb btmtmb">
                <a href="<%=ConfigurationManager.AppSettings("thumb3Url")%>">
                    <img alt="" title="" src="/App_Themes/default/imgs/tmbs/3.jpg" class=""/>
                      <%
                          If (ConfigurationManager.AppSettings("thumb3Text") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("thumb3Text")))) Then
                     %>
                        <span><%=ConfigurationManager.AppSettings("thumb3Text")%></span>
                     <%
                         End If
                    %>
                </a>
            </div>

            <div class="fourthTmb btmtmb">
                <%--Menu.aspx?CategoryId=4--%>
                <a href="<%=ConfigurationManager.AppSettings("thumb4Url")%>">
                    <img alt="" title="" src="/App_Themes/default/imgs/tmbs/4.jpg" class=""/>
                     <%
                         If (ConfigurationManager.AppSettings("thumb4Text") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("thumb4Text")))) Then
                     %>
                        <span><%=ConfigurationManager.AppSettings("thumb4Text")%></span>
                     <%
                         End If
                    %>
                </a>
            </div>

        </div>
   
</div>

   <script type="text/javascript">

       jQuery(document).ready(function ($) {
           var options = {
               $DragOrientation: 1,                                //[Optional] Orientation to drag slide, 0 no drag, 1 horizental, 2 vertical, 3 either, default value is 1 (Note that the $DragOrientation should be the same as $PlayOrientation when $DisplayPieces is greater than 1, or parking position is not 0)
               $ArrowNavigatorOptions: {                       //[Optional] Options to specify and enable arrow navigator or not
                   $Class: $JssorArrowNavigator$,              //[Requried] Class to create arrow navigator instance
                   $ChanceToShow: 2,                               //[Required] 0 Never, 1 Mouse Over, 2 Always
                   $AutoCenter: 2,                                 //[Optional] Auto center arrows in parent container, 0 No, 1 Horizontal, 2 Vertical, 3 Both, default value is 0
                   $Steps: 1                                       //[Optional] Steps to go for each navigation request, default value is 1
               }
           };

           var jssor_slider1 = new $JssorSlider$("slider1_container", options);


           function ScaleSlider() {

               //reserve blank width for margin+padding: margin+padding-left (10) + margin+padding-right (10)
               var paddingWidth = 0;

               //minimum width should reserve for text
               var minReserveWidth = 220;

               var parentElement = jssor_slider1.$Elmt.parentNode;

               //evaluate parent container width
               var parentWidth = parentElement.clientWidth;

               if (parentWidth) {

                   //exclude blank width
                   var availableWidth = parentWidth - paddingWidth;

                   //calculate slider width as 70% of available width
                   var sliderWidth = availableWidth * 1;

                   //slider width is maximum 600
                   sliderWidth = Math.min(sliderWidth, 984);

                   //slider width is minimum 200
                   sliderWidth = Math.max(sliderWidth, 220);
                   var clearFix = "none";

                   //evaluate free width for text, if the width is less than minReserveWidth then fill parent container
                   if (availableWidth - sliderWidth < minReserveWidth) {

                       //set slider width to available width
                       sliderWidth = availableWidth;

                       //slider width is minimum 200
                       sliderWidth = Math.max(sliderWidth, 220);

                       clearFix = "both";
                   }

                   //clear fix for safari 3.1, chrome 3
                   $('#clearFixDiv').css('clear', clearFix);

                   jssor_slider1.$ScaleWidth(sliderWidth);
               }
               else
                   window.setTimeout(ScaleSlider, 30);
           }

           ScaleSlider();

           $(window).bind("load", ScaleSlider);
           $(window).bind("resize", ScaleSlider);
           $(window).bind("orientationchange", ScaleSlider);

           <%
       If (ConfigurationManager.AppSettings("sliderTime") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("sliderTime")))) Then
            %> var sliderTime = <%=ConfigurationManager.AppSettings("sliderTime")%>;<%
   Else
             %> var sliderTime = 3000; <%
   End If
            %>
           
           var counter = 0;
           var i = setInterval(function () {
               // do your thing
               //alert("3 seconds passed");

               $('.jssora03r').click();

               counter++;
               if (counter === 1200) {
                   clearInterval(i);
               }
           }, sliderTime);


       });


      

    </script> 