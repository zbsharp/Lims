<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaoGaoAdd.aspx.cs" Inherits="Report_BaoGaoAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>获取报告号 </title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <style type="text/css">
        .BnCss {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            报告管理 》》获取报告号
        </div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff" style="display: none;">
                <td align="left" style="width: 90px"></td>
                <td align="left"></td>
                <td align="left" style="width: 90px">主检人：</td>
                <td align="left">
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="DropDownList3" ErrorMessage="请选择部门" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr bgcolor="#f4faff">
                <td align="left" style="width: 90px">实验室：</td>
                <td align="left">
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="18px" Width="79px">
                        <asp:ListItem Value="安规">安规</asp:ListItem>
                        <asp:ListItem Value="化学">化学</asp:ListItem>
                        <asp:ListItem Value="EMC">EMC</asp:ListItem>
                        <asp:ListItem>物理</asp:ListItem>
                        <asp:ListItem>电池</asp:ListItem>
                    </asp:DropDownList><asp:DropDownList ID="DropDownList3" Visible="false" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 90px">报告号： </td>
                <%--<td align="left"> <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                    OnClick="Button1_Click" Text="保 存" />（如果提示自动获取失败，请通过在文本框中输入报告号手工获取号码）
              
              <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="获取记录号" 
                  Width="79px" />
             </td>--%>
                <td align="left">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="BnCss"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                        OnClick="Button1_Click" Text="保 存" />

                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="获取报告号"
                        Width="105px" />
                </td>
            </tr>
        </table>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff">
                <td>

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        CssClass="Admin_Table" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound"
                        Width="100%" OnRowDeleting="GridView1_RowDeleting">

                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                        CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="tjid" HeaderText="任务号" />

                            <asp:BoundField DataField="baogaoid" HeaderText="报告号" />
                            <asp:BoundField DataField="fillname" HeaderText="获取人" />
                            <asp:BoundField DataField="filltime" HeaderText="获取日期" />
                            <asp:BoundField DataField="leibie" HeaderText="类别" />
                            <asp:BoundField DataField="zhujianDR" HeaderText="主检人" Visible="false" />
                            <asp:BoundField DataField="pizhundate" DataFormatString="{0:d}" HeaderText="批准日期" Visible="false" />
                            <asp:BoundField DataField="fafangdate" DataFormatString="{0:d}" HeaderText="发放日期" Visible="false" />
                            <asp:BoundField DataField="dangandate" DataFormatString="{0:d}" HeaderText="归档日期" Visible="false" />
                            <asp:CommandField HeaderText="取消" ShowDeleteButton="True" ShowEditButton="false" />

                            <asp:HyperLinkField HeaderText="上传" Text="上传" Target="_blank" DataNavigateUrlFormatString="~/Report/BaoGaoFirstUpLoad.aspx?baogaoid={0}"
                                DataNavigateUrlFields="baogaoid" />

                            <asp:HyperLinkField HeaderText="查看报告" Text="查看报告" Target="_blank" DataNavigateUrlFormatString="~/Report/BaoGaoShenPi2.aspx?baogaoid={0}"
                                DataNavigateUrlFields="baogaoid" />



                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>

                    </asp:GridView>

                </td>
            </tr>

            <tr bgcolor="#f4faff" style="display: none;">
                <td align="left" colspan="4" style="text-align: left; color: Green; font-size: medium;">编号规则：<br />
                    1、CCC报告：C-02101-T201312345，或C-02101-T201312345A

                <br />
                    2、CQC报告：C-02101-V201312345，或C-02101-T201312345A

                <br />
                    3、监督报告：C-02101-I201312345，或C-02101-I201312345A<br />
                    4、信安CCC：C-02101-13ISCCC1234，或C-02101-13ISCCC1234A<br />
                    5、所有其他报告：SET2013-12345<br />
                    6、不出报告请点击“获取记录号”：NO2014-12345
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
