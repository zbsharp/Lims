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

public partial class Report_BaoGaoShenPi2 : System.Web.UI.Page
{
    protected string baogaoid = "";
    protected string src = "";
    protected string sp = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();
        baogaoid = Request.QueryString["baogaoid"].ToString();
        Label1.Text = Request.QueryString["baogaoid"].ToString();


        if (!IsPostBack)
        {
            Literal1.Text = string.Empty;
            Bindfuwufujian();
        }
    }

    protected void limit(string pagename1)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
        }
        else
        {
            con.Close();
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
    }

    protected bool limit1(string pagename1)
    {
        bool A = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
            A = true;
        }
        else
        {
            con.Close();
            A = false;
        }
        return A;
    }




    protected void Button5_Click(object sender, EventArgs e)
    {
        if (FileUpload1.PostedFile.ContentLength == 0)
        {
            Literal1.Text = "<script>alert('请选择文件')</script>";
            return;
        }

        double maxsize = Convert.ToDouble(ConfigurationManager.AppSettings["Length"]);//获取webconfig中配置好的最大上传文件
        double filesize = Convert.ToDouble(FileUpload1.FileContent.Length) / 1024.0;//获取当前上传文件的大小、并转成KB
        if (filesize > maxsize)
        {
            Literal1.Text = "<script>alert('上传文件过大')</script>";
            return;
        }

        string type = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf('.') + 1).ToLower();//获取文件后缀并转成小写
        if (type == "exe")
        {
            Literal1.Text = "<script>alert('不能上传exe文件')</script>";
            return;
        }

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        Random rd = new Random();
        string rand = rd.Next(0, 99999).ToString();
        string newfilename = DateTime.Now.ToString("yyyy-MM-dd") + baogaoid + rand + this.FileUpload1.FileName;
        string path = Server.MapPath("BaogaoFile");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        this.FileUpload1.PostedFile.SaveAs(path + "\\" + newfilename);
        string url = path + "\\" + newfilename;//获取该文件在服务器上存放的路径
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "insert baogaofujian2 values('" + baogaoid + "','" + url + "','" + newfilename + "','" + type + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + baogaoid + "','','','')";
        SqlCommand com = new SqlCommand(sql, con);
        com.ExecuteNonQuery();
        con.Close();
        Bindfuwufujian();
        this.Label2.Text = "上传附件成功";
    }

    public void Bindfuwufujian()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql1 = "select * from baogaofujian2 where caseid='" + baogaoid + "'";
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
        string sql2 = "select * from baogaofujian2 where id=" + Appurtenanceid + "";
        SqlCommand cmd = new SqlCommand(sql2, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            src2 = dr["filename"].ToString();
        }
        dr.Close();
        string sql = "delete from baogaofujian2 where id=" + Appurtenanceid + "";
        SqlCommand com = new SqlCommand(sql, con);
        int i = com.ExecuteNonQuery();
        if (i == 1)
        {
            string url = Server.MapPath("BaogaoFile") + "\\" + src2;

            if (File.Exists(url))
            {
                File.Delete(url);
            }
            this.Label2.Text = "删除成功";
        }
        con.Close();
        Bindfuwufujian();
    }
}