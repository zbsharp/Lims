using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Word;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

public partial class EMCmate_PDF : System.Web.UI.Page
{
    public string chargeid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        chargeid = Request["chargeid"].ToString();
        Bind(chargeid);
    }

    private void Bind(string chargeid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from ChargePDF where chargeid='" + chargeid + "' order by id desc";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Button1.Enabled = false;
        Toprint();
        Button1.Enabled = true;
    }

    private void Toprint()
    {
        Random ran = new Random(Guid.NewGuid().GetHashCode());
        int number = ran.Next(500);
        string numberstr = number.ToString("000");
        string urlbase = chargeid + DateTime.Now.ToString("yyyy-MM-dd") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00");
        string newfilename = urlbase + number;
        string baseFilePath = "";
        string copybasefilePath = "";
        baseFilePath = Server.MapPath("Template.dot");
        if (File.Exists(baseFilePath))
        {
            copybasefilePath = Server.MapPath("PDFfile/" + newfilename + ".dot");
            File.Copy(baseFilePath, copybasefilePath, true);
        }
        else
        {
            return;
        }

        if (File.Exists(copybasefilePath))
        {
            WriteIntoWord wiw = new WriteIntoWord();
            string FilePath = copybasefilePath;
            string SavaDoFile = Server.MapPath("PDFfile/" + newfilename + ".doc");
            wiw.OpenDocument(FilePath);
            try
            {
                //客户信息
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
                {
                    con.Open();

                    string kehuid = string.Empty;
                    string linkmanid = string.Empty;
                    string bankaccountid = string.Empty;
                    decimal money = 0m;
                    string currency = string.Empty;
                    string fillname = string.Empty;
                    string remark = string.Empty;

                    string sqlcharge = "select * from Charge where chargeid='" + chargeid + "'";
                    SqlCommand cmdcharge = new SqlCommand(sqlcharge, con);
                    SqlDataReader drcharge = cmdcharge.ExecuteReader();
                    if (drcharge.Read())
                    {
                        kehuid = drcharge["kehuid"].ToString();
                        linkmanid = drcharge["linkmanid"].ToString();
                        bankaccountid = drcharge["bankaccountid"].ToString();
                        money = Convert.ToDecimal(drcharge["money"]);
                        currency = drcharge["currency"].ToString();
                        fillname = drcharge["fillname"].ToString();
                        remark = drcharge["remark"].ToString();
                    }
                    drcharge.Close();

                    //客户信息
                    string customername = string.Empty;
                    string sqlcustomername = "select CustomName from Customer where Kehuid='" + kehuid + "'";
                    SqlCommand cmdcustomername = new SqlCommand(sqlcustomername, con);
                    SqlDataReader drcustomername = cmdcustomername.ExecuteReader();
                    if (drcustomername.Read())
                    {
                        customername = drcustomername["CustomName"].ToString();
                    }
                    drcustomername.Close();

                    //客户联系人
                    string linkman = string.Empty;
                    string linkmanphone = string.Empty;
                    string linkmanemail = string.Empty;
                    string linkmanmobile = string.Empty;
                    string sqllinkman = "select name,mobile,telephone,email from CustomerLinkMan where id='" + linkmanid + "'";
                    SqlCommand cmdlinkman = new SqlCommand(sqllinkman, con);
                    SqlDataReader drlinkman = cmdlinkman.ExecuteReader();
                    if (drlinkman.Read())
                    {
                        linkman = drlinkman["name"].ToString();
                        linkmanphone = drlinkman["telephone"].ToString();
                        linkmanemail = drlinkman["email"].ToString();
                        linkmanmobile = drlinkman["mobile"].ToString();
                    }
                    drlinkman.Close();

                    //业务员
                    string mobile = string.Empty;
                    string phone = string.Empty;
                    string email = string.Empty;
                    string sqluserinfo = "select youxiang,yidong,banggongdianhua from UserInfo where UserName='" + fillname + "'";
                    SqlCommand cmduserinfo = new SqlCommand(sqluserinfo, con);
                    SqlDataReader druserinfo = cmduserinfo.ExecuteReader();
                    if (druserinfo.Read())
                    {
                        mobile = druserinfo["yidong"].ToString();
                        phone = druserinfo["banggongdianhua"].ToString();
                        email = druserinfo["youxiang"].ToString();
                    }
                    druserinfo.Close();

                    //公司名称&银行信息
                    string getbanksql = "select * from Bankaccount where id='" + bankaccountid + "'";
                    SqlDataAdapter getbankad = new SqlDataAdapter(getbanksql, con);
                    DataSet getbankds = new DataSet();
                    getbankad.Fill(getbankds);
                    string Name = string.Empty;
                    string account = string.Empty;
                    string openaccount = string.Empty;
                    if (getbankds.Tables[0].Rows.Count > 0)
                    {
                        Name = getbankds.Tables[0].Rows[0]["Name"].ToString();
                        if (Name == "周朝政")
                        {
                            Name = "深圳市倍测科技有限公司";
                        }
                        account = getbankds.Tables[0].Rows[0]["account"].ToString();
                        openaccount = getbankds.Tables[0].Rows[0]["openaccout"].ToString();
                    }

                    //表格
                    string sqltb = "select CONVERT(varchar(11) ,starttime, 111 ) as [time],testsite,[hour],sumprice,remark from EMCmake where id in (select EMCid from ChargeEMCid where chargeid='" + chargeid + "')";
                    SqlDataAdapter datb = new SqlDataAdapter(sqltb, con);
                    DataSet ds = new DataSet();
                    datb.Fill(ds);
                    string totalchines = wiw.ConvertToChinese1(money.ToString());


                    wiw.WriteIntoDocument("Customername", customername);
                    wiw.WriteIntoDocument("linkman", linkman);
                    wiw.WriteIntoDocument("phone", linkmanmobile);
                    wiw.WriteIntoDocument("tel", linkmanmobile);
                    wiw.WriteIntoDocument("email", linkmanemail);
                    wiw.WriteIntoDocument("name", Name);
                    wiw.WriteIntoDocument("responser", fillname);
                    wiw.WriteIntoDocument("fax", mobile);
                    wiw.WriteIntoDocument("yidong", phone);
                    wiw.WriteIntoDocument("you", email);
                    wiw.WriteIntoDocument("chargeid", chargeid);
                    wiw.WriteIntoDocumentItemtable("tb", ds, money, currency, totalchines, remark);
                    wiw.WriteIntoDocument("huming", Name);
                    wiw.WriteIntoDocument("openaccount", account);
                    wiw.WriteIntoDocument("account", openaccount);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                wiw.Save_CloseDocument(SavaDoFile);
            }

            //这里是想在生产PDF后删除中间dot和doc文件
            if (File.Exists(Server.MapPath("PDFfile/" + newfilename + ".doc")))
            {
                File.Delete(Server.MapPath("PDFfile/" + newfilename + ".dot"));
                WordToPDF(Server.MapPath("PDFfile/" + newfilename + ".doc"), Server.MapPath("PDFfile/" + newfilename + ".pdf"));
            }
            if (File.Exists(Server.MapPath("PDFfile/" + newfilename + ".pdf")))
            {
                File.Delete(Server.MapPath("PDFfile/" + newfilename + ".doc"));
            }
            ChargePDFAdd(newfilename+".pdf", Server.MapPath("PDFfile/" + newfilename + ".pdf"));
            Bind(chargeid);
        }
    }

    private void ChargePDFAdd(string newfilename, string savaDoFile)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "insert ChargePDF values('" + chargeid + "','" + newfilename + "','" + savaDoFile + "','" + DateTime.Now + "','" + Session["Username"].ToString() + "')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
    }

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
        string command = e.CommandName.ToString();
        GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
        int id = Convert.ToInt32(this.GridView1.DataKeys[gvr.RowIndex].Value);
        if (command == "download")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string filename = string.Empty;
                string filepath = string.Empty;
                string sql = "select filename,filepath from ChargePDF where id=" + id + "";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    filename = dr["filename"].ToString();
                    filepath = dr["filepath"].ToString();
                }
                dr.Close();

                if (!string.IsNullOrEmpty(filepath))
                {
                    FileStream fs = new FileStream(filepath, FileMode.Open);
                    byte[] bits = new byte[(int)fs.Length];
                    fs.Read(bits, 0, bits.Length);
                    fs.Close();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
                    Response.BinaryWrite(bits);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
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

    //将项目表写入表格,需要设置格式  
    public void WriteIntoDocumentItemtable(string BookmarkName, DataSet dataset, decimal moeny, string currency, string totalchines, string remark)
    {
        if (currency == "人民币")
        {
            currency = "RMB  ";
        }
        else
        {
            currency = "USD  ";
        }
        object bookmarkName = BookmarkName;
        Bookmark bm = doc.Bookmarks.get_Item(ref bookmarkName);//返回书签 
        Range range = bm.Range;
        range.Tables.Add(range, dataset.Tables[0].Rows.Count + 3, 5);//创建表格
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

        tb.Columns[1].Width = 110f;
        tb.Columns[2].Width = 120f;
        tb.Columns[3].Width = 75f;
        tb.Columns[4].Width = 70f;
        tb.Columns[5].Width = 130f;

        tb.Cell(1, 1).Range.Text = "测试日期";
        tb.Cell(1, 2).Range.Text = "服务项目";
        tb.Cell(1, 3).Range.Text = "时间";
        tb.Cell(1, 4).Range.Text = "费用";
        tb.Cell(1, 5).Range.Text = "备注";

        for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (j == 3)
                {
                    tb.Cell(i + 2, j + 1).Range.Text = dataset.Tables[0].Rows[i][j].ToString().Substring(0, dataset.Tables[0].Rows[i][j].ToString().Length - 2);
                }
                else
                {
                    tb.Cell(i + 2, j + 1).Range.Text = dataset.Tables[0].Rows[i][j].ToString();
                }
            }
        }

        tb.Cell(dataset.Tables[0].Rows.Count + 2, 1).Range.Text = "合计";
        tb.Cell(dataset.Tables[0].Rows.Count + 2, 3).Range.Text = currency + moeny + "   " + totalchines;
        tb.Cell(dataset.Tables[0].Rows.Count + 2, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 2, 5));
        tb.Cell(dataset.Tables[0].Rows.Count + 3, 1).Range.Text = "备注";
        tb.Cell(dataset.Tables[0].Rows.Count + 3, 3).Range.Text = remark;
        tb.Cell(dataset.Tables[0].Rows.Count + 3, 2).Merge(tb.Cell(dataset.Tables[0].Rows.Count + 3, 5));
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