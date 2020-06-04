<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YiFenYiShou.aspx.cs" Inherits="ShiXiao_YiFenYiShou" %>

<%@ Register Assembly="EeekSoft.Web.PopupWin" Namespace="EeekSoft.Web" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>已分已接收 </title>
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
        .BnCss {
        }

        .zhouqi {
            display: none;
        }
    </style>
</head>
<body>
    <form name="form1" runat="server" id="form1">
        <div>
            <div class="Body_Title">
                工程管理 》》任务查询
                <%--已分派工程师且已接收的任务(是否超期目前根据完成日期来判定,另外，如果在查询结果后要选择排序标准，请在选择排序标准后重新点击查询。)--%>
            </div>
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                width="100%">
                <tr bgcolor="#f4faff">
                    <td style="height: 5px">&nbsp;&nbsp;&nbsp; 查询条件：&nbsp;<asp:DropDownList ID="DropDownList2" runat="server" Width="77px">
                        <asp:ListItem>全部</asp:ListItem>
                        <asp:ListItem Value="taskid">任务编号</asp:ListItem>
                        <asp:ListItem Value="AnJianInFo.bumen">部门</asp:ListItem>
                        <asp:ListItem Value="weituodanwei">委托方</asp:ListItem>
                        <asp:ListItem Value="ZhuJianEngineer.name">工程师</asp:ListItem>
                        <asp:ListItem Value="AnJianInFo.baojiaid">报价号</asp:ListItem>
                        <asp:ListItem Value="baogaoid">报告号</asp:ListItem>
                    </asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:DropDownList ID="DropDownList3" runat="server">
                            <asp:ListItem Selected="True"></asp:ListItem>
                            <asp:ListItem>完成</asp:ListItem>
                            <asp:ListItem>进行中</asp:ListItem>
                            <asp:ListItem>中止</asp:ListItem>
                            <asp:ListItem>暂停</asp:ListItem>
                        </asp:DropDownList>
&nbsp;&nbsp;
                        <asp:TextBox ID="TextBox1" runat="server" Width="178px"></asp:TextBox>
                        &nbsp;
                        <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                            Text="查询" Width="72px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                            RepeatLayout="Flow" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                            <asp:ListItem Value="order by yaoqiuwanchengriqi desc">按完成日期排序</asp:ListItem>
                            <asp:ListItem Value="order by xiadariqi desc" Selected="True">按下达日期排序</asp:ListItem>
                        </asp:RadioButtonList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <%--任务状态：
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="122px" Height="17px">
                            <asp:ListItem>全部</asp:ListItem>
                            <asp:ListItem Value="进行中">进行中</asp:ListItem>
                            <asp:ListItem Value="暂停">暂停</asp:ListItem>
                            <asp:ListItem Value="完成">完成</asp:ListItem>
                        </asp:DropDownList>--%>
                        &nbsp;&nbsp;</td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="height: 5px">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="taskid" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated">
                            <HeaderStyle CssClass="Admin_Table_Title " />
                            <Columns>
                                <%--  <asp:BoundField DataField="beiyong" HeaderText="状态" />
                                <asp:HyperLinkField HeaderText="测试"  Text="审核" Target="_blank" DataNavigateUrlFormatString="~/Case/CeShi.aspx?id={0}"
                                    DataNavigateUrlFields="id" />--%>
                                <asp:TemplateField HeaderText="序 号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>'
                                            ForeColor="Green"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="taskid" HeaderText="任务号" />
                                <asp:BoundField DataField="anjianinfobaojia" HeaderText="报价号" />
                                <asp:BoundField DataField="kehuname" HeaderText="委托方" />
                                <asp:BoundField DataField="state1" HeaderText="状态" />
                                <asp:BoundField DataField="bumen" HeaderText="部门" />
                                <asp:BoundField DataField="name" HeaderText="工程师" />
                                <asp:BoundField DataField="xiadariqi" HeaderText="下达日期" />
                                <asp:BoundField DataField="yaoqiuwanchengriqi" HeaderText="要求完成" />
                                <asp:HyperLinkField HeaderText="样品" Text="样品" Target="_blank" DataNavigateUrlFormatString="~/YangPin/YangPinManage11.aspx?taskid={0}"
                                    DataNavigateUrlFields="taskid" />
                                <asp:HyperLinkField HeaderText="资料" Text="资料" Target="_blank" DataNavigateUrlFormatString="~/Case/ziliaoaddE.aspx?xiangmuid={0}"
                                    DataNavigateUrlFields="taskid" />
                                <asp:HyperLinkField HeaderText="报告" Text="报告" Target="_blank" DataNavigateUrlFormatString="~/Report/XinBaogaoADD.aspx?renwuid={0}"
                                    DataNavigateUrlFields="taskid" />
                                <asp:HyperLinkField HeaderText="通知书" Text="通知书" Target="_blank" DataNavigateUrlFormatString="~/Print/TaskPrint.aspx?bianhao={0}"
                                    DataNavigateUrlFields="tijiaobianhao" />
                                <asp:HyperLinkField Text="附件" HeaderText="附件" Target="button" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?id={0}&&baojiaid="
                                    DataNavigateUrlFields="tijiaobianhao" />
                                <asp:HyperLinkField HeaderText="查看" Text="查看" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=1"
                                    DataNavigateUrlFields="tijiaobianhao" />
                                <asp:HyperLinkField HeaderText="测试进度" Text="查看" DataNavigateUrlFormatString="~/Case/Test1.aspx?taskid={0}" DataNavigateUrlFields="taskid" Target="_blank"/>
                                <asp:HyperLinkField HeaderText="工程师" Text="修改" Target="_blank" DataNavigateUrlFormatString="~/Case/AnJianInFo.aspx?xiangmuid={0}&&anjianinfoid={1}&&id={2}"
                                    DataNavigateUrlFields="taskid,tijiaobianhao,anjianinfoid" />
                                <asp:HyperLinkField HeaderText="测试员" Text="修改" Target="_blank" DataNavigateUrlFormatString="~/Case/TestAdd.aspx?xiangmuid={0}&&anjianinfoid={1}&&id={2}" DataNavigateUrlFields="taskid,tijiaobianhao,anjianinfoid" />
                                <asp:BoundField DataField="zhouqi" HeaderText="周期" HeaderStyle-CssClass="zhouqi" ItemStyle-CssClass="zhouqi">
                                    <HeaderStyle CssClass="zhouqi"></HeaderStyle>
                                    <ItemStyle CssClass="zhouqi"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第%StartRecordIndex%-%EndRecordIndex%"
                            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager2_PageChanged"
                            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                            Style="font-size: 9pt" PageSize="15" Width="100%">
                        </webdiyer:AspNetPager>
                    </td>
                </tr>
            </table>
        </div>


        <asp:Label runat="server" ID="lb"></asp:Label>
        <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>
