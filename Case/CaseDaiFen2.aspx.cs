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
    string jurisdiction = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //只有系统管理员，总经理和工程师经理才能查看此页面
        limit("分派工程");
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
        if (position.Trim() == "系统管理员" || position.Trim() == "总经理")
        {
            jurisdiction = "and 1=1";
        }
        else if (position.Trim() == "工程经理")
        {
            jurisdiction = " and bumen='" + bm + "'";
        }
        shwhere = "((fenpaibiaozhi !='是') and  (anjianinfo2.renwu='是') and  (anjianinfo.state !='完成' and anjianinfo2.state !='关闭' and anjianinfo2.state !='中止')  and anjianinfo.id not in (select baojiaid from ZhuJianEngineer)) and taskid not like 'D%'";
        if (!IsPostBack)
        {
            Bind();
        }
    }

    protected void Bind()
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

    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseID = DropDownList1.SelectedValue.Trim();
        string value = TextBox1.Text.Trim();
        string sql = "";
        //if (DropDownList1.SelectedValue != "kehuname")
        //{
        //    sql = "select anjianinfo.*,(anjianinfo2.state) as statea,anjianinfo2.shiyanleibie, anjianinfo2. yaoqiuwanchengriqi,anjianinfo2.xiadariqi,anjianinfo2.kf,(anjianinfo2.shenqingbianhao) as shenqinghao,(anjianinfo2.weituodanwei) as kehuname    from anjianinfo left join anjianinfo2 on taskid=anjianinfo2.rwbianhao where taskid in (select taskno from anjianxinxi2 where " + DropDownList1.SelectedValue + " like '%" + value + "%') and " + shwhere + " order by substring(taskid,4,5) desc";
        //}
        //else
        //{
        //    sql = "select anjianinfo.*,(anjianinfo2.state) as statea,anjianinfo2.shiyanleibie, anjianinfo2. yaoqiuwanchengriqi,anjianinfo2.xiadariqi,anjianinfo2.kf,(anjianinfo2.shenqingbianhao) as shenqinghao,(anjianinfo2.weituodanwei) as kehuname    from anjianinfo left join anjianinfo2 on taskid=anjianinfo2.rwbianhao where  " + searchwhere.searchcustomer(TextBox1.Text.Trim()) + " and  " + shwhere + "  order by substring(taskid,4,5) desc";

        //}
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



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");

            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 6);
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
            //bool C = false;
            //C = limit1("取消参与");
            //if ( C == true)
            //{



            //}
            //else
            //{
            //    LinkButton LinkBtn_DetailInfo2 = (LinkButton)e.Row.FindControl("LinkButton6");

            //    LinkBtn_DetailInfo2.Enabled = false;
            //    LinkBtn_DetailInfo2.ForeColor = Color.DarkGray;

            //}
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

    protected bool limit1(string pagename1)
    {
        bool A = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {


            con.Close();
            A = true;
        }
        else
        {
            con.Close();
            A = false;
        }
        return A;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();


        if (limit1("取消参与"))
        {
            if (e.CommandName == "cancel1")
            {


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sqly = "";
                string sqlx = "";
                string zhi = "";
                string sql = "select canyu from anjianinfo where id='" + sid + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    zhi = dr["canyu"].ToString();
                }
                dr.Close();

                if (zhi == "" || zhi == "是")
                {
                    sqly = "update anjianinfo set canyu='否' where id='" + sid + "'";

                    sqlx = "update baogaobumen set beizhu2='否' where rwid=(select top 1 taskid from anjianinfo where id='" + sid + "') and bumen=(select top 1 bumen from anjianinfo where id='" + sid + "')";
                }
                else
                {
                    sqly = "update anjianinfo set canyu='是' where id='" + sid + "'";

                    sqlx = "update baogaobumen set beizhu2='是' where rwid=(select top 1 taskid from anjianinfo where id='" + sid + "') and bumen=(select top 1 bumen from anjianinfo where id='" + sid + "')";


                }
                SqlCommand cmdy = new SqlCommand(sqly, con);
                cmdy.ExecuteNonQuery();


                SqlCommand cmdx = new SqlCommand(sqlx, con);
                cmdx.ExecuteNonQuery();

                con.Close();

                ld.Text = "<script>alert('取消成功!');</script>";
                Bind();
            }
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
            if (GridView1.Rows[i].Cells[columns - 1].Text.ToString().Trim() != "Regular 正常")
            {
                //GridView1.Rows[i].BackColor = System.Drawing.Color.Red;//背景颜色
                GridView1.Rows[i].ForeColor = Color.Red;//字体颜色
            }
        }
    }
}