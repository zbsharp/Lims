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

public partial class ShiXiao_ZanTing1 : System.Web.UI.Page
{
    protected string shwhere = "1=1";
    private int _i = 0;
    const string vsKey = "zanting";
    int tichu = 2;
    int kehu = 3;
    protected string str = "";
    Hashtable a = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {

        str = "select *,(select yaoqiuwanchengriqi from anjianinfo2 where rwbianhao=taskno) as yq,(select kf from anjianinfo2 where rwbianhao=taskno) as kf,(select top 1 beizhu2 from zanting where rwbianhao=anjianxinxi2.taskno ) as beizhu2,(select xiadariqi from anjianinfo2 where rwbianhao=anjianxinxi2.taskno) as xiada,(select shixian from anjianinfo2 where rwbianhao=anjianxinxi2.taskno) as shixian,(select yaoqiushixian from anjianinfo2 where rwbianhao=anjianxinxi2.taskno) as shixian2,(select top 1 state from baogao2 where tjid=anjianxinxi2.taskno) as st1,(select top 1 customname from customer where kehuid =anjianxinxi2.kehuid) as kehuname from anjianxinxi2";

        shwhere = "taskno  in (select rwbianhao from zanting)";



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



        sql = str + " where " + shwhere + " order by substring(taskno,4,5) desc ";

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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;

        string sqlstr;

        sqlstr = str + " where " + ChooseNo + " like  '%" + ChooseValue + "%' and " + shwhere + " order by substring(taskno,4,5) desc  ";
        SqlDataAdapter ad = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        con.Dispose();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();


    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");



            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;


            MyExcutSql ext = new MyExcutSql();
            e.Row.Cells[2].Text = ext.Eng(e.Row.Cells[0].Text);
            e.Row.Cells[3].Text = ext.EngBumen(e.Row.Cells[0].Text);


           


           


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
        Bind();
    }
    //beizhu4 是首提日期



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();

        if (e.CommandName == "xiada")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();




            string sqlstate = "delete from zanting where rwbianhao='" + sid + "'";
            SqlCommand cmdstate = new SqlCommand(sqlstate, con);
            cmdstate.ExecuteNonQuery();




            string sqlstate3 = "delete from taskone where taskno='" + sid + "' and (st='暂停' or st='中止')";
            SqlCommand cmdstate3 = new SqlCommand(sqlstate3, con);
            cmdstate3.ExecuteNonQuery();


            string sqlstate4 = "update anjianinfo2 set state='进行中',wancheng='" + DateTime.Now + "' where rwbianhao='" + sid + "'";
            SqlCommand cmdstate4 = new SqlCommand(sqlstate4, con);
            cmdstate4.ExecuteNonQuery();


            string sqlstate2 = "insert into  zanting2 values ('" + sid + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','','','恢复','')";
            SqlCommand cmdstate2 = new SqlCommand(sqlstate2, con);
            cmdstate2.ExecuteNonQuery();
            con.Close();


            Bind();
        }


    }
}