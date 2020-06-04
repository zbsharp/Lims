using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.Util;
using Common;
public partial class Customer_CustomEmail : System.Web.UI.Page
{

    protected string tiaokuan = "";
    protected string name = "";
    SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

        tiaokuan = Server.UrlDecode(Request.QueryString["kehu"].ToString());

        DataBind();
        sqlconn.Open();

        try
        {


            string strEmails = txtReceiver.Text.Trim();
            string[] strEmail = strEmails.Split('|');
            string sumEmail = "";
            for (int i = 0; i < strEmail.Length; i++)
            {

                string SQL = "select email from CustomerLinkMan where linkmanname='" + strEmail[i].ToString() + "' and email !=''";
                SqlCommand cmd = new SqlCommand(SQL, sqlconn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    name = name + dr["email"].ToString() + ";";

                }
                dr.Close();
            }


        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message.ToString() + "')</script>");
        }
        finally
        {
            sqlconn.Close();
        }

        DataBind();

    }
}