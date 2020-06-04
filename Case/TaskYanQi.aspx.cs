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

public partial class Case_TaskYanQi : System.Web.UI.Page
{

    protected string rwid = "";
    protected string bianhao = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        bianhao = Request.QueryString["renwuid"].ToString();



       
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select rwbianhao,yaoqiuwanchengriqi from anjianinfo2 where bianhao='" + bianhao + "' or rwbianhao='" + bianhao + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            rwid = dr["rwbianhao"].ToString();
            TextBox2.Text = dr["yaoqiuwanchengriqi"].ToString();
        }
        dr.Close();
      

        con.Close();


        rwbianhao.Text = rwid;


        if (!IsPostBack)
        {
           
            YaopinList();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "insert into AnJianYanQi values('" + rwid + "','" + Convert.ToDateTime(TextBox2.Text.Trim()) + "','" + Convert.ToDateTime(TextBox3.Text.Trim()) + "','" + TextBox1.Text.Trim() + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','')";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        string sql2 = "update anjianinfo2 set yaoqiuwanchengriqi='"+TextBox3.Text.Trim()+"' where rwbianhao='"+rwid+"'";
        SqlCommand cmd2 = new SqlCommand(sql2,con);
        cmd2.ExecuteNonQuery();

        con.Close();
        YaopinList();
    }


    protected void YaopinList()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from AnJianYanQi where bianhao='" + rwid + "' order by id asc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
        con.Dispose();
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "delete from  AnJianYanQi  where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();
        YaopinList();
    }

}