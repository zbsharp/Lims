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

public partial class Quotation_YiShouLiDai : System.Web.UI.Page
{

    private int _i = 0;
    protected string ys = "";
    protected StringBuilder strSql = new StringBuilder();

    protected StringBuilder strSql2 = new StringBuilder();
    private string minId = "0";
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Request.QueryString["ys"] != null)
        {
            ys = Request.QueryString["ys"].ToString();
        }
        Label3.Visible = true;

        strSql2.Append("select state as state1,");
        strSql2.Append("(select top 1 name from ZhuJianEngineer where bianhao=anjianinfo2.rwbianhao ) as name1,");
        strSql2.Append("(select top 1 fenpainame from anjianinfo where tijiaobianhao=Anjianinfo2.bianhao and type='是') as fenpainame,");
        strSql2.Append("* from Anjianinfo2 ");


        strSql.Append("select top 200 state  as state1,");
        strSql.Append("(select top 1 name from ZhuJianEngineer where bianhao=anjianinfo2.rwbianhao ) as name1,");
        strSql.Append("(select top 1 fenpainame from anjianinfo where tijiaobianhao=Anjianinfo2.bianhao and type='是') as fenpainame,");
        strSql.Append("* from Anjianinfo2 ");

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
                DropDownList2.SelectedValue = Request.QueryString["ti5"].ToString();
                DropDownList1.SelectedValue = Request.QueryString["ti1"].ToString();
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
                    AspNetPager1.RecordCount = Convert.ToInt32(dr["d"].ToString());

                    if (AspNetPager1.RecordCount == 0)
                    {
                        Label3.Text = "当前结果记录数:" + 1;
                    }
                    else
                    {
                        Label3.Text = "当前结果记录数:" + AspNetPager1.RecordCount.ToString();
                    }
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
                    AspNetPager1.RecordCount = Convert.ToInt32(dr["d"].ToString());
                    // Label3.Text = "当前结果记录数:200";
                }
                con.Close();
            }



            TimeBind();
        }
        
        Button3.Visible = false;

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text.Trim();
        string dd = "";

        if (DropDownList2.SelectedValue == "否")
        {

            if (this.TextBox1.Text.Trim() == "")
            {
                dd = " where  state='完成' and rwbianhao not in (select rwbh from invoice) and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'";
                Response.Redirect("YiShouLiDai.aspx?minid=" + Server.UrlEncode(dd) + "&&ti1=" + DropDownList1.SelectedValue + "&&ti2=" + TextBox1.Text.Trim() + "&&ti3=" + txFDate.Value + "&&ti4=" + txTDate.Value + "&&ti5=" + DropDownList2.SelectedValue);

            }
            else
            {



                if (DropDownList1.SelectedValue == "kehuname")
                {
                    dd = "( fukuandanwei like '%" + ChooseValue + "%' or weituodanwei like '%" + ChooseValue + "%' or shengchandanwei like '%" + ChooseValue + "%' or kehuid in (select kehuid from customer where customname like '%" + ChooseValue + "%'))  and state='完成' and rwbianhao not in (select rwbh from invoice) and (  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "')";

                }

                else
                {
                    dd = "( " + DropDownList1.SelectedValue + " like '%" + ChooseValue + "%' and state='完成' and rwbianhao not in (select rwbh from invoice) and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "')";

                }

                Response.Redirect("YiShouLiDai.aspx?minid=" + Server.UrlEncode(dd) + "&&ti1=" + DropDownList1.SelectedValue + "&&ti2=" + TextBox1.Text.Trim() + "&&ti3=" + txFDate.Value + "&&ti4=" + txTDate.Value + "&&ti5=" + DropDownList2.SelectedValue);

            }
        }
        else
        {
            if (this.TextBox1.Text.Trim() == "")
            {
                dd = " where  state='完成' and rwbianhao  in (select rwbh from invoice) and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'";
                Response.Redirect("YiShouLiDai.aspx?minid=" + Server.UrlEncode(dd) + "&&ti1=" + DropDownList1.SelectedValue + "&&ti2=" + TextBox1.Text.Trim() + "&&ti3=" + txFDate.Value + "&&ti4=" + txTDate.Value + "&&ti5=" + DropDownList2.SelectedValue);

            }
            else
            {



                if (DropDownList1.SelectedValue == "kehuname")
                {
                    dd = "( fukuandanwei like '%" + ChooseValue + "%' or weituodanwei like '%" + ChooseValue + "%' or shengchandanwei like '%" + ChooseValue + "%' or kehuid in (select kehuid from customer where customname like '%" + ChooseValue + "%'))  and state='完成' and rwbianhao  in (select rwbh from invoice) and (  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "')";

                }

                else
                {
                    dd = "( " + DropDownList1.SelectedValue + " like '%" + ChooseValue + "%' and state='完成' and rwbianhao  in (select rwbh from invoice) and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "')";

                }

                Response.Redirect("YiShouLiDai.aspx?minid=" + Server.UrlEncode(dd) + "&&ti1=" + DropDownList1.SelectedValue + "&&ti2=" + TextBox1.Text.Trim() + "&&ti3=" + txFDate.Value + "&&ti4=" + txTDate.Value+"&&ti5="+DropDownList2.SelectedValue);

            }
        }



    }

   

    protected void TimeBind()
    {

        string sqlstr;

        if (DropDownList2.SelectedValue == "否")
        {
            sqlstr = strSql + "where  state='完成' and rwbianhao not in (select rwbh from invoice) and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by convert(datetime,beizhu3) desc ";

            if (minId != "0")
            {
                sqlstr = strSql2 + "where  " + minId + "  and rwbianhao not in (select rwbh from invoice) and state='完成' and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by convert(datetime,beizhu3) desc ";

            }
        }
        else
        {
            sqlstr = strSql + "where  state='完成' and rwbianhao  in (select rwbh from invoice) and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by convert(datetime,beizhu3) desc ";

            if (minId != "0")
            {
                sqlstr = strSql2 + "where  " + minId + "  and rwbianhao  in (select rwbh from invoice) and state='完成' and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by convert(datetime,beizhu3) desc ";

            }
        }


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

            //LinkButton LinkBtn_DetailInfo2 = (LinkButton)e.Row.FindControl("LinkButton6");
            //if (limit1("取消受理"))
            //{

            //}
            //else
            //{
            //    LinkBtn_DetailInfo2.Enabled = false;
            //    LinkBtn_DetailInfo2.ForeColor = Color.DarkGray;

            //}

            e.Row.Cells[5].Text = SubStr(e.Row.Cells[5].Text, 10);
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 10);
            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 10);
            MyExcutSql ext = new MyExcutSql();
            e.Row.Cells[8].Text = ext.Eng(e.Row.Cells[1].Text);

            if (ys == "1")
            {
                e.Row.Cells[17].Visible = false;
            }

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Random seed = new Random();
        Random randomNum = new Random(seed.Next());
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string shoufeiid = randomNum.Next().ToString() + DateTime.Now.ToString("yyyyMMdd_hhmmss");
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            if (hzf.Checked)
            {
                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                string sql2 = "update Anjianxinxi2 set bianhaoone='" + shoufeiid + "' where id='" + sid + "'";
                SqlCommand com2 = new SqlCommand(sql2, con);
                com2.ExecuteNonQuery();
            }
        }
        con.Close();
        Response.Redirect("~/Income/InvoiceAdd.aspx?ran=" + shoufeiid);
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        if (ys == "1")
        {
            Button3.Visible = false;
        }
        else
        {
            TimeBind();
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