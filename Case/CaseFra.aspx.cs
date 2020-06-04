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

public partial class Case_CaseFra : System.Web.UI.Page
{
    protected string url = string.Empty;
    protected string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"].ToString();
        if (id == "1")
        {
            url = "CaseDaiFen2.aspx";
        }
     
        else if (id == "yipi")
        {
            url = "QuotationApproed.aspx";
        }
        else if (id == "huiqian")
        {
            url = "QuotationHuiqian.aspx";
        }
        else if (id == "kaian")
        {
            url = "QuoKaiAn.aspx";
        }
        else if (id == "kaian2")
        {
            url = "QuoKaiAn2.aspx";
        }
        else if (id == "3")
        {
            url = "CaseDaiFen.aspx";
        }
        else if (id == "4")
        {
            url = "CaseDaiFen3.aspx";
        }
    }
}