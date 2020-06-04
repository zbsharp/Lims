<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Customer_Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script  type ="text/javascript"  src ="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox1,'yyyy-mm-dd')" ></asp:TextBox>
   
   
        <asp:TextBox ID="TextBox2" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox1,'yyyy-mm-dd')" ></asp:TextBox>
   
   
    </div>
    </form>
</body>
</html>
