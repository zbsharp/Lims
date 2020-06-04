<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseDaiFen.aspx.cs" Inherits="Case_CaseDaiFen" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
     <script type ="text/javascript">


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
        <form id="form2" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"  EnableScriptGlobalization="false" EnableScriptLocalization="false">
    </asp:ScriptManager>
    <div class="Body_Title">
        工程管理 》》任务通知</div>
    查询条件：&nbsp;<asp:DropDownList
            ID="DropDownList1" runat="server" Width="77px" >
           
           <asp:ListItem Value ="kehuname">客户名称</asp:ListItem>
        <asp:ListItem Value ="fillname">业务人员</asp:ListItem>
        <asp:ListItem Value ="baojiaid">报价编号</asp:ListItem>
            <asp:ListItem Value ="taskid">任务编号</asp:ListItem>  
            
              
        </asp:DropDownList>&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
        <asp:Button ID="Button2"  CssClass ="BnCss"  runat="server" 
            onclick="Button2_Click" Text="查询" />
            
            
      
           <asp:Button ID="Button1"  CssClass ="BnCss"  runat="server" 
            onclick="Button1_Click" Text="提交开案"  Visible ="false" />
      
              <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
                      <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged"
                          Text="全选" />
        
        
                        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table"
                               AutoGenerateColumns="False" DataKeyNames="id"   OnRowDataBound="GridView1_RowDataBound">
                                                      
                                                        <Columns>
                                 <asp:TemplateField>
                                    <HeaderTemplate>
                                         <asp:CheckBox ID="CheckBox3"  Enabled ="false"  runat="server"  />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBox1" runat="server"  />
                                    </ItemTemplate>
                                </asp:TemplateField>       
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                            CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="baojiaid" HeaderText="报价编号" />
                                 <asp:BoundField DataField="kehuname" HeaderText="客户" />
                                <asp:BoundField DataField="taskid" HeaderText="任务编号" />
                                <asp:BoundField DataField="fillname" HeaderText="任务下达人" />
                                <asp:BoundField DataField="filltime" HeaderText="任务下达时间" />

                              
                                <asp:HyperLinkField Text="任务通知书" HeaderText="任务通知书" Target="button" DataNavigateUrlFormatString="~/Case/TaskIn.aspx?tijiaobianhao={0}"
                DataNavigateUrlFields="tijiaobianhao" />
                                
                                <asp:HyperLinkField Text="上传附件" HeaderText="上传附件" Target="button" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?id={0}"
                DataNavigateUrlFields="tijiaobianhao" />


                <asp:HyperLinkField HeaderText="上报费用" Text="上报费用" Target="_blank" DataNavigateUrlFormatString="~/Case/CeShiFeiGc.aspx?bianhao={0}"
                    DataNavigateUrlFields="bianhao" />
                                 
                                                            
                                                        </Columns>
                               <EmptyDataTemplate>
                                   <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                               </EmptyDataTemplate>
                                                      <HeaderStyle CssClass="Admin_Table_Title " />    
                                                    </asp:GridView>
             
                  </ContentTemplate>
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                  </Triggers>
              </asp:UpdatePanel>
              
                     
   
   <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                                    CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                                    InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
                                    PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                                    Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="12">
                                </webdiyer:AspNetPager> 
   
    </form>
</body>
</html>

