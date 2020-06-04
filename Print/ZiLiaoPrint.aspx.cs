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

public partial class Print_ZiLiaoPrint : System.Web.UI.Page
{
    protected string tijiaobianhao = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string responser = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        tijiaobianhao = Request.QueryString["bianhao"].ToString();

        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con2.Open();
        string sql = "select * from anjianxinxi2 where (bianhao='" + tijiaobianhao + "' or taskno='" + tijiaobianhao + "')";
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

        string sql2 = "select  * from BaoJiaCPXiangMu where id in (select xiangmubianhao from anjianxinxi3 where  bianhao='" + tijiaobianhao + "')";

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

        string sql6 = "select * from CustomerLinkMan where id =(select linkid from baojialink where baojiaid='" + baojiaid + "') order by  id desc";

        SqlDataAdapter ad6 = new SqlDataAdapter(sql6, con2);
        DataSet ds6 = new DataSet();
        ad6.Fill(ds6);
        DataTable dt6 = ds6.Tables[0];

        int aaa = dt6.Rows.Count;
        if (aaa == 0)
        {

            con2.Close();
            Response.Write("<script>alert('你没有增加该客户的联系人!请增加');window.location.href='../Customer/CustManage.aspx'</script>");
        }


        string sql7 = "select * from userinfo where  username = (select kf from anjianinfo2 where  rwbianhao='" + tijiaobianhao + "')";

        SqlDataAdapter ad7 = new SqlDataAdapter(sql7, con2);
        DataSet ds7 = new DataSet();
        ad7.Fill(ds7);
        DataTable dt7 = ds7.Tables[0];


        int aaa7 = dt7.Rows.Count;
        if (aaa7 == 0)
        {

            con2.Close();
            Response.Write("<script>alert('未分派项目经理');</script>");
            return;
        }


        string sql8 = "select * from Clause where baojiaid='" + baojiaid + "'";

        SqlDataAdapter ad8 = new SqlDataAdapter(sql8, con2);
        DataSet ds8 = new DataSet();
        ad8.Fill(ds8);
        DataTable dt8 = ds8.Tables[0];

        string sql9 = "select * from anjianinfo2 where rwbianhao='" + tijiaobianhao + "'";

        SqlDataAdapter ad9 = new SqlDataAdapter(sql9, con2);
        DataSet ds9 = new DataSet();
        ad9.Fill(ds9);
        DataTable dt9 = ds9.Tables[0];

        int aaa3 = dt9.Rows.Count;
      



        string sql10 = "select id from anjianinfo where taskid='" + tijiaobianhao + "'";

        SqlDataAdapter ad10 = new SqlDataAdapter(sql10, con2);
        DataSet ds10 = new DataSet();
        ad10.Fill(ds10);
        DataTable dt10 = ds10.Tables[0];

        int aaa2 = dt10.Rows.Count;
        if (aaa2 == 0)
        {

            con2.Close();
            Response.Redirect("~/Customer/Welcome.aspx?id=rr1");
        }

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



        string sql128 = "select *,(select name from ziliaotype where id=anjianxinxi.leibieid) as name1,(select ybyq from ziliaotype where id=anjianxinxi.leibieid) as ybyq from anjianxinxi where xiangmuid='" + tijiaobianhao + "' and kaishiriqi='是' and (beizhu !='是' or beizhu is null) order by id desc";

        SqlDataAdapter ad128 = new SqlDataAdapter(sql128, con2);
        DataSet ds128 = new DataSet();
        ad128.Fill(ds128);
        DataTable dt128 = ds128.Tables[0];



        string sql13 = "select sum(xiaoji) as xiaoji from CeShiFei where bianhao='" + tijiaobianhao + "' group by bianhao";

        SqlDataAdapter ad13 = new SqlDataAdapter(sql13, con2);
        DataSet ds13 = new DataSet();
        ad13.Fill(ds13);
        DataTable dt13 = ds13.Tables[0];


        //strTable += "<table width=\"97%\"  border=\"0\" align=\"center\"><tr><td  align=\"left\"><img src='../Images/002.jpg' /></td><td align=\"center\" class=\"F_size16  \">中检集团南方电子产品测试（深圳）有限公司</br><span class=\"F_size19\">CCIC Southern Electronic Product Testing（Shenzhen）Co., Ltd.</span></td></tr></table>";
        //strTable += "<br/>";

        con2.Close();


        strTable += "<table width=\"97%\" height=35 border=\"0\" align=\"center\"><tr><td align=\"center\" style ='' class=\"F_size17 F_B \">补充样品资料通知单</td></tr></table>";


