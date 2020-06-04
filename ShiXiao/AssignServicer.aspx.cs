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

public partial class ShiXiao_AssignServicer : System.Web.UI.Page
{
    protected int mokuaiid = 6;

    private int _i = 0;
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
        string sql = "select * from userinfo where username in (select name from ModuleDuty where modulename='项目经理') order by username asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        response.DataSource = ds.Tables[0];
        response.DataTextField = "username";
        response.DataValueField = "username";
        response.DataBind();

        con.Close();
    }

    public void Bind()
    {



        DateTime FD;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "";
        if (TextBox1.Text == "")
        {
            sql = "select top 15 * from customer  where customname !='' order by id desc";
        }
        else
        {
            sql = "select * from customer where customname like '%" + TextBox1.Text.Trim() + "%' or fillname like '%" + TextBox1.Text.Trim() + "%' or providername like '%" + TextBox1.Text.Trim() + "%' order by customname";

        }

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

            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");


            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
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

                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
             


                string sql = "update customer set providername='"+response.SelectedValue+"' where kehuid='" + sid + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                string sql2 = "update customered set providername='" + response.SelectedValue + "' where kehuid='" + sid + "'";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.ExecuteNonQuery();

            }


        }

        con.Close();
        con.Dispose();

       Bind();



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
    protected void Button2_Click1(object sender, EventArgs e)
    {
        Response.Redirect("personconfigconcel.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from customer where customname like '%" + TextBox1.Text.Trim() + "%' or fillname like '%" + TextBox1.Text.Trim() + "%' or providername like '%" + TextBox1.Text.Trim() + "%' order by customname";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView1.DataSource = ds;
        GridView1.DataBind();

        con.Close();
    }
   
}