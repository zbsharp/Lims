<%@ WebHandler Language="C#" Class="charge" %>

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

public class charge : IHttpHandler, IRequiresSessionState
{
    private string dutyname;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        dutyname = Dutyname(context);
        string parameter = context.Request["parameter"].ToString();
        switch (parameter)
        {
            case "load":
                GetSession(context);
                break;
            case "customer":
                GetCustomer(context);
                break;
            case "linkman":
                Getlinkman(context);
                break;
            case "phone":
                GetPhone(context);
                break;
            case "Bankaccount":
                GetBankaccount(context);
                break;
            case "Select":
                Select(context);
                break;
            case "Add":
                Add(context);
                break;
            case "SelectCharge":
                SelectCharge(context);
                break;
            case "Delete":
                Delete(context);
                break;
        }
    }
    private void Delete(HttpContext context)
    {
        int id = Convert.ToInt32(context.Request["id"]);
        string chargeid = context.Request["chargeid"].ToString();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            try
            {
                string sqlupdate = "update Charge set standby='是' where id=" + id + "";
                SqlCommand cmdupdate = new SqlCommand(sqlupdate, con);
                int iupdate = cmdupdate.ExecuteNonQuery();
                string sqldelete = "delete ChargeEMCid where chargeid='" + chargeid + "'";
                SqlCommand cmddelete = new SqlCommand(sqldelete, con);
                int idelete = cmddelete.ExecuteNonQuery();
                if (iupdate > 0)
                {
                    context.Response.Write("ok");
                }
                else
                {
                    context.Response.Write("on");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }
    }

    private void Add(HttpContext context)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        ObjJson json = jss.Deserialize<ObjJson>(context.Request["jsonobj"]);
        string kehuid = json.customner;
        string linkmanid = json.linkman;
        int bankcountid = json.Bankaccount;
        string emcid = json.emcid;
        string[] arremcid = emcid.Split(',');
        string currency = json.Currency;
        string remark = json.Remark;
        decimal money = 0m;
        string chargeid = GetChargeid();

        int num1 = 0, num2 = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            try
            {
                string sqlmoney = "select SUM(sumprice) as money from EMCmake where id in(" + emcid + ")";
                SqlCommand cmdmoney = new SqlCommand(sqlmoney, con);
                SqlDataReader drmoney = cmdmoney.ExecuteReader();
                if (drmoney.Read())
                {
                    money = Convert.ToDecimal(drmoney["money"]);
                }
                drmoney.Close();
                string sqladd = "insert into [dbo].[Charge] values('" + chargeid + "','" + kehuid + "','" + linkmanid + "','" + bankcountid + "','" + money + "','" + currency + "','" + DateTime.Now + "','" + context.Session["Username"].ToString() + "','否','" + remark + "')";
                SqlCommand cmdadd = new SqlCommand(sqladd, con);
                num1 = cmdadd.ExecuteNonQuery();
                //向收费单与测试预约关联表插入数据
                for (int i = 0; i < arremcid.Length; i++)
                {
                    string sqlceid = "insert ChargeEMCid values('" + chargeid + "'," + arremcid[i] + ")";
                    SqlCommand ChargeEMCid = new SqlCommand(sqlceid, con);
                    num2 = ChargeEMCid.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(jss.Serialize(ex.Message));
                return;
            }
        }
        if (num1 > 0 && num2 > 0)
        {
            context.Response.Write(jss.Serialize("ok"));
        }
        else
        {
            context.Response.Write(jss.Serialize("on"));
        }
    }

    private string GetChargeid()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select top 1 chargeid  from Charge order by id desc";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            string id = "";
            string newyear = DateTime.Now.Year.ToString().Substring(0, 2);
            string newmonth = DateTime.Now.Month.ToString().PadLeft(2, '0');
            if (dr.Read())
            {
                string emcid = dr["chargeid"].ToString();
                string str = emcid.Substring(8, 7);
                string year = emcid.Substring(4, 2);
                string month = emcid.Substring(6, 2);
                if (year == newyear && month == newmonth)
                {
                    int i = Convert.ToInt32(str);
                    i++;
                    string number = i.ToString().PadLeft(7, '0');
                    return id = "BCTC" + year + month + number;
                }
                else
                {
                    return id = "BCTC" + newyear + newmonth + "0000001";
                }
            }
            else
            {
                return id = "BCTC" + newyear + newmonth + "0000001";
            }
        }
    }

    private void Select(HttpContext context)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from EMCmake where responser='" + context.Session["Username"].ToString() + "' and isdelete='否' order by id desc";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<ChargeEMC> list = new List<ChargeEMC>();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    ChargeEMC chargeEMC = new ChargeEMC();
                    chargeEMC.ID = Convert.ToInt32(item["id"]);
                    chargeEMC.Region = item["region"].ToString();
                    chargeEMC.Test = item["testsite"].ToString();
                    chargeEMC.Site = item["testitem"].ToString();
                    chargeEMC.Teststandard = item["teststandard"].ToString();
                    chargeEMC.Starttime = Convert.ToDateTime(item["starttime"]);
                    chargeEMC.Stoptime = Convert.ToDateTime(item["stoptime"]);
                    chargeEMC.Responser = item["responser"].ToString();
                    chargeEMC.Remork = item["remark"].ToString();
                    chargeEMC.Project = item["project"].ToString();
                    chargeEMC.Hour = Convert.ToDouble(item["hour"]);
                    chargeEMC.Money = Convert.ToDecimal(item["sumprice"]);
                    chargeEMC.Customername = item["customername"].ToString();
                    list.Add(chargeEMC);
                }
            }
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            context.Response.Write(JsonConvert.SerializeObject(list, Formatting.Indented, timeConverter));
        }
    }

    private void GetBankaccount(HttpContext context)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select id,openaccout from Bankaccount";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<Bankaccount> list = new List<Bankaccount>();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Bankaccount bankaccount = new Bankaccount();
                    bankaccount.ID = Convert.ToInt32(item["id"]);
                    bankaccount.Name = item["openaccout"].ToString();
                    list.Add(bankaccount);
                }
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            context.Response.Write(jss.Serialize(list));
        }
    }

    private void GetPhone(HttpContext context)
    {
        string id = context.Request["id"].ToString();
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

    private void Getlinkman(HttpContext context)
    {
        string kehuid = context.Request["kehuid"].ToString();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select id,name from CustomerLinkMan where customerid='" + kehuid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<ChargeLinkman> list = new List<ChargeLinkman>();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    ChargeLinkman customerLinkman = new ChargeLinkman();
                    customerLinkman.ID = item["id"].ToString();
                    customerLinkman.Name = item["name"].ToString();
                    list.Add(customerLinkman);
                }
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            context.Response.Write(jss.Serialize(list));
        }
    }

    private void GetCustomer(HttpContext context)
    {
        string responser = context.Request["responser"].ToString();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select Kehuid,CustomName from Customer where Kehuid not like 'D%' and Responser='" + context.Session["Username"].ToString() + "' and CustomName in (select customername from EMCmake where responser='" + context.Session["Username"].ToString() + "')";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<ChargeCustomner> list = new List<ChargeCustomner>();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    ChargeCustomner chargeCustomner = new ChargeCustomner();
                    chargeCustomner.CustomerName = item["CustomName"].ToString();
                    chargeCustomner.Kehuid = item["Kehuid"].ToString();
                    list.Add(chargeCustomner);
                }
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            context.Response.Write(jss.Serialize(list));
        }
    }

    private void GetSession(HttpContext context)
    {
        string s = HttpContext.Current.Session["Username"].ToString();
        context.Response.Write(s);
    }


    private void SelectCharge(HttpContext context)
    {
        int pageindex = Convert.ToInt32(context.Request["page"]); //当前页
        int pagesize = Convert.ToInt32(context.Request["rows"]); //每页显示多少条
        string shwere = context.Request["btnselect"] == null ? "" : context.Request["btnselect"].ToString();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "";
            int start = (pageindex - 1) * pagesize + 1;
            int end = pageindex * pagesize;
            if (string.IsNullOrEmpty(shwere))
            {
                if (dutyname == "系统管理员")
                {
                    sql = @"select * from (
                        select ROW_NUMBER() over(order by id desc) as rowindex,id,chargeid,(select CustomName from Customer where kehuid=Charge.kehuid) as customername,(select name from CustomerLinkMan where id=Charge.linkmanid) as linkman,
                        (select openaccout from Bankaccount where id=Charge.bankaccountid) as openaccout,money,currency,filltime,fillname,remark
                         from Charge where standby !='是') as TableCharge where TableCharge.rowindex between " + start + " and " + end + "";
                }
                else
                {
                    sql = @"select * from (
                            select ROW_NUMBER() over(order by id desc) as rowindex,id,chargeid,(select CustomName from Customer where kehuid=Charge.kehuid) as customername,(select name from CustomerLinkMan where id=Charge.linkmanid) as linkman,
                            (select openaccout from Bankaccount where id=Charge.bankaccountid) as openaccout,money,currency,filltime,fillname,remark
                             from Charge where fillname='" + context.Session["Username"].ToString() + "' and standby !='是') as TableCharge where TableCharge.rowindex between " + start + " and " + end + "";
                }
            }
            else
            {
                string condition = context.Request["condition"].ToString();
                if (dutyname == "系统管理员")
                {
                    sql = @"select * from (
                        select ROW_NUMBER() over(order by id desc) as rowindex,id,chargeid,(select CustomName from Customer where kehuid=Charge.kehuid) as customername,(select name from CustomerLinkMan where id=Charge.linkmanid) as linkman,
                        (select openaccout from Bankaccount where id=Charge.bankaccountid) as openaccout,money,currency,filltime,fillname,remark
                         from Charge where standby !='是' and (chargeid like '%" + condition + "%' or kehuid in (select * from Customer where CustomName like '%" + condition + "%'))) as TableCharge where TableCharge.rowindex between " + start + " and " + end + "";
                }
                else
                {
                    sql = @"select * from (
                            select ROW_NUMBER() over(order by id desc) as rowindex,id,chargeid,(select CustomName from Customer where kehuid=Charge.kehuid) as customername,(select name from CustomerLinkMan where id=Charge.linkmanid) as linkman,
                            (select openaccout from Bankaccount where id=Charge.bankaccountid) as openaccout,money,currency,filltime,fillname,remark
                             from Charge where standby !='是' and fillname='" + context.Session["Username"].ToString() + "' and (chargeid like '%" + condition + "%' or kehuid in (select * from Customer where CustomName like '%" + condition + "%'))) as TableCharge where TableCharge.rowindex between " + start + " and " + end + "";
                }
            }

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<Chargeselect> list = new List<Chargeselect>();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Chargeselect chargeselect = new Chargeselect();
                    chargeselect.ID = Convert.ToInt32(item["id"]);
                    chargeselect.Chargeid = item["chargeid"].ToString();
                    chargeselect.Customer = item["customername"].ToString();
                    chargeselect.linkman = item["linkman"].ToString();
                    chargeselect.Openaccot = item["openaccout"].ToString();
                    chargeselect.Money = Convert.ToDecimal(item["money"]);
                    chargeselect.Currency = item["currency"].ToString();
                    chargeselect.Filltime = Convert.ToDateTime(item["filltime"]);
                    chargeselect.Fillname = item["fillname"].ToString();
                    chargeselect.Remark = item["remark"].ToString();
                    list.Add(chargeselect);
                }
            }
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            context.Response.Write(JsonConvert.SerializeObject(list, Formatting.Indented, timeConverter));
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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}

