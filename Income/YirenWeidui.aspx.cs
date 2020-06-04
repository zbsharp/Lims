using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Income_YirenWeidui : System.Web.UI.Page
{
    public int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bool bu = limit1("财务对账");
            if (bu)
            {
                Bind();
            }
            else
            {
                Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
            }
        }
    }

    private void Bind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = " select * from shuipiao where id in (select shuipiaoid from Claim where issubmit='是' and cancellation='否')";
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
            //SelectClaimCount(GridView1);
        }
    }


    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        AspNetPager2.Visible = false;
        string value = TextBox1.Text.Replace('\'', ' ').Trim();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string shwere = "and 1=1";
            if (string.IsNullOrEmpty(dropaffirm.SelectedValue))
            {

            }
            else if (dropaffirm.SelectedValue == "已确认")
            {
                shwere = "and (queren='确认' or queren='半确认')";
            }
            else
            {
                shwere = "and (queren !='确认' or queren !='半确认')";
            }

            string sql = " select * from shuipiao where id in (select shuipiaoid from Claim where issubmit='是'  and cancellation='否') and (fukuanren like '%" + TextBox1.Text.Trim() + "%' or liushuihao like '%" + TextBox1.Text.Trim() + "%') " + shwere + "";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                int i = Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex].Value);
                string sql = "select distinct submitren from Claim where shuipiaoid=" + i + " and isaffirm='否' and cancellation='否'";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                e.Row.Cells[6].Text = ds.Tables[0].Rows.Count.ToString();
            }
        }
    }

    protected bool limit1(string pagename1)
    {
        bool A = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
            A = true;
        }
        else
        {
            con.Close();
            A = false;
        }
        return A;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        string id = this.GridView1.DataKeys[index].Value.ToString();
        string comname = e.CommandName;
        if (comname == "affirm")
        {
            //查看认领金额是否等于到款金额、如果是则状态为“确认”，如果小于则状态为“半确认”
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string state = "";
                string sql_claim = "select SUM(money) as money  from Claim where shuipiaoid='" + id + "' and  cancellation='否' and  issubmit='是'";
                SqlCommand cmd_claim = new SqlCommand(sql_claim, con);
                SqlDataReader dr_claim = cmd_claim.ExecuteReader();
                decimal claimmoney = 0;
                if (dr_claim.Read())
                {
                    claimmoney = Convert.ToInt32(dr_claim["money"]);
                }
                dr_claim.Close();
                decimal shuipiaomoney = Convert.ToDecimal(this.GridView1.Rows[index].Cells[3].Text);
                if (claimmoney >= shuipiaomoney)
                {
                    state = "确认";
                }
                else
                {
                    state = "半确认";
                }
                string sqlupdate = "update Claim set isaffirm='是',affirmren='" + Session["Username"].ToString() + "',affirmtime='" + DateTime.Now + "' where shuipiaoid='" + id + "' and cancellation='否' and issubmit='是' and isaffirm='否'";
                SqlCommand cmdupdate = new SqlCommand(sqlupdate, con);
                cmdupdate.ExecuteNonQuery();

                string sql_shuipiao = "update shuipiao set queren='" + state + "' ,querenren='" + Session["Username"].ToString() + "',querenriqi='" + DateTime.Now + "' where id='" + id + "'";
                SqlCommand cmd_shuipiao = new SqlCommand(sql_shuipiao, con);
                cmd_shuipiao.ExecuteNonQuery();
                Bind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('已确认');</script>");
            }
        }
        if (comname == "rollback")
        {
            string affirm = this.GridView1.Rows[index].Cells[5].Text.ToString();
            
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string update_claim = "update Claim set issubmit='否' where shuipiaoid='" + id + "' and issubmit='是' and cancellation='否' and isaffirm='否'";
                SqlCommand cmd = new SqlCommand(update_claim, con);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('已退回');</script>");
                    Bind();
                }
            }
        }
        if (comname == "cancellation")
        {
            string affirm = this.GridView1.Rows[index].Cells[5].Text.ToString();
            
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = "update Claim set cancellation='是' where shuipiaoid='" + id + "' and issubmit='是' and isaffirm='否' and cancellation='否'";
                SqlCommand command = new SqlCommand(sql, con);
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    Bind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('已作废')</script>");
                }
            }
        }
    }
}