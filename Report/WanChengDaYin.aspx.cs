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

public partial class Report_WanChengDaYin : System.Web.UI.Page
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
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con1.Open();
            string sql31 = "select dayinshu from baogao2 where baogaoid='" + baogaohao + "'";
            SqlCommand cmd31 = new SqlCommand(sql31, con1);
            SqlDataReader dr31 = cmd31.ExecuteReader();
            if (dr31.Read())
            {
                TextBox1.Text = dr31["dayinshu"].ToString();
            }
            dr31.Close();



            con1.Close();
            con1.Dispose();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Random seed = new Random();
        Random randomNum = new Random(seed.Next());
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql2 = "select * from baogao2 where  baogaoid='" + baogaohao + "' and state='打印完成'";
        SqlCommand cmd2 = new SqlCommand(sql2,con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            dr2.Close();
            string sql = "update baogao2 set dayinshu='" + TextBox1.Text.Trim() + "',state='打印完成' where baogaoid='" + baogaohao + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            string sql3 = "update anjianinfo2 set state='已完成打印' where rwbianhao='" + rwid + "'";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            cmd3.ExecuteNonQuery();


            con.Close();
            ld.Text = "<script>alert('保存成功！');</script>";
        }
        else
        {

            dr2.Close();
            string sql = "update baogao2 set dayinshu='" + TextBox1.Text.Trim() + "',state='打印完成',wanchengtime='" + DateTime.Now + "' where baogaoid='" + baogaohao + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            string sql3 = "update anjianinfo2 set state='已完成打印' where rwbianhao='" + rwid + "'";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            cmd3.ExecuteNonQuery();


            string sqlstate = "insert into  TaskState values ('" + rwid + "','" + rwid + "','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','完成打印','客服受理任务生成案件号')";
            SqlCommand cmdstate = new SqlCommand(sqlstate, con);
            cmdstate.ExecuteNonQuery();
            con.Close();
            ld.Text = "<script>alert('保存成功！');</script>";
            
            con.Close();
            
        }
       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "update baogao2 set beizhu4='" + DateTime.Now.ToString() + "',state='纸制完成',beizhu5='"+Session["UserName"].ToString()+"' where baogaoid='" + baogaohao + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        ld.Text = "<script>alert('保存成功！');</script>";

        con.Close();
    }
}