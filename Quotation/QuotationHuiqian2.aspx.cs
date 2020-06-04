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

public partial class Quotation_QuotationHuiqian2 : System.Web.UI.Page
{
    protected string shwhere = "";
    private int _i = 0;
    public string id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            id = Request.QueryString["id"].ToString();
            DataBind();
        }
        shwhere = "shenpibiaozhi ='通过' and huiqianbiaozhi='是'";
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
        string sql = "select top 100 *,(select name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname  from baojiabiao where " + shwhere + " and  " + searchwhere.search(Session["UserName"].ToString()) + "  order by id desc";

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
            sql = "select *,(select name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name," + searchwhere.searchcustomer() + " from baojiabiao where " + searchwhere.search(Session["UserName"].ToString(), ChooseID, value) + " and " + shwhere + " order by id desc";
        }
        else
        {
            sql = "select *,(select name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name," + searchwhere.searchcustomer() + " from baojiabiao where " + searchwhere.search(Session["UserName"].ToString()) + " and " + searchwhere.searchcustomer(TextBox1.Text.Trim()) + " and  " + shwhere + "  order by id desc";

        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
       
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");

            e.Row.Cells[2].Text = SubStr(e.Row.Cells[2].Text, 10);
            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 10);
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
        }
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
}