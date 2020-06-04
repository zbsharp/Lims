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

public partial class Report_ChuCuoAdd : System.Web.UI.Page
{
    protected string kehuid = "";
    protected string baogaoid = "";
    protected string rwbianhao = "";
    protected string applyid = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        baogaoid = Request.QueryString["baogaoid"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql1 = "select * from baogao2 where baogaoid='" + baogaoid + "' order by baogaoid";
        SqlCommand cmd = new SqlCommand(sql1, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            rwbianhao = dr["tjid"].ToString();
        }
        dr.Close();

        string sql2 = "select * from anjianinfo2 where rwbianhao='" + rwbianhao + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            kehuid = dr2["kehuid"].ToString();
            applyid = dr2["shenqingbianhao"].ToString();
        }
        dr2.Close();
        if (!IsPostBack)
        {
            limit("质量录入");
            TextBox2.Text = baogaoid;
            TextBox3.Text = applyid;

            BindDep();
            Bind();
            string sqldate = "select * from baogao2 where baogaoid='" + baogaoid + "' order by baogaoid";
            SqlCommand cmddate = new SqlCommand(sqldate, con);
            SqlDataReader drdate = cmddate.ExecuteReader();
            if (drdate.Read())
            {
                TextBox11.Text = drdate["pizhundate"].ToString();
            }
            drdate.Close();
            //if (Convert.ToDateTime(TextBox11.Text) < Convert.ToDateTime("1902-1-1"))
            //{
            TextBox11.Text = DateTime.Now.ToShortDateString();
            //}
        }
        con.Close();

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sql = "insert into BaoGaoChaCuo values('" + TextBox2.Text + "','" + rwbianhao + "','" + TextBox3.Text + "','" + kehuid + "','" + DropDownList1.SelectedValue + "','" + DropDownList2.SelectedValue + "','" + DropDownList3.SelectedValue + "','" + DropDownList4.SelectedValue + "','" + Convert.ToDateTime(TextBox11.Text.Trim()) + "','" + Convert.ToDateTime(TextBox11.Text.Trim()).Month + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + TextBox10.Text.Trim() + "','" + DropDownList5.SelectedValue + "','')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            // ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('保存成功');", true);



        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
        }
        finally
        {
            con.Close();

            Bind();
        }
    }

    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select * from UserDepa";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataValueField = "name";
        DropDownList1.DataTextField = "name";
        DropDownList1.DataBind();


        string sql2 = "select distinct name from UserChuCuo ";
        SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con3);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);
        DropDownList3.DataSource = ds2.Tables[0];
        DropDownList3.DataValueField = "name";
        DropDownList3.DataTextField = "name";
        DropDownList3.DataBind();


        con3.Close();
    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from BaoGaoChaCuo where baogaoid='" + TextBox2.Text + "' order by id asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }


    protected void BindGroup1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo where departmentname='" + DropDownList1.SelectedValue + "'  order by username asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataTextField = "username";
        DropDownList2.DataValueField = "username";
        DropDownList2.DataBind();
        con.Close();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo where department='" + DropDownList1.SelectedValue + "'  order by username asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataTextField = "username";
        DropDownList2.DataValueField = "username";
        DropDownList2.DataBind();
        con.Close();
    }


    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from UserChuCuo where name='" + DropDownList3.SelectedValue + "'  order by wenyuan asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList4.DataSource = ds.Tables[0];
        DropDownList4.DataTextField = "wenyuan";
        DropDownList4.DataValueField = "wenyuan";
        DropDownList4.DataBind();
        con.Close();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "delete from BaoGaoChaCuo where id='" + id + "' and fillname='" + Session["UserName"].ToString() + "'";
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

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string uuname2 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
        string uuname = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());
        string uuname4 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
        string uuname3 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());


        string uuname5 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
        string uuname6 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text.ToString());

        string uuname7 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[7].Controls[0]).Text.ToString());


        string sql = "update BaoGaoChaCuo set beizhu2='" + uuname7 + "', month='" + Convert.ToDateTime(uuname6).Month + "', time='" + Convert.ToDateTime(uuname6) + "', bumen='" + uuname + "',gongchengshi='" + uuname2 + "',fenlei='" + uuname3 + "',leirong='" + uuname4 + "',beizhu1='" + uuname5 + "' where id='" + KeyId + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        GridView1.EditIndex = -1;
        Bind();
    }

}