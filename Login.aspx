<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>倍测检测</title>
    <link rel="icon" href="Images/login/favicon.ico" type="image/x-icon" />
    <link href="Web_CSS/Login.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login_form">
            <div class="form">
                <div class="login_con">
                    <span class="welcome"></span>
                    <br />
                    <img src="Images/login/user.png" class="user" />
                    <asp:TextBox ID="userName" runat="server" placeholder="请输入用户名" class="username same"></asp:TextBox>
                    <br />
                    <img src="Images/login/password.png" class="pass" />
                    <asp:TextBox ID="userPwd" placeholder="请输入密码" class="password same" runat="server" TextMode="Password"></asp:TextBox><br />
                    <span id="span_user">
                        <asp:CheckBox ID="CheckBox1" runat="server" />记住用户名</span>
                    <div class="btn">
                        <asp:Button ID="Button1" runat="server" Text="登录" CssClass="denglu" OnClick="Button1_Click" />
                    </div>
                </div>
            </div>
        </div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <div id="footer">
            Copyright @2019 深圳市倍测检测有限公司 版权所有 Designed by
        </div>
    </form>
</body>
</html>
