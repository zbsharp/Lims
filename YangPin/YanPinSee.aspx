<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YanPinSee.aspx.cs" Inherits="YangPin_YanPinSee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>配件修改</title>

      <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js">
    <script type="text/javascript" src="../Celend/popcalendar.js"></script>
    <script type="text/javascript" src="popcalendar.js"></script>   

</head>
<body>
    <form id="form1" runat="server">
   <div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

     <div class="Body_Title">
       业务受理 》》配件修改</div>
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
                            <td style="width: 90px" >
                                批次号编号：</td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True"></asp:TextBox></td>
                            <td style="width: 90px">
                                样品名称：</td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                        </tr>
        <tr bgcolor="#f4faff">
            <td style="width: 90px">
                规格：</td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
            <td style="width: 90px">
                数量：</td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td style="width: 90px">
                单位：</td>
            <td>
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
            <td style="width: 90px">
                购买日期：</td>
            <td>
                <asp:TextBox ID="TextBox6" runat="server"  onclick="popUpCalendar(this,document.forms[0].TextBox6,'yyyy-mm-dd')"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td style="width: 90px">
                生产厂家：</td>
            <td colspan="3">
                <asp:TextBox ID="TextBox7" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td style="width: 90px">
                备注：</td>
            <td colspan="3">
                <asp:TextBox ID="TextBox8" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td colspan="4" style="text-align: center">
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                                    Text="修 改" OnClick="Button1_Click" />
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

