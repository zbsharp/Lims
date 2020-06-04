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

public partial class Case_YiFenYiShou5 : System.Web.UI.Page
{
    protected string shwhere = "1=1";
    protected string shwhere1 = "";
    protected string shwhere11 = "";
    protected string shwhere2 = "";
    protected string shwhere3 = "";
    private int _i = 0;
    const string vsKey = "jinxingzhong";
    int tichu = 2;//提出日期表示从下达日起允许提出的日期，比如11.10下达，则计算暂停和超期就从11.2号开始
    int kehu = 3;
    int chaoqitian = 2;
    protected string str = "";
    Hashtable a = new Hashtable();
    protected bool xian = false;
    protected string pai = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        // pai = "order by convert(datetime,yaoqiuwanchengriqi) desc";

        shwhere3 = " ( kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text.Trim() + "%'))";

        shwhere2 = "  ( kf like '%" + TextBox1.Text.Trim() + "%')";

        shwhere1 = " (convert(datetime, yaoqiuwanchengriqi) < '" + DateTime.Now.AddDays(-1) + "')";
        shwhere11 = " (convert(datetime, yaoqiuwanchengriqi) between '" + DateTime.Now + "' and '" + DateTime.Now.AddDays(4) + "')";


        //xian = limit1("项目经理");

        //if (limit1("取消参与"))
        //{
        //    DropDownList3.Visible = true;
        //    Label1.Visible = true;
        //}
        //else
        //{
        //    DropDownList3.Visible = false;
        //    Label1.Visible = false;
        //}
        if (Session["role"].ToString() == "8" || Session["role"].ToString() == "1" || Session["role"].ToString() == "3" || xian)
        {

            shwhere = "1=1";





        }

        else
        {
            shwhere = " ( rwbianhao in (select bianhao from ZhuJianEngineer where name='" + Session["UserName"].ToString() + "'))";

        }





        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now;
            int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            int dayspan = (-1) * weeknow + 1;
            DateTime dt2 = dt.AddMonths(1);
            //本月第一天
            txFDate.Value = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");

