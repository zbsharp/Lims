using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Income_EMCarrival : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select id,region,EMCnumber,EMCid,shijihour,shijisumprice,responser,customername,linkman,isreceive,[money] from EMCmake  order by id desc";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
    }

    protected void btnselect_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string sql = "select id,region,EMCnumber,EMCid,shijihour,shijisumprice,responser,customername,linkman,isreceive,[money] from EMCmake where region='" + DropDownList1.SelectedValue + "' and (customername like '%" + txtshwere.Text + "%' or responser like '%" + txtshwere.Text + "%' or EMCid like '%" + txtshwere.Text + "%' or EMCnumber like '%" + txtshwere.Text + "%')  order by id desc";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
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

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int id = Convert.ToInt32(this.GridView1.DataKeys[e.RowIndex].Value);
        string money = Server.HtmlEncode(((TextBox)GridView1.Rows[e.RowIndex].Cells[10].Controls[0]).Text.ToString());
        string daozhang = Server.HtmlEncode(((DropDownList)GridView1.Rows[e.RowIndex].Cells[9].FindControl("funtion")).SelectedValue.ToString());
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "update EMCmake set isreceive='" + daozhang + "',[money]='" + money + "' where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        this.GridView1.EditIndex = -1;
        Bind();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView1.EditIndex = e.NewEditIndex;
        Bind();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        this.GridView1.EditIndex = -1;
        Bind();
    }
}