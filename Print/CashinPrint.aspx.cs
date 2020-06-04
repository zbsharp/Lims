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
 

public partial class Print_CashinPrint : System.Web.UI.Page
{

    string shoufeiid ="";

    protected void Page_Load(object sender, EventArgs e)
    {
        shoufeiid = Request.QueryString["shoufeiid"].ToString();

        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con2.Open();

        string sql10 = "select distinct kf.taskid,(select shenqingbianhao from anjianxinxi2 where taskno=kf.taskid) as type,kf.feiyong ,cashin.pinzheng from kf left join cashin on kf.taskid=cashin.taskid where pinzheng='" + shoufeiid + "'";

        SqlDataAdapter ad10 = new SqlDataAdapter(sql10, con2);
        DataSet ds10 = new DataSet();
        ad10.Fill(ds10);
        DataTable dt10 = ds10.Tables[0];
        int qz10 = dt10.Rows.Count;




        string sql12 = "select * from shuipiao where shoufeiid='" + shoufeiid + "'";

        SqlDataAdapter ad12 = new SqlDataAdapter(sql12, con2);
        DataSet ds12 = new DataSet();
        ad12.Fill(ds12);
        DataTable dt12 = ds12.Tables[0];
        int qz12 = dt12.Rows.Count;






        //string sql45 = "select  sum(xiaojine) as xiaojine from cashin2 where daid='" + ds10.Tables[0].Rows[0]["liushuihao"].ToString() + "' and beizhu3='测试部' group by beizhu3";

        //SqlDataAdapter ad45 = new SqlDataAdapter(sql45, con2);
        //DataSet ds45 = new DataSet();
        //ad45.Fill(ds45);
        //DataTable dt45 = ds45.Tables[0];
        //int dt4count5 = dt45.Rows.Count;
        string strTable = "";

        for (int mz1 = 0; mz1 < 2; mz1++)
        {

            string tait = "CCIC-SET内部结算清单(第一联)";
            if (mz1 == 0)
            {
                tait = "CCIC-SET内部结算清单(第一联)";
            }
            else
            {
                tait = "CCIC-SET内部结算清单(第二联)";
            }
            strTable += "<table width=\"97%\" height=30 border=\"0\" align=\"center\"><tr><td align=\"center\" style ='' class=\"F_size17 F_B \">"+tait+"</td></tr></table>";
            strTable += "<br/>";

            strTable += "<table width=\"97%\" height=20 border=\"0\" align=\"center\"><tr><td width=\"25%\">编号/No.：" + Request.QueryString["shoufeiid"].ToString() + "</td><td >付款方：" + ds12.Tables[0].Rows[0]["fukuanren"].ToString() + "</td><td >付款日期：" + ds12.Tables[0].Rows[0]["fukuanriqi"].ToString() + "</td></tr></table>";





            strTable += "<table width=\"97%\" align=\"center\" border=\"1\">";
            strTable += "<tr height=27>";
            strTable += "<td align=\"center\" width=\"2%\"><span style='font-weight :bold ;'>序号</span></td>";

            strTable += "<td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'>任务号</span></td>";
            strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>项目描述</span></td>";
            strTable += "<td align=\"center\" width=\"9%\"><span style='font-weight :bold ;'>金额</span></td>";
            strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>EMC</span></td>";
            strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>安规</span></td>";
            strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>新能源</span></td>";
            strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>校准</span></td>";
            strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>化学</span></td>";
            strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>佛山</span></td>";
            strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>代付</span></td>";
            strTable += "</tr>";

            if (qz10 > 10)
            {


                if (0 == 0)
                {
                    decimal aa = 0;
                    decimal bb = 0;
                    decimal cc = 0;
                    decimal dd = 0;
                    decimal eee = 0;
                    decimal fff = 0;

                    decimal mmm = 0;
                    decimal mmm1 = 0;
                    decimal zzz = 0;

                    for (int z = 0; z < qz10; z++)
                    {

                        int mz = z + 1;
                        strTable += "<tr height=27>";
                        strTable += "<td align=\"center\" width=\"2%\"><span style='font-weight :bold ;'>" + mz + "</span></td>";
                        strTable += "<td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'>" + dt10.Rows[z]["taskid"].ToString() + "</span></td>";
                        strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>" + dt10.Rows[z]["type"].ToString() + "</span></td>";




                        string a = "0.00";
                        string b = "0.00";
                        string c = "0.00";
                        string d = "0.00";
                        string ee = "0.00";
                        string mm = "0.00";
                        string mm1 = "0.00";

                        string sqla = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='EMC射频部' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter ada = new SqlDataAdapter(sqla, con2);
                        DataSet dsa = new DataSet();
                        ada.Fill(dsa);
                        DataTable dta = dsa.Tables[0];


                        string sqlb = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='安规部' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter adb = new SqlDataAdapter(sqlb, con2);
                        DataSet dsb = new DataSet();
                        adb.Fill(dsb);



                        string sqlc = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='新能源部' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter adc = new SqlDataAdapter(sqlc, con2);
                        DataSet dsc = new DataSet();
                        adc.Fill(dsc);




                        string sqld = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='仪器校准部' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter add = new SqlDataAdapter(sqld, con2);
                        DataSet dsd = new DataSet();
                        add.Fill(dsd);



                        string sqlee = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='代付' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter adee = new SqlDataAdapter(sqlee, con2);
                        DataSet dsee = new DataSet();
                        adee.Fill(dsee);


                        string sqlm = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='化学部' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter adm = new SqlDataAdapter(sqlm, con2);
                        DataSet dsm = new DataSet();
                        adm.Fill(dsm);



                        string sqlm1 = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='佛山公司' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter adm1 = new SqlDataAdapter(sqlm1, con2);
                        DataSet dsm1 = new DataSet();
                        adm1.Fill(dsm1);


                        if (dsa.Tables[0].Rows.Count > 0)
                        {
                            a = dsa.Tables[0].Rows[0]["xiaojine"].ToString();
                        }
                        if (dsb.Tables[0].Rows.Count > 0)
                        {
                            b = dsb.Tables[0].Rows[0]["xiaojine"].ToString();
                        }
                        if (dsc.Tables[0].Rows.Count > 0)
                        {
                            c = dsc.Tables[0].Rows[0]["xiaojine"].ToString();
                        }
                        if (dsd.Tables[0].Rows.Count > 0)
                        {
                            d = dsd.Tables[0].Rows[0]["xiaojine"].ToString();
                        }
                        if (dsee.Tables[0].Rows.Count > 0)
                        {
                            ee = dsee.Tables[0].Rows[0]["xiaojine"].ToString();
                        }

                        if (dsm.Tables[0].Rows.Count > 0)
                        {
                            mm = dsm.Tables[0].Rows[0]["xiaojine"].ToString();
                        }

                        if (dsm1.Tables[0].Rows.Count > 0)
                        {
                            mm1 = dsm1.Tables[0].Rows[0]["xiaojine"].ToString();
                        }


                        decimal zz = Convert.ToDecimal(a) + Convert.ToDecimal(b) + Convert.ToDecimal(c) + Convert.ToDecimal(d) + Convert.ToDecimal(ee) + Convert.ToDecimal(mm) + Convert.ToDecimal(mm1);
                        strTable += "<td align=\"center\" width=\"9%\"><span style='font-weight :bold ;'>" + zz + "</span></td>";

                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + a + "</span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + b + "</span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + c + "</span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + d + "</span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + mm + "</span></td>";

                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + mm1 + "</span></td>";

                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + ee + "</span></td>";
                        strTable += "</tr>";

                        aa = aa + Convert.ToDecimal(a);
                        bb = bb + Convert.ToDecimal(b);
                        cc = cc + Convert.ToDecimal(c);
                        dd = dd + Convert.ToDecimal(d);
                        eee = eee + Convert.ToDecimal(ee);

                        mmm = mmm + Convert.ToDecimal(mm);
                        mmm1 = mmm1 + Convert.ToDecimal(mm1);
                        // fff = fff + Convert.ToDecimal(dt10.Rows[z]["feiyong"].ToString());

                        zzz = zzz + zz;
                    }

                    con2.Close();
                   

                    strTable += "<tr height=27>";

                    if (zzz < 0) 
                    {
                        decimal fuz = Math.Abs(zzz);
                        strTable += "<td align=\"center\" colspan=\"3\"><span style='font-weight :bold ;'>合计：负" + ConvertToChinese(fuz.ToString()) + "</span></td>";

                    }
                    else if (zzz > 0)
                    {



                        strTable += "<td align=\"center\" colspan=\"3\"><span style='font-weight :bold ;'>合计：" + ConvertToChinese(zzz.ToString()) + "</span></td>";
                    }
                    else
                    {
                        strTable += "<td align=\"center\" colspan=\"3\"><span style='font-weight :bold ;'>合计：零圆</span></td>";
 
                    }
                    // strTable += "<td align=\"center\" colspan=\"2\"><span style='font-weight :bold ;'>合计：" + zzz + "</span></td>";


                    strTable += "<td align=\"center\" width=\"9%\"><span style='font-weight :bold ;'>" + zzz + "</span></td>";
                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + aa + "</span></td>";
                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + bb + "</span></td>";
                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + cc + "</span></td>";
                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + dd + "</span></td>";
                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + mmm + "</span></td>";

                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + mmm1 + "</span></td>";
                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + eee + "</span></td>";
                    strTable += "</tr>";

                    //koushui hou 

                    
                    double gongshi = 1 + Convert.ToDouble(0.06);
                    double shuizzz = Convert.ToDouble(zzz) / (gongshi);

                    double shuizzz2 = Math.Round(shuizzz, 2);
                  




                    strTable += "<tr height=27>";

                    if (shuizzz2 < 0) 
                    {
                        double d2d = Math.Abs(shuizzz2);
                        strTable += "<td align=\"center\" colspan=\"3\"><span style='font-weight :bold ;'>(扣税后金额)合计：负" + ConvertToChinese(d2d.ToString()) + "</span></td>";


                    }
                    else if (shuizzz2 > 0) 
                    {


                        strTable += "<td align=\"center\" colspan=\"3\"><span style='font-weight :bold ;'>(扣税后金额)合计：" + ConvertToChinese(shuizzz2.ToString()) + "</span></td>";
                    }

                    else
                    {
                        strTable += "<td align=\"center\" colspan=\"3\"><span style='font-weight :bold ;'>(扣税后金额)合计：零圆</span></td>";
 
                    }


                    // strTable += "<td align=\"center\" colspan=\"2\"><span style='font-weight :bold ;'>合计：" + zzz + "</span></td>";


                    strTable += "<td align=\"center\" width=\"9%\"><span style='font-weight :bold ;'>" + shuizzz2.ToString("0.00") + "</span></td>";


                    double shuiaa = Convert.ToDouble(aa) / (gongshi);

                    double shuiaa2 = Math.Round(shuiaa, 2);


                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuiaa2.ToString("0.00") + "</span></td>";


                    double shuibb = Convert.ToDouble(bb) / (gongshi);

                    double shuibb2 = Math.Round(shuibb, 2);

                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuibb2.ToString("0.00") + "</span></td>";


                    double shuicc = Convert.ToDouble(cc) / (gongshi);

                    double shuicc2 = Math.Round(shuicc, 2);


                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuicc2.ToString("0.00") + "</span></td>";

                    double shuidd = Convert.ToDouble(dd) / (gongshi);

                    double shuidd2 = Math.Round(shuidd, 2);



                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuidd2.ToString("0.00") + "</span></td>";

                    double shuimmm = Convert.ToDouble(mmm) / (gongshi);

                    double shuimmm2 = Math.Round(shuimmm, 2);



                    
             


                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuimmm2.ToString("0.00") + "</span></td>";

                    double shuieee = Convert.ToDouble(eee) / (gongshi);

                    double shuieee2 = Math.Round(shuieee, 2);


                    double shuimmmy = Convert.ToDouble(mmm1) / (gongshi);

                    double shuimmm2y = Math.Round(shuimmmy, 2);

                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuimmm2y.ToString("0.00") + "</span></td>";

                    double shuimmm1 = Convert.ToDouble(mmm1) / (gongshi);

                    double shuimmm21 = Math.Round(shuimmm1, 2);



                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuieee2.ToString("0.00") + "</span></td>";
                    strTable += "</tr>";



                    strTable += "</table>";

                    strTable += "<table width=\"80%\" height=20 border=\"0\" align=\"center\"><tr><td width=\"50%\">编制人：" + Session["UserName"].ToString() + "</td><td >结算日期：" + Convert.ToDateTime(ds12.Tables[0].Rows[0]["querenriqi"]).ToShortDateString() + "</td><td>财务确认：</td></tr></table>";
                    strTable += "<br/>";
                    strTable += "<hr width=\"97%\"/>";

                    if (mz1 == 0)
                    {
                        strTable += "<div class=\"PageNext\"></div>";
                    }
                }

                
                con2.Close();

            }
            else
            {
                if (0 == 0)
                {
                    decimal aa = 0;
                    decimal bb = 0;
                    decimal cc = 0;
                    decimal dd = 0;
                    decimal eee = 0;
                    decimal fff = 0;

                    decimal mmm = 0;
                    decimal mmm1 = 0;
                    decimal zzz = 0;

                    for (int z = 0; z < qz10; z++)
                    {

                        int mz = z + 1;
                        strTable += "<tr height=27>";
                        strTable += "<td align=\"center\" width=\"2%\"><span style='font-weight :bold ;'>" + mz + "</span></td>";
                        strTable += "<td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'>" + dt10.Rows[z]["taskid"].ToString() + "</span></td>";
                        strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>" + dt10.Rows[z]["type"].ToString() + "</span></td>";




                        string a = "0.00";
                        string b = "0.00";
                        string c = "0.00";
                        string d = "0.00";
                        string ee = "0.00";
                        string mm = "0.00";
                        string mm1 = "0.00";

                        string sqla = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='EMC射频部' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter ada = new SqlDataAdapter(sqla, con2);
                        DataSet dsa = new DataSet();
                        ada.Fill(dsa);
                        DataTable dta = dsa.Tables[0];


                        string sqlb = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='安规部' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter adb = new SqlDataAdapter(sqlb, con2);
                        DataSet dsb = new DataSet();
                        adb.Fill(dsb);



                        string sqlc = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='新能源部' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter adc = new SqlDataAdapter(sqlc, con2);
                        DataSet dsc = new DataSet();
                        adc.Fill(dsc);




                        string sqld = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='仪器校准部' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter add = new SqlDataAdapter(sqld, con2);
                        DataSet dsd = new DataSet();
                        add.Fill(dsd);



                        string sqlee = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='代付' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter adee = new SqlDataAdapter(sqlee, con2);
                        DataSet dsee = new DataSet();
                        adee.Fill(dsee);


                        string sqlm = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='化学部' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter adm = new SqlDataAdapter(sqlm, con2);
                        DataSet dsm = new DataSet();
                        adm.Fill(dsm);

                        string sqlm1 = "select  sum(jine) as xiaojine from cashin where pinzheng='" + shoufeiid + "' and taskid='" + dt10.Rows[z]["taskid"].ToString() + "' and beizhu3='佛山公司' group by beizhu3,taskid,pinzheng";
                        SqlDataAdapter adm1 = new SqlDataAdapter(sqlm1, con2);
                        DataSet dsm1 = new DataSet();
                        adm1.Fill(dsm1);

                        if (dsa.Tables[0].Rows.Count > 0)
                        {
                            a = dsa.Tables[0].Rows[0]["xiaojine"].ToString();
                        }
                        if (dsb.Tables[0].Rows.Count > 0)
                        {
                            b = dsb.Tables[0].Rows[0]["xiaojine"].ToString();
                        }
                        if (dsc.Tables[0].Rows.Count > 0)
                        {
                            c = dsc.Tables[0].Rows[0]["xiaojine"].ToString();
                        }
                        if (dsd.Tables[0].Rows.Count > 0)
                        {
                            d = dsd.Tables[0].Rows[0]["xiaojine"].ToString();
                        }
                        if (dsee.Tables[0].Rows.Count > 0)
                        {
                            ee = dsee.Tables[0].Rows[0]["xiaojine"].ToString();
                        }

                        if (dsm.Tables[0].Rows.Count > 0)
                        {
                            mm = dsm.Tables[0].Rows[0]["xiaojine"].ToString();
                        }
                        if (dsm1.Tables[0].Rows.Count > 0)
                        {
                            mm1 = dsm1.Tables[0].Rows[0]["xiaojine"].ToString();
                        }


                        decimal zz = Convert.ToDecimal(a) + Convert.ToDecimal(b) + Convert.ToDecimal(c) + Convert.ToDecimal(d) + Convert.ToDecimal(ee) + Convert.ToDecimal(mm) + Convert.ToDecimal(mm1);
                        strTable += "<td align=\"center\" width=\"9%\"><span style='font-weight :bold ;'>" + zz + "</span></td>";

                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + a + "</span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + b + "</span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + c + "</span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + d + "</span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + mm + "</span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + mm1 + "</span></td>";

                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + ee + "</span></td>";
                        strTable += "</tr>";

                        aa = aa + Convert.ToDecimal(a);
                        bb = bb + Convert.ToDecimal(b);
                        cc = cc + Convert.ToDecimal(c);
                        dd = dd + Convert.ToDecimal(d);
                        eee = eee + Convert.ToDecimal(ee);

                        mmm = mmm + Convert.ToDecimal(mm);
                        mmm1 = mmm1 + Convert.ToDecimal(mm1);
                        // fff = fff + Convert.ToDecimal(dt10.Rows[z]["feiyong"].ToString());

                        zzz = zzz + zz;
                    }

                    con2.Close();
                    for (int z = 0; z < 10 - qz10; z++)
                    {
                        strTable += "<tr height=27>";

                        strTable += "<td align=\"center\" width=\"2%\"><span style='font-weight :bold ;'></span></td>";
                        strTable += "<td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'></span></td>";
                        strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'></span></td>";


                        strTable += "<td align=\"center\" width=\"9%\"><span style='font-weight :bold ;'></span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'></span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'></span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'></span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'></span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'></span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'></span></td>";
                        strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'></span></td>";

                        strTable += "</tr>";
                    }

                    strTable += "<tr height=27>";

                   // strTable += "<td align=\"center\" colspan=\"3\"><span style='font-weight :bold ;'>合计：" + ConvertToChinese(zzz.ToString()) + "</span></td>";

                    if (zzz < 0)
                    {
                        decimal fuz = Math.Abs(zzz);
                        strTable += "<td align=\"center\" colspan=\"3\"><span style='font-weight :bold ;'>合计：负" + ConvertToChinese(fuz.ToString()) + "</span></td>";

                    }
                    else if (zzz > 0)
                    {



                        strTable += "<td align=\"center\" colspan=\"3\"><span style='font-weight :bold ;'>合计：" + ConvertToChinese(zzz.ToString()) + "</span></td>";
                    }
                    else
                    {
                        strTable += "<td align=\"center\" colspan=\"3\"><span style='font-weight :bold ;'>合计：零圆</span></td>";
 
                    }

                    strTable += "<td align=\"center\" width=\"9%\"><span style='font-weight :bold ;'>" + zzz + "</span></td>";
                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + aa + "</span></td>";
                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + bb + "</span></td>";
                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + cc + "</span></td>";
                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + dd + "</span></td>";
                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + mmm + "</span></td>";

                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + mmm1 + "</span></td>";
                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + eee + "</span></td>";
                    strTable += "</tr>";

                    //koushui hou 


                    double gongshi = 1 + Convert.ToDouble(0.06);
                    double shuizzz =Convert.ToDouble(zzz) / (gongshi);

                    double shuizzz2 = Math.Round(shuizzz, 2);
                    strTable += "<tr height=27>";

                    if (shuizzz2 < 0)
                    {
                        double d2d = Math.Abs(shuizzz2);
                        strTable += "<td align=\"center\" colspan=\"3\"><span style='font-weight :bold ;'>(扣税后金额)合计：负" + ConvertToChinese(d2d.ToString()) + "</span></td>";


                    }
                    else if (shuizzz2 > 0)
                    {


                        strTable += "<td align=\"center\" colspan=\"3\"><span style='font-weight :bold ;'>(扣税后金额)合计：" + ConvertToChinese(shuizzz2.ToString()) + "</span></td>";
                    }
                    else
                    {
                        strTable += "<td align=\"center\" colspan=\"3\"><span style='font-weight :bold ;'>(扣税后金额)合计：零圆</span></td>";
 
                    }

                    strTable += "<td align=\"center\" width=\"9%\"><span style='font-weight :bold ;'>" + shuizzz2.ToString("0.00") + "</span></td>";


                    double shuiaa =Convert.ToDouble( aa) / (gongshi);

                    double shuiaa2 = Math.Round(shuiaa, 2);


                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuiaa2.ToString("0.00") + "</span></td>";


                    double shuibb =Convert.ToDouble( bb) / (gongshi);

                    double shuibb2 = Math.Round(shuibb, 2);

                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuibb2.ToString("0.00") + "</span></td>";


                    double shuicc = Convert.ToDouble(cc) / (gongshi);

                    double shuicc2 = Math.Round(shuicc, 2);


                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuicc2.ToString("0.00") + "</span></td>";

                    double shuidd = Convert.ToDouble(dd) / (gongshi);

                    double shuidd2 = Math.Round(shuidd, 2);

                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuidd2.ToString("0.00") + "</span></td>";

                    double shuimmm =Convert.ToDouble( mmm) / (gongshi);

                    double shuimmm2 = Math.Round(shuimmm, 2);





                  



                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuimmm2.ToString("0.00") + "</span></td>";

                    double shuieee =Convert.ToDouble(eee) / (gongshi);

                    double shuieee2 = Math.Round(shuieee, 2);


                    double shuimmmy = Convert.ToDouble(mmm1) / (gongshi);

                    double shuimmm2y = Math.Round(shuimmmy, 2);

                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuimmm2y.ToString("0.00") + "</span></td>";

                    double shuimmm1 = Convert.ToDouble(mmm1) / (gongshi);

                    double shuimmm21 = Math.Round(shuimmm1, 2);


                 



                    strTable += "<td align=\"center\" width=\"8%\"><span style='font-weight :bold ;'>" + shuieee2.ToString("0.00") + "</span></td>";
                    strTable += "</tr>";



                    strTable += "</table>";

                    strTable += "<table width=\"80%\" height=20 border=\"0\" align=\"center\"><tr><td width=\"50%\">编制人：" + Session["UserName"].ToString() + "</td><td >结算日期：" + Convert.ToDateTime(ds12.Tables[0].Rows[0]["querenriqi"]).ToShortDateString() + "</td><td>财务确认：</td></tr></table>";
                    strTable += "<br/>";
                    strTable += "<hr width=\"97%\"/>";
                }



                con2.Close();
                
            }
            con2.Close();
        }
        con2.Close();
        lblTable.Text = strTable;
    }



