<%@ Page Language="C#" AutoEventWireup="true" CodeFile="downs.aspx.cs" Inherits="downs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>结果返回页</title>
<script type="text/javascript" >
    function cc(infor_id, infor_name, infor_psw) //参数分别为id,name和password
    {
        window.returnValue = infor_id + "|&|" + infor_name + "|&|" + infor_psw;   //返回值    
        window.close();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="gvshow" runat="server" BackColor="White" BorderColor="#CCCCCC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            onrowdatabound="gvshow_RowDataBound" >
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <RowStyle ForeColor="#000066" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>

