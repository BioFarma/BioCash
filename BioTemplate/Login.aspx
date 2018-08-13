<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UserManagement.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Login | User Management</title>

    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="CSS/font-awesome/css/font-awesome.css" rel="stylesheet" />

    <link href="CSS/animate.css" rel="stylesheet" />
    <link href="CSS/style.css" rel="stylesheet" />
    <link rel="shortcut icon" href="../Images/Resources/ca-bio-logo.ico"/>
</head>

<body class="gray-bg">
    <form id="formLogin" runat="server">
        <div class="middle-box text-center loginscreen animated fadeInDown">
            <div>
                <div>

                    <%--<h1 class="logo-name">CORS</h1>--%>
                    <img class="col-lg-12 animated fadeInDown" src="Images/Login/ca-bio-logo.png"/>
                    <br />
                </div>
                <h3>Selamat Datang di User Management Application</h3>
                <p>
                    Aplikasi untuk mengatur hak akses aplikasi di PT Bio Farma
                </p>
                <p>Silakan log in untuk menggunakan aplikasi</p>
                    <div class="form-group">
                        <asp:TextBox id="tbUsername" CssClass="form-control" placeholder="Username" runat="server" required=""/>
                    </div>
                    <div class="form-group"> 
                        <asp:TextBox id="tbPassword" CssClass="form-control" type="password" placeholder="Password" runat="server" required="" />
                    </div>
                    <asp:Button id="btnLogin" CssClass="btn btn-primary block full-width m-b" Text="Login" type="submit" runat="server" OnClick="btnLogin_Click"/>

                    <a href="#"><small>Lupa password?</small></a>
                <p class="m-t"><small><b>Copyright</b> &copy; 2016 PT Bio Farma (Persero)</small> </p>
            </div>
        </div>

    </form>
</body>

</html>