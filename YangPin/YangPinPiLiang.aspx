<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YangPinPiLiang.aspx.cs" Inherits="YangPin_YangPinPiLiang" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	批量操作
</title>  <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js">
<script language="javascript" src="popcalendar.js"></script>
	<script language="javascript" src="../../Js/calendar.js" type="text/javascript">
function bnQuery_onclick() {

}

        </script>
         <script type="text/javascript" src="../Celend/popcalendar.js"></script>
         
<script type ="text/javascript" >
    function add() {
        var a = parseFloat(document.getElementById("TextBox8").value) * parseFloat(document.getElementById("TextBox9").value);

        if (a != null) {
            document.getElementById("TextBox10").value = a;
        }
    }
</script>

</head>
<body>
    <form name="form1"  runat="server"  id="form1">
      <div class="Body_Title">
       业务受理 》》样品批量处理</div>

<div>


<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>
     
    
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 110px;">
                              样品编号：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox1" runat="server" Width="90%"></asp:TextBox></td>
                                <td style="text-align: left; width: 110px;">
                                    操作方向：</td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                    <asp:ListItem>借出</asp:ListItem>
                                    <asp:ListItem>清退</asp:ListItem>
                                    <asp:ListItem>归还</asp:ListItem>
                                    <asp:ListItem>销毁</asp:ListItem>
                                </asp:DropDownList>
                                交接人：<asp:TextBox ID="TextBox2" runat="server" Width="34%"></asp:TextBox>备注：<asp:TextBox 
                                    ID="TextBox3" runat="server"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="保存" />
                            </td>
                        </tr>
                       
        <tr bgcolor="#f4faff">
            <td colspan="4" style="text-align: left">
                
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" 
                            Width="100%">
                            
                              <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                                            CommandName="BussinessNeeds" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="sampleid" HeaderText="样品编号" />
                                                <asp:BoundField DataField="yangpinname" HeaderText="样品名称" />
                                                <asp:BoundField DataField="name" HeaderText="借出人" />
                                                <asp:BoundField DataField="time" DataFormatString="{0:d}" HeaderText="借出时间" />
                                                <asp:BoundField DataField="remark" HeaderText="备注" />
                                                <asp:BoundField DataField="state" HeaderText="状态" />
                                            </Columns>
                           <HeaderStyle CssClass="Admin_Table_Title " />
                            <EmptyDataTemplate>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时没有数据"></asp:Label>
                            </EmptyDataTemplate>
                         
                        </asp:GridView>
                   
            </td>
        </tr>
                    </table>
    


       </div> 

</form>
</body>
</html>