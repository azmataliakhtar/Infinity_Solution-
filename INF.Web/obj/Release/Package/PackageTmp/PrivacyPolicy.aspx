<%@ page title="" language="VB" masterpagefile="~/SiteMaster.master" autoeventwireup="false" Inherits="INF.Web.PrivacyPolicy" Codebehind="PrivacyPolicy.aspx.vb" %>
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
        If (ConfigurationManager.AppSettings("linkCanonicalPrivacy") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalPrivacy")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalPrivacy")%>" />
        <%
        End If
    %>   

     <%
         If (ConfigurationManager.AppSettings("descPrivacy") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("descPrivacy")))) Then
        %>                                
            <meta name="description" content="<%=ConfigurationManager.AppSettings("descPrivacy")%>" />
        <%
        End If
    %>  

</asp:Content>

<asp:Content runat="server" ID="PageTitleContent" ContentPlaceHolderID="PageTitle">
    

     <%
         If (ConfigurationManager.AppSettings("titlePrivacy") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titlePrivacy")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titlePrivacy")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%> - Privacy Policy 
            <%
        End If
    %>   

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadExtra" runat="Server"></asp:Content>
<asp:Content ID="MainContentPlaceHolderContent" ContentPlaceHolderID="MainContent" runat="Server">

    <section class="cstmsection">
            <h3 class="page-header page-title"><i class="fa fa-eye-slash"></i>Privacy Policy</h3>
            
        
        
          <div class="privacycnt">

             <p>This privacy policy has been compiled to better serve those who are concerned with how their 'Personally identifiable information' (PII) is being used online. 
            PII, as used in US privacy law and information security, is information that can be used on its own or with other information to identify, contact, or locate a single person, 
            or to identify an individual in context.</p>

        <p>Please read our privacy policy carefully to get a clear understanding of how we collect, use, protect or otherwise handle your Personally Identifiable Information in accordance with 
        our website.</p>

        

        <p class="psubhead">
         What personal information do we collect from the people that visit our blog, website or app?
        </p>

        <p>When ordering or registering on our site, as appropriate, you may be asked to enter your name, email address, mailing address, phone number  or other details
         to help you with your experience.
         </p>

         <p class="psubhead">When do we collect information?</p>

         <p>We collect information from you when you register on our site, place an order  or enter information on our site.</p>

         <p class="psubhead">How do we use your information?</p>

         <p> We may use the information we collect from you when you register, make a purchase, sign up for our newsletter, respond to a survey or marketing communication, 
         surf the website, or use certain other site features in the following ways:<p>

         <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; • To quickly process your transactions.</p>

         <p class="psubhead">How do we protect visitor information?</p>
         <p class="">We do not use vulnerability scanning and/or scanning to PCI standards.</p>
         <p class="">We use regular Malware Scanning.</p>
         <p class="">We do not use an SSL certificate</p>

         <p class="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; • We do not need an SSL because:</p>
         <p class="">Because our Payment Page provided by the company we work with has SSL</p>

         <p class="psubhead">Do we use 'cookies'?</p>

         <p>Yes. Cookies are small files that a site or its service provider transfers to your computer's hard drive through your Web browser (if you allow) that enables the site's or service provider's systems to recognize your browser and capture and remember certain information. For instance, we use cookies to help us remember and process the items in your shopping cart. They are also used to help us understand your preferences based on previous or current site activity, which enables us to provide you with improved services. We also use cookies to help us compile aggregate data about site traffic and site interaction so that we can offer better site experiences and tools in the future.</p>
        
         <p class="">We use cookies to:</p>
         <p class="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; • Help remember and process the items in the shopping cart.</p>
         <p class="">You can choose to have your computer warn you each time a cookie is being sent, or you can choose to turn off all cookies. You do this through your browser (like Internet Explorer) settings. Each browser is a little different, so look at your browser's Help menu to learn the correct way to modify your cookies.</p>

         <p class="">If users disable cookies in their browser:</p><br><p class="">If you disable cookies off, some features
          will be disabled It will turn off some of the features that make your site experience more efficient and some of our services will not function properly.</p><br>
          <p class="">However, you can still place orders. You will not be able to process your order over the telephone .</p>


              
        <p class="psubhead">Third Party Disclosure</p>
        <p class="">We do not sell, trade, or otherwise transfer to outside parties your personally identifiable information.</p>
        
        <p class="psubhead">Third party links</p>
        <p class="">Occasionally, at our discretion, we may include or offer third party products or services on our website. These third party sites have separate and independent privacy policies. We therefore have no responsibility or liability for the content and activities of these linked sites. Nonetheless, we seek to protect the integrity of our site and welcome any feedback about these sites.</p>

         <p class="psubhead">If there are any questions regarding this privacy policy you may contact us using the information below.</p>
        
         <p class=""> <%=ConfigurationManager.AppSettings("wbsUrl")%> </p>
         <p class=""> <%=ConfigurationManager.AppSettings("contactCityCountry")%></p>
         <p class=""> <%=ConfigurationManager.AppSettings("customerAddr1")%>,  <%=ConfigurationManager.AppSettings("contactPostcode")%></p>
         <p class=""> <%=ConfigurationManager.AppSettings("contactEmail")%></p>
        
          
          </div>
    </section>

</asp:Content>
