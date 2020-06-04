<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseDaiFen21.aspx.cs" Inherits="Case_CaseDaiFen21" %>

<%@ Register Assembly="EeekSoft.Web.PopupWin" Namespace="EeekSoft.Web" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />

    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
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
        .zhouqi {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
            EnableScriptLocalization="false">
        </asp:ScriptManager>
        <div class="Body_Title">
            工程管理 》》已分未收
        </div>
        查询条件：&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
            <asp:ListItem Value="taskid">任务编号</asp:ListItem>
            <asp:ListItem Value="AnJianInFo.bumen">承接部门</asp:ListItem>
            <asp:ListItem Value="weituodanwei">委托方</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
        <asp:Button ID="Button2" CssClass="BnCss" runat="server" OnClick="Button2_Click"
            Text="查询" />

        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False"
            DataKeyNames="id1" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                            CommandArgument='<%# Eval("id1") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                <asp:BoundField DataField="taskid" HeaderText="任务编号" />
                <asp:BoundField DataField="bjid" HeaderText="报价号" />
                <asp:BoundField DataField="kehuname" HeaderText="委托方" />
                <asp:BoundField DataField="bumen" HeaderText="承接部门" />

                <asp:BoundField DataField="gc" HeaderText="工程师" />

                <asp:BoundField DataField="state1" HeaderText="状态" />


                <asp:BoundField DataField="xiadariqi" HeaderText="下达日期" />
                <asp:BoundField DataField="yaoqiuwanchengriqi" HeaderText="要求完成" />
                <asp:HyperLinkField HeaderText="样品" Text="样品" Target="_blank" DataNavigateUrlFormatString="~/YangPin/YangPinManage11.aspx?taskid={2}"
                    DataNavigateUrlFields="baojiaid,kehuid,taskid" />
                <asp:HyperLinkField HeaderText="资料" Text="资料" Target="_blank" DataNavigateUrlFormatString="~/Case/ziliaoaddE.aspx?xiangmuid={0}"
                    DataNavigateUrlFields="taskid" />
                <asp:HyperLinkField HeaderText="报告" Text="报告" Target="_blank" DataNavigateUrlFormatString="~/Report/XinBaogaoADD.aspx?renwuid={0}"
                    DataNavigateUrlFields="taskid" />

                <asp:HyperLinkField HeaderText="通知书" Text="通知书" Target="_blank" DataNavigateUrlFormatString="~/Print/TaskPrint.aspx?bianhao={0}"
                    DataNavigateUrlFields="tijiaobianhao" />
                <asp:HyperLinkField Text="案件" HeaderText="案件" Visible="false" Target="button" DataNavigateUrlFormatString="~/Case/AnJianInFo2.aspx?xiangmuid={0}"
                    DataNavigateUrlFields="taskid" />

                <asp:HyperLinkField HeaderText="查看" Text="查看" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=0"
                    DataNavigateUrlFields="tijiaobianhao" />
                <asp:TemplateField HeaderText="接收">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton7" runat="server" Text="接收" CommandArgument='<%# Eval("id1") %>'
                            CommandName="cancel2"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>

                <asp:HyperLinkField Text="修改" HeaderText="修改" Target="button" DataNavigateUrlFormatString="~/Case/AnJianInFo.aspx?xiangmuid={0}&&bumen={1}&&id={2}"
                    DataNavigateUrlFields="taskid,bumen,id" Visible="False" />
                <asp:BoundField DataField="zhouqi" HeaderText="周期" HeaderStyle-CssClass="zhouqi" ItemStyle-CssClass="zhouqi">
                    <HeaderStyle CssClass="zhouqi"></HeaderStyle>
                    <ItemStyle CssClass="zhouqi"></ItemStyle>
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
            </EmptyDataTemplate>
            <HeaderStyle CssClass="Admin_Table_Title " />
        </asp:GridView>

        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
            Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="15">
        </webdiyer:AspNetPager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label runat="server" ID="lb"></asp:Label>
                <cc1:PopupWin ID="pw" Title="备注提示" runat="server" ShowAfter="0" HideAfter="100000" Style="left: 0px; top: 0px; height: 37px; width: 76px;"
                    DragDrop="False" ActionType="OpenLink" Link="../ShiXiao/TaskBeiZhu.aspx" LinkTarget="_blank" Height="109px"
                    Width="214px" Message="" AutoShow="true" Visible="False"></cc1:PopupWin>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Literal ID="ld" runat="server"></asp:Literal>

    </form>
</body>
</html>

