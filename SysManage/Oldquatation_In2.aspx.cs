using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;

public partial class SysManage_Oldquatation_In2 : System.Web.UI.Page
{
    protected string type1 = "";
    private void Page_Load(object sender, System.EventArgs e)
    {
        //Session["username"] = "ccic";
        if (!IsPostBack)
        {

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

            //if (type1 == "xls")
            {
                oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
                oleDBConnString += "Data Source=";
                oleDBConnString += fileName;
                oleDBConnString += ";Extended Properties=Excel 8.0;";
            }
            //else
            //{
            //     oleDBConnString = "Provider=Microsoft.ACE.OleDb.12.0; Data Source =" + fileName + ";Extended Properties=Excel 12.0;HDR=yes;IMEX=1";
            //}
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
                type1 = fileExtend;
                //if (fileExtend.ToLower() != "xls" || fileExtend.ToLower() != "xlsx")
                //{
                //    throw new Exception("你选择的文件格式不正确，只能导入EXCEL文件！");
                //}
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

        //SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        //con1.Open();


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
                       pds.Tables[0].Rows[i][10].ToString(), pici,
                       pds.Tables[0].Rows[i][11].ToString()
                    );
            }
        }
        else
        {
            throw new Exception("导入数据为空！");
        }
        return true;
    }


    /// <summary>
    /// 插入数据到数据库
    /// </summary>
    public void Add(string a, string b, string c, string d, string e, string f, string g, string h, string i, string j, string k, string qq, string danwei)
    {

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        if (h == "")
        {
            h = "0";
        }

        string hh = "insert into Product2 values('" + a.Trim() + "','" + b.Trim() + "','" + c.Trim() + "','" + (d.Trim()) + "','" + e.Trim() + "','" + f.Trim() + "','" + g.Trim() + "','" + Convert.ToDecimal(h.Trim()) + "','" + i.Trim() + "','" + j.Trim() + "','" + k.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + qq + "','" + danwei + "')";

        SqlCommand cmd = new SqlCommand(hh, con1);
        cmd.ExecuteNonQuery();

        con1.Close();


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
            LblMessage.Text = "数据导入成功！";
        }
        catch (Exception ex)
        {
            LblMessage.Text = ex.Message;
        }
    }
}