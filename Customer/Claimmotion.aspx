<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Claimmotion.aspx.cs" Inherits="Customer_Claimmotion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <title>认领作业</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            客户管理》》到账认领
        </div>
        <table class="Admin_Table">
            <tr>
                <td>付款客户：</td>
                <td>
                    <asp:TextBox ID="tx_fukuan" runat="server" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox></td>
                <td>付款日期：</td>
                <td>
                    <asp:TextBox ID="tx_fukuandate" runat="server" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox></td>
            </tr>
            <tr>
                <td>付款金额：</td>
                <td>
                    <asp:TextBox ID="tx_momey" runat="server" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox></td>
                <td>未分金额：</td>
                <td>
                    <asp:TextBox ID="tx_curren" runat="server" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox></td>
            </tr>
            <tr>
                <td>已分金额：</td>
                <td>
                    <asp:TextBox ID="tx_way" runat="server" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox></td>
                <td>其中已经确认金额：</td>
                <td>
                    <asp:TextBox ID="tx_name" runat="server" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">客户名称或任务号或报价单号或付款单位或委托单位：
                    <asp:TextBox ID="tx_remark" placeholder="搜索查询框" runat="server" Width="300px"></asp:TextBox>
                    <asp:Button ID="btn_serch" runat="server" Text="查询" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" Width="100%" DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="liushuihao" HeaderText="流水号" />
                            <asp:BoundField DataField="remark" HeaderText="到账信息" />
                            <asp:BoundField DataField="fillname" HeaderText="认领人" />
                            <asp:BoundField DataField="filltime" HeaderText="认领时间" />
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title" />
                    </asp:GridView>
                </td>
            </tr>
        </table>

        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <asp:Button ID="Button1" runat="server" Text="提 交" OnClick="Button1_Click" />

    </form>
</body>
</html>
