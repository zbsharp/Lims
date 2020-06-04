<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Caogao.aspx.cs" Inherits="Report_Caogao" %>

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
    <style type="text/css">
        .BnCss {
            width: 50px;
        }
    </style>
</head>
<body>
    <form name="form1" runat="server" id="form1">
        <div>
            <div class="Body_Title">
                报告管理 》》草稿报告
            </div>
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                width="100%">
                <tr bgcolor="#f4faff">
                    <td style="height: 5px">
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
                            <asp:ListItem Value="0">全部</asp:ListItem>
                            <asp:ListItem Value="tjid">任务编号</asp:ListItem>
                            <asp:ListItem Value="baogaoid">报告编号</asp:ListItem>

                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox1" runat="server" Width="141px"></asp:TextBox>


                        报告录入日期：
               <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
                   style="width: 90px" type="text" visible="true" />
                        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />



                        <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                            Text="查询" /><br />
                        &nbsp;&nbsp;<asp:Button ID="Button3" runat="server" Text="批量出草稿" OnClick="Button3_Click" />
                    </td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="height: 5px">

                        <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged"
                            Text="全选" />

                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="id" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                            <HeaderStyle CssClass="Admin_Table_Title " />
                            <Columns>
                                <asp:BoundField DataField="tjid" HeaderText="任务号" />


                                <asp:BoundField DataField="baogaoid" HeaderText="报告号" />

                                <asp:BoundField DataField="name" HeaderText="产品" />
                                <asp:BoundField HeaderText="工程师" />

                                <asp:BoundField DataField="dayinname" HeaderText="出草稿人" />


                                <asp:BoundField DataField="state" HeaderText="当前状态" />

                                <asp:BoundField DataField="baojiaid" HeaderText="报价编号" />

                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("baogaoid") %>' CommandName="shanzhi">出草稿</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:HyperLinkField HeaderText="上传报告" Text="草稿报告" Target="_blank" DataNavigateUrlFormatString="~/Report/BaoGaoFirstUpLoad.aspx?baogaoid={0}"
                                    DataNavigateUrlFields="baogaoid" />

                                <asp:HyperLinkField HeaderText="任务明细" DataTextField="tjid" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=1"
                                    DataNavigateUrlFields="bianhao2" />

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
                            Style="font-size: 9pt" PageSize="15" Width="100%">
                        </webdiyer:AspNetPager>
                    </td>
                </tr>
            </table>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
        <p>
    </form>
</body>
</html>

