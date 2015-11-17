<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/CategoryMaster.master" AutoEventWireup="false" Inherits="INF.Web.Admin_Category" CodeBehind="Category.aspx.vb" %>

<%@ Import Namespace="INF.Web.UI.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="Server">
    <%= EPATheme.Current.Themes.WebsiteName%> - Menu Category
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content runat="server" ID="Content5" ContentPlaceHolderID="ContentPlaceHolder">
    <div class="col-sm-12 bg-info" style="padding: 10px;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Button runat="server" ID="btnNewCategory" Text="New Category" CssClass="btn btn-primary" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <article class="col-sm-12" style="padding-left: 0; padding-right: 0;">&nbsp;</article>
    <article class="col-sm-12" style="padding-left: 0; padding-right: 0;">
        <asp:UpdatePanel runat="server" ID="upCategoryGridView">
            <ContentTemplate>
                <asp:GridView runat="server" ID="gvCategories" DataKeyNames="ID" AutoGenerateColumns="False" CssClass="table table-hover table-striped"
                    AllowPaging="False" BorderStyle="None" BorderWidth="0" GridLines="None">
                    <Columns>
                        <asp:ButtonField HeaderText="#" Text="Select" CommandName="SelectCategory" ButtonType="Button" ItemStyle-Width="60px" ControlStyle-CssClass="btn btn-sm btn-info" />
                        <asp:ButtonField HeaderText="" Text="Delete" CommandName="DeleteCategory" ButtonType="Button" ItemStyle-Width="60px" ControlStyle-CssClass="btn btn-sm btn-danger"/>
                        <asp:BoundField HeaderText="Name" DataField="Name" />
                        <asp:BoundField HeaderText="Position" DataField="ItemPosition" />
                        <asp:BoundField HeaderText="Excl Discount" DataField="ExclOnlineDiscount" />
                        <asp:BoundField HeaderText="Is Deal" DataField="IsDeal" />
                        <asp:BoundField HeaderText="Max. Dressing" DataField="MaxDressing" />
                        <asp:ButtonField HeaderText="MENU-ITEMS" Text="Menu Items" CommandName="SelectMenuItems" ButtonType="Button" ItemStyle-Width="80px" ControlStyle-CssClass="btn btn-sm btn-warning" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </article>
    <%--Add New Category Modal//--%>
    <div class="modal fade" id="addNewCategoryModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h3 class="modal-title">Add New Category</h3>
                </div>
                <asp:UpdatePanel runat="server" ID="upCategoryEntryView">
                    <ContentTemplate>
                        <div class="modal-body col-md-12">
                            <asp:HiddenField runat="server" ID="MenuCategoryID" />
                            <%--<div class="col-sm-6">--%>
                            <div class="form-group">
                                <div class="col-md-4 control-label">Category Name:</div>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control" placeholder="Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="CategoryNameRequired" ControlToValidate="txtName" ValidationGroup="AddNewCategoryValidation"
                                        SetFocusOnError="True" ErrorMessage="[Category Name] is required" Display="None" EnableClientScript="True"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4 control-label">Item Position:</div>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtPosition" CssClass="form-control" placeholder="0"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ID="ItemPositionIsNumber" ControlToValidate="txtPosition"
                                        SetFocusOnError="True" EnableClientScript="True" ErrorMessage="[ItemPosition] must be a number."
                                        ValidationExpression="^([0-9]{1,3})$" Display="None" ValidationGroup="AddNewCategoryValidation"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4 control-label"></div>
                                <div class="col-md-8">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox runat="server" ID="IsActive" Text="" Checked="True"/>Is Active
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4 control-label">Maximum of Dressing:</div>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtMaxOfDressing" CssClass="form-control" placeholder="0"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="txtMaxOfDressing"
                                        SetFocusOnError="True" EnableClientScript="True" ErrorMessage="[Max Dressing] must be a number."
                                        ValidationExpression="^(\d+)$" Display="None" ValidationGroup="AddNewCategoryValidation"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4"></div>
                                <div class="col-md-8">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox runat="server" ID="chkExclDiscount" Text="" />Exclude from Discount
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4"></div>
                                <div class="col-md-8">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox runat="server" ID="chkIsDeal" Text="" />Is Deal Category
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4"></div>
                                <div class="col-md-8">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox runat="server" ID="chkIsAvailableForDeal" Text="" />Is Available for Deal
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <%--</div>
                            <div class="col-sm-6">--%>
                            <%--<div class="form-group form-group-sm">
                                <div class="col-sm-4 control-label">Normal Image</div>
                                <div class="col-sm-8">
                                    <asp:Image runat="server" ID="imgNormalBackground" />
                                    <br />
                                    <asp:FileUpload runat="server" ID="NormalImageFileUpload" Style="display: inline-block;" onchange="uploadImageNow()" />
                                    <asp:Button runat="server" ID="NormalImageFileUploadButton" Text="Upload" CssClass="btn btn-warning" />
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-sm-4 control-label">Hover Image</div>
                                <div class="col-sm-8">
                                    <asp:Image runat="server" ID="imgHoverBackground" />
                                    <br />
                                    <asp:FileUpload runat="server" ID="HoverImageFileUpload" Style="display: inline-block;" />
                                    <asp:Button runat="server" ID="HoverImageFileUploadButton" Text="Upload" CssClass="btn btn-warning" />
                                </div>
                            </div>
                            <div>
                                <div>
                                </div>
                                <div>
                                    <ajaxToolkit:AsyncFileUpload runat="server" ID="AjaxFileUpload1" UploaderStyle="Modern" UploadingBackColor="#66CCFF" ErrorBackColor="Red" ThrobberID="Throbber" />
                                </div>
                            </div>--%>
                            <%--</div>--%>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btnSaveChanges" Text="Save Changes" CssClass="btn btn-danger" Width="130px" ValidationGroup="AddNewCategoryValidation" />
                            <button class="btn btn-default" data-dismiss="modal" aria-hidden="true" style="width: 100px;">Close</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%--//Add New Category Modal--%>
    <script type="text/javascript">
        <%--$(document).ready(function () {
            $('#<%=NormalImageFileUpload.ClientID%>').click(function (event) {
                event.preventDefault();
            });
        });--%>

        <%--$('#<%=NormalImageFileUpload.ClientID%>').click(function (event) {
            event.preventDefault();
        });
        
        function uploadImageNow() {
            var val = $('#<%=NormalImageFileUpload.ClientID%>').val();
            if (val != '') {
                $("#aspnetForm").submit();
            }
        }--%>
    </script>
</asp:Content>
