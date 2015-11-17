<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="false" Inherits="INF.Web.Admin._Default" CodeBehind="Default.aspx.vb" %>

<%@ Import Namespace="INF.Web.UI.Settings" %>

<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="TitlePlaceHolder">
    <%= EPATheme.Current.Themes.WebsiteName%> - Default
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="Server">
</asp:Content>
