<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModuleInsert.aspx.cs" Inherits="SysManage_ModuleInsert" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
            EnableScriptLocalization="false">
        </asp:ScriptManager>
        <div class="Body_Title">
            系统管理 》》分配权限
        </div>
        查询条件：用户名称或职位名称或部门名称 &nbsp;
    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
        <asp:Button ID="Button2" CssClass="BnCss" runat="server" OnClick="Button2_Click"
            Text="查询" /><asp:Button ID="Button1" CssClass="BnCss" runat="server" OnClick="Button1_Click"
                Text="选择人员和权限后确定保存" /><br />

        <asp:DropDownList ID="DropDownList2" Visible="false" runat="server">
        </asp:DropDownList>


        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="15" RepeatDirection="Horizontal"
            RepeatLayout="Flow">
            <asp:ListItem Value="customer_1">分派客户</asp:ListItem>
            <asp:ListItem Value="quotation_1">审批报价</asp:ListItem>

            <asp:ListItem Value="quotation_2">审批低折报价</asp:ListItem>


            <asp:ListItem Value="quotation_3">修改开案后报价</asp:ListItem>

            <asp:ListItem Value="XieYi_1">协议管理</asp:ListItem>
            <asp:ListItem Value="PriceUpdate">价格增改</asp:ListItem>
            <asp:ListItem Value="CustomerList">客户列表</asp:ListItem>

            <asp:ListItem Value="notice">通知管理</asp:ListItem>

            <asp:ListItem Value="CaseXiaDa">案件下达</asp:ListItem>
            <asp:ListItem Value="anjianzanting">案件暂停</asp:ListItem>
            <asp:ListItem Value="anjianguanbi">案件关闭</asp:ListItem>
            <asp:ListItem Value="anjianhuifu">案件恢复</asp:ListItem>
            <asp:ListItem Value="anjianzt">修改案件状态</asp:ListItem>
            <asp:ListItem Value="xiaoshou">修改销售员</asp:ListItem>

            <asp:ListItem Value="yanqi">延期处理</asp:ListItem>
            <asp:ListItem Value="shangbao">修改上报费用</asp:ListItem>

            <asp:ListItem Value="FenPaiGongChengShi">分派工程</asp:ListItem>
            <asp:ListItem Value="quxiaocanyu">取消参与</asp:ListItem>
            <asp:ListItem Value="renwujieshou">任务接收</asp:ListItem>
            <asp:ListItem Value="fenpaikefu">分派客服</asp:ListItem>
            <asp:ListItem Value="chakanshoufei">查看收费</asp:ListItem>
            <asp:ListItem Value="hesuanfeiyong">核算费用</asp:ListItem>
            <asp:ListItem Value="baogaobianyin">报告编印</asp:ListItem>
            <asp:ListItem Value="xiangmujingli">项目经理</asp:ListItem>
            <asp:ListItem Value="yangpinguanli">样品管理</asp:ListItem>
            <asp:ListItem Value="baogaofafang">报告发放</asp:ListItem>
            <asp:ListItem Value="baogaoshenpi">报告审批</asp:ListItem>
            <asp:ListItem Value="baogaoquxiao">报告取消审批</asp:ListItem>
            <asp:ListItem Value="kehu">客户管理</asp:ListItem>
            <asp:ListItem Value="baojia">报价管理</asp:ListItem>
            <asp:ListItem Value="yewu">业务管理</asp:ListItem>
            <asp:ListItem Value="gongcheng">工程管理</asp:ListItem>
            <asp:ListItem Value="caiwu">财务管理</asp:ListItem>
            <asp:ListItem Value="shixiao">时效管理</asp:ListItem>
            <asp:ListItem Value="tongji">统计管理</asp:ListItem>
            <asp:ListItem Value="baogao">报告管理</asp:ListItem>
            <asp:ListItem Value="zhiliangguanli">质量管理</asp:ListItem>
            <asp:ListItem Value="zhiliangluru">质量录入</asp:ListItem>
            <asp:ListItem Value="wenjian">文件管理</asp:ListItem>
            <asp:ListItem Value="wenjian5">设备管理</asp:ListItem>
            <asp:ListItem Value="wenjian2">资产采购</asp:ListItem>
            <asp:ListItem Value="wenjian3">资产验收</asp:ListItem>
            <asp:ListItem Value="wenjian4">资产校准</asp:ListItem>
            <asp:ListItem Value="commonality">公共客户</asp:ListItem>
            <asp:ListItem>分派测试员</asp:ListItem>
            <asp:ListItem>正式报告</asp:ListItem>
            <asp:ListItem>总经理审批</asp:ListItem>
            <asp:ListItem>人事管理</asp:ListItem>
            <asp:ListItem>财务对账</asp:ListItem>
            <asp:ListItem>工程明细统计</asp:ListItem>
        </asp:CheckBoxList>



        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged"
                    Text="全选人员" />
                <div style="height: 420px; overflow: scroll">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False"
                        DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#(Container.DisplayIndex+1).ToString("000")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckBox3" Enabled="false" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="username" HeaderText="用户编号" ReadOnly="True" />
                            <asp:BoundField DataField="departmentname" HeaderText="所在部门" />
                            <asp:BoundField DataField="jiaosename" HeaderText="职位名称" />
                            <asp:BoundField DataField="youxiang" HeaderText="电子邮箱" />
                            <asp:BoundField DataField="banggongdianhua" HeaderText="办公电话" />
                            <asp:BoundField DataField="yidong" HeaderText="移动电话" />
                            <asp:BoundField DataField="fax" HeaderText="传真" />
                            <asp:BoundField DataField="shortphone" HeaderText="短号" />
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
            Style="font-size: 9pt" UrlPaging="True" PageSize="12">
        </webdiyer:AspNetPager>




        <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>
