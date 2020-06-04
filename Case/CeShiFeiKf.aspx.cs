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

public partial class CeShiFeiKf : System.Web.UI.Page
{

    string bianhao = "";
    string taskno = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string state = "";
    Hashtable ArrAreaSelect = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView3.Attributes.Add("style", "table-layout:fixed");
        bianhao = Request.QueryString["bianhao"].ToString();

        Label3.ForeColor = Color.Red;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql2 = "select taskno,baojiaid ,kehuid from anjianxinxi2 where bianhao='" + bianhao + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            taskno = dr2["taskno"].ToString();
            baojiaid = dr2["baojiaid"].ToString();
            kehuid = dr2["kehuid"].ToString();
        }
        dr2.Close();

        string sql4 = "select *,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname  from baojiabiao where baojiaid ='" + baojiaid + "' order by tijiaotime desc";
        SqlDataAdapter da = new SqlDataAdapter(sql4, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView4.DataSource = ds.Tables[0];
        GridView4.DataBind();

        foreach (GridViewRow item in GridView4.Rows)
        {
            string coupon = item.Cells[4].Text.ToString();
            if (string.IsNullOrEmpty(coupon) || coupon == "&nbsp;" || coupon == "0.00")
            {
                item.Cells[4].Text = string.Empty;
            }
        }

        if (!IsPostBack)
        {
            bind();
            Bind2();
            Bind3();
            BindDep();
            string sql = "delete from CheckHeSuan where bianhao='" + bianhao + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        con.Close();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Random seed = new Random();
        Random randomNum = new Random(seed.Next());
        string shoufeiid = randomNum.Next().ToString() + DateTime.Now.ToString("yyyyMMdd_hhmmss");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        decimal baojiafeiyongzong = 0;
        decimal gongchengshifeiyongzong = 0;
        string sid = "";
        foreach (GridViewRow gr in GridView2.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            decimal baojiafeiyong = 0;
            if (hzf.Checked)
            {
                sid = GridView2.DataKeys[gr.RowIndex].Value.ToString();
                string sqlid = "select total,baojiaid,kehuid from BaoJiaCPXiangMu where id='" + sid + "'";
                SqlCommand cmdid = new SqlCommand(sqlid, con);
                SqlDataReader drid = cmdid.ExecuteReader();
                if (drid.Read())
                {
                    if (drid["total"] == DBNull.Value)
                    {
                        baojiafeiyong = 0;
                    }
                    else
                    {
                        baojiafeiyong = Convert.ToDecimal(drid["total"]);
                        baojiaid = drid["baojiaid"].ToString();
                    }
                }
                drid.Close();
                string sqlup1 = "update BaoJiaCPXiangMu set tijiaohaoma='" + shoufeiid + "',hesuanbiaozhi='是' where id='" + sid + "'";
                SqlCommand cmdup1 = new SqlCommand(sqlup1, con);
                cmdup1.ExecuteNonQuery();
            }
            baojiafeiyongzong += baojiafeiyong;
        }

        decimal feiyong = gongchengshifeiyongzong + baojiafeiyongzong;
        feiyong = Convert.ToDecimal(TextBox7.Text.Trim());
        //decimal fy2 = Convert.ToDecimal(TextBox7.Text.Trim()) * Convert.ToDecimal(DropDownList3.SelectedValue);
        try
        {
            string sql_baojia = "select  currency from BaoJiaBiao where BaoJiaId='" + baojiaid + "'";
            SqlCommand com_baojia = new SqlCommand(sql_baojia, con);
            SqlDataReader dr_baojia = com_baojia.ExecuteReader();
            string currency = "";
            if (dr_baojia.Read())
            {
                currency = dr_baojia["currency"].ToString();
            }
            dr_baojia.Close();
            string sql = "insert into CeShiFeikf values('" + bianhao + "','" + kehuid + "','" + baojiaid + "','" + feiyong + "','" + TextBox3.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + DropDownList2.SelectedValue + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + shoufeiid + "','','','否','','','" + Convert.ToDecimal(DropDownList3.SelectedValue) + "','" + feiyong + "','0','" + taskno + "','','" + DropDownList_project.SelectedValue + "','" + sid + "','" + currency + "','否')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
        con.Close();
        bind();
        Bind2();
        Bind3();
    }

    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        //string sql = "select * from UserDepa where name in(select department from departmenttype where type='核算金额部门') order by name asc";

        string sql = "select * from UserDepa where departmentid='10' or departmentid='11'  or  departmentid='12' or departmentid='13' or departmentid='14' or departmentid='15' or departmentid='1018' ";

        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


        DataSet ds = new DataSet();


        ad.Fill(ds);





        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataValueField = "name";
        DropDownList2.DataTextField = "name";
        DropDownList2.DataBind();
        DropDownList2.Items.Add(new ListItem("", ""));


        con3.Close();
    }



    protected void bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        //string sql = "select * from CeShiFeikf where bianhao='" + bianhao + "'";
        string sql = "select * from CeShiFeikf where xmid in (select xiangmubianhao from anjianxinxi3 where bianhao='" + bianhao + "')";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
    }

    protected void Bind2()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *  from BaoJiaCPXiangMu  where id in (select xiangmubianhao from anjianxinxi3 where bianhao='" + bianhao + "') order  by id desc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();
        con.Close();

        //foreach (GridViewRow gr in GridView2.Rows)
        //{
        //    CheckBox hzf = (CheckBox)gr.Cells[11].FindControl("CheckBox1");
        //    if (gr.Cells[9].Text != "是")
        //    {
        //        hzf.Enabled = true;
        //    }
        //    else
        //    {
        //        hzf.Enabled = false;
        //    }
        //}
    }


    protected void Bind3()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select (select departmentname from userinfo where username=ceshifei.fillname) as bumen,* from CeShiFei where bianhao='" + bianhao + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        GridView3.DataSource = dr;
        GridView3.DataBind();

        con.Close();

        foreach (GridViewRow gr in GridView3.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[8].FindControl("CheckBox1");


            if (gr.Cells[7].Text != "是")
            {
                hzf.Enabled = true;


                //string sql2 = "insert into CheckHeSuan4 values('CeShiFei','" + sid + "','" + ran + "')";
                //SqlCommand cmd2 = new SqlCommand(sql2, con);
                //cmd2.ExecuteNonQuery();

            }
            else
            {
                hzf.Enabled = false;
            }



        }


    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string sql_claim = "select * from Claim where ceshifeikfid='" + id + "'";
        SqlCommand cmd_claim = new SqlCommand(sql_claim, con);
        SqlDataReader dr_claim = cmd_claim.ExecuteReader();
        if (dr_claim.Read())
        {
            dr_claim.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该记录业务已认领，不能进行删改操作。')</script>");
        }
        else
        {
            dr_claim.Close();

            string shoufeiid = "";
            string shoufeibianhao = "否";
            string sqlguanbi = "select state from anjianinfo2 where bianhao='" + bianhao + "'";
            SqlCommand cmdgb = new SqlCommand(sqlguanbi, con);
            SqlDataReader drgb = cmdgb.ExecuteReader();
            if (drgb.Read())
            {
                state = drgb["state"].ToString();
            }
            drgb.Close();
            if (state == "关闭")
            {
            }
            else
            {
                string sql2 = "select bianhaoone,shoufeibianhao from CeShiFeikf where id='" + id + "'";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    shoufeiid = dr2["bianhaoone"].ToString();
                    shoufeibianhao = dr2["shoufeibianhao"].ToString();
                }
                dr2.Close();

                string sql8 = "select hesuanbiaozhi from invoice where inid='" + shoufeibianhao + "' and hesuanbiaozhi='是'";
                SqlCommand cmd8 = new SqlCommand(sql8, con);
                SqlDataReader dr8 = cmd8.ExecuteReader();
                if (dr8.Read())
                {
                    dr8.Close();
                }
                else
                {
                    dr8.Close();
                    string sql3 = "update BaoJiaCPXiangMu set hesuanbiaozhi='否' where tijiaohaoma='" + shoufeiid + "' ";
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    cmd3.ExecuteNonQuery();


                    string sql4 = "update CeShiFei set beizhu2='否' where beizhu1='" + shoufeiid + "' ";
                    SqlCommand cmd4 = new SqlCommand(sql4, con);
                    cmd4.ExecuteNonQuery();

                    string sql = "delete from CeShiFeikf where id='" + id + "'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();


                    string sqlh = "delete from CheckHeSuan where bianhao='" + bianhao + "'";
                    SqlCommand cmdh = new SqlCommand(sqlh, con);
                    cmdh.ExecuteNonQuery();


                    string sqlh1 = "update anjianxinxi2 set bianhaotwo=''  where bianhao='" + bianhao + "'";
                    SqlCommand cmdh1 = new SqlCommand(sqlh1, con);
                    cmdh1.ExecuteNonQuery();
                }
            }
            con.Close();
            bind();
            Bind2();
            Bind3();
            if (shoufeibianhao != "否" && shoufeibianhao != "")
            {
                Response.Redirect("~/Income/InvoiceAdd2.aspx?invoiceid=" + shoufeibianhao);
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        bind();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string KeyId = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        //string uuname1 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());
        //decimal uuname2 =Convert.ToDecimal ( Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString()));
        //decimal zhekou = Convert.ToDecimal(Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString()));
        //decimal uuname4 = Convert.ToDecimal(Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString()));
        //string uuname6 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[7].Controls[0]).Text.ToString());
        //string uuname5 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text.ToString());

        string remark = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text);
        decimal money = 0m;
        try
        {
            money = Convert.ToDecimal(Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text));
        }
        catch (Exception)
        {

        }

        string sql = "update CeShiFeikf set feiyong=" + money + ",baojia=" + money + " ,beizhu2='" + remark + "' where id='" + KeyId + "'";

        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();
        GridView1.EditIndex = -1;
        bind();
    }


    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        bind();
    }

    protected void Button2_Click1(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string sql = "update Anjianxinxi2 set hesuanbiaozhi='是' where bianhao='" + bianhao + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();

        ld.Text = "<script>alert('添加完成！');</script>";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string sql = "update Anjianxinxi2 set hesuanbiaozhi='否' where bianhao='" + bianhao + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();

        ld.Text = "<script>alert('取消完成！');</script>";
    }

    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        var cb = sender as CheckBox;

        //设置复选框只能选择一个
        foreach (GridViewRow item in GridView2.Rows)
        {
            if (item.RowType == DataControlRowType.DataRow)
            {
                var cb1 = (CheckBox)item.FindControl("CheckBox1");
                if (cb == cb1)
                {
                    cb.Checked = true;
                }
                if (cb != cb1 && cb1.Checked)
                {
                    cb1.Checked = false;
                }
            }
        }

        bool chk = cb.Checked;
        if (chk)
        {
            GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;
            TextBox3.Text = this.GridView2.Rows[gvr.RowIndex].Cells[1].Text.ToString();
            TextBox7.Text = this.GridView2.Rows[gvr.RowIndex].Cells[6].Text.ToString();
        }


        #region  2109-12-3注释
        //bool ch = ((CheckBox)sender).Checked;
        //SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con1.Open();
        //string sqlguanbi = "select state from anjianinfo2 where bianhao='" + bianhao + "'";
        //SqlCommand cmdgb = new SqlCommand(sqlguanbi, con1);
        //SqlDataReader drgb = cmdgb.ExecuteReader();
        //if (drgb.Read())
        //{
        //    state = drgb["state"].ToString();
        //}
        //drgb.Close();
        //if (state == "关闭")
        //{
        //    con1.Close();
        //}
        //else
        //{
        //    if (ch)
        //    {
        //        GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;
        //        string strDate = this.GridView2.Rows[gvr.RowIndex].Cells[1].Text.ToString();
        //        string tableid = this.GridView2.Rows[gvr.RowIndex].Cells[0].Text.ToString();
        //        string sql = "select * from CheckHeSuan where tableid='" + tableid + "' and bianhao='" + bianhao + "' and tablename='BaoJiaCPXiangMu'";
        //        SqlCommand cmd = new SqlCommand(sql, con1);
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            dr.Close();
        //        }
        //        else
        //        {
        //            dr.Close();
        //            string sql2 = "insert into CheckHeSuan values('BaoJiaCPXiangMu','" + tableid + "','" + bianhao + "')";
        //            SqlCommand cmd2 = new SqlCommand(sql2, con1);
        //            cmd2.ExecuteNonQuery();
        //        }
        //    }
        //    else
        //    {
        //        GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;
        //        string strDate = this.GridView2.Rows[gvr.RowIndex].Cells[1].Text.ToString();
        //        string tableid = this.GridView2.Rows[gvr.RowIndex].Cells[0].Text.ToString();
        //        string sql = "select * from CheckHeSuan where tableid='" + tableid + "' and bianhao='" + bianhao + "'  and tablename='BaoJiaCPXiangMu'";
        //        SqlCommand cmd = new SqlCommand(sql, con1);
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            dr.Close();
        //            string sql2 = "delete from  CheckHeSuan where tableid='" + tableid + "' and bianhao='" + bianhao + "'  and tablename='BaoJiaCPXiangMu'";
        //            SqlCommand cmd2 = new SqlCommand(sql2, con1);
        //            cmd2.ExecuteNonQuery();
        //        }
        //        else
        //        {
        //            dr.Close();
        //        }
        //    }
        //    int i = 0;
        //    string sql4 = "select (select ceshiname from BaoJiaCPXiangMu where id=CheckHeSuan.tableid and hesuanbiaozhi !='是') as name1 from CheckHeSuan where bianhao='" + bianhao + "' union select (select type from CeShiFei where id=CheckHeSuan.tableid and beizhu2 !='是') as name1 from CheckHeSuan where bianhao='" + bianhao + "'";
        //    SqlCommand cmd4 = new SqlCommand(sql4, con1);
        //    SqlDataReader dr4 = cmd4.ExecuteReader();
        //    while (dr4.Read())
        //    {
        //        if (i == 0)
        //        {
        //            TextBox3.Text = dr4["name1"].ToString() + ",";
        //        }
        //        else if (!TextBox3.Text.Contains(dr4["name1"].ToString()))
        //        {
        //            TextBox3.Text = TextBox3.Text + dr4["name1"].ToString() + ",";
        //        }
        //        i = i + 1;
        //    }
        //    dr4.Close();
        //    string sql5 = "select * from CheckHeSuan where bianhao='" + bianhao + "'";
        //    SqlCommand cmd5 = new SqlCommand(sql5, con1);
        //    SqlDataReader dr5 = cmd5.ExecuteReader();
        //    if (dr5.Read())
        //    {

        //    }
        //    else
        //    {
        //        TextBox3.Text = "";
        //    }
        //    con1.Close();
        //    if (TextBox3.Text.Length > 2)
        //    {
        //        TextBox3.Text = TextBox3.Text.Substring(0, TextBox3.Text.Length - 1);
        //        TextBox3.Text = TextBox3.Text.Substring(1);
        //    }

        //    TextBox7.Text = hesuan();
        // }
        #endregion
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        bool ch = ((CheckBox)sender).Checked;
        //if (ch)
        //{
        //    GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;
        //    string strDate = this.GridView3.Rows[gvr.RowIndex].Cells[1].Text.ToString();
        //    string strIsHoliday = ch.ToString();
        //    if (TextBox3.Text == "")
        //    {
        //        TextBox3.Text = TextBox3.Text + strDate;
        //    }
        //    else
        //    {
        //        TextBox3.Text = TextBox3.Text + "," + strDate;
        //    }
        //}
        //else
        //{

        //    GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;
        //    string strDate = this.GridView3.Rows[gvr.RowIndex].Cells[1].Text.ToString();
        //    string strIsHoliday = ch.ToString();

        //    if (TextBox3.Text.Contains(strDate))
        //    {
        //        TextBox3.Text = TextBox3.Text.Replace(strDate, "");
        //    }
        //}

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();


        string sqlguanbi = "select state from anjianinfo2 where bianhao='" + bianhao + "'";
        SqlCommand cmdgb = new SqlCommand(sqlguanbi, con1);
        SqlDataReader drgb = cmdgb.ExecuteReader();
        if (drgb.Read())
        {
            state = drgb["state"].ToString();
        }
        drgb.Close();

        if (state == "关闭")
        {

            con1.Close();
        }

        else
        {

            if (ch)
            {

                GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;
                string strDate = this.GridView3.Rows[gvr.RowIndex].Cells[1].Text.ToString();
                string tableid = this.GridView3.Rows[gvr.RowIndex].Cells[0].Text.ToString();


                string sql = "select * from CheckHeSuan where tableid='" + tableid + "' and bianhao='" + bianhao + "' and tablename='CeShiFei'";
                SqlCommand cmd = new SqlCommand(sql, con1);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    string sql2 = "insert into CheckHeSuan values('CeShiFei','" + tableid + "','" + bianhao + "')";
                    SqlCommand cmd2 = new SqlCommand(sql2, con1);
                    cmd2.ExecuteNonQuery();
                }
            }

            else
            {

                GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;
                string strDate = this.GridView3.Rows[gvr.RowIndex].Cells[1].Text.ToString();

                string tableid = this.GridView3.Rows[gvr.RowIndex].Cells[0].Text.ToString();


                string sql = "select * from CheckHeSuan where tableid='" + tableid + "' and bianhao='" + bianhao + "'  and tablename='CeShiFei'";
                SqlCommand cmd = new SqlCommand(sql, con1);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    string sql2 = "delete from  CheckHeSuan where tableid='" + tableid + "' and bianhao='" + bianhao + "'  and tablename='CeShiFei'";
                    SqlCommand cmd2 = new SqlCommand(sql2, con1);
                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    dr.Close();
                }
            }



            int i = 0;
            string sql4 = "select (select ceshiname from BaoJiaCPXiangMu where id=CheckHeSuan.tableid and hesuanbiaozhi !='是') as name1 from CheckHeSuan where bianhao='" + bianhao + "' union select (select type from CeShiFei where id=CheckHeSuan.tableid and beizhu2 !='是') as name1 from CheckHeSuan where bianhao='" + bianhao + "'";


            SqlCommand cmd4 = new SqlCommand(sql4, con1);
            SqlDataReader dr4 = cmd4.ExecuteReader();


            while (dr4.Read())
            {

                if (i == 0)
                {

                    TextBox3.Text = dr4["name1"].ToString() + ",";
                }
                else if (!TextBox3.Text.Contains(dr4["name1"].ToString()))
                {
                    TextBox3.Text = TextBox3.Text + dr4["name1"].ToString() + ",";
                }


                i = i + 1;



            }

            dr4.Close();

            string sql5 = "select * from CheckHeSuan where bianhao='" + bianhao + "'";
            SqlCommand cmd5 = new SqlCommand(sql5, con1);
            SqlDataReader dr5 = cmd5.ExecuteReader();
            if (dr5.Read())
            {

            }
            else
            {
                TextBox3.Text = "";
            }



            con1.Close();

            if (TextBox3.Text.Length > 2)
            {
                TextBox3.Text = TextBox3.Text.Substring(0, TextBox3.Text.Length - 1);
                TextBox3.Text = TextBox3.Text.Substring(1);
            }


            TextBox7.Text = hesuan();

        }


    }

    protected string hesuan()
    {
        string feiyong = "0";
        decimal a = 0;
        decimal b = 0;
        decimal c = 0;
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string sql4 = "select (select sum(total) from BaoJiaCPXiangMu where id=CheckHeSuan.tableid and hesuanbiaozhi !='是') as dd from CheckHeSuan where bianhao='" + bianhao + "' ";
        SqlCommand cmd4 = new SqlCommand(sql4, con1);
        SqlDataReader dr4 = cmd4.ExecuteReader();
        while (dr4.Read())
        {
            if (dr4["dd"] == DBNull.Value)
            {

            }
            else
            {
                //baojiazong_txt.Text = Convert.ToDecimal(drsum["total"]).ToString().Trim();
                a = a + Convert.ToDecimal(dr4["dd"]);
            }
        }
        dr4.Close();




        string sql5 = "select (select sum(xiaoji) from CeShiFei where id=CheckHeSuan.tableid and beizhu2 !='是') as bb from CheckHeSuan where bianhao='" + bianhao + "' ";
        SqlCommand cmd5 = new SqlCommand(sql5, con1);
        SqlDataReader dr5 = cmd5.ExecuteReader();
        while (dr5.Read())
        {
            if (dr5["bb"] == DBNull.Value)
            {

            }
            else
            {
                //baojiazong_txt.Text = Convert.ToDecimal(drsum["total"]).ToString().Trim();
                b = b + Convert.ToDecimal(dr5["bb"]);
            }
        }
        dr5.Close();

        c = a + b;
        feiyong = Math.Round(Convert.ToDecimal(c), 2).ToString();

        return feiyong;

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        //先判断工程款的总价是否与测试项目总价相匹配
        //decimal project = 0;
        //decimal xmprice = 0;
        //foreach (GridViewRow gr in GridView1.Rows)
        //{
        //    project += Convert.ToDecimal(gr.Cells[5].Text);
        //    xmprice += Convert.ToDecimal(gr.Cells[4].Text);
        //}
        //if (project != xmprice)
        //{
        //    Response.Write("<script>alert('工程款与测试项目费用不一致。')</script>");
        //    return;
        //}

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        if (DropDownList4.SelectedValue == "1")
        {
            Random seed = new Random();
            Random randomNum = new Random(seed.Next());

            string shoufeiid = randomNum.Next().ToString() + DateTime.Now.ToString("yyyyMMdd_hhmmss");
            string sql2 = "update Anjianxinxi2 set bianhaoone='" + shoufeiid + "' where bianhao='" + bianhao + "'";
            SqlCommand com2 = new SqlCommand(sql2, con);
            com2.ExecuteNonQuery();
            con.Close();
            Response.Redirect("~/Income/InvoiceAdd.aspx?ran=" + shoufeiid + "&&bh=" + bianhao);
        }
        else
        {
            string jiuid = "";
            string sql = "select shoufeibianhao from ceshifeikf where bianhao='" + bianhao + "' and shoufeibianhao !=''";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                jiuid = dr["shoufeibianhao"].ToString();
            }
            dr.Close();
            con.Close();
            Response.Redirect("~/Income/InvoiceAdd2.aspx?invoiceid=" + jiuid);
        }
        con.Close();

    }
    private decimal sum1 = 0;
    private decimal sum2 = 0;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                int id = Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex].Value);
                string sql_claim = "select * from Claim where ceshifeikfid='" + id + "'";
                SqlCommand cmd_claim = new SqlCommand(sql_claim, con);
                SqlDataReader dr_claim = cmd_claim.ExecuteReader();
                if (dr_claim.Read())
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该记录业务已认领，不能进行删改操作。')</script>");
                    e.Row.Cells[this.GridView1.Columns.Count - 1].Text = string.Empty;
                }
                dr_claim.Close();
            }
        }
        if (e.Row.RowIndex >= 0)
        {
            string money = e.Row.Cells[4].Text;
            if (money == "" || money == "&nbsp;")
            {
                money = "0";
            }
            sum1 += Convert.ToDecimal(money);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "小计：";
            e.Row.Cells[4].Text = sum1.ToString();
            e.Row.Cells[2].ForeColor = Color.Blue;
            e.Row.Cells[4].ForeColor = Color.Blue;
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "update baojiacpxiangmu set hesuanbiaozhi='否',tijiaohaoma='' where baojiaid='" + baojiaid + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();

        dr.Close();
        con.Close();
        bind();
        Bind2();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox cb = (CheckBox)e.Row.FindControl("CheckBox1");
            cb.AutoPostBack = true;
            cb.CheckedChanged += new EventHandler(CheckBox2_CheckedChanged);
        }
    }

    protected void btn_affirm_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string edit_ceshifeikf = "update CeShiFeiKf set affirm='是' where taskid='" + taskno + "'";
            SqlCommand cmd = new SqlCommand(edit_ceshifeikf, con);
            int num = cmd.ExecuteNonQuery();
            if (num > 0)
            {
                ld.Text = "<script>alert('已确认')</script>";
            }
            else
            {
                ld.Text = "<script>alert('确认失败、请联系管理员！')</script>";
            }
        }
    }

    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[6].Text == "&nbsp;" || string.IsNullOrEmpty(e.Row.Cells[6].Text) || e.Row.Cells[6].Text == null)
            {
                e.Row.Cells[6].Text = "0";
            }
        }
    }

    protected void btn_account_Click(object sender, EventArgs e)
    {
        //1.检测费之和大于零、则不允许调账
        //2.一个合同里必须要有一条认领记录才能调账
        //3.已认领过的核算费用记录不能调账
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            List<string> listids = new List<string>();//存储选中的
            #region  统计检测费
            {
                decimal sum = 0m;//检测费总和
                foreach (GridViewRow item in this.GridView1.Rows)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chk_account");
                    if (chk.Checked)
                    {
                        listids.Add(this.GridView1.DataKeys[item.RowIndex].Value.ToString());
                        if (item.Cells[5].Text == "检测费")
                        {
                            if (string.IsNullOrEmpty(item.Cells[4].ToString()))
                            {

                            }
                            else
                            {
                                sum += Convert.ToDecimal(item.Cells[4].Text);
                            }
                        }
                    }
                }
                if (sum > 0)
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('调账所选的检测费之和不能大于0')</script>");
                    return;
                }
            }
            #endregion

            #region 检查该合同是否有认领记录
            {
                string baojiaid = this.GridView4.Rows[0].Cells[0].Text.ToString();
                string sqlbaojiabiao = "select * from Claim where ceshifeikfid in (select id from CeShiFeiKf where baojiaid='" + baojiaid + "')";
                SqlCommand commandbaojiabiao = new SqlCommand(sqlbaojiabiao, con);
                SqlDataReader drbaojiabiao = commandbaojiabiao.ExecuteReader();
                if (drbaojiabiao.Read())
                {
                    drbaojiabiao.Close();
                }
                else
                {
                    drbaojiabiao.Close();
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该合同没有业务认领记录、不能调账')</script>");
                    return;
                }
            }
            #endregion

            #region 检查该核算记录是否已认领
            {
                for (int i = 0; i < listids.Count; i++)
                {
                    string sqlclaim = "select ceshifeikfid from Claim where ceshifeikfid in (select id from CeShiFeiKf where id='" + listids[i] + "')";
                    SqlCommand commandclaim = new SqlCommand(sqlclaim, con);
                    SqlDataReader drclaim = commandclaim.ExecuteReader();
                    if (drclaim.Read())
                    {
                        drclaim.Close();
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('所选调账的测试项目已有认领记录')</script>");
                        return;
                    }
                    drclaim.Close();
                }
            }
            #endregion
        }
        Account();
    }

    /// <summary>
    /// 调账
    /// </summary>
    private void Account()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            Random random = new Random();
            string batch = DateTime.Now.ToString("yyyy_MM_dd hms") + random.Next(1000 + DateTime.Now.Second);
            List<string> listsql = new List<string>();
            foreach (GridViewRow item in this.GridView1.Rows)
            {
                CheckBox chk = item.FindControl("chk_account") as CheckBox;
                if (chk.Checked)
                {
                    string id = this.GridView1.DataKeys[item.RowIndex].Value.ToString();
                    decimal money = Convert.ToDecimal(item.Cells[4].Text);
                    listsql.Add("(0," + money + "," + id + ",'" + Session["Username"].ToString() + "','" + DateTime.Now + "','是','" + Session["Username"].ToString() + "','" + DateTime.Now + "','是','系统生成','" + DateTime.Now + "','否','" + batch + "','',''),");
                }
            }

            string sqlvalues = "";
            for (int i = 0; i < listsql.Count; i++)
            {
                sqlvalues += listsql[i] + "  ";
            }

            string sql = "insert into Claim  values" + sqlvalues.Substring(0, sqlvalues.Trim().Length - 1);
            SqlCommand command = new SqlCommand(sql, con);
            int number = command.ExecuteNonQuery();
            if (number > 0)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('调账成功')</script>");
            }
        }
    }
}
