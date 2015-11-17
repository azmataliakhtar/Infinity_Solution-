<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/ReportingMaster.master" AutoEventWireup="false" Inherits="INF.Web.Admin.ReportSettings" CodeBehind="ReportSettings.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
    <style type="text/css">
       
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="col-md-12 row">
        <h1 class="page-header page-title">Report Setting</h1>
    </div>
    <asp:ScriptManager runat="server" ID="smReporting"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="ServicesChargesUpdatePanel" UpdateMode="Conditional" RenderMode="Block">
        <ContentTemplate>
            <div class="col-md-12 row">
                <div class="col-md-12">
                    <div class="row form-group">
                        <div class="col-md-2">Service Name</div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="ServiceNameTextBox" Width="98%" CssClass="form-control" required="true" placeholder="Service Name"></asp:TextBox>
                        </div>
                        <div class="col-md-6">&nbsp;</div>
                    </div>
                    <div class="row form-group">
                        <div class="col-md-2">Service Charge</div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="ServiceChargeTextBox" Width="98%" CssClass="form-control" required="true" placeholder="0.00"></asp:TextBox>
                        </div>
                        <div class="col-md-6">&nbsp;</div>
                    </div>
                    <div class="row form-group">
                        <div class="col-md-2">Charge On Order</div>
                        <div class="col-md-4">
                            <asp:CheckBox runat="server" ID="chkChargeOnOrder" Width="98%"></asp:CheckBox>
                        </div>
                        <div class="col-md-6">&nbsp;</div>
                    </div>
                    <div class="row form-group">
                        <div class="col-md-2">Enabled</div>
                        <div class="col-md-4">
                            <asp:CheckBox runat="server" ID="ServiceEnabledCheckBox" Width="98%"></asp:CheckBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Button runat="server" ID="SaveServiceChargeButton" Text="Save" CssClass="btn btn-danger" Width="110"/></div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="col-md-12 row">
                <asp:DataGrid runat="server" ID="ServicesChargeDataGrid" AutoGenerateColumns="False" 
                    CssClass="table table-bordered table-striped" Width="100%" BorderColor="#2E4D7B" BackColor="#ffffff">
                    <%--<HeaderStyle CssClass="services-charge-header"></HeaderStyle>--%>
                    <Columns>
                        <asp:BoundColumn DataField="Name" ReadOnly="True" HeaderText="Name" />
                        <asp:BoundColumn DataField="Charge" ReadOnly="True" HeaderText="Charge" />
                        <asp:BoundColumn DataField="IsActived" ReadOnly="True" HeaderText="Enabled" />
                        <asp:TemplateColumn>
                            <ItemStyle Width="70px"></ItemStyle>
                            <ItemTemplate>
                                <a href="/Admin/ReportSettings.aspx?type=edit&id=<%# Eval("ID")%>">Edit</a>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <%--<asp:TemplateColumn>
                                            <ItemStyle Width="70px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="DeleteServiceCharge" Text="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                    </Columns>
                </asp:DataGrid>
            </div>
            <div class="col-md-12 row">
                <asp:Button runat="server" ID="SaveSettingsButton" Text="Save Changes" CssClass="btn btn-danger" Visible="False" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="SaveServiceChargeButton" EventName="Click"/>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

