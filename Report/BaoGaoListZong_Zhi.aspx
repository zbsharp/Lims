<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaoGaoListZong_Zhi.aspx.cs" Inherits="Report_BaoGaoListZong_Zhi" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>报告列表 </title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <script type="text/javascript" src="../js/calendar.js"></script>
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
    <form name="form1" runat="server" id="form1">
    <div>
        <div class="Body_Title">
            报告管理 》》报告汇总</div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="tjid">任务编号</asp:ListItem>
                        <asp:ListItem Value="baogaoid">报告编号</asp:ListItem>
                        <asp:ListItem Value="dengjiby">申请编号</asp:ListItem>


                    </asp:DropDownList>
                    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>

                     报告批准日期：
               <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />

                    <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                        Text="查询" />
                    &nbsp;</td>
            </tr>
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound">
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <Columns>
                              
                               
                     <asp:BoundField DataField="tjid" HeaderText="任务号"   Visible ="false" />


                            <asp:BoundField DataField="baogaoid" HeaderText="报告号" />
                            <asp:BoundField DataField="leibie" HeaderText="类别" Visible="false" />
                            <asp:BoundField DataField="dayintime" HeaderText="安排打印" />
                            <asp:BoundField DataField="wanchengtime" HeaderText="实际完成" />
                            <asp:BoundField DataField="state" DataFormatString="{0:d}" HeaderText="状态" />
                            <asp:BoundField DataField="dayinshu" DataFormatString="{0:d}" HeaderText="页数" />
                                     <asp:HyperLinkField HeaderText="报告" Text="报告" Target="_blank" DataNavigateUrlFormatString="~/Report/BaoGaoFirstUpLoadShenHe2.aspx?baogaoid={0}&&chakan=0"
                                DataNavigateUrlFields="baogaoid" />
                            <asp:BoundField DataField="statebumen1" DataFormatString="{0:d}" HeaderText="签字" />
                            <asp:BoundField DataField="statebumen2" DataFormatString="{0:d}" HeaderText="审核" />
                            <asp:BoundField DataField="pizhundate" DataFormatString="{0:d}" HeaderText="签发日期" />
                            <asp:BoundField DataField="fafangdate" DataFormatString="{0:d}" HeaderText="发放日期" />
                            <asp:BoundField DataField="danganid" DataFormatString="{0:d}" HeaderText="归档编号" />
                            <asp:BoundField DataField="dangandate" DataFormatString="{0:d}" HeaderText="归档日期" />
                            <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号"     />
                            <asp:BoundField DataField="kuaidihao" HeaderText="快递编号"     />
                             <asp:HyperLinkField HeaderText="明细"  DataTextField ="tjid" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=0"
                    DataNavigateUrlFields="bianhao2" />

                             <asp:HyperLinkField HeaderText="去向" Text="去向" Target="_blank" DataNavigateUrlFormatString="~/Case/KuaiDiSee.aspx?id={0}&&biaozhi=2"
                    DataNavigateUrlFields="baogaoid" Visible="False" />


                     <asp:HyperLinkField HeaderText="差错录入" Text="差错录入" Target="_blank" DataNavigateUrlFormatString="~/Report/ChuCuoAdd.aspx?baogaoid={0}"
                    DataNavigateUrlFields="baogaoid" />
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
    </div>
    </form>
</body>
</html>
