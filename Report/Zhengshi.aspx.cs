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

public partial class Report_Zhengshi : System.Web.UI.Page
{
    private int _i = 0;
    private string shwere = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string dutyname = "";//职位
            string bumen = "";//部门
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con);
            SqlDataReader dr = cmdstate.ExecuteReader();
            if (dr.Read())
            {
                dutyname = dr["dutyname"].ToString();
                bumen = dr["departmentname"].ToString();
            }
            dr.Close();
            if (dutyname == "系统管理员" || (dutyname == "总经理" && bumen == "总经办") || dutyname == "董事长")
            {
                shwere = " 1=1";
            }
            else
            {
                shwere = " a.kehuid in (select customerid from Customer_Sales where responser='" + Session["Username"].ToString() + "')";
            }
        }
        txFDate.Value = DateTime.Now.AddYears(-1).AddDays(-7).ToString("yyyy-MM-dd");
        txTDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
       // limit("正式报告");
        if (!IsPostBack)
        {
            //该页面只对暂时销售助理开放
            Bind();
        }
    }
    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = @" select b.id as id,b.baogaoid as baogaoid,b.taskid as rwid,a.weituodanwei as weituo,a.name as name,b.fillname as faer,b.time as fatime,b.beizhu1 as remork from "
                      + " BaoGaoFaFang b left join AnJianInFo2 a on b.taskid = a.rwbianhao  where " + shwere + "  order by CONVERT(datetime,b.time)  desc";
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
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 6);
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 6);
            //e.Row.Cells[5].Text = SubStr(e.Row.Cells[5].Text, 6);

            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
            LinkButton LinkBtn_DetailInfo21 = (LinkButton)e.Row.FindControl("LinkButton5");
        }
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";
        if (DropDownList1.SelectedValue == "全部")
        {
            sql = @" select b.id as id,b.baogaoid as baogaoid,b.taskid as rwid,a.weituodanwei as weituo,a.name as name,b.fillname as faer,b.time as fatime,b.beizhu1 as remork from "
              + " BaoGaoFaFang b left join AnJianInFo2 a on b.taskid = a.rwbianhao where " + shwere + " and CONVERT(datetime,b.time) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddDays(1) + "'  order by CONVERT(datetime,b.time)  desc";
        }
        else
        {
            sql = @" select b.id as id,b.baogaoid as baogaoid,b.taskid as rwid,a.weituodanwei as weituo,a.name as name,b.fillname as faer,b.time as fatime,b.beizhu1 as remork from "
                        + " BaoGaoFaFang b left join AnJianInFo2 a on b.taskid = a.rwbianhao  where" + shwere + " and CONVERT(datetime,b.time) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddDays(1) + "' and  " + DropDownList1.SelectedValue + " like '%" + TextBox1.Text + "%'  order by CONVERT(datetime,b.time)  desc";
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
}