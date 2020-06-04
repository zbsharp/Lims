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

public partial class CeShiFeiSee : System.Web.UI.Page
{
    protected string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"].ToString();
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select * from CeShiFei where id='" + id + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                DropDownList1.SelectedValue = dr["type"].ToString();
                TextBox2.Text = dr["xiangmu"].ToString();
                TextBox3.Text = dr["shuliang"].ToString();
                TextBox4.Text = dr["feiyong"].ToString();
                TextBox5.Text = dr["date"].ToString();
                TextBox6.Text = dr["name"].ToString();
            }
            con.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sql = "update CeShiFei set type='" + DropDownList1.SelectedValue + "',xiangmu='" + TextBox2.Text + "',shuliang='" + TextBox3.Text + "',feiyong='" + TextBox4.Text + "',date='" + TextBox5.Text + "',name='" + TextBox6.Text + "' where id='" + id + "'";
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
