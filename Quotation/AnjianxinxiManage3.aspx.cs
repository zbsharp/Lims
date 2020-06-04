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

public partial class Quotation_AnjianxinxiManage3 : System.Web.UI.Page
{
    private int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TimeBind();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "Chakan")
        {
            Response.Redirect("AnjianxinxiSee.aspx?id=" + sid);
        }
        else if (e.CommandName == "renwu1")
        {
            Response.Redirect("~/Case/TaskIn.aspx?tijiaobianhao=" + sid);
        }


        else if (e.CommandName == "renwu2")
        {
            Response.Redirect("~/Print/TaskPrint.aspx?bianhao=" + sid);
        }



        if (e.CommandName == "cancel1")
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sqly = "delete from Anjianxinxi3 where bianhao='" + sid + "'";
            SqlCommand cmdy = new SqlCommand(sqly, con);
            cmdy.ExecuteNonQuery();

            string sqlx = "delete from Anjianxinxi2 where bianhao='" + sid + "'";
            SqlCommand cmdx = new SqlCommand(sqlx, con);
            cmdx.ExecuteNonQuery();

            string sqlx1 = "delete from anjianinfo2 where bianhao='" + sid + "'";
            SqlCommand cmdx1 = new SqlCommand(sqlx1, con);
            cmdx1.ExecuteNonQuery();

            string sqlx2 = "delete from anjianinfo where tijiaobianhao='" + sid + "'";
            SqlCommand cmdx2 = new SqlCommand(sqlx2, con);
            cmdx2.ExecuteNonQuery();

            con.Close();

            ld.Text = "<script>alert('取消成功!');</script>";


            TimeBind(int.Parse(DropDownList1.SelectedValue), TextBox1.Text);
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        int ChooseNo = int.Parse(DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text.Trim();

        TimeBind(ChooseNo, ChooseValue);
        AspNetPager1.Visible = false;
    }

    protected void TimeBind(int a, string b)
    {
        ld.Text = string.Empty;
        int ChooseID = a;
        string ChooseValue = b;
        string sqlstr;
        string dutyname = DutyName();
        //sqlstr = "select * ,(select name from BaoJiaChanPing where id=anjianxinxi2.chanpinbianhao) as name from Anjianxinxi2 where (baojiaid='" + ChooseValue + "' or weituo like '%" + ChooseValue + "%') and shoulibiaozhi='否' and biaozhi='是' and fillname='" + Session["UserName"].ToString() + "' order by id desc ";
        //if (dutyname == "系统管理员"||dutyname=="客服经理")
        //{
        sqlstr = string.Format(@"select * ,(select name from BaoJiaChanPing where id = anjianxinxi2.chanpinbianhao) as name
                              from Anjianxinxi2 where  weituo like '%{0}%' and shoulibiaozhi = '否' and biaozhi = '是'
                                and baojiaid in (select baojiaid from BaoJiaBiao where HuiQianBiaoZhi='是')
                              order by id desc ", ChooseValue);
        //}
        //else
        //{
        //    sqlstr = string.Format(@"select * ,(select name from BaoJiaChanPing where id=anjianxinxi2.chanpinbianhao) as name 
        //                             from Anjianxinxi2 where weituo like '%{0}%'
        //                             and  fillname='{1}'
        //                             and shoulibiaozhi='否' and biaozhi='是' 
        //                             order by id desc ", ChooseValue, Session["UserName"].ToString());
        //}
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

    }


    protected void TimeBind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string dutyname = DutyName();
        string sqlstr;
        //if (dutyname == "系统管理员" || dutyname=="客服经理")
        //{
        sqlstr = "select *,(select name from BaoJiaChanPing where id=anjianxinxi2.chanpinbianhao) as name from Anjianxinxi2 where  shoulibiaozhi='否' and biaozhi='是' and baojiaid in (select baojiaid from BaoJiaBiao where HuiQianBiaoZhi='是') order by id desc ";
        //}
        //else
        //{
        //    sqlstr = string.Format(@"select *,(select name from BaoJiaChanPing where id=anjianxinxi2.chanpinbianhao) as name 
        //                           from Anjianxinxi2 where  shoulibiaozhi='否' and biaozhi='是' and fillname='{0}'
        //                           order by id desc",Session["UserName"].ToString());
        //}
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager1.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager1.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //设置GridView的颜色
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.Cells[2].Text.ToString().Trim() == "预警")
        //    {
        //        e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
        //    }
        //}


        if (e.Row.RowType == DataControlRowType.DataRow)
        {



            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");


            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;



            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 10);


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


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        TimeBind();
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
}