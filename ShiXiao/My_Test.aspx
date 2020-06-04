﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="My_Test.aspx.cs" Inherits="ShiXiao_YiFenYiShou" %>

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
                //document.getElementById(currentRowId).style.backgroundColor = '#ffffff';

                currentRowId = rowId;
            //document.getElementById(rowId).style.backgroundColor = '#FFE0C0';
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
                工程管理 》》已分派工程师且已接收的任务(是否超期目前根据完成日期来判定,另外，如果在查询结果后要选择排序标准，请在选择排序标准后重新点击查询。)
            </div>
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                width="100%">
                <tr bgcolor="#f4faff">
                    <td style="height: 5px">
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="100px">
                            <asp:ListItem Value="0">下达/进行中</asp:ListItem>
                            <asp:ListItem Value="1">暂停</asp:ListItem>
                            <asp:ListItem Value="2">完成</asp:ListItem>

                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>或</asp:ListItem>
                            <asp:ListItem>且</asp:ListItem>
                        </asp:DropDownList>
                        任务号,申请号,客户名称,工程师,项目经理,部门,试验类别，制造商
                    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
                        <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                            Text="查询" />
                        <asp:TextBox ID="TextBox2" runat="server" Visible="false" Width="59px"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server" Text="选择部门后点处理即可取消参与(可取反)"></asp:Label>
                        <asp:DropDownList ID="DropDownList4" Visible="false" runat="server">
                            <asp:ListItem>取消</asp:ListItem>
                            <asp:ListItem>恢复</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownList3" runat="server">
                        </asp:DropDownList>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                            RepeatLayout="Flow" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                            <asp:ListItem Value="order by convert(datetime,yaoqiuwanchengriqi) desc">按完成日期排序</asp:ListItem>
                            <asp:ListItem Value="order by convert(datetime,xiadariqi) desc" Selected="True">按下达日期排序</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="height: 5px">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="rwbianhao" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand"
                            OnRowDataBound="GridView1_RowDataBound">
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
                                <asp:BoundField DataField="rwbianhao" HeaderText="任务号" />
                                <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号" />
                                <asp:BoundField DataField="weituodanwei" HeaderText="委托方" />
                                <asp:BoundField DataField="shiyanleibie" HeaderText="类别" />
                                <asp:BoundField DataField="" HeaderText="承接部门" />
                                <asp:BoundField DataField="" HeaderText="工程师" />
                                <asp:BoundField DataField="" Visible="false" HeaderText="实际" />
                                <asp:BoundField DataField="" Visible="false" HeaderText="总共" />
                                <asp:BoundField DataField="xiada" HeaderText="下达" Visible="false" />
                                <asp:BoundField DataField="yaoqiuwanchengriqi" HeaderText="要求" />
                                <asp:BoundField DataField="day1" HeaderText="用时" />
                                <asp:BoundField DataField="st1" HeaderText="报告状态" Visible="false" />
                                <asp:BoundField DataField="state" HeaderText="状态" />
                                <asp:BoundField DataField="kf" HeaderText="客服" />
                                <asp:HyperLinkField HeaderText="样品" Text="样品" Target="_blank" DataNavigateUrlFormatString="~/YangPin/YangPinManage11.aspx?taskid={2}"
                                    DataNavigateUrlFields="baojiaid,kehuid,rwbianhao" />
                                <asp:HyperLinkField HeaderText="资料" Text="资料" Target="_blank" DataNavigateUrlFormatString="~/Case/ziliaoaddE.aspx?xiangmuid={0}"
                                    DataNavigateUrlFields="rwbianhao" />
                                <asp:HyperLinkField HeaderText="报告" Text="报告" Target="_blank" DataNavigateUrlFormatString="~/Report/BaogaoAdd.aspx?renwuid={0}"
                                    DataNavigateUrlFields="rwbianhao" />
                                <asp:HyperLinkField HeaderText="通知书" Text="通知书" Target="_blank" DataNavigateUrlFormatString="~/Print/TaskPrint.aspx?bianhao={0}"
                                    DataNavigateUrlFields="bianhao" />
                                <asp:HyperLinkField Text="附件" HeaderText="附件" Target="button" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?id={0}&&baojiaid="
                                    DataNavigateUrlFields="bianhao" />
                                <asp:HyperLinkField HeaderText="查看" Text="查看" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=1"
                                    DataNavigateUrlFields="bianhao" />
                                <asp:HyperLinkField HeaderText="测试进度" Text="查看" DataNavigateUrlFormatString="~/Case/Test1.aspx?taskid={0}" DataNavigateUrlFields="rwbianhao" />
                                <asp:HyperLinkField HeaderText="测试员" Text="修改" DataNavigateUrlFormatString="~/Case/TestAdd.aspx?xiangmuid={0}" DataNavigateUrlFields="rwbianhao" />
                                <asp:TemplateField HeaderText="参与">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton6" runat="server" Text="处理" CommandArgument='<%# Eval("rwbianhao") %>'
                                            CommandName="cancel1"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ziliaostate" HeaderText="资料状态" />






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


        <asp:Label runat="server" ID="lb"></asp:Label>
        <cc1:PopupWin ID="pw" Title="备注提示" runat="server" ShowAfter="0" HideAfter="100000" Style="left: 0px; top: 0px; height: 17px; width: 60px;"
            DragDrop="False" ActionType="OpenLink" Link="TaskBeiZhu.aspx" LinkTarget="_blank" Message="" AutoShow="true" Visible="False"></cc1:PopupWin>


        <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>
