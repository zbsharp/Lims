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
public partial class sysManage_personconfigconcel : System.Web.UI.Page
{
    protected int mokuaiid = 6;
    protected void Page_Load(object sender, EventArgs e)
    {

      

        if (!IsPostBack)
        {
            Bind();
             BindResponser();

        }

      
    }
    protected void BindResponser()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo order by username";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        response.DataSource = ds.Tables[0];
        response.DataTextField = "username";
        response.DataValueField = "username";
        response.DataBind();


        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataTextField = "username";
        DropDownList1.DataValueField = "username";
        DropDownList1.DataBind();

        con.Close();
    }

    public void Bind()
    {

      

        DateTime FD;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from PersonConfig where name1='" + response.SelectedValue + "' order by name2";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView1.DataSource = ds;
        GridView1.DataBind();

        con.Close();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }


    protected void bnQuery_ServerClick1(object sender, EventArgs e)
    {

        {
            Bind();
        }
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }



    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {


        int sid = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value.ToString());
        Response.Redirect("CustomerSee?Customerid=" + sid + "");


    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");

            if (hzf.Checked)
            {
               
                int sid = Convert.ToInt32(GridView1.DataKeys[gr.RowIndex].Value.ToString());
                string yewuyuan = "";
                string zhu = "";

                string sql = "select name2,name1 from PersonConfig where id='" + sid + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader drd = cmd.ExecuteReader();
                if (drd.Read())
                {
                    yewuyuan = drd["name2"].ToString();
                    zhu = drd["name1"].ToString();
                }


                drd.Close();


                SqlTransaction trs = con.BeginTransaction();

                SqlCommand cmdinsert = new SqlCommand();
                cmdinsert.Connection = con;
                cmdinsert.Transaction = trs;

                try
                {
                    Random rd = new Random();
                    string rd1 = rd.Next(1000).ToString();
                    string dataName = DateTime.Now.ToString("yyyyMMddhhmmss") + rd1;
                   
                    string sqlinsert3 = "delete PersonConfig where name1='" + zhu + "' and name2='" + yewuyuan + "'";
                    cmdinsert.CommandText = sqlinsert3;
                    cmdinsert.ExecuteNonQuery();
                    trs.Commit();
                    //string WorkInfo = "取消了" + zhu + "的" + yewuyuan + "";
                    //SqlInsert ql = new SqlInsert();
                    //ql.InsertWorkLog(Session["UserName"].ToString(), DateTime.Now.ToString(), WorkInfo);


                }
                catch (SqlException kk)
                {

                    trs.Rollback();
                }

               
            }


        }
        con.Close();
        con.Dispose();

        Bind();

        ld.Text ="<script>alert('配置成功')</script>";


    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

        int i;
        if (((CheckBox)sender).Checked)
        {
            for (i = 0; i < GridView1.Rows.Count; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked = true;
            }
        }
        else
        {
            for (i = 0; i < GridView1.Rows.Count; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked = false;
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        {
            Bind();
        }
    }
    protected void response_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from PersonConfig where name1='" + response.SelectedValue + "'";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView1.DataSource = ds;
        GridView1.DataBind();

        con.Close();
    }
    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from PersonConfig where name2='" +DropDownList1.SelectedValue + "' order by name1";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView1.DataSource = ds;
        GridView1.DataBind();

        con.Close();
    }
}
