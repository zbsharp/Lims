<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaoGaoShenPi22.aspx.cs" Inherits="Report_BaoGaoShenPi22" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	报告批准
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
            报告管理 》》报告批准</div>
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
     
       <tr bgcolor="#f4faff" style ="display :none;">

        <td align="left" style="width: 90px;" >
             上传正式报告：</td>

                                    <td  colspan="3" >
                                        <asp:DropDownList ID="DropDownList1" runat="server">

                                         <asp:ListItem>正式报告</asp:ListItem>
                                          
                                        </asp:DropDownList>
                                        <asp:FileUpload ID="FileUpload1"  runat="server"  />
                                        <asp:Button ID="Button5" runat="server" CausesValidation="false" OnClick="Button5_Click"
                                            Text="上传"  />
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr bgcolor="#f4faff">
                                    <td  colspan="4">
                                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" CaptionAlign="Left"
                                            DataKeyNames="id" OnRowDeleting="GridView5_RowDeleting"  >
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="urltext" DataTextField="filename" HeaderText="报告名称"> 
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:HyperLinkField>
                                                <asp:BoundField DataField="leibie" HeaderText="附件类型" />
                                                <asp:BoundField DataField="typ" HeaderText="文件类型" />
                                                <asp:BoundField DataField="fillname" HeaderText="上传人"  Visible ="false" />
                                                <asp:CommandField DeleteText="删除附件" HeaderText="删除"  Visible ="false"  ShowDeleteButton="True" />
                                                <asp:BoundField DataField="caseid" HeaderText="附件编号" Visible="False" />
                                            </Columns>
                                            <HeaderStyle CssClass="Admin_Table_Title " />   

                                        </asp:GridView>
                                    </td>
                                </tr>
                                
      <tr bgcolor="#f4faff" style ="display :none;">
         <td align="left" style="width: 90px" >
             审核人：</td>
          <td align="left">
              <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
          <td align="left" style="width: 90px">
              &nbsp;</td>
          <td align="left">
              &nbsp;</td>
    </tr>
        <tr bgcolor="#f4faff">
            <td align="left" style="width: 90px">
                批准日期：</td>
            <td align="left">
                <asp:TextBox ID="TextBox3" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox3,'yyyy-mm-dd')"></asp:TextBox></td>
            <td align="left" style="width: 90px">
              批准人：</td>
            <td align="left">
              <asp:TextBox ID="TextBox2" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left" colspan="4" style="text-align: center">
                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                    OnClick="Button1_Click" Text="提 交" />
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

