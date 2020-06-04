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

public partial class Income_Update : System.Web.UI.Page
{
    protected string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"].ToString();
        if (!IsPostBack)
        {
            //判断业务是否认领
            bool b = IsClaim();
            if (b == true)
            {
                Bind();
            }
            else
            {
                Literal1.Text = "<script>alert('该款项业务已认领，不能编辑');window.close();</script>";
            }
        }
    }

    private void Bind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql2 = "select * from shuipiao where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql2, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                TextBox1.Text = dr["fukuanren"].ToString();
                TextBox2.Text = dr["fukuanjine"].ToString();
                TextBox3.Text = dr["fukuanriqi"].ToString();
                TextBox4.Text = dr["fukuanfangshi"].ToString();
                TextBox5.Text = dr["beizhu"].ToString();
                Label3.Text = dr["liushuihao"].ToString();
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //清除文本框中的单引号
        Cleartxt(this.Controls);
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string uuname2 = TextBox1.Text.Trim();
        string uuname3 = TextBox3.Text.Trim();
        string uuname5 = TextBox2.Text.Trim();
        string uuname6 = TextBox4.Text.Trim();
        string uuname8 = TextBox5.Text.Trim();

        string sql = "update shuipiao set fukuanren='" + uuname2 + "',fukuanriqi='" + uuname3 + "',fukuanjine='" + uuname5 + "',fukuanfangshi='" + uuname6 + "',beizhu='" + uuname8 + "'  where id=" + id + "";
        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        Literal1.Text = "<script>alert('保存成功')</script>";
    }

    /// <summary>
    /// 使用清除文本框中的单引号
    /// </summary>
    private void Cleartxt(ControlCollection cc)
    {
        foreach (Control item in cc)
        {
            if (item.HasControls())//判断该控件是否存在子控件
            {
                Cleartxt(item.Controls);
            }
            if (item is TextBox)
            {
                ((TextBox)item).Text = ((TextBox)item).Text.Replace('\'', ' ');
            }
        }
    }

    /// <summary>
    /// 检查该款项是否已被认领
    /// </summary>
    /// <returns></returns>
    private bool IsClaim()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from Claim where shuipiaoid='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                return false;
            }
            else
            {
                dr.Close();
                return true;
            }
        }
    }
}