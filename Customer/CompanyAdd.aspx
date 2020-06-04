<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyAdd.aspx.cs" Inherits="Customer_CompanyAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	增加协议
</title>

        <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
 
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <script type="text/javascript" src="../js/calendar.js"></script>


</head>
<body>
    <form name="form1"  runat="server"  id="form1">
<div>



       <div class="Body_Title">
        销售管理 》》协议录入</div>
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
      <tr bgcolor="#f4faff">
         <td align="left" style="width: 90px" >
             客户编号：</td>
          <td align="left">
              <asp:TextBox ID="TextBox1" runat="server" ReadOnly ="true" ></asp:TextBox></td>
          <td align="left" style="width: 90px">
              标题：</td>
          <td align="left">
              <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
    </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                内容摘要：</td>
            <td align="left" colspan="3">
                <asp:TextBox ID="TextBox3" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                签署日期：</td>
            <td align="left">
                <asp:TextBox ID="TextBox4" runat="server" onclick="new Calendar().show(this.form.TextBox4);"></asp:TextBox></td>
            <td align="left" style="width: 90px">
                有效期：</td>
            <td align="left">
                <asp:TextBox ID="TextBox5" runat="server" onclick="new Calendar().show(this.form.TextBox5);"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                备注：</td>
            <td align="left" colspan="3">
                <asp:TextBox ID="TextBox6" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" colspan="4" style="text-align: center">
                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                    OnClick="Button1_Click" Text="保 存" />
              
            </td>
        </tr>

        <tr><td colspan ="4" align ="center" >
        
        
         <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" >
                        
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                        CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CompanyId" HeaderText="企业编号" />
                            <asp:BoundField DataField="Title" HeaderText="标题" />
                            <asp:BoundField DataField="Signdate" DataFormatString="{0:d}" HeaderText="签署日期" />
                            <asp:BoundField DataField="Enddate" DataFormatString="{0:d}" HeaderText="有效期" />
                            <asp:TemplateField HeaderText="明细">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton8" runat="server" Text="查看" ForeColor="blue" CommandArgument='<%# Eval("id") %>'
                                        CommandName="chakan"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>

                            <asp:CommandField DeleteText="删除" HeaderText="删除" ShowDeleteButton="TRUE" />

                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>

                         <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
        
        </td></tr>
    </table>
    


       </div> 

</form>
</body>
</html>
