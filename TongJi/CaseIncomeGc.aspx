<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseIncomeGc.aspx.cs" Inherits="TongJi_CaseIncomeGc" %>

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

</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
        工程管理 》》工程完成任务查询</div>
    <asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
        <asp:ListItem Value="kehu">客户名称</asp:ListItem>
       
        <asp:ListItem Value="rwbianhao">任务号</asp:ListItem>
       
        <asp:ListItem Value="shenqingbianhao">申请编号</asp:ListItem>
        <asp:ListItem Value="state">状态</asp:ListItem>
         <asp:ListItem Value="gc">工程师</asp:ListItem>
    </asp:DropDownList>
    &nbsp;
    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
    完成日期：
    <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
        style="width: 90px" type="text" visible="true" />
    到
    <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
        style="width: 90px" type="text" visible="true" />
    <asp:DropDownList ID="DropDownList2" runat="server">
        <asp:ListItem></asp:ListItem>
       
        <asp:ListItem>完成</asp:ListItem>
        <asp:ListItem>暂停</asp:ListItem>
        <asp:ListItem>中止</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownList3" runat="server" Visible ="false" >
    </asp:DropDownList>
    <asp:Button ID="Button2" CssClass="BnCss" runat="server" Text="查询" OnClick="Button2_Click" />
    <table width="100%" class="Admin_Table" border="1">
                                <tr>
                                    <td style="width: 100%";  valign="top">
                                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                            OnRowDataBound="GridView1_RowDataBound" ShowFooter="true" OnRowCommand="GridView1_RowCommand"
                                            CssClass="Admin_Table">
                                            <Columns>
                                                <asp:BoundField DataField="rwbianhao" HeaderText="任务号" />
                                                <asp:BoundField DataField="" HeaderText="申请编号" />
                                                <asp:BoundField DataField="kehuname" HeaderText="客户名称" />
                                                <asp:BoundField DataField="chanpinname" HeaderText="产品名称" />
                                                <asp:BoundField DataField="yong1" HeaderText="组别" />
                                                <asp:BoundField DataField="kf" HeaderText="项目经理" />
                                                <asp:BoundField DataField="state" HeaderText="当前状态" />
                                                <asp:BoundField DataField="" HeaderText="工程师" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="" HeaderText="已收" DataFormatString="{0:N2}"  Visible ="false" />
                                                <asp:BoundField DataField="xiadariqi" HeaderText="下达日期" />
                                                <asp:BoundField DataField="yaoqiuwanchengriqi" HeaderText="要求完成日期" />
                                                <asp:BoundField DataField="beizhu3" HeaderText="实际完成日期" />
                                                <asp:TemplateField HeaderText="操作" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton5" runat="server" Text="核算" CommandArgument='<%# Eval("rwbianhao") %>'
                                                            CommandName="xiada"></asp:LinkButton>||
                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="关闭" CommandArgument='<%# Eval("rwbianhao") %>'
                                                            CommandName="xiada1"></asp:LinkButton>||
                                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="中止" CommandArgument='<%# Eval("rwbianhao") %>'
                                                            CommandName="xiada2"></asp:LinkButton>||
                                                        <asp:LinkButton ID="LinkButton3" runat="server" Text="完成" CommandArgument='<%# Eval("rwbianhao") %>'
                                                            CommandName="xiada3"></asp:LinkButton>||
                                                        <asp:LinkButton ID="LinkButton4" runat="server" Text="正常" CommandArgument='<%# Eval("rwbianhao") %>'
                                                            CommandName="xiada4"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:TemplateField>
                                                  <asp:HyperLinkField DataNavigateUrlFields="kehuid" Visible ="false"  DataNavigateUrlFormatString="~/Customer/CustomerSee.aspx?kehuid={0}"
                    HeaderText="" Text="查看客户" Target="button" />



                      <asp:TemplateField HeaderText="序 号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>'
                                        ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="shiyanleibie" HeaderText="类别" />

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
    </form>
</body>
</html>
