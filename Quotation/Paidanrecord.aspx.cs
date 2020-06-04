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
        string dutyname = DutyName();
        string bumen = Bumen();
        string condition = "1=1";
        if (dutyname.Trim() == "系统管理员" || (dutyname.Trim() == "总经理" && bumen == "总经办") || dutyname.Trim() == "董事长")
        {
            condition = "1=1";
        }
        else if (dutyname.Trim() == "客户经理")
        {
            condition = "(bj.responser in (select name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or bj.responser = '" + Session["Username"].ToString() + "')";
        }
        else if (dutyname.Trim() == "销售助理" || dutyname.Trim().Contains("客服"))
        {
            condition = "(bj.responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "') and bj.KeHuId not like 'D%' and (bj.BaoJiaId like 'FY%' or bj.BaoJiaId like 'LH%')  and LEN(bj.BaoJiaId)<13 ";
        }
        else if (dutyname.Trim() == "总经理")
        {
            condition = " bj.responser in (select UserName from UserInfo where department='" + bumen + "')";
        }
        else
        {
            //业务员
            condition = " bj.responser='" + Session["Username"].ToString() + "' and bj.KeHuId not like 'D%' and (bj.BaoJiaId like 'FY%' or bj.BaoJiaId like 'LH%')  and LEN(bj.BaoJiaId)<13 ";
        }

        string sql1 = "select an.id as id ,an.baojiaid as bjid,an.TaskNo as rwid,an.weituo as weituo,bj.responser as baojiaren,bj.Filltime as baojiariqi,an.filltime as tiandanriqi,an.fillname as tiandanren from Anjianxinxi2 an,BaoJiaBiao bj where an.baojiaid=bj.BaoJiaId and " + condition + " order by an.filltime desc";
        SqlDataAdapter da = new SqlDataAdapter(sql1, con);
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
        string dutyname = DutyName();
        string bumen = Bumen();
        string condition = "";
        if (DropDownList1.SelectedValue != "kehuname")
        {
            if (dutyname.Trim() == "系统管理员" || (dutyname.Trim() == "总经理" && bumen == "总经办") || dutyname.Trim() == "董事长")
            {
                condition = "1=1 and " + ChooseID + " like '%" + value + "%'";
            }
            else if (dutyname.Trim() == "客户经理")
            {
                condition = "(bj.responser in (select name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or bj.responser = '" + Session["Username"].ToString() + "') and " + ChooseID + " like '%" + value + "%'";
            }
            else if (dutyname.Trim() == "总经理")
            {
                condition = "bj.responser in (select UserName from UserInfo where department='" + bumen + "') and " + ChooseID + " like '%" + value + "%'";
            }
            else if (dutyname.Trim() == "销售助理" || dutyname.Trim().Contains("客服"))
            {
                condition = "(bj.responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "') and bj.KeHuId not like 'D%' and (bj.BaoJiaId like 'FY%' or bj.BaoJiaId like 'LH%')  and LEN(bj.BaoJiaId)<13  and " + ChooseID + " like '%" + value + "%'";
            }
            else
            {
                //业务员
                condition = "bj.responser='" + Session["Username"].ToString() + "' and bj.KeHuId not like 'D%' and (bj.BaoJiaId like 'FY%' or bj.BaoJiaId like 'LH%')  and LEN(bj.BaoJiaId)<13 and " + ChooseID + " like '%" + value + "%' ";
            }
        }
        else
        {
            if (dutyname.Trim() == "系统管理员")
            {
                condition = "1=1 and bj.KeHuId in (select top 1 KeHuId from customer where customname like '%" + value + "%')";
            }
            else if (dutyname.Trim() == "客户经理")
            {
                condition = "(bj.responser in (select name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or bj.responser = '" + Session["Username"].ToString() + "') and bj.KeHuId in (select top 1 KeHuId from customer where customname like '%" + value + "%')";
            }
            else if (dutyname.Trim() == "总经理")
            {
                condition = " bj.responser in (select UserName from UserInfo where department='" + bumen + "') and bj.KeHuId in (select top 1 KeHuId from customer where customname like '%" + value + "%')";
            }
            else if (dutyname.Trim() == "销售助理" || dutyname.Trim().Contains("客服"))
            {
                condition = "(bj.responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "') and bj.KeHuId not like 'D%' and (bj.BaoJiaId like 'FY%' or bj.BaoJiaId like 'LH%')  and LEN(bj.BaoJiaId)<13  and bj.KeHuId in (select top 1 KeHuId from customer where customname like '%" + value + "%')";
            }
            else
            {
                //业务员
                condition = " bj.responser='" + Session["Username"].ToString() + "' and bj.KeHuId not like 'D%' and (bj.BaoJiaId like 'FY%' or bj.BaoJiaId like 'LH%')  and LEN(bj.BaoJiaId)<13 and bj.KeHuId in (select top 1 KeHuId from customer where customname like '%" + value + "%')";
            }
        }

        string sql1 = "select an.id as id ,an.baojiaid as bjid,an.TaskNo as rwid,an.weituo as weituo,bj.responser as baojiaren,bj.Filltime as baojiariqi,an.filltime as tiandanriqi,an.fillname as tiandanren from Anjianxinxi2 an,BaoJiaBiao bj where an.baojiaid=bj.BaoJiaId and " + condition + " order by an.filltime desc";

        SqlDataAdapter da = new SqlDataAdapter(sql1, con);
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
        string id = e.CommandArgument.ToString();
        if (name == "yulan")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql2 = "select * from anjianxinxi2 where id='" + id + "'";
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