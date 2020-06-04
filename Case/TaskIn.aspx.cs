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
using System.Text.RegularExpressions;
using System.Collections.Generic;

public partial class Case_TaskIn : System.Web.UI.Page
{

    protected string tijiaobianhao = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string responser = "";
    protected string st = "在检";
    protected string providername = "";
    protected string statename = "";
    protected string tasknumber = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        tijiaobianhao = Request.QueryString["tijiaobianhao"].ToString();

        rwbianhao.Text = tijiaobianhao;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sqlinfo = "select * from anjianinfo2 where bianhao='" + tijiaobianhao + "'";
        SqlCommand cmdinfo = new SqlCommand(sqlinfo, con);
        SqlDataReader drinfo = cmdinfo.ExecuteReader();
        if (drinfo.Read())
        {
            drinfo.Close();
            con.Close();
            Response.Redirect("TaskIn1.aspx?tijiaobianhao=" + tijiaobianhao);
        }
        else
        {

            drinfo.Close();

            string sql3 = "select * from AnJianXinXi2 where bianhao='" + tijiaobianhao + "'";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            if (dr3.Read())
            {
                baojiaid = dr3["baojiaid"].ToString();
                kehuid = dr3["kehuid"].ToString();
                responser = dr3["fillname"].ToString();
            }
            dr3.Close();


            if (!IsPostBack)
            {




                string sql = "select * from AnJianXinXi2 where bianhao='" + tijiaobianhao + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    rwkehu.Text = dr["hesuanname"].ToString();
                    rwshenqingbianhao.Text = dr["shenqingbianhao"].ToString();

                    ypshuliang.Text = dr["ypshuliang"].ToString();
                    ypshanbiao.Text = dr["shangbiao"].ToString();
                    //ypchouyangdanwei.Text = dr["chouyangdanwei"].ToString();
                    //ypchouyangdidian.Text = dr["chouyangdidian"].ToString();
                    //ypchouyangmuti.Text = dr["chouyangshu"].ToString();
                    //ypchouyangriqi.Text = dr["chouyangriqi"].ToString();
                    txt_zhouqi.Text = dr["zhouqi"].ToString();
                    rwyouxian.SelectedValue = dr["remark1"].ToString();

                    rwwancheng.Text = dr["wancheng"].ToString();

                    ypbianhao.Text = dr["ypname"].ToString();
                    ypbianhaoy.Text = dr["ypnamey"].ToString();
                    ypxinghao.Text = dr["ypxinghao"].ToString();
                    ypxinghaoy.Text = dr["ypxinghaoy"].ToString();
                    ypsongjianriqi.Text = dr["syriqi"].ToString();
                    ypchouyangfangshi.Text = dr["songyangfangshi"].ToString();
                    ypshengchanriqi.Text = dr["shengcriqi"].ToString();
                    cp.Text = dr["name"].ToString();
                    guige.Text = dr["xinghao"].ToString();
                    TextBox4.Text = dr["fukuan"].ToString();
                    TextBox5.Text = dr["weituo"].ToString();
                    TextBox6.Text = dr["zhizao"].ToString();
                    TextBox7.Text = dr["shenchan"].ToString();
                    TextBox1.Text = dr["qitayaoqiu"].ToString();
                    TextBox2.Text = dr["wancheng"].ToString();
                    //TextBox8.Text = dr["shencriqi"].ToString();
                    //rwkehu.Text = dr["fukuan"].ToString();
                    ypbeizhu.Text = dr["beizhu"].ToString();
                    TextBox9.Text = dr["feiyong"].ToString();
                    DropDownList3.SelectedValue = dr["waibao"].ToString();
                    DropDownList5.SelectedValue = dr["cpleixing"].ToString();
                    txt_zhuce.Text = dr["cpzhuce"].ToString();
                    txt_fujia.Text = dr["cpfujia"].ToString();
                    statename = dr["fillname"].ToString();
                    string EMCbiaozhun1 = dr["xiangmu"].ToString();
                    string[] EMCbiaozhun2 = EMCbiaozhun1.Split('|');

                }
                dr.Close();
                string sql4 = "select * from Customer where kehuid='" + kehuid + "' order by kehuid";
                SqlCommand cmd4 = new SqlCommand(sql4, con);
                SqlDataReader dr4 = cmd4.ExecuteReader();
                if (dr4.Read())
                {
                    kh.Text = dr4["CustomName"].ToString();
                    providername = dr4["providername"].ToString();
                }
                dr4.Close();
                con.Close();
                rwxiadariqi.Text = DateTime.Now.ToString("yyyy-MM-dd");

                searchwhere sr = new searchwhere();

                TextBox11.Text = sr.tian1(Convert.ToDateTime(rwxiadariqi.Text.Trim()), Convert.ToDateTime(rwwancheng.Text.Trim())).ToString();

                BindGroup1();
                BindDep();
                Bind();
                BindBaoJiaBiao();




            }

