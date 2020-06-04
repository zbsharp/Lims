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


public partial class TongJi_XiaoShouYeJi : System.Web.UI.Page
{
    private int _i = 0;
    const string basesql = "select taskid,pinzheng,SUM(xiaojine) as zonghesuan,anjianinfo2.name,(select fukuanren from shuipiao where liushuihao=cashin2.daid)"
        + " as shijifukuanren,(select responser from BaoJiaBiao where baojiaid=anjianinfo2.baojiaid) as yewuyuan,(select departmentname from Userinfo where username=(select responser from BaoJiaBiao where baojiaid=anjianinfo2.baojiaid))"
        + " as baojiabumen,tichenriqi,(select customname from Customer where kehuid=cashin2.kehuid) as kehuming from Cashin2 left join AnJianInFo2 on cashin2.taskid=anjianinfo2.rwbianhao ";
    const string groupbysql=" group by taskid,pinzheng,anjianinfo2.name,cashin2.daid,anjianinfo2.baojiaid,tichenriqi,cashin2.kehuid";
    DataTable DepTabele;
    string userRole = "";
    void Page_Load(object sender, EventArgs e)
    {
        

        if (!IsPostBack)
        {
            DepTabele = SetProjectDep();

            userRole = getUserRole(this.Session["UserName"].ToString());
            if(GridView1.Columns.Count<=9)
            {
                for (int i = 0; i < DepTabele.Rows.Count; i++)
                {
                    BoundField departdata = new BoundField();
                    departdata.HeaderText = DepTabele.Rows[i][0].ToString();
                    departdata.DataField = "";
                    GridView1.Columns.Add(departdata);
                }
            }

            DateTime dt = DateTime.Now;
            int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            int dayspan = (-1) * weeknow + 1;
            DateTime dt2 = dt.AddMonths(1);
            //本月第一天
            txFDate.Value = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");

            DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).AddHours(23);

            txTDate.Value = lastDay.ToString("yyyy-MM-dd");
            BindDep();
            BindUserName();
            TimeBind();
            
            GridView1.ShowFooter = false;
        }
    }

    protected DataTable SetProjectDep()
    {
        DataTable reslult = new DataTable();
        reslult.Columns.Add(new DataColumn("DepName"));
        DataRow R_row = reslult.NewRow();
        R_row[0] = "电池部";
        reslult.Rows.Add(R_row);
        R_row = reslult.NewRow();
        R_row[0] = "安规部";
        reslult.Rows.Add(R_row);
        R_row = reslult.NewRow();
        R_row[0] = "化学部";
        reslult.Rows.Add(R_row);
        R_row = reslult.NewRow();
        R_row[0] = "EMC部";
        reslult.Rows.Add(R_row);
        R_row = reslult.NewRow();
        R_row[0] = "龙华EMC部";
        reslult.Rows.Add(R_row);
        R_row = reslult.NewRow();
        R_row[0] = "认证部";
        reslult.Rows.Add(R_row);
        R_row = reslult.NewRow();
        R_row[0] = "代付";
        reslult.Rows.Add(R_row);
        return reslult;
    }

    protected string getUserRole(string username)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select dutyname from userinfo where username='" + username + "'";
        string result = "";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader ad = cmd.ExecuteReader();
        if (ad.Read())
        {
            result = ad["dutyname"].ToString();
        }
        ad.Close();
        con.Close();
        return result;
    }

  
    protected void Button2_Click(object sender, EventArgs e)
    {
        string sqlstr;
        sqlstr = basesql;
        sqlstr += " where pinzheng!='' and convert(datetime,tichenriqi) between '" + Convert.ToDateTime(this.txFDate.Value) + "' and '" + Convert.ToDateTime(this.txTDate.Value).AddHours(23) + "' and tichenbiaozhi='是'";
        if (this.DropDownList2.SelectedItem.Value != "")
        {
            sqlstr += (" and baojiaid in(select baojiaid from baojiabiao where responser='" + this.DropDownList2.SelectedValue + "')");
        }
        else
        {
            if (this.DropDownList1.SelectedItem.Value != "")
            {
                sqlstr += (" and baojiaid in(select baojiaid from baojiabiao where responser in(select username from userinfo where departmentname='" + this.DropDownList1.SelectedValue + "'))");
            }
        }


        sqlstr += groupbysql;



        //AspNetPager1.Visible = false;
        //GridView1.ShowFooter = true;
        //string str = "select distinct kf.taskid,kf.feiyong ,cashin.pinzheng from kf left join cashin on kf.taskid=cashin.taskid where pinzheng is not null";

        
        //{

        //    string wh = "pinzheng in (select shoufeiid from shuipiao where querenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "')";

        //    string whbumen = "kf. taskid in (select rwbianhao from anjianinfo2 where   ((responser='" + Session["UserName"].ToString() + "') or (responser in (select name2 from PersonConfig where name1='" + Session["UserName"].ToString() + "'))) and  baojiaid in (select baojiaid from baojiabiao where responser in (select username from userinfo where departmentname='" + DropDownList1.SelectedValue + "')))";

        //    string whren = "kf. taskid in (select rwbianhao from anjianinfo2 where baojiaid in (select baojiaid from baojiabiao where   ((responser='" + Session["UserName"].ToString() + "') or (responser in (select name2 from PersonConfig where name1='" + Session["UserName"].ToString() + "'))) and   responser in (select username from userinfo where username='" + DropDownList2.SelectedValue + "')))";

        //    if (DropDownList1.SelectedValue == "")
        //    {
        //        sqlstr = str + " and " + wh;
        //    }
        //    else if (DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue == "")
        //    {
        //        sqlstr = str + " and  " + whbumen + "  and " + wh;

        //    }
        //    else if (DropDownList1.SelectedValue != "" && DropDownList2.SelectedValue != "")
        //    {
        //        sqlstr = str + " and  " + whren + "  and " + wh;

        //    }
        //    else
        //    {
        //        sqlstr = str + " and " + wh;
        //    }
        //}
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


    protected void BindDep()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from UserDepa ";
        string Rolename = "";
        string loginuserdepsql = "select dutyname from userinfo where username='" + this.Session["UserName"].ToString() + "'";
        SqlCommand logoinuserdepcmd = new SqlCommand(loginuserdepsql, con);
        SqlDataReader loginuserdepad = logoinuserdepcmd.ExecuteReader();
        if (loginuserdepad.Read())
        {
            Rolename = loginuserdepad["dutyname"].ToString();
        }
        loginuserdepad.Close();
        if (Rolename == "系统管理员" || Rolename == "财务经理" || Rolename == "总经理")
        {
        }
        else
        {
            sql += " where name in(select departmentname from userinfo where username='" + this.Session["UserName"].ToString() + "')";
        }
        sql += " order by departmentid asc";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con);


        DataSet ds = new DataSet();


        ad.Fill(ds);


        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataValueField = "name";
        DropDownList1.DataTextField = "name";
        DropDownList1.DataBind();

        if (Rolename == "系统管理员" || Rolename == "财务经理" || Rolename == "总经理")
        {

            DropDownList1.Items.Insert(0, new ListItem("", ""));
        }

        con.Close();
    }
  

    protected void TimeBind()
    {
        string sqlstr;
        sqlstr = basesql;
        sqlstr += " where pinzheng!='' and convert(datetime,tichenriqi) between '" + Convert.ToDateTime(this.txFDate.Value) + "' and '" + Convert.ToDateTime(this.txTDate.Value).AddHours(23) + "' and tichenbiaozhi='是'";
        if (this.DropDownList2.SelectedItem.Value != "")
        {
            sqlstr += (" and baojiaid in(select baojiaid from baojiabiao where responser='" + this.DropDownList2.SelectedValue+ "')");
        }
        else
        {
            if (this.DropDownList1.SelectedItem.Value != "")
            {
                sqlstr += (" and baojiaid in(select baojiaid from baojiabiao where responser in(select username from userinfo where departmentname='" + this.DropDownList1.SelectedValue + "'))");
            }
        }


        sqlstr += groupbysql;

        //string whren = "kf. taskid in (select rwbianhao from anjianinfo2 where baojiaid in (select baojiaid from baojiabiao where responser in (select username from userinfo where username='" + DropDownList2.SelectedValue + "')))";


        //string str = "";
        //if (Session["role"].ToString() == "5")
        //{
        //    str = "select distinct kf.taskid,kf.feiyong ,cashin.pinzheng from kf left join cashin on kf.taskid=cashin.taskid where pinzheng is not null";
        //    whren = "kf. taskid in (select rwbianhao from anjianinfo2 where baojiaid in (select baojiaid from baojiabiao where responser ='"+Session["UserName"].ToString()+"'))";

        //}
        //else
        //{
        //     str = "select distinct kf.taskid,kf.feiyong ,cashin.pinzheng from kf left join cashin on kf.taskid=cashin.taskid where pinzheng is not null";
        //     whren = "1=1";
 
        //}



        //string wh = "pinzheng in (select shoufeiid from shuipiao where querenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "')";


        //sqlstr = str + " and " + wh + " and " + whren;

        //sqlstr = basesql + groupbysql;

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

    private decimal sum1 = 0;
    private decimal sum2 = 0;
    private decimal sum3 = 0;
    private decimal sum4 = 0;
    private decimal sum5 = 0;
    private decimal sum6 = 0;
    private decimal sum7 = 0;
    private decimal sum8 = 0;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string pinzhen = e.Row.Cells[8].Text;
            string taskid = e.Row.Cells[2].Text;
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 8);
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 8);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            decimal jiesuan = 0m;
            for (int i = 9; i < GridView1.Columns.Count; i++)
            {
                string getmoneysql = "select sum(xiaojine) as jine from cashin2 where tichenbiaozhi='是' and taskid='" + taskid + "' and pinzheng='" + pinzhen +
                    "' and beizhu3='" + GridView1.Columns[i].HeaderText + "'";
                decimal money = 0m;
                bool havemoeny = false;
                SqlCommand getmoneycmd = new SqlCommand(getmoneysql, con);
                SqlDataReader getmoneyad = getmoneycmd.ExecuteReader();
                if (getmoneyad.Read())
                {
                    try
                    {
                        money = Convert.ToDecimal(getmoneyad["jine"].ToString());
                    }
                    catch
                    {
                        money = 0m;
                    }
                    havemoeny = true;
                }
                getmoneyad.Close();
                if (havemoeny)
                {
                    e.Row.Cells[i].Text = money.ToString();
                }
                else
                {
                    e.Row.Cells[i].Text = "0.00";
                }
                if (GridView1.Columns[i].HeaderText != "代付")
                {
                    jiesuan += Convert.ToDecimal(e.Row.Cells[i].Text);
                }
            }
            e.Row.Cells[6].Text = jiesuan.ToString();

        }


        //return;
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{



        //    string pinzhen = e.Row.Cells[8].Text;
        //    string taskid = e.Row.Cells[2].Text;
        //    string kehuid = "";
        //    string baojiaid = "";
        //    string responser = "";
        //    string bumen = "";
        //    e.Row.Attributes.Add("id", _i.ToString());
        //    e.Row.Attributes.Add("onKeyDown", "SelectRow();");
        //    e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
        //    _i++;

        //    MyExcutSql my = new MyExcutSql();
        //    string sql1 = "select baojiaid,chanpinname,shiyanleibie,weituodanwei from anjianinfo2 where rwbianhao='" + taskid + "'";
        //    baojiaid = my.ExcutSql2(sql1, 0);

        //    string sql2 = "select responser,kehuid from baojiabiao where baojiaid='" + baojiaid + "'";
        //    responser = my.ExcutSql2(sql2, 0);
        //    kehuid = my.ExcutSql2(sql2, 1);

        //    string sql3 = "select departmentname from userinfo where username='" + responser + "'";
        //    bumen = my.ExcutSql2(sql3, 0);


     


        //    string sql4 = "select customname from customer where kehuid='" + kehuid + "'";

        //    string sql5 = "select querenriqi,fukuanren from shuipiao where shoufeiid='" + pinzhen + "'";
        //    string jiesuanriqi = my.ExcutSql2(sql5, 0);


        //    e.Row.Cells[3].Text = my.ExcutSql2(sql4, 0);
        //    e.Row.Cells[0].Text = bumen;
        //    e.Row.Cells[1].Text = responser;
        //    e.Row.Cells[4].Text = my.ExcutSql2(sql1, 1);
        //    e.Row.Cells[5].Text = my.ExcutSql2(sql1, 2);
        //    e.Row.Cells[15].Text = my.ExcutSql2(sql5, 1);
          
        //    e.Row.Cells[7].Text = jiesuanriqi == "no" ? "" : Convert.ToDateTime(jiesuanriqi).ToShortDateString();

        //    string sqlzong = "select  sum(jine) as xiaojine from cashin where pinzheng='" + pinzhen + "' and taskid='" + taskid + "' and ";
        //    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //    con2.Open();
        //    string khriqi = "";
        //    string sqlkh = "select xiadariqi from anjianinfo2 where kehuid='"+kehuid+"' order by id asc";
        //    SqlCommand cmdkh = new SqlCommand(sqlkh,con2);
        //    SqlDataReader drkh = cmdkh.ExecuteReader();
        //    if (drkh.Read())
        //    {
        //        khriqi = drkh["xiadariqi"].ToString();
        //    }
        //    drkh.Close();
        //    e.Row.Cells[16].Text = khriqi;


        //    string a = "0.00";
        //    string b = "0.00";
        //    string c = "0.00";
        //    string d = "0.00";
        //    string ee = "0.00";
        //    string mm = "0.00";

        //    string mmu = "0.00";
        //    string mmd1 = "0.00";
        //    string sqla = sqlzong + "beizhu3='EMC射频部' group by beizhu3,taskid,pinzheng";
        //    SqlDataAdapter ada = new SqlDataAdapter(sqla, con2);
        //    DataSet dsa = new DataSet();
        //    ada.Fill(dsa);
        //    DataTable dta = dsa.Tables[0];
        //    if (dsa.Tables[0].Rows.Count > 0)
        //    {
        //        a = dsa.Tables[0].Rows[0]["xiaojine"].ToString();
        //        e.Row.Cells[9].Text = a.ToString();
        //    }



        //    string sqlb = sqlzong + " beizhu3='安规部' group by beizhu3,taskid,pinzheng";
        //    SqlDataAdapter adb = new SqlDataAdapter(sqlb, con2);
        //    DataSet dsb = new DataSet();
        //    adb.Fill(dsb);
        //    if (dsb.Tables[0].Rows.Count > 0)
        //    {
        //        b = dsb.Tables[0].Rows[0]["xiaojine"].ToString();
        //        e.Row.Cells[10].Text = b.ToString();
        //    }



        //    string sqlc = sqlzong + " beizhu3='新能源部' group by beizhu3,taskid,pinzheng";
        //    SqlDataAdapter adc = new SqlDataAdapter(sqlc, con2);
        //    DataSet dsc = new DataSet();
        //    adc.Fill(dsc);
        //    if (dsc.Tables[0].Rows.Count > 0)
        //    {
        //        c = dsc.Tables[0].Rows[0]["xiaojine"].ToString();
        //        e.Row.Cells[11].Text = c.ToString();
        //    }


        //    string sqld = sqlzong + " beizhu3='仪器校准部' group by beizhu3,taskid,pinzheng";
        //    SqlDataAdapter add = new SqlDataAdapter(sqld, con2);
        //    DataSet dsd = new DataSet();
        //    add.Fill(dsd);
        //    if (dsd.Tables[0].Rows.Count > 0)
        //    {
        //        d = dsd.Tables[0].Rows[0]["xiaojine"].ToString();
        //        e.Row.Cells[12].Text = d.ToString();
        //    }


        //    string sqlm = sqlzong + " beizhu3='化学部' group by beizhu3,taskid,pinzheng";
        //    SqlDataAdapter adm = new SqlDataAdapter(sqlm, con2);
        //    DataSet dsm = new DataSet();
        //    adm.Fill(dsm);
        //    if (dsm.Tables[0].Rows.Count > 0)
        //    {
        //        mm = dsm.Tables[0].Rows[0]["xiaojine"].ToString();
        //        e.Row.Cells[13].Text = mm.ToString();
        //    }




        //    string sqlmu = sqlzong + " beizhu3='佛山公司' group by beizhu3,taskid,pinzheng";
        //    SqlDataAdapter admu = new SqlDataAdapter(sqlmu, con2);
        //    DataSet dsmu = new DataSet();
        //    admu.Fill(dsmu);
        //    if (dsmu.Tables[0].Rows.Count > 0)
        //    {
        //        mmu = dsmu.Tables[0].Rows[0]["xiaojine"].ToString();
        //        e.Row.Cells[17].Text = mmu.ToString();
        //    }




        //    string sqld1 = sqlzong + " beizhu3='代付' group by beizhu3,taskid,pinzheng";
        //    SqlDataAdapter add1 = new SqlDataAdapter(sqld1, con2);
        //    DataSet dsd1 = new DataSet();
        //    add1.Fill(dsd1);
        //    if (dsd1.Tables[0].Rows.Count > 0)
        //    {
        //        mmd1 = dsd1.Tables[0].Rows[0]["xiaojine"].ToString();
        //        e.Row.Cells[14].Text = mmd1.ToString();
        //    }


        //    con2.Close();
        //    decimal zz = Convert.ToDecimal(a) + Convert.ToDecimal(b) + Convert.ToDecimal(c) + Convert.ToDecimal(d) + Convert.ToDecimal(ee) + Convert.ToDecimal(mm) + Convert.ToDecimal(mmu);

        //    if (zz > 0)
        //    {
        //        e.Row.Cells[6].Text = zz.ToString();
        //    }

        //    e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 10);
        //    e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 10);


           
        //}

        //if (e.Row.RowIndex >= 0)
        //{

        //    string qq = e.Row.Cells[9].Text;
        //    if (e.Row.Cells[9].Text == "" || e.Row.Cells[9].Text == "&nbsp;")
        //    {
        //        e.Row.Cells[9].Text = "0";
        //    }

        //    if (e.Row.Cells[10].Text == "" || e.Row.Cells[10].Text == "&nbsp;")
        //    {
        //        e.Row.Cells[10].Text = "0";
        //    }
        //    if (e.Row.Cells[11].Text == "" || e.Row.Cells[11].Text == "&nbsp;")
        //    {
        //        e.Row.Cells[11].Text = "0";
        //    }
        //    if (e.Row.Cells[12].Text == "" || e.Row.Cells[12].Text == "&nbsp;")
        //    {
        //        e.Row.Cells[12].Text = "0";
        //    }
        //    if (e.Row.Cells[13].Text == "" || e.Row.Cells[13].Text == "&nbsp;")
        //    {
        //        e.Row.Cells[13].Text = "0";
        //    }
        //    if (e.Row.Cells[14].Text == "" || e.Row.Cells[14].Text == "&nbsp;")
        //    {
        //        e.Row.Cells[14].Text = "0";
        //    }

        //    if (e.Row.Cells[17].Text == "" || e.Row.Cells[17].Text == "&nbsp;")
        //    {
        //        e.Row.Cells[17].Text = "0";
        //    }

        //    if (e.Row.Cells[6].Text == "" || e.Row.Cells[6].Text == "&nbsp;")
        //    {
        //        e.Row.Cells[6].Text = "0";
        //    }

        //    sum1 += Convert.ToDecimal(e.Row.Cells[9].Text);
        //    sum2 += Convert.ToDecimal(e.Row.Cells[10].Text);
        //    sum3 += Convert.ToDecimal(e.Row.Cells[11].Text);
        //    sum4 += Convert.ToDecimal(e.Row.Cells[12].Text);
        //    sum5 += Convert.ToDecimal(e.Row.Cells[13].Text);
        //    sum6 += Convert.ToDecimal(e.Row.Cells[14].Text);
        //    sum7 += Convert.ToDecimal(e.Row.Cells[6].Text);
        //    sum8 += Convert.ToDecimal(e.Row.Cells[17].Text);

        //}

        //else if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[5].Text = "合计：";
        //    e.Row.Cells[6].Text = sum7.ToString();
        //    e.Row.Cells[9].Text = sum1.ToString();
        //    e.Row.Cells[10].Text = sum2.ToString();
        //    e.Row.Cells[11].Text = sum3.ToString();
        //    e.Row.Cells[12].Text = sum4.ToString();
        //    e.Row.Cells[13].Text = sum5.ToString();
        //    e.Row.Cells[14].Text = sum6.ToString();
        //    e.Row.Cells[17].Text = sum8.ToString();
        //}

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
        BindUserName();
    }

    protected void BindUserName()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string Rolename = "";
        string loginuserdepsql = "select dutyname from userinfo where username='" + this.Session["UserName"].ToString() + "'";
        SqlCommand logoinuserdepcmd = new SqlCommand(loginuserdepsql, con);
        SqlDataReader loginuserdepad = logoinuserdepcmd.ExecuteReader();
        if (loginuserdepad.Read())
        {
            Rolename = loginuserdepad["dutyname"].ToString();
        }
        loginuserdepad.Close();


        string sql = "select * from userinfo where department='" + DropDownList1.SelectedValue + "' order by username asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataTextField = "username";
        DropDownList2.DataValueField = "username";
        DropDownList2.DataBind();
        if (Rolename == "系统管理员" || Rolename == "财务经理" || Rolename == "总经理")
        {
            DropDownList2.Items.Insert(0, new ListItem("", ""));//
        }
        else
        {
            
            this.DropDownList2.Items.Insert(0, new ListItem(Session["UserName"].ToString(), Session["UserName"].ToString()));
            this.DropDownList2.SelectedValue = this.Session["UserName"].ToString();
            this.DropDownList2.Enabled = false;
        }

        con.Close();
    }
}