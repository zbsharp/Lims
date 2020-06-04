using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Case_Pretreatment : System.Web.UI.Page
{
    protected string shwhere = "";
    private int _i = 0;
    string quan = "0";
    string jurisdiction = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string position = "";//部门
        string bm = "";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sqldd = "select dutyname,departmentname from UserInfo where UserName='" + Session["Username"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sqldd, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                position = dr["dutyname"].ToString();
                bm = dr["departmentname"].ToString();
            }
            dr.Close();
        }
        if (position.Trim() == "系统管理员" || position.Trim() == "总经理" || position.Trim() == "董事长")
        {
            jurisdiction = "and 1=1";
        }
        else //if (position.Trim() == "工程经理")
        {
            limit();
            jurisdiction = " and bumen='" + bm + "'";
        }

        shwhere = "((fenpaibiaozhi !='是') and  (anjianinfo2.renwu='是') and  (anjianinfo2.state !='完成' and anjianinfo2.state !='关闭' and anjianinfo2.state !='中止')  and anjianinfo.id not in (select baojiaid from ZhuJianEngineer)) and taskid not like 'D%'";
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select anjianinfo.*,(anjianinfo2.state) as statea,anjianinfo2.shiyanleibie, anjianinfo2. yaoqiuwanchengriqi,anjianinfo2.xiadariqi,anjianinfo2.kf,(anjianinfo2.shenqingbianhao) as shenqinghao,(anjianinfo2.weituodanwei) as kehuname    from anjianinfo left join anjianinfo2 on taskid=anjianinfo2.rwbianhao where " + shwhere + "" + jurisdiction + "  order by id desc";
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
    protected void limit()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string bm = "";
        string sql_bm = "select dutyname,departmentname from UserInfo where UserName='" + Session["Username"].ToString() + "'";
        SqlCommand cmd_bm = new SqlCommand(sql_bm, con);
        SqlDataReader dr_bm = cmd_bm.ExecuteReader();
        if (dr_bm.Read())
        {
            bm = dr_bm["departmentname"].ToString();
        }
        dr_bm.Close();

        string sql_type = "select * from DepartmentType where department='" + bm + "' and [Type]='需前处理'";
        SqlCommand cmd_type = new SqlCommand(sql_type, con);
        SqlDataReader dr_type = cmd_type.ExecuteReader();
        if (dr_type.Read())
        {

        }
        else
        {
            Response.Write("<script>alert('" + bm + "暂没有前处理！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
    }
    protected void Set_Color()
    {
        int columns = this.GridView1.Columns.Count;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (GridView1.Rows[i].Cells[columns - 1].Text.ToString().Trim() != "Regular 正常")
            {
                GridView1.Rows[i].ForeColor = Color.Red;//字体颜色
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseID = DropDownList1.SelectedValue.Trim();
        string value = TextBox1.Text.Trim();
        string sql = "";
        sql = "select anjianinfo.*,(anjianinfo2.state) as statea,anjianinfo2.shiyanleibie, anjianinfo2. yaoqiuwanchengriqi,anjianinfo2.xiadariqi,anjianinfo2.kf,(anjianinfo2.shenqingbianhao) as shenqinghao,(anjianinfo2.weituodanwei) as kehuname    from anjianinfo left join anjianinfo2 on taskid=anjianinfo2.rwbianhao where taskid in (select taskno from anjianxinxi2 where " + DropDownList1.SelectedValue + " like '%" + value + "%') and " + shwhere + "" + jurisdiction + " order by id desc";
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

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
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
