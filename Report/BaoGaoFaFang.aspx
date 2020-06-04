<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaoGaoFaFang.aspx.cs" Inherits="Report_BaoGaoFaFang" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>报告发放记录
    </title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>


    <style type="text/css">
        .BnCss {
        }
    </style>


</head>
<body>
    <form name="form1" runat="server" id="form1">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
            </asp:ScriptManager>
            <div class="Body_Title">
                报告管理 》》发放报告
            </div>
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                width="100%">
                <tr bgcolor="#f4faff">
                    <td align="left" style="width: 90px">任务编号：</td>
                    <td align="left">
                        <asp:TextBox ID="TextBox9" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="left" style="width: 90px">申请编号：</td>
                    <td align="left">
                        <asp:TextBox ID="TextBox10" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td align="left" style="width: 90px">报告编号：</td>
                    <td align="left">
                        <asp:TextBox ID="TextBox7" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="left" style="width: 90px">&nbsp;</td>
                    <td align="left">&nbsp;</td>
                </tr>
                <tr bgcolor="#f4faff">
                     <td align="left" colspan="4" style="text-align: center">
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                            OnClick="Button1_Click" Text="确定" Height="21px" Width="75px" />
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