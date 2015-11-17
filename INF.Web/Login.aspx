<%@ Page Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" Inherits="INF.Web.Login" CodeBehind="Login.aspx.vb" %>

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
        If (ConfigurationManager.AppSettings("linkCanonicalLogin") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalLogin")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalLogin")%>" />
        <%
        End If
    %>   

     <%
         If (ConfigurationManager.AppSettings("descLogin") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("descLogin")))) Then
        %>                                
            <meta name="description" content="<%=ConfigurationManager.AppSettings("descLogin")%>" />
        <%
        End If
    %>  

</asp:Content>

<asp:Content runat="server" ID="PageTitleContent" ContentPlaceHolderID="PageTitle">
    <%
        If (ConfigurationManager.AppSettings("titleLogin") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleLogin")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleLogin")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%>  
            <%
        End If
    %>   
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadExtra" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $(".login-input").keypress(function (e) {
                // No need to do anything if it is not the Enter key
                if (e.keyCode != 13) {
                    return true;
                }

                // Prevent form submit
                e.preventDefault();

                // Trigger the blur event
                this.blur();

                // Submit the form
                //$(e.target).closest('form').submit();
                $(".login-button").click();
                return false;
            });

            $(".register-input").keypress(function (e) {
                // No need to do anything if it is not the Enter key
                if (e.keyCode != 13) {
                    return true;
                }

                // Prevent form submit
                e.preventDefault();

                // Trigger the blur event
                this.blur();

                // Submit the form
                //$(e.target).closest('form').submit();
                $(".register-button").click();
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content runat="server" ID="MainContentPlaceHolderContent" ContentPlaceHolderID="MainContent">
    <section class="">
        
        
            <article class="loginArticleContainer">

               

                <asp:Panel runat="server" ID="pnlUserLoggIn">                    
                    <h3 class="page-header"><i class="fa fa-sign-in"></i>Log In Here</h3>

                   
                    <div class="form-group">

                         <p style="color: red;float:left; text-align:center;">
                            <asp:Literal ID="FailureText" runat="server" Visible="False" EnableViewState="False" Text="Your username or password is invalid. Please try again!"></asp:Literal>
                        </p>

                        <div class="lgnEmail">
                            <asp:ValidationSummary runat="server" ID="LoginValidationSummary" ValidationGroup="logIn" />
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Email address:</asp:Label>
                            <asp:TextBox ID="UserName" runat="server" CssClass="form-control login-input" autofocus placeholder="email address..."></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" EnableClientScript="True" SetFocusOnError="True" ErrorMessage="[Email] is required." ToolTip="[Email] is required." ValidationGroup="logIn" Display="None">
                            </asp:RequiredFieldValidator>
                        </div>

                       <div class="lgnPwd">
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                            <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password" placeholder="password..." ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" EnableClientScript="True" SetFocusOnError="True" ErrorMessage="[Password] is required." ToolTip="Password is required." ValidationGroup="logIn" Display="None">
                            </asp:RequiredFieldValidator>
                        </div>

                       

                        <p class="pwdForgot"><a href="PasswordRecovery.aspx">Forgot your password ?</a></p>

                         <div class="lgnBtn">
                            <asp:Button runat="server" ID="LoginButton" Text="Log In" CssClass="actualLgnBtn" ValidationGroup="logIn" />
                        </div>

                    </div>
                </asp:Panel>
            </article>

            <article class="registerArticleContainer">
               
                <asp:Panel runat="server" ID="pnlRegisterUser" >

                    <h3 class="page-header">New customers? Register Here<i class="fa fa-users"></i></h3>
                    
                    <!--
                    <p class="pregInfo">By creating an account with our store, you will be able to move through the checkout process faster, store multiple shipping addresses, view and track your orders in your account and more.</p>
                    -->
                    
                    <div id="error_messages_wrapper">
                        <asp:PlaceHolder runat="server" ID="ErrorMessages"></asp:PlaceHolder>
                        <asp:ValidationSummary runat="server" ID="NewUserValidationSummary" ValidationGroup="NewUserValidationGroup" ShowSummary="True" />
                    </div>

                    <div class="regAccountFields">

                        <div class="regRow">
                            <div class="regLeft">
                                <label>Phone Number:<span style="color: red;"> *</span></label>
                            </div>
                            <div class="regRight">
                                <asp:TextBox runat="server" ID="txtPhoneNumber" CssClass="form-control" placeholder="phone number..."></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvPhoneNumber" ControlToValidate="txtPhoneNumber" ErrorMessage="[Phone Number] is required." SetFocusOnError="True" EnableClientScript="True" Display="None" ValidationGroup="NewUserValidationGroup">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="regRow">
                            <div class="regLeft">
                                <label>Alt. Number:</label>
                            </div>
                            <div class="regRight">
                                <asp:TextBox runat="server" ID="txtMobilePhone" CssClass="form-control" placeholder="alt. number..."></asp:TextBox>
                            </div>
                        </div>
                        <div class="regRow">
                            <div class="regLeft">
                                <label>First Name:<span style="color: red;"> *</span></label>
                            </div>
                            <div class="regRight">
                                <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" placeholder="first name..."></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvFirstName" ControlToValidate="txtFirstName" ErrorMessage="[First Name] is required." SetFocusOnError="True" EnableClientScript="True" Display="None" ValidationGroup="NewUserValidationGroup">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="regRow">
                            <div class="regLeft">
                                <label>Last Name:<span style="color: red;"> *</span></label>
                            </div>
                            <div class="regRight">
                                <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" placeholder="last name..."></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvLastName" ControlToValidate="txtLastName" ErrorMessage="[Last Name] is required." SetFocusOnError="True" EnableClientScript="True" Display="None" ValidationGroup="NewUserValidationGroup">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="regRow">
                            <div class="regLeft">
                                <label>Address:<span style="color: red;"> *</span></label>
                            </div>
                            <div class="regRight">
                                <asp:TextBox runat="server" ID="txtAddressLine1" CssClass="form-control" placeholder="address line 1..."></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvAddressLine1" ControlToValidate="txtAddressLine1" ErrorMessage="[Address] is required." SetFocusOnError="True" EnableClientScript="True" Display="None" ValidationGroup="NewUserValidationGroup">
                                </asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <div class="regRow">
                            <div class="regLeft"></div>
                            <div class="regRight">
                                <asp:TextBox runat="server" ID="txtAddressLine2" CssClass="form-control" placeholder="address line 2..."></asp:TextBox>
                            </div>
                        </div>
                        <div class="regRow">
                            <div class="regLeft">
                                <label>City:<span style="color: red;"> *</span></label>
                            </div>
                            <div class="regRight">
                                <asp:TextBox runat="server" ID="txtCity" CssClass="form-control" placeholder="city..."></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvCity" ControlToValidate="txtCity" ErrorMessage="[City] is required/" SetFocusOnError="True" EnableClientScript="True" Display="None" ValidationGroup="NewUserValidationGroup">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="regRow">
                            <div class="regLeft">
                                <label>Postcode:<span style="color: red;"> *</span></label>
                            </div>
                            <div class="regRight">
                                <asp:TextBox runat="server" ID="txtPostcode" CssClass="form-control" placeholder="postcode..."></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvPostcode" ControlToValidate="txtPostcode" ErrorMessage="[Postcode] is required." SetFocusOnError="True" EnableClientScript="True" Display="None" ValidationGroup="NewUserValidationGroup">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="regRow">
                            <div class="regLeft">
                                <label>Email Address:<span style="color: red;"> *</span></label></div>
                            <div class="regRight">
                                <asp:TextBox runat="server" ID="txtEmailAddress" CssClass="form-control" placeholder="email address..."></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="EmailTextBoxRequired" ControlToValidate="txtEmailAddress" ErrorMessage="[Email Address] is required." SetFocusOnError="True" EnableClientScript="True" Display="None" ValidationGroup="NewUserValidationGroup">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="regRow">
                            <div class="regLeft">
                                <label>Password:<span style="color: red;"> *</span></label>
                            </div>
                            <div class="regRight">
                                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control" placeholder="password ..."></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="PasswordTextBoxRequired" ControlToValidate="txtPassword" EnableClientScript="True" ErrorMessage="[Password] is required" SetFocusOnError="True" Display="None" ValidationGroup="NewUserValidationGroup">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="regRow">
                            <div class="regLeft">
                                <label>Confirm Password<span style="color: red;"> *</span></label>
                            </div>
                            <div class="regRight">
                                <asp:TextBox runat="server" ID="txtReTypePassword" TextMode="Password" CssClass="form-control" placeholder="confirm password ..."></asp:TextBox>
                            </div>
                        </div>
                        <div class="regRowRegister">
                            <asp:Button runat="server" ID="CreateNewUserButton" Text="Create New" CssClass="registerBtnActual" ValidationGroup="NewUserValidationGroup"/>
                        </div>
                    </div>

                </asp:Panel>
            </article>
       
    </section>
    <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="False" Font-Size="11pt" ForeColor="Red" Visible="False"></asp:Label>
</asp:Content>
