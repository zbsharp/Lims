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

public partial class Income_ShuiPiaoAdd : System.Web.UI.Page
{
    protected string bb = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bool bu = limit1("财务对账");
            if (bu)
            {
                fapiao();
                //if (bb == "shi")
                //{
                //    DropDownList3.SelectedValue = "是";
                //}
                //else
                //{
                //    DropDownList3.SelectedValue = "否";
                //}
            }
            else
            {
                Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Literal2.Text = string.Empty;
        Random seed = new Random();
        Random randomNum = new Random(seed.Next());


        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string shoufeiid = randomNum.Next().ToString() + DateTime.Now.ToString("yyyyMMdd_hhmmss");


        string hh = "insert into shuipiao values('" + shoufeiid + "','" + cp.Text.Trim() + "','" + guige.Text.Trim() + "','" + TextBox1.Text.Trim() + "','" + DropDownList2.SelectedValue + "','" + TextBox4.Text.Trim() + "','" + DropDownList1.SelectedValue + "','', '','1900-1-1','','','','','','" + DateTime.Now.Date + "','','','否','" + DateTime.Now + "','" + Session["UserName"].ToString() + "','" + DateTime.Now.Date + "')";

        SqlCommand cmd = new SqlCommand(hh, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        fapiao();

    }

    protected void fapiao()
    {
        string sql = "select * from shuipiao where (queren='' or queren='否' or queren='半确认') and (beizhu2='否' or beizhu2 is null) order by id desc";

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        SqlDataAdapter ad = new SqlDataAdapter(sql, con1);
        DataSet ds = new DataSet();
        ad.Fill(ds);


        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


        //GridView1.DataSource = ds.Tables[0];
        //GridView1.DataBind();
        con1.Close();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string liushuihao = "";
        string queren = "";
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();

        string sql2 = "select liushuihao,queren from shuipiao where id='" + id + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con1);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            liushuihao = dr2["liushuihao"].ToString();
            queren = dr2["queren"].ToString();
        }
        dr2.Close();

        string sql3 = "select * from cashin2 where daid='" + liushuihao + "'";
        SqlCommand cmd3 = new SqlCommand(sql3, con1);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {
            dr3.Close();
            Literal2.Text = "<script>alert('已有对账记录的不能删除')</script>";
        }
        else
        {

            dr3.Close();
            string sql = "delete from shuipiao where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con1);
            cmd.ExecuteNonQuery();
        }
        con1.Close();
        fapiao();
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }
}