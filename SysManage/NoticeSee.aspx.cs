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
using Common;
using System.IO;
using System.Text;
using System.Drawing;

public partial class SysManage_NoticeSee : System.Web.UI.Page
{
    protected string id = "";
    protected string kehuid = "";
    protected string src = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"].ToString();


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql2 = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='通知管理'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            Button1.Visible = true;
        }
        else
        {
            Button1.Visible = false;
        }



        dr2.Close();


        string sql3 = "select * from NompanyManage where id='" + id + "'";
        SqlCommand com3 = new SqlCommand(sql3, con);
        SqlDataReader dr3 = com3.ExecuteReader();
        if (dr3.Read())
        {
            TextBox1.Text = dr3["CompanyId"].ToString();
            kehuid = dr3["CompanyId"].ToString();
        }
        dr3.Close();


        if (!IsPostBack)
        {

            string sql = "select * from NompanyManage where id='" + id + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                TextBox1.Text = dr["CompanyId"].ToString();
                TextBox2.Text = dr["Title"].ToString();
                TextBox3.Text = dr["Detail"].ToString();
                TextBox4.Text = Convert.ToDateTime(dr["Signdate"].ToString()).ToShortDateString();
                TextBox5.Text = Convert.ToDateTime(dr["Enddate"].ToString()).ToShortDateString();
                TextBox6.Text = dr["remark"].ToString();
            }


            BindFujian();
        }

        con.Close();
    }
    public void BindFujian()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql1 = "select * from Yieyifujian where  caseid='" + id + "' ";
        SqlDataAdapter da = new SqlDataAdapter(sql1, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        this.GridView2.DataSource = ds;
        this.GridView2.DataBind();
        con.Close();
        con.Dispose();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sql = "update NompanyManage set Title='" + TextBox2.Text + "',Detail='" + TextBox3.Text + "',Signdate='" + TextBox4.Text + "',Enddate='" + TextBox5.Text + "',remark='" + TextBox6.Text + "' where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();



            //ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('修改成功');", true);
        }
        catch (Exception ex)
        {
            // ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
        }
        finally
        {
            con.Close();
            BindFujian();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.PostedFile.ContentLength == 0)
            {
                src = "";
            }
            else
            {

                Random rd = new Random();


                string rand = rd.Next(0, 99999).ToString();
                string fileFullname = DateTime.Now.ToShortDateString() + kehuid + rand + this.FileUpload1.FileName;
                string fileName = fileFullname.Substring(fileFullname.LastIndexOf("\\") + 1);
                string type = fileFullname.Substring(fileFullname.LastIndexOf(".") + 1);
                this.FileUpload1.SaveAs(Server.MapPath("../upfiles") + "\\" + fileFullname);
                string ProImg = "upfiles/" + fileFullname;
                string ImageUrl = "~/upfiles/" + fileFullname;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sql = "insert Yieyifujian values('" + id + "','" + ImageUrl + "','" + fileName + "','" + type + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','协议','" + kehuid + "','')";
                SqlCommand com = new SqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                BindFujian();
                this.Label2.Text = "上传附件成功";
            }
        }
        catch (Exception ex)
        {
            this.Label2.Text = ex.Message;
        }
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string src2 = "";
        int Appurtenanceid = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value.ToString());


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();



        string sql3 = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='通知管理'";
        SqlCommand cmd3 = new SqlCommand(sql3, con);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {

            dr3.Close();







            string sql2 = "select * from Yieyifujian where id=" + Appurtenanceid + "";
            SqlCommand cmd = new SqlCommand(sql2, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                src2 = dr["filename"].ToString();
            }
            dr.Close();

            string sql = "delete from Yieyifujian where id=" + Appurtenanceid + " and fillname='" + Session["UserName"].ToString() + "'";
            SqlCommand com = new SqlCommand(sql, con);
            int a = com.ExecuteNonQuery();
            if (a != 0)
            {
                string url = Server.MapPath("~") + "\\upfiles\\" + src2;
                if (File.Exists(url))
                {
                    File.Delete(url);
                }


                this.Label2.Text = "删除成功";
            }
            else
            {
                this.Label2.Text = "不允许删除别人上传的通知";
            }

            BindFujian();

        }
        else
        {
            BindFujian();

            dr3.Close();
        }

        con.Close();
    }
}