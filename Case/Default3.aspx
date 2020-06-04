<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Case_Default3" %>
<%@ Register Assembly="EeekSoft.Web.PopupWin" Namespace="EeekSoft.Web" TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Test</title>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
    <div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" ontick="Timer1_Tick" Interval="10000" Enabled="true">
        </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel> 
    <asp:Label runat="server" ID="lb2"></asp:Label>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label runat="server" ID="lb"></asp:Label>
            <cc1:popupwin id="pw" runat="server" ShowAfter ="1" style="left: 63px; top: 99px" 
                DragDrop="False" Height="109px" 
                Width="214px" AutoShow="true"> </cc1:popupwin>
        </ContentTemplate>  
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
<mce:script type="text/javascript"><!--
        window.onload = null;//页面加载时不运行PopupWin
// --></mce:script>

