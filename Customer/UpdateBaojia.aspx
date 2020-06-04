<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateBaojia.aspx.cs" Inherits="Customer_UpdateBaojia" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../js/calendar.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <script type="text/javascript">
        var currentRowId = 0;
        function MarkRow(rowId) {
            if (document.getElementById(rowId) == null)
                return;
            if (document.getElementById(currentRowId) != null)
                document.getElementById(currentRowId).style.backgroundColor = '#ffffff';
            currentRowId = rowId;
            document.getElementById(rowId).style.backgroundColor = '#FFE0C0';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="Body_Title">
                客服管理 》》修改合同
            </div>
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>全部</asp:ListItem>
                <asp:ListItem Value="kehuname">客户名称</asp:ListItem>
                <asp:ListItem Value="responser">业务人员</asp:ListItem>
                <asp:ListItem Value="baojiaid">报价编号</asp:ListItem>
                <asp:ListItem Value="bumen">部门</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;
            <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="linkbutton1" Text='<%#(Container.DisplayIndex+1).ToString("00") %>' ForeColor="Green"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="customername" HeaderText="客户名称" />
                    <asp:BoundField DataField="baojiaid" HeaderText="报价号" />
                    <asp:BoundField DataField="responser" HeaderText="业务员" />
                    <asp:BoundField DataField="ShenPiBiaoZhi" HeaderText="审批状态" />
                    <asp:BoundField DataField="HuiQianBiaoZhi" HeaderText="财务确认" />
                    <asp:BoundField DataField="kaianbiaozhi" HeaderText="开案状态" />
                    <asp:BoundField DataField="zhehoujia" HeaderText="价格" />
                    <asp:BoundField DataField="Discount" HeaderText="折扣" />
                    <asp:BoundField DataField="paymentmethod" HeaderText="付款方式" />
                    <asp:BoundField DataField="Filltime" HeaderText="报价日期" DataFormatString="{0:d}" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="update" CommandArgument='<%# Eval("baojiaid")%>'>修改</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Admin_Table_Title" />
            </asp:GridView>
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
                PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                Style="font-size: 9pt" PageSize="15">
            </webdiyer:AspNetPager>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>