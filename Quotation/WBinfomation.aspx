<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WBinfomation.aspx.cs" Inherits="Quotation_WBinfomation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="Body_Title">
                外包管理 》》外包机构信息
            </div>
            <table align="center" class="Admin_Table">
                <tr>
                    <td>外包类型：</td>
                    <td>
                        <asp:TextBox ID="txt_type" runat="server"></asp:TextBox>
                    </td>
                    <td>备注：</td>
                    <td>
                        <asp:TextBox ID="txt_beizhu" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr valign="middle" height="60px">
                    <td align="center" colspan="4">
                        <asp:Button ID="Button5" runat="server" Text="保存" OnClick="Button5_Click" Width="141px" />
                    </td>
                </tr>
            </table>
            <table align="center" class="Admin_Table">
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="Admin_Table" DataKeyNames="id" OnRowDeleted="GridView1_RowDeleted" OnRowDeleting="GridView1_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="id" />
                                <asp:BoundField DataField="product2_id" HeaderText="项目编号" />
                                <asp:BoundField DataField="WBtype" HeaderText="外包机构" />
                                <asp:BoundField DataField="Beizhu" HeaderText="备注" />
                                <asp:CommandField HeaderText="操作" ShowDeleteButton="True" />
                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
