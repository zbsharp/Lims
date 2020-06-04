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

public partial class Customer_ContractAdd : System.Web.UI.Page
{
    #region 初始绑定
    protected string name;
    protected string id;
    protected string username = "";
    protected string src = "";
   
    protected string kehuid = "";
    protected string kehuleixing = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"] == null)
        {
            Response.Write("<script>alert('请先登录!');window.location.href='../Login.aspx'</script>");
        }

        else
        {

            id = Request.QueryString["kehuid"].ToString();
            username = Session["UserName"].ToString();

            kehuid = Request.QueryString["kehuid"].ToString();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select customname,kehuid,CustomType from customer where kehuid='" + id + "' ";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                name = dr["customname"].ToString();
                kehuid = dr["kehuid"].ToString();
                kehuleixing = dr["CustomType"].ToString();

            }
            dr.Close();



          
            string sql2 = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='协议管理'";
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





            if (!IsPostBack)
            {
                Bind();
                Bindfuwufujian();

            }

            con.Close();
            con.Dispose();


            DataBind();
        }

    }

    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CustomerContract where  kehuid='" + id + "' ";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();
        con.Close();
        con.Dispose();

    }

 
    #endregion

    #region 保存协议信息
    protected void Button1_Click(object sender, EventArgs e)
    {


      
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        try
        {

           

            string sql = "insert into CustomerContract values('" + id + "','" + txContent.Value.Trim() + "','" + Convert.ToDateTime(Text2.Value.Trim()) + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";

            SqlCommand com = new SqlCommand(sql, con);

            com.ExecuteNonQuery();


            Bind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            con.Close();
        }

    }
    #endregion

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
                string fileFullname = DateTime.Now.ToShortDateString() + kehuid + rand + this.FileUpload1.FileName;
                string fileName = fileFullname.Substring(fileFullname.LastIndexOf("\\") + 1);
                string type = fileFullname.Substring(fileFullname.LastIndexOf(".") + 1);
                this.FileUpload1.SaveAs(Server.MapPath("../upfiles") + "\\" + fileFullname);
                string ProImg = "upfiles/" + fileFullname;
                string ImageUrl = "~/upfiles/" + fileFullname;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sql = "insert xieyifujian values('" + kehuid + "','" + ImageUrl + "','" + fileName + "','" + type + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + kehuid  + "','')";
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

 

    protected void GridView5_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string src2 = "";
        int Appurtenanceid = Convert.ToInt32(GridView5.DataKeys[e.RowIndex].Value.ToString());


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql2 = "select * from xieyifujian where id=" + Appurtenanceid + "";
        SqlCommand cmd = new SqlCommand(sql2, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            src2 = dr["filename"].ToString();
        }
        dr.Close();

        string sql = "delete from xieyifujian where id=" + Appurtenanceid + "";
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

    public void Bindfuwufujian()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql1 = "select * from xieyifujian where caseid='" + kehuid + "' or baojiaid='" + kehuid + "'";
        SqlDataAdapter da = new SqlDataAdapter(sql1, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        this.GridView5.DataSource = ds;
        this.GridView5.DataBind();
        con.Close();
        con.Dispose();
    }

}