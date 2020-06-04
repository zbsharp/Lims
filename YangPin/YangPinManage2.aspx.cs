﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.IO;
using Common;
using DBBLL;
using DBTable;
public partial class YangPinManage2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now;
            int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            int dayspan = (-1) * weeknow + 1;
            DateTime dt2 = dt.AddMonths(1);
            //本月第一天
            txFDate.Value = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");

            //本月最后一天
            txTDate.Value = dt2.AddDays(-dt.Day).ToString("yyyy-MM-dd");


            if (Session["role"].ToString() == "7"|| Session["role"].ToString() == "13" || Session["role"].ToString() == "23" || Session["role"].ToString() == "17" || Session["role"].ToString() == "18" || Session["role"].ToString() == "14")
            {
                Bind1();
            }
            else
            {
                Bind2();
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string begintime;
        string endtime;
        int ChooseNo = int.Parse(DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;
        begintime = txFDate.Value;
        endtime = Convert.ToDateTime(txTDate.Value).AddDays(1).ToShortDateString();

        if (Session["role"].ToString() == "7"|| Session["role"].ToString() == "13" || Session["role"].ToString() == "23" || Session["role"].ToString() == "17" || Session["role"].ToString() == "18" || Session["role"].ToString() == "14")
        {
            TimeBind(begintime, endtime, ChooseNo, ChooseValue);
        }
        else
        {
            ResponserTimeBind(begintime, endtime, ChooseNo, ChooseValue);
        }
        AspNetPager2.Visible = false;
    }
    protected void TimeBind(string a, string b, int c, string d)
    {
        string ds1 = a;
        string ds2 = b;
        int ChooseID = c;
        string ChooseValue = d;

        string sqlstr;
        string sqlx;
        switch (ChooseID)
        {
            case 0:
                sqlstr = "select * from YangPin2 where state='借出' and receivetime between '" + ds1 + "' and '" + ds2 + "' order by id asc";
                break;
            case 1:
                sqlstr = "select * from YangPin2 where state='借出' and receivetime between '" + ds1 + "' and '" + ds2 + "' and kehuname like '%" + ChooseValue + "%' order by id asc";
                break;
            case 2:
                sqlstr = "select * from YangPin2 where state='借出' and receivetime between '" + ds1 + "' and '" + ds2 + "' and name like '%" + ChooseValue + "%' order by id asc";
                break;
            case 3:
                sqlstr = "select * from YangPin2 where state='借出' and receivetime between '" + ds1 + "' and '" + ds2 + "' and sampleid ='" + ChooseValue + "' order by id asc";
                break;
            default:
                sqlstr = "select * from YangPin2 where state='借出' and receivetime between '" + ds1 + "' and '" + ds2 + "' order by id asc";
                break;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }
    protected void ResponserTimeBind(string a, string b, int c, string d)
    {
        string ds1 = a;
        string ds2 = b;
        int ChooseID = c;
        string ChooseValue = d;

        string sqlstr;
        string sqlx;
        switch (ChooseID)
        {
            case 0:
                sqlstr = "select * from YangPin2 where state='借出' and receivetime between '" + ds1 + "' and '" + ds2 + "' order by id asc";
                break;
            case 1:
                sqlstr = "select * from YangPin2 where state='借出' and receivetime between '" + ds1 + "' and '" + ds2 + "' and kehuname like '%" + ChooseValue + "%' order by id asc";
                break;
            case 2:
                sqlstr = "select * from YangPin2 where state='借出' and receivetime between '" + ds1 + "' and '" + ds2 + "' and name like '%" + ChooseValue + "%' order by id asc";
                break;
            case 3:
                sqlstr = "select * from YangPin2 where state='借出' and receivetime between '" + ds1 + "' and '" + ds2 + "' and sampleid ='" + ChooseValue + "' order by id asc";
                break;
            default:
                sqlstr = "select * from YangPin2 where state='借出' and receivetime between '" + ds1 + "' and '" + ds2 + "' order by id asc";
                break;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "chakan")
        {
            Response.Redirect("YangPinSee.aspx?yangpinid=" + sid);
        }
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        if (Session["role"].ToString() == "7"|| Session["role"].ToString() == "13" || Session["role"].ToString() == "23" || Session["role"].ToString() == "17" || Session["role"].ToString() == "18" || Session["role"].ToString() == "14")
        {
            Bind1();
        }
        else
        {
            Bind2();
        }
    }

    protected void Bind1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from YangPin2 where state='借出' order by id asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager2.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }

    protected void Bind2()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from YangPin2 where state='借出' order by id asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager2.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        DisableControls(GridView1);

        Response.ClearContent();

        //Response.AddHeader("content-disposition", "attachment; filename=qiandanlist" + DateTime.Now.ToShortDateString() + ".xls");

        Response.ContentType = "application/ms-excel";

        Response.Charset = "GB2312";
        Response.AddHeader("Content-Disposition", "attachment;   filename=" + System.Web.HttpUtility.UrlEncode("样品管理借出列表" + DateTime.Now, System.Text.Encoding.UTF8) + ".xls");//这样的话，可以设置文件名为中文，且文件名不会乱码。其实就是将汉字转换成UTF8      


        StringWriter sw = new StringWriter();

        HtmlTextWriter htw = new HtmlTextWriter(sw);

        GridView1.RenderControl(htw);

        Response.Write(sw.ToString());

        Response.End();
    }

    private void DisableControls(Control gv)
    {

        LinkButton lb = new LinkButton();

        Literal l = new Literal();

        string name = String.Empty;

        for (int i = 0; i < gv.Controls.Count; i++)
        {

            if (gv.Controls[i].GetType() == typeof(LinkButton))
            {

                l.Text = (gv.Controls[i] as LinkButton).Text;

                gv.Controls.Remove(gv.Controls[i]);

                gv.Controls.AddAt(i, l);

            }

            else if (gv.Controls[i].GetType() == typeof(DropDownList))
            {

                l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;

                gv.Controls.Remove(gv.Controls[i]);

                gv.Controls.AddAt(i, l);

            }



            if (gv.Controls[i].HasControls())
            {

                DisableControls(gv.Controls[i]);

            }

        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
}
