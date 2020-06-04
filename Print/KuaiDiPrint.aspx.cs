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

public partial class Print_KuaiDiPrint : System.Web.UI.Page
{
    protected string id = "";
   


    protected int zs = 1;
    protected int ps = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["bianhao"].ToString();
      
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from KuaiDi where bianhao='" + id + "' order by id asc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
     

        string strTable = "";




     


            strTable += "<table width=\"40%\"  border=\"1\" align=\"center\">";


            strTable += "<tr height=20>";
            strTable += "<td align=\"center\" width=\"70%\"><span style='font-weight :bold ;'>中检集团南方电子产品测试（深圳）有限公司</span></td>";
            strTable += "<td align=\"center\" width=\"30%\">" + ds.Tables[0].Rows[0]["jijianren"].ToString() + "</td>";
            strTable += "</tr>";

            strTable += "<tr height=20>";
            strTable += "<td align=\"center\" width=\"100%\" colspan=\"2\"><span style='font-weight :bold ;'>广东省深圳市南山区西丽沙河路电子检测大厦</span></td>";
            strTable += "</tr>";

            strTable += "<tr height=20>";
            strTable += "<td align=\"center\" width=\"30%\"><span style='font-weight :bold ;'>518055</span></td>";
            strTable += "<td align=\"center\" width=\"70%\"><span style='font-weight :bold ;'>0755-26703698</span></td>";
            strTable += "</tr>";

            strTable += "<tr height=20>";
            strTable += "<td align=\"center\" width=\"70%\"><span style='font-weight :bold ;'>" + ds.Tables[0].Rows[0]["shoujiandanwei"].ToString() + "</span></td>";
            strTable += "<td align=\"center\" width=\"30%\"><span style='font-weight :bold ;'>" + ds.Tables[0].Rows[0]["shoujianren"].ToString() + "</span></td>";
            strTable += "</tr>";

            strTable += "<tr height=20>";

            strTable += "<td align=\"center\" width=\"100%\" colspan=\"2\"><span style='font-weight :bold ;'>" + ds.Tables[0].Rows[0]["shoujiandizhi"].ToString() + "</span></td>";
            strTable += "</tr>";

            strTable += "<tr height=20>";
            strTable += "<td align=\"center\" width=\"30%\"><span style='font-weight :bold ;'></span></td>";
            strTable += "<td align=\"center\" width=\"70%\"><span style='font-weight :bold ;'>" + ds.Tables[0].Rows[0]["shoujiandianhua"].ToString() + "</span></td>";
            strTable += "</tr>";

            strTable += "</table>";
            strTable += "<div class=\"PageNext\"></div>";

            lblTable.Text = strTable;




        
       
    }

}