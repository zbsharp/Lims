using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_BaogaoYP : System.Web.UI.Page
{
    private string baogaoid = "";
    private string taskid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        baogaoid = Request.QueryString["baogaoid"].ToString();
        taskid = Request.QueryString["taskid"].ToString();
        if (!IsPostBack)
        {
            Bind_yangpin();
            Bind_baogao();
        }
    }

    private void Bind_baogao()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = " select * from [dbo].[BaogaoYP] where baogaoid='" + baogaoid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView2.DataSource = ds.Tables[0];
            GridView2.DataBind();
        }
    }

    private void Bind_yangpin()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = " select sampleid,anjianid,kehuname,name,model,fillname,filltime from YangPin2 where anjianid='" + taskid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView2.DataKeys[e.RowIndex].Value.ToString();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_delete = " delete [dbo].[BaogaoYP] where id='" + id + "'";
            SqlCommand cmd_delete = new SqlCommand(sql_delete, con);
            cmd_delete.ExecuteNonQuery();
            Bind_baogao();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string beizhu = TextBox1.Text.Replace('\'', ' ');
            foreach (GridViewRow item in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    string yangpinid = item.Cells[0].Text.ToString();
                    string sql_add = " insert [dbo].[BaogaoYP] values('" + baogaoid + "','" + yangpinid + "','" + taskid + "','" + Session["Username"].ToString() + "','" + DateTime.Now + "','" + beizhu + "')";
                    SqlCommand cmd_add = new SqlCommand(sql_add, con);
                    cmd_add.ExecuteNonQuery();
                }
            }
            Bind_baogao();
        }
    }
}