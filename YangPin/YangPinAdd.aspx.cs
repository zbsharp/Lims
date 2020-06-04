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
using System.Data.SqlClient;
using Common;
using DBBLL;
using DBTable;
using System.Drawing;
public partial class YangPinAdd : System.Web.UI.Page
{
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string bianhao = "";
    protected string sampleidz = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["username"] = "ccic";

        //baojiaid = "HQ2012050111";
        //kehuid = "10000029";
        // limit("样品管理");
        baojiaid = Request.QueryString["baojiaid"].ToString();
        kehuid = Request.QueryString["kehuid"].ToString();
        bianhao = Request.QueryString["bianhao"].ToString();

        if (bianhao == "")
        {
            Button2.Visible = false;
        }
        if (Request.QueryString["sampleid"] != null)
        {
            sampleidz = Request.QueryString["sampleid"].ToString();
            Label1.Text = "(配件)";

        }
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select * from Customer where kehuid='" + kehuid + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                TextBox1.Text = dr["customname"].ToString();
                TextBox12.Text = baojiaid;
            }
            dr.Close();

            string sql2 = "select * from Anjianxinxi2 where bianhao='" + bianhao + "'";
            SqlCommand com2 = new SqlCommand(sql2, con);
            SqlDataReader dr2 = com2.ExecuteReader();
            if (dr2.Read())
            {
                TextBox2.Text = dr2["taskno"].ToString();

            }
            dr2.Close();

            con.Close();
            TextBox11.Text = DateTime.Now.ToShortDateString();
            Bind();
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

    protected bool limit1(string pagename1)
    {
        bool A = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {


            con.Close();
            A = true;
        }
        else
        {
            con.Close();
            A = false;
        }
        return A;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        Random rd = new Random();
        string rd1 = rd.Next(100).ToString();
        string pici = DateTime.Now.ToString("yyyyMMddhh") + rd1;


        string bumen1 = "";
        for (int i = 1; i < CheckBoxList1.Items.Count + 1; i++)
        {
            if (CheckBoxList1.Items[i - 1].Selected)
            {
                bumen1 += CheckBoxList1.Items[i - 1].Text.ToString() + "|";
            }
        }
        if (bumen1.Length > 0)
        {
            bumen1 = bumen1.Substring(0, bumen1.LastIndexOf('|'));
        }
        else
        {
            bumen1 = "0";
        }


        string bumen2 = "";
        //for (int i = 1; i < CheckBoxList2.Items.Count + 1; i++)
        //{
        //    if (CheckBoxList2.Items[i - 1].Selected)
        //    {
        //        bumen2 += CheckBoxList2.Items[i - 1].Text.ToString() + "|";
        //    }
        //}
        //if (bumen2.Length > 0)
        //{
        //    bumen2 = bumen2.Substring(0, bumen2.LastIndexOf('|'));
        //}
        //else
        //{
        //    bumen2 = "0";
        //}
        try
        {
            if (DropDownList2.SelectedValue == "")
            {
                Label2.Text = "请选择价值类别！";
            }
            else
            {
                if (DropDownList1.SelectedValue == "否")
                {
                    string sampleno = sampleid();

                    string sql = "insert into YangPin2 values('" + baojiaid + "','" + kehuid + "','" + TextBox9.Text + "','" + TextBox2.Text.Trim() + "','" + TextBox3.Text + "','" + sampleno + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + Convert.ToInt32(TextBox7.Text) + "','" + TextBox8.Text + "','" + TextBox9.Text + "','" + TextBox11.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + TextBox10.Text + "','" + Convert.ToInt32(TextBox7.Text) + "','入库','','" + DropDownList2.SelectedValue + "','" + bumen1 + "','" + bumen2 + "','" + pici + "','" + bianhao + "','" + txbiaoqianremork.Text.Replace('\'', ' ') + "','','','" + DropDownList3.SelectedValue + "')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    for (int i = 0; i < Convert.ToInt32(TextBox7.Text); i++)
                    {
                        string sampleno = sampleid();

                        string sql = "insert into YangPin2 values('" + baojiaid + "','" + kehuid + "','" + TextBox9.Text + "','" + TextBox2.Text.Trim() + "','" + TextBox3.Text + "','" + sampleno + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','1','" + TextBox8.Text + "','" + TextBox9.Text + "','" + TextBox11.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + TextBox10.Text + "','" + Convert.ToInt32(TextBox7.Text) + "','入库','','" + DropDownList2.SelectedValue + "','" + bumen1 + "','" + bumen2 + "','" + pici + "','" + bianhao + "','" + txbiaoqianremork.Text.Replace('\'', ' ') + "', '', '', '" + DropDownList3.SelectedValue + "')";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
                }
                Label2.Text = "保存成功！";
            }
        }
        catch (Exception ex)
        {
            con.Close();
            Label2.Text = ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！";
        }
        finally
        {
            con.Close();
            Bind();
        }
    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from YangPin2 where anjianid='" + TextBox2.Text.Trim() + "' order by id desc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }


    //样品编号
    protected string sampleid()
    {
        //***********************2019-8-1修改
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select top 1 sampleid from YangPin2 order by id desc ";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                string year = DateTime.Now.Year.ToString();
                string str = year.Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + "00001";
                return str;
            }
            else
            {
                string year = DateTime.Now.Year.ToString();
                string bianhao = ds.Tables[0].Rows[0][0].ToString().Substring(4, 5);
                int i = Convert.ToInt32(bianhao);
                i++;
                string str = year.Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + i.ToString().PadLeft(5, '0');
                return str;
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("YangPinManageCha.aspx?taskno=" + TextBox2.Text.Trim());
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {



            e.Row.Cells[7].Text = SubStr(e.Row.Cells[7].Text, 6);


        }
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
    protected void Button3_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql2 = "select * from Anjianinfo2 where rwbianhao='" + TextBox2.Text.Trim() + "'";
        SqlCommand com2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = com2.ExecuteReader();
        if (dr2.Read())
        {
            TextBox4.Text = dr2["chanpinname"].ToString();
            TextBox5.Text = dr2["xinghaoguige"].ToString();

            TextBox9.Text = dr2["zhizaodanwei"].ToString();
            bianhao = dr2["bianhao"].ToString();
        }
        dr2.Close();

        con.Close();
    }
}
