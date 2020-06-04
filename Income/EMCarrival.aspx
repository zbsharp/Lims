<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EMCarrival.aspx.cs" Inherits="Income_EMCarrival" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            财务管理 》》现场测试到账
        </div>
        <asp:TextBox ID="txtshwere" runat="server" Width="259px"></asp:TextBox>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>龙华</asp:ListItem>
            <asp:ListItem>福永</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnselect" runat="server" Text="模糊查询" OnClick="btnselect_Click" />
        <br />
        <asp:GridView ID="GridView1" runat="server" CssClass="Admin_Table" Width="100%" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" DataKeyNames="id">
            <Columns>
                <asp:TemplateField HeaderText="序 号">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1) %>'
                            ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                <asp:BoundField DataField="region" HeaderText="地区" ReadOnly="true" />
                <asp:BoundField DataField="EMCnumber" HeaderText="测试单号" ReadOnly="true" />
                <asp:BoundField DataField="EMCid" HeaderText="测试单号" ReadOnly="true" />
                <asp:BoundField DataField="shijihour" HeaderText="实际测试时长" ReadOnly="true" />
                <asp:BoundField DataField="shijisumprice" HeaderText="实际总费用" ReadOnly="true" />
                <asp:BoundField DataField="responser" HeaderText="预约人" ReadOnly="true" />
                <asp:BoundField DataField="customername" HeaderText="客户名称" ReadOnly="true" />
                <asp:BoundField DataField="linkman" HeaderText="客户联系人" ReadOnly="true" />
                <asp:TemplateField HeaderText="是否到账">
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="funtion">
                            <asp:ListItem>是</asp:ListItem>
                            <asp:ListItem>否</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("isreceive") %>' />--%>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("isreceive") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="money" HeaderText="到账金额" />
                <asp:CommandField ShowEditButton="True" />
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title" />
        </asp:GridView>
    </form>
</body>
</html>
