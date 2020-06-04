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

public partial class CCSZJiaoZhun_htw_FapiaoAdd : System.Web.UI.Page
{
    protected string daid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["username"] = "ccic";
        //daid = "HQ2012050111";

        daid = Request.QueryString["liushuihao"].ToString();

        if (Request.QueryString["kehuid"] != null)
        {
            DropDownList1.SelectedValue = "借票";
        }
        else
        {
            DropDownList1.SelectedValue = "正常";
        }

        Label3.Text = daid;
        if (!IsPostBack)
        {
           


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sql2 = "select * from shuipiao where liushuihao='"+daid+"'";
            SqlCommand cmd = new SqlCommand(sql2,con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                TextBox7.Text = dr["fukuanren"].ToString();

                TextBox2.Text = dr["fukuanjine"].ToString();
            }



            con.Close();

            Bind1();

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString); 
        con.Open();

        try
        {
            string sql = "insert into FaPiao values('" + TextBox1.Text + "','" + TextBox2.Text.Trim() + "','" + TextBox3.Text.Trim() + "','" + TextBox4.Text.Trim() + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" +  DropDownList1.SelectedValue + "','" + TextBox6.Text + "','" + Label3.Text + "','','','','1','','"+daid+"','')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            Button1.Enabled = false;
            Bind1();
            Label2.Text = "保存成功！";
        }
        catch (Exception ex)
        {
            Label2.Text = ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！";
        }
        finally
        {
            con.Close();
        }
    }

    protected void Bind1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from FaPiao where  daid='"+daid+"'  order by id asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

       
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "delete from   FaPiao  where id='" + id + "' and fillname='"+Session["UserName"].ToString()+"'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();
        Bind1();
    }


}
