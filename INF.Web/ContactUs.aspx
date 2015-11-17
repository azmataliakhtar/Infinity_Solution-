<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" Inherits="INF.Web.ContactUs" CodeBehind="ContactUs.aspx.vb" %>

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
        If (ConfigurationManager.AppSettings("linkCanonicalContactUs") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalContactUs")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalContactUs")%>" />
        <%
        End If
    %>   

      <%
          If (ConfigurationManager.AppSettings("descContactUs") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("descContactUs")))) Then
        %>                                
            <meta name="description" content="<%=ConfigurationManager.AppSettings("descContactUs")%>" />
        <%
        End If
    %>  

</asp:Content>

<asp:Content runat="server" ID="PageTitleContent" ContentPlaceHolderID="PageTitle">
    
    <%
        If (ConfigurationManager.AppSettings("titleContactUs") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleContactUs")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleContactUs")%>
        <%
        Else
            %> 
                <%=EPATheme.Current.Themes.WebsiteName%>  
            <%
        End If
    %>   

</asp:Content>
<asp:Content ID="MainContentPlaceHolderContent" ContentPlaceHolderID="MainContent"
    runat="Server">
    <section class="cstmsection">
        
        <h3 class="page-header page-title"><i class="fa fa-newspaper-o"></i>Contact Us</h3>
            
        <div class="contactusHidden">
             <asp:Image runat="server" ID="MapImage" />    
        </div>

        <%
            If (ConfigurationManager.AppSettings("gmapUrl") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("gmapUrl")))) Then
                %>
                     <div class="leftSideCt">
                        <iframe src="<%=ConfigurationManager.AppSettings("gmapUrl")%>" width="360" height="320" frameborder="0" style="border:0"></iframe>
                    </div>
                <%
            End If
         %>


        <div class="rightSideCt">
            <div class="actualTextCt">

                <div style="display:none">
                 <asp:Label runat="server" ID="ContactInfo"></asp:Label>
                </div>

               
                            

            </div>

            <div class="contactInfoCt">
                <p>Email: <%=ConfigurationManager.AppSettings("contactEmail")%></p>
                <p><a class="contactusPhone" href="tel:<%=ConfigurationManager.AppSettings("contactPhone")%>">Phone: <%=ConfigurationManager.AppSettings("contactPhone")%></a></p>
                <p><%=ConfigurationManager.AppSettings("customerAddr1")%></p>
                <p>Postcode: <%=ConfigurationManager.AppSettings("contactPostcode")%></p>
                <p><%=ConfigurationManager.AppSettings("contactCityCountry")%></p>
            </div>
        </div>

        


       

    </section>
</asp:Content>
