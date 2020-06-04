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

public partial class Case_updatefeiyong : System.Web.UI.Page
{
    protected string id="";
    protected void Page_Load(object sender, EventArgs e)
    {

        id = Request.QueryString["id"].ToString();

        if (!IsPostBack)
        {



            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sql = "  select * from CeShiFeiKf where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Label1.Text = dr["feiyong"].ToString();

            }
            con.Close();

          



        }




        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "  update CeShiFeiKf set feiyong='"+Convert.ToDecimal(TextBox1.Text.Trim())+"' where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
     
   



        string shoufeiid = "";

        string shoufeibianhao = "否";
      

        string sql2 = "select bianhaoone,shoufeibianhao from CeShiFeikf where id='" + id + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            shoufeiid = dr2["bianhaoone"].ToString();
            shoufeibianhao = dr2["shoufeibianhao"].ToString();
        }
        dr2.Close();

        string sql8 = "select hesuanbiaozhi from invoice where inid='" + shoufeibianhao + "' and hesuanbiaozhi='是'";
        SqlCommand cmd8 = new SqlCommand(sql8, con);
        SqlDataReader dr8 = cmd8.ExecuteReader();
        if (dr8.Read())
        {
            dr8.Close();
        }
        else
        {
           
        }

        con.Close();
       
        if (shoufeibianhao != "否" && shoufeibianhao != "")
        {
            Response.Redirect("~/Income/InvoiceAdd2.aspx?invoiceid=" + shoufeibianhao);
        }



    }
}