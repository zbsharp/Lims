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

public partial class ShiXiao_ZantingDaoChu : System.Web.UI.Page
{
    protected string shwhere = " 1=1 ";
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
            DateTime dt = DateTime.Now;
            int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            int dayspan = (-1) * weeknow + 1;
            DateTime dt2 = dt.AddMonths(1);
            //本月第一天
            txFDate.Value = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");

            BindDep();
            DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).AddHours(23);

            txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();
            Bind();
            shwhere = "( state='暂停') and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' ";
        }
    }
    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";



        sql = str + " where " + shwhere + " order by substring(rwbianhao,4,5) desc ";

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
        string sqlstr = "";
        if (DropDownList1.SelectedValue == "st1") 
        {
            sqlstr = str + " where  rwbianhao in (select tjid from baogao2 where state='" + TextBox1.Text.Trim() + "') and  " + shwhere + " order by substring(rwbianhao,4,5) desc  ";

        }
        else
        {
            string ChooseNo = (DropDownList1.SelectedValue);
            string ChooseValue = TextBox1.Text;
            shwhere = "( state='暂停') and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' ";
            
            if (DropDownList2.SelectedValue == "")
            {
                sqlstr = str + " where " + ChooseNo + " like  '%" + ChooseValue + "%' and " + shwhere + " order by substring(rwbianhao,4,5) desc  ";
            }
            else if (DropDownList2.SelectedValue != "")
            {

                if (DropDownList3.SelectedValue == "")
                {

                    sqlstr = str + " where " + ChooseNo + " like  '%" + ChooseValue + "%' and  rwbianhao in (select rwbianhao from zanting2 where beizhu4='" + DropDownList2.SelectedValue + "') and  " + shwhere + " order by substring(rwbianhao,4,5) desc  ";

                }
                else
                {
                    sqlstr = str + " where " + ChooseNo + " like  '%" + ChooseValue + "%' and rwbianhao in (select rwbianhao from zanting2 where beizhu4='" + DropDownList2.SelectedValue + "' and beizhu5='" + DropDownList3.SelectedValue + "') and " + shwhere + " order by substring(rwbianhao,4,5) desc  ";

                }
            }
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


            ButtonBind();
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

    protected void Button1_Click(object sender, EventArgs e)
    {



        DisableControls(GridView1);

        Response.ClearContent();

        Response.AddHeader("content-disposition", "attachment; filename=TaskCount" + DateTime.Now.ToShortDateString() + ".xls");

        Response.ContentType = "application/ms-excel";

        Response.Charset = "UTF-8";

        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");


        StringWriter sw = new StringWriter();

        HtmlTextWriter htw = new HtmlTextWriter(sw);

        GridView1.RenderControl(htw);

        Response.Write(sw.ToString());

        Response.End();
    }

    private void DisableControls(Control gv)
    {

        LinkButton lb = new LinkButton();

        Literal l = new Literal();

        string name = String.Empty;

        for (int i = 0; i < gv.Controls.Count; i++)
        {

            if (gv.Controls[i].GetType() == typeof(LinkButton))
            {

                l.Text = (gv.Controls[i] as LinkButton).Text;

                gv.Controls.Remove(gv.Controls[i]);

                gv.Controls.AddAt(i, l);

            }

            else if (gv.Controls[i].GetType() == typeof(DropDownList))
            {

                l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;

                gv.Controls.Remove(gv.Controls[i]);

                gv.Controls.AddAt(i, l);

            }



            if (gv.Controls[i].HasControls())
            {

                DisableControls(gv.Controls[i]);

            }

        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
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


    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();



        string sql2 = "select distinct name from ZangTingLeiBie ";
        SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con3);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);
        DropDownList2.DataSource = ds2.Tables[0];
        DropDownList2.DataValueField = "name";
        DropDownList2.DataTextField = "name";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("", ""));//



        string sql = "select wenyuan from ZangTingLeiBie  where name='" + DropDownList2.SelectedValue + "'  order by name ";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


        DataSet ds = new DataSet();


        ad.Fill(ds);


        DropDownList3.DataSource = ds.Tables[0];
        DropDownList3.DataValueField = "wenyuan";
        DropDownList3.DataTextField = "wenyuan";
        DropDownList3.DataBind();

        DropDownList3.Items.Insert(0, new ListItem("", ""));//






        con3.Close();
    }


    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ZangTingLeiBie where name='" + DropDownList2.SelectedValue + "'  order by wenyuan asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList3.DataSource = ds.Tables[0];
        DropDownList3.DataTextField = "wenyuan";
        DropDownList3.DataValueField = "wenyuan";
        DropDownList3.DataBind();

        DropDownList3.Items.Insert(0, new ListItem("", ""));//
        con.Close();
    }




}