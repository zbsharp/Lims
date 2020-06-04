<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GongChenAdd.aspx.cs" Inherits="SysManage_GongChenAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工程任务</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>   

</head>
<body>
     <form name="form1"  runat="server"  id="form1">
<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

     <div class="Body_Title">
            综合管理 》》工程任务</div>	
    
     <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
                            <td>
                                按名称查询：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="BnCss"
                                    Text="查 询" OnClick="Button2_Click" /></td>
                        </tr>
         <tr bgcolor="#f4faff">
             <td>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" DataKeyNames="id"
                                            OnRowDeleting="GridView1_RowDeleting"
                                            Style="font-size: 9pt" Width="98%" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                                          
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                                            CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="name" HeaderText="任务名称" />
                                                <asp:BoundField DataField="value" DataFormatString="{0:d}" HeaderText="任务名称" />
                                                <asp:BoundField DataField="luruname" HeaderText="录入人" ReadOnly="True" />
                                                <asp:BoundField DataField="lurutime" DataFormatString="{0:d}" HeaderText="录入时间" ReadOnly="True" />
                                                 <asp:CommandField HeaderText="编辑" ShowDeleteButton="True" ShowEditButton="True" >
                                                    <ItemStyle ForeColor="Blue" />
                                                </asp:CommandField>
                                            </Columns>
                                            
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                                            </EmptyDataTemplate>
                                           <HeaderStyle CssClass="Admin_Table_Title " />
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
             </td>
         </tr>
    </table>
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 88px;">
                                任务名称：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 88px;">
                                任务名称：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox2" runat="server" ></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td colspan="4" style="text-align: center">
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                                    Text="保 存" OnClick="Button1_Click" />&nbsp;
                            </td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td colspan="4" style="text-align: left">
                                &nbsp;</td>
                        </tr>
                    </table>


       </div> 

</form>
</body>
</html>
