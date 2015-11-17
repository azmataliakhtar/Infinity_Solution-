<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" Inherits="INF.Web.ErrorHandler" CodeBehind="Error.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadExtra" runat="Server">
</asp:Content>

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

<asp:Content ID="MainContentPlaceHolderContent" ContentPlaceHolderID="MainContent"
    runat="Server">
    <%If String.IsNullOrEmpty(ErrorMessage) Then%>

    <section class="cstmsection">
        <h3 class="page-header page-title">Techical Problem</h3>
        <div class="text-center">
            <img src="images/Error.png" alt="" style="width: 130px;height: 138px;"/>
            <p>Oops ! We have met a technical error, please close your browser and try again.</p>
            <p>We are aware of the problem and trying to fix as soon as possible.</p>
            <p>Sorry for any inconvinence.</p>
        </div>
    </section>

    <% Else%>
    <section class="cstmsection">
        <h3 class="page-header page-title">Opps! There is an error occurring.</h3>
        <div class="text-center">
            
            <br/><br/><br/>
            <img src="App_Themes/default/images/Error.png" alt="" style="width: 130px;height: 138px;"/>
            <p>
                <asp:Literal runat="server" ID="ltrErrorMessage"></asp:Literal>
            </p>
            <p style="color: red;font-size: 130%;">If you are in progress of Checkout Order, then kindly contact the website owener.</p>
        </div>
    </section>
    <% End If%>
 
</asp:Content>
