<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductPriceAdd.aspx.cs" Inherits="SysManage_ProductPriceAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	
</title>
<link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>

</head>
<body>
    <form name="form1"  runat="server"  id="form1">
<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

                  <div class="Body_Title">系统管理 》》价格明细-新增项目</div>


    
   
    
   
                    <table class="Admin_Table">
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 111px;">
                                大类：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 111px;">
                                中类：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="width: 111px; text-align: left">
                                单位标示：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
                            <td style="width: 111px; text-align: left">
                                平衡点单价：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 111px;">
                                建议单价：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 111px;">
                                最低折扣：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 111px;">
                                部门：</td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="153px">
                                    <asp:ListItem>测试</asp:ListItem>
                                    
                                    <asp:ListItem>化学</asp:ListItem>
                                    <asp:ListItem>国际</asp:ListItem>
                                   <asp:ListItem>试验</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="text-align: left; width: 111px;">
                                其他信息：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 111px;">
                                </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="DropDownList1"  Visible ="false" runat="server" Width="153px">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>CNAS</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="text-align: left; width: 111px;">
                              </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox9" runat="server" Visible ="false"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="width: 111px; text-align: left">
                                备注：</td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="TextBox8" runat="server" Width="90%"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td colspan="4" style="text-align: center">
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                                    Text="提 交" OnClick="Button1_Click" />
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                    </ContentTemplate>
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

