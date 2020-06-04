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

public partial class Income_CashinManage : System.Web.UI.Page
{

    private int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind3();
        }

    }


    public void Bind3()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *,(select sum(xiaojine) from cashin2 where daid=shuipiao.liushuihao) as yidui from shuipiao where shoufeiid !='' order by shoufeiid desc";


        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager2.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();

     
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind3();
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            for (int j = 0; j < GridView1.Columns.Count; j++)
            {
                if (GridView1.Rows[i].Cells[j].Text.ToString() == "否" || GridView1.Rows[i].Cells[j].Text.ToString() == "未提交")
                {
                    GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");


            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
        }


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *,(select sum(xiaojine) from cashin2 where daid=shuipiao.liushuihao) as yidui from shuipiao where shoufeiid like '%" + TextBox1.Text.Trim() + "%'  or fukuanren like '%" + TextBox1.Text.Trim() + "%' or liushuihao in (select daid from cashin2 where xiangmuid2 in (select id from ceshifeikf where taskid like '%" + TextBox1.Text.Trim() + "%')) or liushuihao in (select daid from cashin2 where xiangmuid2 in ( select id from ceshifeikf where taskid in ( select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text.Trim() + "%'))) order by id desc";


        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

       
        AspNetPager2.Visible = false;
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

}