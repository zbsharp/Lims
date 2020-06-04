<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinkManAdd.aspx.cs" Inherits="Customer_LinkManAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="Body_Title">
        销售管理 》》录入联系人</div>
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="500"
        Width="100%">
        <cc1:TabPanel runat="server" HeaderText="标题" ID="TabPanel1">
            <HeaderTemplate>
                新增联系人</HeaderTemplate>
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                       <table class="Admin_Table">
                            <tr>
                                <td >
                            姓名：</td>
                                <td >
                                    <span style="font-size: 13pt; vertical-align: middle; color: red">
                              <asp:TextBox ID="TextBox1" runat="server"  Width="120px"></asp:TextBox><asp:DropDownList
                                  ID="DropDownList3"  Visible ="false"  runat="server">
                                  <asp:ListItem>认证</asp:ListItem>
                                  <asp:ListItem>非认证</asp:ListItem>
                              </asp:DropDownList></span></td>
                                <td >
                          传真：</td>
                                <td >
                              <asp:TextBox ID="TextBox9" runat="server"  Width="120px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td >
                                    部门：</td>
                                <td >
                                <asp:TextBox ID="TextBox2" runat="server"  Width="120px"></asp:TextBox></td>
                                <td >
                              MSN/QQ：</td>
                                <td >
                                <asp:TextBox ID="TextBox10" runat="server"  Width="120px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td >
                                职务：</td>
                                <td >
                                <asp:TextBox ID="TextBox3" runat="server"  Width="120px"></asp:TextBox></td>
                                <td >
                                角色：</td>
                                <td >
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="120px">
                                </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td >
                                职称：</td>
                                <td >
                                <asp:TextBox ID="TextBox4" runat="server"  Width="120px"></asp:TextBox></td>
                                <td >
                                生日：</td>
                                <td >
                                <asp:TextBox ID="TextBox11" runat="server"  Width="120px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td >
                                电话：</td>
                                <td >
                                <asp:TextBox ID="TextBox5" runat="server"  Width="120px"></asp:TextBox></td>
                                <td >
                                爱好：</td>
                                <td >
                                    <asp:DropDownList ID="DropDownList2" runat="server" Width="120px">
                            </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td >
                                手机：</td>
                                <td >
                                <asp:TextBox ID="TextBox6" runat="server"  Width="120px"></asp:TextBox></td>
                                <td >
                                家庭：</td>
                                <td >
                                <asp:TextBox ID="TextBox14" runat="server"  Width="120px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td >
                            电子邮箱：</td>
                                <td  colspan="3">
                            <asp:TextBox ID="TextBox8" runat="server" Width="469px"  ></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td >
                            备注：</td>
                                <td  colspan="3">
                            <asp:TextBox ID="TextBox16" runat="server"  Height="45px" TextMode="MultiLine" Width="469px"
                                ></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td  colspan="4" align ="center" >
                                    <asp:Button ID="Button4" runat="server" CssClass="BnCss" Text="确定" OnClick="Button4_Click" Width="84px" />
                                <asp:Button ID="Button1" runat="server" CssClass="BnCss" Text="取消" OnClick="Button1_Click" Width="84px" /></td>
                            </tr>
                            <tr>
                                <td  colspan="4">
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
               
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="图片" ID="TabPanel2">
            <HeaderTemplate>
               联系人列表</HeaderTemplate>
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="false">
                           
                            <Columns>
                                 <asp:BoundField DataField="linkmanname" HeaderText="姓名"></asp:BoundField>
                                                        <asp:BoundField DataField="department" HeaderText="部门" Visible="false"></asp:BoundField>
                                                        <asp:BoundField DataField="itemjiaose" HeaderText="项目角色"></asp:BoundField>
                                                        <asp:BoundField DataField="officetel" HeaderText="办公电话"></asp:BoundField>
                                                        <asp:BoundField DataField="fax" HeaderText="传真"></asp:BoundField>
                                                        <asp:BoundField DataField="handtel" HeaderText="手机号码"></asp:BoundField>
                                                        <asp:BoundField DataField="email" HeaderText="电子邮箱"></asp:BoundField>
                                                        <asp:BoundField DataField="manlike" HeaderText="个人爱好"></asp:BoundField>
                                                        <asp:BoundField DataField="msn" HeaderText="QQ"></asp:BoundField>
                                                        <asp:BoundField DataField="backinformation" HeaderText="备注"></asp:BoundField>
                                                        <asp:CommandField ShowEditButton="false" ShowDeleteButton="true" CausesValidation="False" />
                                                        <asp:TemplateField HeaderText="详细">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButtony" runat="server"  Text="查看"
                                                                    CommandName="youjian" CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    </form>
</body>
</html>
