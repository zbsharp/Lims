<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CeShiFeiGcM.aspx.cs" Inherits="Case_CeShiFeiGcM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工程师上报费用</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <style type="text/css">
        .BnCss
        {}
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <div class="Body_Title">
        业务管理 》》上报费用查询</div>

    <div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>
    
<div>



     
     <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
    <tr bgcolor="#f4faff">
    <td style="height: 5px">
        &nbsp;&nbsp; 录入时间：<input id="txFDate" runat="server" class="TxCss" name="txFDate"
            onclick="popUpCalendar(this,document.forms[0].txFDate,'yyyy-mm-dd')"
            style="width: 90px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="popUpCalendar(this,document.forms[0].txTDate,'yyyy-mm-dd')" style="width: 90px" type="text" visible="true" />
        &nbsp;
        查询条件：<asp:DropDownList ID="DropDownList1" runat="server"
            Width="77px">
            <asp:ListItem Value="0">全部</asp:ListItem>
            <asp:ListItem Value="1">项目</asp:ListItem>
            <asp:ListItem Value="1">任务号</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
            Text="查询" />
        <asp:Button ID="Button1" runat="server" CssClass="BnCss" OnClick="Button1_Click"
            Text="导出Excel" Width="108px" />
        <asp:Button ID="Button3" runat="server" Text="打印检测收费清单" 
            onclick="Button3_Click" />
        </td>
    </tr>
         <tr bgcolor="#f4faff">
             <td style="height: 5px">
              <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
        
        
                           <asp:GridView ID="GridView1" runat="server" Width="100%"
                               AutoGenerateColumns="False" DataKeyNames="id"  CssClass="Admin_Table" onrowcommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" >
                                                       
                                                             
                                                       
                                                       
                                                       
                                                        <Columns>
                                                          
                                                          <asp:TemplateField HeaderText="序号" >
                                                                <ItemTemplate  >
                                                                     <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>' CommandArgument='<%# Eval("id") %>' CommandName="chakan"  ForeColor="Green"></asp:LinkButton>
                                                                    
                                                                </ItemTemplate>
                                                                 <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="type" HeaderText="类型" />
                                                            <asp:BoundField DataField="xiangmu" HeaderText="项目" />
                                                            <asp:BoundField DataField="shuliang" HeaderText="数量" />
                                                            <asp:BoundField DataField="feiyong" HeaderText="费用" />
                                                            <asp:BoundField DataField="date" HeaderText="日期" />
                                                            <asp:BoundField DataField="name" HeaderText="填写人" />
                                                            <asp:TemplateField HeaderText="明细">
                                                                <ItemTemplate  >
                                                                     <asp:LinkButton ID="LinkButton8" runat="server" Text="查看" ForeColor="blue"  CommandArgument='<%# Eval("id") %>' CommandName="chakan"  ></asp:LinkButton>       
                                                                </ItemTemplate>
                                                                 <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>
                                                          
                                                             <asp:TemplateField>
                            <HeaderTemplate>
                                请选择 />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                                                        </Columns>

                                                        <HeaderStyle CssClass="Admin_Table_Title " />
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
