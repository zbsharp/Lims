﻿using System;
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

public partial class SysManage_Tholiday : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            Bind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "insert into tb_Holiday values('" +Convert.ToDateTime (TextBox1.Text.Trim()) + "','" + TextBox2.Text + "')";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();
        Bind();

    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from tb_Holiday where hdate between '" + DateTime.Now.AddMonths(-2) + "' and '" + DateTime.Now.AddMonths(2) + "' order by hdate asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
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

        string sql = "delete from tb_Holiday where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();

        Bind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Bind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView1.EditIndex = e.NewEditIndex;
        Bind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string KeyId = GridView1.DataKeys[e.RowIndex].Value.ToString();

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string uuname2 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());
        string uuname3 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
       
        string sql = "update tb_Holiday set hdate='" + Convert.ToDateTime(uuname2) + "',name='" + uuname3 + "' where id='" + KeyId + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        GridView1.EditIndex = -1;
        Bind();
    }
}