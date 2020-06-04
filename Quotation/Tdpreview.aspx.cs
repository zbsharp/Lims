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

public partial class AnjianxinxiSee : System.Web.UI.Page
{
    protected string id = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string bianhao = "";
    protected string chanpinbianhao = "";
    protected string st = "下达";
    protected string tikai = "";
    protected string taskno = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"].ToString();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql2 = "select * from Anjianxinxi2 where bianhao='" + id + "'";
        SqlCommand com2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = com2.ExecuteReader();
        if (dr2.Read())
        {
            baojiaid = dr2["baojiaid"].ToString();
            kehuid = dr2["kehuid"].ToString();
            bianhao = dr2["bianhao"].ToString();
            tikai = dr2["biaozhi"].ToString();
            taskno = dr2["taskno"].ToString();
        }
        else
        {
            ld.Text = "<script>alert('该报价还未填过单')</script>";
            Response.Redirect("~/Quotation/QuotationHuiqian.aspx");
        }

        dr2.Close();

        string sql3 = "select * from BaoJiaChanPing where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "' ";
        SqlCommand cmd3 = new SqlCommand(sql3, con);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {
            chanpinbianhao = dr3["id"].ToString();
        }

        dr3.Close();


        string sql4 = "select * from Customer where kehuid='" + kehuid + "' order by kehuid";
        SqlCommand cmd4 = new SqlCommand(sql4, con);
        SqlDataReader dr4 = cmd4.ExecuteReader();
        if (dr4.Read())
        {
            kh.Text = dr4["CustomName"].ToString();

        }
        dr4.Close();

        if (!IsPostBack)
        {
            string sql = "select * from Anjianxinxi2 where bianhao='" + id + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                TextBox29.Text = dr["a1"].ToString();

                TextBox30.Text = dr["a2"].ToString();
                TextBox31.Text = dr["a3"].ToString();
                TextBox32.Text = dr["a4"].ToString();

                TextBox1.Text = dr["baojiaid"].ToString();
                TextBox2.Text = dr["kehuid"].ToString();
                TextBox3.Text = dr["bianhao"].ToString();
                TextBox4.Text = dr["fukuan"].ToString();
                wt.Text = dr["weituo"].ToString();
                zz.Text = dr["zhizao"].ToString();
                sc.Text = dr["shenchan"].ToString();
                TextBox25.Text = dr["wancheng"].ToString();
                yp.Text = dr["ypshuliang"].ToString();
                sb.Text = dr["shangbiao"].ToString();
                TextBox15.Text = dr["shenqingbianhao"].ToString();
                TextBox16.Text = dr["qitayaoqiu"].ToString();
                TextBox17.Text = dr["beizhu"].ToString();

                TextBox18.Text = dr["ypname"].ToString();
                TextBox19.Text = dr["ypnamey"].ToString();
                TextBox20.Text = dr["ypxinghao"].ToString();
                TextBox21.Text = dr["ypxinghaoy"].ToString();
                TextBox22.Text = dr["syriqi"].ToString();
                TextBox23.Text = dr["qyfangshi"].ToString();
                TextBox7.Text = dr["shengcriqi"].ToString();

                rwyouxian.SelectedValue = dr["remark1"].ToString();

                cp.Text = dr["name"].ToString();
                guige.Text = dr["xinghao"].ToString();


                DropDownList1.SelectedValue = dr["songyangfangshi"].ToString();
                DropDownList2.SelectedValue = dr["zhouqi"].ToString();
                DropDownList3.SelectedValue = dr["baogao"].ToString();
                DropDownList4.SelectedValue = dr["lingqu"].ToString();
                DropDownList5.SelectedValue = dr["yangpinchuli"].ToString();
                DropDownList7.SelectedValue = dr["waibao"].ToString();
                DropDownList1.SelectedValue = dr["cpleixing"].ToString();
                TextBox28.Text = dr["hesuanname"].ToString();


                txt_ENweituo.Text = dr["Enweituo"].ToString();
                txt_addressWei.Text = dr["addressWei"].ToString();
                txt_ENaddressWei.Text = dr["ENaddressWei"].ToString();
                txt_ENzhizao.Text = dr["ENzhizao"].ToString();
                txt_ENaddresZhizao.Text = dr["ENaddresZhizao"].ToString();
                txt_addresZhizao.Text = dr["addresZhizao"].ToString();
                txt_ENshengchang.Text = dr["ENshengchang"].ToString();
                txt_addersshengchang.Text = dr["addersshengchang"].ToString();
                txt_ENaddressshengchang.Text = dr["ENaddressshengchang"].ToString();
                txt_ENcpname.Text = dr["ENcpname"].ToString();
                txt_cpzhuce.Text = dr["cpzhuce"].ToString();
                txt_cpfujia.Text = dr["cpzhuce"].ToString();
                txt_cpmiaoshu.Text = dr["cpmiaoshu"].ToString();
                txt_linaxiren.Text = dr["lianxiren"].ToString();
                string EMCbiaozhun1 = dr["xiangmu"].ToString();
                string[] EMCbiaozhun2 = EMCbiaozhun1.Split('|');
            }
            dr.Close();
            con.Close();

