<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YiShouLiYanPing.aspx.cs" Inherits="Quotation_YiShouLiYanPing" %>

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
      <script type="text/javascript" src="../js/calendar.js"></script>

    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>

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
            <asp:Label ID="Label1" runat="server" Text="业务管理 》》任务列表"></asp:Label></div>
        <asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
        <asp:ListItem Value="rwbianhao">任务号</asp:ListItem>
        <asp:ListItem Value="0">全选</asp:ListItem>
            
            <asp:ListItem Value="baojiaid">报价单号</asp:ListItem>
           <asp:ListItem Value="kehuname">客户名称</asp:ListItem>
           <asp:ListItem Value="shenqingbianhao">申请编号</asp:ListItem>
           <asp:ListItem Value="name">产品名称</asp:ListItem>
           <asp:ListItem Value="xinghao">产品型号</asp:ListItem>

           <asp:ListItem Value="kf">客户经理</asp:ListItem>
           <asp:ListItem Value="b3">灯具类</asp:ListItem>
           <asp:ListItem Value="b4">医疗电子</asp:ListItem>

        </asp:DropDownList>
        &nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>


         下达日期： <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />


        <asp:Button ID="Button2" CssClass="BnCss" runat="server" OnClick="Button2_Click"
            Text="查询" />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="写收费单" /><asp:Label
            ID="Label3" runat="server" Text="" ForeColor ="Red" ></asp:Label>
        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
            DataKeyNames="id" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand"
            OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="序号"   >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                            CommandArgument='<%# Eval("id") %>' CommandName="BussinessNeeds" ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                <asp:BoundField DataField="rwbianhao" HeaderText="任务编号"   />
                <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号"  />
                <asp:BoundField DataField="weituodanwei" HeaderText="委托方" />
               
               <asp:BoundField DataField="name" HeaderText="产品名称" />
                <asp:BoundField DataField="xinghao" HeaderText="产品型号" />
                <asp:BoundField DataField="kf" HeaderText="项目经理"   />
             
                
                <asp:BoundField DataField="state1" HeaderText="状态" />
            
           <%--    <asp:HyperLinkField Text="查看" HeaderText="工程师" Target="button" DataNavigateUrlFormatString="~/Case/AnJianInFo.aspx?xiangmuid={0}&&bumen=&&id={1}"
                DataNavigateUrlFields="taskno,id" />--%>


                   <asp:BoundField DataField="" HeaderText="工程师" />

                 <asp:BoundField DataField="xiadariqi" HeaderText="下达日期" />



                    <asp:HyperLinkField HeaderText="通知书" Text="通知书" Target="_blank" Visible ="false"   DataNavigateUrlFormatString="~/Print/TaskPrint.aspx?bianhao={0}"
                    DataNavigateUrlFields="bianhao" />


                    <asp:HyperLinkField HeaderText="上报费用" Text="上报费用" Target="_blank" Visible ="false"   DataNavigateUrlFormatString="~/Case/CeShiFeiGc.aspx?bianhao={0}"
                    DataNavigateUrlFields="bianhao" />


                <asp:HyperLinkField HeaderText="附件" Text="上传" Target="_blank" Visible ="false"  DataNavigateUrlFormatString="~/Case/UploadFile.aspx?id={0}&&baojiaid={1}"
                    DataNavigateUrlFields="id,baojiaid" />

                   


                    

                       <asp:HyperLinkField HeaderText="资料" Text="资料" Target="_blank" Visible ="false"   DataNavigateUrlFormatString="~/Case/ziliaoaddm.aspx?xiangmuid={0}"
                    DataNavigateUrlFields="rwbianhao" />


                <asp:HyperLinkField HeaderText="核算费用" Text="核算费用" Target="_blank" Visible ="false"   DataNavigateUrlFormatString="~/Case/CeShiFeiKf.aspx?baojiaid={0}&kehuid={1}&bianhao={2}"
                    DataNavigateUrlFields="baojiaid,kehuid,bianhao" />
                <asp:HyperLinkField HeaderText="样品" Text="样品" Target="_blank" Visible ="false"   DataNavigateUrlFormatString="~/YangPin/YangPinAdd.aspx?baojiaid={0}&amp;&amp;kehuid={1}&bianhao={2}"
                    DataNavigateUrlFields="baojiaid,kehuid,bianhao" />

                    
                     <asp:HyperLinkField HeaderText="延期" Text="延期" Target="_blank" Visible ="false"   DataNavigateUrlFormatString="~/Case/TaskYanQi.aspx?renwuid={0}"
                    DataNavigateUrlFields="bianhao" />


                     <asp:HyperLinkField HeaderText="样品" Text="样品" Target="_blank" DataNavigateUrlFormatString="~/YangPin/YangPinManageCha.aspx?taskno={0}"
                    DataNavigateUrlFields="rwbianhao" />

                     <asp:HyperLinkField HeaderText="查看" Text="查看"  Visible ="false"  Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=1"
                    DataNavigateUrlFields="bianhao" />
                   

                    <asp:HyperLinkField HeaderText="报告" Text="报告" Target="_blank" Visible ="false"   DataNavigateUrlFormatString="~/Report/BaogaoAdd.aspx?renwuid={0}"
                    DataNavigateUrlFields="rwbianhao" />

                    <asp:TemplateField HeaderText="退回" Visible ="false" >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton5" runat="server" Text="退回"  CommandArgument='<%# Eval("bianhao") %>'
                            CommandName="back"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="取消"  Visible ="false" >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton6" runat="server" Text="取消" CommandArgument='<%# Eval("bianhao") %>'
                            CommandName="cancel1"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>




                <asp:TemplateField Visible ="false" >
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>


                 <asp:BoundField DataField="fillname" HeaderText="受理人" />

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
                                     Style="font-size: 9pt" UrlPaging="True" PageSize="15">
                                </webdiyer:AspNetPager> 

    </div>
    <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>
