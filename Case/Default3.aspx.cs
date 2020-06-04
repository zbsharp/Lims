using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Case_Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lb2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        Bind1();
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Bind1();
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "msg", "pwespopup_winLoad();", true);
        this.lb.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        this.UpdatePanel1.Update();
    }


    protected void Bind1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string dd = DateTime.Now.ToShortDateString() + " 00:00:00";
        string sql = "select  * from anjianbeizhu where neirong !='' and time between '" + Convert.ToDateTime(dd) + "' and '" + DateTime.Now + "' ORDER BY id desc";


        //SqlDataAdapter ad112 = new SqlDataAdapter(sql, con);
        //DataSet ds112 = new DataSet();
        //ad112.Fill(ds112);
        //con.Close();
        //DataTable dt112 = ds112.Tables[0];
        //string zhujian = "";
        //for (int z = 0; z < dt112.Rows.Count; z++)
        //{
        //    zhujian = zhujian + dt112.Rows[z]["name"].ToString() + ",";
        //}
        //if (zhujian.Contains(","))
        //{
        //    zhujian = zhujian.Substring(0, zhujian.Length - 1);
        //}


        //return zhujian;



        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            pw.Message = pw.Message + dr["xiangmuid"].ToString() + ":" + dr["neirong"].ToString() + "<br/>";

            pw.Text = dr["neirong"].ToString();
        }

        con.Close();
        con.Dispose();
    }

}
