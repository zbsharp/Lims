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

public partial class Quotation_YiShouLi4aspxFoShan : System.Web.UI.Page
{
    private int _i = 0;
    protected StringBuilder strSql = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {


        if (limit1("查看佛山任务"))
        {

        }
        else
        {
           // Response.Redirect("~/Customer/Welcome.aspx?id=rr1");
        }

        strSql.Append("select ");

        strSql.Append("id as idd,");
        strSql.Append("kf as kf,");
        strSql.Append("(select top 1 hesuanbiaozhi from invoice where rwbh=anjianinfo2.rwbianhao) as daoz,");
        strSql.Append("(select top 1 name from ZhuJianEngineer where bianhao=anjianinfo2.rwbianhao ) as name1,");
        strSql.Append("renwu as renwu,");
        strSql.Append("state as state1,");
        strSql.Append("xiadariqi as xiadariqi,");
        strSql.Append("(select top 1 fenpainame from anjianinfo where tijiaobianhao=anjianinfo2.rwbianhao and type='是') as fenpainame,");
        strSql.Append("* from anjianinfo2 ");
        if (!IsPostBack)
        {
            TimeBind();
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
        string sid = e.CommandArgument.ToString();

        if (e.CommandName == "back")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql3 = "update Anjianxinxi2 set state='退回',statetime='" + DateTime.Now + "',statename='" + Session["UserName"].ToString() + "' where  bianhao ='" + sid + "'";
            SqlCommand cmd = new SqlCommand(sql3, con);
            cmd.ExecuteNonQuery();


            con.Close();

            ld.Text = "<script>alert('退回成功!');</script>";


            TimeBind((DropDownList1.SelectedValue), TextBox1.Text);
        }

        else if (e.CommandName == "cancel1")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sqly = "delete from Anjianxinxi3 where bianhao='" + sid + "'";
            SqlCommand cmdy = new SqlCommand(sqly, con);
            cmdy.ExecuteNonQuery();

            string sqlx = "delete from Anjianxinxi2 where bianhao='" + sid + "'";
            SqlCommand cmdx = new SqlCommand(sqlx, con);
            cmdx.ExecuteNonQuery();


            con.Close();

            ld.Text = "<script>alert('取消成功!');</script>";


            TimeBind((DropDownList1.SelectedValue), TextBox1.Text);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text.Trim();
        AspNetPager1.Visible = false;
        TimeBind(ChooseNo, ChooseValue);
    }

    protected void TimeBind(string a, string b)
    {


        string ChooseID = a;
        string ChooseValue = b;
        string sqlstr;

        if (DropDownList1.SelectedValue == "kehuname")
        {
            sqlstr = strSql + " where " + searchwhere.searchcustomer(ChooseValue) + " and  (responser in (select username from userinfo where departmentname='佛山公司' or departmentname='广州办事处'))  order by substring(rwbianhao,4,5) desc ";

        }
        else
        {
            sqlstr = strSql + " where " + DropDownList1.SelectedValue + " like '%" + ChooseValue + "%'  and (responser in (select username from userinfo where departmentname='佛山公司' or departmentname='广州办事处')) order by substring(rwbianhao,4,5) desc";

        }



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

        string sqlstr;

        sqlstr = strSql + "  where  (responser in (select username from userinfo where departmentname='佛山公司' or departmentname='广州办事处'))  order by id desc  ";

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");


            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 10);
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 10);
            e.Row.Cells[5].Text = SubStr(e.Row.Cells[5].Text, 10);
            MyExcutSql ext = new MyExcutSql();
            e.Row.Cells[10].Text = ext.Eng(e.Row.Cells[1].Text);

        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        TimeBind();
    }
}