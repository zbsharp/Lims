<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DianZiShangBao.aspx.cs" Inherits="Case_DianZiShangBao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>附件信息</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
  
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
</head>
<body>
    <form id="form1" runat="server">

    <div class="Body_Title">
        案件管理 》》电子上报</div>
    <div>
         <table class="Admin_Table">
                                
                                  <tr>
                                    <td  colspan="4" >
                                        <asp:DropDownList ID="DropDownList1" AutoPostBack ="true"  runat="server" 
                                            onselectedindexchanged="DropDownList1_SelectedIndexChanged">

                                         
                                            
                                           <asp:ListItem>电子上报</asp:ListItem>
                                           <asp:ListItem>纸档上报</asp:ListItem>

                                        </asp:DropDownList>
                                        文档名称：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        备注：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                        <asp:FileUpload ID="FileUpload1"  runat="server"  />
                                        <asp:Button ID="Button5" runat="server" CausesValidation="false" OnClick="Button5_Click"
                                            Text="保存"  />
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td  >
                                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" CaptionAlign="Left"
                                            DataKeyNames="id" OnRowDeleting="GridView5_RowDeleting"  >
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="urltext" HeaderText="文件名"  DataTextField="filename">
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:HyperLinkField>
                                                <asp:BoundField DataField="leibie" HeaderText="文档类型" />
                                                <asp:BoundField DataField="typ" HeaderText="文件类型"   />
                                                <asp:BoundField DataField="fillname" HeaderText="上传人"   />
                                                <asp:BoundField DataField="filltime" HeaderText="上传日期" DataFormatString ="{0:d}"   />

                                                <asp:CommandField DeleteText="删除" HeaderText="删除" ShowDeleteButton="True" />
                                              
                                                <asp:BoundField DataField="baojiaid" HeaderText="文档名称"  />
                                                  <asp:BoundField DataField="pub_field3" HeaderText="备注"  />
                                            </Columns>
                                            <HeaderStyle CssClass="Admin_Table_Title " />   

                                        </asp:GridView>
                                    </td>
                                </tr>
                                

                              <tr>
                                    <td   align ="center" >
                                        <asp:Button ID="Button1" runat="server" Text="打印上报文件目录" 
                                            onclick="Button1_Click" />
                                     </td>
                                </tr>

                                
                                </table> 
    </div>
    </form>
</body>
</html>

