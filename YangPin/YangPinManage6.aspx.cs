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
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.IO;
using Common;
using DBBLL;
using DBTable;

public partial class YangPin_YangPinManage6 : System.Web.UI.Page
{
    private int _i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            txFDate.Value = DateTime.Now.AddMonths(-12).ToString("yyyy-MM-01");

            BindDep();
            DateTime lastDay = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddHours(23);

            txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();
            if (Session["role"].ToString() != "4")
            {
                Bind2();
            }
            else
            {
                Bind1();
            }

            TextBox1.Focus();


            //shixiao();




        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string begintime;
        string endtime;
        int ChooseNo = int.Parse(DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;
        begintime = txFDate.Value;
        endtime = Convert.ToDateTime(txTDate.Value).AddDays(1).ToShortDateString();

        TimeBind(begintime, endtime, ChooseNo, ChooseValue);

        AspNetPager2.Visible = false;
    }
    protected void TimeBind(string a, string b, int c, string d)
    {

        string sqlL = "beizhu2 as dd";

        string ds1 = a;
        string ds2 = b;
        int ChooseID = c;
        string ChooseValue = d;
        string wher = "";
        string sql4 = "beizhu2 as kd";

        if (DropDownList3.SelectedValue == "已完成")
        {
            wher = " state='借出' and anjianid in (select rwbianhao from anjianinfo2 where (state='完成' or state='关闭'))";
        }
        else if (DropDownList3.SelectedValue == "未完成")
        {
            wher = " state='借出' and anjianid in (select rwbianhao from anjianinfo2 where (state='进行中' or state='暂停' or state='中止'))";

        }
        else
        {
            wher = " state='借出' ";

        }
        string sqlstr;
        string sqlx;


        if (DropDownList4.SelectedValue == "")
        {

            switch (ChooseID)
            {
                case 0:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where     " + wher + " order by convert(datetime,fuwuid) desc";
                    break;
                case 1:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where     kehuname like '%" + ChooseValue + "%' and " + wher + " order by convert(datetime,fuwuid) desc";
                    break;
                case 2:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where     name like '%" + ChooseValue + "%' and " + wher + " order by convert(datetime,fuwuid) desc";
                    break;
                case 3:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where     sampleid ='" + ChooseValue + "' and " + wher + " order by convert(datetime,fuwuid) desc";
                    break;


                case 5:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where     anjianid like '%" + ChooseValue + "%' and " + wher + " order by convert(datetime,fuwuid) desc";
                    break;

                case 6:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where   receivetime between '" + ds1 + "' and '" + ds2 + "'  and anjianid in(select rwbianhao from  anjianinfo2 where shenqingbianhao like '%" + ChooseValue + "%') and " + wher + " order by convert(datetime,fuwuid) desc";
                    break;
                case 7:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where   receivetime between '" + ds1 + "' and '" + ds2 + "'  and model like '%" + ChooseValue + "%' and " + wher + " order by convert(datetime,fuwuid) desc";
                    break;

                default:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where     " + wher + " order by convert(datetime,fuwuid) desc";
                    break;
            }
        }
        else
        {


            if (DropDownList5.SelectedValue == "")
            {
                if (DropDownList3.SelectedValue == "已完成")
                {
                    wher = "beizhu2 in ((select username from userinfo where departmentname='" + DropDownList4.SelectedValue + "')) and anjianid in (select bianhao from zhujianengineer where bumen ='" + DropDownList4.SelectedValue + "') and anjianid in (select rwbianhao from anjianinfo2 where (state='完成' or state='关闭'))";
                }
                else if (DropDownList3.SelectedValue == "未完成")
                {
                    wher = "beizhu2 in ((select username from userinfo where departmentname='" + DropDownList4.SelectedValue + "')) and anjianid in (select bianhao from zhujianengineer where bumen ='" + DropDownList4.SelectedValue + "') and anjianid in (select rwbianhao from anjianinfo2 where (state='进行中' or state='暂停' or state='中止'))";

                }
                else
                {
                    wher = "beizhu2 in ((select username from userinfo where departmentname='" + DropDownList4.SelectedValue + "')) and anjianid in (select bianhao from zhujianengineer where bumen ='" + DropDownList4.SelectedValue + "')";

                }
            }
            else
            {
                if (DropDownList3.SelectedValue == "已完成")
                {
                    wher = " beizhu2= '" + DropDownList5.SelectedValue + "' and anjianid in (select rwbianhao from anjianinfo2 where (state='完成' or state='关闭'))";
                }
                else if (DropDownList3.SelectedValue == "未完成")
                {
                    wher = " beizhu2= '" + DropDownList5.SelectedValue + "' and anjianid in (select rwbianhao from anjianinfo2 where (state='进行中' or state='暂停' or state='中止'))";

                }
                else
                {
                    wher = " beizhu2= '" + DropDownList5.SelectedValue + "' ";

                }

            }
            switch (ChooseID)
            {
                case 0:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where     " + wher + " and state ='借出' order by convert(datetime,fuwuid) desc";
                    break;
                case 1:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where     kehuname like '%" + ChooseValue + "%' and " + wher + " and state ='借出' order by convert(datetime,fuwuid) desc";
                    break;
                case 2:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where     name like '%" + ChooseValue + "%' and " + wher + " and state ='借出' order by convert(datetime,fuwuid) desc";
                    break;
                case 3:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where     sampleid ='" + ChooseValue + "' and " + wher + " and state ='借出'  order by convert(datetime,fuwuid) desc";
                    break;

                case 5:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where     anjianid like '%" + ChooseValue + "%' and " + wher + " and state ='借出' order by convert(datetime,fuwuid) desc";
                    break;

                case 6:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where   receivetime between '" + ds1 + "' and '" + ds2 + "'  and anjianid in (select rwbianhao from  anjianinfo2 where shenqingbianhao like '%" + ChooseValue + "%') and " + wher + " and state ='借出' order by convert(datetime,fuwuid) desc";
                    break;


                case 7:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where   receivetime between '" + ds1 + "' and '" + ds2 + "'  and sampleid in(select sampleid from  YangPin2Detail where pub_field3 like '%" + ChooseValue + "%') and " + wher + " and state ='借出' order by convert(datetime,fuwuid) desc";
                    break;

                default:
                    sqlstr = "select  *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq," + sql4 + ",(select state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where     " + wher + " and state ='借出' order by convert(datetime,fuwuid) desc";
                    break;
            }

        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();



    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes.Add("id", _i.ToString());
            //e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");

            e.Row.Cells[5].Text = SubStr(e.Row.Cells[5].Text, 5);
            e.Row.Cells[7].Text = SubStr(e.Row.Cells[7].Text, 5);
            e.Row.Cells[6].Text = SubStr(e.Row.Cells[6].Text, 5);



            //MyExcutSql ext = new MyExcutSql();
            //e.Row.Cells[8].Text = ext.Eng(e.Row.Cells[1].Text);

            //searchwhere sr = new searchwhere();
            //DateTime da = Convert.ToDateTime(e.Row.Cells[18].Text);
            //DateTime db = DateTime.Now;

            //string dc = sr.tian1(da, db).ToString();
            //e.Row.Cells[19].Text = dc;
            //e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            //_i++;

        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "chakan")
        {
            Response.Redirect("YangPinSee.aspx?yangpinid=" + sid);
        }
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {

        if (Session["role"].ToString() != "4")
        {
            Bind2();
        }
        else
        {
            Bind1();
        }

    }

    protected void Bind1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        string sql2 = "update yangpin2 set bianhao=anjianinfo2.bianhao,fuwuid=anjianinfo2.beizhu3 from anjianinfo2 where yangpin2.anjianid=anjianinfo2.rwbianhao";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        cmd2.ExecuteNonQuery();
        DataSet ds = new DataSet();

        {

            string wher = " state='借出' and anjianid in (select rwbianhao from anjianinfo2 where (state='完成' or state='关闭')) and beizhu2= '" + DropDownList5.SelectedValue + "' ";
            string sqlL = "beizhu2 as dd";

            string sql4 = "beizhu2 as kd";
            string sql = "select  *," + sql4 + ",(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq,(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where  " + wher + "  order by convert(datetime,fuwuid) desc";
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);

            ad.Fill(ds);
        }

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

