<%@ Page Title="500 Error" Language="C#" AutoEventWireup="true" CodeBehind="GeneralError.aspx.cs" Inherits="BioPRO.ErrorPages.GeneralError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <%--Bootstrap CSS File--%>
    <link rel="stylesheet" href="~/CSS/bootstrap.min.css" />
    <link rel="stylesheet" href="~/CSS/bootstrap-reset.css" />
    <%--External CSS File --%>
    <link rel="stylesheet" href="~/Assets/font-awesome/css/font-awesome.css" />

    <%--Custom CSS styles for this Web Application--%>
    <link rel="stylesheet" href="~/CSS/style.css" />
    <link rel="stylesheet" href="~/CSS/style-responsive.css" />
</head>
<body class="body-500">
    <form id="form1" runat="server">
        <div class="container">

            <section class="error-wrapper">
                <i class="icon-500"></i>
                <h1>Ouch!</h1>
                <h2>500 Page Error</h2>
                <p class="page-500">Looks like Something went wrong. Pardon us for this unexpected things you see.</p>
                <p class="page-500">The Website Administrator has been notified. <asp:HyperLink runat="server" ID="hlError" NavigateUrl="~/Default.aspx" Text="Return Home"></asp:HyperLink></p>
            </section>

        </div>
    </form>
</body>
</html>
