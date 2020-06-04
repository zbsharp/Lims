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
using System.Data.OleDb;
using System.IO;
using Common;
using DBBLL;
using DBTable;

public partial class SysManage_ProductPriceDetail : System.Web.UI.Page
{
    protected string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"].ToString();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        if (!IsPostBack)
        {
            string sql = "select * from ProductPrice where id='" + id + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                TextBox1.Text = dr["BIGTYPE"].ToString();
                TextBox2.Text = dr["MIDTYPE"].ToString();
                TextBox3.Text = dr["COST"].ToString();
                TextBox4.Text = dr["PRICE"].ToString();
                TextBox5.Text = dr["DISCOUNT"].ToString();
                TextBox6.Text = dr["department"].ToString();
                TextBox7.Text = dr["checkmodel"].ToString();
                TextBox8.Text = dr["cnas"].ToString();
                TextBox9.Text = dr["cardperiod"].ToString();

                TextBox10.Text = dr["units"].ToString();
                TextBox11.Text = dr["beizhu4"].ToString();
            }
            dr.Close();

        }
        con.Close();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {


            string sql = "update productprice set BIGTYPE='" + TextBox1.Text + "',MIDTYPE='" + TextBox2.Text + "', cost='" + TextBox3.Text + "',price='" + TextBox4.Text + "',discount='" + TextBox5.Text + "',department='" + TextBox6.Text + "',checkmodel='" + TextBox7.Text + "',cnas='" + TextBox8.Text + "',cardperiod='" + TextBox9.Text + "',units='" + TextBox10.Text + "',beizhu4='" + TextBox11.Text + "' where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('修改成功');", true);
            //ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('修改成功');window.navigate('CCCCheckManage.aspx','_self');", true);
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