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

public partial class TongJi_KeHuQianKuan : System.Web.UI.Page
{
    private int _i = 0;
    protected string str = "";
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
            GridView1.ShowFooter = false;
        }
      

    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        string sqlstr;
        AspNetPager1.Visible = false;

        GridView1.ShowFooter = true;
        string  str = "select distinct kehuid, (select top 1 class from customer where kehuid =invoice.kehuid) as class,(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname, sum(feiyong) as yins,(select sum(xiaojine) from cashin2 where kehuid=invoice.kehuid and dengji2 in (select inid from invoice where filltime between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "')  group by kehuid) as yis  from invoice ";

        if (DropDownList1.SelectedValue == "1")
        {
            sqlstr = str + " where filltime between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "' and kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text.Trim() + "%') group by kehuid order by sum(feiyong)  desc  ";
        }
        else
        {
            sqlstr = str + " where filltime between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "' group by kehuid order by sum(feiyong)  desc  ";
 
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
        string str = "select distinct kehuid, (select top 1 class from customer where kehuid =invoice.kehuid) as class,(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname, sum(feiyong) as yins,(select sum(xiaojine) from cashin2 where kehuid=invoice.kehuid and dengji2 in (select inid from invoice where filltime between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "')  group by kehuid) as yis  from invoice ";

        string sqlstr;

        sqlstr = str + " where filltime between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "' group by kehuid order by sum(feiyong)  desc  ";

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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");


            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;






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
            sum1 += Convert.ToDecimal(e.Row.Cells[1].Text);
            sum2 += Convert.ToDecimal(e.Row.Cells[2].Text);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "小计：";

            e.Row.Cells[1].Text = sum1.ToString();
            e.Row.Cells[2].Text = sum2.ToString();
            e.Row.Cells[0].ForeColor = Color.Blue;
            e.Row.Cells[1].ForeColor = Color.Blue;

            e.Row.Cells[2].ForeColor = Color.Blue;

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
}