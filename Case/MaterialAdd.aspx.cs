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
using System.Text;
using System.IO;
using Common;

public partial class Case_MaterialAdd : System.Web.UI.Page
{


    protected string tijiaobianhao = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        tijiaobianhao = Request.QueryString["tijiaobianhao"].ToString();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from AnJianXinXi2 where bianhao='" + tijiaobianhao + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            baojiaid = dr["baojiaid"].ToString();
            kehuid = dr["kehuid"].ToString();
          


        }
        con.Close();
        if (!IsPostBack)
        {
            Bind();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string sql = "insert into material values('" + kehuid + "','" + baojiaid + "','" + tijiaobianhao + "','" + name1.Text.Trim() + "','" + name2.Text.Trim() + "','" + name3.Text.Trim() + "','" + name4.Text.Trim() + "','" + name5.Text.Trim() + "','" + name6.Text.Trim() + "','否','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','','')";
        SqlCommand cmd = new SqlCommand(sql,con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        Bind();
        ld.Text = "<script>alert('保存成功!');</script>";
      

    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *,(select top 1 customname from customer where kehuid =Material.kehuid) as kehuname  from Material where renwuhao='"+tijiaobianhao+"' order by id desc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
       
        GridView1.DataSource = ds.Tables [0];
        GridView1.DataBind();
        con.Close();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "update  Material set renwuhao='admin' where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();
        Bind();
    }
}