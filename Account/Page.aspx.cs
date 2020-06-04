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

public partial class Account_Page : System.Web.UI.Page
{
    private int _i = 0;
    protected string str = "";
    private string minId = "0";
    protected void Page_Load(object sender, EventArgs e)
    {

        //  str = "select  rw sum(ceshifeikf.feiyong) as feiyong,taskid,kehuid from CeShiFeiKf where kehuid !='' group by taskid,kehuid";


        if (!IsPostBack)
        {

            txFDate.Value = DateTime.Now.AddMonths(-12).ToString("yyyy-MM-01");
            DateTime lastDay = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddHours(23);
            txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();

            string cmd = "select  count(*) as d from Anjianinfo2 where  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'";

            if (Request.QueryString["minid"] != null)
            {
                minId = Request.QueryString["minid"].ToString();
            }

            if (minId != "0")
            {

                DropDownList3.SelectedValue = Request.QueryString["ti1"].ToString();
                TextBox1.Text = Request.QueryString["ti2"].ToString();
                txFDate.Value = Request.QueryString["ti3"].ToString();
                txTDate.Value = Request.QueryString["ti4"].ToString();

                cmd = "select count(*) as d from Anjianinfo2 where " + Server.UrlDecode(minId) + "";
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

            Bind3();

        }

    }
    public void Bind3()
    {

        if (TextBox1.Text == "")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sqlstr;

            sqlstr = "select  weituodanwei, bianhao, state,shenqingbianhao,shiyanleibie,(select kf from anjianxinxi2 where taskno=anjianinfo2.rwbianhao) as kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2 where state='" + DropDownList3.SelectedValue + "'  and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by substring(rwbianhao,4,5) desc ";

            if (minId != "0")
            {
                sqlstr = "select  weituodanwei, bianhao, state,shenqingbianhao,shiyanleibie,(select kf from anjianxinxi2 where taskno=anjianinfo2.rwbianhao) as kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2  where  " + minId + " and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by substring(rwbianhao,4,5) desc ";

            }



            SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
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
        else
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();


            string sqlstr;

            sqlstr = "select  weituodanwei, bianhao, state,shenqingbianhao,shiyanleibie,(select kf from anjianxinxi2 where taskno=anjianinfo2.rwbianhao) as kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2 where state='" + DropDownList3.SelectedValue + "' and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and (state like '%" + TextBox1.Text + "%' or rwbianhao like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%') or rwbianhao in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text + "%')) order by substring(rwbianhao,4,5) desc ";

            if (minId != "0")
            {
                sqlstr = "select  weituodanwei, bianhao, state,shenqingbianhao,shiyanleibie,(select kf from anjianxinxi2 where taskno=anjianinfo2.rwbianhao) as kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2  where  " + minId + " and  state='" + DropDownList3.SelectedValue + "' and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and (state like '%" + TextBox1.Text + "%' or rwbianhao like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%') or rwbianhao in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text + "%')) order by substring(rwbianhao,4,5) desc ";

            }

            SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            con.Close();
            con.Dispose();
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            AspNetPager2.Visible = false;
        }


    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind3();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {        
            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 6);
            e.Row.Cells[2].Text = SubStr(e.Row.Cells[2].Text, 6);
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 6);
            e.Row.Cells[10].Text = SubStr(e.Row.Cells[10].Text, 6);
        }


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
       

        string dd = "";
        dd = " state='" + DropDownList3.SelectedValue + "' and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and (state like '%" + TextBox1.Text + "%' or rwbianhao like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%') or rwbianhao in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text + "%'))";
        Response.Redirect("Page.aspx?minid=" + Server.UrlEncode(dd) + "&&ti1=" + DropDownList3.SelectedValue + "&&ti2=" + TextBox1.Text.Trim() + "&&ti3=" + txFDate.Value + "&&ti4=" + txTDate.Value + "&&ti5=" + DropDownList1.SelectedValue);






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
       

    }


    protected void Button3_Click(object sender, EventArgs e)
    {
     
    }
}