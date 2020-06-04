<%@ Page Language="C#" AutoEventWireup="true" EnableViewState ="true"  CodeFile="PersonConfig.aspx.cs" Inherits="sysManage_personConfig" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>

     <script type="text/javascript">


         var currentRowId = 0;
         function SelectRow() {
             if (event.keyCode == 40)
                 MarkRow(currentRowId + 1);
             else if (event.keyCode == 38)
                 MarkRow(currentRowId - 1);
         }

         function MarkRow(rowId) {
             if (document.getElementById(rowId) == null)
                 return;

             if (document.getElementById(currentRowId) != null)
                 document.getElementById(currentRowId).style.backgroundColor = '#ffffff';

             currentRowId = rowId;
             document.getElementById(rowId).style.backgroundColor = '#FFE0C0';
         }
         function text() {
             document.getElementById("bnClick").click();
         }
   
    
  
    
    </script>


</head>
<body>
<form name="form1"  runat="server" id="form1">
	<div class="Body_Title">
            系统管理 》》人员配置</div>	
           
            <div>
<asp:DropDownList ID="DropDownList1" runat="server">
    <asp:ListItem>请选择</asp:ListItem>
    <asp:ListItem>部门一</asp:ListItem>
    <asp:ListItem>部门二</asp:ListItem>
</asp:DropDownList>
                <asp:Button ID="Button3" runat="server" Text="查询人员" onclick="Button3_Click" />
                <span style ="color :Red ">||</span>
将列表所选人
<asp:Button ID="Button1" runat="server" class="BnCss" Text="确定配置给" OnClick="Button1_Click1" /><asp:DropDownList ID="response" runat="server">
</asp:DropDownList>
<asp:Button ID="Button2" runat="server" Visible="false" class="BnCss" Text="跳至取消"
    OnClick="Button2_Click1" />
    
     <span style ="color :Red ">||-||</span>
    将下拉框中所选人<asp:Button ID="Button4" runat="server" Text="确定分配给" onclick="Button4_Click" />列表所选人
    
                .</div>
   
<div style ="height:450px; overflow :scroll ">
    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
        DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
        OnRowUpdating="GridView1_RowUpdating" CssClass="Admin_Table">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    全选<asp:CheckBox ID="CheckBox2" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged"
                        AutoPostBack="True" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="username" HeaderText="叶子结点" />
            <asp:BoundField DataField="departmentname" HeaderText="部门" />
        </Columns>
        <HeaderStyle CssClass="Admin_Table_Title " />
    </asp:GridView>
</div>

<asp:Literal ID="ld" runat="server"></asp:Literal>
</form>
</body>
</html>