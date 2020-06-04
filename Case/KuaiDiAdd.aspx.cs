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

public partial class Case_KuaiDiAdd : System.Web.UI.Page
{

    protected string kehuid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["kehuid"] != null)
        {
            kehuid = Request.QueryString["kehuid"].ToString();
        }

        if (!IsPostBack)
        {
             SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sql1 = "select * from Customer where kehuid='" + kehuid + "' order by kehuid";
            SqlCommand cmd = new SqlCommand(sql1, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                TextBox8.Text = dr["CustomName"].ToString();
                TextBox7.Text = dr["address"].ToString();
            }

            dr.Close();
    

            string sql21 = "select * from CustomerLinkMan where  customerid='" + kehuid + "' ";
            SqlDataAdapter ad21 = new SqlDataAdapter(sql21, con);
            DataSet ds21 = new DataSet();
            ad21.Fill(ds21);
            DropDownList1.DataSource = ds21.Tables[0];
            DropDownList1.DataTextField = "name";
            DropDownList1.DataValueField = "name";
            DropDownList1.DataBind();


            string sql2 = "select telephone+'/'+mobile as dd,* from CustomerLinkMan where  customerid='" + kehuid + "' and name='" + DropDownList1.SelectedValue + "' ";
            SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);
            DataSet ds2 = new DataSet();
            ad2.Fill(ds2);
            DropDownList2.DataSource = ds2.Tables[0];
            DropDownList2.DataTextField = "dd";
            DropDownList2.DataValueField = "dd";
            DropDownList2.DataBind();


            con.Close();

            TextBox3.Text = Session["UserName"].ToString();
            TextBox5.Text = DateTime.Now.ToShortDateString();
        }
        TextBox1.Focus();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sql = "insert into KuaiDi values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + DropDownList1.SelectedValue + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" + DropDownList2.SelectedValue + "','" + TextBox10.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('保存成功');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select telephone+'/'+mobile as dd from CustomerLinkMan where  customerid='" + kehuid + "' and name='" + DropDownList1.SelectedValue + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataTextField = "dd";
        DropDownList2.DataValueField = "dd";
        DropDownList2.DataBind();
        con.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Customer/CustomerSee.aspx?kehuid="+kehuid);
    }
}