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

public partial class YangPin_YangPinFenCun : System.Web.UI.Page
{

    protected string sampleid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        sampleid = Request.QueryString["sampleid"].ToString();
        Label1.Text = sampleid;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "insert into YangPin2Detail values('" + Label1.Text + "','','" + TextBox1.Text + "','" + DateTime.Now.ToShortDateString() + "','封存','" + TextBox2.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','"+TextBox3.Text.Trim()+"','','','','')";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();


        string sqlx = "update YangPin2 set state='封存',pubfield1='"+TextBox1.Text+"' where sampleid='" + sampleid + "'";
        SqlCommand cmdx = new SqlCommand(sqlx, con);
        cmdx.ExecuteNonQuery();

        con.Close();
        con.Dispose();
        Response.Write("<script>alert('保存成功')</script>");
    }
}