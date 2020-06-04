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
using System.Text;
using System.IO;
using Common;
public partial class SysManage_UserAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"] == null)
        {
            Response.Write("<script>alert('请先登录!');window.location.href='../Login.aspx'</script>");
        }

        else
        {


            if (!IsPostBack)
            {
                SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con3.Open();
                string sql = "select * from UserDepa";
                string sql2 = "select * from UserDuty order by dutyid asc";

                SqlDataAdapter ad = new SqlDataAdapter(sql, con3);
                SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con3);

                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();

                ad.Fill(ds);
                ad2.Fill(ds2);

                Branch.DataSource = ds.Tables[0];
                Branch.DataValueField = "departmentid";
                Branch.DataTextField = "name";
                Branch.DataBind();


                DropDownList1.DataSource = ds2.Tables[0];
                DropDownList1.DataValueField = "dutyid";
                DropDownList1.DataTextField = "name";
                DropDownList1.DataBind();
                con3.Close();

            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //*****2019-8-8修改  非空判断 用户编号 密码 确认密码 不能为空
        if (string.IsNullOrEmpty(this.name.Text.Trim()) || string.IsNullOrEmpty(this.TextBox1.Text.Trim()) || string.IsNullOrEmpty(this.nTextBox.Text.Trim()))
        {
            Response.Write("<script>alert('用户账号，密码不能为空！')</script>");
            return;
        }
        string names = this.name.Text.Trim();
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql3 = "select username from userinfo where username='" + names + "'";
        SqlCommand com3 = new SqlCommand(sql3, con3);
        SqlDataReader reader3 = com3.ExecuteReader();
        if (reader3.Read())
        {
            Response.Write("<script>alert('此用户已经存在请重新输入用户名称！')</script>");
            reader3.Close();
        }
        else
        {
            reader3.Close();
            try
            {
                string password = TextBox1.Text;
                string npassword = nTextBox.Text;
                int dutyid = Convert.ToInt32(DropDownList1.SelectedValue);
                int jiaose = Convert.ToInt32(DropDownList1.SelectedValue);
                string jiaoname = DropDownList1.SelectedItem.Text;
                string dutyname = DropDownList1.SelectedItem.Text;
                string youxiang = email.Text;
                string departmentid = Branch.SelectedValue;
                string departmentname = Branch.SelectedItem.Text;
                string dianhua = workPhone.Text;
                string yidong = movePhone.Text;
                string strMd5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(TextBox1.Text, "md5");
                string homeloaction = drop_home.SelectedItem.Value;
                string sql = "insert into userinfo values('" + names + "','" + password + "','" + npassword + "','" + dutyid + "','" + dutyname + "','" + departmentid + "','" + departmentname + "','" + jiaose + "','" + jiaoname + "','" + youxiang + "','" + departmentname + "','" + dianhua + "','" + yidong + "','','否','','" + strMd5 + "','" + TextBox2.Text.Trim() + "','" + TextBox3.Text.Trim() + "','" + homeloaction + "') ";
                SqlCommand com1 = new SqlCommand(sql, con3);
                com1.ExecuteNonQuery();
                Response.Write("<script>alert('增加成功,请继续配置相应的人员和权限!');window.location.href='../SysManage/ModuleInsert.aspx?username=" + names + "'</script>");
                con3.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {



    }
}