<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fanjiesuan.aspx.cs" Inherits="Income_Fanjiesuan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>反结算</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            财务管理》》业务已认
        </div>
        <p>
            业务认领批次号：<asp:DropDownList ID="drop_bacth" runat="server"></asp:DropDownList>
            &nbsp; &nbsp; &nbsp; 
            <asp:Button ID="btnfanjiesuan" runat="server" Text="反结算" OnClick="btnfanjiesuan_Click" />
        </p>
        <asp:GridView ID="GridView1" runat="server" CssClass="Admin_Table" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="batch" HeaderText="批次号" />
                <asp:BoundField DataField="fillname" HeaderText="认领人" />
                <asp:BoundField DataField="filltime" HeaderText="认领时间" />
                <asp:BoundField DataField="money" HeaderText="认领金额" />
                <asp:BoundField DataField="affirmren" HeaderText="确认人" />
                <asp:BoundField DataField="affirmtime" HeaderText="确认时间" />
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title"/>
        </asp:GridView>
    </form>
</body>
</html>
