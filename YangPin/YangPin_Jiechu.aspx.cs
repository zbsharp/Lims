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
public partial class YangPin_Jiechu : System.Web.UI.Page
{
    protected string sampleid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        sampleid = Request.QueryString["sampleid"].ToString();

        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();


        if (!IsPostBack)
        {
            Bind1();
            TextBox4.Text = DateTime.Now.ToShortDateString();
            TextBox1.Text = sampleid;
           // TextBox3.Text = Session["username"].ToString();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select * from YangPin2 where sampleid='" + sampleid + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                TextBox2.Text = dr["name"].ToString();
                TextBox6.Text = dr["state"].ToString();
            }
            dr.Close();


            string sql2 = "select kf from anjianinfo2 where rwbianhao=(select anjianid from YangPin2 where sampleid='" + sampleid + "')";
            SqlCommand cmd2 = new SqlCommand(sql2,con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                TextBox3.Text = dr2["kf"].ToString();
            }



            con.Close();

            //if (TextBox6.Text == "已还"||TextBox6.Text == "借出")
            //{
            //    Button1.Visible = false;
            //}
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        try
        {
            string sql = "insert into YangPin2Detail values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','借出','" + TextBox5.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','','','','','')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            string sqlx = "update YangPin2 set state='借出' where sampleid='" + sampleid + "'";
            SqlCommand cmdx = new SqlCommand(sqlx, con);
            cmdx.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('提交成功！');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
        }
        finally
        {
            con.Close();
            Bind1();
        }
    }
    protected void Bind1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from YangPin2Detail where state='借出' and sampleid='" + sampleid + "' order by id desc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }
}
