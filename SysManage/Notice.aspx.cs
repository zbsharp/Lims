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

public partial class SysManage_Notice : System.Web.UI.Page
{

    protected string email = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "insert into notice values('" + Session["UserName"].ToString() + "','" + DateTime.Now.ToShortDateString() + "','" + TextBox1.Text + "','通过','','','','','','')";
        SqlCommand com = new SqlCommand(sql, con);
        com.ExecuteNonQuery();

        //string sql2 = "select youxiang from userinfo";
        //SqlDataAdapter ad = new SqlDataAdapter(sql2, con);
        //DataSet ds = new DataSet();
        //ad.Fill(ds);
        //int a = ds.Tables[0].Rows.Count - 1; ;





        con.Close();
        Response.Write("<script>alert('发布通知成功!')</script>");

    }


}