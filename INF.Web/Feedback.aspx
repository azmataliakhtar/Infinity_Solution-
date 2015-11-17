<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" Inherits="INF.Web.Feedback" CodeBehind="Feedback.aspx.vb" %>

<%@ Import Namespace="INF.Web.UI.Settings" %>

<asp:Content ID="GoogleContent" ContentPlaceHolderID="GooglePlaceHolder" runat="Server">
        
      <%
          If (ConfigurationManager.AppSettings("google-site-verification") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-plus-link")))) Then
        %>                                
             <meta name="google-site-verification" content="<%=ConfigurationManager.AppSettings("google-site-verification")%>"/>
        <%
        End If
    %>    
    
     <%
         If (ConfigurationManager.AppSettings("google-plus-link") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-plus-link")))) Then
        %>                                
             <link href="<%=ConfigurationManager.AppSettings("google-plus-link")%>" rel="publisher"/>
        <%
        End If
    %>   
   
     <%
         If (ConfigurationManager.AppSettings("geo-region") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-region")))) Then
        %>                                
             <meta name="geo.region" content="<%=ConfigurationManager.AppSettings("geo-region")%>" />
        <%
        End If
    %> 

    <%
        If (ConfigurationManager.AppSettings("geo-placename") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-placename")))) Then
        %>                                
             <meta name="geo.placename" content="<%=ConfigurationManager.AppSettings("geo-placename")%>" />
        <%
        End If
    %> 


     <%
         If (ConfigurationManager.AppSettings("geo-position") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-position")))) Then
        %>                                
             <meta name="geo.position" content="<%=ConfigurationManager.AppSettings("geo-position")%>" />
        <%
        End If
    %> 

     <%
         If (ConfigurationManager.AppSettings("google-icbm") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-icbm")))) Then
        %>                                
             <meta name="ICBM" content="<%=ConfigurationManager.AppSettings("google-icbm")%>" />
        <%
        End If
    %> 

</asp:Content>

<asp:Content runat="server" ID="PageTitleContent" ContentPlaceHolderID="PageTitle">
     <%
         If (ConfigurationManager.AppSettings("titleFeedback") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleFeedback")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleFeedback")%>
        <%
        Else
            %> 
                <%= EPATheme.Current.Themes.WebsiteName%>  
            <%
        End If
    %>   
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadExtra" runat="Server">
</asp:Content>
<asp:Content ID="MainContentPlaceHolderContent" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="col-md-12 col-xs-12" style="display:none !important">
        <asp:MultiView ID="mltFeedback" runat="server">
            <asp:View ID="viewFeedback" runat="server">
                <div>
                    <span>
                        <h3 class="page-header page-title">Feedback</h3>
                    </span>
                </div>
                <div class="col-md-12 col-xs-12">
                    <h4>We listen to your concerns!</h4>
                    <p>Please give us your feedback</p>
                    <p>If you’ve got anything, be it criticism or positive feedback, please let us know using the form below. The more you care, the better we serve!</p>
                    <br>
                    <br>
                    <div class="col-md-8 col-xs-8">
                        <div class="form-group row">
                            <div class="col-md-4 col-xs-4">Food Quality</div>
                            <div class="col-md-8 col-xs-8">
                                <ajaxToolkit:Rating ID="ratingFoodQuality" runat="server" CurrentRating="2" MaxRating="5" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-4 col-xs-4">Order Processing Time</div>
                            <div class="col-md-8 col-xs-8">
                                <ajaxToolkit:Rating ID="ratingProcessingTime" runat="server" CurrentRating="2" MaxRating="5" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-4 col-xs-4">Delivery Time</div>
                            <div class="col-md-8 col-xs-8">
                                <ajaxToolkit:Rating ID="ratingDeliveryTime" runat="server" CurrentRating="2" MaxRating="5" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12 col-xs-12">
                                <p>Comments:</p>
                                <textarea id="txtComments" runat="server" class="form-control"></textarea>
                                <div class="clear"></div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12 col-xs-12">
                                <asp:Button ID="btnSubmit" runat="server" Text="Send Feedback" OnClick="SubmitFeedback" ValidationGroup="Join" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
    </section>
</asp:Content>

