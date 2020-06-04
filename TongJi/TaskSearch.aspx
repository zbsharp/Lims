<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskSearch.aspx.cs" Inherits="TongJi_TaskSearch" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
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
            统计管理 》》开案明细
        </div>
        <div>
            下达日期：<input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
                style="width: 90px" type="text" visible="true" />
            到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />
            <asp:Button ID="Button_search" runat="server" Text="查询"
                OnClick="Button_search_Click" />&nbsp;&nbsp;
        <asp:Button ID="Button_outport" runat="server" Text="导出"
            OnClick="Button_outport_Click" />
            <br />

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="dataid">
                        <Columns>
                            <asp:BoundField DataField="xiadariqi" HeaderText="下达日期" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="shijishijian" HeaderText="实际时间" />
                            <asp:BoundField DataField="baojiaid" HeaderText="合同编号" />
                            <asp:BoundField DataField="rwbianhao" HeaderText="任务号" />
                            <asp:BoundField DataField="feiyong" HeaderText="项目总费用" />
                            <asp:BoundField DataField="zhekou" HeaderText="折扣" />
                            <asp:BoundField DataField="yaoqiuwanchengriqi" HeaderText="要求完成日期" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="yewuyuan" HeaderText="业务员" />
                            <asp:BoundField DataField="xiaoshoutuandui" HeaderText="销售团队" />
                            <asp:BoundField DataField="xiaoshouzhuli" HeaderText="销售助理" />
                            <asp:BoundField DataField="fillname" HeaderText="排单客服" />
                            <asp:BoundField DataField="kehuname" HeaderText="公司名称" />
                            <asp:BoundField DataField="linkmanname" HeaderText="联系人" />
                            <asp:BoundField DataField="mobile" HeaderText="客户手机" />
                            <asp:BoundField DataField="telephone" HeaderText="客户电话" />
                            <asp:BoundField DataField="fukuandanwei" HeaderText="付款单位" />
                            <asp:BoundField DataField="chanpin" HeaderText="产品名称" />
                            <asp:BoundField DataField="xinghao" HeaderText="主测型号" />
                            
                            <asp:BoundField DataField="chanpinname" HeaderText="产品类型" />

                            <asp:BoundField DataField="ceshiname" HeaderText="案件项目" />
                            <asp:BoundField DataField="epiboly" HeaderText="是否外发" />
                            <asp:BoundField DataField="waibaofeiyong" HeaderText="外包费用" />
                            <asp:BoundField DataField="" HeaderText="检测费" />
                            <asp:BoundField DataField="" HeaderText="报告号" />
                            <asp:BoundField DataField="shiyanshi" HeaderText="实验室" />
                            <asp:BoundField DataField="" HeaderText="负责人" />
                            <asp:BoundField DataField="" HeaderText="工程师" />
                            <asp:BoundField DataField="kehuleixing" HeaderText="客户类型(案件优先级别)" />
                            <asp:BoundField DataField="" HeaderText="样品信息" />
                            <asp:BoundField DataField="fukuantaitou" HeaderText="收款抬头" />
                            <asp:BoundField DataField="wanchengriqi" HeaderText="结案日期" />
                            <asp:BoundField DataField="" HeaderText="报告发放日期" />
                            <asp:BoundField DataField="xmstate" HeaderText="案件状态" />
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
