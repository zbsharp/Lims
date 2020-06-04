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

public partial class Report_JieShouJiLu : System.Web.UI.Page
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
        string sql3 = "select taskid from anjianinfo where tijiaobianhao='"+tijiaobianhao+"'";
        SqlCommand cmd3 = new SqlCommand(sql3,con);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {
            rwid = dr3["taskid"].ToString();
        }
        dr3.Close();


        if (!IsPostBack)
        {

          

         
           
       
            
            
            Bind();
        }
        con.Close();
        con.Dispose();

    }

    protected void Bind()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from baogaobumen where rwid='" + tijiaobianhao + "' and baogaohao='" + baogaohao + "'";
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


        int i = 0;
        int x = 0;
        int j = 0;
        string shoufeiid = randomNum.Next().ToString() + DateTime.Now.ToString("yyyyMMdd_hhmmss");
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            if (hzf.Checked)
            {
                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                string sql2 = "update baogaobumen set jieshouname='" + Session["UserName"].ToString() + "',jieshoutime='" + DateTime.Now + "',beizhu='" + TextBox1.Text + "' where id='" + sid + "'";
                SqlCommand com2 = new SqlCommand(sql2, con);
                i = com2.ExecuteNonQuery();


               


            }
            else
            {
                string sql21 = "update baogaobumen set jieshouname='',jieshoutime='',beizhu='" + TextBox1.Text + "' where  (rwid='" + tijiaobianhao + "' and beizhu2='否')";
                SqlCommand com21 = new SqlCommand(sql21, con);
                j = com21.ExecuteNonQuery();
            }

            if (i+j > 0)
            {
                x = 1;
            }
        }
        if (x > 0)
        {
            string sql21 = "select * from baogaobumen where baogaohao='" + baogaohao + "'  and beizhu2 !='否'  and jieshouname=''";
            SqlCommand cm2 = new SqlCommand(sql21, con);
            SqlDataReader dr2 = cm2.ExecuteReader();
            if (dr2.Read())
            {
                dr2.Close();
                string sql4 = "update baogao2 set state='资料未齐' where baogaoid='" + baogaohao + "' and state !='已批准'";
                SqlCommand cmd4 = new SqlCommand(sql4, con);
                cmd4.ExecuteNonQuery();
            }
            else
            {
                dr2.Close();
                string sql4 = "update baogao2 set state='资料已齐' where baogaoid='" + baogaohao + "' and state !='已批准'";
                SqlCommand cmd4 = new SqlCommand(sql4, con);
                cmd4.ExecuteNonQuery();
            }

            string sqlstate = "insert into  TaskState values ('" + rwid + "','" + rwid + "','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','接收资料','客服受理任务生成案件号')";
            SqlCommand cmdstate = new SqlCommand(sqlstate, con);
            cmdstate.ExecuteNonQuery();

            con.Close();
            ld.Text = "<script>alert('接收成功!');</script>";
            
        }
        con.Close();

        Bind();
       
    }
}