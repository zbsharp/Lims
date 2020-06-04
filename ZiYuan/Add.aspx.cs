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

public partial class ZiYuan_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["username"] = "ccic";


        if (!IsPostBack)
        {
            UserDepa();
            Userinfo();
            TextBox5.Text = Session["UserName"].ToString();
        }
    }
    protected void UserDepa()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

        con.Open();
        string sql = "select * from UserDepa";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataTextField = "name";
        DropDownList2.DataValueField = "name";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("", ""));//

        con.Close();
    }
    protected void Userinfo()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from Userinfo where departmentname='" + DropDownList2.SelectedValue + "' and flag !='是'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList3.DataSource = ds.Tables[0];
        DropDownList3.DataTextField = "username";
        DropDownList3.DataValueField = "username";
        DropDownList3.DataBind();
        con.Close();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Userinfo();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        try
        {
            string bianhao = bianhao_id();
            string sql = "insert into GoodsInfo values('" + bianhao + "','" + DropDownList1.SelectedValue + "','" + TextBox1.Text + "','" + DropDownList2.SelectedValue + "','" + DropDownList3.SelectedValue + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" + TextBox9.Text + "','" + TextBox10.Text + "','" + DropDownList4.SelectedValue + "','" + TextBox11.Text + "','" + TextBox12.Text + "','" + DropDownList5.SelectedValue + "','" + DropDownList6.SelectedValue + "','" + TextBox13.Text + "','" + TextBox14.Text + "','" + TextBox15.Text + "','" + TextBox16.Text + "','" + DropDownList7.SelectedValue + "','" + TextBox17.Text + "','" + TextBox18.Text + "','" + TextBox19.Text + "','" + TextBox20.Text + "','" + DropDownList8.SelectedValue + "','" + DropDownList9.SelectedValue + "','" + TextBox21.Text + "','" + TextBox22.Text + "','" + TextBox23.Text + "','" + TextBox24.Text + "','" + TextBox25.Text + "','" + TextBox26.Text + "','" + TextBox27.Text + "','" + TextBox28.Text + "','" + DropDownList10.SelectedValue + "','" + TextBox29.Text + "','" + TextBox30.Text + "','" + DropDownList11.SelectedValue  + "','" + TextBox32.Text + "','" + TextBox33.Text + "','" + TextBox34.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','','','','','','')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            //ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('提交成功');", true);
           // ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('提交成功');window.navigate('GoodsInfoUpdate.aspx?bianhao=" + bianhao + "','_self');", true);

            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('提交成功');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected string bianhao_id()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string bianhao = "";
        string bh_start = "";
        string bh_middle = "";

        if (DropDownList1.SelectedValue == "中检")
        {
            bh_start = "A";
        }
        else if (DropDownList1.SelectedValue == "中认")
        {
            bh_start = "R";
        }
        else if (DropDownList1.SelectedValue == "佛山")
        {
            bh_start = "B";
        }
        bh_middle = DateTime.Now.Year.ToString().Substring(2, 2) + Convert.ToInt32(DateTime.Now.Month.ToString()).ToString("00");

        string sql = "select top 1 id from GoodsInfo  order by id desc";
        SqlCommand cmd = new SqlCommand(sql,con);
        SqlDataReader dr = cmd.ExecuteReader();
        if(dr.Read())
        {
            string lastid = dr["id"].ToString();

            bianhao = lastid;
        }
        con.Close();
        return bianhao;
    }
}