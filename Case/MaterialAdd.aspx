<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialAdd.aspx.cs" Inherits="Case_MaterialAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>录入资料清单</title>

    
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
 
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="Body_Title">
            业务管理 》》资料清单</div>

             <table class="Admin_Table">
            <tr>
                <td>
                    资料及配件名称：
                </td>
                <td>
                   <asp:TextBox ID="name1" runat="server" CssClass="txtHInput"></asp:TextBox>
                </td>
              
            </tr>
             <tr>
                <td>
                    描述：
                </td>
                <td>
                    <asp:TextBox ID="name2" runat="server" CssClass="txtHInput"></asp:TextBox>
                </td>
              
            </tr>
                <tr>
                <td>
                    数量：
                </td>
                <td>
                    <asp:TextBox ID="name3" runat="server" CssClass="txtHInput"></asp:TextBox>
                </td>
              
            </tr>
                <tr>
                <td>
                    备注：
                </td>
                <td>
                    <asp:TextBox ID="name4" runat="server" CssClass="txtHInput"></asp:TextBox>
                </td>
              
            </tr>
                <tr>
                <td>
                    
                </td>
                <td>
                    <asp:TextBox ID="name5" Visible ="false"  runat="server" CssClass="txtHInput"></asp:TextBox>
                </td>
              
            </tr>
                <tr>
                <td>
                    
                </td>
                <td>
                    <asp:TextBox ID="name6" Visible ="false" runat="server" CssClass="txtHInput"></asp:TextBox>
                </td>
              
            </tr>
          
                <tr>
                <td colspan ="2" align="center" >
                    <asp:Button ID="Button1" runat="server" Text="保存" onclick="Button1_Click" /></td>
               
              
            </tr>

            <tr><td colspan ="2" align="center" >
            
            <asp:GridView ID="GridView1" runat="server" Width="100%" DataKeyNames ="id" CssClass="Admin_Table" 
                    AutoGenerateColumns="false" onrowdeleting="GridView1_RowDeleting"
     >
        <Columns>
            <asp:TemplateField HeaderText="序号">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                        CommandArgument='<%# Eval("baojiaid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle ForeColor="Green" />
            </asp:TemplateField>
            <asp:BoundField DataField="baojiaid" HeaderText="报价编号" />
            <asp:BoundField DataField="kehuid" HeaderText="客户" />
            <asp:BoundField DataField="renwuhao" HeaderText="任务号" />
            <asp:BoundField DataField="name1" HeaderText="资料及配件名称" />
            <asp:BoundField DataField="name2" HeaderText="描述" />
            <asp:BoundField DataField="name3" HeaderText="数量" />
            <asp:BoundField DataField="name4" HeaderText="备注" />
          
            <asp:BoundField HeaderText="录入人" DataField="fillname" />
            <asp:BoundField HeaderText="录入日期" DataField="filltime" DataFormatString="{0:d}" />
            <asp:BoundField HeaderText="确认标志" DataField="biaozhi" ReadOnly="True" />
            <asp:BoundField HeaderText="确认人" DataField="querenname" ReadOnly="True" />
            <asp:BoundField HeaderText="确认日期" DataField="querentime" ReadOnly="True" DataFormatString="{0:d}"/>
           <asp:CommandField HeaderText="取消" ShowDeleteButton="True"  />
        </Columns>
        <HeaderStyle CssClass="Admin_Table_Title " />
    </asp:GridView>
            
            
            </td></tr>
            </table> 
    </div>

     <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>
