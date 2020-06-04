<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaoGaoFirstUpLoad.aspx.cs" Inherits="Income_BaoGaoFirstUpLoad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>上传收费单</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
        财务管理 》》上传收费单</div>
    <div>
        <table class="Admin_Table">
            <tr>
                <td colspan="4">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button ID="Button5" runat="server" CausesValidation="false" OnClick="Button5_Click"
                        Text="上传" />
                    <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table"
                        CaptionAlign="Left" DataKeyNames="id" OnRowDeleting="GridView5_RowDeleting">
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="urltext" HeaderText="附件名称" DataTextField="filename">
                                <ItemStyle ForeColor="Green" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="leibie" HeaderText="部门" />
                            <asp:BoundField DataField="typ" HeaderText="文件类型" />
                            <asp:BoundField DataField="fillname" HeaderText="上传人" />
                          
                         
                            <asp:CommandField DeleteText="删除附件" HeaderText="删除" ShowDeleteButton="True" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                </td>
            </tr>
        </table>

      

    </div>
    </form>
</body>
</html>

