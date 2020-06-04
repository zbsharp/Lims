<%@ WebHandler Language="C#" Class="exprot" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Web.SessionState;

public class exprot : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string parmes = context.Request["parameter"].ToString();
        switch (parmes)
        {
            case "Select":
                Select(context);
                break;
        }
    }

    public void Select(HttpContext context)
    {
        string btnselect = context.Request["btnselect"] == null ? "" : context.Request["btnselect"].ToString();
        string start, stop;
        if (string.IsNullOrEmpty(btnselect))
        {
            start = context.Request["starttime"].ToString();
            stop = context.Request["stoptime"].ToString();
        }
        else
        {
            start = context.Request["start"].ToString();
            stop = context.Request["stop"].ToString();
        }

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string dutyname = Getdutyname(context);
            string sql = "";
            if (dutyname == "业务员" || dutyname == "客户经理" || dutyname == "销售助理")
            {
                sql = "select * from EMCmake where responser='" + context.Session["Username"].ToString() + "' and CONVERT(varchar(11),starttime,120) between '" + start + "' and '" + stop + "'";
            }
            else
            {
                sql = "select * from EMCmake where CONVERT(varchar(11),starttime,120) between '" + start + "' and '" + stop + "'";
            }
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<EMCclass> list = new List<EMCclass>();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    EMCclass emcmake = new EMCclass();
                    emcmake.ID = Convert.ToInt32(item["id"]);
                    emcmake.Region = item["region"].ToString();
                    emcmake.Starttime = Convert.ToDateTime(item["starttime"]);
                    emcmake.Stoptime = Convert.ToDateTime(item["stoptime"]);
                    emcmake.Testitem = item["testitem"].ToString();
                    emcmake.Testsite = item["testsite"].ToString();
                    emcmake.Customername = item["customername"].ToString();
                    emcmake.Linkman = item["linkman"].ToString();
                    emcmake.Linkmanphone = item["linkmanphone"].ToString();
                    emcmake.Project = item["project"].ToString();
                    emcmake.Responser = item["responser"].ToString();
                    emcmake.Fillname = item["fillname"].ToString();
                    emcmake.Filltime = Convert.ToDateTime(item["filltime"]);
                    emcmake.Isscene = item["isscene"].ToString();
                    emcmake.Isfree = item["isfree"].ToString();
                    emcmake.Isreceive = item["isreceive"].ToString();
                    emcmake.Teststandard = item["teststandard"].ToString();
                    emcmake.Price = item["price"].ToString();
                    emcmake.Newprice = Convert.ToDecimal(item["newprice"]);
                    emcmake.hour = Convert.ToDouble(item["hour"]);
                    emcmake.Sumprice = Convert.ToDecimal(item["sumprice"]);
                    emcmake.Money = Convert.ToDecimal(item["money"]);
                    emcmake.Remark = item["remark"].ToString();
                    emcmake.EMCid = item["EMCid"].ToString();
                    emcmake.Baojiaid = item["baojiaid"].ToString();
                    if (item["shijihour"].ToString() == null || string.IsNullOrEmpty(item["shijihour"].ToString()))
                    {
                        emcmake.Shijihour = 0;
                    }
                    else
                    {
                        emcmake.Shijihour = Convert.ToDouble(item["shijihour"]);
                    }
                    emcmake.Model = item["model"].ToString();
                    emcmake.Bookingstatus = item["bookingstatus"].ToString();
                    emcmake.EMCnumber = item["EMCnumber"].ToString();
                    emcmake.Engineer = item["engineer"].ToString();
                    if (item["shijisumprice"].ToString() == null || string.IsNullOrEmpty(item["shijisumprice"].ToString()))
                    {
                        emcmake.Shijisubprice = 0;
                    }
                    else
                    {
                        emcmake.Shijisubprice = Convert.ToDecimal(item["shijisumprice"]);
                    }
                    list.Add(emcmake);
                }
            }
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            context.Response.Write(JsonConvert.SerializeObject(list, Formatting.Indented, timeConverter));
        }
    }

    public string Getdutyname(HttpContext context)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string dutyname = "";//职位
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", context.Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con);
            SqlDataReader dr = cmdstate.ExecuteReader();
            if (dr.Read())
            {
                dutyname = dr["dutyname"].ToString();
            }
            return dutyname;
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

public class EMCclass
{
    public int ID { get; set; }
    public string Region { get; set; }
    public DateTime Starttime { get; set; }
    public DateTime Stoptime { get; set; }
    public string Testitem { get; set; }
    public string Testsite { get; set; }
    public string Customername { get; set; }
    public string Linkman { get; set; }
    public string Linkmanphone { get; set; }
    public string Project { get; set; }
    public string Responser { get; set; }
    public DateTime Filltime { get; set; }
    public string Fillname { get; set; }
    public string Isscene { get; set; }
    public string EMCid { get; set; }
    public string Teststandard { get; set; }
    public string Price { get; set; }
    public decimal Newprice { get; set; }
    public double hour { get; set; }
    public double Shijihour { get; set; }
    public decimal Sumprice { get; set; }
    public decimal Shijisubprice { get; set; }
    public string Isfree { get; set; }
    public string Isreceive { get; set; }
    public decimal Money { get; set; }
    public string Baojiaid { get; set; }
    public string Remark { get; set; }
    public string Model { get; set; }
    public string Bookingstatus { get; set; }
    public string EMCnumber { get; set; }
    public string Engineer { get; set; }
}
