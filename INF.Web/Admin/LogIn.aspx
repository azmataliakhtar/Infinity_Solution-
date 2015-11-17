<%@ Page Language="VB" AutoEventWireup="false" Inherits="INF.Web.Admin.Login" CodeBehind="LogIn.aspx.vb" %>

<%@ Import Namespace="INF.Web.UI.Settings" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Login</title>
    <%--<link type="text/css" rel="stylesheet" href="../Admin/Css/styles.css" />--%>
    <!-- Bootstrap -->
    <link type="text/css" rel="stylesheet" href="../Admin/css/bootstrap.css" />
    <link type="text/css" rel="stylesheet" href="../Admin/css/bootstrap-theme.css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body style="background-color: gainsboro;">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scriptManager1"></asp:ScriptManager>
        <section class="col-sm-12">
            <div class="col-sm-4"></div>
            <div class="col-sm-4">
                <div style="margin-top: 100px; padding: 30px 15px;">
                    <%-- <div class="input-group">
                        <span class="input-group-addon"><img src="../Images/user_login.png" alt="user login" height="48" width="48" style="margin: -15px 0 0 0;" /></span>
                        <h3 class="form-control page-header">Admin Panel - Log in</h3>
                    </div>--%>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <%--<img src="../Images/user_login.png" alt="user login" height="48" width="48" style="margin: -15px 0 0 0;" />--%>
                            <h3 class="panel-title">Admin Panel - Log in</h3>
                        </div>
                        <div class="panel-body">
                            <div>
                                <img src="../Images/logo_jpg.jpg" alt="Infinity Solution" class="thumbnail img-responsive" />
                            </div>
                            <div style="line-height: 10px;">&nbsp;</div>
                            <div class="input-group">
                                <span class="input-group-addon">User Name:</span>
                                <asp:TextBox runat="server" ID="UserName" CssClass="form-control" placeholder="your username"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="UserNameRequired" ControlToValidate="UserName" SetFocusOnError="True"
                                    ErrorMessage="[User Name] is required" EnableClientScript="True" Display="Dynamic" ValidationGroup="UserLogin"></asp:RequiredFieldValidator>
                            <div style="line-height: 10px;">&nbsp;</div>
                            <div class="input-group">
                                <span class="input-group-addon">Password:&nbsp;&nbsp;</span>
                                <asp:TextBox runat="server" ID="Password" CssClass="form-control" TextMode="Password" placeholder="*********"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="PasswordRequired" ControlToValidate="Password" SetFocusOnError="True"
                                    ErrorMessage="[Password] is required" EnableClientScript="True" Display="Dynamic" ValidationGroup="UserLogin"></asp:RequiredFieldValidator>
                            <div style="line-height: 10px;">&nbsp;</div>
                            <div style="text-align: center;">
                                <asp:Button runat="server" ID="LoginButton" Text="Login" CssClass="btn btn-danger" Style="padding-left: 25px; padding-right: 25px;" ValidationGroup="UserLogin" />
                                &nbsp;<a href="/Account/ForgotPassword.aspx">Forgot Your Password?</a>
                            </div>
                            <div style="line-height: 10px;">&nbsp;</div>
                            <div role="alert">
                                <asp:Label runat="server" ID="ErrorsLabel" ForeColor="#FF0000" Text="Your username or password is invalid. Please try again!"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div>
                        <%-- <div>
                            <div colspan="2">
                                
                            </div>
                        </div>
                        <div>
                            <div colspan="2">&nbsp;</div>
                        </div>--%>
                    </div>
                </div>
            </div>

            <div class="col-sm-4"></div>
        </section>
    </form>
</body>
</html>
