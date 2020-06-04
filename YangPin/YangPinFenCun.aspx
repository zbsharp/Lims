<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YangPinFenCun.aspx.cs" Inherits="YangPin_YangPinFenCun" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>样品封存</title></head>

      <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
   



<body>
    <form id="form1" runat="server">

     <div class="Body_Title">
       样品管理 》》样品封存</div>



       <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                样品编号：</td>
                            <td style="text-align: left">
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
                            <td style="text-align: left; width: 90px;">
                                封存编号：</td>
                            <td style="text-align: left">
                               <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                        </tr>
                        
                          <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                备注：</td>
                            <td style="text-align: left">
                               <asp:TextBox ID="TextBox2"
        runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 90px;">
                                封存日期：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                              </td>
                        </tr>
                        
                          <tr bgcolor="#f4faff">
                            <td style="text-align: center; " colspan="4">
                              <asp:Button ID="Button1"
        runat="server" Text="保存" onclick="Button1_Click" /></td>
                        </tr>
                        
                        </table> 



  
    </form>
</body>
</html>
