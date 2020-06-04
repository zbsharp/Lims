<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Exchange.aspx.cs" Inherits="Income_Exchange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <title>汇率维护</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            财务管理 》》汇率维护
        </div>
        <table class="Admin_Table" style="width: 100%;">
            <tr>
                <td>币种：</td>
                <td>
                    <asp:DropDownList ID="drop_currency" runat="server">
                        <asp:ListItem Selected="True">美元</asp:ListItem>
                    </asp:DropDownList></td>
                <td>汇率：</td>
                <td>
                    <asp:TextBox ID="txt_exchange" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">适用年：<asp:DropDownList ID="drop_year" runat="server"></asp:DropDownList>
                    &nbsp;
                        &nbsp;
                        &nbsp;
                        &nbsp;
                        &nbsp;
                        &nbsp;
                        适用月：<asp:DropDownList ID="drop_month" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">备注：<asp:TextBox ID="txt_remork" runat="server" Width="344px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">说明：汇率是指外币对人民币的汇率，如1美元=6元人民币，则汇率为6</td>
            </tr>
        </table>
        <div style="text-align: center;">
            <asp:Button ID="Button1" runat="server" Text="保存" OnClick="Button1_Click" />
        </div>
        <table class="Admin_Table" style="width: 100%;">
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" Width="100%" DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="currency" HeaderText="币种" />
                            <asp:BoundField DataField="huilv" HeaderText="汇率" />
                            <asp:BoundField DataField="fillname" HeaderText="录入人" />
                            <asp:BoundField DataField="filltime" HeaderText="录入时间" />
                            <asp:BoundField DataField="year" HeaderText="适用年" />
                            <asp:BoundField DataField="month" HeaderText="适用月" />
                            <asp:BoundField DataField="Remork" HeaderText="备注" />
                            <asp:CommandField HeaderText="操作" ShowDeleteButton="True" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
