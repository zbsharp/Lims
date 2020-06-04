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


public partial class Income_InvoiceSee : System.Web.UI.Page
{
    protected string invoiceid = "";
    protected string kehuid = "";
    protected string baojiaid = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        invoiceid = Request.QueryString["invoiceid"].ToString();



     


            if (!IsPostBack)
            {
                Bind();


                Bind2();



            }




        
    }

    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CeShiFeiKf where shoufeibianhao ='" + invoiceid + "' order by filltime desc";


        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


        string sql21 = "select * from CustomerLinkMan where  customerid='" + ds.Tables[0].Rows[0]["kehuid"].ToString() + "' ";
        SqlDataAdapter ad21 = new SqlDataAdapter(sql21, con);
        DataSet ds21 = new DataSet();
        ad21.Fill(ds21);
        DropDownList2.DataSource = ds21.Tables[0];
        DropDownList2.DataTextField = "name";
        DropDownList2.DataValueField = "name";
        DropDownList2.DataBind();


        con.Close();
        con.Dispose();
    }

    public void Bind2()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from invoice where inid='" + invoiceid + "'";


        SqlCommand cmd = new SqlCommand(sql,con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            TextBox1.Text = dr["zongjia"].ToString();
            TextBox2.Text = dr["feiyong"].ToString();
        }

        con.Close();
        con.Dispose();
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


       



    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            for (int j = 0; j < GridView1.Columns.Count; j++)
            {
                if (GridView1.Rows[i].Cells[j].Text.ToString() == "否" || GridView1.Rows[i].Cells[j].Text.ToString() == "未提交")
                {
                    GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.Red;
                }
            }

        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {


    }


    protected void Button1_Click(object sender, EventArgs e)
    {



      
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //string shoufeiid = "";
        SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con5.Open();
        string sql1 = "update Invoice set zongjia='"+Convert.ToDecimal(TextBox1.Text.Trim())+"',feiyong='"+Convert.ToDecimal(TextBox2.Text.Trim())+"',name='"+DropDownList2.SelectedValue +"' where inid='"+invoiceid+"' ";
        SqlCommand cmd = new SqlCommand(sql1,con5);
        cmd.ExecuteNonQuery();
        con5.Close();
      
        //string sql2 = "insert into Invoice values ('" + kehuid + "','" + baojiaid + "','" + shoufeiid + "','" + Convert.ToDecimal(Label1.Text.Trim()) + "','" + Convert.ToDecimal(DropDownList1.SelectedValue) + "','" + Convert.ToDecimal(Label2.Text.Trim()) + "','" + DropDownList2.SelectedValue + "','','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";
     

        //SqlCommand cmd3 = new SqlCommand(sql2, con5);
        //SqlCommand cmd4 = new SqlCommand(sql3, con5);
        //cmd3.ExecuteNonQuery();
        //cmd4.ExecuteNonQuery();
        //con5.Close();
        //con5.Dispose();
        Response.Write("<script>alert('保存成功');</script>");

    }
  







   

  
}