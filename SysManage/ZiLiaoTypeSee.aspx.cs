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
using DBTable;
using System.Data.Common;

public partial class SysManage_ZiLiaoTypeSee : System.Web.UI.Page
{
    protected string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"].ToString();

        if (!IsPostBack)
        {
            RenZheng();
            department();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select * from ZiLiaoType where id='" + id + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                TextBox1.Text = dr["name"].ToString();
                TextBox2.Text = dr["ybyq"].ToString();
                TextBox3.Text = dr["syqk"].ToString();
                TextBox4.Text = dr["jingji"].ToString();
                TextBox5.Text = dr["beizhu"].ToString();

                DropDownList1.SelectedValue = dr["leibie1"].ToString();
                DropDownList2.SelectedValue = dr["bumen"].ToString();
            }
            con.Close();
        }
    }
    protected void RenZheng()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from RenZheng order by id asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataTextField = "name";
        DropDownList1.DataValueField = "name";
        DropDownList1.DataBind();
        con.Close();
    }
    protected void department()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from UserDepa order by departmentid asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataTextField = "name";
        DropDownList2.DataValueField = "name";
        DropDownList2.DataBind();
        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
       
            string sql = "update ZiLiaoType set name='" + TextBox1.Text + "',leibie1='" + DropDownList1.SelectedValue + "',ybyq='" + TextBox2.Text + "',syqk='" + TextBox3.Text + "',bumen='" + DropDownList2.SelectedValue + "',jingji='" + TextBox4.Text + "',beizhu='" + TextBox5.Text + "' where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

      
            con.Close();
        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "delete from  ZiLiaoType  where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();


        con.Close();
    }
}