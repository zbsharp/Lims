<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FapiaoManage.aspx.cs" Inherits="CCSZJiaoZhun_htw_FapiaoManage" %>

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
    <script type="text/javascript" src="../Celend/popcalendar.js"></script>

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
    <script type="text/javascript" src="../js/calendar.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
    <div class="Body_Title">
        财务管理 》》发票查询</div>
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
        width="100%">
        <tr bgcolor="#f4faff">
            <td align="left" style="height: 25px">
                &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; 录入日期：<input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="0">全部</asp:ListItem>
                    <asp:ListItem Value="1">抬头</asp:ListItem>
                    <asp:ListItem Value="2">大ID</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
                <asp:Button ID="Button2" CssClass="BnCss" runat="server" OnClick="Button2_Click"
                    Text="查询" />
                <asp:Button ID="Button1" runat="server" CssClass="BnCss" OnClick="Button1_Click"
                    Text="导出excel" />
            </td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="3" Style="font-size: 9pt" CssClass="Admin_Table"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                            <HeaderStyle CssClass="Admin_Table_Title " />
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                            CommandArgument='<%# Eval("id") %>' CommandName="BussinessNeeds" ForeColor="Green"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="fapiaono" HeaderText="发票号" />
                                <asp:BoundField DataField="fapiaojine" HeaderText="发票金额" />
                                <asp:BoundField DataField="lingpiaoren" HeaderText="领取人" />
                                <asp:BoundField DataField="lingpiaotime" DataFormatString="{0:d}" HeaderText="领取时间" />
                                <asp:BoundField DataField="fillname" HeaderText="录入人" />
                                <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="录入时间" />
                                <asp:BoundField DataField="taitou" HeaderText="抬头" />
                                <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                <asp:BoundField DataField="state" HeaderText="状态" />
                                <asp:BoundField DataField="responser" HeaderText="业务员" />
                                <asp:BoundField DataField="daid" HeaderText="到款编号" />
                                <asp:TemplateField HeaderText="明细">
                                    <ItemTemplate>
                                        <span style="cursor: hand; color: Blue;" onclick="window.open('FapiaoSee.aspx?id=<%#Eval("id") %>','test','dialogWidth=850px;DialogHeight=360px;status:no;help:no;resizable:yes; dialogTop:120px;edge:raised;')">
                                            <asp:Label ID="seeLB" runat="server" Text="明细"></asp:Label></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时没有数据"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第%StartRecordIndex%-%EndRecordIndex%"
                            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                            InputBoxStyle="width:19px" PageSize ="15" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager2_PageChanged"
                            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                            Style="font-size: 9pt"   UrlPaging="True" Width="682px">
                        </webdiyer:AspNetPager>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
