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

public partial class Case_UploadFile : System.Web.UI.Page
{

    protected string xiangmuid = "";
    protected string src = "";
    protected string baojiaid = "";
    protected string dd = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        xiangmuid = Request.QueryString["id"].ToString();
        if (Request.QueryString["ddd"] != null)
        {
            dd = Request.QueryString["ddd"].ToString();

        }
        string a = DateTime.Now.Year.ToString();
        string sql = "";

        if (xiangmuid.Contains("SN"))
        {
            sql = "select baojiaid from yangpin2 where sampleid='" + xiangmuid + "'";
        }
        else if (xiangmuid.Contains(a))
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
                string fileFullname = DateTime.Now.ToString("yyyy-mm-dd") + xiangmuid + rand + this.FileUpload1.FileName;
                string type = fileFullname.Substring(fileFullname.LastIndexOf(".") + 1);

                string path = Server.MapPath("~/Upfiles\\");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                this.FileUpload1.PostedFile.SaveAs(path + "\\" + fileFullname);

                string ProImg = "Upfiles/" + fileFullname;
                string ImageUrl = "~/Upfiles/" + fileFullname;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sql = "insert fuwufujian values('" + xiangmuid + "','" + ImageUrl + "','" + fileFullname + "','" + type + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + TextBox1.Text + "','" + baojiaid + "','')";
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
        string sql1 = "";

        if (dd == "")
        {
            sql1 = "select * from fuwufujian where (caseid='" + xiangmuid + "')";
        }
        else if (dd == "1")
        {
            sql1 = "select * from fuwufujian where (caseid='" + xiangmuid + "') and (fillname='杜巍巍' or fillname='何自棋' or fillname='陈靖' or fillname='郑凤慈' or fillname='梁诗敏')";
        }
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

        string src2 = "";
        int Appurtenanceid = Convert.ToInt32(GridView5.DataKeys[e.RowIndex].Value.ToString());


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql2 = "select * from fuwufujian where id=" + Appurtenanceid + "";
        SqlCommand cmd = new SqlCommand(sql2, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            src2 = dr["filename"].ToString();
        }
        dr.Close();

        string sql = "delete from fuwufujian where id='" + Appurtenanceid + "' and fillname='" + Session["UserName"].ToString() + "'";
        SqlCommand com = new SqlCommand(sql, con);
        int i = com.ExecuteNonQuery();
        if (i > 0)
        {
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