<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChuCuoAdd.aspx.cs" Inherits="Report_ChuCuoAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	报告退回录入
</title>

 <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <script type="text/javascript" src="../js/calendar.js"></script>
</head>
<body>
    <form name="form1"  runat="server"  id="form1">
<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

        <div class="Body_Title">
            报告管理 》》出错信息录入</div>
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                报告编号：</td>
            <td align="left">
                <asp:TextBox ID="TextBox2" runat="server"  ></asp:TextBox>报告退回日期：<asp:TextBox 
                    ID="TextBox11" runat="server" onclick="new Calendar().show(this.form.TextBox11);"></asp:TextBox>
            </td>
            <td align="left" style="width: 90px">
                申请编号：</td>
            <td align="left">
                <asp:TextBox ID="TextBox3" runat="server"  ></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                责任部门：</td>
            <td align="left">
                <asp:DropDownList ID="DropDownList1"  Width ="120"  AutoPostBack ="true" 
                    runat="server" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td align="left" style="width: 90px">
                责任工程师：</td>
            <td align="left">
               <asp:DropDownList ID="DropDownList2" Width ="120" runat="server">
                </asp:DropDownList></td>
        </tr>
      <tr bgcolor="#f4faff">
         <td align="left" style="width: 90px" >
             不合格分类：</td>
          <td align="left">
              <asp:DropDownList ID="DropDownList3"  Width ="160"  AutoPostBack ="true" 
                    runat="server" 
                  onselectedindexchanged="DropDownList3_SelectedIndexChanged" >
                </asp:DropDownList></td>
          <td align="left" style="width: 90px">
              不合格内容：</td>
          <td align="left">
              <asp:DropDownList ID="DropDownList4" Width ="160" runat="server">
                </asp:DropDownList></td>
    </tr>
      <tr bgcolor="#f4faff">
         <td align="left" style="width: 90px" >
             错误来源：</td>
          <td align="left">
              <asp:DropDownList ID="DropDownList5" runat="server">
                  <asp:ListItem>抽查</asp:ListItem>
                  <asp:ListItem>客户投诉</asp:ListItem>
                  <asp:ListItem>CQC</asp:ListItem>
              </asp:DropDownList>
          </td>
          <td align="left" style="width: 90px">
              &nbsp;</td>
          <td align="left">
              &nbsp;</td>
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
                    OnClick="Button1_Click" Text="保 存" />
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">

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
                                <asp:BoundField DataField="bumen" HeaderText="部门" />
                                <asp:BoundField DataField="gongchengshi" HeaderText="工程师"  />
                                <asp:BoundField DataField="fenlei" HeaderText="错误类别"   />
                                <asp:BoundField DataField="leirong" HeaderText="错误内容" />
                                <asp:BoundField DataField="beizhu1" HeaderText="备注" />
                                 

                                <asp:BoundField DataField="time" DataFormatString ="{0:d}" HeaderText="退回日期" />
                                <asp:BoundField DataField="beizhu2" HeaderText="错误来源" />
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


