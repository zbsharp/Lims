using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Web.Services;
using Common;
using System.IO;
using System.Text;
using System.Drawing;


public partial class Quotation_QuotationHuiqian : System.Web.UI.Page
{
    protected string shwhere = "";
    private int _i = 0;
    public string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            id = Request.QueryString["id"].ToString();
            DataBind();
        }
        shwhere = "shenpibiaozhi ='二级通过'";
        if (!IsPostBack)
        {
            Bind();
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql;
        string dutyname = DutyName();
        string bumen = Bumen();
        if (dutyname.Trim() == "系统管理员" || (dutyname.Trim() == "总经理" && dutyname.Trim() == "总经办") || dutyname.Trim() == "董事长")
        {
            sql = string.Format(@"select*,
                                (select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname 
                               from baojiabiao where {0} and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc", shwhere);
        }
        else if (dutyname.Trim() == "客户经理")
        {
            sql = @"select*,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname from baojiabiao 
                    where (responser in (select name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or responser = '" + Session["Username"].ToString() + "') and " + shwhere + "   and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc";
        }
        else if (dutyname.Trim() == "销售助理"||dutyname.Trim().Contains("客服"))
        {
            sql = @"select*,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname from baojiabiao 
                where (responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "') and " + shwhere + "  and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc";
        }
        else if (dutyname.Trim() == "总经理")
        {
            //标源
            sql = @"select*,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname from baojiabiao 
                    where responser in (select  UserName from UserInfo where department = '" + bumen + "') and " + shwhere + "   and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc";
        }
        else
        {
            //业务员
            sql = @"select*,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname from baojiabiao 
                    where responser = '" + Session["Username"].ToString() + "' and " + shwhere + "  and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc";
        }

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        AspNetPager1.Visible = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseID = DropDownList1.SelectedValue.Trim();
        string value = TextBox1.Text.Trim();
        string sql = "";

        ////////////////////****************2019-7-19修改
        string dutyname = DutyName();
        string bumen = Bumen();
        if (DropDownList1.SelectedValue != "kehuname")
        {
            if (dutyname.Trim() == "系统管理员" || (dutyname.Trim() == "总经理" && dutyname.Trim() == "总经办") || dutyname.Trim() == "董事长")
            {
                sql = string.Format(@"select *,
                (select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname
                 from baojiabiao where  {0} like '%{1}%' and  {2}  and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc", ChooseID, value, shwhere);
            }
            else if (dutyname.Trim() == "客户经理")
            {
                sql = @"select*,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname from baojiabiao 
                    where (responser in (select name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or responser = '" + Session["Username"].ToString() + "') and " + shwhere + " and " + ChooseID + " like '%" + value + "%'  and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc";
            }
            else if (dutyname.Trim() == "销售助理"||dutyname.Trim().Contains("客服"))
            {
                sql = @"select*,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname from baojiabiao 
                where (responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "') and " + shwhere + " and " + ChooseID + " like '%" + value + "%'  and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc";
            }
            else if (dutyname.Trim() == "总经理")
            {
                //标源
                sql = @"select*,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname from baojiabiao 
                    where responser in (select top 1000 UserName from UserInfo where department = '" + bumen + "') and " + shwhere + " and " + ChooseID + " like '%" + value + "%'  and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc";
            }
            else
            {
                //业务员
                sql = @"select*,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname from baojiabiao 
                    where responser = '" + Session["Username"].ToString() + "' and " + shwhere + " and " + ChooseID + " like '%" + value + "%'  and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc";
            }
        }
        else
        {
            if (dutyname.Trim() == "系统管理员" || (dutyname.Trim() == "总经理" && dutyname.Trim() == "总经办") || dutyname.Trim() == "董事长")
            {
                sql = string.Format(@"select *,
                                   (select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname
                                    from baojiabiao 
                                    where kehuid in (select KeHuId from customer  where customname like '%{0}%' )
                                     and  {1}  order by Filltime desc", value, shwhere);
            }
            else if (dutyname.Trim() == "客户经理" && bumen != "销售大客户部")
            {
                sql = @"select*,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname from baojiabiao 
                    where responser in (select top 1000 UserName from UserInfo where department = '" + bumen + "') and " + shwhere + " and  kehuid in (select top 1000 KeHuId from customer  where customname like '%" + value + "%' )  and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc";
            }
            else if (dutyname.Trim() == "销售助理"||dutyname.Trim().Contains("客服"))
            {
                sql = @"select*,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname from baojiabiao 
                where (responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "') and " + shwhere + " and kehuid in (select top 1000 KeHuId from customer  where customname like '%" + value + "%' )  and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc";
            }
            else if (dutyname.Trim() == "总经理")
            {
                sql = @"select*,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname from baojiabiao 
                    where responser in (select top 1000 UserName from UserInfo where department = '" + bumen + "') and " + shwhere + " and  kehuid in (select top 1000 KeHuId from customer  where customname like '%" + value + "%' )  and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc";
            }
            else
            {
                //业务员
                sql = @"select*,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname from baojiabiao 
                    where responser = '" + Session["Username"].ToString() + "' and " + shwhere + " and kehuid in (select top 1000 KeHuId from customer  where customname like '%" + value + "%' )  and kehuid not like 'D%' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by Filltime desc";
            }

        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");


            string coupon = e.Row.Cells[13].Text.ToString();
            if (coupon == "0.00" || coupon == "&nbsp;" || coupon == "NULL" || coupon == "null")
            {
                e.Row.Cells[4].Text = e.Row.Cells[14].Text.ToString();
            }
            else
            {
                e.Row.Cells[4].Text = coupon;
            }

        }
    }
    /// <summary>
    /// 返回登录进来人的职位
    /// </summary>
    /// <returns></returns>
    protected string DutyName()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string sql_dutyname = string.Format("select dutyname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlDataAdapter da_dutyname = new SqlDataAdapter(sql_dutyname, con);
            DataSet ds_dutyname = new DataSet();
            da_dutyname.Fill(ds_dutyname);
            string dutyname = ds_dutyname.Tables[0].Rows[0]["dutyname"].ToString();
            return dutyname;
        }
    }
    protected string Bumen()
    {
        string dutyname = "";//职位
        string dn = "";//部门
                       //获取当前登录进来的人的职位和部门
        using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con1.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con1);
            SqlDataReader dr = cmdstate.ExecuteReader();
            if (dr.Read())
            {
                dn = dr["departmentname"].ToString();
                dutyname = dr["dutyname"].ToString();
            }
            dr.Close();
        }
        return dn;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string name = e.CommandName.ToString();
        string baojiaid = e.CommandArgument.ToString();
        if (name == "yulan")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql2 = "select top 1 bianhao from anjianxinxi2 where baojiaid='" + baojiaid + "'  and biaozhi='是'";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    string bianhao = dr2["bianhao"].ToString();
                    dr2.Close();
                    Response.Write("<script>window.open('Tdpreview.aspx?id=" + bianhao + "','_blank')</script>");
                    Response.Write("<script>document.location=document.location;</script>");//防止页面样式会变  Response.write输出脚本代码到顶部，会打乱了文档模型
                }
                else
                {
                    dr2.Close();
                    Literal1.Text = "<script>alert('该报价还未填过单')</script>";
                }
            }
        }
    }
}