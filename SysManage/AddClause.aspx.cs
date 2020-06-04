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
public partial class SysManage_AddClause : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

       


            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con1.Open();


            string sqltiaokuan = "select * from Clause2";







            SqlDataAdapter ad2 = new SqlDataAdapter(sqltiaokuan, con1);
            DataSet ds2 = new DataSet();
            ad2.Fill(ds2);

            con1.Close();
            GridView1.DataSource = ds2.Tables[0];
            GridView1.DataBind();
        
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string sql = "insert into Clause2 values('" + TextBox5.Text + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + RadioButtonList1.SelectedValue + "','')";
        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();

        string sqltiaokuan = "select * from Clause2";







        SqlDataAdapter ad2 = new SqlDataAdapter(sqltiaokuan, con1);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);


        GridView1.DataSource = ds2.Tables[0];
        GridView1.DataBind();

        con1.Close();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "delete from Clause2 where id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        string sqltiaokuan = "select * from Clause2";


        SqlDataAdapter ad2 = new SqlDataAdapter(sqltiaokuan, con);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);


        GridView1.DataSource = ds2.Tables[0];
        GridView1.DataBind();
        con.Close();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

        }
    }
}