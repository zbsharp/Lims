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
public partial class YangPinSee : System.Web.UI.Page
{
    protected string yangpinid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        yangpinid = Request.QueryString["yangpinid"].ToString();

        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();


        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select * from YangPin2 where id='" + yangpinid + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                TextBox1.Text = dr["kehuname"].ToString();
                TextBox2.Text = dr["anjianid"].ToString();
                TextBox3.Text = dr["fuwuid"].ToString();
                TextBox4.Text = dr["name"].ToString();
                TextBox5.Text = dr["model"].ToString();
                TextBox6.Text = dr["type"].ToString();
                TextBox7.Text = dr["count"].ToString();
                TextBox8.Text = dr["danwei"].ToString();
                TextBox9.Text = dr["position"].ToString();
                TextBox10.Text = dr["remark"].ToString();
                TextBox11.Text = Convert.ToDateTime(dr["receivetime"].ToString()).ToShortDateString();
                TextBox12.Text = dr["sampleid"].ToString();
                txtbiaoqianremork.Text = dr["beizhu1"].ToString();
                //DropDownList3.SelectedValue = dr["pub_field5"].ToString();


                string EMCbiaozhun1 = dr["pub_field4"].ToString();
                string[] EMCbiaozhun2 = EMCbiaozhun1.Split('|');
                //foreach (string str in EMCbiaozhun2)
                //{
                //    for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                //    {
                //        if (this.CheckBoxList2.Items[i].Text == str)
                //        {
                //            this.CheckBoxList2.Items[i].Selected = true;
                //        }
                //    }
                //}

                string EMCbiaozhun3 = dr["pub_field3"].ToString();
                string[] EMCbiaozhun4 = EMCbiaozhun3.Split('|');
                foreach (string str in EMCbiaozhun4)
                {
                    for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                    {
                        if (this.CheckBoxList1.Items[i].Text == str)
                        {
                            this.CheckBoxList1.Items[i].Selected = true;
                        }
                    }
                }


                DropDownList1.SelectedValue = dr["pub_field2"].ToString();
                DropDownList2.SelectedValue = dr["beizhu4"].ToString();
                //DataBind();
            }
            dr.Close();


            string sql2 = "select top 1 * from YangPin2Detail where sampleid='" + TextBox12.Text + "' and state='封存'";
            SqlCommand com2 = new SqlCommand(sql2, con);
            SqlDataReader dr2 = com2.ExecuteReader();
            if (dr2.Read())
            {
                TextBox13.Text = dr2["pub_field3"].ToString();
            }
            dr2.Close();

            con.Close();

            YaopinList();
        }

        if (limit1("样品管理"))
        {

        }
        else
        {
            Button1.Enabled = false;
            Button3.Enabled = false;
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
        //for (int i = 1; i < CheckBoxList2.Items.Count+1; i++)
        //{
        //    if (CheckBoxList2.Items[i - 1].Selected)
        //    {
        //        bumen2 += CheckBoxList2.Items[i - 1].Text.ToString() + "|";
        //    }
        //}
        if (bumen2.Length > 0)
        {
            bumen2 = bumen2.Substring(0, bumen2.LastIndexOf('|'));
        }
        else
        {
            bumen2 = "0";
        }

        try
        {
            string sql = "update YangPin2 set beizhu4='" + DropDownList2.SelectedValue + "',pub_field2='" + DropDownList1.SelectedValue + "', pub_field3='" + bumen1 + "', pub_field4='" + bumen2 + "',kehuname='" + TextBox1.Text + "',anjianid='" + TextBox2.Text + "',fuwuid='" + TextBox3.Text + "',name='" + TextBox4.Text + "',model='" + TextBox5.Text + "',type='" + TextBox6.Text + "',count='" + Convert.ToInt32(TextBox7.Text) + "',danwei='" + TextBox8.Text + "',position='" + TextBox9.Text + "',remark='" + TextBox10.Text + "',receivetime='" + TextBox11.Text + "',sampleid='" + TextBox12.Text + "',beizhu1='" + txtbiaoqianremork.Text + "' where (id='" + yangpinid + "')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            string sql2 = "update YangPin2Detail set  pub_field3='" + TextBox13.Text + "' where sampleid='" + TextBox12.Text + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.ExecuteNonQuery();

            Label2.Text = "修改成功！";
        }
        catch (Exception ex)
        {
            Label2.Text = ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！";
        }
        finally
        {
            con.Close();
        }
    }

    protected void YaopinList()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from YaoPinManage where picihao='" + TextBox12.Text.Trim() + "' order by id asc";
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
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "delete from YaoPinManage where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();

        YaopinList();
    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sid = sampleid();
        SqlCommand cmd = new SqlCommand("InsertInto", con);

        cmd.Parameters.Add("@empid", SqlDbType.VarChar, 50);

        cmd.Parameters["@empid"].Value = yangpinid;

        cmd.Parameters.Add("@sid", SqlDbType.VarChar, 50);

        cmd.Parameters["@sid"].Value = sid;


        cmd.Parameters.Add("@laosid", SqlDbType.VarChar, 50);

        cmd.Parameters["@laosid"].Value = TextBox12.Text.Trim();


        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();




        string sql2 = "select * from YaoPinManage where  picihao='" + TextBox12.Text.Trim() + "'";



        SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);
        DataTable dt2 = ds2.Tables[0];
        for (int i = 0; i < dt2.Rows.Count; i++)
        {

            SqlCommand cmd2 = new SqlCommand("InsertInto2", con);

            cmd2.Parameters.Add("@empid", SqlDbType.VarChar, 50);

            cmd2.Parameters["@empid"].Value = dt2.Rows[i]["id"].ToString();

            cmd2.Parameters.Add("@sid", SqlDbType.VarChar, 50);

            cmd2.Parameters["@sid"].Value = sid;


            cmd2.Parameters.Add("@laosid", SqlDbType.VarChar, 50);

            cmd2.Parameters["@laosid"].Value = TextBox12.Text.Trim();


            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.ExecuteNonQuery();




        }

        con.Close();

        Response.Redirect("YangPinSee1.aspx?yangpinid=" + sid);

    }


    protected string sampleid()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string date1 = "";
        string houzhui = "";
        string yue = "";
        string tian = "";
        string yue1 = "";
        if (DateTime.Today.Month < 10)
        {
            date1 = "0" + DateTime.Today.Month;
        }
        else
        {
            date1 = DateTime.Today.Month.ToString();
        }





        string head = "YP" + DateTime.Now.Year.ToString() + date1;
        string sampleid = "";
        int h4 = 1;
        string h5 = h4.ToString("0000");
        string sql = "select sampleid from YangPin2 where sampleid like '" + head + "%'  order by id asc";
        SqlDataAdapter adpter = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        adpter.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            sampleid = head + h5;
        }
        else
        {
            houzhui = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["sampleid"].ToString();
            yue = houzhui.Substring(6, 2);
            tian = houzhui.Substring(8, 4);
            if (DateTime.Today.Month < 10)
            {
                yue1 = "0" + DateTime.Today.Month;
            }
            else
            {
                yue1 = Convert.ToString(DateTime.Today.Month);

            }

            if (yue == yue1)
            {
                int a = Convert.ToInt32(tian) + 1;
                sampleid = "YP" + DateTime.Now.Year.ToString() + yue1 + String.Format("{0:D4}", a);
            }
            else
            {

                sampleid = "YP" + DateTime.Now.Year.ToString() + yue1 + "0001";

            }
        }
        con.Close();
        return sampleid;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}
