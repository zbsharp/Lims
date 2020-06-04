<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShiXiaoPeiZhiSee.aspx.cs" Inherits="ShiXiao_ShiXiaoPeiZhiSee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>时效配置</title>
   <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>

      <script type="text/javascript">


          var currentRowId = 0;
          function SelectRow() {
              if (event.keyCode == 40)
                  MarkRow(currentRowId + 1);
              else if (event.keyCode == 38)
                  MarkRow(currentRowId - 1);
          }

          function MarkRow(rowId) {
              if (document.getElementById(rowId) == null)
                  return;

              if (document.getElementById(currentRowId) != null)
                  document.getElementById(currentRowId).style.backgroundColor = '#ffffff';

              currentRowId = rowId;
              document.getElementById(rowId).style.backgroundColor = '#FFE0C0';
          }
          function text() {
              document.getElementById("bnClick").click();
          }
   
    
  
    
    </script>


</head>
<body>
    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

    <div>
      <div class="Body_Title">
          时效管理 》》时效配置</div>

           <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                任务编号：</td>
            <td align="left">
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
            <td align="left" style="width: 90px">
            </td>
            <td align="left">
            </td>
        </tr>
      <tr bgcolor="#f4faff">
         <td align="left" style="width: 90px" >
             开始时间：</td>
          <td align="left">
              <asp:TextBox ID="TextBox1" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox1,'yyyy-mm-dd')"></asp:TextBox></td>
          <td align="left" style="width: 90px">
              截至时间：</td>
          <td align="left">
              <asp:TextBox ID="TextBox2" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox2,'yyyy-mm-dd')"></asp:TextBox></td>
    </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                提出天数：</td>
            <td align="left">
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
            <td align="left" style="width: 90px">
                允许天：</td>
            <td align="left">
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                预警天：</td>
            <td align="left" colspan="3">
                <asp:TextBox ID="TextBox5" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                提交人：</td>
            <td align="left">
                <asp:TextBox ID="TextBox7" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td align="left" style="width: 90px">
                提交时间：</td>
            <td align="left">
                <asp:TextBox ID="TextBox8" runat="server" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" colspan="4" style="text-align: center">
                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                    OnClick="Button1_Click" Text="修 改" />
                <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="BnCss"
                    OnClick="Button2_Click" Text="删 除" />
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>



    </div>
    </form>
</body>
</html>
