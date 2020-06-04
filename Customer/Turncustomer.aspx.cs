using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Customer_Turncustomer : System.Web.UI.Page
{
    private string kehuid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        kehuid = Request.QueryString["kehuid"].ToString();
        if (!IsPostBack)
        {
            BindDWdepartment();
            if (!string.IsNullOrEmpty(kehuid))
            {
                Bind();
            }
        }
    }

    private void Bind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_customer = "select * from Customer where Kehuid='" + kehuid + "'";
            SqlDataAdapter da_customer = new SqlDataAdapter(sql_customer, con);
            DataSet ds = new DataSet();
            da_customer.Fill(ds);
            DataTable dt_customer = ds.Tables[0];
            lb_id.Text = dt_customer.Rows[0]["Kehuid"].ToString();
            lb_customername.Text = dt_customer.Rows[0]["CustomName"].ToString();
            lb_souce.Text = dt_customer.Rows[0]["CustomSouce"].ToString();
            lb_fillname.Text = dt_customer.Rows[0]["Fillname"].ToString();
            lb_filltime.Text = dt_customer.Rows[0]["Filltime"].ToString();
            lb_responser.Text = dt_customer.Rows[0]["Responser"].ToString();

            string isbaojia = "否";
            string sql_baojia = "select * from BaoJiaBiao where KeHuId='" + kehuid + "'";
            SqlDataAdapter da_baojia = new SqlDataAdapter(sql_baojia, con);
            DataSet ds_baojia = new DataSet();
            da_baojia.Fill(ds_baojia);
            if (ds_baojia.Tables[0].Rows.Count > 0)
            {
                isbaojia = "是";
            }
            else
            {
                isbaojia = "否";
            }
            lb_baojia.Text = isbaojia;

            string sql_CustomerTrace = " select * from CustomerTrace where kehuid='" + kehuid + "'";
            SqlDataAdapter da_CustomerTrace = new SqlDataAdapter(sql_CustomerTrace, con);
            DataSet ds_CustomerTrace = new DataSet();
            da_CustomerTrace.Fill(ds_CustomerTrace);
            GridView1.DataSource = ds_CustomerTrace.Tables[0];
            GridView1.DataBind();
        }
    }

    private void BindDWsalesman()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_salesman = "select UserName from UserInfo where departmentid='" + dw_department.SelectedValue + "'";
            SqlDataAdapter da_salesman = new SqlDataAdapter(sql_salesman, con);
            DataSet ds_salesman = new DataSet();
            da_salesman.Fill(ds_salesman);
            dw_salesman.DataTextField = "UserName";
            dw_salesman.DataValueField = "UserName";
            dw_salesman.DataSource = ds_salesman.Tables[0];
            dw_salesman.DataBind();
        }
    }

    private void BindDWdepartment()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_department = "select DepartmentId,Name from UserDepa where DepartmentId='1' or DepartmentId='2' or DepartmentId='3'or DepartmentId = '4'or DepartmentId = '5'or DepartmentId = '6'or DepartmentId = '7' or DepartmentId = '1019' or DepartmentId='16'";
            SqlDataAdapter da = new SqlDataAdapter(sql_department, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dw_department.DataTextField = "Name";
            dw_department.DataValueField = "DepartmentId";
            dw_department.DataSource = ds.Tables[0];
            dw_department.DataBind();

            BindDWsalesman();
        }
    }

    protected void dw_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDWsalesman();
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_customer = "update Customer set Responser='" + dw_salesman.SelectedValue + "' where Kehuid='" + lb_id.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_customer, con);
            cmd.ExecuteNonQuery();

            string sql_sales = "update Customer_Sales set responser='" + dw_salesman.SelectedValue + "',fillname='" + Session["Username"].ToString() + "',filltime='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where customerid='" + lb_id.Text + "'";
            SqlCommand cmd_sales = new SqlCommand(sql_sales, con);
            cmd_sales.ExecuteNonQuery();

            

            string cause = TextBox1.Text.Replace("'", " ");
            string sql_add = "insert into TurnCustomerLog values('" + kehuid + "','" + lb_customername.Text + "','" + dw_salesman.SelectedValue + "','" + cause + "','" + Session["Username"].ToString() + "','" + DateTime.Now + "','','')";
            SqlCommand cmd_add = new SqlCommand(sql_add, con);
            cmd_add.ExecuteNonQuery();


            Literal1.Text = "<script>alert('已分派')</script>";
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
}