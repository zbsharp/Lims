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

public partial class ShiXiao_JiJangZanTing : System.Web.UI.Page
{
    protected string shwhere = "1=1";
    private int _i = 0;
    const string vsKey = "jzanting";
    int tichu = 2;
    int kehu = 3;

    int chaoqitian = 2;
    Hashtable a = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {


        shwhere = "taskno in (select rwbianhao from anjianinfo2 where state='下达')";



        if (!IsPostBack)
        {

            Bind(string.Empty);

            Session[vsKey] = null;
        }
    }
    public void Bind(string sWhere)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";
        if (sWhere == "" || sWhere == null)
        {



            sql = "select *,(select xiadariqi from anjianinfo2 where rwbianhao=anjianxinxi2.taskno) as xiada,(select shixian from anjianinfo2 where rwbianhao=anjianxinxi2.taskno) as shixian,(select yaoqiushixian from anjianinfo2 where rwbianhao=anjianxinxi2.taskno) as shixian2,(select top 1 state from baogao2 where tjid=anjianxinxi2.taskno) as st1,(select top 1 customname from customer where kehuid =anjianxinxi2.kehuid) as kehuname from anjianxinxi2 where " + shwhere + " ";
        }
        else
        {
            sql = "select *,(select xiadariqi from anjianinfo2 where rwbianhao=anjianxinxi2.taskno) as xiada,(select shixian from anjianinfo2 where rwbianhao=anjianxinxi2.taskno) as shixian,(select yaoqiushixian from anjianinfo2 where rwbianhao=anjianxinxi2.taskno) as shixian2,(select top 1 state from baogao2 where tjid=anjianxinxi2.taskno) as st1,(select top 1 customname from customer where kehuid =anjianxinxi2.kehuid) as kehuname from anjianxinxi2 where " + shwhere + " and " + sWhere + " ";
        }
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager2.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {


        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;

        string sqlstr;

        sqlstr = "" + ChooseNo + " like  '%" + ChooseValue + "%' and " + shwhere + " order by id desc  ";
        AspNetPager2.CurrentPageIndex = 1;

        Session[vsKey] = sqlstr;
        Bind(sqlstr);


    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");



            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();


            DateTime realtime = DateTime.Now;//任务下达日期
            DateTime kaoheriqi = DateTime.Now;//考核日期
            int kaoheshijian = 10;//考核日期也可以根据时限计算
            string kaoheshijians = "";
            string f = e.Row.Cells[0].Text.ToString();
            string yaoqiuwanchengriqi = "";
            string xiadariqi = "";

            string sqlk = "select * from anjianinfo2 where rwbianhao='" + f + "'";
            SqlCommand cmdk = new SqlCommand(sqlk, con);
            SqlDataReader drk = cmdk.ExecuteReader();
            if (drk.Read())
            {
                kaoheshijians = drk["shixian"].ToString();
                yaoqiuwanchengriqi = drk["yaoqiuwanchengriqi"].ToString();
                xiadariqi = drk["xiadariqi"].ToString();
            }
            drk.Close();


            if (xiadariqi == "")
            {
                xiadariqi = "2000-01-01";
            }
            if (yaoqiuwanchengriqi == "")
            {
                yaoqiuwanchengriqi = "2010-01-01";
            }


            if (kaoheshijians == "")
            {

                searchwhere sr = new searchwhere();
                kaoheshijian = sr.tian1(Convert.ToDateTime(xiadariqi), Convert.ToDateTime(yaoqiuwanchengriqi));
            }
            else
            {
                kaoheshijian = Convert.ToInt32(kaoheshijians);
            }
          
            #region 判断因为资料原因是否暂停



            //获取下达日期


            string sql3 = "select dotimereal from taskstate where taskid='" + f + "' and type1='下达任务' order by id desc";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            if (dr3.Read())
            {


                realtime = Convert.ToDateTime(dr3["dotimereal"]);
            }
            dr3.Close();



            //从配置表中获取一段时间内的配置参数，以任务号优先，否则取时间段

            string sqlpeizhi = "select * from WaiQing where waichubianhao='" + f + "'";
            SqlCommand cmdpeizhi = new SqlCommand(sqlpeizhi, con);
            SqlDataReader drpeizhi = cmdpeizhi.ExecuteReader();
            if (drpeizhi.Read())
            {
                if (drpeizhi["chifan"] != DBNull.Value)
                {
                    tichu = Convert.ToInt32(drpeizhi["chifan"]);
                    kehu = Convert.ToInt32(drpeizhi["zhusu"]);
                    chaoqitian = Convert.ToInt32(drpeizhi["beizhu"]);
                }
            }

            else
            {
                drpeizhi.Close();
                string sqlpeizhi1 = "select top 1 * from WaiQing where '" + realtime + "' between chutime and huitime order by id desc";
                SqlCommand cmdpeizhi1 = new SqlCommand(sqlpeizhi1, con);
                SqlDataReader drpeizhi1 = cmdpeizhi1.ExecuteReader();
                if (drpeizhi1.Read())
                {
                    if (drpeizhi1["chifan"] != DBNull.Value)
                    {

                        tichu = Convert.ToInt32(drpeizhi1["chifan"]);
                        kehu = Convert.ToInt32(drpeizhi1["zhusu"]);
                        chaoqitian = Convert.ToInt32(drpeizhi1["beizhu"]);
                    }
                }
                else
                {

                }
                drpeizhi1.Close();

            }
            drpeizhi.Close();
            





        
            int ddd = kehu + tichu-chaoqitian;
            string sqlchaoqi = "select * from anjianxinxi where xiangmuid='" + f + "' and (beizhu !='是' or beizhu is null) and beizhu4 !='' and datediff(day,'" + Convert.ToDateTime(realtime).AddDays(kehu) + "','" + DateTime.Now + "')>'" + ddd + "'";
            SqlCommand cmdchaoqi = new SqlCommand(sqlchaoqi, con);
            SqlDataReader drchaoqi = cmdchaoqi.ExecuteReader();


            if (drchaoqi.Read())
            {
               //e.Row.Cells[6].Text = "暂停";
                e.Row.Visible = true;
                e.Row.ForeColor = Color.Green;


            }
            else
            {
                e.Row.Cells[6].Text = "进行中";
                e.Row.Visible = false;
            }

            drchaoqi.Close();



            #endregion

            #region 计算是否超时

            int zuidatian = 0;

            string sql5 = "select beizhu4,beizhu3 from anjianxinxi where xiangmuid='" + f + "' and (beizhu ='是') and beizhu4 !='' and convert(datetime,beizhu4) between '" + realtime + "' and '" + realtime.AddDays(tichu) + "' and convert(datetime,beizhu3) between '" + realtime + "' and '" + realtime.AddDays(tichu + kehu) + "' order by convert(datetime,beizhu4) asc";
            SqlDataAdapter ad5 = new SqlDataAdapter(sql5, con);
            DataSet ds5 = new DataSet();
            ad5.Fill(ds5);

            DataTable dt5 = ds5.Tables[0];

            int[] arrStr = new Int32[dt5.Rows.Count + 1];

            for (int i = 0; i < dt5.Rows.Count; i++)
            {

                string b15 = dt5.Rows[i]["beizhu3"].ToString();
                string b25 = dt5.Rows[i]["beizhu4"].ToString();


                if (b15 == "")
                {
                    b15 = "2012-01-01";
                }
                else
                {
                    b15 = dt5.Rows[i]["beizhu3"].ToString();
                }

                if (b25 == "")
                {
                    b25 = "2012-01-01";
                }
                else
                {
                    b25 = dt5.Rows[i]["beizhu4"].ToString();
                }
                DateTime time15 = Convert.ToDateTime(b15);
                DateTime time25 = Convert.ToDateTime(b25);

                searchwhere sr2 = new searchwhere();
                arrStr[i] = sr2.tian1(time25, time15);


            }




            zuidatian = getmax(arrStr);

           

            int shijitian = 0;

            searchwhere sr3 = new searchwhere();
            int zonggongtian = sr3.tian1(realtime, DateTime.Now);
            string pizhun = "";
            string pizhundate = "";

            string sqlbao = "select * from baogao2 where tjid='" + f + "'";
            SqlCommand cmdbao = new SqlCommand(sqlbao, con);
            SqlDataReader drbao = cmdbao.ExecuteReader();
            if (drbao.Read())
            {

                pizhun = drbao["pizhunby"].ToString();
                pizhundate = drbao["pizhundate"].ToString();
            }
            drbao.Close();

            if (pizhun == "")//任务还没有结束,判断任务是否超期或者即将超期
            {
                DateTime d1 = DateTime.Now;
                DateTime d2 = realtime;


                if (zonggongtian > kaoheshijian)//还没有结束的任务到今天为止的天数超过考核天数。则一定超期，接下来区分是否因为客户原因超期。
                {

                  //  e.Row.ForeColor = Color.Red;





                    if (zuidatian > kehu)//如果工程师确认的日期到提出日期的差最大值超过了规定的3天，则算为客户原因。
                    {
                        //客户原因超期
                        searchwhere sr4 = new searchwhere();
                        shijitian = sr4.tian1(realtime, DateTime.Now) - (zuidatian + kehu);
                        if (shijitian < 0)
                        {
                            searchwhere sr5 = new searchwhere();
                            shijitian = sr5.tian1(realtime, DateTime.Now);
                        }

                    }
                    else
                    {
                        //内部原因超期
                        searchwhere sr6 = new searchwhere();
                        shijitian = sr6.tian1(realtime, DateTime.Now);



                    }


                }
                else
                {
                    //即将超期或者还没有超期：

                    searchwhere sr7 = new searchwhere();
                    shijitian = sr7.tian1(realtime, DateTime.Now);
                }
            }
            else//任务已经结束
            {
                searchwhere sr8 = new searchwhere();
                shijitian = sr8.tian1(realtime, Convert.ToDateTime(pizhundate)) - (zuidatian + kehu);
                if (shijitian < 0)
                {
                    searchwhere sr9 = new searchwhere();
                    shijitian = sr9.tian1(realtime, DateTime.Now);
                }
                searchwhere sr10 = new searchwhere();
                zonggongtian = sr10.tian1(realtime, Convert.ToDateTime(pizhundate));
            }

            #endregion



            e.Row.Cells[2].Text = shijitian.ToString();
            e.Row.Cells[3].Text = zonggongtian.ToString();

            con.Close();
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
        Bind((string)Session[vsKey]);
    }
    //beizhu4 是首提日期
   
}