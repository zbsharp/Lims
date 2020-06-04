<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaogaoYP.aspx.cs" Inherits="Report_BaogaoYP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <title>报告关联样品</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            报告管理 》》报告号关联样品
            (只能关联当前任务下所绑定的样品)
        </div>
        <div>
            &nbsp;&nbsp;&nbsp;&nbsp;备注：<asp:TextBox ID="TextBox1" runat="server" Width="335px">
            </asp:TextBox><asp:Button ID="Button1" runat="server" Text="确定关联" OnClick="Button1_Click" />
        </div>
        <div>
            <fieldset>
                <legend style="color: red">样品信息</legend>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="sampleid" HeaderText="样品编号" />
                        <asp:BoundField DataField="anjianid" HeaderText="任务编号" />
                        <asp:BoundField DataField="kehuname" HeaderText="客户名称" />
                        <asp:BoundField DataField="name" HeaderText="样品名称" />
                        <asp:BoundField DataField="model" HeaderText="型号" />
                        <asp:BoundField DataField="fillname" HeaderText="录入人" />
                        <asp:BoundField DataField="filltime" HeaderText="录入时间" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title" />
                </asp:GridView>
            </fieldset>
        </div>
        <div>
            <fieldset>
                <legend style="color: red">已关联样品的报告</legend>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" Width="100%" DataKeyNames="id" OnRowDeleting="GridView2_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="baogaoid" HeaderText="报告号" />
                        <asp:BoundField DataField="yangpinid" HeaderText="样品号" />
                        <asp:BoundField DataField="taskid" HeaderText="任务号" />
                        <asp:BoundField DataField="fillname" HeaderText="关联人" />
                        <asp:BoundField DataField="filltime" HeaderText="关联时间" />
                        <asp:BoundField DataField="remork" HeaderText="备注" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title" />
                </asp:GridView>
            </fieldset>
        </div>
    </form>
</body>
</html>
