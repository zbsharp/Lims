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

public partial class Body : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            // Bind();
        }
    }
    protected void Bind()
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();
        //string sql = "select * from notice  ORDER BY id asc";
        //SqlDataAdapter da = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //da.Fill(ds);

        //GridView1.DataSource = ds.Tables[0];
        //GridView1.DataBind();

        //con.Close();
        //con.Dispose();


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        DateTime time3 = DateTime.Now;
        string sql21 = "select hdate from tb_Holiday where convert(varchar,hdate,23)='" + time3.ToString("yyyy-MM-dd") + "' and name !='工作日'";
        SqlCommand cmd21 = new SqlCommand(sql21, con);
        SqlDataReader dr21 = cmd21.ExecuteReader();
        if (dr21.Read())
        {
            dr21.Close();
            con.Close();
        }

        else
        {

            dr21.Close();
            string sql = "select * from taskchaoqiday  where day='" + DateTime.Now.ToShortDateString() + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
            }
            else
            {

                dr.Close();

                string sql2 = "insert into taskchaoqiday values('" + DateTime.Now.ToShortDateString() + "')";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.ExecuteNonQuery();
                con.Close();

                searchwhere sx4 = new searchwhere();
                string sjt1 = sx4.ShiXiao("1");


                //Response.Write(sjt1);

            }
        }
        con.Close();

    }

}