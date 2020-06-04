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

public partial class Report_AnPaiDaYin : System.Web.UI.Page
{
    protected string rwid = "";
    protected string tijiaobianhao = "";

    protected string baogaohao = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        tijiaobianhao = Request.QueryString["rwid"].ToString();
        baogaohao = Request.QueryString["baogaoid"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql3 = "select taskid from anjianinfo where tijiaobianhao='" + tijiaobianhao + "'";
        SqlCommand cmd3 = new SqlCommand(sql3, con);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {
            rwid = dr3["taskid"].ToString();
        }
        dr3.Close();
        con.Close();
        con.Dispose();

        if (!IsPostBack)
        {


            BindDR();





            Bind();
        }
        

    }
    protected void BindDR()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select distinct name  from  ModuleDuty where modulename='报告编印'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataTextField = "name";
        DropDownList1.DataValueField = "name";
        DropDownList1.DataBind();
        con.Close();
    }


    protected void Bind()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from baogaobumen where rwid='" + rwid + "' and baogaohao='" + baogaohao + "'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Random seed = new Random();
        Random randomNum = new Random(seed.Next());
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "update baogao2 set dayinname='"+DropDownList1.SelectedValue+"',state='正在打印',dayintime='"+DateTime.Now+"' where baogaoid='"+baogaohao+"'";
        SqlCommand cmd = new SqlCommand(sql,con);
        cmd.ExecuteNonQuery();

        string sqlstate = "insert into  TaskState values ('" + rwid + "','" + rwid + "','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','安排打印','客服受理任务生成案件号')";
        SqlCommand cmdstate = new SqlCommand(sqlstate, con);
        cmdstate.ExecuteNonQuery();


        string sql3 = "update anjianinfo2 set state='正在打印' where rwbianhao='"+rwid+"'";
        SqlCommand cmd3 = new SqlCommand(sql3,con);
        cmd3.ExecuteNonQuery();

        con.Close();
        Bind();
        ld.Text = "<script>alert('安排成功!');</script>";
    }
}