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

public partial class Case_YiFenYiShou4 : System.Web.UI.Page
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
        if (Session["role"].ToString() == "8" || Session["role"].ToString() == "1" || Session["role"].ToString() == "3" || xian || Session["role"].ToString() == "9")
        {

            shwhere = "1=1";

        }

        else
        {
            shwhere = " ( rwbianhao in (select bianhao from ZhuJianEngineer where name='" + Session["UserName"].ToString() + "'))";

        }





        if (!IsPostBack)
        {
            //DateTime dt = DateTime.Now.AddMonths(-6);
            //int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            //int dayspan = (-1) * weeknow + 1;
            //DateTime dt2 = DateTime.Now.AddMonths(+6);
            ////本月第一天
            //txFDate.Value = dt.AddMonths(-6).AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");


            //DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(6).ToString("yyyy-MM-01")).AddDays(-1).AddHours(23);

            //txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();

            txFDate.Value = DateTime.Now.AddMonths(-12).ToString("yyyy-MM-01");


            DateTime lastDay = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddHours(23);

            txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();


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
      // string chaos = " and ((convert(datetime,yaoqiuwanchengriqi) < '" + DateTime.Now + "' and state !='完成')  or (rwbianhao in (select rwbianhao from chaoqibiao)) or (convert(datetime,beizhu3) > convert(datetime,yaoqiuwanchengriqi) and state='完成')) and convert(datetime,xiadariqi) < convert(datetime,yaoqiuwanchengriqi)";

        string chaos = " and state !='暂停'  and baogao='是' and convert(datetime,xiadariqi) >'2013-3-6' and (day1>(yaoqiushixian+shixian+abs(yaoqiushixian-shixian))/2) ";


       string str = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (( state='进行中' or state='下达')) and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "' and " + shwhere + " " + chaos + " " + dd + "";


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


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        AspNetPager2.Visible = false;
       
        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;
        string pai = RadioButtonList1.SelectedValue;
        string sqlstr = "";

       // string chaos = " and ((convert(datetime,yaoqiuwanchengriqi) < '" + DateTime.Now + "' and state !='完成')  or (rwbianhao in (select rwbianhao from chaoqibiao)) or (convert(datetime,beizhu3) > convert(datetime,yaoqiuwanchengriqi) and state='完成')) and convert(datetime,xiadariqi) < convert(datetime,yaoqiuwanchengriqi)";
        string chaos = " and baogao='是' and convert(datetime,xiadariqi) >'2013-3-6' and (day1>(yaoqiushixian+shixian+abs(yaoqiushixian-shixian))/2) ";


        string shic = " and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "' ";

        if (TextBox1.Text.Trim() == "")
        {

            if (Session["role"].ToString() == "8" || Session["role"].ToString() == "1" || xian)
            {

                if (DropDownList1.SelectedValue == "0")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state ='下达' or state='进行中')   "+chaos+"   " + shic + " )   " + pai + "";

                }
                else if (DropDownList1.SelectedValue == "1")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='暂停')  "+chaos+"   " + shic + " )  " + pai + "";

                }
                else if (DropDownList1.SelectedValue == "2")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='完成' or state='关闭')  " + chaos + "   " + shic + ")  " + pai + "";

                }
              
            }
            else
            {
                if (DropDownList1.SelectedValue == "0")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state ='下达' or state='进行中')   and " + shwhere + " "+chaos+"   " + shic + ")  " + pai + "";

                }
                else if (DropDownList1.SelectedValue == "1")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='暂停')  and " + shwhere + " "+chaos+"   " + shic + ")  " + pai + "";

                }
                else if (DropDownList1.SelectedValue == "2")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='完成' or state='关闭')  and " + shwhere + " " + chaos + "   " + shic + ")  " + pai + "";

                }
              
            }
        }
        else
        {
            if (Session["role"].ToString() == "8" || Session["role"].ToString() == "1" || xian)
            {

                if (DropDownList2.SelectedValue == "或")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (  rwbianhao  in (select bianhao from ZhuJianEngineer))   and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%') "+chaos+"   " + shic + ") " + pai + "";
                }
                else
                {

                    if (DropDownList1.SelectedValue == "0")
                    {

                        sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (  rwbianhao  in (select bianhao from ZhuJianEngineer))    and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='下达' or state='进行中'  ) "+chaos+"   " + shic + " " + pai + "";
                    }
                    else if (DropDownList1.SelectedValue == "1")
                    {

                        sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (  rwbianhao  in (select bianhao from ZhuJianEngineer))   and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or kf like '%" + TextBox1.Text.Trim() + "%' or (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='暂停' ) "+chaos+"   " + shic + " " + pai + "";
                    }
                    else if (DropDownList1.SelectedValue == "2")
                    {

                        sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (  rwbianhao  in (select bianhao from ZhuJianEngineer))   and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='完成' or state='关闭')" + chaos + "   " + shic + "  " + pai + "";
                    }
                   
                }

            }
            else
            {
                if (DropDownList2.SelectedValue == "或")
                {
                    sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) "+chaos+"   " + shic + " " + pai + "";
                }
                else
                {
                    if (DropDownList1.SelectedValue == "0")
                    {

                        sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='下达' or state='进行中' ) "+chaos+"   " + shic + " " + pai + "";
                    }
                    else if (DropDownList1.SelectedValue == "1")
                    {

                        sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='暂停' ) "+chaos+"   " + shic + " " + pai + "";
                    }
                    else if (DropDownList1.SelectedValue == "2")
                    {

                        sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where (   rwbianhao  in (select bianhao from ZhuJianEngineer)  and " + shwhere + " ) and (rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or shiyanleibie like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%' or rwbianhao in (select taskid from anjianinfo where bumen like '%" + TextBox1.Text.Trim() + "%') or  (weituodanwei like '%" + TextBox1.Text.Trim() + "%') or rwbianhao in (select bianhao from ZhuJianEngineer where name like '%" + TextBox1.Text.Trim() + "%')) and (state='完成' or state='关闭') " + chaos + "   " + shic + " " + pai + "";
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

           

            e.Row.Cells[2].Text = SubStr(e.Row.Cells[2].Text, 5);
            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 5);
           // e.Row.Cells[22].Text = SubStr(e.Row.Cells[22].Text, 11) + "...";

            if (e.Row.Cells[9].Text != "" && e.Row.Cells[9].Text != "&nbsp" && e.Row.Cells[9].Text.Trim().ToString().Substring(0, 4) == "1900")
            {
                e.Row.Cells[9].Text = "";
            }


            MyExcutSql ext = new MyExcutSql();
            e.Row.Cells[5].Text = ext.Eng(e.Row.Cells[0].Text);
            e.Row.Cells[4].Text = ext.EngBumen(e.Row.Cells[0].Text);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select * from zanting2 where rwbianhao='"+e.Row.Cells[0].Text+"'";
            SqlCommand cmd = new SqlCommand(sql,con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                e.Row.Cells[22].Text = "客户原因";
            }
            else
            {
               

                e.Row.Cells[22].Text = "内部原因";
            }
            con.Close();
         

            //string sjt = "";
            //string zts = "";
            //searchwhere sx = new searchwhere();
            //string sjt1 = sx.ShiXiao(e.Row.Cells[0].Text, out sjt, out zts);

            //int asjt = Convert.ToInt32(sjt)-1;
            //int bzts = Convert.ToInt32(zts) - 1;
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


          
             
                string sql = "update anjianinfo2 set songjiandate='"+DropDownList3.SelectedValue +"' where (rwbianhao='" + sid + "')";
               
               

                
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
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state ='下达' or state='进行中')   " + chaos + "   " + shic + " )   " + pai + "";

                        }
                        else if (DropDownList1.SelectedValue == "1")
                        {
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='暂停')  " + chaos + "   " + shic + " )  " + pai + "";

                        }
                        else if (DropDownList1.SelectedValue == "2")
                        {
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='完成')   " + chaos + "   " + shic + ")  " + pai + "";

                        }

                    }
                    else
                    {
                        if (DropDownList1.SelectedValue == "0")
                        {
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state ='下达' or state='进行中')   and " + shwhere + " " + chaos + "   " + shic + ")  " + pai + "";

                        }
                        else if (DropDownList1.SelectedValue == "1")
                        {
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='暂停')  and " + shwhere + " " + chaos + "   " + shic + ")  " + pai + "";

                        }
                        else if (DropDownList1.SelectedValue == "2")
                        {
                            sqlstr = "select *,(select top 1 pizhundate from baogao2 where tjid=anjianinfo2.rwbianhao) as wancheng3, chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2  from anjianinfo2 where ((state='完成')  and " + shwhere + " " + chaos + "   " + shic + ")  " + pai + "";

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