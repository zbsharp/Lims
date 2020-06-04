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
using System.IO;

public partial class SysManage_ChuCuoLeiBie1aspx2 : System.Web.UI.Page
{
    string id = "";
    protected string src = "";
    protected string kehuid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"].ToString();
        kehuid = id;
        if (!IsPostBack)
        {
            //limit("文件管理");

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sql = "select top 10 * from UserChuCuoWenJian where departmentid='" + id + "'";
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            TextBox1.Text = ds.Tables[0].Rows[0]["name"].ToString();
            TextBox2.Text = ds.Tables[0].Rows[0]["wenyuan"].ToString();
            TextBox3.Text = ds.Tables[0].Rows[0]["fax"].ToString();
            con.Close();
            con.Dispose();
            BindFujian();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string name = "";

        string sql2 = "select wenyuan,name from UserChuCuoWenJian where departmentid='" + id + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con1);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            name = dr2["name"].ToString();
        }
        dr2.Close();



        string sql = "update UserChuCuoWenJian set wenyuan='" + TextBox2.Text.Trim() + "',fax='" + TextBox3.Text.Trim() + "' where departmentid='" + id + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();





        con1.Close();
        BindFujian();
        Response.Write("<script>alert('保存成功')</script>");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string name = "";

        string sql2 = "select name from UserChuCuoWenJian where departmentid='" + id + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con1);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            name = dr2["name"].ToString();
        }
        dr2.Close();

        string sql = "update UserChuCuoWenJian set name='" + TextBox1.Text.Trim() + "' where  name='" + name + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();



        con1.Close();
        BindFujian();
        Response.Write("<script>alert('保存成功')</script>");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();


        string sql = "delete from UserChuCuoWenJian where wenyuan='" + TextBox2.Text.Trim() + "' and departmentid='" + id + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        Response.Write("<script>alert('保存成功')</script>");
        BindFujian();
    }
    protected void Button4_Click(object sender, EventArgs e)
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
                this.FileUpload1.SaveAs(Server.MapPath("../WenJian") + "\\" + fileFullname);
                string ProImg = "WenJian/" + fileFullname;
                string ImageUrl = "~/WenJian/" + fileFullname;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sql = "insert xieyifujian1 values('" + kehuid + "','" + ImageUrl + "','" + fileName + "','" + type + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','协议','" + kehuid + "','')";
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



        string sql3 = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='文件管理'";
        SqlCommand cmd3 = new SqlCommand(sql3, con);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {

            dr3.Close();







            string sql2 = "select * from xieyifujian1 where id=" + Appurtenanceid + "";
            SqlCommand cmd = new SqlCommand(sql2, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                src2 = dr["filename"].ToString();
            }
            dr.Close();

            string sql = "delete from xieyifujian1 where id=" + Appurtenanceid + " and fillname='" + Session["UserName"].ToString() + "'";
            SqlCommand com = new SqlCommand(sql, con);
            int a = com.ExecuteNonQuery();
            if (a != 0)
            {
                string url = Server.MapPath("~") + "\\WenJian\\" + src2;
                if (File.Exists(url))
                {
                    File.Delete(url);
                }


                this.Label2.Text = "删除成功";
            }
            else
            {
                this.Label2.Text = "不允许删除别人上传的协议";
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

    public void BindFujian()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql1 = "select * from xieyifujian1 where  baojiaid='" + kehuid + "' and baojiaid !=''";
        SqlDataAdapter da = new SqlDataAdapter(sql1, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        this.GridView2.DataSource = ds;
        this.GridView2.DataBind();
        con.Close();
        con.Dispose();
    }
}