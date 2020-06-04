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
public partial class sysManage_personConfig : System.Web.UI.Page
{
    protected int mokuaiid = 6;

    private int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            Bind();
            BindResponser();


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


                con3.Close();

            


        }

        
    }
    protected void BindResponser()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo  order by username";
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

        string sql = "select * from userinfo order by username";
       
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
               
                int sid = Convert.ToInt32(GridView1.DataKeys[gr.RowIndex].Value.ToString());
                string yewuyuan = "";

               
                string sql = "select username from userinfo where id='" + sid + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader drd = cmd.ExecuteReader();
                if (drd.Read())
                {
                    yewuyuan = drd["username"].ToString();
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

                    

                    string sqlinsert3 = "insert into PersonConfig values('" + response.SelectedValue + "','" + yewuyuan + "','" + Session["UserName"].ToString() + "')";
                    cmdinsert.CommandText = sqlinsert3;
                    cmdinsert.ExecuteNonQuery();
                    trs.Commit();
                    //string WorkInfo = "配置了" + yewuyuan + "给" + response.SelectedValue + "";
                    //SqlInsert ql = new SqlInsert();

                    //ql.InsertWorkLog(Session["UserName"].ToString(), DateTime.Now.ToString(), WorkInfo);

                    ld.Text = "<script>alert('配置成功');this.location.href='../SysManage/PersonConfig.aspx'</script>";
                }
                catch (SqlException kk)
                {

                    trs.Rollback();
                }

              
            }


        }

        con.Close();
        con.Dispose();

        //Bind();
        


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

        string sql = "select * from userinfo where departmentname='"+DropDownList1.SelectedValue+"' order by username";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView1.DataSource = ds;
        GridView1.DataBind();

        con.Close();
    }
    protected void Button4_Click(object sender, EventArgs e)
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


                string sql = "select username from userinfo where id='" + sid + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader drd = cmd.ExecuteReader();
                if (drd.Read())
                {
                    yewuyuan = drd["username"].ToString();
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



                    string sqlinsert3 = "insert into PersonConfig values('" + yewuyuan + "','" + response.SelectedValue + "','" + Session["UserName"].ToString() + "')";
                    cmdinsert.CommandText = sqlinsert3;
                    cmdinsert.ExecuteNonQuery();
                    trs.Commit();
                    //string WorkInfo = "配置了" + yewuyuan + "给" + response.SelectedValue + "";
                    //SqlInsert ql = new SqlInsert();

                    //ql.InsertWorkLog(Session["UserName"].ToString(), DateTime.Now.ToString(), WorkInfo);
                    ld.Text = "<script>alert('配置成功');this.location.href='../SysManage/PersonConfig.aspx'</script>";
              
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
        
    }
}
