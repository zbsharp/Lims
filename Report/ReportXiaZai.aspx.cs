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

public partial class Report_ReportXiaZai : System.Web.UI.Page
{
    public string tijiaobianhao = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string responser = "";
    protected string task = "";
    protected string renwu = "";
    protected string sendname = "";
    protected string chakan = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        tijiaobianhao = Request.QueryString["tijiaobianhao"].ToString();
     
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";
        if (tijiaobianhao.Contains("-"))
        {
            string sqlbian = "select bianhao from AnJianXinXi2 where (taskno='" + tijiaobianhao + "')";
            SqlCommand cmdbian = new SqlCommand(sqlbian, con);
            SqlDataReader drbian = cmdbian.ExecuteReader();
            if (drbian.Read())
            {
                tijiaobianhao = drbian["bianhao"].ToString();
            }
            drbian.Close();
        }

        sql = "select * from AnJianinfo2 where (bianhao='" + tijiaobianhao + "')";

        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            baojiaid = dr["baojiaid"].ToString();
            kehuid = dr["kehuid"].ToString();
            responser = dr["fillname"].ToString();
      
            task = dr["rwbianhao"].ToString();
        }

    

    
        con.Close();



        if (!IsPostBack)
        {


            BindBaoGao();
          
        }

        chakan = Request.QueryString["chakan"].ToString();
       
    }

    protected void BindBaoGao()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from baogao2 where rwid='" + task + "' or tjid='" + task + "' order by id asc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView4.DataSource = ds.Tables[0];
        GridView4.DataBind();
        con.Close();
        con.Dispose();
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            LinkButton LinkBtn_DetailInfo21 = (LinkButton)e.Row.FindControl("LinkButton5");
            bool d = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sqlk1 = "select * from baogao2 where baogaoid='" + e.Row.Cells[1].Text + "' and fafangby !=''";
            SqlCommand cmdk1 = new SqlCommand(sqlk1, con);
            SqlDataReader drk1 = cmdk1.ExecuteReader();
            if (drk1.Read())
            {
                d = true;
            }
            con.Close();
            if (d == true)
            {


            }
            else
            {

                LinkBtn_DetailInfo21.Enabled = false;
                LinkBtn_DetailInfo21.ForeColor = Color.DarkGray;
            }



        }

    }

    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        string state = "";

        if (e.CommandName == "xiada")
        {




            // Response.Redirect("~/Income/InvoiceAdd.aspx?ran=" + shoufeiid);

            Response.Redirect("~/Report/BaoGaoShenPi.aspx?baogaoid=" + sid + "&&pp=1");




        }
    }

}