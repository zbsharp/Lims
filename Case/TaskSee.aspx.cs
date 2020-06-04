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

public partial class Case_TaskSee : System.Web.UI.Page
{
    public string tijiaobianhao = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string responser = "";
    protected string task = "";
    protected string renwu = "";
    protected string sendname = "";
    protected string chakan = "";
    protected string rwid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        tijiaobianhao = Request.QueryString["tijiaobianhao"].ToString();
        GridView1.Attributes.Add("style", "table-layout:fixed");
        rwbianhao.Text = tijiaobianhao;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";
        if (tijiaobianhao.Contains("-"))
        {
            string sqlbian = "select bianhao from AnJianXinXi2 where (taskno='" + tijiaobianhao + "')";
            SqlCommand cmdbian = new SqlCommand(sqlbian, con);
            SqlDataReader drbian = cmdbian.ExecuteReader();
            if (drbian.Read())
            {
                tijiaobianhao = drbian["bianhao"].ToString();
            }
            drbian.Close();
        }

        sql = "select * from AnJianinfo2 where (bianhao='" + tijiaobianhao + "')";

        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            baojiaid = dr["baojiaid"].ToString();
            kehuid = dr["kehuid"].ToString();
            responser = dr["fillname"].ToString();
            rwshenqingbianhao.Text = dr["shenqingbianhao"].ToString();


            rwbianhao.Text = dr["rwbianhao"].ToString();
            rwid = dr["rwbianhao"].ToString();
            task = dr["rwbianhao"].ToString();
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

            bindqingk();
            Bindbeizhu();
            BindLinkMan();
            BindGroup1();
            BindDep();
            Bind_ProjectItem();
            Bind_itembaogao();
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con2.Open();


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


                string EMCbiaozhun1 = dr2["shiyanleibie"].ToString();
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



                rwxiadariqi.Text = dr2["xiadariqi"].ToString();
                rwbaogao.SelectedValue = dr2["baogao"].ToString();
                rwwancheng.Text = dr2["yaoqiuwanchengriqi"].ToString();
                rwyouxian.Text = dr2["youxian"].ToString();
                rwkehu.Text = dr2["kehuyaoqiu"].ToString();
                rwbeizhu.Text = dr2["beizhu"].ToString();
                TextBox1.Text = dr2["keshi"].ToString();
                cp.Text = dr2["name"].ToString();
                guige.Text = dr2["xinghao"].ToString();

                DropDownList5.SelectedValue = dr2["b3"].ToString();
                TextBox4.Text = dr2["fukuandanwei"].ToString();
                TextBox5.Text = dr2["weituodanwei"].ToString();
                TextBox6.Text = dr2["zhizaodanwei"].ToString();
                TextBox7.Text = dr2["shengchandanwei"].ToString();
                cp.Text = dr2["chanpinname"].ToString();
                guige.Text = dr2["xinghaoguige"].ToString();
                ypshanbiao.Text = dr2["shangbiao"].ToString();
                ypshuliang.Text = dr2["num"].ToString();
                ypsongjianriqi.Text = dr2["songjiandate"].ToString();
                ypchouyangfangshi.Text = dr2["quyangfangshi"].ToString();
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
                txt_zhuce.Text = dr2["b5"].ToString();
                txt_fujia.Text = dr2["b6"].ToString();

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
            string sqlb = "select responser from baojiabiao where baojiaid='" + baojiaid + "'";
            SqlCommand cmdb = new SqlCommand(sqlb, con2);
            SqlDataReader drb = cmdb.ExecuteReader();
            if (drb.Read())
            {
                Label1.Text = drb["responser"].ToString();
                //显示销售助理
                lb_zhuli.Text = Eng(Label1.Text.ToString());
            }


