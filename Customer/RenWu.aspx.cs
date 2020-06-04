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
using System.Data.SqlClient;
using System.Drawing;
public partial class Customer_RenWu : System.Web.UI.Page
{
    private int _i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

       

            if (!IsPostBack)
            {

                Bind2();
            }
        
    }

    public void Bind2()
    {

        DataView dv = renwu1(Session["UserName"].ToString()).Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager2.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();

    }
    public DataSet renwu1(string jiaose)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select  renwu.*,customer.Customname from renwu left join customer on renwu.bianhao=customer.kehuid where guanbi='否' and( (jieshouren='" + Session["UserName"].ToString() + "') or (jieshouren in (select name2 from PersonConfig where name1='" + Session["UserName"].ToString() + "')) or jieshouren='' )  and renwuname='跟踪提醒'  and renwutime <='" + DateTime.Now.AddDays(7) + "'  order by renwutime desc";
        //string sql = "select * from studentInfo";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        con.Close();
        con.Dispose();
        return ds;
    }


    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
       
            if (!IsPostBack)
            {

                Bind2();
            }
        
    }

    protected void bnQuery_ServerClick(object sender, EventArgs e)
    {

    }

    public string SelectName()
    {

        string a = "";
        return a;
    }



    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        try
        {
            string kehuid = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            int sid = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value.ToString());
            string status = "已查看";
            string sql = "update renwu set biaozhi='" + status + "' where renwuid=" + sid + " ";
            string sql2 = "select bianhao from renwu where renwuid='" + sid + "'";

            SqlCommand com = new SqlCommand(sql, con);
            SqlCommand com1 = new SqlCommand(sql2, con);
            SqlDataReader dr = com1.ExecuteReader();


            if (dr.Read())
            {
                kehuid = dr["bianhao"].ToString();
            }
            dr.Close();
            com.ExecuteNonQuery();
            con.Close();

            {

                if (kehuid.Length == 7)
                {

                    Response.Redirect("~/Customer/CustomerSee.aspx?kehuid=" + kehuid);
                }
                else if (kehuid.Length > 7)
                {
                    Response.Redirect("~/Quotation/QuotationAdd.aspx?baojiaid=" + kehuid);
                }
                else
                {
                    Response.Write("<script>alert('没有相关信息')</script>");
                }

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            int sid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con1.Open();
            string sql1 = "delete from renwu where renwuid=" + sid + "";
            SqlCommand com1 = new SqlCommand(sql1, con1);
            com1.ExecuteNonQuery();


            con1.Close();
            Bind2();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("id", _i.ToString());
                e.Row.Attributes.Add("onKeyDown", "SelectRow();");
                e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
                _i++;
            }


        }



    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "guanbi")
        {
            string sid = e.CommandArgument.ToString();


            try
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con1.Open();
                string sql1 = "update renwu set guanbi='是' where renwuid=" + sid + "";
                SqlCommand com1 = new SqlCommand(sql1, con1);
                com1.ExecuteNonQuery();


                con1.Close();
                Bind2();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }



        }
    }
}