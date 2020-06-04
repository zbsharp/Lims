<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddClause.aspx.cs" EnableViewState="true"
    Inherits="SysManage_AddClause" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
<body >
    <form id="form1" runat="server">
   <div class="Body_Title">
            综合管理 》》增加条款</div>	
    <div>
        <table class="Admin_Table">
             <tr>
                <td align="center">
                    <fieldset style="width: 100%">
                        <legend>增加新条款</legend>
                        <asp:TextBox ID="TextBox5" runat="server" Width="89%" Height="41px" TextMode="MultiLine"></asp:TextBox><br />
                        <br />
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem>部门1</asp:ListItem>
                            <asp:ListItem>部门2</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadioButtonList1"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox5"
                            ErrorMessage="*"></asp:RequiredFieldValidator></fieldset>
                    <asp:Button ID="Button8" runat="server" CssClass="BnCss" Text="保存" OnClick="Button8_Click" />
                </td>
            </tr>
            <tr>
                <td style="height: 24px; width: 982px;">
                    <asp:GridView ID="GridView1" runat="server" DataKeyNames ="id" AutoGenerateColumns="False" CssClass="Admin_Table" OnRowDeleting="GridView1_RowDeleting"
                        OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#(Container.DisplayIndex+1).ToString("00")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="neirong" HeaderText="条款内容" />
                            <asp:BoundField DataField="bumen" HeaderText="所属部门" />
                            <asp:CommandField HeaderText="删除" ShowDeleteButton="True" />
                        </Columns>
                      
                    </asp:GridView>
                </td>
            </tr>
           
        </table>
    </div>
    </form>
</body>
</html>
