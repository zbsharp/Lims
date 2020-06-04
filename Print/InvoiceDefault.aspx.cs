using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Print_InvoiceDefault : System.Web.UI.Page
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
    protected string inid = "";
    
    
    
    
    public string DocumentID;
    public string XYBH;
    public string BMJH;
    public string JF;
    public string YF;
    public string HZNR;
    public string QLZR;
    public string CPMC;
    public string DGSL;
    public string DGRQ;
    public string FIELDXML;//定义收保护对象（已XML形式）

    public string mScriptName;
    public string mServerName;
    public string mHttpUrl;
    public string mServerUrl;
    public string mWebUrl;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        mScriptName = "Print/InvoiceDefault.aspx";
        mServerName = "Print/Service.aspx";
        mHttpUrl = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ServerVariables["SCRIPT_NAME"];
        mHttpUrl = mHttpUrl.Substring(0, mHttpUrl.Length - mScriptName.Length);
        mServerUrl = mHttpUrl + mServerName;
        mWebUrl = "http://localhost:8080/iSignatureServer/OfficeServer.jsp";
        // 在此处放置用户代码以初始化页面



        DocumentID = Request.QueryString["DocumentID"];
        //取得编号
        if (DocumentID == null)
        {
            DocumentID = "";            //编号为空
        }
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //打开数据库
        conn.Open();
        string strSelectCmd = "Select * From HTMLDocument Where DocumentID='" + DocumentID + "'";
        SqlCommand mCommand = new SqlCommand(strSelectCmd, conn);
        SqlDataReader mReader = mCommand.ExecuteReader();

        if (mReader.Read())
        {
            DocumentID = mReader["DocumentID"].ToString();
            XYBH = mReader["XYBH"].ToString();
            BMJH = mReader["BMJH"].ToString();
            JF = mReader["JF"].ToString();
            YF = mReader["YF"].ToString();
            HZNR = mReader["HZNR"].ToString();
            QLZR = mReader["QLZR"].ToString();
            CPMC = mReader["CPMC"].ToString();
            DGSL = mReader["DGSL"].ToString();
            DGRQ = mReader["DGRQ"].ToString();
        }
        else
        {
            //取得唯一值(DocumentID)
            System.DateTime SystemTime;
            SystemTime = DateTime.Now;
            DocumentID = SystemTime.ToString("yyyyMMddhhmmss");
            XYBH = "JGKJ2004-" + SystemTime.Second;
            BMJH = "中级";
            JF = "江西金格网络科技有限责任公司";
            YF = "XXX有限责任公司";
            HZNR = "订购甲方iSignature 电子印章系统" + "\r\n" + "For Word,For Excel,For Html 版本";
            QLZR = "内容完整性鉴别";
            CPMC = "iSignature 电子印章系统";
            DGSL = SystemTime.ToString("mm");
            DGRQ = DateTime.Now.ToShortDateString();
        }
        mReader.Close();

        /*
          说明：iSignature HTML控件提供两种保护数据方式，一种为FieldsList字符串，别一种是FieldsXml方式
          本例程序是使用FieldsList字符串方式，如使用XML方式，请参考以下：
        */
        FIELDXML = "<?xml version='1.0' encoding='GB2312' standalone='yes'?>";
        FIELDXML = FIELDXML + "<Signature>";
        FIELDXML = FIELDXML + "<Field>";                     //保护"保密级别"列
        FIELDXML = FIELDXML + "<Field Index='Caption'>保密级别</Field>";
        FIELDXML = FIELDXML + "<Field Index='ID'>BMJH</Field>";
        FIELDXML = FIELDXML + "<Field Index='VALUE'>" + BMJH + "</Field>";
        FIELDXML = FIELDXML + "<Field Index='ProtectItem'>TRUE</Field>";
        FIELDXML = FIELDXML + "</Field>";

        FIELDXML = FIELDXML + "<Field>";                        //保护"甲方签章"列
        FIELDXML = FIELDXML + "<Field Index='Caption'>甲方签章</Field>";
        FIELDXML = FIELDXML + "<Field Index='ID'>JF</Field>";
        FIELDXML = FIELDXML + "<Field Index='VALUE'>JF</Field>";
        FIELDXML = FIELDXML + "<Field Index='ProtectItem'>TRUE</Field>";
        FIELDXML = FIELDXML + "</Field>";
        FIELDXML = FIELDXML + "</Signature>";

        conn.Close();






        string baojiaid = "SQ2013071285";
        inid = "IN13-09178";
        string customerid = "1005275";
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




        //if (!string.IsNullOrEmpty(Request.QueryString["baojiaid"]))
        {



            string strTable = "";


            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con2.Open();


            string sql10 = "select * from Invoice where inid='" + inid + "'";

            SqlDataAdapter ad10 = new SqlDataAdapter(sql10, con2);
            DataSet ds10 = new DataSet();
            ad10.Fill(ds10);
            DataTable dt10 = ds10.Tables[0];
            int qz10 = dt10.Rows.Count;

            string sql11 = "select * from yangpin2 where bianhao=(select top 1 bianhao from anjianxinxi2 where bianhaotwo='" + inid + "')";

            SqlDataAdapter ad11 = new SqlDataAdapter(sql11, con2);
            DataSet ds11 = new DataSet();
            ad11.Fill(ds11);
            DataTable dt11 = ds11.Tables[0];
            int qz11 = dt11.Rows.Count;

            string sql12 = "select * from anjianxinxi2 where bianhaotwo='" + inid + "'";

            SqlDataAdapter ad12 = new SqlDataAdapter(sql12, con2);
            DataSet ds12 = new DataSet();
            ad12.Fill(ds12);
            DataTable dt12 = ds12.Tables[0];
            int qz12 = dt12.Rows.Count;

            string sql13 = "select * from anjianinfo2 where bianhao=(select top 1 bianhao from anjianxinxi2 where bianhaotwo='" + inid + "' order by id desc)";

            SqlDataAdapter ad13 = new SqlDataAdapter(sql13, con2);
            DataSet ds13 = new DataSet();
            ad13.Fill(ds13);
            DataTable dt13 = ds13.Tables[0];
            int qz13 = dt13.Rows.Count;



            string sql133 = "select * from anjianinfo2 where rwbianhao=(select top 1 taskid from CeShiFeiKf where shoufeibianhao='" + inid + "' order by id desc)";

            SqlDataAdapter ad133 = new SqlDataAdapter(sql133, con2);
            DataSet ds133 = new DataSet();
            ad133.Fill(ds133);
            DataTable dt133 = ds133.Tables[0];
            int qz133 = dt133.Rows.Count;






            string sql6 = "select * from CustomerLinkMan where name='" + dt10.Rows[0]["name"].ToString() + "' and customerid='" + dt10.Rows[0]["kehuid"].ToString() + "' order by  id desc";

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


            string sqlsum = "select sum(feiyong) as total from CeShiFeiKf where shoufeibianhao='" + inid + "'";
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
            string sql1 = "select  * from BaoJiaChanPing where baojiaid='SQ2013071285' order by id";

            SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con2);
            DataSet ds1 = new DataSet();
            ad1.Fill(ds1);
            DataTable dt1 = ds1.Tables[0];
            int dt1count = dt1.Rows.Count;

            string sql2 = "select  * from BaoJiaCPXiangMu where baojiaid='SQ2013071285' order by id";

            SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con2);
            DataSet ds2 = new DataSet();
            ad2.Fill(ds2);
            DataTable dt2 = ds2.Tables[0];
            int dt2count = dt2.Rows.Count;




            string sql3 = "select  * from BaoJiaBiao where baojiaid='SQ2013071285' order by id";

            SqlDataAdapter ad3 = new SqlDataAdapter(sql3, con2);
            DataSet ds3 = new DataSet();
            ad3.Fill(ds3);
            DataTable dt3 = ds3.Tables[0];

            customerid = dt3.Rows[0]["kehuid"].ToString();


            // zhangdan = dt3.Rows[0]["zhangdan"].ToString();

            string sql4 = "select  * from quoterequires where baojiaid='SQ2013071285' order by id";

            SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con2);
            DataSet ds4 = new DataSet();
            ad4.Fill(ds4);
            DataTable dt4 = ds4.Tables[0];
            int dt4count = dt4.Rows.Count;


            string sql5 = "select top 1 fukuandanwei as customname from anjianinfo2 where rwbianhao in (select taskid from ceshifeikf where shoufeibianhao='" + inid + "')";

            SqlDataAdapter ad5 = new SqlDataAdapter(sql5, con2);
            DataSet ds5 = new DataSet();
            ad5.Fill(ds5);
            DataTable dt5 = ds5.Tables[0];




            string sql7 = "select * from userinfo where username='" + ds3.Tables[0].Rows[0]["responser"].ToString() + "'";

            SqlDataAdapter ad7 = new SqlDataAdapter(sql7, con2);
            DataSet ds7 = new DataSet();
            ad7.Fill(ds7);
            DataTable dt7 = ds7.Tables[0];

            string sql8 = "select * from Clause where baojiaid='" + baojiaid + "'";

            SqlDataAdapter ad8 = new SqlDataAdapter(sql8, con2);
            DataSet ds8 = new DataSet();
            ad8.Fill(ds8);
            DataTable dt8 = ds8.Tables[0];



            string sql9 = "select * from Provision2 where baojiaid='" + baojiaid + "' and remark !=''";

            SqlDataAdapter ad9 = new SqlDataAdapter(sql9, con2);
            DataSet ds9 = new DataSet();
            ad9.Fill(ds9);
            DataTable dt9 = ds9.Tables[0];
            int qz = dt9.Rows.Count;


            string sql911 = "select feiyong,beizhu2 from CeShiFeiKf where shoufeibianhao='" + inid + "'";

            SqlDataAdapter ad911 = new SqlDataAdapter(sql911, con2);
            DataSet ds911 = new DataSet();
            ad911.Fill(ds911);
            DataTable dt911 = ds911.Tables[0];
            int qz911 = dt911.Rows.Count;







            strTable += "<table width=\"800\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" >";
            strTable += "<tr height=30><td align=\"center\" colspan=\"2\" style ='width :240'></td><td align=\"center\" colspan=\"2\" style ='width :560'><span style='font-weight :bold ;'>中检集团南方电子产品测试（深圳）有限公司</br>CCIC Southern Electronic Product Testing（Shenzhen）Co., Ltd.</span></td></tr>";
            strTable += "<tr height=30><td align=\"left\" style ='width :80'><span style='font-weight :bold ;'></span></td><td align=\"left\" ></td><td align=\"left\" style ='width :120'><span style='font-weight :bold ;'>签字/Signature:</span></td><td align=\"left\"  style=\"border-bottom:1px #CCCCCC solid;width :200\"></td></tr>";
            strTable += "<tr height=30><td align=\"left\" style ='width :80'><span style='font-weight :bold ;'></span></td><td align=\"left\" ></td><td align=\"left\" style ='width :120'><span style='font-weight :bold ;'>日期/Date:</span></td><td align=\"center\"  style=\"border-bottom:1px #CCCCCC solid;width :200\">" + DateTime.Now.ToShortDateString() + "</td></tr>";
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

    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// 设计器支持所需的方法 - 不要使用代码编辑器修改
    /// 此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
    }
    #endregion
}