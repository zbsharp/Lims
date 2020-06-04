using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Quotation_WBinfomation : System.Web.UI.Page
{
    string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"].ToString();
        Bind();
    }

    private void Bind()
    {
        string sql = "select * from WBinfomation where  product2_id='" + id + "'";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        string sql = "insert into WBinfomation(product2_id,WBtype,Beizhu) values('" + id + "','" + this.txt_type.Text + "','" + this.txt_type.Text + "')";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        this.txt_beizhu.Text = string.Empty;
        this.txt_type.Text = string.Empty;
        Bind();
    }

    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string keyid = this.GridView1.DataKeys[e.RowIndex].Value.ToString();
        string sql = "delete  from [dbo].[WBinfomation] where　[id]='" + keyid + "'";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        Bind();
    }
}