        strTable += "<table width=\"97%\" height=30 border=\"0\" align=\"center\"><tr><td wids1h=\"43%\">&nbsp;</td><td  class=\"F_size16 F_B \"></td><td  align=\"right\">编号.：" + tijiaobianhao + "</td></tr></table>";
        strTable += "<br/>";
        strTable += "<table width=\"97%\" align=\"center\" border=\"0.5\">";
        if (aaa != 0)
        {
            strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>Attn：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["name"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>From:</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt7.Rows[0]["UserName"].ToString() + "</span></td></tr>";


            strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>Tel：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["telephone"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>Tel：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt7.Rows[0]["banggongdianhua"].ToString() + "</span></td></tr>";
            strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>Fax：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["fax"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>Fax：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt7.Rows[0]["fax"].ToString() + "</span></td></tr>";







            strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>E-mail：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["email"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>E-mail：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt7.Rows[0]["youxiang"].ToString() + "</span></td></tr>";
        }



        strTable += "</table>";

        strTable += "<br/>";

        strTable += "<table width=\"97%\" align=\"center\" border=\"0\">";


        strTable += "<tr height=27>";

        strTable += "<td  width=\"100%\" colspan=\"9\" align=\"left\"><span style='font-weight :bold ;'>" + dt5.Rows[0]["customname"].ToString() + ",贵公司的以下检测内容：</span></td>";
       
        strTable += "</tr>";

       

        strTable += "</table>";

       
        strTable += "<table width=\"97%\" align=\"center\" border=\"0\">";
        strTable += "<tr height=25><td align=\"center\"><span style='font-weight :bold ;'>产品名称：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt1.Rows[0]["name"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>产品型号：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt1.Rows[0]["type"].ToString() + "</span></td></tr>";
        strTable += "<tr height=25><td align=\"center\"><span style='font-weight :bold ;'>检测类别：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["shiyanleibie"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>申请编号：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["shenqingbianhao"].ToString() + "</span></td></tr>";


        strTable += "</table>";


        strTable += "<table width=\"97%\" align=\"center\" border=\"0\">";


     

        strTable += "<tr height=27>";

        strTable += "<td  width=\"100%\" colspan=\"9\" align=\"left\"><span style='font-weight :bold ;'>经工程师初步审核，需要补充提供以下样品或资料：</span></td>";

        strTable += "</tr>";

        strTable += "</table>";




      

        strTable += "<table width=\"97%\" align=\"center\" border=\"1\">";

        strTable += "<tr height=27>";

        strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>序号</span></td>";

        strTable += "<td align=\"center\" width=\"25%\"><span style='font-weight :bold ;'>样品/资料名称</span></td>";
        strTable += "<td align=\"center\" width=\"25%\"><span style='font-weight :bold ;'>一般要求</span></td>";

      
        strTable += "<td align=\"center\" width=\"40%\"><span style='font-weight :bold ;'>补正原因</span></td>";
      

        strTable += "</tr>";

        for (int z = 0; z < ds128.Tables[0].Rows.Count; z++)
        {


            strTable += "<tr height=27>";
            int m = z + 1;


            strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>" + m + "</span></td>";

            strTable += "<td align=\"center\" width=\"25%\"><span style='font-weight :bold ;'>" + dt128.Rows[z]["name1"].ToString() + "</span></td>";
            strTable += "<td align=\"center\" width=\"25%\"><span style='font-weight :bold ;'>" + dt128.Rows[z]["ybyq"].ToString() + "</span></td>";

            //strTable += "<td align=\"center\" width=\"15%\"><span style='font-weight :bold ;'>" + dt128.Rows[z]["kaishiriqi"].ToString() + "</span></td>";
            //strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>" + dt128.Rows[z]["jihuajieshu"].ToString() + "</span></td>";
           strTable += "<td align=\"center\" width=\"40%\"><span style='font-weight :bold ;'>" + dt128.Rows[z]["jihuajieshu"].ToString() + "</span></td>";

            strTable += "</tr>";


        }




        strTable += "</table>";



        strTable += "<br/>";
        strTable += "<table width=\"97%\" align=\"center\" border=\"0\">";


        strTable += "<tr height=27>";

        strTable += "<td  width=\"100%\" colspan=\"9\" align=\"left\">&nbsp;&nbsp;&nbsp;&nbsp;为了使检测工作顺利推进，确保贵司按期获得检测报告和证书，请在本通知发出之日起<span style='font-weight :bold ;'>3个工作日</span>内，收集齐全上述样品资料，并提交给项目经理<span style='font-weight :bold ;'>" + dt9.Rows[0]["kf"].ToString() + "</span></td>";

        strTable += "</tr>";

        strTable += "<tr height=27>";

        strTable += "<td  width=\"100%\" colspan=\"9\" align=\"left\"><span style='font-weight :bold ;'>&nbsp;&nbsp;&nbsp;&nbsp;若逾期未能提供相关样品资料而导致项目延期，由贵公司承担相应后果。</span></td>";

        strTable += "</tr>";


        strTable += "<tr height=27>";

        strTable += "<td  width=\"100%\" colspan=\"9\" align=\"left\"><span style='font-weight :bold ;'>&nbsp;&nbsp;&nbsp;&nbsp;特此通知，多谢合作！</span></td>";

        strTable += "</tr>";

        strTable += "</table>";


        strTable += "<table width=\"97%\" align=\"center\" border=\"0\">";


        strTable += "<tr height=27><td align=\"left\" width=\"13%\"><span style='font-weight :bold ;'></span></td><td align=\"left\" width=\"37%\"><span style='font-weight :bold ;'></span></td><td align=\"left\" width=\"13%\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'>中检集团南方电子产品测试（深圳）有限公司</span></td></tr>";
        strTable += "<tr height=27><td align=\"left\" width=\"13%\"><span style='font-weight :bold ;'></span></td><td align=\"left\" width=\"37%\"><span style='font-weight :bold ;'></span></td><td align=\"left\" width=\"13%\"><span style='font-weight :bold ;'></span></td><td align=\"center\"><span style='font-weight :bold ;'>"+DateTime.Now.ToShortDateString()+"</span></td></tr>";

     
        strTable += "</table>";


        strTable += "<hr width=\"97%\"/>";



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