public class ChargeCustomner
{
    public string Kehuid { get; set; }
    public string CustomerName { get; set; }
}

public class ChargeLinkman
{
    public string ID { get; set; }
    public string Name { get; set; }
}

public class Bankaccount
{
    public int ID { get; set; }
    public string Name { get; set; }
}

public class ChargeEMC
{
    public int ID { get; set; }
    public string Region { get; set; }
    public DateTime Starttime { get; set; }
    public DateTime Stoptime { get; set; }
    public DateTime Filltime { get; set; }
    public string Site { get; set; }
    public double Hour { get; set; }
    public decimal Money { get; set; }
    public string Test { get; set; }
    public string Teststandard { get; set; }
    public string Customername { get; set; }
    public string Project { get; set; }
    public string Responser { get; set; }
    public string Fillname { get; set; }
    public string Remork { get; set; }
}

/// <summary>
/// 接收前端传过来的json
/// </summary>
public class ObjJson
{
    public string customner { get; set; } //客户编号
    public string linkman { get; set; } //客户联系人编号
    public int Bankaccount { get; set; }//收款账户编号
    public string emcid { get; set; } //预约编号数组
    public string Currency { get; set; }//币种
    public string Remark { get; set; }
}

public class Chargeselect
{
    public int ID { get; set; }
    public string Chargeid { get; set; }
    public string Customer { get; set; }
    public string linkman { get; set; }
    public string Openaccot { get; set; }
    public decimal Money { get; set; }
    public string Currency { get; set; }
    public DateTime Filltime { get; set; }
    public string Fillname { get; set; }
    public string  Remark { get; set; }
}