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

public partial class Report_BaoGaoFaFang : System.Web.UI.Page
{
    protected string id = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string fafangby = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();
        id = Request.QueryString["id"].ToString();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sqly = "select baojiaid,kehuid from anjianinfo2 where rwbianhao =(select top 1 tjid from baogao2 where id='" + id + "')";
        SqlCommand cmdy = new SqlCommand(sqly, con);
        SqlDataReader dry = cmdy.ExecuteReader();
        if (dry.Read())
        {
            kehuid = dry["kehuid"].ToString();
            baojiaid = dry["baojiaid"].ToString();
        }
        dry.Close();

        if (!IsPostBack)
        {
            string sqlx = "select * from baogao2 where id='" + id + "'";
            SqlCommand comx = new SqlCommand(sqlx, con);
            SqlDataReader drx = comx.ExecuteReader();
            if (drx.Read())
            {
                //TextBox3.Text = drx["qianlinby"].ToString();
                //TextBox2.Text = drx["fafangby"].ToString();

                //if (drx["fafangdate"].ToString().Substring(0, 4) == "1900")
                //{
                //    TextBox4.Text = DateTime.Now.ToShortDateString();
                //}
                //else
                //{
                //    TextBox4.Text = Convert.ToDateTime(drx["fafangdate"].ToString()).ToShortDateString();
                //}

                //TextBox6.Text = drx["remark"].ToString();
                TextBox9.Text = drx["tjid"].ToString();
                TextBox10.Text = drx["dengjiby"].ToString();
            }
            drx.Close();


            //if (TextBox2.Text == "")
            //{
            //    TextBox2.Text = Session["UserName"].ToString();
            //    TextBox4.Text = DateTime.Now.ToShortDateString();
            //}
            Bindlianxi();
            //TextBox3.Visible = false;
        }




        string sqlxy = "select * from baogao2 where id='" + id + "'";
        SqlCommand comxy = new SqlCommand(sqlxy, con);
        SqlDataReader drxy = comxy.ExecuteReader();
        if (drxy.Read())
        {
            TextBox7.Text = drxy["baogaoid"].ToString();
            //TextBox8.Text = Convert.ToDateTime(drxy["pizhundate"]).ToShortDateString();
            fafangby = drxy["fafangby"].ToString();
        }
        drxy.Close();
        if (fafangby == "")
        {

        }
        else
        {

            string sql3 = "select * from baogao2 where id='" + id + "' and fafangby='" + Session["UserName"].ToString() + "'";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            if (dr3.Read() || Session["UserName"].ToString() == "admin")
            {

                //TextBox3.Visible = true;
                Button1.Text = "修改";
                //DropDownList1.Visible = false;
            }
            else
            {
                Button1.Visible = false;
            }

        }



        con.Close();


    }


    protected void Bindlianxi()
    {
        string strs = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql21 = "select * from CustomerLinkMan where  customerid='" + kehuid + "' ";
        SqlDataAdapter ad21 = new SqlDataAdapter(sql21, con);
        DataSet ds21 = new DataSet();
        ad21.Fill(ds21);
        //DropDownList1.DataSource = ds21.Tables[0];
        //DropDownList1.DataTextField = "name";
        //DropDownList1.DataValueField = "id";
        //DropDownList1.DataBind();
        //DropDownList1.Items.Insert(0, new ListItem("其他人", "其他人"));//



        if (baojiaid != "")
        {
            string linkman = "";


            string sqllink = "select * from baojialink where baojiaid='" + baojiaid + "'";
            SqlCommand cmdlink = new SqlCommand(sqllink, con);
            SqlDataReader drlink = cmdlink.ExecuteReader();
            if (drlink.Read())
            {
                linkman = drlink["linkid"].ToString();
            }
            drlink.Close();
            //DropDownList1.SelectedValue = linkman;

        }
        con.Close();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string qianlin = "";
        //if (TextBox3.Text.Trim() == "")
        //{
        //    qianlin = DropDownList1.SelectedItem.Text;
        //}
        //else
        //{
        //    qianlin = TextBox3.Text.Trim();
        //}

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sql = "update baogao2 set qianlinby='" + qianlin + "',fafangby='" + Session["Username"].ToString() + "',fafangdate='" + DateTime.Now + "',youjidanid=''  where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            string sql2 = "insert into baogaofafang values('" + TextBox9.Text.Trim() + "','" + TextBox7.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','','','')";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('保存成功');location.replace('BaoGaoShenPi.aspx?baogaoid=" + TextBox7.Text + "&&pp=1');", true);

            //Response.Write("<script>alert('保存成功');window.location.href='BaoGaoShenPi.aspx?baogaoid=" + TextBox7.Text + "&&pp=1'</script>");
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (DropDownList1.SelectedValue == "其他人")
        //{
        //    TextBox3.Visible = true;
        //}
        //else
        //{
        //    TextBox3.Visible = false;
        //}
    }
}