            con.Close();
        }
        con.Close();
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
        DropDownList6.DataSource = ds21.Tables[0];
        DropDownList6.DataTextField = "name";
        DropDownList6.DataValueField = "id";
        DropDownList6.DataBind();

        GridView5.DataSource = ds21.Tables[0];
        GridView5.DataBind();

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

        //财务备注
        string sql_finance = " select financebeizhu from BaoJiaBiao where BaoJiaId='" + baojiaid + "'";
        SqlCommand cmd_finance = new SqlCommand(sql_finance, con);
        SqlDataReader dr_finance = cmd_finance.ExecuteReader();
        if (dr_finance.Read())
        {
            txt_financebeizhu.Text = dr_finance["financebeizhu"].ToString();
        }

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
        string sql = "select * from UserDepa where departmentid='10' or departmentid='11'  or  departmentid='12' or departmentid='13' or departmentid='14' or departmentid='15' or departmentid='1018' ";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


        DataSet ds = new DataSet();


        ad.Fill(ds);


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

            int selectednum1 = 0;
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    selectednum1++;
                }
            }

            if (selectednum1 == 0)
            {
                ld.Text = "<script>alert('请选择下达部门!');</script>";
            }
            else
            {

                lock (this)
                {

                    string dd = Taskid();
                    searchwhere sr = new searchwhere();
                    TextBox11.Text = sr.tian1(Convert.ToDateTime(rwxiadariqi.Text.Trim()), Convert.ToDateTime(rwwancheng.Text.Trim())).ToString();

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                    con.Open();

                    string sqlde1 = "delete from anjianinfo where tijiaobianhao='" + tijiaobianhao + "'";
                    SqlCommand cmdde1 = new SqlCommand(sqlde1, con);
                    cmdde1.ExecuteNonQuery();

                    string sqlde2 = "delete from anjianinfo2 where bianhao='" + tijiaobianhao + "'";
                    SqlCommand cmdde2 = new SqlCommand(sqlde2, con);
                    cmdde2.ExecuteNonQuery();

                    string sql4 = "";
                    int xuhao = 0;
                    string tiaokuan = "";
                    for (int i = 1; i < CheckBoxList1.Items.Count + 1; i++)
                    {
                        if (CheckBoxList1.Items[i - 1].Selected)
                        {
                            xuhao++;

                            string xuhaos = xuhao.ToString();
                            string xuhaosh = xuhaos + ",";

                            tiaokuan += CheckBoxList1.Items[i - 1].Text.ToString() + "|";


                            sql4 = "insert into anjianinfo values('" + baojiaid + "','" + kehuid + "','" + dd + "','" + tijiaobianhao + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "','" + CheckBoxList1.Items[i - 1].Text.ToString() + "','否','','','未开始','否','','" + Convert.ToDateTime(rwwancheng.Text.Trim()) + "','','是','','','','" + txt_zhouqi.Text + "')";


                            SqlCommand cmd = new SqlCommand(sql4, con);
                            cmd.ExecuteNonQuery();
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



                    //string sqlinsert4 = "insert into anjianinfo values('" + baojiaid + "','" + kehuid + "','" + tijiaobianhao + "','" + tijiaobianhao + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "','" + DropDownList2.SelectedValue + "','否','" + DropDownList1.SelectedValue + "','','未开始','是','','','')";
                    //SqlCommand cmd4 = new SqlCommand(sqlinsert4, con);
                    //cmd4.ExecuteNonQuery();


                    string rand = "";
                    string rand1 = "";
                    Hashtable ta = new Hashtable();

                    string sqlran = "select top 1 b4 from anjianinfo2 order by id desc";
                    SqlCommand cmdrand = new SqlCommand(sqlran, con);
                    SqlDataReader drrand = cmdrand.ExecuteReader();
                    while (drrand.Read())
                    {
                        ta.Add(drrand["b4"].ToString(), drrand["b4"].ToString());
                    }
                    drrand.Close();

                    Random rd = new Random();

                    for (int i = 0; i < ta.Count; i++)
                    {
                        rand = rd.Next(10000000, 99999999).ToString();
                        if (!ta.Contains(rand))
                        {
                            rand1 = rand;
                        }
                    }




                    string kh = rwkehu.Text.Trim();


                    tiaokuan = tiaokuan + "|" + DropDownList2.SelectedValue;
                    //string sql = "insert into anjianinfo2 values('" + baojiaid + "','" + kehuid + "','" + responser + "','" + rwbianhao.Text.Trim() + "','" + tijiaobianhao + "','" + rwstate.SelectedValue + "','" + rwshenqingbianhao.Text.Trim() + "','" + bumen1 + "','" + rwxiadariqi.Text.Trim() + "','" + rwwancheng.Text.Trim() + "','" + rwbaogao.SelectedValue + "','" + kh + "','" + rwyouxian.SelectedValue + "','" + rwbeizhu.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + tiaokuan + "','" + cp.Text + "','" + guige.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "','" + cp.Text + "','" + guige.Text + "','" + ypshanbiao.Text + "','" + ypshuliang.Text + "','" + ypsongjianriqi.Text + "','" + ypchouyangfangshi.Text + "','" + ypshengchanriqi.Text + "','" + ypbeizhu.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + DropDownList3.SelectedValue + "','" + TextBox8.Text + "','" + TextBox9.Text + "','" + DropDownList4.SelectedValue + "','否','" + TextBox10.Text.Trim() + "','','" + TextBox11.Text.Trim() + "','资料开始','','','','','','','','1','','" + DropDownList5.SelectedValue + "','" + rand1 + "','','','','')";
                    string sql = "insert into anjianinfo2 values('" + baojiaid + "','" + kehuid + "','" + responser + "','" + rwbianhao.Text.Trim() + "','" + tijiaobianhao + "','" + rwstate.SelectedValue + "','" + rwshenqingbianhao.Text.Trim() + "','" + bumen1 + "','" + rwxiadariqi.Text.Trim() + "','" + rwwancheng.Text.Trim() + "','" + rwbaogao.SelectedValue + "','" + kh + "','" + rwyouxian.SelectedValue + "','" + rwbeizhu.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + tiaokuan + "','" + cp.Text + "','" + guige.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "','" + cp.Text + "','" + guige.Text + "','" + ypshanbiao.Text + "','" + ypshuliang.Text + "','" + ypsongjianriqi.Text + "','" + ypchouyangfangshi.Text + "','" + ypshengchanriqi.Text + "','" + ypbeizhu.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + DropDownList3.SelectedValue + "','" + TextBox9.Text + "','','否','" + TextBox10.Text.Trim() + "','','" + TextBox11.Text.Trim() + "','资料开始','','','','','','','','1','','" + DropDownList5.SelectedValue + "','" + rand1 + "','','','','')";

                    SqlCommand cmdi = new SqlCommand(sql, con);
                    cmdi.ExecuteNonQuery();

                    //string sqls = "update Anjianxinxi2 set shoulibiaozhi='是',taskno='"+taskid()+"',shouliren='"+Session["UserName"].ToString()+"',shoulitime='"+DateTime.Now+"' where bianhao='"+tijiaobianhao+"'";
                    //SqlCommand cmdis = new SqlCommand(sqls, con);
                    //cmdis.ExecuteNonQuery();

                    string sql6 = "insert into yangpin values('" + baojiaid + "','" + kehuid + "','" + tijiaobianhao + "','" + ypbianhao.Text.Trim() + "','" + ypbianhaoy.Text.Trim() + "','" + ypxinghao.Text.Trim() + "','" + ypxinghaoy.Text.Trim() + "','" + ypshanbiao.Text.Trim() + "','" + ypshuliang.Text.Trim() + "','" + ypchouyangfangshi.Text.Trim() + "','" + ypsongjianriqi.Text.Trim() + "','" + ypbeizhu.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "')";
                    SqlCommand cmd6 = new SqlCommand(sql6, con);
                    cmd6.ExecuteNonQuery();

                    //string sql2 = "update baojiabiao set kaianbiaozhi='是' where baojiaid='" + baojiaid + "'";
                    //SqlCommand cmd2 = new SqlCommand(sql2, con);
                    //cmd2.ExecuteNonQuery();
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
                    string sqlstate = "insert into  TaskState values ('" + tijiaobianhao + "','','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','保存任务','客服保存任务anjianinfo2')";
                    SqlCommand cmdstate = new SqlCommand(sqlstate, con);
                    cmdstate.ExecuteNonQuery();
                    //受理
                    {
                        string sqls = "update Anjianxinxi2 set shoulibiaozhi='是',taskno='" + dd + "',shouliren='" + Session["UserName"].ToString() + "',shoulitime='" + DateTime.Now + "' where bianhao='" + tijiaobianhao + "'";
                        SqlCommand cmdis = new SqlCommand(sqls, con);
                        cmdis.ExecuteNonQuery();



                        string sqli = "update Anjianinfo2 set rwbianhao='" + dd + "' where bianhao='" + tijiaobianhao + "'";
                        SqlCommand cmdii = new SqlCommand(sqli, con);
                        cmdii.ExecuteNonQuery();



                        string sql2 = "update baojiabiao set kaianbiaozhi='是' where baojiaid='" + baojiaid + "'";
                        SqlCommand cmd2 = new SqlCommand(sql2, con);
                        cmd2.ExecuteNonQuery();


                        //string sqlinsert41 = "insert into anjianinfo values('" + baojiaid + "','" + kehuid + "','" + dd + "','" + tijiaobianhao + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "','" + DropDownList2.SelectedValue + "','否','" + DropDownList1.SelectedValue + "','','未开始','是','','','')";
                        //SqlCommand cmd41 = new SqlCommand(sqlinsert41, con);
                        //cmd41.ExecuteNonQuery();

                        string sql5 = "insert  into BaoJiaCPXiangMu2 select * from BaoJiaCPXiangMu  where id in (select xiangmubianhao from anjianxinxi3 where bianhao='" + tijiaobianhao + "')";
                        SqlCommand cmd5 = new SqlCommand(sql5, con);
                        cmd5.ExecuteNonQuery();


                        //string sql1 = "update anjianinfo set taskid='" + dd + "' where tijiaobianhao='" + tijiaobianhao + "'";
                        //SqlCommand cmd1 = new SqlCommand(sql1, con);
                        //cmd1.ExecuteNonQuery();


                        string sqlstate1 = "insert into  TaskState values ('" + tijiaobianhao + "','" + dd + "','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + Convert.ToDateTime(rwxiadariqi.Text.Trim()) + "','" + DateTime.Now + "','受理任务','客服受理任务生成案件号')";
                        SqlCommand cmdstate1 = new SqlCommand(sqlstate1, con);
                        cmdstate1.ExecuteNonQuery();



                    }

                    //if (CheckBoxList2.SelectedValue == "EMC")
                    //{
                    //    string sqlemc = "update anjianinfo2 set state='完成',beizhu3='" + DateTime.Now.ToShortDateString() + "' where bianhao='" + tijiaobianhao + "'";
                    //    SqlCommand cmdemc = new SqlCommand(sqlemc, con);
                    //    cmdemc.ExecuteNonQuery();
                    //}

                    con.Close();


                    MyExcutSql my = new MyExcutSql();
                    my.ExtTaskone(tijiaobianhao, dd, "受理", "手工提交", Session["UserName"].ToString(), "受理开案声称了anjianinfo2的记录", Convert.ToDateTime(rwxiadariqi.Text.Trim()), "下达");

                    searchwhere sx4 = new searchwhere();
                    string sjt1 = sx4.ShiXiao3(dd);

                    ld.Text = "<script>alert('受理仅保存成功!');</script>";
                    Button3.Visible = false;
                }
            }
            //}
        }

    }

    protected string Taskid()
    {
        //****************2019-8-2修改
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string str;
            con.Open();
            string bj = baojiaid;
            string area = bj.Substring(0, 2);
            string sql = "select top 1 rwbianhao from AnJianInFo2 where rwbianhao !='' and rwbianhao not like 'D%' order by id desc";
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
                    str = "P" + area + DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + "000001";
                }
            }
            //递归判断任务号是否已存在
            string strid = SoldTaskid(str);
            return strid;
        }
    }

    public string SoldTaskid(string taskid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = " select rwbianhao from AnJianInFo2 where rwbianhao !='' and rwbianhao not like 'D%' and rwbianhao='" + taskid + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string str = taskid.Substring(7, 6);
                int number = int.Parse(str);
                number++;
                string area = dr["rwbianhao"].ToString().Substring(0, 7);
                string strid = area + number.ToString().PadLeft(6, '0');
                return SoldTaskid(strid);
            }
            else
            {
                return taskid;
            }
        }
    }

    protected void Bind()
    {
        Literal1.Text = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *  from BaoJiaCPXiangMu  where id in (select xiangmubianhao from anjianxinxi3 where bianhao='" + tijiaobianhao + "') order  by id desc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
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

            Cktrue();
        }
        con.Close();
    }

    /// <summary>
    /// //把已有的部门默认选中
    /// </summary>
    protected void Cktrue()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select bumen  from BaoJiaCPXiangMu  where id in (select xiangmubianhao from anjianxinxi3 where bianhao='" + tijiaobianhao + "') order  by id desc";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<string> list = new List<string>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(ds.Tables[0].Rows[i]["bumen"].ToString());
            }

            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (list.Contains(CheckBoxList1.Items[i].Text))
                {
                    CheckBoxList1.Items[i].Selected = true;
                }
            }

        }
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



    protected void ypchouyangmuti_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(rwwancheng.Text.Trim()) < Convert.ToDateTime(rwxiadariqi.Text.Trim()))
        {
            ld.Text = "<script>alert('任务完成日期早于任务下达日期!请更改');</script>";
        }
        else
        {
            int selectednum1 = 0;
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    selectednum1++;
                }
            }
            if (selectednum1 == 0)
            {
                ld.Text = "<script>alert('请选择下达部门!');</script>";
            }
            else
            {
                string dd = Taskid();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                searchwhere sr = new searchwhere();
                TextBox11.Text = sr.tian1(Convert.ToDateTime(rwxiadariqi.Text.Trim()), Convert.ToDateTime(rwwancheng.Text.Trim())).ToString();

                string sqlde1 = "delete from anjianinfo where tijiaobianhao='" + tijiaobianhao + "'";
                SqlCommand cmdde1 = new SqlCommand(sqlde1, con);
                cmdde1.ExecuteNonQuery();

                string sqlde2 = "delete from anjianinfo2 where bianhao='" + tijiaobianhao + "'";
                SqlCommand cmdde2 = new SqlCommand(sqlde2, con);
                cmdde2.ExecuteNonQuery();

                string sql4 = "";
                int xuhao = 0;
                string tiaokuan = "";
                for (int i = 1; i < CheckBoxList1.Items.Count + 1; i++)
                {
                    if (CheckBoxList1.Items[i - 1].Selected)
                    {
                        xuhao++;

                        string xuhaos = xuhao.ToString();
                        string xuhaosh = xuhaos + ",";

                        tiaokuan += CheckBoxList1.Items[i - 1].Text.ToString() + "|";


                        sql4 = "insert into anjianinfo values('" + baojiaid + "','" + kehuid + "','" + dd + "','" + tijiaobianhao + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "','" + CheckBoxList1.Items[i - 1].Text.ToString() + "','否','','','进行中','否','','" + Convert.ToDateTime(rwwancheng.Text.Trim()) + "','','是','','','','" + txt_zhouqi.Text + "')";


                        SqlCommand cmd = new SqlCommand(sql4, con);
                        cmd.ExecuteNonQuery();
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


                string rand = "";
                string rand1 = "";
                Hashtable ta = new Hashtable();

                string sqlran = "select top 1 b4 from anjianinfo2 order by id desc";
                SqlCommand cmdrand = new SqlCommand(sqlran, con);
                SqlDataReader drrand = cmdrand.ExecuteReader();
                while (drrand.Read())
                {
                    ta.Add(drrand["b4"].ToString(), drrand["b4"].ToString());
                }
                drrand.Close();

                Random rd = new Random();

                for (int i = 0; i < ta.Count; i++)
                {
                    rand = rd.Next(10000000, 99999999).ToString();
                    if (!ta.Contains(rand))
                    {
                        rand1 = rand;
                    }
                }

                string kh = rwkehu.Text.Trim();

                tiaokuan = tiaokuan + "|" + DropDownList2.SelectedValue;
                //DropDownList4  曾经的客服人员
                //string sql = "insert into anjianinfo2 values('" + baojiaid + "','" + kehuid + "','" + responser + "','" + rwbianhao.Text.Trim() + "','" + tijiaobianhao + "','" + rwstate.SelectedValue + "','" + rwshenqingbianhao.Text.Trim() + "','" + bumen1 + "','" + rwxiadariqi.Text.Trim() + "','" + rwwancheng.Text.Trim() + "','" + rwbaogao.SelectedValue + "','" + kh + "','" + rwyouxian.SelectedValue + "','" + rwbeizhu.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + tiaokuan + "','" + cp.Text + "','" + guige.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "','" + cp.Text + "','" + guige.Text + "','" + ypshanbiao.Text + "','" + ypshuliang.Text + "','" + ypsongjianriqi.Text + "','" + ypchouyangfangshi.Text + "','" + ypshengchanriqi.Text + "','" + ypbeizhu.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + DropDownList3.SelectedValue + "','" + TextBox8.Text + "','" + TextBox9.Text + "','" + DropDownList4.SelectedValue + "','否','" + TextBox10.Text.Trim() + "','','" + TextBox11.Text.Trim() + "','资料开始','','','','','','','','1','','" + DropDownList5.SelectedValue + "','" + rand1 + "','','','','')";
                string sql = "insert into anjianinfo2 values('" + baojiaid + "','" + kehuid + "','" + responser + "','" + rwbianhao.Text.Trim() + "','" + dd + "','" + rwstate.SelectedValue + "','" + rwshenqingbianhao.Text.Trim() + "','" + bumen1 + "','" + rwxiadariqi.Text.Trim() + "','" + rwwancheng.Text.Trim() + "','" + rwbaogao.SelectedValue + "','" + kh + "','" + rwyouxian.SelectedValue + "','" + rwbeizhu.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + tiaokuan + "','" + cp.Text + "','" + guige.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "','" + cp.Text + "','" + guige.Text + "','" + ypshanbiao.Text + "','" + ypshuliang.Text + "','" + ypsongjianriqi.Text + "','" + ypchouyangfangshi.Text + "','" + ypshengchanriqi.Text + "','" + ypbeizhu.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + DropDownList3.SelectedValue + "','" + TextBox9.Text + "','','否','" + TextBox10.Text.Trim() + "','','" + TextBox11.Text.Trim() + "','资料开始','','','','','','','','1','','" + DropDownList5.SelectedValue + "','" + rand1 + "','" + txt_zhuce.Text + "','" + txt_fujia.Text + "','','')";

                SqlCommand cmdi = new SqlCommand(sql, con);
                cmdi.ExecuteNonQuery();



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
                string sqlstate = "insert into  TaskState values ('" + tijiaobianhao + "','','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','保存任务','客服保存任务anjianinfo2')";
                SqlCommand cmdstate = new SqlCommand(sqlstate, con);
                cmdstate.ExecuteNonQuery();
                //受理
                {
                    string sqls = "update Anjianxinxi2 set shoulibiaozhi='是',taskno='" + dd + "',shouliren='" + Session["UserName"].ToString() + "',shoulitime='" + DateTime.Now + "' where bianhao='" + tijiaobianhao + "'";
                    SqlCommand cmdis = new SqlCommand(sqls, con);
                    cmdis.ExecuteNonQuery();
                    //string sqli = "update Anjianinfo2 set rwbianhao='" + dd + "' where bianhao='" + tijiaobianhao + "'";
                    //SqlCommand cmdii = new SqlCommand(sqli, con);
                    //cmdii.ExecuteNonQuery();
                    string sql2 = "update baojiabiao set kaianbiaozhi='是' where baojiaid='" + baojiaid + "'";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    cmd2.ExecuteNonQuery();
                    string sql5 = "insert  into BaoJiaCPXiangMu2 select * from BaoJiaCPXiangMu  where id in (select xiangmubianhao from anjianxinxi3 where bianhao='" + tijiaobianhao + "')";
                    SqlCommand cmd5 = new SqlCommand(sql5, con);
                    cmd5.ExecuteNonQuery();
                    string sqlstate1 = "insert into  TaskState values ('" + tijiaobianhao + "','" + dd + "','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + Convert.ToDateTime(rwxiadariqi.Text.Trim()) + "','" + DateTime.Now + "','受理任务','客服受理任务生成案件号')";
                    SqlCommand cmdstate1 = new SqlCommand(sqlstate1, con);
                    cmdstate1.ExecuteNonQuery();
                }
                con.Close();
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con1.Open();

                MyExcutSql my = new MyExcutSql();
                my.ExtTaskone(tijiaobianhao, dd, "受理", "手工提交", Session["UserName"].ToString(), "受理开案生成了anjianinfo2的记录", Convert.ToDateTime(rwxiadariqi.Text.Trim()), "下达");

                string sqli2 = "update Anjianinfo2 set renwu='是',state='进行中' ,xiadariqi='" + rwxiadariqi.Text.Trim() + "' where bianhao='" + tijiaobianhao + "'";
                SqlCommand cmdii2 = new SqlCommand(sqli2, con1);
                cmdii2.ExecuteNonQuery();

                DateTime d1 = DateTime.Now;

                if (rwxiadariqi.Text.Trim() == "")
                {

                }
                else
                {
                    d1 = Convert.ToDateTime(rwxiadariqi.Text.Trim());
                }
                string sqlstate2 = "insert into  TaskState values ('" + tijiaobianhao + "','" + tijiaobianhao + "','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + d1 + "','下达任务','客服下达任务到科室')";
                SqlCommand cmdstate2 = new SqlCommand(sqlstate2, con1);
                cmdstate2.ExecuteNonQuery();
                con1.Close();

                MyExcutSql my1 = new MyExcutSql();
                my1.ExtTaskone(tijiaobianhao, dd, "进行中", "手工提交", Session["UserName"].ToString(), "受理开案生成了anjianinfo2的记录并修改了记录至下达", Convert.ToDateTime(rwxiadariqi.Text.Trim()), st);

                searchwhere sx41 = new searchwhere();
                string sjt1 = sx41.ShiXiao3(dd);
                tasknumber = dd;
                CeShiFeiKf();//核算费用

                ld.Text = "<script>alert('受理并下达成功!');</script>";
            }
        }
    }

    /// <summary>
    /// 核算费用
    /// </summary>
    private void CeShiFeiKf()
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
            decimal kuozhanfei = 0m;

            if (dr_baojiabiao.Read())
            {
                currency = dr_baojiabiao["currency"].ToString();
                isvat = dr_baojiabiao["isVAT"].ToString();
                coupon = dr_baojiabiao["coupon"].ToString();
                baojiaprice = Convert.ToDecimal(dr_baojiabiao["zhehoujia"]);//报价单金额
                if (string.IsNullOrEmpty(dr_baojiabiao["kuozhanfei"].ToString()) || dr_baojiabiao["kuozhanfei"].ToString() == "0.00" || dr_baojiabiao["kuozhanfei"].ToString() == "&nbsp;" || dr_baojiabiao["kuozhanfei"] == DBNull.Value)
                {

                }
                else
                {
                    kuozhanfei = Convert.ToDecimal(dr_baojiabiao["kuozhanfei"].ToString());
                }
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
                    string sumprice = item.Cells[9].Text.ToString();//项目总金额
                    string epiboly_price = item.Cells[11].Text.ToString();
                    string bumen = ((Label)item.Cells[13].FindControl("Label2")).Text.ToString();
                    string epiboly = ((Label)item.Cells[10].FindControl("Label3")).Text.ToString();
                    //一个项目只能核算一次
                    string sql_count = "select * from CeShiFeiKf where xmid='" + xmid + "' and project='检测费'";
                    SqlCommand cmd_count = new SqlCommand(sql_count, con);
                    SqlDataReader dr_count = cmd_count.ExecuteReader();
                    if (dr_count.Read())
                    {
                        dr_count.Close();
                    }
                    else
                    {
                        dr_count.Close();
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

    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if(rwkehu.Text)
        //rwkehu.Text = rwkehu.Text + ",邮寄联系人：" + DropDownList6.SelectedItem.Text;
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //取消更新
        GridView1.EditIndex = -1;
        Bind();
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
                Literal1.Text = "<script>alert('外包价格不能超过项目总价格、请重新输入。');</script>";
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
                    SqlCommand cmdprojectitem = new SqlCommand(sqlprojectitem,con);
                    cmdprojectitem.ExecuteNonQuery();

                    Bind();
                }
                UPdataBaojia();
            }
        }
        catch (Exception)
        {
            Literal1.Text = "<script>alert('金额输入不合法、请重新输入。');</script>";
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        Bind();
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
                                    ,bumen,epiboly_Price) select baojiaid,kehuid,cpid,ceshiname,biaozhun,neirong,yp,feiyong,zhekou,shuliang,beizhu,fillname,filltime,responser,zhouqi,tijiaobiaozhi,tijiaotime,jishuyaoqiu,daid,zhongid,xiaoid,yuanshi,hesuanbiaozhi,hesuantime,hesuanname,epiboly,jigou
                                    ,bumen,epiboly_Price from BaoJiaCPXiangMu where id='" + sid + "'";
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
                            com_add.ExecuteNonQuery();
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
            string sql_currency = "select currency,coupon from BaoJiaBiao where BaoJiaId='" + baojiaid + "'";
            SqlCommand cmd_currency = new SqlCommand(sql_currency, con);
            SqlDataReader dr_currency = cmd_currency.ExecuteReader();
            string currency = "";//币种
            string finalprice = "";//优惠后金额
            if (dr_currency.Read())
            {
                currency = dr_currency["currency"].ToString();
                finalprice = dr_currency["coupon"].ToString();
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
            if (!string.IsNullOrEmpty(finalprice) && finalprice != "0.00" && finalprice != "&nbsp;" && finalprice.ToLower() != "null")
            {
                if (biaozhun == 0m || string.IsNullOrEmpty(biaozhun.ToString("f2")))
                {
                    //无项目 
                    zhengdanzhekou = 1m;
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
                if (biaozhun == 0m || string.IsNullOrEmpty(biaozhun.ToString("f2")))
                {
                    //无项目 
                    zhengdanzhekou = 1m;
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

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string xmname = GridView1.Rows[e.RowIndex].Cells[3].Text.ToString();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_xm = "delete BaoJiaCPXiangMu where id='" + id + "'";
            SqlCommand com_xm = new SqlCommand(sql_xm, con);
            com_xm.ExecuteNonQuery();
            string sql_an = " delete AnjianXinXi3 where xiangmubianhao='" + id + "'";
            SqlCommand com_an = new SqlCommand(sql_an, con);
            com_an.ExecuteNonQuery();
            //string sql_add = "insert Deleterecord values('" + baojiaid + "','" + rwbianhao.Text + "','" + TextBox5.Text + "','" + xmname + "','" + Session["Username"].ToString() + "','" + DateTime.Now.ToString() + "')";
            //SqlCommand cmd_add = new SqlCommand(sql_add, con);
            //cmd_add.ExecuteNonQuery();
        }
        UPdataBaojia();
        Bind();
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
        #region  2020-05-28注释

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