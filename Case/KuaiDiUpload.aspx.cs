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

public partial class Case_KuaiDiUpload : System.Web.UI.Page
{
    protected string xiangmuid = "";
    protected string src = "";
    protected string baojiaid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        xiangmuid = Request.QueryString["id"].ToString();
        string a = DateTime.Now.Year.ToString();
      
   

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
                this.FileUpload1.SaveAs(Server.MapPath("../upfiles") + "\\" + fileFullname);
                string ProImg = "upfiles/" + fileFullname;
                string ImageUrl = "~/upfiles/" + fileFullname;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sql = "insert KuaiDiFuJian values('" + xiangmuid + "','" + ImageUrl + "','" + fileName + "','" + type + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + baojiaid + "','')";
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
        string sql1 = "select * from KuaiDiFuJian where caseid='" + xiangmuid + "'";
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

        string sql2 = "select * from KuaiDiFuJian where id=" + Appurtenanceid + "";
        SqlCommand cmd = new SqlCommand(sql2, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            src2 = dr["filename"].ToString();
        }
        dr.Close();

        string sql = "delete from KuaiDiFuJian where id=" + Appurtenanceid + "";
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