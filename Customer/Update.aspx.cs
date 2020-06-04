using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Common;

public partial class Customer_Update : System.Web.UI.Page
{

    protected string CustomerId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CustomerId = Request.QueryString["kehuid"].ToString();

        if (!IsPostBack)
        {
           
           
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "update CustomerTrace set xiacitime='" + Convert.ToDateTime(Text1.Value.Trim()) + "' where genzongid='" + CustomerId + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();

        ld.Text = "<script>alert('修改成功');</script>";
    }
}