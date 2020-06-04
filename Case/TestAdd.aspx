<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestAdd.aspx.cs" Inherits="Case_Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <style type="text/css">
        .id {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            工程管理 》》待分测试员
        </div>
        <fieldset>
            <legend style="color: red">任务信息</legend>
            <asp:GridView ID="gdvtaskon" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="rwbianhao" HeaderText="任务编号"></asp:BoundField>
                    <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号" Visible="false"></asp:BoundField>
                    <asp:BoundField DataField="state" HeaderText="状态"></asp:BoundField>
                    <asp:BoundField DataField="shiyanleibie" HeaderText="试验类别"></asp:BoundField>
                    <asp:BoundField DataField="xiadariqi" HeaderText="下单日期"></asp:BoundField>
                    <asp:BoundField DataField="yaoqiuwanchengriqi" HeaderText="要求完成日期"></asp:BoundField>
                    <asp:BoundField DataField="baogao" HeaderText="是否出报告"></asp:BoundField>
                    <asp:BoundField DataField="youxian" HeaderText="客户类型"></asp:BoundField>
                    <asp:BoundField DataField="beizhu" HeaderText="备注"></asp:BoundField>
                    <asp:BoundField DataField="chenjieren" HeaderText="承接人" Visible="false"></asp:BoundField>
                    <asp:BoundField DataField="fillname" HeaderText="填写人"></asp:BoundField>
                    <asp:BoundField DataField="filltime" HeaderText="填写日期"></asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="Admin_Table_Title " />
            </asp:GridView>
        </fieldset>


        <fieldset>
            <legend style="color: red;">项目信息</legend>
            <asp:GridView ID="gdvprojectitem" runat="server" Width="100%" AutoGenerateColumns="False"
                DataKeyNames="id" CssClass="Admin_Table">
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
        </fieldset>


        <table class="Admin_Table">
            <tr>
                <td>部&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 门：</td>
                <td>
                    <asp:TextBox ID="txt_depa" runat="server" Width="196px" ReadOnly="true"></asp:TextBox>
                </td>
                <td>测试员：</td>
                <td>
                    <asp:DropDownList ID="drop_Test" runat="server" Height="30px" Width="112px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btn_yes" runat="server" Text="确定" Width="101px" OnClick="btn_yes_Click" />
                </td>
            </tr>
        </table>

        <fieldset>
            <legend style="color: red;">已分派项目</legend>
            <asp:GridView ID="GridView1" runat="server" CssClass="Admin_Table" AutoGenerateColumns="False" DataMember="id" OnRowDeleting="GridView1_RowDeleting" OnDataBound="GridView1_DataBound">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="id" HeaderStyle-CssClass="id" ItemStyle-CssClass="id">
                        <HeaderStyle CssClass="id"></HeaderStyle>
                        <ItemStyle CssClass="id"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="renwuid" HeaderText="任务号" />
                    <asp:BoundField DataField="project" HeaderText="测试项目" />
                    <asp:BoundField DataField="ceshiyuan" HeaderText="测试员" />
                    <asp:BoundField DataField="fillname" HeaderText="分派人" />
                    <asp:BoundField DataField="filltime" HeaderText="分派时间" />
                    <asp:BoundField DataField="bumen" HeaderText="部门" />
                    <asp:BoundField DataField="actiontime" HeaderText="开始时间" />
                    <asp:BoundField DataField="endtime" HeaderText="结束时间" />
                    <asp:BoundField DataField="jielun" HeaderText="结论" />
                    <asp:CommandField HeaderText="删除" ShowDeleteButton="True" />
                </Columns>
                <HeaderStyle CssClass="Admin_Table_Title" />
            </asp:GridView>
        </fieldset>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
