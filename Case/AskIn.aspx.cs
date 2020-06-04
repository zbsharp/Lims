using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Case_AskIn : System.Web.UI.Page
{

    protected string id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"].ToString();
    }
}