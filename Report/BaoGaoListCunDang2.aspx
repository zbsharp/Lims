<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaoGaoListCunDang2.aspx.cs"
    Inherits="Report_BaoGaoListCunDang2" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>报告列表 </title>
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
    <script type="text/javascript" src="../js/calendar.js"></script>
    
</head>
<body>
    <form name="form1" runat="server" id="form1">
    <div>
        <div class="Body_Title">
            报告管理 》》报告上报</div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff">
                <td style="height: 5px">


                 批准日期：
               <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />


                    <asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="tjid">任务编号</asp:ListItem>
                        <asp:ListItem Value="baogaoid">报告编号</asp:ListItem>

                        <asp:ListItem Value="dengjiby">申请编号</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                        Text="查询" />
                </td>
            </tr>
            <tr bgcolor="#f4faff">
                <td style="height: 5px">

                <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged"
                Text="全选" />
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound">
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <Columns>
                              <asp:HyperLinkField HeaderText="任务号"  DataTextField ="tjid" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=0"
                    DataNavigateUrlFields="tjid" />
                            <asp:BoundField DataField="baogaoid" HeaderText="报告号" />
                            <asp:BoundField DataField="leibie" HeaderText="类别" Visible="false" />
                            <asp:BoundField DataField="dayintime" HeaderText="安排打印时间"  DataFormatString ="{0:d}" Visible="false"/>
                            <asp:BoundField DataField="wanchengtime" HeaderText="实际完成时间" DataFormatString ="{0:d}" Visible="false"/>
                            <asp:BoundField DataField="state" DataFormatString="{0:d}" HeaderText="当前状态" Visible="false"/>
                            <asp:BoundField DataField="dayinname" HeaderText="打印人" Visible="false"/>
                            <asp:BoundField DataField="statebumen1" DataFormatString="{0:d}" HeaderText="签字" Visible="false"/>
                            <asp:BoundField DataField="statebumen2" DataFormatString="{0:d}" HeaderText="审核" Visible="false"/>
                            <asp:BoundField DataField="pizhunby" DataFormatString="{0:d}" HeaderText="批准" Visible="false"/>
                            <asp:BoundField DataField="pizhundate" DataFormatString="{0:d}" HeaderText="批准日期" />
                            <asp:BoundField DataField="dayinshu" DataFormatString="{0:d}" HeaderText="页数" Visible="false"/>
                            <asp:HyperLinkField HeaderText="报告" Text="报告" Target="_blank" DataNavigateUrlFormatString="~/Report/BaoGaoFirstUpLoadShenHe2.aspx?baogaoid={0}&&chakan=0"
                                DataNavigateUrlFields="baogaoid" />
                            <asp:BoundField DataField="danganid" DataFormatString="{0:d}" HeaderText="归档编号" />
                            <asp:BoundField DataField="dangandate" DataFormatString="{0:d}" HeaderText="归档日期" />

                             <asp:BoundField DataField="fafangdate" DataFormatString="{0:d}" HeaderText="发放日期" Visible="false"/>

                            <asp:BoundField DataField="kuaidihao" DataFormatString="{0:d}" HeaderText="快递编号" />
                            <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号"     />

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
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
                        Style="font-size: 9pt" UrlPaging="True" PageSize="15" Width="100%">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </table>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff">
                <td align="left" style="width: 90px">
                    快递单号：
                </td>
                <td align="left">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
                <td align="left" style="width: 90px">
                    上报日期：
                </td>
                <td align="left">
                    <asp:TextBox ID="TextBox3" runat="server" onclick="new Calendar().show(this.form.TextBox3);"></asp:TextBox>
                </td>
            </tr>
            <tr bgcolor="#f4faff">
                <td align="left" style="width: 90px">
                    CQC联系：&nbsp;
                </td>
                <td align="left">
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 90px">
                    备注：
                </td>
                <td align="left">
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr bgcolor="#f4faff">
                <td align="left" colspan="4" style="text-align: center">
                    <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="BnCss"
                        OnClick="Button3_Click" Text="提 交" />
                </td>
            </tr>
        </table>
        <asp:Literal ID="ld" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
