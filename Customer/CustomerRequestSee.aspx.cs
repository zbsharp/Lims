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

public partial class Customer_CustomerRequestSee : System.Web.UI.Page
{

    protected string kehuid = "";
    protected string kehuname = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        kehuid = Request.QueryString["kehuid"].ToString();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql1 = "select * from Customer where kehuid='" + kehuid + "' order by kehuid";
        SqlCommand cmd = new SqlCommand(sql1, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            kehuname = dr["CustomName"].ToString();
           

        }
        con.Close();

        if (!IsPostBack)
        {
            BindMyFill();
            
        }
    }

    protected void BindMyFill()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CustomerRequest where kehuname like '%" + kehuname + "%' order by id desc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
    }
}