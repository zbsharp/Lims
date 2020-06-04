<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAdd.aspx.cs" Inherits="SysManage_UserAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <style type="text/css">
        .BnCss {
            height: 21px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            系统管理 》》增加用户
        </div>
        <div style="font-size: 9pt">

            <table align="center" class="Admin_Table">


                <tr valign="middle" height="60px">
                    <td style="height: 8px" align="left" valign="bottom">用户账号：</td>
                    <td style="height: 8px; width: 152px;" align="left" valign="bottom">
                        <asp:TextBox ID="name" runat="server" class="TxCss" Width="127px"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="name"
                            ErrorMessage="请输入用户名称" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    <td style="width: 75px; height: 8px;" align="left" valign="bottom">职务： </td>
                    <td style="height: 8px" align="left" valign="bottom">
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="127px">
                        </asp:DropDownList><br />
                    </td>
                </tr>
                <tr valign="middle" height="60px">
                    <td style="height: 7px" align="left" valign="bottom">密码：</td>
                    <td style="height: 7px; width: 152px;" align="left" valign="bottom">
                        <asp:TextBox ID="TextBox1" Width="127px" class="TxCss" runat="server" TextMode="Password"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                            ErrorMessage="请输入密码" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    <td style="height: 7px" align="left" valign="bottom">重复密码：</td>
                    <td style="height: 7px" align="left" valign="bottom">
                        <asp:TextBox ID="nTextBox" runat="server" class="TxCss" TextMode="Password" Width="127px"></asp:TextBox><br />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox1"
                            ControlToValidate="nTextBox" ErrorMessage="您两次输入密码不一致" Display="Dynamic"></asp:CompareValidator></td>
                </tr>


                <tr valign="middle" height="60px">
                    <td style="width: 79px; height: 12px;" align="left" valign="bottom">邮箱：</td>
                    <td style="width: 152px; height: 12px;" align="left" valign="bottom">
                        <asp:TextBox ID="email" runat="server" Width="127px" class="TxCss"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="请输入正确的邮箱格式"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="email" Width="149px" Display="Dynamic"></asp:RegularExpressionValidator></td>
                    <td style="width: 75px; height: 12px;" align="left" valign="bottom">所属部门：</td>
                    <td style="height: 12px" align="left" valign="bottom">
                        <asp:DropDownList ID="Branch" runat="server" Width="127px">
                        </asp:DropDownList><br />
                    </td>
                </tr>
                <tr valign="middle" height="60px">
                    <td style="height: 6px" align="left" valign="bottom">办公电话：</td>
                    <td style="height: 6px; width: 152px;" align="left" valign="bottom">
                        <asp:TextBox ID="workPhone" class="TxCss" Width="127px" runat="server"></asp:TextBox></td>
                    <td style="height: 6px" align="left" valign="bottom">移动电话：</td>
                    <td style="height: 6px" align="left" valign="bottom">
                        <asp:TextBox ID="movePhone" class="TxCss" Width="127px" runat="server"></asp:TextBox></td>
                </tr>


                <tr valign="middle" height="60px">
                    <td style="height: 6px" align="left" valign="bottom">传真：</td>
                    <td style="height: 6px; width: 152px;" align="left" valign="bottom">
                        <asp:TextBox ID="TextBox2" runat="server" Width="124px"></asp:TextBox>
                    </td>
                    <td style="height: 6px" align="left" valign="bottom">内部短号：</td>
                    <td style="height: 6px" align="left" valign="bottom">
                        <asp:TextBox ID="TextBox3" runat="server" Width="128px"></asp:TextBox>
                    </td>
                </tr>


                <tr valign="middle" height="60px">
                    <td style="height: 6px" align="left" valign="bottom">归属地：</td>
                    <td style="height: 6px; width: 152px;" align="left" valign="bottom">
                        <asp:DropDownList ID="drop_home" runat="server">
                            <asp:ListItem Value="FY">福永</asp:ListItem>
                            <asp:ListItem Value="LH">龙华</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr valign="middle" height="60px">
                    <td align="center" colspan="4">&nbsp;<asp:Button ID="Button3" CssClass="BnCss" runat="server" Text="增 加" Width="53px" OnClick="Button3_Click" />
                        &nbsp;&nbsp;  <a href="#" onclick="history.go(-1);">返回</a>&nbsp;
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
