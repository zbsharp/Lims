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

public partial class Print_YangPinPrint2 : System.Web.UI.Page
{
    protected string sampleid = "";
    protected string sampleidz = "";


    protected int zs = 1;
    protected int ps = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        sampleid = Request.QueryString["sampleid"].ToString();
        sampleidz = Request.QueryString["sampleidz"].ToString();
        zs = Convert.ToInt32(Request.QueryString["zs"]);
        ps = Convert.ToInt32(Request.QueryString["ps"]);








        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from YaoPinManage where picihao='" + sampleid + "' order by id asc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        string sql2 = "select * from YangPin2 where sampleid='" + sampleid + "' order by id asc";
        SqlDataAdapter da2 = new SqlDataAdapter(sql2, con);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);


        string sql3 = "select * from anjianinfo2 where rwbianhao='" + ds2.Tables[0].Rows[0]["anjianid"].ToString() + "' order by id asc";
        SqlDataAdapter da3 = new SqlDataAdapter(sql3, con);
        DataSet ds3 = new DataSet();
        da3.Fill(ds3);
        con.Close();
        string qq = "";
        if (ds3.Tables[0].Rows.Count > 0)
        {
            qq = ds3.Tables[0].Rows[0]["shenqingbianhao"].ToString();
        }
        else
        {
            qq = "无";
        }

        string strTable = "";


        string px = "12px";

        for (int i = 0; i < zs; i++)
        {



            strTable += "<table width=\"275\"  border=\"1\" height=\"150\"  valign=\"middle\" align=\"center\">";


            strTable += "<tr height=40>";
            strTable += "<td align=\"center\" width=\"30\"><span style='font-weight :bold ;font-size :" + px + ";'>待试</span></td>";
            strTable += "<td align=\"center\" style='font-size :" + px + ";' >" + BarCodeToHTML.get39("1307796", int.Parse("2"), int.Parse("15")) + "</td>";
            strTable += "</tr>";

            strTable += "<tr height=20>";
            strTable += "<td align=\"center\" width=\"30\"><span style='font-weight :bold ;font-size :" + px + ";'>任务</span></td>";
            strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds2.Tables[0].Rows[0]["anjianid"].ToString() + "</span></td>";
            strTable += "</tr>";

            strTable += "<tr height=20>";
            strTable += "<td align=\"center\" width=\"30\"><span style='font-weight :bold ;font-size :" + px + ";'>编号</span></td>";
            strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + qq + "</span></td>";
            strTable += "</tr>";

            strTable += "<tr height=20>";
            strTable += "<td align=\"center\" width=\"30\"><span style='font-weight :bold ;font-size :" + px + ";'>名称</span></td>";
            strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds2.Tables[0].Rows[0]["name"].ToString() + "</span></td>";
            strTable += "</tr>";

            strTable += "<tr height=20>";
            strTable += "<td align=\"center\" width=\"30\"><span style='font-weight :bold ;font-size :" + px + ";'>型号</span></td>";
            strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds2.Tables[0].Rows[0]["model"].ToString() + "</span></td>";
            strTable += "</tr>";

            strTable += "<tr height=20>";
            strTable += "<td align=\"center\" width=\"30\"><span style='font-weight :bold ;font-size :" + px + ";'>厂商</span></td>";
            strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds2.Tables[0].Rows[0]["position"].ToString() + "</span></td>";
            strTable += "</tr>";

            strTable += "</table>";
            strTable += "<div class=\"PageNext\"></div>";






        }
        if (ds.Tables[0].Rows.Count > 0)
        {



            for (int i = 0; i < ps; i++)
            {




                for (int z = 0; z < ds.Tables[0].Rows.Count; z++)
                {


                    string dd = "0";
                    string EMCbiaozhun1 = sampleidz.ToString();
                    string[] EMCbiaozhun2 = EMCbiaozhun1.Split('|');
                    foreach (string str in EMCbiaozhun2)
                    {
                        dd = EMCbiaozhun2[z].ToString();
                    }


                    for (int q = 0; q < Convert.ToInt32(dd); q++)
                    {




                        strTable += "<table width=\"260\"  border=\"1\" height=\"150\"  valign=\"middle\" align=\"center\">";


                        strTable += "<tr height=40>";
                        strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>待试</span></td>";
                        strTable += "<td align=\"center\" >" + BarCodeToHTML.get39("13-12345", int.Parse("1"), int.Parse("18")) + "</td>";
                        strTable += "</tr>";

                        strTable += "<tr height=20>";
                        strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>任务号</span></td>";
                        strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds2.Tables[0].Rows[0]["anjianid"].ToString() + "</span></td>";
                        strTable += "</tr>";

                        strTable += "<tr height=20>";
                        strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>申请编号</span></td>";
                        strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + qq + "</span></td>";
                        strTable += "</tr>";

                        strTable += "<tr height=20>";
                        strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>样品名称</span></td>";
                        strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds.Tables[0].Rows[z]["yaopinname"].ToString() + "</span></td>";
                        strTable += "</tr>";

                        strTable += "<tr height=20>";
                        strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>样品型号</span></td>";
                        strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds.Tables[0].Rows[z]["guige"].ToString() + "</span></td>";
                        strTable += "</tr>";

                        strTable += "<tr height=20>";
                        strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>制造厂商</span></td>";
                        strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds.Tables[0].Rows[z]["shengchanchangjia"].ToString() + "</span></td>";
                        strTable += "</tr>";

                        strTable += "</table>";
                        strTable += "<div class=\"PageNext\"></div>";

                    }

                }


            }
        }

        lblTable.Text = strTable;
    }


    /// <summary>
    /// 条形码生成
    /// </summary>
    /// <param name="strTemp">要生成条形码的文本</param>
    /// <param name="height">每个_和|的高度</param>
    /// <param name="width">每个_和|的宽度</param>
    /// <param name="showstrTemp">是否显示文本</param>
    /// <example>Response.Write(CreateBarCode("6911989251236", 50, 1, true));</example>
    /// <returns></returns>
    public string CreateBarCode(string text, int height, int width, bool showText)
    {
        string strTemp = text.ToLower();

        //替换各个字符
        strTemp = strTemp.Replace("0", "_|_|__||_||_|"); ;
        strTemp = strTemp.Replace("1", "_||_|__|_|_||");
        strTemp = strTemp.Replace("2", "_|_||__|_|_||");
        strTemp = strTemp.Replace("3", "_||_||__|_|_|");
        strTemp = strTemp.Replace("4", "_|_|__||_|_||");
        strTemp = strTemp.Replace("5", "_||_|__||_|_|");
        strTemp = strTemp.Replace("7", "_|_|__|_||_||");
        strTemp = strTemp.Replace("6", "_|_||__||_|_|");
        strTemp = strTemp.Replace("8", "_||_|__|_||_|");
        strTemp = strTemp.Replace("9", "_|_||__|_||_|");
        strTemp = strTemp.Replace("a", "_||_|_|__|_||");
        strTemp = strTemp.Replace("b", "_|_||_|__|_||");
        strTemp = strTemp.Replace("c", "_||_||_|__|_|");
        strTemp = strTemp.Replace("d", "_|_|_||__|_||");
        strTemp = strTemp.Replace("e", "_||_|_||__|_|");
        strTemp = strTemp.Replace("f", "_|_||_||__|_|");
        strTemp = strTemp.Replace("g", "_|_|_|__||_||");
        strTemp = strTemp.Replace("h", "_||_|_|__||_|");
        strTemp = strTemp.Replace("i", "_|_||_|__||_|");
        strTemp = strTemp.Replace("j", "_|_|_||__||_|");
        strTemp = strTemp.Replace("k", "_||_|_|_|__||");
        strTemp = strTemp.Replace("l", "_|_||_|_|__||");
        strTemp = strTemp.Replace("m", "_||_||_|_|__|");
        strTemp = strTemp.Replace("n", "_|_|_||_|__||");
        strTemp = strTemp.Replace("o", "_||_|_||_|__|");
        strTemp = strTemp.Replace("p", "_|_||_||_|__|");
        strTemp = strTemp.Replace("r", "_||_|_|_||__|");
        strTemp = strTemp.Replace("q", "_|_|_|_||__||");
        strTemp = strTemp.Replace("s", "_|_||_|_||__|");
        strTemp = strTemp.Replace("t", "_|_|_||_||__|");
        strTemp = strTemp.Replace("u", "_||__|_|_|_||");
        strTemp = strTemp.Replace("v", "_|__||_|_|_||");
        strTemp = strTemp.Replace("w", "_||__||_|_|_|");
        strTemp = strTemp.Replace("x", "_|__|_||_|_||");
        strTemp = strTemp.Replace("y", "_||__|_||_|_|");
        strTemp = strTemp.Replace("z", "_|__||_||_|_|");
        strTemp = strTemp.Replace("-", "_|__|_|_||_||");
        strTemp = strTemp.Replace("*", "_|__|_||_||_|");
        strTemp = strTemp.Replace("/", "_|__|__|_|__|");
        strTemp = strTemp.Replace("%", "_|_|__|__|__|");
        strTemp = strTemp.Replace("+", "_|__|_|__|__|");
        strTemp = strTemp.Replace(".", "_||__|_|_||_|");

        //替换字符中的_和|
        strTemp = strTemp.Replace("_", "<span style='height:" + height + ";width:" + width + ";background:#FFFFFF;'></span>");
        strTemp = strTemp.Replace("|", "<span style='height:" + height + ";width:" + width + ";background:#000000;'></span>");

        if (showText)
        {
            return strTemp + "<br/>" + text;
        }
        else
        {
            return strTemp;
        }
    }
}