            con2.Close();
            BindZhujianengineer();
            Bind();
            BindYanPin();
            Bindzanting();
            BindBaoGao();
            Bindj();
            Bindj2();
            bindhesuan();
            Bindshangbao();
        }

        chakan = Request.QueryString["chakan"].ToString();
        if (chakan == "0")
        {
            Panel1.Visible = false;


        }
        else if (chakan == "1")
        {
            Panel1.Visible = true;
        }
        //Panel1.Visible = true;


        if (limit1("核算费用"))
        {
            Button9.Visible = true;
        }
        else
        {
            Button9.Visible = false;
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

    protected void bindhesuan()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CeShiFeikf where bianhao=(select top 1 bianhao from anjianinfo2 where rwbianhao='" + rwbianhao.Text + "')";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con);


        DataSet ds = new DataSet();


        ad.Fill(ds);
        GridView10.DataSource = ds.Tables[0];
        GridView10.DataBind();
        con.Close();
    }
    protected void Bindshangbao()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CeShiFei where bianhao=(select top 1 bianhao from anjianinfo2 where rwbianhao='" + rwbianhao.Text + "')";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        GridView11.DataSource = dr;
        GridView11.DataBind();

        con.Close();
    }
    protected void Bindj()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from baogaobumen where rwid='" + rwbianhao.Text + "'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        GridView8.DataSource = ds.Tables[0];
        GridView8.DataBind();

        con.Close();
        con.Dispose();
    }

    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        string state = "";

        if (e.CommandName == "xiada")
        {




            // Response.Redirect("~/Income/InvoiceAdd.aspx?ran=" + shoufeiid);

            Response.Redirect("~/Report/BaoGaoShenPi.aspx?baogaoid=" + sid + "&&pp=1");




        }
    }


    protected void Bindj2()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from zanting2 where rwbianhao='" + rwbianhao.Text + "'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        GridView9.DataSource = ds.Tables[0];
        GridView9.DataBind();

        con.Close();
        con.Dispose();
    }



    protected void bindqingk()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();



        string sql = "select top 50 *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=(select top 1 taskid from CeShiFeiKf where shoufeibianhao=invoice.inid)) as shenqingbianhao,(select top 1 kf from anjianinfo2 where rwbianhao=(select top 1 taskid from CeShiFeiKf where shoufeibianhao=invoice.inid)) as kf,(select sum(feiyong) from CeShiFeiKf where shoufeibianhao=invoice.inid) as feiyong1,(select count(*) from CeShiFeiKf where shoufeibianhao=invoice.inid) as shu,(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname ,(select top 1 taskid from CeShiFeiKf where shoufeibianhao=invoice.inid) as taskno from Invoice where inid in (select shoufeibianhao from CeShiFeiKf where taskid='" + rwbianhao.Text + "') order by id desc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView7.DataSource = ds.Tables[0];
        GridView7.DataBind();

        con.Close();
    }



    protected void BindZhujianengineer()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *  from Zhujianengineer  where bianhao='" + rwbianhao.Text.Trim() + "' order  by id desc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView6.DataSource = ds.Tables[0];
        GridView6.DataBind();



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

    protected void BindYanPin()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from YangPin2 where anjianid='" + rwbianhao.Text.Trim() + "' order by id asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        GridView5.DataSource = ds.Tables[0];
        GridView5.DataBind();

        con.Close();
        con.Dispose();
    }


    protected void BindBaoGao()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from baogao2 where rwid='" + rwbianhao.Text.Trim() + "' or tjid='" + rwbianhao.Text.Trim() + "' order by id asc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView4.DataSource = ds.Tables[0];
        GridView4.DataBind();
        con.Close();
        con.Dispose();
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sqlde1 = "delete from anjianinfo where tijiaobianhao='" + tijiaobianhao + "'";
            SqlCommand cmdde1 = new SqlCommand(sqlde1, con);
            cmdde1.ExecuteNonQuery();
            searchwhere sr = new searchwhere();
            TextBox13.Text = sr.tian1(Convert.ToDateTime(rwxiadariqi.Text.Trim()), Convert.ToDateTime(rwwancheng.Text.Trim())).ToString();


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


                    sql4 = "insert into anjianinfo values('" + baojiaid + "','" + kehuid + "','" + rwbianhao.Text.Trim() + "','" + tijiaobianhao + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "','" + CheckBoxList1.Items[i - 1].Text.ToString() + "','否','','','未开始','否','','" + Convert.ToDateTime(rwwancheng.Text.Trim()) + "','','是','','','')";


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


            //string sqlinsert4 = "insert into anjianinfo values('" + baojiaid + "','" + kehuid + "','" + rwbianhao.Text + "','" + tijiaobianhao + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "','" + DropDownList2.SelectedValue + "','否','" + DropDownList1.SelectedValue + "','','未开始','是','','','')";
            //SqlCommand cmd4 = new SqlCommand(sqlinsert4, con);
            //cmd4.ExecuteNonQuery();





            tiaokuan = tiaokuan + "|" + DropDownList2.SelectedValue;
            string sql = "update anjianinfo2 set yaoqiushixian='" + TextBox13.Text.Trim() + "', shixian='" + TextBox12.Text.Trim() + "', waibao='" + DropDownList3.SelectedValue + "', qitayaoqiu='" + TextBox2.Text + "',yuji='" + TextBox8.Text + "', name='" + cp.Text + "',xinghao='" + guige.Text + "', keshi='" + tiaokuan + "',state='" + rwstate.SelectedValue + "',shiyanleibie='" + bumen1 + "',xiadariqi='" + rwxiadariqi.Text + "',yaoqiuwanchengriqi='" + rwwancheng.Text + "',baogao='" + rwbaogao.SelectedValue + "',beizhu='" + rwbeizhu.Text + "',kehuyaoqiu='" + rwkehu.Text + "',youxian='" + rwyouxian.Text + "',fukuandanwei='" + TextBox4.Text + "',weituodanwei='" + TextBox5.Text + "',zhizaodanwei='" + TextBox6.Text + "',shengchandanwei='" + TextBox7.Text + "',chanpinname='" + cp.Text + "',xinghaoguige='" + guige.Text + "',shangbiao='" + ypshanbiao.Text + "',num='" + ypshuliang.Text + "',songjiandate='" + ypsongjianriqi.Text + "',quyangfangshi='" + ypchouyangfangshi.Text + "',chouyangdanwei='" + "',shengchandate='" + ypshengchanriqi.Text + "',remark='" + ypbeizhu.Text + "' where bianhao='" + tijiaobianhao + "' ";
            SqlCommand cmdi = new SqlCommand(sql, con);
            cmdi.ExecuteNonQuery();

            //string sqls = "update Anjianxinxi2 set shoulibiaozhi='是',shouliren='" + Session["UserName"].ToString() + "',shoulitime='" + DateTime.Now + "' where bianhao='" + tijiaobianhao + "'";
            //SqlCommand cmdis = new SqlCommand(sqls, con);
            //cmdis.ExecuteNonQuery();

            // string sql6 = "insert into yangpin values('" + baojiaid + "','" + kehuid + "','" + tijiaobianhao + "','" + ypbianhao.Text.Trim() + "','" + ypbianhaoy.Text.Trim() + "','" + ypxinghao.Text.Trim() + "','" + ypxinghaoy.Text.Trim() + "','" + ypshanbiao.Text.Trim() + "','" + ypshuliang.Text.Trim() + "','" + ypsongjianriqi.Text.Trim() + "','" + ypbeizhu.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "')";
            string sql6 = "insert into yangpin values('" + baojiaid + "','" + kehuid + "','" + tijiaobianhao + "','" + ypbianhao.Text.Trim() + "','" + ypbianhaoy.Text.Trim() + "','" + ypxinghao.Text.Trim() + "','" + ypxinghaoy.Text.Trim() + "','" + ypshanbiao.Text.Trim() + "','" + ypshuliang.Text.Trim() + "','" + ypchouyangfangshi.Text.Trim() + "','" + ypsongjianriqi.Text.Trim() + "','" + ypbeizhu.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "')";
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


            string sqlup = "update taskstate set dotimetianxie='" + Convert.ToDateTime(rwxiadariqi.Text.Trim()) + "' where bianhao='" + tijiaobianhao + "' and type1='受理任务'";
            SqlCommand cmdup = new SqlCommand(sqlup, con);
            cmdup.ExecuteNonQuery();

            con.Close();

            ld.Text = "<script>alert('保存成功!');window.close();</script>";
        }

    }
    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "";
        sql = "select * from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "'";
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

    protected void Bindzanting()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from zanting2 where rwbianhao='" + rwid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            gv_zanting.DataSource = ds.Tables[0];
            gv_zanting.DataBind();
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
        string task = "";


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sql1 = "select taskno from anjianxinxi2 where taskno !='' order by taskno asc";
            string id = "";
            SqlDataAdapter adpter = new SqlDataAdapter(sql1, con);
            DataSet ds = new DataSet();
            adpter.Fill(ds);
            string date1 = "";
            string day = "";
            string houzhui = "";
            string two = "";
            string yue = "";
            string tian = "";
            string yue1 = "";
            string tian1 = "";
            if (ds.Tables[0].Rows.Count == 0)
            {

                task = DateTime.Now.Year.ToString().Substring(2) + "-" + "00001";
            }
            else
            {
                houzhui = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["taskno"].ToString();

                tian = houzhui.Substring(3, 5);


                int a = Convert.ToInt32(tian) + 1;
                task = DateTime.Now.Year.ToString().Substring(2) + "-" + String.Format("{0:D5}", a);



            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }


        con.Close();


        return task;
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Bind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView1.EditIndex = e.NewEditIndex;
        Bind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        bool A = false;
        A = limit1("案件受理");
        if (A == true)
        {


            string KeyId = GridView1.DataKeys[e.RowIndex].Value.ToString();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string uuname1 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
            string uuname2 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
            string uuname3 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
            string uuname4 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
            string uuname5 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text.ToString());
            string uuname6 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[7].Controls[0]).Text.ToString());
            string uuname7 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[8].Controls[0]).Text.ToString());
            string uuname8 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[9].Controls[0]).Text.ToString());
            string uuname9 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[10].Controls[0]).Text.ToString());
            string uuname10 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[11].Controls[0]).Text.ToString());

            string sql = "update BaoJiaCPXiangMu set ceshiname='" + uuname1 + "',biaozhun='" + uuname2 + "',neirong='" + uuname3 + "',yp='" + uuname4 + "',zhouqi='" + uuname5 + "',feiyong='" + Convert.ToDecimal(uuname6) + "',zhekou='" + Convert.ToDecimal(uuname7) + "',shuliang='" + Convert.ToDecimal(uuname8) + "',beizhu='" + uuname9 + "',jishuyaoqiu='" + uuname10 + "' where id='" + KeyId + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            Bind();
        }

    }



    protected void Button4_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sqli = "update Anjianinfo2 set renwu='是',state='下达' ,xiadariqi='" + rwxiadariqi.Text.Trim() + "' where bianhao='" + tijiaobianhao + "'";
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


        con.Close();

        searchwhere sx4 = new searchwhere();
        string sjt1 = sx4.ShiXiao(task);


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
            string beizhu = TextBox15.Text.Replace('\'', ' ');
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "insert into anjianbeizhu values('" + rwbianhao.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + beizhu + "','')";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            GridView2.DataSource = dr;
            GridView2.DataBind();
            dr.Close();
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
    protected void Button1_Click2(object sender, EventArgs e)
    {

    }
    protected void Button18_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();


            string sql = "insert into anjianbeizhu values('" + rwbianhao.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','(申请暂停)" + TextBox15.Text + "','申请暂停')";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            GridView2.DataSource = dr;
            GridView2.DataBind();
            dr.Close();


            string sql2 = "update anjianinfo2 set b6='申请暂停',b2='" + Session["UserName"].ToString() + "',b7='" + DateTime.Now.ToShortDateString() + "',b8='" + TextBox15.Text + "' where rwbianhao='" + rwbianhao.Text.Trim() + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.ExecuteNonQuery();

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
    protected void Button19_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();


            string sql = "insert into anjianbeizhu values('" + rwbianhao.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','(申请恢复)" + TextBox15.Text + "','申请恢复')";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            GridView2.DataSource = dr;
            GridView2.DataBind();
            dr.Close();


            string sql2 = "update anjianinfo2 set b6='申请恢复',b2='" + Session["UserName"].ToString() + "',b7='" + DateTime.Now.ToShortDateString() + "',b8='" + TextBox15.Text + "' where rwbianhao='" + rwbianhao.Text.Trim() + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.ExecuteNonQuery();

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
    /// <summary>
    /// 加载工程师项目表
    /// </summary>
    protected void Bind_ProjectItem()
    {
        if (string.IsNullOrEmpty(rwbianhao.Text))
        {

        }
        else
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = "select * from ProjectItem where taskid='" + rwbianhao.Text + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                gv_projectitem.DataSource = ds.Tables[0];
                gv_projectitem.DataBind();
            }
        }
    }

    /// <summary>
    /// 加载报告项目表
    /// </summary>
    protected void Bind_itembaogao()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select  baogaoid,taskid,xmname,fillname,filltime,(select biaozhun from BaoJiaCPXiangMu where BaoJiaCPXiangMu.id=xmid) as biaozhun from ItemBaogao where taskid='" + rwbianhao.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            grid_itembaogao.DataSource = ds.Tables[0];
            grid_itembaogao.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            string dutyname = DutyName();
            if (dutyname.Trim() == "工程师")
            {
                e.Row.Cells[8].Visible = false;
            }
        }
    }
    /// <summary>
    /// 返回登录进来人的职位
    /// </summary>
    /// <returns></returns>
    protected string DutyName()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlDataAdapter da_dutyname = new SqlDataAdapter(sql_dutyname, con);
            DataSet ds_dutyname = new DataSet();
            da_dutyname.Fill(ds_dutyname);
            string dutyname = ds_dutyname.Tables[0].Rows[0]["dutyname"].ToString();
            return dutyname;
        }
    }

    public string Eng(string name)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql112 = "select UserName from CustomerServer where marketid='" + name + "'";

        SqlDataAdapter ad112 = new SqlDataAdapter(sql112, con);
        DataSet ds112 = new DataSet();
        ad112.Fill(ds112);
        con.Close();
        DataTable dt112 = ds112.Tables[0];
        string zhujian = "";
        for (int z = 0; z < dt112.Rows.Count; z++)
        {
            zhujian = zhujian + dt112.Rows[z]["UserName"].ToString() + ",";
        }
        if (zhujian.Contains(","))
        {
            zhujian = zhujian.Substring(0, zhujian.Length - 1);
        }
        return zhujian;
    }

    protected void gv_projectitem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string cause = this.txcause.Text.Replace('\'', ' ');
        if (string.IsNullOrEmpty(cause))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入状态更改原因')</script>");
        }
        else
        {
            var id = e.CommandArgument;
            var method = e.CommandName;
            string state = string.Empty;
            if (method == "suspend")
            {
                state = "中止";
            }
            else
            {
                state = "暂停";
            }
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = " update ProjectItem set finishdate='" + DateTime.Now + "',cause='" + cause + "',state='" + state + "' where id=" + id + "";
                SqlCommand cmd = new SqlCommand(sql, con);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    Bind_ProjectItem();
                    this.txcause.Text = string.Empty;
                }
            }
        }
    }

    protected void gv_projectitem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string dutyname = DutyName();
        if (!dutyname.Contains("客服") && dutyname != "系统管理员" && !dutyname.Contains("工程"))
        {
            e.Row.Cells[8].Visible = false;
        }
    }
}