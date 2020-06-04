using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
public partial class Customer_Welcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["id"] != null)
        {
            if (Request.QueryString["id"] == "rr1")
            {
                Label1.Text = "未填写任务通知书";
            }
            else if (Request.QueryString["id"] == "rr2")
            {
                Label1.Text = "请先填写上报费用再打印";
            }
            else if (Request.QueryString["id"] == "rr7")
            {
                Label1.Text = "请先填写电子上报再打印";
            }
            else if (Request.QueryString["id"] == "rr3")
            {
                Label1.Text = "您填写的报价单未录入产品，系统已自动作废处理";
            }
            else if (Request.QueryString["id"] == "rrweituo")
            {
                Label1.Text = "您填写的报价单有项目底于85折，需要领导审批后才能打印";
            }

            else if (Request.QueryString["id"] == "rrt")
            {
                Label1.Text = "已提交报价单还不能编辑";
            }
            else if (Request.QueryString["id"] == "rrb")
            {
                Label1.Text = "该客户的信用等级为E级，因此不能报价";
            }
            else if (Request.QueryString["id"] == "LinkMan")
            {
                Label1.Text = "请先为该客户添加联系人再报价";
            }
            else
            {
                Label1.Text = "请找到该客户增加联系人并在报价单上选择联系人后再打印";
            }
        }
        else
        {
            Label1.Text = "欢迎来到销售管理";
        }


    }
}