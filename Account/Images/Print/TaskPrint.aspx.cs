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
        string sql = "select kehuid,baojiaid,responser from BaoJiaCPXiangMu where tijiaohaoma='" + tijiaobianhao + "'";
        SqlCommand cmd = new SqlCommand(sql, con2);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            baojiaid = dr["baojiaid"].ToString();
            kehuid = dr["kehuid"].ToString();
            responser = dr["responser"].ToString();
        }
        dr.Close();


        string strTable = "";


        string sql1 = "select  * from BaoJiaChanPing where baojiaid='" + baojiaid + "' order by id";

        SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con2);
        DataSet ds1 = new DataSet();
        ad1.Fill(ds1);
        DataTable dt1 = ds1.Tables[0];
        int dt1count = dt1.Rows.Count;

        string sql2 = "select  * from BaoJiaCPXiangMu where tijiaohaoma='" + tijiaobianhao + "' order by id";

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

        string sql6 = "select * from CustomerLinkMan where customerid='" + kehuid + "' order by  id desc";

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

        string sql9 = "select * from yangpin where bianhao='" + tijiaobianhao + "'";

        SqlDataAdapter ad9 = new SqlDataAdapter(sql9, con2);
        DataSet ds9 = new DataSet();
        ad9.Fill(ds9);
        DataTable dt9 = ds9.Tables[0];

        string sql10 = "select * from anjianinfo2 where bianhao='" + tijiaobianhao + "'";

        SqlDataAdapter ad10 = new SqlDataAdapter(sql10, con2);
        DataSet ds10= new DataSet();
        ad10.Fill(ds10);
        DataTable dt10 = ds10.Tables[0];

        string sql11 = "select * from KeHuLianXi where baojiaid='" + baojiaid + "'";

        SqlDataAdapter ad11 = new SqlDataAdapter(sql11, con2);
        DataSet ds11 = new DataSet();
        ad11.Fill(ds11);
        DataTable dt11 = ds11.Tables[0];


        strTable += "<table width=\"97%\"  border=\"0\" align=\"center\"><tr><td  align=\"left\"><img src='../Images/002.jpg' /></td><td align=\"center\" class=\"F_size16  \">中检集团南方电子产品测试（深圳）有限公司</br><span class=\"F_size19\">CCIC Southern Electronic Product Testing（Shenzhen）Co., Ltd.</span></td></tr></table>";
        strTable += "<br/>";
        strTable += "<table width=\"97%\" height=30 border=\"0\" align=\"center\"><tr><td align=\"center\" style ='' class=\"F_size17 F_B \">任务通知书</td></tr></table>";
        strTable += "<table width=\"97%\" height=30 border=\"0\" align=\"center\"><tr><td wids1h=\"43%\">&nbsp;</td><td  class=\"F_size16 F_B \"></td><td  align=\"right\">任务编号：" + dt10.Rows[0]["rwbianhao"].ToString() + "</td></tr></table>";

        strTable += "<table width=\"97%\" height=30 border=\"0\" align=\"center\"><tr><td wids1h=\"43%\">&nbsp;</td><td  class=\"F_size16 F_B \"></td><td  align=\"right\">优先级：" + dt10.Rows[0]["youxian"].ToString() + "</td></tr></table>";
        strTable += "<br/>";
        strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=27><td align=\"left\" width=\"10%\"><span style='font-weight :bold ;'>样品名称</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["name"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>制造厂商</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["name"].ToString() + "</span></td></tr>";
        strTable += "<tr height=27><td align=\"left\" width=\"10%\"><span style='font-weight :bold ;'>To:样品型号：</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["xinghao"].ToString() + "</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>委托单位：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt11.Rows[0]["name"].ToString() + "</span></td></tr>";

        strTable += "<tr height=27><td align=\"left\" width=\"10%\"><span style='font-weight :bold ;'>商标：</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["shangbiao"].ToString() + "</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>取样方式：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["quyang"].ToString() + "</span></td></tr>";


        strTable += "<tr height=27><td align=\"left\" width=\"10%\"><span style='font-weight :bold ;'>样品数量：</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["shuliang"].ToString() + "</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>抽样单位：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["chouyangdanwei"].ToString() + "</span></td></tr>";
        strTable += "<tr height=27><td align=\"left\" width=\"10%\"><span style='font-weight :bold ;'>送检日期：</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["songjianriqi"].ToString() + "</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>抽样母体数：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["chouyangmuti"].ToString() + "</span></td></tr>";

        strTable += "<tr height=27><td align=\"left\" width=\"10%\"><span style='font-weight :bold ;'>生产日期：</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["shengchanriqi"].ToString() + "</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>抽样日期：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["chouyangriqi"].ToString() + "</span></td></tr>";

        strTable += "<tr height=27><td align=\"left\" width=\"10%\"><span style='font-weight :bold ;'>检测类别：</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>" + dt10.Rows[0]["shiyanleibie"].ToString() + "</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>抽样地点：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["chouyangdidian"].ToString() + "</span></td></tr>";

        strTable += "<tr height=27><td align=\"left\" width=\"10%\"><span style='font-weight :bold ;'>申请编号：</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>" + dt10.Rows[0]["shenqingbianhao"].ToString() + "</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>报告编号：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt10.Rows[0]["baogao"].ToString() + "</span></td></tr>";





      



        strTable += "</table>";

        strTable += "<br/>";
        strTable += "<table width=\"97%\" height=30 border=\"0\" align=\"center\"><tr><td align=\"center\" style ='' class=\"F_size17 F_B \">检测项目和技术要求</td></tr></table>";


        strTable += "<table width=\"97%\" align=\"center\" border=\"1\">";
        
        strTable += "<tr height=27>";

        strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>序号</span></td>";
        strTable += "<td align=\"center\" width=\"30%\"><span style='font-weight :bold ;'>测试项目</span></td>";
        strTable += "<td align=\"center\" width=\"30%\"><span style='font-weight :bold ;'>技术要求</span></td>";
      
        strTable += "<td align=\"center\" width=\"30%\"><span style='font-weight :bold ;'>备注</span></td>";
        strTable += "</tr>";

        for (int z = 0; z < dt2count; z++)
        {


            strTable += "<tr height=27>";

            strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>" + z + 1 + "</span></td>";
            strTable += "<td align=\"center\" width=\"30%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["ceshiname"].ToString() + "</span></td>";
            strTable += "<td align=\"center\" width=\"30%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["jishuyaoqiu"].ToString() + "</span></td>";
         
            strTable += "<td align=\"center\" width=\"30%\"><span style='font-weight :bold ;'>" + dt2.Rows[z]["beizhu"].ToString() + "</span></td>";
            strTable += "</tr>";


        }

        strTable += "</table>";

        strTable += "<br/>";

        strTable += "<table width=\"97%\" align=\"center\" border=\"1\"><tr height=27><td align=\"left\" width=\"10%\" ><span style='font-weight :bold ;'>联系人</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["name"].ToString() + "</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>任务承接人：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt10.Rows[0]["chenjieren"].ToString() + "</span></td></tr>";
        strTable += "<tr height=27><td align=\"left\" width=\"10%\" ><span style='font-weight :bold ;'>联系电话：</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["telephone"].ToString() + "</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>检测负责人：</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td></tr>";

        strTable += "<tr height=27><td align=\"left\" width=\"10%\"><span style='font-weight :bold ;'>电子邮件：</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["email"].ToString() + "</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>任务下达日期：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt10.Rows[0]["xiadariqi"].ToString() + "</span></td></tr>";



        strTable += "<tr height=27><td align=\"left\" width=\"10%\"><span style='font-weight :bold ;'>客户要求：</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>" + dt9.Rows[0]["shangbiao"].ToString() + "</span></td><td align=\"left\" width=\"30%\"><span style='font-weight :bold ;'>要求完成日期：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt10.Rows[0]["yaoqiuwanchengriqi"].ToString() + "</span></td></tr>";





        strTable += "</table>";
   

        con2.Close();

        lblTable.Text = strTable;
    }
}