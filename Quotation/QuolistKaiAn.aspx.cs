using Common;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Quotation_QuolistKaiAn : System.Web.UI.Page
{

    protected string kehuid = "";
    protected DateTime d;
    protected string baojiaid = "";
    protected string baojiaid1 = "";
    protected string chanpingid = "";
    protected string tijiaobiaozhi = "0";
    protected string shenpibiaozhi = "0";
    protected string huiqianbiaozhi = "0";
    protected string bianhao = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        kehuid = Request.QueryString["kehuid"].ToString();
        baojiaid = Request.QueryString["baojiaid"].ToString();

        // this.Button10.Attributes["onclick"] = "javascript:GetMyValue('cs_txt',window.showModalDialog('Default2.aspx?kehuid=" + kehuid + "&baojiaid=" + baojiaid + "','window','dialogWidth=900px;DialogHeight=500px;status:no;help:no;resizable:yes; dialogTop:100px;edge:raised;'));";


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        string sql2 = "select * from anjianxinxi2 where baojiaid='" + baojiaid + "' and (taskno is null or taskno='') and biaozhi !='是'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {

            bianhao = dr2["bianhao"].ToString();
            con.Close();
            Response.Redirect("AnjianxinxiSee.aspx?id=" + bianhao);
        }
        else
        {


            dr2.Close();

            string sql = "select * from BaoJiaChanPing where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                chanpingid = dr["id"].ToString();

            }
            dr.Close();

            if (!IsPostBack)
            {
                Clear();
                BindContract();
                BindXiangMu();
                BindChanPin();
                BindBaoJiaBiao();
                Baojialink();//查询报价联系人
                CP();//查看报价产品
                BingSelect();//查询上一次填单的客户信息
                Panel1.Visible = false;

                string sqldi1 = "select * from CustomerLinkMan where id='" + DropDownList6.SelectedValue + "'";
                SqlCommand cmddi1 = new SqlCommand(sqldi1, con);
                SqlDataReader drdi1 = cmddi1.ExecuteReader();
                if (drdi1.Read())
                {
                    TextBox12.Text = drdi1["telephone"].ToString() + "," + drdi1["mobile"].ToString();
                    TextBox11.Text = DropDownList6.SelectedItem.Text;
                }
                drdi1.Close();

                //string sqlhuaxue = "select * from renzheng";
                //SqlDataAdapter ad9 = new SqlDataAdapter(sqlhuaxue, con);
                //DataSet ds9 = new DataSet();
                //ad9.Fill(ds9);
                //CheckBoxList1.DataSource = ds9.Tables[0];

                //CheckBoxList1.DataTextField = "name";
                //CheckBoxList1.DataValueField = "name"; ;
                //CheckBoxList1.DataBind();
                string klx = "";
                string sqldi = "select address,customname from customer where kehuid='" + kehuid + "'";
                SqlCommand cmddi = new SqlCommand(sqldi, con);
                SqlDataReader drdi = cmddi.ExecuteReader();
                if (drdi.Read())
                {
                    TextBox14.Text = drdi["address"].ToString();
                    TextBox13.Text = drdi["customname"].ToString();
                }

                rwkehu.Text = klx;
                drdi.Close();
            }


            con.Close();

        }



    }
    /// <summary>
    /// 查看报价联系人
    /// </summary>
    protected void Baojialink()
    {
        string sql = "select name from  [dbo].[CustomerLinkMan] where id=(select top 1 linkid from BaoJiaLink where baojiaid='" + baojiaid + "')";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_linaxiren.Text = dr["name"].ToString();
            }
            dr.Close();
        }
    }
    /// <summary>
    /// 查看报价产品
    /// </summary>
    protected void CP()
    {
        string sql = "select id,name from BaoJiaChanPing where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "'";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.drop_CP.DataValueField = "id";
            this.drop_CP.DataTextField = "name";
            this.drop_CP.DataSource = ds.Tables[0];
            this.drop_CP.DataBind();
        }
    }
    /// <summary>
    /// 动态绑定产品数据
    /// </summary>
    protected void CPBind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select name,type from BaoJiaChanPing where id='" + this.drop_CP.SelectedItem.Value + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cp.Text = dr["name"].ToString();
                guige.Text = dr["type"].ToString();
            }
            dr.Close();
        }
    }

    /// <summary>
    /// 查询上一次填单的客户信息
    /// </summary>
    protected void BingSelect()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select top 1* from TiandanRecord where baojiaid='" + baojiaid + "' order by id desc ";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            if (dataReader.Read())
            {
                wt.Text = dataReader["chweituo"].ToString();
                txt_addressWei.Text = dataReader["chweituoaddress"].ToString();
                txt_ENweituo.Text = dataReader["enweituo"].ToString();
                txt_ENaddressWei.Text = dataReader["enweituoaddress"].ToString();
                zz.Text = dataReader["chzhizao"].ToString();
                txt_addresZhizao.Text = dataReader["chzhizaoaddress"].ToString();
                txt_ENzhizao.Text = dataReader["enzhizao"].ToString();
                txt_ENaddresZhizao.Text = dataReader["enzhizaoaddress"].ToString();
                sc.Text = dataReader["chshengchan"].ToString();
                txt_addersshengchang.Text = dataReader["chshengchanaddress"].ToString();
                txt_ENshengchang.Text = dataReader["enshengchan"].ToString();
                txt_ENaddressshengchang.Text = dataReader["enshengchanaddress"].ToString();
            }
            dataReader.Close();
        }
    }

    protected void BindBaoJiaBiao()
    {

        string strs = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql21 = "select * from CustomerLinkMan where  customerid='" + kehuid + "' and delete_biaozhi !='是'";
        SqlDataAdapter ad21 = new SqlDataAdapter(sql21, con);
        DataSet ds21 = new DataSet();
        ad21.Fill(ds21);
        DropDownList6.DataSource = ds21.Tables[0];
        DropDownList6.DataTextField = "name";
        DropDownList6.DataValueField = "id";
        DropDownList6.DataBind();

        GridView5.DataSource = ds21.Tables[0];
        GridView5.DataBind();


        // if (Request.QueryString["baojiaid"] != null)
        {
            string linkman = "";


            string sqllink = "select * from baojialink where baojiaid='" + baojiaid + "'";
            SqlCommand cmdlink = new SqlCommand(sqllink, con);
            SqlDataReader drlink = cmdlink.ExecuteReader();
            if (drlink.Read())
            {
                linkman = drlink["linkid"].ToString();
            }
            drlink.Close();
            DropDownList6.SelectedValue = linkman;
            DropDownList6.Items.Insert(0, new ListItem("", ""));//
        }

        con.Close();
    }
    protected void Clear()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from Customer where kehuid='" + kehuid + "' order by kehuid";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            kh.Text = dr["CustomName"].ToString();
            wt.Text = dr["CustomName"].ToString();
            fk.Text = dr["CustomName"].ToString();
            zz.Text = dr["CustomName"].ToString();
            sc.Text = dr["CustomName"].ToString();
        }
        dr.Close();
        con.Close();
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
            TextBox12.Text = drdi["telephone"].ToString() + "," + drdi["mobile"].ToString();
        }
        drdi.Close();
        con.Close();

        TextBox11.Text = DropDownList6.SelectedItem.Text;

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

        DropDownList1.DataSource = ds21.Tables[0];
        DropDownList1.DataTextField = "name";
        DropDownList1.DataValueField = "id";
        DropDownList1.DataBind();

        //GridView5.DataSource = ds21.Tables[0];
        //GridView5.DataBind();


        if (Request.QueryString["baojiaid"] != null)
        {
            string linkman = "";
            string sqllink = "select * from CustomerLinkMan where baojiaid='" + baojiaid + "'";
            SqlCommand cmdlink = new SqlCommand(sqllink, con);
            SqlDataReader drlink = cmdlink.ExecuteReader();
            if (drlink.Read())
            {
                linkman = drlink["id"].ToString();
            }
            drlink.Close();
            DropDownList1.SelectedValue = linkman;
            con.Close();
        }
        con.Close();
    }








    protected void BindChanPin()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from BaoJiaChanPing where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "' ";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            cp.Text = dr["name"].ToString();
            guige.Text = dr["type"].ToString();
            chanpingid = dr["id"].ToString();

        }
        dr.Close();

        con.Close();
    }


    protected void BindXiangMu()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *,(select name from BaoJiaChanPing where BaoJiaChanPing.id=BaoJiaCPXiangMu.cpid) as cpname,(select [type] from BaoJiaChanPing where BaoJiaChanPing.id = BaoJiaCPXiangMu.cpid) as cptype from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        ResetTextBox(this.Controls);
        if (ypcl.SelectedItem.Text == "快递/Express(到付)" && (TextBox11.Text.Trim() == "" || TextBox12.Text.Trim() == "" || TextBox13.Text.Trim() == "" || TextBox14.Text.Trim() == ""))
        {
            // ld.Text = "<script>alert('请填写邮件接收人信息!');</script>";
        }
        else
        {
            //if (CheckBoxList1.SelectedValue == "")
            //{
            //    ld.Text = "<script>alert('请选择检测项目类别!');</script>";
            //}
            if (ypcl.SelectedValue == "请选择")
            {
                ld.Text = "<script>alert('请选择样品处理!');</script>";
            }
            else
            {
                //int selectednum = 0;
                //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                //{
                //    if (CheckBoxList1.Items[i].Selected)
                //    {
                //        selectednum++;
                //    }
                //}

                //if (selectednum > 1)
                //{
                //    ld.Text = "<script>alert('只能选择一个检测项目!');</script>";
                //}
                //else
                //{
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                MyExcutSql ext = new MyExcutSql();
                bool A = ExcutSqlShenQ(sqbianhao.Text.Trim(), "anjianinfo2");
                if (A == false)
                {
                    Random rd = new Random();
                    string rd1 = rd.Next(1000).ToString();
                    string bianhao = DateTime.Now.ToString("yyyyMMddhhmmss") + rd1;
                    string bumen1 = "委托";
                    //for (int i = 1; i < CheckBoxList1.Items.Count + 1; i++)
                    //{
                    //    if (CheckBoxList1.Items[i - 1].Selected)
                    //    {
                    //        bumen1 += CheckBoxList1.Items[i - 1].Text.ToString() + "|";
                    //    }
                    //}
                    //if (bumen1.Length > 0)
                    //{
                    //    bumen1 = bumen1.Substring(0, bumen1.LastIndexOf('|'));
                    //}
                    //else
                    //{
                    //    bumen1 = "0";
                    //}
                    //TextBox10.Text 预计费用
                    bool bu = false;
                    foreach (GridViewRow gr in GridView2.Rows)
                    {
                        CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
                        if (hzf.Checked)
                        {
                            bu = true;
                            string sid = GridView2.DataKeys[gr.RowIndex].Value.ToString();
                            string sqlx = "insert into Anjianxinxi3 values ('" + baojiaid + "','" + kehuid + "','" + bianhao + "','" + chanpingid + "','" + sid + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";
                            SqlCommand cmdx = new SqlCommand(sqlx, con);
                            cmdx.ExecuteNonQuery();
                        }
                    }
                    if (bu == false)
                    {
                        ld.Text = "<script>alert('测试项目至少需要一个。')</script>";
                        return;
                    }
                    string kh = TextBox11.Text + "," + TextBox12.Text + "," + TextBox13.Text + "," + TextBox14.Text;
                    //string sql = "insert into Anjianxinxi2 values('" + baojiaid + "','" + kehuid + "','" + bianhao + "','" + fk.Text.Trim() + "','" + wt.Text.Trim() + "','" + zz.Text.Trim() + "','" + sc.Text.Trim() + "','" + chanpingid + "','" + yp.Text.Trim() + "','" + sb.Text.Trim() + "','" + quyang.SelectedValue + "','" + sqbianhao.Text.Trim() + "','" + zq.SelectedValue + "','" + bgxs.SelectedValue + "','" + bglq.SelectedValue + "','" + ypcl.SelectedValue + "','" + qita.Text.Trim() + "','" + beizhu.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','正常','" + Session["UserName"].ToString() + "','','','否','','','','','否','','" + kh + "','否','','','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox7.Text + "','','" + cp.Text.Trim() + "','" + guige.Text.Trim() + "','" + DropDownList2.SelectedValue + "','" + TextBox8.Text + "','否','','','" + bumen1 + "','" + TextBox10.Text + "','" + DropDownList3.SelectedValue + "','" + TextBox11.Text + "','" + TextBox12.Text + "','"+TextBox14.Text+"','"+ txt_ENweituo.Text + "','"+txt_addressWei.Text+"','"+txt_ENaddressWei.Text+"','"+txt_ENzhizao.Text+"','"+txt_ENaddresZhizao.Text+"','"+txt_addresZhizao.Text+"','"+txt_ENshengchang.Text+"','"+txt_addersshengchang.Text+"','"+txt_ENaddressshengchang.Text+"','"+txt_ENcpname.Text+"','"+txt_cpzhuce.Text+"','"+txt_cpfujia.Text+"','"+ txt_cpmiaoshu.Text+ "')";
                    //string sql = "insert into Anjianxinxi2 values('" + baojiaid + "','" + kehuid + "','" + bianhao + "','" + fk.Text.Trim() + "','" + wt.Text.Trim() + "','" + zz.Text.Trim() + "','" + sc.Text.Trim() + "','" + chanpingid + "','" + yp.Text.Trim() + "','" + sb.Text.Trim() + "','" + quyang.SelectedValue + "','" + cydanwei.Text.Trim() + "','" + cydidian.Text.Trim() + "','" + cymushu.Text.Trim() + "','" + cyriqi.Text.Trim() + "','" + sqbianhao.Text.Trim() + "','" + zq.SelectedValue + "','" + bgxs.SelectedValue + "','" + bglq.SelectedValue + "','" + ypcl.SelectedValue + "','" + qita.Text.Trim() + "','" + beizhu.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','正常','" + Session["UserName"].ToString() + "','','','否','','','','','否','','" + kh + "','否','','','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox7.Text + "','','" + cp.Text.Trim() + "','" + guige.Text.Trim() + "','" + DropDownList2.SelectedValue + "','" + TextBox8.Text + "','否','','','" + bumen1 + "','" + TextBox9.Text + "','" + TextBox10.Text + "','" + DropDownList3.SelectedValue + "','" + TextBox11.Text + "','" + TextBox12.Text + "','" + TextBox13.Text + "','" + TextBox14.Text + "')";
                    string youx = rwyouxian.SelectedValue;
                    string sql = @"insert into Anjianxinxi2
                         ([baojiaid],[kehuid],[bianhao],[fukuan], weituo, zhizao, shenchan, chanpinbianhao, ypshuliang, shangbiao, songyangfangshi, shenqingbianhao, zhouqi, baogao, lingqu, yangpinchuli, qitayaoqiu, beizhu, fillname, filltime,[state], statename, statetime, remark1, shoulibiaozhi, shouliren, shoulitime, bianhaoone, bianhaotwo, hesuanbiaozhi, hesuantime, hesuanname, shoufeibiaozhi, shoufeitime, shoufeiname, ypname, ypnamey, ypxinghao, ypxinghaoy, syriqi, qyfangshi, shengcriqi, TaskNo, name, xinghao, waibao, wancheng, biaozhi, biaozhiname, biaozhitime, xiangmu, feiyong, cpleixing, a1, a2, a3, a4, Enweituo, addressWei, ENaddressWei, ENzhizao, ENaddresZhizao, addresZhizao, ENshengchang, addersshengchang, ENaddressshengchang, ENcpname, cpzhuce, cpfujia, cpmiaoshu,lianxiren)
                         values('" + baojiaid + "','" + kehuid + "','" + bianhao + "','" + fk.Text.Trim() + "','" + wt.Text.Trim() + "','" + zz.Text.Trim() + "','" + sc.Text.Trim() + "','" + chanpingid + "','" + yp.Text.Trim() + "','" + sb.Text.Trim() + "','" + quyang.SelectedValue + "','" + sqbianhao.Text.Trim() + "','" + zq.SelectedValue + "','" + bgxs.SelectedValue + "','" + bglq.SelectedValue + "','" + ypcl.SelectedValue + "','" + qita.Text.Trim() + "','" + beizhu.Text.Trim() + "','" + Session["Username"].ToString() + "','" + DateTime.Now + "','正常','" + Session["Username"].ToString() + "','','" + rwyouxian.SelectedValue + "','否','','','','','否','','" + kh + "','否','','','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox7.Text + "','','" + cp.Text.Trim() + "','" + guige.Text.Trim() + "','" + DropDownList2.SelectedValue + "','" + TextBox8.Text + "','否','','','" + bumen1 + "','','" + DropDownList3.SelectedValue + "','" + TextBox11.Text + "','" + TextBox12.Text + "','" + TextBox13.Text + "','" + TextBox14.Text + "','" + txt_ENweituo.Text + "','" + txt_addressWei.Text + "','" + txt_ENaddressWei.Text + "','" + txt_ENzhizao.Text + "','" + txt_ENaddresZhizao.Text + "','" + txt_addresZhizao.Text + "','" + txt_ENshengchang.Text + "','" + txt_addersshengchang.Text + "','" + txt_ENaddressshengchang.Text + "','" + txt_ENcpname.Text + "','" + txt_cpzhuce.Text + "','" + txt_cpfujia.Text + "','" + txt_cpmiaoshu.Text + "','" + txt_linaxiren.Text + "')";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();


                    string sql2 = "update baojiabiao set kaianbiaozhi='' where baojiaid='" + baojiaid + "'";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    cmd2.ExecuteNonQuery();

                    string sqlstate = "insert into  TaskState values ('" + bianhao + "','','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','填单','第一次保存')";
                    SqlCommand cmdstate = new SqlCommand(sqlstate, con);
                    cmdstate.ExecuteNonQuery();

                    string sqlrecord = "insert into TiandanRecord values('" + baojiaid + "','" + wt.Text + "','" + txt_addressWei.Text + "','" + txt_ENweituo.Text + "','" + txt_ENaddressWei.Text + "','" + zz.Text + "','" + txt_addresZhizao.Text + "','" + txt_ENzhizao.Text + "','" + txt_ENaddresZhizao.Text + "','" + sc.Text + "','" + txt_addresZhizao.Text + "','" + txt_ENshengchang.Text + "','" + txt_ENaddressshengchang.Text + "')";
                    SqlCommand cmdaddrecord = new SqlCommand(sqlrecord, con);
                    cmdaddrecord.ExecuteNonQuery();
                    con.Close();

                    MyExcutSql my = new MyExcutSql();
                    my.ExtTaskone(bianhao, "", "提交开案", "手工提交", Session["UserName"].ToString(), "提交开案修改了anjianxinxi2的biaozhi字段", DateTime.Now, "受理");

                    Button5.Enabled = false;

                    ld.Text = "<script>alert('保存成功!');</script>";
                    Response.Redirect("AnjianxinxiSee.aspx?id=" + bianhao);
                }
                else
                {
                    //con.Close();
                    ld.Text = "<script>alert('申请编号有重复!');</script>";
                }
                con.Close();
                //}
            }
        }
    }

    public bool ExcutSqlShenQ(string shenqingbianhao, string tablename)
    {
        bool real = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select shenqingbianhao from " + tablename + " where shenqingbianhao !='' and state !='关闭' and state !='中止' and shenqingbianhao='" + shenqingbianhao.Trim() + "'";
        SqlCommand myComm = new SqlCommand(sql, con);
        SqlDataReader dr = myComm.ExecuteReader();
        if (dr.Read())
        {
            real = true;
        }
        else
        {
            real = false;
        }
        con.Close();
        return real;
    }

    protected void cp_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ypcl_SelectedIndexChanged(object sender, EventArgs e)
    {
        ld.Text = "";
        if (ypcl.SelectedItem.Text == "快递/Express(到付)")
        {
            Panel1.Visible = true;
        }
        else
        {
            Panel1.Visible = false;
        }
    }

    protected void drop_CP_SelectedIndexChanged(object sender, EventArgs e)
    {
        CPBind();
    }
    int i = 0;

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

    protected void chk_all_CheckedChanged(object sender, EventArgs e)
    {
        ld.Text = string.Empty;
        if (chk_all.Checked)
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                ((CheckBox)GridView2.Rows[i].FindControl("CheckBox1")).Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                ((CheckBox)GridView2.Rows[i].FindControl("CheckBox1")).Checked = false;
            }
        }
    }
}