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
using System.Data.OleDb;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Web.Services;
using Common;
using System.IO;
using System.Text;
using System.Drawing;

public partial class Case_xiangmulist : System.Web.UI.Page
{

    protected string tijiaobianhao = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        tijiaobianhao = Request.QueryString["tijiaobianhao"].ToString();
        if (!IsPostBack)
        {



            Bind();

        }
    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *  from BaoJiaCPXiangMu  where tijiaohaoma='"+tijiaobianhao+"' order  by id desc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
       
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
    }
}