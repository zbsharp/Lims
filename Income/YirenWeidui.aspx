<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YirenWeidui.aspx.cs" Inherits="Income_YirenWeidui" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>已认未对</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.9.0.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            财务管理》》业务已认
        </div>
        <div style="margin: 5px;">
            &nbsp;&nbsp;&nbsp;付款方或流水号：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            &nbsp;<asp:DropDownList ID="dropaffirm" runat="server">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>已确认</asp:ListItem>
                <asp:ListItem>未确认</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />
        </div>
        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
            DataKeyNames="id" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="序 号" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>' ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                <asp:BoundField DataField="liushuihao" HeaderText="流水号" ReadOnly="True" />
                <asp:BoundField DataField="fukuanren" HeaderText="付款人" />
                <asp:BoundField DataField="fukuanjine" HeaderText="付款金额" />
                <asp:BoundField DataField="fukuanriqi" HeaderText="付款日期" DataFormatString="{0:d}" />
                <asp:BoundField DataField="queren" HeaderText="状态" />
                <asp:BoundField HeaderText="当前认领人数" />
                <asp:HyperLinkField HeaderText="查看" Text="认领详细信息" DataNavigateUrlFormatString="~/Income/Renlingxiangxi.aspx?shuipiaoid={0}" DataNavigateUrlFields="id" Target="_blank" />
                <asp:TemplateField HeaderText="确认">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_affirm" runat="server" CommandName="affirm" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'>确认</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="退回">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_rollback" runat="server" CommandName="rollback" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'>退回</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="作废">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_cancellation" runat="server" CommandName="cancellation" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'>作废</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField HeaderText="反结算" Text="反结算"  DataNavigateUrlFields="id,fukuanjine" DataNavigateUrlFormatString="~/Income/Fanjiesuan.aspx?liushuihao={0}&money={1}" Target="_blank"/>
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第%StartRecordIndex%-%EndRecordIndex%"
            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager2_PageChanged"
            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
            Style="font-size: 9pt" UrlPaging="True" PageSize="15" Width="100%">
        </webdiyer:AspNetPager>
    </form>
</body>
</html>
