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

public partial class TongJi_ReportCount : System.Web.UI.Page
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

            Bind3();
        }

    }
    public void Bind3()
    {

        GridView1.DataSource = table(txFDate.Value, txTDate.Value);
        GridView1.DataBind();


    }


  
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

      
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