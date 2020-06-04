<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoticeList.aspx.cs" Inherits="SysManage_NoticeList" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div class="div_All">
        <div class="Body_Title">
            客户管理 》》通知列表</div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    发布时间：<input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="popUpCalendar(this,document.forms[0].txFDate,'yyyy-mm-dd')"
                        style="width: 90px" type="text" visible="true" />
                    到
                    <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="popUpCalendar(this,document.forms[0].txTDate,'yyyy-mm-dd')"
                        style="width: 90px" type="text" visible="true" />
                    &nbsp; 查询条件：<asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        
                        <asp:ListItem Value="2">标题</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                        Text="查询" />
                </td>
            </tr>
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                        
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                        CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CompanyId" HeaderText="企业编号"  Visible ="false" />
                             <asp:BoundField DataField="kh" HeaderText="企业名称" Visible ="false"/>
                            <asp:BoundField DataField="Title" HeaderText="标题" />
                            <asp:BoundField DataField="Signdate" DataFormatString="{0:d}" HeaderText="发布日期" />
                            <asp:BoundField DataField="Enddate" DataFormatString="{0:d}" HeaderText="有效期" />
                            <asp:TemplateField HeaderText="明细">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton8" runat="server" Text="查看" ForeColor="blue" CommandArgument='<%# Eval("id") %>'
                                        CommandName="chakan"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>

                         <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
            Style="font-size: 9pt" UrlPaging="True" PageSize="15">
        </webdiyer:AspNetPager>
    </div>
    </form>
</body>
</html>
