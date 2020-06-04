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

public partial class Income_IncomeCheck1 : System.Web.UI.Page
{

    private int _i = 0;
    private void Page_Load(object sender, System.EventArgs e)
    {


        // GridView1.Attributes.Add("style", "table-layout:fixed");
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


        this.Load += new System.EventHandler(this.Page_Load);

    }
    #endregion


    //// <summary>
    /// 从Excel提取数据--》Dataset
    /// </summary>
    /// <param name="filename">Excel文件路径名</param>


    /// <summary>
    /// 上传Excel文件
    /// </summary>
    /// <param name="inputfile">上传的控件名</param>
    /// <returns></returns>


    /// <summary>
    /// 将Dataset的数据导入数据库
    /// </summary>
    /// <param name="pds">数据集</param>
    /// <param name="Cols">数据集列数</param>
    /// <returns></returns>


    protected void fapiao()
    {
        string sql = "select * from shuipiao where queren='' or queren='否' order by id desc";

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        SqlDataAdapter ad = new SqlDataAdapter(sql, con1);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con1.Close();
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

    }

    /// <summary>
    /// 插入数据到数据库
    /// </summary>



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");

            // e.Row.Cells[2].Text = SubStr(e.Row.Cells[2].Text, 6);
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
        }




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

        string sql2 = "select liushuihao from shuipiao where id='" + id + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con1);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            liushuihao = dr2["liushuihao"].ToString();
        }
        dr2.Close();

        string sql3 = "select * from cashin2 where daid='" + liushuihao + "'";
        SqlCommand cmd3 = new SqlCommand(sql3, con1);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {
            dr3.Close();
        }
        else
        {

            dr3.Close();
            string sql = "delete from shuipiao where id='" + id + "' and shoufeiid ='' and daoruren='" + Session["UserName"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, con1);
            cmd.ExecuteNonQuery();
        }
        con1.Close();
        fapiao();





    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select  * from shuipiao where (fukuanren like '%" + TextBox1.Text.Trim() + "%' or fukuanjine like '%" + TextBox1.Text.Trim() + "%' or fukuanfangshi like '%" + TextBox1.Text.Trim() + "%' or liushuihao like '%" + TextBox1.Text.Trim() + "%') and queren='" + DropDownList1.SelectedValue + "' order by id desc";


        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);


        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
        AspNetPager2.Visible = false;
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fapiao();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string KeyId = GridView1.DataKeys[e.RowIndex].Value.ToString();

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string uuname2 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
        string uuname3 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());

        string uuname5 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
        string uuname6 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text.ToString());
        string uuname8 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[8].Controls[0]).Text.ToString());


        string sql = "update shuipiao set fukuanren='" + uuname2 + "',fukuanriqi='" + uuname3 + "',fukuanjine='" + uuname5 + "',fukuanfangshi='" + uuname6 + "',beizhu='" + uuname8 + "' where id='" + KeyId + "' and shoufeiid ='' and daoruren='" + Session["UserName"].ToString() + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        GridView1.EditIndex = -1;
        fapiao();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fapiao();
    }

    public string SubStr(string sString, int nLeng)
    {
        if (sString.Length <= nLeng)
        {
            return sString;
        }
        string sNewStr = sString.Substring(0, nLeng);

        return sNewStr;
    }
}