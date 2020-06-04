<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update.aspx.cs" Inherits="Income_Update" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>修改到款记录</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            财务管理 》》修改自己录入的未结算的到款记录
        </div>

        <div>
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                width="100%">
                <tr bgcolor="#f4faff">
                    <td colspan="4" style="text-align: left">流水号：<asp:Label ID="Label3" runat="server"></asp:Label>
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="text-align: left; width: 90px;">付款人：</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 90px;">付款金额：</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="TextBox2" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="text-align: left; width: 90px;">付款日期：</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 90px;">付款方式</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="text-align: left; width: 90px;">付款备注：</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 90px;">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="text-align: center;" colspan="4">
                        <asp:Button ID="Button1" runat="server" Text="修改" OnClick="Button1_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
