<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoticeManage.aspx.cs" Inherits="SysManage_NoticeManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
     <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>
    
      <div class="Body_Title">
            综合管理 》》通知列表</div>	
     <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
    <tr bgcolor="#f4faff">
    
    <td align="left" style="height: 25px">
        &nbsp;&nbsp; &nbsp; 时间：<input id="txFDate" name="txFDate" class="TxCss" type="text" value="" onclick="popUpCalendar(this,document.forms[0].txFDate,'yyyy-mm-dd')"  runat="server" style="width: 100px" />
        到&nbsp;<input id="txTDate" name="txTDate" class="TxCss" type="text" value="" onclick="popUpCalendar(this,document.forms[0].txTDate,'yyyy-mm-dd')"  runat="server" style="width: 100px" />
        查询条件：<asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
            <asp:ListItem Value="0">全    选</asp:ListItem>
            <asp:ListItem Value="1">录入人</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button2"  CssClass ="BnCss"  runat="server" 
            onclick="Button2_Click" Text="查询" /></td>
    </tr>
         <tr bgcolor="#f4faff">
             <td align="left">
    
    
   
                   <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
               <ContentTemplate>
                   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound"
                       Style="font-size: 9pt" Width="100%" OnRowDeleting="GridView1_RowDeleting">
                     
                       <Columns>
                           <asp:TemplateField HeaderText="序号">
                               <ItemTemplate>
                                   <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                       CommandName="BussinessNeeds" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                               </ItemTemplate>
                               <ItemStyle ForeColor="Green" />
                           </asp:TemplateField>
                           <asp:BoundField DataField="name" HeaderText="录入人" />
                           <asp:BoundField DataField="time" HeaderText="录入时间" DataFormatString="{0:d}" />
                           <asp:BoundField DataField="notice" HeaderText="信息" >
                               <ItemStyle Width="55%" />
                           </asp:BoundField>
                          
                           <asp:CommandField HeaderText="删除" ShowDeleteButton="True"    />
                       </Columns>
                     
                       <EmptyDataTemplate>
                           <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时没有数据"></asp:Label>
                       </EmptyDataTemplate>
                      <HeaderStyle CssClass="Admin_Table_Title " />
                   </asp:GridView>
                 
               </ContentTemplate>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
             </Triggers></asp:UpdatePanel> 
                                                    
                   

  
             </td>
         </tr>
    </table>
    </form>
</body>
</html>
