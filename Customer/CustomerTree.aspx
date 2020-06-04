<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerTree.aspx.cs" Inherits="Customer_CustomerTree" %>

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
</head>
<body>
     <form name="form1"  runat="server"  id="form1">
    <div class="Body_Title">
            销售管理 》》客户录入情况 <asp:TextBox ID="TextBox1" runat="server"  ></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Text="查询"   CssClass ="BnCss" OnClick="Button1_Click"/><asp:Button ID="Button2" runat="server" CssClass ="BnCss" onclick="Button2_Click" Text="全部展开" />
                        <asp:Button ID="Button3" runat="server" CssClass ="BnCss" Text="全部收起" onclick="Button3_Click" /></div>

<div style ="height:600px; overflow:scroll ;">

    <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" OnTreeNodePopulate="TreeView1_TreeNodePopulate">
        <Nodes>
            
        </Nodes>
    </asp:TreeView>


</div>



</form>
</body>
</html>
