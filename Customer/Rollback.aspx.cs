using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

public partial class Customer_Rollback : System.Web.UI.Page
{
    private int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        limit();
        if (!IsPostBack)
        {
            Bind();
        }
    }
    /// <summary>
    /// 加载时绑定GridView
    /// </summary>
    protected void Bind()
    {
        string sql = "";
        DataTable da = Dutyname();
        string dutyname = da.Rows[0]["dutyname"].ToString();//职位
        string dn = da.Rows[0]["departmentname"].ToString();//部门
        //系统管理员、董事长、总经理查看所有
        //销售经理查看自己部门
        if (dutyname == "系统管理员" || (dutyname.Trim() == "总经理" && dn.Trim() == "总经办") || (dutyname == "董事长" && Session["Username"].ToString() != "蒋娜"))
        {
            sql = @"select BaoJiaId,(select CustomName from Customer where Kehuid=BaoJiaBiao.KeHuId) as customername ,
                        ShenPiBiaoZhi,HuiQianBiaoZhi,zhehoujia,Discount,kaianbiaozhi,paymentmethod,Filltime,responser,coupon,currency,kuozhanfei
                        from BaoJiaBiao where (ShenPiBiaoZhi = '一级通过' or ShenPiBiaoZhi = '二级通过') and isdelete='否' and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13  order by id desc";
            Bind_Gridview(sql);
        }
        else if (dutyname.Trim() == "客户经理" && dn != "销售大客户部")
        {
            sql = @"select BaoJiaId,(select CustomName from Customer where Kehuid=BaoJiaBiao.KeHuId) as customername ,
                      ShenPiBiaoZhi,HuiQianBiaoZhi,zhehoujia,Discount,kaianbiaozhi,paymentmethod,Filltime,responser,coupon,currency,kuozhanfei
                      from BaoJiaBiao where (ShenPiBiaoZhi = '一级通过' or ShenPiBiaoZhi = '二级通过') 
                      and KeHuId in (select  customerid from Customer_Sales where responser in (select UserName from UserInfo where department = '" + dn + "')) and isdelete='否' and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13  order by id desc";
            Bind_Gridview(sql);
        }
        else if (dn == "销售大客户部")
        {
            sql = @" select BaoJiaId,(select CustomName from Customer where Kehuid=BaoJiaBiao.KeHuId) as customername ,
                    ShenPiBiaoZhi,HuiQianBiaoZhi,zhehoujia,Discount,kaianbiaozhi,paymentmethod,Filltime,responser,coupon,currency,kuozhanfei
                    from BaoJiaBiao where (ShenPiBiaoZhi='一级通过' or ShenPiBiaoZhi='二级通过') 
                    and KeHuId in (select  customerid from Customer_Sales where
                    responser in( select name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or responser = '" + Session["Username"].ToString() + "') and isdelete='否' and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13  order by id desc";

            Bind_Gridview(sql);
        }
        else if ((dn.Trim() == "标源销售部" || dn.Trim() == "销售龙华部") && dutyname.Trim() == "总经理")
        {
            sql = @"select BaoJiaId,(select CustomName from Customer where Kehuid=BaoJiaBiao.KeHuId) as customername ,
                      ShenPiBiaoZhi,HuiQianBiaoZhi,zhehoujia,Discount,kaianbiaozhi,paymentmethod,Filltime,responser,coupon,currency,kuozhanfei
                      from BaoJiaBiao where (ShenPiBiaoZhi = '一级通过' or ShenPiBiaoZhi = '二级通过') 
                      and KeHuId in (select  customerid from Customer_Sales where responser in (select UserName from UserInfo where department = '" + dn + "')) and isdelete='否' and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13  order by id desc";
            Bind_Gridview(sql);
        }
        else
        {
            sql = @"select BaoJiaId,(select CustomName from Customer where Kehuid = BaoJiaBiao.KeHuId) as customername ,
                    ShenPiBiaoZhi,HuiQianBiaoZhi,zhehoujia,Discount,kaianbiaozhi,paymentmethod,Filltime,responser,coupon,currency,kuozhanfei
                    from BaoJiaBiao where(ShenPiBiaoZhi = '一级通过' or ShenPiBiaoZhi = '二级通过') and responser = '" + Session["UserName"].ToString() + "'and isdelete = '否' and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%') and LEN(BaoJiaId)< 13  order by id desc";
            Bind_Gridview(sql);
        }
    }
    /// <summary>
    /// 按查询条件绑定Gridview
    /// </summary>
    /// <param name="shwere"></param>
    protected void Bind_shwere(string shwere)
    {
        string sql = "";
        DataTable da = Dutyname();
        string dutyname = da.Rows[0]["dutyname"].ToString();//职位
        string dn = da.Rows[0]["departmentname"].ToString();//部门
        //系统管理员、董事长、总经理查看所有
        //销售经理查看自己部门
        if (dutyname == "系统管理员" || (dutyname.Trim() == "总经理" && dn.Trim() == "总经办") || dutyname == "董事长")
        {
            sql = @"select BaoJiaId,(select CustomName from Customer where Kehuid=BaoJiaBiao.KeHuId) as customername ,
                        ShenPiBiaoZhi,HuiQianBiaoZhi,zhehoujia,Discount,kaianbiaozhi,paymentmethod,Filltime,responser,coupon,currency,kuozhanfei
                        from BaoJiaBiao where (ShenPiBiaoZhi = '一级通过' or ShenPiBiaoZhi = '二级通过') " + shwere + " and isdelete='否' and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13  order by id desc";
        }
        else if (dutyname.Trim() == "客户经理" && dn != "销售大客户部")
        {
            sql = @"select BaoJiaId,(select CustomName from Customer where Kehuid=BaoJiaBiao.KeHuId) as customername ,
                      ShenPiBiaoZhi,HuiQianBiaoZhi,zhehoujia,Discount,kaianbiaozhi,paymentmethod,Filltime,responser,coupon,currency,kuozhanfei  
                      from BaoJiaBiao where (ShenPiBiaoZhi = '一级通过' or ShenPiBiaoZhi = '二级通过') 
                      and KeHuId in (select  customerid from Customer_Sales where responser in (select UserName from UserInfo where department = '" + dn + "')) " + shwere + " and isdelete='否' and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13  order by id desc";
        }
        else if (dn == "销售大客户部")
        {
            sql = @" select BaoJiaId,(select CustomName from Customer where Kehuid=BaoJiaBiao.KeHuId) as customername ,
                    ShenPiBiaoZhi,HuiQianBiaoZhi,zhehoujia,Discount,kaianbiaozhi,paymentmethod,Filltime,responser,coupon,currency,kuozhanfei
                    from BaoJiaBiao where (ShenPiBiaoZhi='一级通过' or ShenPiBiaoZhi='二级通过') 
                    and KeHuId in (select  customerid from Customer_Sales where
                    responser in( select name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or responser = '" + Session["Username"].ToString() + "') " + shwere + " and isdelete='否' and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13  order by id desc";
        }
        else if ((dn.Trim() == "标源销售部" || dn.Trim() == "销售龙华部") && dutyname.Trim() == "总经理")
        {
            sql = @"select BaoJiaId,(select CustomName from Customer where Kehuid=BaoJiaBiao.KeHuId) as customername ,
                      ShenPiBiaoZhi,HuiQianBiaoZhi,zhehoujia,Discount,kaianbiaozhi,paymentmethod,Filltime,responser,coupon,currency,kuozhanfei
                      from BaoJiaBiao where (ShenPiBiaoZhi = '一级通过' or ShenPiBiaoZhi = '二级通过') 
                      and KeHuId in (select  customerid from Customer_Sales where responser in (select UserName from UserInfo where department = '" + dn + "')) " + shwere + " and isdelete='否' and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13  order by id desc";
        }
        else
        {

        }
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            SqlDataAdapter da_sql = new SqlDataAdapter(sql, con);
            DataSet ds_sql = new DataSet();
            da_sql.Fill(ds_sql);
            GridView1.DataSource = ds_sql.Tables[0];
            GridView1.DataBind();
        }
    }
    protected void Bind_Gridview(string sql)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
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
    protected void limit()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select dutyname from UserInfo where UserName='" + Session["Username"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows[0]["dutyname"].ToString() == "系统管理员" || ds.Tables[0].Rows[0]["dutyname"].ToString() == "总经理" || ds.Tables[0].Rows[0]["dutyname"].ToString() == "董事长" || ds.Tables[0].Rows[0]["dutyname"].ToString().Trim() == "客户经理" || ds.Tables[0].Rows[0]["dutyname"].ToString().Trim() == "副总经理")
        {

        }
        else
        {
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Literal1.Text = "";
        AspNetPager1.Visible = false;
        DataTable da = Dutyname();
        string dn = da.Rows[0]["departmentname"].ToString();//部门
        string shwere = "";
        string chooseId = DropDownList1.SelectedValue.Trim();
        if (chooseId == "全部")
        {
            shwere = "and 1=1";
        }
        else if (chooseId == "kehuname")
        {
            shwere = "and KeHuId in (select Kehuid from Customer where CustomName like '%" + TextBox1.Text + "%')";
        }
        else if (chooseId == "bumen")
        {
            shwere = "and KeHuId in (select  customerid from Customer_Sales where responser in (select username from userinfo where departmentname like '%" + TextBox1.Text + "%'))";
        }
        else
        {
            shwere = "and " + chooseId + " like '%" + TextBox1.Text + "%'";
        }
        Bind_shwere(shwere);
    }
    protected DataTable Dutyname()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlDataAdapter dr = new SqlDataAdapter(sql_dutyname, con);
            DataSet ds = new DataSet();
            dr.Fill(ds);
            return ds.Tables[0];
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

            string coupon = e.Row.Cells[15].Text;
            if (coupon == "0.00" || coupon == "&nbsp;" || coupon == "NULL" || coupon == "null")
            {
                e.Row.Cells[7].Text = e.Row.Cells[14].Text;
            }
            else
            {
                e.Row.Cells[7].Text = coupon;
            }

            //扩展费
            if (e.Row.Cells[11].Text == "&nbsp;" || string.IsNullOrEmpty(e.Row.Cells[11].Text) || e.Row.Cells[11].Text == null)
            {
                e.Row.Cells[11].Text = "0";
            }

        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string baojiaid = e.CommandArgument.ToString();
        if (e.CommandName == "rollback")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                //回退前先判断该报价是否开案、如果已开案则不能回退
                string sql_anjianinfo2 = "select * from AnJianInFo2 where baojiaid='" + baojiaid + "'";
                SqlCommand com_anjianinfo2 = new SqlCommand(sql_anjianinfo2, con);
                SqlDataReader dr_anjianinfo2 = com_anjianinfo2.ExecuteReader();
                if (dr_anjianinfo2.Read())
                {
                    dr_anjianinfo2.Close();
                    Literal1.Text = "<script>alert('该合同已开案、在此不能回退')</script>";
                }
                else
                {
                    dr_anjianinfo2.Close();
                    //执行回退操作时、需要修改报价单的提交状态、审核状态、审核记录、回签状态以及删除报告生成的pdf记录和填单记录
                    string sql_state = @"update BaoJiaBiao set TiJiaoBiaozhi='否',TijiaoTime='1900-01-01',ShenPiBiaoZhi='否',HuiQianBiaoZhi='否',huiqiantime='1900-01-01'
                                where BaoJiaId = '" + baojiaid + "'";
                    string delete_approval = "delete Approval where bianhao='" + baojiaid + "'";
                    string delete_anjianxinxi2 = "delete Anjianxinxi2 where baojiaid='" + baojiaid + "'";
                    string delete_anjianxinxi3 = "delete Anjianxinxi3 where baojiaid='" + baojiaid + "'";
                    string delete_quotationSign = "delete QuotaionSign where quotationid='" + baojiaid + "'";
                    string delete_BaoJiaLink = "delete BaoJiaLink where baojiaid='" + baojiaid + "'";
                    string sql_rollback = "insert into [dbo].[BaojiaRollback] values('" + baojiaid + "','" + Session["Username"].ToString() + "','" + DateTime.Now + "')";
                    string sql_QuotaionSign = "select fileurl from QuotaionSign where quotationid='" + baojiaid + "'";//查询生成pdf文件的路径
                    SqlCommand com_quotaionsign = new SqlCommand(sql_QuotaionSign, con);
                    SqlDataReader dr = com_quotaionsign.ExecuteReader();
                    if (dr.Read())
                    {
                        //当存在生成pdf记录时则删除相对应的文件
                        string path = @"" + dr["fileurl"].ToString() + "";
                        File.Delete(path);
                    }
                    dr.Close();
                    SqlCommand com = new SqlCommand();
                    SqlTransaction Tran = con.BeginTransaction();
                    try
                    {
                        com.Connection = con;
                        com.Transaction = Tran;
                        com.CommandText = sql_state;
                        com.ExecuteNonQuery();//修改baojiabiao状态
                        com.CommandText = delete_approval;
                        com.ExecuteNonQuery();//删除审批记录
                        com.CommandText = delete_anjianxinxi2;
                        com.ExecuteNonQuery();//删除填单信息
                        com.CommandText = delete_anjianxinxi3;
                        com.ExecuteNonQuery();//删除填单信息
                        com.CommandText = delete_quotationSign;
                        com.ExecuteNonQuery();//删除生成pdf的记录
                        com.CommandText = delete_BaoJiaLink;
                        com.ExecuteNonQuery();//删除报价联系人表
                        com.CommandText = sql_rollback;
                        com.ExecuteNonQuery();//插入回退记录表数据
                        Tran.Commit();
                        Literal1.Text = "<script>alert('回退成功！')</script>";
                        Bind();
                    }
                    catch (Exception err)
                    {
                        Tran.Rollback();
                        throw new ApplicationException("事务操作出错，系统信息：" + err.Message);
                    }
                }
            }
        }
    }
}