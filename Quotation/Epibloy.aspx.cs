using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Quotation_Epibloy : System.Web.UI.Page
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
        string sql = string.Format("select * from Product2 where epiboly='外包' and baojiaid!=''and  baojiaid is not null and kehuid != '' and kehuid is not null and shoufei != '' and shoufei is not null and[baojiaUsername] in (select name2 from PersonConfig where name1 = '"+Session["UserName"].ToString()+"' ) or [baojiaUsername] = '"+Session["UserName"].ToString()+"'");
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int index = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
        string baojiaid = this.GridView1.Rows[index].Cells[0].Text;
        string kehuid = this.GridView1.Rows[index].Cells[1].Text;
        //Response.Redirect("~/Quotation/QuotationAdd1.aspx?baojiaid=" + baojiaid + "&&kehuid=" + kehuid + "");
        Response.Write("<script>window.open('QuotationAdd.aspx?baojiaid=" + baojiaid + "&&kehuid=" + kehuid + "','_blank')</script>");
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}