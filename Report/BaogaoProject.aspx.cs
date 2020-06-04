using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_BaogaoProject : System.Web.UI.Page
{
    string taskid = "";
    string baogaoid = "";
    string engineer = "";
    string baojiaid = "";
    string kehuid = "";
    string bumen = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        taskid = Request.QueryString["tjid"].ToString();
        baogaoid = Request.QueryString["baogaoid"].ToString();
        BasicData();
        if (!IsPostBack)
        {
            //1.使用任务号从anjianinfo2中查询出报价编号和客户编号
            //2.使用报告号从baogao2查出部门
            //3.使用报价编号、客户编号、部门从BaoJiaCPXiangMu2中查询出相对应的测试项目
            Bind();
            BindBaogaoItem();
        }
    }
    protected void Bind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "' and bumen='" + bumen + "' and id in (select xmid from ProjectItem where engineer='"+Session["username"].ToString()+"')";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }
    protected void BasicData()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_baojiaid = "select baojiaid,kehuid from AnJianinfo2 where rwbianhao='" + taskid + "'";
            SqlCommand com_baojiaid = new SqlCommand(sql_baojiaid, con);
            SqlDataReader dr_baojiaid = com_baojiaid.ExecuteReader();
            if (dr_baojiaid.Read())
            {
                baojiaid = dr_baojiaid["baojiaid"].ToString();
                kehuid = dr_baojiaid["kehuid"].ToString();
            }
            dr_baojiaid.Close();

            string sql_baogao = "select leibie,fillname from baogao2 where baogaoid='" + baogaoid + "'";
            SqlCommand com_baogao = new SqlCommand(sql_baogao, con);
            SqlDataReader dr_baogao = com_baogao.ExecuteReader();
            if (dr_baogao.Read())
            {
                bumen = dr_baogao["leibie"].ToString();
                engineer = dr_baogao["fillname"].ToString();
            }
            dr_baogao.Close();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string xmid = e.CommandArgument.ToString();
        if (e.CommandName == "Action")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = "insert ItemBaogao values('" + baogaoid + "','" + xmid + "','" + taskid + "','" + engineer + "','" + Session["username"].ToString() + "','" + DateTime.Now + "')";
                SqlCommand com = new SqlCommand(sql, con);
                int i = com.ExecuteNonQuery();
                if (i > 0)
                {
                    BindBaogaoItem();
                }
            }
        }
    }
    protected void BindBaogaoItem()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from ItemBaogao where baogaoid='" + baogaoid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView2.DataSource = ds.Tables[0];
            GridView2.DataBind();
        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView2.DataKeys[e.RowIndex].Value.ToString();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = " delete ItemBaogao where id='" + id + "'";
            SqlCommand com = new SqlCommand(sql, con);
            int i = com.ExecuteNonQuery();
            if (i > 0)
            {
                BindBaogaoItem();
            }
        }
    }
}