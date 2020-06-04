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


public partial class CCSZJiaoZhun_htw_ChanpinManage : System.Web.UI.Page
{

    private int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            //TimeBind(int.Parse(DropDownList1.SelectedValue), TextBox1.Text);
            Bind(DropDownList1.SelectedValue);
        }

    }

    private void Bind(string selectedValue)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from Product2 where leibieid='" + selectedValue + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataView dv = ds.Tables[0].DefaultView;
            PagedDataSource pagedDataSource = new PagedDataSource();
            AspNetPager2.RecordCount = dv.Count;
            pagedDataSource.DataSource = dv;
            pagedDataSource.AllowPaging = true;
            pagedDataSource.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
            pagedDataSource.PageSize = AspNetPager2.PageSize;
            this.GridView1.DataSource = dv;
            this.GridView1.DataBind();
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        AspNetPager2.Visible = false;
        Select();
    }

    private void Select()
    {
        int id = int.Parse(this.DropDownList1.SelectedValue);
        string value = this.TextBox1.Text;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from Product2 where leibieid='" + id + "' and (chanpinname like '%" + value + "%' or neirong like '%" + value + "%' or biaozhun like '%" + value + "%')";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
    }

    protected void TimeBind(int a, string b)
    {

        AspNetPager2.Visible = false;
        int ChooseID = a;
        string ChooseValue = b;
        string sqlstr;
        switch (ChooseID)
        {
            case 0:
                sqlstr = "select top 10 * from Product2 order by id asc";
                break;
            case 1:
                sqlstr = "select * from Product2 where leibiename like '%" + ChooseValue + "%' order by id asc ";
                break;
            case 2:
                sqlstr = "select * from Product2 where chanpinname like '%" + ChooseValue + "%' order by id asc ";
                break;
            case 3:
                sqlstr = "select * from Product2 where neirong like '%" + ChooseValue + "%' order by id asc ";
                break;
            default:
                sqlstr = "select top 100 * from Product2 order by id asc";
                break;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);


        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind(this.DropDownList1.SelectedValue);
    }
}
