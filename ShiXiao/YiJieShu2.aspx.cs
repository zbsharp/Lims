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

public partial class ShiXiao_YiJieShu2 : System.Web.UI.Page
{
    protected string shwhere = "1=1";
    private int _i = 0;
    const string vsKey = "jieshu";
    int tichu = 2;
    int kehu = 3;
    Hashtable a = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {


        shwhere = "(rwbianhao in (select top 1 tjid from baogao2 where pizhunby !='') or ( state='完成' or state='关闭'))";



        if (!IsPostBack)
        {

            Bind();


        }
    }
    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";




        sql = "select *, state as state1, xiadariqi  as xiada, shixian  as shixian, yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where " + shwhere + " order by substring(rwbianhao,4,5) desc";

        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        con.Dispose();
        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager2.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        AspNetPager2.Visible = false;

        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text.Trim();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";




        sql = "select *, state as state1, xiadariqi  as xiada, shixian  as shixian, yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where " + shwhere + " and " + ChooseNo + " like '%" + TextBox1.Text.Trim() + "%' order by substring(rwbianhao,4,5) desc";

        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        con.Dispose();

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


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
        Bind();
    }
    //beizhu4 是首提日期
}