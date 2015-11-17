<%@ page title="" language="VB" masterpagefile="~/SiteMaster.master" autoeventwireup="false" Inherits="INF.Web.PhotoGallery" Codebehind="PhotoGallery.aspx.vb" %>
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
        If (ConfigurationManager.AppSettings("linkCanonicalPhotoGallery") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalPhotoGallery")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalPhotoGallery")%>" />
        <%
        End If
    %>   

    <%
        If (ConfigurationManager.AppSettings("descPhotoGallery") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("descPhotoGallery")))) Then
        %>                                
            <meta name="description" content="<%=ConfigurationManager.AppSettings("descPhotoGallery")%>" />
        <%
        End If
    %>   

</asp:Content>

<asp:Content runat="server" ID="PageTitleContent" ContentPlaceHolderID="PageTitle">
    
     <%
         If (ConfigurationManager.AppSettings("titlePhotoGallery") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titlePhotoGallery")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titlePhotoGallery")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%> - Photo Gallery 
            <%
        End If
    %>   

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadExtra" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
                   
          
            $(".fancybox").fancybox({
                beforeShow: function () {
                   
                }
            });


        });

    </script>

</asp:Content>
<asp:Content ID="MainContentPlaceHolderContent" ContentPlaceHolderID="MainContent" runat="Server">

    <section class="cstmsection">

        <h3 class="page-header page-title"><i class="fa fa-photo"></i>Photo Gallery</h3>
            
                    

           
        <!--
         <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/1big.jpg"><img src="App_Themes/default/imgs/photogallery/1.jpg"/></a>
        -->
                 
        
         <%
             If (ConfigurationManager.AppSettings("pgImages") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("pgImages")))) Then
               
                 Dim pgNr As Integer = FormatNumber(ConfigurationManager.AppSettings("pgImages"))
                     
                 For numberSlides As Integer = 0 To pgNr Step +1
                        
                         
                     If (numberSlides = 1) Then
                     %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/1big.jpg"><img src="App_Themes/default/imgs/photogallery/1.jpg"/></a> <%
                     ElseIf (numberSlides = 2) Then
                      %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/2big.jpg"><img src="App_Themes/default/imgs/photogallery/2.jpg"/></a> <%
                     ElseIf (numberSlides = 3) Then
                      %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/3big.jpg"><img src="App_Themes/default/imgs/photogallery/3.jpg"/></a> <%    
                     ElseIf (numberSlides = 4) Then
                        %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/4big.jpg"><img src="App_Themes/default/imgs/photogallery/4.jpg"/></a> <%  
                     ElseIf (numberSlides = 5) Then
                       %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/5big.jpg"><img src="App_Themes/default/imgs/photogallery/5.jpg"/></a> <%   
                     ElseIf (numberSlides = 6) Then
                        %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/6big.jpg"><img src="App_Themes/default/imgs/photogallery/6.jpg"/></a> <%  
                     ElseIf (numberSlides = 7) Then
                       %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/7big.jpg"><img src="App_Themes/default/imgs/photogallery/7.jpg"/></a> <%   
                     ElseIf (numberSlides = 8) Then
                        %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/8big.jpg"><img src="App_Themes/default/imgs/photogallery/8.jpg"/></a> <%  
                     ElseIf (numberSlides = 9) Then
                        %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/9big.jpg"><img src="App_Themes/default/imgs/photogallery/9.jpg"/></a> <%  
                     ElseIf (numberSlides = 10) Then
                        %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/10big.jpg"><img src="App_Themes/default/imgs/photogallery/10.jpg"/></a> <%  
                     ElseIf (numberSlides = 11) Then
                        %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/11big.jpg"><img src="App_Themes/default/imgs/photogallery/11.jpg"/></a> <%  
                     ElseIf (numberSlides = 12) Then
                        %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/12big.jpg"><img src="App_Themes/default/imgs/photogallery/12.jpg"/></a> <%  
                     ElseIf (numberSlides = 13) Then
                        %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/13big.jpg"><img src="App_Themes/default/imgs/photogallery/13.jpg"/></a> <%  
                     ElseIf (numberSlides = 14) Then
                         %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/14big.jpg"><img src="App_Themes/default/imgs/photogallery/14.jpg"/></a> <% 
                     ElseIf (numberSlides = 15) Then
                        %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/15big.jpg"><img src="App_Themes/default/imgs/photogallery/15.jpg"/></a> <%  
                     ElseIf (numberSlides = 16) Then
                        %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/16big.jpg"><img src="App_Themes/default/imgs/photogallery/16.jpg"/></a> <%  
                     ElseIf (numberSlides = 17) Then
                        %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/17big.jpg"><img src="App_Themes/default/imgs/photogallery/17.jpg"/></a> <%  
                     ElseIf (numberSlides = 18) Then
                        %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/18big.jpg"><img src="App_Themes/default/imgs/photogallery/18.jpg"/></a> <%  
                     ElseIf (numberSlides = 19) Then
                       %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/19big.jpg"><img src="App_Themes/default/imgs/photogallery/19.jpg"/></a> <%   
                     ElseIf (numberSlides = 20) Then
                         %> <a class="pgelem fancybox" rel="gallery" href="/App_Themes/default/imgs/photogallery/20big.jpg"><img src="App_Themes/default/imgs/photogallery/20.jpg"/></a> <% 
                         
                         
                     End If
                     Next
                     
               
                    
            End If
            %>

          
         
    </section>

</asp:Content>
