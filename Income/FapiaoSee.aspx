<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FapiaoSee.aspx.cs" Inherits="CCSZJiaoZhun_htw_FapiaoSee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 发票修改
	
</title><link href="../css.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>

    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>

    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>

       
       <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
         
<script type ="text/javascript" >
function add()
{
   var a=parseFloat(document .getElementById ("TextBox8").value) *parseFloat(document.getElementById ("TextBox9").value);

   if (a!=null)
   {
      document .getElementById ("TextBox10").value =a;
   }
}
</script>

</head>
<body>
    <form name="form1"  runat="server"  id="form1">
<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

      <div class="Body_Title">
            业务管理 》》发票修改</div>
    
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
        <tr bgcolor="#f4faff">
            <td colspan="4" style="text-align: left">
                申请编号：<asp:Label ID="Label3" runat="server"></asp:Label></td>
        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                发票编号：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 90px;">
                                发票金额：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                        </tr>
                        
        <tr bgcolor="#f4faff">
            <td style="width: 90px; text-align: left">
                状态：</td>
            <td style="text-align: left">
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
            <td style="width: 90px; text-align: left">
                业务员：</td>
            <td style="text-align: left">
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                抬头：</td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="TextBox7" runat="server" Width="90%"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                备注：</td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="TextBox8" runat="server" Width="90%"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td colspan="4" style="text-align: center">
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                                    Text="修 改" OnClick="Button1_Click" />
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                &nbsp;
                            </td>
                        </tr>


                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                领票人：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 90px;">
                                领票时间：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox4" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox4,'yyyy-mm-dd')"></asp:TextBox></td>
                        </tr>


                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                领票备注：</td>
                            <td style="text-align: left" >
                                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                            </td>

                             <td style="text-align: left; width: 90px;">
                                任务号：</td>
                            <td style="text-align: left" >
                                <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                            </td>


                        </tr>


                        <tr bgcolor="#f4faff">
                            <td style="text-align:center;" colspan="4">
                                <asp:Button ID="Button2" runat="server" Text="领票" onclick="Button2_Click" /></td>
                        </tr>


                    </table>
    
     <fieldset>
        <legend style="color: Red">借票专用</legend> 
        
         <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
           <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                流水号或付款人：</td>
                            <td style="text-align: left" >
                               
                                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                               
                            </td>

                             <td style="text-align: left; " >
                                 <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="查询" />
                            </td>
                            <td style="text-align: left; " >
                                <asp:Button ID="Button4" runat="server" Text="确定" onclick="Button4_Click" />

                                <asp:Button ID="Button5" runat="server" Text="取消" onclick="Button5_Click" />

                            </td>
                        </tr>

                           <tr bgcolor="#f4faff">
                           <td style="text-align:center; " colspan="4">
                                 <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" 
                      >
                        <Columns>
                            <asp:TemplateField HeaderText="序 号" Visible="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>'
                                        CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="liushuihao" HeaderText="流水号" ReadOnly="True" />
                            <asp:BoundField DataField="fukuanren" HeaderText="付款人" />
                            <asp:BoundField DataField="fukuanriqi" HeaderText="付款日期" />
                            <asp:BoundField DataField="danwei" HeaderText="币种" />
                            <asp:BoundField DataField="fukuanjine" HeaderText="付款金额" />

                            <asp:BoundField DataField="queren" HeaderText="确认" />

                            <asp:BoundField DataField="beizhu" HeaderText="备注" />
                            <asp:BoundField DataField="daoruren" HeaderText="导入人" />
                            <asp:BoundField DataField="daorutime" DataFormatString="{0:d}" HeaderText="导入时间" />
                            <asp:BoundField DataField="fapiaoleibie" HeaderText="批次" />

                            <asp:BoundField DataField="fapiaohao" HeaderText="发票" />

                        <asp:TemplateField>
                                    <HeaderTemplate>
                                        请选择
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                    
                    </td>
 

                        </tr>

        
        
        </fieldset> 

       </div> 

</form>
       
        
        
        
        
        
        </body>
</html>
