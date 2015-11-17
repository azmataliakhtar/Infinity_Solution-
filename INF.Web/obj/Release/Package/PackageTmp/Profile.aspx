<%@ Page Language="VB" AutoEventWireup="false" Inherits="INF.Web.Profile"
    MasterPageFile="~/SiteMaster.master" Codebehind="Profile.aspx.vb" %>
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
        If (ConfigurationManager.AppSettings("linkCanonicalProfile") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalProfile")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalProfile")%>" />
        <%
        End If
    %>   

     <%
         If (ConfigurationManager.AppSettings("descProfile") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("descProfile")))) Then
        %>                                
            <meta name="description" content="<%=ConfigurationManager.AppSettings("descProfile")%>" />
        <%
        End If
    %>  


</asp:Content>

<asp:Content runat="server" ID="PageTitleContent" ContentPlaceHolderID="PageTitle">
   
     <%
         If (ConfigurationManager.AppSettings("titleProfile") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleProfile")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleProfile")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%> - User Profile 
            <%
        End If
    %>   

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadExtra" runat="Server">
</asp:Content>
<asp:Content runat="server" ID="MainContentPlaceHolderContent" ContentPlaceHolderID="MainContent">

    <section class="wrapper profile cstmsection">

        <h3><i class="fa fa-user"></i>Profile</h3>

        <div class="content">

            <article class="subWrapper">
                <div class="div-validation-summary">
                    <asp:ValidationSummary runat="server" ID="vsInformation" ValidationGroup="UserInformation" ShowSummary="True" EnableClientScript="True" HeaderText="Opps! Something's wrong"/>
                    <%If Not String.IsNullOrWhiteSpace(Me.StatusMessage) Then%>
                        <ul>
                            <li><%= Me.StatusMessage%></li>
                        </ul>
                    <%End If%>
                </div>
                
                <h4>Your information</h4>

                <label>First Name</label>
                <asp:TextBox runat="server" ID="txtFirstName" placeholder="First Name..."></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvFirstName" ControlToValidate="txtFirstName" ErrorMessage="[First Name] is required." ValidationGroup="UserInformation" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>

                <label>Last Name</label>
                <asp:TextBox runat="server" ID="txtLastName" placeholder="Last Name..."></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtLastName" ErrorMessage="[Last Name] is required." ValidationGroup="UserInformation" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>

                <label>Telephone Number</label>
                <asp:TextBox runat="server" ID="txtTelephone" placeholder="Telephone Number..."></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtTelephone" ErrorMessage="[Telephone Number] is required." ValidationGroup="UserInformation" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>

                <label>Mobile Number</label>
                <asp:TextBox runat="server" ID="txtMobile" placeholder="Mobile Number..."></asp:TextBox>

                <label>Email Address</label>
                <asp:TextBox runat="server" ID="txtEmailAddress" ReadOnly="True" placeholder="Email Address..."></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtEmailAddress" ErrorMessage="[Email Address] is required." ValidationGroup="UserInformation" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>

                <asp:Button runat="server" ID="btnUpdateInformation" Text="Update Information" ValidationGroup="UserInformation" OnClick="OnUpdateInformation"/>
            </article>
        
            <div class="clear"></div>

            <article class="subWrapper">
                <div class="div-validation-summary">
                    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="ChangePassword" ShowSummary="True" EnableClientScript="True" HeaderText="Opps! Something's wrong"/>
                    <%If Not String.IsNullOrWhiteSpace(Me.ChangePasswordStatusMessage) Then%>
                        <ul>
                            <li><%= Me.ChangePasswordStatusMessage%></li>
                        </ul>
                    <%End If%>
                </div>
                
                <h4>Change Your Password</h4>

                <label>Old Password</label>
                <asp:TextBox runat="server" ID="txtOldPassword" TextMode="Password" placeholder="Old Password..."></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtOldPassword" ErrorMessage="You have to input Old Password to make this change." ValidationGroup="ChangePassword" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>

                <label>New Password</label>
                <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password" placeholder="New Password..."></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtNewPassword" ErrorMessage="[New Password] is required." ValidationGroup="ChangePassword" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>

                <label>Confirm New Password</label>
                <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" placeholder="Confirm Password..."></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtConfirmPassword" ErrorMessage="Please re-type the new password to make confirmation" ValidationGroup="ChangePassword" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>

                            <asp:CompareValidator runat="server" ID="cvPasswords" ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword"
                                ErrorMessage="The re-typed password is not the same with New Password." ValidationGroup="ChangePassword" SetFocusOnError="True" Display="None"></asp:CompareValidator>

                <asp:Button runat="server" ID="btnSaveChangePassword" Text="Save Password" ValidationGroup="ChangePassword" OnClick="OnChangePassword"/>
            </article>
        
            <div class="clear"></div>
            
            <article class="subWrapper">
                <h4>Your Addresses</h4>
                    <asp:ValidationSummary runat="server" ID="ValidationSummary2" ValidationGroup="UpdateAddress" ShowSummary="True" EnableClientScript="True" HeaderText="Opps! Something's wrong"/>
                
                    <asp:Repeater runat="server" ID="DlAddresses" OnItemCommand="DlAddresses_OnItemCommand">
                        <ItemTemplate>
                            <div class="addrProfile">
                            <label>Address Line</label>
                                                <asp:TextBox runat="server" ID="txtAddressLine1" Width="98%" Text='<%# Eval("Address") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtAddressLine1" ErrorMessage="[Address Line] is required." ValidationGroup="UpdateAddress" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                            
                            <asp:TextBox runat="server" ID="txtAddressLine2" Width="98%" Text='<%# Eval("AddressNotes") %>'></asp:TextBox>
                                
                            <label>Postcode</label>
                                                <asp:TextBox runat="server" ID="txtPostcode" Width="98%" Text='<%# Eval("PostCode") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtPostcode" ErrorMessage="[Postcode] is required." ValidationGroup="UpdateAddress" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                            
                            <label>City</label>
                                                <asp:TextBox runat="server" ID="txtCity" Width="98%" Text='<%# Eval("City") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtCity" ErrorMessage="[City] is required." ValidationGroup="UpdateAddress" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                            
                                    <asp:HiddenField runat="server" ID="hfAddressID" Value='<%# Eval("ID") %>'/>
                                    <asp:Button runat="server" ID="btnUpdateAddress" Text="Update" CssClass="changeaddr" CommandName="UpdateAddress" ValidationGroup="UpdateAddress"/>
                                    <asp:Button runat="server" ID="btnDeleteAddress" Text="Delete" CssClass="changeaddr" CommandName="DeleteAddress" />
                            </div>
                         </ItemTemplate>
                    </asp:Repeater>
            </article>
        </div>
    </section>
</asp:Content>