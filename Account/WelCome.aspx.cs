using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
public partial class Account_WelCome : System.Web.UI.Page
{
    protected string MeId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "倍测Lims系统";
       
        
        //if (Request.QueryString["MenuId2"] != null)
        //{
        //    MeId = Request.QueryString["MenuId2"].ToString();
        //}
        //else
        //{
        //    MeId = "1";
        //}

        //if (MeId == "1")
        //{
        //    Label1.Text = "系统管理";
        //}
        //else if (MeId == "2")
        //{
        //    Label1.Text = "客户管理";
        //}
        //else if (MeId == "3")
        //{
        //    Label1.Text = "工程管理";
         
        //}
        //else if (MeId == "4")
        //{
        //    Label1.Text = "财务管理";
        //}
        //else if (MeId == "6")
        //{
        //    Label1.Text = "报价管理";
        //}
        //else if (MeId == "7")
        //{
        //    Label1.Text = "统计管理";
        //}
        //else if (MeId == "8")
        //{
        //    Label1.Text = "业务管理";
        //}
        //else if (MeId == "9")
        //{
        //    Label1.Text = "报告管理";
        //}
        //else if (MeId == "10")
        //{
        //    Label1.Text = "时效管理";
        //}
        //else if (MeId == "13")
        //{
        //    Label1.Text = "样品管理";
        //}
        //else if (MeId == "14")
        //{
        //    Label1.Text = "文件管理";
        //}
    }
}