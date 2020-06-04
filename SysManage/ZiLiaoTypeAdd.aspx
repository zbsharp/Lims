<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZiLiaoTypeAdd.aspx.cs" Inherits="SysManage_ZiLiaoTypeAdd" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	
</title>

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
           系统管理 》》资料录入</div>	
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
            <td align="left">
                <asp:TextBox ID="TextBox6"  runat="server" ontextchanged="TextBox6_TextChanged"></asp:TextBox>
            </td>
            <td align="left" style="width: 90px">
                &nbsp;</td>
            <td align="left">
                &nbsp;</td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="TextBox5" runat="server" Width="90%" Visible ="false" ></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" colspan="4" style="text-align: center">
                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                    OnClick="Button1_Click" Text="保 存" />
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    


       </div> 

</form>
</body>
</html>

