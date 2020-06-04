using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Case_Test : System.Web.UI.Page
{
    string renwuid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        renwuid = Request.QueryString["taskid"].ToString();
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
            string sql = " select * from DaiFenTest where renwuid='" + renwuid + "'";
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
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow item in GridView1.Rows)
        {
            if (item.Cells[7].Text == "1900/1/1 0:00:00")
            {
                item.Cells[7].Text = string.Empty;
            }
            if (item.Cells[8].Text == "1900/1/1 0:00:00")
            {
                item.Cells[8].Text = string.Empty;
            }
            if (item.Cells[9].Text == "&nbsp;")
            {
                item.Cells[9].Text = string.Empty;
            }
        }
    }
}