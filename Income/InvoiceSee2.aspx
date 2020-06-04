<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceSee2.aspx.cs" Inherits="Income_InvoiceSee2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title>项目应收已收记录</title>
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
        业务管理 》》项目应收已收记录</div>
    
                
               <fieldset>
        <legend style="color: Red">应收款项目</legend>
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
                                                 <asp:BoundField DataField="id" HeaderText="编号" />
                                              
                                                <asp:BoundField DataField="taskid" HeaderText="任务号" />
                                                 <asp:BoundField DataField="type" HeaderText="项目" />
                                <asp:BoundField DataField="baojia" HeaderText="报价" />
                                  <asp:BoundField DataField="zhekou" HeaderText="折扣" />
                                <asp:BoundField DataField="feiyong" HeaderText="应收"  DataFormatString ="{0:N2}"/>
                              <asp:BoundField DataField="shoufeibianhao" DataFormatString="{0:d}" HeaderText="收费编号" ReadOnly="true" />
                                <asp:BoundField DataField="beizhu2" HeaderText="备注" />
                                <asp:BoundField DataField="beizhu3" HeaderText="类别" />
                                <asp:BoundField DataField="fillname" HeaderText="录入人" ReadOnly="true" />
                                                <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="时间" ReadOnly="true" />

                                                 <asp:BoundField DataField="heduibiaozhi" HeaderText="是否对账" />
                                                  <asp:TemplateField Visible ="false" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server"  OnCheckedChanged ="CheckBox1_CheckedChanged"  AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                            </Columns>
                                          <HeaderStyle CssClass="Admin_Table_Title " />
                                        </asp:GridView>
                                    </td>
                                </tr>
                               
                            </table>
                          <div style ="text-align :center ">
    <asp:TextBox ID="TextBox1" Visible ="false"  runat="server"></asp:TextBox>
                           <asp:TextBox ID="TextBox2" Visible ="false" runat="server"></asp:TextBox>
                         
                            <asp:DropDownList ID="DropDownList2" Visible ="false" runat="server">
    </asp:DropDownList><asp:Button ID="Button1" runat="server" Text="确定分款" 
        onclick="Button1_Click1" />
                              <asp:TextBox ID="TextBox3" runat="server"  Visible ="false"  ></asp:TextBox>
                            </div>
                             </fieldset> 


                              <fieldset>
        <legend style="color: Red">已收款项目</legend>


                            <div>
                            
                               <asp:GridView ID="GridView2" runat="server" CssClass="Admin_Table"  
                                    AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="id" onrowdeleting="GridView2_RowDeleting" >
                       
                        <Columns>
                            <asp:TemplateField HeaderText="序 号" Visible="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("0000") %>'
                                        CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="taskid" DataFormatString="{0:d}" HeaderText="任务编号" />
                            <asp:BoundField DataField="shenqing" HeaderText="申请编号" />
                            <asp:BoundField DataField="fukuanjine" HeaderText="付款金额"  Visible ="false" />

                            <asp:BoundField DataField="na" HeaderText="项目"  Visible ="false" />
                            <asp:BoundField DataField="name" HeaderText="分款人" />
                            
                            <asp:BoundField DataField="riqi" DataFormatString="{0:d}" HeaderText="分款日期" />
                            <asp:BoundField DataField="xiaojine" HeaderText="分款金额" />
                          
                             <asp:BoundField DataField="beizhu3" HeaderText="部门" />
                             <asp:CommandField  ShowDeleteButton ="true" />
                        </Columns>
                          <HeaderStyle CssClass="Admin_Table_Title " />

                        <EmptyDataTemplate>
                            <div style="color: Red;">
                                无到账信息</div>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                            
                            
                            
                            </div>
    </fieldset> 

    </form>
</body>
</html>