            //本月最后一天
            DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).AddHours(23);

            txTDate.Value = lastDay.ToShortDateString();
            Bind(RadioButtonList1.SelectedValue);
            BindDep();

        }
    }

    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select * from chaoqi ";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


        DataSet ds = new DataSet();


        ad.Fill(ds);





        //DropDownList2.DataSource = ds.Tables[0];
        //DropDownList2.DataValueField = "name";
        //DropDownList2.DataTextField = "name";
        //DropDownList2.DataBind();


        DropDownList3.DataSource = ds.Tables[0];
        DropDownList3.DataValueField = "name";
        DropDownList3.DataTextField = "name";
        DropDownList3.DataBind();
        con3.Close();
        DropDownList3.Items.Insert(0, new ListItem("", ""));//

    }

    public void Bind(string dd)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";
        string chaos = " and songjiandate !='' and convert(datetime,xiadariqi) < convert(datetime,yaoqiuwanchengriqi)";



        string str = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state ='完成' or state='暂停' or state='进行中' or state='下达') and  rwbianhao  in (select bianhao from ZhuJianEngineer)) and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "' and " + shwhere + " " + chaos + " " + dd + "";


        sql = str;


        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        con.Dispose();
        //DataView dv = ds.Tables[0].DefaultView;
        //PagedDataSource pds = new PagedDataSource();
        //AspNetPager2.RecordCount = dv.Count;
        //pds.DataSource = dv;
        //pds.AllowPaging = true;
        //pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        //pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        AspNetPager2.Visible = false;

        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;
        string pai = RadioButtonList1.SelectedValue;
        string sqlstr = "";

        string chaos = " and songjiandate ='"+DropDownList3.SelectedValue+"' and convert(datetime,xiadariqi) < convert(datetime,yaoqiuwanchengriqi)";


        string shic = " and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "' ";

        if (TextBox1.Text.Trim() == "")
        {

            if (Session["role"].ToString() == "8" || Session["role"].ToString() == "1" || xian)
            {

                if (DropDownList1.SelectedValue == "0")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state ='下达' or state='进行中') and  rwbianhao  in (select bianhao from ZhuJianEngineer)  " + chaos + "   " + shic + " )   " + pai + "";

                }
                else if (DropDownList1.SelectedValue == "1")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='暂停') and  rwbianhao  in (select bianhao from ZhuJianEngineer) " + chaos + "   " + shic + " )  " + pai + "";

                }
                else if (DropDownList1.SelectedValue == "2")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='完成') and  rwbianhao  in (select bianhao from ZhuJianEngineer)  " + chaos + "   " + shic + ")  " + pai + "";

                }

            }
            else
            {
                if (DropDownList1.SelectedValue == "0")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state ='下达' or state='进行中') and  rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " " + chaos + "   " + shic + ")  " + pai + "";

                }
                else if (DropDownList1.SelectedValue == "1")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='暂停') and  rwbianhao  in (select bianhao from ZhuJianEngineer) and " + shwhere + " " + chaos + "   " + shic + ")  " + pai + "";

                }
                else if (DropDownList1.SelectedValue == "2")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='完成') and  rwbianhao  in (select bianhao from ZhuJianEngineer) and " + shwhere + " " + chaos + "   " + shic + ")  " + pai + "";

                }

            }
        }
        else
        {
            if (Session["role"].ToString() == "8" || Session["role"].ToString() == "1" || xian)
            {

                if (DropDownList2.SelectedValue == "或")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (  rwbianhao  in (select bianhao from ZhuJianEngineer))   and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%') " + chaos + "   " + shic + ") " + pai + "";
                }
                else
                {

                    if (DropDownList1.SelectedValue == "0")
                    {

                        sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (  rwbianhao  in (select bianhao from ZhuJianEngineer))    and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='下达' or state='进行中'  ) " + chaos + "   " + shic + " " + pai + "";
                    }
                    else if (DropDownList1.SelectedValue == "1")
                    {

                        sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (  rwbianhao  in (select bianhao from ZhuJianEngineer))   and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or kf like '%" + TextBox1.Text.Trim() + "%' or (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='暂停' ) " + chaos + "   " + shic + " " + pai + "";
                    }
                    else if (DropDownList1.SelectedValue == "2")
                    {

                        sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (  rwbianhao  in (select bianhao from ZhuJianEngineer))   and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='完成' )" + chaos + "   " + shic + "  " + pai + "";
                    }

                }

            }
            else
            {
                if (DropDownList2.SelectedValue == "或")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) " + chaos + "   " + shic + " " + pai + "";
                }
                else
                {
                    if (DropDownList1.SelectedValue == "0")
                    {

                        sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='下达' or state='进行中' ) " + chaos + "   " + shic + " " + pai + "";
                    }
                    else if (DropDownList1.SelectedValue == "1")
                    {

                        sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='暂停' ) " + chaos + "   " + shic + " " + pai + "";
                    }
                    else if (DropDownList1.SelectedValue == "2")
                    {

                        sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='完成' ) " + chaos + "   " + shic + " " + pai + "";
                    }

                }


            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter ad = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        GridView1.DataSource = ds.Tables[0];
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

            string pizhun = "";
            string pizhundate = "";

            e.Row.Cells[2].Text = SubStr(e.Row.Cells[2].Text, 5);
            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 5);
            e.Row.Cells[22].Text = SubStr(e.Row.Cells[22].Text, 11) + "...";

            if (e.Row.Cells[9].Text != "" && e.Row.Cells[9].Text != "&nbsp" && e.Row.Cells[9].Text.Trim().ToString().Substring(0, 4) == "1900")
            {
                e.Row.Cells[9].Text = "";
            }


            MyExcutSql ext = new MyExcutSql();
            e.Row.Cells[5].Text = ext.Eng(e.Row.Cells[0].Text);
            e.Row.Cells[4].Text = ext.EngBumen(e.Row.Cells[0].Text);


            //{
            //    DateTime realtime = DateTime.Now;//任务下达日期
            //    DateTime kaoheriqi = DateTime.Now;//考核日期
            //    int kaoheshijian = 10;//考核日期也可以根据时限计算
            //    string kaoheshijians = "";
            //    string f = e.Row.Cells[0].Text.ToString();
            //    //f = "13-01406";




            //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //    con.Open();
            //    string yaoqiuwanchengriqi = "";
            //    string xiadariqi = "";

            //    string sqlk = "select * from anjianinfo2 where rwbianhao='" + f + "'";
            //    SqlCommand cmdk = new SqlCommand(sqlk, con);
            //    SqlDataReader drk = cmdk.ExecuteReader();
            //    if (drk.Read())
            //    {
            //        kaoheshijians = drk["shixian"].ToString();
            //        yaoqiuwanchengriqi = drk["yaoqiuwanchengriqi"].ToString();
            //        xiadariqi = drk["xiadariqi"].ToString();
            //    }
            //    drk.Close();

            //    if (xiadariqi == "")
            //    {
            //        xiadariqi = "2000-01-01";
            //    }
            //    if (yaoqiuwanchengriqi == "")
            //    {
            //        yaoqiuwanchengriqi = "2010-01-01";
            //    }



            //    if (kaoheshijians == "")
            //    {

            //        searchwhere sr = new searchwhere();
            //        kaoheshijian = sr.tian1(Convert.ToDateTime(xiadariqi), Convert.ToDateTime(yaoqiuwanchengriqi));
            //    }
            //    else
            //    {
            //        kaoheshijian = Convert.ToInt32(kaoheshijians);
            //    }




            //    #region 判断因为资料原因是否暂停

            //    //获取下达日期
            //    string sql3 = "select dotimereal from taskstate where taskid='" + f + "' and type1='下达任务' order by id desc";
            //    SqlCommand cmd3 = new SqlCommand(sql3, con);
            //    SqlDataReader dr3 = cmd3.ExecuteReader();
            //    if (dr3.Read())
            //    {


            //        realtime = Convert.ToDateTime(dr3["dotimereal"]);
            //    }
            //    else
            //    {
            //        dr3.Close();

            //        string sql31 = "select dotimereal from taskstate where taskid='" + f + "' and type1='受理任务' order by id desc";
            //        SqlCommand cmd31 = new SqlCommand(sql31, con);
            //        SqlDataReader dr31 = cmd31.ExecuteReader();
            //        if (dr31.Read())
            //        {


            //            realtime = Convert.ToDateTime(dr31["dotimereal"]);
            //        }
            //        dr31.Close();

            //    }

            //    dr3.Close();

            //    //从配置表中获取一段时间内的配置参数，以任务号优先，否则取时间段

            //    string sqlpeizhi = "select * from WaiQing where waichubianhao='" + f + "'";
            //    SqlCommand cmdpeizhi = new SqlCommand(sqlpeizhi, con);
            //    SqlDataReader drpeizhi = cmdpeizhi.ExecuteReader();
            //    if (drpeizhi.Read())
            //    {
            //        if (drpeizhi["chifan"] != DBNull.Value)
            //        {
            //            tichu = Convert.ToInt32(drpeizhi["chifan"]);
            //            kehu = Convert.ToInt32(drpeizhi["zhusu"]);
            //            chaoqitian = Convert.ToInt32(drpeizhi["beizhu"]);
            //        }
            //    }

            //    else
            //    {
            //        drpeizhi.Close();
            //        string sqlpeizhi1 = "select top 1 * from WaiQing where '" + realtime + "' between chutime and huitime order by id desc";
            //        SqlCommand cmdpeizhi1 = new SqlCommand(sqlpeizhi1, con);
            //        SqlDataReader drpeizhi1 = cmdpeizhi1.ExecuteReader();
            //        if (drpeizhi1.Read())
            //        {
            //            if (drpeizhi1["chifan"] != DBNull.Value)
            //            {

            //                tichu = Convert.ToInt32(drpeizhi1["chifan"]);
            //                kehu = Convert.ToInt32(drpeizhi1["zhusu"]);
            //                chaoqitian = Convert.ToInt32(drpeizhi1["beizhu"]);

            //            }
            //        }
            //        else
            //        {

            //        }
            //        drpeizhi1.Close();

            //    }
            //    drpeizhi.Close();

            //    bool jian = false;
            //    string sqljian = "select * from zanting where rwbianhao='" + f + "'";
            //    SqlCommand cmdjian = new SqlCommand(sqljian, con);
            //    SqlDataReader drjian = cmdjian.ExecuteReader();
            //    if (drjian.Read())
            //    {
            //        jian = true;
            //    }
            //    drjian.Close();


            //    //判断是否因为资料暂停还是继续,如果暂停，就放到暂停页面去。在这里不显示.这里要求一定要有下达日期
            //    int ddd = kehu + tichu;
            //    string sqlchaoqi = "select * from anjianxinxi where (xiangmuid='" + f + "' and (beizhu !='是' or beizhu is null) and beizhu4 !='' and datediff(day,'" + Convert.ToDateTime(realtime).AddDays(kehu) + "','" + DateTime.Now + "')>'" + ddd + "')";
            //    SqlCommand cmdchaoqi = new SqlCommand(sqlchaoqi, con);
            //    SqlDataReader drchaoqi = cmdchaoqi.ExecuteReader();


            //    //if (drchaoqi.Read()||jian==true)
            //    //{
            //    //    e.Row.Cells[6].Text = "暂停";
            //    //    e.Row.Visible = false;
            //    //}
            //    //else
            //    //{
            //    //    e.Row.Cells[6].Text = "进行中";

            //    //}

            //    drchaoqi.Close();


            //    #endregion



            //    #region 计算是否超时


            //    int zuidatian = 0;
            //    string sql5 = "select beizhu4,beizhu3 from anjianxinxi where xiangmuid='" + f + "' and (beizhu ='是') and beizhu4 !='' and convert(datetime,beizhu4) between '" + realtime + "' and '" + realtime.AddDays(tichu) + "' and convert(datetime,beizhu3) between '" + realtime + "' and '" + realtime.AddDays(tichu + kehu) + "' order by convert(datetime,beizhu4) asc";
            //    SqlDataAdapter ad5 = new SqlDataAdapter(sql5, con);
            //    DataSet ds5 = new DataSet();
            //    ad5.Fill(ds5);

            //    DataTable dt5 = ds5.Tables[0];

            //    int[] arrStr = new Int32[dt5.Rows.Count + 1];

            //    for (int i = 0; i < dt5.Rows.Count; i++)
            //    {

            //        string b15 = dt5.Rows[i]["beizhu3"].ToString();
            //        string b25 = dt5.Rows[i]["beizhu4"].ToString();


            //        if (b15 == "")
            //        {
            //            b15 = "2012-01-01";
            //        }
            //        else
            //        {
            //            b15 = dt5.Rows[i]["beizhu3"].ToString();
            //        }

            //        if (b25 == "")
            //        {
            //            b25 = "2012-01-01";
            //        }
            //        else
            //        {
            //            b25 = dt5.Rows[i]["beizhu4"].ToString();
            //        }
            //        DateTime time15 = Convert.ToDateTime(b15);
            //        DateTime time25 = Convert.ToDateTime(b25);

            //        searchwhere sr = new searchwhere();
            //        arrStr[i] = sr.tian1(time25, time15);


            //    }




            //    zuidatian = getmax(arrStr);



            //    int shijitian = 0;

            //    searchwhere sr2 = new searchwhere();
            //    int zonggongtian = sr2.tian1(realtime, DateTime.Now);


            //    string sqlbao = "select * from baogao2 where tjid='" + f + "'";
            //    SqlCommand cmdbao = new SqlCommand(sqlbao, con);
            //    SqlDataReader drbao = cmdbao.ExecuteReader();
            //    if (drbao.Read())
            //    {

            //        pizhun = drbao["pizhunby"].ToString();
            //        pizhundate = drbao["pizhundate"].ToString();
            //    }
            //    drbao.Close();
            //    con.Close();
            //    if (pizhun == "")//任务还没有结束,判断任务是否超期或者即将超期
            //    {
            //        DateTime d1 = DateTime.Now;
            //        DateTime d2 = realtime;


            //        if (zonggongtian > kaoheshijian)//还没有结束的任务到今天为止的天数超过考核天数。则一定超期，接下来区分是否因为客户原因超期。
            //        {

            //            // e.Row.ForeColor = Color.Red;





            //            if (zuidatian > kehu)//如果工程师确认的日期到提出日期的差最大值超过了规定的3天，则算为客户原因。
            //            {
            //                //客户原因超期
            //                searchwhere sr66 = new searchwhere();
            //                shijitian = sr66.tian1(realtime, DateTime.Now) - (zuidatian + kehu);
            //                if (shijitian < 0)
            //                {
            //                    searchwhere sr = new searchwhere();
            //                    shijitian = sr.tian1(realtime, DateTime.Now);
            //                }


            //            }
            //            else
            //            {
            //                //内部原因超期
            //                searchwhere sr = new searchwhere();
            //                shijitian = sr.tian1(realtime, DateTime.Now);



            //            }


            //        }
            //        else
            //        {
            //            //即将超期或者还没有超期：
            //            searchwhere sr = new searchwhere();
            //            shijitian = sr.tian1(realtime, DateTime.Now);

            //            //searchwhere sr3 = new searchwhere();
            //            //if (sr3.tian1(realtime, DateTime.Now.AddDays(chaoqitian)) == kaoheshijian)
            //            //{
            //            //    e.Row.ForeColor = Color.Chocolate; 
            //            //}




            //        }
            //    }
            //    else//任务已经结束
            //    {
            //        searchwhere sr = new searchwhere();
            //        shijitian = sr.tian1(realtime, Convert.ToDateTime(pizhundate));
            //        if (shijitian < 0)
            //        {
            //            searchwhere sr4 = new searchwhere();
            //            shijitian = sr4.tian1(realtime, DateTime.Now);
            //        }
            //        searchwhere sr5 = new searchwhere();
            //        zonggongtian = sr5.tian1(realtime, Convert.ToDateTime(pizhundate));
            //    }

            //    #endregion


            //    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //    con1.Open();
            //    string leibie = "";
            //    string mtian = "0";
            //    string sqlz = "select top 1 beizhu1 ,beizhu3,time1 from zanting2 where rwbianhao='" + f + "' order by id desc";
            //    SqlCommand cmdz = new SqlCommand(sqlz, con1);
            //    SqlDataReader drz = cmdz.ExecuteReader();
            //    if (drz.Read())
            //    {
            //        leibie = drz["beizhu3"].ToString();
            //        mtian = drz["beizhu1"].ToString();

            //    }
            //    drz.Close();


            //    if (mtian == "")
            //    {
            //        mtian = "0";
            //    }
            //    int mtian1 = Convert.ToInt32(mtian);





            //    if (leibie == "暂停")
            //    {

            //        if (mtian1 == 0)
            //        {
            //            mtian1 = shijitian;
            //        }
            //        e.Row.Cells[10].Text = mtian1.ToString();
            //    }
            //    else
            //    {
            //        int st = 0;
            //        int zt = 0;
            //        int mt = 0;
            //        string sqlz1 = "select top 2 beizhu1 ,beizhu3,time1 from zanting2 where rwbianhao='" + f + "' order by id desc";
            //        SqlDataAdapter ad10 = new SqlDataAdapter(sqlz1, con1);
            //        DataSet ds10 = new DataSet();
            //        ad10.Fill(ds10);
            //        DataTable dt10 = ds10.Tables[0];
            //        if (dt10.Rows.Count > 1)
            //        {
            //            DateTime d1z = Convert.ToDateTime(dt10.Rows[0]["time1"].ToString());
            //            DateTime d2z = Convert.ToDateTime(dt10.Rows[1]["time1"].ToString());
            //            string bst = "0";
            //            if (dt10.Rows[1]["beizhu1"].ToString() == "")
            //            {
            //                bst = "0";
            //            }
            //            else
            //            {
            //                bst = dt10.Rows[1]["beizhu1"].ToString();
            //            }
            //            st = Convert.ToInt32(bst);

            //            searchwhere sr51 = new searchwhere();

            //            zt = sr51.tian1(d2z, d1z);
            //            if (zt == 1)
            //            {
            //                zt = 0;
            //            }

            //            searchwhere sr52 = new searchwhere();

            //            mt = sr52.tian1(realtime, Convert.ToDateTime(dt10.Rows[0]["time1"].ToString()));



            //        }
            //        int shijitianjian = shijitian - (mt - Convert.ToInt32(st));
            //        if (shijitianjian == 0)
            //        {
            //            shijitianjian = shijitian;
            //        }
            //        e.Row.Cells[10].Text = shijitianjian.ToString();
            //    }


            //    con1.Close();


            //    e.Row.Cells[11].Text = zonggongtian.ToString();
            //}

            //string bijiao1 = "0";

            //if (e.Row.Cells[10].Text != "" && e.Row.Cells[10].Text != "&nbsp")
            //{
            //    bijiao1 = e.Row.Cells[10].Text;
            //}

            //bool A = false;

            //if (Convert.ToInt32(bijiao1) > 10)
            //{
            //    A = true;
            //}
            //bool B = false;

            //if (pizhun == "" && Convert.ToDateTime(e.Row.Cells[8].Text) < DateTime.Now)
            //{
            //    B = true;
            //}

            //bool C = false;
            //if (pizhun != "" && Convert.ToDateTime(e.Row.Cells[8].Text) < Convert.ToDateTime(e.Row.Cells[9].Text))
            //{
            //    C = true;
            //}



            //if (A || B || C)
            //{
            //   // e.Row.ForeColor  = Color.Red;
            //    e.Row.Visible = true;
            //}
            //else
            //{
            //   // e.Row.ForeColor = Color.Black;
            //    e.Row.Visible = false;
            //}

            //if (e.Row.Cells[3].Text != "CCC" && e.Row.Cells[14].Text == "完成")
            //{
            //    e.Row.Visible = false;
            //}

            //string sjt3 = "";
            //string zts3 = "";
            //searchwhere sx3 = new searchwhere();
            //string sjt1 = sx3.ShiXiao(e.Row.Cells[0].Text, out sjt3, out zts3);

            //int asjt = Convert.ToInt32(sjt3) - 1;
            //int bzts = Convert.ToInt32(zts3) - 1;
            //e.Row.Cells[10].Text = asjt.ToString();
            //e.Row.Cells[11].Text = bzts.ToString();

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
        //string mq = Session["UserName"].ToString() + DateTime.Now.ToShortDateString();

        //if (Session[mq] == null)
        //{
        //    Session[mq] = RadioButtonList1.SelectedValue;
        //}

        //RadioButtonList1.SelectedValue = Session[mq].ToString();
        Bind(RadioButtonList1.SelectedValue);

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
        string sid = e.CommandArgument.ToString();

        if (e.CommandName == "cancel1")
        {

            if (DropDownList3.SelectedValue != "")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();




                string sql = "update anjianinfo2 set songjiandate='" + DropDownList3.SelectedValue + "' where (rwbianhao='" + sid + "')";




                SqlCommand cmdy = new SqlCommand(sql, con);
                cmdy.ExecuteNonQuery();




                con.Close();

                AspNetPager2.Visible = false;

                string ChooseNo = (DropDownList1.SelectedValue);
                string ChooseValue = TextBox1.Text;
                string pai = RadioButtonList1.SelectedValue;
                string sqlstr = "";

                string chaos = " and ((convert(datetime,yaoqiuwanchengriqi) < '" + DateTime.Now + "' and state !='完成')  or (rwbianhao in (select rwbianhao from chaoqibiao)) or (convert(datetime,beizhu3) > convert(datetime,yaoqiuwanchengriqi) and state='完成')) and convert(datetime,xiadariqi) < convert(datetime,yaoqiuwanchengriqi)";


                string shic = " and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "' ";

                if (TextBox1.Text.Trim() == "")
                {

                    if (Session["role"].ToString() == "8" || Session["role"].ToString() == "1" || xian)
                    {

                        if (DropDownList1.SelectedValue == "0")
                        {
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state ='下达' or state='进行中') and  rwbianhao  in (select bianhao from ZhuJianEngineer)  " + chaos + "   " + shic + " )   " + pai + "";

                        }
                        else if (DropDownList1.SelectedValue == "1")
                        {
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='暂停') and  rwbianhao  in (select bianhao from ZhuJianEngineer) " + chaos + "   " + shic + " )  " + pai + "";

                        }
                        else if (DropDownList1.SelectedValue == "2")
                        {
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='完成') and  rwbianhao  in (select bianhao from ZhuJianEngineer)  " + chaos + "   " + shic + ")  " + pai + "";

                        }

                    }
                    else
                    {
                        if (DropDownList1.SelectedValue == "0")
                        {
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state ='下达' or state='进行中') and  rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " " + chaos + "   " + shic + ")  " + pai + "";

                        }
                        else if (DropDownList1.SelectedValue == "1")
                        {
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='暂停') and  rwbianhao  in (select bianhao from ZhuJianEngineer) and " + shwhere + " " + chaos + "   " + shic + ")  " + pai + "";

                        }
                        else if (DropDownList1.SelectedValue == "2")
                        {
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='完成') and  rwbianhao  in (select bianhao from ZhuJianEngineer) and " + shwhere + " " + chaos + "   " + shic + ")  " + pai + "";

                        }

                    }
                }
                else
                {
                    if (Session["role"].ToString() == "8" || Session["role"].ToString() == "1" || xian)
                    {

                        if (DropDownList2.SelectedValue == "或")
                        {
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (  rwbianhao  in (select bianhao from ZhuJianEngineer))   and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%') " + chaos + "   " + shic + ") " + pai + "";
                        }
                        else
                        {

                            if (DropDownList1.SelectedValue == "0")
                            {

                                sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (  rwbianhao  in (select bianhao from ZhuJianEngineer))    and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='下达' or state='进行中'  ) " + chaos + "   " + shic + " " + pai + "";
                            }
                            else if (DropDownList1.SelectedValue == "1")
                            {

                                sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (  rwbianhao  in (select bianhao from ZhuJianEngineer))   and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or kf like '%" + TextBox1.Text.Trim() + "%' or (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='暂停' ) " + chaos + "   " + shic + " " + pai + "";
                            }
                            else if (DropDownList1.SelectedValue == "2")
                            {

                                sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (  rwbianhao  in (select bianhao from ZhuJianEngineer))   and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='完成' )" + chaos + "   " + shic + "  " + pai + "";
                            }

                        }

                    }
                    else
                    {
                        if (DropDownList2.SelectedValue == "或")
                        {
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) " + chaos + "   " + shic + " " + pai + "";
                        }
                        else
                        {
                            if (DropDownList1.SelectedValue == "0")
                            {

                                sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='下达' or state='进行中' ) " + chaos + "   " + shic + " " + pai + "";
                            }
                            else if (DropDownList1.SelectedValue == "1")
                            {

                                sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='暂停' ) " + chaos + "   " + shic + " " + pai + "";
                            }
                            else if (DropDownList1.SelectedValue == "2")
                            {

                                sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='完成' ) " + chaos + "   " + shic + " " + pai + "";
                            }

                        }


                    }
                }
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con1.Open();
                SqlDataAdapter ad = new SqlDataAdapter(sqlstr, con1);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                con1.Close();
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                // ld.Text = "<script>alert('请选择部门!');</script>";
                // Bind(RadioButtonList1.SelectedValue);
            }
        }
        else if (e.CommandName == "cancel2")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sql = "update anjianinfo2 set beizhu1='',beizhu2='" + Session["UserName"].ToString() + "' where (rwbianhao='" + sid + "')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            Bind(RadioButtonList1.SelectedValue);
        }







    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string mq = Session["UserName"].ToString() + DateTime.Now.ToShortDateString();
        //Session[mq] = RadioButtonList1.SelectedValue;
        //Bind(Session[mq].ToString());
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
}