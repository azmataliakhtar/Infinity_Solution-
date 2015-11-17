<%@ page title="" language="VB" masterpagefile="~/SiteMaster.master" autoeventwireup="false" Inherits="INF.Web.Testimonials" Codebehind="Testimonials.aspx.vb" %>
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
        If (ConfigurationManager.AppSettings("linkCanonicalTestimonials") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalTestimonials")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalTestimonials")%>" />
        <%
        End If
    %>   

    <%
        If (ConfigurationManager.AppSettings("descTestimonials") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("descTestimonials")))) Then
        %>                                
            <meta name="description" content="<%=ConfigurationManager.AppSettings("descTestimonials")%>" />
        <%
        End If
    %>  

</asp:Content>

<asp:Content runat="server" ID="PageTitleContent" ContentPlaceHolderID="PageTitle">
   

     <%
         If (ConfigurationManager.AppSettings("titleTestimonials") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleTestimonials")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleTestimonials")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%> - Testimonials
            <%
        End If
    %>   

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadExtra" runat="Server">

     <script type="text/javascript">

         $(document).ready(function () {


             var $container = $('.cstmContainer');
             // initialize
             $container.masonry({
                 columnWidth: 20,
                 itemSelector: '.testimon'
             });




         });

    </script>

</asp:Content>
<asp:Content ID="MainContentPlaceHolderContent" ContentPlaceHolderID="MainContent" runat="Server">

   <section class="testimonials cstmsection">

        <h3 class="page-header page-title"><i class="fa fa-comments"></i>Testimonials</h3>
        
        
            <asp:Literal runat="server" ID="TestimonialsContent">

                <div class="cstmContainer">

                    <div class="testimon">
                        <div class="topRow">
                            <img src="App_Themes/default/imgs/testimonials/1.jpg" />
                            
                            <div class="topRRow">
                               
                                <span class="nameTestim">James</span>
                                <span class="occupTestim">student, Glasgow</span>
                                <span class="dateTestim">7 January 2015</span>
                            </div>
                        </div>

                        <div class="bottomRow">
                           <i class="fa fa-quote-left"></i>
                                malikas is my best restaurant...their food is nice..thanx malikas   
                           <i class="fa fa-quote-right"></i>

                           
                        </div>
                    </div>

                    

                    <div class="testimon">
                        <div class="topRow">
                            <img src="App_Themes/default/imgs/testimonials/2.jpg" />
                            
                            <div class="topRRow">
                               
                                <span class="nameTestim">Andrew</span>
                                <span class="dateTestim">27 December 2014</span>
                                
                            </div>
                        </div>

                        <div class="bottomRow">
                           <i class="fa fa-quote-left"></i>
                                I went to Malikas the other night for dinner while staying in Oxford and received probably the best Indian food I've ever been had before. It's much better than anything you find in America. It's a family-run business and the staff are all genial and very attentive. They even served us complimentary Brandi with our dessert, which was a real   
                           <i class="fa fa-quote-right"></i>

                            
                        </div>
                    </div>

                     <div class="testimon">
                        <div class="topRow">
                            <img src="App_Themes/default/imgs/testimonials/3.jpg" />
                            
                            <div class="topRRow">
                               
                                <span class="nameTestim">Vicky</span>
                                <span class="dateTestim">6 December 2014</span>
                               
                            </div>
                        </div>

                        <div class="bottomRow">
                           <i class="fa fa-quote-left"></i>
                                Having eaten at practically every Asian restaurant in Oxford, I keep coming back here as I think that the food is always excellent plus service is good as it the atmosphere..  
                           <i class="fa fa-quote-right"></i>

                            
                        </div>
                    </div>

                    

                     <div class="testimon">
                        <div class="topRow">
                            <img src="App_Themes/default/imgs/testimonials/4.jpg" />
                            
                            <div class="topRRow">
                               
                                <span class="nameTestim">Shamial</span>
                                <span class="dateTestim">18 November 2014</span>
                             
                            </div>
                        </div>

                        <div class="bottomRow">
                           <i class="fa fa-quote-left"></i>
                                We went to Malikas for my wife's 30th birthday last week and had a brilliant time. There was 31 of us and the buffet food was plentiful and suited every ones palate. All of our guests commented on how nice the food was and on the excellent quality of service that we received from the staff. 
                           <i class="fa fa-quote-right"></i>

                            
                        </div>
                    </div>


                </div>

            </asp:Literal>
        
    </section>

</asp:Content>
