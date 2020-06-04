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
using System.Data.OleDb;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Web.Services;
using Common;
using System.IO;
using System.Text;
using System.Drawing;


public partial class TongJi_KeFuYeWu : System.Web.UI.Page
{

    private int _i = 0;
    protected string str = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        //  str = "select  rw sum(ceshifeikf.feiyong) as feiyong,taskid,kehuid from CeShiFeiKf where kehuid !='' group by taskid,kehuid";


        if (!IsPostBack)
        {

            DateTime dt = DateTime.Now;
            int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            int dayspan = (-1) * weeknow + 1;
            DateTime dt2 = dt.AddMonths(1);
            //本月第一天
            txFDate.Value = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");

            //本月最后一天
            DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).AddHours(23);

            txTDate.Value = lastDay.ToShortDateString();

            str = "select distinct kehuid, (select top 1 class from customer where kehuid =anjianinfo2.kehuid) as class,count(rwbianhao) as rws,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2  where  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "'  group by kehuid";
            
            Bind3();
        }

    }
    public void Bind3()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = str + "";


        SqlDataAdapter da = new SqlDataAdapter(sql, con);
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

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind3();
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

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string kehuid = "";
            string sqlkehu = "select kehuid from customer where customname='"+e.Row.Cells[0].Text+"'";
            SqlCommand cmdkehu = new SqlCommand(sqlkehu,con);
            SqlDataReader drkehu = cmdkehu.ExecuteReader();
            if (drkehu.Read())
            {
                kehuid = drkehu["kehuid"].ToString();
            }

            drkehu.Close();
            string sqlk3 = "select sum(feiyong)as feiyong from CeShiFeiKf where kehuid='" + kehuid + "' and taskid in (select rwbianhao from anjianinfo2 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "') group by kehuid";
            SqlCommand cmdk3 = new SqlCommand(sqlk3, con);
            SqlDataReader drk3 = cmdk3.ExecuteReader();
            if (drk3.Read())
            {
                e.Row.Cells[2].Text = Math.Round(Convert.ToDecimal(drk3["feiyong"]), 2).ToString();            
            }
            drk3.Close();

            string sqlk1 = "select sum(xiaojine) as xiaojine from cashin2 where kehuid='" + kehuid + "' and taskid in (select rwbianhao from anjianinfo2 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "') group by kehuid";
            SqlCommand cmdk1 = new SqlCommand(sqlk1, con);
            SqlDataReader drk1 = cmdk1.ExecuteReader();
            if (drk1.Read())
            {
                e.Row.Cells[3].Text = drk1["xiaojine"].ToString();
            }

            drk1.Close();
            int a = Convert.ToInt32(TextBox2.Text);
            string sqlk4 = "select sum(feiyong)as feiyong from CeShiFeiKf where kehuid='" + kehuid + "' and taskid in (select rwbianhao from anjianinfo2 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value).AddMonths(-a) + "' and '" + Convert.ToDateTime(txTDate.Value).AddMonths(-a) + "') group by kehuid";
            SqlCommand cmdk4 = new SqlCommand(sqlk4, con);
            SqlDataReader drk4 = cmdk4.ExecuteReader();
            if (drk4.Read())
            {
                e.Row.Cells[4].Text = Math.Round(Convert.ToDecimal(drk4["feiyong"]), 2).ToString();
            }
            drk4.Close();

            string sqlk5 = "select sum(xiaojine) as xiaojine from cashin2 where kehuid='" + kehuid + "' and taskid in (select rwbianhao from anjianinfo2 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value).AddMonths(-a) + "' and '" + Convert.ToDateTime(txTDate.Value).AddMonths(-a) + "') group by kehuid";
            SqlCommand cmdk5 = new SqlCommand(sqlk5, con);
            SqlDataReader drk5 = cmdk5.ExecuteReader();
            if (drk5.Read())
            {
                e.Row.Cells[5].Text = drk5["xiaojine"].ToString();
            }

            drk5.Close();

            string sqlk6 = "select top 1 xiadariqi from anjianinfo2  where kehuid='" + kehuid + "' order by id asc ";
            SqlCommand cmdk6 = new SqlCommand(sqlk6, con);
            SqlDataReader drk6 = cmdk6.ExecuteReader();
            if (drk6.Read())
            {
                e.Row.Cells[6].Text = drk6["xiadariqi"].ToString();
            }

            con.Close();

        }


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";
    

        string ss1="kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%')";

        if (TextBox1.Text.Trim() == "")
        {
            sql = "select distinct kehuid,(select top 1 class from customer where kehuid =anjianinfo2.kehuid) as class,count(rwbianhao) as rws,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2  where  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "'  group by kehuid order by count(rwbianhao)";
        }
        else
        {
            sql = "select distinct kehuid,(select top 1 class from customer where kehuid =anjianinfo2.kehuid) as class,count(rwbianhao) as rws,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2  where  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "'  and " + ss1 + " group by kehuid order by count(rwbianhao)";
        }


       


        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        con.Close();
        con.Dispose();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


        AspNetPager2.Visible = false;
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


    protected void Button3_Click(object sender, EventArgs e)
    {



        DisableControls(GridView1);

        Response.ClearContent();

        Response.AddHeader("content-disposition", "attachment; filename=TaskCount" + DateTime.Now.ToShortDateString() + ".xls");

        Response.ContentType = "application/ms-excel";

        Response.Charset = "UTF-8";

        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");


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