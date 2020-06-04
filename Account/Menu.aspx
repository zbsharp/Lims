<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Account_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="LeftNote">
      <div class="Menu"><%=ManageMenuTitle%></div>
        <div class="MenuNote" style="display:block;">
            <ul class="MenuUL">
                <%=ManageMenu %>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>


