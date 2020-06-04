<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test1.aspx.cs" Inherits="Case_Test" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../js/calendar.js"></script>
    <style>
        .id {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            工程管理 》》我的任务
        </div>
        <asp:GridView ID="GridView1" runat="server" CssClass="Admin_Table" AutoGenerateColumns="False" Width="100%" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="id" HeaderStyle-CssClass="id" ItemStyle-CssClass="id">
                    <HeaderStyle CssClass="id"></HeaderStyle>
                    <ItemStyle CssClass="id"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                            CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="renwuid" HeaderText="任务号" />
                <asp:BoundField DataField="project" HeaderText="测试项目" />
                <asp:BoundField DataField="ceshiyuan" HeaderText="测试员" />
                <asp:BoundField DataField="fillname" HeaderText="分派人" />
                <asp:BoundField DataField="filltime" HeaderText="分派时间" />
                <asp:BoundField DataField="actiontime" HeaderText="开始时间" />
                <asp:BoundField DataField="endtime" HeaderText="结束时间" />
                <asp:BoundField DataField="jielun" HeaderText="结论" />
                <asp:BoundField DataField="bumen" HeaderText="部门" />
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title" />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
            Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="15">
        </webdiyer:AspNetPager>
    </form>
</body>
</html>
