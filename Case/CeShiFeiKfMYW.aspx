<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CeShiFeiKfMYW.aspx.cs" Inherits="Case_CeShiFeiKfMYW" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>收费单列表</title>
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

    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            业务管理 》》收费单列表
        </div>
        <div>
            <table class="Admin_Table" width="100%">
                <tr>
                    <td align="left" colspan="4">客户名称或任务号或收费编号
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:DropDownList ID="DropDownList1"
                        runat="server" >
                        <asp:ListItem Value="否">未到账</asp:ListItem>
                        <asp:ListItem Value="是">已到账</asp:ListItem>
                    </asp:DropDownList>

                        开单日期：
                        <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
                            style="width: 90px" type="text" visible="true" />
                        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />

                        <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="合并打印" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4" style="text-align: center">
                        <asp:GridView ID="GridView1" ShowFooter="True" runat="server" OnRowDeleting="GridView1_RowDeleting"
                            class="Admin_Table" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                            <Columns>


<%--                                <asp:HyperLinkField HeaderText="任务号" DataTextField="rwbh" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=1"
                                    DataNavigateUrlFields="rwbh" />--%>


                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                            CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>


                                <asp:BoundField DataField="rwbh" HeaderText="任务号" />


                                <asp:BoundField DataField="inid" HeaderText="收费编号" />


                                <asp:BoundField DataField="yingshoujine" DataFormatString="{0:N2}" HeaderText="应收金额" />

                                <asp:BoundField DataField="kehuname" HeaderText="客户名称" />

                                <asp:BoundField DataField="weituo" HeaderText="委托方" />
                                <asp:BoundField DataField="name" HeaderText="联系人" />


                                <asp:BoundField DataField="hesuanbiaozhi" HeaderText="对账记录" />
                                <asp:BoundField DataField="yiduijine" HeaderText="已对金额" DataFormatString="{0:N2}" />
                                <asp:BoundField DataField="fillname" HeaderText="开单人" />
                                <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="开单日期" />





                                <asp:HyperLinkField HeaderText="打印" Text="打印" DataNavigateUrlFormatString="~/Print/InvoicePrint.aspx?baojiaid={0}&&customerid={1}&&inid={2}"
                                    DataNavigateUrlFields="baojiaid,kehuid,inid" Target="_blank" Visible="False" />

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        操作
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%#Eval("inid") %>' CommandName="delete">删除</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        请选择 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>

                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
                            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                            Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="15">
                        </webdiyer:AspNetPager>


                    </td>
                </tr>
            </table>
        </div>
        <p>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </p>
    </form>
</body>
</html>

