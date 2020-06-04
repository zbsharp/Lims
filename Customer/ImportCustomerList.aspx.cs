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
using System.Text;
using System.IO;
using Common;
using System.Data.OleDb;

public partial class CCSZJiaoZhun_Customer_ImportCustomerList : System.Web.UI.Page
{

    private void Page_Load(object sender, System.EventArgs e)
    {
        limit("客户列表");
    }
    protected void limit(string pagename1)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
        }
        else
        {
            con.Close();
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
    }

    protected bool limit1(string pagename1)
    {
        bool A = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {


            con.Close();
            A = true;
        }
        else
        {
            con.Close();
            A = false;
        }
        return A;
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

            AddDatasetToSQL(ds, 22);
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
            string  rrr = rd.Next(100).ToString();
            string pici = DateTime.Now.ToString("yyyyMMddhh") + rd.Next(100).ToString();

            for (int i = 0; i < pds.Tables[0].Rows.Count; i++)
            {
                if (pds.Tables[0].Rows[i][0].ToString().Trim() != "")
                {

                    string kehuid = "";

                
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                    con1.Open();

                    string sql1 = "select kehuid from Customer order by id desc";
                    SqlDataAdapter adpter = new SqlDataAdapter(sql1, con1);
                    DataSet ds = new DataSet();
                    adpter.Fill(ds);
                    con1.Close();
                    string haoma = ds.Tables[0].Rows[0]["kehuid"].ToString();
                    string houzhui = haoma.Substring(0, 7);
                    int a = Convert.ToInt32(houzhui);
                    int b = a + 1;
                    kehuid = b.ToString();
                    
                    
                    
                    string[] sqlargs = new string[]
                    {
                        kehuid,//<--先随意创建一个客户ID
                        pds.Tables[0].Rows[i][0].ToString(),
                        pds.Tables[0].Rows[i][1].ToString(),
                        pds.Tables[0].Rows[i][2].ToString(),
                        pds.Tables[0].Rows[i][3].ToString(),
                        pds.Tables[0].Rows[i][4].ToString(),
                        pds.Tables[0].Rows[i][5].ToString(),

                        DateTime.Now.ToString(),//插入记录的时间
                        pds.Tables[0].Rows[i][7].ToString(),
                        pds.Tables[0].Rows[i][8].ToString(),
                        pds.Tables[0].Rows[i][9].ToString(),
                        pds.Tables[0].Rows[i][10].ToString(),

                        pds.Tables[0].Rows[i][11].ToString(),
                        pds.Tables[0].Rows[i][12].ToString(),
                        pds.Tables[0].Rows[i][13].ToString(),
                        pds.Tables[0].Rows[i][14].ToString(),

                        pici,//批次号

                        pds.Tables[0].Rows[i][16].ToString(),
                        pds.Tables[0].Rows[i][17].ToString(),
                        pds.Tables[0].Rows[i][18].ToString(),
                        pds.Tables[0].Rows[i][19].ToString(),
                        pds.Tables[0].Rows[i][20].ToString(),
                        pds.Tables[0].Rows[i][21].ToString(),
                        pds.Tables[0].Rows[i][22].ToString()
                    };

                    Add(sqlargs);
                }
            }
            this.TxtPici.Text = pici;
        }
        else
        {
            throw new Exception("导入数据为空！");
        }
        return true;
    }

    protected void BindCustomerList()
    {
        string sql = "select top 50 * from customer order by filltime desc";

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        SqlDataAdapter ad = new SqlDataAdapter(sql, con1);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con1.Close();
        //DataView dv = ds.Tables[0].DefaultView;
        //PagedDataSource pds = new PagedDataSource();
        //AspNetPager2.RecordCount = dv.Count;
        //pds.DataSource = dv;
        //pds.AllowPaging = true;
        //pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        //pds.PageSize = AspNetPager2.PageSize;
        //GridView1.DataSource = pds;
        //GridView1.DataBind();


        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        
    }

    /// <summary>
    /// 插入数据到数据库
    /// </summary>
    public void Add(string[] sqlargs)
    {

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        

        /////////////////////检测该用户是否被添加过
        string sql="select id from customer where CustomName like '" +sqlargs[1]+ "'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con1);
        DataSet ds=new DataSet();
        ad.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            con1.Close();
            return;
        }//已经添加过相同的客户
        ///////////////

        string sqlvalues = "";
        for (int i = 0; i < sqlargs.Length; i++)
        {
            if (i != sqlargs.Length - 1)
                sqlvalues += "'" + sqlargs[i].Trim().Replace("'", "") + "',";//该表各字段的数据类型都需要引号
            else
                sqlvalues += "'" + sqlargs[i].Trim().Replace("'", "") + "'";
        }
        //insert into customer table
        string sqlinsert = "insert into customer values(" + sqlvalues + ")";

        SqlCommand cmd = new SqlCommand(sqlinsert, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
    }


    protected void BtnImport_Click(object sender, EventArgs e)
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
            BindCustomerList();
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
    protected void BtnCancelImport_Click(object sender, EventArgs e)
    {
        LblMessage.Visible = false;
        string sql = "delete from customer where bianhao= '" + this.TxtPici.Text.Trim().Replace("'", "") + "'";

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        BindCustomerList();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string sql = "delete from customer where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();

        BindCustomerList();



    }
}




