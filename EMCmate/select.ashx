<%@ WebHandler Language="C#" Class="select" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

public class select : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string parameter = context.Request["parameter"].ToString();
        switch (parameter)
        {
            case "Select":
                Select(context);
                break;
            case "FY":
                FY(context);
                break;
            case "LH":
                LH(context);
                break;
        }
    }

    public void FY(HttpContext context)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string time = context.Request["time"].ToString();
            string sql = "select responser,testitem,starttime,stoptime from EMCmake where region='福永' and CONVERT(varchar(11),starttime,120)='" + time + "' and isdelete='否'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<EMCmakeselect> list = new List<EMCmakeselect>();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    EMCmakeselect emc = new EMCmakeselect();
                    emc.Responser = item["responser"].ToString();
                    emc.Starttime = Convert.ToDateTime(item["starttime"]);
                    emc.Stoptime = Convert.ToDateTime(item["stoptime"]);
                    emc.Testitem = item["testitem"].ToString();
                    list.Add(emc);
                }
            }
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            context.Response.Write(JsonConvert.SerializeObject(list, Formatting.Indented, timeConverter));
        }
    }

    public void LH(HttpContext context)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string time = context.Request["time"].ToString();
            string sql = "select responser,testitem,starttime,stoptime from EMCmake where region='龙华' and CONVERT(varchar(11),starttime,120)='" + time + "' and isdelete='否'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<EMCmakeselect> list = new List<EMCmakeselect>();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    EMCmakeselect emc = new EMCmakeselect();
                    emc.Responser = item["responser"].ToString();
                    emc.Starttime = Convert.ToDateTime(item["starttime"]);
                    emc.Stoptime = Convert.ToDateTime(item["stoptime"]);
                    emc.Testitem = item["testitem"].ToString();
                    list.Add(emc);
                }
            }
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            context.Response.Write(JsonConvert.SerializeObject(list, Formatting.Indented, timeConverter));
        }
    }


    public void Select(HttpContext context)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string time = context.Request["time"] == null ? DateTime.Now.ToString("yyyy-MM-dd") : context.Request["time"].ToString();
            string region = context.Request["region"].ToString();
            if (region == "FY")
            {
                region = "福永";
            }
            else
            {
                region = "龙华";
            }
            string sql = "select id,testitem,starttime,stoptime,responser,fillname,customername,remark,emcid from EMCmake where CONVERT(varchar(11),starttime,120)='" + time + "' and region='" + region + "' and isdelete!='是'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<EMCmakeselect> list = new List<EMCmakeselect>();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    EMCmakeselect eMCmake = new EMCmakeselect();
                    eMCmake.ID = Convert.ToInt32(item["id"]);
                    eMCmake.Testitem = item["testitem"].ToString();
                    eMCmake.Starttime = Convert.ToDateTime(item["starttime"]);
                    eMCmake.Stoptime = Convert.ToDateTime(item["stoptime"]);
                    eMCmake.Responser = item["responser"].ToString();
                    eMCmake.Fillname = item["fillname"].ToString();
                    eMCmake.EMCid = item["emcid"].ToString();
                    eMCmake.Customername = item["customername"].ToString();
                    eMCmake.Remark = item["remark"].ToString();
                    list.Add(eMCmake);
                }
            }
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            context.Response.Write(JsonConvert.SerializeObject(list, Formatting.Indented, timeConverter));
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
public class EMCmakeselect
{
    public int ID { get; set; }
    public string Testitem { get; set; }
    public DateTime Starttime { get; set; }
    public DateTime Stoptime { get; set; }
    public string Responser { get; set; }
    public string Fillname { get; set; }
    public string Customername { get; set; }
    public string Remark { get; set; }
    public string EMCid { get; set; }
}