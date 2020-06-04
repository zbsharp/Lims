<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnjianxinxiManage2.aspx.cs" Inherits="Quotation_AnjianxinxiManage2" %>

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
    <form name="form1" runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
    <div>
        <div class="Body_Title">
            业务管理 》》业务受理</div>
        <asp:DropDownList ID="DropDownList1" Visible ="false"  runat="server" Width="77px">
            <asp:ListItem Value="0">报价单号</asp:ListItem>
            <asp:ListItem Value="1">委托单位</asp:ListItem>
        </asp:DropDownList>
       报价单号或委托单位
        <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
        <asp:Button ID="Button2" CssClass="BnCss" runat="server" OnClick="Button2_Click"
            Text="查询" />
        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
            DataKeyNames="id" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand"
            OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                            CommandArgument='<%# Eval("id") %>' CommandName="BussinessNeeds" ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                <asp:BoundField DataField="weituo" HeaderText="委托方" />

               <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号"  />
               
                
                <asp:BoundField DataField="baojiaid" HeaderText="报价编号" />
                    <asp:BoundField DataField="name" HeaderText="测试产品" />
               
                <asp:BoundField DataField="fillname" HeaderText="填单人" />
                <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="填单日期" />
                


                   <asp:HyperLinkField HeaderText="编辑" Text="编辑" Target="_blank"  DataNavigateUrlFormatString="AnjianxinxiSee.aspx?id={0}"
                    DataNavigateUrlFields="bianhao" />

                     <asp:HyperLinkField HeaderText="打印" Text="打印" Target="_blank" DataNavigateUrlFormatString="~/Print/PrintWeiTuo.aspx?bianhao={0}"
                    DataNavigateUrlFields="bianhao" />

                     <asp:HyperLinkField HeaderText="下任务" Text="下任务" Target="_blank" DataNavigateUrlFormatString="~/Case/TaskIn.aspx?tijiaobianhao={0}"
                    DataNavigateUrlFields="bianhao" />


                       <asp:HyperLinkField HeaderText="受理" Text="受理" Target="_blank" DataNavigateUrlFormatString="~/Case/TaskIn1.aspx?tijiaobianhao={0}"
                    DataNavigateUrlFields="bianhao" />

                <asp:TemplateField HeaderText="退回" Visible ="false" >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton5" runat="server" Text="退回" CommandArgument='<%# Eval("bianhao") %>'
                            CommandName="back"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="取消" Visible ="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton6" runat="server" Text="取消" CommandArgument='<%# Eval("bianhao") %>'
                            CommandName="cancel1"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                <asp:HyperLinkField HeaderText="附件" Text="上传" Target="_blank" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?id={0}&&baojiaid={1}"
                    DataNavigateUrlFields="id,baojiaid" />
              
               
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
            <EmptyDataTemplate>
                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>

        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                                    CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                                    InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
                                    PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                                    Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="12">
                                </webdiyer:AspNetPager> 

    </div>
    <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>

