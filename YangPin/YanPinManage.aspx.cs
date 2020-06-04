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

public partial class YangPin_YanPinManage : System.Web.UI.Page
{
    protected string picihao = "";
    protected string biaozhi = "0";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["biaozhi"] != null)
        {
            biaozhi = Request.QueryString["biaozhi"].ToString();
        }
        if (biaozhi == "1")
        {
            Button1.Visible = false;
            Button2.Visible = false;

        }


        picihao = Request.QueryString["sampleid"].ToString();
        //picihao = "YY201209";

        TextBox1.Text = picihao;

        if (!IsPostBack)
        {

            YaopinList();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql2 = "select * from yangpin2 where sampleid='" + picihao + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                TextBox7.Text = dr["position"].ToString();
            }

            con.Close();
            TextBox6.Text = DateTime.Now.ToShortDateString();
        }

        if (!limit1("样品管理"))
        {
            Button1.Enabled = false;
            Button2.Enabled = false;
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
        try
        {
            if (DropDownList1.SelectedValue == "否")
            {
                string ypid = YP_id();
                string pchid = PCH_id();
                string sql = "insert into YaoPinManage values('" + ypid + "','" + picihao + "','" + pchid + "','" + TextBox2.Text + "','','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox7.Text + "','" + TextBox6.Text + "','" + TextBox8.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','','','','','')";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                for (int i = 0; i < Convert.ToInt32(TextBox4.Text.Trim()); i++)
                {
                    string ypid = YP_id();
                    string pchid = PCH_id();
                    string sql = "insert into YaoPinManage values('" + ypid + "','" + picihao + "','" + pchid + "','" + TextBox2.Text + "','','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox7.Text + "','" + TextBox6.Text + "','" + TextBox8.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','','','','','')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
        }
        finally
        {
            con.Close();
            YaopinList();
        }
    }

    protected void YaopinList()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from YaoPinManage where picihao='" + picihao + "' order by id asc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
        con.Dispose();
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (limit1("样品管理"))
        {


            if (biaozhi == "0")
            {
                string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();

                string sql = "delete from YaoPinManage where id='" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                con.Close();

                YaopinList();
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
            if (biaozhi == "1")
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[9].Visible = false;
            }


        }
    }

    //药品编号
    protected string YP_id()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string head = "1";
        string YPid = "";
        int h4 = 1;
        string h5 = h4.ToString("00000");
        string sql = "select yaopinid from YaoPinManage where picihao='" + picihao + "' order by id asc";
        SqlDataAdapter adpter = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        adpter.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            YPid = head + h5;
        }
        else
        {
            string haoma = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["yaopinid"].ToString();
            string qianzhui = haoma.Substring(0, 1);

            string houzhui = haoma.Substring(1, 5);
            int a1 = Convert.ToInt32(houzhui);
            int b1 = a1 + 1;
            YPid = qianzhui + b1.ToString("00000");
        }
        con.Close();
        return YPid;
    }

    //批次号+编号
    protected string PCH_id()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string head = picihao + "-";
        string PCHid = "";
        int h4 = 1;
        string h5 = h4.ToString("00");
        string sql = "select picihaobianhao from YaoPinManage where  picihao='" + picihao + "' order by id asc";
        SqlDataAdapter adpter = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        adpter.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            PCHid = head + h5;
        }
        else
        {
            //string haoma = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["picihaobianhao"].ToString();
            //string qianzhui = haoma.Substring(0, 11);
            //string houzhui = haoma.Substring(11, 2);
            //int a1 = Convert.ToInt32(houzhui);
            //int b1 = a1 + 1;
            //PCHid = qianzhui + b1.ToString("00");
            //2019-9-7修改
            string haoma = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["picihaobianhao"].ToString();
            string qianzhui = haoma.Substring(haoma.LastIndexOf("-") + 1);
            int i = int.Parse(qianzhui);
            i++;
            string str = i.ToString();
            string s = "";
            if (str.Length < 2)
            {
                s = str.PadLeft(2, '0');
            }
            else
            {
                s = str;
            }
            PCHid = head + s;
        }
        con.Close();
        return PCHid;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        YaopinList();
    }
}