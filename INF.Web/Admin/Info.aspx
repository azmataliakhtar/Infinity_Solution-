<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" Inherits="INF.Web.Admin.Info" CodeBehind="Info.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="Server">
    <%--<%= AdminConstanst.AdminTitle%>--%>
    - Website Information
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SidebarPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <%--<h3 class="page-header" style="margin-top: 10px;">Website Information</h3>--%>
    <article>
        <asp:ValidationSummary runat="server" ID="ValidationMsg" EnableClientScript="True" />
        <asp:Label runat="server" ID="MessageLabel" Text="" EnableViewState="True" Visible="False"></asp:Label>
    </article>
    <article>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Website Information</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12" style="padding: 2px 0 3px 0;">
                    <div class="col-sm-2 text-right">Website Name <span style="color: red;">*</span></div>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" ID="WebsiteName" CssClass="form-control" required="true" placeholder="Website Name"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="WebsiteNameRequired" ControlToValidate="WebsiteName" Display="None"
                            SetFocusOnError="True" ErrorMessage="[Website Name] is required." EnableClientScript="True"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-12" style="padding: 2px 0 3px 0;">
                    <div class="col-sm-2 text-right">Meta</div>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" ID="WebsiteMeta" CssClass="form-control" TextMode="MultiLine" Height="300px"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </article>
    <hr />
    <article style="text-align: center;">
        <asp:Button runat="server" ID="SaveChanges" Text="Save Changes" CssClass="btn btn-lg btn-primary" />
    </article>
</asp:Content>
