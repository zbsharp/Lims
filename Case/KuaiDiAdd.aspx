<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KuaiDiAdd.aspx.cs" Inherits="Case_KuaiDiAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	快递登记
</title>

 <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js">
    
   
    
    </script>


     <script language="javascript" for="document" event="onkeydown" type="text/javascript">
   if(event.keyCode==13 && event.srcElement.type!='button' && event.srcElement.type!='submit' && event.srcElement.type!='reset' && event.srcElement.type!='textarea' && event.srcElement.type!="")
     event.keyCode=9;
</script>

</head>
<body>
    <form name="form1"  runat="server"  id="form1">
<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

        <div class="Body_Title">
            业务管理 》》快递登记</div>
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
              
              <asp:DropDownList ID="DropDownList1" AutoPostBack ="true"  runat="server" 
                  onselectedindexchanged="DropDownList1_SelectedIndexChanged">
              </asp:DropDownList>
              
              </td>
          <td align="left" style="width: 90px">
              收件电话：</td>
          <td align="left">
              <asp:DropDownList ID="DropDownList2" runat="server">
              </asp:DropDownList>
              
              
              </td>
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
                <asp:Button ID="Button3" runat="server" Text="Button"  Visible ="false"  />


                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                    OnClick="Button1_Click" Text="保 存" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              
              
              <asp:Button ID="Button2" runat="server" Text="增加联系人" onclick="Button2_Click" />
              
              
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
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

