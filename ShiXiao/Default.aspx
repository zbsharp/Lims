<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ShiXiao_Default" %>
<%@ Register Assembly="EeekSoft.Web.PopupWin" Namespace="EeekSoft.Web" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>时效监督 </title>
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
    <style type="text/css">
        .caozuo{display:none;}
    </style>

</head>
<body>
    <form name="form1" runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  

    

    <div>
        <div class="Body_Title">
            时效管理 》》进行中任务</div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
                        <asp:ListItem Value="rwbianhao">任务编号</asp:ListItem>
                        <asp:ListItem Value="shenqingbianhao">申请编号</asp:ListItem>
                        <asp:ListItem Value="kehu">客户名称</asp:ListItem>
                      <asp:ListItem Value="kf">项目经理</asp:ListItem>
                         <asp:ListItem Value="sq">申请暂停</asp:ListItem>

                    </asp:DropDownList>
                    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                        Text="查询" />
                    如果点击暂停,请填写原因：<asp:TextBox ID="TextBox2"   runat="server" Width="180px" 
                        ></asp:TextBox>
                    <asp:DropDownList ID="DropDownList2" AutoPostBack ="true"  onselectedindexchanged="DropDownList2_SelectedIndexChanged" runat="server">
                        <asp:ListItem>客户原因</asp:ListItem>
                        <asp:ListItem>内部原因</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList3" runat="server">
                    </asp:DropDownList>
                    <span style ="color:Red;">请认真选择暂停原因以用来统计，可在另外的暂停原因页面增加下拉框选项</span>
                </td>
            </tr>
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand"  OnRowDataBound="GridView1_RowDataBound">
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <Columns>

                     

                            <asp:BoundField DataField="rwbianhao" HeaderText="任务号" />
                            <asp:BoundField DataField="kehuname" HeaderText="实际客户" />
                            <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号" />
                            <asp:BoundField DataField="na" HeaderText="名称" />
                            <asp:BoundField DataField="gg" HeaderText="型号" />
                            <asp:BoundField DataField="day1" HeaderText="实际" />
                            <asp:BoundField DataField="day2" HeaderText="总共" />
                            <asp:BoundField DataField="xiada" HeaderText="下达日期"  />
                            <asp:BoundField DataField="shixian" HeaderText="考核" />
                            <asp:BoundField DataField="ziliaostate" HeaderText="资料状态"  />
                            <asp:BoundField DataField="st1" HeaderText="报告状态" />
                             <asp:BoundField DataField="state" HeaderText="案件状态" />

                            <asp:BoundField DataField="kf" HeaderText="客服" />
                            <asp:BoundField DataField="" HeaderText="工程师" />
                           

                           <asp:BoundField DataField="b2" HeaderText="申请暂停" />
                            <asp:HyperLinkField HeaderText="操作" Text="分派" HeaderStyle-CssClass="caozuo" ItemStyle-CssClass="caozuo" Target="_blank" DataNavigateUrlFormatString="~/Case/FenPaiKeFu.aspx?xiangmuid={0}"
                                DataNavigateUrlFields="rwbianhao" >
<HeaderStyle CssClass="caozuo"></HeaderStyle>

<ItemStyle CssClass="caozuo"></ItemStyle>
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="xiada" HeaderText="下达日期"  Visible ="false" />
                            
                              <asp:HyperLinkField HeaderText="明细" Text="明细" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=1"
                    DataNavigateUrlFields="bianhao" />

                        <asp:TemplateField HeaderText="操作" Visible="False" >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton5" runat="server" Text="暂停" CommandArgument='<%# Eval("rwbianhao") %>'
                            CommandName="xiada"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>

                  <asp:HyperLinkField HeaderText="附件" Text="上传" Target="_blank" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?id={0}&&baojiaid={1}"
                    DataNavigateUrlFields="id,baojiaid" />

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
                    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第%StartRecordIndex%-%EndRecordIndex%"
                        CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                        InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager2_PageChanged"
                        PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                        Style="font-size: 9pt" UrlPaging="True" PageSize="15" Width="100%">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
       <asp:Literal ID="ld" runat="server"></asp:Literal>

       <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" ontick="Timer1_Tick" Enabled ="false"  Interval="20000" >
        </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel> 
    <asp:Label runat="server" ID="lb2"></asp:Label>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" Visible="false">
        <ContentTemplate>
            <asp:Label runat="server" ID="lb"></asp:Label>
            <%--<cc1:popupwin id="pw" Title ="备注提示" runat="server" ShowAfter ="0" HideAfter ="100000" style="left: 0px; top: 0px; height: 132px; width: 226px;" 
                DragDrop="False"  ActionType ="OpenLink"  Link="TaskBeiZhu.aspx"  LinkTarget="_blank" Message ="" AutoShow="true" Visible="False"> </cc1:popupwin>--%>
        </ContentTemplate>  
        </asp:UpdatePanel>

    </form>
</body>
</html>
