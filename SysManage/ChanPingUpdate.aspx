<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChanPingUpdate.aspx.cs" Inherits="SysManage_ChanPingUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改项目</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="Body_Title">
                报价管理 》》价格修改
            </div>
            <table align="center" class="Admin_Table">

                <tr>
                    <td>业务类别编号：</td>
                    <td>
                        <asp:TextBox ID="TextBox11" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>业务名称：</td>
                    <td>


                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>


                    </td>
                </tr>


                <tr>
                    <td>产品类别编号：</td>
                    <td>

                        <asp:TextBox ID="TextBox10" runat="server" Enabled="false"></asp:TextBox>

                    </td>
                    <td>&nbsp;产品名称：</td>
                    <td>

                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>项目编号：</td>
                    <td>

                        <asp:TextBox ID="TextBox2" runat="server" Enabled="false"></asp:TextBox>

                    </td>
                    <td>&nbsp;项目名称：</td>
                    <td>

                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>

                    </td>
                </tr>


                <tr>
                    <td>样品：  </td>
                    <td>

                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>

                    </td>
                    <td>标准： </td>
                    <td>

                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>

                    </td>
                </tr>


                <tr>
                    <td>费用：</td>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>(数字类型)
                    </td>
                    <td>单位：</td>
                    <td>

                        <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>周期：</td>
                    <td>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </td>
                    <td>备注： </td>
                    <td>

                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>

                    </td>
                </tr>

                <tr valign="middle" height="60px">
                    <td align="center" colspan="4">&nbsp;<asp:Button ID="Button3" CssClass="BnCss"
                        runat="server" Text="修改" Width="53px" OnClick="Button3_Click" />

                        <asp:Button ID="Button4" runat="server" Text="删除" OnClick="Button4_Click" Visible="False" />
                        <asp:Button ID="Button5" runat="server" Text="新增" OnClick="Button5_Click" Visible="False" />

                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
