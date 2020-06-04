<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZiLiaoTypeSee.aspx.cs" Inherits="SysManage_ZiLiaoTypeSee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>  

</head>
<body>
     <form name="form1"  runat="server"  id="form1">
<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

         <div class="Body_Title">
           系统管理 》》资料修改</div>	
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
      <tr bgcolor="#f4faff">
         <td align="left" style="width: 90px" >
             名称：</td>
          <td align="left">
              <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
          <td align="left" style="width: 90px">
              检测类别：</td>
          <td align="left">
              <asp:DropDownList ID="DropDownList1" runat="server" Width="154px">
              </asp:DropDownList></td>
    </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                一般要求：</td>
            <td align="left">
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
            <td align="left" style="width: 90px">
                适应情况：</td>
            <td align="left">
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                适应部门：</td>
            <td align="left">
                <asp:DropDownList ID="DropDownList2" runat="server" Width="154px">
                </asp:DropDownList></td>
            <td align="left" style="width: 90px">
                紧急情况：</td>
            <td align="left">
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                顺序：</td>
            <td align="left" colspan="3">
                <asp:TextBox ID="TextBox5" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" colspan="4" style="text-align: center">
                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                    OnClick="Button1_Click" Text="修 改" />
               
                <asp:Button ID="Button2" runat="server" Text="删除" onclick="Button2_Click" />
               
            </td>
        </tr>
    </table>
    


       </div> 

</form>
</body>
</html>
