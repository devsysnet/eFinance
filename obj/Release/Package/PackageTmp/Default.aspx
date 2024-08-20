<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="eFinance.Default" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <title>Login e-Finance</title>
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0,minimal-ui">
    <link href="<%=Func.BaseUrl%>Assets/images/shortcut-icon.png" rel="shortcut icon" type="image/x-icon" />

    <link rel="apple-touch-icon" href="apple-touch-icon.html">
    <!-- core plugins -->
    <link rel="stylesheet" href="<%=Func.BaseUrl%>Assets/stylesheets/animate.min.css">
    <link rel="stylesheet" href="<%=Func.BaseUrl%>Assets/vendor/bower_components/font-awesome/css/font-awesome.min.css">
    <link rel="stylesheet" href="<%=Func.BaseUrl%>Assets/vendor/bower_components/material-design-iconic-font/dist/css/material-design-iconic-font.css">
    <link rel="stylesheet" href="<%=Func.BaseUrl%>Assets/stylesheets/bootstrap.css">
    <!-- core plugins -->
    <!-- styles for the current page -->
    <link rel="stylesheet" href="<%=Func.BaseUrl%>Assets/stylesheets/login.css">
    <!-- / styles for the current page -->
</head>

<body class="simple-page page-login">
   
    <div class="simple-page-wrap" style="margin-top:100px;">
        <div class="simple-page-content mb-4">
            <div class="simple-page-logo animated zoomIn">
                <a href="index.html">
                    <span>
                        <i class="fa fa-houzz"></i>
                    </span>
                    <span>SAK 4.0</span>
                </a>
            </div>
            <div class="simple-page-form animated flipInY" id="login-form">
                <h6 class="form-title mb-4 text-center">Sign In With Your SAK 4.0 Account</h6>
                <form runat="server" id="form">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    <div class="form-group">
                        <asp:TextBox runat="server" class="form-control" ID="txtUserID" placeholder="Username"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox runat="server" class="form-control" ID="txtPassword" type="password" placeholder="Password"></asp:TextBox>
                    </div>
                    <asp:Button runat="server" ID="btnSignIn" class="btn btn-primary" Text="Sign In" OnClick="btnSignIn_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"/>
                </form>
            </div>
        </div>
    </div>
    <script src="<%=Func.BaseUrl%>Assets/javascripts/jquery.min.js"></script>
    <script src="<%=Func.BaseUrl%>Assets/javascripts/tether.min.js"></script>
    <script src="<%=Func.BaseUrl%>Assets/javascripts/bootstrap.min.js"></script>
</body>

</html>

