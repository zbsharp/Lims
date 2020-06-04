<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaoGaoListFaFang.aspx.cs" Inherits="Report_BaoGaoListFaFang" %>

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
        .renwuhao {
            display: none;
        }
        .BnCss {
            height: 27px;
        }
    </style>
</head>
<body>
    <form name="form1" runat="server" id="form1">
        <div>
            <div class="Body_Title">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                报告管理 》》报告发放
            </div>
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                width="100%">
                <tr bgcolor="#f4faff">
                    <td style="height: 5px">报告缮制日期：
               <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
                   style="width: 90px" type="text" visible="true" />
                        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>全部</asp:ListItem>
                        <asp:ListItem Value="baogaoid">报告号</asp:ListItem>
                        <asp:ListItem Value="rwid">任务号</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                            Text="查询" />
                        <br />

                        &nbsp;备注：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        <asp:Button ID="Button3" runat="server" Text="批量发放" OnClick="Button3_Click" />



                    </td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="height: 5px">

                        <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged"
                            Text="全选" />


                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="id" CssClass="Admin_Table" OnRowCommand="GridView5_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                            <HeaderStyle CssClass="Admin_Table_Title " />
                            <Columns>



                                <asp:HyperLinkField HeaderText="任务号" DataTextField="tjid" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=0"
                                    DataNavigateUrlFields="bianhao2" />
                                <asp:BoundField DataField="tjid" HeaderText="任务号" ItemStyle-CssClass="renwuhao" HeaderStyle-CssClass="renwuhao">

                                    <HeaderStyle CssClass="renwuhao"></HeaderStyle>

                                    <ItemStyle CssClass="renwuhao"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="baogaoid" HeaderText="报告号" />


                                <asp:BoundField DataField="weituo" HeaderText="委托方" />


                                <asp:BoundField DataField="name" HeaderText="产品" />
                                <asp:BoundField DataField="pizhundate" DataFormatString="{0:d}" HeaderText="缮制日期" />

                                <asp:BoundField DataField="fafangby" HeaderText="上传人" />

                                <asp:BoundField DataField="fafangby" HeaderText="发放人" />

                                <asp:BoundField DataField="fafangdate" DataFormatString="{0:d}" HeaderText="发放日期" />

                                <asp:BoundField DataField="state" HeaderText="当前状态" />

                                <asp:TemplateField HeaderText="发放">
                                    <ItemTemplate>
                                        <%--<span style="cursor: hand; color: Blue;" onclick="window.open('BaoGaoFaFang.aspx?id=<%#Eval("id") %>','test','dialogWidth=750px;DialogHeight=300px;status:no;help:no;resizable:yes;edge:raised;')">
                                        发放</span>--%>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="fafang">发放</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>'
                                            CommandArgument='<%# Eval("id") %>' CommandName="BussinessNeeds" ForeColor="Green"></asp:LinkButton>
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
                            Style="font-size: 9pt" UrlPaging="True" PageSize="15" Width="100%">
                        </webdiyer:AspNetPager>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>


