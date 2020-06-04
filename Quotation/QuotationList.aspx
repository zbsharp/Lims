<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="QuotationList.aspx.cs" Inherits="Quotation_QuotationList" %>

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
    <script type="text/javascript" src="../js/calendar.js"></script>

    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
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
    <form runat="server" id="form1">
        <div class="Body_Title">
            销售管理 》》报价列表
        </div>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>全部</asp:ListItem>
            <asp:ListItem Value="kehuname">客户名称</asp:ListItem>
            <asp:ListItem Value="responser">业务人员</asp:ListItem>
            <asp:ListItem Value="baojiaid">报价编号</asp:ListItem>
            <asp:ListItem Value="bumen">部门</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>(报价/开案)日期:<input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />
        <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />
        &nbsp;<asp:Button ID="Button2" runat="server" Text="EXCEL"
            OnClick="Button2_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp; 是否开案:<asp:DropDownList ID="DropDownList2"
            runat="server">
            <asp:ListItem>否</asp:ListItem>
            <asp:ListItem>是</asp:ListItem>
        </asp:DropDownList>
        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False"
            OnRowDataBound="GridView1_RowDataBound" ShowFooter="True">
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
                <asp:BoundField HeaderText="报价" />
                <asp:BoundField HeaderText="外包比列" />
                <asp:BoundField DataField="currency" HeaderText="币种" />
                <asp:BoundField DataField="Discount" HeaderText="折扣" />
                <asp:BoundField HeaderText="报价人" DataField="responser" />
                <asp:BoundField HeaderText="报价日期" DataField="filltime" DataFormatString="{0:d}" />
                <asp:BoundField HeaderText="提交" DataField="tijiaobiaozhi" ReadOnly="True" />
                <asp:BoundField HeaderText="提交日期" DataField="tijiaotime" DataFormatString="{0:d}" />
                <asp:BoundField HeaderText="审批情况" DataField="shenpibiaozhi" ReadOnly="True" />
                <asp:BoundField HeaderText="财务确定" DataField="huiqianbiaozhi" ReadOnly="True" />
                <asp:BoundField HeaderText="确定日期" DataField="huiqiantime" DataFormatString="{0:d}" />
                <asp:BoundField DataField="caiwu" HeaderText="财务审批" Visible="False" />
                <asp:BoundField DataField="kuozhanfei" HeaderText="扩展费" />
                <asp:HyperLinkField HeaderText="预览" Text="预览" DataNavigateUrlFormatString="~/Print/QuoPrint.aspx?baojiaid={0}&&customerid={1}"
                    DataNavigateUrlFields="baojiaid,kehuid" Target="_blank" />
                <asp:HyperLinkField HeaderText="正式报价" Text="下载" DataNavigateUrlFormatString="~/Print/QuoSign.aspx?quotationid={0}"
                    DataNavigateUrlFields="baojiaid" Target="_blank" />
                <asp:HyperLinkField HeaderText="编辑" Text="编辑" Target="button" DataNavigateUrlFormatString="~/Quotation/QuotationAdd.aspx?baojiaid={0}&amp;&amp;kehuid={1}"
                    DataNavigateUrlFields="baojiaid,kehuid" Visible="False" />
           <%--     <asp:HyperLinkField HeaderText="附件" Text="附件" Target="button" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?baojiaid={0}&amp;&amp;id={1}"
                    DataNavigateUrlFields="baojiaid,kehuid" />--%>

                <asp:BoundField DataField="zhehoujia" HeaderText="zhehoujia" HeaderStyle-CssClass="money" ItemStyle-CssClass="money">
                </asp:BoundField>
                <asp:BoundField DataField="coupon" HeaderText="coupon" HeaderStyle-CssClass="money" ItemStyle-CssClass="money">
                </asp:BoundField>
                <asp:BoundField DataField="epiboly_Price" HeaderText="epiboly_Price" HeaderStyle-CssClass="money" ItemStyle-CssClass="money">
                </asp:BoundField>
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
        </asp:GridView>

        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
            Style="font-size: 9pt" PageSize="15">
        </webdiyer:AspNetPager>
    </form>
</body>
</html>
