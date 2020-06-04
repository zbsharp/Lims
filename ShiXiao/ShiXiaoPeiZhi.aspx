<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShiXiaoPeiZhi.aspx.cs" Inherits="ShiXiao_ShiXiaoPeiZhi" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
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
    <form name="form1"  runat="server"  id="form1">

      <div class="Body_Title">
          时效管理 》》时效配置</div>

<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

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
                <asp:TextBox ID="TextBox3" runat="server">2</asp:TextBox></td>
            <td align="left" style="width: 90px">
                允许等待：</td>
            <td align="left">
                <asp:TextBox ID="TextBox4" runat="server" Width="148px">3</asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                预警天数：</td>
            <td align="left" colspan="3">
                <asp:TextBox ID="TextBox5" runat="server" Width="90%" Text ="2"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" colspan="4" style="text-align: center">
                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                    OnClick="Button1_Click" Text="保 存" />
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" colspan="4" style="text-align: left">
                1，提出天数：(下达后从第几天开始计算提出日期，默认下达后第二天)。<br />
                2，(提出要求后从第几天开始计算暂停，即允许等待客户的天数（11.20提出，则11.23要确认，否则暂停）)。<br />
                3，要求完成或者确认提前几天开始预警（这里资料暂停和任务完成预警天数共用一个时限）。<br />
                4，有任务号则根据任务号判断，否则根据下达日期所在时间段判断。</td>
        </tr>
    </table>
    


       </div> 

</form>
</body>
</html>

