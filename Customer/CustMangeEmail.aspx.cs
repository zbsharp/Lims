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
using AjaxControlToolkit;
using System.IO;
using System.Text;
using System.Drawing;
using Common;
public partial class Customer_CustMangeEmail : System.Web.UI.Page
{
    public static string IDStr = string.Empty;
    public static string UserName = string.Empty;
    public static string RadStrSerch = string.Empty;
    protected DataTable dt;
    protected string departmentid = "";
    protected string departmentname = "";

    private int _i = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
       

        string a = Session["role"].ToString();

        if (!IsPostBack)
        {
            Bindlingdao();
        }
    }
    protected void Bindlingdao()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        //string sql = "select  * from Customered  where (responser='" + Session["UserName"].ToString() + "') or (responser in (select name2 from PersonConfig where name1='" + Session["UserName"].ToString() + "')) order by kehuid desc";
        string sql = "select  * from Customered   where " + searchwhere.search(Session["UserName"].ToString()) + "  order by kehuid desc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager1.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager1.PageSize;

        GridView1.DataSource = pds;
        GridView1.DataBind();


        con.Close();
        con.Dispose();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strTable = "";

            SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con4.Open();
            string sql4 = "select * from CustomerLinkMan where customerid='" +e.Row.Cells[1].Text+ "'";

            SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con4);
            DataSet ds4 = new DataSet();
            ad4.Fill(ds4);
            con4.Close();
            DataTable dt = new DataTable();
            dt = ds4.Tables[0];

            string sql41 = "select responser,customname from Customered where kehuid='" + e.Row.Cells[1].Text + "'";

            SqlDataAdapter ad41 = new SqlDataAdapter(sql41, con4);
            DataSet ds41 = new DataSet();
            ad41.Fill(ds41);
            DataTable dt41 = new DataTable();

            dt41 = ds41.Tables[0];

            CheckBoxList chck11 = (CheckBoxList)e.Row.FindControl("CheckBoxList11");

            chck11.DataSource = dt41;

            chck11.DataTextField = dt41.Columns["responser"].ToString();
            chck11.DataValueField = dt41.Columns["responser"].ToString();
            chck11.DataBind();


            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    CheckBoxList chck1 = (CheckBoxList)e.Row.FindControl("CheckBoxList1");
                    strTable += "<table  align=\"center\"   style ='border-collapse:collapse;background-color: #ffffdd;border-width: 2px;border-style: solid;border-color: #284775;'  border=\"1\"><tr><td align=\"center\"><span style='font-weight :bold ;'>姓名</span></td><td align=\"center\"><span style='font-weight :bold ;'>部门</span></td><td align=\"center\"><span style='font-weight :bold ;'>电话</span></td><td align=\"center\"><span style='font-weight :bold ;'>职位</span></td><td align=\"center\"><span style='font-weight :bold ;'>手机</span></td><td align=\"center\"><span style='font-weight :bold ;'>邮箱</span></td></tr>";


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        strTable += "<tr><td align=\"center\">" + dt.Rows[i]["name"].ToString() + "</td><td align=\"center\">" + dt.Rows[i]["department"].ToString() + "</td><td align=\"center\">" + dt.Rows[i]["telephone"].ToString() + "</td><td align=\"center\">" + dt.Rows[i]["rode"].ToString() + "</td><td align=\"center\">" + dt.Rows[i]["mobile"].ToString() + "</td><td align=\"center\">" + dt.Rows[i]["email"].ToString() + "</td></tr>";


                    }
                    strTable += "</table>";

                    chck1.DataSource = dt;

                    chck1.DataTextField = dt.Columns["name"].ToString();
                    chck1.DataValueField = dt.Columns["name"].ToString();
                    chck1.DataBind();



                }
            }


            con4.Close();
            Label3.Text = strTable;

            CheckBoxList lab = e.Row.FindControl("CheckBoxList1") as CheckBoxList;




            string f = e.Row.Cells[1].Text.ToString();
            string g = xianshi(f);



            if (g != "")
            {

                lab.Attributes.Add("onmousemove", "Show('" + g + "', 'gg', 'gg')");
                lab.Attributes.Add("onmouseout", "Hide();");
                e.Row.Cells[3].Attributes.Add("onmousemove", "Show('" + g + "', 'gg', 'gg')");
                e.Row.Cells[3].Attributes.Add("onmouseout", "Hide();");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("id", _i.ToString());
                e.Row.Attributes.Add("onKeyDown", "SelectRow();");
                e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
                _i++;
            }
           

        }


    }

    public string xianshi(string dd)
    {
        string strTable = "";

        SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con4.Open();
        string sql4 = "select * from CustomerLinkMan where customerid='" + dd + "'";

        SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con4);
        DataSet ds4 = new DataSet();
        ad4.Fill(ds4);
        DataTable dt4 = new DataTable();




        dt4 = ds4.Tables[0];

        if (dt4 != null)
        {
            if (dt4.Rows.Count > 0)
            {



                strTable += "<table  align=\"center\"   style =border-collapse:collapse;background-color: #ffffdd;border-width: 2px;border-style: solid;border-color: #284775;  border=\"2\"><tr><td align=\"center\"><span style=font-weight :bold ;>姓名</span></td><td align=\"center\"><span style=font-weight :bold ;>部门</span></td><td align=\"center\"><span style=font-weight :bold ;>电话</span></td><td align=\"center\"><span style=font-weight :bold ;>职位</span></td><td align=\"center\"><span style=font-weight :bold ;>手机</span></td><td align=\"center\"><span style=font-weight :bold ;>邮箱</span></td></tr>";


                for (int i = 0; i < dt4.Rows.Count; i++)
                {

                    strTable += "<tr><td align=\"center\"><span style= color:green;>" + dt4.Rows[i]["name"].ToString() + "</span></td><td align=\"center\">" + dt4.Rows[i]["department"].ToString() + "</td><td align=\"center\">" + dt4.Rows[i]["telephone"].ToString() + "</td><td align=\"center\">" + dt4.Rows[i]["rode"].ToString() + "</td><td align=\"center\">" + dt4.Rows[i]["mobile"].ToString() + "</td><td align=\"center\"><span style= color:red;>" + dt4.Rows[i]["email"].ToString() + "</span></td></tr>";



                }
                strTable += "</table>";







            }
        }

        con4.Close();
        return strTable;
    }


    protected void Button4_Click(object sender, EventArgs e)
    {
        string tiaokuan = "";
        string zongtiaokuan = "";
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBoxList hzf = (CheckBoxList)gr.Cells[3].FindControl("CheckBoxList1");


            for (int i = 0; i < hzf.Items.Count; i++)
            {


                if (hzf.Items[i].Selected)
                {

                    tiaokuan += hzf.Items[i].Value.ToString() + "|";



                }



            }




        }

        Response.Redirect("Customemail.aspx?kehu=" + Server.UrlEncode(tiaokuan));

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        AspNetPager1.Visible = false;
        
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";

        if (SerchCondition.SelectedValue == "0")
        {
            sql = "select  * from Customered   where " + searchwhere.search(Session["UserName"].ToString()) + " and kehuid in (select kehuid from CustomerLinkMan where name like '%" + SerchText .Text.Trim()+ "%')  order by kehuid desc";
        }
        else if (SerchCondition.SelectedValue == "1")
        {
            sql = "select  * from Customered   where " + searchwhere.search(Session["UserName"].ToString()) + " and customname like '%"+SerchText.Text.Trim()+"%' order by kehuid desc";
 
        }
        else if (SerchCondition.SelectedValue == "2")
        {
            sql = "select  * from Customered   where " + searchwhere.search(Session["UserName"].ToString()) + "  order by kehuid desc";

        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


        con.Close();
        con.Dispose();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bindlingdao();
    }
    protected void Button3_Click(object sender, EventArgs e)
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
}