<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CeShiFeiSee.aspx.cs" Inherits="CeShiFeiSee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>工程师上报费用</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form name="form1"  runat="server"  id="form1">
<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>
                     <div class="Body_Title">
        业务管理 》》修改上报费用</div>

    <div>
       
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
      <tr bgcolor="#f4faff">
         <td align="left" style="width: 90px" >
             类别：</td>
          <td align="left">
              <asp:DropDownList ID="DropDownList1" runat="server" Width="151px">
                <asp:ListItem>安全检测费</asp:ListItem>
                <asp:ListItem>电磁兼容检测费</asp:ListItem>
                <asp:ListItem>电磁兼容转报告核查</asp:ListItem>
                <asp:ListItem>电磁兼容转报告核查</asp:ListItem>
              </asp:DropDownList></td>
          <td align="left" style="width: 90px">
              项目：</td>
          <td align="left">
              <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
    </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                数量：</td>
            <td align="left">
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
            <td align="left" style="width: 90px">
                费用：</td>
            <td align="left">
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                日期：</td>
            <td align="left">
                <asp:TextBox ID="TextBox5" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox5,'yyyy-mm-dd')" ></asp:TextBox></td>
            <td align="left" style="width: 90px">
                填写人：</td>
            <td align="left">
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" colspan="4" style="text-align: center">
                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                    OnClick="Button1_Click" Text="修 改" />
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
