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
using System.Text;
using System.IO;
using Common;

public partial class SysManage_NoticeList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now;
            int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            int dayspan = (-1) * weeknow + 1;
            DateTime dt2 = dt.AddMonths(1);
            //本月第一天
            txFDate.Value = dt.AddYears(-1).AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");

            //本月最后一天
            txTDate.Value = dt2.AddDays(-dt.Day).ToString("yyyy-MM-dd");

            Bind(txFDate.Value, txTDate.Value, int.Parse(DropDownList1.SelectedValue), TextBox1.Text);
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "chakan")
        {
            Response.Redirect("NoticeSee.aspx?id=" + sid);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string begintime;
        string endtime;
        string onlytime;
        begintime = txFDate.Value;
        endtime = txTDate.Value;
        int ChooseNo = int.Parse(DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;

        Bind(begintime, endtime, ChooseNo, ChooseValue);
    }

    protected void Bind(string a, string b, int c, string d)
    {
        string ds1 = a;
        string ds2 = b;
        int ChooseID = c;
        string ChooseValue = d;

        string sqlstr;
        switch (ChooseID)
        {
            case 0:
                sqlstr = "select * from NompanyManage where Signdate between '" + ds1 + "' and '" + ds2 + "' order by id asc  ";
                break;
            case 1:
                sqlstr = "select *  from NompanyManage where Signdate between '" + ds1 + "' and '" + ds2 + "'  order by id asc  ";
                break;
            case 2:
                sqlstr = "select *  from NompanyManage where Signdate between '" + ds1 + "' and '" + ds2 + "' and Title like'%" + ChooseValue + "%' order by id asc  ";
                break;
            default:
                sqlstr = "select *  from NompanyManage where Signdate between '" + ds1 + "' and '" + ds2 + "' order by id asc  ";
                break;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
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
        con.Close();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        string begintime;
        string endtime;
        string onlytime;
        begintime = txFDate.Value;
        endtime = txTDate.Value;
        int ChooseNo = int.Parse(DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;

        Bind(begintime, endtime, ChooseNo, ChooseValue);
    }
}