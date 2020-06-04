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

public partial class Print_TaskBiaoQ : System.Web.UI.Page
{
    protected string sampleid = "";
    protected string sampleidz = "";


    protected int zs = 1;
    protected int ps = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        sampleid = Request.QueryString["taskid"].ToString();
       
        string strTable = "";

        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con2.Open();

        string sql10 = "select * from anjianinfo2 where rwbianhao='" + sampleid + "'";

        SqlDataAdapter ad10 = new SqlDataAdapter(sql10, con2);
        DataSet ds10 = new DataSet();
        ad10.Fill(ds10);
        DataTable dt10 = ds10.Tables[0];

        int aaa2 = dt10.Rows.Count;

        con2.Close();

      
            strTable += "<table width=\"100%\"  border=\"0\" align=\"center\">";

           


            strTable += "<tr height=20>";
            strTable += "<td align=\"center\" colspan=\"3\"  ><img src='../Handler.ashx?code=" + sampleid + "&height=20'' /></td>";
            strTable += "</tr>";

            strTable += "<tr height=20>";
            strTable += "<td align=\"center\" colspan=\"3\"  >"+dt10.Rows[0]["shenqingbianhao"].ToString()+"</td>";
            strTable += "</tr>";


            strTable += "</table>";
            

        

       

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