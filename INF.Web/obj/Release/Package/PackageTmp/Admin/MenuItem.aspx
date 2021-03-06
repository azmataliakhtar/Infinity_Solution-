﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/CategoryMaster.master" CodeBehind="MenuItem.aspx.vb" Inherits="INF.Web.Admin.MenuItem" %>

<%@ Import Namespace="INF.Web.UI.Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <%= EPATheme.Current.Themes.WebsiteName%> - Menu Item
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=chkLinkDressingOrTopping.ClientID%>').click(function () {
                alert(this.checked);
                if (this.checked) {
                    alert('checked');
                    $('#<%=ddlMenuLink1.ClientID%>').attr('disabled', 'disabled');
                } else {
                    alert('unchecked');
                    $('#<%=ddlMenuLink1.ClientID%>').removeAttr('disabled');
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="col-sm-12 bg-info" style="padding: 10px;">
        <div class="col-sm-6 text-left">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button runat="server" ID="btnNewMenuItem" Text="New Menu" CssClass="btn btn-primary" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-sm-6 text-right">
            <asp:Button runat="server" ID="btnBack" Text="Back" CssClass="btn btn-default" Width="110px"/>
        </div>
    </div>
    <article class="col-sm-12" style="padding-left: 0; padding-right: 0;">
        <asp:UpdatePanel runat="server" ID="upMenuItemGrid">
            <ContentTemplate>
                <asp:GridView runat="server" ID="gvMenuItem" DataKeyNames="ID" AutoGenerateColumns="False" CssClass="table table-hover table-striped"
                    AllowPaging="False" BorderStyle="None" BorderWidth="0" GridLines="None">
                    <Columns>
                        <asp:ButtonField HeaderText="#" Text="Select" CommandName="SelectMenuItem" ButtonType="Button" ItemStyle-Width="60px" ControlStyle-CssClass="btn btn-sm btn-info" />
                        <asp:ButtonField HeaderText="" Text="Delete" CommandName="DeleteMenuItem" ButtonType="Button" ItemStyle-Width="60px" ControlStyle-CssClass="btn btn-sm btn-danger" />
                        <asp:BoundField HeaderText="Name" DataField="Name" />
                        <asp:BoundField HeaderText="Position" DataField="ItemPosition" />
                        <asp:BoundField HeaderText="Collection Price" DataField="CollectionPrice" DataFormatString="{0:C2}" />
                        <asp:BoundField HeaderText="Delivery Price" DataField="DeliveryPrice" DataFormatString="{0:C2}" />
                        <asp:ButtonField HeaderText="SUB-ITEMS" Text="Sub Menu Items" CommandName="SubMenuItems" ButtonType="Button" ItemStyle-Width="80px" ControlStyle-CssClass="btn btn-sm btn-warning" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvMenuItem" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
    </article>

    <%--Menu Item Editor Modal//--%>
    <div class="modal fade" id="addMenuItemEditorModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="upMenuItemView">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h3 class="modal-title">
                                <asp:Literal runat="server" ID="ltrMenuItemEditorHeaderText"></asp:Literal></h3>
                        </div>
                        <asp:HiddenField runat="server" ID="hdnMenuItemID" Value="0" />
                        <div class="modal-body col-md-12">
                            <div class="form-group">
                                <div class="col-sm-2">Category</div>
                                <div class="col-sm-10">
                                    <%--<asp:DropDownList ID="CategoryDropdown" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                    <asp:TextBox runat="server" ID="txtCategoryName" CssClass="form-control disabled" Enabled="False" />
                                    <asp:HiddenField runat="server" ID="hdnCategoryID" Value="0" />
                                    <asp:DropDownList runat="server" ID="ddlCategories" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-2">Item Name</div>
                                <div class="col-sm-10">
                                    <asp:TextBox runat="server" ID="txtMenuItemName" CssClass="form-control" placeholder="[Menu Item Name]"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="MenuItemNameRequired" ControlToValidate="txtMenuItemName" ValidationGroup="MenuItemValidation"
                                        SetFocusOnError="True" ErrorMessage="[Menu Item Name] is required" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-2">Position</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtItemPosition" CssClass="form-control" placeholder="0"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtItemPosition" ValidationGroup="MenuItemValidation"
                                        ValidationExpression="^[0-9]+$" ErrorMessage="[Item Position] must be numeric." SetFocusOnError="True" Display="Dynamic" EnableClientScript="True"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-sm-2">Pre. Time (mins)</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtPreparationTime" CssClass="form-control" placeholder="0"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtPreparationTime" ValidationGroup="MenuItemValidation"
                                        ValidationExpression="^[0-9]+$" ErrorMessage="[Pre. Time] must be numeric." SetFocusOnError="True" Display="Dynamic" EnableClientScript="True"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-4">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox runat="server" ID="chkActive" Text="" />Is Actived
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-2"></div>
                                <div class="col-sm-4">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox runat="server" ID="chkBaseSelection" Text="" />Base Selection
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-4 radio">
                                    <label>
                                        <asp:RadioButton ID="radOneSize" GroupName="Size" runat="server" Checked="True" />Is One Size
                                    </label>
                                </div>
                                <div class="col-sm-2"></div>
                                <div class="col-sm-4 radio">
                                    <label>
                                        <asp:RadioButton ID="radMultipleSize" GroupName="Size" runat="server" />Is Multiple Size  
                                    </label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-4 checkbox">
                                    <label>
                                        <asp:CheckBox runat="server" ID="chkLinkDressingOrTopping" />Link Dressing/Topping</label>
                                </div>
                                <div class="col-sm-2"></div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlMenuLink1" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-4 checkbox">
                                    <label>
                                        <asp:CheckBox ID="chkLinkMenu" runat="server" />Link Menu
                                    </label>
                                </div>
                                <div class="col-sm-2"></div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlMenuLink2" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-3">Del. Price</div>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" ID="txtDeliveryPrice" CssClass="form-control" placeholder="0.00"></asp:TextBox>
                                    <%--^[0-9]+\.?[0-9]*$--%>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDeliveryPrice" ValidationGroup="MenuItemValidation"
                                        ValidationExpression="^[0-9]+\.?[0-9]*$" ErrorMessage="[Delivery Price] must be numeric." SetFocusOnError="True" Display="Dynamic" EnableClientScript="True"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-sm-3">Col. Price</div>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" ID="txtCollectionPrice" CssClass="form-control" placeholder="0.00"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtCollectionPrice" ValidationGroup="MenuItemValidation"
                                        ValidationExpression="^[0-9]+\.?[0-9]*$" ErrorMessage="[Collection Price] must be numeric." SetFocusOnError="True" Display="Dynamic" EnableClientScript="True"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-3">Topping Price 1</div>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" ID="txtToppingPrice1" CssClass="form-control" placeholder="0.00"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtToppingPrice1" ValidationGroup="MenuItemValidation"
                                        ValidationExpression="^[0-9]+\.?[0-9]*$" ErrorMessage="[Topping Price 1] must be numeric." SetFocusOnError="True" Display="Dynamic" EnableClientScript="True"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-sm-3">Topping Price 2</div>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" ID="txtToppingPrice2" CssClass="form-control" placeholder="0.00"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtToppingPrice2" ValidationGroup="MenuItemValidation"
                                        ValidationExpression="^[0-9]+\.?[0-9]*$" ErrorMessage="[Topping Price 2] must be numeric." SetFocusOnError="True" Display="Dynamic" EnableClientScript="True"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-3">Topping Price 3</div>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtToppingPrice3" CssClass="form-control" placeholder="0.00"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtToppingPrice3" ValidationGroup="MenuItemValidation"
                                        ValidationExpression="^[0-9]+\.?[0-9]*$" ErrorMessage="[Topping Price 3] must be numeric." SetFocusOnError="True" Display="Dynamic" EnableClientScript="True"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-3">Pro. Text</div>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="PromotionText" CssClass="form-control" placeholder="[Promotion Text]"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-3">Remarks</div>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="Remarks" TextMode="MultiLine" CssClass="form-control" placeholder="[Remarks]"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btnSaveChanges" Text="Save Changes" CssClass="btn btn-danger" Width="130px" ValidationGroup="MenuItemValidation" />
                            <button class="btn btn-default" data-dismiss="modal" aria-hidden="true" style="width: 100px;">Close</button>
                        </div>
                    </ContentTemplate>
                    <Triggers></Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%--//Menu Item Editor Modal--%>
</asp:Content>
