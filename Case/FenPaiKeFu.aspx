<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FenPaiKeFu.aspx.cs" Inherits="Case_FenPaiKeFu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>分派客服人员</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <div class="Body_Title">
        销售管理 》》分派客服
    </div>
    <form id="form1" runat="server">
        <fieldset>
            <table cellpadding="2" cellspacing="1" class="Admin_Table">
                <thead>
                    <tr class="Admin_Table_Title">
                        <th>分配客服人员</th>
                    </tr>
                </thead>
                <tr>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;选择部门：&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="dw_department" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dw_department_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <b style="font-size: 15px;">将列表所选人</b>&nbsp;<asp:Button ID="Button2" runat="server" Text="确定分配给" OnClick="Button2_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;销售助理：<asp:DropDownList ID="DropDownList2" runat="server" Height="30px" Width="218px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    全选
                                <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="username" HeaderText="业务员" />
                            <asp:BoundField DataField="department" HeaderText="部门" />
                            <asp:BoundField DataField="dutyname" HeaderText="职位" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <table cellpadding="2" cellspacing="1" class="Admin_Table">
                <thead>
                    <tr class="Admin_Table_Title">
                        <th>已指定客服的业务人员</th>
                    </tr>
                </thead>
                <tr>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" Width="100%" OnRowDeleting="GridView2_RowDeleting" OnRowDataBound="GridView2_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="编号" />
                            <asp:BoundField DataField="marketid" HeaderText="业务员" />
                            <asp:BoundField DataField="UserName" HeaderText="客服" />
                            <asp:BoundField DataField="fillname" HeaderText="指定人" />
                            <asp:BoundField DataField="filltime" HeaderText="指定时间" />
                            <asp:CommandField HeaderText="操作" ShowDeleteButton="True" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                </tr>
            </table>
        </fieldset>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
