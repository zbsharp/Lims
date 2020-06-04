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

public partial class ShiXiao_ZanTing : System.Web.UI.Page
{
    protected string shwhere = "1=1";
    private int _i = 0;
    const string vsKey = "zanting";
    int tichu = 2;
    int kehu = 3;
    protected string str = "";
    Hashtable a = new Hashtable();
    string gvUniqueID = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        str = "select *,(select top 1  convert(varchar,time1,23) +'-'+ beizhu4 from zanting where rwbianhao=anjianinfo2.rwbianhao ) as beizhu33,( xiadariqi ) as xiada,( shixian ) as shixian,( yaoqiushixian ) as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2";

        shwhere = " ( state='暂停')";
       


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



        sql = str + " where " + shwhere + " order by id desc ";
     
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

        ButtonBind();

    }

    protected void ButtonBind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;

        string sqlstr;
        if (DropDownList1.SelectedValue != "sq")
        {
            sqlstr = str + " where " + ChooseNo + " like  '%" + ChooseValue + "%' and " + shwhere + " order by id desc  ";
        }
        else
        {
            sqlstr = str + " where ( b1='申请恢复') and " + shwhere + " order by id desc  ";

        }
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

            e.Row.Cells[2].Text = SubStr(e.Row.Cells[2].Text, 6);
            e.Row.Cells[15].Text = SubStr(e.Row.Cells[15].Text, 18);
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
                  
            bool d = false;
            d = limit1("案件暂停");

            LinkButton LinkBtn_DetailInfo2 = (LinkButton)e.Row.FindControl("LinkButton5");
            if (e.Row.Cells[14].Text == Session["UserName"].ToString() || d == true)
            {
               
            }
            else
            {
                LinkBtn_DetailInfo2.Enabled = false;
                LinkBtn_DetailInfo2.ForeColor = Color.DarkGray;
            }

            //string sjt5 = "";
            //string zts5 = "";
            //searchwhere sx5 = new searchwhere();
            //string sjt1 = sx5.ShiXiao(e.Row.Cells[0].Text, out sjt5, out zts5);

            //int asjt = Convert.ToInt32(sjt5) - 1;
            //int bzts = Convert.ToInt32(zts5) - 1;
            //e.Row.Cells[2].Text = asjt.ToString();
            //e.Row.Cells[3].Text = bzts.ToString();
        }

        GridViewRow row = e.Row;
        string strSort = string.Empty;

        // Make sure we aren't in header/footer rows
        if (row.DataItem == null)
        {
            return;
        }

        //Find Child GridView control
        GridView gv = new GridView();
        gv = (GridView)row.FindControl("GridView2");

        //Check if any additional conditions (Paging, Sorting, Editing, etc) to be applied on child GridView
        if (gv.UniqueID == gvUniqueID)
        {

            ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["rwbianhao"].ToString() + "','one');</script>");
        }

        //Prepare the query for Child GridView by passing the Customer ID of the parent row
        gv.DataSource = ChildDataSource(((DataRowView)e.Row.DataItem)["rwbianhao"].ToString(), strSort);
        gv.DataBind();


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


    protected void limit(string pagename1)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {


            con.Close();
        }
        else
        {
            con.Close();
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
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


            string sqlstate4 = "update anjianinfo2 set state='进行中',wancheng='"+DateTime.Now+"' where rwbianhao='" + sid + "'";
            SqlCommand cmdstate4 = new SqlCommand(sqlstate4, con);
            cmdstate4.ExecuteNonQuery();


            string sqlstate2 = "insert into  zanting2 values ('" + sid + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','','','恢复','','')";
            SqlCommand cmdstate2 = new SqlCommand(sqlstate2, con);
            cmdstate2.ExecuteNonQuery();
            con.Close();

            Bind();
           // ButtonBind();
        }


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

    private SqlDataSource ChildDataSource(string shoufeiid, string strSort)
    {
        //  kuanleibie = kuanlb;
        string strQRY = "";
        SqlDataSource dsTemp = new SqlDataSource();
        //AccessDataSource dsTemp = new AccessDataSource();
        dsTemp.ConnectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        {
            strQRY = "SELECT * FROM [zanting2] WHERE [rwbianhao] = '" + shoufeiid + "' order by id desc";
        }


        dsTemp.SelectCommand = strQRY;
        return dsTemp;
    }


   
}