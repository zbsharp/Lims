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

public partial class Report_BaoGaoListShenHe : System.Web.UI.Page
{
    protected string shwhere = "1=1";
    private int _i = 0;
    const string vsKey = "searchbaogao2";
    protected void Page_Load(object sender, EventArgs e)
    {


        shwhere = "state ='打印完成' and statebumen1='合格' and (statebumen2='' or  statebumen2='在审') ";



        if (!IsPostBack)
        {
            txFDate.Value = DateTime.Now.AddDays(-90).ToShortDateString();
            txTDate.Value = DateTime.Now.ToShortDateString();
            Bind();
            
        }
    }
    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";


        sql = "select *,(select shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao from BaoGao2 where " + shwhere + "";
        
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
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
    protected void Button2_Click(object sender, EventArgs e)
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;

        string sqlstr;
        if (DropDownList1.SelectedValue == "0")
        {
            sqlstr = "select  *,(select shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao from BaoGao2 where " + shwhere + " and  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' order by id desc";
        }
        else
        {
            sqlstr = " select  *,(select shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao from BaoGao2  where " + shwhere + " and  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and  " + ChooseNo + " like  '%" + ChooseValue + "%'";
        }
        AspNetPager2.Visible = false;

        SqlDataAdapter ad = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        con.Dispose();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    


    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");

            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

            string f = e.Row.Cells[1].Text;
            searchwhere sxb1 = new searchwhere();
            e.Row.Cells[5].Text = sxb1.BaoGaoShiJian(f).ToString();

        }
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text.Trim().ToString().Substring(0, 4) == "1900")
                {
                    e.Row.Cells[4].Text = "";
                }
               
                if (e.Row.Cells[3].Text.Trim().ToString().Substring(0, 4) == "1900")
                {
                    e.Row.Cells[3].Text = "";
                }
            }
        }
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
}