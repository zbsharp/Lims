<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Manage.aspx.cs" Inherits="ZiYuan_Manage" %>
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
   <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
  <script type="text/javascript" src="../js/calendar.js"></script>


</head>
<body>
    <form name="form1"  runat="server"  id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>
<div>



       <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"   width="100%">
                <tr bgcolor="#f4faff">
                    <td>
                        <strong>未验收资产</strong></td>
                </tr>
            </table>
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
        <tr bgcolor="#f4faff">
            <td align="left">
                &nbsp; 购买日期：<input id="txFDate" runat="server" class="TxCss" name="txFDate"
            onclick="new Calendar().show(this.form.txFDate);"
            style="width: 100px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);" style="width: 100px" type="text" visible="true" />
        查询条件：&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
            <asp:ListItem Value="0">全部</asp:ListItem>
            <asp:ListItem Value="1">名称</asp:ListItem>
            <asp:ListItem Value="2">型号</asp:ListItem>
            <asp:ListItem Value="3">申请部门</asp:ListItem>
            <asp:ListItem Value="4">申请人</asp:ListItem>
            <asp:ListItem Value="5">测试</asp:ListItem>
            <asp:ListItem Value="6">状态</asp:ListItem>

            <asp:ListItem Value="7">编号</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
            Text="查询" />
            </td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left">
              <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
        
        
                           <asp:GridView ID="GridView1" runat="server" Width="100%" 
                               AutoGenerateColumns="False" DataKeyNames="id"  CssClass="Admin_Table" 
                               style="font-size: 9pt" onrowcommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" >
                                                       
                                                        <HeaderStyle CssClass="Admin_Table_Title " />
                                                       
                                                        <Columns>
                                                          
                                                          <asp:TemplateField HeaderText="序号">
                                                                <ItemTemplate  >
                                                                     <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>' CommandArgument='<%# Eval("id") %>' CommandName="chakan"  ForeColor="Green"></asp:LinkButton>
                                                                    
                                                                </ItemTemplate>
                                                                 <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="bianhao" HeaderText="编号" />
                                                            <asp:BoundField DataField="jq_name" HeaderText="名称" />
                                                            <asp:BoundField DataField="jq_id" HeaderText="型号" />
                                                            <asp:BoundField DataField="sqbumen" HeaderText="申请部门" />
                                                            <asp:BoundField DataField="sqby" HeaderText="申请人" />
                                                            <asp:BoundField DataField="daohuoflag" HeaderText="是否到货" />
                                                            <asp:BoundField DataField="state1" HeaderText="状态" />
                                                            <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="填写日期" />
                                                            
                                                            
                                                              <asp:TemplateField HeaderText="查看">
                                                                <ItemTemplate  >
                                      <asp:LinkButton ID="LinkButton8" runat="server" Text="明细" ForeColor="blue" CommandArgument='<%# Eval("bianhao") %>' CommandName="chakan"  ></asp:LinkButton>
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
                                                        width="682px" style="font-size: 9pt" UrlPaging="True" PageSize="15">
              </webdiyer:aspnetpager>
                  </ContentTemplate>
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                  </Triggers>
              </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    


       </div> 

</form>
</body>
</html>
