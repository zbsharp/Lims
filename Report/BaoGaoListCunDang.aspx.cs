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


public partial class Report_BaoGaoListCunDang : System.Web.UI.Page
{
    protected string shwhere = "1=1";
    private int _i = 0;
    const string vsKey = "searchbaogao3";
    protected void Page_Load(object sender, EventArgs e)
    {


        shwhere = " statebumen1='合格' and statebumen2='合格' and pizhunby !='' ";



        if (!IsPostBack)
        {
            txFDate.Value = DateTime.Now.AddDays (-7).ToShortDateString();
            txTDate.Value = DateTime.Now.ToShortDateString();
            Bind();
         
        }
    }
    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";
       
        {
            sql = "select *,(select shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao from BaoGao2 where " + shwhere + " and convert(datetime,pizhundate) between '" + DateTime.Now.AddDays(-7) + "' and '" + DateTime.Now.AddHours(23) + "' order by  convert(datetime,pizhundate) desc";
        }
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
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
    protected void Button2_Click(object sender, EventArgs e)
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;

        string sqlstr;
        if (DropDownList1.SelectedValue == "0")
        {
            sqlstr = "select  *,(select shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao from BaoGao2 where " + shwhere + " and convert(datetime,pizhundate) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + DateTime.Now.AddHours(23) + "' order by  convert(datetime,pizhundate) desc";
        }
        else
        {
            if (TextBox1.Text.Trim() == "")
            {

                if (DropDownList1.SelectedValue == "danganid") 
                {
                    sqlstr = " select  *,(select shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao from BaoGao2  where  danganid=''  and convert(datetime,pizhundate) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' order by  convert(datetime,pizhundate) desc ";

                }
                else
                {
                    sqlstr = " select  *,(select shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao from BaoGao2  where  " + shwhere + "  and convert(datetime,pizhundate) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' order by  convert(datetime,pizhundate) desc ";
                }
            }
            else
            {
                sqlstr = " select  *,(select shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao from BaoGao2  where  " + shwhere + " and " + ChooseNo + " like  '%" + ChooseValue + "%' and convert(datetime,pizhundate) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' order by  convert(datetime,pizhundate) desc ";

            }
        }
        AspNetPager2.Visible = false;

        SqlDataAdapter ad = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        con.Dispose();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();


    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");


            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
        }
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                if (e.Row.Cells[3].Text.Trim().ToString().Substring(0, 4) == "1900")
                {
                    e.Row.Cells[3].Text = "";
                }
                if (e.Row.Cells[4].Text.Trim().ToString().Substring(0, 4) == "1900")
                {
                    e.Row.Cells[4].Text = "";
                }
            }
        }
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            if (hzf.Checked)
            {
                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();






                string sql = "update baogao2 set danganid='" + TextBox2.Text + "',dangandate='" + TextBox3.Text + "' where id='" + sid + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
        }
        con.Close();
        Bind();

    }
}