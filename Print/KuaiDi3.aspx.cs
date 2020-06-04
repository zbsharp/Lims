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

public partial class Print_KuaiDi3 : System.Web.UI.Page
{
    protected string tijiaobianhao = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string responser = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        tijiaobianhao = Request.QueryString["bianhao"].ToString();
        string id = Request.QueryString["id"].ToString();
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con2.Open();
        string sql = "select * from kuaidizibiao where (bianhao='" + id + "' or (bianhao='" +tijiaobianhao + "' and bianhao !=''))  order by id asc";
        SqlDataAdapter ad1 = new SqlDataAdapter(sql, con2);
        DataSet ds1 = new DataSet();
        ad1.Fill(ds1);
        DataTable dt1 = ds1.Tables[0];
        int dt1count = dt1.Rows.Count;


        string strTable = "";


   

        


        strTable += "<table width=\"97%\" height=35 border=\"0\" align=\"center\"><tr><td align=\"center\" style ='' class=\"F_size17 F_B \">邮寄清单</td></tr></table>";


     





        strTable += "<table width=\"97%\" align=\"center\" border=\"1\">";

        strTable += "<tr height=27>";

        strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>序号</span></td>";

        strTable += "<td align=\"center\" width=\"25%\"><span style='font-weight :bold ;'>类型</span></td>";
        strTable += "<td align=\"center\" width=\"25%\"><span style='font-weight :bold ;'>内容</span></td>";


        strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>名称</span></td>";
        strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>型号</span></td>";

        strTable += "</tr>";

        for (int z = 0; z < ds1.Tables[0].Rows.Count; z++)
        {


            string sql2 = "select * from yangpin2 where sampleid='" + ds1.Tables[0].Rows[z]["neirong"].ToString() + "'";
            SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con2);
            DataSet ds2 = new DataSet();
            ad2.Fill(ds2);
            DataTable dt2 = ds2.Tables[0];

            int yp = dt2.Rows.Count;


            strTable += "<tr height=27>";
            int m = z + 1;


            strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>" + m + "</span></td>";

            strTable += "<td align=\"center\" width=\"25%\"><span style='font-weight :bold ;'>" + ds1.Tables[0].Rows[z]["leixing"].ToString() + "</span></td>";
            strTable += "<td align=\"center\" width=\"25%\"><span style='font-weight :bold ;'>" + ds1.Tables[0].Rows[z]["neirong"].ToString() + "</span></td>";

            if (yp > 0)
            {
                strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>" + ds2.Tables[0].Rows[0]["name"].ToString() + "</span></td>";
                strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>" + ds2.Tables[0].Rows[0]["model"].ToString() + "</span></td>";
            }
            else
            {
                strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'></span></td>";
                strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'></span></td>";
       
            }
            strTable += "</tr>";


        }


        con2.Close();

        strTable += "</table>";



        strTable += "<br/>";
    


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