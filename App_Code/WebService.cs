using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Script.Services;
using System.Linq;

/// <summary>
///WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    public WebService()
    {
        //如果使用设计的组件，请取消注释以下行
        //InitializeComponent(); 
    }
    //[WebMethod]
    //public string[] Getsuggestion(string prefixText, int count)
    //{
    //    List<string> suggestions = new List<string>();

    //    if (prefixText == "公司" || prefixText == "深圳" || prefixText == "有限公司" || prefixText == "有限" || prefixText == "有限公" || prefixText == "深圳市") { }
    //    else
    //    {
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

    //        con.Open();
    //        string responser = "";
    //        string sql2 = "select  customname,weituoname,responser from Customered where customname like '%" + prefixText + "%'";
    //        SqlCommand cmd2 = new SqlCommand(sql2, con);
    //        SqlDataReader dr2 = cmd2.ExecuteReader();
    //        while (dr2.Read())
    //        {
    //            responser = responser + dr2["responser"].ToString() + ",";
    //        }

    //        dr2.Close();


    //        string sql = "select  customname,weituoname,fillname from Customer where customname like '%" + prefixText + "%'";
    //        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
    //        DataSet ds = new DataSet();
    //        ad.Fill(ds);
    //        DataTable dt = ds.Tables[0];
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            // suggestions.Add(dr.GetSqlString(0).Value);

    //            string a = dt.Rows[i][0].ToString(); ;
    //            string b = dt.Rows[i][1].ToString();
    //            string c = dt.Rows[i][2].ToString();

    //            string sql3 = "select departmentname from userinfo where username='" + c + "' and departmentname='客户服务部'";
    //            SqlDataAdapter ad3 = new SqlDataAdapter(sql3, con);
    //            DataSet ds3 = new DataSet();
    //            ad3.Fill(ds3);
    //            DataTable dt3 = ds3.Tables[0];
    //            if (dt3.Rows.Count > 0)
    //            {
    //                suggestions.Add(a);
    //                suggestions.Add("......业务：" + b + "--录入人：" + c + "-业务员：公司客户");

    //            }
    //            else
    //            {
    //                suggestions.Add(a);
    //                suggestions.Add("......业务：" + b + "--录入人：" + c + "-业务员：" + responser);
    //            }
    //        }
    //        con.Close();
    //    }
    //    return suggestions.ToArray();

    //}

    [WebMethod]
    [ScriptMethod]

    public string[] GetTextString(string prefixText, int count)
    {
        List<string> suggestions = new List<string>();
        if (prefixText == "公司" || prefixText == "深圳" || prefixText == "有限公司" || prefixText == "有限" || prefixText == "有限公" || prefixText == "深圳市") { }
        else
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string getcustomersql = "select customname,fillname from customer where customname like '%" + prefixText + "%'";
            SqlDataAdapter getcustomerad = new SqlDataAdapter(getcustomersql, con);

            DataSet getcustomerds = new DataSet();
            getcustomerad.Fill(getcustomerds);
            for (int i = 0; i < getcustomerds.Tables[0].Rows.Count; i++)
            {
                string kehuname = getcustomerds.Tables[0].Rows[i]["customname"].ToString();
                string fillname = getcustomerds.Tables[0].Rows[i]["fillname"].ToString();
                string str = "  客户名称：" + kehuname + "--录入人：" + fillname + "";
                suggestions.Add(str);
            }
            con.Close();
        }
        return suggestions.ToArray();
    }
}
