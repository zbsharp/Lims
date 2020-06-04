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

public partial class TongJi_ReportTongJi : System.Web.UI.Page
{
    private int _i = 0;
    int numCount = 0;





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

            DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).AddHours(23);

            txTDate.Value = lastDay.ToString("yyyy-MM-dd");
            TimeBind();




            GridView2.DataSource = table(txFDate.Value, txTDate.Value);
            GridView2.DataBind();


        }







    }


    protected DataTable table(string a, string b)
    {




        DataTable dt = new DataTable();
        dt.Columns.Add("类别", Type.GetType("System.String"));
        dt.Columns.Add("CCC", Type.GetType("System.String"));
        dt.Columns.Add("CQC", Type.GetType("System.String"));
        dt.Columns.Add("监督", Type.GetType("System.String"));
        dt.Columns.Add("其他", Type.GetType("System.String"));
        dt.Columns.Add("合计", Type.GetType("System.String"));


        for (int yue = 0; yue < 1; yue++)
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            DataRow dr;
            dr = dt.NewRow();

            dr[0] = "数量";

            string sql31 = "select count(*) as c1 from baogao2 where convert(datetime,pizhundate) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and leibie='CCC' and state='已批准'";
            SqlCommand cmd1 = new SqlCommand(sql31, con);
            SqlDataReader dread1 = cmd1.ExecuteReader();
            if (dread1.Read())
            {
                dr[1] = dread1["c1"].ToString();
            }

            dread1.Close();


            string sql32 = "select count(*) as c1 from baogao2 where convert(datetime,pizhundate) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and leibie='CQC' and state='已批准'";
            SqlCommand cmd2 = new SqlCommand(sql32, con);
            SqlDataReader dread2 = cmd2.ExecuteReader();
            if (dread2.Read())
            {
                dr[2] = dread2["c1"].ToString();
            }

            dread2.Close();



            string sql33 = "select count(*) as c1 from baogao2 where convert(datetime,pizhundate) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and leibie='监督' and state='已批准'";
            SqlCommand cmd3 = new SqlCommand(sql33, con);
            SqlDataReader dread3 = cmd3.ExecuteReader();
            if (dread3.Read())
            {
                dr[3] = dread3["c1"].ToString();
            }

            dread3.Close();

            string sql34 = "select count(*) as c1 from baogao2 where convert(datetime,pizhundate) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and leibie='其他' and state='已批准'";
            SqlCommand cmd4 = new SqlCommand(sql34, con);
            SqlDataReader dread4 = cmd4.ExecuteReader();
            if (dread4.Read())
            {
                dr[4] = dread4["c1"].ToString();
            }

            dread4.Close();


            string sql35 = "select count(*) as c1 from baogao2 where convert(datetime,pizhundate) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and  state='已批准'";
            SqlCommand cmd5 = new SqlCommand(sql35, con);
            SqlDataReader dread5 = cmd5.ExecuteReader();
            if (dread5.Read())
            {
                dr[5] = dread5["c1"].ToString();
            }

            dread5.Close();


            dt.Rows.Add(dr);
            con.Close();
        }

        return dt;
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        string sqlstr;



        sqlstr = "select distinct leibie from baogao2 where convert(datetime,pizhundate) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and  state='已批准'";



        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        GridView2.DataSource = table(txFDate.Value, txTDate.Value);
        GridView2.DataBind();

    }



    protected void TimeBind()
    {

        string sqlstr;



        sqlstr = "select distinct leibie from baogao2 where convert(datetime,pizhundate) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and  state='已批准'";



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

    private int sum1 = 0;
    private int sum2 = 0;
    private int sum3 = 0;
    private int sum4 = 0;
    private int sum5 = 0;
    private int sum6 = 0;
   
 



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;


            string sqlzong = "select count(distinct bianhao) as xiaojine from zhujianengineer left join baogao2 on zhujianengineer.bianhao=baogao2.rwid where convert(datetime,pizhundate) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and  state='已批准' and  ( leibie='" + e.Row.Cells[0].Text.Trim() + "') and ";
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con2.Open();


            string a = "0";
            string b = "0";
            string c = "0";
            string d = "0";
            string ee = "0";
            string mm = "0";
         
            string sqla = sqlzong + " bumen='EMC射频部' group by bumen";
            SqlDataAdapter ada = new SqlDataAdapter(sqla, con2);
            DataSet dsa = new DataSet();
            ada.Fill(dsa);
            DataTable dta = dsa.Tables[0];
            if (dsa.Tables[0].Rows.Count > 0)
            {
                a = dsa.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[1].Text = a.ToString();
            }



            string sqlb = sqlzong + " bumen='安规部' group by bumen";
            SqlDataAdapter adb = new SqlDataAdapter(sqlb, con2);
            DataSet dsb = new DataSet();
            adb.Fill(dsb);
            if (dsb.Tables[0].Rows.Count > 0)
            {
                b = dsb.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[2].Text = b.ToString();
            }



            string sqlc = sqlzong + " bumen='新能源部' group by bumen";
            SqlDataAdapter adc = new SqlDataAdapter(sqlc, con2);
            DataSet dsc = new DataSet();
            adc.Fill(dsc);
            if (dsc.Tables[0].Rows.Count > 0)
            {
                c = dsc.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[3].Text = c.ToString();
            }


            string sqld = sqlzong + " bumen='仪器校准部' group by bumen";
            SqlDataAdapter add = new SqlDataAdapter(sqld, con2);
            DataSet dsd = new DataSet();
            add.Fill(dsd);
            if (dsd.Tables[0].Rows.Count > 0)
            {
                d = dsd.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[4].Text = d.ToString();
            }


            string sqlm = sqlzong + " bumen='化学部' group by bumen";
            SqlDataAdapter adm = new SqlDataAdapter(sqlm, con2);
            DataSet dsm = new DataSet();
            adm.Fill(dsm);
            if (dsm.Tables[0].Rows.Count > 0)
            {
                mm = dsm.Tables[0].Rows[0]["xiaojine"].ToString();
                e.Row.Cells[5].Text = mm.ToString();
            }

            //string sqlm3 = sqlzong + " bumen='客户服务部' group by bumen";
            //SqlDataAdapter adm3 = new SqlDataAdapter(sqlm3, con2);
            //DataSet dsm3 = new DataSet();
            //adm3.Fill(dsm3);
            //if (dsm3.Tables[0].Rows.Count > 0)
            //{
            //    mm1 = dsm3.Tables[0].Rows[0]["xiaojine"].ToString();
            //    e.Row.Cells[6].Text = mm1.ToString();
            //}



            con2.Close();
            int zz = Convert.ToInt32(a) + Convert.ToInt32(b) + Convert.ToInt32(c) + Convert.ToInt32(d) + Convert.ToInt32(ee);

            int my = Convert.ToInt32(a) + Convert.ToInt32(b) + Convert.ToInt32(c) + Convert.ToInt32(d) + Convert.ToInt32(mm);
           // e.Row.Cells[1].Text = my.ToString();

            //if (zz > 0)
            //{
            //    e.Row.Cells[5].Text = zz.ToString();
            //}
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
          


            sum1 += Convert.ToInt32(e.Row.Cells[1].Text);
            sum2 += Convert.ToInt32(e.Row.Cells[2].Text);
            sum3 += Convert.ToInt32(e.Row.Cells[3].Text);
            sum4 += Convert.ToInt32(e.Row.Cells[4].Text);
            sum5 += Convert.ToInt32(e.Row.Cells[5].Text);
       
        
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "合计：";

            e.Row.Cells[1].Text = sum1.ToString();
            e.Row.Cells[2].Text = sum2.ToString();
            e.Row.Cells[3].Text = sum3.ToString();
            e.Row.Cells[4].Text = sum4.ToString();
            e.Row.Cells[5].Text = sum5.ToString();
         
    

            e.Row.Cells[1].ForeColor = Color.Blue;
            e.Row.Cells[2].ForeColor = Color.Blue;
            e.Row.Cells[3].ForeColor = Color.Blue;
            e.Row.Cells[4].ForeColor = Color.Blue;
            e.Row.Cells[5].ForeColor = Color.Blue;
       
        


            //int toLeft =  numCount-2;
            //int numCols = GridView1.Rows[0].Cells.Count; for (int i = 0; i < toLeft; i++)
            //{
            //    GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
            //    for (int j = 0; j < numCols; j++)
            //    {
            //        TableCell cell = new TableCell(); cell.Text = "&nbsp;";
            //        row.Cells.Add(cell);
            //    } 
            //    GridView1.Controls[0].Controls.AddAt(numCount + 1 + i, row);
            //}

           // TableCellCollection tcc = e.Row.Cells;
            //this.GridViewProjectGroupQuatityView.FooterRow.Cells.Add(new TableCell());
           // tcc.Clear();

            //e.Row.Cells.Add(new TableCell());

            //e.Row.Cells[0].Text = "汇总:";
            //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;

            //e.Row.Cells.Add(new TableCell());

            //e.Row.Cells[1].Text = sum1.ToString();//

            //e.Row.Cells.Add(new TableCell());

            //e.Row.Cells[2].Text = sum2.ToString();//

            //e.Row.Cells.Add(new TableCell());

            //e.Row.Cells[3].Text = sum3.ToString();//

            //e.Row.Cells.Add(new TableCell());

            //e.Row.Cells[4].Text = sum4.ToString();//

            //e.Row.Cells.Add(new TableCell());

            //e.Row.Cells[5].Text = sum5.ToString();//

            //e.Row.Cells.Add(new TableCell());

            //e.Row.Cells[6].Text = sum6.ToString();//

       




      


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