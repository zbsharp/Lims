<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Notice.aspx.cs" Inherits="SysManage_Notice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../css.css" type="text/css" rel="stylesheet" />

</head>
<body>
 <form id="form1" runat="server">
    
    <div style="font-size: 9pt">
    <table border="0" cellpadding="0" cellspacing="0" bordercolor="#111111" width="100%">
				<tr height="30">
					<td width="3%" background="../Images/topbg.jpg" align=center style="height: 30px"><IMG height="16" src="../Images/icon/right.GIF"></td>
					<td background="../Images/topbg.jpg" width="80" style="height: 30px"><b>
                        <asp:Label ID="Label2" runat="server" Text="发布通知" Width="92px"></asp:Label></b></td>
					<TD background="../Images/topbg.jpg" align="right" style="height: 30px">
					</TD>
				</tr>
			</table>
        <table align="center" style="width: 560px; height: 300px" >
           <tr  valign ="middle" height ="60px"  >
                <td   align ="center"colspan="4" >
                    <asp:TextBox ID="TextBox1" runat="server" TextMode ="MultiLine" Height="211px" Width="519px" ></asp:TextBox></td>
            </tr>

        
            <tr  valign ="middle"  >
                <td align="center" colspan="4">
                    &nbsp;<asp:Button ID="Button3"  CssClass="BnCss" runat="server" Text="发 布" Width="53px" OnClick="Button3_Click" />
                    &nbsp;&nbsp;&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

