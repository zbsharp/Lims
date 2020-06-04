<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerRequestSee.aspx.cs" Inherits="Customer_CustomerRequestSee" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div style ="text-align :center ">
        <asp:GridView ID="GridView1" AutoGenerateColumns ="false"  runat="server" Width="100%" CssClass="Admin_Table">
        <Columns >
           <asp:BoundField DataField="kehuname"  HeaderText="客户名称" />
                <asp:BoundField DataField="beizhu"  HeaderText="操作理由" />
                <asp:BoundField DataField="biaoji"  HeaderText="处理标记" />
                <asp:BoundField DataField="fillname"  HeaderText="填写人" />
               
                <asp:BoundField DataField="filltime" HeaderText="录入时间" DataFormatString="{0:d}"  />
        </Columns>
        <HeaderStyle CssClass="Admin_Table_Title " />
        <EmptyDataTemplate>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
