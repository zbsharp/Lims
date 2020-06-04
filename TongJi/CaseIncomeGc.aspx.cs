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

public partial class TongJi_CaseIncomeGc : System.Web.UI.Page
{
    private int _i = 0;
    protected string str = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        //  str = "select  rw sum(ceshifeikf.feiyong) as feiyong,taskid,kehuid from CeShiFeiKf where kehuid !='' group by taskid,kehuid";


        str = "select yong1, shiyanleibie, state,xiadariqi,yaoqiuwanchengriqi,beizhu3 ,kf,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2 ";
        if (!IsPostBack)
        {

            //DateTime dt = DateTime.Now.AddMonths(-6);
            //int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            //int dayspan = (-1) * weeknow + 1;
            //DateTime dt2 = DateTime.Now.AddMonths(+6);
            ////本月第一天
            //txFDate.Value = dt.AddMonths(-6).AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");


            //DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(6).ToString("yyyy-MM-01")).AddDays(-1).AddHours(23);

            //txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();

            txFDate.Value = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");


            DateTime lastDay = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddHours(23);

            txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();


            Bind3();
            GridView1.ShowFooter = false;
        }

    }
    public void Bind3()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select top 200 yong1,shiyanleibie, state,xiadariqi,yaoqiuwanchengriqi,beizhu3,kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2 where shiyanleibie='EMC' order by convert(datetime,beizhu3) desc";


        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);


        string sqlhuaxue = "select * from chaoqi";
        SqlDataAdapter ad9 = new SqlDataAdapter(sqlhuaxue, con);
        DataSet ds9 = new DataSet();
        ad9.Fill(ds9);
        DropDownList3.DataSource = ds9.Tables[0];

        DropDownList3.DataTextField = "name";
        DropDownList3.DataValueField = "name"; ;
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("", ""));//


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

    private decimal sum1 = 0;
    private decimal sum2 = 0;
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

            string sql = "select shenqingbianhao from anjianxinxi2 where taskno ='" + e.Row.Cells[0].Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                e.Row.Cells[1].Text = dr["shenqingbianhao"].ToString();
            }
            dr.Close();

            //MyExcutSql ext = new MyExcutSql();
            //e.Row.Cells[13].Text = ext.KeHu(e.Row.Cells[0].Text);


            //string sqlk3 = "select sum(feiyong)as feiyong,taskid  from CeShiFeiKf where taskid='" + e.Row.Cells[0].Text + "' group by taskid";
            //SqlCommand cmdk3 = new SqlCommand(sqlk3, con);
            //SqlDataReader drk3 = cmdk3.ExecuteReader();
            //if (drk3.Read())
            //{
            //    e.Row.Cells[7].Text = Math.Round(Convert.ToDecimal(drk3["feiyong"]), 2).ToString();
            //}
            //drk3.Close();

            //string sqlk1 = "select sum(xiaojine) as xiaojine from cashin2 where taskid='" + e.Row.Cells[0].Text + "' group by taskid";
            //SqlCommand cmdk1 = new SqlCommand(sqlk1, con);
            //SqlDataReader drk1 = cmdk1.ExecuteReader();
            //if (drk1.Read())
            //{
            //    e.Row.Cells[8].Text = Math.Round(Convert.ToDecimal(drk1["xiaojine"]), 2).ToString();
            //}
            con.Close();


            MyExcutSql ext = new MyExcutSql();
            e.Row.Cells[7].Text = ext.Eng(e.Row.Cells[0].Text);


            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 10);
            e.Row.Cells[2].Text = SubStr(e.Row.Cells[2].Text, 10);
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 10);
        }

        //if (e.Row.RowIndex >= 0)
        //{
        //    if (e.Row.Cells[7].Text == "" || e.Row.Cells[7].Text == "&nbsp;")
        //    {
        //        e.Row.Cells[7].Text = "0";
        //    }
        //    if (e.Row.Cells[8].Text == "" || e.Row.Cells[8].Text == "&nbsp;")
        //    {
        //        e.Row.Cells[8].Text = "0";
        //    }
        //    sum1 += Convert.ToDecimal(e.Row.Cells[7].Text);
        //    sum2 += Convert.ToDecimal(e.Row.Cells[8].Text);
        //}


        //else if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[6].Text = "合计：";
        //    e.Row.Cells[7].Text = sum1.ToString();
        //    e.Row.Cells[8].Text = sum2.ToString();


        //    e.Row.Cells[7].ForeColor = Color.Blue;
        //    e.Row.Cells[8].ForeColor = Color.Blue;
        //}



    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";
        AspNetPager2.Visible = false;

        GridView1.ShowFooter = true;

        if (DropDownList2.SelectedValue == "" && DropDownList3.SelectedValue == "")
        {
            sql = str + "where convert(datetime,beizhu3) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and (state like '%" + TextBox1.Text + "%' or rwbianhao like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%') or rwbianhao in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text + "%')) order by convert(datetime,beizhu3)";
        }
    
        else if (DropDownList2.SelectedValue != "" && DropDownList3.SelectedValue == "")
        {

            if (DropDownList1.SelectedValue == "gc")
            {
                sql = str + "where convert(datetime,beizhu3) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and state='" + DropDownList2.SelectedValue + "' and (rwbianhao in (select bianhao from zhujianengineer where name like '%" + TextBox1.Text + "%')) order by convert(datetime,beizhu3)";
            }
            else
            {
                sql = str + "where convert(datetime,beizhu3) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and state='" + DropDownList2.SelectedValue + "' and (rwbianhao like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%') or rwbianhao in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text + "%')) order by convert(datetime,beizhu3)";
 
            }
        }
     
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        con.Close();
        con.Dispose();
        GridView1.DataSource = ds.Tables[0];
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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {



    }
}