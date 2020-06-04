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

public partial class Income_CheckFra : System.Web.UI.Page
{

    protected string liushuihao = "";
    protected string kehuname = "";
    protected string url = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        liushuihao = Request.QueryString["liushuihao"].ToString();
        kehuname = Request.QueryString["fukuanren"].ToString();
        if (Session["role"].ToString() == "7" || Session["UserName"].ToString() == "admin" || Session["role"].ToString() == "6")
        {
            url = "Check.aspx?liushuihao=" + liushuihao + "&&fukuanren=" + kehuname;
        }
        else 
        {


          
        }
    }
}