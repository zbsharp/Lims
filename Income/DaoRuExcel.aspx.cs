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
using System.Data.OleDb;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Web.Services;
using Common;
using System.IO;
using System.Text;
using System.Drawing;
using System.Data.OleDb;
using Common;
using System.IO;
using System.Text;
using System.Drawing;

public partial class Income_DaoRuExcel : System.Web.UI.Page
{
   private void Page_Load(object sender, System.EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Write("<script>alert('请先登录!');window.location.href='../Login.aspx'</script>");
        }

       

            if (!IsPostBack)
            {
                fapiao();
            }
      




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

        this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
        this.Load += new System.EventHandler(this.Page_Load);

    }
    #endregion


    //// <summary>
    /// 从Excel提取数据--》Dataset
    /// </summary>
    /// <param name="filename">Excel文件路径名</param>
    private void ImportXlsToData(string fileName)
    {
        try
        {
            if (fileName == string.Empty)
            {
                throw new ArgumentNullException("Excel文件上传失败！");
            }

            string oleDBConnString = String.Empty;
            oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
            oleDBConnString += "Data Source=";
            oleDBConnString += fileName;
            oleDBConnString += ";Extended Properties=Excel 8.0;";
            OleDbConnection oleDBConn = null;
            OleDbDataAdapter oleAdMaster = null;
            DataTable m_tableName = new DataTable();
            DataSet ds = new DataSet();

            oleDBConn = new OleDbConnection(oleDBConnString);
            oleDBConn.Open();
            m_tableName = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (m_tableName != null && m_tableName.Rows.Count > 0)
            {

                m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString();

            }
            string sqlMaster;
            sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
            oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
            oleAdMaster.Fill(ds, "m_tableName");
            oleAdMaster.Dispose();
            oleDBConn.Close();
            oleDBConn.Dispose();

            AddDatasetToSQL(ds, 6);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 上传Excel文件
    /// </summary>
    /// <param name="inputfile">上传的控件名</param>
    /// <returns></returns>
    private string UpLoadXls(System.Web.UI.HtmlControls.HtmlInputFile inputfile)
    {
        string orifilename = string.Empty;
        string uploadfilepath = string.Empty;
        string modifyfilename = string.Empty;
        string fileExtend = "";//文件扩展名
        int fileSize = 0;//文件大小
        try
        {
            if (inputfile.Value != string.Empty)
            {
                //得到文件的大小
                fileSize = inputfile.PostedFile.ContentLength;
                if (fileSize == 0)
                {
                    throw new Exception("导入的Excel文件大小为0，请检查是否正确！");
                }
                //得到扩展名
                fileExtend = inputfile.Value.Substring(inputfile.Value.LastIndexOf(".") + 1);
                if (fileExtend.ToLower() != "xls")
                {
                    throw new Exception("你选择的文件格式不正确，只能导入EXCEL文件！");
                }
                //路径
                uploadfilepath = Server.MapPath("~/Service/GraduateChannel/GraduateApply/ImgUpLoads");
                //新文件名
                modifyfilename = System.Guid.NewGuid().ToString();
                modifyfilename += "." + inputfile.Value.Substring(inputfile.Value.LastIndexOf(".") + 1);
                //判断是否有该目录
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(uploadfilepath);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                orifilename = uploadfilepath + "\\" + modifyfilename;
                //如果存在,删除文件
                if (File.Exists(orifilename))
                {
                    File.Delete(orifilename);
                }
                // 上传文件
                inputfile.PostedFile.SaveAs(orifilename);
            }
            else
            {
                throw new Exception("请选择要导入的Excel文件!");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return orifilename;
    }

    /// <summary>
    /// 将Dataset的数据导入数据库
    /// </summary>
    /// <param name="pds">数据集</param>
    /// <param name="Cols">数据集列数</param>
    /// <returns></returns>
    private bool AddDatasetToSQL(DataSet pds, int Cols)
    {
        int ic, ir;
        ic = pds.Tables[0].Columns.Count;
        if (pds.Tables[0].Columns.Count < Cols)
        {
            throw new Exception("导入Excel格式错误！Excel只有" + ic.ToString() + "列");
        }
        ir = pds.Tables[0].Rows.Count;
        if (pds != null && pds.Tables[0].Rows.Count > 0)
        {
            Random rd = new Random();
            string rd1 = rd.Next(100).ToString();
            string pici = DateTime.Now.ToString("yyyyMMddhh") + rd1;



            for (int i = 0; i < pds.Tables[0].Rows.Count; i++)
            {
                Add(pds.Tables[0].Rows[i][0].ToString(),
                    pds.Tables[0].Rows[i][1].ToString(),
              pds.Tables[0].Rows[i][2].ToString(),
              pds.Tables[0].Rows[i][3].ToString(),
            pds.Tables[0].Rows[i][4].ToString(),
            pds.Tables[0].Rows[i][5].ToString(),
            pds.Tables[0].Rows[i][6].ToString(),
            pds.Tables[0].Rows[i][7].ToString(),
            pds.Tables[0].Rows[i][8].ToString(),
            pds.Tables[0].Rows[i][9].ToString(),
            pds.Tables[0].Rows[i][10].ToString(),
            pds.Tables[0].Rows[i][11].ToString(), 
            pici);
            }
        }
        else
        {
            throw new Exception("导入数据为空！");
        }
        return true;
    }

    protected void fapiao()
    {
        string sql = "select * from pinzheng2 where biaozhi='否' order by id asc";

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        SqlDataAdapter ad = new SqlDataAdapter(sql, con1);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        //DataView dv = ds.Tables[0].DefaultView;
        //PagedDataSource pds = new PagedDataSource();
        //AspNetPager2.RecordCount = dv.Count;
        //pds.DataSource = dv;
        //pds.AllowPaging = true;
        //pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        //pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


        //GridView1.DataSource = ds.Tables[0];
        //GridView1.DataBind();
        con1.Close();
    }

    /// <summary>
    /// 插入数据到数据库
    /// </summary>
    public void Add(string a, string b, string c, string d, string f, string e, string p,string p1,string p2,string p3,string p4,string p5,string p6)
    {
       

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string sql2 = "select * from pinzheng2 where fapiaohao='"+b.Trim()+"'";
        SqlCommand cmd2 = new SqlCommand(sql2,con1);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read()) 
        {
            dr2.Close();
        }
        else
        {
            dr2.Close();

            string hh = "insert into pinzheng2 values('" + a.Trim() + "','" + b.Trim() + "','" + c.Trim() + "','" + d.Trim() + "','" + f.Trim() + "','" + e.Trim() + "','"+Convert.ToDecimal(p.Trim())+"','"+Convert.ToDecimal(p1.Trim())+"','"+Convert.ToDecimal(p2.Trim())+"','" + p5.Trim() + "','"+p3.Trim()+"','"+p4.Trim()+"','"+Session["UserName"].ToString()+"','" + DateTime.Now.Date + "','否','"+p6+"','')";

            SqlCommand cmd = new SqlCommand(hh, con1);
            cmd.ExecuteNonQuery();
        }

        con1.Close();
    }

    private void BtnImport_Click(object sender, System.EventArgs e)
    {

    }
    protected void BtnImport_Click1(object sender, EventArgs e)
    {
        string filename = string.Empty;
        try
        {
            filename = UpLoadXls(FileExcel);//上传XLS文件
            ImportXlsToData(filename);//将XLS文件的数据导入数据库                
            if (filename != string.Empty && System.IO.File.Exists(filename))
            {
                System.IO.File.Delete(filename);//删除上传的XLS文件
            }
            LblMessage.Text = "数据导入成功！您导入的信息如下";
            fapiao();
        }
        catch (Exception ex)
        {
            LblMessage.Text = ex.Message;
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }



    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        LblMessage.Visible = false;
        string a = TextBox1.Text.Trim();
        string sql = "delete from pinzheng2 where pici= '" + a + "' and name='"+Session["UserName"].ToString()+"' and biaozhi='否'";

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        fapiao();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        fapiao();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


        string liushuihao = "";
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();

      
            string sql = "delete from pinzheng2 where id='" + id + "' and name='" + Session["UserName"].ToString() + "' and biaozhi='否' ";
            SqlCommand cmd = new SqlCommand(sql, con1);
            cmd.ExecuteNonQuery();
        
        con1.Close();
        fapiao();





    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();






        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            if (hzf.Checked)
            {
                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();






                string sql2 = "update pinzheng2 set biaozhi='否' where id='" + sid + "'";
                SqlCommand com2 = new SqlCommand(sql2, con);
                com2.ExecuteNonQuery();
            }
        }

        con.Close();

        Response.Write("<script>alert('确认成功')</script>");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string sql = "";
        if (TextBox2.Text.Trim() == "")
        {

            if (DropDownList1.SelectedValue == "否")
            {
                sql = "select * from pinzheng2 where  biaozhi='" + DropDownList1.SelectedValue + "'order by id asc";
            }
            else
            {
                sql = "select top 200 * from pinzheng2 where  biaozhi='" + DropDownList1.SelectedValue + "'order by id desc";

            }

        }
        else
        {

            if (DropDownList1.SelectedValue == "否")
            {
                sql = "select * from pinzheng2 where ( fapiaohao like '%" + TextBox2.Text + "%' or fapiaodaima like '%" + TextBox2.Text + "%' or kehu like '%" + TextBox2.Text.Trim() + "%') and biaozhi='" + DropDownList1.SelectedValue + "'order by id asc";
            }
            else
            {
                sql = "select * from pinzheng2 where ( fapiaohao like '%" + TextBox2.Text + "%' or fapiaodaima like '%" + TextBox2.Text + "%' or kehu like '%" + TextBox2.Text.Trim() + "%') and biaozhi='" + DropDownList1.SelectedValue + "'order by id asc";
 
            }
        }
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        SqlDataAdapter ad = new SqlDataAdapter(sql, con1);
        DataSet ds = new DataSet();
        ad.Fill(ds);

    
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


        con1.Close();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {


      


        Random seed = new Random();
        Random randomNum = new Random(seed.Next());
        string shoufeiid = randomNum.Next().ToString() + DateTime.Now.ToString("yyyyMMdd_hhmmss");


        int shumu = 0;

        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf1 = (CheckBox)gr.Cells[16].FindControl("CheckBox1");
            if (hzf1.Checked)
            { shumu = shumu + 1; }
        }


        

        shumu = shumu + 1;


       
        int j = 0; 
        {

            int n = -1;
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox hzf = (CheckBox)gr.Cells[16].FindControl("CheckBox1");
                if (hzf.Checked)
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                    con.Open();
                    
                    j = j + 1;
                    string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                    string zhaiyao = "1";
                    string sql2 = "select * from pinzheng2 where id='" + sid + "' and biaozhi='是'";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.Read())
                    {
                        dr2.Close();
                        //zhaiyao = "收"+dr2["fukuanren"].ToString()+dr2["fukuanjine"].ToString()+dr2["fukuanriqi"].ToString();
                    }
                    else
                    {

                        dr2.Close();



                        string xiaojizong = "0";
                        string xiaojizong1 = "0";
                        string xiaojizong2 = "0";
                        string kehu = "";


                        string sqlsum1 = "select jine,shuie,kehu from pinzheng2 where id='" + sid + "'";
                        SqlCommand cmdsum1 = new SqlCommand(sqlsum1, con);
                        SqlDataReader drsum1 = cmdsum1.ExecuteReader();
                        if (drsum1.Read())
                        {
                            if (drsum1["jine"] == DBNull.Value)
                            {
                                xiaojizong1 = "0";
                            }
                            else
                            {
                                xiaojizong1 = Convert.ToDecimal(drsum1["jine"]).ToString().Trim();

                            }

                            if (drsum1["shuie"] == DBNull.Value)
                            {
                                xiaojizong2 = "0";
                            }
                            else
                            {
                                xiaojizong2 = Convert.ToDecimal(drsum1["shuie"]).ToString().Trim();

                            }

                            kehu = drsum1["kehu"].ToString();
                        }


                        xiaojizong = (Convert.ToDecimal(drsum1["shuie"]) + Convert.ToDecimal(drsum1["jine"])).ToString();

                        drsum1.Close();

                        string kehubianhao = "";

                        string sqlsum3 = "select bianhao from table1 where kehuname='" + kehu + "'";
                        SqlCommand cmdsum3 = new SqlCommand(sqlsum3, con);
                        SqlDataReader drsum3 = cmdsum3.ExecuteReader();
                        if (drsum3.Read())
                        {
                            kehubianhao = drsum3["bianhao"].ToString();
                        }
                        else
                        {
                            kehubianhao = "1000";
                            kehu = "借发票";
                        }
                        drsum3.Close();




                        string xiaoji = "";
                        string sqlsum = "select daozhang from pinzheng2 where id='" + sid + "'";
                        SqlCommand cmdsum = new SqlCommand(sqlsum, con);
                        SqlDataReader drsum = cmdsum.ExecuteReader();
                        if (drsum.Read())
                        {
                            xiaoji = drsum["daozhang"].ToString();
                        }
                        drsum.Close();

                        string hesuanxiangmu = "客户---" + kehubianhao + "---" + kehu;

                        string sql21 = "select * from pinzheng2 where id='" + sid + "'";
                        SqlCommand cmd21 = new SqlCommand(sql21, con);
                        SqlDataReader dr21 = cmd21.ExecuteReader();
                        if (dr21.Read())
                        {


                            if (xiaoji == "银行" || xiaoji == "POS")
                            {

                                zhaiyao = "收" + dr21["kehu"].ToString() + dr21["gerenfukuan"].ToString() + "测试费" + (dr21["daozhangriqi"]).ToString();

                            }
                            else if (xiaoji == "现金")
                            {

                                zhaiyao = "收" + dr21["kehu"].ToString() + dr21["gerenfukuan"].ToString() + "测试费"  + (dr21["daozhangriqi"]).ToString();

                            }
                            else if (xiaoji == "借票")
                            {



                                zhaiyao = "收" + dr21["kehu"].ToString() + dr21["gerenfukuan"].ToString() + "借发票"  + (dr21["daozhangriqi"]).ToString();


                            }
                            else if (xiaoji == "预收")
                            {

                                zhaiyao = "收" + dr21["kehu"].ToString() + dr21["gerenfukuan"].ToString() + "冲预收" + (dr21["daozhangriqi"]).ToString();


                            }
                            else
                            {

                                zhaiyao = "收" + dr21["kehu"].ToString() + dr21["gerenfukuan"].ToString() + "测试费" + (dr21["daozhangriqi"]).ToString();


                            }









                        }
                        dr21.Close();
                        string pin = "1";

                        if (j < 11)
                        {
                            pin = "1";
                        }
                        else if (j >= 11 && j < 21)
                        {
                            pin = "2";
                        }
                        else if (j >= 21 && j < 31)
                        {
                            pin = "3";
                        }
                        else if (j >= 31 && j < 41)
                        {
                            pin = "4";
                        }
                        else if (j >= 41 && j < 51)
                        {
                            pin = "5";
                        }
                        else if (j >= 51 && j < 61)
                        {
                            pin = "6";
                        }
                        else if (j >= 61 && j < 71)
                        {
                            pin = "7";
                        }
                        else if (j >= 71 && j < 81)
                        {
                            pin = "8";
                        }
                        else if (j >= 81 && j < 91)
                        {
                            pin = "9";
                        }

                       


                        string pin1 = "";


                        

                       

                        for (int i = 0; i < 3; i++)
                        {
                            string sql = "";
                            string sqlsum5 = "select top 1 pzhao from pinzheng order by id desc";
                            SqlCommand cmdsum5 = new SqlCommand(sqlsum5, con);
                            SqlDataReader drsum5 = cmdsum5.ExecuteReader();
                            if (drsum5.Read())
                            {
                                pin1 = drsum5["pzhao"].ToString();
                            }
                            else
                            {
                                pin1 = "1";

                            }
                            drsum5.Close();
                            if (pin1 == pin)
                            {
                                n = n + 1;
                            }
                            else
                            {
                                n = 0;
                            }

                            if (xiaoji == "银行" || xiaoji == "POS")
                            {

                                

                                if (i == 0)
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','1002.02','银行存款 - 农行西丽支行','RMB','人民币','" + xiaojizong + "','" + xiaojizong + "','0','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";
                                }
                                else if (i == 1)
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','6001.02','主营业务收入 - 测试部','RMB','人民币','" + xiaojizong1 + "','0','" + xiaojizong1 + "','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";

                                }
                                else
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','2221.07.06','应交税费 - 应交增值税 - 销项税额','RMB','人民币','" + xiaojizong2 + "','0','" + xiaojizong2 + "','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";

                                }
                            }
                            else if (xiaoji == "现金")
                            {


                                if (i == 0)
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','1001','库存现金','RMB','人民币','" + xiaojizong + "','" + xiaojizong + "','0','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";
                                }
                                else if (i == 1)
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','6001.02','主营业务收入 - 测试部','RMB','人民币','" + xiaojizong1 + "','0','" + xiaojizong1 + "','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";

                                }
                                else
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','2221.07.06','应交税费 - 应交增值税 - 销项税额','RMB','人民币','" + xiaojizong2 + "','0','" + xiaojizong2 + "','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";

                                }
                            }
                            else if (xiaoji == "借票")
                            {



                                if (i == 0)
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','1122.01','应收账款 - 应收账款-客户/1000 - 借发票','RMB','人民币','" + xiaojizong + "','" + xiaojizong + "','0','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','" + hesuanxiangmu + "','','','')";
                                }
                                else if (i == 1)
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','6001.02','主营业务收入 - 测试部','RMB','人民币','" + xiaojizong1 + "','0','" + xiaojizong1 + "','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";

                                }
                                else
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','2221.07.06','应交税费 - 应交增值税 - 销项税额','RMB','人民币','" + xiaojizong2 + "','0','" + xiaojizong2 + "','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";

                                }
                            }
                            else if (xiaoji == "预收")
                            {


                                if (i == 0)
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','2203.01','预收账款 - 未达账挂账','RMB','人民币','" + xiaojizong + "','" + xiaojizong + "','0','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";
                                }
                                else if (i == 1)
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','6001.02','主营业务收入 - 测试部','RMB','人民币','" + xiaojizong1 + "','0','" + xiaojizong1 + "','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";

                                }
                                else
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','2221.07.06','应交税费 - 应交增值税 - 销项税额','RMB','人民币','" + xiaojizong2 + "','0','" + xiaojizong2 + "','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";

                                }
                            }
                            else
                            {


                                if (i == 0)
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','1002.02','银行存款 - 农行西丽支行','RMB','人民币','" + xiaojizong + "','" + xiaojizong + "','0','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";
                                }
                                else if (i == 1)
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','6001.02','主营业务收入 - 测试部','RMB','人民币','" + xiaojizong1 + "','0','" + xiaojizong1 + "','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";

                                }
                                else
                                {
                                    sql = "insert into PinZheng values ('rwbianhao','" + sid + "','" + shoufeiid + "','','" + DateTime.Now + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','收','" + pin + "','2221.07.06','应交税费 - 应交增值税 - 销项税额','RMB','人民币','" + xiaojizong2 + "','0','" + xiaojizong2 + "','" + Session["UserName"].ToString() + "','none','none','none','','*','','" + zhaiyao + "','0','*','0','','" + DateTime.Now + "','','0','','','1','1','" + n + "','','','','')";

                                }
                            }


                            SqlCommand cmd = new SqlCommand(sql, con);
                            cmd.ExecuteNonQuery();
                        }
                    }


                    string sql3 = "update pinzheng2 set pici2='" + shoufeiid + "' where id='" + sid + "'";
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    cmd3.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
       

        Response.Write("<script>window.open('DaoChuExcel.aspx?ran=" + shoufeiid + "')</script>");
    }



    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        //全选或全不选
        int i;
        if (((CheckBox)sender).Checked)
        {
            for (i = 0; i < GridView1.Rows.Count; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked = true;
            }
        }
        else
        {
            for (i = 0; i < GridView1.Rows.Count; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked = false;
            }
        }

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("kehu.aspx");
    }
}
