<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaogaoProject.aspx.cs" Inherits="Report_BaogaoProject" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报告关联项目</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <style>
        /*.xmid {
            display: none;
        }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3" width="100%">
                <thead>
                    <tr class="Admin_Table_Title">
                        <th>测试项目</th>
                    </tr>
                </thead>
                <tr bgcolor="#f4faff">
                    <td>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand">
                            <HeaderStyle CssClass="Admin_Table_Title " />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="项目编号" HeaderStyle-CssClass="xmid" ItemStyle-CssClass="xmid"></asp:BoundField>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>' ForeColor="Green"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ceshiname" HeaderText="测试项目" />
                                <asp:BoundField DataField="biaozhun" HeaderText="标准" />
                                <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" runat="server" Text="关联" CommandArgument='<%#Eval("id") %>' CommandName="Action" />
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
            <br />
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3" width="100%">
                <thead>
                    <tr class="Admin_Table_Title">
                        <th>已关联的报告</th>
                    </tr>
                </thead>
                <tr bgcolor="#f4faff">
                    <td>
                        <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView2_RowDeleting">
                            <HeaderStyle CssClass="Admin_Table_Title " />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="id" HeaderStyle-CssClass="xmid" ItemStyle-CssClass="xmid"></asp:BoundField>
                                <asp:BoundField DataField="baogaoid" HeaderText="报告号" />
                                <asp:BoundField DataField="xmid" HeaderText="项目编号" />
                                <asp:BoundField DataField="taskid" HeaderText="任务号" />
                                <asp:BoundField DataField="engineer" HeaderText="工程师" />
                                <asp:BoundField DataField="fillname" HeaderText="创建人" />
                                <asp:BoundField DataField="filltime" HeaderText="创建时间" />
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
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