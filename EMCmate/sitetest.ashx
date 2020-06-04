<%@ WebHandler Language="C#" Class="sitetest" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

public class sitetest : IHttpHandler, IRequiresSessionState
{
    private string dutyname;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string parameter = context.Request["parameter"].ToString();
        dutyname = Dutyname(context);
        switch (parameter)
        {
            case "load":
                GetSession(context);
                break;
            case "region":
                ComRegion(context);
                break;
            case "regionFY":
                ComRegionFY(context);
                break;
            case "regiontwo":
                {
                    int id = Convert.ToInt32(context.Request["id"]);
                    ComRegionTwo(context, id);
                    break;
                }
            case "regiontwoFY":
                {
                    int id = Convert.ToInt32(context.Request["id"]);
                    ComRegionTwo(context, id);
                    break;
                }
            case "regionprice":
                {
                    int id = Convert.ToInt32(context.Request["id"]);
                    Getprice(context, id);
                    break;
                }
            case "regionpriceFY":
                {
                    int id = Convert.ToInt32(context.Request["id"]);
                    GetpriceFY(context, id);
                    break;
                }
            case "customer":
                string customnername = context.Request["customnername"].ToString();
                string responser = context.Request["Responser"].ToString();
                GetCustomer(context, customnername, responser);
                break;
            case "linkman":
                string kehuid = context.Request["kehuid"].ToString();
                Getlinkman(context, kehuid);
                break;
            case "phone":
                {
                    int id = Convert.ToInt32(context.Request["id"]);
                    GetPhone(context, id);
                }
                break;
            case "LHadd":
                LHAdd(context);
                break;
            case "FYadd":
                FYAdd(context);
                break;
            case "Select":
                Select(context);
                break;
            case "delete":
                {
                    int id = Convert.ToInt32(context.Request["id"]);
                    Delete(context, id);
                }
                break;
            case "Userinfo":
                GetUserInfo(context);
                break;
            case "Update":
                Update(context);
                break;
            case "UpdateFY":
                UpdateFY(context);
                break;
            case "Updatereceptionist":
                Updatereceptionist(context);
                break;
        }
    }

    /// <summary>
    /// 前台编辑
    /// </summary>
    /// <param name="context"></param>
    private void Updatereceptionist(HttpContext context)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        EMCmake eMCmake = jss.Deserialize<EMCmake>(context.Request["data"]);//反序列化
        string emcid = eMCmake.EMCid;
        DateTime starttime = eMCmake.Starttime;
        DateTime stoptime = eMCmake.Stoptime;
        decimal price = eMCmake.Newprice;
        string responser = eMCmake.Responser;
        string remork = eMCmake.Remark;
        string site = eMCmake.Testitem;
        double shijihour = eMCmake.Shijihour;
        decimal shijisumprice = eMCmake.Shijisubprice;
        string emcnumber = eMCmake.EMCnumber;
        string engineer = eMCmake.Engineer;
        string bookingstatus = eMCmake.Bookingstatus;

        TimeSpan timeSpan = stoptime - starttime;
        double horu = timeSpan.TotalHours;

        string name = "";
        bool btime = TimeSection(site, starttime, stoptime, ref name, ref emcid);
        if (btime == false)
        {
            context.Response.Write(jss.Serialize(name));
        }
        else
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = "update EMCmake set starttime='" + starttime + "',stoptime='" + stoptime + "',newprice=" + price + ",responser='" + responser + "',remark='" + remork.Replace('\'', ' ') + "',hour=" + horu + ",bookingstatus='" + bookingstatus + "',EMCnumber='" + emcnumber + "',engineer='" + engineer + "',shijihour=" + shijihour + ",shijisumprice=" + shijisumprice + " where EMCid='" + emcid + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        context.Response.Write(jss.Serialize("ok"));
                    }
                }
                catch (Exception)
                {
                    context.Response.Write(jss.Serialize("on"));
                }
            }
        }
    }

    /// <summary>
    /// 修改福永预约信息
    /// </summary>
    /// <param name="context"></param>
    private void UpdateFY(HttpContext context)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        EMCmake eMCmake = jss.Deserialize<EMCmake>(context.Request["data"]);//反序列化
        string emcid = eMCmake.EMCid;
        DateTime starttime = eMCmake.Starttime;
        DateTime stoptime = eMCmake.Stoptime;
        decimal price = eMCmake.Newprice;
        string responser = eMCmake.Responser;
        string remork = eMCmake.Remark;
        string site = eMCmake.Testitem;
        TimeSpan timeSpan = stoptime - starttime;
        double hour = timeSpan.TotalHours;
        decimal shijisumprice = Convert.ToDecimal(hour) * price;
        string name = "";
        bool btime = TimeSection(site, starttime, stoptime, ref name, ref emcid);
        bool holiday = IsHolidayByDate(starttime, stoptime);

        if (btime == false)
        {
            context.Response.Write(jss.Serialize(name));
        }
        else if (holiday == true && (dutyname == "业务员" || dutyname == "客户经理" || dutyname == "销售助理"))
        {
            //周日不能预约
            context.Response.Write(jss.Serialize("date"));
        }
        else
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = "update EMCmake set starttime='" + starttime + "',stoptime='" + stoptime + "',newprice=" + price + ",responser='" + responser + "',remark='" + remork.Replace('\'', ' ') + "',hour=" + hour + ",shijisumprice=" + shijisumprice + " where EMCid='" + emcid + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        context.Response.Write(jss.Serialize("ok"));
                    }
                }
                catch (Exception)
                {
                    context.Response.Write(jss.Serialize("on"));
                }
            }
        }
    }

    /// <summary>
    /// 修改龙华预约信息
    /// </summary>
    /// <param name="context"></param>
    private void Update(HttpContext context)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        EMCmake eMCmake = jss.Deserialize<EMCmake>(context.Request["data"]);//反序列化
        string emcid = eMCmake.EMCid;
        DateTime starttime = eMCmake.Starttime;
        DateTime stoptime = eMCmake.Stoptime;
        decimal price = eMCmake.Newprice;
        string responser = eMCmake.Responser;
        string remork = eMCmake.Remark;
        string site = eMCmake.Testitem;
        TimeSpan timeSpan = stoptime - starttime;
        double hour = timeSpan.TotalHours;
        decimal shijisumprice = Convert.ToDecimal(hour) * price;

        string name = "";
        bool btime = TimeSection(site, starttime, stoptime, ref name, ref emcid);
        bool holiday = IsHolidayByDate(starttime, stoptime);

        if (btime == false)
        {
            context.Response.Write(jss.Serialize(name));
        }
        else if (holiday == true && (dutyname == "业务员" || dutyname == "客户经理" || dutyname == "销售助理"))
        {
            //周日不能预约
            context.Response.Write(jss.Serialize("date"));
        }
        else
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = "update EMCmake set starttime='" + starttime + "',stoptime='" + stoptime + "',newprice=" + price + ",responser='" + responser + "',remark='" + remork.Replace('\'', ' ') + "',hour=" + hour + ",shijisumprice=" + shijisumprice + " where EMCid='" + emcid + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        context.Response.Write(jss.Serialize("ok"));
                    }
                }
                catch (Exception)
                {
                    context.Response.Write(jss.Serialize("on"));
                }
            }
        }
    }

    private void GetUserInfo(HttpContext context)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ToString()))
        {
            con.Open();
            string sql = "select UserName from UserInfo where (departmentname like '销售%' or departmentname='总经办' or departmentname='EMC部') and flag='否' and UserName!='admin'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<Userinfo> list = new List<Userinfo>();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Userinfo userinfo = new Userinfo();
                    userinfo.Responser = item["UserName"].ToString();
                    list.Add(userinfo);
                }
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            context.Response.Write(jss.Serialize(list));
        }
    }

    private void Delete(HttpContext context, int id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "update EMCmake set isdelete='是',deleteer='" + context.Session["Username"].ToString() + "' where id=" + id + "";
            SqlCommand cmd = new SqlCommand(sql, con);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                context.Response.Write("ok");
            }
            else
            {
                context.Response.Write("on");
            }
        }
    }

    private void Select(HttpContext context)
    {
        int pageindex = Convert.ToInt32(context.Request["page"]); //当前页
        int pagesize = Convert.ToInt32(context.Request["rows"]); //每页显示多少条

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            int start = (pageindex - 1) * pagesize + 1;
            int end = pageindex * pagesize;
            string sql = "";
            string isclick = context.Request["btnselect"] == null ? "" : context.Request["btnselect"].ToString();
            string shwere = string.Empty;
            if (string.IsNullOrEmpty(isclick))
            {
                if (dutyname == "业务员" || dutyname == "客户经理")
                {
                    shwere = " responser='" + context.Session["Username"].ToString() + "' and ";
                }
                else if (dutyname == "销售助理")
                {
                    shwere = " responser in (select marketid from CustomerServer where UserName='" + context.Session["Username"].ToString() + "') or responser='" + context.Session["Username"].ToString() + "' and ";
                }
            }
            else
            {
                string condition = context.Request["txtshwere"].ToString();
                string area = context.Request["area"].ToString();
                string date = context.Request["date"].ToString();
                string str = " and (customername like '%" + condition + "%' or responser like '%" + condition + "%' or EMCid like '%" + condition + "%' or EMCnumber like '%" + condition + "%')";
                if (string.IsNullOrEmpty(area) && string.IsNullOrEmpty(date))
                {
                    if (dutyname == "业务员" || dutyname == "客户经理")
                    {
                        shwere = " responser='" + context.Session["Username"].ToString() + "'" + str + " and ";
                    }
                    else if (dutyname == "销售助理")
                    {
                        shwere = " (responser in (select marketid from CustomerServer where UserName='" + context.Session["Username"].ToString() + "') or responser='" + context.Session["Username"].ToString() + "') " + str + " and ";
                    }
                    else
                    {
                        shwere = " (customername like '%" + condition + "%' or responser like '%" + condition + "%' or EMCid like '%" + condition + "%' or EMCnumber like '%" + condition + "%') and ";
                    }
                }
                else if (string.IsNullOrEmpty(area) && !string.IsNullOrEmpty(date))
                {
                    if (dutyname == "业务员" || dutyname == "客户经理")
                    {
                        shwere = " responser='" + context.Session["Username"].ToString() + "' and CONVERT(varchar(11),starttime,120)='" + date + "'" + str + " and ";
                    }
                    else if (dutyname == "销售助理")
                    {
                        shwere = " (responser in (select marketid from CustomerServer where UserName='" + context.Session["Username"].ToString() + "') or responser='" + context.Session["Username"].ToString() + "') and  CONVERT(varchar(11),starttime,120)='" + date + "'" + str + " and ";
                    }
                    else
                    {
                        shwere = " CONVERT(varchar(11),starttime,120)='" + date + "'" + str + " and ";
                    }
                }
                else if (!string.IsNullOrEmpty(area) && string.IsNullOrEmpty(date))
                {
                    if (dutyname == "业务员" || dutyname == "客户经理")
                    {
                        shwere = " responser='" + context.Session["Username"].ToString() + "' and region='" + area + "'" + str + " and ";
                    }
                    else if (dutyname == "销售助理")
                    {
                        shwere = " (responser in (select marketid from CustomerServer where UserName='" + context.Session["Username"].ToString() + "') or responser='" + context.Session["Username"].ToString() + "') and  region='" + area + "'" + str + " and ";
                    }
                    else
                    {
                        shwere = " region='" + area + "'" + str + " and ";
                    }
                }
                else
                {
                    if (dutyname == "业务员" || dutyname == "客户经理")
                    {
                        shwere = " responser='" + context.Session["Username"].ToString() + "' and region='" + area + "' and CONVERT(varchar(11),starttime,120)='" + date + "' " + str + " and ";
                    }
                    else if (dutyname == "销售助理")
                    {
                        shwere = " (responser in (select marketid from CustomerServer where UserName='" + context.Session["Username"].ToString() + "') or responser='" + context.Session["Username"].ToString() + "') and  region='" + area + "' and CONVERT(varchar(11),starttime,120)='" + date + "'" + str + " and ";
                    }
                    else
                    {
                        shwere = " region='" + area + "' and CONVERT(varchar(11),starttime,120)='" + date + "'" + str + " and ";
                    }
                }
            }
            sql = @"select * from (select ROW_NUMBER() over(order by id desc) as [row],*  from EMCmake where " + shwere + "  isdelete='否'" +
                                                  " ) E where e.row between " + start + " and " + end + "";
            string sqlpagecount = "select COUNT(id) as count from EMCmake where  " + shwere + " isdelete='否'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<EMCmake> list = new List<EMCmake>();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    EMCmake emcmake = new EMCmake();
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
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //string datagrid = jss.Serialize(list);
            //context.Response.Write(jss.Serialize(list));
            PageObject<EMCmake> pageObject = new PageObject<EMCmake>();
            pageObject.total = GetCount(context, sqlpagecount);
            pageObject.rows = list;

            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            // string datagrid = JsonConvert.SerializeObject(pageObject, Formatting.Indented, timeConverter);
            context.Response.Write(JsonConvert.SerializeObject(pageObject, Formatting.Indented, timeConverter));
        }
    }

    private int GetCount(HttpContext context, string sql)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            int i = 0;
            if (dr.Read())
            {
                i = Convert.ToInt32(dr["count"]);
                return i;
            }
            else
            {
                return i;
            }
        }
    }

    /// <summary>
    /// 添加福永预约
    /// </summary>
    /// <param name="context"></param>
    private void FYAdd(HttpContext context)
    {
        string region = context.Request["region"].ToString();
        string testcd = context.Request["testcd"].ToString();
        DateTime datestart = Convert.ToDateTime(context.Request["datestart"]);
        DateTime datestop = Convert.ToDateTime(context.Request["datestop"]);
        string testxm = context.Request["testxm"].ToString();
        string kehuid = context.Request["customername"].ToString();
        string linkman = context.Request["linkman"].ToString();
        string linkmanphone = context.Request["linkmanphone"].ToString();
        string project = context.Request["project"].ToString();
        string chk = context.Request["chk"].ToString();
        string teststandard = context.Request["teststandard"].ToString();
        decimal money = Convert.ToDecimal(context.Request["money"]);//实际费用
        string price = context.Request["price"].ToString();//标准费用
        string isfree = context.Request["isfree"].ToString();//是否免费
        string remork = context.Request["remork"].ToString();
        string baojiaid = context.Request["baojiaid"].ToString();
        string responser = context.Request["respoonser"].ToString();
        string model = context.Request["model"].ToString();
        string emcid = GenerateID();
        TimeSpan time = datestop - datestart;
        double hour = time.TotalHours;
        decimal shijisumprice = money * Convert.ToDecimal(hour);

        string name = "";
        string eeid = ""; //该参数是为了区分修改和新增时判断时间的方法
        bool btime = TimeSection(testcd, datestart, datestop, ref name, ref eeid);
        bool holiday = IsHolidayByDate(datestart, datestop);

        if (btime == false)
        {
            context.Response.Write(name);
        }
        else if (holiday == true && (dutyname == "业务员" || dutyname == "客户经理" || dutyname == "销售助理"))
        {
            //周日不能预约
            context.Response.Write("date");
        }
        else
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = @"insert into EMCmake values('" + region + "','" + datestart + "','" + datestop + "','" + testcd + "','" + testxm + "','" + kehuid + "','" + linkman + "'"
                        + ",'" + linkmanphone + "','" + project + "','" + responser + "','" + DateTime.Now + "','" + context.Session["Username"].ToString() + "','" + chk + "','" + emcid + "','" + teststandard + "','" + price + "'," + money + "," + hour + "," + hour + "," + shijisumprice + ",'" + isfree + "','否','','" + remork + "','否','','" + baojiaid + "','" + model + "','预约成功','','')";
                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    int number = cmd.ExecuteNonQuery();
                    if (number > 0)
                    {
                        context.Response.Write("OK");
                    }
                }
                catch (Exception)
                {
                    context.Response.Write("ON");
                }
            }
        }
    }

    /// <summary>
    /// 添加龙华预约
    /// </summary>
    /// <param name="context"></param>
    private void LHAdd(HttpContext context)
    {
        string region = context.Request["region"].ToString();
        string testcd = context.Request["testcd"].ToString();
        DateTime datestart = Convert.ToDateTime(context.Request["datestart"]);
        DateTime datestop = Convert.ToDateTime(context.Request["datestop"]);
        string testxm = context.Request["testxm"].ToString();
        string kehuid = context.Request["customername"].ToString();
        string linkman = context.Request["linkman"].ToString();
        string linkmanphone = context.Request["linkmanphone"].ToString();
        string project = context.Request["project"].ToString();
        string chk = context.Request["chk"].ToString();
        string teststandard = context.Request["teststandard"].ToString();
        decimal money = Convert.ToDecimal(context.Request["money"]);//实际费用
        string price = context.Request["price"].ToString();//标准费用
        string isfree = context.Request["isfree"].ToString();//是否免费
        string remork = context.Request["remork"].ToString();//备注
        string responser = context.Request["responser"].ToString();
        string baojiaid = context.Request["baojiaid"].ToString();
        string model = context.Request["model"].ToString();
        string emcid = GenerateID();
        TimeSpan time = datestop - datestart;
        double hour = time.TotalHours;
        decimal shijisumprice = money * Convert.ToDecimal(hour);//实际总费用
        string name = "";
        string eeid = ""; //该参数是为了区分修改和新增时判断时间的方法(修改时需要过滤掉自己)
        bool btime = TimeSection(testcd, datestart, datestop, ref name, ref eeid);
        bool holiday = IsHolidayByDate(datestart, datestop);

        if (btime == false)
        {
            //这个时间段已有人预约
            context.Response.Write(name);
        }
        else if (holiday == true && (dutyname == "业务员" || dutyname == "客户经理" || dutyname == "销售助理"))
        {
            //周日不能预约
            context.Response.Write("date");
        }
        else
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = @"insert into EMCmake values('" + region + "','" + datestart + "','" + datestop + "','" + testcd + "','" + testxm + "','" + kehuid + "','" + linkman + "'"
                        + ",'" + linkmanphone + "','" + project + "','" + responser + "','" + DateTime.Now + "','" + context.Session["Username"].ToString() + "','" + chk + "','" + emcid + "','" + teststandard + "','" + price + "'," + money + "," + hour + "," + hour + "," + shijisumprice + ",'" + isfree + "','否','','" + remork + "','否','','" + baojiaid + "','" + model + "','预约成功','','')";
                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    int number = cmd.ExecuteNonQuery();
                    if (number > 0)
                    {
                        context.Response.Write("OK");
                    }
                }
                catch (Exception)
                {
                    context.Response.Write("ON");
                }
            }
        }
    }

    private void GetPhone(HttpContext context, int id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select mobile from CustomerLinkMan where id=" + id + "";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            string phone = "";
            if (dr.Read())
            {
                phone = dr["mobile"].ToString();
            }
            context.Response.Write(phone);
        }
    }

    private void Getlinkman(HttpContext context, string kehuid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select id,name from CustomerLinkMan where customerid='" + kehuid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<CustomerLinkman> list = new List<CustomerLinkman>();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    CustomerLinkman customerLinkman = new CustomerLinkman();
                    customerLinkman.Id = item["id"].ToString();
                    customerLinkman.Name = item["name"].ToString();
                    list.Add(customerLinkman);
                }
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string cmbdata = jss.Serialize(list);
            context.Response.Write(cmbdata);
        }
    }

    private void GetCustomer(HttpContext context, string customnername, string responser)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select Kehuid,CustomName from Customer where Kehuid not like 'D%' and Responser='" + responser + "' and CustomName like '%" + customnername + "%'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<Customner> list = new List<Customner>();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Customner customner = new Customner();
                    customner.Kehuid = item["Kehuid"].ToString();
                    customner.CustomerName = item["CustomName"].ToString();
                    list.Add(customner);
                }
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string cmbdata = jss.Serialize(list);
            context.Response.Write(cmbdata);
        }
    }

    /// <summary>
    /// 加载龙华项目价格
    /// </summary>
    /// <param name="context"></param>
    /// <param name="id"></param>
    private void Getprice(HttpContext context, int id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select price from EMCmakeitme where id=" + id + "";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            decimal price = 0m;
            if (dr.Read())
            {
                price = Convert.ToDecimal(dr["price"]);
            }
            context.Response.Write(price);
        }
    }

    /// <summary>
    /// 加载福永项目价格
    /// </summary>
    /// <param name="context"></param>
    /// <param name="id"></param>
    private void GetpriceFY(HttpContext context, int id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select price from EMCmakeitme where id=" + id + "";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            decimal price = 0m;
            if (dr.Read())
            {
                price = Convert.ToDecimal(dr["price"]);
            }
            context.Response.Write(price);
        }
    }

    private void GetSession(HttpContext context)
    {
        string s = HttpContext.Current.Session["Username"].ToString();
        string json = "{responser:\"" + s + "\",dutyname:\"" + dutyname + "\"}";
        JavaScriptSerializer jss = new JavaScriptSerializer();
        context.Response.Write(jss.Serialize(json));
    }

    /// <summary>
    /// 加载龙华项目
    /// </summary>
    /// <param name="context"></param>
    /// <param name="id"></param>
    private void ComRegionTwo(HttpContext context, int id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select id,item from EMCmakeitme where siteid=" + id + "";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<EMCmakeitme> list = new List<EMCmakeitme>();
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    EMCmakeitme t = new EMCmakeitme();
                    t.Id = Convert.ToInt32(item["id"]);
                    t.Item = item["item"].ToString();
                    list.Add(t);
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string comdata = js.Serialize(list);
            context.Response.Write(comdata);
        }
    }

    /// <summary>
    ///加载龙华测试场地下拉框
    /// </summary>
    /// <param name="context"></param>
    private void ComRegion(HttpContext context)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from EMCmakeSite where region='龙华'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<Test> list = new List<Test>();
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Test t = new Test();
                    t.Id = (int)item["id"];
                    t.Testsite = item["site"].ToString();
                    list.Add(t);
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string comdata = js.Serialize(list);
            context.Response.Write(comdata);
        }
    }

    /// <summary>
    ///加载福永测试场地下拉框
    /// </summary>
    /// <param name="context"></param>
    private void ComRegionFY(HttpContext context)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from EMCmakeSite where region='福永'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<Test> list = new List<Test>();
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Test t = new Test();
                    t.Id = (int)item["id"];
                    t.Testsite = item["site"].ToString();
                    list.Add(t);
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string comdata = js.Serialize(list);
            context.Response.Write(comdata);
        }
    }

    /// <summary>
    /// 加载福永项目
    /// </summary>
    /// <param name="context"></param>
    /// <param name="id"></param>
    private void ComRegionTwoFY(HttpContext context, int id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select id,item from EMCmakeitme where siteid=" + id + "";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<EMCmakeitme> list = new List<EMCmakeitme>();
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    EMCmakeitme t = new EMCmakeitme();
                    t.Id = Convert.ToInt32(item["id"]);
                    t.Item = item["item"].ToString();
                    list.Add(t);
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string comdata = js.Serialize(list);
            context.Response.Write(comdata);
        }
    }

    /// <summary>
    /// 生成预约编号
    /// </summary>
    /// <returns>编号</returns>
    private string GenerateID()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select top 1 EMCid from EMCmake order by id desc";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            string id = "";
            string newyear = DateTime.Now.Year.ToString().Substring(0, 2);
            string newmonth = DateTime.Now.Month.ToString().PadLeft(2, '0');
            if (dr.Read())
            {
                string emcid = dr["EMCid"].ToString();
                string str = emcid.Substring(8, 4);
                string year = emcid.Substring(4, 2);
                string month = emcid.Substring(6, 2);
                if (year == newyear && month == newmonth)
                {
                    int i = Convert.ToInt32(str);
                    i++;
                    string number = i.ToString().PadLeft(4, '0');
                    return id = "BCTC" + year + month + number;
                }
                else
                {
                    return id = "BCTC" + newyear + newmonth + "0001";
                }
            }
            else
            {
                return id = "BCTC" + newyear + newmonth + "0001";
            }
        }
    }

    /// <summary>
    /// 获取当前登录进来人的职位
    /// </summary>
    protected string Dutyname(HttpContext context)
    {
        using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string dutyname = "";
            con1.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", context.Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con1);
            SqlDataReader dr = cmdstate.ExecuteReader();
            if (dr.Read())
            {
                dutyname = dr["dutyname"].ToString();
            }
            dr.Close();
            return dutyname;
        }
    }

    /// <summary>
    /// 判断当前时间区间是否有人预约
    /// </summary>
    /// <returns></returns>
    private bool TimeSection(string site, DateTime start, DateTime stop, ref string name, ref string cmcid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "";
            if (string.IsNullOrEmpty(cmcid))
            {
                sql = "select * from EMCmake where testitem='" + site + "' and isdelete='否'";
            }
            else
            {
                sql = "select * from EMCmake where testitem='" + site + "' and isdelete='否' and EMCid!='" + cmcid + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable tb = ds.Tables[0];
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow item in tb.Rows)
                {
                    DateTime starttime = Convert.ToDateTime(item["starttime"]);
                    DateTime stoptime = Convert.ToDateTime(item["stoptime"]);
                    if (start >= starttime && start < stoptime || starttime >= start && starttime < stop)
                    {
                        name = item["responser"].ToString();
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return true;
            }
        }
    }

    /// <summary>
    /// 判断是否为周末
    /// </summary>
    /// <param name="startdate"></param>
    /// <param name="stopdate"></param>
    /// <returns></returns>
    public bool IsHolidayByDate(DateTime startdate, DateTime stopdate)
    {
        var isHoliday = false;
        var daystart = startdate.DayOfWeek;
        var daystop = stopdate.DayOfWeek;

        if (daystart == DayOfWeek.Sunday || daystop == DayOfWeek.Sunday)
            return true;

        return isHoliday;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}

/// <summary>
/// 测试场地
/// </summary>
public class Test
{
    public int Id { get; set; }
    public string Testsite { get; set; }
}

/// <summary>
/// 测试项目
/// </summary>
public class EMCmakeitme
{
    public int Id { get; set; }
    public string Item { get; set; }
}

/// <summary>
/// 客户名称
/// </summary>
public class Customner
{
    public string Kehuid { get; set; }
    public string CustomerName { get; set; }
}

/// <summary>
/// 客户联系人
/// </summary>
public class CustomerLinkman
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
}

public class EMCmake
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

/// <summary>
/// 分页对象
/// </summary>
public class PageObject<T>
{
    public int total { get; set; }
    public List<T> rows { get; set; }
}

public class Userinfo
{
    public string Responser { get; set; }
}