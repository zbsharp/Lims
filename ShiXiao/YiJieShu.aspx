<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YiJieShu.aspx.cs" Inherits="ShiXiao_YiJieShu" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>已结束任务 </title>
   <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>

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
    <form name="form1" runat="server" id="form1">

    <div>
        <div class="Body_Title">
          时效管理 》》已结束任务</div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
                        <asp:ListItem Value="0">全部</asp:ListItem>

                        <asp:ListItem Value="rwbianhao">任务编号</asp:ListItem>
                      <asp:ListItem Value="kf">项目经理</asp:ListItem>
                     <asp:ListItem Value="shenqingbianhao">申请编号</asp:ListItem>
                     <asp:ListItem Value="kehuname">客户名称</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>

                    完成日期：
    <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
        style="width: 90px" type="text" visible="true" />
    到
    <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
        style="width: 90px" type="text" visible="true" />



                    <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                        Text="查询" />
                </td>
            </tr>
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" >
                      <HeaderStyle CssClass="Admin_Table_Title " />
                        <Columns>
                            
                          
                           


                     <asp:BoundField DataField="rwbianhao" HeaderText="任务号" />

                            <asp:BoundField DataField="kehuname" HeaderText="客户" />
                          
                     
                        
                               <asp:BoundField DataField="shixian2" HeaderText="客户要求时限" />
                            <asp:BoundField DataField="shixian" HeaderText="考核时限" />
                            <asp:BoundField DataField="day1" HeaderText="实际用时" />
                           
                            <asp:BoundField DataField="state1" HeaderText="状态" />
                          

                         <asp:BoundField DataField="xiada" HeaderText="下达日期" />

                         <asp:BoundField DataField="beizhu3" HeaderText="实际完成日期"  />
                           <asp:HyperLinkField HeaderText="明细" Text="明细" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=0"
                    DataNavigateUrlFields="bianhao" />
                    <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号" />

                     <asp:TemplateField HeaderText="序号" >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>'
                            CommandArgument='<%# Eval("id") %>' CommandName="BussinessNeeds" ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>

                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>



                    <webdiyer:aspnetpager id="AspNetPager2" runat="server" custominfohtml="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第%StartRecordIndex%-%EndRecordIndex%"
                        custominfotextalign="Center" firstpagetext="【首页】" height="25px" horizontalalign="Center"
                        inputboxstyle="width:19px" lastpagetext="【尾页】" nextpagetext="【下页】 " onpagechanged="AspNetPager2_PageChanged"
                        prevpagetext="【前页】 " showcustominfosection="Left" showinputbox="Never" shownavigationtooltip="True"
                        style="font-size: 9pt" urlpaging="True" PageSize ="15" width="100%">
                      </webdiyer:aspnetpager>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>


