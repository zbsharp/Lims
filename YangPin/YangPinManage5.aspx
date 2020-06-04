<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YangPinManage5.aspx.cs" Inherits="YangPin_YangPinManage5" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js">
    <script type="text/javascript" src="../Celend/popcalendar.js"></script>


    <script type="text/javascript" src="popcalendar.js"></script>
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

    <script type="text/javascript" src="../js/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
        工程管理 》》未归还样品</div>
  
    <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
        <tr>
            <td align="left" style="height: 25px">
                <input id="txFDate"   visible ="false"  runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" />
        
        <input id="txTDate" visible ="false" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text"  />


            <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack ="true" 
            onselectedindexchanged="DropDownList4_SelectedIndexChanged" >
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList5" runat="server">
        </asp:DropDownList>

                <asp:DropDownList ID="DropDownList1" runat="server" 
                     >
                       <asp:ListItem Value="3">样品编号</asp:ListItem>
                       <asp:ListItem Value="0">全部</asp:ListItem>
                      
                   
                    <asp:ListItem Value="1">厂商名称</asp:ListItem>
                    <asp:ListItem Value="2">样品名称</asp:ListItem>
                   

                    

                     <asp:ListItem Value="5">任务编号</asp:ListItem>
                      <asp:ListItem Value="6">申请编号</asp:ListItem>
                      <asp:ListItem Value="7">封存编号</asp:ListItem>
                     <%-- <asp:ListItem Value="4">样品状态</asp:ListItem>--%>

                </asp:DropDownList>
              <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
               任务状态: <asp:DropDownList ID="DropDownList3" runat="server">
                     <asp:ListItem>已完成</asp:ListItem>
                    <asp:ListItem>未完成</asp:ListItem>
                     <asp:ListItem>全部</asp:ListItem>
                    
                   
                </asp:DropDownList>
                
                <asp:Button ID="Button2" CssClass="BnCss" runat="server" OnClick="Button2_Click"
                    Text="查询" />
                <asp:Button ID="Button1" runat="server" CssClass="BnCss" OnClick="Button1_Click"
                    Text="导出excel" />
                <asp:Button ID="Button3" runat="server" Text="样品登记" OnClick="Button3_Click"  Visible ="false" />
                <asp:Button ID="Button4" runat="server" Text="批量处理" OnClick="Button4_Click"  Visible ="false" />

                

            </td>
        </tr>
        <tr>
            <td align="left">
              
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                            CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                               
                                <asp:BoundField DataField="sampleid" HeaderText="样品编号" />

                                  <asp:BoundField DataField="anjianid" HeaderText="任务号"  />
                                   <asp:BoundField DataField="sq" HeaderText="申请编号"  />
                                  <asp:BoundField DataField="name" HeaderText="样品名称" />
                                <asp:BoundField DataField="model" HeaderText="型号" />
                                 <asp:BoundField DataField="position" HeaderText="制造厂商" />
                                  
                                <asp:BoundField DataField="receivetime" DataFormatString="{0:d}" HeaderText="收到日期" />
                                 <asp:BoundField DataField="kf" HeaderText="客服"  />
                              <asp:BoundField DataField="gc" HeaderText="工程师"  />
                                
                               
                               
                                <asp:BoundField DataField="state" HeaderText="状态" />
                                 <asp:BoundField DataField="dd" HeaderText="借出人"  />

                                <asp:TemplateField HeaderText="操作"  visible="false">
                                    <ItemTemplate>
                                        <span style="cursor: hand; color: Blue;" onclick="window.open('YangPin_Jiechu.aspx?sampleid=<%#Eval("sampleid") %>')">
                                            <asp:Label ID="seeLB" runat="server" Text="借出"></asp:Label></span> || <span style="cursor: hand;
                                                color: Blue;" onclick="window.open('YangPin_FanHuan.aspx?sampleid=<%#Eval("sampleid") %>')">
                                                <asp:Label ID="Label1" runat="server" Text="返还"></asp:Label></span> ||
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="处置"  >
                                    <ItemTemplate>
                                        <span style="cursor: hand;
                                                    color: Blue;" onclick="window.open('../Print/YangPinCuiHuan.aspx?sampleid=<%#Eval("sampleid") %>&&caozuo=2')">
                                                    <asp:Label ID="Label6" runat="server" Text="催还"></asp:Label></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="编辑">
                                    <ItemTemplate>
                                        <span style="cursor: hand; color: Blue; display :none;" onclick="window.open('YangPinSee.aspx?yangpinid=<%#Eval("id") %>')">
                                            <asp:Label ID="seeLB" runat="server" Text="编辑"></asp:Label></span><span style="cursor: hand;
                                                color: Blue; display :none;" onclick="window.open('../Print/YangPinFen.aspx?sampleid=<%#Eval("sampleid") %>')">
                                                <asp:Label ID="Label3" runat="server" Text="标签"></asp:Label></span><span style="cursor: hand;
                                                color: Blue;" onclick="window.open('YangPinSee2.aspx?yangpinid=<%#Eval("id") %>')">
                                                <asp:Label ID="Label7" runat="server" Text="查看"></asp:Label></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="附件" Text="附件" Target="_blank" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?baojiaid={0}&amp;&amp;id={1}"
                                    DataNavigateUrlFields="baojiaid,kehuid" />


                                     <asp:HyperLinkField HeaderText="配件" Text="配件" Visible ="false" Target="_blank" DataNavigateUrlFormatString="~/YangPin/YanPinManage.aspx?baojiaid={0}&&kehuid={1}&&sampleid={2}&&bianhao={3}"
                                    DataNavigateUrlFields="baojiaid,kehuid,sampleid,bianhao" />


                                     <asp:HyperLinkField HeaderText="流转" Text="流转" Target="_blank" DataNavigateUrlFormatString="~/Print/YangPinLiuZhuan.aspx?baojiaid={0}&&kehuid={1}&&sampleid={2}&&bianhao={3}"
                                    DataNavigateUrlFields="baojiaid,kehuid,sampleid,bianhao" />

                                     <asp:HyperLinkField HeaderText="任务号"  DataTextField ="anjianid" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=0"
                    DataNavigateUrlFields="bianhao" />

                     <asp:BoundField DataField="dajine" HeaderText="到账"  />
                      <asp:BoundField DataField="zt" HeaderText="状态"  />
                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>'
                                                 CommandName="BussinessNeeds" ForeColor="Green"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Green" />
                                    </asp:TemplateField>
                                    
                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                            <EmptyDataTemplate>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时没有数据"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第%StartRecordIndex%-%EndRecordIndex%"
                            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager2_PageChanged"
                            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                            Style="font-size: 9pt" UrlPaging="True" Width="682px"  PageSize ="15">
                        </webdiyer:AspNetPager>
                  
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

