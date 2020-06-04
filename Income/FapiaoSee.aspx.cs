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

public partial class CCSZJiaoZhun_htw_FapiaoSee : System.Web.UI.Page
{
    protected string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();

        id = Request.QueryString["id"].ToString();

        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString); 
            con.Open();
            string sql = "select * from FaPiao where id='" + id + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                TextBox1.Text = dr["fapiaono"].ToString();
                TextBox2.Text = dr["fapiaojine"].ToString();
                TextBox3.Text = dr["lingpiaoren"].ToString();
                TextBox4.Text = Convert.ToDateTime(dr["lingpiaotime"].ToString()).ToShortDateString();
                TextBox5.Text = dr["state"].ToString();
                TextBox6.Text = dr["responser"].ToString();
                TextBox7.Text = dr["taitou"].ToString();
                TextBox8.Text = dr["beizhu"].ToString();
                TextBox9.Text = dr["lingpiaobeizhu"].ToString();
                TextBox10.Text = dr["renwuhao"].ToString();
                Label3.Text = dr["daid"].ToString();
            }
            dr.Close();
            con.Close();

            fapiao();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString); 
        con.Open();

        try
        {
            string sql = "update FaPiao set fapiaono='" + TextBox1.Text + "',fapiaojine='" + TextBox2.Text + "',state='" + TextBox5.Text + "',responser='" + TextBox6.Text + "',taitou='" + TextBox7.Text + "',beizhu='" + TextBox8.Text + "' where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            Label2.Text = "修改成功！";
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        try
        {
            string sql = "update FaPiao set lingpiaoren='" + TextBox3.Text.Trim() + "',lingpiaotime='" + TextBox4.Text.Trim() + "',fillname2='"+Session["UserName"].ToString()+"',filltime2='"+DateTime.Now+"',renwuhao='"+TextBox10.Text.Trim()+"',lingpiaobeizhu='"+TextBox9.Text.Trim()+"' where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            Label2.Text = "修改成功！";
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
    protected void Button3_Click(object sender, EventArgs e)
    {
        fapiao();
    }


    protected void fapiao()
    {
        string sql = "select top 10 * from shuipiao where liushuihao like '%" + TextBox11.Text.Trim() + "%' or fukuanren like '%" + TextBox11.Text.Trim() + "%' or fukuanren like '%" + TextBox7.Text.Trim() + "%' order by daorutime desc";

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        SqlDataAdapter ad = new SqlDataAdapter(sql, con1);
        DataSet ds = new DataSet();
        ad.Fill(ds);


        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


        //GridView1.DataSource = ds.Tables[0];
        //GridView1.DataBind();
        con1.Close();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        try
        {
            string sql = "update FaPiao set  state='正常' where fapiaono='" + TextBox1.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");

                if (hzf.Checked)
                {

                    string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();



                    string sqlx = "insert into FaPiaoDaoKuan values ('" + id + "','" + sid + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";
                    SqlCommand cmdx = new SqlCommand(sqlx, con);
                    cmdx.ExecuteNonQuery();



                    string sqly = " update shuipiao set fapiaohao='" + TextBox1.Text.Trim() + "' where id='"+sid+"'";
                    SqlCommand cmdy = new SqlCommand(sqly, con);
                    cmdy.ExecuteNonQuery();



                }
            }


            Label2.Text = "修改成功！";
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
    protected void Button5_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        try
        {
            string sql = "update FaPiao set  state='借票' where fapiaono='" + TextBox1.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");

                if (hzf.Checked)
                {

                    string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();



                    string sqlx = "delete from  FaPiaoDaoKuan where daid='"+sid+"'";
                    SqlCommand cmdx = new SqlCommand(sqlx, con);
                    cmdx.ExecuteNonQuery();



                    string sqly = " update shuipiao set fapiaohao='' where id='" + sid  + "'";
                    SqlCommand cmdy = new SqlCommand(sqly, con);
                    cmdy.ExecuteNonQuery();



                }
            }


            Label2.Text = "修改成功！";
            fapiao();
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
}
