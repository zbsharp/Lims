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

public partial class Case_CaseDaiFen22 : System.Web.UI.Page
{
    //protected string shwhere = "";
    private int _i = 0;
    protected string condition = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string posistion = Position();
        if (posistion.Trim() == "测试员")
        {
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
        //shwhere = "anjianinfo.id  in (select baojiaid from ZhuJianEngineer where isreception='是')";
        string positon = Position();
        string bumen = BuMen();
        if (positon == "系统管理员" || positon == "总经理" || positon == "董事长")
        {
            condition = "1=1";
        }
        else if (positon == "工程经理")
        {
            condition = "ZhuJianEngineer.bumen='" + bumen + "'";
        }
        else
        {
            condition = "ZhuJianEngineer.name='" + Session["Username"].ToString() + "'";
        }

        if (!IsPostBack)
        {
            Bind();
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

    protected void Bind()
    {
        ld.Text = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = @"select *,AnJianInFo.[state] as state1,
                        (ZhuJianEngineer.name) as gc,(AnJianInFo.baojiaid) as bjid,
                        (select top 1 weituodanwei from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid) as kehuname,
                        (select top 1 xiadariqi from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid)as xiadariqi,
                        (select top 1 yaoqiuwanchengriqi from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as yaoqiuwanchengriqi
                        from ZhuJianEngineer left join AnJianInFo on ZhuJianEngineer.bianhao=AnJianInFo.taskid and ZhuJianEngineer.bumen=AnJianInFo.bumen
                        where ZhuJianEngineer.isreception='是' and AnJianInFo.id in (select id from AnJianInFo where [state]='进行中') and bianhao not like 'D%' and " + condition + " order by xiadariqi desc ";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
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
        if (ChooseID == "weituodanwei")
        {
            sql = @"select *,AnJianInFo.[state] as state1,(ZhuJianEngineer.name) as gc,(AnJianInFo.baojiaid) as bjid,
                    (select top 1 weituodanwei from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid) as kehuname,
                    (select top 1 xiadariqi from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid)as xiadariqi,
                    (select top 1 yaoqiuwanchengriqi from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as yaoqiuwanchengriqi
                    from ZhuJianEngineer left join AnJianInFo on ZhuJianEngineer.bianhao=AnJianInFo.taskid and ZhuJianEngineer.bumen=AnJianInFo.bumen
                    where ZhuJianEngineer.isreception='是' and AnJianInFo.id in (select id from AnJianInFo where [state]='进行中') and bianhao not like 'D%' and " + condition + " and ZhuJianEngineer.bianhao in (select rwbianhao from AnJianInFo2 where weituodanwei like '%" + value + "%') order by xiadariqi desc ";
        }
        else
        {
            sql = @"select *,AnJianInFo.[state] as state1,(ZhuJianEngineer.name) as gc,(AnJianInFo.baojiaid) as bjid,
                        (select top 1 weituodanwei from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid) as kehuname,
                        (select top 1 xiadariqi from AnJianInFo2 where AnJianInFo2.rwbianhao=AnJianInFo.taskid)as xiadariqi,
                        (select top 1 yaoqiuwanchengriqi from AnJianInFo2 where AnJianInFo.taskid=AnJianInFo2.rwbianhao) as yaoqiuwanchengriqi
                        from ZhuJianEngineer left join AnJianInFo on ZhuJianEngineer.bianhao=AnJianInFo.taskid and ZhuJianEngineer.bumen=AnJianInFo.bumen
                        where ZhuJianEngineer.isreception='是' and AnJianInFo.id in (select id from AnJianInFo where [state]='进行中') and bianhao not like 'D%' and " + condition + " and (" + DropDownList1.SelectedValue + "  like '%" + value + "%' )  order by xiadariqi desc ";
        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        AspNetPager1.Visible = false;
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "cancel2")
        {
            GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)); //此得出的值是表示那行被选中的索引值
            string rwid = GridView1.Rows[drv.RowIndex].Cells[1].Text.ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "update ZhuJianEngineer set isreception='否' where bianhao='" + rwid + "' and name='" + Session["Username"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Bind();
            }
            else
            {
                ld.Text = "<script>alert('只能由本人取消任务')</script>";
            }
            con.Close();
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