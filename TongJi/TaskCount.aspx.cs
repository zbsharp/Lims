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

public partial class TongJi_TaskCount : System.Web.UI.Page
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
       
        GridView1.DataSource = table(txFDate.Value,txTDate.Value);
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
           
            sum1 += Convert.ToInt32(e.Row.Cells[1].Text);
            sum2 += Convert.ToInt32(e.Row.Cells[2].Text);
            sum3 += Convert.ToInt32(e.Row.Cells[3].Text);
            sum4 += Convert.ToInt32(e.Row.Cells[4].Text);
            sum5 += Convert.ToInt32(e.Row.Cells[5].Text);
            sum6 += Convert.ToInt32(e.Row.Cells[6].Text);
            sum7 += Convert.ToInt32(e.Row.Cells[7].Text);
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

            e.Row.Cells[1].ForeColor = Color.Blue;
            e.Row.Cells[2].ForeColor = Color.Blue;
            e.Row.Cells[3].ForeColor = Color.Blue;
            e.Row.Cells[4].ForeColor = Color.Blue;
            e.Row.Cells[5].ForeColor = Color.Blue;
            e.Row.Cells[6].ForeColor = Color.Blue;
            e.Row.Cells[7].ForeColor = Color.Blue;
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

    protected DataTable table(string a,string b)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

       
        string sql = "select * from UserDepa where departmentid='12' or departmentid='13' or departmentid='15' or departmentid='16' or departmentid='17' or departmentid='9'";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con);


        DataSet ds = new DataSet();


        ad.Fill(ds);

        int dtcount=ds.Tables[0].Rows.Count;

        DataTable dt = new DataTable();
        dt.Columns.Add("类别", Type.GetType("System.String"));

        for (int z = 0; z < dtcount; z++)
        {

            dt.Columns.Add(ds.Tables[0].Rows[z]["name"].ToString(), Type.GetType("System.String"));
           

        }

        dt.Columns.Add("合计", Type.GetType("System.String"));

        string sql2 = "select * from renzheng";

        SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);


        DataSet ds2 = new DataSet();


        ad2.Fill(ds2);
        int dtcount2 = ds2.Tables[0].Rows.Count;

        for (int yue = 0; yue < dtcount2; yue++)
        {


            DataRow dr;
            dr = dt.NewRow();

            for (int c = 1; c < dt.Columns.Count-1; c++)
            {
                dr[0] = ds2.Tables[0].Rows[yue]["name"].ToString();

                string sql3 = "select count(*) as c1 from view_taskcount where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and bumen='" + dt.Columns[c].ToString() + "' and shiyanleibie='" + ds2.Tables[0].Rows[yue]["name"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(sql3, con);
                SqlDataReader dread = cmd.ExecuteReader();
                if (dread.Read())
                {
                    dr[c] = dread["c1"].ToString();
                }

                dread.Close();
            }

            string sql31 = "select count(*) as c1 from anjianinfo2 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and shiyanleibie='" + ds2.Tables[0].Rows[yue]["name"].ToString() + "'";
            SqlCommand cmd1 = new SqlCommand(sql31, con);
            SqlDataReader dread1 = cmd1.ExecuteReader();
            if (dread1.Read())
            {
                dr[dt.Columns.Count-1] = dread1["c1"].ToString();
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