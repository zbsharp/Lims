<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaiWu.aspx.cs" Inherits="Quotation_CaiWu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server">
        <div class="Body_Title">
            销售管理 》》财务审批
        </div>
        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table"
            AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                            CommandArgument='<%# Eval("baojiaid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                <asp:BoundField DataField="baojiaid" HeaderText="报价编号" />
                <asp:BoundField DataField="zhehoujia" HeaderText="报价" />
                <asp:BoundField HeaderText="报价人" DataField="fillname" />
                <asp:BoundField HeaderText="报价日期" DataField="filltime" DataFormatString="{0:d}" />
                <asp:BoundField HeaderText="提交状态" DataField="tijiaobiaozhi" ReadOnly="True" />
                <asp:BoundField HeaderText="提交日期" DataField="tijiaotime" DataFormatString="{0:d}" />
                <asp:BoundField HeaderText="审批状态" DataField="shenpibiaozhi" ReadOnly="True" Visible="False" />
                <asp:HyperLinkField HeaderText="审批" Text="审批" DataNavigateUrlFormatString="~/Quotation/ShenPi.aspx?baojiaid={0}"
                    DataNavigateUrlFields="baojiaid" Target="_blank" />
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
        </asp:GridView>
    </form>
</body>
</html>
