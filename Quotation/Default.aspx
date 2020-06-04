<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Quotation_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type ="text/javascript"  language="javascript">
            function CustomAdd()
            {
                window.showModalDialog("Default2.aspx?kehuid=1000001", "window", "dialogWidth:880px;DialogHeight=450px;status:no;help:no;resizable:yes;;");	
                
            }
		</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Button" />
    </div>
    </form>
</body>
</html>
