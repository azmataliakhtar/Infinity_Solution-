<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/CategoryMaster.master" CodeBehind="Postcode.aspx.vb" Inherits="INF.Web.Admin.Postcode" %>

<%@ Import Namespace="INF.Web.UI.Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <%= EPATheme.Current.Themes.WebsiteName%>- Postcode
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="col-sm-12 bg-info" style="padding: 10px;">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button runat="server" ID="btnNewPostcode" Text="New Postcode" CssClass="btn btn-primary" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <article class="col-sm-12" style="padding-left: 0; padding-right: 0;">
        <asp:UpdatePanel runat="server" ID="upPostcodeGrid">
            <ContentTemplate>
                <asp:GridView runat="server" ID="gvPostcode" DataKeyNames="ID" AutoGenerateColumns="False" CssClass="table table-hover table-striped"
                    AllowPaging="False" BorderStyle="None" BorderWidth="0" GridLines="None">
                    <Columns>
                        <asp:ButtonField HeaderText="#" Text="Select" CommandName="SelectMenuItem" ButtonType="Button" ItemStyle-Width="60px" ControlStyle-CssClass="btn btn-sm btn-info" />
                        <asp:ButtonField HeaderText="" Text="Delete" CommandName="DeleteMenuItem" ButtonType="Button" ItemStyle-Width="60px" ControlStyle-CssClass="btn btn-sm btn-danger" />
                        <asp:BoundField HeaderText="Postcode" DataField="PostCode" />
                        <asp:BoundField HeaderText="Allowed Delivery" DataField="AllowDelivery" />
                        <asp:BoundField HeaderText="Min. Order Value" DataField="MinOrder" />
                        <asp:BoundField HeaderText="Delivery Charge" DataField="Price" DataFormatString="{0:C2}" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvPostcode" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
    </article>

    <div class="modal fade" id="postcodeEditorModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="upPostcodeView">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h3 class="modal-title">
                                <asp:Literal runat="server" ID="ltrMenuItemEditorHeaderText"></asp:Literal></h3>
                        </div>
                        <asp:HiddenField runat="server" ID="hdnPostcodeID" Value="0" />
                        <div class="modal-body col-md-12">
                            <asp:PlaceHolder runat="server" ID="phMessage" Visible="False">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="alert alert-warning" role="alert">
                                            <asp:Literal runat="server" ID="ltrMessage"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </asp:PlaceHolder>
                            <div class="form-group">
                                <div class="col-sm-4">PostCode</div>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="txtPostcode" CssClass="form-control" placeholder="[Postcode]" />
                                    <asp:RequiredFieldValidator runat="server" ID="PostCodeRequired" ControlToValidate="txtPostcode" SetFocusOnError="True"
                                        ErrorMessage="[PostCode] is required" Display="None" EnableClientScript="True" ValidationGroup="PostcodeValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-4">Delivery Charge</div>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="Price" CssClass="form-control" placeholder="0.00"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="Price" ValidationGroup="PostcodeValidation"
                                        ValidationExpression="^[0-9]+\.?[0-9]*$" ErrorMessage="[Delivery Charge] must be numeric." SetFocusOnError="True" Display="Dynamic" EnableClientScript="True"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-4">Min. Order Value</div>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="MinimumOrderValue" CssClass="form-control" placeholder="0.00"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="MinimumOrderValue" ValidationGroup="PostcodeValidation"
                                        ValidationExpression="^[0-9]+\.?[0-9]*$" ErrorMessage="[Min. Order Value] must be numeric." SetFocusOnError="True" Display="Dynamic" EnableClientScript="True"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-4">&nbsp;</div>
                                <div class="col-sm-8">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox runat="server" ID="AllowDelivery" Text="" />Allow Deliver to this Postcode?
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btnSaveChanges" Text="Save Changes" CssClass="btn btn-danger" Width="130px" ValidationGroup="PostcodeValidation" />
                            <button class="btn btn-default" data-dismiss="modal" aria-hidden="true" style="width: 100px;">Close</button>
                        </div>
                    </ContentTemplate>
                    <Triggers></Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
