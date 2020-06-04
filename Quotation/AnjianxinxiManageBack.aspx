<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnjianxinxiManageBack.aspx.cs" Inherits="Quotation_AnjianxinxiManageBack" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
     <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
     <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
     <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
     <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js">
    
    	
    
    


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
    <form name="form1" runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
    <div>
       <div class="Body_Title">
        业务管理 》》被退回任务列表</div>
      
                   <asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
                      <asp:ListItem Value="0">报价单号</asp:ListItem>
                        <asp:ListItem Value="1">报价单号</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
                    <asp:Button ID="Button2" CssClass="BnCss" runat="server" OnClick="Button2_Click"
                        Text="查询" />
                
          
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                        
                       
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                        CommandArgument='<%# Eval("id") %>' CommandName="BussinessNeeds" ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="bianhao" HeaderText="任务编号" />
                            <asp:BoundField DataField="baojiaid" HeaderText="报价编号" />
                            <asp:BoundField DataField="kehuid" HeaderText="客户编号" />
                           
                         
                            
                            <asp:BoundField DataField="ypshuliang" HeaderText="数量" />
                            <asp:BoundField DataField="shangbiao" HeaderText="商标" />
                           
                         
                            <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号" />

                             <asp:BoundField DataField="state" HeaderText="当前状态" />
                             <asp:BoundField DataField="statetime" HeaderText="退回时间" />
                       
                            <asp:BoundField DataField="fillname" HeaderText="录入人" />
                            <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="录入时间" />
                            <asp:TemplateField HeaderText="编辑">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton8" runat="server" Text="编辑" ForeColor="blue" CommandArgument='<%# Eval("bianhao") %>'
                                        CommandName="Chakan"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="退回" Visible ="false" >
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton5" runat="server" Text="退回" CommandArgument='<%# Eval("bianhao") %>'
                        CommandName="back"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle ForeColor="Green" />
            </asp:TemplateField>

               <asp:TemplateField HeaderText="取消">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton6" runat="server" Text="取消" CommandArgument='<%# Eval("bianhao") %>'
                        CommandName="cancel1"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle ForeColor="Green" />
            </asp:TemplateField>


                             <asp:HyperLinkField  HeaderText="附件" Text="上传" Target="button" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?id={0}&&baojiaid={1}"
                DataNavigateUrlFields="id,baojiaid" />


              




                <asp:TemplateField HeaderText="任务通知书" Visible ="false" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonr1" runat="server" Text="编辑" ForeColor="blue" CommandArgument='<%# Eval("bianhao") %>'
                                        CommandName="renwu1"></asp:LinkButton>|| <asp:LinkButton ID="LinkButtonr2" runat="server" Text="打印" ForeColor="blue" CommandArgument='<%# Eval("bianhao") %>'
                                        CommandName="renwu2"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>





                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />

                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
             
    </div>
    <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>
