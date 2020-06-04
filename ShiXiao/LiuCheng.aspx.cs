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
public partial class ShiXiao_LiuCheng : System.Web.UI.Page
{
    protected string shwhere = "1=1";
    private int _i = 0;
    const string vsKey = "searchbaogao";
    int biaozhuntian = 3;

    Hashtable a = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {


        shwhere = "(1=1)";



        if (!IsPostBack)
        {

            Bind(string.Empty);
        }
    }
    public void Bind(string sWhere)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";
        if (sWhere == "" || sWhere == null)
        {



            sql = "select *,(select top 1 beizhu from anjianxinxi where leibie='工程' and xiangmuid=anjianxinxi2.taskno) as queren,(select max(convert(datetime,beizhu4)) from anjianxinxi where (xiangmuid=anjianxinxi2.bianhao) and leibie='资料' and beizhu !='是') as kaichu,(select top 1 customname from customer where kehuid =anjianxinxi2.kehuid) as kehuname from anjianxinxi2 where " + shwhere + " order by id desc";
        }
        else
        {
            sql = "select *,(select top 1 beizhu from anjianxinxi where leibie='工程' and xiangmuid=anjianxinxi2.taskno) as queren,(select max(convert(datetime,beizhu4)) from anjianxinxi where ( xiangmuid=anjianxinxi2.bianhao) and leibie='资料' and beizhu !='是') as kaichu,(select top 1 customname from customer where kehuid =anjianxinxi2.kehuid) as kehuname from anjianxinxi2 where " + shwhere + " and " + sWhere + " order by id desc";
        }
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager2.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {


        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;

        string sqlstr;

        sqlstr = "" + ChooseNo + " like  '%" + ChooseValue + "%' and " + shwhere + " order by id desc  ";
        AspNetPager2.CurrentPageIndex = 1;

        Session[vsKey] = sqlstr;
        Bind(sqlstr);


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

            string f = e.Row.Cells[0].Text.ToString();
            e.Row.Cells[2].Text = tian(f).ToString();


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select beizhu4,beizhu3 from anjianxinxi where xiangmuid='" + f + "' and  DATEDIFF(day,convert(datetime,beizhu4),'" + DateTime.Now + "')>'" + biaozhuntian + "' and beizhu !='是'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                e.Row.Cells[6].Text = "暂停";
            }
            else
            {
                e.Row.Cells[6].Text = "进行中";
            }

            con.Close();
        }
        if (e.Row.RowIndex >= 0)
        {

        }
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind((string)Session[vsKey]);
    }
    //beizhu4 是首提日期
    protected int tian(string rwid)
    {
        int a = 0;
        int b = 0;
        int diyi = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select beizhu4,beizhu3 from anjianxinxi where xiangmuid='" + rwid + "' order by convert(datetime,beizhu4) asc";

        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        DataTable dt = ds.Tables[0];

        int xiuxi = 0;
        DateTime qq = DateTime.Now;

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            if (i == 0)
            {
                qq = Convert.ToDateTime(dt.Rows[i]["beizhu3"]);
                diyi = tian1(Convert.ToDateTime(dt.Rows[i]["beizhu3"]), Convert.ToDateTime(dt.Rows[i]["beizhu4"]));

            }
            else
            {
                DateTime time1 = Convert.ToDateTime(dt.Rows[i]["beizhu3"]);
                DateTime time2 = Convert.ToDateTime(dt.Rows[i]["beizhu4"]);
                DateTime shangcitime = Convert.ToDateTime(dt.Rows[i - 1]["beizhu3"]);
                if (time2 < qq)
                {
                    if (time1 <= qq)
                    {

                    }
                    else if (time1 > qq)
                    {
                        time2 = qq;

                        diyi = diyi + tian1(time1, time2) - 1;
                        //用time2,time1去计算隔的天数（上次的提交日期赋予本次首提日期，再去和本次的提交日期计算）
                    }
                }
                else if (time2 >= qq)
                {
                    diyi = diyi + tian1(time1, time2);
                }

                if (time1 <= qq)
                {

                }
                else
                {
                    qq = time1;
                }



            }



        }
        if (diyi == 0)
        {
            diyi = 1;
        }

        if (qq == DateTime.Now)
        {
            diyi = 0;
        }

        return diyi;
    }

    protected int tian1(DateTime d1, DateTime d2)
    {
        int ff = 0;
        int b = 0;
        int xiuxi = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        DateTime time1 = d1;
        DateTime time2 = d2;
        TimeSpan ts = time1.Date - time2.Date;
        b = ts.Days;

        for (int j = 0; j < b + 1; j++)
        {
            DateTime time3 = time2.AddDays(j);

            string sql2 = "select hdate from tb_Holiday where convert(varchar,hdate,101)='" + time3.ToShortDateString() + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                if (a.Contains(time3)) { }
                else
                {
                    xiuxi = xiuxi + 1;
                    a.Add(time3, time3);
                }
            }
            dr2.Close();
        }

        con.Close();

        int re = 0;

        ff = b - xiuxi;
        if (ff == 0)
        {
            re = 0;
        }
        else
        {
            int biao = ff + 1;


            if (biao > biaozhuntian || biao == biaozhuntian)
            {
                re = biaozhuntian;
            }
            else if (biao < biaozhuntian)
            {
                re = biao;
            }
        }

        return re;
    }
}