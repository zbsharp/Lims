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

public partial class Report_ReportDepLeiXing : System.Web.UI.Page
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

            txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();
            BindDep();
            Bind3();
        }

    }


    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select * from UserDepa where departmentid='10' or departmentid='11'  or  departmentid='12' or departmentid='13' or departmentid='14' or departmentid='15'";
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


    public void Bind3()
    {

        GridView1.DataSource = table(txFDate.Value, txTDate.Value);
        GridView1.DataBind();


    }


    private int sum1 = 0;
    private int sum2 = 0;
    private int sum3 = 0;
    private int sum4 = 0;
    private int sum5 = 0;
    private int sum6 = 0;
    private int sum7 = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[2].Text == "" || e.Row.Cells[2].Text == "&nbsp;")
            {
                e.Row.Cells[2].Text = "0";
            }


            sum1 += Convert.ToInt32(e.Row.Cells[2].Text);


        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "合计：";

            e.Row.Cells[2].Text = sum1.ToString();



            e.Row.Cells[2].ForeColor = Color.Blue;


        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        GridView1.DataSource = table(txFDate.Value, txTDate.Value);
        GridView1.DataBind();



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

    protected DataTable table(string a, string b)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        //string sql = "select username from userinfo  where dutyid='4' order by username";


        //SqlDataAdapter ad = new SqlDataAdapter(sql, con);


        //DataSet ds = new DataSet();


        //ad.Fill(ds);

        //int dtcount = ds.Tables[0].Rows.Count;

        DataTable dt = new DataTable();
        dt.Columns.Add("错误类别", Type.GetType("System.String"));
        dt.Columns.Add("错误内容", Type.GetType("System.String"));

        //for (int z = 0; z < dtcount; z++)
        //{

        //    dt.Columns.Add(ds.Tables[0].Rows[z]["username"].ToString(), Type.GetType("System.String"));


        //}

        dt.Columns.Add("合计", Type.GetType("System.String"));

        string sql2 = "select * from UserChuCuo where wenyuan in (select leirong from BaoGaoChaCuo  where convert(datetime,time) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "'  and bumen='" + DropDownList1.SelectedValue + "') and  name in (select fenlei from BaoGaoChaCuo  where convert(datetime,time) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "'  and bumen='" + DropDownList1.SelectedValue + "') order by name";

        SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);


        DataSet ds2 = new DataSet();


        ad2.Fill(ds2);
        int dtcount2 = ds2.Tables[0].Rows.Count;

        for (int yue = 0; yue < dtcount2; yue++)
        {

            DataRow dr;
            dr = dt.NewRow();

            dr[1] = ds2.Tables[0].Rows[yue]["wenyuan"].ToString();
            dr[0] = ds2.Tables[0].Rows[yue]["name"].ToString();

            string sql31 = "select count(*) as c1 from BaoGaoChaCuo where convert(datetime,time) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and leirong='" + ds2.Tables[0].Rows[yue]["wenyuan"].ToString() + "' and fenlei='" + ds2.Tables[0].Rows[yue]["name"].ToString() + "' and bumen='" + DropDownList1.SelectedValue + "' group by leirong";
            SqlCommand cmd1 = new SqlCommand(sql31, con);
            SqlDataReader dread1 = cmd1.ExecuteReader();
            if (dread1.Read())
            {
                dr[2] = dread1["c1"].ToString();
            }

            dread1.Close();
            dt.Rows.Add(dr);

        }
        con.Close();
        return dt;
    }

    protected void Button1_Click(object sender, EventArgs e)
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