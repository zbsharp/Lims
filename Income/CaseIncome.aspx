﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseIncome.aspx.cs" Inherits="Income_CaseIncome" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title>项目应收已收记录</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../js/calendar.js"></script>
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
        <div class="Body_Title">
            业务管理 》》任务状态操作
        </div>
        <table width="100%" class="Admin_Table" border="1">
            <tr>
                <td>
                    <asp:DropDownList
                        ID="DropDownList1" runat="server" Width="77px">
                        <asp:ListItem Value="rwbianhao">任务号</asp:ListItem>
                        <asp:ListItem Value="kehu">客户名称</asp:ListItem>



                    </asp:DropDownList>&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>


                    下达日期：
        <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" visible="true" />
                    到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />

                    任务状态:
        <asp:DropDownList ID="DropDownList3" runat="server">
            <asp:ListItem>全部</asp:ListItem>
            <asp:ListItem>进行中</asp:ListItem>
            <asp:ListItem>完成</asp:ListItem>
            <asp:ListItem>暂停</asp:ListItem>
            <asp:ListItem>中止</asp:ListItem>
        </asp:DropDownList>
                    <asp:Button ID="Button2" CssClass="BnCss" runat="server"
                        Text="查询" OnClick="Button2_Click" />
                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click"
                        Text="批量暂停" />
                </td>
            </tr>
            <tr>
                <td>任务状态修改原因：<asp:TextBox ID="TextBox2" runat="server" Width="553px"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" BorderColor="White" Text="修改任务状态的时候请注明修改原因" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <fieldset>
            <%--            <legend style="color: Red">应收款项目</legend>--%>
            <table width="100%" class="Admin_Table"
                border="1">
                <tr>
                    <td style="width: 100%" valign="top">
                        <asp:GridView ID="GridView1" runat="server" DataKeyNames="rwbianhao" Width="100%" AutoGenerateColumns="False"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand"
                            CssClass="Admin_Table">
                            <Columns>
                                <asp:BoundField DataField="rwbianhao" HeaderText="任务号" />
                                <asp:BoundField DataField="kehuname" HeaderText="客户名称" />
                                <asp:BoundField DataField="chanpinname" HeaderText="产品名称" />
                                <asp:BoundField DataField="xinghaoguige" HeaderText="型号" />
                                <asp:BoundField DataField="state" HeaderText="当前状态" />
                                <asp:BoundField DataField="shiyanleibie" HeaderText="类别" />
                                <asp:BoundField DataField="weituodanwei" HeaderText="委托单位" />
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="中止" CommandArgument='<%# Eval("rwbianhao") %>'
                                            CommandName="zhongzhi"></asp:LinkButton>||
                                        <asp:LinkButton ID="LinkButton3" runat="server" Text="完成" CommandArgument='<%# Eval("rwbianhao") %>'
                                            CommandName="wancheng"></asp:LinkButton>||
                                        <asp:LinkButton ID="LinkButton4" runat="server" Text="进行中" CommandArgument='<%# Eval("rwbianhao") %>'
                                            CommandName="jinxing"></asp:LinkButton>|| 
                        <asp:LinkButton ID="LinkButton6" runat="server" Text="暂停" CommandName="zanting" CommandArgument='<%# Eval("rwbianhao") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="查看" Text="查看" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=0"
                                    DataNavigateUrlFields="bianhao" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " PrevPageText="【前页】 "
                            ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                            Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="15" OnPageChanged="AspNetPager2_PageChanged">
                        </webdiyer:AspNetPager>

                    </td>
                </tr>

            </table>

        </fieldset>
    </form>
</body>
</html>
