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

public partial class Case_TaskIn1 : System.Web.UI.Page
{
    protected string tijiaobianhao = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string responser = "";
    protected string task = "";
    protected string renwu = "";
    protected string sendname = "";
    protected string tasknumber = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Dutyname();
        tijiaobianhao = Request.QueryString["tijiaobianhao"].ToString();
        GridView1.Attributes.Add("style", "table-layout:fixed");
        rwbianhao.Text = tijiaobianhao;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from AnJianXinXi2 where bianhao='" + tijiaobianhao + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            baojiaid = dr["baojiaid"].ToString();
            kehuid = dr["kehuid"].ToString();
            responser = dr["fillname"].ToString();
            tasknumber = dr["TaskNo"].ToString();

            //rwshenqingbianhao.Text = dr["shenqingbianhao"].ToString();


            rwbianhao.Text = dr["taskno"].ToString();
            task = dr["taskno"].ToString();
        }

        TextBox14.Text = baojiaid;

        dr.Close();
        string sql4 = "select * from Customer where kehuid='" + kehuid + "' order by kehuid";
        SqlCommand cmd4 = new SqlCommand(sql4, con);
        SqlDataReader dr4 = cmd4.ExecuteReader();
        if (dr4.Read())
        {
            kh.Text = dr4["CustomName"].ToString();

        }
        dr4.Close();
        con.Close();



        if (!IsPostBack)
        {

            Bindbeizhu();
            BindLinkMan();
            BindGroup1();
            BindDep();
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con2.Open();

            string sqls = "select * from AnJianXinXi2 where bianhao='" + tijiaobianhao + "'";
            SqlCommand cmds = new SqlCommand(sqls, con2);
            SqlDataReader drs = cmds.ExecuteReader();
            if (drs.Read())
            {
                rwshenqingbianhao.Text = drs["shenqingbianhao"].ToString();
                ypshanbiao.Text = drs["shangbiao"].ToString();
                txt_zhouqi.Text = drs["zhouqi"].ToString();
            }
            drs.Close();
            //string sqlhuaxue = "select * from renzheng";
            //SqlDataAdapter ad9 = new SqlDataAdapter(sqlhuaxue, con2);
            //DataSet ds9 = new DataSet();
            //ad9.Fill(ds9);
            //CheckBoxList2.DataSource = ds9.Tables[0];

            //CheckBoxList2.DataTextField = "name";
            //CheckBoxList2.DataValueField = "name"; ;
            //CheckBoxList2.DataBind();




            string sql2 = "select * from anjianinfo2 where bianhao='" + tijiaobianhao + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con2);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {

                rwstate.SelectedValue = dr2["state"].ToString();

                renwu = dr2["renwu"].ToString();

                TextBox16.Text = dr2["responser"].ToString();
                string EMCbiaozhun1 = dr2["shiyanleibie"].ToString();
                string[] EMCbiaozhun2 = EMCbiaozhun1.Split('|');


                rwxiadariqi.Text = dr2["xiadariqi"].ToString();
                rwbaogao.SelectedValue = dr2["baogao"].ToString();
                rwwancheng.Text = dr2["yaoqiuwanchengriqi"].ToString();
                rwyouxian.Text = dr2["youxian"].ToString();
                rwkehu.Text = dr2["kehuyaoqiu"].ToString();
                rwbeizhu.Text = dr2["beizhu"].ToString();
                TextBox1.Text = dr2["keshi"].ToString();
                cp.Text = dr2["name"].ToString();
                guige.Text = dr2["xinghao"].ToString();
                DropDownList6.SelectedValue = dr2["b3"].ToString();

                TextBox4.Text = dr2["fukuandanwei"].ToString();
                TextBox5.Text = dr2["weituodanwei"].ToString();
                TextBox6.Text = dr2["zhizaodanwei"].ToString();
                TextBox7.Text = dr2["shengchandanwei"].ToString();
                cp.Text = dr2["chanpinname"].ToString();
                guige.Text = dr2["xinghaoguige"].ToString();
                ypshuliang.Text = dr2["num"].ToString();
                ypsongjianriqi.Text = dr2["songjiandate"].ToString();
                ypchouyangfangshi.Text = dr2["quyangfangshi"].ToString();
                txt_zhuce.Text = dr2["b5"].ToString();
                txt_fujia.Text = dr2["b6"].ToString();
                //ypchouyangdanwei.Text = dr2["chouyangdanwei"].ToString();
                //ypchouyangdidian.Text = dr2["chouyangdidian"].ToString();
                //ypchouyangriqi.Text = (dr2["chouyangdate"].ToString());
                //ypchouyangmuti.Text = dr2["chouyangmutinum"].ToString();
                ypshengchanriqi.Text = dr2["shengchandate"].ToString();
                ypbeizhu.Text = dr2["remark"].ToString();

                DropDownList3.SelectedValue = dr2["waibao"].ToString();
                //TextBox10.Text = dr2["shencriqi"].ToString();

                TextBox2.Text = dr2["qitayaoqiu"].ToString();
                TextBox8.Text = dr2["yuji"].ToString();
                TextBox6.Text = dr2["zhizaodanwei"].ToString();
                TextBox12.Text = dr2["shixian"].ToString();
                TextBox13.Text = dr2["yaoqiushixian"].ToString();


                //string EMCbiaozhun3 = dr2["keshi"].ToString();
                //string[] EMCbiaozhun33 = EMCbiaozhun3.Split('|');
                //foreach (string str in EMCbiaozhun33)
                //{
                //    for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                //    {
                //        if (this.CheckBoxList1.Items[i].Text == str)
                //        {
                //            this.CheckBoxList1.Items[i].Selected = true;
                //        }
                //    }
                //}



            }

            dr2.Close();
            string sqlg = "select * from anjianinfo where tijiaobianhao='" + tijiaobianhao + "' and type ='是'";
            SqlCommand cmdg = new SqlCommand(sqlg, con2);
            SqlDataReader drg = cmdg.ExecuteReader();
            if (drg.Read())
            {
                DropDownList2.SelectedValue = drg["bumen"].ToString();
            }
            drg.Close();
            dr.Close();
            string sqlgg = "select * from anjianinfo where tijiaobianhao='" + tijiaobianhao + "' and type ='否'";



            SqlDataAdapter adgg = new SqlDataAdapter(sqlgg, con2);
            DataSet dsgg = new DataSet();
            adgg.Fill(dsgg);
            DataTable dtgg = dsgg.Tables[0];
            for (int i = 0; i < dtgg.Rows.Count; i++)
            {
                for (int j = 0; j < CheckBoxList1.Items.Count; j++)
                {
                    if (this.CheckBoxList1.Items[j].Text == dtgg.Rows[i]["bumen"].ToString())
                    {
                        this.CheckBoxList1.Items[j].Selected = true;
                    }
                }
            }

            string sqlb = "select fillname,financebeizhu from baojiabiao where baojiaid='" + baojiaid + "'";
            SqlCommand cmdb = new SqlCommand(sqlb, con2);
            SqlDataReader drb = cmdb.ExecuteReader();
            if (drb.Read())
            {
                Label1.Text = drb["fillname"].ToString();
                txt_financebeizhu.Text = drb["financebeizhu"].ToString(); //财务备注
            }

            con2.Close();

            Bind();

            BindCustomer();


        }


        //if (Request.QueryString["id"] != null)
        //{
        //    sendname = Request.QueryString["id"].ToString();

        //    if (sendname == "xinshouli")
        //    {
        //        Button2.Visible = false;
        //        Button3.Visible = false;
        //        Button4.Visible = false;
        //        Button5.Visible = false;
        //        Button6.Visible = false;

        //    }
        //}
        bool A = false;
        A = limit1("案件受理");
        if (A == true)
        {
            //Button2.Visible = true;
            Button3.Visible = true;
        }
        else
        {
            //Button2.Visible = false;
            Button3.Visible = false;
            Button4.Visible = false;
            Button5.Visible = false;
            Button6.Visible = false;

        }
        if (task == "")
        {
            Button3.Visible = true;
        }
        else
        {
            Button3.Visible = false;
        }


        bool B = false;
        B = limit1("延期处理");
        if (B == true)
        {
            Button9.Enabled = true;
        }
        else
        {
            Button9.Enabled = false;
        }

        if (rwstate.SelectedValue == "完成")
        {
            Button4.Enabled = false;

        }
        if (limit1("修改销售员"))
        {
            //Button10.Visible = true;
        }
        else
        {
            //Button10.Visible = false;
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
    protected void BindBaoJiaBiao()
    {

        string strs = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql21 = "select * from CustomerLinkMan where  customerid='" + kehuid + "' ";
        SqlDataAdapter ad21 = new SqlDataAdapter(sql21, con);
        DataSet ds21 = new DataSet();
        ad21.Fill(ds21);
        DropDownList7.DataSource = ds21.Tables[0];
        DropDownList7.DataTextField = "name";
        DropDownList7.DataValueField = "id";
        DropDownList7.DataBind();

        GridView5.DataSource = ds21.Tables[0];
        GridView5.DataBind();


        if (Request.QueryString["baojiaid"] != null)
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
            DropDownList7.SelectedValue = linkman;

        }

        con.Close();
    }
    public void Bindbeizhu()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from anjianbeizhu where xiangmuid='" + rwbianhao.Text.Trim() + "' order by id desc";
        //string sql = "select * from studentInfo";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        GridView3.DataSource = ds.Tables[0];
        GridView3.DataBind();

    }


    protected void BindLinkMan()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CustomerLinkMan where id =(select top 1 linkid from baojialink where baojiaid='" + baojiaid + "')";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();

        con.Close();
    }

    protected void BindGroup1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo order by username asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        //DropDownList1.DataSource = ds.Tables[0];
        //DropDownList1.DataTextField = "username";
        //DropDownList1.DataValueField = "username";
        //DropDownList1.DataBind();
        con.Close();
    }

    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select * from UserDepa where departmentid='10' or departmentid='11'  or  departmentid='12' or departmentid='13' or departmentid='14' or departmentid='15' or departmentid='1018'";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


        DataSet ds = new DataSet();


        ad.Fill(ds);





        //DropDownList2.DataSource = ds.Tables[0];
        //DropDownList2.DataValueField = "name";
        //DropDownList2.DataTextField = "name";
        //DropDownList2.DataBind();


        CheckBoxList1.DataSource = ds.Tables[0];
        CheckBoxList1.DataValueField = "name";
        CheckBoxList1.DataTextField = "name";
        CheckBoxList1.DataBind();
        con3.Close();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(rwwancheng.Text.Trim()) < Convert.ToDateTime(rwxiadariqi.Text.Trim()))
        {
            ld.Text = "<script>alert('任务完成日期早于任务下达日期!请更改');</script>";
        }
        else
        {
            //int selectednum = 0;
            //for (int i = 0; i < CheckBoxList2.Items.Count; i++)
            //{
            //    if (CheckBoxList2.Items[i].Selected)
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
            //string sqlde12 = "delete from zhujianengineer where baojiaid =(select top 1 id from anjianinfo where tijiaobianhao='" + tijiaobianhao + "') ";
            //SqlCommand cmdde12 = new SqlCommand(sqlde12, con);
            //cmdde12.ExecuteNonQuery();
            string tiaokuan = "";
            string sql4 = "";
            int xuhao = 0;
            for (int i = 1; i < CheckBoxList1.Items.Count + 1; i++)
            {
                if (CheckBoxList1.Items[i - 1].Selected)
                {
                    xuhao++;
                    string xuhaos = xuhao.ToString();
                    string xuhaosh = xuhaos + ",";
                    tiaokuan += CheckBoxList1.Items[i - 1].Text.ToString() + "|";
                    string sqlep = "select * from anjianinfo where bumen='" + CheckBoxList1.Items[i - 1].Text.ToString() + "' and tijiaobianhao='" + tijiaobianhao + "'";
                    SqlCommand cmdep = new SqlCommand(sqlep, con);
                    SqlDataReader drep = cmdep.ExecuteReader();
                    if (drep.Read())
                    {
                        drep.Close();
                    }
                    else
                    {
                        drep.Close();
                        sql4 = "insert into anjianinfo values('" + baojiaid + "','" + kehuid + "','" + rwbianhao.Text.Trim() + "','" + tijiaobianhao + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "','" + CheckBoxList1.Items[i - 1].Text.ToString() + "','否','','','进行中','否','','" + Convert.ToDateTime(rwwancheng.Text.Trim()) + "','','是','','','','" + txt_zhouqi.Text + "')";
                        SqlCommand cmd = new SqlCommand(sql4, con);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string sqlde12 = "delete from zhujianengineer where bumen='" + CheckBoxList1.Items[i - 1].Text + "' and bianhao='" + rwbianhao.Text + "'";
                    SqlCommand cmdde12 = new SqlCommand(sqlde12, con);
                    cmdde12.ExecuteNonQuery();
                    string sqlde1 = "delete from anjianinfo where bumen='" + CheckBoxList1.Items[i - 1].Text + "' and  tijiaobianhao='" + tijiaobianhao + "'";
                    SqlCommand cmdde1 = new SqlCommand(sqlde1, con);
                    cmdde1.ExecuteNonQuery();
                }
            }
            if (tiaokuan.Length > 0)
            {
                tiaokuan = tiaokuan.Substring(0, tiaokuan.LastIndexOf('|'));
            }
            else
            {
                tiaokuan = "0";
            }
            string bumen1 = "";
            //for (int i = 1; i < CheckBoxList2.Items.Count + 1; i++)
            //{
            //    if (CheckBoxList2.Items[i - 1].Selected)
            //    {
            //        bumen1 += CheckBoxList2.Items[i - 1].Text.ToString() + "|";
            //    }
            //}
            //if (bumen1.Length > 0)
            //{
            //    bumen1 = bumen1.Substring(0, bumen1.LastIndexOf('|'));
            //}
            //else
            //{
            bumen1 = "委托";
            //}
            //string sqlinsert4 = "insert into anjianinfo values('" + baojiaid + "','" + kehuid + "','" + rwbianhao.Text + "','" + tijiaobianhao + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "','" + DropDownList2.SelectedValue + "','否','" + DropDownList1.SelectedValue + "','','未开始','是','','','')";
            //SqlCommand cmd4 = new SqlCommand(sqlinsert4, con);
            //cmd4.ExecuteNonQuery();
            tiaokuan = tiaokuan + "|" + DropDownList2.SelectedValue;
            //string sql = "update anjianinfo2 set b3='" + DropDownList6.SelectedValue + "', shenqingbianhao='" + rwshenqingbianhao.Text.Trim() + "',  shixian='" + TextBox12.Text.Trim() + "',kf='" + DropDownList4.SelectedValue + "', feiyong='" + TextBox11.Text + "', shencriqi='" + TextBox10.Text + "', waibao='" + DropDownList3.SelectedValue + "', qitayaoqiu='" + TextBox2.Text + "',yuji='" + TextBox8.Text + "', name='" + cp.Text + "',xinghao='" + guige.Text + "', keshi='" + tiaokuan + "',shiyanleibie='" + bumen1 + "',baogao='" + rwbaogao.SelectedValue + "',beizhu='" + rwbeizhu.Text + "',kehuyaoqiu='" + rwkehu.Text + "',youxian='" + rwyouxian.Text + "',fukuandanwei='" + TextBox4.Text + "',weituodanwei='" + TextBox5.Text + "',zhizaodanwei='" + TextBox6.Text + "',shengchandanwei='" + TextBox7.Text + "',chanpinname='" + cp.Text + "',xinghaoguige='" + guige.Text + "',shangbiao='" + ypshanbiao.Text + "',num='" + ypshuliang.Text + "',songjiandate='" + ypsongjianriqi.Text + "',quyangfangshi='" + ypchouyangfangshi.Text + "',shengchandate='" + ypshengchanriqi.Text + "',remark='" + ypbeizhu.Text + "' where bianhao='" + tijiaobianhao + "' ";
            string sql = "update anjianinfo2 set b3='" + DropDownList6.SelectedValue + "', shenqingbianhao='" + rwshenqingbianhao.Text.Trim() + "',  shixian='" + TextBox12.Text.Trim() + "', waibao='" + DropDownList3.SelectedValue + "', qitayaoqiu='" + TextBox2.Text + "',yuji='" + TextBox8.Text + "', name='" + cp.Text + "',xinghao='" + guige.Text + "', keshi='" + tiaokuan + "',shiyanleibie='" + bumen1 + "',baogao='" + rwbaogao.SelectedValue + "',beizhu='" + rwbeizhu.Text + "',kehuyaoqiu='" + rwkehu.Text + "',youxian='" + rwyouxian.Text + "',fukuandanwei='" + TextBox4.Text + "',weituodanwei='" + TextBox5.Text + "',zhizaodanwei='" + TextBox6.Text + "',shengchandanwei='" + TextBox7.Text + "',chanpinname='" + cp.Text + "',xinghaoguige='" + guige.Text + "',shangbiao='" + ypshanbiao.Text + "',num='" + ypshuliang.Text + "',songjiandate='" + ypsongjianriqi.Text + "',quyangfangshi='" + ypchouyangfangshi.Text + "',shengchandate='" + ypshengchanriqi.Text + "',remark='" + ypbeizhu.Text + "',b5='" + txt_zhuce.Text + "' ,b6='" + txt_fujia.Text + "' where bianhao='" + tijiaobianhao + "' ";
            SqlCommand cmdi = new SqlCommand(sql, con);
            cmdi.ExecuteNonQuery();

            string sqli3 = "update anjianxinxi2 set shenqingbianhao='" + rwshenqingbianhao.Text.Trim() + "' where bianhao='" + tijiaobianhao + "' ";
            SqlCommand cmdi3 = new SqlCommand(sqli3, con);
            cmdi3.ExecuteNonQuery();

            string sqli4 = "update invoice set sqbianhao='" + rwshenqingbianhao.Text.Trim() + "',name1='" + TextBox4.Text.Trim() + "',name2='" + TextBox5.Text.Trim() + "' where rwbh='" + rwbianhao.Text + "' ";
            SqlCommand cmdi4 = new SqlCommand(sqli4, con);
            cmdi4.ExecuteNonQuery();

            //string sqls = "update Anjianxinxi2 set shoulibiaozhi='是',shouliren='" + Session["UserName"].ToString() + "',shoulitime='" + DateTime.Now + "' where bianhao='" + tijiaobianhao + "'";
            //SqlCommand cmdis = new SqlCommand(sqls, con);
            //cmdis.ExecuteNonQuery();

            //string sql6 = "insert into yangpin values('" + baojiaid + "','" + kehuid + "','" + tijiaobianhao + "','" + ypbianhao.Text.Trim() + "','" + ypbianhaoy.Text.Trim() + "','" + ypxinghao.Text.Trim() + "','" + ypxinghaoy.Text.Trim() + "','" + ypshanbiao.Text.Trim() + "','" + ypshuliang.Text.Trim() + "','" + ypchouyangfangshi.Text.Trim() + "','" + ypsongjianriqi.Text.Trim() + "','" + ypbeizhu.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "')";
            string sql6 = "insert into yangpin values('" + baojiaid + "','" + kehuid + "','" + tijiaobianhao + "','" + ypbianhao.Text.Trim() + "','" + ypbianhaoy.Text.Trim() + "','" + ypxinghao.Text.Trim() + "','" + ypxinghaoy.Text.Trim() + "','" + ypshanbiao.Text.Trim() + "','" + ypshuliang.Text.Trim() + "','" + ypchouyangfangshi.Text.Trim() + "','','" + ypsongjianriqi.Text.Trim() + "','" + ypbeizhu.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "')";

            SqlCommand cmd6 = new SqlCommand(sql6, con);
            cmd6.ExecuteNonQuery();

            string sql2 = "update baojiabiao set kaianbiaozhi='是' where baojiaid='" + baojiaid + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.ExecuteNonQuery();

            string sqly = "delete from Anjianxinxi3 where bianhao='" + tijiaobianhao + "'";
            SqlCommand cmdy = new SqlCommand(sqly, con);
            cmdy.ExecuteNonQuery();


            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");

                if (hzf.Checked)
                {
                    string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                    string sqlx = "insert into Anjianxinxi3 values ('" + baojiaid + "','" + kehuid + "','" + tijiaobianhao + "','','" + sid + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";
                    SqlCommand cmdx = new SqlCommand(sqlx, con);
                    cmdx.ExecuteNonQuery();
                }
            }

            string sqlstate = "insert into  TaskState values ('" + tijiaobianhao + "','','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','修改任务','客服修改任务anjianinfo2')";
            SqlCommand cmdstate = new SqlCommand(sqlstate, con);
            cmdstate.ExecuteNonQuery();

            string sqlinvoice = "update invoice set sqbianhao='" + rwshenqingbianhao.Text.Trim() + "' where rwbh='" + rwbianhao.Text.Trim() + "'";
            SqlCommand cmdinvoice = new SqlCommand(sqlinvoice, con);
            cmdinvoice.ExecuteNonQuery();

            string sqlbaogao = "update baogao2 set dengjiby='" + rwshenqingbianhao.Text.Trim() + "' where tjid='" + rwbianhao.Text.Trim() + "'";
            SqlCommand cmdbaogao = new SqlCommand(sqlbaogao, con);
            cmdbaogao.ExecuteNonQuery();


            string sqlup = "update taskstate set dotimetianxie='" + Convert.ToDateTime(rwxiadariqi.Text.Trim()) + "' where bianhao='" + tijiaobianhao + "' and type1='受理任务'";
            SqlCommand cmdup = new SqlCommand(sqlup, con);
            cmdup.ExecuteNonQuery();
            con.Close();
            UpdateCeShiFeiKf();

            ld.Text = "<script>alert('保存成功!');window.close();</script>";
            //}
        }

    }
    protected void Bind()
    {
        ld.Text = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        //sql = "select * from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "'";
        //string sql = "select *  from BaoJiaCPXiangMu  where id in (select xiangmubianhao from anjianxinxi3 where bianhao='" + tijiaobianhao + "') order  by id desc";
        string sql = "select *  from BaoJiaCPXiangMu  where id in (select xiangmubianhao from anjianxinxi3 where bianhao='" + tijiaobianhao + "') order  by id desc";


        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
            string sqlx = "select * from anjianxinxi3 where xiangmubianhao='" + sid + "' and bianhao='" + tijiaobianhao + "'";
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





    protected void BindCustomer()
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();
        //string sql2 = "select *,(select customname from customer where kehuid=anjianxinxi2.kehuid) as kehuname  from AnJianxinxi2  where bianhao='" + tijiaobianhao + "' order  by id desc";

        //SqlDataAdapter da2 = new SqlDataAdapter(sql2, con);
        //DataSet ds2 = new DataSet();
        //da2.Fill(ds2);

        //GridView4.DataSource = ds2.Tables[0];
        //GridView4.DataBind();





        //string sql = "select * from CustomerLinkMan where baojiaid='" + baojiaid + "'";
        //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //ad.Fill(ds);

        //GridView5.DataSource = ds.Tables[0];
        //GridView5.DataBind();

        //con.Close();
        //con.Dispose();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();
        //string sql2 = "delete from yangpin where bianhao='"+tijiaobianhao+"'";
        //SqlCommand cmd2 = new SqlCommand(sql2, con);
        //cmd2.ExecuteNonQuery();


        //string sql = "insert into yangpin values('"+baojiaid+"','"+kehuid+"','"+tijiaobianhao+"','" + ypbianhao.Text.Trim() + "','" + ypbianhaoy.Text.Trim() + "','" + ypxinghao.Text.Trim() + "','" + ypxinghaoy.Text.Trim() + "','" + ypshanbiao.Text.Trim() + "','" + ypshuliang.Text.Trim() + "','" + ypchouyangfangshi.Text.Trim() + "','" + ypchouyangdidian.Text.Trim() + "','" + ypchouyangdanwei.Text.Trim() + "','" + ypchouyangriqi.Text.Trim() + "','" + ypchouyangmuti.Text.Trim() + "','" + ypchouyangriqi.Text.Trim() + "','" + ypsongjianriqi.Text.Trim() + "','" + ypbeizhu.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "')";
        //SqlCommand cmd = new SqlCommand(sql,con);
        //cmd.ExecuteNonQuery();
        //con.Close();
        //ld.Text = "<script>alert('保存成功!');</script>";
        //BindYangPin();

    }


    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo where department='" + DropDownList2.SelectedValue + "' order by username asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataTextField = "username";
        DropDownList1.DataValueField = "username";
        DropDownList1.DataBind();
        con.Close();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {

        //string dd = taskid();
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();

        //string sqls = "update Anjianxinxi2 set shoulibiaozhi='是',taskno='" + dd + "',shouliren='" + Session["UserName"].ToString() + "',shoulitime='" + DateTime.Now + "' where bianhao='" + tijiaobianhao + "'";
        //SqlCommand cmdis = new SqlCommand(sqls, con);
        //cmdis.ExecuteNonQuery();



        //string sqli = "update Anjianinfo2 set rwbianhao='" + dd + "' where bianhao='" + tijiaobianhao + "'";
        //SqlCommand cmdii = new SqlCommand(sqli, con);
        //cmdii.ExecuteNonQuery();



        //string sql2 = "update baojiabiao set kaianbiaozhi='是' where baojiaid='" + baojiaid + "'";
        //SqlCommand cmd2 = new SqlCommand(sql2, con);
        //cmd2.ExecuteNonQuery();


        //string sqlinsert4 = "insert into anjianinfo values('" + baojiaid + "','" + kehuid + "','" + dd + "','" + tijiaobianhao + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "','" + DropDownList2.SelectedValue + "','否','" + DropDownList1.SelectedValue + "','','未开始','是','','','')";
        //SqlCommand cmd4 = new SqlCommand(sqlinsert4, con);
        //cmd4.ExecuteNonQuery();

        //string sql5 = "insert  into BaoJiaCPXiangMu2 select * from BaoJiaCPXiangMu  where id in (select xiangmubianhao from anjianxinxi3 where bianhao='" + tijiaobianhao + "')";
        //SqlCommand cmd5 = new SqlCommand(sql5,con);
        //cmd5.ExecuteNonQuery();


        //string sql1 = "update anjianinfo set taskid='"+dd+"' where tijiaobianhao='"+tijiaobianhao+"'";
        //SqlCommand cmd1 = new SqlCommand(sql1,con);
        //cmd1.ExecuteNonQuery();


        //string sqlstate = "insert into  TaskState values ('" + tijiaobianhao + "','"+dd+"','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','受理任务','客服受理任务生成案件号')";
        //SqlCommand cmdstate = new SqlCommand(sqlstate, con);
        //cmdstate.ExecuteNonQuery();


        //con.Close();

        //ld.Text = "<script>alert('保存成功!');window.close();</script>";

        // Button3.Enabled = false;

    }


    protected string taskid()
    {
        //string task = "";


        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();
        //try
        //{
        //    string sql1 = "select taskno from anjianxinxi2 where taskno !='' order by taskno asc";
        //    string id = "";
        //    SqlDataAdapter adpter = new SqlDataAdapter(sql1, con);
        //    DataSet ds = new DataSet();
        //    adpter.Fill(ds);
        //    string date1 = "";
        //    string day = "";
        //    string houzhui = "";
        //    string two = "";
        //    string yue = "";
        //    string tian = "";
        //    string yue1 = "";
        //    string tian1 = "";
        //    if (ds.Tables[0].Rows.Count == 0)
        //    {

        //        task = DateTime.Now.Year.ToString().Substring(2) + "-" + "00001";
        //    }
        //    else
        //    {
        //        houzhui = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["taskno"].ToString();

        //        tian = houzhui.Substring(3, 5);


        //        int a = Convert.ToInt32(tian) + 1;
        //        task = DateTime.Now.Year.ToString().Substring(2) + "-" + String.Format("{0:D5}", a);



        //    }
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message);
        //}
        //con.Close();

        //return task;
        //****************2019-8-2修改
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string str;
            con.Open();
            string bj = baojiaid;
            string area = bj.Substring(0, 2);
            string sql = "select top 1 taskno from anjianxinxi2 where taskno !='' order by taskno desc";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                str = "P" + area + DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + "000001";
            }
            else
            {
                string taskno = ds.Tables[0].Rows[0][0].ToString();
                string yue = taskno.Substring(5, 2);
                string year = taskno.Substring(3, 2);
                if (yue == DateTime.Now.Month.ToString().PadLeft(2, '0') && year == DateTime.Now.Year.ToString().Substring(2, 2))
                {
                    string num = taskno.Substring(7, 6);
                    int i = Convert.ToInt32(num);
                    i++;
                    str = "P" + area + DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + i.ToString().PadLeft(6, '0');
                }
                else
                {
                    str = "P" + area + DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + "0000001";
                }
            }
            return str;
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Bind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            int id = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
            //如果该项目已被业务认领则不能删改
            string sql_claim = "select * from Claim where ceshifeikfid in (select id from CeShiFeiKf where xmid='" + id + "')";
            SqlCommand com_claim = new SqlCommand(sql_claim, con);
            SqlDataReader dr_claim = com_claim.ExecuteReader();
            if (dr_claim.Read())
            {
                dr_claim.Close();
                ld.Text = "<script>alert('该项目业务已认领、不能进行删改操作。');</script>";
            }
            else
            {
                dr_claim.Close();
                this.GridView1.EditIndex = e.NewEditIndex;
                Bind();
            }
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string money = Server.HtmlEncode(((TextBox)GridView1.Rows[e.RowIndex].Cells[7].Controls[0]).Text.ToString());
        string epiboly_price = Server.HtmlEncode(((TextBox)GridView1.Rows[e.RowIndex].Cells[11].Controls[0]).Text.ToString());
        decimal dmoney = 0m;
        decimal depiboly_price = 0m;
        try
        {
            dmoney = Convert.ToDecimal(money);
            depiboly_price = Convert.ToDecimal(epiboly_price);
            if (dmoney < depiboly_price)
            {
                ld.Text = "<script>alert('外包价格不能超过项目总价格、请重新输入。');</script>";
            }
            else
            {
                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                string xmname = Server.HtmlEncode(((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
                string biaozhun = Server.HtmlEncode(((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
                string yp = Server.HtmlEncode(((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
                string zhouqi = Server.HtmlEncode(((TextBox)GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text.ToString());
                string beizhu = Server.HtmlEncode(((TextBox)GridView1.Rows[e.RowIndex].Cells[12].Controls[0]).Text.ToString());
                string bumen = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[13].FindControl("funtion")).SelectedValue;
                string isepiboly = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[11].FindControl("epiboly")).SelectedValue;

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
                {
                    con.Open();
                    string sql = "update BaoJiaCPXiangMu set ceshiname='" + xmname + "',biaozhun='" + biaozhun + "',beizhu='" + beizhu + "',yp='" + yp + "',zhouqi='" + zhouqi + "',bumen='" + bumen + "',feiyong='" + dmoney + "',epiboly='" + isepiboly + "',epiboly_Price='" + depiboly_price + "' where id='" + id + "'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    GridView1.EditIndex = -1;


                    //修改Itembaogao和projectitem表（保证这两个表的项目名称与baojiacpxiangmu一致）
                    string sqlitembaogao = "update ItemBaogao set xmname='" + xmname + "' where xmid='" + id + "'";
                    SqlCommand cmditembaogao = new SqlCommand(sqlitembaogao, con);
                    cmditembaogao.ExecuteNonQuery();

                    string sqlprojectitem = "update ProjectItem set xmname='" + xmname + "' where xmid='" + id + "'";
                    SqlCommand cmdprojectitem = new SqlCommand(sqlprojectitem, con);
                    cmdprojectitem.ExecuteNonQuery();
                }
                UPdataBaojia();
                Bind();
            }
        }
        catch (Exception)
        {
            ld.Text = "<script>alert('金额输入不合法、请重新输入。');</script>";
        }
    }



    protected void Button4_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sqli = "update Anjianinfo2 set renwu='是',state='进行中' ,xiadariqi='" + rwxiadariqi.Text.Trim() + "' where bianhao='" + tijiaobianhao + "'";
        SqlCommand cmdii = new SqlCommand(sqli, con);
        cmdii.ExecuteNonQuery();

        DateTime d1 = DateTime.Now;

        if (rwxiadariqi.Text.Trim() == "")
        {

        }
        else
        {
            d1 = Convert.ToDateTime(rwxiadariqi.Text.Trim());
        }
        string sqlstate = "insert into  TaskState values ('" + tijiaobianhao + "','" + task + "','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + d1 + "','下达任务','客服下达任务到科室')";
        SqlCommand cmdstate = new SqlCommand(sqlstate, con);
        cmdstate.ExecuteNonQuery();

        //if (CheckBoxList2.SelectedValue == "EMC")
        //{
        //    string sqlemc = "update anjianinfo2 set state='完成' ,beizhu3='" + DateTime.Now.ToShortDateString() + "' where bianhao='" + tijiaobianhao + "'";
        //    SqlCommand cmdemc = new SqlCommand(sqlemc, con);
        //    cmdemc.ExecuteNonQuery();
        //}
        con.Close();

        searchwhere sx4 = new searchwhere();
        string sjt1 = sx4.ShiXiao3(task);

        ld.Text = "<script>alert('保存成功!');window.close();</script>";
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Print/TaskBiaoQian.aspx?taskid=" + task);
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sqli = "update Anjianinfo2 set wancheng='" + DateTime.Now + "' where bianhao='" + tijiaobianhao + "'";
        SqlCommand cmdii = new SqlCommand(sqli, con);
        cmdii.ExecuteNonQuery();

        con.Close();

        ld.Text = "<script>alert('保存成功!');window.close();</script>";
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Print/QuoPrint.aspx?baojiaid=" + baojiaid + "&&kehuid=" + kehuid);
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();


            string sql = "insert into anjianbeizhu values('" + rwbianhao.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + TextBox15.Text + "','')";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            GridView2.DataSource = dr;
            GridView2.DataBind();


            con.Close();
            con.Dispose();
            Bindbeizhu();
            TextBox15.Text = "";
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        searchwhere sr = new searchwhere();
        TextBox13.Text = sr.tian1(Convert.ToDateTime(rwxiadariqi.Text.Trim()), Convert.ToDateTime(rwwancheng.Text.Trim())).ToString();



        string sql = "update anjianinfo2 set  yaoqiushixian='" + TextBox13.Text.Trim() + "', shixian='" + TextBox12.Text.Trim() + "',yaoqiuwanchengriqi='" + rwwancheng.Text + "' where bianhao='" + tijiaobianhao + "' ";
        SqlCommand cmdi = new SqlCommand(sql, con);
        cmdi.ExecuteNonQuery();
        con.Close();

        searchwhere sx4 = new searchwhere();
        string sjt1 = sx4.ShiXiao3(task);




        ld.Text = "<script>alert('修改成功!');window.close();</script>";

    }
    protected void Button10_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        if (TextBox16.Text.Trim() != "" && limit1("修改销售员"))
        {
            string sqlresponser = "update anjianinfo2 set responser='" + TextBox16.Text + "' where bianhao='" + tijiaobianhao + "' ";
            SqlCommand cmdresponser = new SqlCommand(sqlresponser, con);
            cmdresponser.ExecuteNonQuery();

            string sql2 = "update baojiabiao set responser ='" + TextBox16.Text + "' where baojiaid in (select baojiaid from anjianinfo2 where bianhao='" + tijiaobianhao + "')";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.ExecuteNonQuery();
        }
        con.Close();
        ld.Text = "<script>alert('修改成功!');window.close();</script>";
    }
    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {
        rwkehu.Text = rwkehu.Text + ",邮寄联系人：" + DropDownList7.SelectedValue;
    }
    /// <summary>
    /// 判断登录进来人的职位
    /// </summary>
    protected void Dutyname()
    {
        string dn = "";
        string dutyname = "";
        using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con1.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con1);
            SqlDataReader dr = cmdstate.ExecuteReader();
            if (dr.Read())
            {
                dutyname = dr["dutyname"].ToString();
            }
            dr.Close();
            if (dutyname.Trim() == "系统管理员")
            {
                this.Button10.Visible = true;
                this.Button2.Visible = true;
            }
            else if (dutyname.Trim() == "客户经理")
            {
                this.Button10.Visible = true;
                this.Button2.Visible = true;
            }
            else if (dutyname.Trim() == "客服经理" || dutyname.Trim() == "客服人员" || dutyname.Trim() == "销售助理")
            {
                this.Button2.Visible = true;
            }
        }
    }

    protected void btn_copy_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gr.Cells[1].FindControl("chk_copy");
                if (chk.Checked)
                {
                    string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                    string sql = @" insert into BaoJiaCPXiangMu(baojiaid,kehuid,cpid,ceshiname,biaozhun,neirong,yp,feiyong,zhekou,shuliang,beizhu,fillname,filltime,responser,zhouqi,tijiaobiaozhi,tijiaotime,jishuyaoqiu,daid,zhongid,xiaoid,yuanshi,hesuanbiaozhi,hesuantime,hesuanname,epiboly,jigou
                                    ,bumen,epiboly_Price) select baojiaid,kehuid,cpid,ceshiname,biaozhun,neirong,yp,feiyong,zhekou,shuliang,beizhu,'" + Session["Username"].ToString() + "','" + DateTime.Now + "',responser,zhouqi,tijiaobiaozhi,tijiaotime,jishuyaoqiu,daid,zhongid,xiaoid,yuanshi,hesuanbiaozhi,hesuantime,hesuanname,epiboly,jigou,bumen,epiboly_Price from BaoJiaCPXiangMu where id='" + sid + "'";
                    SqlCommand cmdx = new SqlCommand(sql, con);
                    cmdx.ExecuteNonQuery();

                    string lastid = "";
                    string sql_lastid = "select top 1 id from BaoJiaCPXiangMu order by id desc";
                    SqlCommand com_lastid = new SqlCommand(sql_lastid, con);
                    SqlDataReader dr_lastid = com_lastid.ExecuteReader();
                    if (dr_lastid.Read())
                    {
                        lastid = dr_lastid["id"].ToString();
                        dr_lastid.Close();
                        string anjianxinxiid = "";
                        string sql_anjianxinxi3 = "select id from anjianxinxi3  where xiangmubianhao = '" + sid + "' and bianhao='" + tijiaobianhao + "'";
                        SqlCommand com_anjianxinxi3 = new SqlCommand(sql_anjianxinxi3, con);
                        SqlDataReader dr_anjianid = com_anjianxinxi3.ExecuteReader();
                        if (dr_anjianid.Read())
                        {
                            anjianxinxiid = dr_anjianid["id"].ToString();
                            dr_anjianid.Close();
                            string sql_add = @"insert into anjianxinxi3(baojiaid,kehuid,bianhao,cpbianhao,xiangmubianhao,fillname,filltime) 
                                        select baojiaid,kehuid,bianhao,cpbianhao,'" + lastid + "',fillname,filltime from anjianxinxi3 where id='" + anjianxinxiid + "'";
                            SqlCommand com_add = new SqlCommand(sql_add, con);
                            int i = com_add.ExecuteNonQuery();
                        }
                        dr_anjianid.Close();
                    }
                    dr_lastid.Close();
                }
            }
            UPdataBaojia();
            Bind();
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string xmname = GridView1.Rows[e.RowIndex].Cells[3].Text.ToString();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            //如果该项目已被业务认领则不能删改
            string sql_claim = "select * from Claim where ceshifeikfid in (select id from CeShiFeiKf where xmid='" + id + "')";
            SqlCommand com_claim = new SqlCommand(sql_claim, con);
            SqlDataReader dr_claim = com_claim.ExecuteReader();
            if (dr_claim.Read())
            {
                dr_claim.Close();
                ld.Text = "<script>alert('该项目业务已认领、不能进行删改操作。');</script>";
            }
            else
            {
                dr_claim.Close();
                string sql_delete = "select * from CeShiFeiKf where xmid='" + id + "'";
                SqlCommand cmd_delete = new SqlCommand(sql_delete, con);
                cmd_delete.ExecuteNonQuery();

                string sql_xm = "delete BaoJiaCPXiangMu where id='" + id + "'";
                SqlCommand com_xm = new SqlCommand(sql_xm, con);
                com_xm.ExecuteNonQuery();

                string sql_an = " delete AnjianXinXi3 where xiangmubianhao='" + id + "'";
                SqlCommand com_an = new SqlCommand(sql_an, con);
                com_an.ExecuteNonQuery();

                //保存删除记录信息
                string sql_add = "insert Deleterecord values('" + baojiaid + "','" + rwbianhao.Text + "','" + TextBox5.Text + "','" + xmname + "','" + Session["Username"].ToString() + "','" + DateTime.Now.ToString() + "')";
                SqlCommand cmd_add = new SqlCommand(sql_add, con);
                cmd_add.ExecuteNonQuery();

                UPdataBaojia();

                Bind();
            }
        }
    }

    /// <summary>
    /// 修改核算费用
    /// </summary>
    protected void UpdateCeShiFeiKf()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_baojiabiao = "select * from baojiabiao where BaoJiaId='" + baojiaid + "'";
            SqlCommand cmd_baojiabiao = new SqlCommand(sql_baojiabiao, con);
            SqlDataReader dr_baojiabiao = cmd_baojiabiao.ExecuteReader();
            string currency = "";
            string isvat = "";
            string coupon = "";
            decimal baojiaprice = 0m;

            if (dr_baojiabiao.Read())
            {
                currency = dr_baojiabiao["currency"].ToString();
                isvat = dr_baojiabiao["isVAT"].ToString();
                coupon = dr_baojiabiao["coupon"].ToString();
                baojiaprice = Convert.ToDecimal(dr_baojiabiao["zhehoujia"]);
            }
            dr_baojiabiao.Close();
            foreach (GridViewRow item in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)item.Cells[0].FindControl("CheckBox1");
                if (chk.Checked)
                {
                    string xmid = GridView1.DataKeys[item.RowIndex].Value.ToString();
                    string xmname = item.Cells[3].Text.ToString();
                    string price = item.Cells[7].Text.ToString();
                    string sumprice = item.Cells[9].Text.ToString();
                    string epiboly_price = item.Cells[11].Text.ToString();
                    string bumen = ((Label)item.Cells[13].FindControl("Label2")).Text.ToString();
                    string epiboly = ((Label)item.Cells[10].FindControl("Label3")).Text.ToString();
                    string tijiaohaoma = item.Cells[15].Text.ToString();

                    string sql_ceshifeikf = "select bianhaoone,xmid from CeShiFeiKf where bianhaoone='" + tijiaohaoma + "'";
                    SqlDataAdapter da_ceshifeikf = new SqlDataAdapter(sql_ceshifeikf, con);
                    DataSet ds_ceshifeikf = new DataSet();
                    da_ceshifeikf.Fill(ds_ceshifeikf);

                    if (ds_ceshifeikf.Tables[0].Rows.Count > 0)
                    {
                        //该项目已经过自动核算 
                        if (epiboly == "外包")
                        {
                            decimal interior = Calculate(coupon, isvat, sumprice, baojiaprice);
                            //当把内部项目改为外包时、ceshifeikf表里没有规费记录
                            string sql_hsjc = "select id from CeShiFeiKf where bianhaoone='" + ds_ceshifeikf.Tables[0].Rows[0]["bianhaoone"].ToString() + "' and project='规费'";
                            SqlCommand cmd_hsjc = new SqlCommand(sql_hsjc, con);
                            SqlDataReader dr_hsjc = cmd_hsjc.ExecuteReader();
                            if (dr_hsjc.Read())
                            {
                                string bianhaoone = dr_hsjc["id"].ToString();
                                dr_hsjc.Close();
                                string sql_edit = "update CeShiFeiKf set feiyong='-" + Convert.ToDecimal(epiboly_price) + "',baojia='-" + Convert.ToDecimal(epiboly_price) + "',type='" + xmname + "',beizhu3='" + bumen + "',project='规费' where id='" + bianhaoone + "'";
                                SqlCommand cmd_edit = new SqlCommand(sql_edit, con);
                                cmd_edit.ExecuteNonQuery();
                            }
                            else
                            {
                                dr_hsjc.Close();
                                //新增规费
                                string sql_addepiboly = "insert CeShiFeiKf values('" + tijiaobianhao + "','" + kehuid + "','" + baojiaid + "','-" + Convert.ToDecimal(epiboly_price) + "','" + xmname + "','','','" + bumen + "','自动核算','" + DateTime.Now + "','" + ds_ceshifeikf.Tables[0].Rows[0]["bianhaoone"].ToString() + "','','','否','','','1','-" + Convert.ToDecimal(epiboly_price) + "','0','" + task + "','','规费','" + xmid + "','" + currency + "','否')";
                                SqlCommand cmd_addpiboly = new SqlCommand(sql_addepiboly, con);
                                cmd_addpiboly.ExecuteNonQuery();
                            }

                            //修改检测费
                            string sql_hsjc1 = "select id from CeShiFeiKf where bianhaoone='" + ds_ceshifeikf.Tables[0].Rows[0]["bianhaoone"].ToString() + "' and project='检测费'";
                            SqlCommand cmd_hsjc1 = new SqlCommand(sql_hsjc1, con);
                            SqlDataReader dr_hsjc1 = cmd_hsjc1.ExecuteReader();
                            if (dr_hsjc1.Read())
                            {
                                string bianhaoone = dr_hsjc1["id"].ToString();
                                dr_hsjc1.Close();
                                string sql_edit = "update CeShiFeiKf set feiyong='" + interior + "',baojia='" + interior + "',type='" + xmname + "',beizhu3='" + bumen + "',project='检测费' where id='" + bianhaoone + "'";
                                SqlCommand cmd_edit = new SqlCommand(sql_edit, con);
                                cmd_edit.ExecuteNonQuery();
                            }
                            dr_hsjc1.Close();
                        }
                        else
                        {
                            decimal interior = Calculate(coupon, isvat, sumprice, baojiaprice);
                            //删除规费记录
                            string sql_hs = "select id from CeShiFeiKf where bianhaoone='" + ds_ceshifeikf.Tables[0].Rows[0]["bianhaoone"].ToString() + "' and project='规费'";
                            SqlDataAdapter da_hs = new SqlDataAdapter(sql_hs, con);
                            DataSet ds_hs = new DataSet();
                            da_hs.Fill(ds_hs);
                            if (ds_hs.Tables[0].Rows.Count > 0)
                            {
                                string ceshi_id = ds_hs.Tables[0].Rows[0][0].ToString();
                                string sql_delete = "delete CeShiFeiKf where id='" + ceshi_id + "'";
                                SqlCommand cmd_delete = new SqlCommand(sql_delete, con);
                                cmd_delete.ExecuteNonQuery();
                            }

                            //修改检测费
                            string sql_hs1 = "select id from CeShiFeiKf where bianhaoone='" + ds_ceshifeikf.Tables[0].Rows[0]["bianhaoone"].ToString() + "' and project='检测费'";
                            SqlCommand cmd_hs1 = new SqlCommand(sql_hs1, con);
                            SqlDataReader dr_sh1 = cmd_hs1.ExecuteReader();
                            if (dr_sh1.Read())
                            {
                                string id = dr_sh1["id"].ToString();
                                dr_sh1.Close();
                                string sql_edit = "update CeShiFeiKf set feiyong='" + interior + "',baojia='" + interior + "',type='" + xmname + "',beizhu3='" + bumen + "',project='检测费' where id='" + id + "'";
                                SqlCommand cmd_eidt = new SqlCommand(sql_edit, con);
                                cmd_eidt.ExecuteNonQuery();
                            }
                            dr_sh1.Close();
                        }
                    }
                    else
                    {
                        //在编辑时新增的测试项目
                        if (epiboly == "外包")
                        {
                            Random seed = new Random();
                            Random randomNum = new Random(seed.Next());
                            string shoufeiid = randomNum.Next().ToString() + DateTime.Now.ToString("yyyyMMdd_hhmmss");

                            decimal interior = Calculate(coupon, isvat, sumprice, baojiaprice);

                            //存在外包项目时需要在ceshifeikf表中插入两条数据
                            string sql_addepiboly = "insert CeShiFeiKf values('" + tijiaobianhao + "','" + kehuid + "','" + baojiaid + "','-" + epiboly_price + "','" + xmname + "','','','" + bumen + "','自动核算','" + DateTime.Now + "','" + shoufeiid + "','','','否','','','1','-" + epiboly_price + "','0','" + tasknumber + "','','规费','" + xmid + "','" + currency + "','否')";
                            SqlCommand cmd_addepiboly = new SqlCommand(sql_addepiboly, con);
                            cmd_addepiboly.ExecuteNonQuery();

                            string sqlup1 = "update BaoJiaCPXiangMu set tijiaohaoma='" + shoufeiid + "',hesuanbiaozhi='是' where id='" + xmid + "'";
                            SqlCommand cmdup1 = new SqlCommand(sqlup1, con);
                            cmdup1.ExecuteNonQuery();

                            string sql_addinterior = "insert CeShiFeiKf values('" + tijiaobianhao + "','" + kehuid + "','" + baojiaid + "','" + interior.ToString("f2") + "','" + xmname + "','','','" + bumen + "','自动核算','" + DateTime.Now + "','" + shoufeiid + "','','','否','','','1','" + interior.ToString("f2") + "','0','" + tasknumber + "','','检测费','" + xmid + "','" + currency + "','否')";
                            SqlCommand cmd_addinterior = new SqlCommand(sql_addinterior, con);
                            cmd_addinterior.ExecuteNonQuery();
                        }
                        else
                        {
                            Random seed = new Random();
                            Random randomNum = new Random(seed.Next());
                            string shoufeiid = randomNum.Next().ToString() + DateTime.Now.ToString("yyyyMMdd_hhmmss");
                            //内部费用
                            decimal interior = Calculate(coupon, isvat, sumprice, baojiaprice);

                            string sql_addepiboly = "insert CeShiFeiKf values('" + tijiaobianhao + "','" + kehuid + "','" + baojiaid + "','" + interior.ToString("f2") + "','" + xmname + "','','','" + bumen + "','自动核算','" + DateTime.Now + "','" + shoufeiid + "','','','否','','','1','" + interior.ToString("f2") + "','0','" + tasknumber + "','','检测费','" + xmid + "','" + currency + "','否')";
                            SqlCommand cmd_addepiboly = new SqlCommand(sql_addepiboly, con);
                            cmd_addepiboly.ExecuteNonQuery();

                            string sqlup2 = "update BaoJiaCPXiangMu set tijiaohaoma='" + shoufeiid + "',hesuanbiaozhi='是' where id='" + xmid + "'";
                            SqlCommand cmdup2 = new SqlCommand(sqlup2, con);
                            cmdup2.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }

    protected decimal Total()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_total = "select * from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql_total, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            decimal sum = 0m;
            decimal price = 0m;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                price = Convert.ToDecimal(ds.Tables[0].Rows[i]["total"]);
                sum += price;
            }
            return sum;
        }
    }

    protected decimal Total_sum()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_total = "select * from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql_total, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            decimal sum = 0m;
            decimal price = 0m;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                price = Convert.ToDecimal(ds.Tables[0].Rows[i]["total"]);
                sum += price;
            }
            return sum;
        }
    }

    /// <summary>
    /// 修改报价表的实际价格、外包费用、折扣
    /// </summary>
    private void UPdataBaojia()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select SUM(total) as shijizong,SUM(shuliang*yuanshi) as yuanshizong,SUM(epiboly_price) as waibaozong,epiboly from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' group by epiboly";
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            //汇率
            string sql_currency = "select currency,coupon,kuozhanfei from BaoJiaBiao where BaoJiaId='" + baojiaid + "'";
            SqlCommand cmd_currency = new SqlCommand(sql_currency, con);
            SqlDataReader dr_currency = cmd_currency.ExecuteReader();
            string currency = "";//币种
            string finalprice = "";//优惠后金额
            decimal kuozhanfei = 0m;//扩展费
            if (dr_currency.Read())
            {
                currency = dr_currency["currency"].ToString();
                finalprice = dr_currency["coupon"].ToString();
                if (dr_currency["kuozhanfei"].ToString() == "&nbsp;" || dr_currency["kuozhanfei"] == null || string.IsNullOrEmpty(dr_currency["kuozhanfei"].ToString()) || dr_currency["kuozhanfei"].ToString() == "0" || dr_currency["kuozhanfei"].ToString() == "0.00")
                {

                }
                else
                {
                    kuozhanfei = Convert.ToDecimal(dr_currency["kuozhanfei"]);
                }
            }
            dr_currency.Close();

            decimal exchange = 0m;
            if (currency == "美元")
            {
                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString();
                string sql_date = "select top 1 huilv from Exchange where [year]='" + year + "' and [month]='" + month + "' order by id desc";
                SqlCommand cmd_date = new SqlCommand(sql_date, con);
                SqlDataReader dr_date = cmd_date.ExecuteReader();
                if (dr_date.Read())
                {
                    exchange = Convert.ToDecimal(dr_date["huilv"].ToString());
                    dr_date.Close();
                }
                else
                {
                    dr_date.Close();
                    string sql_huilv = "select top 1 huilv from Exchange order by id desc";
                    SqlCommand cmd_huilv = new SqlCommand(sql_huilv, con);
                    SqlDataReader dr_huilv = cmd_huilv.ExecuteReader();
                    if (dr_huilv.Read())
                    {
                        exchange = Convert.ToDecimal(dr_huilv["huilv"].ToString());
                    }
                    dr_huilv.Close();
                }
            }

            decimal feiwaibaozong = 0m;
            decimal hanwaibaozong = 0m;
            decimal waibaobufen = 0m;
            decimal biaozhun = 0m;//标准总价
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string rowtype = ds.Tables[0].Rows[i]["epiboly"].ToString();
                    if (rowtype == "外包")
                    {
                        hanwaibaozong += Convert.ToDecimal(ds.Tables[0].Rows[i]["shijizong"].ToString());
                        biaozhun += Convert.ToDecimal(ds.Tables[0].Rows[i]["yuanshizong"].ToString());
                        waibaobufen += Convert.ToDecimal(ds.Tables[0].Rows[i]["waibaozong"].ToString());
                    }
                    else
                    {
                        feiwaibaozong += Convert.ToDecimal(ds.Tables[0].Rows[i]["shijizong"].ToString());
                        biaozhun += Convert.ToDecimal(ds.Tables[0].Rows[i]["yuanshizong"].ToString());
                    }
                }
            }

            decimal zhengdanzhekou = 0m;
            decimal totalmoney = hanwaibaozong + feiwaibaozong;
            if (biaozhun == 0m || string.IsNullOrEmpty(biaozhun.ToString("f2")))
            {
                //无项目 
                zhengdanzhekou = 1m;
            }
            else if (!string.IsNullOrEmpty(finalprice) && finalprice != "0.00" && finalprice != "&nbsp;" && finalprice.ToLower() != "null")
            {
                //存在优惠后金额
                if (kuozhanfei > 0)
                {
                    //存在扩展费
                    if (currency == "美元")
                    {
                        decimal fenmu = biaozhun / exchange;
                        zhengdanzhekou = (Convert.ToDecimal(finalprice) - kuozhanfei) / fenmu;
                    }
                    else
                    {
                        zhengdanzhekou = (Convert.ToDecimal(finalprice) - kuozhanfei) / biaozhun;
                    }
                }
                else
                {
                    if (currency == "美元")
                    {
                        decimal fenmu = biaozhun / exchange;
                        zhengdanzhekou = Convert.ToDecimal(finalprice) / fenmu;
                    }
                    else
                    {
                        zhengdanzhekou = Convert.ToDecimal(finalprice) / biaozhun;
                    }
                }
            }
            else
            {
                if (kuozhanfei > 0)
                {
                    if (currency == "美元")
                    {
                        decimal fenmu = biaozhun / exchange;
                        zhengdanzhekou = (totalmoney - kuozhanfei) / fenmu;
                    }
                    else
                    {
                        zhengdanzhekou = (totalmoney - kuozhanfei) / biaozhun;
                    }
                }
                else
                {
                    if (currency == "美元")
                    {
                        decimal fenmu = biaozhun / exchange;
                        zhengdanzhekou = totalmoney / fenmu;
                    }
                    else
                    {
                        zhengdanzhekou = totalmoney / biaozhun;
                    }
                }
            }
            string sql_edit = "update BaoJiaBiao set Discount=" + zhengdanzhekou.ToString("f2") + ", realdiscount=" + zhengdanzhekou.ToString("f2") + ",zhehoujia=" + totalmoney + ",epiboly_Price=" + waibaobufen + " where BaoJiaId='" + baojiaid + "'";
            SqlCommand cmd_edit = new SqlCommand(sql_edit, con);
            cmd_edit.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// 计算内部金额
    /// </summary>
    /// <returns></returns>
    public decimal Calculate(string coupon, string isvat, string sumprice, decimal baojiaprice)
    {
        decimal interior = 0m;
        if (isvat == "增值税")
        {
            decimal taxes = baojiaprice * 0.06m;//税金
            decimal total = Total();
            interior = Convert.ToDecimal(sumprice) + taxes * (Convert.ToDecimal(sumprice) / total);
        }
        else
        {
            interior = Convert.ToDecimal(sumprice);
        }

        #region   2020-05-07注释
        //if (string.IsNullOrEmpty(coupon) || coupon == "0.00" || coupon == "&nbsp;")//无优惠后金额
        //{
        //    if (isvat == "增值税")
        //    {
        //        decimal taxes = baojiaprice * 0.06m;//税金
        //        decimal total = Total();
        //        interior = Convert.ToDecimal(sumprice) + taxes * (Convert.ToDecimal(sumprice) / total);
        //    }
        //    else
        //    {
        //        interior = Convert.ToDecimal(sumprice);
        //    }
        //}
        //else
        //{
        //    //有优惠后金额
        //    if (isvat == "增值税")
        //    {
        //        decimal discounts = 0m;//优惠金额
        //        decimal total = Total();
        //        decimal taxes = Convert.ToDecimal(coupon) * 0.06m;
        //        if (Convert.ToDecimal(coupon) > baojiaprice)
        //        {
        //            discounts = 0m;
        //        }
        //        else
        //        {
        //            discounts = baojiaprice - Convert.ToDecimal(coupon);
        //        }
        //        decimal A = Convert.ToDecimal(sumprice) - discounts * (Convert.ToDecimal(sumprice) / total);
        //        interior = A + taxes * (Convert.ToDecimal(sumprice) / total);
        //    }
        //    else
        //    {
        //        decimal discounts = 0m;
        //        decimal total = Total();
        //        if (Convert.ToDecimal(coupon) > baojiaprice)
        //        {
        //            discounts = 0m;
        //        }
        //        else
        //        {
        //            discounts = baojiaprice - Convert.ToDecimal(coupon);
        //        }
        //        interior = Convert.ToDecimal(sumprice) - discounts * (Convert.ToDecimal(sumprice) / total);
        //    }
        //}
        #endregion

        return interior;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                //((LinkButton)e.Row.Cells[14].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除吗?')");
                DataControlFieldCell f = e.Row.Cells[14] as DataControlFieldCell;
                for (int i = 0; i < f.Controls.Count; i++)
                {
                    LinkButton b = f.Controls[i] as LinkButton;
                    if (b != null && b.Text == "删除")
                    {
                        b.OnClientClick = "return confirm('你真的要删除吗？')";
                    }
                }
            }
        }
    }
}