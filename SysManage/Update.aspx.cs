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
using System.Data.SqlClient;
using Common;
using DBTable;
using System.Data.Common;
public partial class SysManage_Update : System.Web.UI.Page
{
    protected string id = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["id"].ToString() != "person")
        {
            id = Request.QueryString["id"].ToString();
        }
        else
        {
            Users User = new Users();
            string sql3 = "select * from userinfo where username='" + Session["UserName"].ToString() + "'";
            DbDataReader dr3 = User.Select(sql3);
            if (dr3.Read())
            {
                id = dr3["id"].ToString();
            }
            dr3.Close();
            DropDownList1.Enabled = false;
            Branch.Enabled = false;
            name.Enabled = false;
            drop_area.Enabled = false;
            // Label2.Text = "您不可自行修改";
            //TextBox2.Enabled = false;
            //TextBox3.Enabled = false;
            //name.Enabled = false;
            //workPhone.Enabled = false;
            //movePhone.Enabled = false;
            //email.Enabled = false;
            //Button3.Visible = false;
            //TextBox5.Enabled = false;
        }

        if (!IsPostBack)
        {
            Binddepartment();
            Bindduty();
            BindUser();
            Bind();
        }
    }

    protected void Binddepartment()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from UserDepa order by departmentid asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        Branch.DataSource = ds.Tables[0];
        Branch.DataTextField = "name";
        Branch.DataValueField = "departmentid";
        Branch.DataBind();
        con.Close();
    }
    protected void Bindduty()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from UserDuty order by dutyid asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataTextField = "name";
        DropDownList1.DataValueField = "dutyid";
        DropDownList1.DataBind();
        con.Close();
    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        int dutyid = 0;
        string jiaose = DropDownList1.SelectedValue;
        string jiaoname = DropDownList1.SelectedItem.Text;
        string department = Branch.SelectedItem.Text;
        string departmentid = Branch.SelectedValue;
        string dianhua = workPhone.Text;
        string yidong = movePhone.Text;
        string youxiang = email.Text;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "update userinfo set canceltime='" + TextBox5.Text.Trim() + "',jiaose='" + jiaose + "',jiaosename='" + jiaoname + "',dutyid='" + jiaose + "',dutyname='" + jiaoname + "',youxiang='" + youxiang + "',banggongdianhua='" + dianhua + "',yidong='" + yidong + "',departmentid='" + departmentid + "',departmentname='" + department + "',department='" + department + "',fax='" + TextBox2.Text.Trim() + "',shortphone='" + TextBox3.Text.Trim() + "',homelocation='" + drop_area.SelectedValue + "' where id='" + id + "' ";
        SqlCommand com1 = new SqlCommand(sql, con);
        com1.ExecuteNonQuery();


        ld.Text = "<script>alert('修改成功!')</script>";
        con.Close();
    }

    protected void BindUser()
    {
        Users User = new Users();
        string sql3 = "select * from userinfo where id='" + id + "'";
        DbDataReader dr3 = User.Select(sql3);
        if (dr3.Read())
        {
            DropDownList1.SelectedValue = dr3["jiaose"].ToString();
            Branch.SelectedValue = dr3["departmentid"].ToString();
            email.Text = dr3["youxiang"].ToString();
            workPhone.Text = dr3["banggongdianhua"].ToString();
            movePhone.Text = dr3["yidong"].ToString();
            name.Text = dr3["UserName"].ToString();
            //TextBox1.Text = dr3["password"].ToString();
            TextBox2.Text = dr3["fax"].ToString();
            TextBox3.Text = dr3["shortphone"].ToString();
            TextBox5.Text = dr3["canceltime"].ToString();
            drop_area.SelectedValue = dr3["homelocation"].ToString();
        }
        dr3.Close();
    }
    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo where flag !='是'  order by id";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager1.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager1.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();

        con.Close();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        AspNetPager1.Visible = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo where username like '%" + TextBox4.Text.Trim() + "%' or departmentname like '%" + TextBox4.Text.Trim() + "%' or dutyname like '%" + TextBox4.Text.Trim() + "%'  order by id";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);


        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TextBox1.Text.Trim()))
        {
            ld.Text = "<script>alert('密码不能为空!')</script>";
        }
        else
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string strMd5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(TextBox1.Text, "md5");
            string sql = "update userinfo set password='" + TextBox1.Text + "',npassword='" + TextBox1.Text + "',pw='" + strMd5 + "' where id='" + id + "' ";
            SqlCommand com1 = new SqlCommand(sql, con);
            com1.ExecuteNonQuery();
            ld.Text = "<script>alert('修改成功!')</script>";
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (true)//如果允许改变列宽
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("onmousemove", "SyDG_moveOnTd(this)");
                e.Row.Cells[i].Attributes.Add("onmousedown", "SyDG_downOnTd(this)");
                e.Row.Cells[i].Attributes.Add("onmouseup", "this.mouseDown=false");
                e.Row.Cells[i].Attributes.Add("onmouseout", "this.mouseDown=false");
            }
        }
    }
}