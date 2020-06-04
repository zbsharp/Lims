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

public partial class Case_ShangBaoType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {




        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();



        string sql2 = "select * from ShangBaoType ";

        SqlDataAdapter da2 = new SqlDataAdapter(sql2, con);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);


        GridView2.DataSource = ds2.Tables[0];
        GridView2.DataBind();

  

        con.Close();


    }
    protected void Button1_Click(object sender, EventArgs e)
    {



        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "insert into ShangBaoType values('" + RadioButtonList1.SelectedValue + "','" + TextBox1.Text + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();




        string sql2 = "select * from ShangBaoType ";

        SqlDataAdapter da2 = new SqlDataAdapter(sql2, con);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);


        GridView2.DataSource = ds2.Tables[0];
        GridView2.DataBind();




        con.Close();

    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "delete from ShangBaoType where id='" + GridView2.DataKeys[e.RowIndex].Value.ToString() + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();




        string sql2 = "select * from ShangBaoType";

        SqlDataAdapter da2 = new SqlDataAdapter(sql2, con);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);


        GridView2.DataSource = ds2.Tables[0];
        GridView2.DataBind();



        con.Close();
    }
}