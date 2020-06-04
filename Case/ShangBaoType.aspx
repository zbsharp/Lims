<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShangBaoType.aspx.cs" Inherits="Case_ShangBaoType" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>电子上报类型</title>
 <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <style type="text/css">
        body
        {
            font-size: 12px;
            cursor: default;
            font-family: 宋体;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0" ms_positioning="GridLayout">
    <form id="form1" runat="server">
    
    <div>
        <table>
            
            <tr>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                    </asp:RadioButtonList>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="保存" />
                </td>
            </tr>
            <tr>
                <td style="height: 24px;">
                  
                    <div style="float: left">
                        
                        <asp:GridView ID="GridView2" runat="server" DataKeyNames="id" AutoGenerateColumns="False"
                            CssClass="Admin_Table" OnRowDeleting="GridView2_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#(Container.DisplayIndex+1).ToString("00")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="name" HeaderText="组别" />
                                <asp:CommandField HeaderText="删除" ShowDeleteButton="True" />
                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />  
                        </asp:GridView>
                    </div>
                
                </td>
            </tr>
          
        </table>
    </div>
    </form>
</body>
</html>
