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

public partial class Quotation_QuoLink : System.Web.UI.Page
{
    protected string kehuid = "";
    protected string baojiaid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["username"] = "ccic";

      //  kehuid = Request.QueryString["kehuid"].ToString();
        baojiaid = Request.QueryString["baojiaid"].ToString();

        //kehuid = "kh110001";
        //baojiaid = "xz110001";

        if (!IsPostBack)
        {
            Bind();
        }
    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from kehulianxi where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "' order by id asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        try
        {
            string sql = "insert into kehulianxi values('" + kehuid + "','" + baojiaid + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" + TextBox9.Text + "','" + DropDownList1.SelectedValue + "','" + TextBox10.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            Label2.Text = "保存成功！";
        }
        catch (Exception ex)
        {
            Label2.Text = ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！";
        }
        finally
        {
            con.Close();
            con.Dispose();
            Bind();
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "delete from kehulianxi where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();

        Bind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Bind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView1.EditIndex = e.NewEditIndex;
        Bind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string KeyId = GridView1.DataKeys[e.RowIndex].Value.ToString();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string uuname1 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());
        string uuname2 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
        string uuname3 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
        string uuname4 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
        string uuname5 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
        string uuname6 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text.ToString());
        string uuname7 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[7].Controls[0]).Text.ToString());
        string uuname8 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[8].Controls[0]).Text.ToString());
        string uuname9 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[9].Controls[0]).Text.ToString());
        string uuname10 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[10].Controls[0]).Text.ToString());
        string uuname11 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[11].Controls[0]).Text.ToString());

        string sql = "update kehulianxi set bianhao='" + uuname1 + "',name='" + uuname2 + "',address='" + uuname3 + "',lianxiren='" + uuname4 + "',dianhua='" + uuname5 + "',shouji='" + uuname6 + "',youxiang='" + uuname7 + "',chuanzhen='" + uuname8 + "',beizhu='" + uuname9 + "',type='" + uuname10 + "',beizhu1='" + uuname11 + "' where id='" + KeyId + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();
        GridView1.EditIndex = -1;
        Bind();
    }
}