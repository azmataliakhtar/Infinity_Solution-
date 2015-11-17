<%@ page title="" language="VB" masterpagefile="~/SiteMaster.master" autoeventwireup="false" Inherits="INF.Web.PaymentSuccess" Codebehind="PaymentSuccess.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadExtra" runat="Server">
    <link href="Css/styles.css" rel="stylesheet" type="text/css" />
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


    <section class="cstmsection">
                
                
                 <h3><i class="fa fa-credit-card"></i>Payment Successful</h3>
               
                     <br/>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="font-size: 14px;">
                        <tr>
                            <td width="150" align="left" valign="top">
                                 
                                <img src="App_Themes/default/images/PaymentSuccess.png" alt="" width="130" height="138" />
                            </td>
                            <td width="454" align="left" valign="top" class="bodytext2">
                                
                                <br/>
                                <p>
                                    Thans for your online payment, You payment is authorized and we have received 
                                    the money.&nbsp;
                                </p>
                                <p>
                                    We are trying our best to prepare your order as soon as possible, You can track 
                                    your order status using this website track link or call anytime in store at 
                                   <span class="coloredPhone"><%=ConfigurationManager.AppSettings("customerPhone")%></span>
                                </p>
                                <p>
                                    Thanks for your order !
                                </p>
                               
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
               
    </section>
</asp:Content>
