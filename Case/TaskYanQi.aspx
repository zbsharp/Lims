<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskYanQi.aspx.cs" Inherits="Case_TaskYanQi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务延期处理</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="Body_Title">
            业务管理 》》任务延期处理</div>


             <table align="center" class="Admin_Table" width="100%">
         
            <tr>
                <td>
                    任务编号：
                </td>
                <td>
                    <asp:TextBox ID="rwbianhao" runat="server" Enabled ="false"   ></asp:TextBox>
                </td>
             </tr> 
         
            <tr>
                <td>
                    当前要求完成日期：</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"   Enabled ="false" ></asp:TextBox>
                </td>
             </tr> 
         
            <tr>
                <td>
                    延期至完成日期：</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox3,'yyyy-mm-dd')" ></asp:TextBox>
                </td>
             </tr> 
         
            <tr>
                <td>
                    延期原因：</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width ="300px"></asp:TextBox>
                </td>
              
                     </tr> 
         
            <tr>
                <td colspan="2" align ="center" >
                    <asp:Button ID="Button1" runat="server" Text="保存" onclick="Button1_Click" />
                </td>
        
                     
                     
                     </tr> 
         
            <tr>
                <td colspan="2" align ="center">
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            CssClass="Admin_Table" DataKeyNames="id" 
                           Width="100%" onrowdeleting="GridView1_RowDeleting">
                          
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                            CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                 <asp:BoundField DataField="bianhao" HeaderText="任务号" />

                                <asp:BoundField DataField="yuanshitime" HeaderText="原始日期" />
                                 <asp:BoundField DataField="xiugaitime" HeaderText="延长至日期" />
                                  <asp:BoundField DataField="beizhu" HeaderText="延时原因" />
                                <asp:BoundField DataField="fillname" HeaderText="填写人" />
                                <asp:BoundField DataField="filltime" HeaderText="填写日期" />
                       
                                   <asp:CommandField HeaderText="取消" ShowDeleteButton="True" ShowEditButton ="false"  />
                            </Columns>
                             <HeaderStyle CssClass="Admin_Table_Title " />
                            <EmptyDataTemplate>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                            </EmptyDataTemplate>
                          
                        </asp:GridView>
                </td>
        
                     
                     
                     </tr> 
                </table> 


    </div>
    </form>
</body>
</html>
