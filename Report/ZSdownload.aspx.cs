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

public partial class Report_ZSdownload : System.Web.UI.Page
{
    protected string baogaoid = "";
    protected string src = "";
    protected string pp = "";
    protected string sp = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();
        baogaoid = Request.QueryString["baogaoid"].ToString();

        if (!IsPostBack)
        {
            Bindfuwufujian();
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

    public void Bindfuwufujian()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql1 = "select * from baogaofujian2 where caseid='" + baogaoid + "'";
        SqlDataAdapter da = new SqlDataAdapter(sql1, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        this.GridView5.DataSource = ds;
        this.GridView5.DataBind();
        con.Close();
        con.Dispose();
    }

    protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        if (e.CommandName == "dowtxt")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                string filename = "";
                string url = "";
                con.Open();
                string sql = " select fileName,urltext from baogaofujian2 where caseid='" + baogaoid + "'";
                SqlCommand com = new SqlCommand(sql, con);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    filename = dr["fileName"].ToString();
                    url = dr["urltext"].ToString();
                }
                dr.Close();

                if (!string.IsNullOrEmpty(filename))
                {
                    string urlpath = url;
                    try
                    {
                        FileStream fs = new FileStream(urlpath, FileMode.Open);
                        byte[] by = new byte[(int)fs.Length];
                        fs.Read(by, 0, by.Length);
                        fs.Close();
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
                        Response.BinaryWrite(by);
                        Response.Flush();
                        Response.End();
                    }
                    catch (Exception)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('下载失败')</script>");
                    }
                }
            }
        }
    }

    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }
}