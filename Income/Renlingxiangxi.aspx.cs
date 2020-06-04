using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Income_Renlingxiangxi : System.Web.UI.Page
{
    protected string shuipiaoid;
    protected void Page_Load(object sender, EventArgs e)
    {
        shuipiaoid = Request.QueryString["shuipiaoid"].ToString();
        if (!IsPostBack)
        {
            BindDATA();
        }
    }
    protected void BindDATA()
    {
        string sql = "select id, baojiaid,taskid,type,project,feiyong,beizhu3,(select sum(money) from claim where ceshifeikfid=ceshifeikf.id and isaffirm='是' " +
            "and cancellation='否') as yiquerenjine,(select sum(money) from claim where ceshifeikfid=ceshifeikf.id and isaffirm='否' and issubmit='是' " +
            "and cancellation='否') as bencitijiao,(select top(1) submitren from claim where ceshifeikfid=ceshifeikf.id and isaffirm='否' and issubmit='是' " +
            "and cancellation='否') as tijiaoren,(select top(1) submittime from claim where ceshifeikfid=ceshifeikf.id and isaffirm='否' and issubmit='是' " +
            "and cancellation='否') as tijiaoshijian from ceshifeikf where baojiaid in(select baojiaid from ceshifeikf where id in(select ceshifeikfid from claim " +
            "where shuipiaoid='" + shuipiaoid + "')) order by baojiaid asc";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        con.Close();
        this.GridView1.DataSource = ds.Tables[0];
        this.GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }
}