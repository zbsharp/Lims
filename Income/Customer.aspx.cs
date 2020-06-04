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
public partial class Income_Customer : System.Web.UI.Page
{
    private int _i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {



            if (Session["role"].ToString() == "8" || Session["role"].ToString() == "9" || Session["role"].ToString() == "1" || Session["role"].ToString() == "2")
            {
                Bindlingdao1();
            }

            else
            {


                Bindlingdao();


            }






        }
    }
    protected void Bindlingdao()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select top 100 * from Customer where " + searchwhere.search(Session["UserName"].ToString()) + " order by id  desc";   //string sql = "select * from studentInfo";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
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

    protected void Bindlingdao1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select top 100 * from Customer  order by id  desc";   //string sql = "select * from studentInfo";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
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
        if (Session["role"].ToString() == "8" || Session["role"].ToString() == "9")
        {
            Bindlingdao1();
        }

        else
        {


            Bindlingdao();


        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

        }


    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        AspNetPager2.Visible = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseID = SerchCondition.SelectedValue.Trim();
        string value = SerchText.Text.Trim();
        string sql2 = "";
        if (Session["role"].ToString() == "8" || Session["role"].ToString() == "9" || Session["role"].ToString() == "1" || Session["role"].ToString() == "2")
        {
            sql2 = "select * from Customer where  " + ChooseID + " like '%" + value + "%'  order by customname desc";
        }
        else
        {
            sql2 = "select * from Customer where " + searchwhere.search(Session["UserName"].ToString(), ChooseID, value) + "  order by customname desc";
 
        }
        SqlDataAdapter da2 = new SqlDataAdapter(sql2, con);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        con.Close();
        con.Dispose();

        GridView1.DataSource = ds2.Tables[0];
        GridView1.DataBind();
      


    }
}