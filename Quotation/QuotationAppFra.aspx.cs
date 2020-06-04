using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Web.Services;
using Common;
using System.IO;
using System.Text;
using System.Drawing;

public partial class Quotation_QuotationAppFra : System.Web.UI.Page
{

    protected string url = string.Empty;
    protected string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"].ToString();
        if (id == "draft")
        {
            url = "QuoDraft.aspx";
        }
        else if (id == "shenpi")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select * from ModuleDuty where name='"+Session["UserName"].ToString()+"' and modulename='审批报价'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                url = "QuotationAppro.aspx";
            }
            else
            {
                con.Close();
                Response.Write("<script>alert('您没有权限，请与相关人员联系！');top.main.location.href='../Account/WelCome.aspx?MeId=6'</script>");
            }
           
        }
        else if (id == "shenpi2")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='审批低折报价'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                url = "QuotationAppro2.aspx";
            }
            else
            {
                con.Close();
                Response.Write("<script>alert('您没有权限，请与相关人员联系！');top.main.location.href='../Account/WelCome.aspx?MeId=6'</script>");
            }

        }

        else if (id == "yipi")
        {
            url = "QuotationApproed.aspx";
        }
        else if (id == "huiqian")
        {
            url = "QuotationHuiqian.aspx";
        }
        else if (id == "kaian")
        {
            url = "QuoKaiAn.aspx";
        }
        else if (id == "kaian2")
        {
            url = "QuoKaiAn2.aspx";
        }
    }
}