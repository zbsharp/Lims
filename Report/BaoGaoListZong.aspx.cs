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

public partial class Report_BaoGaoListZong : System.Web.UI.Page
{
    protected string shwhere = "1=1";
    private int _i = 0;
    private string minId = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        Button3.Attributes.Add("onclick", "return confirm('您确定需要取消选中报告的批准状态吗？')");
       
        if (!IsPostBack)
        {
            string zhiwei = Position();
            string bumen = BuMen();
            if (zhiwei == "系统管理员" || zhiwei == "总经理" || zhiwei == "董事长")
            {

            }
            else
            {
                shwhere = "leibie like '%" + bumen + "%'";
            }
            txFDate.Value = DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd");
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
        Literal1.Text = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select top 200 id,tjid,rwid,baogaoid,leibie,fillname,filltime,state,dayinname,dayintime,CMA,CNAS,AtwolA,(select bianhao from AnJianInFo2 where rwbianhao=rwid) as bianhao2,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from baogao2 where beizhu1='否' and " + shwhere + " and rwid not like 'D%' order by id desc";
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
        Literal1.Text = string.Empty;
        string dd = "";
        if (DropDownList1.SelectedValue == "0")
        {
            dd = " " + shwhere + " and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' ";
        }
        else
        {
            dd = " " + shwhere + " and  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and  " + DropDownList1.SelectedValue + " like '%" + TextBox1.Text + "%'";
        }
        Bind_sql(dd);
    }

    protected void Bind_sql(string dd)
    {
        Literal1.Text = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select top 200 id,tjid,rwid,baogaoid,leibie,fillname,filltime,state,dayinname,dayintime,CMA,CNAS,AtwolA,(select bianhao from AnJianInFo2 where rwbianhao=rwid) as bianhao2,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from baogao2 where beizhu1='否' and " + dd + " and rwid not like 'D%' order by id desc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        con.Dispose();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            MyExcutSql ext = new MyExcutSql();
            e.Row.Cells[3].Text = ext.Eng(e.Row.Cells[0].Text);
            _i++;
        }

    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[13].FindControl("CheckBox1");
            if (hzf.Checked)
            {
                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                string state = gr.Cells[4].Text.ToString();
                if (state == "已缮制")
                {
                    string sql = "update baogao2 set state='已出草稿', shenheby='" + Session["UserName"].ToString() + "',pizhunby='',pizhundate='1900-1-1' where id='" + sid + "' and [state]='已缮制'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        con.Close();
        Bind();
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