<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update.aspx.cs" MaintainScrollPositionOnPostback="false" Inherits="SysManage_Update" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head2" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript">
        //设置Datagrid列宽可以被拖动的函数
        function SyDG_moveOnTd(td) {
            if (event.offsetX > td.offsetWidth - 10)
                td.style.cursor = 'w-resize';
            else
                td.style.cursor = 'default';
            if (td.mouseDown != null && td.mouseDown == true) {
                if (td.oldWidth + (event.x - td.oldX) > 0)
                    td.width = td.oldWidth + (event.x - td.oldX);
                td.style.width = td.width;
                td.style.cursor = 'w-resize';

                table = td;
                while (table.tagName != 'TABLE') table = table.parentElement;
                table.width = td.tableWidth + (td.offsetWidth - td.oldWidth); table.style.width = table.width;
            }
        }
        function SyDG_downOnTd(td) {
            if (event.offsetX > td.offsetWidth - 10) {
                td.mouseDown = true;
                td.oldX = event.x;
                td.oldWidth = td.offsetWidth;
                table = td; while (table.tagName != 'TABLE') table = table.parentElement;
                td.tableWidth = table.offsetWidth;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="div_All">
            <div class="Body_Title">用户个人信息</div>
            <fieldset>
                <legend style="color: Red">个人信息(如需修改其他信息请与系统管理员联系)</legend>
                <table cellpadding="2" cellspacing="1" class="Admin_Table" style="width: 100%; margin: 5px auto;">
                    <tr>
                        <td>登录：</td>
                        <td>
                            <asp:TextBox ID="name" ReadOnly="true" runat="server" class="txtHInput"></asp:TextBox></td>
                        <td>部门：</td>
                        <td>
                            <asp:DropDownList ID="Branch" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>邮箱：</td>
                        <td>
                            <asp:TextBox ID="email" runat="server" class="txtHInput"></asp:TextBox></td>
                        <td>职务：</td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                            </asp:DropDownList>
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>电话：</td>
                        <td>
                            <asp:TextBox ID="workPhone" class="txtHInput" runat="server"></asp:TextBox></td>
                        <td>手机：</td>
                        <td>
                            <asp:TextBox ID="movePhone" class="txtHInput" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>传真：</td>
                        <td>
                            <asp:TextBox ID="TextBox2" class="txtHInput" runat="server"></asp:TextBox>
                        </td>
                        <td>短号：</td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" Width="193px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>QQ号</td>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                        </td>
                        <td>密码：<span style="color: Red">Here</span></td>
                        <td>
                            <asp:TextBox ID="TextBox1" ToolTip="输入您的新密码" TextMode="Password" runat="server"></asp:TextBox><asp:Button ID="Button4" ToolTip="请在密码框中输入您新的密码后点该按钮保存" runat="server" Text="重置自己的密码" OnClick="Button4_Click" /></td>
                    </tr>
                    <tr>
                        <td>地区：</td>
                        <td>
                            <asp:DropDownList ID="drop_area" runat="server">
                                <asp:ListItem Value="FY">福永</asp:ListItem>
                                <asp:ListItem Value="LH">龙华</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="Button3" CssClass="BnCss" CausesValidation="false" runat="server" Text="修 改" Width="53px" OnClick="Button3_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset style="display: none;">
                <legend style="color: Red">通讯录</legend>
                用户登录名或所在部门或者职务：<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                <asp:Button ID="Button1"
                    runat="server" Text="查询" OnClick="Button1_Click" CausesValidation="false" />
                <asp:GridView ID="GridView1" runat="server" CssClass="Admin_Table" AutoGenerateColumns="False"
                    DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#(Container.DisplayIndex+1).ToString("000")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="username" HeaderText="登录名称" ReadOnly="True" />
                        <asp:BoundField DataField="departmentname" HeaderText="所在部门" />
                        <asp:BoundField DataField="jiaosename" HeaderText="职务" />
                        <asp:BoundField DataField="youxiang" HeaderText="电子邮箱" />
                        <asp:BoundField DataField="banggongdianhua" HeaderText="办公电话" />
                        <asp:BoundField DataField="yidong" HeaderText="移动电话" />
                        <asp:BoundField DataField="fax" HeaderText="传真" />
                        <asp:BoundField DataField="shortphone" HeaderText="短号" />
                        <asp:BoundField DataField="flag" HeaderText="是否停用" />
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title " />
                </asp:GridView>

                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                    CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                    InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
                    PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                    Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="10">
                </webdiyer:AspNetPager>

            </fieldset>


        </div>
        <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>

