<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigManage.aspx.cs" Inherits="Customer_ConfigManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
</head>
<body>
    <form name="form1" runat="server" id="form1">
    <div class="Body_Title">
        销售管理 》》查看与自己相关的人员配置</div>
    <asp:DropDownList Visible="false" ID="response" runat="server" AutoPostBack="true"
        OnSelectedIndexChanged="response_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" Visible="false"
        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
    </asp:DropDownList>
    <asp:Button ID="Button1" runat="server" class="BnCss" Visible="false" Text="确定取消配置"
        OnClick="Button1_Click1" />
    <div style="height: 450px; overflow: scroll">
        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
            DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
            OnRowUpdating="GridView1_RowUpdating" CssClass="Admin_Table">
            <RowStyle CssClass="textcenter" ForeColor="#000066" />
            <FooterStyle CssClass="headcenter" BackColor="White" ForeColor="#000066"></FooterStyle>
            <Columns>
                <asp:TemplateField Visible ="false">
                    <HeaderTemplate>
                        全选<asp:CheckBox ID="CheckBox2" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged"
                            AutoPostBack="True" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="name1" HeaderText="主人员" />
                <asp:BoundField DataField="name2" HeaderText="从人员" />
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
        </asp:GridView>


        <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False"
            DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
            OnRowUpdating="GridView1_RowUpdating" CssClass="Admin_Table">
            <RowStyle CssClass="textcenter" ForeColor="#000066" />
            <FooterStyle CssClass="headcenter" BackColor="White" ForeColor="#000066"></FooterStyle>
            <Columns>
                <asp:TemplateField Visible ="false" >
                    <HeaderTemplate>
                        全选<asp:CheckBox ID="CheckBox2" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged"
                            AutoPostBack="True" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="name1" HeaderText="主人员" />
                <asp:BoundField DataField="name2" HeaderText="从人员" />
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
        </asp:GridView>
       </div>
    <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>
