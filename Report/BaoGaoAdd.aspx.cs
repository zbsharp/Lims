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
using DBBLL;

public partial class Report_BaoGaoAdd : System.Web.UI.Page
{
    protected string bianhao = "";
    protected string rwid = "";
    protected string shenqingbianhao = "";
    protected void Page_Load(object sender, EventArgs e)
    {



        //if (Session["UserName"].ToString() != "admin")
        //{
        //    Response.Write("<script>alert('该任务没有指定工程部门');window.location.href='../Customer/CustomerManage.aspx'</script>");

        //}

        bianhao = Request.QueryString["renwuid"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string state = "";


        string sql3 = "select taskid from anjianinfo where taskid='" + bianhao + "'";
        SqlCommand cmd3 = new SqlCommand(sql3, con);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {
            rwid = dr3["taskid"].ToString();
        }
        dr3.Close();

        string sqlb = "select state from anjianinfo2 where rwbianhao='" + rwid + "'";
        SqlCommand cmdb = new SqlCommand(sqlb, con);
        SqlDataReader drb = cmdb.ExecuteReader();
        if (drb.Read())
        {
            state = drb["state"].ToString();
        }
        drb.Close();


        if (rwid == "")
        {
            con.Close();
            Response.Write("<script>alert('该任务没有指定工程部门');window.location.href='../Customer/CustomerManage.aspx'</script>");
            return;
        }


        string sql31 = "select shenqingbianhao from anjianxinxi2 where  taskno='" + bianhao + "'";
        SqlCommand cmd31 = new SqlCommand(sql31, con);
        SqlDataReader dr31 = cmd31.ExecuteReader();
        if (dr31.Read())
        {
            shenqingbianhao = dr31["shenqingbianhao"].ToString();
        }
        dr31.Close();


        con.Close();

        if (!IsPostBack)
        {
            BindDR();
            YaopinList();
        }


        if (state == "中止")
        {
            Button1.Enabled = false;
            Label3.Text = "该任务已被中止，不能获取报告";
        }
    }
    protected void BindDR()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from UserDepa";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList3.DataSource = ds.Tables[0];
        DropDownList3.DataTextField = "name";
        DropDownList3.DataValueField = "name";
        DropDownList3.DataBind();

        DropDownList3.Items.Insert(0, new ListItem("", ""));
        con.Close();
    }
    protected void YaopinList()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from baogao2 where (rwid='" + bianhao + "' or tjid='" + rwid + "') and tjid !='' order by id asc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
        con.Dispose();
    }

    protected string TiJian_id()
    {
        //体检单号
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string qianzhui = "";
        if (DropDownList1.SelectedItem.Text == "CCC")
        {
            qianzhui = "C-02101-T" + DateTime.Now.Year.ToString();
        }
        else if (DropDownList1.SelectedItem.Text == "CQC")
        {
            qianzhui = "C-02101-V" + DateTime.Now.Year.ToString();
        }
        else if (DropDownList1.SelectedItem.Text == "监督")
        {
            qianzhui = "C-02101-I" + DateTime.Now.Year.ToString();
        }
        else if (DropDownList1.SelectedItem.Text == "信安CCC")
        {
            qianzhui = "C-02101-14" + "ISCCC";
        }
        else
        {
            qianzhui = "SET" + DateTime.Now.Year.ToString() + "-";
        }

        //Select Max(baogaoid) From baogao2 Where baogaoid Like 'C-022-T________[0123456789]'

        string tijianid = "";


        if (DropDownList1.SelectedItem.Text != "其他")
        {
            int h4 = 1;
            string h5 = h4.ToString("00000");
            string sql1 = "";

            if (DropDownList1.SelectedItem.Text == "监督")
            {
                sql1 = "select Max(baogaoid) as baogaoid from baogao2 where leibie='" + DropDownList1.SelectedItem.Text + "'  and baogaoid Like 'C-02101-I________[0123456789]' and baogaoid is not null";
            }
            else if (DropDownList1.SelectedItem.Text == "CQC")
            {
                sql1 = "select Max(baogaoid) as baogaoid from baogao2 where leibie='" + DropDownList1.SelectedItem.Text + "'  and baogaoid Like 'C-02101-V________[0123456789]'  and baogaoid is not null";
            }
            else if (DropDownList1.SelectedItem.Text == "CCC")
            {
                sql1 = "Select Max(baogaoid) as baogaoid  From baogao2 Where leibie='" + DropDownList1.SelectedItem.Text + "' and baogaoid Like 'C-02101-T________[0123456789]' and beizhu1 !='是' and baogaoid is not null";
            }
            else if (DropDownList1.SelectedItem.Text == "信安CCC")
            {
                sql1 = "Select Max(baogaoid) as baogaoid  From baogao2 Where leibie='" + DropDownList1.SelectedItem.Text + "' and baogaoid Like 'C-02101-14ISCCC%' and baogaoid is not null";
            }
            else
            {
                sql1 = "select baogaoid from baogao2 where leibie ='其他' and baogaoid like 'SET%' and beizhu1 !='是' and baogaoid is not null  order by convert (int, substring(baogaoid,11,5)) asc";

            }
            SqlDataAdapter adpter = new SqlDataAdapter(sql1, con);
            DataSet ds = new DataSet();
            adpter.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                tijianid = qianzhui + h5;
            }
            else
            {
                string haoma = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["baogaoid"].ToString();

                string qianzhui1 = "";

                int a1 = 0;
                int b1 = 0;


                if (DropDownList1.SelectedItem.Text == "信安CCC")
                {
                    qianzhui1 = haoma.Substring(15, 4);
                    a1 = Convert.ToInt32(qianzhui1);
                    b1 = a1 + 1;
                    tijianid = qianzhui + b1.ToString("0000");
                }
                else
                {
                    qianzhui1 = haoma.Substring(13, 5);
                    a1 = Convert.ToInt32(qianzhui1);
                    b1 = a1 + 1;
                    tijianid = qianzhui + b1.ToString("00000");
                }
            }
        }
        else
        {
            int h4 = 1;
            string h5 = h4.ToString("00000");
            string sql1 = "";

            if (DropDownList1.SelectedItem.Text != "其他" && DropDownList1.SelectedItem.Text != "CCC")
            {
                sql1 = "select baogaoid from baogao2 where leibie='" + DropDownList1.SelectedItem.Text + "' and baogaoid like 'C%' and baogaoid is not null  order by convert (int, substring(baogaoid,14,5)) asc";
            }
            else if (DropDownList1.SelectedItem.Text == "CCC")
            {
                sql1 = "Select Max(baogaoid) as baogaoid From baogao2 Where leibie='" + DropDownList1.SelectedItem.Text + "' and baogaoid Like 'C-02101-T________[0123456789]' and baogaoid is not null";
            }
            else
            {
                sql1 = "select Max(baogaoid) as baogaoid  from baogao2 where leibie ='其他' and baogaoid like 'SET%' and beizhu1 !='是'  and baogaoid Like 'SET2014_____[0123456789]' and baogaoid is not null ";

            }
            SqlDataAdapter adpter = new SqlDataAdapter(sql1, con);
            DataSet ds = new DataSet();
            adpter.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                tijianid = qianzhui + h5;
            }
            else
            {
                string haoma = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["baogaoid"].ToString();
                string qianzhui1 = "";

                int a1 = 0;
                int b1 = 0;


                qianzhui1 = haoma.Substring(8, 5);

                a1 = Convert.ToInt32(qianzhui1);
                b1 = a1 + 1;
                tijianid = qianzhui + b1.ToString("00000");
            }
        }
        con.Close();
        return tijianid;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        //if (DropDownList3.SelectedValue == "") 
        //{
        //    Label3.Text = "请选择部门";
        //}
        //else
        {
            try
            {

                string tijianid = "";
                if (TextBox1.Text == "")
                {
                    tijianid = TiJian_id();
                }
                else
                {
                    tijianid = TextBox1.Text.Trim();

                    if (DropDownList1.SelectedItem.Text != "其他")
                    {
                        if (TextBox1.Text.Trim().Length > 18)
                        {
                            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                            con1.Open();
                            string sql1 = "";
                            if (DropDownList1.SelectedItem.Text == "信安CCC")
                            {
                                sql1 = "Select Max(baogaoid) as baogaoid From baogao2 Where leibie='" + DropDownList1.SelectedItem.Text + "' and baogaoid Like 'C-02101-13ISCCC%'";
                            }
                            else
                            {
                                sql1 = "Select Max(baogaoid) as baogaoid From baogao2 Where leibie='" + DropDownList1.SelectedItem.Text + "' and baogaoid Like 'C-02101-" + DropDownList1.SelectedValue + "________[0123456789]'";

                            }
                            SqlDataAdapter adpter = new SqlDataAdapter(sql1, con1);
                            DataSet ds = new DataSet();
                            adpter.Fill(ds);
                            con1.Close();
                            string haoma = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["baogaoid"].ToString();
                            string qianzhui1 = "";

                            int a1 = 0;
                            int b1 = 0;

                            if (DropDownList1.SelectedItem.Text == "信安CCC")
                            {
                                qianzhui1 = haoma.Substring(15, 4);
                            }
                            else
                            {
                                qianzhui1 = haoma.Substring(13, 5);
                            }

                            a1 = Convert.ToInt32(qianzhui1);

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                string qz = "";
                                if (DropDownList1.SelectedItem.Text == "信安CCC")
                                {
                                    qz = TextBox1.Text.Trim().Substring(15, 4);
                                }
                                else
                                {
                                    qz = TextBox1.Text.Trim().Substring(13, 5);
                                }


                                int qzi = Convert.ToInt32(qz);

                                if (qzi > a1)
                                {

                                    Label3.Text = "您输入的号太大了" + haoma;
                                    return;
                                }
                            }
                        }
                        else if (TextBox1.Text.Trim().Length == 18)
                        {
                            if (TextBox1.Text.Trim().Substring(TextBox1.Text.Trim().Length - 1, 1) == "A" || TextBox1.Text.Trim().Substring(TextBox1.Text.Trim().Length - 1, 1) == "B" || TextBox1.Text.Trim().Substring(TextBox1.Text.Trim().Length - 1, 1) == "C" || TextBox1.Text.Trim().Substring(TextBox1.Text.Trim().Length - 1, 1) == "D" || TextBox1.Text.Trim().Substring(TextBox1.Text.Trim().Length - 1, 1) == "E" || TextBox1.Text.Trim().Substring(TextBox1.Text.Trim().Length - 1, 1) == "F" || TextBox1.Text.Trim().Substring(TextBox1.Text.Trim().Length - 1, 1) == "G" || TextBox1.Text.Trim().Substring(TextBox1.Text.Trim().Length - 1, 1) == "H" || TextBox1.Text.Trim().Substring(TextBox1.Text.Trim().Length - 1, 1) == "I" || TextBox1.Text.Trim().Substring(TextBox1.Text.Trim().Length - 1, 1) == "K" || TextBox1.Text.Trim().Substring(TextBox1.Text.Trim().Length - 1, 1) == "J")
                            {

                            }
                            else
                            {
                                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                                con1.Open();
                                string sql1 = "Select Max(baogaoid) as baogaoid From baogao2 Where leibie='" + DropDownList1.SelectedItem.Text + "' and baogaoid Like 'C-02101-" + DropDownList1.SelectedValue + "________[0123456789]'";
                                SqlDataAdapter adpter = new SqlDataAdapter(sql1, con1);
                                DataSet ds = new DataSet();
                                adpter.Fill(ds);
                                con1.Close();
                                string haoma = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["baogaoid"].ToString();
                                string qianzhui1 = "";

                                int a1 = 0;
                                int b1 = 0;


                                qianzhui1 = haoma.Substring(13, 5);

                                a1 = Convert.ToInt32(qianzhui1);

                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    string qz = TextBox1.Text.Trim().Substring(13, 5);
                                    int qzi = Convert.ToInt32(qz);

                                    if (qzi > a1)
                                    {

                                        Label3.Text = "您输入的号太大了" + haoma;
                                        return;
                                    }
                                }
                            }
                        }



                    }





                }
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sql31 = "select baogaoid from baogao2 where baogaoid='" + tijianid + "'";
                SqlCommand cmd31 = new SqlCommand(sql31, con);
                SqlDataReader dr31 = cmd31.ExecuteReader();
                if (dr31.Read())
                {
                    dr31.Close();
                    Label3.ForeColor = Color.Red;
                    Label3.Text = "该报告号已被使用，请重新获取";
                }
                else
                {
                    dr31.Close();
                    string dep = "";
                    string sqlu = "select departmentname from userinfo where username='" + Session["UserName"].ToString() + "'";
                    SqlCommand cmdu = new SqlCommand(sqlu, con);
                    SqlDataReader dru = cmdu.ExecuteReader();
                    if (dru.Read())
                    {
                        dep = dru["departmentname"].ToString();
                    }
                    dru.Close();


                    string rand = "";
                    string rand1 = "";
                    //Hashtable ta = new Hashtable();

                    //string sqlran = "select top 1 beizhu3 from baogao2 order by id desc";
                    //SqlCommand cmdrand = new SqlCommand(sqlran, con);
                    //SqlDataReader drrand = cmdrand.ExecuteReader();
                    //while (drrand.Read())
                    //{
                    //    ta.Add(drrand["beizhu3"].ToString(), drrand["beizhu3"].ToString());
                    //}
                    //drrand.Close();

                    //Random rd = new Random();

                    //for (int i = 0; i < ta.Count; i++)
                    //{
                    //    rand = rd.Next(10000000, 99999999).ToString();
                    //    if (!ta.Contains(rand))
                    //    {
                    //        rand1 = rand;
                    //    }
                    //}

                    string sqlran = "select top 1 b4 from anjianinfo2 where rwbianhao='" + rwid + "' order by id desc";
                    SqlCommand cmdrand = new SqlCommand(sqlran, con);
                    SqlDataReader drrand = cmdrand.ExecuteReader();
                    if (drrand.Read())
                    {
                        rand1 = drrand["b4"].ToString();
                    }
                    drrand.Close();


                    Label3.Text = "";
                    string sql = "";
                    if (TextBox1.Text.Trim() == "")
                    {
                        sql = "insert into baogao2 values('" + rwid + "','" + bianhao + "','" + tijianid + "','" + DropDownList1.SelectedItem.Text + "','" + DropDownList2.SelectedValue + "','','','','" + shenqingbianhao + "','','','','','','','','" + Session["username"].ToString() + "','" + DateTime.Now + "','','','','','','','','','','','" + DropDownList3.SelectedValue + "','" + rand1 + "','','','否')";
                    }
                    else
                    {
                        sql = "insert into baogao2 values('" + rwid + "','" + bianhao + "','" + tijianid + "','" + DropDownList1.SelectedItem.Text + "','" + DropDownList2.SelectedValue + "','','','','" + shenqingbianhao + "','','','','','','','','" + Session["username"].ToString() + "','" + DateTime.Now + "','','','','','','','','','','是','" + DropDownList3.SelectedValue + "','" + rand1 + "','','','否')";

                    }
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();


                    //string sqlran = "update anjianinfo2 set b4='" + rand + "'";



                }

                string sql2 = "select * from jieshoubumen where  taskid='" + rwid + "'";



                SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);
                DataSet ds2 = new DataSet();
                ad2.Fill(ds2);
                DataTable dt2 = ds2.Tables[0];
                for (int i = 0; i < dt2.Rows.Count; i++)
                {

                    string sql3 = "insert into baogaobumen values('" + rwid + "','" + bianhao + "','" + dt2.Rows[i]["bumen"].ToString() + "','" + tijianid + "','','','','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + dt2.Rows[i]["canyu"].ToString() + "')";
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    cmd3.ExecuteNonQuery();
                }







                con.Close();
                TextBox1.Text = "";


                //ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('保存成功');", true);
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
                Label3.ForeColor = Color.Red;


                Label3.Text = "获取报告号失败,请输入报告号获取";
            }
            finally
            {

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
        }
        //if (e.Row.RowIndex >= 0)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (e.Row.Cells[4].Text.Trim().ToString().Substring(0, 4) == "1900")
        //        {
        //            e.Row.Cells[4].Text = "";
        //        }
        //        if (e.Row.Cells[5].Text.Trim().ToString().Substring(0, 4) == "1900")
        //        {
        //            e.Row.Cells[5].Text = "";
        //        }
        //        if (e.Row.Cells[6].Text.Trim().ToString().Substring(0, 4) == "1900")
        //        {
        //            e.Row.Cells[6].Text = "";
        //        }
        //    }
        //}
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql2 = "select * from baogao2 where id='" + id + "' and (pizhunby !='')";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        { }
        else
        {
            dr2.Close();
            string sql = "delete from  baogao2  where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        con.Close();
        YaopinList();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string rand1 = "";
        string sql = "";
        string sqlran = "select top 1 b4 from anjianinfo2 where rwbianhao='" + rwid + "' order by id desc";
        SqlCommand cmdrand = new SqlCommand(sqlran, con);
        SqlDataReader drrand = cmdrand.ExecuteReader();
        if (drrand.Read())
        {
            rand1 = drrand["b4"].ToString();
        }
        drrand.Close();

        string tijianid = "";

        tijianid = TiJian_id2();

        {
            //sql = "insert into baogao2 values('" + rwid + "','" + bianhao + "','" + tijianid + "','" + DropDownList1.SelectedItem.Text + "','" + DropDownList2.SelectedValue + "','','','','" + shenqingbianhao + "','','','','','','','','" + Session["username"].ToString() + "','" + DateTime.Now + "','','','','','','','','','','是','" + DropDownList3.SelectedValue + "','" + rand1 + "','','','是')";
            sql = @"insert into [dbo].[baogao2](tjid,rwid,baogaoid,leibie,dengjiby,beizhu2,fillname,filltime)
                    values('" + rwid + "','" + bianhao + "','" + tijianid + "','" + DropDownList1.SelectedItem.Text + "','" + shenqingbianhao + "','" + rand1 + "','" + Session["username"].ToString() + "','" + DateTime.Now.ToString() + "')";
        }
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();


        con.Close();
        YaopinList();
    }



    protected string TiJian_id2()
    {
        //体检单号
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();
        //string qianzhui = "";
        //{
        //    qianzhui = "NO" + DateTime.Now.Year.ToString() + "-";
        //}
        ////Select Max(baogaoid) From baogao2 Where baogaoid Like 'C-022-T________[0123456789]'
        //string tijianid = "";
        //int h4 = 1;
        //string h5 = h4.ToString("00000");
        //string sql1 = "";
        //sql1 = "select Max(baogaoid) as baogaoid from baogao2 where   beizhu6 ='是'";
        //SqlDataAdapter adpter = new SqlDataAdapter(sql1, con);
        //DataSet ds = new DataSet();
        //adpter.Fill(ds);
        //if (ds.Tables[0].Rows.Count == 0)
        //{
        //    tijianid = qianzhui + h5;
        //}
        //else
        //{
        //    string haoma = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["baogaoid"].ToString();
        //    string qianzhui1 = "";
        //    int a1 = 0;
        //    int b1 = 0;
        //    qianzhui1 = haoma.Substring(7, 5);
        //    a1 = Convert.ToInt32(qianzhui1);
        //    b1 = a1 + 1;
        //    tijianid = qianzhui + b1.ToString("00000");
        //}
        //con.Close();
        //return tijianid;

        ////////////////////*************2019-8-2修改
        string laboratory = this.DropDownList1.SelectedItem.Text;
        string type;
        if (laboratory == "安规")
        {
            type = "S";
        }
        else if (laboratory == "EMC")
        {
            type = "E";
        }
        else if (laboratory == "化学")
        {
            type = "R";
        }
        else if (laboratory == "物理")
        {
            type = "P";
        }
        else
        {
            type = "C";
        }
        string rw = bianhao;
        rw = rw.Substring(0, 13);
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select baogaoid from baogao2 where rwid='" + bianhao + "' and baogaoid like '%" + type + "0%' order by id desc ";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                //该任务单还没有报告号
                string str = rw + type + "0" + "1";
                return str;
            }
            else
            {
                string bg_id = ds.Tables[0].Rows[0][0].ToString();
                bg_id = bg_id.Remove(0, 15);
                int i = Convert.ToInt32(bg_id);
                i++;
                string str = rw + type + "0" + i;
                return str;
            }
        }
    }
}