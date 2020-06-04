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
using Common;
using DBTable;
using System.Data.Common;

public partial class SysManage_ChanPingUpdate : System.Web.UI.Page
{

    protected string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"].ToString();
        if (!IsPostBack)
        {
            BindUser();
        }
    }


    protected void BindUser()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql3 = "select * from Product2 where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql3,con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            TextBox2.Text = dr["neirongid"].ToString();
            TextBox3.Text = dr["neirong"].ToString();
            TextBox4.Text = dr["biaozhun"].ToString();
            TextBox5.Text = dr["shoufei"].ToString();
            TextBox6.Text = dr["yp"].ToString();
            TextBox7.Text = dr["zhouqi"].ToString();
            TextBox8.Text= dr["beizhu"].ToString();
            
            TextBox11.Text = dr["leibieid"].ToString();
            TextBox9.Text = dr["leibiename"].ToString();
            TextBox10.Text = dr["chanpinid"].ToString();
            TextBox1.Text = dr["chanpinname"].ToString();
            TextBox12.Text = dr["danwei"].ToString();
        }

        con.Close();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql3 = "update Product2 set danwei='" + TextBox12.Text.Trim() + "', leibieid='" + TextBox11.Text.Trim() + "', leibiename='" + TextBox9.Text.Trim() + "', chanpinid='" + TextBox10.Text.Trim() + "', chanpinname='" + TextBox1.Text.Trim() + "',neirongid='" + TextBox2.Text.Trim() + "',neirong='" + TextBox3.Text.Trim() + "',biaozhun='" + TextBox4.Text.Trim() + "',shoufei='" + TextBox5.Text.Trim() + "',yp='" + TextBox6.Text.Trim() + "',zhouqi='" + TextBox7.Text.Trim() + "',beizhu='" + TextBox8.Text.Trim() + "' where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql3,con);
        cmd.ExecuteNonQuery();
        Response.Write("<script>alert('保存成功')</script>");

        con.Close();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql3 = "delete from Product2  where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql3, con);
        cmd.ExecuteNonQuery();
        Response.Write("<script>alert('保存成功')</script>");

        con.Close();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sql = "insert into Product2 values('" + TextBox11.Text + "','" + TextBox9.Text + "','" + TextBox10.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('保存成功')</script>");
        }
        finally
        {
            con.Close();
            
        }
    }
}