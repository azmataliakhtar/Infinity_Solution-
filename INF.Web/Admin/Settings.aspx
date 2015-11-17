<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" Inherits="INF.Web.Admin.Settings" CodeBehind="Settings.aspx.vb" %>

<%@ Import Namespace="INF.Web.UI.Settings" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    <%= EPATheme.Current.Themes.WebsiteName%>- Restaurant Settings
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <asp:ScriptManager runat="server" ID="SettingsScriptManager"></asp:ScriptManager>
    <%--<h3 class="page-header" style="margin-top: 10px;">Restaurant Settings</h3>--%>
    <article class='<%= AlertStyles%>' role="alert">
        <asp:ValidationSummary runat="server" ID="ValidationMsg" EnableClientScript="True"/>
        <asp:Label runat="server" ID="MessageLabel" Text="" EnableViewState="True" Visible="False"></asp:Label>
        <asp:HiddenField runat="server" ID="RestaurantID" Value="" />
    </article>
    <article>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Common Information</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12" style="margin: 2px 0 3px 0;">
                    <div class="col-sm-2">Shop Name <span style="color: red;">*</span></div>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="ShopName" placeholder="Shop Name" CssClass="form-control" required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="ShopNameRequired" ControlToValidate="ShopName"
                            ErrorMessage="[ShopName] is required." SetFocusOnError="True" Display="None"
                            EnableClientScript="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-2">
                        Shop No. <span style="color: red;">*</span>
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="ShopNo" CssClass="form-control" placeholder="Shop No." required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="ShopNoRequired" ControlToValidate="ShopNo"
                            ErrorMessage="[ShopNo] is required." SetFocusOnError="True" Display="None" EnableClientScript="True"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-12" style="margin: 2px 0 3px 0;">
                    <div class="col-sm-2">PostCode <span style="color: red;">*</span></div>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="PostCode" CssClass="form-control" placeholder="XXX XXX" required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="PostCodeRequired" ControlToValidate="PostCode"
                            ErrorMessage="[PostCode] is required." SetFocusOnError="True" Display="None"
                            EnableClientScript="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-2">Building Name</div>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="BuildingName" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12" style="margin: 2px 0 3px 0;">
                    <div class="col-sm-2">Street</div>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" ID="Street" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12" style="margin: 2px 0 3px 0;">
                    <div class="col-sm-2">City</div>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="City" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-2">
                        Telephone
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="Telephone1" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12" style="margin: 2px 0 3px 0;">
                    <div class="col-sm-2">
                        Mobile
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="Mobile" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-2">
                        Fax
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="Fax" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12" style="margin: 2px 0 3px 0;">
                    <div class="col-sm-2">
                        Email
                    </div>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" ID="Email" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Shopping</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12" style="margin: 2px 0 3px 0;">
                    <div class="col-sm-2">Delivery Charge (£)</div>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="DeliveryCharge" CssClass="form-control"></asp:TextBox>
                        <asp:RegularExpressionValidator runat="server" ID="DeliveryChargeIsDecimal" ControlToValidate="DeliveryCharge"
                            EnableClientScript="True" ErrorMessage="[DeliveryCharge] must be a numeric."
                            Display="None" SetFocusOnError="True" ValidationExpression="^([0-9]{1,2}(.[0-9]{1,9}){0,1})$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-sm-2">Service Charge (£)</div>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="ServiceCharge" CssClass="form-control"></asp:TextBox>
                        <asp:RegularExpressionValidator runat="server" ID="ServiceChargeIsDecimal" ControlToValidate="ServiceCharge"
                            EnableClientScript="True" ErrorMessage="[ServiceCharge] must be a numeric." Display="None"
                            SetFocusOnError="True" ValidationExpression="^([0-9]{1,2}(.[0-9]{1,9}){0,1})$"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-12" style="margin: 2px 0 3px 0;">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-4">
                        <asp:CheckBox runat="server" ID="EnableCashPayments" CssClass="checkbox-inline" Text="Enable Cash Payments" />
                    </div>
                    <div class="col-sm-2"></div>
                    <div class="col-sm-4">
                        <asp:CheckBox runat="server" ID="EnableNochex" CssClass="checkbox-inline" Text="Enable Nochex " />
                    </div>
                </div>
                <div class="col-sm-12" style="margin: 2px 0 3px 0;">
                    <div class="col-sm-2"><span style="color: red;">Online Discount&nbsp;(%)</span> </div>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" ID="OnlineDiscountTextBox" CssClass="form-control" required="true"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="OnlineDiscountMaskedEditExtender"
                            TargetControlID="OnlineDiscountTextBox" Mask="99.99" MessageValidatorTip="True"
                            MaskType="Number" />
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Website status</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12" style="margin: 2px 0 3px 0;">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-10">
                        <asp:CheckBox runat="server" ID="WebsiteStatus" onclick="CloseWebsiteConfirm(this);" CssClass="checkbox-inline" Text="Temporaly close website" />
                    </div>
                </div>
            </div>
        </div>

        <%--<h3>Themes:</h3>
        <div>
            <strong>Select theme:</strong>
            <asp:DropDownList runat="server" ID="ddlThemeList" Width="200px"/>
        </div>--%>
    </article>
    <article style="text-align: center">
        <asp:Button runat="server" ID="SaveRestautanSettings" Text="Save Changes" CssClass="btn btn-lg btn-danger" />
    </article>
    <script type="text/javascript">
        function CloseWebsiteConfirm(ctrl) {
            if (ctrl.checked) {
                var result = confirm('Are you sure to want to close website?');
                if (result) {
                    ctrl.checked = true;
                } else {
                    ctrl.checked = false;
                }
            }
        }
    </script>
</asp:Content>
