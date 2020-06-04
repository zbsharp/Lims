using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data.SqlClient;
using System.Configuration;
public partial class Account_Menu : System.Web.UI.Page
{
    protected string ManageMenu = string.Empty;
    protected string ManageMenuTitle = string.Empty;
    protected string MenuId = "";
    protected string departid = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"] == null)
        {
            Response.Write("<script>alert('请重新登录');top.location.href='../Default.aspx'</script>");
        }

        string sql = "select departmentid from userinfo where username='" + Session["UserName"].ToString() + "'";
        MyExcutSql ext = new MyExcutSql();
        departid = ext.ExcutSql2(sql, 0);
        bool AB = false;
        AB = limit1("项目经理");



        if (Request.QueryString["MenuId"] != null)
        {
            MenuId = Request.QueryString["MenuId"].ToString();
        }
        else
        {
            MenuId = "2";
        }
        if (Session["Role"] == null)
        {

        }
        else if (MenuId == "1")
        {

            if (Session["Role"].ToString() == "8")
            {
                ManageMenuTitle = "系统管理";
                ManageMenu += "<li><a href=\"../SysManage/UserList.aspx\" target=\"main\">用户信息</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/PersonConfig.aspx\" target=\"main\">人员配置</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/Personconfigconcel.aspx\" target=\"main\">取消配置</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/ModuleInsert.aspx\" target=\"main\">权限分配</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/ModuleSearch.aspx\" target=\"main\">权限查询</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/LoginList.aspx\" target=\"main\">登录日志</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/AddClause.aspx\" target=\"main\">增加条款</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/DataBase_department.aspx\" target=\"main\">增加部门</a></li>";
                ManageMenu += "<li><a href=\"../Case/zengjiaxiangmu.aspx\" target=\"main\">试验类别</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/ChanpinAdd.aspx\" target=\"main\">价格录入</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/ChanpinManage.aspx\" target=\"main\">价格查询</a></li>";
                ManageMenu += "<li><a href=\"../Quotation/Default4.aspx\" target=\"main\">价格目录</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/ZiLiaoTypeManage.aspx\" target=\"main\">资料列表</a></li>";

                //ManageMenu += "<li><a href=\"../SysManage/ZiLiaoTypeAdd.aspx\" target=\"main\">资料增加</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/GongChenAdd.aspx\" target=\"main\">工程任务</a></li>";

                //ManageMenu += "<li><a href=\"../Case/zengjiachaoqi.aspx\" target=\"main\">增加组别</a></li>";

                //ManageMenu += "<li><a href=\"../Case/ShangBaoType.aspx\" target=\"main\">上报类型</a></li>";

                //ManageMenu += "<li><a href=\"../Quotation/SendCheckUnit.aspx\" target=\"main\">测试项目</a></li>";

                //ManageMenu += "<li><a href=\"../Customer/Customernew.aspx\" target=\"main\">客户新页面</a></li>";
                ManageMenu += "<li><a href=\"../Case/FenPaiKeFu.aspx\" target=\"main\">分派销售助理</a></li>";
            }
            else
            {
                ManageMenuTitle = "没有权限";
            }

        }
        else if (MenuId == "2")
        {

            if (Session["Role"].ToString() == "8" || Session["Role"].ToString() == "1" || limit1("客户管理"))
            {
                ManageMenuTitle = "客户管理";
                //ManageMenu += "<li><a href=\"../SysManage/NoticeAdd.aspx\" target=\"main\">发布通知</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/NoticeList.aspx\" target=\"main\">通知列表</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/ChuCuoLeiBieM3.aspx?type=2\" target=\"main\">仪校证书</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/ChuCuoLeiBieM4.aspx?type=2\" target=\"main\">文件标准</a></li>";
                ManageMenu += "<li><a href=\"../Customer/CustomerAdd.aspx\" target=\"main\">新增客户</a></li>";
                ManageMenu += "<li><a href=\"../Customer/CustManage.aspx\" target=\"main\">我的客户</a></li>";
                //ManageMenu += "<li><a href=\"../Customer/CustomerAssign.aspx\" target=\"main\">未审客户</a></li>";
                //ManageMenu += "<li><a href=\"../Customer/CustomerAssigned.aspx\" target=\"main\">已审客户</a></li>";
                //ManageMenu += "<li><a href=\"../Customer/CustomerAssign1.aspx\" target=\"main\">被请分派</a></li>";
                ManageMenu += "<li><a href=\"../Customer/CustTraceList.aspx\" target=\"main\">跟踪记录</a></li>";
                // ManageMenu += "<li><a href=\"../Customer/XieYiList.aspx\" target=\"main\">协议管理</a></li>";
                ManageMenu += "<li><a href=\"../Customer/CustManage3.aspx\" target=\"main\">客户列表</a></li>";
                ManageMenu += "<li><a href=\"../Customer/CustomerCommonality.aspx\" target=\"main\">公共客户</a></li>";
                ManageMenu += "<li><a href=\"../Customer/TurnCustomerlog.aspx\" target=\"main\">客户分派记录</a></li>";
                //ManageMenu += "<li><a href=\"../Customer/ImportCustomerList.aspx\" target=\"main\">导入客户</a></li>";
                //ManageMenu += "<li><a href=\"../Customer/cs.aspx\" target=\"main\">公开客户</a></li>";
                //ManageMenu += "<li><a href=\"../Income/Cs.aspx\" target=\"main\">已超期任务</a></li>";
                ManageMenu += "<li><a href=\"../Quotation/YiShouLi.aspx\" target=\"main\">任务列表</a></li>";
                //ManageMenu += "<li><a href=\"../Case/CeShiFeiKfMYW.aspx\" target=\"main\">收费单列表</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/addbook.aspx\" target=\"main\">通讯目录</a></li>";
                //ManageMenu += "<li><a href=\"../Customer/ConfigManage.aspx\" target=\"main\">配置查看</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/ModuleSearch1.aspx\" target=\"main\">权限管理</a></li>";
                ManageMenu += "<li><a href=\"../Quotation/QuotationHuiqian.aspx\" target=\"main\">销售排单</a></li>";
                ManageMenu += "<li><a href=\"../Quotation/Paidanrecord.aspx\" target=\"main\">排单记录</a></li>";
                ManageMenu += "<li><a href=\"../Customer/Claim.aspx\" target=\"main\">到账认领</a></li>";
                ManageMenu += "<li><a href=\"../Report/Zhengshi.aspx\" target=\"main\">正式报告</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/Update.aspx?id=person\" target=\"main\">个人信息</a></li>";
            }
            else
            {
                //ManageMenuTitle = "没有权限";
                ManageMenu += "<li><a href=\"../SysManage/Update.aspx?id=person\" target=\"main\">个人信息</a></li>";
            }
        }
        else if (MenuId == "4")
        {

            if (Session["Role"].ToString() == "8" || Session["Role"].ToString() == "1" || limit1("财务管理"))
            {
                ManageMenuTitle = "财务管理";
                ManageMenu += "<li><a href=\"../Quotation/QuotationApproed.aspx\" target=\"main\">报价确认</a></li>";
                ManageMenu += "<li><a href=\"../Income/HeSuanfeiyong.aspx\" target=\"main\">修改核算费用</a></li>";
                //ManageMenu += "<li><a href=\"../Case/CeShiFeiKfM.aspx\" target=\"main\">请款列表</a></li>";
                ManageMenu += "<li><a href=\"../Case/CeShiFeiKfMYW.aspx\" target=\"main\">收费单列表</a></li>";

                //ManageMenu += "<li><a href=\"../Income/CashinManage.aspx\" target=\"main\">凭证列表</a></li>";
                ManageMenu += "<li><a href=\"../Income/daoexcel.aspx\" target=\"main\">银行到账</a></li>";

                ManageMenu += "<li><a href=\"../Income/ShuiPiaoAdd.aspx?quere=fou\" target=\"main\">现金到账</a></li>";
                ManageMenu += "<li><a href=\"../Income/IncomeCheck.aspx\" target=\"main\">到账列表</a></li>";
                ManageMenu += "<li><a href=\"../Income/YirenWeidui.aspx\" target=\"main\">查看认领</a></li>";
                ManageMenu += "<li><a href=\"../Report/BaoGaoListFaFang.aspx\" target=\"main\">报告发放</a></li>";
                ManageMenu += "<li><a href=\"../Customer/CustManage3.aspx\" target=\"main\">客户列表</a></li>";
                ManageMenu += "<li><a href=\"../Income/Exchange.aspx\" target=\"main\">汇率维护</a></li>";
                ManageMenu += "<li><a href=\"../Income/DiscountData.aspx\" target=\"main\">报价数据</a></li>";
                ManageMenu += "<li><a href=\"../Income/EMCarrival.aspx\" target=\"main\">现场测试到账</a></li>";
                //ManageMenu += "<li><a href=\"../Income/IncomeCheck.aspx\" target=\"main\">财务开票</a></li>";
                //ManageMenu += "<li><a href=\"../Income/CustomerJiePiao.aspx\" target=\"main\">财务借票</a></li>";

                //ManageMenu += "<li><a href=\"../Income/FapiaoManage.aspx\" target=\"main\">发票发放</a></li>";

                //ManageMenu += "<li><a href=\"../Income/FapiaoManage2.aspx\" target=\"main\">借票查询</a></li>";
                //ManageMenu += "<li><a href=\"../Income/CaseIncome4.aspx\" target=\"main\">查看任务</a></li>";
            }
            else
            {
                ManageMenuTitle = "没有权限";
            }
        }
        else if (MenuId == "3")
        {

            if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "8" || limit1("工程管理"))
            {

                ManageMenuTitle = "工程管理";

                //ManageMenu += "<li><a href=\"../YangPin/YangPinManage6.aspx\" target=\"main\">未还样品</a></li>";
                //ManageMenu += "<li><a href=\"../YangPin/YangPin_Jiechu22.aspx\" target=\"main\">转借样品</a></li>";
                //ManageMenu += "<li><a href=\"../YangPin/YangPinManage6.aspx\" target=\"main\">样品时效</a></li>";
                ManageMenu += "<li><a href=\"../Case/Pretreatment.aspx\" target=\"main\">前处理</a></li>";
                ManageMenu += "<li><a href=\"../Case/CaseDaiFen2.aspx\" target=\"main\">待分工程师</a></li>";
                ManageMenu += "<li><a href=\"../Case/CaseDaiFen21.aspx\" target=\"main\">已分未收</a></li>";
                ManageMenu += "<li><a href=\"../Case/CaseDaiFen22.aspx\" target=\"main\">已分已收</a></li>";
                ManageMenu += "<li><a href=\"../ShiXiao/YiFenYiShou.aspx\" target=\"main\">任务查询</a></li>";
                ManageMenu += "<li><a href=\"../Case/CaseDaiFenTest.aspx\" target=\"main\">待分测试员</a></li>";
                ManageMenu += "<li><a href=\"../Case/My_Test.aspx\" target=\"main\">我的测试项</a></li>";
                ManageMenu += "<li><a href=\"../TongJi/LaboratoryData.aspx\" target=\"main\">开案明细</a></li>";
                ManageMenu += "<li><a href=\"../TongJi/SettleProject.aspx\" target=\"main\">结案明细</a></li>";

                //ManageMenu += "<li><a href=\"../Income/Cs.aspx\" target=\"main\">已超期任务</a></li>";

                // ManageMenu += "<li><a href=\"../Case/TreeEngineer2.aspx\" target=\"main\">工作任务</a></li>";
                //ManageMenu += "<li><a href=\"../tongji/CaseIncomeGc.aspx\" target=\"main\">完成查询</a></li>";
                //ManageMenu += "<li><a href=\"../Report/BaoGaoListQianZi.aspx\" target=\"main\">报告签字</a></li>";
                //ManageMenu += "<li><a href=\"../Report/BaoGaoListShenHe.aspx\" target=\"main\">报告审核</a></li>";
                //ManageMenu += "<li><a href=\"../Case/CaseDaiFen4.aspx\" target=\"main\">最近已分()</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/ShangBaoLeiBie.aspx\" target=\"main\">上报类别</a></li>";
                //ManageMenu += "<li><a href=\"../Case/YiFenYiShou4.aspx\" target=\"main\">已经超期任务</a></li>";
                //ManageMenu += "<li><a href=\"../Case/YiFenYiShou4(WillChaoQi).aspx\" target=\"main\">将要超期任务</a></li>";
                //ManageMenu += "<li><a href=\"../Case/YiFenYiShou1.aspx\" target=\"main\">已被暂停任务</a></li>";
                //ManageMenu += "<li><a href=\"../Case/YiFenYiShou2.aspx\" target=\"main\">已被恢复任务</a></li>";
                //ManageMenu += "<li><a href=\"../Quotation/YiShouLi.aspx?ys=1\" target=\"main\">客户以往任务</a></li>";
                //ManageMenu += "<li><a href=\"../TongJi/TaskCount1.aspx\" target=\"main\">工程师任务数</a></li>";
                //ManageMenu += "<li><a href=\"../Report/BaoGaoListFaFang.aspx\" target=\"main\">报告发放</a></li>";
                //ManageMenu += "<li><a href=\"../Report/EMCtest.aspx\" target=\"main\">EMC测试项目</a></li>";
            }
            else
            {
                ManageMenuTitle = "没有权限";
            }

        }
        else if (MenuId == "6")
        {
            if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "8" || limit1("报价管理"))
            {
                ManageMenuTitle = "报价管理";
                ManageMenu += "<li><a href=\"../Customer/CustManage.aspx\" target=\"main\">客户报价</a></li>";
                ManageMenu += "<li><a href=\"../Quotation/QuoDraft.aspx\" target=\"main\">草稿报价</a></li>";
                ManageMenu += "<li><a href=\"../Quotation/QuotationAppro.aspx\" target=\"main\">经理审批</a></li>";
                ManageMenu += "<li><a href=\"../Quotation/ZShenpi.aspx\" target=\"main\">总经理审批</a></li>";
                //ManageMenu += "<li><a href=\"../Quotation/QuotationAppro2.aspx\" target=\"main\">领导待批</a></li>";
                //ManageMenu += "<li><a href=\"../Quotation/QuotationHuiqian.aspx\" target=\"main\">已签报价</a></li>";
                //ManageMenu += "<li><a href=\"../Quotation/CaiWu.aspx\" target=\"main\">财务审批</a></li>";
                ManageMenu += "<li><a href=\"../Quotation/QuotationList.aspx\" target=\"main\">报价列表</a></li>";
                //ManageMenu += "<li><a href=\"../Quotation/Epibloy.aspx\" target=\"main\">外包项目</a></li>";
                //ManageMenu += "<li><a href=\"../Quotation/YiShouLi4aspx.aspx\" target=\"main\">任务列表</a></li>";
                ////ManageMenu += "<li><a href=\"../Quotation/YiShouLi4aspxFoshan.aspx\" target=\"main\">佛山任务</a></li>";
                //ManageMenu += "<li><a href=\"../TongJi/XiaoShouYeJi.aspx\" target=\"main\">销售业绩</a></li>";
                //ManageMenu += "<li><a href=\"../Case/CeShiFeiKfMYW.aspx\" target=\"main\">客户催款</a></li>";
                //ManageMenu += "<li><a href=\"../Quotation/WaiBaoChaList.aspx\" target=\"main\">外包费用查询</a></li>";
                // ManageMenu += "<li><a href=\"../Quotation/Institution.aspx\" target=\"main\">增加外出机构</a></li>";
                //ManageMenu += "<li><a href=\"../sysManage/DataBase_yuzhixiangmu.aspx\" target=\"main\">增加外出类别</a></li>";
                ManageMenu += "<li><a href=\"../Customer/Rollback.aspx\" target=\"main\">合同回退</a></li>";
            }
            else
            {
                ManageMenuTitle = "没有权限";
            }
        }

        else if (MenuId == "7")
        {
            if (limit1("统计管理"))
            {
                ManageMenuTitle = "统计查询";
                ManageMenu += "<li><a href=\"../TongJi/KeHuQianKuan.aspx\" target=\"main\">客户应收</a></li>";
                ManageMenu += "<li><a href=\"../Income/CaseIncome2.aspx\" target=\"main\">应收明细</a></li>";
                ManageMenu += "<li><a href=\"../TongJi/XiaoShouYeJi.aspx\" target=\"main\">销售业绩</a></li>";
                ManageMenu += "<li><a href=\"../TongJi/ReportTongJi.aspx\" target=\"main\">报告统计</a></li>";
                ManageMenu += "<li><a href=\"../TongJi/TaskSearch.aspx\" target=\"main\">开案数据</a></li>";
                ManageMenu += "<li><a href=\"../TongJi/Settle.aspx\" target=\"main\">结案数据</a></li>";
                //ManageMenu += "<li><a href=\"../TongJi/LaboratoryData.aspx\" target=\"main\">工程明细</a></li>";
                // ManageMenu += "<li><a href=\"../TongJi/XiaoShouYeJi.aspx\" target=\"main\">销售业绩</a></li>";
                // ManageMenu += "<li><a href=\"../TongJi/KeFuYeWu.aspx\" target=\"main\">客户业务</a></li>";
                //ManageMenu += "<li><a href=\"../TongJi/KeFuYeWu.aspx\" target=\"main\">客服业绩</a></li>";
                //ManageMenu += "<li><a href=\"../TongJi/TaskCount.aspx\" target=\"main\">任务数量</a></li>";
                //  ManageMenu += "<li><a href=\"../TongJi/TaskCount1.aspx\" target=\"main\">工程统计</a></li>";
                //ManageMenu += "<li><a href=\"../TongJi/ReportCount.aspx\" target=\"main\">报告数量</a></li>";
                //  ManageMenu += "<li><a href=\"../TongJi/ReportTongJi.aspx\" target=\"main\">报告统计</a></li>";               
                //  ManageMenu += "<li><a href=\"../TongJi/JieSuanCount.aspx\" target=\"main\">结算费用</a></li>";
                // ManageMenu += "<li><a href=\"../TongJi/FenBuMengMingXi.aspx\" target=\"main\">部门结算</a></li>";
                //ManageMenu += "<li><a href=\"../TongJi/CaseIncomeEMC.aspx\" target=\"main\">EMC组统计</a></li>";

                //ManageMenu += "<li><a href=\"../TongJi/CaseIncomeTask.aspx\" target=\"main\">任务导出</a></li>";
                //ManageMenu += "<li><a href=\"../TongJi/TongJiKeFu.aspx\" target=\"main\">客服业绩</a></li>";
                //ManageMenu += "<li><a href=\"../Income/CaseIncome3.aspx\" target=\"main\">返点记录</a></li>"; 
            }
            else
            {
                ManageMenuTitle = "没有权限";
            }
        }
        else if (MenuId == "8")
        {
            if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "8" || limit1("业务管理"))
            {
                ManageMenuTitle = "业务管理";


                //ManageMenu += "<li><a href=\"../Quotation/QuotationHuiqian.aspx\" target=\"main\">客服排单</a></li>";
                ManageMenu += "<li><a href=\"../Quotation/AnjianxinxiManage3.aspx\" target=\"main\">待受任务</a></li>";
                //ManageMenu += "<li><a href=\"../Income/Customer.aspx\" target=\"main\">快速报价</a></li>";
                //ManageMenu += "<li><a href=\"../Quotation/QuotationHuiqian2.aspx?id=yewu\" target=\"main\">提交开案</a></li>";
                //ManageMenu += "<li><a href=\"../Quotation/AnjianxinxiManage.aspx\" target=\"main\">业务受理</a></li>";
                ManageMenu += "<li><a href=\"../Quotation/YiShouLi.aspx\" target=\"main\">任务列表</a></li>";
                //ManageMenu += "<li><a href=\"../Quotation/YiShouLiDai.aspx\" target=\"main\">待核算任务</a></li>";

                //ManageMenu += "<li><a href=\"../Case/CeShiFeiKfM.aspx\" target=\"main\">请款列表</a></li>";



                // ManageMenu += "<li><a href=\"../Income/IncomeCheck1.aspx\" target=\"main\">到账列表</a></li>";


                // ManageMenu += "<li><a href=\"../Income/CashinManage.aspx\" target=\"main\">凭证列表</a></li>";

                ManageMenu += "<li><a href=\"../Income/HeSuanfeiyong.aspx\" target=\"main\">核算费用</a></li>";

                // ManageMenu += "<li><a href=\"../YangPin/YangPinManage1.aspx\" target=\"main\">样品管理</a></li>";

                // ManageMenu += "<li><a href=\"../Income/FapiaoManageKeFu.aspx\" target=\"main\">发票信息</a></li>";
                //ManageMenu += "<li><a href=\"../YangPin/YangPinManage2.aspx\" target=\"main\">借出样品</a></li>";
                //ManageMenu += "<li><a href=\"../YangPin/YangPinManage3.aspx\" target=\"main\">已还样品</a></li>";
                //ManageMenu += "<li><a href=\"../YangPin/YangPinManage.aspx\" target=\"main\">样品状态</a></li>";

                //ManageMenu += "<li><a href=\"../Case/MaterialList.aspx\" target=\"main\">资料清单</a></li>";

                //ManageMenu += "<li><a href=\"../Quotation/AnjianxinxiManageBack.aspx\" target=\"main\">被退委托</a></li>";
                //ManageMenu += "<li><a href=\"../Income/Customer.aspx\" target=\"main\">快递登记</a></li>";
                //ManageMenu += "<li><a href=\"../Case/KuaiDiManage.aspx\" target=\"main\">快递查询</a></li>";
                //ManageMenu += "<li><a href=\"../Quotation/YiShouLi2.aspx\" target=\"main\">新受理任务</a></li>";
                // ManageMenu += "<li><a href=\"../Quotation/YiShouLi3.aspx\" target=\"main\">未下达任务</a></li>";
                //ManageMenu += "<li><a href=\"../ShiXiao/Default.aspx\" target=\"main\">进行中任务</a></li>";
                //ManageMenu += "<li><a href=\"../ShiXiao/Default4.aspx\" target=\"main\">待恢复任务</a></li>";
                //ManageMenu += "<li><a href=\"../ShiXiao/ZanTing.aspx\" target=\"main\">已暂停任务</a></li>";
                //ManageMenu += "<li><a href=\"../ShiXiao/ZanTingDaoChu.aspx\" target=\"main\">可导出暂停</a></li>";
                // ManageMenu += "<li><a href=\"../ShiXiao/YiJieShu.aspx\" target=\"main\">已结束任务</a></li>";
                //ManageMenu += "<li><a href=\"../ShiXiao/JiJangZanTing.aspx\" target=\"main\">将暂停任务</a></li>";
                //ManageMenu += "<li><a href=\"../Case/YiFenYiShou4.aspx\" target=\"main\">已超期任务</a></li>";
                //ManageMenu += "<li><a href=\"../Case/YiFenYiShou4(WillChaoQi).aspx\" target=\"main\">将超期任务</a></li>";
                // ManageMenu += "<li><a href=\"../ShiXiao/Default2.aspx\" target=\"main\">任务查询</a></li>";
                //ManageMenu += "<li><a href=\"../Case/YiFenYiShou3.aspx\" target=\"main\">任务查询</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/Tholiday.aspx\" target=\"main\">工作日表</a></li>";
                //ManageMenu += "<li><a href=\"../ShiXiao/ShiXiaoPeiZhi.aspx\" target=\"main\">配置录入</a></li>";
                //ManageMenu += "<li><a href=\"../ShiXiao/ShiXiaoPeiZhiManage.aspx\" target=\"main\">配置查询</a></li>";
                ManageMenu += "<li><a href=\"../Income/CaseIncome.aspx\"  target=\"main\">任务状态操作</a></li>";
                //ManageMenu += "<li><a href=\"../Income/Cs.aspx\" target=\"main\">已超期任务</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/ZanTingLeiBie.aspx\" target=\"main\">配置暂停原因</a></li>";
                //ManageMenu += "<li><a href=\"../Quotation/QuoDraft.aspx\" target=\"main\">修改合同</a></li>";
                //ManageMenu += "<li><a href=\"../Quotation/Deleterecord.aspx\" target=\"main\">删除项目记录</a></li>";
                ManageMenu += "<li><a href=\"../TongJi/TaskSearch.aspx\" target=\"main\">开案数据</a></li>";
            }
            else
            {
                ManageMenuTitle = "没有权限";
            }
        }

        else if (MenuId == "9")
        {
            if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "8" || limit1("报告管理"))
            {
                ManageMenuTitle = "报告管理";
                //ManageMenu += "<li><a href=\"../Report/BaoGaoList.aspx\" target=\"main\">报告编印</a></li>";
                //ManageMenu += "<li><a href=\"../Report/BaoGaoListQianZi.aspx\" target=\"main\">报告签字</a></li>";
                //ManageMenu += "<li><a href=\"../Report/BaoGaoListShenHe.aspx\" target=\"main\">报告审核</a></li>";
                ManageMenu += "<li><a href=\"../Report/Caogao.aspx\" target=\"main\">草稿报告</a></li>";
                ManageMenu += "<li><a href=\"../Report/BaoGaoListPiZhun.aspx\" target=\"main\">报告缮制</a></li>";
                //ManageMenu += "<li><a href=\"../Report/BaoGaoListCunDang.aspx\" target=\"main\">记录存档</a></li>";
                ManageMenu += "<li><a href=\"../Report/BaoGaoListZong.aspx\" target=\"main\">报告列表</a></li>";
                //ManageMenu += "<li><a href=\"../Report/BaoGaoListCunDang2.aspx\" target=\"main\">报告上报</a></li>";


            }
            else
            {
                ManageMenuTitle = "没有权限";
            }
        }

        else if (MenuId == "10")
        {
            //           if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "8" || limit1("时效管理"))
            //           {
            //               ManageMenuTitle = "时效管理";
            //               ManageMenu += "<li><a href=\"../Quotation/YiShouLi2.aspx\" target=\"main\">新受理任务</a></li>";
            //               ManageMenu += "<li><a href=\"../Quotation/YiShouLi3.aspx\" target=\"main\">未下达任务</a></li>";
            //               ManageMenu += "<li><a href=\"../ShiXiao/Default.aspx\" target=\"main\">进行中任务</a></li>";
            //               ManageMenu += "<li><a href=\"../ShiXiao/Default3.aspx\" target=\"main\">待暂停任务</a></li>";

            //               ManageMenu += "<li><a href=\"../ShiXiao/Default4.aspx\" target=\"main\">待恢复任务</a></li>";

            //               ManageMenu += "<li><a href=\"../ShiXiao/ZanTing.aspx\" target=\"main\">已暂停任务</a></li>";
            //               ManageMenu += "<li><a href=\"../ShiXiao/ZanTingDaoChu.aspx\" target=\"main\">可导出暂停</a></li>";
            //               ManageMenu += "<li><a href=\"../ShiXiao/YiJieShu.aspx\" target=\"main\">已结束任务</a></li>";
            //               //ManageMenu += "<li><a href=\"../ShiXiao/JiJangZanTing.aspx\" target=\"main\">将暂停任务</a></li>";
            //               //ManageMenu += "<li><a href=\"../Case/YiFenYiShou4.aspx\" target=\"main\">已超期任务</a></li>";
            //               //ManageMenu += "<li><a href=\"../Case/YiFenYiShou4(WillChaoQi).aspx\" target=\"main\">将超期任务</a></li>";
            //               // ManageMenu += "<li><a href=\"../ShiXiao/Default2.aspx\" target=\"main\">任务查询</a></li>";
            //               ManageMenu += "<li><a href=\"../Case/YiFenYiShou3.aspx\" target=\"main\">任务查询</a></li>";
            //               //ManageMenu += "<li><a href=\"../SysManage/Tholiday.aspx\" target=\"main\">工作日表</a></li>";
            //               //ManageMenu += "<li><a href=\"../ShiXiao/ShiXiaoPeiZhi.aspx\" target=\"main\">配置录入</a></li>";
            //               //ManageMenu += "<li><a href=\"../ShiXiao/ShiXiaoPeiZhiManage.aspx\" target=\"main\">配置查询</a></li>";

            //               ManageMenu += "<li><a href=\"../SysManage/ZanTingLeiBie.aspx\" target=\"main\">暂停原因</a></li>";

            //    ManageMenu += "<li><a href=\"../ShiXiao/AssignServicer.aspx\" target=\"main\">指定客服</a></li>";
            ////ManageMenu += "<li><a href=\"../TongJi/TongJiKeFu.aspx\" target=\"main\">客服业绩</a></li>"; 

            //           }
            //           else
            //           {
            //               ManageMenuTitle = "没有权限";
            //           }
        }
        else if (MenuId == "12")
        {
            if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "8" || limit1("质量管理"))
            {
                ManageMenuTitle = "质量管理";

                ManageMenu += "<li><a href=\"../SysManage/ChuCuoLeiBie.aspx\" target=\"main\">类型调整</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/ChuCuoLeiBieM.aspx\" target=\"main\">错误查询</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/ChuCuoTree.aspx\" target=\"main\">差错分类</a></li>";


                ManageMenu += "<li><a href=\"../Report/BaoGaoListZong_Zhi.aspx\" target=\"main\">差错录入</a></li>";



                ManageMenu += "<li><a href=\"../Report/ReportMonth.aspx\" target=\"main\">年度统计</a></li>";
                ManageMenu += "<li><a href=\"../Report/ReportChaCuo.aspx\" target=\"main\">差错明细</a></li>";



                ManageMenu += "<li><a href=\"../Report/ReportGongChengShi.aspx\" target=\"main\">部门差错数量</a></li>";
                ManageMenu += "<li><a href=\"../Report/ReportMonthDep.aspx\" target=\"main\">部门月度差错</a></li>";
                ManageMenu += "<li><a href=\"../Report/ReportDepLeiXing.aspx\" target=\"main\">部门差错类型</a></li>";


                ManageMenu += "<li><a href=\"../Report/ReportDepartment.aspx\" target=\"main\">工程师差错数量</a></li>";

                ManageMenu += "<li><a href=\"../Report/ReportGongChengLeiXing.aspx\" target=\"main\">工程师差错类型</a></li>";

                ManageMenu += "<li><a href=\"../Report/ReportLeiXing1.aspx\" target=\"main\">差错类型工程师</a></li>";


            }
            else
            {
                ManageMenuTitle = "没有权限";
            }
        }

        else if (MenuId == "13")
        {
            if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "8" || limit1("样品管理"))
            {
                ManageMenuTitle = "样品管理";
                ManageMenu += "<li><a href=\"../YangPin/YangPinAdd.aspx?baojiaid=&&kehuid=&&bianhao=\" target=\"main\">样品登记</a></li>";

                ManageMenu += "<li><a href=\"../Quotation/YiShouLiYanPing.aspx\" target=\"main\">关联样品</a></li>";

                ManageMenu += "<li><a href=\"../YangPin/YangPinManageCha1.aspx\" target=\"main\">关联任务</a></li>";

                ManageMenu += "<li><a href=\"../YangPin/YangPinManage1.aspx\" target=\"main\">样品查询</a></li>";

                ManageMenu += "<li><a href=\"../YangPin/YangPin_Jiechu2.aspx\" target=\"main\">样品借出</a></li>";
                ManageMenu += "<li><a href=\"../YangPin/YangPin_Jiechu3.aspx\" target=\"main\">样品归还</a></li>";
                ManageMenu += "<li><a href=\"../YangPin/YangPin_Jiechu4.aspx\" target=\"main\">样品清退</a></li>";
                ManageMenu += "<li><a href=\"../YangPin/YangPin_JiechuQingTui.aspx\" target=\"main\">样品封存</a></li>";
                ManageMenu += "<li><a href=\"../YangPin/YangPin_JiechuFengCun.aspx\" target=\"main\">样品销毁</a></li>";
                ManageMenu += "<li><a href=\"../YangPin/YangPinManage6.aspx\" target=\"main\">未还样品</a></li>";
                //ManageMenu += "<li><a href=\"../YangPin/YangPinManage6.aspx\" target=\"main\">样品时效</a></li>";
            }
            else
            {
                ManageMenuTitle = "没有权限";
                //ManageMenu += "<li><a href=\"../YangPin/YangPinManage1.aspx\" target=\"main\">样品查询</a></li>";
            }
        }

        else if (MenuId == "14")
        {
            if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "8" || limit1("文件管理"))
            {
                ManageMenuTitle = "文件管理";
                ManageMenu += "<li><a href=\"../SysManage/ChuCuoLeiBie1.aspx\" target=\"main\">文件录入</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/ChuCuoLeiBieM.aspx\" target=\"main\">错误查询</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/ChuCuoLeiBieM.aspx\" target=\"main\">文件查询</a></li>";
            }
            else
            {
                ManageMenuTitle = "没有权限";

            }
        }
        else if (MenuId == "15")
        {
            //if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "8" || limit1("仪校管理"))
            {
                ManageMenuTitle = "仪校管理";
                ManageMenu += "<li><a href=\"../SysManage/ChuCuoLeiBie2.aspx?type=2\" target=\"main\">证书录入</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/ChuCuoLeiBieM.aspx\" target=\"main\">错误查询</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/ChuCuoLeiBieM2.aspx?type=2\" target=\"main\">证书查询</a></li>";
            }
            //else
            //{
            //    ManageMenuTitle = "没有权限";

            //}
        }

        else if (MenuId == "16")
        {
            if (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "8" || limit1("设备管理"))
            {
                ManageMenuTitle = "设备管理";
                ManageMenu += "<li><a href=\"../ziyuan/add.aspx\" target=\"main\">设备录入</a></li>";
                //ManageMenu += "<li><a href=\"../SysManage/ChuCuoLeiBieM.aspx\" target=\"main\">错误查询</a></li>";
                ManageMenu += "<li><a href=\"../ziyuan/manage.aspx\" target=\"main\">未验设备</a></li>";
                ManageMenu += "<li><a href=\"../ziyuan/manage2.aspx\" target=\"main\">已验设备</a></li>";
                // ManageMenu += "<li><a href=\"../ziyuan/GoodsinfoManageOut2.aspx\" target=\"main\">待校设备</a></li>";

                ManageMenu += "<li><a href=\"../ziyuan/GoodsinfoManageOut3.aspx\" target=\"main\">待校设备</a></li>";
                ManageMenu += "<li><a href=\"../ziyuan/GoodsinfoManageOut1.aspx\" target=\"main\">供应导出</a></li>";
                ManageMenu += "<li><a href=\"../ziyuan/manage3.aspx\" target=\"main\">全部设备</a></li>";

                ManageMenu += "<li><a href=\"../ziyuan/daoexcel.aspx\" target=\"main\">设备导入</a></li>";
            }
            else
            {
                ManageMenuTitle = "没有权限";

            }
        }
        else if (MenuId == "1017")
        {
            if (limit1("人事管理"))
            {
                ManageMenuTitle = "人事管理";
                ManageMenu += "<li><a href=\"../SysManage/UserList.aspx\" target=\"main\">用户信息</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/PersonConfig.aspx\" target=\"main\">人员配置</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/Personconfigconcel.aspx\" target=\"main\">取消配置</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/ModuleInsert.aspx\" target=\"main\">权限分配</a></li>";
                ManageMenu += "<li><a href=\"../SysManage/ModuleSearch.aspx\" target=\"main\">权限查询</a></li>";
            }
            else
            {
                ManageMenuTitle = "没有权限";
            }
        }
        else if (MenuId == "make")
        {
            ManageMenuTitle = "现场预约";
            ManageMenu += "<li><a href=\"../EMCmate/sitetest.html\" target=\"main\">预约测试</a></li>";
            ManageMenu += "<li><a href=\"../EMCmate/select.html\" target=\"main\">查看预约</a></li>";
            ManageMenu += "<li><a href=\"../EMCmate/charge.html\" target=\"main\">生成收费单</a></li>";
            ManageMenu += "<li><a href=\"../EMCmate/exportExcel.html\" target=\"main\">数据导出</a></li>";

        }
    }

    protected void limit(string pagename1)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
        }
        else
        {
            con.Close();
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
    }

    protected bool limit1(string pagename1)
    {
        bool A = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
            A = true;
        }
        else
        {
            con.Close();
            A = false;
        }
        return A;
    }
}