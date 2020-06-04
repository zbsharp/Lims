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

public partial class ShiXiao_CountShiJi : System.Web.UI.Page
{

    //beizhu:工程是否确认，beizhu3：工程师确认日期，beizhu4:首次提出日期
    protected string shwhere = "1=1";
    protected string shwhere1 = "";
    protected string shwhere2 = "";
    protected string shwhere3 = "";
    private int _i = 0;
    const string vsKey = "jinxingzhong";
    int tichu = 2;//提出日期表示从下达日起允许提出的日期，比如11.10下达，则计算暂停和超期就从11.2号开始
    int kehu = 3;
    int chaoqitian = 2;
    protected string str = "";
    Hashtable a = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {


     
      
        if (!IsPostBack)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
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
               
                string sql2 = "insert into taskchaoqiday values('"+DateTime.Now.ToShortDateString()+"')";
                SqlCommand cmd2 = new SqlCommand(sql2,con);
                cmd2.ExecuteNonQuery();
                con.Close();

                searchwhere sx4 = new searchwhere();
                string sjt1 = sx4.ShiXiao("1");

               
                //Response.Write(sjt1);
              
            }


        }
    }
    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";

        sql = str + " where " + shwhere + "  order by substring(rwbianhao,4,5) desc";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        con.Dispose();
     
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
       
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;



            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select * from taskchaoqiday  where day='"+DateTime.Now.ToShortDateString()+"'";
            SqlCommand cmd = new SqlCommand(sql,con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
            }
            else
            {
                con.Close();
                //string sjt4 = "";
                //string zts4 = "";
                //searchwhere sx4 = new searchwhere();
                //string sjt1 = sx4.ShiXiao(e.Row.Cells[0].Text, out sjt4, out zts4);

                //int asjt = Convert.ToInt32(sjt4) - 1;
                //int bzts = Convert.ToInt32(zts4) - 1;
                //e.Row.Cells[5].Text = asjt.ToString();
                //e.Row.Cells[6].Text = bzts.ToString();
            }

            //sx4.CountShiJian(e.Row.Cells[0].Text);



        


        }

    }

    /// <summary>
    /// 获取数组中最大的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Array"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static int getmax(int[] arr)
    {
        int max = arr[0];
        for (int x = 0; x < arr.Length; x++)
        {
            if (arr[x] > max)
                max = arr[x];

        }
        return max;
    }






    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
       // Bind();
    }




    public string SubStr(string sString, int nLeng)
    {
        if (sString.Length <= nLeng)
        {
            return sString;
        }
        string sNewStr = sString.Substring(0, nLeng);

        return sNewStr;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       







    }
}