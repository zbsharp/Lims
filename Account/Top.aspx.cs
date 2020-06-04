using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBTable;
using Common;
using System.Collections;
using DBBLL;
using System.Data.SqlClient;
using System.Configuration;
public partial class Account_Top : System.Web.UI.Page
{
    protected string ManageMenu = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {


        Response.Buffer = true;
        Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "No-Cache");

        Label1.Text = Session["UserName"].ToString();
        TextBox1.Text = Session.SessionID;

        // DBTable.DBLoginInfo LoginInfo = new DBLoginInfo();

        // LoginInfo.Add2(Session.SessionID, DateTime.Now, Request.UserHostAddress.ToString(), Label1.Text.Trim());
    }

    protected void LBQuit_Click(object sender, EventArgs e)
    {
        //Session.Remove("UserName");
        Session.Abandon();
        Response.Write("<script>top.location.href='../Login.aspx'</script>");
    }
}