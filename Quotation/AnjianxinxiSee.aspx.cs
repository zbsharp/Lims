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

        this.Button1.Attributes.Add("onclick", "javascript:return confirm('确认需要修改委托书吗')");
        this.Button4.Attributes.Add("onclick", "javascript:return confirm('确认需要提交开案吗')");
        this.Button5.Attributes.Add("onclick", "javascript:return confirm('确认需要受理吗')");

        this.Button2.Attributes.Add("onclick", "javascript:return confirm('确认需要退回吗')");

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

        dr2.Close();


        if (tikai == "是")
        {
            Button4.Visible = false;
        }

        if (taskno != "")
        {
            Button5.Visible = false;
        }

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


            //string sqlhuaxue = "select * from renzheng";
            //SqlDataAdapter ad9 = new SqlDataAdapter(sqlhuaxue, con);
            //DataSet ds9 = new DataSet();
            //ad9.Fill(ds9);
            //CheckBoxList1.DataSource = ds9.Tables[0];

            //CheckBoxList1.DataTextField = "name";
            //CheckBoxList1.DataValueField = "name"; ;
            //CheckBoxList1.DataBind();


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
                //TextBox11.Text = dr["chouyangdanwei"].ToString();
                //TextBox12.Text = dr["chouyangdidian"].ToString();
                //TextBox13.Text = dr["chouyangshu"].ToString();
                //TextBox14.Text = dr["chouyangriqi"].ToString();
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

                //TextBox27.Text = dr["feiyong"].ToString();
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
                //TextBox26.Text = dr["shencriqi"].ToString();

                string EMCbiaozhun1 = dr["xiangmu"].ToString();
                string[] EMCbiaozhun2 = EMCbiaozhun1.Split('|');
                //foreach (string str in EMCbiaozhun2)
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
            dr.Close();
            con.Close();

            BindContract();
            BindXiangMu();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ResetTextBox(this.Controls);
        //if (DropDownList5.SelectedItem.Text == "快递/Express(到付)" && (TextBox29.Text.Trim() == "" || TextBox30.Text.Trim() == "" || TextBox31.Text.Trim() == "" || TextBox32.Text.Trim() == ""))
        //{
        //    // ld.Text = "<script>alert('请填写邮件接收人信息!');</script>";

        //}
        //else
        //{
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
        //MyExcutSql ext=new MyExcutSql ();
        //bool A= ext.ExcutSqlShenQ(TextBox15.Text.Trim(),"anjianxinxi2");

        //if (A == false)
        {


            string bumen1 = "";
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
            bumen1 = "委托";
            //}
            try
            {
                string ddd = TextBox29.Text.Trim() + "," + TextBox30.Text.Trim() + "," + TextBox31.Text.Trim() + "," + TextBox32.Text.Trim();
                string sql = "update Anjianxinxi2 set a1='" + TextBox29.Text.Trim() + "',a2='" + TextBox30.Text.Trim() + "',a3='" + TextBox31.Text.Trim() + "',a4='" + TextBox32.Text.Trim() + "', hesuanname='" + ddd + "', cpleixing='" + DropDownList1.SelectedValue + "', rwyouxian='" + rwyouxian.SelectedValue + "', xiangmu='" + bumen1 + "', name='" + cp.Text.Trim() + "',xinghao='" + guige.Text.Trim() + "', waibao='" + DropDownList7.SelectedValue + "',wancheng='" + TextBox25.Text + "', state='正常',statetime='', fukuan='" + TextBox4.Text + "',weituo='" + TextBox5.Text + "',zhizao='" + TextBox6.Text + "',shenchan='" + TextBox7.Text + "',ypshuliang='" + yp.Text + "',shangbiao='" + sb.Text + "',songyangfangshi='" + DropDownList1.SelectedValue + "',shenqingbianhao='" + TextBox15.Text + "',zhouqi='" + DropDownList2.SelectedValue + "',baogao='" + DropDownList3.SelectedValue + "',lingqu='" + DropDownList4.SelectedValue + "',yangpinchuli='" + DropDownList5.SelectedValue + "',qitayaoqiu='" + TextBox16.Text + "',beizhu='" + TextBox17.Text + "',ypname='" + TextBox18.Text + "',ypnamey='" + TextBox19.Text + "',ypxinghao='" + TextBox20.Text + "',ypxinghaoy='" + TextBox21.Text + "',syriqi='" + TextBox22.Text + "',qyfangshi='" + TextBox23.Text + "',shengcriqi='" + TextBox7.Text + "',Enweituo='" + txt_ENweituo.Text + "',addressWei='" + txt_addressWei.Text + "',ENaddressWei='" + txt_ENaddressWei.Text + "',ENzhizao='" + txt_ENzhizao.Text + "',ENaddresZhizao='" + txt_ENaddresZhizao.Text + "',addresZhizao='" + txt_addresZhizao.Text + "',ENshengchang='" + txt_ENshengchang.Text + "',addersshengchang='" + txt_addersshengchang.Text + "',ENaddressshengchang='" + txt_ENaddressshengchang.Text + "',ENcpname='" + txt_ENcpname.Text + "',cpzhuce='" + txt_cpzhuce.Text + "',cpfujia='" + txt_cpfujia.Text + "',cpmiaoshu='" + txt_cpmiaoshu.Text + "' where bianhao='" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                string sqly = "delete from Anjianxinxi3 where bianhao='" + bianhao + "'";
                SqlCommand cmdy = new SqlCommand(sqly, con);
                cmdy.ExecuteNonQuery();
                foreach (GridViewRow gr in GridView2.Rows)
                {
                    CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
                    if (hzf.Checked)
                    {
                        string sid = GridView2.DataKeys[gr.RowIndex].Value.ToString();
                        string sqlx = "insert into Anjianxinxi3 values ('" + baojiaid + "','" + kehuid + "','" + bianhao + "','" + chanpinbianhao + "','" + sid + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";
                        SqlCommand cmdx = new SqlCommand(sqlx, con);
                        cmdx.ExecuteNonQuery();
                    }
                }

                string sqlstate = "insert into  TaskState values ('" + bianhao + "','','','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','修改填单','修改')";
                SqlCommand cmdstate = new SqlCommand(sqlstate, con);
                cmdstate.ExecuteNonQuery();

                Label2.Text = "保存成功1";
            }
            catch (Exception ex)
            {
                Label2.Text = ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！";
            }
            finally
            {
                con.Close();
            }
            //}
            //else
            //{
            //    Label2.Text = "申请编号有重复";
            //}
            //}
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();
        //string sqly = "update anjianxinxi2 set biaozhi ='被退回' where bianhao='" + bianhao + "'";
        //SqlCommand cmdy = new SqlCommand(sqly, con);
        //cmdy.ExecuteNonQuery();
        //con.Close();
        //ld.Text = "<script>alert('提交成功!');</script>";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Print/PrintWeiTuo.aspx?bianhao=" + bianhao);
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (DropDownList5.SelectedItem.Text == "快递/Express(到付)" && (TextBox29.Text.Trim() == "" || TextBox30.Text.Trim() == "" || TextBox31.Text.Trim() == "" || TextBox32.Text.Trim() == ""))
        {
            // ld.Text = "<script>alert('请填写邮件接收人信息!');</script>";

        }
        else
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            //修改报价表中的开案状态
            string sql_baojiabiao = "update baojiabiao set kaianbiaozhi='是' where baojiaid='" + baojiaid + "'";
            SqlCommand cmd_baojiabiao = new SqlCommand(sql_baojiabiao, con);
            cmd_baojiabiao.ExecuteNonQuery();

            string sqly = "update Anjianxinxi2 set biaozhi='是',biaozhiname='" + Session["UserName"].ToString() + "',biaozhitime='" + DateTime.Now + "' where bianhao='" + bianhao + "'";
            SqlCommand cmdy = new SqlCommand(sqly, con);
            cmdy.ExecuteNonQuery();
            Button4.Enabled = false;
            con.Close();


            MyExcutSql my = new MyExcutSql();
            my.ExtTaskone(bianhao, "", "提交开案", "手工提交", Session["UserName"].ToString(), "提交开案修改了anjianxinxi2的biaozhi字段", DateTime.Now, st);

            ld.Text = "<script>alert('提交成功!');</script>";
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Case/TaskIn.aspx?tijiaobianhao=" + bianhao);
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
