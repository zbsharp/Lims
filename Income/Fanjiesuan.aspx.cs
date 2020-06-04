using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Income_Fanjiesuan : System.Web.UI.Page
{
    private string serial = "";
    private decimal money = 0;
    private string time = (DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + 1).ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        serial = Request.QueryString["liushuihao"].ToString();
        money = Convert.ToDecimal(Request.QueryString["money"]);
        if (!IsPostBack)
        {
            BindDrop();
            Bind();
        }
    }

    private void Bind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = " select batch,fillname,filltime,money,affirmren,affirmtime from Claim where isaffirm='是' and cancellation='否' and  shuipiaoid='" + serial + "' order by batch";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
    }

    private void BindDrop()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = " select distinct batch from Claim where isaffirm = '是' and cancellation = '否' and affirmtime >= '" + time + "' and shuipiaoid = '" + serial + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.drop_bacth.DataSource = ds.Tables[0];
            this.drop_bacth.DataTextField = "batch";
            this.drop_bacth.DataValueField = "batch";
            this.drop_bacth.DataBind();
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

    protected void btnfanjiesuan_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();

            string sql = " update Claim set isaffirm='否' ,cancellation='是' where batch='" + drop_bacth.SelectedValue + "' and affirmtime >= '" + time + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            int i = cmd.ExecuteNonQuery();

            string sqlclaim = " select SUM(money) as jine from Claim where shuipiaoid=" + serial + " and isaffirm='是' and cancellation='否' and issubmit='是'";
            SqlCommand cmdclaim = new SqlCommand(sqlclaim, con);
            SqlDataReader drclaim = cmdclaim.ExecuteReader();
            decimal jine = 0;
            if (drclaim.Read())
            {
                if (drclaim["jine"] == DBNull.Value)
                {

                }
                else
                {
                    jine = Convert.ToDecimal(drclaim["jine"]);
                }
            }
            drclaim.Close();

            string state = "";
            if (jine == 0)
            {
                state = "";
            }
            else if (money > jine)
            {
                state = "半确认";
            }
            else if (money == jine)
            {
                state = "确认";
            }

            string sqlshuipiao = " update shuipiao set queren='" + state + "' where id=" + serial + "";
            SqlCommand cmdshuipiao = new SqlCommand(sqlshuipiao,con);
            cmdshuipiao.ExecuteNonQuery();

            if (i > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('反结算成功')</script>");
            }
        }
    }
}