<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" Inherits="INF.Web.PasswordRecovery" CodeBehind="PasswordRecovery.aspx.vb" %>

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

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="Server">
  
     <%
         If (ConfigurationManager.AppSettings("titlePasswordRecovery") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titlePasswordRecovery")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titlePasswordRecovery")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%>  
            <%
        End If
    %>   


</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadExtra" runat="Server">
</asp:Content>
<asp:Content ID="MainContentPlaceHolderContent" ContentPlaceHolderID="MainContent" runat="Server">

    <section class="cstmsection">
       
            <asp:MultiView ID="mltFeedback" runat="server">
                <asp:View ID="viewFeedbackSent" runat="server">
                    
                        <h3 class="page-header"><span>Password Recovered & Email Sent</span></h3>

                        <div class="cstmpg">
                            <img src="images/mailsent.png" alt="" style="height: 117px; width: 113px" />
                            <p class="dearRecov">Dear Customer</p>
                            <p>An email has been sent to your email account, please check your email, we are waiting for your order.<br/> In case you have any problem, please Call us</p>
                            
                        </div>
                   
                </asp:View>

                <asp:View ID="viewFeedback" runat="server">
                    
                        <h3 class="page-header"><i class="fa fa-lock"></i>Password recovery</h3>

                        <div class="cstmpg">
                            <img src="images/PasswordRecovery.png" style="height: 116px; width: 114px" />

                            <div class="secondPasRecov">
                                <p>Simply enter your email address you are registered with us and we will email your a new password to the email.</p>

                                <div class="emailRecov">
                                    <label>Email:</label>
                                </div>
                                <div class="inputemRecov">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Style="min-width: 200px; width: 50%;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EmailRequired" ControlToValidate="txtEmail" Display="none" ErrorMessage="<b>Required Field Missing</b><br /><br>Email is required." runat="server"></asp:RequiredFieldValidator>
                                </div>
                                <div class="submitRecov">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Recover Password" ValidationGroup="Join" CssClass="btn btn-success" Style="width: 120px;" />
                                </div>
                            </div>
                        </div>

                   
                </asp:View>
            </asp:MultiView>
       
    </section>

</asp:Content>

