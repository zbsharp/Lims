<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FapiaoAdd.aspx.cs" Inherits="CCSZJiaoZhun_htw_FapiaoAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	开票
</title> 
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
            业务管理 》》发票录入</div>
    
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
                        <tr bgcolor="#f4faff" style ="display :none ;">
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
            <td style="width: 90px; text-align: left">
                状态：</td>
            <td style="text-align: left">
                <asp:DropDownList ID="DropDownList1" runat="server" Enabled ="false" >
                    <asp:ListItem>正常</asp:ListItem>
                    <asp:ListItem>借票</asp:ListItem>
                </asp:DropDownList>
            </td>
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
                                    Text="保 存" OnClick="Button1_Click" />
                               
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                                  
                               
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                    BackColor="White" BorderColor="#CCCCCC" DataKeyNames ="id" OnRowDeleting="GridView1_RowDeleting" BorderWidth="1px" CellPadding="3" 
                                    CssClass="Admin_Table"  
                                    Style="font-size: 9pt" Width="100%">
                                    <HeaderStyle CssClass="Admin_Table_Title " />
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                                    CommandArgument='<%# Eval("id") %>' CommandName="BussinessNeeds" 
                                                    ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle ForeColor="Green" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="fapiaono" HeaderText="发票号" />
                                        <asp:BoundField DataField="fapiaojine" HeaderText="发票金额" />
                                        <asp:BoundField DataField="lingpiaoren" HeaderText="领取人" />
                                        <asp:BoundField DataField="lingpiaotime" DataFormatString="{0:d}" 
                                            HeaderText="领取时间" />
                                        <asp:BoundField DataField="fillname" HeaderText="录入人" />
                                        <asp:BoundField DataField="filltime" DataFormatString="{0:d}" 
                                            HeaderText="录入时间" />
                                        <asp:BoundField DataField="taitou" HeaderText="抬头" />
                                        <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                        <asp:BoundField DataField="state" HeaderText="状态" />
                                        <asp:BoundField DataField="responser" HeaderText="业务员" />
                                        <asp:BoundField DataField="daid" HeaderText="到款编号" />
                                         <asp:TemplateField HeaderText="明细">
                                    <ItemTemplate>
                                        <span style="cursor: hand; color: Blue;" onclick="window.open('FapiaoSee.aspx?id=<%#Eval("id") %>','test','dialogWidth=850px;DialogHeight=360px;status:no;help:no;resizable:yes; dialogTop:120px;edge:raised;')">
                                            <asp:Label ID="seeLB" runat="server" Text="明细"></asp:Label></span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:CommandField HeaderText="取消" ShowDeleteButton="True"  />


                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="暂时没有数据"></asp:Label>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td colspan="4" style="text-align: center">
                                &nbsp;</td>
                        </tr>
                    </table>
    


       </div> 

</form>
</body>
</html>