    public string ConvertToChinese(string stringNumber)
    {
        string[] Price = { "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟" };
        string[] PriceDot = { "角", "分", "厘" };
        string[] Number = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
        string MoneyPrice = stringNumber.ToString();
        string MoneyPriceDot = string.Empty;
        if (stringNumber.IndexOf(".") > 1)
        {
            MoneyPrice = stringNumber.Split('.')[0];
            MoneyPriceDot = stringNumber.Split('.')[1];
        }
        string part1 = string.Empty;
        string part2 = string.Empty;
        for (int i = 0; i < MoneyPrice.Length; i++)
        {
            int numberIndex = Convert.ToInt32(MoneyPrice[i].ToString());
            part1 += Number[numberIndex];
            part1 += Price[MoneyPrice.Length - i - 1];
        }
        if (MoneyPriceDot.Length > 0)
        {
            if (Convert.ToInt32(MoneyPriceDot) > 0)
            {
                for (int i = 0; i < MoneyPriceDot.Length-1; i++)
                {
                    int numberIndex = Convert.ToInt32(MoneyPriceDot[i].ToString());
                    part2 += Number[numberIndex];
                    part2 += PriceDot[i];
                }
            }
        }
        part1 = part1.Replace("零仟", "零");
        part1 = part1.Replace("零佰", "零");
        part1 = part1.Replace("零拾", "零");
        part1 = part1.Replace("零元", "元");
        part1 = part1.Replace("零零零万", "");
        part1 = part1.Replace("零零零", "零");
        part1 = part1.Replace("零零", "零");
        part1 = part1.Replace("零万", "万");
        part1 = part1.Replace("零亿", "亿");
        part2 = part2.Replace("零角", "零");
        part2 = part2.Replace("零分", "零");
        part2 = part2.Replace("零厘", "");
        part2 = part2.Replace("零零", "零");
        return part1 + part2;
    }
  
}