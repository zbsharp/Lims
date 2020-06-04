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


public partial class SysManage_NoticeManage : System.Web.UI.Page
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
            txFDate.Value = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");

            //本月最后一天
            txTDate.Value = dt2.AddDays(-dt.Day).ToString("yyyy-MM-dd");
            Bind();
        }


    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from notice  ORDER BY id asc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string begintime;
        string endtime;
        int ChooseNo = int.Parse(DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;
        begintime = txFDate.Value;
        endtime = txTDate.Value;


        TimeBind(begintime, endtime, ChooseNo, ChooseValue);

    }

    protected void TimeBind(string a, string b, int c, string d)
    {
        string ds1 = a;
        string ds2 = b;
        int ChooseID = c;
        string ChooseValue = d;

        string sqlstr;
        string sqlx;
        switch (ChooseID)
        {
            case 0:
                sqlstr = "select * from notice where  time between '" + ds1 + "' and '" + ds2 + "' order by id asc";
                break;
            case 1:
                sqlstr = "select * from notice where  name = '" + ChooseValue + "' and time between '" + ds1 + "' and '" + ds2 + "' order by id asc";
                break;
            default:
                sqlstr = "select * from notice where  time between '" + ds1 + "' and '" + ds2 + "' order by id asc";
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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "delete from notice where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();

        Bind();
    }
}