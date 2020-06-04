using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Income_Baojiadetail : System.Web.UI.Page
{
    private string baojiaid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            baojiaid = Request.QueryString["baojiaid"].ToString();
            Bind_baojia(baojiaid);
            Bind_xm(baojiaid);
            Bind_Hs(baojiaid);
        }
    }

    private void Bind_product()
    {
        StringBuilder sb = new StringBuilder();
        foreach (GridViewRow item in GV_xm.Rows)
        {
            sb.Append("or id='" + item.Cells[0].Text.ToString() + "' ");
        }
        if (sb.Length > 0)
        {
            string shwere = sb.ToString().Substring(2);
            string sql = "select * from Product2 where " + shwere + "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GV_jz.DataSource = ds.Tables[0];
                GV_jz.DataBind();
            }
        }
    }

    private void Bind_xm(string baojiaid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select *,(select name from BaoJiaChanPing where BaoJiaChanPing.id=BaoJiaCPXiangMu.cpid) as cpname from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GV_xm.DataSource = ds.Tables[0];
            GV_xm.DataBind();
            Bind_product();
        }
    }

    private void Bind_baojia(string baojiaid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select *,(select CustomName from Customer where Customer.Kehuid=BaoJiaBiao.KeHuId)as customer from BaoJiaBiao where BaoJiaId='" + baojiaid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GV_baojia.DataSource = ds.Tables[0];
            GV_baojia.DataBind();
        }
    }

    private void Bind_Hs(string baojiaid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = @"select BaoJiaCPXiangMu.xiaoid as number,BaoJiaCPXiangMu.ceshiname as name,BaoJiaCPXiangMu.biaozhun as biaozhun,CeShiFeiKf.feiyong as [money],CeShiFeiKf.beizhu3 as dempert,CeShiFeiKf.fillname as fillname,CeShiFeiKf.filltime as filltime from CeShiFeiKf left join BaoJiaCPXiangMu on CeShiFeiKf.xmid=BaoJiaCPXiangMu.id
                            where  CeShiFeiKf.baojiaid='" + baojiaid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GV_hs.DataSource = ds.Tables[0];
            GV_hs.DataBind();
        }
    }
}