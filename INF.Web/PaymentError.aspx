<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" Inherits="INF.Web.PaymentError" Codebehind="PaymentError.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadExtra" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
   
     <section class="cstmsection">
          <h3><i class="fa fa-credit-card"></i>Payment Error</h3>

        <div class="col-md-12 col-xs-12 text-center">
            <p>&nbsp;</p>
           
            <img src=" App_Themes/default/images/payment_card_fail.png" width="256px" alt="payment by card fail"/>
            <h3 style="color: red;">Opps! The payment is not successfull. Please contact the payment service provider for details.</h3>
            <p>The Form transaction did not completed successfully and the customer has been returned
            to this completion page for the following reason:</p>
            <br/>
             <p>
                <asp:Literal runat="server" ID="ltrDetailErrors"></asp:Literal>    
            </p>
            <strong style="color: #343434;"><a href="Default.aspx">Click here</a> to back Home Page.</strong>
        </div>
    </section>

</asp:Content>
