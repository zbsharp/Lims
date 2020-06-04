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

public partial class ShiXiao_ShiXiaoPeiZhiSee : System.Web.UI.Page
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
            string sqlx = "select * from WaiQing where id='" + id + "'";
            SqlCommand comx = new SqlCommand(sqlx, con);
            SqlDataReader drx = comx.ExecuteReader();
            if (drx.Read())
            {
                if (drx["chutime"].ToString().Substring(0, 4) == "1900")
                {
                    TextBox1.Text = "";
                }
                else
                {
                    TextBox1.Text = Convert.ToDateTime(drx["chutime"].ToString()).ToShortDateString();
                }

                if (drx["huitime"].ToString().Substring(0, 4) == "1900")
                {
                    TextBox2.Text = "";
                }
                else
                {
                    TextBox2.Text = Convert.ToDateTime(drx["huitime"].ToString()).ToShortDateString();
                }

                TextBox3.Text = drx["chifan"].ToString();
                TextBox4.Text = drx["zhusu"].ToString();
                TextBox5.Text = drx["beizhu"].ToString();
                TextBox6.Text = drx["waichubianhao"].ToString();

                TextBox7.Text = drx["fillname"].ToString();
                TextBox8.Text = Convert.ToDateTime(drx["filltime"].ToString()).ToShortDateString();
            }
            drx.Close();
            con.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sql = "update WaiQing set chutime='" + TextBox1.Text + "',huitime='" + TextBox2.Text + "',chifan='" + TextBox3.Text + "',zhusu='" + TextBox4.Text + "',beizhu='" + TextBox5.Text + "',waichubianhao='" + TextBox6.Text + "' where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('修改成功');", true);
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sql = "delete from WaiQing where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('修改成功');", true);
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
}