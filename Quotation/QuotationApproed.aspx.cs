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

public partial class Quotation_QuotationApproed : System.Web.UI.Page
{
    //protected string shwhere = "";
    private int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable table = Department();
            string bumen = table.Rows[0]["departmentname"].ToString();//部门
            string dutyname = table.Rows[0]["dutyname"].ToString();//职位 
            if (dutyname == "系统管理员" || dutyname == "董事长" || bumen == "财务部" || (dutyname == "总经理" && bumen == "总经办"))
            {
                Bind();
            }
            else
            {
                Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
            }
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
        string sql = "select *,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname  from baojiabiao where shenpibiaozhi = '二级通过' and huiqianbiaozhi != '是' order by id desc";

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
        ld.Text = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseID = DropDownList1.SelectedValue.Trim();
        string value = TextBox1.Text.Trim();
        string sql = "";

        string huiqian = DropDownList2.SelectedValue;
        string shwere = " and 1=1";
        if (string.IsNullOrEmpty(huiqian))
        {
        }
        else if (huiqian == "是")
        {
            shwere = " and huiqianbiaozhi = '是'";
        }
        else
        {
            shwere = " and huiqianbiaozhi != '是'";
        }

        if (ChooseID == "kehuname")
        {
            sql = "select *,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname  from baojiabiao where  KeHuId in (select Kehuid from Customer where CustomName like '%" + value + "%') and shenpibiaozhi = '二级通过' " + shwere + " order by id desc";
        }
        else if (ChooseID == "全部")
        {
            sql = "select top 200 *,(select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname  from baojiabiao where shenpibiaozhi = '二级通过'  " + shwere + "  order by id desc";
        }
        else
        {
            sql = "select *,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname  from baojiabiao where " + ChooseID + " like '%" + value + "%'  and shenpibiaozhi = '二级通过' " + shwere + " order by id desc";
        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string baojiaid = e.CommandArgument.ToString();
        if (e.CommandName == "cancel1")
        {
            string financeremork = TextBox2.Text.Replace('\'', ' ');
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql3 = "update baojiabiao set huiqianbiaozhi='是',huiqiantime='" + DateTime.Now + "',financebeizhu='" + financeremork + "' where  baojiaid ='" + baojiaid + "'";
            SqlCommand cmd = new SqlCommand(sql3, con);
            cmd.ExecuteNonQuery();
            con.Close();

            ld.Text = "<script>alert('确认成功!');</script>";

            Bind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

            string coupon = e.Row.Cells[15].Text.ToString();
            if (coupon == "0.00" || coupon == "&nbsp;" || coupon == "NULL" || coupon == "null")
            {
                e.Row.Cells[4].Text = e.Row.Cells[16].Text.ToString();
            }
            else
            {
                e.Row.Cells[4].Text = coupon;
            }

            //扩展费
            if (e.Row.Cells[9].Text == "&nbsp;" || string.IsNullOrEmpty(e.Row.Cells[9].Text) || e.Row.Cells[9].Text == null)
            {
                e.Row.Cells[9].Text = "0";
            }


        }
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