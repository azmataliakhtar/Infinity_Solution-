<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Footer.ascx.vb" Inherits="INF.Web.Public.default.Footer" %>
<div class="footer col-md-12 col-xs-12">
    <div class="row">
           <div class="socialPlugins">

                <%                        
                    If (ConfigurationManager.AppSettings("alergyMessage") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("alergyMessage")))) Then
                        %>
                            <div class="allergyContainer">
                                <%=ConfigurationManager.AppSettings("alergyMessage")%>
                            </div>
               
                         <%
                    End If
                %>

                <%                        
                    If (ConfigurationManager.AppSettings("fbid") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("fbid")))) Then
                        %>
                            <div class="firstSectionPlugin">
                                <div class="fb-like-box" data-href="<%=ConfigurationManager.AppSettings("fbid")%>" data-colorscheme="light" data-show-faces="false" data-header="false" data-stream="false" data-show-border="false"></div>
                             </div>
               
                         <%
                    End If
                %>

          

             <div class="secondSectionPlugin">
                 
                 <!--
                 <div id="yelpwidget"><div style="width:225px;" id="yelp">
                        <a href="http://www.yelp.co.uk/biz/new-saffrani-edinburgh">
                          <img src="http://s3-media1.fl.yelpcdn.com/bphoto/pyxSbR8DTgqcTTVRFw-Y-Q/ms.jpg" id="businessimg" style=" box-shadow : 0px 0px 4px rgba(0, 0, 0, .4); margin-right : .5em; float : left ">
                        </a>
                          <div style="display:block" id="yelpheader">
                          <div width="225px" id="yelptitle" style="font-weight:bold;">New Saffrani</div>
                          <img width="115px" src="http://s3-media3.fl.yelpcdn.com/assets/2/www/img/bd9b7a815d1b/ico/stars/v1/stars_large_3_half.png" id="yelpstarrating">
                          <br>
                              <a href="http://www.yelp.co.uk/biz/new-saffrani-edinburgh"><img src="http://chrisawren.com/widgets/yelp/yelp/reviewsFromYelpRED.gif" id="yelpbutton" style="">
                            <br></a>
                        <div id="numreviews" style="font-weight:bold;">6 Reviews</div>
                      </div>
                    <br>
                    </div>
                    </div>
                     -->
             </div>
              

        </div>
        

        <div class="powBy">
            Please read our <a href="PrivacyPolicy.aspx">Privacy Policy</a>
            <br />
            Powered by: <a href="http://www.infinitysol.co.uk" class="a1t" target="_blank">Infinity
                                    Solutions</a>
        </div>
    </div>
</div>
