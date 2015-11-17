<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" Inherits="INF.Web.Admin.StaticPages" Codebehind="StaticPages.aspx.vb" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    <%--<%= AdminConstanst.AdminTitle%>--%>
    - Static Pages
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div id="static_pages_header">
        <asp:LinkButton runat="server" ID="ContactUsViewLink" Text="Contact Us"></asp:LinkButton>
        <asp:LinkButton runat="server" ID="AboutUsViewLink" Text="About Us"></asp:LinkButton>
    </div>
    <h2><asp:Literal runat="server" ID="StaticPagesTitle"></asp:Literal></h2>
    <div id="error_messages_wrapper">
        <asp:PlaceHolder runat="server" ID="ErrorMessages"></asp:PlaceHolder>
    </div>
    <hr />
    <div id="static_pages">
        <asp:MultiView runat="server" ID="StaticPagesMultiView">
            <asp:View runat="server" ID="ContactUsView">
                <asp:HiddenField runat="server" ID="ContactUsID" />
                <table style="width: 100%">
                    <tr>
                        <th style="width: 20%; text-align: left;">
                            Map Image
                        </th>
                        <td style="width: 80%">
                            <asp:Image runat="server" ID="MapImage" Width="350px" Height="300px" />
                            <br />
                            <asp:FileUpload runat="server" ID="MapImageFileUpload" />
                            <asp:Button runat="server" ID="MapImageFileUploadButton" Text="Upload" Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <th colspan="2" style="text-align: left;">
                            Contact Information
                        </th>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <CKEditor:CKEditorControl ID="ContactInfo" BasePath="/ckeditor/" runat="server" Width="100%"
                                Height="300px" Toolbar="Basic"></CKEditor:CKEditorControl>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View runat="server" ID="AboutUsView">
                <asp:HiddenField runat="server" ID="AboutUsID" />
                <div>
                    <CKEditor:CKEditorControl ID="AboutUsEditor" BasePath="/ckeditor/" runat="server" Width="100%"
                                Height="500px" ></CKEditor:CKEditorControl>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    <p>
        &nbsp;</p>
    <hr />
    <div style="text-align: center;">
        <asp:Button runat="server" ID="SaveChanges" Width="120px" Height="28px" Text="Save Changes" />
    </div>
</asp:Content>
