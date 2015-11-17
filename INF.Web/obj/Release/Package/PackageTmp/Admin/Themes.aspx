<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="false" Inherits="INF.Web.Admin.Themes" CodeBehind="Themes.aspx.vb" %>

<%@ Import Namespace="INF.Web.UI.Settings" %>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    <%= EPATheme.Current.Themes.WebsiteName%> - Themes Settings
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <asp:ScriptManager runat="server" ID="ThemesScriptManager"></asp:ScriptManager>
    <article>
        <h3 class="page-header" style="margin-top: 10px; margin-bottom: 10px;">Themes Settings</h3>
    </article>
    <article id="error_messages_wrapper">
        <asp:PlaceHolder runat="server" ID="ErrorMessages"></asp:PlaceHolder>
    </article>

    <article id="themes_tabs">
        <article id="themes_tabs_header">
            <asp:LinkButton runat="server" ID="ShowLogoAndSloganView" Text="→ Logo and Slogan"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="ShowNavigationImagesView" Text="→ Navigation Images"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="ShowHeaderView" Text="→ Header"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="ShowFooterView" Text="→ Footer"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="ShowHomePageView" Text="→ HomePage"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="ShowMenuCategory" Text="→ Menu Category"></asp:LinkButton>
        </article>
        <article id="themes_tabs_content">
            <asp:MultiView runat="server" ID="ThemesMultiView">
                <asp:View runat="server" ID="LogoAndSloganView">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Logo and Slogan</h3>
                        </div>
                        <div class="panel-body">
                            <div>
                                <div class="col-sm-3 text-right">Slogan</div>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="Slogan" CssClass="form-control" placeholder="Your Slogan"></asp:TextBox>
                                </div>
                            </div>
                            <p>&nbsp;</p>
                            <div>
                                <div class="col-sm-3 text-right">Logo</div>
                                <div class="col-sm-9">
                                    Width (px): &nbsp;<asp:TextBox runat="server" ID="LogoImageWidth" CssClass="form-control" Width="65px" Text="160" placeholder="0" required="true" Style="display: inline-block;"></asp:TextBox>
                                    Height (px): &nbsp;<asp:TextBox runat="server" ID="LogoImageHeight" CssClass="form-control" Width="65px" Text="90" placeholder="0" required="true" Style="display: inline-block;"></asp:TextBox>
                                    <br />
                                    <asp:Image runat="server" ID="Logo" Height="90px" ImageUrl="../Images/logo.png" CssClass="img-thumbnail" />
                                    <br />
                                    <asp:FileUpload runat="server" ID="LogoFileUpload" Style="display: inline-block;" />
                                    <asp:Button runat="server" ID="LogoFileUploadButton" Text="Upload" Width="100px" CssClass="btn btn-warning" />
                                </div>
                            </div>
                            <p>&nbsp;</p>
                        </div>
                    </div>
                </asp:View>
                <asp:View runat="server" ID="NavigationImagesView">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Horizontal Navigation</h3>
                        </div>
                        <div class="panel-body">
                            <div>
                                <div class="col-sm-3 text-right">Navigation Image</div>
                                <div class="col-sm-9">
                                    Width (px): &nbsp;<asp:TextBox runat="server" ID="NavigationImageWitdh" Width="65px" Text="160" Style="display: inline-block;" CssClass="form-control" placeholder="0" required="true"></asp:TextBox>
                                    &nbsp; - &nbsp; Height (px): &nbsp;<asp:TextBox runat="server" ID="NavigationImageHeight" Width="65px" Text="90" Style="display: inline-block;" CssClass="form-control" placeholder="0" required="true"></asp:TextBox>
                                    <p></p>
                                    <asp:Image runat="server" ID="NavigationImage" BorderStyle="Dashed" BorderColor="#CCCCCC" BorderWidth="1px" />
                                    <p></p>
                                    <asp:FileUpload runat="server" ID="NavigationImageFileUpload" Style="display: inline-block;" />
                                    <asp:Button runat="server" ID="NavigationImageFileUploadButton" Text="Upload" Width="100px" CssClass="btn btn-warning" />
                                </div>
                            </div>
                            <p>&nbsp;</p>
                            <div>
                                <div class="col-sm-3 text-right">Navigation Image (hover)</div>
                                <div class="col-sm-9">
                                    Width (px): &nbsp;<asp:TextBox runat="server" ID="NavigationHoverImageWidth" Width="65px" Text="160" CssClass="form-control" placeholder="0" required="true" Style="display: inline-block;"></asp:TextBox>
                                    &nbsp; - &nbsp; Height (px): &nbsp;<asp:TextBox runat="server" ID="NavigationHoverImageHeight" Width="65px" Text="90" CssClass="form-control" placeholder="0" required="true" Style="display: inline-block;"></asp:TextBox>
                                    <p></p>
                                    <asp:Image runat="server" ID="NavigationHoverImage" BorderStyle="Dashed" BorderColor="#CCCCCC" BorderWidth="1px" />
                                    <p></p>
                                    <asp:FileUpload runat="server" ID="NavigationHoverImageFileUpload" Style="display: inline-block;" />
                                    <asp:Button runat="server" ID="NavigationHoverImageFileUploadButton" Text="Upload" Width="100px" CssClass="btn btn-warning" />
                                </div>
                            </div>
                            <p>&nbsp;</p>
                        </div>
                    </div>
                </asp:View>
                <asp:View runat="server" ID="HeaderView">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Page Header</h3>
                        </div>
                        <div class="panel-body">
                            <div>
                                <div class="col-sm-2 text-right">Header background image</div>
                                <div class="col-sm-10">
                                    Width (px): &nbsp;<asp:TextBox runat="server" ID="HeaderBgImageWidth" Width="65px" Text="160" CssClass="form-control" placeholder="0" required="true" Style="display: inline-block;"></asp:TextBox>
                                    &nbsp; - &nbsp; Height (px): &nbsp;<asp:TextBox runat="server" ID="HeaderBgImageHeight" Width="65px" Text="90" CssClass="form-control" placeholder="0" required="true" Style="display: inline-block;"></asp:TextBox>
                                    <br />
                                    <asp:Image runat="server" ID="HeaderBgImage" BorderStyle="Dashed" BorderColor="#CCCCCC" BorderWidth="1px" />
                                    <br />
                                    <asp:FileUpload runat="server" ID="HeaderBgImageFileUpload" Style="display: inline-block;" />
                                    <asp:Button runat="server" ID="HeaderBgImageFileUploadButton" Text="Upload" Width="100px" CssClass="btn btn-warning" />
                                </div>
                            </div>
                            <p>&nbsp;</p>
                        </div>
                    </div>
                </asp:View>
                <asp:View runat="server" ID="FooterView">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Page Footer</h3>
                        </div>
                        <div class="panel-body">
                            <div>
                                <div class="col-sm-2 text-right">Footer background image</div>
                                <div class="col-sm-10">
                                    Width (px): &nbsp;<asp:TextBox runat="server" ID="FooterbgImageWidth" Width="65px" Text="160" CssClass="form-control" placeholder="0" required="true" Style="display: inline-block;"></asp:TextBox>
                                    &nbsp; - &nbsp; Height (px): &nbsp;<asp:TextBox runat="server" ID="FooterbgImageHeight" Width="65px" Text="90" CssClass="form-control" placeholder="0" required="true" Style="display: inline-block;"></asp:TextBox>
                                    <br />
                                    <asp:Image runat="server" ID="FooterbgImage" BorderStyle="Dashed" BorderColor="#CCCCCC" BorderWidth="1px" />
                                    <br />
                                    <asp:FileUpload runat="server" ID="FooterbgImageFileUpload" Style="display: inline-block;" />
                                    <asp:Button runat="server" ID="FooterbgImageFileUploadButton" Text="Upload" Width="90px" CssClass="btn btn-warning" />
                                </div>
                            </div>
                            <p>&nbsp;</p>
                        </div>
                    </div>
                    <%--<fckeditorv2:fckeditor id="FooterText" runat="server" ToolbarSet="TheBeerHouse" Height="400px" Width="100%" />--%>
                </asp:View>
                <asp:View runat="server" ID="HomePageView">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Home Page</h3>
                        </div>
                        <div class="panel-body">
                            <div>
                                <div class="col-sm-2 text-right">HomePage background image</div>
                                <div class="col-sm-10">
                                    Width (px): &nbsp;<asp:TextBox runat="server" ID="HomePageBgImageWidth" Width="65px" Text="160" CssClass="form-control" placeholder="0" required="true" Style="display: inline-block;"></asp:TextBox>
                                    &nbsp; - &nbsp; Height (px): &nbsp;<asp:TextBox runat="server" ID="HomePageBgImageHeight" Width="65px" Text="90" CssClass="form-control" placeholder="0" required="true" Style="display: inline-block;"></asp:TextBox>
                                    <br />
                                    <asp:Image runat="server" ID="HomePageBgImage" BorderStyle="Dashed" BorderColor="#CCCCCC" BorderWidth="1px" />
                                    <br />
                                    <asp:FileUpload runat="server" ID="HomePageBgImageFileUpload" Style="display: inline-block;" />
                                    <asp:Button runat="server" ID="HomePageBgImageFileUploadButton" Text="Upload" Width="100px" CssClass="btn btn-warning" />
                                </div>
                            </div>
                            <p>&nbsp;</p>
                        </div>
                    </div>
                </asp:View>
                <asp:View runat="server" ID="MenuCategoryView">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Menu Category Settings</h3>
                        </div>
                        <div class="panel-body">
                            <div>
                                <div class="col-sm-3 text-right">Background Image Width (px)<span style="color: red;">*</span></div>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="MenuCategoryWidth" CssClass="form-control" placeholder="0" required="true"></asp:TextBox>
                                </div>
                            </div>
                            <p style="line-height: 5px;">&nbsp;</p>
                            <div>
                                <div class="col-sm-3 text-right">Background Image Height (px)<span style="color: red;">*</span></div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="MenuCategoryHeight" runat="server" CssClass="form-control" placeholder="0" required="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Other background images</h3>
                        </div>
                        <div class="panel-body">
                            <div>
                                <div class="col-sm-3 text-right">Edit Order</div>
                                <div class="col-sm-9">
                                    <asp:Image runat="server" ID="EditOrderImage" BorderStyle="Dashed" BorderColor="#CCCCCC" BorderWidth="1px" />
                                    <br />
                                    <asp:FileUpload runat="server" ID="EditOrderImageFileUpload" Style="display: inline-block;" />
                                    <asp:Button runat="server" ID="EditOrderImageUploadButton" Text="Upload" Width="100px" CssClass="btn btn-warning" />
                                </div>
                            </div>
                            <p style="line-height: 5px;">&nbsp;</p>
                            <div>
                                <div class="col-sm-3 text-right">Confirm Order</div>
                                <div class="col-sm-9">
                                    <asp:Image runat="server" ID="ConfirmOrderImage" BorderStyle="Dashed" BorderColor="#CCCCCC"
                                        BorderWidth="1px" />
                                    <br />
                                    <asp:FileUpload runat="server" ID="ConfirmOrderFileUpload" Style="display: inline-block;" />
                                    <asp:Button runat="server" ID="ConfirmOrderUploadButton" Text="Upload" Width="100px" CssClass="btn btn-warning" />
                                </div>
                            </div>
                            <p style="line-height: 5px;">&nbsp;</p>
                            <div>
                                <div class="col-sm-3 text-right">Check Out</div>
                                <div class="col-sm-9">
                                    <asp:Image runat="server" ID="CheckOutImage" BorderStyle="Dashed" BorderColor="#CCCCCC"
                                        BorderWidth="1px" />
                                    <br />
                                    <asp:FileUpload runat="server" ID="CheckOutFileUpload" Style="display: inline-block;" />
                                    <asp:Button runat="server" ID="CheckOutButton" Text="Upload" Width="100px" CssClass="btn btn-warning" />
                                </div>
                            </div>
                            <p style="line-height: 5px;">&nbsp;</p>
                            <div>
                                <div class="col-sm-3 text-right">Add to Cart</div>
                                <div class="col-sm-9">
                                    <asp:Image runat="server" ID="AddToCartImage" BorderStyle="Dashed" BorderColor="#CCCCCC"
                                        BorderWidth="1px" />
                                    <br />
                                    <asp:FileUpload runat="server" ID="AddToCartFileUpload" Style="display: inline-block;" />
                                    <asp:Button runat="server" ID="AddToCartButton" Text="Upload" Width="100px" CssClass="btn btn-warning" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Theme colors</h3>
                        </div>
                        <div class="panel-body">
                            <div>
                                <div class="col-sm-3 text-right">Base Colour</div>
                                <div class="col-sm-9">
                                    <div style="display: block;" class="colours-picker">
                                        <asp:Label runat="server" ID="BaseColorSampleLabel" Width="127px" Height="25px" Text=""></asp:Label>
                                        <asp:TextBox runat="server" ID="BaseColorTextBox" Width="120px" Enabled="True"></asp:TextBox>&nbsp;
                                    <div style="display: inline;">
                                        <asp:Image runat="server" src="../Images/color-picker.png" alt="color-picker" ID="BaseColorPopupButton" />
                                    </div>
                                        <ajaxToolkit:ColorPickerExtender runat="server" ID="BaseColorPickerExtender" TargetControlID="BaseColorTextBox"
                                            SampleControlID="BaseColorSampleLabel" PopupButtonID="BaseColorPopupButton" />
                                    </div>
                                </div>
                            </div>
                            <p style="line-height: 5px;">&nbsp;</p>
                            <div>
                                <div class="col-sm-3 text-right">Background Colour</div>
                                <div class="col-sm-9">
                                    <div style="display: block;" class="colours-picker">
                                        <asp:Label runat="server" ID="BackColorSampleLabel" Width="127px" Height="25px" Text=""></asp:Label>
                                        <asp:TextBox runat="server" ID="BackColorTextBox" Width="120px" Enabled="True"></asp:TextBox>&nbsp;
                                    <div style="display: inline;">
                                        <asp:Image runat="server" src="../Images/color-picker.png" alt="color-picker" ID="BackColorPopupButton" />
                                    </div>
                                        <ajaxToolkit:ColorPickerExtender runat="server" ID="BackColorPickerExtender" TargetControlID="BackColorTextBox"
                                            SampleControlID="BackColorSampleLabel" PopupButtonID="BackColorPopupButton" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </article>
    </article>
    <p>
        &nbsp;
    </p>
    <hr />
    <div style="text-align: center;">
        <asp:Button runat="server" ID="SaveThemesSettings" Text="Save Changes" CssClass="btn btn-lg btn-danger" />
    </div>
</asp:Content>
