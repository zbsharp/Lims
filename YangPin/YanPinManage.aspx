<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YanPinManage.aspx.cs" Inherits="YangPin_YanPinManage" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>配件录入
    </title>

    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js">
    <script type="text/javascript" src="../Celend/popcalendar.js"></script>
    <script type="text/javascript" src="popcalendar.js"></script>

</head>
<body>
    <form name="form1" runat="server" id="form1">
        <div>
            <div class="Body_Title">
                业务受理 》》配件录入
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
            </asp:ScriptManager>

            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                width="100%">
                <tr bgcolor="#f4faff">
                    <td style="width: 90px">样品编号：</td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td style="width: 90px">接收日期：</td>
                    <td>
                        <asp:TextBox ID="TextBox6" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox6,'yyyy-mm-dd')"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 90px">配件名称：</td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                    <td style="width: 90px">配件个数：</td>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server" Text="1" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>是</asp:ListItem>
                            <asp:ListItem>否</asp:ListItem>
                        </asp:DropDownList>
                        <span style="font-size: 10px;">(否表示一个数量占一行、是表示数量值有多少就有多少行)</span> </td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 90px">单位：</td>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server" Text="台"></asp:TextBox></td>
                    <td style="width: 90px">生产厂家：</td>
                    <td colspan="3">
                        <asp:TextBox ID="TextBox7" runat="server" Width="85%"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 90px">备注：</td>
                    <td colspan="3">
                        <asp:TextBox ID="TextBox8" runat="server" Width="90%"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td colspan="4" style="text-align: center">
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                            Text="保 存" OnClick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="BnCss"
                            Text="刷 新" OnClick="Button2_Click" /></td>
                </tr>
            </table>

            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                width="100%">
                <tr bgcolor="#f4faff">
                    <td style="text-align: left">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound"
                                    OnRowDeleting="GridView1_RowDeleting" Style="font-size: 9pt" Width="98%">

                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                                    CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle ForeColor="Green" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="点击明细">
                                            <ItemTemplate>
                                                <span style="cursor: hand; color: Blue;" onclick="window.showModalDialog('YanPinSee.aspx?id=<%#Eval("id") %>','test','dialogWidth=800px;DialogHeight=350px;status:no;help:no;resizable:yes; edge:raised;')">
                                                    <asp:Label ID="seeLB" runat="server" Text='<%# Eval("picihaobianhao") %>'></asp:Label></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="yaopinname" HeaderText="样品名称" />
                                        <asp:BoundField DataField="guige" HeaderText="型号" Visible="False" />
                                        <asp:BoundField DataField="jiliang" HeaderText="数量" Visible="False" />
                                        <asp:BoundField DataField="danwei" HeaderText="单位" />
                                        <asp:BoundField DataField="shengchanchangjia" HeaderText="生产厂家" />
                                        <asp:BoundField DataField="goumaidate" DataFormatString="{0:d}" HeaderText="接收日期" />
                                        <asp:BoundField DataField="remark" HeaderText="备注" />
                                        <asp:CommandField HeaderText="删除" ShowDeleteButton="True">
                                            <ItemStyle ForeColor="Blue" />
                                        </asp:CommandField>
                                    </Columns>
                                    <HeaderStyle CssClass="Admin_Table_Title " />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                                    </EmptyDataTemplate>

                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>


        </div>

    </form>
</body>
</html>
