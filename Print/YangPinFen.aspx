<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YangPinFen.aspx.cs" Inherits="Print_YangPinFen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>确定打印份数</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff">
                <td>
                    主样品份数：
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Text="2"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="打印" OnClick="Button1_Click" />
                </td>
                <td>
                   
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Text="1" Visible ="false" ></asp:TextBox>
                </td>
            </tr>
            <tr bgcolor="#f4faff">
                <td>
                    选择配件：
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Visible ="false" >
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff">
                <td style="text-align: left">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table"
                        DataKeyNames="id" Style="font-size: 9pt" Width="98%">
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
                                    <span style="cursor: hand; color: Blue;" onclick="window.showModalDialog('../YangPin/YanPinSee.aspx?id=<%#Eval("id") %>','test','dialogWidth=800px;DialogHeight=350px;status:no;help:no;resizable:yes; edge:raised;')">
                                        <asp:Label ID="seeLB" runat="server" Text='<%# Eval("picihaobianhao") %>'></asp:Label></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="yaopinname" HeaderText="样品名称" />
                            <asp:BoundField DataField="guige" HeaderText="型号" />
                            <asp:BoundField DataField="jiliang" HeaderText="数量" />
                            <asp:BoundField DataField="danwei" HeaderText="单位" />
                            <asp:BoundField DataField="shengchanchangjia" HeaderText="生产厂家" />
                            <asp:BoundField DataField="goumaidate" DataFormatString="{0:d}" HeaderText="接收日期" />
                            <asp:BoundField DataField="remark" HeaderText="备注" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" CssClass="td" ID="tbyy"  Text='1'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:Label ID="hsrq" runat="server" Text="打印份数"></asp:Label>
                                </HeaderTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
