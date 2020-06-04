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
using System.IO;
using System.Text;
using System.Drawing;
using Common;
public partial class SysManage_WorkLogList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Basic.SessionIn())
        {
            Bind();
        }
        else
        {
            string Url="login.aspx";
            MessageBox.Show("登录超时,请重新登录",Url);
        }
    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from WorkLog";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {

    }
}