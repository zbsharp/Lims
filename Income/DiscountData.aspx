<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DiscountData.aspx.cs" Inherits="Income_DiscountData" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报价数据</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/calendar.js"></script>
    <script src="../js/jquery-1.9.0.min.js"></script>
    <script type="text/javascript">
        var currentRowId = 0;

        function SelectRow() {
            if (event.keyCode == 40)
                MarkRow(currentRowId + 1);
            else if (event.keyCode == 38)
                MarkRow(currentRowId - 1);
        }

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
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <div class="Body_Title">
            财务管理 》》报价数据
        </div>
        <div>
            <asp:DropDownList ID="dw_where" runat="server">
                <asp:ListItem Value="customname">客户名称</asp:ListItem>
                <asp:ListItem Value="responser">业务人员</asp:ListItem>
                <asp:ListItem Value="BaoJiaId">报价编号</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="tx_value" runat="server"></asp:TextBox>
            <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
                style="width: 90px" type="text" visible="true" />
            到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />

            <asp:Button ID="bt_select" runat="server" Text="查询" OnClick="Button1_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:Button ID="bt_derive" runat="server" Text="导出Excel" OnClick="bt_derive_Click" />
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="BaoJiaId" HeaderText="报价编号" />
                        <asp:BoundField DataField="客户名称" HeaderText="客户名称" />
                        <asp:BoundField DataField="实际价格" HeaderText="实际价格" />
                        <asp:BoundField DataField="折扣" HeaderText="折扣" />
                        <asp:BoundField DataField="优惠后金额" HeaderText="优惠后金额" />
                        <asp:BoundField DataField="报价人" HeaderText="报价人" />
                        <asp:BoundField DataField="外发金额" HeaderText="外发金额" />
                        <asp:BoundField DataField="付款方式" HeaderText="付款方式" />
                        <asp:BoundField DataField="首款金额" HeaderText="首款金额" />
                        <asp:BoundField DataField="含税状态" HeaderText="含税状态" />
                        <asp:BoundField DataField="币种" HeaderText="币种" />
                        <asp:BoundField DataField="收款账户" HeaderText="收款账户" />
                        <asp:HyperLinkField HeaderText="查看" Text="详细信息" DataNavigateUrlFormatString="~/Income/Baojiadetail.aspx?baojiaid={0}" DataNavigateUrlFields="baojiaid" Target="_blank"/>
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title" />
                </asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                    CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                    InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 "
                    PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                    Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="12" OnPageChanged="AspNetPager1_PageChanged">
                </webdiyer:AspNetPager>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
