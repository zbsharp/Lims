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
using Common;
using System.IO;
using System.Text;
using System.Drawing;
public partial class Print_QuoPrint : System.Web.UI.Page
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
        string xiaoji = "";
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


            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con2.Open();



            string sqlsum = "select sum(total) as total from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' group by baojiaid";
            SqlCommand cmdsum = new SqlCommand(sqlsum, con2);
            SqlDataReader drsum = cmdsum.ExecuteReader();
            if (drsum.Read())
            {
                if (drsum["total"] == DBNull.Value)
                {
                    xiaoji = "0";
                }
                else
                {
                    xiaoji = Convert.ToDecimal(drsum["total"]).ToString().Trim();
                    xiaoji = Math.Round(Convert.ToDecimal(drsum["total"]),2).ToString();
                }
            }
            drsum.Close();
            string sql1 = "select  * from BaoJiaChanPing where baojiaid='" + Request.QueryString["baojiaid"].ToString() + "' order by id";

            SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con2);
            DataSet ds1 = new DataSet();
            ad1.Fill(ds1);
            DataTable dt1 = ds1.Tables[0];
            int dt1count = dt1.Rows.Count;

            string sql2 = "select  * from BaoJiaCPXiangMu where baojiaid='" + Request.QueryString["baojiaid"].ToString() + "' order by id";

            SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con2);
            DataSet ds2 = new DataSet();
            ad2.Fill(ds2);
            DataTable dt2 = ds2.Tables[0];
            int dt2count = dt2.Rows.Count;




            string sql3 = "select  * from BaoJiaBiao where baojiaid='" + Request.QueryString["baojiaid"].ToString() + "' order by id";

            SqlDataAdapter ad3 = new SqlDataAdapter(sql3, con2);
            DataSet ds3 = new DataSet();
            ad3.Fill(ds3);
            DataTable dt3 = ds3.Tables[0];
           
            customerid = dt3.Rows[0]["kehuid"].ToString();

            string sql4 = "select  * from quoterequires where baojiaid='" + Request.QueryString["baojiaid"].ToString() + "' order by id";

            SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con2);
            DataSet ds4 = new DataSet();
            ad4.Fill(ds4);
            DataTable dt4 = ds4.Tables[0];
            int dt4count = dt4.Rows.Count;


            string sql5 = "select * from Customer where kehuid='" + customerid + "'";

            SqlDataAdapter ad5 = new SqlDataAdapter(sql5, con2);
            DataSet ds5 = new DataSet();
            ad5.Fill(ds5);
            DataTable dt5 = ds5.Tables[0];

            string sql6 = "select * from CustomerLinkMan where customerid='" + customerid + "' order by  id desc";

            SqlDataAdapter ad6 = new SqlDataAdapter(sql6, con2);
            DataSet ds6 = new DataSet();
            ad6.Fill(ds6);
            DataTable dt6 = ds6.Tables[0];

            int aaa = dt6.Rows.Count;
            if (aaa == 0)
            {
                Response.Write("<script>alert('你没有增加该客户的联系人!请增加');window.location.href='../Customer/CustManage.aspx'</script>");
            }


            string sql7 = "select * from userinfo where username='" + Session["UserName"].ToString() + "'";

            SqlDataAdapter ad7 = new SqlDataAdapter(sql7, con2);
            DataSet ds7 = new DataSet();
            ad7.Fill(ds7);
            DataTable dt7 = ds7.Tables[0];

            string sql8 = "select * from Clause where baojiaid='" + baojiaid + "'";

            SqlDataAdapter ad8 = new SqlDataAdapter(sql8, con2);
            DataSet ds8 = new DataSet();
            ad8.Fill(ds8);
            DataTable dt8 = ds8.Tables[0];


            con2.Close();




            strTable += "<table width=\"97%\"  border=\"0\" align=\"center\"><tr><td  align=\"left\"><img src='../Images/002.jpg' /></td><td align=\"center\" class=\"F_size16  \">中检集团南方电子产品测试（深圳）有限公司</br><span class=\"F_size19\">CCIC Southern Electronic Product Testing（Shenzhen）Co., Ltd.</span></td></tr></table>";
            strTable += "<br/>";
            strTable += "<table width=\"97%\" height=30 border=\"0\" align=\"center\"><tr><td align=\"center\" style ='' class=\"F_size17 F_B \">报 价 单</td></tr></table>";
            //strTable += "<table width=\"97%\" height=30 border=\"0\" align=\"center\"><tr><td wids1h=\"43%\">&nbsp;</td><td  class=\"F_size16 F_B \"></td><td  align=\"right\">Quo No：" + Request.QueryString["baojiaid"].ToString() + "</td></tr></table>";
            strTable += "<br/>";

            strTable += "<table width=\"97%\" align=\"center\" border=\"0.5\"><tr height=27><td align=\"left\"><span style='font-weight :bold ;'>报价单号</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + Request.QueryString["baojiaid"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>日期</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + DateTime.Now.ToShortDateString() + "</span></td></tr>";
            strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>To:客户名称/Company：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt5.Rows[0]["customname"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>From</span></td><td align=\"left\"><span style='font-weight :bold ;'>倍测检测/CCIC-SET</span></td></tr>";
            if (aaa != 0)
            {
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>联系人/Contact Person：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["name"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>报价</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt3.Rows[0]["fillname"].ToString() + "</span></td></tr>";


                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>电话/Tel：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["telephone"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>电话/Tel：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt7.Rows[0]["banggongdianhua"].ToString() + "</span></td></tr>";
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>传真/Fax：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["fax"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>传真/Fax：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt7.Rows[0]["fax"].ToString() + "</span></td></tr>";



            }
            else
            {
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>联系人/Contact Person</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>报价</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td></tr>";

                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>电话/Tel：</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>电话/Tel：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt7.Rows[0]["banggongdianhua"].ToString() + "</span></td></tr>";
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>传真/Fax：</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>传真/Fax：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt7.Rows[0]["fax"].ToString() + "</span></td></tr>";

            }


            strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>电子邮件/E-mail：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["email"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>电子邮件/E –mail：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt7.Rows[0]["youxiang"].ToString() + "</span></td></tr>";


          

              strTable += "</table>";
              strTable += "<br/>";

           
                strTable += "<table width=\"97%\" align=\"center\" border=\"1\">";


                strTable += "<tr height=27>";

                strTable += "<td   width=\"4%\" align=\"center\"><span style='font-weight :bold ;'>产品:</span></td>";
                strTable += "<td  width=\"97%\" colspan=\"8\" align=\"left\"><span style='font-weight :bold ;'>" + dt1.Rows[0]["name"].ToString() + "," + dt1.Rows[0]["type"].ToString() + "</span></td>";
                
                strTable += "</tr>";



                strTable += "<tr height=27>";

                strTable += "<td align=\"center\" width=\"4%\"><span style='font-weight :bold ;'>序号</span></td>";
                strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>测试项目</span></td>";
                strTable += "<td align=\"center\" width=\"25%\"><span style='font-weight :bold ;'>依据标准</span></td>";
                strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>所需样品</span></td>";
                strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>周期</span></td>";
                strTable += "<td align=\"center\" width=\"7%\"><span style='font-weight :bold ;'>单价/RMB</span></td>";
                strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>数量/小时</span></td>";
                strTable += "<td align=\"center\" width=\"5%\"><span style='font-weight :bold ;'>小计</span></td>";
                strTable += "<td align=\"center\" width=\"9%\"><span style='font-weight :bold ;'>备注</span></td>";
                strTable += "</tr>";
            
                for (int z = 0; z < dt2count; z++)
                {


                    strTable += "<tr height=27>";

                    strTable += "<td align=\"center\" width=\"4%\"><span style='font-weight :bold ;'>" + z + 1 + "</span></td>";
                    strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["ceshiname"].ToString() + "</span></td>";
                    strTable += "<td align=\"center\" width=\"25%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["biaozhun"].ToString() + "</span></td>";
                    strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["yp"].ToString() + "</span></td>";
                    strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["zhouqi"].ToString() + "</span></td>";
                    strTable += "<td align=\"center\" width=\"7%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["feiyong"].ToString() + "</span></td>";
                    strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["shuliang"].ToString() + "</span></td>";
                    strTable += "<td align=\"center\" width=\"5%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["feiyong"].ToString() + "</span></td>";
                    strTable += "<td align=\"center\" width=\"9%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["beizhu"].ToString() + "</span></td>";
                    strTable += "</tr>";


                }
                strTable += "<tr height=27>";

                strTable += "<td align=\"center\" width=\"4%\"><span style='font-weight :bold ;'></span></td>";
                strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'></span></td>";
                strTable += "<td align=\"center\" width=\"25%\"><span style='font-weight :bold ;'></span></td>";
                strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'></span></td>";
                strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>小计</span></td>";
                strTable += "<td align=\"center\" width=\"7%\"><span style='font-weight :bold ;'>" + xiaoji + "</span></td>";
                strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>实际报价</span></td>";
                strTable += "<td align=\"center\" width=\"5%\"><span style='font-weight :bold ;'>" + dt3.Rows[0]["zhehoujia"].ToString() + "</span></td>";
                strTable += "<td align=\"center\" width=\"9%\"><span style='font-weight :bold ;'></span></td>";
                strTable += "</tr>";
            
          
             
            
          

            strTable += "</table>";



            //strTable +="<div class=\"PageNext\"></div>";

            strTable += " <hr style ='width :97%' />";



            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" style ='' ><span style='font-weight :bold ;'>条款：</span></td></tr></table>";


            int dd = dt8.Rows.Count;

            for (int i = 0; i < dd; i++)
            {


                strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" style ='' class=\"F_size12 \"  width=\"20\">" + dt8.Rows[i]["bianhao"].ToString() + "</td><td align=\"left\" class=\"F_size12 \"  style ='' >" + dt8.Rows[i]["tiaokuan"].ToString() + "</td></tr></table>";
            }
            strTable += " <hr style ='width :97%' />";

            strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=25 border=\"0\" align=\"center\"><tr><td align=\"left\" style ='' class=\"F_size12 \"  width=\"40\">备注：</td><td align=\"left\" class=\"F_size12 \"  style ='' >" + dt3.Rows[0]["beizhu"].ToString() + "</td></tr></table>";


            strTable += " <hr style ='width :97%' />";
            strTable += "<table width=\"97%\" align=\"center\" border=\"0\"><tr height=27><td align=\"center\"><span >账户信息</span></td></tr></table>";

            strTable += "<table width=\"97%\" align=\"center\" border=\"0\"><tr height=27><td align=\"center\"><span >户名：中检集团南方电子产品测试（深圳）有限公司</span></td></tr></table>";
            strTable += "<table width=\"97%\" align=\"center\" border=\"0\"><tr height=27><td align=\"center\"><span >开户行：农行深圳市分行西丽支行（开户行地址：深圳市）</span></td></tr></table>";
            strTable += "<table width=\"97%\" align=\"center\" border=\"0\"><tr height=27><td align=\"center\"><span >账号：41013800040006130</span></td></tr></table>";



            strTable += "<table width=\"97%\" align=\"center\" border=\"1\">";
            strTable += "<tr height=27><td align=\"center\" style ='width :50%'>" + dt5.Rows[0]["customname"].ToString() + "</td><td align=\"center\" style ='width :50%'><span style='font-weight :bold ;'>中检集团南方电子产品测试（深圳）有限公司</span></td></tr>";
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