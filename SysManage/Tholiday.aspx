<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tholiday.aspx.cs" Inherits="SysManage_Tholiday" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> </title> 
	
<link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>   
        <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form name="form1"  runat="server"  id="form1">
<div>
  <div class="Body_Title">
            系统管理 》》工作日表</div>	
<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

     
    
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 100px;">
                                日期：</td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="TextBox1" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox1,'yyyy-mm-dd')"></asp:TextBox></td>
                            <td colspan="1" style="width: 100px; text-align: left">
                                备注：</td>
                            <td colspan="1" style="text-align: left">
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                        </tr>
       
        <tr bgcolor="#f4faff">
            <td colspan="6" style="text-align: center">
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                                    Text="保 存" OnClick="Button1_Click" /></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td colspan="6" style="text-align: left">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table"
                            Width="100%" DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                            <RowStyle CssClass="textcenter" ForeColor="#000066" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                            CommandName="BussinessNeeds" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="hdate" HeaderText="日期"  />
                                <asp:BoundField DataField="name" HeaderText="名字" />
                         
                                <asp:CommandField HeaderText="编辑" ShowDeleteButton="True" ShowEditButton="True" >
                                                    <ItemStyle ForeColor="Blue" />
                                                </asp:CommandField>
                            </Columns>
                           <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
                    </table>
    


       </div> 

</form>
</body>
</html>