    protected void Bind2()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        string sql2 = "update yangpin2 set bianhao=anjianinfo2.bianhao,fuwuid=anjianinfo2.beizhu3 from anjianinfo2 where yangpin2.anjianid=anjianinfo2.rwbianhao";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        cmd2.ExecuteNonQuery();
        DataSet ds = new DataSet();

        {

            string wher = " state='借出'  and anjianid in (select rwbianhao from anjianinfo2 where (state='完成' or state='关闭'))";
            string sqlL = "beizhu2 as dd";
            string sql4 = "beizhu2 as kd";
            string sql = "select  *," + sql4 + ",(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=anjianid) as sq,(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where  " + wher + "  order by convert(datetime,fuwuid) desc";
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);

            ad.Fill(ds);
        }


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


    protected void Button1_Click(object sender, EventArgs e)
    {
        DisableControls(GridView1);

        Response.ClearContent();

        //Response.AddHeader("content-disposition", "attachment; filename=qiandanlist" + DateTime.Now.ToShortDateString() + ".xls");

        Response.ContentType = "application/ms-excel";

        Response.Charset = "GB2312";
        Response.AddHeader("Content-Disposition", "attachment;   filename=" + System.Web.HttpUtility.UrlEncode("样品管理入库列表" + DateTime.Now, System.Text.Encoding.UTF8) + ".xls");//这样的话，可以设置文件名为中文，且文件名不会乱码。其实就是将汉字转换成UTF8      


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
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/YangPin/YangPinAdd.aspx?baojiaid=&&kehuid=&&bianhao=");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/YangPin/YangPinPiLiang.aspx?baojiaid=&&kehuid=");
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

    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string depart = "";

        string sql2 = "select name from UserDepa where name=(select departmentname from userinfo where username='" + Session["UserName"].ToString() + "')";
        SqlCommand cmd2 = new SqlCommand(sql2, con3);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            depart = dr2["name"].ToString();
        }

        dr2.Close();


        string sql = "select * from UserDepa where departmentid='10' or departmentid='11'  or  departmentid='12' or departmentid='13' or departmentid='14' or departmentid='15'";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


        DataSet ds = new DataSet();


        ad.Fill(ds);


        DropDownList4.DataSource = ds.Tables[0];
        DropDownList4.DataValueField = "name";
        DropDownList4.DataTextField = "name";
        DropDownList4.DataBind();

        DropDownList4.Items.Insert(0, new ListItem("", ""));//

        if (Session["role"].ToString() != "4")
        {

        }
        else
        {
            DropDownList4.SelectedValue = depart;


            string sql3 = "select * from userinfo where department='" + DropDownList4.SelectedValue + "' order by username desc ";
            SqlDataAdapter ad3 = new SqlDataAdapter(sql3, con3);
            DataSet ds3 = new DataSet();
            ad3.Fill(ds3);
            DropDownList5.DataSource = ds3.Tables[0];
            DropDownList5.DataTextField = "username";
            DropDownList5.DataValueField = "username";
            DropDownList5.DataBind();
            DropDownList5.SelectedValue = Session["UserName"].ToString();

        }

        con3.Close();
    }

    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo where department='" + DropDownList4.SelectedValue + "' order by username desc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList5.DataSource = ds.Tables[0];
        DropDownList5.DataTextField = "username";
        DropDownList5.DataValueField = "username";
        DropDownList5.DataBind();
        DropDownList5.Items.Insert(0, new ListItem("", ""));//

        con.Close();
    }
    protected void shixiao()
    {
        SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con4.Open();
        string sql4 = "";

        sql4 = "select top 10 * from yangpin2  where fuwuid !='' order by anjianid";


        SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con4);
        DataSet ds4 = new DataSet();
        ad4.Fill(ds4);
        con4.Close();

        for (int ii = 0; ii < ds4.Tables[0].Rows.Count; ii++)
        {
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con2.Open();

            searchwhere sr = new searchwhere();
            DateTime da = Convert.ToDateTime(ds4.Tables[0].Rows[ii]["fuwuid"].ToString());
            DateTime db = DateTime.Now;
            string rw = ds4.Tables[0].Rows[ii]["anjianid"].ToString();
            string dc = sr.tian1(da, db).ToString();
            string sql5 = "update yangpin2 set type='' where anjianid='" + ds4.Tables[0].Rows[ii]["anjianid"].ToString() + "'";
            SqlCommand cmd5 = new SqlCommand(sql5, con2);
            cmd5.ExecuteNonQuery();
            con2.Close();
        }

    }
}