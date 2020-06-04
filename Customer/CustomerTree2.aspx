<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerTree2.aspx.cs" Inherits="Customer_CustomerTree2" %>

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
                      </div>

<div style ="height:600px; overflow:scroll ;">

    <asp:TreeView ID="TreeView1" runat="server" EnableClientScript="true" OnTreeNodePopulate="PopulateNode"  ExpandDepth="0">
            <Nodes>
                <asp:TreeNode Text="公司部门1" Value="公司部门1" SelectAction="Expand" PopulateOnDemand="true"/>
                <asp:TreeNode Text="公司部门2" Value="公司部门2" SelectAction="Expand" PopulateOnDemand="true"/>
            </Nodes>
        </asp:TreeView>


</div>



</form>
</body>
</html>
