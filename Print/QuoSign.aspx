<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuoSign.aspx.cs" Inherits="Print_QuoSign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            页面说明：生成PDF时，如报价已经审批通过，自动带章，否则不带章，文档生成过程中如遇比较慢，请等待<br />
            <br />
            <br />
            报价编号：<asp:TextBox ID="name" runat="server" Enabled="false"></asp:TextBox>
            <asp:Button ID="Btprint_Click" runat="server" Text="生成中文PDF"
                OnClick="Btprint_Click_Click" />
            &nbsp;
            <asp:Button ID="btn_englishPDF" runat="server" Text="生成英文PDF" OnClick="btn_englishPDF_Click" />
            <br />
            <br />
            <br />
            生成记录：
        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table"
            AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                            CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                <asp:BoundField DataField="quotationid" HeaderText="报价编号" />
                <asp:BoundField DataField="fillname" HeaderText="生成人" />
                <asp:BoundField DataField="filltime" HeaderText="生成时间" />
                <asp:TemplateField HeaderText="报价文件">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDownFile" runat="server" CommandName="DownFile" CommandArgument='<%#Eval("id") %>'>下载</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
        </asp:GridView>
        </div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
