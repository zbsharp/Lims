<%@ Page Language="C#" AutoEventWireup="true" EnableViewState ="true" MaintainScrollPositionOnPostback ="true" CodeFile="Oldquatation_In.aspx.cs" Inherits="CCSZJiaoZhun_Quatation_Oldquatation_In" %>
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
   
     <div>
     <table border="0" cellpadding="0" cellspacing="0" bordercolor="#111111" width="100%">
				<tr height="30">
					<td width="3%" background="../Images/topbg.jpg" align=center><IMG height="16" src="../Images/icon/right.GIF"></td>
					<td background="../Images/topbg.jpg" width="80"><b>导入信息</b></td>
					<TD background="../Images/topbg.jpg" align="right">
					</TD>
				</tr>
			</table>
    
    
     <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
    <tr bgcolor="#f4faff">
                <td style="width: 151px"><font face="宋体">请选择要导入的文件</font></td>
                <td style="width: 350px" align="left" width="350"><input id="FileExcel" style="width: 300px" type="file" size="42" name="filephoto" runat="server"><font color="red"></font></td>
                <td class="hint"><font face="宋体"><asp:button id="BtnImport" text="导 入"  CssClass ="BnCss" runat="server" OnClick="BtnImport_Click1"></asp:button></font><a href="../File/价格表格式要求.xls">请按照所提供表的格式要求并保证Excel为2003-97版本(点击下载)</a></td>
              </tr>
     <tr bgcolor="#f4faff">
                    <td class="hint" colspan="3">
      <asp:label id="LblMessage" runat="server" font-bold="True" forecolor="Red" Width="224px"></asp:label></td>
                </tr>
            </table>

    </div>
   
    </form>
</body>
</html>
