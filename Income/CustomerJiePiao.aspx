<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerJiePiao.aspx.cs" Inherits="Income_CustomerJiePiao" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <style type="text/css">
        .fixedheader
        {
            position: relative;
            table-layout: fixed;
            top: expression(this.offsetParent.scrollTop -1);
            z-index: 10;
        }
        
        .fixedheader th
        {
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
        }
    </style>
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
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" id="Table4" cellspacing="1" cellpadding="0" border="0">
        <tr>
            <td style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; padding-top: 0px;"
                valign="bottom" width="100%">
                <asp:DropDownList ID="SerchCondition" runat="server" Width="95px" CssClass="DDLStyle">
                    <asp:ListItem Value="customname">客户名称</asp:ListItem>
                    <asp:ListItem Value="responser">业务人员</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="SerchText" runat="server" CssClass="TextBoxCss" Width="91px"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" CssClass="BnCss" Text="查询" Width="76px" OnClick="Button2_Click" />
                <a href="../Customer/CustomerAdd.aspx" style="margin-left: 20px; margin-right: 20px"
                    target="button">
                    <asp:Label ID="Label2" runat="server" Font-Bold="true" ForeColor="Red" Text="新增客户"></asp:Label>
                </a>
                <asp:Panel runat="server" ID="fixedheader">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="kehuid" PageSize="20" OnRowDataBound="GridView1_RowDataBound" CssClass="Admin_Table">
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <Columns>
                            <asp:BoundField DataField="customname" HeaderText="客户名称" />
                            <asp:BoundField DataField="CustomType" HeaderText="客户类型" />
                            <asp:BoundField DataField="fillname" HeaderText="填写人" />
                            <asp:BoundField DataField="responser" HeaderText="业务员" />
                           <asp:HyperLinkField DataNavigateUrlFields="kehuid" HeaderText="借票" Target="_blank" DataNavigateUrlFormatString="FapiaoAdd.aspx?liushuihao={0}&&kehuid={0}"
                                Text="借票" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div style="color: Red;">
                                无到账信息</div>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </asp:Panel>
                &nbsp;<webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                    CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                    InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " PrevPageText="【前页】 "
                    ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                    Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="15" OnPageChanged="AspNetPager2_PageChanged">
                </webdiyer:AspNetPager>
    </table>
    </form>
</body>
</html>

