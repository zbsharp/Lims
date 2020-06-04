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
using System.Diagnostics;

public partial class TongJi_TaskCount1 : System.Web.UI.Page
{
    private int _i = 0;
    protected string str = "";
    protected string bumen1 = "";
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
           // bumen1 = Request.QueryString["bumen"].ToString();

           // bumen1 = "安规部";

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
        string sql = "";
       // if (Session["role"].ToString() == "1" || Session["role"].ToString() == "8")
        {
            sql = "select * from UserDepa where departmentid='12' or departmentid='13' or departmentid='22' or departmentid='15' or departmentid='16' or departmentid='17' or departmentid='15' or departmentid='16' or departmentid='17' or departmentid='9' order by name";
        }
        //else
        //{
        //    sql = "select * from UserDepa where name =(select top 1 departmentname from userinfo where username='"+Session["UserName"].ToString()+"') order by name";
 
        //}

        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


        DataSet ds = new DataSet();


        ad.Fill(ds);


        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataValueField = "name";
        DropDownList1.DataTextField = "name";
        DropDownList1.DataBind();

        //DropDownList1.Items.Insert(0, new ListItem("", ""));//

        con3.Close();
    }


    public void Bind3()
    {

        Stopwatch stopwatch = new Stopwatch();      
        stopwatch.Start();              
        
        GridView1.DataSource = table(txFDate.Value, txTDate.Value);
        GridView1.DataBind();
                     
        stopwatch.Stop();             
        //Response.Write(stopwatch.Elapsed); 
        
        
        


    }



    private int[] numbers = new int[50];

    private string[] numbers1 = new string[50];

    private string[] numbers2 = new string[50];
    private string[] numbers3 = new string[50];
    private string[] numbers4 = new string[50];
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string zongbu = "";

        string sql2p = "select  sum(jine) as aa from cashin left join anjianinfo2 on cashin.taskid=anjianinfo2.rwbianhao  where ( tichenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' ) and   cashin.beizhu3='" + DropDownList1.SelectedValue + "' ";


        //string sql2 = "select sum(feiyong) as aa from ceshifeikf where beizhu3='" + DropDownList1.SelectedValue + "' and taskid in (select bianhao from view_taskcount1 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and shiyanleibie='" + e.Row.Cells[0].Text + "' and name='" + numbers1[m].ToString() + "')";
        SqlCommand cmd2p = new SqlCommand(sql2p, con);
        SqlDataReader dr2p = cmd2p.ExecuteReader();
        if (dr2p.Read())
        {
            zongbu = dr2p["aa"].ToString();
        }

        dr2p.Close();




        for (int m = 1; m < e.Row.Cells.Count; m++)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                numbers1[m] = e.Row.Cells[m].Text;



                string sql21 = "select  sum(jine) as aa from cashin left join anjianinfo2 on cashin.taskid=anjianinfo2.rwbianhao  where ( tichenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' )   and cashin.beizhu3='" + DropDownList1.SelectedValue + "' and taskid in (select bianhao from zhujianengineer where name='" + numbers1[m].ToString() + "')";

                
                
                SqlCommand cmd21 = new SqlCommand(sql21, con);
                SqlDataReader dr21 = cmd21.ExecuteReader();
                if (dr21.Read())
                {
                    numbers2[m] = dr21["aa"].ToString();
                }

                dr21.Close();


              



            }




            if (e.Row.RowIndex >= 0)
            {


                e.Row.Attributes.Add("id", _i.ToString());
                e.Row.Attributes.Add("onKeyDown", "SelectRow();");


                e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
                _i++;

                if (e.Row.Cells[m].Text == "" || e.Row.Cells[m].Text == "&nbsp;")
                {
                    e.Row.Cells[m].Text = "0";

                }

                if (e.Row.Cells[m].Text == "0")
                {
                    e.Row.Cells[m].ForeColor = Color.Black;
                }
                else
                {
                    e.Row.Cells[m].ForeColor = Color.Red;
                }

                numbers[m] += Convert.ToInt32(e.Row.Cells[m].Text);


               // if (e.Row.Cells[m].Text != "0")
                {

                    string wei = "";
                    string yi = "";
                    string fei = "0";
                    string fff = "0";
                    string fff1 = "0";
                    string fff2 = "0";



                    string sql2 = "select  sum(jine) as aa from cashin left join anjianinfo2 on cashin.taskid=anjianinfo2.rwbianhao  where ( tichenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' ) and  ( shiyanleibie='" + e.Row.Cells[0].Text.Trim() + "') and cashin.beizhu3='" + DropDownList1.SelectedValue + "' and taskid in (select bianhao from zhujianengineer where name='" + numbers1[m].ToString() + "')";


                    //string sql2 = "select sum(feiyong) as aa from ceshifeikf where beizhu3='" + DropDownList1.SelectedValue + "' and taskid in (select bianhao from view_taskcount1 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and shiyanleibie='" + e.Row.Cells[0].Text + "' and name='" + numbers1[m].ToString() + "')";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.Read())
                    {
                        fei = dr2["aa"].ToString();
                    }

                    dr2.Close();






                    if (numbers1[m].ToString() != "合计")
                    {

                        e.Row.Cells[m].Text = fei;
                    }
                    else
                    {


                        string sql2h = "select  sum(jine) as aa from cashin left join anjianinfo2 on cashin.taskid=anjianinfo2.rwbianhao  where ( tichenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' ) and  ( shiyanleibie='" + e.Row.Cells[0].Text.Trim() + "') and cashin.beizhu3='" + DropDownList1.SelectedValue + "'";


                        //string sql2h = "select sum(feiyong) as aa from ceshifeikf where beizhu3='" + DropDownList1.SelectedValue + "' and taskid in (select bianhao from view_taskcount1 where  bumen='" + DropDownList1.SelectedValue + "' and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and shiyanleibie='" + e.Row.Cells[0].Text + "')";
                        SqlCommand cmd2h = new SqlCommand(sql2h, con);
                        SqlDataReader dr2h = cmd2h.ExecuteReader();
                        if (dr2h.Read())
                        {
                            fff = dr2h["aa"].ToString();
                        }

                        dr2h.Close();


                     




                        e.Row.Cells[m].Text = fff;



                    }
                }

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "合计：";


                if (numbers1[m].ToString() != "合计")
                {


                    e.Row.Cells[m].Text =  numbers2[m].ToString();


                }
                else
                {
                    e.Row.Cells[m].Text =zongbu;
                }
            }
        }
        con.Close();

        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        GridView1.DataSource = table(txFDate.Value, txTDate.Value);
        GridView1.DataBind();
        //Bind3();


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


        string sql = "select distinct name from view_taskcount1 where bumen='" + DropDownList1.SelectedValue + "' and name !='' and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' ";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con);


        DataSet ds = new DataSet();


        ad.Fill(ds);

        int dtcount = ds.Tables[0].Rows.Count;

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

            for (int c = 1; c < dt.Columns.Count - 1; c++)
            {
                dr[0] = ds2.Tables[0].Rows[yue]["name"].ToString();

                string sql3 = "select count(*) as c1 from view_taskcount1 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and name='" + dt.Columns[c].ToString() + "' and shiyanleibie='" + ds2.Tables[0].Rows[yue]["name"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(sql3, con);
                SqlDataReader dread = cmd.ExecuteReader();
                if (dread.Read())
                {
                    dr[c] = dread["c1"].ToString();
                }

                dread.Close();
            }

            string sql31 = "select count(distinct bianhao) as c1 from view_taskcount1 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(a) + "' and '" + Convert.ToDateTime(b).AddHours(23) + "' and shiyanleibie='" + ds2.Tables[0].Rows[yue]["name"].ToString() + "' and bumen='"+DropDownList1.SelectedValue+"'";
            SqlCommand cmd1 = new SqlCommand(sql31, con);
            SqlDataReader dread1 = cmd1.ExecuteReader();
            if (dread1.Read())
            {
                dr[dt.Columns.Count - 1] = dread1["c1"].ToString();
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

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Bind3();
    }

}