            BindContract();
            BindXiangMu();
        }
    }

    public void BindContract()
    {
        string strs = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql21 = "select * from CustomerLinkMan where  customerid='" + kehuid + "' ";
        SqlDataAdapter ad21 = new SqlDataAdapter(sql21, con);
        DataSet ds21 = new DataSet();
        ad21.Fill(ds21);
        DropDownList6.DataSource = ds21.Tables[0];
        DropDownList6.DataTextField = "name";
        DropDownList6.DataValueField = "id";
        DropDownList6.DataBind();

        GridView5.DataSource = ds21.Tables[0];
        GridView5.DataBind();

        string linkman = "";
        string sqllink = "select * from CustomerLinkMan where baojiaid='" + baojiaid + "'";
        SqlCommand cmdlink = new SqlCommand(sqllink, con);
        SqlDataReader drlink = cmdlink.ExecuteReader();
        if (drlink.Read())
        {
            linkman = drlink["id"].ToString();
        }
        drlink.Close();
        DropDownList6.SelectedValue = linkman;
        con.Close();
    }


    protected void BindXiangMu()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();
        foreach (GridViewRow gr in GridView2.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            string sid = GridView2.DataKeys[gr.RowIndex].Value.ToString();
            string sqlx = "select * from anjianxinxi3 where xiangmubianhao='" + sid + "' and bianhao='" + id + "'";
            SqlCommand cmdx = new SqlCommand(sqlx, con);
            SqlDataReader drx = cmdx.ExecuteReader();
            if (drx.Read())
            {
                hzf.Checked = true;
            }
            else
            {
                hzf.Checked = false;
            }
            drx.Close();
        }
        con.Close();
    }


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string klx = "";
        string sqldi = "select * from CustomerLinkMan where id='" + DropDownList6.SelectedValue + "'";
        SqlCommand cmddi = new SqlCommand(sqldi, con);
        SqlDataReader drdi = cmddi.ExecuteReader();
        if (drdi.Read())
        {
            TextBox30.Text = drdi["telephone"].ToString() + "," + drdi["mobile"].ToString();
        }
        drdi.Close();
        con.Close();

        TextBox29.Text = DropDownList6.SelectedItem.Text;
    }

    private void ResetTextBox(ControlCollection cc)
    {
        foreach (Control item in cc)
        {
            if (item.HasControls())
            {
                ResetTextBox(item.Controls);
            }
            string type = item.GetType().ToString();//获取控件类型
            //if (item is TextBox)
            if (type == "System.Web.UI.WebControls.TextBox")
            {
                ((TextBox)item).Text = ((TextBox)item).Text.Replace('\'', ' ');
            }
        }
    }
}
