<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RenWu.aspx.cs" Inherits="Customer_RenWu" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title></title>
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
            销售管理 》》跟踪记录提醒</div>
        <div>
    <div>
        <asp:GridView ID="GridView1" runat="server" CssClass="Admin_Table" AutoGenerateColumns="False"
            DataKeyNames="renwuid" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
            OnRowEditing="GridView1_RowEditing" OnRowCommand="GridView1_RowCommand">
           
            <Columns>
                <asp:BoundField DataField="Customname" HeaderText="客户名称" />
                <asp:BoundField DataField="fasongren" HeaderText="发送人" DataFormatString="{0:D}" />
                <asp:BoundField DataField="jieshouren" HeaderText="接收人" DataFormatString="{0:D}" />
                <asp:BoundField DataField="renwutime" HeaderText="下次跟踪日期" DataFormatString="{0:d}" />
                <asp:BoundField DataField="biaozhi" HeaderText="是否阅读" Visible="false" />
                <asp:HyperLinkField DataNavigateUrlFields="bianhao" DataNavigateUrlFormatString="~/Customer/CustomerSee.aspx?kehuid={0}"
                    HeaderText="" Text="查看"  Target ="button"/>

 <asp:HyperLinkField DataNavigateUrlFields="bianhao" DataNavigateUrlFormatString="~/Customer/Update.aspx?kehuid={0}"
                    HeaderText="" Text="修改日期"  Target ="button"/>

                <asp:TemplateField HeaderText="关闭">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" Text="关闭" CommandName="guanbi" CommandArgument='<%# Eval("renwuid") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="sid" HeaderText="sid" Visible="False" />
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页&nbsp; %StartRecordIndex%-%EndRecordIndex%"
            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager2_PageChanged"
            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
            Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="5">
        </webdiyer:AspNetPager>
        &nbsp;</div>
    </form>
</body>
</html>
