<%@ Page Language="C#" AutoEventWireup="true" CodeFile="updatefeiyong.aspx.cs" Inherits="Case_updatefeiyong" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        应收款：<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

        修改为：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

        <asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click" />

    </div>
    </form>
</body>
</html>
