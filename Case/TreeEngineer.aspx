﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreeEngineer.aspx.cs" Inherits="Case_TreeEngineer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title>
	
</title>
  

 <head id="Head1" runat="server">
    <title></title>

     <style  type ="text/css" >
    
     #mytable {   
    padding: 0;
    margin: 0;   
    border-collapse:collapse;
}
#mytable tr {
    height:5px;
}

#mytable td {
     border: 1px solid #C1DAD7;   
    background: #fff;
    font-size:11px;
    padding: 2px 2px 2px 2px;
    color: #4f6b72;
    width:170px;
}

#mytable td.alt {
    background: #F5FAFA;
    color: #797268;
}
.b
{
  cursor :hand;
}
    </style>


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
            工程管理 》》工作任务(该页面显示会有点慢) <asp:TextBox ID="TextBox1" runat="server" Visible="false"  ></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Text="查询"  Visible="false"   CssClass ="BnCss" OnClick="Button1_Click"/><asp:Button ID="Button2" runat="server" CssClass ="BnCss" onclick="Button2_Click" Text="全部展开" />
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



     








</form>
</body>
</html>
