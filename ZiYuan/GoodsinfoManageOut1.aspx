<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoodsinfoManageOut1.aspx.cs" Inherits="ZiYuan_GoodsinfoManageOut1" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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
                        <strong>待校资产</strong></td>
                </tr>
            </table>
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
        <tr bgcolor="#f4faff">
            <td align="left">
                &nbsp; 购买日期：<input id="txFDate" runat="server" class="TxCss" name="txFDate"
            onclick="popUpCalendar(this,document.forms[0].txFDate,'yyyy-mm-dd')"
            style="width: 100px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="popUpCalendar(this,document.forms[0].txTDate,'yyyy-mm-dd')" style="width: 100px" type="text" visible="true" />
        查询条件：&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
            <asp:ListItem Value="0">全部</asp:ListItem>
            <asp:ListItem Value="1">名称</asp:ListItem>
            <asp:ListItem Value="2">联系人</asp:ListItem>
            <asp:ListItem Value="3">电话</asp:ListItem>
            <asp:ListItem Value="4">地址</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
            Text="查询" />
                <asp:Button ID="Button1" runat="server" CssClass="BnCss" OnClick="Button1_Click"
                    Text="导出excel" /></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td align="left">
              <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
        
        
                           <asp:GridView ID="GridView1" runat="server" Width="100%" 
                               AutoGenerateColumns="False" DataKeyNames="id"  CssClass="Admin_Table" onrowcommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" >
                                                       
                                                       
                                                        <HeaderStyle CssClass="Admin_Table_Title " />
                                                       
                                                        <Columns>
                                                          
                                                          <asp:TemplateField HeaderText="序号">
                                                                <ItemTemplate  >
                                                                     <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>' CommandArgument='<%# Eval("bianhao") %>' CommandName="chakan"  ForeColor="Green"></asp:LinkButton>
                                                                    
                                                                </ItemTemplate>
                                                                 <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="jq_name" HeaderText="名称" />
                                                            <asp:BoundField DataField="gongyingshang" HeaderText="供应商名称" />
                                                            <asp:BoundField DataField="gys_address" HeaderText="地址" />
                                                            <asp:BoundField DataField="gys_linkman" HeaderText="联系人" />
                                                            <asp:BoundField DataField="gys_tel" HeaderText="电话" />
                                                            <asp:BoundField DataField="remark" HeaderText="备注" />
                                                            <asp:BoundField DataField="buydate" DataFormatString="{0:d}" HeaderText="购买日期" />
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