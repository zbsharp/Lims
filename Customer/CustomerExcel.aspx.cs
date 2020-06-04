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
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using Common;
public partial class Customer_CustomerExcel : System.Web.UI.Page
{

    protected string departmentid = "";
    protected string departmentname = "";


    private void Page_Load(object sender, System.EventArgs e)
    {

       

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

        SqlConnection con1 =new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();


        int ic, ir;
        ic = pds.Tables[0].Columns.Count;
        if (pds.Tables[0].Columns.Count < Cols)
        {
            throw new Exception("导入Excel格式错误！Excel只有" + ic.ToString() + "列");
        }
        ir = pds.Tables[0].Rows.Count;
        if (pds != null && pds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < pds.Tables[0].Rows.Count; i++)
            {

                string kehuid = "";
                int h4 = 1;
                string h5 = h4.ToString("10000");



                string sql1 = "select kehuid from Customer order by id";
                SqlDataAdapter adpter = new SqlDataAdapter(sql1, con1);
                DataSet ds = new DataSet();
                adpter.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    kehuid = h5 + "00";
                }
                else
                {
                    string haoma = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["kehuid"].ToString();
                    string houzhui = haoma.Substring(0, 5);
                    int a = Convert.ToInt32(houzhui);
                    int b = a + 1;

                    kehuid = b + "00";
                }


                Add(kehuid, 
                    pds.Tables[0].Rows[i][0].ToString(),
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

                    pds.Tables[0].Rows[i][11].ToString()
                  );

            }
        }
        else
        {
            throw new Exception("导入数据为空！");
        }
        con1.Close();
        return true;
    }

    protected void fapiao()
    {
        string sql = "select * from Customer where fillname='" + Session["UserName"].ToString() + "' and b=''";

        SqlConnection con1 =new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        SqlDataAdapter ad = new SqlDataAdapter(sql, con1);
        DataSet ds = new DataSet();
        ad.Fill(ds);
     
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();



   





    }

    /// <summary>
    /// 插入数据到数据库
    /// </summary>
    public void Add(string kehuid, string a, string b, string c, string d, string f, string e, string g, string h, string i, string j, string k, string w)
    {


        int num1 = 0;
        int num2 = 0;
        string kehuid1 = "";

        SqlConnection con1 =new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();




   
        int h4 = 1;
        string h5 = h4.ToString("10000");
        string sql1 = "select kehuid from Customer order by id ";
        SqlDataAdapter adpter = new SqlDataAdapter(sql1, con1);
        DataSet ds = new DataSet();
        adpter.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            kehuid1 = h5 + "00";
        }
        else
        {

            string haoma = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["kehuid"].ToString();
            string qianzhui = haoma.Substring(0, 5);
            string houzhui = haoma.Substring(5, 2);
            int a1 = Convert.ToInt32(houzhui);
            int b1 = a1 + 1;
            kehuid1 = qianzhui + b1.ToString("00");

        }





        if (num1 != 1)
        {

            string hh = "insert Customer values('" + kehuid + "','" + a + "','" + b + "','"+c+"'," +
                "'" + d + "'," +
                "'" + Session["UserName"].ToString() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','否','" + DateTime.Now + "','" + DateTime.Now + "','" + DateTime.Now + "','"+f+"','"+e+"','" + g + "','" + h + "','" + i + "','"+k+"','','','','','')";

            SqlCommand cmd = new SqlCommand(hh, con1);
            cmd.ExecuteNonQuery();

            //string SqlStr = "insert into LinkManInformation(kehuid,LinkManName,Department,ZhiWu,ZhiCheng,OfficeTel,HandTel,SmallTel,Email,fax,Msn,DateTimeStr,ManLike,HomeInformation,ItemJiaoSe,BackInformation) values(";
            //SqlStr = SqlStr + "'" + kehuid + "','" + name + "','','','','" + diahua + "','" + shouji + "','','" + youxiang + "','','','','','','','')";

            //SqlCommand cmdlian = new SqlCommand(SqlStr, con1);
            //cmdlian.ExecuteNonQuery();
        }
        //else
        //{
        //    string hh = "insert Customerchongfu values('" + kehuid1 + "','" + a + "','" + b + "','自行开发'," +
        //                "'中国','','','" + d + "'," +
        //                "'" + e + "','','" + k + "'," +
        //                "'" + j + "','" + Session["UserName"].ToString() + "','" + Session["UserName"].ToString() + "','新增客户','" + i + "','" + DateTime.Now + "','" + Session["jiaose"].ToString() + "','','','','" + departmentid + "','" + departmentname + "','" + DateTime.Now + "','','','" + f + "','" + w + "','正常','刚录入','','','','','','','','','','','','','','','','','','')";

        //    SqlCommand cmd = new SqlCommand(hh, con1);
        //    cmd.ExecuteNonQuery();


        //}



        con1.Close();



    }


    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        fapiao();
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

     

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int sid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        SqlConnection con1 =new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string sql1 = "delete from Customer where id=" + sid + "";
        SqlCommand com1 = new SqlCommand(sql1, con1);
        com1.ExecuteNonQuery();


        con1.Close();
        fapiao();
    }

  
}