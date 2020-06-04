<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChuCuoLeiBie1aspx1.aspx.cs" Inherits="SysManage_ChuCuoLeiBie1aspx1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改文件</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 100px;">
                                文件类别：</td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                            <td colspan="1" style="width: 100px; text-align: left">
                                文件名称：</td>
                            <td colspan="1" style="text-align: left">
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                        </tr>
        <tr bgcolor="#f4faff" style ="display :none ;">
            <td style="width: 100px; text-align: left">
                电话：</td>
            <td colspan="3" style="text-align: left">
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
            <td colspan="1" style="width: 100px; text-align: left">
                传真：</td>
            <td colspan="1" style="text-align: left">
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td style="width: 100px; text-align: left">
                备注：</td>
            <td colspan="5" style="text-align: left">
                <asp:TextBox ID="TextBox5" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td colspan="6" style="text-align: center">
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                                    Text="修改文件名称" OnClick="Button1_Click" />
                                <asp:Button ID="Button2" runat="server" Text="修改文件类别" onclick="Button2_Click" />
                                <asp:Button ID="Button3" runat="server" Text="删除该文件" onclick="Button3_Click" />
                               
            </td>
        </tr></table> 

          <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
      <tr bgcolor="#f4faff">
         <td >
             <strong>附件管理：</strong></td>
      </tr>
        <tr bgcolor="#f4faff">
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" Width="40%" />
                <asp:Button ID="Button4" runat="server" CausesValidation="false" CssClass="BnCss"
                    OnClick="Button4_Click" Text="上传" Width="49px" />
                <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                    DataKeyNames="id" OnRowDeleting="GridView2_RowDeleting"
                    Width="99%" CssClass="Admin_Table" ShowHeader="true">
                  <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="urltext" DataTextField="filename">
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:HyperLinkField>
                                                <asp:BoundField DataField="leibie" HeaderText="附件类型" />
                                                <asp:BoundField DataField="typ" HeaderText="文件类型" />
                                                <asp:BoundField DataField="fillname" HeaderText="上传人"  Visible ="false" />
                                                <asp:CommandField DeleteText="删除附件" HeaderText="删除" ShowDeleteButton="TRUE" />
                                                <asp:BoundField DataField="caseid" HeaderText="附件编号" Visible="False" />

                                              
                                            </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="暂无附件"></asp:Label>
                    </EmptyDataTemplate>
                </asp:GridView>
               
            </td>
        </tr>
    </table>

    </div>
    </form>
</body>
</html>

