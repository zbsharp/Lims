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

public partial class Quotation_ZShenpi : System.Web.UI.Page
{
    protected string shwhere = "";
    private int _i = 0;
    private string condition = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        limit("总经理审批");
        string positon = Position();
        string bumen = BuMen();
        string area = Area();
        if (positon == "系统管理员" || (positon == "总经理" && area == "FY") || positon == "董事长")
        {
            condition = "and 1=1";
        }
        else
        {
            condition = "and responser in (select UserName from UserInfo where departmentname='" + bumen + "')";
        }
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
        string sql = " select *,(select top 1 fillname from Approval where bianhao=baojiabiao.baojiaid and result ='通过'  order by id desc) as fname1,(select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname  from baojiabiao  where ShenPiBiaoZhi = '一级通过'  and TiJiaoBiaozhi = '是' and HuiQianBiaoZhi='否' and Other = 'other' " + condition + " and isdelete='否' order by id desc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        DataTable table = ds.Tables[0];
        DataView dv = table.DefaultView;
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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseID = DropDownList1.SelectedValue.Trim();
        string value = TextBox1.Text.Trim();
        string sql = "";

        if (ChooseID == "kehuname")
        {
            sql = " select *,(select top 1 fillname from Approval where bianhao=baojiabiao.baojiaid and result ='通过'  order by id desc) as fname1,(select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname  from baojiabiao  where KeHuId in (select Kehuid from Customer where CustomName like '%" + value + "%') and ShenPiBiaoZhi = '一级通过'  and TiJiaoBiaozhi = '是' and HuiQianBiaoZhi='否' and Other = 'other' and isdelete='否' order by id desc";
        }
        else
        {
            sql = " select *,(select top 1 fillname from Approval where bianhao=baojiabiao.baojiaid and result ='通过'  order by id desc) as fname1,(select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname  from baojiabiao  where  " + ChooseID + " like '%" + value + "%' and  ShenPiBiaoZhi = '一级通过'  and TiJiaoBiaozhi = '是' and HuiQianBiaoZhi='否' and Other = 'other' and isdelete='否' order by id desc";
        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        DataTable table = ds.Tables[0];
        DataView dv = table.DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager1.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager1.PageSize;
        GridView1.DataSource = pds;
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

            string coupon = e.Row.Cells[17].Text;
            if (coupon == "0.00" || coupon == "&nbsp;" || coupon == "NULL" || coupon == "null")
            {
                e.Row.Cells[4].Text = e.Row.Cells[16].Text;
            }
            else
            {
                e.Row.Cells[4].Text = coupon;
            }

            //外包比列
            string epiboly_Price = e.Row.Cells[18].Text.ToString();
            if (epiboly_Price == "&nbsp;" || string.IsNullOrEmpty(epiboly_Price) || epiboly_Price == "0.00")
            {
                e.Row.Cells[5].Text = "0%";
            }
            else
            {
                string price = e.Row.Cells[4].Text;
                if (!string.IsNullOrEmpty(price) && price != "0.00" && price != "&nbsp;")
                {
                    decimal waibao = Convert.ToDecimal(epiboly_Price) / Convert.ToDecimal(price);
                    e.Row.Cells[5].Text = (waibao * 100).ToString("f2") + "%";
                }
            }

            //扩展费
            if (string.IsNullOrEmpty(e.Row.Cells[9].Text) || e.Row.Cells[9].Text == null || e.Row.Cells[9].Text == "&nbsp;")
            {
                e.Row.Cells[9].Text = "0";
            }


        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string baojiaid = e.CommandArgument.ToString();
        if (e.CommandName == "cancel1")
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql3 = "update baojiabiao set tijiaobiaozhi='否',tijiaotime='" + DateTime.Now + "' where  baojiaid ='" + baojiaid + "'";
            SqlCommand cmd = new SqlCommand(sql3, con);
            cmd.ExecuteNonQuery();

            string kehuid = "";

            string sql2 = "select kehuid from baojiabiao where baojiaid='" + baojiaid + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                kehuid = dr2["kehuid"].ToString();

            }
            dr2.Close();
            con.Close();

            ld.Text = "<script>alert('退回成功!');</script>";


            Bind();
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
    /// <summary>
    /// 查询当前登录进来人的部门
    /// </summary>
    /// <returns></returns>
    private string BuMen()
    {
        string name = "";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select department from UserInfo where UserName='" + Session["Username"].ToString() + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                name = dr["department"].ToString();
            }
            return name;
        }
    }
    protected string Position()
    {
        string position = "";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sqldd = "select dutyname from UserInfo where UserName='" + Session["Username"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sqldd, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                position = dr["dutyname"].ToString();
            }
            dr.Close();
        }
        return position;
    }
    /// <summary>
    /// 获取当前登录进来人的地区
    /// </summary>
    protected string Area()
    {
        using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string homelocation = "";
            con1.Open();
            string sql_dutyname = string.Format("select homelocation from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con1);
            SqlDataReader dr = cmdstate.ExecuteReader();
            if (dr.Read())
            {
                homelocation = dr["homelocation"].ToString();
            }
            dr.Close();
            return homelocation;
        }
    }
}