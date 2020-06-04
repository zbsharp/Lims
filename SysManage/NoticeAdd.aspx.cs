using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Common;


public partial class SysManage_NoticeAdd : System.Web.UI.Page
{
    protected string kehuid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       // TextBox1.Text = Request.QueryString["kehuid"].ToString();
        //kehuid = Request.QueryString["kehuid"].ToString();


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();



        string sql2 = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='通知管理'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            Button1.Visible = true;
        }
        else
        {
            Button1.Visible = false;
        }

        con.Close();







        if (!IsPostBack)
        {
            bind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "insert into NompanyManage values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','','','')";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        //ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('保存成功');", true);

        bind();

        con.Close();

    }

    protected void bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql1 = "select top 20 * from NompanyManage";
        SqlDataAdapter da = new SqlDataAdapter(sql1, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        this.GridView1.DataSource = ds;
        this.GridView1.DataBind();
        con.Close();
        con.Dispose();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


        int Appurtenanceid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql2 = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='通知管理'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            dr2.Close();
            string sql = "delete from NompanyManage where id=" + Appurtenanceid + " and fillname='" + Session["UserName"].ToString() + "'";
            SqlCommand com = new SqlCommand(sql, con);
            com.ExecuteNonQuery();
        }
        else
        {

            dr2.Close();

        }






        con.Close();
        bind();
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "chakan")
        {
            Response.Redirect("NoticeSee.aspx?id=" + sid);
        }
    }
}