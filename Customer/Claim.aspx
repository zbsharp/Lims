<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Claim.aspx.cs" Inherits="Customer_Claim" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>到账认领</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.9.0.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            客户管理》》到账认领
        </div>
        <div style="margin: 5px;">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;付款人或付款单位：
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            &nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="id" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#(Container.DisplayIndex+1).ToString("00") %>'>LinkButton</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="liushuihao" HeaderText="流水号" />
                <asp:BoundField DataField="fukuanren" HeaderText="付款人" />
                <asp:BoundField DataField="fukuanriqi" HeaderText="付款日期" DataFormatString="{0:D}" />

                <asp:BoundField DataField="fukuanfangshi" HeaderText="付款方式" />
                <asp:BoundField DataField="danwei" HeaderText="币种" />
                <asp:BoundField DataField="fukuanjine" HeaderText="付款金额" />
                <asp:BoundField DataField="beizhu" HeaderText="备注" />
                <%-- 传的参数先加密、在接收的时候再解密  防止IE浏览器出现中文乱码 --%>
                <%--<asp:TemplateField HeaderText="认领">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" CommandName="action" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' runat="server">认领</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:HyperLinkField HeaderText="认领" Text="认领" Target="_blank" DataNavigateUrlFields="id" DataNavigateUrlFormatString="~/Income/daozhangrenling.aspx?id={0}"/>

            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title" />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 "
            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
            Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="15" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </form>
</body>
</html>
