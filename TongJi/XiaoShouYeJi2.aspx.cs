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

public partial class TongJi_XiaoShouYeJi2 : System.Web.UI.Page
{
    private int _i = 0;


    void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now;
            int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            int dayspan = (-1) * weeknow + 1;
            DateTime dt2 = dt.AddMonths(1);
            //本月第一天
            txFDate.Value = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");

            DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).AddHours(23);

            txTDate.Value = lastDay.ToShortDateString();
            TimeBind();
            BindDep();
            GridView1.ShowFooter = false;
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        string sqlstr;

        AspNetPager1.Visible = false;
        GridView1.ShowFooter = true;
        string str = "select distinct kf.taskid,kf.feiyong ,cashin.pinzheng from kf left join cashin on kf.taskid=cashin.taskid where pinzheng is not null";

        if (Session["role"].ToString() == "5")
        {
            //SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //con1.Open();
            //string department="";
            //string sql1 = "select departmentname from userinfo where username='"+Session["UserName"].ToString()+"'";
            //SqlCommand cmd1 = new SqlCommand(sql1,con1);
            //SqlDataReader dr1 = cmd1.ExecuteReader();
            //if (dr1.Read())
            //{
            //    department = dr1["departmentname"].ToString();
            //}
            //dr1.Close();
            //string sql = "select * from userinfo where department='" + DropDownList1.SelectedValue + "' and username='"+Session["UserName"].ToString()+"' order by username asc ";
            //SqlDataAdapter ad2 = new SqlDataAdapter(sql, con1);
            //DataSet ds2 = new DataSet();
            //ad2.Fill(ds2);
            //DropDownList2.DataSource = ds2.Tables[0];
            //DropDownList2.DataTextField = "username";
            //DropDownList2.DataValueField = "username";
            //DropDownList2.DataBind();


            //con1.Close();
            //DropDownList1.SelectedValue = department;


            string wh = "pinzheng in (select shoufeiid from shuipiao where querenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "')";

            string whbumen = "kf. taskid in (select rwbianhao from anjianinfo2 where baojiaid in (select baojiaid from baojiabiao where responser in (select username from userinfo where departmentname='" + DropDownList1.SelectedValue + "' and username='" + Session["UserName"].ToString() + "')))";

            string whren = "kf. taskid in (select rwbianhao from anjianinfo2 where baojiaid in (select baojiaid from baojiabiao where responser in (select username from userinfo where username='" + DropDownList2.SelectedValue + "' and username='" + Session["UserName"].ToString() + "')))";

            if (DropDownList1.SelectedValue == "")
            {
                sqlstr = str + " and " + wh;
            }
            else if (DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue == "")
            {
                sqlstr = str + " and  " + whbumen + "  and " + wh;

            }
            else if (DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue != "")
            {
                sqlstr = str + " and  " + whren + "  and " + wh;

            }
            else
            {
                sqlstr = str + " and " + wh;
            }





        }
        else
        {

            string wh = "pinzheng in (select shoufeiid from shuipiao where querenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "')";

            string whbumen = "kf. taskid in (select rwbianhao from anjianinfo2 where baojiaid in (select baojiaid from baojiabiao where responser in (select username from userinfo where departmentname='" + DropDownList1.SelectedValue + "')))";

            string whren = "kf. taskid in (select rwbianhao from anjianinfo2 where baojiaid in (select baojiaid from baojiabiao where responser in (select username from userinfo where username='" + DropDownList2.SelectedValue + "')))";

            if (DropDownList1.SelectedValue == "")
            {
                sqlstr = str + " and " + wh;
            }
            else if (DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue == "")
            {
                sqlstr = str + " and  " + whbumen + "  and " + wh;

            }
            else if (DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue != "")
            {
                sqlstr = str + " and  " + whren + "  and " + wh;

            }
            else
            {
                sqlstr = str + " and " + wh;
            }
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
    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select * from UserDepa order by name";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


        DataSet ds = new DataSet();


        ad.Fill(ds);


        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataValueField = "name";
        DropDownList1.DataTextField = "name";
        DropDownList1.DataBind();

        DropDownList1.Items.Insert(0, new ListItem("", ""));//

        con3.Close();
    }


    protected void TimeBind()
    {


        string whren = "kf. taskid in (select rwbianhao from anjianinfo2 where baojiaid in (select baojiaid from baojiabiao where responser in (select username from userinfo where username='" + DropDownList2.SelectedValue + "')))";


        string str = "";
        if (Session["role"].ToString() == "5")
        {
            str = "select distinct kf.taskid,kf.feiyong ,cashin.pinzheng from kf left join cashin on kf.taskid=cashin.taskid where pinzheng is not null";
            whren = "kf. taskid in (select rwbianhao from anjianinfo2 where baojiaid in (select baojiaid from baojiabiao where responser ='" + Session["UserName"].ToString() + "'))";

        }
        else
        {
            str = "select distinct kf.taskid,kf.feiyong ,cashin.pinzheng from kf left join cashin on kf.taskid=cashin.taskid where pinzheng is not null";
            whren = "1=1";

        }



        string sqlstr;

        string wh = "pinzheng in (select shoufeiid from shuipiao where querenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "')";


        sqlstr = str + " and " + wh + " and " + whren;



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

    private decimal sum1 = 0;
    private decimal sum2 = 0;
    private decimal sum3 = 0;
    private decimal sum4 = 0;
    private decimal sum5 = 0;
    private decimal sum6 = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {



            string pinzhen = e.Row.Cells[8].Text;
            string taskid = e.Row.Cells[2].Text;
            string kehuid = "";
            string baojiaid = "";
            string responser = "";
            string bumen = "";
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

            MyExcutSql my = new MyExcutSql();
            string sql1 = "select baojiaid,chanpinname,shiyanleibie,weituodanwei from anjianinfo2 where rwbianhao='" + taskid + "'";
            baojiaid = my.ExcutSql2(sql1, 0);

            string sql2 = "select responser,kehuid from baojiabiao where baojiaid='" + baojiaid + "'";
            responser = my.ExcutSql2(sql2, 0);
            kehuid = my.ExcutSql2(sql2, 1);

            string sql3 = "select departmentname from userinfo where username='" + responser + "'";
            bumen = my.ExcutSql2(sql3, 0);

            string sql4 = "select customname from customer where kehuid='" + kehuid + "'";
            e.Row.Cells[3].Text = my.ExcutSql2(sql4, 0);
            e.Row.Cells[0].Text = bumen;
            e.Row.Cells[1].Text = responser;
            e.Row.Cells[4].Text = my.ExcutSql2(sql1, 1);
            e.Row.Cells[5].Text = my.ExcutSql2(sql1, 2);
            e.Row.Cells[15].Text = my.ExcutSql2(sql1, 3);
            string sql5 = "select querenriqi from shuipiao where shoufeiid='" + pinzhen + "'";
            string jiesuanriqi = my.ExcutSql2(sql5, 0);
            e.Row.Cells[7].Text = jiesuanriqi == "no" ? "" : Convert.ToDateTime(jiesuanriqi).ToShortDateString();

            string sqlzong = "select  sum(jine) as xiaojine from cashin where pinzheng='" + pinzhen + "' and taskid='" + taskid + "' and ";
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con2.Open();
            string a = "0.00";
            string b = "0.00";
            string c = "0.00";
            string d = "0.00";
            string ee = "0.00";
            string mm = "0.00";
            string mmd1 = "0.00";
            string sqla = sqlzong + "beizhu3='EMC射频部' group by beizhu3,taskid,pinzheng";
            SqlDataAdapter ada = new SqlDataAdapter(sqla, con2);
            DataSet dsa = new DataSet();
            ada.Fill(dsa);
            DataTable dta = dsa.Tables[0];
            if (dsa.Tables[0].Rows.Count > 0)
            {
                a = dsa.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[9].Text = a.ToString();
            }



            string sqlb = sqlzong + " beizhu3='安规部' group by beizhu3,taskid,pinzheng";
            SqlDataAdapter adb = new SqlDataAdapter(sqlb, con2);
            DataSet dsb = new DataSet();
            adb.Fill(dsb);
            if (dsb.Tables[0].Rows.Count > 0)
            {
                b = dsb.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[10].Text = b.ToString();
            }



            string sqlc = sqlzong + " beizhu3='新能源部' group by beizhu3,taskid,pinzheng";
            SqlDataAdapter adc = new SqlDataAdapter(sqlc, con2);
            DataSet dsc = new DataSet();
            adc.Fill(dsc);
            if (dsc.Tables[0].Rows.Count > 0)
            {
                c = dsc.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[11].Text = c.ToString();
            }


            string sqld = sqlzong + " beizhu3='仪器校准部' group by beizhu3,taskid,pinzheng";
            SqlDataAdapter add = new SqlDataAdapter(sqld, con2);
            DataSet dsd = new DataSet();
            add.Fill(dsd);
            if (dsd.Tables[0].Rows.Count > 0)
            {
                d = dsd.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[12].Text = d.ToString();
            }


            string sqlm = sqlzong + " beizhu3='化学部' group by beizhu3,taskid,pinzheng";
            SqlDataAdapter adm = new SqlDataAdapter(sqlm, con2);
            DataSet dsm = new DataSet();
            adm.Fill(dsm);
            if (dsm.Tables[0].Rows.Count > 0)
            {
                mm = dsm.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[13].Text = mm.ToString();
            }

            string sqld1 = sqlzong + " beizhu3='代付' group by beizhu3,taskid,pinzheng";
            SqlDataAdapter add1 = new SqlDataAdapter(sqld1, con2);
            DataSet dsd1 = new DataSet();
            add1.Fill(dsd1);
            if (dsd1.Tables[0].Rows.Count > 0)
            {
                mmd1 = dsd1.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[14].Text = mmd1.ToString();
            }


            con2.Close();
            decimal zz = Convert.ToDecimal(a) + Convert.ToDecimal(b) + Convert.ToDecimal(c) + Convert.ToDecimal(d) + Convert.ToDecimal(ee) + Convert.ToDecimal(mm);

            if (zz > 0)
            {
                e.Row.Cells[6].Text = zz.ToString();
            }

            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 10);
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 10);



        }

        if (e.Row.RowIndex >= 0)
        {

            string qq = e.Row.Cells[9].Text;
            if (e.Row.Cells[9].Text == "" || e.Row.Cells[9].Text == "&nbsp;")
            {
                e.Row.Cells[9].Text = "0";
            }

            if (e.Row.Cells[10].Text == "" || e.Row.Cells[10].Text == "&nbsp;")
            {
                e.Row.Cells[10].Text = "0";
            }
            if (e.Row.Cells[11].Text == "" || e.Row.Cells[11].Text == "&nbsp;")
            {
                e.Row.Cells[11].Text = "0";
            }
            if (e.Row.Cells[12].Text == "" || e.Row.Cells[12].Text == "&nbsp;")
            {
                e.Row.Cells[12].Text = "0";
            }
            if (e.Row.Cells[13].Text == "" || e.Row.Cells[13].Text == "&nbsp;")
            {
                e.Row.Cells[13].Text = "0";
            }
            if (e.Row.Cells[14].Text == "" || e.Row.Cells[14].Text == "&nbsp;")
            {
                e.Row.Cells[14].Text = "0";
            }

            sum1 += Convert.ToDecimal(e.Row.Cells[9].Text);
            sum2 += Convert.ToDecimal(e.Row.Cells[10].Text);
            sum3 += Convert.ToDecimal(e.Row.Cells[11].Text);
            sum4 += Convert.ToDecimal(e.Row.Cells[12].Text);
            sum5 += Convert.ToDecimal(e.Row.Cells[13].Text);
            sum6 += Convert.ToDecimal(e.Row.Cells[14].Text);



        }

        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = "合计：";

            e.Row.Cells[9].Text = sum1.ToString();
            e.Row.Cells[10].Text = sum2.ToString();
            e.Row.Cells[11].Text = sum3.ToString();
            e.Row.Cells[12].Text = sum4.ToString();
            e.Row.Cells[13].Text = sum5.ToString();
            e.Row.Cells[14].Text = sum6.ToString();
        }

    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        TimeBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {




        DisableControls(GridView1);

        Response.ClearContent();

        Response.AddHeader("content-disposition", "attachment; filename=IncomeList" + DateTime.Now.ToShortDateString() + ".xls");

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

    public string SubStr(string sString, int nLeng)
    {
        if (sString.Length <= nLeng)
        {
            return sString;
        }
        string sNewStr = sString.Substring(0, nLeng);

        return sNewStr;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo where department='" + DropDownList1.SelectedValue + "' order by username asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataTextField = "username";
        DropDownList2.DataValueField = "username";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("", ""));//

        con.Close();
    }
}