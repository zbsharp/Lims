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


public partial class ShiXiao_Default3 : System.Web.UI.Page
{
    //beizhu:工程是否确认，beizhu3：工程师确认日期，beizhu4:首次提出日期
    //http://blog.csdn.net/wang4978/article/details/4220431

    protected string shwhere = "1=1";
    protected string shwhere1 = "";
    protected string shwhere2 = "";
    protected string shwhere3 = "";
    protected string shwhere4 = "";


    private int _i = 0;
    const string vsKey = "jinxingzhong";
    int tichu = 2;//提出日期表示从下达日起允许提出的日期，比如11.10下达，则计算暂停和超期就从11.2号开始
    int kehu = 3;
    int chaoqitian = 2;
    protected string str = "";
    Hashtable a = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {
        shwhere4 = "rwbianhao not in (select rwbianhao from zanting) and rwbianhao not in (select top 1 tjid from baogao2 where pizhunby !='') and (state='下达' or state='进行中') and  (b1='申请暂停' or b1='是')";


        shwhere3 = "rwbianhao not in (select rwbianhao from zanting) and rwbianhao not in (select top 1 tjid from baogao2 where pizhunby !='') and (state='下达' or state='进行中') and  ( kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text.Trim() + "%'))";

        shwhere2 = "rwbianhao not in (select rwbianhao from zanting) and rwbianhao not in (select top 1 tjid from baogao2 where pizhunby !='') and (state='下达' or state='进行中') and  ( kf like '%" + TextBox1.Text.Trim() + "%')";

        shwhere1 = "rwbianhao not in (select rwbianhao from zanting) and rwbianhao not in (select top 1 tjid from baogao2 where pizhunby !='') and ( state='下达' or state='进行中') and (convert(datetime, yaoqiuwanchengriqi) < '" + DateTime.Now + "')";

        shwhere = "rwbianhao not in (select rwbianhao from zanting) and rwbianhao not in (select top 1 tjid from baogao2 where pizhunby !='') and  ( state='下达' or state='进行中')";

        str = "select top 200 *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 ";

        if (!IsPostBack)
        {

            Bind();
            Bind1();
            pw.Visible = true;
            BindDep();
        }
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



        string sql = "select * from ZangTingLeiBie where name='" + DropDownList2.SelectedValue + "'  order by wenyuan asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList3.DataSource = ds.Tables[0];
        DropDownList3.DataTextField = "wenyuan";
        DropDownList3.DataValueField = "wenyuan";
        DropDownList3.DataBind();



        con3.Close();
    }



    protected void Timer1_Tick(object sender, EventArgs e)
    {








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


    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";

        sql = str + " where " + shwhere + " and b6='申请暂停' order by convert(datetime,b7) desc";


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
        string ChooseValue = TextBox1.Text.Trim();

        string sqlstr;


        string str1 = "select  *,chanpinname  as na, xinghaoguige as gg, ziliaostate, kf, xiadariqi  as xiada, shixian  as shixian,yaoqiushixian  as shixian2,(select top 1 state from baogao2 where tjid=anjianinfo2.rwbianhao) as st1,(select top 1 customname from customer where kehuid =anjianinfo2.kehuid) as kehuname from anjianinfo2 order by convert(datetime,b7)";



        if (DropDownList1.SelectedValue == "yichaoqi")
        {
            sqlstr = str1 + " where  " + shwhere1 + " and b6='申请暂停' order by convert(datetime,b7) desc ";

        }
        else if (DropDownList1.SelectedValue == "kf")
        {
            sqlstr = str1 + " where  " + shwhere2 + " and b6='申请暂停' order by convert(datetime,b7) desc ";

        }
        else if (DropDownList1.SelectedValue == "kehu")
        {
            sqlstr = str1 + " where  " + shwhere3 + " and b6='申请暂停' order by convert(datetime,b7) desc ";

        }
        else if (DropDownList1.SelectedValue == "sq")
        {
            sqlstr = str1 + " where  " + shwhere4 + " and b6='申请暂停' order by convert(datetime,b7) desc ";

        }
        else
        {
            sqlstr = str1 + " where " + ChooseNo + " like  '%" + ChooseValue + "%' and " + shwhere + " and b6='申请暂停' order by convert(datetime,b7) desc ";
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


            e.Row.Cells[1].Text = SubStr(e.Row.Cells[1].Text, 5);
            e.Row.Cells[2].Text = SubStr(e.Row.Cells[2].Text, 18);
            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 6);
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 6);




