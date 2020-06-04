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
public partial class ShiXiao_YiFenYiShou :  BasePage
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

        ///dddd
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
            //发射点发
        }
        else
        {
            shwhere = " ( rwbianhao in (select bianhao from ZhuJianEngineer where name='" + Session["UserName"].ToString() + "'))";
        }
        //犯得上发
        if (!IsPostBack)
        {
            Session["dd"] = RadioButtonList1.SelectedValue;
            Bind(RadioButtonList1.SelectedValue);
            BindDep();
            Bind1();
        }
    }
    protected void Bind1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string dd = DateTime.Now.ToShortDateString() + " 00:00:00";
        string sql = "select  * from anjianbeizhu where neirong !='' and time between '" + Convert.ToDateTime(dd) + "' and '" + DateTime.Now + "' ORDER BY id desc";





        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            pw.Message = pw.Message + dr["xiangmuid"].ToString() + ":" + dr["neirong"].ToString() + "<br/>";

            pw.Text = dr["neirong"].ToString();
        }

        con.Close();
        con.Dispose();
    }
    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select * from UserDepa where departmentid='12' or departmentid='13' or departmentid='15' or departmentid='16' or departmentid='17' or departmentid='9'";
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



        string str = "select top 200 *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname,(select top 1 beiyong from AnJianInFo where tijiaobianhao=AnJianInFo2.bianhao) as beiyong  from anjianinfo2 where (state !='受理' and  rwbianhao  in (select bianhao from ZhuJianEngineer)) and " + shwhere + " " + dd + "";


        sql = str;


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
        Set_Color();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        AspNetPager2.Visible = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;
        string pai = RadioButtonList1.SelectedValue;
        string sqlstr = "";
        if (TextBox1.Text == "认可CB" || TextBox1.Text == "认可cb") 
        {
            sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where baojiaid in (select baojiaid from baojiacpxiangmu where ceshiname like '%认可cb%')";
        }
        else
        {

            if (TextBox1.Text.Trim() == "")
            {

                if (Session["role"].ToString() == "8" || Session["role"].ToString() == "1" || xian)
                {

                    if (DropDownList1.SelectedValue == "0")
                    {
                        sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where ((state ='下达' or state='进行中')  )   " + pai + "";

                    }
                    else if (DropDownList1.SelectedValue == "1")
                    {
                        sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where ((state='暂停')   )  " + pai + "";

                    }
                    else if (DropDownList1.SelectedValue == "2")
                    {
                        sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where ((state='完成')   )  " + pai + "";

                    }
                    else if (DropDownList1.SelectedValue == "3")
                    {
                        sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where ((state ='下达' or state='进行中')  and (convert(datetime, yaoqiuwanchengriqi) < '" + DateTime.Now.AddDays(-1) + "'  ) " + pai + "";

                    }
                    else if (DropDownList1.SelectedValue == "4")
                    {
                        sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where ((state ='下达' or state='进行中')  and (convert(datetime, yaoqiuwanchengriqi) between '" + DateTime.Now + "' and  '" + DateTime.Now.AddDays(4) + "' )   " + pai + "";

                    }
                }
                else
                {
                    if (DropDownList1.SelectedValue == "0")
                    {
                        sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where ((state ='下达' or state='进行中')   and " + shwhere + " )  " + pai + "";

                    }
                    else if (DropDownList1.SelectedValue == "1")
                    {
                        sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where ((state='暂停')  and " + shwhere + " )  " + pai + "";

                    }
                    else if (DropDownList1.SelectedValue == "2")
                    {
                        sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where ((state='完成')  and " + shwhere + ")  " + pai + "";

                    }
                    else if (DropDownList1.SelectedValue == "3")
                    {
                        sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where ((state ='下达' or state='进行中')  and (convert(datetime, yaoqiuwanchengriqi) < '" + DateTime.Now.AddDays(-1) + "') and " + shwhere + " )  " + pai + "";

                    }
                    else if (DropDownList1.SelectedValue == "4")
                    {
                        sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where ((state ='下达' or state='进行中')  and (convert(datetime, yaoqiuwanchengriqi) between '" + DateTime.Now + "' and  '" + DateTime.Now.AddDays(4) + "') and " + shwhere + " ) " + pai + "";

                    }
                }
            }
            else
            {
                if (Session["role"].ToString() == "8" || Session["role"].ToString() == "1" || xian)
                {

                    if (DropDownList2.SelectedValue == "或")
                    {
                        sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where   (b3 like '%" + TextBox1.Text.Trim() + "%'  or  rwbianhao like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) " + pai + "";
                    }
                    else
                    {

                        if (DropDownList1.SelectedValue == "0")
                        {

                            sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where   ( b3 like '%" + TextBox1.Text.Trim() + "%'  or rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='下达' or state='进行中' ) " + pai + "";
                        }
                        else if (DropDownList1.SelectedValue == "1")
                        {

                            sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where   (b3 like '%" + TextBox1.Text.Trim() + "%'  or  rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='暂停' ) " + pai + "";
                        }
                        else if (DropDownList1.SelectedValue == "2")
                        {

                            sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where   (b3 like '%" + TextBox1.Text.Trim() + "%'  or  rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='完成' ) " + pai + "";
                        }
                        else if (DropDownList1.SelectedValue == "3")
                        {

                            sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where   ( b3 like '%" + TextBox1.Text.Trim() + "%'  or rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and " + shwhere1 + " " + pai + "";
                        }
                        else if (DropDownList1.SelectedValue == "4")
                        {

                            sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where   (b3 like '%" + TextBox1.Text.Trim() + "%'  or  rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and " + shwhere11 + " " + pai + "";
                        }
                    }

                }
                else
                {
                    if (DropDownList2.SelectedValue == "或")
                    {
                        sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and   ( b3 like '%" + TextBox1.Text.Trim() + "%'  or rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) " + pai + "";
                    }
                    else
                    {
                        if (DropDownList1.SelectedValue == "0")
                        {

                            sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (b3 like '%" + TextBox1.Text.Trim() + "%'  or  rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='下达' or state='进行中' ) " + pai + "";
                        }
                        else if (DropDownList1.SelectedValue == "1")
                        {

                            sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (b3 like '%" + TextBox1.Text.Trim() + "%'  or  rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='暂停' ) " + pai + "";
                        }
                        else if (DropDownList1.SelectedValue == "2")
                        {

                            sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (b3 like '%" + TextBox1.Text.Trim() + "%'  or  rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='完成' ) " + pai + "";
                        }
                        else if (DropDownList1.SelectedValue == "3")
                        {

                            sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (b3 like '%" + TextBox1.Text.Trim() + "%'  or  rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and " + shwhere1 + " " + pai + "";
                        }
                        else if (DropDownList1.SelectedValue == "4")
                        {

                            sqlstr = "select *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (b3 like '%" + TextBox1.Text.Trim() + "%'  or  rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and " + shwhere11 + " " + pai + "";
                        }
                    }


                }
            }
        }
        SqlDataAdapter ad = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
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



            e.Row.Cells[2].Text = SubStr(e.Row.Cells[2].Text, 5);
            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 5);
            MyExcutSql ext = new MyExcutSql();
            e.Row.Cells[5].Text = ext.Eng(e.Row.Cells[0].Text);
            e.Row.Cells[4].Text = ext.EngBumen(e.Row.Cells[0].Text);
            // e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 5);



            string sid = e.Row.Cells[1].ToString();
            bool B = false;
            B = limit1("取消参与");

            string dd = Session["UserName"].ToString();

            if (B == true || e.Row.Cells[5].Text.Contains(dd))
            {



            }
            else
            {
                LinkButton LinkBtn_DetailInfo2 = (LinkButton)e.Row.FindControl("LinkButton6");

                LinkBtn_DetailInfo2.Enabled = false;
                LinkBtn_DetailInfo2.ForeColor = Color.DarkGray;

            }

            //if (e.Row.Cells[12].Text == "暂停" || e.Row.Cells[12].Text == "中止")
            //{
            //    e.Row.ForeColor = Color.Red;
            //}

            
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
        string mq = Session["UserName"].ToString() + DateTime.Now.ToShortDateString();

        if (Session[mq] == null)
        {
            Session[mq] = RadioButtonList1.SelectedValue;
        }

        RadioButtonList1.SelectedValue = Session[mq].ToString();
        Bind(Session[mq].ToString());

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


                string sqly = "";
                string sqlx = "";
                string zhi = "";
                string sql = "select canyu from anjianinfo where (taskid='" + sid + "' and bumen='" + DropDownList3.SelectedValue + "')";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    zhi = dr["canyu"].ToString();
                }
                dr.Close();

                if (zhi == "" || zhi == "是")
                {
                    sqly = "update anjianinfo set canyu='否' where (taskid='" + sid + "' and bumen='" + DropDownList3.SelectedValue + "')";

                    sqlx = "update baogaobumen set beizhu2='否' where rwid=(select top 1 taskid from anjianinfo where (taskid='" + sid + "' and bumen='" + DropDownList3.SelectedValue + "')) and bumen=(select top 1 bumen from anjianinfo where (taskid='" + sid + "' and bumen='" + DropDownList3.SelectedValue + "'))";
                }
                else
                {
                    sqly = "update anjianinfo set canyu='是' where (taskid='" + sid + "' and bumen='" + DropDownList3.SelectedValue + "')";

                    sqlx = "update baogaobumen set beizhu2='是' where rwid=(select top 1 taskid from anjianinfo where (taskid='" + sid + "' and bumen='" + DropDownList3.SelectedValue + "')) and bumen=(select top 1 bumen from anjianinfo where (taskid='" + sid + "' and bumen='" + DropDownList3.SelectedValue + "'))";


                }
                SqlCommand cmdy = new SqlCommand(sqly, con);
                cmdy.ExecuteNonQuery();


                SqlCommand cmdx = new SqlCommand(sqlx, con);
                cmdx.ExecuteNonQuery();

                con.Close();

                //Bind(RadioButtonList1.SelectedValue);
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
           // Bind(RadioButtonList1.SelectedValue);
        }







    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mq = Session["UserName"].ToString() + DateTime.Now.ToShortDateString();
        Session[mq] = RadioButtonList1.SelectedValue;
        Bind(Session[mq].ToString());
    }
    /// <summary>
    /// 设置加急行的颜色
    /// </summary>
    protected void Set_Color()
    {
        //************2019-8-16修改  设置GridView的颜色
        int columns = this.GridView1.Columns.Count;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            string a = GridView1.Rows[i].Cells[columns - 1].Text.ToString().Trim();
            if (GridView1.Rows[i].Cells[columns - 1].Text.ToString().Trim() != "普通")
            {
                //GridView1.Rows[i].BackColor = System.Drawing.Color.Red;//背景颜色
                GridView1.Rows[i].ForeColor = Color.Red;//字体颜色
            }
        }
    }
}