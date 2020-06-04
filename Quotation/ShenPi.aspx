<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShenPi.aspx.cs" Inherits="Quotation_ShenPi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>外包项目审核</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            报价管理 》》外包项目审核
        </div>
        <table class="Admin_Table">
            <tr>
                <td colspan="4" align="center">结论：
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>通过</asp:ListItem>
                        <asp:ListItem>不通过</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="Button3" runat="server" Text="确定保存" OnClick="Button3_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="Admin_Table"
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
                            <asp:BoundField DataField="kehuname" HeaderText="客户" />
                            <asp:BoundField DataField="name" HeaderText="产品" />
                            <asp:BoundField DataField="zhehoujia" HeaderText="报价" />
                            <asp:BoundField HeaderText="报价人" DataField="fillname" />
                            <asp:BoundField HeaderText="报价日期" DataField="filltime" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="提交状态" DataField="tijiaobiaozhi" />
                            <asp:BoundField HeaderText="提交日期" DataField="tijiaotime" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="财务审批" DataField="caiwu" ReadOnly="True" />
                            <asp:BoundField DataField="caiwuren" HeaderText="审批人" />
                            <asp:BoundField DataField="caiwutime" HeaderText="审批时间" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>外包测试项目：</td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="Admin_Table" DataKeyNames="id" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                        <Columns>
                            <asp:BoundField DataField="ceshiname" HeaderText="测试项目" />
                            <asp:BoundField DataField="biaozhun" HeaderText="测试标准" />
                            <asp:BoundField DataField="feiyong" HeaderText="费用" />
                            <asp:BoundField DataField="shuliang" HeaderText="数量" />
                            <asp:BoundField DataField="beizhu" HeaderText="备注" />
                            <asp:BoundField DataField="zhouqi" HeaderText="周期" />
                            <asp:BoundField DataField="jigou" HeaderText="外包机构" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
