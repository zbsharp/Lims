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

public partial class Case_CeShi : System.Web.UI.Page
{
    protected string baojiaid = "";
    protected string kehuname = "";
    protected string kehuid = "";
    protected string yewuyuan = "";
    protected string baogaohao = "";
    protected string filename = "";
    protected void Page_Load(object sender, EventArgs e)
    {



        TextBox1.Text = Request.QueryString["id"].ToString().Trim();


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        string sql = "select taskid from anjianinfo  where id='" + TextBox1.Text + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            baogaohao = dr["taskid"].ToString();

            

        }
        TextBox3.Text = baogaohao;


        con.Close();
        if (!IsPostBack)
        {
            dinge();

        }



    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        string sql = "update anjianinfo set beiyong='通过' where id='" + TextBox1.Text + "'";

        string sql3 = "insert into TaskCeShi values('" + baogaohao + "','" + TextBox1.Text + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','通过','" + TextBox8.Text + "','" + TextBox2.Text + "','" + TextBox6.Text + "','" + DropDownList1.SelectedValue + "','" + filename + "')";



        SqlCommand com = new SqlCommand(sql, con);

        SqlCommand com3 = new SqlCommand(sql3, con);


        com.ExecuteNonQuery();

        com3.ExecuteNonQuery();

      




        con.Close();
        dinge();

        Response.Write("<script>alert('审核成功'),opener.location.reload();</script>");

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");


        }



    }

    protected void dinge()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *  from TaskCeShi where baogaobianhao='" + TextBox1.Text.Trim() + "'";
        SqlCommand com = new SqlCommand(sql, con);
        SqlDataReader dr = com.ExecuteReader();
        GridView1.DataSource = dr;
        GridView1.DataBind();

        con.Close();
        con.Dispose();

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "delete from TaskCeShi where id='" + GridView1.DataKeys[e.RowIndex].Value + "' and shenheren='" + Session["UserName"].ToString() + "'";

        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();
        dinge();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        this.GridView1.EditIndex = e.NewEditIndex;
        dinge();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        dinge();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {



    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        string sql = "update anjianinfo set beiyong='不通过' where id='" + TextBox1.Text + "'";

        string sql3 = "insert into TaskCeShi values('" + baogaohao + "','" + TextBox1.Text + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','不通过','" + TextBox8.Text + "','" + TextBox2.Text + "','" + TextBox6.Text + "','" + DropDownList1.SelectedValue + "','" + filename + "')";



        SqlCommand com = new SqlCommand(sql, con);

        SqlCommand com3 = new SqlCommand(sql3, con);


        com.ExecuteNonQuery();

        com3.ExecuteNonQuery();

    




        con.Close();
        dinge();

        Response.Write("<script>alert('审核不通过'),opener.location.reload();</script>");
    }
}