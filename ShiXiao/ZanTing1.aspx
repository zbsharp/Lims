<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZanTing1.aspx.cs" Inherits="ShiXiao_ZanTing1" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>暂停任务 </title>
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
</head>
<body>
    <form name="form1" runat="server" id="form1">
    <div>
        <div class="Body_Title">
            时效管理 》》暂停任务（单位：天，时限天数均除休息日外。）</div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="taskno">任务编号</asp:ListItem>
                        <asp:ListItem Value="kf">项目经理</asp:ListItem>
                          <asp:ListItem Value="shenqingbianhao">申请编号</asp:ListItem>
                        <asp:ListItem Value="weituodanwei">客户名称</asp:ListItem>

                    </asp:DropDownList>
                    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                        Text="查询" />
                </td>
            </tr>
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand"
                        OnRowDataBound="GridView1_RowDataBound">
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <Columns>
                            <asp:BoundField DataField="taskno" HeaderText="任务号" />
                            <asp:BoundField DataField="kehuname" HeaderText="客户" />
                            <asp:BoundField DataField="" HeaderText="工程师" />
                            <asp:BoundField DataField="" HeaderText="部门" />
                            <asp:BoundField DataField="shixian2" HeaderText="要求" />
                            <asp:BoundField DataField="shixian" HeaderText="考核" />
                            <asp:BoundField DataField="" HeaderText="资料状态"  Visible ="false" />
                            <asp:BoundField DataField="st1" HeaderText="进展状态" Visible ="false"/>
                            <asp:HyperLinkField HeaderText="资料" Text="资料" Target="_blank" DataNavigateUrlFormatString="~/Case/ziliaoaddE.aspx?xiangmuid={0}"
                                DataNavigateUrlFields="taskno" />
                            <asp:BoundField DataField="xiada" HeaderText="下达日期" />
                            <asp:BoundField DataField="yq" HeaderText="要求完成" />
                            <asp:HyperLinkField HeaderText="明细" Text="明细" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=0"
                                DataNavigateUrlFields="bianhao" />
                            <asp:TemplateField HeaderText="操作" Visible ="false" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton5" runat="server" Text="点击恢复" CommandArgument='<%# Eval("taskno") %>'
                                        CommandName="xiada"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>

                              <asp:HyperLinkField HeaderText="附件" Text="上传" Target="_blank" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?id={0}&&baojiaid={1}"
                    DataNavigateUrlFields="id,baojiaid" />

                     <asp:BoundField DataField="kf" HeaderText="客服" />

                        <asp:BoundField DataField="beizhu2" HeaderText="暂停原因" />

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
    </form>
</body>
</html>

