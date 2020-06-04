<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Discount.aspx.cs" Inherits="SysManage_Discount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
     <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div class="Body_Title">
            综合管理 》》权限折扣</div>	
      
                    <table class="Admin_Table">
                     
                        <tr bgcolor="#f4faff">
                            <td align="left" class="style3" style="width: 294px; height: 22px;" >
                                客户规模:</td>
                            <td align="left" colspan="3" style="height: 22px">
                                <asp:TextBox ID="TextBox1" runat="server" Width="148px">0</asp:TextBox>
                                <asp:TextBox ID="TextBox2" runat="server" Width="150px" OnTextChanged="TextBox2_TextChanged">0</asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td align="left" class="style3" style="height: 20px; width: 294px;">
                                建议折扣:</td>
                            <td align="left" colspan="3" style="height: 20px">
                                <asp:TextBox ID="TextBox3" runat="server">1</asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td align="left" class="style3" style="width: 294px" >
                                业务员折扣:</td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="TextBox4" runat="server">1</asp:TextBox></td>
                        </tr>
                        
                           <tr bgcolor="#f4faff">
                            <td align="left" class="style3" style="width: 294px" >
                                经理折扣：</td>
                               <td align="left" colspan="3">
                                   <asp:TextBox ID="TextBox5" runat="server">1</asp:TextBox></td>
                        </tr>
                        
                        <tr bgcolor="#f4faff">
                            <td align="left" class="style3" style="width: 294px" >
                                领导折扣:</td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="TextBox6" runat="server">1</asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td align="center" colspan="4" style="height: 20px">
                                <asp:Button ID="Button1" runat="server" Text="保存"  CssClass ="BnCss" OnClick="Button1_Click"/></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td align="left" class="style1" colspan="4">
                               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="Admin_Table"  DataKeyNames="id"
                                    Style="font-size: 9pt" Width="100%" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                                  
                                    <Columns>
                                      
                                        <asp:BoundField DataField="amount1" HeaderText="年度金额起" />
                                       <asp:BoundField DataField="amount2" HeaderText="年度金额止" />
                                        <asp:BoundField DataField="advicediscount" HeaderText="建议折扣" />
                                    
                                       <asp:BoundField DataField="salesman" HeaderText="业务员" />
                                        <asp:BoundField DataField="head" HeaderText="经理" />
                                       
                                      <asp:BoundField DataField="leadship" HeaderText="领导" />
                                        <asp:CommandField HeaderText="操作"  ShowDeleteButton ="true"  ShowEditButton ="true" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                            
                        </tr>
                        </table>
               
    </div>
    </form>
</body>
</html>

