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
using Common;
using DBBLL;
using DBTable;
public partial class SysManage_ProductPrice : System.Web.UI.Page
{
    private void Page_Load(object sender, System.EventArgs e)
    {
        GridView1.Attributes.Add("style", "table-layout:fixed");
        //Session["username"] = "ccic";
        if (!IsPostBack)
        {
            ProductPrice();
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



    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int startIndex;
        startIndex = GridView1.PageIndex * GridView1.PageSize;
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        ProductPrice();

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

        //SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
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
            for (int i = 0; i < pds.Tables[0].Rows.Count; i++)
            {

                Add(pds.Tables[0].Rows[i][0].ToString(),
                    pds.Tables[0].Rows[i][1].ToString(),
                    pds.Tables[0].Rows[i][2].ToString(),
                    pds.Tables[0].Rows[i][3].ToString(),
                    pds.Tables[0].Rows[i][4].ToString(),
                    pds.Tables[0].Rows[i][5].ToString(),
                    pds.Tables[0].Rows[i][6].ToString(),
                    pds.Tables[0].Rows[i][9].ToString(),
                    pds.Tables[0].Rows[i][8].ToString(),
                    pds.Tables[0].Rows[i][7].ToString(),
                    pds.Tables[0].Rows[i][10].ToString(),
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

    protected void ProductPrice()
    {
        string sql = "select * from ProductPrice ";

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





        // GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con1.Close();
    }

    /// <summary>
    /// 插入数据到数据库
    /// </summary>
    //public void Add(string a, string b, string c, string d, string e, string f, string g, string h, string m, string n, string x, string y,string yy,string yyy,string z,string zz)
    //{
    public void Add(string a, string b, string c, string d, string e, string f, string g, string h, string i, string j, string k, string bbb)
    {

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();


        // string hh = "insert ProductPrice values('" + h + "','" + b + "','" + c + "','','" + d + "','" + Convert.ToDecimal(e) + "','" + Convert.ToDecimal(f) + "','" + Convert.ToDecimal(yyy) + "','" + g + "','" + n + "','" + x + "','" + y + "','" + DateTime.Now + "','" + DateTime.Now.Month.ToString() + "','" + Session["UserName"].ToString() + "','" + yy + "','" + z + "','" + zz + "')";

        string hh = "insert into ProductPrice values('" + a + "','" + b + "','" + c + "','','" + d + "','" + Convert.ToDecimal(e) + "','" + Convert.ToDecimal(f) + "','" + Convert.ToDecimal(g) + "','" + h + "','','','" + bbb + "','" + DateTime.Now + "','" + DateTime.Now.Month.ToString() + "','" + Session["UserName"].ToString() + "','" + i + "','" + j + "','" + k + "')";

        SqlCommand cmd = new SqlCommand(hh, con1);
        cmd.ExecuteNonQuery();

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
            ProductPrice();
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

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int sid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string sql1 = "delete from ProductPrice where id=" + sid + "";
        SqlCommand com1 = new SqlCommand(sql1, con1);
        com1.ExecuteNonQuery();


        con1.Close();
        ProductPrice();
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        AspNetPager2.Visible = false;
        string search = DropDownList1.SelectedValue;



        string sql = "";

        if (search == "科室")
        {
            sql = "select * from ProductPrice  where department='" + TextBox2.Text + "' or bigtype like '%" + TextBox2.Text + "%' or MIDtype like '%" + TextBox2.Text + "%'";
        }
        else if (search == "大类")
        {
            sql = "select * from ProductPrice  where bigtype like '%" + TextBox2.Text + "%'";
        }
        else if (search == "中类")
        {
            sql = "select * from ProductPrice  where MIDtype like '%" + TextBox2.Text + "%'";
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
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        int startIndex;





        startIndex = GridView1.PageIndex * GridView1.PageSize;
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        // ProductPrice();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductPriceAdd.aspx");
    }
    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "delete from productprice where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();
        ProductPrice();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView1.EditIndex = e.NewEditIndex;
        ProductPrice();

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        ProductPrice();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        double cost = 0;
        double price = 0;
        double discount = 0;
        string checkmodel = "";

        string KeyId = GridView1.DataKeys[e.RowIndex].Value.ToString();


        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        double uuname3 = Convert.ToDouble(Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text));

        double uuname4 = Convert.ToDouble(Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text));
        double uuname5 = Convert.ToDouble(Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text));

        checkmodel = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text).ToString();


        SqlCommand cm = new SqlCommand("update productprice set cost='" + uuname3 + "',price='" + uuname4 + "',discount='" + uuname5 + "' ,checkmodel='" + checkmodel + "' where id=" + KeyId, con1); //列的编号是从1,这里更新的是第二列和第三列所以cell[2],cell[3],而行的索引是从0开始的
        cm.ExecuteNonQuery();




        GridView1.EditIndex = -1;
        con1.Close();

        ProductPrice();


    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        ProductPrice();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "detail")
        {
            Response.Redirect("ProductPriceDetail.aspx?id=" + sid);
        }
    }
}