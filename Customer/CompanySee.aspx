<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanySee.aspx.cs" Inherits="Customer_CompanySee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	
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

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

       <div class="Body_Title">
        销售管理 》》协议编辑</div>
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
      <tr bgcolor="#f4faff">
         <td align="left" style="width: 90px" >
             企业编号：</td>
          <td align="left">
              <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
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
                    OnClick="Button1_Click" Text="修 改" />
            </td>
        </tr>
    </table>
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
      <tr bgcolor="#f4faff">
         <td >
             <strong>附件管理：</strong></td>
      </tr>
        <tr bgcolor="#f4faff">
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" Width="40%" />
                <asp:Button ID="Button2" runat="server" CausesValidation="false" CssClass="BnCss"
                    OnClick="Button2_Click" Text="上传" Width="49px" />
                <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                    DataKeyNames="id" OnRowDeleting="GridView2_RowDeleting"
                    Width="99%" CssClass="Admin_Table" ShowHeader="true">
                  <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="urltext" DataTextField="filename">
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:HyperLinkField>
                                                <asp:BoundField DataField="leibie" HeaderText="附件类型" />
                                                <asp:BoundField DataField="typ" HeaderText="文件类型" />
                                                <asp:BoundField DataField="fillname" HeaderText="上传人"  Visible ="false" />
                                                <asp:CommandField DeleteText="删除附件" HeaderText="删除" ShowDeleteButton="TRUE" />
                                                <asp:BoundField DataField="caseid" HeaderText="附件编号" Visible="False" />
                                            </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="暂无附件"></asp:Label>
                    </EmptyDataTemplate>
                </asp:GridView>
               
            </td>
        </tr>
    </table>


       </div> 

</form>
</body>
</html>
