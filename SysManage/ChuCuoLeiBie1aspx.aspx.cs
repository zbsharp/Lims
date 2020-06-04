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
using DBBLL;
using DBTable;
using System.Drawing;

public partial class SysManage_ChuCuoLeiBie1aspx : System.Web.UI.Page
{
    string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"].ToString();
        if (!IsPostBack)
        {
            limit("质量录入");
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sql = "select top 10 * from UserChuCuo where departmentid='" + id + "'";
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            TextBox1.Text = ds.Tables[0].Rows[0]["name"].ToString();
            TextBox2.Text = ds.Tables[0].Rows[0]["wenyuan"].ToString();
            TextBox3.Text = ds.Tables[0].Rows[0]["fax"].ToString();
            con.Close();
            con.Dispose();
        }
    }

    protected void limit(string pagename1)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {


            con.Close();
        }
        else
        {
            con.Close();
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
    }

    protected bool limit1(string pagename1)
    {
        bool A = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {


            con.Close();
            A = true;
        }
        else
        {
            con.Close();
            A = false;
        }
        return A;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string name = "";

        string sql2 = "select wenyuan from userchucuo where departmentid='" + id + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con1);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            name = dr2["name"].ToString();
        }
        dr2.Close();


      
        string sql = "update UserChuCuo set wenyuan='" + TextBox2.Text.Trim() + "',fax='" + TextBox3.Text.Trim() + "' where departmentid='" + id + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();


        string sql3 = "update BaoGaoChaCuo set leirong='" + TextBox2.Text.Trim() + "'where leirong='" + name + "'";

        SqlCommand cmd3 = new SqlCommand(sql3, con1);
        cmd3.ExecuteNonQuery();


        con1.Close();
        Response.Write("<script>alert('保存成功')</script>");
    }

  
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string name = "";

        string sql2 = "select name from userchucuo where departmentid='" + id + "'";
        SqlCommand cmd2 = new SqlCommand(sql2,con1);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            name = dr2["name"].ToString();
        }
        dr2.Close();

        string sql = "update UserChuCuo set name='" + TextBox1.Text.Trim() + "' where  name='"+name+"'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();

        string sql3 = "update BaoGaoChaCuo set fenlei='" + TextBox1.Text.Trim() + "' where  fenlei='" + name + "'";

        SqlCommand cmd3 = new SqlCommand(sql3, con1);
        cmd3.ExecuteNonQuery();


        con1.Close();

        Response.Write("<script>alert('保存成功')</script>");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();


        string sql = "delete from UserChuCuo where wenyuan='" + TextBox2.Text.Trim() + "' and departmentid='" + id + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        Response.Write("<script>alert('保存成功')</script>");

    }
}