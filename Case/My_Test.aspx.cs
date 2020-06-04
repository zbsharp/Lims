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
public partial class Case_My_Test : System.Web.UI.Page
{
    protected string shwhere = "";
    protected string shwhere1 = "";
    protected string shwhere11 = "";
    protected string shwhere2 = "";
    protected string shwhere3 = "";
    private int _i = 0;
    const string vsKey = "jinxingzhong";
    int tichu = 2;//提出日期表示从下达日起允许提出的日期，比如11.10下达，则计算暂停和超期就从11.2号开始
    int kehu = 3;
    int chaoqitian = 2;
    protected string str = "";
    Hashtable a = new Hashtable();
    protected bool xian = false;
    protected string pai = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }
    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string position = Position();
        string bumen = BuMen();
       
        if (position.Trim() == "系统管理员" || position.Trim() == "总经理" || position.Trim() == "董事长")
        {
            shwhere = "where 1=1";
        }
        else if (position.Trim() == "工程经理")
        {
            shwhere = " where bumen='" + bumen + "'";
        }
        else if (position.Trim() == "工程师")
        {
            shwhere = " where renwuid in (select bianhao from ZhuJianEngineer where name='" + Session["Username"].ToString() + "')";
        }
        else
        {
            shwhere = "where ceshiyuan='" + Session["Username"].ToString() + "'";
        }
        string sql = @"select *,(select top 1 name from ZhuJianEngineer where ZhuJianEngineer.bianhao=AnJianInFo.taskid) as name1,(AnJianInFo.bumen) as bumen1,
                        (select top 1 yaoqiuwanchengriqi from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as yaoqiuwanchengriqi,
                        (select top 1 weituodanwei from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as weituodanwei 
                        from AnJianInFo where taskid in (select renwuid from DaiFenTest " + shwhere + ")  and taskid not like 'D%'";
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
        Set_Color();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

            //e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 5);
            //MyExcutSql ext = new MyExcutSql();
            //e.Row.Cells[5].Text = ext.Eng(e.Row.Cells[0].Text);
            //e.Row.Cells[4].Text = ext.EngBumen(e.Row.Cells[0].Text);
            // e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 5);

            string sid = e.Row.Cells[1].ToString();

            string dd = Session["UserName"].ToString();
            //if (e.Row.Cells[12].Text == "暂停" || e.Row.Cells[12].Text == "中止")
            //{
            //    e.Row.ForeColor = Color.Red;
            //}
        }
    }

    /// <summary>
    /// 获取数组中最大的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Array"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static int getmax(int[] arr)
    {
        int max = arr[0];
        for (int x = 0; x < arr.Length; x++)
        {
            if (arr[x] > max)
                max = arr[x];
        }
        return max;
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
    public string SubStr(string sString, int nLeng)
    {
        if (sString.Length <= nLeng)
        {
            return sString;
        }
        string sNewStr = sString.Substring(0, nLeng);

        return sNewStr;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    /// <summary>
    /// 设置加急行的颜色
    /// </summary>
    protected void Set_Color()
    {
        //************2019-8-16修改  设置GridView的颜色
        int columns = this.GridView1.Columns.Count;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            string a = GridView1.Rows[i].Cells[columns - 1].Text.ToString().Trim();
            if (GridView1.Rows[i].Cells[columns - 1].Text.ToString().Trim() != "Regular 正常")
            {
                //GridView1.Rows[i].BackColor = System.Drawing.Color.Red;//背景颜色
                GridView1.Rows[i].ForeColor = Color.Red;//字体颜色
            }
        }
    }

    protected void btn_select_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string position = Position();
            string bumen = BuMen();
            if (position.Trim() == "系统管理员" || position.Trim() == "总经理" || position.Trim() == "董事长")
            {
                shwhere = "where 1=1";
            }
            else if (position.Trim() == "工程经理")
            {
                shwhere = " where bumen='" + bumen + "'";
            }
            else if (position.Trim() == "工程师")
            {
                shwhere = " where renwuid in (select bianhao from ZhuJianEngineer where name='" + Session["Username"].ToString() + "')";
            }
            else
            {
                shwhere = "where ceshiyuan='" + Session["Username"].ToString() + "'";
            }
            string sql = @"select *,(select top 1 name from ZhuJianEngineer where ZhuJianEngineer.bianhao=AnJianInFo.taskid) as name1,(AnJianInFo.bumen) as bumen1,
                        (select top 1 yaoqiuwanchengriqi from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as yaoqiuwanchengriqi,
                        (select top 1 weituodanwei from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as weituodanwei 
                        from AnJianInFo where taskid in (select renwuid from DaiFenTest " + shwhere + ") and taskid='" + txt_renwuid.Text.Trim() + "'  and taskid not like 'D%'";
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
            Set_Color();
        }
    }
    /// <summary>
    /// 职位
    /// </summary>
    /// <returns></returns>
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
}