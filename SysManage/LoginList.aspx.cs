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
using System.IO;
using System.Text;
using System.Drawing;
using Common;
using DBBLL;
using DBTable;
public partial class SysManage_LoginList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Basic.SessionIn())
        {
            Bind();
        }
        else
        {
            string Url = "login.aspx";
            MessageBox.Show("登录超时,请重新登录", Url);
        }
        
       
    }
    protected void Bind()
    {
    
       
        UserLoginList db = new UserLoginList();     
        string sql = "select top 200 * from LoginInfo order by loginid desc";      
        DataSet ds = db.Select(sql);
      
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
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
}