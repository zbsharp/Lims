<%@ Page Language="C#" AutoEventWireup="true" EnableViewState ="true"  CodeFile="CustomerConfig.aspx.cs" Inherits="CCSZJiaoZhun_BaseData_CustomerConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>      

    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>  


<link href="../css.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
            系统管理 》》资料录入</div>	
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
        </asp:ScriptManager>
      
    


   
   
      资料名称： <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
       <asp:Button ID="Button6" runat="server" CssClass ="BnCss" Text="资料名称" OnClick="Button6_Click" />
       
       <asp:GridView ID="GridView6" AutoGenerateColumns ="False"  Width ="100%" runat="server" CssClass="Admin_Table" GridLines="None" DataKeyNames="id" OnRowDeleting="GridView6_RowDeleting">
            <HeaderStyle CssClass="Admin_Table_Title " />
           <Columns >
            <asp:BoundField DataField="id" HeaderText="编号" />
            <asp:BoundField DataField="name" HeaderText="名称" />
            <asp:CommandField  ShowDeleteButton ="True" />
           </Columns>
       </asp:GridView>
       

   
    
    
    </form>
</body>
</html>
