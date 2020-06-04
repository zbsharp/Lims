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

public partial class Print_YangPinPrint : System.Web.UI.Page
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
        string sql = "select *,(select model from YangPin2 where sampleid='" + sampleid + "') as [type] from YaoPinManage where picihao='" + sampleid + "' order by id asc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        string sql2 = "select * from YangPin2 where sampleid='" + sampleid + "' order by id asc";
        SqlDataAdapter da2 = new SqlDataAdapter(sql2, con);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);

        string sqlcustomer = " select Responser from Customer where CustomName like '%" + ds2.Tables[0].Rows[0]["kehuname"].ToString() + "%'";
        SqlCommand com = new SqlCommand(sqlcustomer, con);
        SqlDataReader dr = com.ExecuteReader();
        string customer = "";
        if (dr.Read())
        {
            customer = dr["Responser"].ToString();
        }
        dr.Close();

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


        string px = "10px";

        for (int i = 0; i < zs; i++)
        {
            strTable += "<table width=\"260\"  border=\"1\" height=\"160\"  valign=\"middle\" align=\"center\">";
            strTable += "<tr height=24>";
            strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>样品号</span></td>";
            strTable += "<td colspan='3' align=\"center\" style='font-size :" + px + ";' ><img src='../Handler2.ashx?code=" + sampleid + "&height=15' /><span style='font-weight :bold ;font-size :larger;float :right ;'>" + ds2.Tables[0].Rows[0]["pub_field2"].ToString() + "</span></td>";
            strTable += "</tr>";

            if (ds2.Tables[0].Rows[0]["pub_field3"].ToString() == "实验室销毁")
            {
                strTable += "<tr height=18>";
                strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>任务号</span></td>";
                strTable += "<td colspan='3' align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds2.Tables[0].Rows[0]["anjianid"].ToString() + "</span></td>";
                strTable += "</tr>";
            }
            else
            {
                strTable += "<tr height=18>";
                strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>任务号</span></td>";
                strTable += "<td colspan='3' align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds2.Tables[0].Rows[0]["anjianid"].ToString() + "</span></td>";
                strTable += "</tr>";
            }

            strTable += "<tr height=18>";
            strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>样品名称</span></td>";
            strTable += "<td colspan='3' align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds2.Tables[0].Rows[0]["name"].ToString() + "</span></td>";
            strTable += "</tr>";

            strTable += "<tr height=18>";
            strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>样品型号</span></td>";
            strTable += "<td colspan='3' align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds2.Tables[0].Rows[0]["model"].ToString() + "</span></td>";
            strTable += "</tr>";

            strTable += "<tr height=18>";
            strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>制造厂商</span></td>";
            strTable += "<td colspan='3' align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds2.Tables[0].Rows[0]["position"].ToString() + "</span></td>";
            strTable += "</tr>";

            strTable += "<tr height=18>";
            strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>状态</span>";
            strTable += "<td colspan='3' align =\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'><input type=\"checkbox\">待测&nbsp;&nbsp;&nbsp;<input type=\"checkbox\">测中&nbsp;&nbsp;&nbsp;<input type=\"checkbox\">已测</span>";
            strTable += "</tr>";

            strTable += "<tr height=18>";
            strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>业务员</span>";
            strTable += "<td ><span style='font-weight :bold ;font-size :" + px + ";'>" + customer + "</span></td>";
            strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>备注</span>";
            strTable += "<td ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds2.Tables[0].Rows[0]["beizhu1"].ToString() + "</span></td>";
            strTable += "</tr>";

            strTable += "</table>";

            strTable += "<div class=\"PageNext\"></div>";

            //if (i != zs - 1)
            //{
            //    strTable += "<div class=\"PageNext\"></div>";
            //}
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
                        strTable += "<table width=\"260\"  border=\"1\" height=\"160\"  valign=\"middle\" align=\"center\">";

                        strTable += "<tr height=40>";
                        strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>待试</span></td>";
                        strTable += "<td align=\"center\" ><img src='../Handler2.ashx?code=" + ds.Tables[0].Rows[z]["picihaobianhao"].ToString() + "&height=15' /></td>";
                        strTable += "</tr>";

                        strTable += "<tr height=30>";
                        strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>任务号</span></td>";
                        strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds2.Tables[0].Rows[0]["anjianid"].ToString() + "</span></td>";
                        strTable += "</tr>";

                        //strTable += "<tr height=20>";
                        //strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>申请编号</span></td>";
                        //strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + qq + "</span></td>";
                        //strTable += "</tr>";

                        strTable += "<tr height=30>";
                        strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>样品名称</span></td>";
                        strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds.Tables[0].Rows[z]["yaopinname"].ToString() + "</span></td>";
                        strTable += "</tr>";

                        strTable += "<tr height=30>";
                        strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>样品型号</span></td>";
                        strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds.Tables[0].Rows[z]["type"].ToString() + "</span></td>";
                        strTable += "</tr>";

                        strTable += "<tr height=30>";
                        strTable += "<td align=\"center\" width=\"50\"><span style='font-weight :bold ;font-size :" + px + ";'>制造厂商</span></td>";
                        strTable += "<td align=\"center\" ><span style='font-weight :bold ;font-size :" + px + ";'>" + ds.Tables[0].Rows[z]["shengchanchangjia"].ToString() + "</span></td>";
                        strTable += "</tr>";

                        strTable += "</table>";
                        strTable += "<div class=\"PageNext\"></div>";
                        //if (q != Convert.ToInt32(dd) - 1)
                        //{
                        //    strTable += "<div class=\"PageNext\"></div>";
                        //}
                    }

                }


            }
        }
        string html = strTable.Substring(0, strTable.Length - 28);
        lblTable.Text = html;
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
        strTemp = strTemp.Replace("0", "_|_|__||_||_|");
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