<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShiXiaoPeiZhiManage.aspx.cs"
    Inherits="ShiXiao_ShiXiaoPeiZhiManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>时效配置</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="Body_Title">
            时效管理 》》时效配置</div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
            EnableScriptLocalization="false">
        </asp:ScriptManager>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    &nbsp;&nbsp; 外出时间：<input id="txFDate" runat="server" class="TxCss" name="txFDate"
                        onclick="popUpCalendar(this,document.forms[0].txFDate,'yyyy-mm-dd')" style="width: 90px"
                        type="text" visible="true" />
                    到
                    <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="popUpCalendar(this,document.forms[0].txTDate,'yyyy-mm-dd')"
                        style="width: 90px" type="text" visible="true" />
                    &nbsp; 查询条件：<asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="1">企业编号</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                        Text="查询" />
                </td>
            </tr>
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="id" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand"
                                OnRowDataBound="GridView1_RowDataBound">
                              <HeaderStyle CssClass="Admin_Table_Title " />
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                                CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Green" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="waichubianhao" HeaderText="任务编号" />
                                    <asp:BoundField DataField="chutime" DataFormatString="{0:d}" HeaderText="开始时间" />
                                    <asp:BoundField DataField="huitime" DataFormatString="{0:d}" HeaderText="截至时间" />
                                    <asp:BoundField DataField="chifan" HeaderText="提出天数" />
                                    <asp:BoundField DataField="zhusu" HeaderText="允许天数" />
                                    <asp:BoundField DataField="beizhu" HeaderText="预警天数" />
                                    <asp:BoundField DataField="fillname" HeaderText="提交人" />
                                    <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="提交时间" />
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
                            </asp:GridView>
                            <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第%StartRecordIndex%-%EndRecordIndex%"
                                CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                                InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager2_PageChanged"
                                PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                                Style="font-size: 9pt" UrlPaging="True" Width="682px">
                            </webdiyer:AspNetPager>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
