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

public partial class ZiYuan_daoexcel : System.Web.UI.Page
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
                Add(
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
                    pds.Tables[0].Rows[i][10].ToString());
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
        string sql = "select * from goodsinfo order by id desc";

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        SqlDataAdapter ad = new SqlDataAdapter(sql, con1);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager2.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();


        //GridView1.DataSource = ds.Tables[0];
        //GridView1.DataBind();
        con1.Close();
    }

    /// <summary>
    /// 插入数据到数据库
    /// </summary>
    public void Add(string a, string b, string c, string d, string f, string e, string p,string m,string n,string o,string q)
    {


        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string sql2 = "select * from goodsinfo where bianhao='" + c.Trim() + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con1);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            dr2.Close();
        }
        else
        {
            dr2.Close();

            string hh = "insert into GoodsInfo values('" + c.Trim() + "','中检','" + d.Trim() + "','" + f.Trim() + "','','0','','0','','" + a.Trim() + "','','" + b.Trim() + "','','','','','" + Convert.ToDecimal(e.Trim()) + "','','','','','','','','','','','','','','','','','','','','','','','0','0','是','','','','" + Session["username"].ToString() + "','" + DateTime.Now + "','','" + p.Trim() + "','" + m.Trim() + "','" + n.Trim() + "','" + o.Trim() + "','')";

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


        //for (int i = 0; i < GridView1.Rows.Count; i++)
        //{
        //    for (int j = 0; j < GridView1.Columns.Count; j++)
        //    {







        //        GridView1.Rows[i].Cells[1].Attributes.Add("onclick", "javascript:window.showModalDialog('daorubeizhu.aspx?liushuihao=" + Server.UrlEncode(GridView1.Rows[i].Cells[1].Text) + "&&kehuname=" + GridView1.Rows[i].Cells[1].Text + "&&zongfeiyong=" + Server.UrlEncode(GridView1.Rows[i].Cells[2].Text) + "&&rand=" + DateTime.Now + "','window','dialogWidth=800px;DialogHeight=500px;status:no;help:no;resizable:yes;')");
        //        GridView1.Rows[i].Cells[2].Attributes.Add("onclick", "javascript:window.showModalDialog('daorubeizhu.aspx?liushuihao=" + Server.UrlEncode(GridView1.Rows[i].Cells[1].Text) + "&&kehuname=" + GridView1.Rows[i].Cells[1].Text + "&&zongfeiyong=" + Server.UrlEncode(GridView1.Rows[i].Cells[2].Text) + "&&rand=" + DateTime.Now + "','window','dialogWidth=800px;DialogHeight=500px;status:no;help:no;resizable:yes;')");



        //    }



        //}

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //LblMessage.Visible = false;
        //string a = TextBox1.Text.Trim();
        //string sql = "delete from goodsinfo where fapiaoleibie= '" + a + "' and daoruren='" + Session["UserName"].ToString() + "'";

        //SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con1.Open();

        //SqlCommand cmd = new SqlCommand(sql, con1);
        //cmd.ExecuteNonQuery();
        //con1.Close();
        //fapiao();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        fapiao();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


        //string liushuihao = "";
        //SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con1.Open();
        //string id = GridView1.DataKeys[e.RowIndex].Value.ToString();

        //string sql2 = "select bianhao from shuipiao where id='" + id + "'";
        //SqlCommand cmd2 = new SqlCommand(sql2, con1);
        //SqlDataReader dr2 = cmd2.ExecuteReader();
        //if (dr2.Read())
        //{
        //    liushuihao = dr2["liushuihao"].ToString();
        //}
        //dr2.Close();

        //string sql3 = "select * from cashin2 where daid='" + liushuihao + "'";
        //SqlCommand cmd3 = new SqlCommand(sql3, con1);
        //SqlDataReader dr3 = cmd3.ExecuteReader();
        //if (dr3.Read())
        //{
        //    dr3.Close();
        //}
        //else
        //{

        //    dr3.Close();
        //    string sql = "delete from shuipiao where id='" + id + "' and daoruren='" + Session["UserName"].ToString() + "' ";
        //    SqlCommand cmd = new SqlCommand(sql, con1);
        //    cmd.ExecuteNonQuery();
        //}
        //con1.Close();
        //fapiao();





    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();






        //foreach (GridViewRow gr in GridView1.Rows)
        //{
        //    CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
        //    if (hzf.Checked)
        //    {
        //        string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();






        //        string sql2 = "update shuipiao set beizhu2='否' where id='" + sid + "'";
        //        SqlCommand com2 = new SqlCommand(sql2, con);
        //        com2.ExecuteNonQuery();
        //    }
        //}

        //con.Close();

        Response.Write("<script>alert('确认成功')</script>");
    }
}