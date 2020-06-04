<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YangPinManageCha2.aspx.cs" Inherits="YangPin_YangPinManageCha2" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查找任务关联样品</title>
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
        样品管理 》》样品关联<asp:Label ID="Label6" runat="server" Text=""></asp:Label></div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
    <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
        <tr>
            <td align="left" style="height: 25px">
               <input id="txFDate"  visible ="false"  runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text"  />
     
        <input id="txTDate" runat="server" class="TxCss" visible ="false" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text"  />
                <asp:DropDownList ID="DropDownList1" Visible ="false"  runat="server">
                    <asp:ListItem Value="0">全部</asp:ListItem>
                    <asp:ListItem Value="1">厂商名称</asp:ListItem>
                    <asp:ListItem Value="2">任务编号</asp:ListItem>
                    
                </asp:DropDownList>制造商或任务编号 
                <asp:DropDownList ID="DropDownList2" Visible ="false"  runat="server">
                    <asp:ListItem>入库</asp:ListItem>
                    <asp:ListItem>借出</asp:ListItem>
                    <asp:ListItem>归还</asp:ListItem>
                    <asp:ListItem>封存</asp:ListItem>
                    <asp:ListItem>清退</asp:ListItem>
                    <asp:ListItem>销毁</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
                <asp:Button ID="Button2" CssClass="BnCss" runat="server" OnClick="Button2_Click"
                    Text="查询任务" />
                <asp:Button ID="Button1" runat="server" CssClass="BnCss" Visible ="false" OnClick="Button1_Click"
                    Text="导出excel" />
                <asp:Button ID="Button3" runat="server" Text="样品登记" OnClick="Button3_Click"  Visible ="false" />
                <asp:Button ID="Button4" runat="server" Text="批量处理" OnClick="Button4_Click" Visible ="false"/>
                
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" DataKeyNames="rwbianhao" AutoGenerateColumns="false"
                            CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                    <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                         

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
                            Style="font-size: 9pt" UrlPaging="True" Width="682px">
                        </webdiyer:AspNetPager>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr><td align ="center" ><asp:Button ID="Button5" runat="server" Text="确定关联" onclick="Button5_Click" /></td></tr>
    </table>
    </form>
</body>
</html>



