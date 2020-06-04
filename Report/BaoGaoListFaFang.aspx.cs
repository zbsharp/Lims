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
using System.Collections.Generic;

public partial class Report_BaoGaoListFaFang : System.Web.UI.Page
{
    private int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            limit("报告发放");
            txFDate.Value = DateTime.Now.AddYears(-1).AddDays(-7).ToString("yyyy-MM-dd");
            txTDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            Bind();
        }
    }
    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *,(select weituodanwei from anjianinfo2 where rwbianhao =baogao2.tjid) as weituo,(select  name from anjianinfo2 where rwbianhao =baogao2.rwid) as name,(select bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2  from BaoGao2  where ([state]='已发放' or [state]='已缮制')   and baogaoid in (select caseid from baogaofujian2)  order by convert(datetime,pizhundate) desc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        Literal1.Text = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sqlstr = "";
        if (DropDownList1.SelectedValue == "全部")
        {

            sqlstr = "select  *,(select weituodanwei from anjianinfo2 where rwbianhao =baogao2.tjid) as weituo ,(select name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select  bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2  from BaoGao2 where ([state]='已发放' or [state]='已缮制') and  convert(datetime,pizhundate) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' order by convert(datetime,pizhundate) desc";
        }
        else
        {
            sqlstr = "select  *,(select  weituodanwei from anjianinfo2 where rwbianhao =baogao2.tjid) as weituo ,(select  name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select   bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2  from BaoGao2 where ([state]='已发放' or [state]='已缮制') and " + DropDownList1.SelectedValue + " like '%" + TextBox1.Text.Trim() + "%' and convert(datetime,pizhundate) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' order by convert(datetime,pizhundate) desc";
        }
        SqlDataAdapter ad = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        con.Dispose();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");

            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 6);
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 6);
            //e.Row.Cells[5].Text = SubStr(e.Row.Cells[5].Text, 6);

            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
            LinkButton LinkBtn_DetailInfo21 = (LinkButton)e.Row.FindControl("LinkButton5");

            e.Row.Cells[6].Text = Eng(e.Row.Cells[2].Text);
        }
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
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

    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        if (con.State == ConnectionState.Broken)
        {
            con.Close();
            con.Open();
        }
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[9].FindControl("CheckBox1");
            if (hzf.Checked)
            {
                int index = gr.RowIndex;
                string renwuid = GridView1.Rows[index].Cells[1].Text.ToString();
                string baogaoid = GridView1.Rows[index].Cells[2].Text.ToString();

                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                string sql = "update baogao2 set qianlinby='',fafangby='" + Session["Username"].ToString() + "',fafangdate='" + DateTime.Now + "',youjidanid='',[state]='已发放'  where id='" + sid + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                string sql2 = "insert into baogaofafang values('" + renwuid + "','" + baogaoid + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + TextBox3.Text + "','','')";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.ExecuteNonQuery();
            }
        }
        con.Close();
        Bind();
    }

    protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        int index = gvrow.RowIndex;
        string renwuid = GridView1.Rows[index].Cells[1].Text.ToString();
        string baogaoid = GridView1.Rows[index].Cells[2].Text.ToString();
        string state = "";
        if (e.CommandName == "fafang")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = "update baogao2 set qianlinby='',fafangby='" + Session["Username"].ToString() + "',fafangdate='" + DateTime.Now + "',youjidanid='',[state]='已发放'  where id='" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                string sql2 = "insert into baogaofafang values('" + renwuid + "','" + baogaoid + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + TextBox3.Text + "','','')";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.ExecuteNonQuery();
                Bind();
                Literal1.Text = "<script>alert('发放成功')</script>";
            }
        }
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

    public string Eng(string baogaoid)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql112 = "select fillname from baogaofujian2 where caseid='" + baogaoid + "'";

        SqlDataAdapter ad112 = new SqlDataAdapter(sql112, con);
        DataSet ds112 = new DataSet();
        ad112.Fill(ds112);
        con.Close();
        DataTable dt112 = ds112.Tables[0];
        DataView dataView = new DataView(dt112);
        dt112 = dataView.ToTable(true, "fillname");
        string zhujian = "";
        for (int z = 0; z < dt112.Rows.Count; z++)
        {
            zhujian += dt112.Rows[z]["fillname"].ToString() + ",";
        }

        if (zhujian.Contains(","))
        {
            zhujian = zhujian.Substring(0, zhujian.Length - 1);
        }
        return zhujian;
    }
}