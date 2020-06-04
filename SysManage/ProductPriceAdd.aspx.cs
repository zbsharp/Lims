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
public partial class SysManage_ProductPriceAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sql = "insert into productprice values('','" + TextBox1.Text + "','" + TextBox2.Text + "','','" + TextBox6.Text + "','" + Convert.ToDecimal(TextBox3.Text) + "','" + Convert.ToDecimal(TextBox4.Text) + "','" + Convert.ToDecimal(TextBox5.Text) + "','" + DropDownList2.SelectedValue + "','','','" + TextBox8.Text + "','" + DateTime.Now + "','" + DateTime.Now.Month + "','" + Session["username"].ToString() + "','" + TextBox7.Text + "','" + DropDownList1.SelectedValue + "','" + TextBox9.Text + "')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('提交成功');", true);
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