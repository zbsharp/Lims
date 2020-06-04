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
public partial class ShiXiao_YiFenYiShou : System.Web.UI.Page
{
    protected string shwhere = "";
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
    string dutyname = "";//职位
    string dn = "";//部门
    protected void Page_Load(object sender, EventArgs e)
    {
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
        if (dutyname == "测试员")
        {
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
        //1.管理员查看全部   2.工程师只看到分配给他的任务   3.工程经理看到自己部门的任务   4.工程师不能看到分派工程师的那一列
        if (dutyname == "系统管理员" || dutyname == "总经理" || dutyname == "董事长")
        {
            shwhere = "1=1";
        }
        else if (dutyname == "工程经理")
        {
            if (dn=="龙华安规部")
            {
                shwhere = "(ZhuJianEngineer.bumen='" + dn + "' or ZhuJianEngineer.bumen='标源安规部')";
            }
            else if(dn=="龙华EMC部")
            {
                shwhere = "(ZhuJianEngineer.bumen='" + dn + "' or ZhuJianEngineer.bumen='标源EMC部')";
            }
            else
            {
                shwhere = "ZhuJianEngineer.bumen='" + dn + "'";
            }
        }
        else
        {
            shwhere = "ZhuJianEngineer.name='" + Session["Username"].ToString() + "'";
        }
        //犯得上发
        if (!IsPostBack)
        {
            Session["dd"] = RadioButtonList1.SelectedValue;
            Bind(RadioButtonList1.SelectedValue);
            BindDep();
            Bind1();
        }
    }
    protected void Bind1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string dd = DateTime.Now.ToShortDateString() + " 00:00:00";
        string sql = "select  * from anjianbeizhu where neirong !='' and time between '" + Convert.ToDateTime(dd) + "' and '" + DateTime.Now + "' ORDER BY id desc";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        con.Close();
        con.Dispose();
    }
    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select * from UserDepa where departmentid='12' or departmentid='13' or departmentid='15' or departmentid='16' or departmentid='17' or departmentid='9'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);
        DataSet ds = new DataSet();
        ad.Fill(ds);
    }

    public void Bind(string dd)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = @"select *,(select top 1 [state]  from AnJianInFo where AnJianInFo.taskid=bianhao) as state1,(ZhuJianEngineer.name) as gc,(AnJianInFo.id) as anjianinfoid,(AnJianInFo.baojiaid) as anjianinfobaojia,
                        (select top 1 weituodanwei from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid) as kehuname,
                        (select top 1 xiadariqi from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid)as xiadariqi,
                        (select top 1 yaoqiuwanchengriqi from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as yaoqiuwanchengriqi
                        from ZhuJianEngineer left join AnJianInFo on ZhuJianEngineer.bianhao=AnJianInFo.taskid and ZhuJianEngineer.bumen=AnJianInFo.bumen where ZhuJianEngineer.bianhao not like 'D%' and " + shwhere + " " + dd + "";
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseNo = DropDownList2.SelectedValue;
        string value = TextBox1.Text;
        string sql = "";
        string state = "";
        string drop_state = DropDownList3.SelectedValue;
        if (string.IsNullOrEmpty(drop_state))
        {
            state = " 1=1";
        }
        else
        {
            state = " AnJianInFo.id in (select id from AnJianInFo where [state]='" + drop_state + "') ";
        }

        if (ChooseNo == "weituodanwei")
        {
            sql = @"select *,(select top 1 [state]  from AnJianInFo where AnJianInFo.taskid=bianhao) as state1,(ZhuJianEngineer.name) as gc,(AnJianInFo.id) as anjianinfoid,(AnJianInFo.baojiaid) as anjianinfobaojia,
                        (select top 1 weituodanwei from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid) as kehuname,
                        (select top 1 xiadariqi from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid)as xiadariqi,
                        (select top 1 yaoqiuwanchengriqi from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as yaoqiuwanchengriqi
                        from ZhuJianEngineer left join AnJianInFo on ZhuJianEngineer.bianhao=AnJianInFo.taskid and ZhuJianEngineer.bumen=AnJianInFo.bumen where " + shwhere + " and ZhuJianEngineer.bianhao in (select rwbianhao from AnJianInFo2 where weituodanwei like '%" + value + "%') and " + state + " and ZhuJianEngineer.bianhao not like 'D%' order by xiadariqi desc";
        }
        else if (ChooseNo == "baogaoid")
        {
            sql = @"select *,(select top 1 [state]  from AnJianInFo where AnJianInFo.taskid=bianhao) as state1,(ZhuJianEngineer.name) as gc,(AnJianInFo.id) as anjianinfoid,(AnJianInFo.baojiaid) as anjianinfobaojia,
                    (select top 1 weituodanwei from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid) as kehuname,
                    (select top 1 xiadariqi from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid)as xiadariqi,
                    (select top 1 yaoqiuwanchengriqi from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as yaoqiuwanchengriqi
                    from ZhuJianEngineer left join AnJianInFo on ZhuJianEngineer.bianhao=AnJianInFo.taskid and ZhuJianEngineer.bumen=AnJianInFo.bumen where " + shwhere + " and (taskid in (select taskid from ItemBaogao where baogaoid like '%" + value + "%')) and " + state + " and ZhuJianEngineer.bianhao not like 'D%' order by xiadariqi desc";
        }
        else if (ChooseNo == "全部")
        {
            sql = @"select *,(select top 1 [state]  from AnJianInFo where AnJianInFo.taskid=bianhao) as state1,(ZhuJianEngineer.name) as gc,(AnJianInFo.id) as anjianinfoid,(AnJianInFo.baojiaid) as anjianinfobaojia,
                        (select top 1 weituodanwei from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid) as kehuname,
                        (select top 1 xiadariqi from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid)as xiadariqi,
                        (select top 1 yaoqiuwanchengriqi from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as yaoqiuwanchengriqi
                        from ZhuJianEngineer left join AnJianInFo on ZhuJianEngineer.bianhao=AnJianInFo.taskid and ZhuJianEngineer.bumen=AnJianInFo.bumen where " + shwhere + "  and " + state + " and ZhuJianEngineer.bianhao not like 'D%' order by xiadariqi desc";
        }
        else
        {
            sql = @"select *,(select top 1 [state]  from AnJianInFo where AnJianInFo.taskid=bianhao) as state1,(ZhuJianEngineer.name) as gc,(AnJianInFo.id) as anjianinfoid,(AnJianInFo.baojiaid) as anjianinfobaojia,
                        (select top 1 weituodanwei from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid) as kehuname,
                        (select top 1 xiadariqi from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid)as xiadariqi,
                        (select top 1 yaoqiuwanchengriqi from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as yaoqiuwanchengriqi
                        from ZhuJianEngineer left join AnJianInFo on ZhuJianEngineer.bianhao=AnJianInFo.taskid and ZhuJianEngineer.bumen=AnJianInFo.bumen where " + shwhere + " and (" + ChooseNo + "  like '%" + value.Trim() + "%') and " + state + " and ZhuJianEngineer.bianhao not like 'D%' order by xiadariqi desc";
        }
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 6);
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
            //MyExcutSql ext = new MyExcutSql();
            //e.Row.Cells[5].Text = ext.Eng(e.Row.Cells[0].Text);
            //e.Row.Cells[4].Text = ext.EngBumen(e.Row.Cells[0].Text);
            // e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 5);

            string sid = e.Row.Cells[1].ToString();


            //if (e.Row.Cells[12].Text == "暂停" || e.Row.Cells[12].Text == "中止")
            //{
            //    e.Row.ForeColor = Color.Red;
            //}
            string dutyname = DutyName();
            if (dutyname.Trim() == "工程师")
            {
                //e.Row.Cells[14].Visible = false;
                GridView1.Columns[16].Visible = false;
            }
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
        string mq = Session["UserName"].ToString() + DateTime.Now.ToShortDateString();

        if (Session[mq] == null)
        {
            Session[mq] = RadioButtonList1.SelectedValue;
        }

        RadioButtonList1.SelectedValue = Session[mq].ToString();
        Bind(Session[mq].ToString());
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
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mq = Session["UserName"].ToString() + DateTime.Now.ToShortDateString();
        Session[mq] = RadioButtonList1.SelectedValue;
        Bind(Session[mq].ToString());
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
            if (GridView1.Rows[i].Cells[columns - 1].Text.ToString().Trim() != "Regular 正常")
            {
                //GridView1.Rows[i].BackColor = System.Drawing.Color.Red;//背景颜色
                GridView1.Rows[i].ForeColor = Color.Red;//字体颜色
            }
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
    /// <summary>
    /// 查看职位
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

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
}