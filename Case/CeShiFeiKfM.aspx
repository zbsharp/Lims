<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CeShiFeiKfM.aspx.cs" Inherits="Case_CeShiFeiKfM" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>请款单列表</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
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

     <script type="text/javascript" src="../js/calendar.js"></script>

    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
        财务管理 》》收费单列表</div>
    <div>
        <table class="Admin_Table" width="100%">
            <tr>
                <td align="left" colspan="4">
                    客户名称或任务号或申请编号或收费编号或项目经理或客户联系人或委托方或付款方或金额
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:DropDownList ID="DropDownList1"
                        runat="server">
                        <asp:ListItem  Value ="否">未到账</asp:ListItem>
                        <asp:ListItem Value ="是">已到账</asp:ListItem>
                    </asp:DropDownList>

                    请款日期： <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />

                    <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="合并打印" />
                </td></tr> 
                <tr>
                    <td align="left" colspan="4" style="text-align: center">
                        <asp:GridView ID="GridView1"  ShowFooter ="true" runat="server" OnRowDeleting="GridView1_RowDeleting"
                            class="Admin_Table" Width="100%" AutoGenerateColumns="false" 
                            DataKeyNames="id" onrowdatabound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                            CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                

                                <asp:BoundField DataField="inid" HeaderText="收费编号" />


                                 <asp:HyperLinkField HeaderText="任务号"  DataTextField ="taskno" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=1"
                    DataNavigateUrlFields="taskno" />
                                

                                 <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号" />
                                <asp:BoundField DataField="feiyong1" HeaderText="请款金额"  DataFormatString ="{0:N2}"/>

                                <asp:BoundField DataField="kehuname" HeaderText="客户名称" />
                                 <asp:BoundField DataField="fk" HeaderText="付款方" />
                                  <asp:BoundField DataField="weituo" HeaderText="委托方" />
                                <asp:BoundField DataField="name" HeaderText="联系人" />

                                 <asp:BoundField DataField="kf" HeaderText="项目经理" />
                                 <asp:BoundField DataField="hesuanbiaozhi" HeaderText="到账" />
                                <asp:BoundField DataField="fillname" HeaderText="开单人" />
                                <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="开单日期" />
                                  <asp:BoundField DataField="beizhu3" DataFormatString="{0:d}" HeaderText="完成日期" />
                                 <asp:HyperLinkField HeaderText="上传" Text="上传" Visible ="false"  Target="_blank" DataNavigateUrlFormatString="~/Income/BaoGaoFirstUpLoad.aspx?inid={0}"
                    DataNavigateUrlFields="inid" />


                      <asp:HyperLinkField HeaderText="上传收费" Text="上传收费" Target="_blank"  DataNavigateUrlFormatString="~/Case/UploadFile.aspx?id={0}&&baojiaid={1}&&ddd=1"
                    DataNavigateUrlFields="idd,baojiaid" />


                                <asp:HyperLinkField HeaderText="编辑" Text="编辑" DataNavigateUrlFormatString="~/Income/InvoiceAdd3.aspx?invoiceid={0}"
                                    DataNavigateUrlFields="inid" Target="_blank" />
                                <asp:HyperLinkField HeaderText="打印" Text="打印" DataNavigateUrlFormatString="~/Print/InvoicePrint.aspx?baojiaid={0}&&customerid={1}&&inid={2}"
                                    DataNavigateUrlFields="baojiaid,kehuid,inid" Target="_blank" />
                               <asp:CommandField ShowDeleteButton ="true" HeaderText="取消" />
                                <asp:BoundField DataField="res" HeaderText="业务员" />
                                                         <asp:TemplateField>
                            <HeaderTemplate>
                                请选择 
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>

                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                                    CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                                    InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
                                    PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                                    Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="15">
                                </webdiyer:AspNetPager> 


                    </td>
                </tr>
        </table>
    </div>
    </form>
</body>
</html>
