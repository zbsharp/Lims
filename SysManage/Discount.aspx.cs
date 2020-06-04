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
public partial class SysManage_Discount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.Attributes.Add("style", "table-layout:fixed");

        if (!IsPostBack)
        {
            BindDiscount();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "insert into discountconfig values ('" + Convert.ToInt32(TextBox1.Text) + "','" + Convert.ToInt32(TextBox2.Text) + "','" + Convert.ToDecimal(TextBox3.Text) + "','" + Convert.ToDecimal(TextBox4.Text) + "','" + Convert.ToDecimal(TextBox5.Text) + "','" + Convert.ToDecimal(TextBox6.Text) + "','','" + DateTime.Now + "','" + Session["UserName"].ToString() + "')";
        SqlCommand com = new SqlCommand(sql, con);
        SqlDataReader dr = com.ExecuteReader();

        con.Close();
    }
    protected void BindDiscount()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from discountconfig";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
    }

    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindDiscount();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView1.EditIndex = e.NewEditIndex;
        BindDiscount();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        double cost = 0;
        double price = 0;
        double discount = 0;
        string checkmodel = "";

        string KeyId = GridView1.DataKeys[e.RowIndex].Value.ToString();


        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        int uuname3 = Convert.ToInt32(Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[0].Controls[0]).Text));

        int uuname4 = Convert.ToInt32(Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text));
        double uuname5 = Convert.ToDouble(Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text));

        double uuname6 = Convert.ToDouble(Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text));

        double uuname7 = Convert.ToDouble(Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text));


        double uuname8 = Convert.ToDouble(Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text));


        SqlCommand cm = new SqlCommand("update DiscountConfig set amount1='" + uuname3 + "',amount2='" + uuname4 + "',advicediscount='" + uuname5 + "' ,salesman='" + uuname6 + "',head='" + uuname7 + "',leadship='" + uuname8 + "' where id=" + KeyId, con1); //列的编号是从1,这里更新的是第二列和第三列所以cell[2],cell[3],而行的索引是从0开始的
        cm.ExecuteNonQuery();




        GridView1.EditIndex = -1;
        con1.Close();

        BindDiscount();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "delete from DiscountConfig where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();
        BindDiscount();
    }
}