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

public partial class Case_DianZiShangBao : System.Web.UI.Page
{
    protected string xiangmuid = "";
    protected string src = "";
    protected string baojiaid = "";
    protected string rw = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        xiangmuid = Request.QueryString["id"].ToString();
        rw = xiangmuid;

        if (!IsPostBack)
        {
            Bindfuwufujian();
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {

        if (DropDownList1.SelectedValue == "电子上报")
        {
            if (FileUpload1.PostedFile.ContentLength == 0)
            {
                src = "";
                this.Label2.Text = "请选择上传附件";
            }
            else
            {

                Random rd = new Random();
                string rand = rd.Next(0, 99999).ToString();
                string fileFullname = DateTime.Now.ToShortDateString() + xiangmuid + rand + this.FileUpload1.FileName;
                string fileName = fileFullname.Substring(fileFullname.LastIndexOf("\\") + 1);
                string type = fileFullname.Substring(fileFullname.LastIndexOf(".") + 1);
                this.FileUpload1.SaveAs(Server.MapPath("../DianZiShangBao") + "\\" + fileFullname);
                string ProImg = "DianZiShangBao/" + fileFullname;
                string ImageUrl = "~/DianZiShangBao/" + fileFullname;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sql = "insert fuwufujian2 values('" + xiangmuid + "','" + ImageUrl + "','" + fileName + "','" + type + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + TextBox1.Text + "','" + TextBox2.Text + "')";
                SqlCommand com = new SqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
                Bindfuwufujian();
                this.Label2.Text = "上传附件成功";
                TextBox1.Text = "";
            }
        }
        else
        {
           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "insert fuwufujian2 values('" + xiangmuid + "','','','','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + TextBox1.Text + "','" + TextBox2.Text + "')";
            SqlCommand com = new SqlCommand(sql, con);
            com.ExecuteNonQuery();
            con.Close();
            Bindfuwufujian();
            this.Label2.Text = "保存成功";
            TextBox1.Text = "";
        }
    }

    public void Bindfuwufujian()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql1 = "select * from fuwufujian2 where caseid='" + xiangmuid + "' and leibie !=''";
        SqlDataAdapter da = new SqlDataAdapter(sql1, con);
        DataSet ds = new DataSet();
        da.Fill(ds);


     
            string sqlhuaxue = "select * from ShangBaoType order by id desc";
            SqlDataAdapter ad9 = new SqlDataAdapter(sqlhuaxue, con);
            DataSet ds9 = new DataSet();
            ad9.Fill(ds9);
            DropDownList1.DataSource = ds9.Tables[0];

            DropDownList1.DataTextField = "name";
            DropDownList1.DataValueField = "name"; ;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("", ""));//
        


        con.Close();
        con.Dispose();
        this.GridView5.DataSource = ds;
        this.GridView5.DataBind();

   

    }

    protected void GridView5_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string src2 = "";
        int Appurtenanceid = Convert.ToInt32(GridView5.DataKeys[e.RowIndex].Value.ToString());


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql2 = "select * from fuwufujian2 where id='" + Appurtenanceid + "'";
        SqlCommand cmd = new SqlCommand(sql2, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            src2 = dr["filename"].ToString();
        }
        dr.Close();

        string sql = "delete from fuwufujian2 where id='" + Appurtenanceid + "'and fillname='" + Session["UserName"].ToString() + "'";
        SqlCommand com = new SqlCommand(sql, con);
        int i = com.ExecuteNonQuery();

        if (i > 0)
        {
            string url = Server.MapPath("~") + "\\DianZiShangBao\\" + src2;
            if (File.Exists(url))
            {
                File.Delete(url);
            }


            this.Label2.Text = "删除成功";
            con.Close();
            Bindfuwufujian();
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "纸档上报")
        {
            FileUpload1.Visible = false;
        }
        else
        {
            FileUpload1.Visible = true;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Print/PrintShangBao2.aspx?bianhao=" + xiangmuid);
    }
}