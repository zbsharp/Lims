<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomEmail.aspx.cs" EnableViewState ="true"   Inherits="Customer_CustomEmail" %>

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
    <div>
    		<table border="0" cellpadding="0" cellspacing="0" bordercolor="#111111" width="100%" class="px12">
				<tr height="30">
					<td width="3%" background="Images/topbg.jpg" align=center><IMG height="16" src="Images/icon/right.GIF"></td>
					<td background="Images/topbg.jpg" width="80"><b><asp:Label runat="server" ID="Label1" Text="您所选择的邮件客户" style="color:Black;
	font-size: 12px;" Width="164px" ></asp:Label></b></td>
					<TD background="Images/topbg.jpg" align="right">
					</TD>
				</tr>
			</table><div style ="text-align :center ;">
         <asp:TextBox ID="txtReceiver" runat="server" Text ="<%#tiaokuan%>" Width="353px" Font-Size="9pt" Height="129px"></asp:TextBox>
        </div> <div  style ="text-align:center "><a href ="mailto:<%#name%>"  style ="color :Red ;">发送邮件</a></div>
    </div>
    </form>
</body>
</html>
