<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceSeeInvoice.aspx.cs" Inherits="Income_InvoiceSeeInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title>开票</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
       财务管理 》》开票</div>
    
                
                  <fieldset>
        <legend style="color: Red">应收款项</legend>
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

                                                <asp:BoundField DataField="bianhao" HeaderText="任务号" />

                                                <asp:BoundField DataField="type" HeaderText="项目" />
                                                <asp:BoundField DataField="feiyong" HeaderText="费用" />
                                                <asp:BoundField DataField="beizhu1" HeaderText="CQC编号" />
                                                <asp:BoundField DataField="beizhu2" HeaderText="备注" />
                                                <asp:BoundField DataField="beizhu3" HeaderText="部门" />
                                                <asp:BoundField DataField="fillname" HeaderText="录入人" ReadOnly="true" />
                                                <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="时间" ReadOnly="true" />

                                                 <asp:BoundField DataField="heduibiaozhi" HeaderText="是否对账" />
                                                  <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                            </Columns>
                                          <HeaderStyle CssClass="Admin_Table_Title " />
                                        </asp:GridView>
                                    </td>
                                </tr>
                               
                            </table></fieldset> 
                          <div style ="text-align :center ">
    <asp:TextBox ID="TextBox1" Visible ="false"  runat="server"></asp:TextBox>
                           <asp:TextBox ID="TextBox2" Visible ="false" runat="server"></asp:TextBox>
                         
                            <asp:DropDownList ID="DropDownList2" Visible ="false" runat="server">
    </asp:DropDownList>
                            </div>

                        <fieldset>
        <legend style="color: Red">到款记录</legend>
                            
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
                            <asp:BoundField DataField="fukuanriqi" DataFormatString="{0:d}" HeaderText="付款日期" />
                            <asp:BoundField DataField="daid" HeaderText="流水号" />
                            <asp:BoundField DataField="fukuanjine" HeaderText="付款金额" />

                            <asp:BoundField DataField="xiangmuid2" HeaderText="项目" />
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
                            
                            
                            
                          </fieldset> 

                             <fieldset>
        <legend style="color: Red">填写发票</legend>



                           <table class="Admin_Table" >
            <tr>
                <td align="left" >
                    发票号：
                </td>
                <td align="left" >
                   
                   
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                   
                   
                </td>

               <td align="left" >
                      开票日期：
                </td>
              <td align="left" >
                   
                   
                    <asp:TextBox ID="TextBox5" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox5,'yyyy-mm-dd')"></asp:TextBox>
                   
                   
                </td>
              
            </tr>

                <tr>
                <td align="left" >
                    发票抬头：
                </td>
                <td align="left" >
                   
                   
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                   
                   
                </td>

               <td align="left" >
                      开票内容：
                </td>
              <td align="left" >
                   
                   
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                   
                   
                </td>
              
            </tr>
    
        <tr>
                <td align="left" >
                    性质：
                </td>
                <td align="left" >
                   
                   
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>正常</asp:ListItem>
                        <asp:ListItem>借票</asp:ListItem>
                    </asp:DropDownList>
                   
                   
                </td>

               <td align="left" >
                      取票人：
                </td>
              <td align="left" >
                   
                   
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                   
                   
                </td>
              
            </tr>

             <tr>
                <td align="left" >
                    备注：
                </td>
                <td align="left" >
                   
                   
                  <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                   
                   
                </td>

               <td align="left" >
                      
                          <div style ="text-align :center ">
    <asp:TextBox ID="TextBox9" Visible ="false"  runat="server"></asp:TextBox>
                           <asp:TextBox ID="TextBox10" Visible ="false" runat="server"></asp:TextBox>
                         
                            <asp:DropDownList ID="DropDownList3" Visible ="false" runat="server">
    </asp:DropDownList></div>开票金额：

                </td>
              <td align="left" >
                   
                   
                    
                   
                   
                  <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                   
                   
                    
                   
                   
                </td>
              
            </tr>

 
             <tr>
                <td align="center" colspan="4" >
                    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="保存" />
                </td>
              
            </tr>

 
    </table> 
    
    </fieldset> 
    

    </form>
</body>
</html>

