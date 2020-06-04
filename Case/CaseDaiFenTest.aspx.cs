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

public partial class Case_CaseDaiFen2 : System.Web.UI.Page
{
    protected string shwhere = "";
    private int _i = 0;
    string quan = "0";
    protected string condition = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //只有系统管理员、总经理、工程师经理及工程师才能查看此页面
        limit("分派测试员");
        string bumen = BuMen();//部门
        string position = Zhiwei();

        if (position == "系统管理员" || position == "总经理" || position == "董事长")
        {
            shwhere = "1=1";
        }
        else if (position == "工程经理")
        {
            shwhere = "ZhuJianEngineer.bumen='" + bumen + "'";
        }
        else
        {
            shwhere = "ZhuJianEngineer.name='" + Session["Username"].ToString() + "'";
        }

        if (!IsPostBack)
        {
            Bind();
        }
    }
    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = @"select *,(select top 1 [state]  from AnJianInFo where AnJianInFo.taskid=bianhao) as state1,(ZhuJianEngineer.name) as gc,(AnJianInFo.id) as anjianinfoid,(AnJianInFo.baojiaid) as anjianinfobaojia,
                        (select top 1 weituodanwei from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid) as kehuname,(AnJianInFo.baojiaid) as bjid,
                        (select top 1 xiadariqi from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid)as xiadariqi,
                        (select top 1 yaoqiuwanchengriqi from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as yaoqiuwanchengriqi
                        from ZhuJianEngineer left join AnJianInFo on ZhuJianEngineer.bianhao=AnJianInFo.taskid and ZhuJianEngineer.bumen=AnJianInFo.bumen where " + shwhere + " and ZhuJianEngineer.bianhao not like 'D%' order by xiadariqi desc";
        SqlDataAdapter sda = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        sda.Fill(ds);
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
        Set_Color();
    }
    /// <summary>
    /// 职位
    /// </summary>
    /// <returns></returns>
    private string Zhiwei()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string position = "";
            con.Open();
            string sql = "select dutyname from UserInfo where UserName='" + Session["Username"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader da = cmd.ExecuteReader();
            if (da.Read())
            {
                position = da["dutyname"].ToString();
            }
            return position;
        }
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

    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseID = DropDownList1.SelectedValue.Trim();
        string value = TextBox1.Text.Trim();
        string sql = "";
        string condition = DropDownList2.SelectedValue;
        if (string.IsNullOrEmpty(condition))
        {
            condition = string.Empty;
        }
        else if (condition == "已分派")
        {
            condition = "and ZhuJianEngineer.bianhao in (select renwuid from DaiFenTest)";
        }
        else
        {
            condition = "and ZhuJianEngineer.bianhao not in (select renwuid from DaiFenTest)";
        }

        if (ChooseID == "weituodanwei")
        {
            sql = @"select *,(select top 1 [state]  from AnJianInFo where AnJianInFo.taskid=bianhao) as state1,(ZhuJianEngineer.name) as gc,(AnJianInFo.id) as anjianinfoid,(AnJianInFo.baojiaid) as anjianinfobaojia,
                        (select top 1 weituodanwei from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid) as kehuname,(AnJianInFo.baojiaid) as bjid,
                        (select top 1 xiadariqi from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid)as xiadariqi,
                        (select top 1 yaoqiuwanchengriqi from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as yaoqiuwanchengriqi
                        from ZhuJianEngineer left join AnJianInFo on ZhuJianEngineer.bianhao=AnJianInFo.taskid and ZhuJianEngineer.bumen=AnJianInFo.bumen where " + shwhere + " and ZhuJianEngineer.bianhao in (select rwbianhao from AnJianInFo2 where weituodanwei like '%" + value + "%') and ZhuJianEngineer.bianhao not like 'D%' " + condition + " order by xiadariqi desc";
        }
        else
        {
            sql = @"select *,(select top 1 [state]  from AnJianInFo where AnJianInFo.taskid=bianhao) as state1,(ZhuJianEngineer.name) as gc,(AnJianInFo.id) as anjianinfoid,(AnJianInFo.baojiaid) as anjianinfobaojia,
                        (select top 1 weituodanwei from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid) as kehuname,(AnJianInFo.baojiaid) as bjid,
                        (select top 1 xiadariqi from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid)as xiadariqi,
                        (select top 1 yaoqiuwanchengriqi from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as yaoqiuwanchengriqi
                        from ZhuJianEngineer left join AnJianInFo on ZhuJianEngineer.bianhao=AnJianInFo.taskid and ZhuJianEngineer.bumen=AnJianInFo.bumen where " + shwhere + "  and (" + ChooseID + " like '%" + value + "%') and ZhuJianEngineer.bianhao not like 'D%' " + condition + " order by xiadariqi desc";
        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        AspNetPager1.Visible = false;
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
    protected void limit(string pagename1)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
        }
        else
        {
            con.Close();
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 6);
            _i++;
        }
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
}