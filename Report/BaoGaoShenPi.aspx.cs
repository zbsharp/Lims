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

public partial class Report_BaoGaoShenPi : System.Web.UI.Page
{
    protected string id = "";

    protected string src = "";
    protected string xiangmuid = "";
    protected string pp = "";
    protected string rwid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        // limit("报告审批");
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();
        id = Request.QueryString["baogaoid"].ToString();

        bool A = false;
        A = limit1("报告审批");
        if (A == true)
        {
            Button1.Enabled = true;
        }
        else
        {
            Button1.Enabled = false;
        }

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        if (Request.QueryString["pp"] != null)
        {
            pp = Request.QueryString["pp"].ToString();
            string sqlp = "select * from baogao2 where baogaoid='" + id + "' and fafangby !=''";
            SqlCommand cmdp = new SqlCommand(sqlp, con1);
            SqlDataReader drp = cmdp.ExecuteReader();
            if (drp.Read())
            {

            }
            else
            {
                Label3.Text = "该报告未发放，不能下载";
                GridView5.Visible = false;
            }
            drp.Close();
            Button1.Visible = false;
        }
        xiangmuid = id;
        string sqlx1 = "select *,(select top 1 shenheren from baogaoshenhe2 where shenhebianhao=baogao2.baogaoid) as shenheby1 from baogao2 where baogaoid='" + id + "'";
        SqlCommand comx1 = new SqlCommand(sqlx1, con1);
        SqlDataReader drx1 = comx1.ExecuteReader();
        if (drx1.Read())
        {
            rwid = drx1["tjid"].ToString();
        }
        drx1.Close();
        con1.Close();
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sqlx = "select *,(select top 1 shenheren from baogaoshenhe2 where shenhebianhao=baogao2.baogaoid) as shenheby1 from baogao2 where baogaoid='" + id + "'";
            SqlCommand comx = new SqlCommand(sqlx, con);
            SqlDataReader drx = comx.ExecuteReader();
            //if (drx.Read())
            //{
            //    TextBox1.Text = drx["shenheby1"].ToString();

            //    TextBox2.Text = drx["pizhunby"].ToString();

