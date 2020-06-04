using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Word;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class Print_QuoSign : System.Web.UI.Page
{
    string quotationid = "";
    bool withsign = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["quotationid"] != null)
        {
            quotationid = Request.QueryString["quotationid"].ToString();
            this.name.Text = quotationid;
        }
        else
        {
            this.Btprint_Click.Enabled = false;
        }
        //这里判定是否要盖章，如果还未审批通过（不含财务确认），则withsign为false，否则为true
        withsign = true;
        string shwhere = "shenpibiaozhi='二级通过' and other='other'";
        string judgesql = "select * from baojiabiao where " + shwhere + " and baojiaid='" + quotationid + "'";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand(judgesql, con);
        SqlDataReader ad = cmd.ExecuteReader();
        withsign = ad.Read();
        ad.Close();

        con.Close();


        if (!IsPostBack)
        {
            BindFile();
        }
    }

    protected void BindFile()
    {
        string sql = "select * from QuotaionSign where quotationid='" + this.quotationid + "' order by id desc";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        con.Close();
        this.GridView1.DataSource = ds.Tables[0];
        this.GridView1.DataBind();
    }

    protected void Btprint_Click_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select *,(select customname from Customer where kehuid=CustomerLinkMan.customerid) as customname from CustomerLinkMan  where id =(select top 1 linkid from baojialink where baojiaid='" + quotationid + "' order by id) order by  id desc";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["telephone"].ToString()) && string.IsNullOrEmpty(ds.Tables[0].Rows[0]["mobile"].ToString()) && string.IsNullOrEmpty(ds.Tables[0].Rows[0]["email"].ToString()))
                {
                    Literal1.Text = "<script>alert('客户联系人必须要有一种联系方式')</script>";
                }
                else
                {
                    if (this.name.Text != "")
                    {
                        string sql_bumen = "select * from userinfo where username=(select responser from baojiabiao where baojiaid='" + quotationid + "')";
                        SqlCommand cmd_bumen = new SqlCommand(sql_bumen, con);
                        SqlDataReader dr_bumen = cmd_bumen.ExecuteReader();
                        string bumen = "";
                        if (dr_bumen.Read())
                        {
                            bumen = dr_bumen["departmentname"].ToString();
                        }
                        dr_bumen.Close();

                        //防止用户重复点击
                        Btprint_Click.Enabled = false;
                        Toprint(bumen);
                        Btprint_Click.Enabled = true;
                    }
                }
            }
            else
            {
                Literal1.Text = "<script>alert('请先在报价单上选择联系人。')</script>";
            }
        }
    }

    public void Toprint(string bumen)
    {
        Random ran = new Random(Guid.NewGuid().GetHashCode());
        int number = ran.Next(500);
        string numberstr = number.ToString("000");
        string urlbase = this.name.Text + DateTime.Now.ToString("yyyy-MM-dd") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00");
        string newfilename = urlbase + number;
        string baseFilePath = "";
        string copybasefilePath = "";
        if (withsign)
        {
            //2020-3-16取消标源
            //if (bumen == "标源销售部")
            //{
            //    baseFilePath = Server.MapPath("biaoyuan.dot");
            //}
            //else
            //{
            baseFilePath = Server.MapPath("quosigned.dot");
            //}
        }
        else
        {
            //2020-3-16取消标源
            //if (bumen == "标源销售部")
            //{
            //    baseFilePath = Server.MapPath("biaoyuanno.dot");
            //}
            //else
            //{
            baseFilePath = Server.MapPath("quonotsign.dot");
            //}
        }
        if (File.Exists(baseFilePath))
        {
            copybasefilePath = Server.MapPath("OuotationPrinted/" + newfilename + ".dot");
            File.Copy(baseFilePath, copybasefilePath, true);
        }
        else
        {
            return;
        }
        if (File.Exists(Server.MapPath("OuotationPrinted/" + newfilename + ".dot")))
        {
            WriteIntoWord wiw = new WriteIntoWord();

            string FilePath = Server.MapPath("OuotationPrinted/" + newfilename + ".dot");     //模板路径
            string SaveDocPath = Server.MapPath("OuotationPrinted/" + newfilename + ".doc");
            wiw.OpenDocument(FilePath);
            try
            {
                string customername = "";
                string customerlinkman = "";
                string customertel = "";
                string customermobile = "";
                string customeremail = "";
                string saltel = "";
                string salmobile = "";
                string saleemail = "";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string getcustomerbasesql = "select *,(select customname from Customer where kehuid=CustomerLinkMan.customerid) as customname from CustomerLinkMan  where id =(select top 1 linkid from baojialink where baojiaid='" + quotationid + "') order by  id desc";
                SqlCommand getcustomerbasecmd = new SqlCommand(getcustomerbasesql, con);
                SqlDataReader getcustomerbasead = getcustomerbasecmd.ExecuteReader();
                if (getcustomerbasead.Read())
                {
                    customername = getcustomerbasead["customname"].ToString();
                    customerlinkman = getcustomerbasead["name"].ToString();
                    customertel = getcustomerbasead["telephone"].ToString();
                    customeremail = getcustomerbasead["email"].ToString();
                    customermobile = getcustomerbasead["mobile"].ToString();
                }
                getcustomerbasead.Close();
                string getsalesbasesql = "select * from userinfo where username=(select responser from baojiabiao where baojiaid='" + quotationid + "')";
                SqlCommand getsalesbasecmd = new SqlCommand(getsalesbasesql, con);
                SqlDataReader getsalesbasead = getsalesbasecmd.ExecuteReader();
                if (getsalesbasead.Read())
                {
                    saltel = getsalesbasead["UserName"].ToString();
                    salmobile = getsalesbasead["yidong"].ToString();
                    saleemail = getsalesbasead["youxiang"].ToString();
                }
                getsalesbasead.Close();

                string getTestitemsql = "select BaoJiaChanPing.name +'/'+ [type] as one,ceshiname,biaozhun,total,yp,zhouqi,BaoJiaCPXiangMu.beizhu from BaoJiaCPXiangMu left join BaoJiaChanPing on BaoJiaCPXiangMu.cpid=BaoJiaChanPing.id where BaoJiaCPXiangMu.baojiaid='" + quotationid + "' order by  BaoJiaChanPing.id  asc";
                SqlDataAdapter getTestitemad = new SqlDataAdapter(getTestitemsql, con);
                DataSet getTestitemds = new DataSet();
                getTestitemad.Fill(getTestitemds);
                decimal totalmoney = 0m;
                for (int i = 0; i < getTestitemds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        getTestitemds.Tables[0].Rows[i]["total"] = Convert.ToDecimal(getTestitemds.Tables[0].Rows[i]["total"].ToString()).ToString("f2");
                        totalmoney += Convert.ToDecimal(getTestitemds.Tables[0].Rows[i]["total"].ToString());
                    }
                    catch
                    {
                    }
                }
                string totalchines = ConvertToChinese(totalmoney.ToString());

                string getclausesql = "select tiaokuan from Clause where baojiaid='" + quotationid + "' order by id asc";
                SqlDataAdapter getclausead = new SqlDataAdapter(getclausesql, con);
                DataSet getclauseds = new DataSet();
                getclausead.Fill(getclauseds);
                string getbaojiabase = "select * from baojiabiao where baojiaid='" + quotationid + "'";
                string bankuser = "";
                string banknum = "";
                string bankname = "";
                string signdate = "";
                string paytype = "";
                string zhangdan = "";
                string shoukuanjine = "";
                string zongjine = "";
                string beizhu = "";
                string isvat = "";
                string coupon = "";
                string currency = "";
                SqlCommand getbaojiabasecmd = new SqlCommand(getbaojiabase, con);
                SqlDataReader getbaojiabasead = getbaojiabasecmd.ExecuteReader();
                if (getbaojiabasead.Read())
                {
                    zhangdan = getbaojiabasead["zhangdan"].ToString();
                    signdate = Convert.ToDateTime(getbaojiabasead["filltime"].ToString()).ToString("yyyy-MM-dd");
                    paytype = getbaojiabasead["paymentmethod"].ToString();
                    beizhu = getbaojiabasead["BeiZhu"].ToString();
                    isvat = getbaojiabasead["isVAT"].ToString();//是否含税
                    currency = getbaojiabasead["currency"].ToString();//币种
                    try
                    {
                        shoukuanjine = Convert.ToDecimal(getbaojiabasead["thefirst"].ToString()).ToString("f2");
                    }
                    catch
                    {
                        shoukuanjine = "0m";
                    }
                    zongjine = Convert.ToDecimal(getbaojiabasead["zhehoujia"].ToString()).ToString("f2");
                    if (string.IsNullOrEmpty(getbaojiabasead["coupon"].ToString()) || getbaojiabasead["coupon"].ToString() == "&nbsp;")
                    {
                        coupon = "";
                    }
                    else
                    {
                        coupon = Convert.ToDecimal(getbaojiabasead["coupon"].ToString()).ToString("f2");
                    }
                }
                getbaojiabasead.Close();

                string getbanksql = "select * from Bankaccount where id='" + zhangdan + "'";
                SqlDataAdapter getbankad = new SqlDataAdapter(getbanksql, con);
                DataSet getbankds = new DataSet();
                getbankad.Fill(getbankds);

                //公司名称
                string Name = "";
                if (getbankds.Tables[0].Rows[0]["Name"].ToString() == "周朝政")
                {
                    Name = "深圳市倍测科技有限公司";
                }
                //else if (getbankds.Tables[0].Rows[0]["Name"].ToString() == "蒋梅" && bumen.Trim() == "标源销售部")
                //{
                //    Name = "深圳市标源质量技术服务有限公司";
                //}
                else
                {
                    Name = getbankds.Tables[0].Rows[0]["Name"].ToString();
                }

                con.Close();
                wiw.WriteIntoDocument("Name", Name);
                wiw.WriteIntoDocument("Customer", customername);
                wiw.WriteIntoDocument("Linkman", customerlinkman);
                wiw.WriteIntoDocument("Custel", customertel);
                wiw.WriteIntoDocument("Cusmobile", customermobile);
                wiw.WriteIntoDocument("Cusemail", customeremail);

                wiw.WriteIntoDocument("Saletel", saltel);
                wiw.WriteIntoDocument("Salemobile", salmobile);
                wiw.WriteIntoDocument("Saleemail", saleemail);
                wiw.WriteIntoDocument("Salequotationid", quotationid);
                wiw.WriteIntoDocumentItemtable("Itemlist", getTestitemds, totalmoney, totalchines, beizhu, isvat, currency, coupon);
                if (getclauseds.Tables[0].Rows.Count > 0)
                {
                    wiw.WriteIntoDocumentclausetable("clauselist", getclauseds);
                }
                else
                {
                    wiw.WriteIntoDocument("clauselist", "无");
                }
                if (paytype == "首款")
                {
                    //如果存在优惠金额、则用优惠后金额代替总金额,如果不含税还要加上税金
                    if (isvat == "增值税")
                    {
                        if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;")
                        {
                            decimal vat = Convert.ToDecimal(coupon) * 0.06m;
                            decimal weiprice = Convert.ToDecimal(coupon) - Convert.ToDecimal(shoukuanjine) + vat;
                            wiw.WriteIntoDocument("moneypay", "    首款：" + shoukuanjine + "      尾款：" + weiprice.ToString("f2"));
                        }
                        else
                        {
                            decimal vat = Convert.ToDecimal(zongjine) * 0.06m;
                            decimal weiprice = Convert.ToDecimal(zongjine) - Convert.ToDecimal(shoukuanjine) + vat;
                            wiw.WriteIntoDocument("moneypay", "    首款：" + shoukuanjine + "      尾款：" + weiprice.ToString("f2"));
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;")
                        {
                            wiw.WriteIntoDocument("moneypay", "    首款：" + shoukuanjine + "      尾款：" + (Convert.ToDecimal(coupon) - Convert.ToDecimal(shoukuanjine)).ToString("f2"));
                        }
                        else
                        {
                            wiw.WriteIntoDocument("moneypay", "    首款：" + shoukuanjine + "      尾款：" + (Convert.ToDecimal(zongjine) - Convert.ToDecimal(shoukuanjine)).ToString("f2"));
                        }
                    }
                }
                else
                {
                    if (isvat == "增值税")
                    {
                        if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;")
                        {
                            decimal vat = Convert.ToDecimal(coupon) * 0.06m;
                            decimal pricesum = Convert.ToDecimal(coupon) + vat;
                            wiw.WriteIntoDocument("moneypay", "检测开始前支付全款金额" + pricesum.ToString("f2"));
                        }
                        else
                        {
                            decimal vat = Convert.ToDecimal(zongjine) * 0.06m;
                            decimal pricesum = Convert.ToDecimal(zongjine) + vat;
                            wiw.WriteIntoDocument("moneypay", "检测开始前支付全款金额" + pricesum.ToString("f2"));
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;")
                        {
                            wiw.WriteIntoDocument("moneypay", "检测开始前支付全款金额" + coupon);
                        }
                        else
                        {
                            wiw.WriteIntoDocument("moneypay", "检测开始前支付全款金额" + zongjine);
                        }
                    }
                }
                if (getbankds.Tables[0].Rows.Count > 0)
                {
                    bankuser = getbankds.Tables[0].Rows[0]["Name"].ToString();
                    banknum = getbankds.Tables[0].Rows[0]["account"].ToString();
                    bankname = getbankds.Tables[0].Rows[0]["openaccout"].ToString();
                }

                wiw.WriteIntoDocument("bankuser", bankuser);
                wiw.WriteIntoDocument("banknum", banknum);
                wiw.WriteIntoDocument("bankname", bankname);
                wiw.WriteIntoDocument("signdate", signdate);

                //动态插入章
                if (withsign)
                {
                    //查询审批人
                    SqlConnection con_shenpi = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                    con_shenpi.Open();
                    string sql_shenpi = "select * from Approval where bianhao='" + quotationid + "' and result ='二级通过'";
                    SqlCommand cmd_shenpi = new SqlCommand(sql_shenpi, con_shenpi);
                    SqlDataReader dr_shenpi = cmd_shenpi.ExecuteReader();
                    string shenpiren = "";
                    if (dr_shenpi.Read())
                    {
                        shenpiren = dr_shenpi["fillname"].ToString();
                    }
                    dr_shenpi.Close();

                    //查询审批人的部门和职位
                    string dutyname = "";//职位
                    string dn = "";//部门
                    string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", shenpiren);
                    SqlCommand cmdstate = new SqlCommand(sql_dutyname, con_shenpi);
                    SqlDataReader dr = cmdstate.ExecuteReader();
                    if (dr.Read())
                    {
                        dn = dr["departmentname"].ToString();
                        dutyname = dr["dutyname"].ToString();
                    }
                    dr.Close();
                    con_shenpi.Close();
                    //当职位为总经理 董事长 系统管理员时盖陈总的章、需要总经理审批时盖陈总的章。否则盖该部门销售经理的章

                    string path = "";
                    if (Name == "深圳市倍测科技有限公司" || Name == "Shenzhen BCTC Technology Co.，Ltd.")
                    {
                        if (dutyname == "总经理" || dutyname == "系统管理员" || dutyname == "董事长")
                        {
                            if (dn == "销售龙华部")
                            {
                                //张总的章
                                path = Server.MapPath("Images/张伟/keji.png");
                            }
                            else
                            {
                                path = Server.MapPath("Images/keji.png");
                            }
                        }
                        else
                        {
                            if (dn == "销售大客户部")
                            {
                                switch (shenpiren)
                                {
                                    case "黄银":
                                        path = Server.MapPath("Images/黄银/keji.png");
                                        break;
                                    case "李小蓉":
                                        path = Server.MapPath("Images/李小蓉/keji.png");
                                        break;
                                    case "方瑞":
                                        path = Server.MapPath("Images/方瑞/keji.png");
                                        break;
                                    default:
                                        //方芳的章
                                        path = Server.MapPath("Images/方芳/keji.png");
                                        break;
                                }
                            }
                            else if (dn == "销售1部")
                            {
                                //周总的章
                                path = Server.MapPath("Images/周琴/keji.png");
                            }
                            else if (dn == "销售2部")
                            {
                                //李爽
                                path = Server.MapPath("Images/李爽/keji.png");
                            }
                            else if (dn == "销售化学部")
                            {
                                //龙景向
                                path = Server.MapPath("Images/龙景向/keji.png");
                            }
                            else if (dn == "销售龙华部")
                            {
                                //龚经理
                                path = Server.MapPath("Images/龚佳梅/keji.png");
                            }
                            else
                            {
                                //有些没章的暂用陈总的章
                                path = Server.MapPath("Images/keji.png");
                            }
                        }
                    }
                    else if (Name == "深圳市倍测检测有限公司")
                    {
                        if (dutyname == "总经理" || dutyname == "系统管理员" || dutyname == "董事长")
                        {
                            if (dn == "销售龙华部")
                            {
                                //张总的章
                                path = Server.MapPath("Images/张伟/jiance.png");
                            }
                            else
                            {
                                path = Server.MapPath("Images/jiance.png");
                            }
                        }
                        else
                        {
                            if (dn == "销售大客户部")
                            {
                                switch (shenpiren)
                                {
                                    case "黄银":
                                        path = Server.MapPath("Images/黄银/jiance.png");
                                        break;
                                    case "李小蓉":
                                        path = Server.MapPath("Images/李小蓉/jiance.png");
                                        break;
                                    case "方瑞":
                                        path = Server.MapPath("Images/方瑞/jiance.png");
                                        break;
                                    default:
                                        //方芳的章
                                        path = Server.MapPath("Images/方芳/jiance.png");
                                        break;
                                }
                            }
                            else if (dn == "销售1部")
                            {
                                //周总的章
                                path = Server.MapPath("Images/周琴/jiance.png");

                            }
                            else if (dn == "销售2部")
                            {
                                //李爽
                                path = Server.MapPath("Images/李爽/jiance.png");
                            }
                            else if (dn == "销售化学部")
                            {
                                //龙景向
                                path = Server.MapPath("Images/龙景向/jiance.png");
                            }
                            else if (dn == "销售龙华部")
                            {
                                //龚经理
                                path = Server.MapPath("Images/龚佳梅/jiance.png");
                            }
                            else
                            {
                                //有些没章的暂用陈总的章
                                path = Server.MapPath("Images/jiance.png");
                            }
                        }
                    }
                    else if (Name == "深圳市倍测科技有限公司东莞分公司")
                    {
                        path = Server.MapPath("Images/dg.png");
                    }
                    //else
                    //{
                    //    //标源
                    //    path = Server.MapPath("Images/biaoyuan.png");
                    //}
                    wiw.WriteIntoPicture("signpicture", path);
                }
            }
            catch (Exception ex)
            {
                //this.name.Text = ex.Message;
            }
            finally
            {
                wiw.Save_CloseDocument(SaveDocPath);
            }

            //这里是想在生产PDF后删除中间dot和doc文件
            if (File.Exists(Server.MapPath("OuotationPrinted/" + newfilename + ".doc")))
            {
                File.Delete(Server.MapPath("OuotationPrinted/" + newfilename + ".dot"));
                WordToPDF(Server.MapPath("OuotationPrinted/" + newfilename + ".doc"), Server.MapPath("OuotationPrinted/" + newfilename + ".pdf"));
            }
            if (File.Exists(Server.MapPath("OuotationPrinted/" + newfilename + ".pdf")))
            {
                File.Delete(Server.MapPath("OuotationPrinted/" + newfilename + ".doc"));
                //把文档添加到数据库中
                string addsql = "insert into QuotaionSign values('" + this.quotationid + "','" + newfilename + ".pdf','" + Server.MapPath("OuotationPrinted/" + newfilename + ".pdf") + "','" + Session["UserName"].ToString() + "','" + DateTime.Now.ToString() + "')";
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con1.Open();
                SqlCommand cmd = new SqlCommand(addsql, con1);
                cmd.ExecuteNonQuery();

                con1.Close();
            }
        }
        BindFile();
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
                for (int i = 0; i < MoneyPriceDot.Length - 1; i++)
                {
                    int numberIndex = Convert.ToInt32(MoneyPriceDot[i].ToString());
                    part2 += Number[numberIndex];
                    part2 += PriceDot[i];
                }
            }
        }
        part1 = part1.Replace("零仟", "");
        part1 = part1.Replace("零佰", "");
        part1 = part1.Replace("零拾", "");
        part1 = part1.Replace("零元", "元");
        part1 = part1.Replace("零零零万", "");
        part1 = part1.Replace("零零零", "零");
        part1 = part1.Replace("零零", "");
        part1 = part1.Replace("零万", "万");
        part1 = part1.Replace("零亿", "亿");
        part2 = part2.Replace("零角", "零");
        part2 = part2.Replace("零分", "零");
        part2 = part2.Replace("零厘", "");
        part2 = part2.Replace("零零", "零");
        return part1 + part2;
    }

    public class WriteIntoWord
    {

        ApplicationClass app = null;   //定义应用程序对象 
        Document doc = null;   //定义 word 文档对象
        Object missing = System.Reflection.Missing.Value; //定义空变量
        Object isReadOnly = false;
        // 向 word 文档写入数据 
        public void OpenDocument(string FilePath)
        {
            object filePath = FilePath;//文档路径
            app = new ApplicationClass(); //打开文档 
            doc = app.Documents.Open(ref filePath, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            doc.Activate();//激活文档
        }

        /// <summary> 
        /// </summary> 
        ///<param name="parLableName">域标签</param> 
        /// <param name="parFillName">写入域中的内容</param> 
        /// 
        //打开word，将对应数据写入word里对应书签域

        public void WriteIntoDocument(string BookmarkName, string FillName)
        {
            object bookmarkName = BookmarkName;
            Bookmark bm = doc.Bookmarks.get_Item(ref bookmarkName);//返回书签 
            bm.Range.Text = " " + FillName;//设置书签域的内容
        }

        //添加图片，需要修改,为嵌入，不能用
        public void WriteIntoPicture(string BookmarkName, string filename)
        {
            object bookmarkName = BookmarkName;
            Bookmark bm = doc.Bookmarks.get_Item(ref bookmarkName);//返回书签
            bm.Select();
            Selection se1 = app.Selection;

            InlineShape il = se1.InlineShapes.AddPicture(filename);
            //设置图片大小
            il.Width = 108;
            il.Height = 100;
            // 把图片设置为文字下方
            il.ConvertToShape().WrapFormat.Type = Microsoft.Office.Interop.Word.WdWrapType.wdWrapBehind;
        }

        //将项目表写入表格,需要设置格式   中文
        public void WriteIntoDocumentItemtable(string BookmarkName, DataSet dataset, decimal totalmoney, string moneychines, string reamrk, string isvat, string currency, string coupon)
        {
            if (currency == "人民币")
            {
                currency = "RMB  ";
            }
            else
            {
                currency = "USD  ";
            }
            int row = 0;

            //isvat为空也代表含税、因为老数据都是默认含税的
            if ((string.IsNullOrEmpty(coupon) || coupon == "0.00" || coupon == "&nbsp;") && (isvat == "是" || string.IsNullOrEmpty(isvat)))
            {
                row = 3;
            }
            else if ((string.IsNullOrEmpty(coupon) || coupon == "0.00" || coupon == "&nbsp;") && isvat == "否")
            {
                row = 3;
            }
            else if ((string.IsNullOrEmpty(coupon) || coupon == "0.00" || coupon == "&nbsp;") && isvat == "增值税")
            {
                row = 5;
            }
            else if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;" && (isvat == "是" || string.IsNullOrEmpty(isvat)))
            {
                row = 4;
            }
            else if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;" && isvat == "否")
            {
                row = 4;
            }
            else if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;" && isvat == "增值税")
            {
                row = 6;
            }

            object bookmarkName = BookmarkName;
            Bookmark bm = doc.Bookmarks.get_Item(ref bookmarkName);//返回书签 
            Range range = bm.Range;
            range.Tables.Add(range, dataset.Tables[0].Rows.Count + row, 7);//创建表格
            Microsoft.Office.Interop.Word.Table tb = range.Tables[1];
            //外边框
            tb.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            tb.Borders.OutsideLineWidth = Microsoft.Office.Interop.Word.WdLineWidth.wdLineWidth075pt;
            //内边框
            tb.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            tb.Borders.InsideLineWidth = Microsoft.Office.Interop.Word.WdLineWidth.wdLineWidth075pt;
            //文字居中,表头居中
            //tb.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;文字剧左
            tb.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter; //文字剧中
            tb.LeftPadding = 2;

            //表格居中
            tb.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;

            //设置行间距
            tb.Range.Paragraphs.LineSpacing = 13f;
            //垂直居中
            tb.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            //字体大小
            tb.Range.Font.Size = 12;
            //设置表格文字和单元格上下边框距离
            tb.TopPadding = 3;
            tb.BottomPadding = 3;

            tb.Columns[1].Width = 75f;
            tb.Columns[2].Width = 75f;
            tb.Columns[3].Width = 75f;
            tb.Columns[4].Width = 70f;
            tb.Columns[5].Width = 60f;
            tb.Columns[6].Width = 60f;
            tb.Columns[7].Width = 60f;

            // tb.set_Style("网格型¨ª");
            tb.Cell(1, 1).Range.Text = "Product/Model No\n产品/型号";
            //tb.Cell(1, 2).Range.Text = "Model No. \n型号";
            tb.Cell(1, 2).Range.Text = "Service Item \n服务项目";
            tb.Cell(1, 3).Range.Text = "Ref. Standard \n标准";
            tb.Cell(1, 4).Range.Text = "Cost \n费用";
            tb.Cell(1, 5).Range.Text = "Sample Qty \n样品数量";
            tb.Cell(1, 6).Range.Text = "Lead Time \n周期";
            tb.Cell(1, 7).Range.Text = "Remark \n备注";
            tb.Rows[1].Height = 25f;

            #region 清空相同的数据
            //{
            //    #region 
            //    for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
            //    {
            //        string cell = dataset.Tables[0].Rows[i][0].ToString();
            //        if (string.IsNullOrEmpty(cell))
            //        {
            //            continue;
            //        }
            //        for (int j = i; j < dataset.Tables[0].Rows.Count; j++)
            //        {
            //            //第一行不用比较
            //            if (j != i)
            //            {
            //                if (dataset.Tables[0].Rows[j][0].ToString() == cell)
            //                {
            //                    dataset.Tables[0].Rows[j][0] = "";
            //                }
            //            }
            //        }
            //    }
            //    #endregion
            //}
            #endregion

            for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    tb.Cell(i + 2, j + 1).Range.Text = dataset.Tables[0].Rows[i][j].ToString();
                }
            }

            Dictionary<string, List<int>> dic = new Dictionary<string, List<int>>();
            {
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    string key = dataset.Tables[0].Rows[i][0].ToString();
                    if (dic.ContainsKey(key))
                    {
                        dic[key].Add(i + 2);
                    }
                    else
                    {
                        List<int> newList = new List<int>();
                        newList.Add(i + 2);
                        dic.Add(key, newList);
                    }
                }
            }

            //纵向合并单元格
            foreach (string item in dic.Keys)
            {
                List<int> listData = dic[item];
                //拿取第一个位置（合并得起始行）
                int rowData = listData[0];
                for (int i = 1; i < listData.Count; i++)
                {
                    int number = listData[i - 1];
                    tb.Cell(number + 1, 1).Range.Text = "";
                }
                if (listData.Count > 1)
                {
                    int number = listData.Last();
                    tb.Cell(rowData, 1).Merge(tb.Cell(number, 1));
                }
            }

            decimal tax = totalmoney * 0.06m; //没有优惠金额的税金
            decimal summoney = totalmoney + tax;

            switch (row)
            {
                case 3:
                    if (isvat == "否")
                    {
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.Text = "Total Amount 合计:";
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 3).Range.Text = currency + totalmoney.ToString("f2") + "    " + moneychines + "整";
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 2));//倒数第二行第一列合并两个单元格
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 6));
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.Text = "备注:";
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 3).Range.Text = reamrk;
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 2));
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 6));
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    }
                    else
                    {
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.Text = "Total Amount 合计:";
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 3).Range.Text = currency + totalmoney.ToString("f2") + "    " + moneychines + "整" + "（报价已包含6%增值税）";
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 2));//倒数第二行第一列合并两个单元格
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 6));
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.Text = "备注:";
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 3).Range.Text = reamrk;
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 2));
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 6));
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    }
                    break;
                case 4:
                    if (isvat == "否")
                    {
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.Text = "Total Amount 合计:";
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 3).Range.Text = currency + totalmoney.ToString("f2") + "    " + moneychines + "整";
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 2));
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 6));
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.Text = "After Dicount优惠后金额:";
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 3).Range.Text = currency + Convert.ToDecimal(coupon).ToString("f2") + "    " + ConvertToChinese1(Convert.ToDecimal(coupon).ToString("f2")) + "整";
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 2));
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 6));
                        tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.Text = "备注:";
                        tb.Cell(dataset.Tables[0].Rows.Count + 4, 3).Range.Text = reamrk;
                        tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 2));
                        tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 6));
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    }
                    else
                    {
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.Text = "Total Amount 合计:";
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 3).Range.Text = currency + totalmoney.ToString("f2") + "    " + moneychines + "整" + " (已含6%增值税)";
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 2));
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 6));
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.Text = "After Dicount优惠后金额:";
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 3).Range.Text = currency + Convert.ToDecimal(coupon).ToString("f2") + "    " + ConvertToChinese1(Convert.ToDecimal(coupon).ToString("f2")) + "整";
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 2));
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 6));
                        tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.Text = "备注:";
                        tb.Cell(dataset.Tables[0].Rows.Count + 4, 3).Range.Text = reamrk;
                        tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 2));
                        tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 6));
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    }
                    break;
                case 5:
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.Text = "Total Amount 合计:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 3).Range.Text = currency + totalmoney.ToString("f2") + "    " + moneychines + "整";
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.Text = "Tax       税   金:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 3).Range.Text = currency + tax.ToString("f2") + "    " + ConvertToChinese1(tax.ToString("f2")) + "整";
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.Text = "Total Price 总   价:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 3).Range.Text = currency + summoney.ToString("f2") + "    " + ConvertToChinese1(summoney.ToString("f2")) + "整";
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 1).Range.Text = "备注:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 3).Range.Text = reamrk;
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 5, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 5, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    break;
                case 6:
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.Text = "Original Price 原    价:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 3).Range.Text = currency + totalmoney.ToString("f2") + "    " + moneychines + "整";
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.Text = "After Dicount优惠后金额:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 3).Range.Text = currency + Convert.ToDecimal(coupon).ToString("f2") + "    " + ConvertToChinese1(Convert.ToDecimal(coupon).ToString("f2")) + "整";
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 6));
                    decimal tax1 = Convert.ToDecimal(coupon) * 0.06m;
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.Text = "Tax       税   金:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 3).Range.Text = currency + tax1.ToString("f2") + "    " + ConvertToChinese1(tax1.ToString("f2")) + "整";
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 1).Range.Text = "Total Price 总   价:";
                    decimal pricesum = Convert.ToDecimal(coupon) + tax1;
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 3).Range.Text = currency + pricesum.ToString("f2") + "    " + ConvertToChinese1(pricesum.ToString("f2")) + "整";
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 5, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 5, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 6, 1).Range.Text = "备注:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 6, 3).Range.Text = reamrk;
                    tb.Cell(dataset.Tables[0].Rows.Count + 6, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 6, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 6, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 6, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 6, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 6, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    break;
            }
        }

        //英文
        public void EnglishWriteIntoDocumentItemtable(string BookmarkName, DataSet dataset, decimal totalmoney, string moneychines, string reamrk, string isvat, string currency, string coupon)
        {
            if (currency == "人民币")
            {
                currency = "RMB  ";
            }
            else
            {
                currency = "USD  ";
            }
            int row = 0;
            //isvat为空也代表含税、因为老数据都是默认含税的
            if ((string.IsNullOrEmpty(coupon) || coupon == "0.00" || coupon == "&nbsp;") && (isvat == "是" || string.IsNullOrEmpty(isvat)))
            {
                row = 3;
            }
            else if ((string.IsNullOrEmpty(coupon) || coupon == "0.00" || coupon == "&nbsp;") && isvat == "否")
            {
                row = 3;
            }
            else if ((string.IsNullOrEmpty(coupon) || coupon == "0.00" || coupon == "&nbsp;") && isvat == "增值税")
            {
                row = 5;
            }
            else if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;" && (isvat == "是" || string.IsNullOrEmpty(isvat)))
            {
                row = 4;
            }
            else if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;" && isvat == "否")
            {
                row = 4;
            }
            else if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;" && isvat == "增值税")
            {
                row = 6;
            }

            object bookmarkName = BookmarkName;
            Bookmark bm = doc.Bookmarks.get_Item(ref bookmarkName);//返回书签 
            Range range = bm.Range;
            range.Tables.Add(range, dataset.Tables[0].Rows.Count + row, 7);//创建表格
            Microsoft.Office.Interop.Word.Table tb = range.Tables[1];
            //外边框
            tb.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            tb.Borders.OutsideLineWidth = Microsoft.Office.Interop.Word.WdLineWidth.wdLineWidth075pt;
            //内边框
            tb.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            tb.Borders.InsideLineWidth = Microsoft.Office.Interop.Word.WdLineWidth.wdLineWidth075pt;
            //文字居中,表头居中
            //tb.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;文字剧左
            tb.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter; //文字剧中
            tb.LeftPadding = 2;

            //表格居中
            tb.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;

            //设置行间距
            tb.Range.Paragraphs.LineSpacing = 13f;
            //垂直居中
            tb.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            //字体大小
            tb.Range.Font.Size = 12;
            //设置表格文字和单元格上下边框距离
            tb.TopPadding = 3;
            tb.BottomPadding = 3;

            tb.Columns[1].Width = 75f;
            tb.Columns[2].Width = 75f;
            tb.Columns[3].Width = 75f;
            tb.Columns[4].Width = 70f;
            tb.Columns[5].Width = 60f;
            tb.Columns[6].Width = 60f;
            tb.Columns[7].Width = 60f;

            // tb.set_Style("网格型¨ª");
            tb.Cell(1, 1).Range.Text = "Product/Model No";
            //tb.Cell(1, 2).Range.Text = "Model No. \n型号";
            tb.Cell(1, 2).Range.Text = "Service Item";
            tb.Cell(1, 3).Range.Text = "Ref. Standard";
            tb.Cell(1, 4).Range.Text = "Cost";
            tb.Cell(1, 5).Range.Text = "Sample Qty";
            tb.Cell(1, 6).Range.Text = "Lead Time";
            tb.Cell(1, 7).Range.Text = "Remark";
            tb.Rows[1].Height = 25f;


            for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    tb.Cell(i + 2, j + 1).Range.Text = dataset.Tables[0].Rows[i][j].ToString();
                }
            }
            Dictionary<string, List<int>> dic = new Dictionary<string, List<int>>();
            {
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    string key = dataset.Tables[0].Rows[i][0].ToString();
                    if (dic.ContainsKey(key))
                    {
                        dic[key].Add(i + 2);
                    }
                    else
                    {
                        List<int> newList = new List<int>();
                        newList.Add(i + 2);
                        dic.Add(key, newList);
                    }
                }
            }
            //纵向合并单元格
            foreach (string item in dic.Keys)
            {
                List<int> listData = dic[item];
                //拿取第一个位置（合并得起始行）
                int rowData = listData[0];
                for (int i = 1; i < listData.Count; i++)
                {
                    int number = listData[i - 1];
                    tb.Cell(number + 1, 1).Range.Text = "";
                }
                if (listData.Count > 1)
                {
                    int number = listData.Last();
                    tb.Cell(rowData, 1).Merge(tb.Cell(number, 1));
                }
            }

            decimal tax = totalmoney * 0.06m; //没有优惠金额的税金
            decimal summoney = totalmoney + tax;
            switch (row)
            {
                case 3:
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.Text = "Total Amount:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 3).Range.Text = currency + totalmoney.ToString("f2");
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 2));//倒数第二行第一列合并两个单元格
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.Text = "Remark:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 3).Range.Text = reamrk;
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    break;
                case 4:
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.Text = "Total Amount:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 3).Range.Text = currency + totalmoney.ToString("f2");
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.Text = "After Dicount:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 3).Range.Text = currency + Convert.ToDecimal(coupon).ToString("f2");
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.Text = "Remark:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 3).Range.Text = reamrk;
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    break;
                case 5:
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.Text = "Total Amount:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 3).Range.Text = currency + totalmoney.ToString("f2");
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.Text = "Tax:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 3).Range.Text = currency + tax.ToString("f2");
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.Text = "Total Price:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 3).Range.Text = currency + summoney.ToString("f2");
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 1).Range.Text = "Remark:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 3).Range.Text = reamrk;
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 5, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 5, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    break;
                case 6:
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.Text = "Original Price:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 3).Range.Text = currency + totalmoney.ToString("f2");
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.Text = "After Dicount:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 3).Range.Text = currency + Convert.ToDecimal(coupon).ToString("f2");
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 6));
                    decimal tax1 = Convert.ToDecimal(coupon) * 0.06m;
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.Text = "Tax:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 3).Range.Text = currency + tax1.ToString("f2");
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 4, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 1).Range.Text = "Total Price:";
                    decimal pricesum = Convert.ToDecimal(coupon) + tax1;
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 3).Range.Text = currency + pricesum.ToString("f2");
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 5, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 5, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 6, 1).Range.Text = "Remark:";
                    tb.Cell(dataset.Tables[0].Rows.Count + 6, 3).Range.Text = reamrk;
                    tb.Cell(dataset.Tables[0].Rows.Count + 6, 1).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 6, 2));
                    tb.Cell(dataset.Tables[0].Rows.Count + 6, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 6, 6));
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 6, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 4, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 5, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    tb.Cell(dataset.Tables[0].Rows.Count + 6, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    break;
            }
        }


        //将条款表写入表格,需要设置格式
        public void WriteIntoDocumentclausetable(string BookmarkName, DataSet dataset)
        {
            object bookmarkName = BookmarkName;
            Bookmark bm = doc.Bookmarks.get_Item(ref bookmarkName);//返回书签 
            Range range = bm.Range;
            range.Tables.Add(range, dataset.Tables[0].Rows.Count, 2);
            Microsoft.Office.Interop.Word.Table tb = range.Tables[1];
            tb.Columns[1].Width = 25f;
            tb.Columns[2].Width = 465f;
            //字体大小
            tb.Range.Font.Size = 9;
            // tb.set_Style("网格型¨ª");


            for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
            {
                tb.Cell(i + 1, 1).Range.Text = "(" + (i + 1).ToString() + ")";
                tb.Cell(i + 1, 2).Range.Text = dataset.Tables[0].Rows[i][0].ToString();
                //for (int j = 0; j < 2; j++)
                //{
                //    tb.Cell(i + 1, j + 1).Range.Text = dataset.Tables[0].Rows[i][j].ToString();
                //}
            }
        }

        /// <summary> 
        /// 保存并关闭 
        /// </summary> 
        /// <param name="parSaveDocPath">文档另存为的路径</param>
        /// 
        public void Save_CloseDocument(string SaveDocPath)
        {
            object savePath = SaveDocPath;  //文档另存为的路径 
            Object saveChanges = app.Options.BackgroundSave;//文档另存为 
            doc.SaveAs(ref savePath, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing);
            doc.Close(ref saveChanges, ref missing, ref missing);//关闭文档
            app.Quit(ref missing, ref missing, ref missing);//关闭应用程序
        }
        public string ConvertToChinese1(string stringNumber)
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
                    for (int i = 0; i < MoneyPriceDot.Length - 1; i++)
                    {
                        int numberIndex = Convert.ToInt32(MoneyPriceDot[i].ToString());
                        part2 += Number[numberIndex];
                        part2 += PriceDot[i];
                    }
                }
            }
            part1 = part1.Replace("零仟", "");
            part1 = part1.Replace("零佰", "");
            part1 = part1.Replace("零拾", "");
            part1 = part1.Replace("零元", "元");
            part1 = part1.Replace("零零零万", "");
            part1 = part1.Replace("零零零", "零");
            part1 = part1.Replace("零零", "");
            part1 = part1.Replace("零万", "万");
            part1 = part1.Replace("零亿", "亿");
            part2 = part2.Replace("零角", "零");
            part2 = part2.Replace("零分", "零");
            part2 = part2.Replace("零厘", "");
            part2 = part2.Replace("零零", "零");
            return part1 + part2;
        }
    }

    #region
    /// <summary>
    /// 将word文档转换成PDF格式
    /// </summary>
    /// <param name="sourcePath">源文件路径</param>
    /// <param name="targetPath">目标文件路径</param>
    /// <returns></returns>
    //private bool WordConvertPDF(string sourcePath, string targetPath)
    //{
    //    bool result;
    //    Microsoft.Office.Interop.Word.WdExportFormat exportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF;   //PDF格式
    //    object paramMissing = Type.Missing;
    //    Microsoft.Office.Interop.Word.ApplicationClass wordApplication = new Microsoft.Office.Interop.Word.ApplicationClass();
    //    Microsoft.Office.Interop.Word.Document wordDocument = null;
    //    try
    //    {
    //        object paramSourceDocPath = sourcePath;
    //        string paramExportFilePath = targetPath;

    //        Microsoft.Office.Interop.Word.WdExportFormat paramExportFormat = exportFormat;
    //        bool paramOpenAfterExport = false;
    //        Microsoft.Office.Interop.Word.WdExportOptimizeFor paramExportOptimizeFor =
    //                Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
    //        Microsoft.Office.Interop.Word.WdExportRange paramExportRange = Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument;
    //        int paramStartPage = 0;
    //        int paramEndPage = 0;
    //        Microsoft.Office.Interop.Word.WdExportItem paramExportItem = Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentContent;
    //        bool paramIncludeDocProps = true;
    //        bool paramKeepIRM = true;
    //        Microsoft.Office.Interop.Word.WdExportCreateBookmarks paramCreateBookmarks =
    //                Microsoft.Office.Interop.Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
    //        bool paramDocStructureTags = true;
    //        bool paramBitmapMissingFonts = true;
    //        bool paramUseISO19005_1 = false;

    //        wordDocument = wordApplication.Documents.Open(
    //                ref paramSourceDocPath, ref paramMissing, ref paramMissing,
    //                ref paramMissing, ref paramMissing, ref paramMissing,
    //                ref paramMissing, ref paramMissing, ref paramMissing,
    //                ref paramMissing, ref paramMissing, ref paramMissing,
    //                ref paramMissing, ref paramMissing, ref paramMissing,
    //                ref paramMissing);

    //        if (wordDocument != null)
    //        {
    //            wordDocument.ExportAsFixedFormat(paramExportFilePath,
    //                    paramExportFormat, paramOpenAfterExport,
    //                    paramExportOptimizeFor, paramExportRange, paramStartPage,
    //                    paramEndPage, paramExportItem, paramIncludeDocProps,
    //                    paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
    //                    paramBitmapMissingFonts, paramUseISO19005_1,
    //                    ref paramMissing);
    //            result = true;
    //        }
    //        else
    //            result = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        result = false;
    //    }
    //    finally
    //    {
    //        if (wordDocument != null)
    //        {
    //            wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
    //            wordDocument = null;
    //        }
    //        if (wordApplication != null)
    //        {
    //            wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
    //            wordApplication = null;
    //        }
    //        GC.Collect();
    //        GC.WaitForPendingFinalizers();
    //        GC.Collect();
    //        GC.WaitForPendingFinalizers();
    //    }
    //    return result;
    //}
    #endregion

    public bool WordToPDF(string sourcePath, string targetPath)
    {
        Object oMissing = System.Reflection.Missing.Value; //定义空变量
        bool result = false;
        Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
        Document document = null;
        try
        {
            application.Visible = false;
            document = application.Documents.Open(sourcePath);
            document.ExportAsFixedFormat(targetPath, WdExportFormat.wdExportFormatPDF);
            result = true;
        }
        catch (Exception e)
        {
            this.name.Text = e.Message;
        }
        finally
        {

            document.Close();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(document);
            document = null;
            application.Quit(ref oMissing, ref oMissing, ref oMissing);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
            application = null;
            System.GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        return result;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownFile")
        {
            int id = int.Parse(e.CommandArgument.ToString());

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sqlDown = "select fileurl,filename from QuotaionSign where id='" + id + "'";
            SqlCommand cmdDown = new SqlCommand(sqlDown, con);
            SqlDataReader adDown = cmdDown.ExecuteReader();
            string fileurl = "";
            string filename = "";
            if (adDown.Read())
            {
                fileurl = adDown["fileurl"].ToString();
                filename = adDown["filename"].ToString();
            }
            adDown.Close();
            con.Close();

            if (fileurl != "")
            {
                string filePath = fileurl;
                FileStream fs = new FileStream(filePath, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }
    }

    protected void btn_englishPDF_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select *,(select customname from Customer where kehuid=CustomerLinkMan.customerid) as customname from CustomerLinkMan  where id =(select top 1 linkid from baojialink where baojiaid='" + quotationid + "' order by id) order by  id desc";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["telephone"].ToString()) && string.IsNullOrEmpty(ds.Tables[0].Rows[0]["mobile"].ToString()) && string.IsNullOrEmpty(ds.Tables[0].Rows[0]["email"].ToString()))
                {
                    Literal1.Text = "<script>alert('客户联系人必须要有一种联系方式')</script>";
                }
                else
                {
                    if (this.name.Text != "")
                    {
                        string sql_bumen = "select departmentname from UserInfo where UserName='" + Session["Username"].ToString() + "'";
                        SqlCommand cmd_bumen = new SqlCommand(sql_bumen, con);
                        SqlDataReader dr_bumen = cmd_bumen.ExecuteReader();
                        string bumen = "";
                        if (dr_bumen.Read())
                        {
                            bumen = dr_bumen["departmentname"].ToString();
                        }
                        dr_bumen.Close();
                        //防止用户重复点击
                        btn_englishPDF.Enabled = false;
                        Toprintenglish(bumen);
                        btn_englishPDF.Enabled = true;
                    }
                }
            }
            else
            {
                Literal1.Text = "<script>alert('请先在报价单上选择联系人。')</script>";
            }
        }
    }

    public void Toprintenglish(string bumen)
    {
        Random ran = new Random(Guid.NewGuid().GetHashCode());
        int number = ran.Next(500);
        string numberstr = number.ToString("000");
        string urlbase = this.name.Text + DateTime.Now.ToString("yyyy-MM-dd") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00");
        string newfilename = urlbase + number;
        string baseFilePath = "";
        string copybasefilePath = "";
        if (withsign)
        {
            baseFilePath = Server.MapPath("Englishquosigned.dot");
        }
        else
        {
            baseFilePath = Server.MapPath("Englishquonotsign.dot");
        }
        if (File.Exists(baseFilePath))
        {
            copybasefilePath = Server.MapPath("OuotationPrinted/" + newfilename + ".dot");
            File.Copy(baseFilePath, copybasefilePath, true);
        }
        else
        {
            return;
        }
        if (File.Exists(Server.MapPath("OuotationPrinted/" + newfilename + ".dot")))
        {
            WriteIntoWord wiw = new WriteIntoWord();
            string FilePath = Server.MapPath("OuotationPrinted/" + newfilename + ".dot");     //模板路径
            string SaveDocPath = Server.MapPath("OuotationPrinted/" + newfilename + ".doc");
            wiw.OpenDocument(FilePath);
            try
            {
                string customername = "";
                string customerlinkman = "";
                string customertel = "";
                string customermobile = "";
                string customeremail = "";
                string saltel = "";
                string salmobile = "";
                string saleemail = "";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string getcustomerbasesql = "select *,(select customname from Customer where kehuid=CustomerLinkMan.customerid) as customname from CustomerLinkMan  where id =(select top 1 linkid from baojialink where baojiaid='" + quotationid + "') order by  id desc";
                SqlCommand getcustomerbasecmd = new SqlCommand(getcustomerbasesql, con);
                SqlDataReader getcustomerbasead = getcustomerbasecmd.ExecuteReader();
                if (getcustomerbasead.Read())
                {
                    customername = getcustomerbasead["customname"].ToString();
                    customerlinkman = getcustomerbasead["name"].ToString();
                    customertel = getcustomerbasead["telephone"].ToString();
                    customeremail = getcustomerbasead["email"].ToString();
                    customermobile = getcustomerbasead["mobile"].ToString();
                }
                getcustomerbasead.Close();
                string getsalesbasesql = "select * from userinfo where username=(select responser from baojiabiao where baojiaid='" + quotationid + "')";
                SqlCommand getsalesbasecmd = new SqlCommand(getsalesbasesql, con);
                SqlDataReader getsalesbasead = getsalesbasecmd.ExecuteReader();
                if (getsalesbasead.Read())
                {
                    saltel = getsalesbasead["UserName"].ToString();
                    salmobile = getsalesbasead["yidong"].ToString();
                    saleemail = getsalesbasead["youxiang"].ToString();
                }
                getsalesbasead.Close();

                string getTestitemsql = "select BaoJiaChanPing.name +'/'+ [type] as one,ceshiname,biaozhun,total,yp,zhouqi,BaoJiaCPXiangMu.beizhu from BaoJiaCPXiangMu left join BaoJiaChanPing on BaoJiaCPXiangMu.cpid=BaoJiaChanPing.id where BaoJiaCPXiangMu.baojiaid='" + quotationid + "' order by BaoJiaChanPing.id asc";
                SqlDataAdapter getTestitemad = new SqlDataAdapter(getTestitemsql, con);
                DataSet getTestitemds = new DataSet();
                getTestitemad.Fill(getTestitemds);
                decimal totalmoney = 0m;
                for (int i = 0; i < getTestitemds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        getTestitemds.Tables[0].Rows[i]["total"] = Convert.ToDecimal(getTestitemds.Tables[0].Rows[i]["total"].ToString()).ToString("f2");
                        totalmoney += Convert.ToDecimal(getTestitemds.Tables[0].Rows[i]["total"].ToString());
                    }
                    catch
                    {
                    }
                }
                string totalchines = ConvertToChinese(totalmoney.ToString());

                string getclausesql = "select tiaokuan from Clause where baojiaid='" + quotationid + "' order by id asc";
                SqlDataAdapter getclausead = new SqlDataAdapter(getclausesql, con);
                DataSet getclauseds = new DataSet();
                getclausead.Fill(getclauseds);
                string getbaojiabase = "select * from baojiabiao where baojiaid='" + quotationid + "'";
                string bankuser = "";
                string banknum = "";
                string bankname = "";
                string signdate = "";
                string paytype = "";
                string zhangdan = "";
                string shoukuanjine = "";
                string zongjine = "";
                string beizhu = "";
                string isvat = "";
                string coupon = "";
                string currency = "";
                SqlCommand getbaojiabasecmd = new SqlCommand(getbaojiabase, con);
                SqlDataReader getbaojiabasead = getbaojiabasecmd.ExecuteReader();
                if (getbaojiabasead.Read())
                {
                    zhangdan = getbaojiabasead["zhangdan"].ToString();
                    signdate = Convert.ToDateTime(getbaojiabasead["filltime"].ToString()).ToString("yyyy-MM-dd");
                    paytype = getbaojiabasead["paymentmethod"].ToString();
                    beizhu = getbaojiabasead["BeiZhu"].ToString();
                    isvat = getbaojiabasead["isVAT"].ToString();//是否含税
                    currency = getbaojiabasead["currency"].ToString();//币种
                    try
                    {
                        shoukuanjine = Convert.ToDecimal(getbaojiabasead["thefirst"].ToString()).ToString("f2");
                    }
                    catch
                    {
                        shoukuanjine = "0m";
                    }
                    zongjine = Convert.ToDecimal(getbaojiabasead["zhehoujia"].ToString()).ToString("f2");
                    if (string.IsNullOrEmpty(getbaojiabasead["coupon"].ToString()) || getbaojiabasead["coupon"].ToString() == "&nbsp;")
                    {
                        coupon = "";
                    }
                    else
                    {
                        coupon = Convert.ToDecimal(getbaojiabasead["coupon"].ToString()).ToString("f2");
                    }
                }
                getbaojiabasead.Close();

                string getbanksql = "select * from Bankaccount where id='" + zhangdan + "'";
                SqlDataAdapter getbankad = new SqlDataAdapter(getbanksql, con);
                DataSet getbankds = new DataSet();
                getbankad.Fill(getbankds);

                //公司名称
                string Name = "";
                if (getbankds.Tables[0].Rows[0]["Name"].ToString() == "周朝政")
                {
                    Name = "深圳市倍测科技有限公司";
                }
                //else if (getbankds.Tables[0].Rows[0]["Name"].ToString() == "蒋梅" && bumen.Trim() == "标源销售部")
                //{
                //    Name = "深圳市标源质量技术服务有限公司";
                //}
                else
                {
                    Name = getbankds.Tables[0].Rows[0]["Name"].ToString();
                }

                con.Close();
                wiw.WriteIntoDocument("Name", Name);
                wiw.WriteIntoDocument("Customer", customername);
                wiw.WriteIntoDocument("Linkman", customerlinkman);
                wiw.WriteIntoDocument("Custel", customertel);
                wiw.WriteIntoDocument("Cusmobile", customermobile);
                wiw.WriteIntoDocument("Cusemail", customeremail);

                wiw.WriteIntoDocument("Saletel", saltel);
                wiw.WriteIntoDocument("Salemobile", salmobile);
                wiw.WriteIntoDocument("Saleemail", saleemail);
                wiw.WriteIntoDocument("Salequotationid", quotationid);
                wiw.EnglishWriteIntoDocumentItemtable("Itemlist", getTestitemds, totalmoney, totalchines, beizhu, isvat, currency, coupon);
                if (getclauseds.Tables[0].Rows.Count > 0)
                {
                    wiw.WriteIntoDocumentclausetable("clauselist", getclauseds);
                }
                else
                {
                    wiw.WriteIntoDocument("clauselist", "无");
                }
                if (paytype == "首款")
                {
                    //如果存在优惠金额、则用优惠后金额代替总金额,如果不含税还要加上税金
                    if (isvat == "增值税")
                    {
                        if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;")
                        {
                            decimal vat = Convert.ToDecimal(coupon) * 0.06m;
                            decimal pricesum = (Convert.ToDecimal(coupon) - Convert.ToDecimal(shoukuanjine)) + vat;
                            wiw.WriteIntoDocument("moneypay", "    The first:" + shoukuanjine + "      balance payment:" + pricesum.ToString("f2"));
                        }
                        else
                        {
                            decimal vat = Convert.ToDecimal(zongjine) * 0.06m;
                            decimal pricesum = (Convert.ToDecimal(zongjine) - Convert.ToDecimal(shoukuanjine)) + vat;
                            wiw.WriteIntoDocument("moneypay", "    The first:" + shoukuanjine + "      balance payment:" + pricesum.ToString("f2"));
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;")
                        {
                            wiw.WriteIntoDocument("moneypay", "    The first:" + shoukuanjine + "      balance payment:" + (Convert.ToDecimal(coupon) - Convert.ToDecimal(shoukuanjine)).ToString("f2"));
                        }
                        else
                        {
                            wiw.WriteIntoDocument("moneypay", "    The first:" + shoukuanjine + "      balance payment:" + (Convert.ToDecimal(zongjine) - Convert.ToDecimal(shoukuanjine)).ToString("f2"));
                        }
                    }
                }
                else
                {
                    if (isvat == "增值税")
                    {
                        if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;")
                        {
                            decimal vat = Convert.ToDecimal(coupon) * 0.06m;
                            decimal pricesum = Convert.ToDecimal(coupon) + vat;
                            wiw.WriteIntoDocument("moneypay", "Pay the full amount before the test starts:" + pricesum.ToString("f2"));
                        }
                        else
                        {
                            decimal vat = Convert.ToDecimal(zongjine) * 0.06m;
                            decimal pricesum = Convert.ToDecimal(zongjine) + vat;
                            wiw.WriteIntoDocument("moneypay", "Pay the full amount before the test starts:" + pricesum);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(coupon) && coupon != "0.00" && coupon != "&nbsp;")
                        {
                            wiw.WriteIntoDocument("moneypay", "Pay the full amount before the test starts:" + Convert.ToDecimal(coupon));
                        }
                        else
                        {
                            wiw.WriteIntoDocument("moneypay", "Pay the full amount before the test starts:" + Convert.ToDecimal(zongjine));
                        }
                    }
                }
                if (getbankds.Tables[0].Rows.Count > 0)
                {
                    bankuser = getbankds.Tables[0].Rows[0]["Name"].ToString();
                    banknum = getbankds.Tables[0].Rows[0]["account"].ToString();
                    bankname = getbankds.Tables[0].Rows[0]["openaccout"].ToString();
                }

                wiw.WriteIntoDocument("bankuser", bankuser);
                wiw.WriteIntoDocument("banknum", banknum);
                wiw.WriteIntoDocument("bankname", bankname);
                wiw.WriteIntoDocument("signdate", signdate);

                //动态插入章
                if (withsign)
                {
                    //查询审批人
                    SqlConnection con_shenpi = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                    con_shenpi.Open();
                    string sql_shenpi = "select * from Approval where bianhao='" + quotationid + "' and result ='二级通过'";
                    SqlCommand cmd_shenpi = new SqlCommand(sql_shenpi, con_shenpi);
                    SqlDataReader dr_shenpi = cmd_shenpi.ExecuteReader();
                    string shenpiren = "";
                    if (dr_shenpi.Read())
                    {
                        shenpiren = dr_shenpi["fillname"].ToString();
                    }
                    dr_shenpi.Close();

                    //查询审批人的部门和职位
                    string dutyname = "";//职位
                    string dn = "";//部门
                    string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", shenpiren);
                    SqlCommand cmdstate = new SqlCommand(sql_dutyname, con_shenpi);
                    SqlDataReader dr = cmdstate.ExecuteReader();
                    if (dr.Read())
                    {
                        dn = dr["departmentname"].ToString();
                        dutyname = dr["dutyname"].ToString();
                    }
                    dr.Close();
                    con_shenpi.Close();

                    //当职位为总经理 董事长 系统管理员时盖陈总的章、需要总经理审批时盖陈总的章。否则盖该部门销售经理的章
                    string path = "";
                    if (Name == "深圳市倍测科技有限公司" || Name == "Shenzhen BCTC Technology Co.，Ltd.")
                    {
                        if (dutyname == "总经理" || dutyname == "系统管理员" || dutyname == "董事长")
                        {
                            if (dn == "销售龙华部")
                            {
                                //张总的章
                                path = Server.MapPath("Images/张伟/keji.png");
                            }
                            else
                            {
                                path = Server.MapPath("Images/keji.png");
                            }
                        }
                        else
                        {
                            if (dn == "销售大客户部")
                            {
                                switch (shenpiren)
                                {
                                    case "黄银":
                                        path = Server.MapPath("Images/黄银/keji.png");
                                        break;
                                    case "李小蓉":
                                        path = Server.MapPath("Images/李小蓉/keji.png");
                                        break;
                                    case "方瑞":
                                        path = Server.MapPath("Images/方瑞/keji.png");
                                        break;
                                    default:
                                        //方芳的章
                                        path = Server.MapPath("Images/方芳/keji.png");
                                        break;
                                }
                            }
                            else if (dn == "销售1部")
                            {
                                //周总的章
                                path = Server.MapPath("Images/周琴/keji.png");
                            }
                            else if (dn == "销售2部")
                            {
                                //李爽
                                path = Server.MapPath("Images/李爽/keji.png");
                            }
                            else if (dn == "销售化学部")
                            {
                                //龙景向
                                path = Server.MapPath("Images/龙景向/keji.png");
                            }
                            else if (dn == "销售龙华部")
                            {
                                //龚经理
                                path = Server.MapPath("Images/龚佳梅/keji.png");
                            }
                            else
                            {
                                //有些没章的暂用陈总的章
                                path = Server.MapPath("Images/keji.png");
                            }
                        }
                    }
                    else if (Name == "深圳市倍测检测有限公司")
                    {
                        if (dutyname == "总经理" || dutyname == "系统管理员" || dutyname == "董事长")
                        {
                            if (dn == "销售龙华部")
                            {
                                //张总的章
                                path = Server.MapPath("Images/张伟/jiance.png");
                            }
                            else
                            {
                                path = Server.MapPath("Images/jiance.png");
                            }
                        }
                        else
                        {
                            if (dn == "销售大客户部")
                            {
                                switch (shenpiren)
                                {
                                    case "黄银":
                                        path = Server.MapPath("Images/黄银/jiance.png");
                                        break;
                                    case "李小蓉":
                                        path = Server.MapPath("Images/李小蓉/jiance.png");
                                        break;
                                    case "方瑞":
                                        path = Server.MapPath("Images/方瑞/jiance.png");
                                        break;
                                    default:
                                        //方芳的章
                                        path = Server.MapPath("Images/方芳/jiance.png");
                                        break;
                                }
                            }
                            else if (dn == "销售1部")
                            {
                                //周总的章
                                path = Server.MapPath("Images/周琴/jiance.png");
                            }
                            else if (dn == "销售2部")
                            {
                                //李爽
                                path = Server.MapPath("Images/李爽/jiance.png");
                            }
                            else if (dn == "销售化学部")
                            {
                                //龙景向
                                path = Server.MapPath("Images/龙景向/jiance.png");
                            }
                            else if (dn == "销售龙华部")
                            {
                                //龚经理
                                path = Server.MapPath("Images/龚佳梅/jiance.png");
                            }
                            else
                            {
                                //有些没章的暂用陈总的章
                                path = Server.MapPath("Images/jiance.png");
                            }
                        }
                    }
                    else if (Name == "深圳市倍测科技有限公司东莞分公司")
                    {
                        path = Server.MapPath("Images/dg.png");
                    }
                    //else
                    //{
                    //    //标源
                    //    path = Server.MapPath("Images/biaoyuan.png");
                    //}
                    wiw.WriteIntoPicture("signpicture", path);
                }
            }
            catch (Exception ex)
            {
                //this.name.Text = ex.Message;
            }
            finally
            {
                wiw.Save_CloseDocument(SaveDocPath);
            }

            //这里是想在生产PDF后删除中间dot和doc文件
            if (File.Exists(Server.MapPath("OuotationPrinted/" + newfilename + ".doc")))
            {
                File.Delete(Server.MapPath("OuotationPrinted/" + newfilename + ".dot"));
                WordToPDF(Server.MapPath("OuotationPrinted/" + newfilename + ".doc"), Server.MapPath("OuotationPrinted/" + newfilename + ".pdf"));
            }
            if (File.Exists(Server.MapPath("OuotationPrinted/" + newfilename + ".pdf")))
            {
                File.Delete(Server.MapPath("OuotationPrinted/" + newfilename + ".doc"));
                //把文档添加到数据库中
                string addsql = "insert into QuotaionSign values('" + this.quotationid + "','" + newfilename + ".pdf','" + Server.MapPath("OuotationPrinted/" + newfilename + ".pdf") + "','" + Session["UserName"].ToString() + "','" + DateTime.Now.ToString() + "')";
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con1.Open();
                SqlCommand cmd = new SqlCommand(addsql, con1);
                cmd.ExecuteNonQuery();

                con1.Close();
            }
        }
        BindFile();
    }
}