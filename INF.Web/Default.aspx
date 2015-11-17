<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="false" Inherits="INF.Web._Default" CodeBehind="Default.aspx.vb" %>

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
        If (ConfigurationManager.AppSettings("linkCanonicalDefault") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalDefault")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalDefault")%>" />
        <%
        End If
    %>   

    <%
        If (ConfigurationManager.AppSettings("descDefault") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("descDefault")))) Then
        %>                                
            <meta name="description" content="<%=ConfigurationManager.AppSettings("descDefault")%>" />
        <%
        End If
    %>   

</asp:Content>

<asp:Content runat="server" ID="PageTitleContent" ContentPlaceHolderID="PageTitle">
  
     <%
         If (ConfigurationManager.AppSettings("titleDefault") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleDefault")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleDefault")%>
        <%
        Else
            %> 
                <%#EPATheme.Current.Themes.WebsiteName%>  
            <%
        End If
    %>   

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadExtra" runat="Server">
</asp:Content>
<asp:Content ID="MainContentPlaceHolderContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:PlaceHolder runat="server" ID="phDefaultPage"></asp:PlaceHolder>
    <script type="text/javascript">
        $(document).ready(function () {
            //resizeDiv();
        });
        

    </script>
</asp:Content>
