<%@ WebHandler Language="C#" Class="FindNewMessage" %>

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


public class FindNewMessage : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
   


        //context.Session.Remove("UserName");
        // context.Session.Abandon();
        
        string maid = "";
        string shwhere = "";

        string maid1 = "";
        string shwhere1 = "";

        int ma = 0;
        
       
        if (context.Session["role"].ToString() == "8" || context.Session["role"].ToString() == "1" || context.Session["role"].ToString() == "3")
        {
            shwhere = "1=1 and state ='进行中' and wancheng between '" + Convert.ToDateTime(DateTime.Now.AddDays(-1).ToShortDateString()) + "' and '" + DateTime.Now + "'";
        }

        else
        {
            shwhere = " (state ='进行中' and rwbianhao in (select bianhao from ZhuJianEngineer where name='" + context.Session["UserName"].ToString() + "') and wancheng between '" + Convert.ToDateTime(DateTime.Now.AddDays(-1).ToShortDateString()) + "' and '" + DateTime.Now + "')";
        }


    


        if (context.Session["role"].ToString() == "8" || context.Session["role"].ToString() == "1" || context.Session["role"].ToString() == "3")
        {
            shwhere1 = " rwbianhao in (select  rwbianhao from zanting2 where time1 between '" + Convert.ToDateTime(DateTime.Now.AddDays(-1).ToShortDateString()) + "' and '" + DateTime.Now + "' and beizhu3='暂停' )";
        }

        else
        {
            shwhere1 = " ( rwbianhao in (select bianhao from ZhuJianEngineer where name='" + context.Session["UserName"].ToString() + "') and  rwbianhao in (select  rwbianhao from zanting2 where time1 between '" + Convert.ToDateTime(DateTime.Now.AddDays(-1).ToShortDateString()) + "' and '" + DateTime.Now + "' and beizhu3='暂停' ))";
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql2 = "select count(*) as c   from anjianinfo2  where " + shwhere + " ";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            maid = dr2["c"].ToString();
        }
        dr2.Close();
        
        
        string sql21 = "select count(*) as c   from anjianinfo2  where " + shwhere1 + " ";
        SqlCommand cmd21 = new SqlCommand(sql21, con);
        SqlDataReader dr21 = cmd21.ExecuteReader();
        if (dr21.Read())
        {
            maid1 = dr21["c"].ToString();
        }
        dr21.Close();
        
        
        con.Close();

        ma = Convert.ToInt32(maid) + Convert.ToInt32(maid1);

        string f = "停:"+maid1 +"-复:"+maid;
        context.Response.Write(f);

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}