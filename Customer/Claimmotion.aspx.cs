using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Customer_Claimmotion : System.Web.UI.Page
{
    public string id = "";
    public string fukuanren = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            id = Request.QueryString["id"].ToString();
            fukuanren = Request.QueryString["fukuanren"].ToString();
            //BindTxt(id);
            //Bind();
        }
    }

    private void BindTxt(string id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from shuipiao where id=" + id + "";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                //tx_fukuan.Text = dr["fukuanren"].ToString();
                //tx_fukuandate.Text = dr["fukuanriqi"].ToString();
                //tx_curren.Text = dr["danwei"].ToString();
                //tx_momey.Text = dr["fukuanjine"].ToString();
                //Label1.Text = dr["liushuihao"].ToString();
                //tx_name.Text = Session["Username"].ToString();
            }
            dr.Close();
        }
    }

    private void Bind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            //con.Open();
            //string sql = "select * from Claim where liushuihao='" + Label1.Text + "'";
            //SqlDataAdapter da = new SqlDataAdapter(sql, con);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //GridView1.DataSource = ds.Tables[0];
            //GridView1.DataBind();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string remark = tx_remark.Text.Replace('\'', ' ');
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            //con.Open();
            //string sql = "insert Claim values('" + Label1.Text + "','" + remark + "','" + Session["Username"].ToString() + "','" + DateTime.Now + "','')";
            //SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.ExecuteNonQuery();
            //Bind();
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            //con.Open();
            //string state = "";
            //string sql_select = "select queren from shuipiao where liushuihao='" + Label1.Text + "'";
            //SqlCommand cmd_select = new SqlCommand(sql_select, con);
            //SqlDataReader dr_select = cmd_select.ExecuteReader();
            //if (dr_select.Read())
            //{
            //    state = dr_select["queren"].ToString();
            //}
            //dr_select.Close();

            //if (string.IsNullOrEmpty(state) || state.ToLower() == "null" || state == "&nbsp;")
            //{
            //    string sql = "delete Claim where id=" + id + "";
            //    SqlCommand cmd = new SqlCommand(sql, con);
            //    cmd.ExecuteNonQuery();
            //    Bind();
            //}
            //else
            //{
            //    Literal1.Text = "<script>alert('该数据财务已对账、不能删除')</script>";
            //}
        }
    }
}