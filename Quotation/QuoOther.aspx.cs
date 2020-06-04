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

public partial class Quotation_QuoOther : System.Web.UI.Page
{
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string responser = "";

    
    protected void Page_Load(object sender, EventArgs e)
    {

        baojiaid = Request.QueryString["baojiaid"].ToString();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select kehuid,baojiaid,responser from BaoJiaBiao where baojiaid='" + baojiaid + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            responser = dr["responser"].ToString();
            kehuid = dr["kehuid"].ToString();
        }
        con.Close();

        
        if (!IsPostBack)
        {
            Bind();
        }
    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *  from CustomerWeiTuo  where baojiaid='" + baojiaid + "' order  by id desc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "insert into CustomerWeiTuo values('" + kehuid + "','" + baojiaid + "','" + responser + "','" +weituot1 .Text.Trim() + "','" +weituolianxi1 .Text.Trim() + "','" +fukuan1.Text.Trim() + "','" +fukuanlianxi1 .Text.Trim() + "','" +daili1 .Text.Trim() + "','" +daililianxi1.Text.Trim() + "','" +zhizao1.Text.Trim() + "','" +zhizaolianxi1.Text.Trim() + "','" +shenchan1.Text.Trim() + "','" +shenchanlianxi1.Text.Trim() + "','" +shenchandidian1.Text.Trim() + "','" +beizhu .Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
        Bind();
    }
}