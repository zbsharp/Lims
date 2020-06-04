<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XinBaogaoADD.aspx.cs" Inherits="Report_XinBaogaoADD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>获取报告号</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <style>
        .xmid {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            报告管理 》》获取报告号
            (获取报告时请先选中所需关联的测试项目)
        </div>
        <table cellpadding="2" cellspacing="1" class="Admin_Table" style="width: 100%">
            <thead>
                <tr class="Admin_Table_Title">
                    <th>测试项目</th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:GridView ID="GridView5" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" OnRowDataBound="GridView5_RowDataBound">
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="项目编号" HeaderStyle-CssClass="xmid" ItemStyle-CssClass="xmid">
                                <HeaderStyle CssClass="xmid"></HeaderStyle>
                                <ItemStyle CssClass="xmid"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>' ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ceshiname" HeaderText="测试项目" />
                            <asp:BoundField DataField="biaozhun" HeaderText="标准" />
                            <asp:BoundField DataField="beizhu" HeaderText="备注" />
                            <asp:BoundField DataField="bumen" HeaderText="部门" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    请选择
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>

        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3" width="100%">
            <tr bgcolor="#f4faff">
                <td align="left" style="width: 90px">实验室：</td>
                <td align="left">
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="18px" Width="79px">
                        <asp:ListItem Value="安规部">安规部</asp:ListItem>
                        <asp:ListItem Value="化学部">化学部</asp:ListItem>
                        <asp:ListItem Value="EMC部">EMC部</asp:ListItem>
                        <asp:ListItem Value="电池部">电池部</asp:ListItem>
                        <asp:ListItem Value="龙华EMC部">龙华EMC部</asp:ListItem>
                        <asp:ListItem Value="龙华安规部">龙华安规部</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    是否Y类：
                    <asp:DropDownList ID="drop_y" runat="server">
                        <asp:ListItem Selected="True">否</asp:ListItem>
                        <asp:ListItem>是</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="自动获取报告号"
                        Width="150px" />
                </td>
            </tr>
            <tr bgcolor="#f4faff">
                <td>报告号：</td>
                <td>
                    <asp:TextBox ID="txt_baogaoid" runat="server" Width="222px"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btn_hand" runat="server" Text="手动获取报告" OnClick="btn_hand_Click" Width="150px" />
                </td>
            </tr>
        </table>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <thead>
                <tr class="Admin_Table_Title">
                    <th>报告号</th>
                </tr>
            </thead>
            <tr bgcolor="#f4faff">
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        CssClass="Admin_Table" DataKeyNames="id"
                        Width="100%" OnRowCommand="GridView1_RowCommand">
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
                            <asp:BoundField DataField="filltime" HeaderText="获取日期" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="leibie" HeaderText="类别" />
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="delete_baogao" CommandArgument='<%# Eval("id") %>' OnClientClick="return confirm('删除报告号时与该报告号已关联的测试项目也将删除')">删除</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="zhujianDR" HeaderText="主检人" Visible="false" />
                            <asp:BoundField DataField="pizhundate" DataFormatString="{0:d}" HeaderText="批准日期" Visible="false" />
                            <asp:BoundField DataField="fafangdate" DataFormatString="{0:d}" HeaderText="发放日期" Visible="false" />
                            <asp:BoundField DataField="dangandate" DataFormatString="{0:d}" HeaderText="归档日期" Visible="false" />
                            <asp:HyperLinkField HeaderText="草稿上传" Text="上传" Target="_blank" DataNavigateUrlFormatString="~/Report/BaoGaoFirstUpLoad.aspx?baogaoid={0}"
                                DataNavigateUrlFields="baogaoid" />
                            <asp:HyperLinkField HeaderText="关联样品" Text="选择样品" Target="_blank" DataNavigateUrlFormatString="~/Report/BaogaoYP.aspx?baogaoid={0}&taskid={1}"
                                DataNavigateUrlFields="baogaoid,tjid" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3" width="100%">
            <thead>
                <tr class="Admin_Table_Title">
                    <th>已关联的报告</th>
                </tr>
            </thead>
            <tr bgcolor="#f4faff">
                <td>
                    <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id" CssClass="Admin_Table" OnRowDeleting="GridView2_RowDeleting">
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id" HeaderStyle-CssClass="xmid" ItemStyle-CssClass="xmid"></asp:BoundField>
                            <asp:BoundField DataField="baogaoid" HeaderText="报告号" />
                            <asp:BoundField DataField="xmid" HeaderText="项目编号" HeaderStyle-CssClass="xmid" ItemStyle-CssClass="xmid" />
                            <asp:BoundField DataField="xmname" HeaderText="项目名称" />
                            <asp:BoundField DataField="taskid" HeaderText="任务号" />
                            <asp:BoundField DataField="fillname" HeaderText="创建人" />
                            <asp:BoundField DataField="filltime" HeaderText="创建时间" DataFormatString="{0:d}" />
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</body>
</html>
