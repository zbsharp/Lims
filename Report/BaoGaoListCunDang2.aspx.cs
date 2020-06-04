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

public partial class Report_BaoGaoListCunDang2 : System.Web.UI.Page
{
    protected string shwhere = "1=1";
    private int _i = 0;
    const string vsKey = "searchbaogao3";
    protected void Page_Load(object sender, EventArgs e)
    {


        shwhere = " statebumen1='合格'  and statebumen2='合格' and pizhunby !='' ";



        if (!IsPostBack)
        {
            txFDate.Value = DateTime.Now.AddDays(-7).ToShortDateString();
            txTDate.Value = DateTime.Now.ToShortDateString();
            Bind();
        

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sql21 = "select * from CustomerLinkMan where  customerid=(select top 1 kehuid from customer where customname like '%CQC%')";
            SqlDataAdapter ad21 = new SqlDataAdapter(sql21, con);
            DataSet ds21 = new DataSet();
            ad21.Fill(ds21);
            con.Close();
            con.Dispose();
            DropDownList2.DataSource = ds21.Tables[0];
            DropDownList2.DataTextField = "name";
            DropDownList2.DataValueField = "id";
            DropDownList2.DataBind();

            con.Close();
            DataBind();

        }
    }
    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";


        sql = "select *,(select shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao from BaoGao2 where " + shwhere + " and convert(datetime,pizhundate) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and kuaidihao ='' order by  convert(datetime,pizhundate) desc";
    
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

        con.Close();
        con.Dispose();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;

        string sqlstr;
        if (DropDownList1.SelectedValue == "0")
        {
            sqlstr = "select  *,(select shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao from BaoGao2 where convert(datetime,pizhundate) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and kuaidihao ='' order by  convert(datetime,pizhundate) desc ";
        }
        else
        {
            sqlstr = " select  *,(select shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao from BaoGao2  where  " + ChooseNo + " like  '%" + ChooseValue + "%' and convert(datetime,pizhundate) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and kuaidihao ='' order by  convert(datetime,pizhundate) desc";
        }
        AspNetPager2.Visible = false;

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
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");


            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
        }
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (e.Row.Cells[10].Text.Trim().ToString().Substring(0, 4) == "1900")
                //{
                //    e.Row.Cells[10].Text = "";
                //}
                //if (e.Row.Cells[14].Text.Trim().ToString().Substring(0, 4) == "1900")
                //{
                //    e.Row.Cells[14].Text = "";
                //}
                //if (e.Row.Cells[15].Text.Trim().ToString().Substring(0, 4) == "1900")
                //{
                //    e.Row.Cells[15].Text = "";
                //}
                //if (e.Row.Cells[3].Text.Trim().ToString().Substring(0, 4) == "1900")
                //{
                //    e.Row.Cells[3].Text = "";
                //}
                //if (e.Row.Cells[4].Text.Trim().ToString().Substring(0, 4) == "1900")
                //{
                //    e.Row.Cells[4].Text = "";
                //}

            }
        }
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string dianhua = "";
        string  sql2 = "select * from userinfo where username='"+Session["UserName"].ToString()+"'";
        SqlCommand cmd2 = new SqlCommand(sql2,con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            dianhua = dr2["banggongdianhua"].ToString();
        }
        dr2.Close();


        string shoujianren = "";
        string shoujiandianhua = "";

        string sql3 = "select * from CustomerLinkMan where id='" + DropDownList2.SelectedValue + "'";
        SqlCommand cmd3 = new SqlCommand(sql3, con);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {
            shoujianren  = dr3["name"].ToString();
            shoujiandianhua = dr3["telephone"].ToString();
        }
        dr3.Close();

        string shoujiangongsi = "";
        string shoujiandizhi = "";


        string sql4 = "select top 1 * from Customer where customname like '%CQC%'";
        SqlCommand cmd4 = new SqlCommand(sql4, con);
        SqlDataReader dr4 = cmd4.ExecuteReader();
        if (dr4.Read())
        {
            shoujiangongsi  = dr4["customname"].ToString();
            shoujiandizhi  = dr4["address"].ToString();
        }
        dr4.Close();


        string sql = "insert into KuaiDi values('倍测检测','" + TextBox2.Text + "','" + Session["UserName"].ToString() + "','" + dianhua + "','" + Convert.ToDateTime(TextBox3.Text)+ "','" + shoujianren  + "','" + shoujiandizhi  + "','" + shoujiangongsi   + "','" + shoujiandianhua  + "','" + TextBox4.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "')";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        string sql6 = "insert into KuaiDizibiao values('" + TextBox2.Text + "','" + TextBox2.Text + "','报告','报告','备注','" + Session["username"].ToString() + "','" + DateTime.Now + "')";
        SqlCommand cmd6 = new SqlCommand(sql6, con);
        cmd6.ExecuteNonQuery();




        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            if (hzf.Checked)
            {
                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();






                string sql5 = "update baogao2 set kuaidihao='" + TextBox2.Text + "' where id='" + sid + "'";
                SqlCommand cmd5 = new SqlCommand(sql5, con);
                cmd5.ExecuteNonQuery();
            }
        }
        con.Close();

        ld.Text = "<script>alert('保存成功!');</script>";

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