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

public partial class ShiXiao_TaskBeiZhu : System.Web.UI.Page
{

    private int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind1();
        }
    }

    protected void Bind1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string dd = DateTime.Now.AddDays(-1).ToShortDateString() + " 00:00:00";
        string sql = "";
        if (Session["role"].ToString() == "5" || Session["role"].ToString() == "4")
        {
            sql = "select  *,anjianinfo2.bianhao as bianhao from anjianbeizhu left join anjianinfo2 on xiangmuid=anjianinfo2.rwbianhao where neirong !='' and time between '" + Convert.ToDateTime(dd) + "' and '" + DateTime.Now + "' and rwbianhao in (select bianhao from ZhuJianEngineer where name='"+Session["UserName"].ToString()+"') ORDER BY anjianbeizhu.id desc";

        }
        else
        {

             sql = "select  *,anjianinfo2.bianhao as bianhao from anjianbeizhu left join anjianinfo2 on xiangmuid=anjianinfo2.rwbianhao where neirong !='' and time between '" + Convert.ToDateTime(dd) + "' and '" + DateTime.Now + "' ORDER BY anjianbeizhu.id desc";
        }

       



        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        GridView1.DataSource = dr;
        GridView1.DataBind();
        con.Close();
        con.Dispose();
    }

    public string SubStr(string sString, int nLeng)
    {
        if (sString.Length <= nLeng)
        {
            return sString;
        }
        string sNewStr = sString.Substring(0, nLeng);

        return sNewStr;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 20);
        }
    }
}