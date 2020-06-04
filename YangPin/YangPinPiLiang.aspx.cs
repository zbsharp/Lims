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
using System.Data.SqlClient;
using Common;
using DBBLL;
using DBTable;

public partial class YangPin_YangPinPiLiang : System.Web.UI.Page
{

    protected string baojiaid = "";
    protected string kehuid = "";

    protected string sampleid = "";
    protected string caozuo = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["baojiaid"] != null)
        {
            baojiaid = Request.QueryString["baojiaid"].ToString();
            kehuid = Request.QueryString["kehuid"].ToString();
        }

        if (Request.QueryString["sampleid"] != null)
        {
            sampleid = Request.QueryString["sampleid"].ToString();
            TextBox1.Text = Request.QueryString["sampleid"].ToString();
        }
        if (Request.QueryString["caozuo"] != null)
        {
            caozuo = Request.QueryString["caozuo"].ToString();
            if (caozuo == "1")
            {
                DropDownList1.SelectedValue = "清退";
            }
            else if (caozuo == "2")
            {
                DropDownList1.SelectedValue = "催还";
            }
            else if (caozuo == "3")
            {
                DropDownList1.SelectedValue = "销毁";
            }
            else 
            {
                DropDownList1.SelectedValue = "借出";
            }
        }

        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select top 1 * from kuaidizibiao where leixing='样品' and neirong='"+sampleid+"' order by id desc";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                TextBox2.Text = dr["fillname"].ToString();
                TextBox3.Text = dr["kuaidiid"].ToString();
            }
            dr.Close();
            con.Close();
            //TextBox11.Text = DateTime.Now.ToShortDateString();
            Bind();
        }
    }
    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from YangPin2Detail where  sampleid='" + TextBox1.Text + "' order by id desc";
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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sqlx = "update YangPin2 set state='" + DropDownList1.SelectedValue + "' where sampleid='" + sampleid + "'";
        SqlCommand cmdx = new SqlCommand(sqlx, con);
        cmdx.ExecuteNonQuery();

        
        string sql = "insert into YangPin2Detail values('" + TextBox1.Text + "','','" + TextBox2.Text + "','" + DateTime.Now.ToShortDateString() + "','"+DropDownList1.SelectedValue+"','" + TextBox3.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','','','','','')";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();
        con.Dispose();

        Bind();

    }
}