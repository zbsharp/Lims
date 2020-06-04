<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TurnCustomerlog.aspx.cs" Inherits="Customer_TurnCustomerlog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户分派记录</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/login/favicon.ico" type="image/x-icon" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            销售管理 》》客户分派记录
        </div>
        <table class="Admin_Table" style="width: 100%;">
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="kehuid" HeaderText="客户编号" />
                            <asp:BoundField DataField="customername" HeaderText="客户名称" />
                            <asp:BoundField DataField="responser" HeaderText="现归属人" />
                            <asp:BoundField DataField="fillname" HeaderText="分派人" />
                            <asp:BoundField DataField="cause" HeaderText="分派原因" />
                            <asp:BoundField DataField="filltime" HeaderText="分派时间" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
