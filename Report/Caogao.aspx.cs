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

public partial class Report_Caogao : System.Web.UI.Page
{
    protected string shwhere = "1=1";
    private int _i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        shwhere = " 1=1 ";
        Button3.Attributes.Add("onclick", "return confirm('您确定需要修改选中报告为完成状态吗？')");

        if (!IsPostBack)
        {
            txFDate.Value = DateTime.Now.AddYears(-1).AddDays(-7).ToString("yyyy-MM-dd");
            txTDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            Bind();
        }
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


    public void Bind()
    {
        Literal1.Text = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";
        string zhiwei = Position();
        string bumen = BuMen();
        //系统管理员查看所有，其他人查看自己部门
        if (zhiwei.Trim() == "系统管理员" || zhiwei.Trim() == "总经理" || zhiwei.Trim() == "董事长")
        {
            sql = "select *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否' and rwid not like 'D%' order by id desc";
        }
        else if (bumen == "龙华EMC部")
        {
            sql = "select *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where (leibie like '%" + bumen + "%' or leibie='标源EMC部') and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否'  and rwid not like 'D%' order by id desc";
        }
        else if (bumen == "龙华安规部")
        {
            sql = "select *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源安规部') and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否'  and rwid not like 'D%' order by id desc";
        }
        else
        {
            sql = "select *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  leibie like '%" + bumen + "%' and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否'  and rwid not like 'D%' order by id desc";
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;
        string sqlstr;
        string position = Position();
        string bumen = BuMen();
        if (position.Trim() == "系统管理员" || position == "总经理" || position == "董事长")
        {
            if (DropDownList1.SelectedValue == "0")
            {
                sqlstr = "select *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否'  and rwid not like 'D%' order by id desc";
            }
            else
            {
                sqlstr = "select *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and  " + ChooseNo + " like '%" + ChooseValue + "%' and beizhu1='否'  and rwid not like 'D%' order by id desc";
            }
        }
        else if (bumen == "龙华EMC部")
        {
            if (DropDownList1.SelectedValue == "0")
            {
                sqlstr = "select *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源EMC部') and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否'  and rwid not like 'D%' order by id desc";
            }
            else
            {
                sqlstr = " select *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源EMC部') and  " + ChooseNo + " like  '%" + ChooseValue + "%' and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否'  and rwid not like 'D%' order by id desc";
            }
        }
        else if (bumen == "龙华安规部")
        {
            if (DropDownList1.SelectedValue == "0")
            {
                sqlstr = "select *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源安规部') and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否'  and rwid not like 'D%' order by id desc";
            }
            else
            {
                sqlstr = " select *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源安规部') and  " + ChooseNo + " like  '%" + ChooseValue + "%' and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否'  and rwid not like 'D%' order by id desc";
            }
        }
        else
        {
            if (DropDownList1.SelectedValue == "0")
            {
                sqlstr = "select *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  leibie like '%" + bumen + "%' and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否'  and rwid not like 'D%' order by id desc";
            }
            else
            {
                sqlstr = " select *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  leibie like '%" + bumen + "%' and  " + ChooseNo + " like  '%" + ChooseValue + "%' and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否'  and rwid not like 'D%' order by id desc";
            }
        }

        SqlDataAdapter ad = new SqlDataAdapter(sqlstr, con);
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
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");

            MyExcutSql ext = new MyExcutSql();
            e.Row.Cells[3].Text = ext.Eng(e.Row.Cells[0].Text);
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

            //e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 10);

        }

    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        //全选或全不选
        int i;
        if (((CheckBox)sender).Checked)
        {
            for (i = 0; i < GridView1.Rows.Count; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked = true;
            }
        }
        else
        {
            for (i = 0; i < GridView1.Rows.Count; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked = false;
            }
        }

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox hzf = (CheckBox)gr.Cells[9].FindControl("CheckBox1");
                if (hzf.Checked)
                {
                    string state = gr.Cells[5].Text.ToString();
                    if (state == "&nbsp;")
                    {
                        string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                        string sql = "update baogao2 set statebumen1='合格',statebumen2='合格',dayinname='" + Session["Username"].ToString() + "',dayintime='" + DateTime.Now + "' ,state='已出草稿', shenheby='" + Session["Username"].ToString() + "',pizhunby='" + Session["Username"].ToString() + "',pizhundate='" + DateTime.Now + "' where id='" + sid + "'";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
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
        string id = e.CommandArgument.ToString();
        GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        int index = gvrow.RowIndex;
        if (e.CommandName == "shanzhi")
        {
            string rwid = GridView1.Rows[index].Cells[0].Text.ToString();
            string state = GridView1.Rows[index].Cells[5].Text.ToString();
            if (state == "&nbsp;")
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
                {
                    con.Open();
                    string sql = "update baogao2 set statebumen1='合格',statebumen2='合格',dayinname='" + Session["Username"].ToString() + "',dayintime='" + DateTime.Now + "' ,state='已出草稿', shenheby='" + Session["Username"].ToString() + "',pizhunby='" + Session["Username"].ToString() + "',pizhundate='" + DateTime.Now + "' where baogaoid='" + id + "'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
                Bind();
            }
            else
            {
                Literal1.Text = "<script>alert('该报告已出草稿')</script>";
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
    /// <summary>
    /// 当前登录进来人的职位
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
}