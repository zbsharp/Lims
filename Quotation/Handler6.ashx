<%@ WebHandler Language="C#" Class="Handler6" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public class Handler6 : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string id = context.Request.QueryString["id1"].ToString();
        string gid = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql1 = "insert into haoma values('','','" + DateTime.Now + "')";
        string sql2 = "select max(id) as id from haoma order by id desc";
        SqlCommand com = new SqlCommand(sql1, con);
        SqlCommand com2 = new SqlCommand(sql2, con);
        using (SqlTransaction stran = con.BeginTransaction())
        {
            com.Transaction = stran;
            com2.Transaction = stran;
            try
            {
                com.ExecuteNonQuery();
                SqlDataReader dr = com2.ExecuteReader();
                if (dr.Read())
                {
                    gid = dr["id"].ToString();
                }
                dr.Close();
                stran.Commit();
            }
            catch (Exception ex)
            {
                stran.Rollback();
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
                context.Response.Write("" + gid + "");
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}