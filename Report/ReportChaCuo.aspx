<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportChaCuo.aspx.cs" Inherits="Report_ReportChaCuo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title>质量管理</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
      <script type="text/javascript" src="../js/calendar.js">
      
        <script type="text/javascript">


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
      
      </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
        质量管理 》》差错明细</div>


         按报告退回日期： <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />

          <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack ="true" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged" >
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList2" runat="server">
        </asp:DropDownList>
      
        <asp:TextBox ID="TextBox1" runat="server" Visible ="false"  Width="111px"></asp:TextBox>(目前自由选择时间，默认当前月)
        <asp:Button ID="Button2"  CssClass ="BnCss"  runat="server" 
             Text="查询" onclick="Button2_Click" /><asp:Button ID="Button1"    runat="server" 
            onclick="Button1_Click" Text="导出excel" />

 
                            <table width="100%" class="Admin_Table" 
                                border="1">
                                <tr>
                                    <td style="width: 100%";  valign="top">
                                        <asp:GridView ID="GridView1" AutoGenerateColumns ="false"  runat="server" ShowFooter ="true"  Width="98%" 
                                            OnRowDataBound="GridView1_RowDataBound" 
                                          
                                            CssClass="Admin_Table">
                                          <Columns >

                                                 <asp:TemplateField HeaderText="序号" >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>'
                            CommandArgument='<%# Eval("id") %>' CommandName="BussinessNeeds" ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                                          <asp:BoundField DataField="baogaoid" HeaderText="报告号" />
                            <asp:BoundField DataField="applyhao" HeaderText="申请编号" />
                            <asp:BoundField DataField="rwbianhao" HeaderText="任务编号" />
                            <asp:BoundField DataField="bumen" HeaderText="部门" />
                            <asp:BoundField DataField="gongchengshi" HeaderText="工程师" />
                            <asp:BoundField DataField="fenlei" HeaderText="错误类型" />
                            <asp:BoundField DataField="beizhu2" HeaderText="错误来源" />
                                     <asp:BoundField DataField="leirong" HeaderText="错误内容" />    
                                     <asp:BoundField DataField="time" DataFormatString ="{0:d}" HeaderText="错误时间" />
                                  
                                        
                                          </Columns>
                                          
                                          <HeaderStyle CssClass="Admin_Table_Title " />
                                        </asp:GridView>
                                      
                                       <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第%StartRecordIndex%-%EndRecordIndex%"
                        CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                        InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager2_PageChanged"
                        PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                        Style="font-size: 9pt" UrlPaging="True" PageSize="15" Width="100%">
                    </webdiyer:AspNetPager>

                                    </td>
                                </tr>
                               
                            </table>
                        
                             
    </form>
</body>
</html>
