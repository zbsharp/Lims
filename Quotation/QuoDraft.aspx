<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuoDraft.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Quotation_QuoDraft" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
        function text() {
            document.getElementById("bnClick").click();
        }

    </script>
    <style type="text/css">
        .money {
            display: none;
        }
    </style>
</head>
<body>

    <form id="form2" runat="server">
        <div class="Body_Title">
            销售管理 》》草稿报价
        </div>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="kehuname">客户名称</asp:ListItem>
            <asp:ListItem Value="responser">业务人员</asp:ListItem>
            <asp:ListItem Value="baojiaid">报价编号</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1"
            runat="server" Text="查询" OnClick="Button1_Click" TabIndex="1" />

        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table"
            AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
            OnRowCommand="GridView1_RowCommand">
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
                <asp:BoundField HeaderText="报价" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="currency" HeaderText="币种" />
                <asp:BoundField HeaderText="报价人" DataField="responser" />
                <asp:BoundField HeaderText="报价日期" DataField="filltime" DataFormatString="{0:d}" />
                <asp:BoundField HeaderText="提交状态" DataField="tijiaobiaozhi" />
                <asp:BoundField DataField="Discount" HeaderText="折扣" />
                <asp:BoundField HeaderText="外包比列" />
                <asp:BoundField HeaderText="审批状态" DataField="shenpibiaozhi" ReadOnly="True" />
                <asp:BoundField HeaderText="扩展费" DataField="kuozhanfei" />
                <asp:HyperLinkField HeaderText="预览" Text="预览" DataNavigateUrlFormatString="~/Print/QuoPrint.aspx?baojiaid={0}&&customerid={1}"
                    DataNavigateUrlFields="baojiaid,kehuid" Target="_blank" />
                <asp:HyperLinkField HeaderText="编辑" Text="编辑" Target="_blank" DataNavigateUrlFormatString="~/Quotation/QuotationAdd.aspx?baojiaid={0}&amp;&amp;kehuid={1}"
                    DataNavigateUrlFields="baojiaid,kehuid" />

                <asp:TemplateField HeaderText="提交审批">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton5" runat="server" Text="提交" CommandArgument='<%# Eval("baojiaid") %>'
                            CommandName="cancel1"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="删除">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton6" runat="server" Text="删除" CommandArgument='<%# Eval("baojiaid") %>'
                            CommandName="cancel2"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>

                <asp:BoundField DataField="zhehoujia" HeaderText="zhehoujia" HeaderStyle-CssClass="money" ItemStyle-CssClass="money"></asp:BoundField>
                <asp:BoundField DataField="coupon" HeaderText="coupon" HeaderStyle-CssClass="money" ItemStyle-CssClass="money"></asp:BoundField>
                <asp:BoundField DataField="epiboly_Price" HeaderText="epiboly_Price" HeaderStyle-CssClass="money" ItemStyle-CssClass="money"></asp:BoundField>
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
        </asp:GridView>



        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
            Width="682px" Style="font-size: 9pt" PageSize="12">
        </webdiyer:AspNetPager>
        <asp:Literal ID="ld" runat="server"></asp:Literal>

    </form>
</body>
</html>
