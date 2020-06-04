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

public partial class TongJi_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected DataTable table()
    {
        string jihuashu = "";
        string shijishu = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        con.Open();
        DataTable dt = new DataTable();
        dt.Columns.Add("检验员", Type.GetType("System.String"));
        dt.Columns.Add("月份", Type.GetType("System.String"));
        dt.Columns.Add("当月目标进度", Type.GetType("System.String"));
        dt.Columns.Add("当月计划进度", Type.GetType("System.String"));
        dt.Columns.Add("当月实际进度", Type.GetType("System.String"));

        dt.Columns.Add("当月计划数", Type.GetType("System.String"));

        dt.Columns.Add("当月实际数", Type.GetType("System.String"));

        dt.Columns.Add("计划数占全年数比例", Type.GetType("System.String"));
        dt.Columns.Add("实际数占全年数比例", Type.GetType("System.String"));

        int monthben = DateTime.Now.Month + 1;
        for (int yue = 0; yue < 11; yue++)
        {

            string sql = "select top 1 * from tjindu where name='" + Session["UserName"].ToString() + "' and yue='" + yue + "' order by id desc";

            string ff = "select sum(nianliang) as jihuashu from calendar where name='" + Session["UserName"].ToString() + "' and id='" + yue + "' and yuanyin2='计划工作'";

            string ff1 = "select sum(nianliang) as shijishu from calendar where name='" + Session["UserName"].ToString() + "' and id='" + yue + "' and yuanyin2='实际工作'";



            SqlCommand cmff = new SqlCommand(ff, con);
            SqlDataReader drff = cmff.ExecuteReader();
            if (drff.Read())
            {
                jihuashu = drff["jihuashu"].ToString();
            }
            drff.Close();

            SqlCommand cmff1 = new SqlCommand(ff1, con);
            SqlDataReader drff1 = cmff1.ExecuteReader();
            if (drff1.Read())
            {
                shijishu = drff1["shijishu"].ToString();
            }
            drff1.Close();



            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr;
                dr = dt.NewRow();
                dr["检验员"] = ds.Tables[0].Rows[0]["name"].ToString();
                dr["月份"] = ds.Tables[0].Rows[0]["yue"].ToString();


                dr["当月目标进度"] = ds.Tables[0].Rows[0]["mubiao"].ToString();
                dr["当月计划进度"] = ds.Tables[0].Rows[0]["dangqianjihua"].ToString();
                dr["当月实际进度"] = ds.Tables[0].Rows[0]["dangqianshiji"].ToString();
                dr["当月计划数"] = jihuashu;

                dr["当月实际数"] = shijishu;

                dr["计划数占全年数比例"] = ds.Tables[0].Rows[0]["jihuabili"].ToString();
                dr["实际数占全年数比例"] = ds.Tables[0].Rows[0]["shijibili"].ToString();
                //dr["dangqianjihua"] = "yujijun";
                dt.Rows.Add(dr);
            }
        }
        con.Close();
        return dt;
    }

}