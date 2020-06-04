<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Customer_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
      <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods ="true"  runat="server">
    </asp:ScriptManager>
    <div>
        到

            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="500"
        Width="100%">
        <cc1:TabPanel runat="server" HeaderText="标题" ID="TabPanel1">
            <HeaderTemplate>
                新增信息</HeaderTemplate>
            <ContentTemplate>
             <input id="txFDate" name="txFDate" class="TxCss" type="text" visible ="true"  value="" onclick="popUpCalendar(this,document.forms[0].txFDate,'yyyy-mm-dd')" readonly  runat="server" style="width: 122px" />
       
            
            </ContentTemplate> </cc1:TabPanel></cc1:TabContainer>  
    </div>
    </form>
</body>
</html>
