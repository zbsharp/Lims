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

public partial class Quotation_QuotationAppro2 : System.Web.UI.Page
{
    protected string shwhere = "";
    private int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        shwhere = "(((shenpibiaozhi='通过' and tijiaobiaozhi='是' and other='是'  and weituo='是') ) and fillname not in  (select username from userinfo where departmentname='客户服务部'))";
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
        string sql = "select *,(select top 1 fillname from Approval where bianhao=baojiabiao.baojiaid and result ='通过'  order by id desc) as fname1,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname  from baojiabiao where " + shwhere + " and  " + searchwhere.search(Session["UserName"].ToString()) + "  order by tijiaotime desc";

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
            sql = "select *,(select top 1 fillname from Approval where bianhao=baojiabiao.baojiaid and result ='通过' order by id desc) as fname1," + searchwhere.searchcustomer() + " from baojiabiao where " + searchwhere.search(Session["UserName"].ToString(), ChooseID, value) + " and " + shwhere + " order by id desc";
        }
        else
        {
            sql = "select *,(select top 1 fillname from Approval where bianhao=baojiabiao.baojiaid and result ='通过' order by id desc) as fname1," + searchwhere.searchcustomer() + " from baojiabiao where " + searchwhere.search(Session["UserName"].ToString()) + " and " + searchwhere.searchcustomer(TextBox1.Text.Trim()) + " and  " + shwhere + "  order by id desc";

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
            string xy6 = e.Row.Cells[4].Text.Trim().ToString();
            if (xy6 != "1.00")
            {
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            }



        }

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
            string sql3 = "update baojiabiao set tijiaobiaozhi='否',tijiaotime='" + DateTime.Now + "' where  baojiaid ='" + baojiaid + "'";
            SqlCommand cmd = new SqlCommand(sql3, con);
            cmd.ExecuteNonQuery();

            string kehuid = "";

            string sql2 = "select kehuid from baojiabiao where baojiaid='" + baojiaid + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                kehuid = dr2["kehuid"].ToString();

            }
            dr2.Close();
            con.Close();

            ld.Text = "<script>alert('退回成功!');</script>";


            Bind();
        }
    }
}