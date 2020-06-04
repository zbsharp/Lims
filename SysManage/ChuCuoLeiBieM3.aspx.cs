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
using DBBLL;
using DBTable;
using System.Drawing;

public partial class SysManage_ChuCuoLeiBieM3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            Bind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        AspNetPager2.Visible = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from  UserChuCuoWenJian where (name like '%" + TextBox6.Text.Trim() + "%' or wenyuan like '%" + TextBox6.Text.Trim() + "%') and fillname='杨郑继'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();





        con.Close();


    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from UserChuCuoWenJian where fillname='杨郑继' order by departmentid desc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);


        string sql2 = "select distinct name from UserChuCuoWenJian order by name";
        SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);
        DropDownList1.DataSource = ds2.Tables[0];
        DropDownList1.DataValueField = "name";
        DropDownList1.DataTextField = "name";
        DropDownList1.DataBind();


        con.Close();

        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager2.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "delete from UserChuCuoWenJian where departmentid='" + id + "' and fillname='" + Session["UserName"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();

        Bind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Bind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView1.EditIndex = e.NewEditIndex;
        Bind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string KeyId = GridView1.DataKeys[e.RowIndex].Value.ToString();

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string uuname2 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
        string uuname = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());
        string uuname4 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());

        string sql = "update UserChuCuoWenJian set wenyuan='" + uuname2 + "',name='" + uuname + "',fax='" + uuname4 + "' where departmentid='" + KeyId + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        GridView1.EditIndex = -1;
        Bind();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
}