<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KuaiDiSee.aspx.cs" Inherits="Case_KuaiDiSee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	快递查看
</title>
 <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form name="form1"  runat="server"  id="form1">
<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

        <div class="Body_Title">
            业务管理 》》快递编辑</div>
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                公司：</td>
            <td align="left" colspan="3">
                <asp:TextBox ID="TextBox1" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                编号：</td>
            <td align="left">
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
            <td align="left" style="width: 90px">
                寄件人：</td>
            <td align="left">
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                寄件电话：</td>
            <td align="left">
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
            <td align="left" style="width: 90px">
                寄件日期：</td>
            <td align="left">
                <asp:TextBox ID="TextBox5" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox5,'yyyy-mm-dd')"></asp:TextBox></td>
        </tr>
      <tr bgcolor="#f4faff">
         <td align="left" style="width: 90px" >
             收件人：</td>
          <td align="left">
              <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
          <td align="left" style="width: 90px">
              收件电话：</td>
          <td align="left">
              <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
    </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                收件地址：</td>
            <td align="left" colspan="3">
                <asp:TextBox ID="TextBox7" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                收件单位：</td>
            <td align="left" colspan="3">
                <asp:TextBox ID="TextBox8" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                备注：</td>
            <td align="left" colspan="3">
                <asp:TextBox ID="TextBox10" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" colspan="4" style="text-align: center">
                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                    OnClick="Button1_Click" Text="修 改" />
                <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="BnCss"
                    OnClick="Button2_Click" Text="删 除" />
                <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="顺打印" />
                <asp:Button ID="Button5" runat="server" onclick="Button5_Click" Text="德打印" />
                <asp:Button ID="Button6" runat="server" Text="打印清单" onclick="Button6_Click" />
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                类型：</td>
            <td align="left" colspan="3">
                <asp:DropDownList ID="DropDownList1" runat="server" 
                    onselectedindexchanged="DropDownList1_SelectedIndexChanged"  >
                    <asp:ListItem>报告</asp:ListItem>
                    <asp:ListItem>发票</asp:ListItem>
                    <asp:ListItem>样品</asp:ListItem>
                    <asp:ListItem>其他</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                内容：</td>
            <td align="left" colspan="3">
                <asp:TextBox ID="TextBox11" runat="server" Width="90%" ></asp:TextBox>
                <asp:DropDownList ID="DropDownList2" Visible ="false"  runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                备注：</td>
            <td align="left" colspan="3">
                <asp:TextBox ID="TextBox12" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" colspan="4" style="text-align: center">
                <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="BnCss" Text="提 交" OnClick="Button3_Click" /></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" colspan="4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="Admin_Table"  DataKeyNames="id"
                            OnRowDataBound="GridView1_RowDataBound" Style="font-size: 9pt" Width="100%" OnRowDeleting="GridView1_RowDeleting">
                           <HeaderStyle CssClass="Admin_Table_Title " />
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                            CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="bianhao" HeaderText="编号" />
                                <asp:BoundField DataField="leixing" HeaderText="类型" />
                                <asp:BoundField DataField="neirong" HeaderText="内容" />
                                <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                <asp:CommandField HeaderText="取消" ShowDeleteButton="True" >
                                            <ItemStyle ForeColor="Blue" />
                                        </asp:CommandField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    


       </div> 

</form>
</body>
</html>

