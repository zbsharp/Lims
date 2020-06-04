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

public partial class Quotation_AnjianxinxiManage2 : System.Web.UI.Page
{
    private int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            TimeBind();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "Chakan")
        {
            Response.Redirect("AnjianxinxiSee.aspx?id=" + sid);
        }
        else if (e.CommandName == "renwu1")
        {
            Response.Redirect("~/Case/TaskIn.aspx?tijiaobianhao=" + sid);
        }


        else if (e.CommandName == "renwu2")
        {
            Response.Redirect("~/Print/TaskPrint.aspx?bianhao=" + sid);
        }



        else if (e.CommandName == "back")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql3 = "update Anjianxinxi2 set state='退回',statetime='" + DateTime.Now + "',statename='" + Session["UserName"].ToString() + "' where  bianhao ='" + sid + "'";
            SqlCommand cmd = new SqlCommand(sql3, con);
            cmd.ExecuteNonQuery();


            con.Close();

            ld.Text = "<script>alert('退回成功!');</script>";


            TimeBind(int.Parse(DropDownList1.SelectedValue), TextBox1.Text);
        }

        else if (e.CommandName == "cancel1")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sqly = "delete from Anjianxinxi3 where bianhao='" + sid + "'";
            SqlCommand cmdy = new SqlCommand(sqly, con);
            cmdy.ExecuteNonQuery();

            string sqlx = "delete from Anjianxinxi2 where bianhao='" + sid + "'";
            SqlCommand cmdx = new SqlCommand(sqlx, con);
            cmdx.ExecuteNonQuery();


            con.Close();

            ld.Text = "<script>alert('取消成功!');</script>";


            TimeBind(int.Parse(DropDownList1.SelectedValue), TextBox1.Text);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        int ChooseNo = int.Parse(DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;
        AspNetPager1.Visible = false;
        TimeBind(ChooseNo, ChooseValue);
    }

    protected void TimeBind(int a, string b)
    {
        int ChooseID = a;
        string ChooseValue = b;
        string sqlstr;

        sqlstr = "select * ,(select name from BaoJiaChanPing where id=anjianxinxi2.chanpinbianhao) as name from Anjianxinxi2 where (baojiaid='" + ChooseValue + "' or weituo like '%" + ChooseValue + "%') and shoulibiaozhi='否' and biaozhi='是'  order by id desc ";


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

        sqlstr = "select *,(select name from BaoJiaChanPing where id=anjianxinxi2.chanpinbianhao) as name from Anjianxinxi2 where  shoulibiaozhi='否' and biaozhi='是' order by id desc ";


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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("id", _i.ToString());
                e.Row.Attributes.Add("onKeyDown", "SelectRow();");
                //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");


                e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
                _i++;
            }

            LinkButton LinkBtn_DetailInfo2 = (LinkButton)e.Row.FindControl("LinkButton5");
            if (e.Row.Cells[7].Text == "退回")
            {
                LinkBtn_DetailInfo2.Enabled = false;
                LinkBtn_DetailInfo2.ForeColor = Color.Black;
                LinkBtn_DetailInfo2.Text = "已退回";
            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        TimeBind();
    }
}