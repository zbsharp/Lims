using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Customer_CustomerCommonality : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        limit("公共客户");
        Bind();
        //3.１年无成交记录
        //在查询公共客户之前，先筛选一个公共客户
        //Commonality();
    }

    private void Bind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from Customer where Fillname='' and Responser=''  and Kehuid not like 'D%'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
    }

    /// <summary>
    /// 筛选公共客户
    /// </summary>
    private void Commonality()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            //1.１月内无跟进记录
            //将客户表中创建日期超过一个月的客户id全部查出来（创建人为null的除外）
            List<string> list_customerTrace = new List<string>();//定义两个集合，分别用来存放customer表中的客户id和customerTrace表中的客户id
            List<string> list_kehuid = new List<string>();
            string sql_CustomerTrace = string.Format("select kehuid,filltime from Customer where Fillname is not null");
            SqlDataAdapter da_customerTrace = new SqlDataAdapter(sql_CustomerTrace, con);
            DataSet ds_cutomerTrace = new DataSet();
            da_customerTrace.Fill(ds_cutomerTrace);
            StringBuilder sb_id = new StringBuilder();//用于记录客户编号
            if (ds_cutomerTrace.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds_cutomerTrace.Tables[0].Rows.Count; i++)
                {
                    DateTime time = Convert.ToDateTime(ds_cutomerTrace.Tables[0].Rows[i]["filltime"]);
                    TimeSpan span = DateTime.Now - time;
                    double day = span.TotalDays;
                    if (day >= 30)
                    {
                        sb_id.Append("'" + ds_cutomerTrace.Tables[0].Rows[i]["kehuid"].ToString() + "',");
                        list_kehuid.Add(ds_cutomerTrace.Tables[0].Rows[i]["kehuid"].ToString());
                    }
                }
            }
            sb_id = sb_id.Remove(sb_id.Length - 1, 1);
            //将查出来的id作为条件放到日志表中去匹配kehuid。没查到数据的id和创建日志时间超过一个月的为公共客户
            string sql_kehuid = string.Format("select distinct kehuid, filltime from CustomerTrace where kehuid in({0}) order by filltime desc", sb_id);
            SqlDataAdapter da_kehudi = new SqlDataAdapter(sql_kehuid, con);
            DataSet ds_kehuid = new DataSet();
            da_kehudi.Fill(ds_kehuid);
            List<string> newlist_update = new List<string>();
            if (ds_kehuid.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds_kehuid.Tables[0].Rows.Count; i++)
                {
                    DateTime time = Convert.ToDateTime(ds_cutomerTrace.Tables[0].Rows[i]["filltime"]);
                    TimeSpan span = DateTime.Now - time;
                    double day = span.TotalDays;
                    if (day >= 30 || ds_kehuid.Tables[0].Rows[i]["kehuid"].ToString() == string.Empty)
                    {
                        //由于一个客户id可以对应多个日志记录，在此我们只需离当前时间最近的日志
                        if (!newlist_update.Contains(ds_kehuid.Tables[0].Rows[i]["kehuid"].ToString()))
                        {
                            list_customerTrace.Add(ds_kehuid.Tables[0].Rows[i]["kehuid"].ToString());
                        }
                    }
                }
            }
            newlist_update = list_customerTrace.FindAll(x => !list_kehuid.Contains(x));




            //将一个月没有跟踪记录的客户录入人改为null（Fillname为null表示为公共客户）
            //string sql_ctUpdate = string.Format("update Customer set Fillname=null where Kehuid in ('{0}')", sb_id);
            //SqlCommand cmd = new SqlCommand(sql_ctUpdate, con);
            //cmd.ExecuteNonQuery();










            //2.３月无报价记录
            //客户从录入时间起三个月没有报过价
            List<string> list_customer = new List<string>();//定义两个集合，分别用来存放customer表中的客户id和Baojia表中的客户id
            List<string> list_baojia = new List<string>();
            string sql_customer = string.Format("select Kehuid,Filltime from Customer ");
            SqlDataAdapter da_customer = new SqlDataAdapter(sql_customer, con);
            DataSet ds_customer = new DataSet();
            da_customer.Fill(ds_customer);
            StringBuilder sb_kehuid = new StringBuilder();
            //查询录入客户时间超过三个月的 客户id
            if (ds_customer.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds_customer.Tables[0].Rows.Count; i++)
                {
                    DateTime time = Convert.ToDateTime(ds_customer.Tables[0].Rows[i]["Filltime"]);
                    TimeSpan span = DateTime.Now - time;
                    double day = span.TotalDays;
                    if (day >= 90)
                    {
                        sb_kehuid.Append("'" + ds_customer.Tables[0].Rows[i]["Kehuid"].ToString() + "',");
                        list_customer.Add(ds_customer.Tables[0].Rows[i]["Kehuid"].ToString());
                    }
                }
            }
            sb_kehuid = sb_kehuid.Remove(sb_kehuid.Length - 1, 1);
            //查询录入时间超过三个月的客户是否有报价
            string sql_Baojia = string.Format("select KeHuId from BaoJiaBiao where KeHuId in ({0})", sb_kehuid);
            SqlDataAdapter da_Baojia = new SqlDataAdapter(sql_Baojia, con);
            DataSet ds_Baojia = new DataSet();
            da_Baojia.Fill(ds_Baojia);
            StringBuilder sb_baojiaid = new StringBuilder();
            if (ds_Baojia.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds_Baojia.Tables[0].Rows.Count; i++)
                {
                    sb_baojiaid.Append("'" + ds_Baojia.Tables[0].Rows[i]["KeHuId"].ToString() + "',");
                    list_baojia.Add(ds_Baojia.Tables[0].Rows[i]["KeHuId"].ToString());
                }
            }
            List<string> newlist = list_customer.FindAll(x => !list_baojia.Contains(x));//获取三个月没有报价的客户id
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < newlist.Count; i++)
            {
                sb.Append("'" + newlist[i].ToString() + "',");
            }
            sb = sb.Remove(sb.Length - 1, 1);
        }
    }

    protected void limit(string pagename1)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
        }
        else
        {
            con.Close();
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from Customer where Fillname='' and Responser=''  and Kehuid not like 'D%' and CustomName like '%" + TextBox1.Text.Trim() + "%'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }
}