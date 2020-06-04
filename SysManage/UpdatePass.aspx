<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePass.aspx.cs" Inherits="SysManage_UpdatePass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="div_All">
    <div class="Body_Title">用户管理 》》修改用户密码</div>
        <hr />
     <table cellpadding="2" cellspacing="1" class="Admin_Table" style="width:90%; margin:5px auto;">
     <thead>
        <tr class="Admin_Table_Title">
            <th colspan="3">修改用户密码</th>
            <th>&nbsp;</th>
        </tr>
     </thead>
        <tr>
            <td>原始密码：</td>
            <td>
                <asp:TextBox ID="txtOldPass" runat="server" CssClass="txtHInput" 
                    MaxLength="100" TextMode="Password"></asp:TextBox>&nbsp;<img src="../Images/warning.png"/>必填</td>
            <td>原始登录密码。</td>
            <td>
                <asp:TextBox ID="txtOldPass0" runat="server" CssClass="txtHInput" 
                    MaxLength="100" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td>新该密码：</td>
            <td>
                <asp:TextBox ID="txtPassWord" runat="server" CssClass="txtHInput" 
                    MaxLength="100" TextMode="Password"></asp:TextBox>&nbsp;<img src="../Images/warning.png"/>必填</td>
            <td>新改的登录密码，不能为空！</td>
            <td>
                <asp:TextBox ID="txtOldPass1" runat="server" CssClass="txtHInput" 
                    MaxLength="100" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td>确认密码：</td>
            <td>
            <asp:TextBox ID="txtCheckPass" runat="server" CssClass="txtHInput" MaxLength="100" TextMode="Password"></asp:TextBox>&nbsp;<img src="../Images/warning.png"/>必填</td>
            <td>确认登录密码，不能为空！</td>
            <td>
                <asp:TextBox ID="txtOldPass2" runat="server" CssClass="txtHInput" 
                    MaxLength="100" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="BtnUpdateUserPass" runat="server" Text="保存信息" 
                    onclick="BtnSaveUserInfo_Click" />&nbsp;
                <input id="txtRe" type="reset" value=" 重填 " />
                &nbsp;</td>
            <td></td>
            <td>&nbsp;</td>
        </tr>
     </table>
    </div>
    </form>
</body>
</html>

