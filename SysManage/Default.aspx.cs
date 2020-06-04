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
using System.Text;
using System.IO;
using Common;
using DBBLL;

public partial class SysManage_Default : System.Web.UI.Page
{
    private string  minId = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
          
             SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
             con.Open();

            string cmd = "select count(*) from userinfo";
            if ((Request.QueryString["minid"]!=null))
            {
                cmd = "select count(*) from userinfo where departmentname like '%"+minId+"%'";

            }
           

                SqlDataAdapter da = new SqlDataAdapter(cmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);



             
                AspNetPager2.RecordCount = ds.Tables[0].Rows.Count;

               

            
            con.Close();
        

    }

  
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo  order by username";

        if (Request.QueryString["minid"] != null)
        {
            sql = "select * from userinfo where departmentname like '%" + minId + "%'";

            TextBox1.Text = minId.ToString();
        }

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);



        SqlDataSource1.SelectCommand = sql;
        DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        AspNetPager2.RecordCount = dv.Count;

        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();

        con.Close();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

        }
    }



    protected void Button1_Click1(object sender, EventArgs e)
    {


       
        if (this.TextBox1.Text.Trim() == "")
        {
            ld.Text = "<script>alert('请输入查询条件')</script>";

        }
        else
        {
            Response.Redirect("default.aspx?minid=" + TextBox1.Text);
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "log")
        {

            Response.Redirect("update.aspx?id=" + sid);

        }
        else if (e.CommandName == "cancel1")
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "update  userinfo  set flag='是' where id='" + sid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            con.Dispose();

            ld.Text = "<script>alert('冻结成功！该用户不能登录系统')</script>";
        }

        else if (e.CommandName == "cancel2")
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "update  userinfo  set flag='否' where id='" + sid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            con.Dispose();
            ld.Text = "<script>alert('解除成功！该用户不能登录系统')</script>";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserAdd.aspx");
    }
}