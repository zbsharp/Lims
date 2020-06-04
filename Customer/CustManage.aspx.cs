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

public partial class Customer_CustManage : System.Web.UI.Page
{

    private int _i = 0;
    private int _i2 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMyFill();
            //BindMyResonser();
        }
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {

        BindMyFill();
        //BindMyResonser();
    }

    protected void BindMyFill()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string dutyname = "";//职位
        string dn = "";//部门
        string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
        SqlCommand cmdstate = new SqlCommand(sql_dutyname, con);
        SqlDataReader dr = cmdstate.ExecuteReader();
        if (dr.Read())
        {
            dn = dr["departmentname"].ToString();
            dutyname = dr["dutyname"].ToString();
        }
        dr.Close();
        string sql = "";
        if (dutyname.Trim() == "销售助理" || dutyname.Trim().Contains("客服"))
        {
            sql = @"select * from Customer where Kehuid in(select customerid from Customer_Sales where (responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "')) and Kehuid not like 'D%'";
        }
        else
        {
            sql = string.Format("select * from Customer where Kehuid in(select customerid from Customer_Sales where responser='" + Session["Username"].ToString() + "') and Kehuid not like 'D%'");
        }

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager1.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager1.PageSize;

        GridView1.DataSource = pds;
        GridView1.DataBind();
        con.Close();
    }
    protected void BindMyResonser()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select TOP 200 * from Customered where " + searchwhere.search(Session["UserName"].ToString()) + "";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();
        con.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        AspNetPager1.Visible = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string dutyname = "";//职位
        string dn = "";//部门
        string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
        SqlCommand cmdstate = new SqlCommand(sql_dutyname, con);
        SqlDataReader dr = cmdstate.ExecuteReader();
        if (dr.Read())
        {
            dn = dr["departmentname"].ToString();
            dutyname = dr["dutyname"].ToString();
        }
        dr.Close();
        string ChooseID = DropDownList1.SelectedValue.Trim();
        string value = TextBox1.Text.Trim();
        string sql = "";

        if (DropDownList1.SelectedValue != "contact")
        {
            //sql = "select * from Customer where fillname ='" + Session["UserName"].ToString() + "' and " + ChooseID + " like '%" + value + "%'  order by id desc";
            if (dutyname.Trim() == "销售助理" || dutyname.Trim().Contains("客服"))
            {
                sql = @"select * from Customer where Kehuid in (select customerid from Customer_Sales where (responser in(select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "')) and " + ChooseID + " like '%" + value + "%' and  Kehuid not like 'D%' order by id desc";
            }
            else
            {
                sql = "select * from Customer where Kehuid in (select customerid from Customer_Sales where responser='" + Session["Username"].ToString() + "') and " + ChooseID + " like '%" + value + "%' and  Kehuid not like 'D%' order by id desc";
            }
        }
        else
        {
            if (dutyname.Trim() == "销售助理" || dutyname.Trim().Contains("客服"))
            {
                sql = @"select * from Customer where Kehuid in (select customerid from Customer_Sales where (responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "')) and Kehuid in(select customerid from CustomerLinkMan where name='" + TextBox1.Text.Trim() + "') and  Kehuid not like 'D%'";
            }
            else
            {
                sql = @"select * from Customer where Kehuid in (select customerid from Customer_Sales where responser='" + Session["Username"].ToString() + "') and Kehuid in(select customerid from CustomerLinkMan where name='" + TextBox1.Text.Trim() + "') and  Kehuid not like 'D%'";
            }
        }


        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        string sql2 = "";
        if (DropDownList1.SelectedValue != "contact")
        {

            sql2 = "select * from Customered where " + searchwhere.search(Session["UserName"].ToString(), ChooseID, value) + " and  Kehuid not like 'D%' order by customname desc";
        }
        else
        {
            sql2 = "select * from Customer where " + searchwhere.search(Session["UserName"].ToString()) + " and kehuid in (select customerid from CustomerLinkMan where name like '%" + TextBox1.Text.Trim() + "%') and  Kehuid not like 'D%' order by customname desc";
        }

        SqlDataAdapter da2 = new SqlDataAdapter(sql2, con);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        GridView2.DataSource = ds2.Tables[0];
        GridView2.DataBind();
        con.Close();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");
            e.Row.Cells[6].Text = Eng(e.Row.Cells[5].Text.ToString());

            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i2.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");

            e.Row.Attributes.Add("onClick", "MarkRow(" + _i2.ToString() + ");");
            _i2++;
        }
    }

    public string Eng(string name)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql112 = "select UserName from CustomerServer where marketid='" + name + "'";

        SqlDataAdapter ad112 = new SqlDataAdapter(sql112, con);
        DataSet ds112 = new DataSet();
        ad112.Fill(ds112);
        con.Close();
        DataTable dt112 = ds112.Tables[0];
        string zhujian = "";
        for (int z = 0; z < dt112.Rows.Count; z++)
        {
            zhujian = zhujian + dt112.Rows[z]["UserName"].ToString() + ",";
        }
        if (zhujian.Contains(","))
        {
            zhujian = zhujian.Substring(0, zhujian.Length - 1);
        }
        return zhujian;
    }
}