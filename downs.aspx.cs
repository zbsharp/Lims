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


public partial class downs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
             SetBind();
        }

    }

    public void SetBind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        DataSet ds = new DataSet();

        string sql = "";

        sql = "select top 5 *  from product2  order by name";




        SqlDataAdapter da = new SqlDataAdapter(sql, con);

        da.Fill(ds);
        gvshow.DataSource = ds.Tables[0];
        gvshow.DataBind();

        con.Close();
    }
    protected void gvshow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onclick", "cc('" + e.Row.Cells[0].Text + "','" + e.Row.Cells[1].Text + "','" + e.Row.Cells[2].Text + "')");
        }
    }

}