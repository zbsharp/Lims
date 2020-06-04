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

public partial class Case_KuaiDiSee : System.Web.UI.Page
{
    protected string id = "";
    protected string baogaoid = "";
    protected string biaozhi = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();
        id = Request.QueryString["id"].ToString();
        if (Request.QueryString["biaozhi"] != null)
        {
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = false;

            string sqlkz = "select top 1 bianhao from kuaidizibiao where neirong = '" + id + "'";
            MyExcutSql ext = new MyExcutSql();
            id = ext.ExcutSql2(sqlkz,0);
            if (id == "no")
            {
                id = "";
            }
           

            
        }
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sqlx = "select * from KuaiDi where id='" + id + "'";
            SqlCommand comx = new SqlCommand(sqlx, con);
            SqlDataReader drx = comx.ExecuteReader();
            if (drx.Read())
            {
                TextBox1.Text = drx["gongsi"].ToString();
                TextBox2.Text = drx["bianhao"].ToString();
                TextBox3.Text = drx["jijianren"].ToString();
                TextBox4.Text = drx["jijiandianhua"].ToString();
                TextBox5.Text = Convert.ToDateTime(drx["jijianriqi"].ToString()).ToShortDateString();
                TextBox6.Text = drx["shoujianren"].ToString();
                TextBox7.Text = drx["shoujiandizhi"].ToString();
                TextBox8.Text = drx["shoujiandanwei"].ToString();
                TextBox9.Text = drx["shoujiandianhua"].ToString();
                TextBox10.Text = drx["beizhu"].ToString();
            }
            drx.Close();
            con.Close();
            Bind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sql = "update KuaiDi set gongsi='" + TextBox1.Text + "',bianhao='" + TextBox2.Text + "',jijianren='" + TextBox3.Text + "',jijiandianhua='" + TextBox4.Text + "',jijianriqi='" + TextBox5.Text + "',shoujianren='" + TextBox6.Text + "',shoujiandizhi='" + TextBox7.Text + "',shoujiandanwei='" + TextBox8.Text + "',shoujiandianhua='" + TextBox9.Text + "',beizhu='" + TextBox10.Text + "' where id ='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            string sqlx = "update kuaidizibiao set kuaidiid='" + TextBox2.Text + "' where bianhao ='" + id + "'";
            SqlCommand cmdx = new SqlCommand(sqlx, con);
            cmdx.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('修改成功');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sqlx = "delete from kuaidizibiao where bianhao ='" + id + "' and fillname='"+Session["UserName"].ToString()+"'";
            SqlCommand cmdx = new SqlCommand(sqlx, con);
            cmdx.ExecuteNonQuery();

            string sql = "delete from KuaiDi where id ='" + id + "' and fillname='" + Session["UserName"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('已删除成功');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from kuaidizibiao where (bianhao='" + id + "' or (bianhao='"+TextBox2.Text+"' and bianhao !=''))  order by id asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


    

        string sql21 = "select distinct baogaoid from baogao2";
        SqlDataAdapter ad21 = new SqlDataAdapter(sql21, con);
        DataSet ds21 = new DataSet();
        ad21.Fill(ds21);
        DropDownList2.DataSource = ds21.Tables[0];
        DropDownList2.DataTextField = "baogaoid";
        DropDownList2.DataValueField = "baogaoid";
        DropDownList2.DataBind();


        con.Close();
        con.Dispose();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {

            string sql = "";
           // if (DropDownList1.SelectedValue != "报告")
            {
                 sql = "insert into kuaidizibiao values('" + id + "','" + TextBox2.Text + "','" + DropDownList1.SelectedValue + "','" + TextBox11.Text + "','" + TextBox12.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "')";
            }
            //else
            //{
            //    sql = "insert into kuaidizibiao values('" + id + "','" + TextBox2.Text + "','" + DropDownList1.SelectedValue + "','" + DropDownList2.SelectedValue + "','" + TextBox12.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "')";
 
            //}
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            if(DropDownList1.SelectedValue =="样品")
            {
                string sql2 = "insert into YangPin2Detail values('" + TextBox11.Text.Trim() + "','" + TextBox11.Text.Trim() + "','" + TextBox3.Text + "','"+DateTime.Now+"','" + DropDownList1.SelectedValue + "','" + TextBox7.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','','','','','')";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    cmd2.ExecuteNonQuery();

                    string sqlx = "update YangPin2 set state='清退',pub_field1='" + DateTime.Now + "' where sampleid='" + TextBox11.Text.Trim() + "'";
                    SqlCommand cmdx = new SqlCommand(sqlx, con);
                    cmdx.ExecuteNonQuery();


                   
            }

            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('已提交成功！');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
        }
        finally
        {
            con.Close();
            Bind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        if (Request.QueryString["biaozhi"] != null) { }
        else
        {

            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sql = "delete from kuaidizibiao where id='" + id + "' and fillname='"+Session["UserName"].ToString()+"'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        Bind();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "报告")
        {
            DropDownList2.Visible = true;
            TextBox11.Visible = true;
        }
        else
        {
            DropDownList2.Visible = false;
            TextBox11.Visible = true;
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Print/KuaiDi.aspx?bianhao=" + TextBox2.Text);
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Print/KuaiDi2.aspx?bianhao=" + TextBox2.Text);
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Print/KuaiDi3.aspx?bianhao=" + TextBox2.Text+"&&id="+id);
    }
}