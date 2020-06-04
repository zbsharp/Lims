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


public partial class Case_TreeEngineer2 : System.Web.UI.Page
{
    private int _i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        //convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "'
        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now;
            int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            int dayspan = (-1) * weeknow + 1;
            DateTime dt2 = dt.AddMonths(1);
            //本月第一天
            txFDate.Value = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");

            DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).AddHours(23);

            txTDate.Value = lastDay.ToShortDateString();
            TimeBind();
            BindDep();
         
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        string sqlstr;

      
        if (DropDownList1.SelectedValue == "")
        {
            sqlstr = "select *,(select bianhao from anjianinfo2 where rwbianhao=zhujianengineer.bianhao) as bianhao2,(select state from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as state,(select kf from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as kf,(select xiadariqi from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as xiadariqi,(select yaoqiuwanchengriqi from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as yaoqiuwanchengriqi from ZhuJianEngineer where  bianhao in (select rwbianhao from anjianinfo2 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "' and state='" + DropDownList3.SelectedValue + "') order by name desc";
        }
        else if (DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue == "")
        {
            sqlstr = "select *,(select bianhao from anjianinfo2 where rwbianhao=zhujianengineer.bianhao) as bianhao2,(select state from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as state,(select kf from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as kf,(select xiadariqi from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as xiadariqi,(select yaoqiuwanchengriqi from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as yaoqiuwanchengriqi from ZhuJianEngineer where name in (select username from userinfo where departmentname='" + DropDownList1.SelectedValue + "')  and bianhao in (select rwbianhao from anjianinfo2 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "' and state='" + DropDownList3.SelectedValue + "') order by name desc";

        }
        else if (DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue != "")
        {
            sqlstr = "select *,(select bianhao from anjianinfo2 where rwbianhao=zhujianengineer.bianhao) as bianhao2,(select state from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as state,(select kf from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as kf,(select xiadariqi from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as xiadariqi,(select yaoqiuwanchengriqi from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as yaoqiuwanchengriqi from ZhuJianEngineer where name ='" + DropDownList2.SelectedValue + "' and bianhao in (select rwbianhao from anjianinfo2 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "' and state='" + DropDownList3.SelectedValue + "') order by name desc";


        }
        else
        {
            sqlstr = "select *,(select bianhao from anjianinfo2 where rwbianhao=zhujianengineer.bianhao) as bianhao2,(select state from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as state,(select kf from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as kf,(select xiadariqi from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as xiadariqi,(select yaoqiuwanchengriqi from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as yaoqiuwanchengriqi from ZhuJianEngineer where bianhao in (select rwbianhao from anjianinfo2 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "' and state='" + DropDownList3.SelectedValue + "') order by name desc";

        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        AspNetPager1.Visible = false;
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }
    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select * from UserDepa where departmentid='12' or departmentid='22' or departmentid='13' or departmentid='15' or departmentid='16' or departmentid='17' or departmentid='9'";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


        DataSet ds = new DataSet();


        ad.Fill(ds);


        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataValueField = "name";
        DropDownList1.DataTextField = "name";
        DropDownList1.DataBind();

        DropDownList1.Items.Insert(0, new ListItem("", ""));//

        con3.Close();
    }


    protected void TimeBind()
    {

        string sqlstr;



        sqlstr = "select top 300 *,(select bianhao from anjianinfo2 where rwbianhao=zhujianengineer.bianhao) as bianhao2,(select state from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as state,(select kf from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as kf,(select xiadariqi from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as xiadariqi,(select yaoqiuwanchengriqi from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as yaoqiuwanchengriqi from ZhuJianEngineer where bianhao in (select rwbianhao from anjianinfo2 where convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "' and state='"+DropDownList3.SelectedValue+"')  order by name desc";



        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager1.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager1.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {



            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        TimeBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {




        DisableControls(GridView1);

        Response.ClearContent();

        Response.AddHeader("content-disposition", "attachment; filename=IncomeList" + DateTime.Now.ToShortDateString() + ".xls");

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

    public string SubStr(string sString, int nLeng)
    {
        if (sString.Length <= nLeng)
        {
            return sString;
        }
        string sNewStr = sString.Substring(0, nLeng);

        return sNewStr;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo where department='" + DropDownList1.SelectedValue + "' order by username asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataTextField = "username";
        DropDownList2.DataValueField = "username";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("", ""));//

        con.Close();
    }
}