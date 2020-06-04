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

public partial class TongJi_TongJiKeFu : System.Web.UI.Page
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
         

            sum1 += Convert.ToInt32(e.Row.Cells[1].Text);
            sum2 += Convert.ToInt32(e.Row.Cells[2].Text);
            sum3 += Convert.ToInt32(e.Row.Cells[3].Text);
            sum4 += Convert.ToInt32(e.Row.Cells[4].Text);
            sum5 += Convert.ToInt32(e.Row.Cells[5].Text);
            sum6 += Convert.ToInt32(e.Row.Cells[6].Text);
          
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
          

            e.Row.Cells[1].ForeColor = Color.Blue;
            e.Row.Cells[2].ForeColor = Color.Blue;
            e.Row.Cells[3].ForeColor = Color.Blue;
            e.Row.Cells[4].ForeColor = Color.Blue;
            e.Row.Cells[5].ForeColor = Color.Blue;
            e.Row.Cells[6].ForeColor = Color.Blue;
           
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


        DataTable dt = new DataTable();
        dt.Columns.Add("客服", Type.GetType("System.String"));
        dt.Columns.Add("CCC(Y)", Type.GetType("System.String"));
        dt.Columns.Add("市场(Y)", Type.GetType("System.String"));
        dt.Columns.Add("合计(Y)", Type.GetType("System.String"));

        dt.Columns.Add("CCC(N)", Type.GetType("System.String"));
        dt.Columns.Add("市场(N)", Type.GetType("System.String"));
        dt.Columns.Add("合计(N)", Type.GetType("System.String"));

        string sql2 = "select * from userinfo where username in (select name from ModuleDuty where modulename='项目经理') order by username asc";
      

        SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);


        DataSet ds2 = new DataSet();


        ad2.Fill(ds2);
        int dtcount2 = ds2.Tables[0].Rows.Count;

        for (int yue = 0; yue < dtcount2; yue++)
        {


            DataRow dr;
            dr = dt.NewRow();

         
                dr[0] = ds2.Tables[0].Rows[yue]["username"].ToString();

                string sql3 = "select count(*) as c1 from anjianinfo2 where convert(datetime,beizhu3) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and kf='" + ds2.Tables[0].Rows[yue]["username"].ToString() + "' and shiyanleibie='CCC' and (state='完成' or state='关闭')";
                SqlCommand cmd = new SqlCommand(sql3, con);
                SqlDataReader dread = cmd.ExecuteReader();
                if (dread.Read())
                {
                    dr[1] = dread["c1"].ToString();
                }

                dread.Close();


                string sql4 = "select count(*) as c1 from anjianinfo2 where convert(datetime,beizhu3) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and kf='" + ds2.Tables[0].Rows[yue]["username"].ToString() + "' and shiyanleibie !='CCC' and (state='完成' or state='关闭')";
                SqlCommand cmd4 = new SqlCommand(sql4, con);
                SqlDataReader dread4 = cmd4.ExecuteReader();
                if (dread4.Read())
                {
                    dr[2] = dread4["c1"].ToString();
                }

                dread4.Close();


                string sql5 = "select count(*) as c1 from anjianinfo2 where convert(datetime,beizhu3) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and kf='" + ds2.Tables[0].Rows[yue]["username"].ToString() + "'  and (state='完成' or state='关闭')";
                SqlCommand cmd5 = new SqlCommand(sql5, con);
                SqlDataReader dread5 = cmd5.ExecuteReader();
                if (dread5.Read())
                {
                    dr[3] = dread5["c1"].ToString();
                }

                dread5.Close();


                string sql6 = "select count(*) as c1 from anjianinfo2 where  kf='" + ds2.Tables[0].Rows[yue]["username"].ToString() + "' and shiyanleibie ='CCC' and (state='进行中' or state='下达' or state='暂停')";
                SqlCommand cmd6 = new SqlCommand(sql6, con);
                SqlDataReader dread6 = cmd6.ExecuteReader();
                if (dread6.Read())
                {
                    dr[4] = dread6["c1"].ToString();
                }

                dread6.Close();


                string sql7 = "select count(*) as c1 from anjianinfo2 where  kf='" + ds2.Tables[0].Rows[yue]["username"].ToString() + "' and shiyanleibie !='CCC' and  (state='进行中' or state='下达' or state='暂停')";
                SqlCommand cmd7 = new SqlCommand(sql7, con);
                SqlDataReader dread7 = cmd7.ExecuteReader();
                if (dread7.Read())
                {
                    dr[5] = dread7["c1"].ToString();
                }

                dread7.Close();



                string sql8 = "select count(*) as c1 from anjianinfo2 where  kf='" + ds2.Tables[0].Rows[yue]["username"].ToString() + "'  and  (state='进行中' or state='下达' or state='暂停')";
                SqlCommand cmd8 = new SqlCommand(sql8, con);
                SqlDataReader dread8 = cmd8.ExecuteReader();
                if (dread8.Read())
                {
                    dr[6] = dread8["c1"].ToString();
                }

                dread8.Close();
          
          



            dt.Rows.Add(dr);

        }
       


        DataRow dr1;
        dr1 = dt.NewRow();


        dr1[0] = "其他";

        string sql31 = "select count(*) as c1 from anjianinfo2 where convert(datetime,beizhu3) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and kf='' and shiyanleibie='CCC' and (state='完成' or state='关闭')";
        SqlCommand cmd1 = new SqlCommand(sql31, con);
        SqlDataReader dread1 = cmd1.ExecuteReader();
        if (dread1.Read())
        {
            dr1[1] = dread1["c1"].ToString();
        }

        dread1.Close();


        string sql41 = "select count(*) as c1 from anjianinfo2 where convert(datetime,beizhu3) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and kf='' and shiyanleibie !='CCC' and (state='完成' or state='关闭')";
        SqlCommand cmd41 = new SqlCommand(sql41, con);
        SqlDataReader dread41 = cmd41.ExecuteReader();
        if (dread41.Read())
        {
            dr1[2] = dread41["c1"].ToString();
        }

        dread41.Close();


        string sql51 = "select count(*) as c1 from anjianinfo2 where convert(datetime,beizhu3) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and kf=''  and (state='完成' or state='关闭')";
        SqlCommand cmd51 = new SqlCommand(sql51, con);
        SqlDataReader dread51 = cmd51.ExecuteReader();
        if (dread51.Read())
        {
            dr1[3] = dread51["c1"].ToString();
        }

        dread51.Close();


        string sql61 = "select count(*) as c1 from anjianinfo2 where  kf='' and shiyanleibie ='CCC' and (state='进行中' or state='下达' or state='暂停')";
        SqlCommand cmd61 = new SqlCommand(sql61, con);
        SqlDataReader dread61 = cmd61.ExecuteReader();
        if (dread61.Read())
        {
            dr1[4] = dread61["c1"].ToString();
        }

        dread61.Close();


        string sql71 = "select count(*) as c1 from anjianinfo2 where  kf='' and shiyanleibie !='CCC' and  (state='进行中' or state='下达' or state='暂停')";
        SqlCommand cmd71 = new SqlCommand(sql71, con);
        SqlDataReader dread71 = cmd71.ExecuteReader();
        if (dread71.Read())
        {
            dr1[5] = dread71["c1"].ToString();
        }

        dread71.Close();



        string sql81 = "select count(*) as c1 from anjianinfo2 where  kf=''  and  (state='进行中' or state='下达' or state='暂停')";
        SqlCommand cmd81 = new SqlCommand(sql81, con);
        SqlDataReader dread81 = cmd81.ExecuteReader();
        if (dread81.Read())
        {
            dr1[6] = dread81["c1"].ToString();
        }

        dread81.Close();




        dt.Rows.Add(dr1);

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