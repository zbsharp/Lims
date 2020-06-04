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

public partial class TongJi_JieSuanCount : System.Web.UI.Page
{
    private int _i = 0;
    int numCount = 0;


   


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() == "admin" || Session["UserName"].ToString() == "王鹏" || Session["UserName"].ToString() == "林幸笋" || Session["UserName"].ToString() == "许芳" || Session["UserName"].ToString() == "于敦贞" || Session["UserName"].ToString() == "于可心")
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

            }
        }
        else
        {
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
 
        }

     


    


    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        string sqlstr;



        sqlstr = "select sum(feiyong) as feiyong,shiyanleibie  from kf2  where exists (select 1 from cashin where taskid=kf2.taskid and exists (select 1 from shuipiao where querenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and shoufeiid=cashin.pinzheng))  group by shiyanleibie";



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



        sqlstr = "select sum(feiyong) as feiyong,shiyanleibie  from kf2  where taskid in (select taskid from cashin where pinzheng in (select shoufeiid from shuipiao where querenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'))  group by shiyanleibie";



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

    private decimal sum1 = 0;
    private decimal sum2 = 0;
    private decimal sum3 = 0;
    private decimal sum4 = 0;
    private decimal sum5 = 0;
    private decimal sum6 = 0;
    private decimal sum7 = 0;
    private decimal sum8 = 0;
    private decimal sum9 = 0;
    private int En = 0;
    private int An = 0;
    private int Xn = 0;
    private int Jn = 0;
    private int Hn = 0;
   
   

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

      
         
         





            string sqlzong = "select  sum(jine) as xiaojine from cashin left join anjianinfo2 on cashin.taskid=anjianinfo2.rwbianhao  where  ( tichenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' ) and  ( shiyanleibie='" + e.Row.Cells[0].Text.Trim() + "') and ";
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con2.Open();




            string a = "0.00";
            string b = "0.00";
            string c = "0.00";
            string d = "0.00";
            string ee = "0.00";
            string mm = "0.00";
            string mm1 = "0.00";

            string mm2 = "0.00";
            string sqla = sqlzong + " cashin.beizhu3='EMC射频部' group by cashin.beizhu3";
            SqlDataAdapter ada = new SqlDataAdapter(sqla, con2);
            DataSet dsa = new DataSet();
            ada.Fill(dsa);
            DataTable dta = dsa.Tables[0];
            if (dsa.Tables[0].Rows.Count > 0)
            {
                a = dsa.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[2].Text = a.ToString();
            }



            string sqlb = sqlzong + " cashin.beizhu3='安规部' group by cashin.beizhu3";
            SqlDataAdapter adb = new SqlDataAdapter(sqlb, con2);
            DataSet dsb = new DataSet();
            adb.Fill(dsb);
            if (dsb.Tables[0].Rows.Count > 0)
            {
                b = dsb.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[3].Text = b.ToString();
            }



            string sqlc = sqlzong + " cashin.beizhu3='新能源部' group by cashin.beizhu3";
            SqlDataAdapter adc = new SqlDataAdapter(sqlc, con2);
            DataSet dsc = new DataSet();
            adc.Fill(dsc);
            if (dsc.Tables[0].Rows.Count > 0)
            {
                c = dsc.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[4].Text = c.ToString();
            }


            string sqld = sqlzong + " cashin.beizhu3='仪器校准部' group by cashin.beizhu3";
            SqlDataAdapter add = new SqlDataAdapter(sqld, con2);
            DataSet dsd = new DataSet();
            add.Fill(dsd);
            if (dsd.Tables[0].Rows.Count > 0)
            {
                d = dsd.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[5].Text = d.ToString();
            }


            string sqlm = sqlzong + " cashin.beizhu3='化学部' group by cashin.beizhu3";
            SqlDataAdapter adm = new SqlDataAdapter(sqlm, con2);
            DataSet dsm = new DataSet();
            adm.Fill(dsm);
            if (dsm.Tables[0].Rows.Count > 0)
            {
                mm = dsm.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[6].Text = mm.ToString();
            }

            string sqlm3 = sqlzong + " cashin.beizhu3='佛山公司' group by cashin.beizhu3";
            SqlDataAdapter adm3 = new SqlDataAdapter(sqlm3, con2);
            DataSet dsm3 = new DataSet();
            adm3.Fill(dsm3);
            if (dsm3.Tables[0].Rows.Count > 0)
            {
                mm1 = dsm3.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[7].Text = mm1.ToString();
            }

            string sqlm4 = sqlzong + " cashin.beizhu3='代付' group by cashin.beizhu3";
            SqlDataAdapter adm4 = new SqlDataAdapter(sqlm4, con2);
            DataSet dsm4 = new DataSet();
            adm4.Fill(dsm4);
            if (dsm4.Tables[0].Rows.Count > 0)
            {
                mm2 = dsm4.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[8].Text = mm2.ToString();
            }

            con2.Close();
            decimal zz = Convert.ToDecimal(a) + Convert.ToDecimal(b) + Convert.ToDecimal(c) + Convert.ToDecimal(d) + Convert.ToDecimal(ee) + Convert.ToDecimal(mm) + Convert.ToDecimal(mm1);

            decimal my = Convert.ToDecimal(a) + Convert.ToDecimal(b) + Convert.ToDecimal(c) + Convert.ToDecimal(d) + Convert.ToDecimal(mm) + Convert.ToDecimal(mm1) + Convert.ToDecimal(mm2);
            e.Row.Cells[1].Text = my.ToString();

            if (zz > 0)
            {
                e.Row.Cells[9].Text = zz.ToString();
            }
            numCount++;
           
        }


        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[1].Text == "" || e.Row.Cells[1].Text == "&nbsp;")
            {
                e.Row.Cells[1].Text = "0";
            }
            if (e.Row.Cells[2].Text == "" || e.Row.Cells[2].Text == "&nbsp;")
            {
                e.Row.Cells[2].Text = "0";
            }
            if (e.Row.Cells[3].Text == "" || e.Row.Cells[3].Text == "&nbsp;")
            {
                e.Row.Cells[3].Text = "0";
            }
            if (e.Row.Cells[4].Text == "" || e.Row.Cells[4].Text == "&nbsp;")
            {
                e.Row.Cells[4].Text = "0";
            }
            if (e.Row.Cells[5].Text == "" || e.Row.Cells[5].Text == "&nbsp;")
            {
                e.Row.Cells[5].Text = "0";
            }
            if (e.Row.Cells[6].Text == "" || e.Row.Cells[6].Text == "&nbsp;")
            {
                e.Row.Cells[6].Text = "0";
            }
            if (e.Row.Cells[7].Text == "" || e.Row.Cells[7].Text == "&nbsp;")
            {
                e.Row.Cells[7].Text = "0";
            }
            if (e.Row.Cells[8].Text == "" || e.Row.Cells[8].Text == "&nbsp;")
            {
                e.Row.Cells[8].Text = "0";
            }
            if (e.Row.Cells[9].Text == "" || e.Row.Cells[8].Text == "&nbsp;")
            {
                e.Row.Cells[9].Text = "0";
            }
            sum1 += Convert.ToDecimal(e.Row.Cells[1].Text);
            sum2 += Convert.ToDecimal(e.Row.Cells[2].Text);
            sum3 += Convert.ToDecimal(e.Row.Cells[3].Text);
            sum4 += Convert.ToDecimal(e.Row.Cells[4].Text);
            sum5 += Convert.ToDecimal(e.Row.Cells[5].Text);
            sum6 += Convert.ToDecimal(e.Row.Cells[6].Text);
            sum7 += Convert.ToDecimal(e.Row.Cells[7].Text);
            sum8 += Convert.ToDecimal(e.Row.Cells[8].Text);
            sum9 += Convert.ToDecimal(e.Row.Cells[9].Text);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "合计：";

            e.Row.Cells[1].Text = sum1.ToString();
            e.Row.Cells[2].Text = sum2.ToString();
            e.Row.Cells[3].Text = sum3.ToString();
            e.Row.Cells[4].Text = sum4.ToString();
            e.Row.Cells[5].Text = sum5.ToString();
            e.Row.Cells[6].Text = sum6.ToString();
            e.Row.Cells[7].Text = sum7.ToString();
            e.Row.Cells[8].Text = sum8.ToString();
            e.Row.Cells[9].Text = sum9.ToString();
            e.Row.Cells[1].ForeColor = Color.Blue;
            e.Row.Cells[2].ForeColor = Color.Blue;
            e.Row.Cells[3].ForeColor = Color.Blue;
            e.Row.Cells[4].ForeColor = Color.Blue;
            e.Row.Cells[5].ForeColor = Color.Blue;
            e.Row.Cells[6].ForeColor = Color.Blue;
            e.Row.Cells[8].ForeColor = Color.Blue;
            e.Row.Cells[9].ForeColor = Color.Blue;




        }

    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        
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

 

}