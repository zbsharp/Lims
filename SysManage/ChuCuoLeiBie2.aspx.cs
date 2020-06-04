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
using Common;
using DBBLL;
using DBTable;
using System.Drawing;

public partial class SysManage_ChuCuoLeiBie2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            //limit("文件管理");

            Bind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        int a = 0;

        if (TextBox1.Text.Trim() == "一般性错误")
        {
            a = 2;
        }
        else if (TextBox1.Text.Trim() == "CQC原因")
        {
            a = 4;
        }
        else if (TextBox1.Text.Trim() == "客户原因")
        {
            a = 5;
        }
        else if (TextBox1.Text.Trim() == "与认证规则或标准不符")
        {
            a = 3;
        }
        else if (TextBox1.Text.Trim() == "标准实施错误")
        {
            a = 1;
        }
        else
        {
            a = 6;
        }
        string sql = "insert into UserChuCuoWenJian values('" + TextBox1.Text.Trim() + "','" + TextBox2.Text.Trim() + "','" + TextBox3.Text.Trim() + "','" + TextBox4.Text.Trim() + "','" + TextBox5.Text.Trim() + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + a + "')";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();
        Bind();

    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select top 10 * from UserChuCuoWenJian where fillname='杨郑继' order by departmentid desc";
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

        string sql = "delete from UserChuCuoWenJian where departmentid='" + id + "' and fillname='" + Session["UserName"].ToString() + "'";
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

        string uuname2 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
        string uuname = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());
        string uuname4 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());

        string sql = "update UserChuCuoWenJian set wenyuan='" + uuname2 + "',name='" + uuname + "',fax='" + uuname4 + "' where departmentid='" + KeyId + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        GridView1.EditIndex = -1;
        Bind();
    }
}