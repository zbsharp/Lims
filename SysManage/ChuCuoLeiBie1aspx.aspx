<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChuCuoLeiBie1aspx.aspx.cs" Inherits="SysManage_ChuCuoLeiBie1aspx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改错误类别</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 100px;">
                                错误大类：</td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                            <td colspan="1" style="width: 100px; text-align: left">
                                错误内容：</td>
                            <td colspan="1" style="text-align: left">
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                        </tr>
        <tr bgcolor="#f4faff" style ="display :none ;">
            <td style="width: 100px; text-align: left">
                电话：</td>
            <td colspan="3" style="text-align: left">
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
            <td colspan="1" style="width: 100px; text-align: left">
                传真：</td>
            <td colspan="1" style="text-align: left">
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td style="width: 100px; text-align: left">
                备注：</td>
            <td colspan="5" style="text-align: left">
                <asp:TextBox ID="TextBox5" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td colspan="6" style="text-align: center">
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                                    Text="修改错误内容" OnClick="Button1_Click" />
                                <asp:Button ID="Button2" runat="server" Text="修改错误大类" onclick="Button2_Click" />
                                <asp:Button ID="Button3" runat="server" Text="删除错误内容" onclick="Button3_Click" />
                               
            </td>
        </tr></table> 
    </div>
    </form>
</body>
</html>