            //    rwid = drx["tjid"].ToString();
            //    if (TextBox2.Text == "")
            //    {
            //        TextBox2.Text = "吴立安";
            //    }
            //    if (drx["pizhundate"].ToString().Substring(0, 4) == "1900")
            //    {
            //        TextBox3.Text = DateTime.Now.ToShortDateString();
            //    }
            //    else
            //    {
            //        TextBox3.Text = Convert.ToDateTime(drx["pizhundate"].ToString()).ToShortDateString();
            //    }
            //}
            drx.Close();
            con.Close();
            Bindfuwufujian();
            TextBox4.Text = id;
            TextBox5.Text = id;
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


    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        //string sqlrw = "select state from anjianinfo2 where rwbianhao='"+rwid+"' and state='暂停'";
        //SqlCommand cmdrw = new SqlCommand(sqlrw,con);
        //SqlDataReader drrw = cmdrw.ExecuteReader();
        //if (drrw.Read())
        //{
        //    drrw.Close();
        //    con.Close();
        //    //ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "该任务处于暂停状态，不能批准报告');", true);
        //    ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('该任务处于暂停状态，不能批准报告');", true);
        //}
        //else
        {
            // drrw.Close();


            string sql = "update baogao2 set statebumen1='合格',statebumen2='合格',state='已缮制', shenheby='" + Session["Username"].ToString() + "',pizhunby='" + Session["Username"].ToString() + "',pizhundate='" + DateTime.Now + "' where baogaoid='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            string sqlr = "select beizhu3 from anjianinfo2 where rwbianhao=(select tjid from baogao2 where baogaoid='" + id + "') and beizhu3=''";
            SqlCommand cmdr = new SqlCommand(sqlr, con);
            SqlDataReader drr = cmdr.ExecuteReader();
            if (drr.Read())
            {
                drr.Close();

                string sqlstate4 = "update anjianinfo2 set state='完成',beizhu3='" + DateTime.Now + "' where rwbianhao='" + rwid + "'";
                SqlCommand cmdstate4 = new SqlCommand(sqlstate4, con);
                cmdstate4.ExecuteNonQuery();
            }
            else
            {
                drr.Close();

                string sqlstate4 = "update anjianinfo2 set state='已缮制' where rwbianhao='" + rwid + "'";
                SqlCommand cmdstate4 = new SqlCommand(sqlstate4, con);
                cmdstate4.ExecuteNonQuery();
            }


            string sqlfujian1 = "update baogaofujian set caseid ='" + TextBox5.Text.Trim() + "' where caseid='" + id + "'";
            SqlCommand cmdfujian = new SqlCommand(sqlfujian1, con);
            cmdfujian.ExecuteNonQuery();

            string sqlfujian2 = "update baogaofujian2 set caseid ='" + TextBox5.Text.Trim() + "' where caseid='" + id + "'";
            SqlCommand cmdfujian2 = new SqlCommand(sqlfujian2, con);
            cmdfujian2.ExecuteNonQuery();


            string sqlfujian3 = "update baogaobumen set baogaohao ='" + TextBox5.Text.Trim() + "' where baogaohao='" + id + "'";
            SqlCommand cmdfujian3 = new SqlCommand(sqlfujian3, con);
            cmdfujian3.ExecuteNonQuery();

            string sqlfujian4 = "update baogaoshenhe set shenhebianhao ='" + TextBox5.Text.Trim() + "' where shenhebianhao='" + id + "'";
            SqlCommand cmdfujian4 = new SqlCommand(sqlfujian4, con);
            cmdfujian4.ExecuteNonQuery();


            string sqlfujian5 = "update baogaoshenhe2 set shenhebianhao ='" + TextBox5.Text.Trim() + "' where shenhebianhao='" + id + "'";
            SqlCommand cmdfujian5 = new SqlCommand(sqlfujian5, con);
            cmdfujian5.ExecuteNonQuery();



            string sql2 = "update baogao2 set jiubaogao=baogaoid, baogaoid='" + TextBox5.Text.Trim() + "' where baogaoid='" + id + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.ExecuteNonQuery();



            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('提交成功');", true);

            con.Close();


            MyExcutSql my = new MyExcutSql();
            my.ExtTaskone(id, rwid, "批准报告", "手工提交", Session["UserName"].ToString(), "批准报告", DateTime.Now, "已缮制");
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
                string sql = "insert baogaofujian2 values('" + xiangmuid + "','" + ImageUrl + "','" + fileName + "','" + type + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + xiangmuid + "','','','')";
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
        string sql1 = "select * from baogaofujian2 where caseid='" + xiangmuid + "' or baojiaid='" + xiangmuid + "'";
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
        //if (pp == "" && TextBox2.Text.Trim() == "")
        //{
        //    string src2 = "";
        //    int Appurtenanceid = Convert.ToInt32(GridView5.DataKeys[e.RowIndex].Value.ToString());


        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //    con.Open();

        //    string sql2 = "select * from baogaofujian2 where id=" + Appurtenanceid + " and fillname='" + Session["UserName"].ToString() + "'";
        //    SqlCommand cmd = new SqlCommand(sql2, con);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.Read())
        //    {
        //        src2 = dr["filename"].ToString();
        //    }
        //    dr.Close();

        //    string sql = "delete from baogaofujian2 where id=" + Appurtenanceid + "";
        //    SqlCommand com = new SqlCommand(sql, con);
        //    com.ExecuteNonQuery();

        //    string url = Server.MapPath("~") + "\\upfiles\\" + src2;
        //    if (File.Exists(url))
        //    {
        //        File.Delete(url);
        //    }
        //    this.Label2.Text = "删除成功";
        //    con.Close();
        //    Bindfuwufujian();
        //}
    }
}