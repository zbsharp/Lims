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

public partial class Print_TaskPrint : System.Web.UI.Page
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
        string sql = "select kehuid,baojiaid,fillname from anjianxinxi2 where bianhao='" + tijiaobianhao + "'";
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

        int aaa3 = dt3.Rows.Count;
        if (aaa3 == 0)
        {

            string sqlde3 = "delete from anjianxinxi2 where baojiaid='" + baojiaid + "'";
            SqlCommand cmdde3 = new SqlCommand(sqlde3, con2);
            cmdde3.ExecuteNonQuery();
            con2.Close();
            Response.Write("<script>alert('该项目所在报价单录入不符合要求，已请求作废处理');window.location.href='../Customer/CustManage.aspx'</script>");
            return;

        }


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
            Response.Write("<script>alert('该报价需重新添加联系人');</script>");
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

        string sql9 = "select * from baogao2 where rwid='" + tijiaobianhao + "'";

        SqlDataAdapter ad9 = new SqlDataAdapter(sql9, con2);
        DataSet ds9 = new DataSet();
        ad9.Fill(ds9);
        DataTable dt9 = ds9.Tables[0];

        int baogaocount = dt9.Rows.Count;

        string baogaohao = "";

        if (baogaocount > 0)
        {
            baogaohao = dt9.Rows[0]["baogaoid"].ToString();
        }


        string sql10 = "select * from anjianinfo2 where bianhao='" + tijiaobianhao + "'";

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

        string sql11 = "select * from anjianinfo2 where bianhao='" + tijiaobianhao + "'";

        SqlDataAdapter ad11 = new SqlDataAdapter(sql11, con2);
        DataSet ds11 = new DataSet();
        ad11.Fill(ds11);
        DataTable dt11 = ds11.Tables[0];



        string sql112 = "select  bumen from anjianinfo where taskid='" + dt10.Rows[0]["rwbianhao"].ToString() + "'";

        SqlDataAdapter ad112 = new SqlDataAdapter(sql112, con2);
        DataSet ds112 = new DataSet();
        ad112.Fill(ds112);
        DataTable dt112 = ds112.Tables[0];
        string zhujian = "";
        for (int z = 0; z < dt112.Rows.Count; z++)
        {
            zhujian = zhujian + dt112.Rows[z]["bumen"].ToString() + ",";
        }
        if (zhujian.Length > 1)
        {
            zhujian = zhujian.Substring(0, zhujian.Length - 1);
        }
        //strTable += "<table width=\"97%\"  border=\"0\" align=\"center\"><tr><td  align=\"left\"><img src='../Images/002.jpg' /></td><td align=\"center\" class=\"F_size16  \">中检集团南方电子产品测试（深圳）有限公司</br><span class=\"F_size19\">CCIC Southern Electronic Product Testing（Shenzhen）Co., Ltd.</span></td></tr></table>";
        //strTable += "<br/>";
        con2.Close();

        strTable += "<div style=\"border-style:solid; border-width :thin;\">";

        strTable += "<table width=\"97%\" height=35 border=\"0\" align=\"center\"><tr><td align=\"center\" style ='' class=\"F_size17 F_B \">工作任务通知书</td></tr></table>";
        strTable += "<table width=\"97%\" height=35 border=\"0\" align=\"center\"><tr><td wids1h=\"43%\">&nbsp;</td><td  class=\"F_size16 F_B \"></td><td  align=\"right\">任务编号：" + dt10.Rows[0]["rwbianhao"].ToString() + "</td></tr></table>";

        strTable += "<table width=\"97%\" height=35 border=\"0\" align=\"center\"><tr><td wids1h=\"43%\">&nbsp;</td><td  class=\"F_size16 F_B \"></td><td  align=\"right\">优先级：" + dt10.Rows[0]["youxian"].ToString() + "</td></tr></table>";
        strTable += "<br/>";
        strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=35><td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'>样品名称：</span></td><td align=\"left\" width=\"38%\"><span style='font-weight :bold ;'>" + (dt10.Rows[0]["name"].ToString()) + "</span></td><td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'>制造厂商：</span></td><td align=\"left\" width=\"38%\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["zhizaodanwei"].ToString() + "</span></td></tr>";
        strTable += "<tr height=35><td align=\"center\"><span style='font-weight :bold ;'>产品型号：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + (dt10.Rows[0]["xinghao"].ToString()) + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>委托单位：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["weituodanwei"].ToString() + "</span></td></tr>";
        strTable += "<tr height=35><td align=\"center\"><span style='font-weight :bold ;'>生产单位：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["shengchandanwei"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>付款单位：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["fukuandanwei"].ToString() + "</span></td></tr>";

        strTable += "<tr height=35><td align=\"center\"><span style='font-weight :bold ;'>商&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;标：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["shangbiao"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>取样方式：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["quyangfangshi"].ToString() + "</span></td></tr>";


        //strTable += "<tr height=35><td align=\"center\"><span style='font-weight :bold ;'>样品数量：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["num"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>抽样单位：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["chouyangdanwei"].ToString() + "</span></td></tr>";
        //strTable += "<tr height=35><td align=\"center\"><span style='font-weight :bold ;'>送检日期：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["xiadariqi"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>抽样母数：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["chouyangmutinum"].ToString() + "</span></td></tr>";

        //strTable += "<tr height=35><td align=\"center\"><span style='font-weight :bold ;'>生产日期：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt10.Rows[0]["shencriqi"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>抽样日期：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["chouyangdate"].ToString() + "</span></td></tr>";

        //strTable += "<tr height=35><td align=\"center\"><span style='font-weight :bold ;'>检测类别：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt10.Rows[0]["shiyanleibie"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>抽样地点：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["chouyangdidian"].ToString() + "</span></td></tr>";

        strTable += "<tr height=35><td align=\"center\"><span style='font-weight :bold ;'>申请编号：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt10.Rows[0]["shenqingbianhao"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>报告编号：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + baogaohao + "</span></td></tr>";









        strTable += "</table>";

        strTable += "<br/>";
        strTable += "<table width=\"97%\" height=35 border=\"1\" align=\"center\"><tr><td align=\"center\" style ='' class=\"F_size17 F_B \">检测项目和技术要求</td></tr></table>";


        strTable += "<table width=\"97%\" align=\"center\" border=\"1\">";

        strTable += "<tr height=35>";

        strTable += "<td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'>序号</span></td>";
        strTable += "<td align=\"center\" width=\"38%\"><span style='font-weight :bold ;'>测试项目</span></td>";
        strTable += "<td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'>技术要求</span></td>";

        strTable += "<td align=\"center\" width=\"38%\"><span style='font-weight :bold ;'>备注</span></td>";
        strTable += "</tr>";

        for (int z = 0; z < dt2count; z++)
        {

            int m = z + 1;
            strTable += "<tr height=35>";

            strTable += "<td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'>" + m + "</span></td>";
            strTable += "<td align=\"center\" width=\"38%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["ceshiname"].ToString() + "</span></td>";
            strTable += "<td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["biaozhun"].ToString() + "</span></td>";

            strTable += "<td align=\"center\" width=\"38%\"><span style='font-weight :bold ;'>(留样：是&nbsp;&nbsp;&nbsp;&nbsp;否)" + dt2.Rows[z]["beizhu"].ToString() + "</span></td>";
            strTable += "</tr>";


        }


        for (int z = 0; z < 2; z++)
        {

            int m = z + 1;
            strTable += "<tr height=35>";

            strTable += "<td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'></span></td>";
            strTable += "<td align=\"center\" width=\"38%\"><span style='font-weight :bold ;'></span></td>";
            strTable += "<td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'></span></td>";

            strTable += "<td align=\"center\" width=\"38%\"><span style='font-weight :bold ;'></span></td>";
            strTable += "</tr>";


        }



        strTable += "</table>";

        strTable += "<br/>";



        strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=35><td align=\"center\" width=\"12%\" ><span style='font-weight :bold ;'>业务员：&nbsp;&nbsp;&nbsp;&nbsp;</span></td><td align=\"left\" width=\"38%\"><span style='font-weight :bold ;'>" + dt3.Rows[0]["fillname"].ToString() + "</span></td><td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'>项目经理：</span></td><td align=\"left\" width=\"38%\"><span style='font-weight :bold ;'>" + dt10.Rows[0]["kf"].ToString() + "</span></td></tr>";

        strTable += " <tr height=35><td align=\"center\" width=\"12%\" ><span style='font-weight :bold ;'>联系人：&nbsp;&nbsp;&nbsp;&nbsp;</span></td><td align=\"left\" width=\"38%\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["name"].ToString() + "</span></td><td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'>承接部门：</span></td><td align=\"left\" width=\"38%\"><span style='font-weight :bold ;'>" + zhujian + "</span></td></tr>";

        strTable += "<tr height=35><td align=\"center\" ><span style='font-weight :bold ;'>联系电话：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["telephone"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>主检工程师：</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td></tr>";

        strTable += "<tr height=35><td align=\"center\"><span style='font-weight :bold ;'>电子邮件：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["email"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>任务下达日期：</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td></tr>";

        //" + dt10.Rows[0]["xiadariqi"].ToString() + "

        strTable += "<tr height=35><td align=\"center\"><span style='font-weight :bold ;'>客户要求：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt10.Rows[0]["qitayaoqiu"].ToString() + "</span></td><td align=\"center\"><span style='font-weight :bold ;'>要求完成日期：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt10.Rows[0]["yaoqiuwanchengriqi"].ToString() + "</span></td></tr>";





        strTable += "</table>";

        strTable += "<br/>";

        strTable += "<table width=\"97%\" align=\"center\" border=\"0\">";

        strTable += "<tr height=35>";

        strTable += "<td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'></span></td>";
        strTable += "<td align=\"center\" width=\"38%\"><span style='font-weight :bold ;'></span></td>";
        strTable += "<td align=\"center\" width=\"12%\"><span style='font-weight :bold ;'>编制人：</span></td>";

        strTable += "<td align=\"center\" width=\"38%\"><span style='font-weight :bold ;'>" + dt10.Rows[0]["fillname"].ToString() + "</span></td>";
        strTable += "</tr>";

        strTable += "</table>";

        strTable += "</div>";

        con2.Close();

        lblTable.Text = strTable;
    }

    public string SubStr(string sString, int nLeng)
    {
        if (sString.Length <= nLeng)
        {
            return sString;
        }
        string sNewStr = sString.Substring(0, nLeng);

        return sNewStr;
    }

}