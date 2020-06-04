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

public partial class SysManage_ZiLiaoTypeAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            RenZheng();
            department();
        }
    }
    protected void RenZheng()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from RenZheng order by id asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataTextField = "name";
        DropDownList1.DataValueField = "name";
        DropDownList1.DataBind();
        con.Close();
    }
    protected void department()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from UserDepa order by departmentid asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataTextField = "name";
        DropDownList2.DataValueField = "name";
        DropDownList2.DataBind();
        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string bianhao = zl_id();
            string sql = "insert into ZiLiaoType values('" + TextBox1.Text + "','" + DropDownList1.SelectedValue + "','" + bianhao + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + DropDownList2.SelectedValue + "','" + TextBox4.Text + "','" + TextBox6.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "')";
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

    //样品编号
    protected string zl_id()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string head = "1";
        string zlid = "";
        int h4 = 1;
        string h5 = h4.ToString("00000");
        string sql = "select bianhao from ZiLiaoType order by id asc";
        SqlDataAdapter adpter = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        adpter.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            zlid = head + h5;
        }
        else
        {
            string haoma = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["bianhao"].ToString();
            string qianzhui = haoma.Substring(0, 1);

            string houzhui = haoma.Substring(1, 5);
            int a1 = Convert.ToInt32(houzhui);
            int b1 = a1 + 1;
            zlid = qianzhui + b1.ToString("00000");
        }
        con.Close();
        return zlid;
    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {

    }
}