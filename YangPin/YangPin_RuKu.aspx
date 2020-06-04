<%@ Page Language="C#" AutoEventWireup="true" EnableViewState ="true" CodeFile="YangPin_RuKu.aspx.cs" Inherits="YangPin_RuKu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	
</title>  <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js">
<script language="javascript" src="popcalendar.js"></script>

         


</head>
<body>
    <form name="form1"  runat="server"  id="form1">
       <div class="Body_Title">
       业务受理 》》样品录入</div>

<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

       <table border="0" cellpadding="0" cellspacing="0" bordercolor="#111111" width="100%" class="px12">
				<tr height="30">
					<td width="3%" background="../Images/topbg.jpg" align=center><IMG height="16" src="../Images/icon/right.GIF"></td>
					<td background="../Images/topbg.jpg" style="width: 58px"><b><asp:Label runat="server" ID="Label1" Text="样品入库明细" Width="125px" ></asp:Label></b></td>
					<TD background="../Images/topbg.jpg" align="right">
					</TD>
				</tr>
			</table>
    
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                样品编号：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 90px;">
                                样品名称：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                入库人：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 90px;">
                                入库时间：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox4" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox4,'yyyy-mm-dd')"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                备注：</td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="TextBox5" runat="server" Width="90%"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td colspan="4" style="text-align: center">
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                                    Text="提交" OnClick="Button1_Click" />
                                <asp:TextBox ID="TextBox6" runat="server" ToolTip="状态" Visible="False" Width="3px"></asp:TextBox></td>
                        </tr>
        <tr bgcolor="#f4faff">
            <td colspan="4" style="text-align: left">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table"  OnRowDataBound="GridView1_RowDataBound"
                                            Width="100%">
                                           
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                                            CommandName="BussinessNeeds" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="sampleid" HeaderText="样品编号" />
                                                <asp:BoundField DataField="yangpinname" HeaderText="样品名称" />
                                                <asp:BoundField DataField="name" HeaderText="入库人" />
                                                <asp:BoundField DataField="time" DataFormatString="{0:d}" HeaderText="入库时间" />
                                                <asp:BoundField DataField="remark" HeaderText="备注" />
                                            </Columns>
                                          <HeaderStyle CssClass="Admin_Table_Title " />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时没有数据"></asp:Label>
                                            </EmptyDataTemplate>
                                          
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
            </td>
        </tr>
                    </table>
    


       </div> 

</form>
</body>
</html>
