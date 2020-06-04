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
 
public partial class CCSZJiaoZhun_PDF_DrawPrint5 : System.Web.UI.Page
{
    protected DateTime time1 = DateTime.Now;
    protected DateTime time2 = DateTime.Now;
    protected decimal changjianjine = 0;
    protected decimal xiachangjianjine = 0;

    protected decimal zaichangshijian = 0;
    protected decimal lvtushijian = 0;
    protected decimal buzhujine = 0;
    protected int changshu1 = 0;
    protected int changshu2 = 0;
    protected int changshu3 = 0;

    protected decimal zongjine = 0;
    protected string name = "";

    protected decimal totalstandard = 0;
    protected decimal shenqingfee = 0;
    protected decimal zhucefee = 0;
    protected decimal guanlifee = 0;

    protected decimal biaozhifee = 0;
    protected decimal qitafee = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        string baojiaid = Request.QueryString["baojiaid"].ToString();
        string customerid = "";
        //      页眉页脚的一些参数用法
        //窗口标题 &w
        //网页地址 (URL) &u
        //短日期格式（由"控制面板"中的"区域设置"指定） &d
        //长日期格式（由"控制面板"中的"区域设置"指定） &D
        //当前页码 &p
        //总页数 &P
        //文本右对齐（后跟 &b） &b
        //文字居中（&b&b 之间） &b&b




        if (!string.IsNullOrEmpty(Request.QueryString["baojiaid"]))
        {



            string strTable = "";


            SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
            con2.Open();
            string sql2 = "select  * from instrument where baojiaid='" + Request.QueryString["baojiaid"].ToString() + "' order by id";

            SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con2);
            DataSet ds2 = new DataSet();
            ad2.Fill(ds2);
            DataTable dt2 = ds2.Tables[0];
            int dt2count = dt2.Rows.Count;




            string sql3 = "select  * from Quatation where baojiaid='" + Request.QueryString["baojiaid"].ToString() + "' order by id";

            SqlDataAdapter ad3 = new SqlDataAdapter(sql3, con2);
            DataSet ds3 = new DataSet();
            ad3.Fill(ds3);
            DataTable dt3 = ds3.Tables[0];
            string zhekou = dt3.Rows[0]["zhefangshi"].ToString();

            string cnas = dt3.Rows[0]["dipatchname"].ToString();
            customerid = dt3.Rows[0]["customerid"].ToString();

            string sql4 = "select  * from quoterequires where baojiaid='" + Request.QueryString["baojiaid"].ToString() + "' order by id";

            SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con2);
            DataSet ds4 = new DataSet();
            ad4.Fill(ds4);
            DataTable dt4 = ds4.Tables[0];
            int dt4count = dt4.Rows.Count;


            string sql5 = "select * from JZCustomInformation where customerid='" + customerid + "'";

            SqlDataAdapter ad5 = new SqlDataAdapter(sql4, con2);
            DataSet ds5 = new DataSet();
            ad5.Fill(ds5);
            DataTable dt5 = ds5.Tables[0];

            string sql6 = "select * from LinkMan where customerid='" + customerid + "' order by  id desc";

            SqlDataAdapter ad6 = new SqlDataAdapter(sql6, con2);
            DataSet ds6 = new DataSet();
            ad6.Fill(ds6);
            DataTable dt6 = ds6.Tables[0];

            int aaa = dt6.Rows.Count;
            if (aaa == 0)
            {
                Response.Write("<script>alert('你没有增加该客户的联系人!请增加');window.location.href='../Customer/CustomerManage.aspx'</script>");
            }


            string sql7 = "select * from userinfo where username='" + Session["UserName"].ToString() + "'";

            SqlDataAdapter ad7 = new SqlDataAdapter(sql7, con2);
            DataSet ds7 = new DataSet();
            ad7.Fill(ds7);
            DataTable dt7 = ds7.Tables[0];


            con2.Close();




            strTable += "<table width=\"97%\"  border=\"0\" align=\"center\"><tr><td  align=\"left\"><img src='../img/002.jpg' /></td><td align=\"center\" class=\"F_size16  \">中国检验认证集团深圳有限公司校准测量中心</br><span class=\"F_size19\">CHINA CERTIFICATION & INSPECTION GROUP SHENZHEN CO.,LTD</span></td></tr></table>";
            strTable += "<br/>";
            strTable += "<table width=\"97%\" height=30 border=\"0\" align=\"center\"><tr><td align=\"center\" style ='' class=\"F_size17 F_B \">仪器校准合同</td></tr></table>";
            //strTable += "<table width=\"97%\" height=30 border=\"0\" align=\"center\"><tr><td wids1h=\"43%\">&nbsp;</td><td  class=\"F_size16 F_B \"></td><td  align=\"right\">Quo No：" + Request.QueryString["baojiaid"].ToString() + "</td></tr></table>";
            strTable += "<br/>";

            strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=27><td align=\"left\"><span style='font-weight :bold ;'>报价单号</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + Request.QueryString["baojiaid"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>日期</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + DateTime.Now.ToShortDateString() + "</span></td></tr>";
            strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>客户名称</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt3.Rows[0]["kehuname"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>发件</span></td><td align=\"left\"><span style='font-weight :bold ;'>CCIC 校准测量中心</span></td></tr>";
            if (aaa != 0)
            {
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>联系人</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["name"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>报价</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt3.Rows[0]["lururen"].ToString() + "</span></td></tr>";


                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>电话</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["telephone"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>电话</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt7.Rows[0]["banggongdianhua"].ToString() + "</span></td></tr>";
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>传真</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["fax"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>传真</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt7.Rows[0]["fax"].ToString() + "</span></td></tr>";



            }
            else
            {
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>联系人</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>报价</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td></tr>";

                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>电话</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>电话</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt7.Rows[0]["banggongdianhua"].ToString() + "</span></td></tr>";
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>传真</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>传真</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt7.Rows[0]["fax"].ToString() + "</span></td></tr>";

            }




            strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>地址</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt3.Rows[0]["jiaozhundidian"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>地址</span></td><td align=\"left\"><span style='font-weight :bold ;'>深圳市南山区科苑北路银河风云大厦一，二楼</span></td></tr>";


            strTable += "</table>";
            strTable += "<br/>";

            if (cnas != "是"||cnas=="")
            {

                strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=27><td align=\"center\" width=\"5%\"><span style='font-weight :bold ;'>序号</span></td><td align=\"center\" width=\"25%\"><span style='font-weight :bold ;'>仪器名称</span></td><td align=\"center\" width=\"15%\"><span style='font-weight :bold ;'>型号/规格</span></td><td align=\"center\" width=\"5%\"><span style='font-weight :bold ;'>数量</span></td><td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>单价</span></td><td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>小计</span></td><td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>方式</span></td><td align=\"center\"><span style='font-weight :bold ;'>备注</span></td></tr>";

                for (int z = 0; z < dt2count; z++)
                {


                    string lowdis = dt2.Rows[z]["lowdiscount"].ToString();
                    if (lowdis == "1.00")
                    {
                        lowdis = "不能";
                    }
                    else
                    {
                        lowdis = "能";
                    }

                    if (dt2.Rows[z]["checkmodel"].ToString() != "代送")
                    {


                        strTable += "<tr height=27><td align=\"center\" width=\"5%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["num"].ToString() + "</span></td><td align=\"center\" ><span style='font-weight :bold ;'>" + dt2.Rows[z]["name"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["model"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["count"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["confirmprice"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["totalconfirmprice"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["checkmodel"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["beizhu1"].ToString() + "</span></td></tr>";
                    }
                    else
                    {

                        string dss = dt2.Rows[z]["daisongjine"].ToString();
                        if (dss == "0.00")
                        {
                            dss = dt2.Rows[z]["confirmprice"].ToString();
                        }
                        strTable += "<tr height=27><td align=\"center\" width=\"5%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["num"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["name"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["model"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["count"].ToString() + "</span></td> <td align=\"center\"><span style='font-weight :bold ;'>" + dss + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["totalconfirmprice"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["checkmodel"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["beizhu1"].ToString() + "</span></td></tr>";

                    }


                }
                strTable += "<tr height=27><td align=\"center\" ><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'>小计:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt3.Rows[0]["totalconfirmprice"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";
                string qqqqq = dt3.Rows[0]["youhuibili"].ToString();
                if (zhekou == "是" && dt3.Rows[0]["youhuibili"].ToString()!="100")
                {

                    strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>折扣:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt3.Rows[0]["youhuibili"].ToString() + "%</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";
                    strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>优惠:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt3.Rows[0]["youhuijine"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";


                    decimal traffic = Convert.ToDecimal(dt3.Rows[0]["traffic"]);
                    if (traffic != 0)
                    {
                        strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>交通费:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + traffic + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";
                        decimal zong = traffic + Convert.ToDecimal(dt3.Rows[0]["youhuijine"]);

                        strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>共计收费:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" +Convert.ToInt32(zong) + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";

                    }
                    else
                    {
                        strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>共计收费:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + Convert.ToInt32(dt3.Rows[0]["youhuijine"]) + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";

                    }



                }
                else if (zhekou == "否") 
                {
                    decimal traffic = Convert.ToDecimal(dt3.Rows[0]["traffic"]);
                    if (traffic != 0)
                    {
                        strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>交通费:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + traffic + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";
                        decimal zong = traffic + Convert.ToDecimal(dt3.Rows[0]["youhuijine"]);

                        strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>共计收费:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + Convert.ToInt32(zong) + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";

                    }
                    else
                    {
                        strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>共计收费:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + Convert.ToInt32(dt3.Rows[0]["totalconfirmprice"])+ "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";

                    }
                }

            }
            else
            {
                strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=27><td align=\"center\" width=\"5%\"><span style='font-weight :bold ;'>序号</span></td><td align=\"center\" width=\"25%\"><span style='font-weight :bold ;'>仪器名称</span></td><td align=\"center\" width=\"15%\"><span style='font-weight :bold ;'>型号/规格</span></td><td align=\"center\" width=\"5%\"><span style='font-weight :bold ;'>数量</span></td><td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>单价</span></td><td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>小计</span></td><td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>方式</span></td><td align=\"center\"><span style='font-weight :bold ;'>CNAS</span></td><td align=\"center\"><span style='font-weight :bold ;'>备注</span></td></tr>";

                for (int z = 0; z < dt2count; z++)
                {


                    string lowdis = dt2.Rows[z]["lowdiscount"].ToString();
                    if (lowdis == "1.00")
                    {
                        lowdis = "不能";
                    }
                    else
                    {
                        lowdis = "能";
                    }

                    if (dt2.Rows[z]["checkmodel"].ToString() != "代送")
                    {


                        strTable += "<tr height=27><td align=\"center\" width=\"5%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["num"].ToString() + "</span></td><td align=\"center\" ><span style='font-weight :bold ;'>" + dt2.Rows[z]["name"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["model"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["count"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["confirmprice"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["totalconfirmprice"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["checkmodel"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["cnas"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["beizhu1"].ToString() + "</span></td></tr>";
                    }
                    else
                    {

                        string dss = dt2.Rows[z]["daisongjine"].ToString();
                        if (dss == "0.00")
                        {
                            dss = dt2.Rows[z]["confirmprice"].ToString();
                        }
                        
                        strTable += "<tr height=27><td align=\"center\" width=\"5%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["num"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["name"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["model"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["count"].ToString() + "</span></td> <td align=\"center\"><span style='font-weight :bold ;'>" + dss + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["totalconfirmprice"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["checkmodel"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["cnas"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["beizhu1"].ToString() + "</span></td></tr>";

                    }


                }
                strTable += "<tr height=27><td align=\"center\" ><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'>小计:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt3.Rows[0]["totalconfirmprice"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";

                if (zhekou == "是" && dt3.Rows[0]["youhuibili"].ToString() != "100")
                {

                    strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>折扣:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt3.Rows[0]["youhuibili"].ToString() + "%</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";
                    strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>优惠:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + dt3.Rows[0]["youhuijine"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";


                    decimal traffic = Convert.ToDecimal(dt3.Rows[0]["traffic"]);
                    if (traffic != 0)
                    {
                        strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>交通费:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + traffic + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";
                        decimal zong = traffic + Convert.ToDecimal(dt3.Rows[0]["youhuijine"]);

                        strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>共计收费:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + Convert.ToInt32(zong) + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";

                    }
                    else
                    {
                        strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>共计收费:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + Convert.ToInt32(dt3.Rows[0]["youhuijine"]) + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";

                    }



                }
                else if (zhekou == "否") 
                {
                    decimal traffic = Convert.ToDecimal(dt3.Rows[0]["traffic"]);
                    if (traffic != 0)
                    {
                        strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>交通费:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + traffic + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";
                        decimal zong = traffic + Convert.ToDecimal(dt3.Rows[0]["youhuijine"]);

                        strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>共计收费:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + Convert.ToInt32(zong) + "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";

                    }
                    else
                    {
                        strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>共计收费:</span></td><td align=\"center\"><span style='font-weight :bold ;'>" + Convert.ToInt32(dt3.Rows[0]["totalconfirmprice"])+ "</span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'></span></td></tr>";

                    }
                }

            }


            strTable += "</table>";



            //strTable +="<div class=\"PageNext\"></div>";

            strTable += " <hr style ='width :97%' />";


            string strTable1 = "";
            string strTable2 = "";
            string strTable3 = "";
            string strTable4 = "";
            string strTable5 = "";


            string strTable6 = "";
            string strTable7 = "";
            string strTable8 = "";
            string strTable9 = "";
            string strTable10 = "";

            string strTable11 = "";
            string strTable12 = "";
            string strTable13 = "";
            string strTable14 = "";
            string strTable15 = "";


            string strTable16 = "";
            string strTable17 = "";
            string strTable18 = "";
            string strTable19 = "";
            string strTable20 = "";


            string strTable21 = "";
            string strTable22 = "";
            string strTable23 = "";


            string a = dt4.Rows[0]["beizhu"].ToString();
           string b = dt4.Rows[1]["beizhu"].ToString();
           string c = dt4.Rows[2]["beizhu"].ToString();
           string d = dt4.Rows[3]["beizhu"].ToString();
            string ee = dt4.Rows[4]["beizhu"].ToString();

            string f = dt4.Rows[5]["beizhu"].ToString();
           string g = dt4.Rows[6]["beizhu"].ToString();

            string h = dt4.Rows[7]["beizhu"].ToString();
            string i = dt4.Rows[8]["beizhu"].ToString();
            string j = dt4.Rows[9]["beizhu"].ToString();

            string k = dt4.Rows[10]["beizhu"].ToString();
            string l = dt4.Rows[11]["beizhu"].ToString();
            string m = dt4.Rows[12]["beizhu"].ToString();
            string n = dt4.Rows[13]["beizhu"].ToString();
            string o = dt4.Rows[14]["beizhu"].ToString();

            string p = dt4.Rows[15]["beizhu"].ToString();
            string q = dt4.Rows[16]["beizhu"].ToString();
           string r = dt4.Rows[17]["beizhu"].ToString();
           string s = dt4.Rows[18]["beizhu"].ToString();
            string t = dt4.Rows[19]["beizhu"].ToString();

            string u = dt4.Rows[20]["beizhu"].ToString();
            string v = dt4.Rows[21]["beizhu"].ToString();
           string vv = dt4.Rows[22]["beizhu"].ToString();

            //if (a == "是")
            //{

            //    strTable1 = "国家检定规程/校准规范/实验室制定并得到认可的方法";

            //}
            //else
            //{
            //    strTable1 = "无要求";

            //}

            //if (b == "是")
            //{

            //    strTable2 = "客户要求的其它方法(请详细说明)";

            //}
            //else
            //{
            //    strTable2 = "无要求";

            //}
            //if (c == "是")
            //{

            //    strTable3 = "送检";

            //}
            //else
            //{
            //    strTable3 = "无要求";

            //}
            //if (d == "是")
            //{

            //    strTable4 = "上门收送,同意支付交通费";

            //}
            //else
            //{
            //    strTable4 = "无要求";

            //}
            //if (ee == "是")
            //{

            //    strTable5 = "现场,CCIC安排车辆";

            //}
            //else
            //{
            //    strTable5 = "无要求";

            //}
            //if (f == "是")
            //{

            //    strTable6 = "现场,客户接送</input>";

            //}
            //else
            //{
            //    strTable6 = "无要求";

            //}

            ////

            //if (g == "是")
            //{

            //    strTable7 = "调修,同意支付调修费用";

            //}
            //else
            //{
            //    strTable7 = "无要求";

            //}

            //if (h == "是")
            //{

            //    strTable8 = "不调修,返回";

            //}
            //else
            //{
            //    strTable8 = "无要求";

            //} if (i == "是")
            //{

            //    strTable9 = "发现故障后与您确认";

            //}
            //else
            //{
            //    strTable9 = "无要求";

            //} if (j == "是")
            //{

            //    strTable10 = "不需要";

            //}
            //else
            //{
            //    strTable10 = "无要求";

            //} if (k == "是")
            //{

            //    strTable11 = "需要,按照规程或规范的要求</input>";

            //}
            //else
            //{
            //    strTable11 = "无要求";

            //} if (l == "是")
            //{

            //    strTable12 = "需要,客户指定校准周期";

            //}
            //else
            //{
            //    strTable12 = "无要求";

            //} if (m == "是")
            //{

            //    strTable13 = "不需要,出具标准校准证书即可";

            //}
            //else
            //{
            //    strTable13 = "无要求";

            //} if (n == "是")
            //{

            //    strTable14 = "符合相关指标的给予判定,有超差点的只出具校准证书";

            //}
            //else
            //{
            //    strTable14 = "无要求";

            //} if (o == "是")
            //{

            //    strTable15 = "需要,按照客户指定";

            //}
            //else
            //{
            //    strTable15 = "无要求";

            //} if (p == "是")
            //{

            //    strTable16 = "指定代送深圳计量院或华南,费用按其收费单按实支付";

            //}
            //else
            //{
            //    strTable16 = "无要求";

            //} if (q == "是")
            //{

            //    strTable17 = "无指定要求";

            //}
            //else
            //{
            //    strTable17 = "无要求";

            //}

            //if (r == "是")
            //{

            //    strTable18 = "指定要求";

            //}
            //else
            //{
            //    strTable18 = "无要求";

            //}


            //if (s == "是")
            //{

            //    strTable18 = "正常";

            //}
            //else
            //{
            //    strTable18 = "无要求";

            //} if (t == "是")
            //{

            //    strTable19 = "加急(3个工作日)";

            //}
            //else
            //{
            //    strTable19 = "无要求";

            //} if (u == "是")
            //{

            //    strTable20 = "特急(1个工作日)";

            //}
            //else
            //{
            //    strTable20 = "无要求";

            //} if (v == "是")
            //{

            //    strTable21 = "要求现场粘贴";

            //}
            //else
            //{
            //    strTable21 = "无要求";

            //}
            //if (vv == "是")
            //{

            //    strTable22 = "要求打印标签";

            //}
            //else
            //{
            //    strTable22 = "无要求";

            //}


            //strTable23 = "指定要求";

            if (a == "是")
            {

                strTable1 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >国家检定规程/校准规范/实验室制定并得到认可的方法</input>";

            }
            else
            {
                strTable1 = "<input id=\"Checkbox1\" type=\"checkbox\"  >国家检定规程/校准规范/实验室制定并得到认可的方法</input>";

            }

            if (b == "是")
            {

                strTable2 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >客户要求的其它方法(请详细说明)</input>";

            }
            else
            {
                strTable2 = "<input id=\"Checkbox1\" type=\"checkbox\"  >客户要求的其它方法(请详细说明)</input>";

            }
            if (c == "是")
            {

                strTable3 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >送检</input>";

            }
            else
            {
                strTable3 = "<input id=\"Checkbox1\" type=\"checkbox\"  >送检</input>";

            }
            if (d == "是")
            {

                strTable4 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >上门收送,同意支付交通费</input>";

            }
            else
            {
                strTable4 = "<input id=\"Checkbox1\" type=\"checkbox\" >上门收送,同意支付交通费</input>";

            }
            if (ee == "是")
            {

                strTable5 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >现场,CCIC安排车辆</input>";

            }
            else
            {
                strTable5 = "<input id=\"Checkbox1\" type=\"checkbox\"  >现场,CCIC安排车辆</input>";

            }
            if (f == "是")
            {

                strTable6 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >现场,客户接送</input>";

            }
            else
            {
                strTable6 = "<input id=\"Checkbox1\" type=\"checkbox\"  >现场,客户接送</input>";

            }



            if (g == "是")
            {

                strTable7 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >调修,同意支付调修费用</input>";

            }
            else
            {
                strTable7 = "<input id=\"Checkbox1\" type=\"checkbox\"  >调修,同意支付调修费用</input>";

            }

            if (h == "是")
            {

                strTable8 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >不调修,返回</input>";

            }
            else
            {
                strTable8 = "<input id=\"Checkbox1\" type=\"checkbox\"  >不调修,返回</input>";

            } if (i == "是")
            {

                strTable9 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >发现故障后与您确认</input>";

            }
            else
            {
                strTable9 = "<input id=\"Checkbox1\" type=\"checkbox\"  >发现故障后与您确认</input>";

            } if (j == "是")
            {

                strTable10 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >不需要</input>";

            }
            else
            {
                strTable10 = "<input id=\"Checkbox1\" type=\"checkbox\"  >不需要</input>";

            } if (k == "是")
            {

                strTable11 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >需要,按照规程或规范的要求</input>";

            }
            else
            {
                strTable11 = "<input id=\"Checkbox1\" type=\"checkbox\"  >需要,按照规程或规范的要求</input>";

            } if (l == "是")
            {

                strTable12 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >需要,客户指定校准周期</input> <input  type =\"text\"  style =\"  background:none;border-bottom:1px solid black;border-top:none; border-right:none;  border-left:none; width:55px\"/>";

            }
            else
            {
                strTable12 = "<input id=\"Checkbox1\" type=\"checkbox\"  >需要,客户指定校准周期</input>  <input  type =\"text\"  style =\"  background:none;border-bottom:1px solid black;border-top:none; border-right:none;  border-left:none; width:55px\"/>";

            } if (m == "是")
            {

                strTable13 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >不需要,出具标准校准证书即可</input>";

            }
            else
            {
                strTable13 = "<input id=\"Checkbox1\" type=\"checkbox\"  >不需要,出具标准校准证书即可</input>";

            } if (n == "是")
            {

                strTable14 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >符合相关指标的给予判定,有超差点的只出具校准证书</input>";

            }
            else
            {
                strTable14 = "<input id=\"Checkbox1\" type=\"checkbox\"  >符合相关指标的给予判定,有超差点的只出具校准证书</input>";

            } if (o == "是")
            {

                strTable15 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >需要,按照客户指定</input>";

            }
            else
            {
                strTable15 = "<input id=\"Checkbox1\" type=\"checkbox\"  >需要,按照客户指定</input>";

            } if (p == "是")
            {

                strTable16 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >指定代送深圳计量院或华南,费用按其收费单按实支付</input>";

            }
            else
            {
                strTable16 = "<input id=\"Checkbox1\" type=\"checkbox\"  >指定代送深圳计量院或华南,费用按其收费单按实支付</input>";

            } if (q == "是")
            {

                strTable17 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >无指定要求</input>";

            }
            else
            {
                strTable17 = "<input id=\"Checkbox1\" type=\"checkbox\" >无指定要求</input>";

            }

            if (r == "是")
            {

                strTable18 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >指定要求</input>";

            }
            else
            {
                strTable18 = "<input id=\"Checkbox1\" type=\"checkbox\"  >指定要求</input>";

            }


            if (s == "是")
            {

                strTable18 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >正常</input>";

            }
            else
            {
                strTable18 = "<input id=\"Checkbox1\" type=\"checkbox\"  >正常</input>";

            } if (t == "是")
            {

                strTable19 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >加急(3个工作日)</input>";

            }
            else
            {
                strTable19 = "<input id=\"Checkbox1\" type=\"checkbox\"  >加急(3个工作日)</input>";

            } if (u == "是")
            {

                strTable20 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >特急(1个工作日)</input>";

            }
            else
            {
                strTable20 = "<input id=\"Checkbox1\" type=\"checkbox\"  >特急(1个工作日)</input>";

            } if (v == "是")
            {

                strTable21 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" >要求现场粘贴</input>";

            }
            else
            {
                strTable21 = "<input id=\"Checkbox1\" type=\"checkbox\"  >要求现场粘贴</input>";

            }
            if (vv == "是")
            {

                strTable22 = "<input id=\"Checkbox1\" type=\"checkbox\" checked =\"checked\" > 要求打印标签</input>";

            }
            else
            {
                strTable22 = "<input id=\"Checkbox1\" type=\"checkbox\"  > 要求打印标签</input>";

            }


            strTable23 = "<input id=\"Checkbox1\" type=\"checkbox\"  > 指定要求</input><input  type =\"text\"  style =\"  background:none;border-bottom:1px solid black;border-top:none; border-right:none;  border-left:none; width:55px\"/>";

                     

            string s1 = strTable1 + strTable2;
            string s2 = strTable3 + strTable4 + strTable5 + strTable6;
            string s3 = strTable9 + strTable7 + strTable8;
            string s4 = strTable10 + strTable11 + strTable12;
            string s5 = strTable13 + strTable14 + strTable15;
            string s6 = strTable16 + strTable17 + strTable23;
            string s7 = strTable18 + strTable19 + strTable20;
            string s8 = strTable21 + strTable22;


            //s1 = s1.Replace("无要求", "");
            //s2 = s2.Replace("无要求", "");
            //s3 = s3.Replace("无要求", "");
            //s4 = s4.Replace("无要求", "");
            //s5 = s5.Replace("无要求", "");
            //s6 = s6.Replace("无要求", "");
            //s7 = s7.Replace("无要求", "");
            //s8 = s8.Replace("无要求", "");

            //strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"center\" style ='' >客户对校准工作的要求</td></tr></table>";



            //if (s1.Contains("无要求")) { }
            //else
            //{

            //    strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" style ='' class=\"F_size12 \"  width=\"220\">1,您希望本中心校准依据优先采用:</td><td align=\"left\" class=\"F_size12 \"  style ='' >" + s1 + "</td></tr></table>";
            //}
            //if (s2.Contains("无要求")) { }
            //else
            //{

            //    strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" style ='' class=\"F_size12 \" width=\"220\">2,您希望本批次器具的校准方式为:</td><td align=\"left\" class=\"F_size12 \" >" + s2 + "</td></tr></table>";
            //}

            //if (s3.Contains("无要求")) { }
            //else
            //{
            //    strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" width=\"220\">3,仪器出现故障,您需要我们:</td><td align=\"left\" class=\"F_size12 \" >" + s3 + "</td></tr></table>";

            //}
            //if (s4.Contains("无要求")) { }
            //else
            //{
            //    strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" width=\"220\">4,证书上是否需要提示下次校准日期:</td><td align=\"left\" class=\"F_size12 \" >" + s4 + "</td></tr></table>";

            //}
            //if (s5.Contains("无要求")) { }
            //else
            //{

            //    strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" width=\"220\">5,证书上是否需要判定:</td><td align=\"left\" class=\"F_size12 \" >" + s5 + "</td></tr></table>";

            //}
            //if (s6.Contains("无要求")) { }
            //else
            //{
            //    strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" width=\"220\">6,如超出能力范围,是否同意代送其它机构:</td><td align=\"left\" class=\"F_size12 \" >" + s6 + "</td></tr></table>";
            //}
            //if (s7.Contains("无要求")) { }
            //else
            //{


            //    strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" width=\"220\">7,时间要求:</td><td align=\"left\" class=\"F_size12 \" >" + s7 + "</td></tr></table>";
            //}
            //if (s8.Contains("无要求")) { }
            //else
            //{
            //    strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" width=\"220\">8,标签要求:</td><td align=\"left\" class=\"F_size12 \" >" + s8 + "</td></tr></table>";
            //}

            //strTable += " <hr style ='width :97%' />";


            //strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"center\" style ='' >备注事项 (重要,请您仔细阅读)</td></tr></table>";





            //strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" style ='' class=\"F_size12 \"  >1,如客户所委托仪器的型号规格或准确度与上述所列不符，校准后将以实际的型号规格和准确度确定最后收费全额;</td></tr></table>";

            //strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" style ='' class=\"F_size12 \" >2,若需现场校准敬请提前七天通知；送检工作完成周期为5个工作日，量块、温度仪表、玻璃器具为7-10个工作日;</td></tr></table>";


            //strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" >3,若现场校准的设备因故取消或故障比例达到四分之一以上，且取消部分不再复校的，则取消全部折扣;</td></tr></table>";



            //strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" >4,由客户方原因导致现场校准未按计划完成的，补校设备需由客户送检至实验室。若有5台以上设备或有大型设备必须下厂的.由客户负责车辆接送或承担交通费用;</td></tr></table>";






            //strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" >5,必须在实验室中完成的被校设备（如量块、温湿度计等）,我司在下厂校准时免费带回，校准完成后需由客户自取或承担相关送还费用;</td></tr></table>";


            //strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" >6,折扣部分不包括代送设备的校准费用和交通费用;</td></tr></table>";

            //strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" >7,以上报价已含税费，有效期60天.</td></tr></table>";




            //strTable += " <hr style ='width :97%' />";



            //strTable += "<table width=\"97%\" align=\"center\" border=\"1\">";
            //strTable += "<tr height=27><td align=\"center\" style ='width :50%'>" + dt3.Rows[0]["kehuname"].ToString() + "</td><td align=\"center\" style ='width :50%'><span style='font-weight :bold ;'>中国检验认证集团深圳有限公司校准测量中心</span></td></tr>";
            //strTable += "<tr height=27><td align=\"left\" style ='width :50%'><span style='font-weight :bold ;'>签字</span></td><td align=\"left\" style ='width :50%'><span style='font-weight :bold ;'>签字</span></td></tr>";
            //strTable += "<tr height=27><td align=\"left\" style ='width :50%'><span style='font-weight :bold ;'>日期</span></td><td align=\"left\" style ='width :50%'><span style='font-weight :bold ;'>日期</span></td></tr>";




            //strTable += "</table>";




            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"center\" style ='' ><span style='font-weight :bold ;'>客户对校准工作的要求(重要,请您认真勾选)</span></td></tr></table>";





            strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" style ='' class=\"F_size12 \"  width=\"220\">1,您希望本中心校准依据优先采用:</td><td align=\"left\" class=\"F_size12 \"  style ='' >" + s1 + "</td></tr></table>";

            strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" style ='' class=\"F_size12 \" width=\"220\">2,您希望本批次器具的校准方式为:</td><td align=\"left\" class=\"F_size12 \" >" + s2 + "</td></tr></table>";


            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" width=\"220\">3,仪器出现故障,您需要我们:</td><td align=\"left\" class=\"F_size12 \" >" + s3 + "</td></tr></table>";



            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" width=\"220\">4,证书上是否需要提示下次校准日期:</td><td align=\"left\" class=\"F_size12 \" >" + s4 + "</td></tr></table>";



            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" width=\"220\">5,证书上是否需要判定:</td><td align=\"left\" class=\"F_size12 \" >" + s5 + "</td></tr></table>";


            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" width=\"220\">6,如超出能力范围,是否同意代送其它机构:</td><td align=\"left\" class=\"F_size12 \" >" + s6 + "</td></tr></table>";


            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" width=\"220\">7,时间要求:</td><td align=\"left\" class=\"F_size12 \" >" + s7 + "</td></tr></table>";

            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" width=\"220\">8,标签要求:</td><td align=\"left\" class=\"F_size12 \" >" + s8 + "</td></tr></table>";


            strTable += " <hr style ='width :97%' />";


            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"center\" style =''><span style='font-weight :bold ;'>备注事项 (重要,请您仔细阅读)</span></td></tr></table>";





            strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" style ='' class=\"F_size12 \"  >1,如客户所委托仪器的型号规格或准确度与上述所列不符，校准后将以实际的型号规格和准确度确定最后收费全额;</td></tr></table>";

            strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" style ='' class=\"F_size12 \" >2,若需现场校准敬请提前七天通知；送检工作完成周期为5个工作日，量块、温度仪表、玻璃器具为7-10个工作日;</td></tr></table>";


            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" >3,若现场校准的设备因故取消或故障比例达到四分之一以上，且取消部分不再复校的，则取消全部折扣;</td></tr></table>";



            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" >4,由客户方原因导致现场校准未按计划完成的，补校设备需由客户送检至实验室。若有5台以上设备或有大型设备必须下厂的.由客户负责车辆接送或承担交通费用;</td></tr></table>";






            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" >5,必须在实验室中完成的被校设备（如量块、温湿度计等）,我司在下厂校准时免费带回，校准完成后需由客户自取或承担相关送还费用;</td></tr></table>";


            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" >6,折扣部分不包括代送设备的校准费用和交通费用;</td></tr></table>";

            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" class=\"F_size12 \" >7,以上报价已含税费，有效期60天.</td></tr></table>";




            strTable += " <hr style ='width :97%' />";



            strTable += "<table width=\"97%\" align=\"center\" border=\"1\">";
            strTable += "<tr height=27><td align=\"center\" style ='width :50%'>" + dt3.Rows[0]["kehuname"].ToString() + "</td><td align=\"center\" style ='width :50%'><span style='font-weight :bold ;'>中国检验认证集团深圳有限公司校准测量中心</span></td></tr>";
            strTable += "<tr height=27><td align=\"left\" style ='width :50%'><span style='font-weight :bold ;'>签字</span></td><td align=\"left\" style ='width :50%'><span style='font-weight :bold ;'>签字</span></td></tr>";
            strTable += "<tr height=27><td align=\"left\" style ='width :50%'><span style='font-weight :bold ;'>日期</span></td><td align=\"left\" style ='width :50%'><span style='font-weight :bold ;'>日期</span></td></tr>";




            strTable += "</table>";









            lblTable.Text = strTable;
        }



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
                for (int i = 0; i < MoneyPriceDot.Length; i++)
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