            DateTime realtime = DateTime.Now;//任务下达日期
            DateTime kaoheriqi = DateTime.Now;//考核日期
            int kaoheshijian = 10;//考核日期也可以根据时限计算
            string kaoheshijians = "";
            string f = e.Row.Cells[0].Text.ToString();



            MyExcutSql ext = new MyExcutSql();
            e.Row.Cells[13].Text = ext.Eng(e.Row.Cells[0].Text);


            //string sjt4 = "";
            //string zts4 = "";
            //searchwhere sx4 = new searchwhere();
            //string sjt1 = sx4.ShiXiao(e.Row.Cells[0].Text, out sjt4, out zts4);

            //int asjt = Convert.ToInt32(sjt4) - 1;
            //int bzts = Convert.ToInt32(zts4) - 1;
            //e.Row.Cells[5].Text = asjt.ToString();
            //e.Row.Cells[6].Text = bzts.ToString();




            bool d = false;
            d = limit1("案件暂停");

            LinkButton LinkBtn_DetailInfo2 = (LinkButton)e.Row.FindControl("LinkButton5");



            if (e.Row.Cells[12].Text == Session["UserName"].ToString() || d == true)
            {



            }
            else
            {
                LinkBtn_DetailInfo2.Enabled = false;
                LinkBtn_DetailInfo2.ForeColor = Color.DarkGray;




            }


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

        if (e.CommandName == "xiada")
        {



            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string index1 = gvrow.Cells[5].Text.Trim();





            string sqlstate = "insert into  zanting values ('" + sid + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','','" + TextBox2.Text.Trim() + "','" + DropDownList2.SelectedValue + "','" + DropDownList3.SelectedValue + "')";
            SqlCommand cmdstate = new SqlCommand(sqlstate, con);
            cmdstate.ExecuteNonQuery();



            string sqlstate4 = "update anjianinfo2 set state='暂停',b1='',b2='',b6='' where rwbianhao='" + sid + "'";
            SqlCommand cmdstate4 = new SqlCommand(sqlstate4, con);
            cmdstate4.ExecuteNonQuery();



            string sqlstate2 = "insert into  zanting2 values ('" + sid + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','" + index1 + "','" + TextBox2.Text.Trim() + "','暂停','" + DropDownList2.SelectedValue + "','" + DropDownList3.SelectedValue + "')";
            SqlCommand cmdstate2 = new SqlCommand(sqlstate2, con);
            cmdstate2.ExecuteNonQuery();


            con.Close();


            MyExcutSql my = new MyExcutSql();
            my.ExtTaskone(sid, sid, "暂停任务", "手工提交", Session["UserName"].ToString(), "中止任务", DateTime.Now, "暂停");




            TextBox2.Text = "";

            if (TextBox1.Text == "")
            {
                Bind();
            }
            else
            {
                ButtonBind();
            }

        }

        else   if (e.CommandName == "xiada1")
        {



            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string index1 = gvrow.Cells[5].Text.Trim();





         



            string sqlstate4 = "update anjianinfo2 set b6='' where rwbianhao='" + sid + "'";
            SqlCommand cmdstate4 = new SqlCommand(sqlstate4, con);
            cmdstate4.ExecuteNonQuery();





            con.Close();


            MyExcutSql my = new MyExcutSql();
            my.ExtTaskone(sid, sid, "取消申请暂停", "手工提交", Session["UserName"].ToString(), "取消申请暂停", DateTime.Now, "取消申请暂停");




            TextBox2.Text = "";

            if (TextBox1.Text == "")
            {
                Bind();
            }
            else
            {
                ButtonBind();
            }

        }





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
        con.Close();
    }

}