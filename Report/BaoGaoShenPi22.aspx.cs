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

public partial class Report_BaoGaoShenPi22 : System.Web.UI.Page
{
    protected string id = "";
    protected string idd = "";
    protected string src = "";
    protected string xiangmuid = "";
    protected string pp = "";
    protected string rwid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();


        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string sqlb = "select baogaoid from baogao2 where tjid='" + Request.QueryString["taskid"].ToString() + "'";
        SqlCommand cmdb = new SqlCommand(sqlb,con1);
        SqlDataReader drb = cmdb.ExecuteReader();
        if (drb.Read())
        {
            id = drb["baogaoid"].ToString();
        }

        con1.Close();


      


        if (Request.QueryString["pp"] != null)
        {
            pp = Request.QueryString["pp"].ToString();

            Button1.Visible = false;
        }
        xiangmuid = id;
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sqlx = "select *,(select top 1 shenheren from baogaoshenhe2 where shenhebianhao=baogao2.baogaoid) as shenheby1 from baogao2 where baogaoid='" + id + "'";
            SqlCommand comx = new SqlCommand(sqlx, con);
            SqlDataReader drx = comx.ExecuteReader();
            if (drx.Read())
            {
                TextBox1.Text = drx["shenheby1"].ToString();

                TextBox2.Text = drx["pizhunby"].ToString();

                rwid = drx["tjid"].ToString();
                if (TextBox2.Text == "")
                {
                    TextBox2.Text = "吴立安";
                }
                if (drx["pizhundate"].ToString().Substring(0, 4) == "1900")
                {
                    TextBox3.Text = DateTime.Now.ToShortDateString();
                }
                else
                {
                    TextBox3.Text = Convert.ToDateTime(drx["pizhundate"].ToString()).ToShortDateString();
                }
            }
            drx.Close();
            con.Close();
            Bindfuwufujian();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sql = "update baogao2 set shenheby='" + TextBox1.Text + "',pizhunby='" + TextBox2.Text + "',pizhundate='" + TextBox3.Text + "' where baogaoid='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            string sqlstate4 = "update anjianinfo2 set state='完成' where rwbianhao='" + rwid + "'";
            SqlCommand cmdstate4 = new SqlCommand(sqlstate4, con);
            cmdstate4.ExecuteNonQuery();


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

        MyExcutSql my = new MyExcutSql();
        my.ExtTaskone(id, rwid, "批准报告", "手工提交", Session["UserName"].ToString(), "批准报告", Convert.ToDateTime(TextBox3.Text.Trim()), "完成");




    }


    protected void Button5_Click(object sender, EventArgs e)
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
                string fileFullname = DateTime.Now.ToShortDateString() + xiangmuid + rand + this.FileUpload1.FileName;
                string fileName = fileFullname.Substring(fileFullname.LastIndexOf("\\") + 1);
                string type = fileFullname.Substring(fileFullname.LastIndexOf(".") + 1);
                this.FileUpload1.SaveAs(Server.MapPath("../upfiles") + "\\" + fileFullname);
                string ProImg = "upfiles/" + fileFullname;
                string ImageUrl = "~/upfiles/" + fileFullname;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sql = "insert baogaofujian2 values('" + xiangmuid + "','" + ImageUrl + "','" + fileName + "','" + type + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + xiangmuid + "','','','')";
                SqlCommand com = new SqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                Bindfuwufujian();
                this.Label2.Text = "上传附件成功";
            }
        }
        catch (Exception ex)
        {
            this.Label2.Text = ex.Message;
        }
    }

    public void Bindfuwufujian()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql1 = "select * from baogaofujian2 where caseid in (select baogaoid from baogao2 where tjid='" + Request.QueryString["taskid"].ToString() + "')";
        SqlDataAdapter da = new SqlDataAdapter(sql1, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        this.GridView5.DataSource = ds;
        this.GridView5.DataBind();
        con.Close();
        con.Dispose();
    }

    protected void GridView5_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (pp == "" && TextBox2.Text.Trim() == "")
        {
            string src2 = "";
            int Appurtenanceid = Convert.ToInt32(GridView5.DataKeys[e.RowIndex].Value.ToString());


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sql2 = "select * from baogaofujian2 where id=" + Appurtenanceid + " and fillname='" + Session["UserName"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql2, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                src2 = dr["filename"].ToString();
            }
            dr.Close();

            string sql = "delete from baogaofujian2 where id=" + Appurtenanceid + "";
            SqlCommand com = new SqlCommand(sql, con);
            com.ExecuteNonQuery();

            string url = Server.MapPath("~") + "\\upfiles\\" + src2;
            if (File.Exists(url))
            {
                File.Delete(url);
            }


            this.Label2.Text = "删除成功";
            con.Close();
            Bindfuwufujian();
        }
    }

}