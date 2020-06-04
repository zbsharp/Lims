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

public partial class Print_YangPinCuiHuan : System.Web.UI.Page
{

    protected string sampleid = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string responser = "";
    protected string rwbianhao = "";
    protected string gongchengshi = "";
    protected string wancheng = "";
    protected string shenqingbianhao = "";
    protected string name = "";
    protected string xinghao = "";
    protected string zhizao = "";
    protected string state = "";
    protected void Page_Load(object sender, EventArgs e)
    {
         sampleid = Request.QueryString["sampleid"].ToString();

        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con2.Open();
       


        string strTable = "";



        string sqlL = "select TOP 1 * from YangPin2Detail where sampleid='" + sampleid + "' and state='借出' order by id desc";

        SqlDataAdapter adL = new SqlDataAdapter(sqlL, con2);
        DataSet dsL = new DataSet();
        adL.Fill(dsL);
        DataTable dtL = dsL.Tables[0];





        string sql = "select  * from yangpin2 where sampleid='" + sampleid + "' order by id";

        SqlDataAdapter ad = new SqlDataAdapter(sql, con2);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DataTable dt = ds.Tables[0];


        string sql3 = "select  * from YaoPinManage where picihao='" + sampleid + "' order by id";

        SqlDataAdapter ad3 = new SqlDataAdapter(sql3, con2);
        DataSet ds3 = new DataSet();
        ad3.Fill(ds3);
        DataTable dt3 = ds3.Tables[0];


        rwbianhao = dt.Rows[0]["anjianid"].ToString();


        string sqly = "select  * from anjianinfo2 where rwbianhao='" + rwbianhao + "' order by id";

        SqlDataAdapter ady = new SqlDataAdapter(sqly, con2);
        DataSet dsy = new DataSet();
        ady.Fill(dsy);
        DataTable dty = dsy.Tables[0];

        wancheng = dsy.Tables[0].Rows[0]["beizhu3"].ToString();
        shenqingbianhao = dsy.Tables[0].Rows[0]["shenqingbianhao"].ToString();
        zhizao = dsy.Tables[0].Rows[0]["zhizaodanwei"].ToString();
        state = dsy.Tables[0].Rows[0]["state"].ToString();
        con2.Close();
        //strTable += "<table width=\"97%\"  border=\"0\" align=\"center\"><tr><td  align=\"left\"><img src='../Images/002.jpg' /></td><td align=\"center\" class=\"F_size16  \">中检集团南方电子产品测试（深圳）有限公司</br><span class=\"F_size19\">CCIC Southern Electronic Product Testing（Shenzhen）Co., Ltd.</span></td></tr></table>";
        //strTable += "<br/>";

        name = dt.Rows[0]["name"].ToString();
        xinghao = dt.Rows[0]["model"].ToString();

        if (dtL.Rows.Count > 0)
        {
            gongchengshi = dtL.Rows[0]["name"].ToString();
        }


        if (dt.Rows[0]["state"].ToString()!="借出")
        {
            lblTable.Visible = true;
            lblTable.Text = "该样品未借出,请不要打印";
            lblTable.ForeColor = Color.Red;
        }
        else if (dsy.Tables[0].Rows[0]["state"].ToString() != "完成" && dsy.Tables[0].Rows[0]["state"].ToString() != "关闭")
        {
            lblTable.Visible = true;
            lblTable.Text = "该样品所在的任务未完成,请不要打印";
            lblTable.ForeColor = Color.Red;
        }



        strTable += "<table width=\"97%\" height=35 border=\"0\" align=\"center\"><tr><td align=\"center\" style ='' class=\"F_size17 F_B \">样品流转清单</td></tr></table>";

        strTable += "<br/>";
        strTable += "<table width=\"97%\" align=\"center\" border=\"1\">";
       
        strTable += "<tr height=27><td align=\"center\"><span style='font-weight :bold ;'>样品编号：</span></td><td align=\"left\"><span style='font-weight :bold ;'>"+sampleid+"</span></td><td align=\"center\"><span style='font-weight :bold ;'>任务编号：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt.Rows[0]["anjianid"].ToString() + "</span></td></tr>";


        strTable += "</table>";
       
       

        strTable += "<table width=\"97%\" align=\"center\" border=\"1\">";

        strTable += "<tr height=27>";

        strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>序号</span></td>";
        strTable += "<td align=\"center\" width=\"35%\"><span style='font-weight :bold ;'>样品(配件)名称</span></td>";
        strTable += "<td align=\"center\" width=\"35%\"><span style='font-weight :bold ;'>型号规格</span></td>";
        strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>数量</span></td>";
     

        strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>备注</span></td>";
        strTable += "</tr>";

        strTable += "<tr height=27>";

        strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>0</span></td>";
        strTable += "<td align=\"center\" width=\"35%\"><span style='font-weight :bold ;'>" + dt.Rows[0]["name"].ToString() + "</span></td>";
        strTable += "<td align=\"center\" width=\"35%\"><span style='font-weight :bold ;'>" + dt.Rows[0]["model"].ToString() + "</span></td>";
        strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>" + dt.Rows[0]["count"].ToString() + "</span></td>";


        strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>" + dt.Rows[0]["remark"].ToString() + "</span></td>";
        strTable += "</tr>";


        for (int z = 0; z < ds3.Tables[0].Rows.Count; z++)
        {    
            int m = z + 1;
            strTable += "<tr height=27>";
            strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>"+m+"</span></td>";
            strTable += "<td align=\"center\" width=\"35%\"><span style='font-weight :bold ;'>" + dt3.Rows[z]["yaopinname"].ToString() + "</span></td>";
            strTable += "<td align=\"center\" width=\"35%\"><span style='font-weight :bold ;'>" + dt3.Rows[z]["guige"].ToString() + "</span></td>";
            strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>" + dt3.Rows[z]["jiliang"].ToString() + "</span></td>";
            strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>" + dt3.Rows[z]["remark"].ToString() + "</span></td>";
            strTable += "</tr>";
        }

        strTable += "<tr height=30>";
        strTable += "<td align=\"left\" colspan=\"5\" width=\"100%\"><span style='font-weight :bold ;'>保管要求："+dt.Rows[0]["pub_field4"].ToString()+"</span></td>";
        strTable += "</tr>";

        strTable += "<tr height=30>";
        strTable += "<td align=\"left\" colspan=\"5\" width=\"100%\"><span style='font-weight :bold ;'>最终处置：" + dt.Rows[0]["pub_field5"].ToString() + "</span></td>";
        strTable += "</tr>";


        strTable += "</table>";


        strTable += "<table width=\"97%\" align=\"center\" border=\"1\">";
        strTable += "<tr height=27>";

        strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>序号</span></td>";
        strTable += "<td align=\"center\" width=\"15%\"><span style='font-weight :bold ;'>接收人签名</span></td>";
        strTable += "<td align=\"center\" width=\"15%\"><span style='font-weight :bold ;'>接收日期</span></td>";
        strTable += "<td align=\"center\" width=\"30%\"><span style='font-weight :bold ;'>检测项目/样品用途</span></td>";


        strTable += "<td align=\"center\" width=\"30%\"><span style='font-weight :bold ;'>备注</span></td>";
        strTable += "</tr>";


        for (int z = 0; z < 10; z++)
        {
            int m = z + 1;
            strTable += "<tr height=27>";
            strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>" + m + "</span></td>";
            strTable += "<td align=\"center\" width=\"15%\"><span style='font-weight :bold ;'></span></td>";
            strTable += "<td align=\"center\" width=\"15%\"><span style='font-weight :bold ;'></span></td>";
            strTable += "<td align=\"center\" width=\"30%\"><span style='font-weight :bold ;'></span></td>";
            strTable += "<td align=\"center\" width=\"30%\"><span style='font-weight :bold ;'></span></td>";
            strTable += "</tr>";
        }


        strTable += "<tr height=20>";
        strTable += "<td align=\"left\" colspan=\"5\" width=\"100%\"><span style='font-weight :bold ;'>注：本表随样品流转，请注意保管。样品退库时将本表交由样品管理人员存档。</span></td>";
        strTable += "</tr>";
        strTable += "</table>";

      



       

       // lblTable.Text = strTable;
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