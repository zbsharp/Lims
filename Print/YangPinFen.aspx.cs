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

public partial class Print_YangPinFen : System.Web.UI.Page
{

    protected string spid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        spid = Request.QueryString["sampleid"].ToString();
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select * from YaoPinManage where picihao='" + spid + "' order by id asc";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            con.Dispose();

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();


            DropDownList1.DataSource = ds.Tables[0];
            DropDownList1.DataTextField = "picihaobianhao";
            DropDownList1.DataValueField = "picihaobianhao";
            DropDownList1.DataBind();
            
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string a = "";
        foreach (GridViewRow gr in GridView1.Rows)
        {
            TextBox hzf = (TextBox)gr.Cells[9].FindControl("tbyy");
            a += hzf.Text.ToString() + "|";
        }
        Response.Redirect("YangPinPrint.aspx?zs=" + TextBox1.Text.Trim() + "&&ps=" + TextBox2.Text.Trim() + "&&sampleid=" + spid+"&&sampleidz="+a);
    }
}