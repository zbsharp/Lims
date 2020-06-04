<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SettleProject.aspx.cs" Inherits="SettleProject" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../js/calendar.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js">
    </script>
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
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <div class="Body_Title">
            统计管理 》》实验室结案明细
        </div>
        <div>
            完成日期：<input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
                style="width: 90px" type="text" visible="true" />
            到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />
            &nbsp;&nbsp;
            <asp:Button ID="Button_search" runat="server" Text="查询"
                OnClick="Button_search_Click" Style="width: 60px" />&nbsp;&nbsp;
            
        <asp:Button ID="Button_outport" runat="server" Text="导出"
            OnClick="Button_outport_Click" />
            <span style="color: red;">由于数据量过大查询比较慢请耐心等待</span>
            <br />

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="xmid">
                        <Columns>
                            <asp:BoundField DataField="受理日期" HeaderText="受理日期" />
                            <asp:BoundField DataField="要求完成时间" HeaderText="要求完成时间" />
                            <asp:BoundField DataField="收款抬头" HeaderText="收款抬头" />
                            <asp:BoundField DataField="报价编号" HeaderText="报价编号" />
                            <asp:BoundField DataField="任务编号" HeaderText="任务编号" />
                            <asp:BoundField DataField="业务员" HeaderText="业务员" />
                            <asp:BoundField DataField="所属部门" HeaderText="所属部门" />
                            <asp:BoundField DataField="销售助理" HeaderText="销售助理" />
                            <asp:BoundField DataField="公司名称" HeaderText="公司名称" />
                            <asp:BoundField DataField="产品名称" HeaderText="产品名称" />
                            <asp:BoundField DataField="主测型号" HeaderText="主测型号" />
                            <asp:BoundField DataField="测试项目" HeaderText="测试项目" />
                            <asp:BoundField DataField="项目标准" HeaderText="项目标准" />
                            <asp:BoundField DataField="客户类型" HeaderText="客户类型" />
                            <asp:BoundField DataField="是否加急" HeaderText="是否加急" />
                            <asp:BoundField HeaderText="报告号" />
                            <asp:BoundField DataField="实验室" HeaderText="实验室" />
                            <asp:BoundField HeaderText="分派人" />
                            <asp:BoundField HeaderText="工程师" />
                            <asp:BoundField HeaderText="测试员" />
                            <asp:BoundField DataField="项目状态" HeaderText="项目状态" />
                            <asp:BoundField DataField="案件状态" HeaderText="案件状态" />
                            <asp:BoundField DataField="结案日期" HeaderText="结案日期" />
                            <asp:BoundField DataField="feiyong" HeaderText="项目总费用" />
                            <asp:BoundField DataField="是否外包" HeaderText="是否外包" />
                            <asp:BoundField DataField="waibaofeiyong" HeaderText="规费" />
                            <asp:BoundField HeaderText="检测费" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                        CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                        InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
                        PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                        Style="font-size: 9pt" UrlPaging="false" PageSize="15">
                    </webdiyer:AspNetPager>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>
