<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" Inherits="INF.Web.Admin.Users" CodeBehind="Users.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <asp:ScriptManager runat="server" ID="scriptManager1"></asp:ScriptManager>
    <article>
        <h3 class="page-header" style="margin-top: 10px;">System Users</h3>
    </article>
    <article>
        <article>
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button runat="server" ID="CreateUserButton" Text="Create User" CssClass="btn btn-info" Width="120px" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
        <article style="margin-top: 20px;">
            <asp:UpdatePanel runat="server" ID="upUsersGrid">
                <ContentTemplate>
                    <div>
                        <asp:PlaceHolder runat="server" ID="MessageBox" Visible="False">
                            <ul>
                                <li style="color: #C44113;">
                                    <asp:Literal runat="server" ID="Messages"></asp:Literal></li>
                            </ul>
                        </asp:PlaceHolder>
                    </div>
                    <div>
                        <p>
                            There are<b>&nbsp;<asp:Literal runat="server" ID="ltrNumberOfUsers"></asp:Literal></b>
                        </p>
                    </div>
                    <div>
                        <asp:GridView runat="server" ID="UsersGridView" DataKeyNames="ID" AutoGenerateColumns="False" CssClass="table table-hover table-striped"
                            AllowPaging="True" PageSize="25" BorderStyle="None" BorderWidth="0" GridLines="None">
                            <PagerStyle HorizontalAlign="Right" Height="26" Font-Bold="True"></PagerStyle>
                            <Columns>
                                <asp:ButtonField CommandName="EditUser" Text="Edit" HeaderText="" ControlStyle-CssClass="btn btn-info" ButtonType="Button" ItemStyle-Width="60px">
                                    <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="ChangePassword" Text="ChangePassword" HeaderText="" ControlStyle-CssClass="btn btn-warning" ButtonType="Button" ItemStyle-Width="100px">
                                    <ControlStyle CssClass="btn btn-warning"></ControlStyle>
                                </asp:ButtonField>
                                <asp:BoundField HeaderText="User Name" DataField="UserName" ItemStyle-Width="150px" />
                                <asp:BoundField HeaderText="Email" DataField="Email" />
                                <asp:BoundField HeaderText="Full Name" DataField="FullName" />
                                <asp:BoundField HeaderText="Is Actived" DataField="IsActived" ItemStyle-Width="100px" />
                                <asp:BoundField HeaderText="User Role" DataField="UserRole" ItemStyle-Width="100px" />
                                <asp:BoundField HeaderText="Last Logged In" DataField="LastLoggedIn" ItemStyle-Width="160px" />
                            </Columns>
                            <FooterStyle CssClass="table-footer"></FooterStyle>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="SaveButton" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="SaveEditButton" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </article>

        <%--Add New User Popup - Using Bootstrap--%>
        <div class="modal fade" id="addNewUserModal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h3 class="modal-title">Add New User</h3>
                    </div>
                    <asp:UpdatePanel runat="server" ID="upAddNewUser">
                        <ContentTemplate>
                            <div class="modal-body col-sm-12">
                                <div>
                                    <div class="col-sm-4">User Name:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="UserNameTextBox" CssClass="form-control" placeholder="username"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="EmailTextBox" Display="Dynamic"
                                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="CreateUser">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">Email:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="EmailTextBox" CssClass="form-control" placeholder="account@domail.com"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RFVEmail" ControlToValidate="EmailTextBox" Display="Dynamic"
                                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="CreateUser">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">Confirm Email:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="ConfirmEmailTextBox" CssClass="form-control" placeholder="account@domail.com"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="ConfirmEmailTextBox" Display="Dynamic"
                                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="CreateUser">
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="ConfirmEmailTextBox" ControlToCompare="EmailTextBox" Display="Dynamic"
                                            SetFocusOnError="True" ErrorMessage="Comfirm Email does not match the Email!" ValidationGroup="CreateUser"></asp:CompareValidator>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">Password:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="PasswordTextBox" TextMode="Password" CssClass="form-control" placeholder="********"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="PasswordTextBox" Display="Dynamic"
                                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">Confirm Password:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="ConfirmPasswordTextBox" TextMode="Password" CssClass="form-control" placeholder="********"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="ConfirmPasswordTextBox" Display="Dynamic"
                                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="ConfirmPasswordTextBox" ControlToCompare="PasswordTextBox" Display="Dynamic"
                                            SetFocusOnError="True" ErrorMessage="Confirm Password does not match the Password" ValidationGroup="CreateUser"></asp:CompareValidator>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">Last Name:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" placeholder="Your LastName"></asp:TextBox>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">First Name:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" placeholder="Your FirstName"></asp:TextBox>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">Role:</div>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" ID="UserRolesDropDownList" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" ID="SaveButton" Text="Save" CssClass="btn btn-info" Width="100px" ValidationGroup="CreateUser" />
                                <button class="btn btn-default" data-dismiss="modal" aria-hidden="true" style="width: 100px;">Close</button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="SaveButton" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
        <%--//Add New User Popup - Using Bootstrap--%>

        <%--Edit User Popup - Bootstrap--%>
        <div class="modal fade" id="editUserModal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h3 class="modal-title">Edit Existing User</h3>
                    </div>
                    <asp:UpdatePanel runat="server" ID="upEditUser">
                        <ContentTemplate>
                            <div class="modal-body col-sm-12">
                                <div>
                                    <div class="col-sm-4">User Name:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtEditUserName" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEditUserName" Display="Dynamic"
                                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="EditUser">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">Email:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtEditEmail" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtEditEmail" Display="Dynamic"
                                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="EditUser">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">Last Name:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtEditLastName" CssClass="form-control" placeholder="Your LastName"></asp:TextBox>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">First Name:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtEditFirstName" CssClass="form-control" placeholder="Your FirstName"></asp:TextBox>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">Role:</div>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" ID="ddlEditUserRole" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">&nbsp;</div>
                                    <div class="col-sm-8">
                                        <asp:CheckBox runat="server" ID="chkActivated" CssClass="checkbox-inline" Text="Is Activated"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" ID="SaveEditButton" Text="Save" CssClass="btn btn-info" Width="100px" ValidationGroup="EditUser" />
                                <button class="btn btn-default" data-dismiss="modal" aria-hidden="true" style="width: 100px;">Close</button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="UsersGridView" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="SaveEditButton" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <%--//Edit User Popup - Bootstrap--%>

        <%--Change Password Popup - Bootstrap--%>
        <div class="modal fade" id="changePasswordModal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h3 class="modal-title">Change Password</h3>
                    </div>
                    <asp:UpdatePanel runat="server" ID="upChangePassword">
                        <ContentTemplate>
                            <div class="modal-body col-sm-12">
                                <div>
                                    <div class="col-sm-4">User Name:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtChangePassUserName" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">Old Password:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtChangePassOldPassword" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="txtChangePassOldPassword" Display="Dynamic"
                                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="ChangePassword">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">New Password:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtChangePassNewPassword" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="txtChangePassNewPassword" Display="Dynamic"
                                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="ChangePassword">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                                <div>
                                    <div class="col-sm-4">New Password Confirmation:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtChangePassNewPasswordConfirm" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="txtChangePassNewPasswordConfirm" Display="Dynamic"
                                            SetFocusOnError="True" ErrorMessage="*Required Field!" ValidationGroup="ChangePassword">
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtChangePassNewPasswordConfirm" ControlToCompare="txtChangePassNewPassword" Display="Dynamic"
                                            SetFocusOnError="True" ErrorMessage="Confirm Password does not match the Password" ValidationGroup="ChangePassword"></asp:CompareValidator>
                                    </div>
                                </div>
                                <p style="line-height: 1px;">&nbsp;</p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" ID="btnSaveChangePassword" Text="Save" CssClass="btn btn-info" Width="100px" ValidationGroup="ChangePassword" />
                                <button class="btn btn-default" data-dismiss="modal" aria-hidden="true" style="width: 100px;">Close</button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="UsersGridView" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="btnSaveChangePassword" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <%--//Change Password Popup - Bootstrap--%>
    </article>
</asp:Content>

