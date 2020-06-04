<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pretreatment.aspx.cs" Inherits="Case_Pretreatment" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../js/calendar.js"></script>

    <style type="text/css">
        .zhouqi {
            display: none;
        }
    </style>
    <script type="text/javascript">
        var currentRowId = 0;
        function MarkRow(rowId) {
            if (document.getElementById(rowId) == null)
                return;

            if (document.getElementById(currentRowId) != null)
                document.getElementById(currentRowId).style.backgroundColor = '#ffffff';

            currentRowId = rowId;
            document.getElementById(rowId).style.backgroundColor = '#FFE0C0';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            工程管理 》》前处理
        </div>
        查询条件：&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
            <asp:ListItem Value="baojiaid">报价编号</asp:ListItem>
            <asp:ListItem Value="taskno">任务编号</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
        <asp:Button ID="Button2" CssClass="BnCss" runat="server" OnClick="Button2_Click"
            Text="查询" />

        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False"
            DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                            CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                <asp:BoundField DataField="taskid" HeaderText="任务编号" />
                <asp:BoundField DataField="baojiaid" HeaderText="报价号" />
                <asp:BoundField DataField="kehuname" HeaderText="委托方" />
                <asp:BoundField DataField="bumen" HeaderText="承接部门" />

                <asp:BoundField DataField="shiyanleibie" HeaderText="检测类别" />

                <asp:BoundField DataField="" HeaderText="工程师" Visible="false" />

                <asp:BoundField DataField="statea" HeaderText="状态" />


                <asp:BoundField DataField="type" HeaderText="主检" Visible="false" />
                <asp:BoundField DataField="xiadariqi" HeaderText="下达日期" />
                <asp:BoundField DataField="yaoqiuwanchengriqi" HeaderText="要求完成" />
                <asp:HyperLinkField HeaderText="样品" Text="样品" Target="_blank" DataNavigateUrlFormatString="~/YangPin/YangPinManage11.aspx?taskid={2}"
                    DataNavigateUrlFields="baojiaid,kehuid,taskid" />
                <asp:HyperLinkField HeaderText="资料" Text="资料" Target="_blank" DataNavigateUrlFormatString="~/Case/ziliaoaddE.aspx?xiangmuid={0}"
                    DataNavigateUrlFields="taskid" />

                <asp:HyperLinkField HeaderText="通知书" Text="通知书" Target="_blank" DataNavigateUrlFormatString="~/Print/TaskPrint.aspx?bianhao={0}"
                    DataNavigateUrlFields="tijiaobianhao" />
                <asp:HyperLinkField Text="附件" HeaderText="附件" Target="button" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?id={0}&&baojiaid="
                    DataNavigateUrlFields="tijiaobianhao" />


                <asp:HyperLinkField HeaderText="查看" Text="查看" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=0"
                    DataNavigateUrlFields="tijiaobianhao" />

                <asp:HyperLinkField HeaderText="前处理" Text="操作" Target="_blank" DataNavigateUrlFormatString="~/Case/OneDispose.aspx?xiangmuid={0}&&tijiaobianhao={1}&&id={2}" DataNavigateUrlFields="taskid,tijiaobianhao,id" />
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
            Width="682px" Style="font-size: 9pt" PageSize="15">
        </webdiyer:AspNetPager>
    </form>
</body>
</html>
