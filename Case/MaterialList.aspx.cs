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

public partial class Case_MaterialList : System.Web.UI.Page
{
    protected string shwhere = "";
    private int _i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

       
        if (!IsPostBack)
        {
            Bind();
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *,(select top 1 customname from customer where kehuid =Material.kehuid) as kehuname  from Material where renwuhao !='admin' order by id desc";

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
        AspNetPager1.Visible = false;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseID = DropDownList1.SelectedValue.Trim();
        string value = TextBox1.Text.Trim();
        string sql = "";

        if (DropDownList1.SelectedValue != "kehuname")
        {
            sql = "select * from  Material where "+ChooseID+"  like '%"+value+"%' order by id desc";
        }
        else
        {
            sql = "select *," + searchwhere.searchcustomer() + " from Material where  " + searchwhere.searchcustomer(TextBox1.Text.Trim()) + "  order by id desc";

        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
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
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string baojiaid = e.CommandArgument.ToString();
        if (e.CommandName == "cancel1")
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql3 = "update Material set biaozhi='是',querentime='" + DateTime.Now + "',querenname='"+Session["UserName"].ToString()+"' where  id ='" + baojiaid + "'";
            SqlCommand cmd = new SqlCommand(sql3, con);
            cmd.ExecuteNonQuery();
            con.Close();


          


            Bind();
        }
    }
}