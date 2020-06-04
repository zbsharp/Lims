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
using System.Drawing;

public partial class ZiYuan_GoodsInfoFilesSee : System.Web.UI.Page
{
    protected string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        id = Request.QueryString["id"].ToString();

        string url = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from GoodsInfo_LoadList where id='" + id + "'";
        SqlCommand com = new SqlCommand(sql, con);
        SqlDataReader dr = com.ExecuteReader();
        if (dr.Read())
        {
            url = dr["url"].ToString();
        }
        dr.Close();
        con.Close();
        Response.Redirect(url);
    }
}