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
public partial class Print_QuoPrint : System.Web.UI.Page
{
    protected DateTime time1 = DateTime.Now;
    protected DateTime time2 = DateTime.Now;
    protected decimal changjianjine = 0;
    protected decimal xiachangjianjine = 0;

    protected decimal zaichangshijian = 0;
    protected decimal lvtushijian = 0;
    protected decimal buzhujine = 0;
    protected int changshu1 = 0;
    protected int changshu2 = 0;
    protected int changshu3 = 0;

    protected decimal zongjine = 0;
    protected string name = "";

    protected decimal totalstandard = 0;
    protected decimal shenqingfee = 0;
    protected decimal zhucefee = 0;
    protected decimal guanlifee = 0;

    protected decimal biaozhifee = 0;
    protected decimal qitafee = 0;

    protected string zhangdan = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        string baojiaid = Request.QueryString["baojiaid"].ToString();
        string customerid = "";
        string xiaoji = "";
        //      页眉页脚的一些参数用法
        //窗口标题 &w
        //网页地址 (URL) &u
        //短日期格式（由"控制面板"中的"区域设置"指定） &d
        //长日期格式（由"控制面板"中的"区域设置"指定） &D
        //当前页码 &p
        //总页数 &P
        //文本右对齐（后跟 &b） &b
        //文字居中（&b&b 之间） &b&b


        if (!string.IsNullOrEmpty(Request.QueryString["baojiaid"]))
        {
            string strTable = "";

            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con2.Open();
            // string sql6 = "select * from CustomerLinkMan  where id =(select top 1 linkid from baojialink where baojiaid='" + baojiaid + "') order by  id desc";
            string sql6 = "select *,(select customname from Customer where kehuid=CustomerLinkMan.customerid) as customname from CustomerLinkMan  where id =(select top 1 linkid from baojialink where baojiaid='" + baojiaid + "') order by  id desc";

            SqlDataAdapter ad6 = new SqlDataAdapter(sql6, con2);
            DataSet ds6 = new DataSet();
            ad6.Fill(ds6);
            DataTable dt6 = ds6.Tables[0];

            int aaa = dt6.Rows.Count;
            if (aaa == 0)
            {
                con2.Close();
                Response.Redirect("~/Customer/Welcome.aspx?id=rr");

                //Response.Write("<script>alert('请先在客户上增加联系人并在报价单上选择联系人');window.close();</script>");
            }


            string sqlsum = "select sum(yuanshi*shuliang) as  total from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' group by baojiaid";
            SqlCommand cmdsum = new SqlCommand(sqlsum, con2);
            SqlDataReader drsum = cmdsum.ExecuteReader();
            if (drsum.Read())
            {
                if (drsum["total"] == DBNull.Value)
                {
                    xiaoji = "0";
                }
                else
                {
                    xiaoji = Convert.ToDecimal(drsum["total"]).ToString().Trim();
                    xiaoji = Math.Round(Convert.ToDecimal(drsum["total"]), 2).ToString();
                }
            }
            drsum.Close();
            string sql1 = "select  * from BaoJiaChanPing where baojiaid='" + Request.QueryString["baojiaid"].ToString() + "' order by id";

            SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con2);
            DataSet ds1 = new DataSet();
            ad1.Fill(ds1);
            DataTable dt1 = ds1.Tables[0];
            int dt1count = dt1.Rows.Count;

            string sql2 = "select  *,(feiyong*zhekou) as feiyong2 from BaoJiaCPXiangMu where baojiaid='" + Request.QueryString["baojiaid"].ToString() + "' order by id";

            SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con2);
            DataSet ds2 = new DataSet();
            ad2.Fill(ds2);
            DataTable dt2 = ds2.Tables[0];
            int dt2count = dt2.Rows.Count;

            string sql3 = "select  * from BaoJiaBiao where baojiaid='" + Request.QueryString["baojiaid"].ToString() + "' order by id";
            SqlDataAdapter ad3 = new SqlDataAdapter(sql3, con2);
            DataSet ds3 = new DataSet();
            ad3.Fill(ds3);
            DataTable dt3 = ds3.Tables[0];
            customerid = dt3.Rows[0]["kehuid"].ToString();
            zhangdan = dt3.Rows[0]["zhangdan"].ToString();
            string weituo = dt3.Rows[0]["weituo"].ToString();
            string coupon = dt3.Rows[0]["coupon"].ToString();
            string currency = dt3.Rows[0]["currency"].ToString();
            string isVAT = dt3.Rows[0]["isVAT"].ToString();


            string sql4 = "select  * from quoterequires where baojiaid='" + Request.QueryString["baojiaid"].ToString() + "' order by id";
            SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con2);
            DataSet ds4 = new DataSet();
            ad4.Fill(ds4);
            DataTable dt4 = ds4.Tables[0];
            int dt4count = dt4.Rows.Count;


            string sql5 = "select * from Customer where kehuid='" + customerid + "'";

            SqlDataAdapter ad5 = new SqlDataAdapter(sql5, con2);
            DataSet ds5 = new DataSet();
            ad5.Fill(ds5);
            DataTable dt5 = ds5.Tables[0];

            string sql7 = "select * from userinfo where username='" + dt3.Rows[0]["responser"].ToString() + "'";

            SqlDataAdapter ad7 = new SqlDataAdapter(sql7, con2);
            DataSet ds7 = new DataSet();
            ad7.Fill(ds7);
            DataTable dt7 = ds7.Tables[0];

            string sql8 = "select * from Clause where baojiaid='" + baojiaid + "' order by bianhao asc";

            SqlDataAdapter ad8 = new SqlDataAdapter(sql8, con2);
            DataSet ds8 = new DataSet();
            ad8.Fill(ds8);
            DataTable dt8 = ds8.Tables[0];

            string getsalesbasesql = "select * from userinfo where username=(select responser from baojiabiao where baojiaid='" + baojiaid + "')";
            SqlDataAdapter da_getsales = new SqlDataAdapter(getsalesbasesql, con2);
            DataSet ds_getsales = new DataSet();
            da_getsales.Fill(ds_getsales);

            //报价项目
            string getTestitemsql = "select BaoJiaChanPing.name +'/'+ [type] as one,ceshiname,biaozhun,total,yp,zhouqi,BaoJiaCPXiangMu.beizhu from BaoJiaCPXiangMu left join BaoJiaChanPing on BaoJiaCPXiangMu.cpid=BaoJiaChanPing.id where BaoJiaCPXiangMu.baojiaid='" + baojiaid + "' order by BaoJiaChanPing.id asc";
            SqlDataAdapter da_test = new SqlDataAdapter(getTestitemsql, con2);
            DataSet ds_test = new DataSet();
            da_test.Fill(ds_test);

            //银行账户
            string sql_Bankaccount = "select * from Bankaccount where id='" + zhangdan + "'";
            SqlDataAdapter da_Bankaccount = new SqlDataAdapter(sql_Bankaccount, con2);
            DataSet ds_Bankaccount = new DataSet();
            da_Bankaccount.Fill(ds_Bankaccount);
            string bankaccount = "";
            if (ds_Bankaccount.Tables[0].Rows[0]["Name"].ToString() == "周朝政")
            {
                bankaccount = "深圳市倍测科技有限公司";
            }
            else
            {
                bankaccount = ds_Bankaccount.Tables[0].Rows[0]["Name"].ToString();
            }
            strTable += "<table width=\"97%\"  border=\"0\" align=\"center\"><tr><td  align=\"left\"><img src='../Images/002.jpg' /></td><td align=\"left\" class=\"F_size16  \">深圳市倍测科技有限公司</br><span class=\"F_size19\">Shenzhen BCTC Technology CO., LTD.</span></td></tr></table>";
            strTable += "<br/>";
            strTable += "<table width=\"97%\" height=30 border=\"0\" align=\"center\"><tr><td align=\"center\" style ='' class=\"F_size17 F_B \">报 价 单</br>Quotation</td></tr></table>";
            //strTable += "<table width=\"97%\" height=30 border=\"0\" align=\"center\"><tr><td wids1h=\"43%\">&nbsp;</td><td  class=\"F_size16 F_B \"></td><td  align=\"right\">文件编号/No.：" + Request.QueryString["baojiaid"].ToString() + "</td></tr></table>";
            strTable += "<br/>";

            strTable += "<table width=\"97%\" align=\"center\" border=\"0.5\"><tr height=27><td align=\"left\" width=\"13%\"><span style='font-weight :bold ;'>致/To：</span></td><td align=\"left\" width=\"37%\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["customname"].ToString() + "</span></td><td align=\"left\" width=\"13%\"><span style='font-weight :bold ;'>From: /Date:</span></td><td align=\"left\" width=\"37%\"><span style='font-weight :bold ;'>" + bankaccount + "</span></td></tr>";
            if (aaa != 0)
            {
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>Attn：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["name"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>Attn:</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + ds_getsales.Tables[0].Rows[0]["UserName"].ToString() + "</span></td></tr>";
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>Tel：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["telephone"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>Mobile: </span></td><td align=\"left\"><span style='font-weight :bold ;'>" + ds_getsales.Tables[0].Rows[0]["yidong"].ToString() + "</span></td></tr>";
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>Mobile：</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["mobile"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>E-mail: </span></td><td align=\"left\"><span style='font-weight :bold ;'>" + ds_getsales.Tables[0].Rows[0]["youxiang"].ToString() + "</span></td></tr>";
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>E-mail:</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + dt6.Rows[0]["email"].ToString() + "</span></td><td align=\"left\"><span style='font-weight :bold ;'>Doc. No:</span></td><td align=\"left\"><span style='font-weight :bold ;'>" + Request.QueryString["baojiaid"].ToString() + "</span></td></tr>";
            }
            else
            {
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>Attn：</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>Attn:</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td></tr>";
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>Tel：</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>Mobile: </span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td></tr>";
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>Mobile：</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>E-mail: </span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td></tr>";
                strTable += "<tr height=27><td align=\"left\"><span style='font-weight :bold ;'>E-mail:</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td><td align=\"left\"><span style='font-weight :bold ;'>Doc. No:</span></td><td align=\"left\"><span style='font-weight :bold ;'></span></td></tr>";
            }
            if (xiaoji == "")
            {
                xiaoji = "0";
            }
            strTable += "</table>";
            strTable += "<br/>";
            //打印尊敬的用户您好
            strTable += "<table width=\"97%\" align=\"center\">";
            strTable += "<tr><td>Dear Valued Customer / 尊敬的用户：您好！</td></tr>";
            strTable += "<tr><td>Hello! Thank you very much for your interest in our testing services, According to the documents provided by you,we are pleased to quote the following services for your perusal</td></tr>";
            strTable += "<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;非常感谢您对我们测试服务的支持与关照，现根据您提供的产品资料，我们做出如下建议和报价：</td></tr>";
            strTable += "<tr><td>&nbsp;</td></tr></table>";
            strTable += "<table width=\"97%\" align=\"center\" border=\"1\">";

            //string ddddd = dt1.Rows[0]["type"].ToString() + "," + dt1.Rows[0]["beizhu"].ToString() + ",";
            //string dddd2 = ddddd.Trim();
            //string dddd3 = dddd2.Remove(dddd2.LastIndexOf(","), 1);


            decimal abc = 0;

            strTable += "<tr height=27>";
            strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>Product/Mo del No 产品/型号</br>Test</span></td>";
            strTable += "<td align=\"center\" width=\"15%\"><span style='font-weight :bold ;'>Service Item/服务项目 </br>Items</span></td>";
            strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>Ref.Standard/标准</br>Standards</span></td>";
            strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>Cost/费用 </br>Samples</span></td>";
            strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>Sample Qty/样品数量 </br>Leadtime</span></td>";
            strTable += "<td align=\"center\" width=\"7%\"><span style='font-weight :bold ;'>Lead Time/周期/RMB</br>UnitPrice</span></td>";
            strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>Remark  备注 </br>UnitPrice</span></td>";
            strTable += "</tr>";
            for (int z = 0; z < ds_test.Tables[0].Rows.Count; z++)
            {
                strTable += "<tr height=27>";
                strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>" + ds_test.Tables[0].Rows[z]["one"].ToString() + "</span></td>";
                strTable += "<td align=\"center\" width=\"15%\"><span style='font-weight :bold ;'>" + ds_test.Tables[0].Rows[z]["ceshiname"].ToString() + "</span></td>";
                strTable += "<td align=\"center\" width=\"20%\"><span style='font-weight :bold ;'>" + ds_test.Tables[0].Rows[z]["biaozhun"].ToString() + "</span></td>";
                strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>" + Convert.ToDecimal(ds_test.Tables[0].Rows[z]["total"]).ToString("F2") + "</span></td>";
                strTable += "<td align=\"center\" width=\"10%\"><span style='font-weight :bold ;'>" + ds_test.Tables[0].Rows[z]["yp"].ToString() + "</span></td>";
                strTable += "<td align=\"center\" width=\"7%\"><span style='font-weight :bold ;'>" + ds_test.Tables[0].Rows[z]["zhouqi"].ToString() + "</span></td>";
                strTable += "<td align=\"center\" width=\"7%\"><span style='font-weight :bold ;'>" + ds_test.Tables[0].Rows[z]["beizhu"].ToString() + "</span></td>";
                strTable += "</tr>";
                abc = abc + Math.Round(Convert.ToDecimal(ds_test.Tables[0].Rows[z]["total"]), 2);
            }
            con2.Close();
            if ((string.IsNullOrEmpty(coupon) || coupon == "0.00" || coupon == "&nbsp;") && isVAT == "增值税")
            {
                //不存在优惠后金额且有增值税
                decimal tax = abc * 0.06m;
                decimal sumprice = abc + tax;
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp; Total Amount 合计: &nbsp;&nbsp;&nbsp;" + abc + "</span></td>";
                strTable += "</tr>";
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp; Tax       税金: &nbsp;&nbsp;&nbsp;" + tax.ToString("f2") + "</span></td>";
                strTable += "</tr>";
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp;Total Price 总   价: &nbsp;&nbsp;&nbsp;" + sumprice.ToString("f2") + "</span></td>";
                strTable += "</tr>";
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp; 备注 : &nbsp;&nbsp;&nbsp;" + dt3.Rows[0]["Beizhu"].ToString() + "</span></td>";
                strTable += "</tr>";
                strTable += "</table>";
            }
            else if ((string.IsNullOrEmpty(coupon) || coupon == "0.00" || coupon == "&nbsp;") && isVAT == "是")
            {
                //不存在优惠后金额且含税
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp; Total Amount 合计: &nbsp;&nbsp;&nbsp;" + abc + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（报价已包含 6%增值税)</span></td>";
                strTable += "</tr>";
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp; 备注 : &nbsp;&nbsp;&nbsp;" + dt3.Rows[0]["Beizhu"].ToString() + "</span></td>";
                strTable += "</tr>";
                strTable += "</table>";
            }
            else if ((string.IsNullOrEmpty(coupon) || coupon == "0.00" || coupon == "&nbsp;") && isVAT == "否")
            {
                //不存在优惠后金额且不含税
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp; Total Amount 合计: &nbsp;&nbsp;&nbsp;" + abc + "</span></td>";
                strTable += "</tr>";
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp; 备注 : &nbsp;&nbsp;&nbsp;" + dt3.Rows[0]["Beizhu"].ToString() + "</span></td>";
                strTable += "</tr>";
                strTable += "</table>";
            }
            else if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;" && isVAT == "是")
            {
                //存在优惠金额且含税
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp; Original Price 原   价: &nbsp;&nbsp;&nbsp;" + abc + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（报价已包含 6%增值税)</span></td>";
                strTable += "</tr>";
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp;After Dicount 优惠后金额: &nbsp;&nbsp;&nbsp;" + coupon + "</span></td>";
                strTable += "</tr>";
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp; 备注 : &nbsp;&nbsp;&nbsp;" + dt3.Rows[0]["Beizhu"].ToString() + "</span></td>";
                strTable += "</tr>";
                strTable += "</table>";
            }
            else if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;" && isVAT == "增值税")
            {
                //存在优惠后金额且含增值税
                decimal tax = Convert.ToDecimal(coupon) * 0.06m;
                decimal sumprice = Convert.ToDecimal(coupon) + tax;
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp;Original Price 原   价: &nbsp;&nbsp;&nbsp;" + abc + "</span></td>";
                strTable += "</tr>";
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp;After Dicount 优惠金额:  &nbsp;&nbsp;&nbsp;" + Convert.ToDecimal(coupon).ToString("f2") + "</span></td>";
                strTable += "</tr>";
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp;Tax       税   金:  &nbsp;&nbsp;&nbsp;" + tax.ToString("f2") + "</span></td>";
                strTable += "</tr>";
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp;Total Price 总   价:  &nbsp;&nbsp;&nbsp;" + sumprice + "</span></td>";
                strTable += "</tr>";
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp; 备注 : &nbsp;&nbsp;&nbsp;" + dt3.Rows[0]["Beizhu"].ToString() + "</span></td>";
                strTable += "</tr>";
                strTable += "</table>";
            }
            else if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;" && isVAT == "否")
            {
                //存在优惠金额且不含税
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp; Original Price 原   价: &nbsp;&nbsp;&nbsp;" + abc + "</span></td>";
                strTable += "</tr>";
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp;After Dicount 优惠后金额: &nbsp;&nbsp;&nbsp;" + coupon + "</span></td>";
                strTable += "</tr>";
                strTable += "<tr height=27>";
                strTable += "<td aalign=\"center\" width=\"16%\" colspan=\"7\"><span style='font-weight :bold;'>&nbsp;&nbsp;&nbsp; 备注 : &nbsp;&nbsp;&nbsp;" + dt3.Rows[0]["Beizhu"].ToString() + "</span></td>";
                strTable += "</tr>";
                strTable += "</table>";
            }
            //生成条款
            strTable += "<table width=\"97%\" height=25 border=\"0\" align=\"center\"><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr><tr><td align=\"left\" style ='' ><span style='font-weight :bold ;font-size:16px;'>一、Details in quotation 报价说明：</span></td></tr></table>";
            int dd = dt8.Rows.Count;
            for (int i = 0; i < dd; i++)
            {
                strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=35 border=\"0\" align=\"center\"><tr><td align=\"left\" style ='' class=\"F_size12 \"  width=\"20\">" + dt8.Rows[i]["bianhao"].ToString() + "&nbsp;&nbsp;" + dt8.Rows[i]["tiaokuan"].ToString() + "</td></table>";
            }

            int das = dt8.Rows.Count + 1;
            string dasg = das.ToString() + ",";

            strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=35 border=\"0\" align=\"center\">";
            strTable += "<tr><td>&nbsp;</td></tr><tr><td style=\"font-size:18px;font-weight:bold\">二、Application Flow申请检测流程：</td></tr><tr><td>&nbsp;</td></tr>";
            strTable += "<tr><td>1.Sign back this quotation, fill in the application form, submit the technical information & samples,and make payment according to payment terms.</td></tr>";
            strTable += "<tr><td>&nbsp;&nbsp;回签报价合同，填写服务申请表寄送样板和产品资料，并按照付款方式支付款项。</td></tr><tr><td>&nbsp;</td></tr>";
            strTable += "<tr><td>2.We' || arrange the pre-test and check the submitted information.However, if there are failure points, We Will inform the applicant to modify the porducts and resubmit revised samples & proper information in time.</td></tr>";
            strTable += "<tr><td>安排预测试并核对资料，如果有不符合项目，及时通知申请方整改，并重新送样及提供合格的资料</td></tr><tr><td>&nbsp;</td></tr>";
            strTable += "<tr><td>3.Arranging the retests, passing all formal tests,received certificate confirmation copy and issuing the certification.</td></tr>";
            strTable += "<tr><td>安排重新测试，正式猜测但是通过，收到证书确认件，取得证书。</td></tr><tr><td>&nbsp;</td></tr>";
            strTable += "</table>";

            //资料样品要求
            strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=35 border=\"0\" align=\"center\">";
            strTable += "<tr><td style=\"font-size:18px;font-weight:bold\">三、Documents & samples required资料&样品要求：</td></tr><tr><td>&nbsp;</td></tr>";
            strTable += "<tr><td>Application申请表</td></tr>";
            strTable += "<tr><td>Clicuit diagram，PCB layuot， English Instruction manual，Copy component Certificate，Parts list and other relevant documents etc.</td></tr>";
            strTable += "<tr><td>线路图，线路布置图，英文说明书，零部件证书复印件，零部件清单及其相关文件等。</td></tr><tr><td>&nbsp;</td></tr>";
            strTable += "</table>";
            //付款方式

            strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=35 border=\"0\" align=\"center\">";
            strTable += "<tr><td style=\"font-size:18px;font-weight:bold\">四、Payment Conditions付款方式：</td></tr>";
            strTable += "<tr><td>please gice us the transfer documents by fas or email，after marking the remittance.</td></tr>";
            strTable += "<tr><td>请支付款项后，将付款低单或email的形式回传给我们。</td></tr>";
            strTable += "</table>";
            //生成银行账户信息
            strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=35 border=\"1\" align=\"center\">";
            strTable += "<tr><td style=\"font-size:18px;font-weight:bold;width:30%;\">Company 户名：</td><td>" + ds_Bankaccount.Tables[0].Rows[0]["Name"].ToString() + "</td></tr>";
            strTable += "<tr><td style=\"font-size:18px;font-weight:bold;width:30%;\">Company bank account 账号：</td><td>" + ds_Bankaccount.Tables[0].Rows[0]["account"].ToString() + "</td></tr>";
            strTable += "<tr><td style=\"font-size:18px;font-weight:bold;width:30%;\">Bank 开户银行：</td><td>" + ds_Bankaccount.Tables[0].Rows[0]["openaccout"].ToString() + "</td></tr>";
            strTable += "</table>";

            strTable += "<table width=\"97%\"  class=\"F_size12 \"  height=35   align=\"center\">";
            strTable += "<tr><td>We assure you of our best services at all times. Please sign and return if you accept this quotation.</td></tr>";
            strTable += "<tr><td>我们竭诚为您提供最好的服务，若您接受此报价，请签字并回传给我们</td></tr>";
            strTable += "</table>";


            strTable += "<table width=\"97%\" align=\"center\" border=\"0\">";
            strTable += "<tr height=10><td align=\"center\" colspan=\"2\" style ='width :50%'></td><td align=\"center\" colspan=\"2\" style ='width :50%'><span style='font-weight :bold ;'></br></span></td></tr>";
            strTable += "<tr height=27><td align=\"left\" style ='width :15%'><span style='font-weight :bold ;'>Confirmed and Accepted By Signature with <br/> Stamp 委托方签字盖章 </span></td><td align=\"left\"></td><td align=\"left\" style ='width :15%'><span style='font-weight :bold ;'>BCTC Testing International <br/> Signature with Stamp 受理方签字盖章 </span></td></tr>";
            strTable += "</table>";
            lblTable.Text = strTable;
        }
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