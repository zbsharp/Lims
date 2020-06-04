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

public partial class Customer_CustTraceList : System.Web.UI.Page
{

    const string vsKey = "searchCriteriauser"; //ViewState key
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
           
        
        }
    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *,(select top 1 customname from customer where kehuid =CustomerTrace.kehuid) as kehuname from CustomerTrace  where  " + searchwhere.search(Session["UserName"].ToString()) + " order by filltime desc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";
        if (DropDownList1.SelectedValue == "responser")
        {
            sql = "select *,(select top 1 customname from customer where kehuid =CustomerTrace.kehuid) as kehuname from CustomerTrace  where (" + DropDownList1.SelectedValue + " like '%" + TextBox1.Text + "%') and  " + searchwhere.search(Session["UserName"].ToString()) + " order by convert(datetime, xiacitime) desc";
        }
        else
        {
            sql = "select *,(select top 1 customname from customer where kehuid =CustomerTrace.kehuid) as kehuname from CustomerTrace  where kehuid in (select kehuid from customer where (" + DropDownList1.SelectedValue + " like '%" + TextBox1.Text + "%')) and  " + searchwhere.search(Session["UserName"].ToString()) + " order by convert(datetime, xiacitime) desc";
 
        }

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
     
        con.Close();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }



    protected void Button2_Click(object sender, EventArgs e)
    {
       

    }
}