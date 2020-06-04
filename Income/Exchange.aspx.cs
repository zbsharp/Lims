using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Income_Exchange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bool bu = limit1("财务对账");
            if (bu)
            {
                Bind_Datetime();
                Bind();
            }
            else
            {
                Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
            }
        }
    }

    private void Bind_Datetime()
    {
        string[] str = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        List<int> list_year = new List<int>();
        for (int i = 2018; i < 2030; i++)
        {
            list_year.Add(i);
        }
        drop_month.DataSource = str;
        drop_month.DataBind();
        drop_year.DataSource = list_year;
        drop_year.DataBind();
        string year = DateTime.Now.Year.ToString();
        for (int i = 0; i < drop_year.Items.Count; i++)
        {
            if (drop_year.Items[i].Value == year)
            {
                drop_year.Items[i].Selected = true;
            }
        }

        string month = DateTime.Now.Month.ToString();
        for (int i = 0; i < drop_month.Items.Count; i++)
        {
            if (drop_month.Items[i].Value == month)
            {
                drop_month.Items[i].Selected = true;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string remork = txt_remork.Text.Replace('\'', ' ');
        string exchange = txt_exchange.Text;
        if (string.IsNullOrEmpty(exchange))
        {
            exchange = "0.00";
        }
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_add = "insert [dbo].[Exchange] values('" + drop_currency.SelectedValue + "','" + exchange + "','" + Session["Username"].ToString() + "','" + DateTime.Now + "','" + drop_year.SelectedValue + "','" + drop_month.SelectedValue + "','" + remork + "')";
            SqlCommand cmd_add = new SqlCommand(sql_add, con);
            cmd_add.ExecuteNonQuery();
            Bind();
        }
    }

    private void Bind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from Exchange";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)GridView1.DataKeys[e.RowIndex].Value;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "delete Exchange where id='" + id + "'";
            SqlCommand cmd_delete = new SqlCommand(sql, con);
            cmd_delete.ExecuteNonQuery();
            Bind();
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }
}