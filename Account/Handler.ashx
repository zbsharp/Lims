<%@ WebHandler Language="C#" Class="Handler" %>

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
using System.Web.SessionState;

public class Handler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {

        string id = context.Request.QueryString["id"].ToString();
    

        
   
        
       context.Response.Buffer = true;
       context.Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
       context.Response.Expires = 0;
       context.Response.CacheControl = "no-cache";
       context.Response.AddHeader("Pragma", "No-Cache");
       
       //context.Session.Remove("UserName");
      // context.Session.Abandon();
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();
        //string maid = "";

        //string sql2 = "select top 1 loginid from KeLuRu where ip='" + context.Request.UserHostAddress.ToString() + "'";
        //SqlCommand cmd2 = new SqlCommand(sql2,con);
        //SqlDataReader dr2 = cmd2.ExecuteReader();
        //if (dr2.Read())
        //{
        //    maid = dr2["loginid"].ToString(); 
        //}
        //dr2.Close();
        //string sql = "delete from KeLuRu where ip='" + context.Request.UserHostAddress.ToString() + "' and loginid='" + maid + "'";
        //SqlCommand cmd = new SqlCommand(sql, con);
        //cmd.ExecuteNonQuery();
        //con.Close();
       // context.Response.Write("您已成功退出系统"); 
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}