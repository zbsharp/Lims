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
using System.Data.SqlClient;
using Common;
using DBBLL;
using DBTable;
using System.Drawing;

public partial class ZiYuan_Manage3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["username"] = "ccic";

        if (!IsPostBack)
        {
            txFDate.Value = "2010-01-01";
            txTDate.Value = DateTime.Now.AddDays(1).ToShortDateString();

            Bind();
        }
    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *  from GoodsInfo  order by id desc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager2.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();


    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "chakan")
        {
            Response.Redirect("GoodsInfoUpdate.aspx?bianhao=" + sid);
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        string begintime;
        string endtime;
        int ChooseNo = int.Parse(DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;
        begintime = txFDate.Value;
        endtime = Convert.ToDateTime(txTDate.Value).AddDays(1).ToShortDateString();

        TimeBind(begintime, endtime, ChooseNo, ChooseValue);
    }

    protected void TimeBind(string a, string b, int c, string d)
    {
        string ds1 = a;
        string ds2 = b;
        int ChooseID = c;
        string ChooseValue = d;
        string sqlstr;
        switch (ChooseID)
        {
            case 0:
                sqlstr = "select * from GoodsInfo where filltime between '" + ds1 + "' and '" + ds2 + "'  order by id desc  ";
                break;
            case 1:
                sqlstr = "select * from GoodsInfo where filltime between '" + ds1 + "' and '" + ds2 + "' and jq_name like '%" + ChooseValue + "%' order by id desc  ";
                break;
            case 2:
                sqlstr = "select * from GoodsInfo where filltime between '" + ds1 + "' and '" + ds2 + "' and jq_id like '%" + ChooseValue + "%' order by id desc  ";
                break;
            case 3:
                sqlstr = "select * from GoodsInfo where filltime between '" + ds1 + "' and '" + ds2 + "' and sqbumen like '%" + ChooseValue + "%' order by id desc  ";
                break;
            case 4:
                sqlstr = "select * from GoodsInfo where filltime between '" + ds1 + "' and '" + ds2 + "' and sqby like '%" + ChooseValue + "%' order by id desc  ";
                break;
            case 5:
                sqlstr = "select * from GoodsInfo where filltime between '" + ds1 + "' and '" + ds2 + "' and testflag like '%" + ChooseValue + "%' order by id desc  ";
                break;
            case 6:
                sqlstr = "select * from GoodsInfo where filltime between '" + ds1 + "' and '" + ds2 + "' and state1 like '%" + ChooseValue + "%' order by id desc  ";
                break;
            case 7:
                sqlstr = "select * from GoodsInfo where filltime between '" + ds1 + "' and '" + ds2 + "' and bianhao like '%" + ChooseValue + "%' order by id desc  ";
                break;
            default:
                sqlstr = "select * from GoodsInfo where filltime between '" + ds1 + "' and '" + ds2 + "' order by id desc  ";
                break;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
     
        AspNetPager2.Visible = false;
    }
}