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
using System.Text;
using System.IO;
using Common;
using DBBLL;

public partial class SysManage_Default2 : System.Web.UI.Page
{
    const string vsKey = "searchCriteria"; //ViewState key
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            searchOrders(string.Empty);
        }
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        AspNetPager2.CurrentPageIndex = 1;

        string s = " where departmentname = '" +TextBox1.Text.Trim()+"'";
        Session[vsKey] = s;
        searchOrders(s);
    }

    void searchOrders(string sWhere)
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo " + sWhere + "";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);



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

 
    protected void btn_all_Click(object sender, EventArgs e)
    {
        Session[vsKey] = null;
      
        AspNetPager2.CurrentPageIndex = 1;
        searchOrders(null);
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        searchOrders((string)Session[vsKey]);
    }
}