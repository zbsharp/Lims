<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceSee.aspx.cs" Inherits="Income_InvoiceSee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title>编辑收费单</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
        业务管理 》》编辑收费单</div>
    
                
              
                            <table width="100%" class="Admin_Table" 
                                border="1">
                                <tr>
                                    <td style="width: 100%";  valign="top">
                                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                            DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                                            OnRowUpdating="GridView1_RowUpdating" CssClass="Admin_Table">
                                          
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                                            CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="bianhao" HeaderText="编号"  Visible ="false" />
                                                       <asp:BoundField DataField="taskid" HeaderText="任务号" />

                                                <asp:BoundField DataField="type" HeaderText="项目" />
                                                <asp:BoundField DataField="feiyong" HeaderText="费用" />
                                               <asp:BoundField DataField="shoufeibianhao" DataFormatString="{0:d}" HeaderText="收费编号" ReadOnly="true" />
                                                <asp:BoundField DataField="beizhu2" HeaderText="备注" />
                                                <asp:BoundField DataField="beizhu3" HeaderText="部门" />
                                                <asp:BoundField DataField="fillname" HeaderText="录入人" ReadOnly="true" />
                                                <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="时间" ReadOnly="true" />
                                            </Columns>
                                          <HeaderStyle CssClass="Admin_Table_Title " />
                                        </asp:GridView>
                                    </td>
                                </tr>
                               
                            </table>


                                           <table class="Admin_Table" width="100%">
            <tr>
            <td>
                联系人：</td>
               <td>
                            <asp:DropDownList ID="DropDownList2" runat="server">
    </asp:DropDownList>
              </td>
              <td>总费用：</td>
               <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
           </tr> 
           <tr>
           <td>折后价：</td>
            <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
             <td>保存：</td>
              <td> <asp:Button ID="Button3" runat="server" CssClass="BnCss" Text="保存" OnClick="Button3_Click" /></td>

            </tr>
           
           </table> 




                         
    </form>
</body>
</html>
