using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Quotation_ShenPi : System.Web.UI.Page
{
    protected string baojiaid;
    protected void Page_Load(object sender, EventArgs e)
    {
        baojiaid = Request.QueryString["baojiaid"].ToString();
        if (!IsPostBack)
        {
            Bind();
            BindBaojia();
        }
    }
    private void BindBaojia()
    {
        string sql = @"select *,(select top 1 name from BaoJiaChanPing where baojiaid = baojiabiao.baojiaid) as name,(select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname  from baojiabiao where baojiaid='"+baojiaid+"'";
        using (SqlConnection con=new SqlConnection (ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql,con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView2.DataSource = ds.Tables[0];
            this.GridView2.DataBind();
        }
    }
    private void Bind()
    {
        string sql = "select* from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' and epiboly='外包'";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView1.EditIndex = e.NewEditIndex;
        Bind();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        this.GridView1.EditIndex = -1;
        Bind();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = this.GridView1.DataKeys[e.RowIndex].Value.ToString();//获取客户日志表的id
        string neirongid = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[0].Controls[0]).Text.ToString());
        string neirong = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());
        string shoufei = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
        string yp = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
        string zhouqi = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
        string beizhu = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
        string danwei = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[8].Controls[0]).Text.ToString());
        //验证价格输入是否合理
        try
        {
            double price = Convert.ToDouble(shoufei);
        }
        catch (Exception)
        {
            Response.Write("<script>alert('价格只能输入数字类型。')</script>");
            return;
        }
        int a = Convert.ToInt32(shoufei.Substring(0, 1).ToString());
        if (Convert.ToInt32(shoufei.Substring(0, 1).ToString()) <= 0)
        {
            Response.Write("<script>alert('价格输入不合理。')</script>");
            return;
        }
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = string.Format(@"update Product2 set neirongid='" + neirongid + "', neirong='" + neirong + "', [shoufei]='" + shoufei + "',[yp]='" + yp + "', [zhouqi]='" + zhouqi + "', [beizhu]='" + beizhu + "',  [danwei]='" + danwei + "'  where [id]='" + id + "'");
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        this.GridView1.EditIndex = -1;
        Bind();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        //审核时首先要验证价格是否合法
        //string price = this.GridView1.Rows[0].Cells[2].Text.Trim();
        //if (string.IsNullOrEmpty(price) || price == "&nbsp;")
        //{
        //    Response.Write("<script>alert('价格为空的项目不能审核。')</script>");
        //    return;
        //}
        //string state = this.DropDownList1.SelectedValue;
        //string sql = string.Format(@"update  Product2 set state='" + state + "',verifier='" + Session["UserName"].ToString() + "'  where id=" + id + "");
        //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        //{
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    cmd.ExecuteNonQuery();
        //}
        //Response.Write("<script>alert('已审核')</script>");
        //Bind();

        //------------------2019-8-15修改
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "update BaoJiaBiao set caiwu='" + DropDownList1.SelectedValue + "',caiwuren='"+Session["Username"].ToString()+"',caiwutime='"+DateTime.Now+"' where BaoJiaId='" + baojiaid + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        BindBaojia();
        Response.Write("<script>alert('审核成功')</script>");
    }
}