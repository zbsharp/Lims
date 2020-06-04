<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">

    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <title>选择项目</title>
</head>
<script>
    function RtValue(rtstr) {

        window.returnValue = rtstr;
        window.close()
    }


</script>
<body>
    <form runat="server" id="fm1">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
            EnableScriptLocalization="false">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%; margin-right: 1px;">
                    <tr style="width: 100%; overflow: scroll;">
                        <td style="width: 20%;" valign="top">

                            <asp:TextBox ID="TextBox1" runat="server" Visible="true"></asp:TextBox>
                            <asp:Button ID="Button_search" runat="server" Text="查询"
                                OnClick="Button_search_Click" Visible="true" />


                            <asp:Button ID="Button1" runat="server" Text="查询" Visible="false" CssClass="BnCss" OnClick="Button1_Click" />


                            <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:TreeView ID="TreeView1" runat="server" ShowLines="true" NodeIndent="40">
                                    </asp:TreeView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>


                        <td valign="top" style="height: 450px;">




                            <iframe name="content3" src="Default3.aspx?baojiaid=<%=baojiaid) %>&&kehuid=<%=kehuid %>" frameborder="0" style="width: 100%;"
                                height="450px" runat="server" id="detailm"></iframe>
                        </td>

                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
