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

public partial class Report_BaoGaoList : System.Web.UI.Page
{

    protected string shwhere = "1=1";
    private int _i = 0;
    private string minId = "0";
    protected void Page_Load(object sender, EventArgs e)
    {


       // shwhere = "(state !='已批准' or statebumen1 ='不合格' or statebumen2 ='不合格')";

        shwhere = "1=1";
        
        
        
        if (!IsPostBack)
        {
            txFDate.Value = DateTime.Now.AddDays(-90).ToShortDateString();
            txTDate.Value = DateTime.Now.ToShortDateString();


            string cmd = "select  count(*) as d from BaoGao2 where  filltime between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'";

            if (Request.QueryString["minid"] != null)
            {
                minId = Request.QueryString["minid"].ToString();
            }

            if (minId != "0")
            {
                DropDownList3.SelectedValue = Request.QueryString["ti6"].ToString();
                DropDownList2.SelectedValue = Request.QueryString["ti1"].ToString();
                TextBox1.Text = Request.QueryString["ti2"].ToString();
                txFDate.Value = Request.QueryString["ti3"].ToString();
                txTDate.Value = Request.QueryString["ti4"].ToString();

                cmd = "select count(*) as d from BaoGao2 where " + Server.UrlDecode(minId) + "";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                SqlCommand cmd1 = new SqlCommand(cmd, con);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    AspNetPager2.RecordCount = Convert.ToInt32(dr["d"].ToString());
                }
                con.Close();
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                SqlCommand cmd1 = new SqlCommand(cmd, con);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    AspNetPager2.RecordCount = Convert.ToInt32(dr["d"].ToString());
                    // Label3.Text = "当前结果记录数:200";
                }
                con.Close();
            }


            Bind();
            
        }
    }
    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";

        if (DropDownList3.SelectedValue == "1")
        {

            sql = "select *,(select kf from anjianinfo2 where rwbianhao=baogao2.tjid) as kf,(select shenqingbianhao from anjianinfo2 where rwbianhao=baogao2.tjid) as shenqingbianhao from BaoGao2  where " + shwhere + " order by wanchengtime desc";

            if (minId != "0")
            {
                sql = "select *,(select kf from anjianinfo2 where rwbianhao=baogao2.tjid) as kf,(select shenqingbianhao from anjianinfo2 where rwbianhao=baogao2.tjid) as shenqingbianhao from BaoGao2  where  " + minId + " and  filltime between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by wanchengtime desc";

            }
        }
        else
        {
            sql = "select *,(select kf from anjianinfo2 where rwbianhao=baogao2.tjid) as kf,(select shenqingbianhao from anjianinfo2 where rwbianhao=baogao2.tjid) as shenqingbianhao from BaoGao2  where " + shwhere + " order by convert(datetime,beizhu4) desc";

            if (minId != "0")
            {
                sql = "select *,(select kf from anjianinfo2 where rwbianhao=baogao2.tjid) as kf,(select shenqingbianhao from anjianinfo2 where rwbianhao=baogao2.tjid) as shenqingbianhao from BaoGao2  where  " + minId + " and  filltime between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by convert(datetime,beizhu4) desc";

            }
 
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
        string dd = "";

        if (DropDownList3.SelectedValue == "1")
        {

           

            if (DropDownList2.SelectedValue == "全部")
            {
                dd = " " + shwhere + " and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and  ((tjid like '%" + TextBox1.Text.Trim() + "%' or baogaoid like '%" + TextBox1.Text.Trim() + "%' or tjid in (select  rwbianhao from anjianinfo2 where shenqingbianhao  like '%" + TextBox1.Text.Trim() + "%' and shenqingbianhao !='') or tjid in (select  rwbianhao from anjianinfo2 where kf like '%" + TextBox1.Text.Trim() + "%') or dayinname like '%" + TextBox1.Text.Trim() + "%')) ";
            }
            else
            {
                dd = " " + shwhere + " and  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and   ( state like  '%" + DropDownList2.SelectedValue + "%' ) and  (tjid like '%" + TextBox1.Text.Trim() + "%' or baogaoid like '%" + TextBox1.Text.Trim() + "%' or tjid in (select  rwbianhao from anjianinfo2 where shenqingbianhao  like '%" + TextBox1.Text.Trim() + "%') or tjid in (select  rwbianhao from anjianinfo2 where kf like '%" + TextBox1.Text.Trim() + "%') or dayinname like '%" + TextBox1.Text.Trim() + "%')";
            }
        }
        else
        {
          

            if (DropDownList2.SelectedValue == "全部")
            {
                dd = " " + shwhere + " and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and  ((tjid like '%" + TextBox1.Text.Trim() + "%' or baogaoid like '%" + TextBox1.Text.Trim() + "%' or tjid in (select  rwbianhao from anjianinfo2 where shenqingbianhao  like '%" + TextBox1.Text.Trim() + "%' and shenqingbianhao !='') or tjid in (select  rwbianhao from anjianinfo2 where kf like '%" + TextBox1.Text.Trim() + "%') or dayinname like '%" + TextBox1.Text.Trim() + "%')) ";
            }
            else
            {
                dd = " " + shwhere + " and  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and   ( state like  '%" + DropDownList2.SelectedValue + "%' ) and  (tjid like '%" + TextBox1.Text.Trim() + "%' or baogaoid like '%" + TextBox1.Text.Trim() + "%' or tjid in (select  rwbianhao from anjianinfo2 where shenqingbianhao  like '%" + TextBox1.Text.Trim() + "%') or tjid in (select  rwbianhao from anjianinfo2 where kf like '%" + TextBox1.Text.Trim() + "%') or dayinname like '%" + TextBox1.Text.Trim() + "%')";
            }
        }



        
       // dd = " state='" + DropDownList3.SelectedValue + "' and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and (state like '%" + TextBox1.Text + "%' or rwbianhao like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%') or rwbianhao in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text + "%'))";
        Response.Redirect("BaoGaoList.aspx?minid=" + Server.UrlEncode(dd) + "&&ti1=" + DropDownList2.SelectedValue + "&&ti6=" + DropDownList3.SelectedValue + "&&ti2=" + TextBox1.Text.Trim() + "&&ti3=" + txFDate.Value + "&&ti4=" + txTDate.Value + "&&ti5=" + DropDownList1.SelectedValue);

        
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");

            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

            string f = e.Row.Cells[1].Text;
            searchwhere sxb1 = new searchwhere();
            e.Row.Cells[6].Text = sxb1.BaoGaoShiJian(f).ToString();
            if (e.Row.Cells[7].Text == "正在打印")
            {
                e.Row.Cells[7].ForeColor = Color.Red;
            }


        }
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text.Trim().ToString().Substring(0, 4) == "1900")
                {
                    e.Row.Cells[4].Text = "";
                }
                if (e.Row.Cells[5].Text.Trim().ToString().Substring(0, 4) == "1900")
                {
                    e.Row.Cells[5].Text = "";
                }
               
            }
        }
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
}