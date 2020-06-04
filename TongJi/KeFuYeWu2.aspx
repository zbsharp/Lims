﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KeFuYeWu2.aspx.cs" Inherits="TongJi_KeFuYeWu2" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
      <script type="text/javascript" src="../js/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
        统计管理 》》客户业务</div>


         案件下达日期： <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />


      <asp:DropDownList
            ID="DropDownList1" runat="server" Width="77px" >
            
           <asp:ListItem Value ="kehu">客户名称</asp:ListItem>
       
        
           
              
        </asp:DropDownList>&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
        <asp:Button ID="Button2"  CssClass ="BnCss"  runat="server" 
             Text="查询" onclick="Button2_Click" />

 
                            <asp:Button ID="Button3" runat="server" 
        onclick="Button3_Click" Text="EXCEL" />

 
                            <table width="100%" class="Admin_Table" 
                                border="1">
                                <tr>
                                    <td style="width: 100%";  valign="top">
                                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                            OnRowDataBound="GridView1_RowDataBound" 
                                          
                                            CssClass="Admin_Table">
                                          
                                            <Columns>

                                                 <asp:TemplateField HeaderText="序 号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>'
                                        ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>

                                               
                                              <asp:BoundField DataField="rwbianhao" HeaderText="任务编号" />
                                              
                                                <asp:BoundField DataField="state" HeaderText="状态" />
                                              
                                            

                                     
                                            
                                            <asp:BoundField DataField="xiadariqi" HeaderText="下达日期" DataFormatString ="{0:N}"/>
                              
                               
                                           <asp:BoundField DataField="kf" HeaderText="客服"  DataFormatString ="{0:N}" />
                         
                            <asp:BoundField DataField="yu" HeaderText="申请编号" />

                                            </Columns>
                                          <HeaderStyle CssClass="Admin_Table_Title " />
                                        </asp:GridView>

                                           <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                        CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                        InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " PrevPageText="【前页】 "
                        ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                        Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="15" OnPageChanged="AspNetPager2_PageChanged">
                    </webdiyer:AspNetPager>

                                    </td>
                                </tr>
                               
                            </table>
                        
                             
    </form>
</body>
</html>


