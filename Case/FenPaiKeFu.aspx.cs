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

public partial class Case_FenPaiKeFu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        limit("分派客服");
        Literal1.Text = "";
        if (!IsPostBack)
        {
            BindDep();
            BindDWsalesman();
            Bind();
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

    protected void Button2_Click(object sender, EventArgs e)
    {

        bool bu = false;//定义一个变量用于标识是否有选中项
        foreach (GridViewRow item in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)item.Cells[0].FindControl("CheckBox1");
            if (hzf.Checked)
            {
                bu = true;
                int index = item.RowIndex;//复选框所选中行的索引
                string marketid = this.GridView1.Rows[index].Cells[1].Text;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
                {
                    con.Open();
                    string sql = string.Format(@"insert into CustomerServer( [UserName],[marketid],fillname,filltime)
                            values('{0}', '{1}', '{2}','{3}')", this.DropDownList2.SelectedValue, marketid, Session["Username"].ToString(), DateTime.Now.ToString());
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    //修改customer表中的销售助理人员
                    //string sql_customer = string.Format("update Customer set [customerserver]='{0}' where Fillname='{1}'", this.DropDownList2.SelectedValue, marketid);
                    //SqlCommand cmd_customer = new SqlCommand(sql_customer, con);
                    //cmd_customer.ExecuteNonQuery();
                }
            }
        }
        if (bu)
        {
            BindDep();
            Bind();
            this.Literal1.Text = "<script>alert('分配成功');</script>";
        }
    }

    protected void BindDep()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_combox = string.Format("select UserName from userinfo where dutyname='销售助理' or dutyname='客服人员'");
            SqlDataAdapter da = new SqlDataAdapter(sql_combox, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.DropDownList2.DataSource = ds.Tables[0];
            this.DropDownList2.DataValueField = "UserName";
            this.DropDownList2.DataTextField = "UserName";
            this.DropDownList2.DataBind();
            // string sql_list = string.Format("select UserName,dutyname,department from  userinfo where  UserName  not in (select distinct marketid from  [dbo].[CustomerServer]) and  (dutyname='业务员' or dutyname='客户经理' or department='董事会' or department='总经办')");
            string sql_list = "select UserName,dutyname,department from  userinfo where  (dutyname='业务员' or dutyname='客户经理' or department='董事会' or department='总经办') and departmentid='" + this.dw_department.SelectedValue + "'";
            SqlDataAdapter da_list = new SqlDataAdapter(sql_list, con);
            DataSet ds_list = new DataSet();
            da_list.Fill(ds_list);
            this.GridView1.DataSource = ds_list.Tables[0];
            this.GridView1.DataBind();
        }
    }
    /// <summary>
    /// 全选
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    /// <summary>
    /// 加载已经指定销售助理的业务员
    /// </summary>
    protected void Bind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string sql = string.Format("select * from CustomerServer");
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView2.DataSource = ds.Tables[0];
            this.GridView2.DataBind();
        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(this.GridView2.Rows[e.RowIndex].Cells[0].Text);
        string sql = string.Format("delete CustomerServer where [id]='{0}'", id);
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        this.Literal1.Text = string.Empty;
        BindDep();
        Bind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }


    private void BindDWsalesman()
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
            dw_department.SelectedIndex = 0;
            dw_department.DataBind();
        }
    }

    protected void dw_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDep();
    }
}