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

public partial class Income_BaoGaoFirstUpLoad : System.Web.UI.Page
{
    protected string xiangmuid = "";
    protected string src = "";
    protected string baojiaid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        xiangmuid = Request.QueryString["inid"].ToString();


        if (!IsPostBack)
        {
            Bindfuwufujian();
            BindDep();
        

        }
    }


    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select * from UserDepa where name=(select departmentname from userinfo where username='" + Session["UserName"].ToString() + "')";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


        DataSet ds = new DataSet();


        ad.Fill(ds);





        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataValueField = "name";
        DropDownList1.DataTextField = "name";
        DropDownList1.DataBind();




        con3.Close();
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
                this.FileUpload1.SaveAs(Server.MapPath("../ShouFeiFuJian") + "\\" + fileFullname);
                string ProImg = "ShouFeiFuJian/" + fileFullname;
                string ImageUrl = "~/ShouFeiFuJian/" + fileFullname;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sql = "insert ShouFeiFuJian values('" + xiangmuid + "','" + ImageUrl + "','" + fileName + "','" + type + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + baojiaid + "','','','')";
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
        string sql1 = "select * from ShouFeiFuJian where caseid='" + xiangmuid + "'";
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




   
            string sql2 = "select * from ShouFeiFuJian where id=" + Appurtenanceid + "";
            SqlCommand cmd = new SqlCommand(sql2, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                src2 = dr["filename"].ToString();
            }
            dr.Close();

            string sql = "delete from ShouFeiFuJian where id=" + Appurtenanceid + " and fillname='"+Session["UserName"].ToString()+"'";
            SqlCommand com = new SqlCommand(sql, con);
            int i = 0;
        
            i= com.ExecuteNonQuery();
            if (i > 0)
            {
                string url = Server.MapPath("~") + "\\ShouFeiFuJian\\" + src2;
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