<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChanpinAdd.aspx.cs" Inherits="CCSZJiaoZhun_htw_ChanpinAdd" %>

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

</head>
<body>
    <form name="form1" runat="server" id="form1">
        <div class="buttons">
            <div class="Body_Title">
                报价管理 》》价格管理
            </div>
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                width="99%">
                <tr bgcolor="#f4faff">
                    <td style="text-align: left; width: 90px;">部门</td>
                    <td colspan="6" style="text-align: left">
                        <asp:DropDownList ID="DropDownList1" runat="server" Height="20px" Width="104px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="text-align: left; width: 90px;">产品类别名称：</td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txt_chanpin" runat="server"></asp:TextBox></td>
                    <td colspan="1" style="width: 90px; text-align: left">项目名称：</td>
                    <td colspan="1" style="text-align: left">
                        <asp:TextBox ID="txt_xm" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 90px; text-align: left">标准：</td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txt_biaozhun" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="1" style="width: 90px; text-align: left">样品：</td>
                    <td colspan="1" style="text-align: left">
                        <asp:TextBox ID="txt_yp" runat="server"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 90px; text-align: left">收费：</td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txt_price" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>
                     (数字类型)</td>
                    <td colspan="1" style="width: 90px; text-align: left">单位：</td>
                    <td colspan="1" style="text-align: left">
                        <asp:TextBox ID="txt_danwei" runat="server">RMB</asp:TextBox>
                    </td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 90px; text-align: left">周期：</td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txt_zhouqi" runat="server"></asp:TextBox></td>
                    <td colspan="1" style="width: 90px; text-align: left">备注：</td>
                    <td colspan="1" style="text-align: left">
                        <asp:TextBox ID="txt_beizhu" runat="server"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td colspan="6" style="text-align: center">
                        <asp:Button ID="Button1" runat="server" CssClass="positive"
                            OnClick="Button1_Click" Text="保存"></asp:Button>


                        <asp:Button ID="Button2" runat="server" Text="批量导入" CausesValidation="false" OnClick="Button2_Click" Visible="False" />
                    </td>
                </tr>
            </table>
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                width="99%">
                <tr bgcolor="#f4faff">
                    <td>

                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" DataKeyNames="id"
                            OnRowDeleting="GridView1_RowDeleting"
                            Style="font-size: 9pt" Width="98%" OnRowDataBound="GridView1_RowDataBound">

                            <Columns>
                                <asp:TemplateField HeaderText="序号" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                            CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="leibieid" HeaderText="业务类别号" />
                                <asp:BoundField DataField="leibiename" HeaderText="业务类别名称" />
                                <asp:BoundField DataField="chanpinid" HeaderText="产品类别编号" />
                                <asp:BoundField DataField="chanpinname" HeaderText="产品类别名称" />
                                <asp:BoundField DataField="neirongid" HeaderText="项目编号" />
                                <asp:BoundField DataField="neirong" HeaderText="项目名称" />
                                <asp:BoundField DataField="biaozhun" HeaderText="标准" />
                                <asp:BoundField DataField="shoufei" HeaderText="收费" />
                                <asp:BoundField DataField="danwei" HeaderText="单位" />
                                <asp:BoundField DataField="yp" HeaderText="样品" Visible="false" />
                                <asp:BoundField DataField="zhouqi" HeaderText="周期" Visible="false" />
                                <asp:BoundField DataField="beizhu" HeaderText="备注" />

                                <asp:TemplateField HeaderText="修改">
                                    <ItemTemplate>

                                        <span style="cursor: hand;" onclick="window.open('ChanPingUpdate.aspx?id=<%#Eval("id") %>','test','dialogWidth=800px;DialogHeight=400px;status:no;help:no;resizable:yes; dialogTop:100px;edge:raised;')">修改</span>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:CommandField HeaderText="删除" ShowDeleteButton="True">
                                    <ItemStyle ForeColor="Blue" />
                                </asp:CommandField>
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
                            Style="font-size: 9pt" UrlPaging="True">
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
