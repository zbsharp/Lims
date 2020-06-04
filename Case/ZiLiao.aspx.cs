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
using Ionic.Zip;


public partial class Case_ZiLiao : System.Web.UI.Page
{
    protected string xiangmuid = "";
    protected string src = "";
    protected string baojiaid = "";
    protected string rw = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        xiangmuid = Request.QueryString["id"].ToString();
        string a = DateTime.Now.Year.ToString();
        string sql = "";
        if (xiangmuid.Contains(a))
        {
            sql = "select baojiaid from BaoJiaCPXiangMu where tijiaohaoma='" + xiangmuid + "'";
        }
        else
        {
            sql = "select baojiaid from BaoJiaCPXiangMu where id='" + xiangmuid + "'";
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            baojiaid = dr["baojiaid"].ToString();
        }
        else
        {
            baojiaid = Request.QueryString["baojiaid"].ToString();
        }
        dr.Close();

        string sql2 = "select top 1 xiangmuid from anjianxinxi where id='"+xiangmuid+"'";
        SqlCommand cmd2 = new SqlCommand(sql2,con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            rw = dr2["xiangmuid"].ToString();
        }

        con.Close();

        if (!IsPostBack)
        {
            Bindfuwufujian();
        }
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
                this.FileUpload1.SaveAs(Server.MapPath("../Ziliao") + "\\" + fileFullname);
                string ProImg = "Ziliao/" + fileFullname;
                string ImageUrl = "~/Ziliao/" + fileFullname;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sql = "insert fuwufujian values('" + xiangmuid + "','" + ImageUrl + "','" + fileName + "','" + type + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + baojiaid + "','资料')";
                SqlCommand com = new SqlCommand(sql, con);
                com.ExecuteNonQuery();

                string sql2 = "update anjianinfo2 set ziliaostate='需要审核' where rwbianhao='"+rw+"'";
                SqlCommand cmd2 = new SqlCommand(sql2,con);
                cmd2.ExecuteNonQuery();

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
        string sql1 = "select * from fuwufujian where caseid='" + xiangmuid + "' and leibie !=''";
        SqlDataAdapter da = new SqlDataAdapter(sql1, con);
        DataSet ds = new DataSet();
        da.Fill(ds);


        string sql12 = "select * from fuwufujian where caseid in (select convert(varchar,id) from anjianxinxi where xiangmuid='" + rw + "') and leibie !=''";
        SqlDataAdapter da2 = new SqlDataAdapter(sql12, con);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);


        con.Close();
        con.Dispose();
        this.GridView5.DataSource = ds;
        this.GridView5.DataBind();

        this.GridView1.DataSource = ds2;
        this.GridView1.DataBind();
      
    }

    protected void GridView5_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string src2 = "";
        int Appurtenanceid = Convert.ToInt32(GridView5.DataKeys[e.RowIndex].Value.ToString());


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql2 = "select * from fuwufujian where id='" + Appurtenanceid + "'";
        SqlCommand cmd = new SqlCommand(sql2, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            src2 = dr["filename"].ToString();
        }
        dr.Close();

        string sql = "delete from fuwufujian where id='" + Appurtenanceid + "'and fillname='" + Session["UserName"].ToString() + "'";
        SqlCommand com = new SqlCommand(sql, con);
        int i= com.ExecuteNonQuery();

        if (i > 0)
        {
            string url = Server.MapPath("~") + "\\Ziliao\\" + src2;
            if (File.Exists(url))
            {
                File.Delete(url);
            }


            this.Label2.Text = "删除成功";
            con.Close();
            Bindfuwufujian();
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "application/zip";
        Response.AddHeader("content-disposition", "filename=ZiLiao.zip");
        using (ZipFile zip = new ZipFile(System.Text.Encoding.Default))//解决中文乱码问题
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
                if (hzf.Checked)

                {
                   // zip.AddFile(Server.MapPath("~") + "\\Ziliao\\" + (gr.Cells[1]).Text, "");

                    zip.AddFile(Server.MapPath("~") + "\\Ziliao\\" + (gr.Cells[1]).Text, "");


                }
            }

            zip.Save(Response.OutputStream);
        }

        Response.End();

    }
}