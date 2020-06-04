<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update.aspx.cs" Inherits="Customer_Update" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
   
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <tr>
                                <td>
                                </td>
                                <td>
                                     下次跟踪日期<input id="Text1" runat="server" class="TxCss" name="txFDate" onclick="popUpCalendar(this,document.forms[0].Text1,'yyyy-mm-dd')"
                                         type="text" style="width: 148px" />
                                </td>
                            </tr>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="修改" />
    </div>

      <asp:Literal ID="ld" runat="server"></asp:Literal>

    </form>
</body>
</html>
