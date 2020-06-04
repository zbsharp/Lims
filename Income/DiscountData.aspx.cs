using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Income_DiscountData : System.Web.UI.Page
{
    private int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txFDate.Value = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");
            txTDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
            Bind();
        }
    }

    private void Bind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            DataTable table = Department();
            string bumen = table.Rows[0]["departmentname"].ToString();//部门
            string dutyname = table.Rows[0]["dutyname"].ToString();//职位
            string sql = "";
            if ((dutyname == "总经理" && bumen == "总经办") || bumen == "财务部" || dutyname == "系统管理员" || dutyname == "董事长" || dutyname == "系统管理员")
            {
                sql = @"select BaoJiaId,(select CustomName from Customer where Customer.Kehuid=BaoJiaBiao.KeHuId)as 客户名称,zhehoujia as 实际价格,Discount as 折扣,coupon as 优惠后金额,
                         responser as 报价人,epiboly_Price as 外发金额,paymentmethod as 付款方式,thefirst as 首款金额,isVAT as 含税状态,currency as 币种,
                         (select openaccout from Bankaccount where Bankaccount.id=BaoJiaBiao.zhangdan) as 收款账户
                         from BaoJiaBiao where HuiQianBiaoZhi='是'  and CONVERT(datetime,huiqiantime) between '" + txFDate.Value + "' and '" + txTDate.Value + "' order by huiqiantime desc";
            }
            else if (bumen == "标源销售部")
            {
                sql = @"select BaoJiaId,(select CustomName from Customer where Customer.Kehuid=BaoJiaBiao.KeHuId)as 客户名称,zhehoujia as 实际价格,Discount as 折扣,coupon as 优惠后金额,
                        responser as 报价人,epiboly_Price as 外发金额,paymentmethod as 付款方式,thefirst as 首款金额,isVAT as 含税状态,currency as 币种,
                        (select openaccout from Bankaccount where Bankaccount.id=BaoJiaBiao.zhangdan) as 收款账户
                        from BaoJiaBiao where HuiQianBiaoZhi='是' and (BaoJiaId in (select baojiaid from BaoJiaCPXiangMu  where bumen='标源EMC部' or bumen='标源安规部') or 
                        responser in(select username from userinfo where departmentname='标源销售部') or Fillname in (select username from userinfo where departmentname='标源销售部'))   and CONVERT(datetime,huiqiantime) between '" + txFDate.Value + "' and '" + txTDate.Value + "' order by huiqiantime desc";
            }


            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataView dv = ds.Tables[0].DefaultView;
            PagedDataSource pds = new PagedDataSource();
            AspNetPager1.RecordCount = dv.Count;
            pds.DataSource = dv;
            pds.AllowPaging = true;
            pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
            pds.PageSize = AspNetPager1.PageSize;
            GridView1.DataSource = pds;
            GridView1.DataBind();
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        AspNetPager1.Visible = false;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string chooseID = dw_where.SelectedValue;
            string value = tx_value.Text.ToString().Replace('\'', ' ').Trim();
            string sql = "";
            DataTable table = Department();
            string bumen = table.Rows[0]["departmentname"].ToString();//部门
            string dutyname = table.Rows[0]["dutyname"].ToString();//职位
            if ((dutyname == "总经理" && bumen == "总经办") || bumen == "财务部" || dutyname == "系统管理员" || dutyname == "董事长" || dutyname == "系统管理员")
            {
                if (chooseID == "customname")
                {
                    sql = @"select BaoJiaId,(select CustomName from Customer where Customer.Kehuid=BaoJiaBiao.KeHuId)as 客户名称,zhehoujia as 实际价格,Discount as 折扣,coupon as 优惠后金额,
                       responser as 报价人,epiboly_Price as 外发金额,paymentmethod as 付款方式,thefirst as 首款金额,isVAT as 含税状态,currency as 币种,
                       (select openaccout from Bankaccount where Bankaccount.id=BaoJiaBiao.zhangdan) as 收款账户
                       from BaoJiaBiao where HuiQianBiaoZhi='是'  and KeHuId in (select Kehuid from Customer where CustomName like '%" + value + "%')  and CONVERT(datetime,huiqiantime) between '" + txFDate.Value + "' and '" + txTDate.Value + "' order by huiqiantime desc";
                }
                else
                {
                    sql = @"select BaoJiaId,(select CustomName from Customer where Customer.Kehuid=BaoJiaBiao.KeHuId)as 客户名称,zhehoujia as 实际价格,Discount as 折扣,coupon as 优惠后金额,
                       responser as 报价人,epiboly_Price as 外发金额,paymentmethod as 付款方式,thefirst as 首款金额,isVAT as 含税状态,currency as 币种,
                       (select openaccout from Bankaccount where Bankaccount.id=BaoJiaBiao.zhangdan) as 收款账户
                       from BaoJiaBiao where HuiQianBiaoZhi='是' 
                       and " + chooseID + " like '%" + value + "%'  and CONVERT(datetime,huiqiantime) between '" + txFDate.Value + "' and '" + txTDate.Value + "' order by huiqiantime desc";
                }
            }
            else if (bumen == "标源销售部")
            {
                if (chooseID == "customname")
                {
                    sql = @"select BaoJiaId,(select CustomName from Customer where Customer.Kehuid=BaoJiaBiao.KeHuId)as 客户名称,zhehoujia as 实际价格,Discount as 折扣,coupon as 优惠后金额,
                       responser as 报价人,epiboly_Price as 外发金额,paymentmethod as 付款方式,thefirst as 首款金额,isVAT as 含税状态,currency as 币种,
                       (select openaccout from Bankaccount where Bankaccount.id=BaoJiaBiao.zhangdan) as 收款账户
                       from BaoJiaBiao where HuiQianBiaoZhi='是'  and KeHuId in (select Kehuid from Customer where CustomName like '%" + value + "%') and (BaoJiaId in (select baojiaid from BaoJiaCPXiangMu  where bumen='标源EMC部' or bumen='标源安规部') or responser in(select username from userinfo where departmentname = '标源销售部') or Fillname in (select username from userinfo where departmentname = '标源销售部')) and CONVERT(datetime,huiqiantime) between '" + txFDate.Value + "' and '" + txTDate.Value + "' order by huiqiantime desc";
                }
                else
                {
                    sql = @"select BaoJiaId,(select CustomName from Customer where Customer.Kehuid=BaoJiaBiao.KeHuId)as 客户名称,zhehoujia as 实际价格,Discount as 折扣,coupon as 优惠后金额,
                       responser as 报价人,epiboly_Price as 外发金额,paymentmethod as 付款方式,thefirst as 首款金额,isVAT as 含税状态,currency as 币种,
                       (select openaccout from Bankaccount where Bankaccount.id=BaoJiaBiao.zhangdan) as 收款账户
                       from BaoJiaBiao where HuiQianBiaoZhi='是' 
                       and " + chooseID + " like '%" + value + "%' and  (BaoJiaId in (select baojiaid from BaoJiaCPXiangMu  where bumen='标源EMC部' or bumen='标源安规部') or responser in(select username from userinfo where departmentname = '标源销售部') or Fillname in (select username from userinfo where departmentname = '标源销售部')) and CONVERT(datetime,huiqiantime) between '" + txFDate.Value + "' and '" + txTDate.Value + "' order by huiqiantime desc";
                }
            }

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }

    protected void bt_derive_Click(object sender, EventArgs e)
    {
        GridView1.AllowPaging = false; //清除分页
        GridView1.AllowSorting = false; //清除排序     
        BindDataWithoutPaging();
        base.Response.ClearContent();
        base.Response.AddHeader("content-disposition", "attachment; filename=Quotedprice" + DateTime.Now.ToShortDateString() + ".xls");
        base.Response.ContentType = "application/ms-excel";
        base.Response.Charset = "UTF-8";
        base.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
        this.GridView1.RenderControl(writer);
        base.Response.Write(stringWriter.ToString());
        base.Response.End();
        GridView1.AllowSorting = true; //恢复分页          
        Bind(); //再次绑定
    }

    private void BindDataWithoutPaging()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = @"select BaoJiaId,(select CustomName from Customer where Customer.Kehuid=BaoJiaBiao.KeHuId)as 客户名称,zhehoujia as 实际价格,Discount as 折扣,coupon as 优惠后金额,
                         responser as 报价人,epiboly_Price as 外发金额,paymentmethod as 付款方式,thefirst as 首款金额,isVAT as 含税状态,currency as 币种,
                         (select openaccout from Bankaccount where Bankaccount.id=BaoJiaBiao.zhangdan) as 收款账户
                         from BaoJiaBiao where HuiQianBiaoZhi='是' and responser !='admin' and CONVERT(datetime,huiqiantime) between '" + txFDate.Value + "' and '" + txTDate.Value + "' order by huiqiantime desc";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private DataTable Department()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(sql_dutyname, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
}