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

public partial class Quotation_QuoKaiAn : System.Web.UI.Page
{

    protected string shwhere = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        shwhere = "(tijiaobiaozhi !='是' or tijiaobiaozhi is null) and baojiaid in (select baojiaid from baojiabiao where huiqianbiaozhi='是')";
        if (!IsPostBack)
        {
            Bind();
        }
    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *,(select name from BaoJiaChanPing where baojiaid=baojiacpxiangmu.baojiaid) as name,(select top 1 customname from customer where kehuid =baojiacpxiangmu.kehuid) as kehuname  from baojiacpxiangmu where " + shwhere + " and  " + searchwhere.search(Session["UserName"].ToString()) + "  order by id desc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager1.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager1.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();
        con.Close();
        con.Dispose();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseID = DropDownList1.SelectedValue.Trim();
        string value = TextBox1.Text.Trim();
        string sql = "";

        if (DropDownList1.SelectedValue != "kehuname")
        {
            sql = "select *,(select name from BaoJiaChanPing where baojiaid=baojiacpxiangmu.baojiaid) as name," + searchwhere.searchcustomer("BaoJiaCPXiangMu", "") + " from baojiacpxiangmu where " + searchwhere.search(Session["UserName"].ToString(), ChooseID, value) + " and " + shwhere + " order by id desc";
        }
        else
        {
            sql = "select *,(select name from BaoJiaChanPing where baojiaid=baojiacpxiangmu.baojiaid) as name," + searchwhere.searchcustomer("BaoJiaCPXiangMu", "") + " from baojiacpxiangmu where " + searchwhere.search(Session["UserName"].ToString()) + " and " + searchwhere.searchcustomer(TextBox1.Text.Trim()) + " and  " + shwhere + "  order by id desc";

        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
        con.Dispose();
        AspNetPager1.Visible = false;
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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {

            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
                if (hzf.Checked)
                {
                    int sid = Convert.ToInt32(GridView1.DataKeys[gr.RowIndex].Value.ToString());
                    string sqlx = "update BaoJiaCPXiangMu set tijiaobiaozhi='是',tijiaoname='" + Session["UserName"].ToString() + "',tijiaotime='" + DateTime.Now + "' where id='" + sid + "'";
                    SqlCommand cmdx = new SqlCommand(sqlx, con);
                    cmdx.ExecuteNonQuery();
                }
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('OK！');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
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
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
}