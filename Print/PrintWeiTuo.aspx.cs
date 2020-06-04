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

public partial class Print_PrintWeiTuo : System.Web.UI.Page
{
    protected string tijiaobianhao = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string responser = "";
    protected string rwid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        tijiaobianhao = Request.QueryString["bianhao"].ToString();
        //rwid = Request.QueryString["rwid"].ToString();
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con2.Open();
        string sql = "select * from anjianxinxi2 where bianhao='" + tijiaobianhao + "'";
        SqlCommand cmd = new SqlCommand(sql, con2);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            baojiaid = dr["baojiaid"].ToString();
            kehuid = dr["kehuid"].ToString();
            responser = dr["fillname"].ToString();
        }
        else
        {
            con2.Close();
            Response.Redirect("~/Customer/Welcome.aspx?id=rr1");
        }
        dr.Close();


        string strTable = "";


        string sql1 = "select  * from BaoJiaChanPing where baojiaid='" + baojiaid + "' order by id";

        SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con2);
        DataSet ds1 = new DataSet();
        ad1.Fill(ds1);
        DataTable dt1 = ds1.Tables[0];
        int dt1count = dt1.Rows.Count;

        //string sql2 = "select  * from BaoJiaCPXiangMu where id in (select xiangmubianhao from anjianxinxi3 where  bianhao='" + tijiaobianhao + "')";
        string sql2 = "select  * from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "'";
        SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con2);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);
        DataTable dt2 = ds2.Tables[0];
        int dt2count = dt2.Rows.Count;




        string sql3 = "select  * from BaoJiaBiao where baojiaid='" + baojiaid + "' order by id";

        SqlDataAdapter ad3 = new SqlDataAdapter(sql3, con2);
        DataSet ds3 = new DataSet();
        ad3.Fill(ds3);
        DataTable dt3 = ds3.Tables[0];

        kehuid = dt3.Rows[0]["kehuid"].ToString();

        string sql4 = "select  * from quoterequires where baojiaid='" + baojiaid + "' order by id";

        SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con2);
        DataSet ds4 = new DataSet();
        ad4.Fill(ds4);
        DataTable dt4 = ds4.Tables[0];
        int dt4count = dt4.Rows.Count;


        string sql5 = "select * from Customer where kehuid='" + kehuid + "'";

        SqlDataAdapter ad5 = new SqlDataAdapter(sql5, con2);
        DataSet ds5 = new DataSet();
        ad5.Fill(ds5);
        DataTable dt5 = ds5.Tables[0];

        string sql6 = "select * from CustomerLinkMan where id =(select top 1 linkid from baojialink where baojiaid='" + baojiaid + "') order by  id desc";

        SqlDataAdapter ad6 = new SqlDataAdapter(sql6, con2);
        DataSet ds6 = new DataSet();
        ad6.Fill(ds6);
        DataTable dt6 = ds6.Tables[0];

        int aaa = dt6.Rows.Count;
        if (aaa == 0)
        {
            con2.Close();

            Response.Write("<script>alert('你没有增加该客户的联系人!请增加');window.location.href='../Customer/CustManage.aspx'</script>");
            return;

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



        string sql11 = "select * from anjianxinxi2 where bianhao='" + tijiaobianhao + "'";

        SqlDataAdapter ad11 = new SqlDataAdapter(sql11, con2);
        DataSet ds11 = new DataSet();
        ad11.Fill(ds11);
        DataTable dt11 = ds11.Tables[0];


        string sql12 = "select * from CeShiFei where bianhao='" + tijiaobianhao + "' order by type";

        SqlDataAdapter ad12 = new SqlDataAdapter(sql12, con2);
        DataSet ds12 = new DataSet();
        ad12.Fill(ds12);
        DataTable dt12 = ds12.Tables[0];


        string sql13 = "select sum(xiaoji) as xiaoji from CeShiFei where bianhao='" + tijiaobianhao + "' group by bianhao";

        SqlDataAdapter ad13 = new SqlDataAdapter(sql13, con2);
        DataSet ds13 = new DataSet();
        ad13.Fill(ds13);
        DataTable dt13 = ds13.Tables[0];



        string sql14 = "select * from yangpin2 where anjianid='" + rwid + "' order by type";

        SqlDataAdapter ad14 = new SqlDataAdapter(sql14, con2);
        DataSet ds14 = new DataSet();
        ad14.Fill(ds14);
        DataTable dt14 = ds14.Tables[0];
        int dt14count = dt14.Rows.Count;


        con2.Close();

        strTable += "<table width=\"97%\"  border=\"0\" align=\"center\"><tr><td  align=\"left\"><img src='../Images/002.jpg' /></td><td align=\"left\" class=\"F_size16  \">深圳市倍测科技有限公司</br><span class=\"F_size19\">Shenzhen BCTC Technology CO., LTD.</span></td></tr></table>";

        strTable += "<hr width=\"97%\"/>";

        strTable += "<table width=\"97%\" height=27 border=\"1\" align=\"center\"><tr><td align=\"center\" style ='' class=\"F_size17 F_B \">认证检测申请表</br>Certification & Test Application Form</td></tr></table>";

        strTable += "<table width=\"97%\" height=30 border=\"0\" align=\"center\"><tr><td wids1h=\"43%\">&nbsp;</td><td  class=\"F_size16 F_B \"></td><td  align=\"right\">任务编号/No.：" + dt11.Rows[0]["taskno"].ToString() + "</td></tr></table>";


        strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=28><td align=\"left\" width=\"13%\" colspan=\"4\"><span style='font-weight :bold ;'>客户信息</span></td></tr>";


        strTable += "<tr height=22><td align=\"left\" width=\"13%\"><span>委托单位：</span></td><td align=\"left\" width=\"37%\"><span >" + dt11.Rows[0]["weituo"].ToString() + "</span></td><td align=\"left\" width=\"13%\"><span>委托单位英文名:</span></td><td align=\"left\" width=\"37%\"><span>" + dt11.Rows[0]["Enweituo"].ToString() + "</span></td></tr>";
        strTable += "<tr height=22><td align=\"left\"><span >委托单位地址：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["addressWei"].ToString() + "</span></td><td align=\"left\"><span>委托单位英文地址：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["EnaddressWei"].ToString() + "</span></td></tr>";
        strTable += "<tr height=22><td align=\"left\"><span >制造单位：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["zhizao"].ToString() + "</span></td><td align=\"left\"><span>制造单位英文名：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["Enshengchang"].ToString() + "</span></td></tr>";
        strTable += "<tr height=22><td align=\"left\"><span >制造单位地址：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["addresZhizao"].ToString() + "</span></td><td align=\"left\"><span>制造单位英文地址：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["ENaddresZhizao"].ToString() + "</span></td></tr>";
        strTable += "<tr height=22><td align=\"left\"><span >生产单位：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["shenchan"].ToString() + "</span></td><td align=\"left\"><span>生产单位英文名：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["Enshengchang"].ToString() + "</span></td></tr>";
        strTable += "<tr height=22><td align=\"left\"><span >生产单位地址：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["addersshengchang"].ToString() + "</span></td><td align=\"left\"><span>生产单位英文地址：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["EnaddressShengchang"].ToString() + "</span></td></tr>";
        strTable += "<tr height=22><td align=\"left\" width=\"13%\"><span >联系人：</span></td><td align=\"left\" width=\"37%\"><span>" + dt6.Rows[0]["name"].ToString() + "</span></td><td align=\"left\" width=\"13%\"><span>手机:</span></td><td align=\"left\" width=\"37%\"><span>" + dt6.Rows[0]["mobile"].ToString() + "</span></td></tr>";
        strTable += "<tr height=22><td align=\"left\" width=\"13%\"><span >微信：</span></td><td align=\"left\" width=\"37%\"><span>" + dt6.Rows[0]["Weixin"].ToString() + "</span></td><td align=\"left\" width=\"13%\"><span>QQ:</span></td><td align=\"left\" width=\"37%\"><span>" + dt6.Rows[0]["QQ"].ToString() + "</span></td></tr>";



        strTable += "<tr height=22><td align=\"left\"><span>付款方：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["fukuan"].ToString() + "</span></td><td align=\"left\"><span>邮箱：</span></td><td align=\"left\"><span>" + dt6.Rows[0]["email"].ToString() + "</span></td></tr>";

        strTable += "</table>";



        strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=28><td align=\"left\" width=\"13%\" colspan=\"4\"><span style='font-weight :bold ;'>产品信息</span></td></tr>";

        strTable += "<tr height=22><td align=\"left\"><span>产品名称：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["name"].ToString() + "</span></td><td align=\"left\"><span>送样方式：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["songyangfangshi"].ToString() + "</span></td></tr>";
        strTable += "<tr height=22><td align=\"left\"><span>产品型号：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["xinghao"].ToString() + "</span></td><td align=\"left\"><span>产品商标：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["shangbiao"].ToString() + "</span></td></tr>";


        strTable += "</table>";





        strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=28><td align=\"left\"  colspan=\"4\"><span style='font-weight :bold ;'>检测类别</span></td></tr>";


        strTable += "<tr height=22><td align=\"left\"><span>检测类别：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["xiangmu"].ToString() + "</span></td><td align=\"left\"><span>申请编号：</span></td><td align=\"left\" style=\"width:20%;\"><span>" + dt11.Rows[0]["shenqingbianhao"].ToString() + "</span></td></tr>";



        strTable += "</table>";



        strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=28><td align=\"left\" width=\"13%\" colspan=\"5\"><span style='font-weight :bold ;'>检测项目</span></td></tr>";
        strTable += "<tr height=22>";
        strTable += "<td align=\"center\" width=\"8%\"><span>序号</span></td>";
        strTable += "<td align=\"center\" width=\"30%\"><span>项目</span></td>";
        strTable += "<td align=\"center\" width=\"30%\"><span>标准</span></td>";
        strTable += "<td align=\"center\" width=\"8%\"><span>数量</span></td>";
        strTable += "<td align=\"center\" width=\"24%\"><span>备注</span></td>";
        strTable += "</tr>";

        for (int z = 0; z < ds2.Tables[0].Rows.Count; z++)
        {
            strTable += "<tr height=22>";
            int m = z + 1;
            strTable += "<td align=\"center\" width=\"8%\"><span>" + m + "</span></td>";
            strTable += "<td align=\"center\" width=\"30%\"><span>" + dt2.Rows[z]["ceshiname"].ToString() + "</span></td>";
            strTable += "<td align=\"center\" width=\"30%\"><span>" + dt2.Rows[z]["biaozhun"].ToString() + "</span></td>";
            strTable += "<td align=\"center\" width=\"8%\"><span>" + dt2.Rows[z]["shuliang"].ToString() + "</span></td>";
            strTable += "<td align=\"center\" width=\"24%\"><span>" + dt2.Rows[z]["beizhu"].ToString() + "</span></td>";
            strTable += "</tr>";
        }

        strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=28><td align=\"left\" width=\"13%\" colspan=\"5\"><span style='font-weight :bold ;'>样品信息</span></td></tr>";

        strTable += "<tr height=22>";


        strTable += "<td align=\"left\" width=\"30%\"><span>名称</span></td>";
        strTable += "<td align=\"left\" width=\"30%\"><span>型号</span></td>";
        strTable += "<td align=\"left\" width=\"8%\"><span>数量</span></td>";
        strTable += "<td align=\"left\" width=\"12%\"><span>送样日期</span></td>";

        strTable += "<td align=\"left\" width=\"20%\"><span>备注</span></td>";
        strTable += "</tr>";

        for (int z = 0; z < dt14count; z++)
        {
            strTable += "<tr height=27>";
            strTable += "<td align=\"left\" width=\"30%\"><span>" + dt14.Rows[z]["name"].ToString() + "</span></td>";
            strTable += "<td align=\"left\" width=\"30%\"><span>" + dt14.Rows[z]["model"].ToString() + "</span></td>";
            strTable += "<td align=\"left\" width=\"8%\"><span>" + dt14.Rows[z]["count"].ToString() + "</span></td>";
            strTable += "<td align=\"left\" width=\"12%\"><span>" + Convert.ToDateTime(dt14.Rows[z]["receivetime"]).ToShortDateString() + "</span></td>";
            strTable += "<td align=\"left\" width=\"20%\"><span>" + dt14.Rows[z]["remark"].ToString() + "</span></td>";
            strTable += "</tr>";
            //strTable += "<tr height=22>";
            //strTable += "<td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'></span></td>";
            //strTable += "<td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'></span></td>";
            //strTable += "<td align=\"left\" width=\"8%\"><span style='font-weight :bold ;'></span></td>";
            //strTable += "<td align=\"left\" width=\"12%\"><span style='font-weight :bold ;'></span></td>";
            //strTable += "<td align=\"left\" width=\"20%\"><span style='font-weight :bold ;'></span></td>";
            //strTable += "</tr>";
        }
        strTable += "</table>";

        strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=28><td align=\"left\" width=\"13%\" colspan=\"4\"><span style='font-weight :bold ;'>服务要求</span></td></tr>";

        strTable += "<tr height=22><td align=\"left\"><span>周期要求：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["zhouqi"].ToString() + "</span></td><td align=\"left\"><span>报告形式：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["baogao"].ToString() + "</span></td></tr>";
        strTable += "<tr height=22><td align=\"left\"><span>报告领取：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["lingqu"].ToString() + "</span></td><td align=\"left\"><span>样品处理：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["yangpinchuli"].ToString() + "</span></td></tr>";

        strTable += "<tr height=22><td align=\"left\"><span>是否分包：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["waibao"].ToString() + "</span></td><td align=\"left\"><span>其他要求：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["qitayaoqiu"].ToString() + "</span></td></tr>";
        strTable += "<tr height=22><td align=\"left\"><span>备注：</span></td><td align=\"left\" colspan=3><span>" + dt11.Rows[0]["beizhu"].ToString() + "</span></td></tr>";



        strTable += "</table>";



        strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=28><td align=\"left\"  colspan=\"4\"><span style='font-weight :bold ;'>委托方代表</span></td></tr>";


        strTable += "<tr height=22><td align=\"left\" width=\"13%\"><span>姓名：</span></td><td align=\"left\" width=\"37%\"><span>" + dt6.Rows[0]["name"].ToString() + "</span></td><td align=\"left\" width=\"13%\"><span>职位：</span></td><td align=\"left\"><span></span></td></tr>";
        strTable += "<tr height=22><td align=\"left\" width=\"13%\"><span>日期：</span></td><td align=\"left\" width=\"37%\"><span></span></td><td align=\"left\" width=\"13%\"><span>签字盖章：</span></td><td align=\"left\"><span></span></td></tr>";


        strTable += "</table>";

        strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=28><td align=\"left\"  colspan=\"4\"><span style='font-weight :bold ;'>承检方代表</span></td></tr>";


        strTable += "<tr height=22><td align=\"left\" width=\"15%\"><span>姓名：</span></td><td align=\"left\" width=\"35%\"><span></span></td><td align=\"left\" width=\"13%\"><span>职位：</span></td><td align=\"left\"><span></span></td></tr>";
        strTable += "<tr height=22><td align=\"left\" width=\"15%\"><span>日期：</span></td><td align=\"left\" width=\"35%\"><span></span></td><td align=\"left\" width=\"13%\"><span>签字盖章：</span></td><td align=\"left\"><span></span></td></tr>";

        //strTable += "<tr height=22><td align=\"left\" width=\"15%\"><span>预计检验费：</span></td><td align=\"left\" width=\"35%\"><span>" + dt11.Rows[0]["feiyong"].ToString() + "</span></td><td align=\"left\" width=\"13%\"><span>预计完成：</span></td><td align=\"left\"><span>" + dt11.Rows[0]["wancheng"].ToString() + "</span></td></tr>";

        strTable += "</table>";



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