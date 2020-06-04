<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChanpinManage.aspx.cs" Inherits="CCSZJiaoZhun_htw_ChanpinManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>

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
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
        </asp:ScriptManager>
        <div>
            <div class="Body_Title">
                报价管理 》》价格查询
            </div>

            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                width="100%">
                <tr bgcolor="#f4faff">
                    <td align="left" style="height: 25px">&nbsp;&nbsp; 查询条件：&nbsp;<asp:DropDownList
                        ID="DropDownList1" runat="server" Width="77px">


                        <asp:ListItem Value="2">安规部</asp:ListItem>
                        <asp:ListItem Value="3">EMC部</asp:ListItem>

                        <asp:ListItem Value="4">标源EMC部</asp:ListItem>

                        <asp:ListItem Value="5">化学部</asp:ListItem>
                        <asp:ListItem Value="6">标源安规部</asp:ListItem>
                        <asp:ListItem Value="1">电池部</asp:ListItem>

                    </asp:DropDownList>&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
                        <asp:Button ID="Button2" CssClass="BnCss" runat="server"
                            OnClick="Button2_Click" Text="查询" />
                    </td>
                </tr>

            </table>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" CellPadding="3" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound"
                Style="font-size: 9pt" Width="98%">

                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle ForeColor="Green" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="leibiename" HeaderText="业务类别名称" />
                    <asp:BoundField DataField="chanpinid" Visible="false" HeaderText="产品类别编号" />
                    <asp:BoundField DataField="chanpinname" HeaderText="产品类别名称" />
                    <asp:BoundField DataField="neirong" HeaderText="项目名称" />
                    <asp:BoundField DataField="biaozhun" HeaderText="标准" />
                    <asp:BoundField DataField="shoufei" HeaderText="收费" />
                    <asp:BoundField DataField="yp" HeaderText="样品" />
                    <asp:BoundField DataField="zhouqi" HeaderText="周期" />
                    <asp:BoundField DataField="beizhu" HeaderText="备注" Visible="false" />
                    <asp:TemplateField HeaderText="修改">
                        <ItemTemplate>
                            <span style="cursor: hand;" onclick="window.open('ChanPingUpdate.aspx?id=<%#Eval("id") %>','test','dialogWidth=800px;DialogHeight=400px;status:no;help:no;resizable:yes; dialogTop:100px;edge:raised;')">修改</span>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <HeaderStyle CssClass="Admin_Table_Title " />
                <EmptyDataTemplate>
                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                </EmptyDataTemplate>

            </asp:GridView>


            <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第%StartRecordIndex%-%EndRecordIndex%"
                CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager2_PageChanged"
                PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                Style="font-size: 9pt" UrlPaging="True" PageSize="20">
            </webdiyer:AspNetPager>




        </div>

    </form>
</body>
</html>
