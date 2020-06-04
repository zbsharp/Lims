<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Turncustomer.aspx.cs" Inherits="Customer_Turncustomer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户分派</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/login/favicon.ico" type="image/x-icon" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            销售管理 》》客户分派
        </div>
        <table class="Admin_Table">
            <tr>
                <td>客户编号：</td>
                <td>
                    <asp:Label ID="lb_id" runat="server" Text=""></asp:Label></td>
                <td>客户名称：</td>
                <td>
                    <asp:Label ID="lb_customername" runat="server" Text=""></asp:Label></td>
                <td>现归属人：
                </td>
                <td>
                    <asp:Label ID="lb_responser" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                </td>
                <td>历史录入人：</td>
                <td>
                    <asp:Label ID="lb_fillname" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>录入时间：</td>
                <td>
                    <asp:Label ID="lb_filltime" runat="server" Text="Label"></asp:Label></td>
                <td>客户来源：</td>
                <td>
                    <asp:Label ID="lb_souce" runat="server" Text="Label"></asp:Label></td>
                <td>是否有过报价：</td>
                <td>
                    <asp:Label ID="lb_baojia" runat="server" Text="Label"></asp:Label></td>
            </tr>

        </table>
        <fieldset>
            <legend style="color: red;">跟踪记录</legend>
            <asp:GridView ID="GridView1" runat="server" CssClass="Admin_Table" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="neirong" HeaderText="访谈内容" />
                    <asp:BoundField DataField="style" HeaderText="联系方式" />
                    <asp:BoundField DataField="result" HeaderText="访谈结果" />
                    <asp:BoundField DataField="zhongyao" HeaderText="联系人" />
                    <asp:BoundField DataField="responser" HeaderText="业务员" />
                </Columns>
                <HeaderStyle CssClass="Admin_Table_Title" />
            </asp:GridView>
        </fieldset>
        <table class="Admin_Table">
            <tr>
                <td>
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   分派原因： <asp:TextBox ID="TextBox1" runat="server" Width="698px" Height="25px" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        部门：
                    <asp:DropDownList ID="dw_department" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dw_department_SelectedIndexChanged"></asp:DropDownList>
                    &nbsp; &nbsp; &nbsp; 
                    业务员：
                    <asp:DropDownList ID="dw_salesman" runat="server"></asp:DropDownList>
                    &nbsp; &nbsp; &nbsp; 
                    <asp:Button ID="btn_ok" runat="server" Text="确定分派" OnClick="btn_ok_Click" />

                </td>
            </tr>
        </table>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
