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

public partial class Quotation_QuotationAdd1 : System.Web.UI.Page
{

    protected string kehuid = "";
    protected DateTime d;
    protected string baojiaid = "";
    protected string baojiaid1 = "";
    protected string chanpingid = "";
    protected string tijiaobiaozhi = "0";
    protected string shenpibiaozhi = "0";
    protected string huiqianbiaozhi = "0";
    protected string fast = "0";
    protected string idf = "";

    protected string name = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        name = Session["UserName"].ToString();
        baojiaid = Text50.Value.Trim();
        if (Request.QueryString["idf"] != null)
        {
            idf = Request.QueryString["idf"].ToString();
        }
        d = DateTime.Now;
        Label1.Text = Request.QueryString["kehuid"].ToString();
        kehuid = Request.QueryString["kehuid"].ToString();
        GridView1.Attributes.Add("style", "table-layout:fixed");
        GridView2.Attributes.Add("style", "table-layout:fixed");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        //string sqlk = "select class from customer where kehuid='" + Label1.Text + "'";
        string sqlk = "select * from CustomerLinkMan where customerid='" + Label1.Text + "' and delete_biaozhi !='是'";
        SqlCommand cmdk = new SqlCommand(sqlk, con);
        SqlDataReader drk = cmdk.ExecuteReader();
        if (drk.Read())
        {

        }
        else
        {
            con.Close();
            Response.Redirect("~/Customer/Welcome.aspx?id=LinkMan");
        }
        drk.Close();

        if (Request.QueryString["baojiaid"] != null)
        {
            baojiaid = Request.QueryString["baojiaid"].ToString();
        }
        else
        {
            Panel1.Visible = true;
        }
        if (Request.QueryString["fast"] != null)
        {
            fast = Request.QueryString["fast"].ToString();
        }
        string sql = "select * from baojiabiao where baojiaid='" + baojiaid + "'";
        SqlCommand com = new SqlCommand(sql, con);
        SqlDataReader dr = com.ExecuteReader();
        if (dr.Read())
        {

            tijiaobiaozhi = dr["tijiaobiaozhi"].ToString();
            shenpibiaozhi = dr["shenpibiaozhi"].ToString();
            huiqianbiaozhi = dr["huiqianbiaozhi"].ToString();
            //Calculate();
        }

        dr.Close();
        con.Close();
        if (!IsPostBack)
        {
            BindDep();
            Bind();
            BindClause();
            BindBaoJiaBiao();
            DataBind();
            BindChanPin();
            BankAccount();
        }
        if (tijiaobiaozhi == "是")
        {
            Button1.Enabled = false;
            Button2.Enabled = false;
            if (shenpibiaozhi != "通过")
            {
                Button3.Enabled = false;
                Button10.Visible = false;
            }
        }
        else
        {
            Button3.Enabled = true;
            // Button10.Visible = true;
        }
        Random rd = new Random();
        string qz = DateTime.Now.ToString() + rd.Next(1000);
        // this.Button10.Attributes["onclick"] = "window.showModalDialog('Default2.aspx?kehuid=" + kehuid + "&baojiaid=" + baojiaid + "&dd=" + qz + "&cp=" + DropDownList3.SelectedValue + "','test','dialogWidth=1400px;help:no;resizable:yes; dialogTop:100px;');";

        //this.Button10.Attributes["onclick"] = "window.open('Default2.aspx?kehuid=" + kehuid + "&baojiaid=" + baojiaid + "&dd=" + qz + "&cp=" + DropDownList3.SelectedValue + "','test','dialogWidth=1400px;help:no;resizable:yes; dialogTop:100px;');";
        if (DropDownList1.SelectedValue == "")
        {
            Response.Redirect("~/Customer/Welcome.aspx?id=rr");
        }
    }

    protected void BindDep()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select name from UserDepa where name in(select department from DepartmentType where Type='报价外包部门')";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        this.DropDownList_xiangmubumen.DataSource = ds.Tables[0];

        DropDownList_xiangmubumen.DataTextField = "name";
        DropDownList_xiangmubumen.DataValueField = "name";
        DropDownList_xiangmubumen.DataBind();



        con.Close();
    }

    protected void BindChanPin1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from BaoJiaChanPing where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "' and baojiaid !=''";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);


        DropDownList3.DataSource = ds.Tables[0];
        DropDownList3.DataTextField = "name";
        DropDownList3.DataValueField = "id";
        DropDownList3.DataBind();



        con.Close();
    }
    protected void Bind()
    {
        ld.Text = "";
        BindChanPin();
        BindXiangMu();
        BindShenPi();
        BindContract();
        BindChanPin1();
    }
    public void BindContract()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CustomerContract where  kehuid='" + kehuid + "' ";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView4.DataSource = ds.Tables[0];
        GridView4.DataBind();



        con.Close();
        con.Dispose();


    }


    #region 绑定报价基本信息
    protected void BindBaoJiaBiao()
    {

        string strs = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql21 = "select * from CustomerLinkMan where  customerid='" + kehuid + "' and delete_biaozhi !='是'";
        SqlDataAdapter ad21 = new SqlDataAdapter(sql21, con);
        DataSet ds21 = new DataSet();
        ad21.Fill(ds21);
        DropDownList1.DataSource = ds21.Tables[0];
        DropDownList1.DataTextField = "name";
        DropDownList1.DataValueField = "id";
        DropDownList1.DataBind();

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
            DropDownList1.SelectedValue = linkman;

        }


        string sql = "select * from baojiabiao where baojiaid='" + baojiaid + "' and baojiaid !=''";
        SqlCommand com = new SqlCommand(sql, con);
        SqlDataReader dr = com.ExecuteReader();
        if (dr.Read())
        {
            strs = dr["clause"].ToString();
            DropDownList2.SelectedValue = dr["zhangdan"].ToString();
            Calculate();
            baojiabeizhu_txt.Text = dr["beizhu"].ToString();

        }
        dr.Close();

        string sql2 = "select * from Provision2 where baojiaid='" + baojiaid + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            TextBox1.Text = dr2["remark"].ToString();
        }

        dr2.Close();



        string sqlq = "select * from BaoJiaChanPing where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "' and baojiaid !=''";
        SqlDataAdapter ad = new SqlDataAdapter(sqlq, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);


        DropDownList3.DataSource = ds.Tables[0];
        DropDownList3.DataTextField = "name";
        DropDownList3.DataValueField = "id";
        DropDownList3.DataBind();



        con.Close();



        string[] strtemp = strs.Split('|');
        foreach (string str in strtemp)
        {
            for (int i = 1; i < CheckBoxList9.Items.Count + 1; i++)
            {
                if (this.CheckBoxList9.Items[i - 1].Text == str)
                {
                    this.CheckBoxList9.Items[i - 1].Selected = true;
                }
            }
        }
    }
    #endregion

    #region 绑定条款
    protected void BindClause()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sqltiaokuan = "select * from Clause2 where banben='" + drop_language.SelectedValue + "'";
        SqlDataAdapter ad2 = new SqlDataAdapter(sqltiaokuan, con);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);

        CheckBoxList9.DataSource = ds2.Tables[0];

        CheckBoxList9.DataTextField = "neirong";
        CheckBoxList9.DataValueField = "neirong"; ;
        CheckBoxList9.DataBind();
        con.Close();
        //默认选中电子电器的条款
        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
        {
            if (ds2.Tables[0].Rows[i]["bumen"].ToString() == "电子电器")
            {
                CheckBoxList9.Items[i].Selected = true;
            }
            if (ds2.Tables[0].Rows[i]["bumen"].ToString() == "默认")
            {
                CheckBoxList9.Items[i].Enabled = false;
                CheckBoxList9.Items[i].Selected = true;
            }
        }
    }
    #endregion

    #region 绑定审批信息
    protected void BindShenPi()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from Approval where bianhao='" + baojiaid + "' order by id desc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView3.DataSource = ds.Tables[0];
        GridView3.DataBind();

        con.Close();
    }
    #endregion


    #region 绑定产品列表
    protected void BindChanPin()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from BaoJiaChanPing where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "' and baojiaid !='' order by id asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        //外包项目，产品下拉框
        this.drop_CP.DataTextField = "name";
        this.drop_CP.DataValueField = "id";
        this.drop_CP.DataSource = ds.Tables[0];
        this.drop_CP.DataBind();

        string sql2 = "select * from dd";
        SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);
        con.Close();
    }
    #endregion

    #region 删除产品
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql3 = "select * from anjianxinxi3 where xiangmubianhao in (select id from BaoJiaCPXiangMu where CPid='" + id + "')";
        SqlCommand cmd3 = new SqlCommand(sql3, con);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {
            con.Close();
        }
        else
        {
            dr3.Close();
            string sql = "delete from BaoJiaChanPing where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            string sql2 = "delete from BaoJiaCPXiangMu where CPid='" + id + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.ExecuteNonQuery();

        }
        con.Close();
        Calculate();
        //upbaojia();

        Bind();
        //dcount();
        Panel1.Visible = true;

    }
    #endregion

    #region 产生报价编号
    //***********************2019-7-31
    protected string MakeBaojiaid()
    {
        string sql = "select top 1 BaoJiaId from [dbo].[BaoJiaBiao] where  (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by  SUBSTRING(BaoJiaId,3,10) desc";
        string str = "";
        string year;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            //判断用户是福永还是龙华地区的人
            string homelocation = "";
            string sql_home = "select [homelocation] from [dbo].[UserInfo] where UserName='" + Session["Username"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql_home, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                homelocation = dr["homelocation"].ToString();
                dr.Close();
            }
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                //当数据库里还没有编号时
                year = DateTime.Now.Year.ToString();
                str = homelocation + year.Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + "000001";
            }
            else
            {
                year = DateTime.Now.Year.ToString();
                string id = ds.Tables[0].Rows[0][0].ToString();
                string nian = id.Substring(2, 2);
                string yue = id.Substring(4, 2);
                string bianhao = id.Substring(6, 6);
                if (yue == DateTime.Now.Month.ToString().PadLeft(2, '0') && nian == year.Substring(2, 2))
                {
                    int i = Convert.ToInt32(bianhao);
                    i++;
                    str = homelocation + year.Substring(2, 2) + yue + i.ToString().PadLeft(6, '0');
                }
                else
                {
                    str = homelocation + year.Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + "000001";
                }
            }
        }
        return str;
    }
    #endregion

    #region 保存产品
    protected void Button2_Click(object sender, EventArgs e)
    {

        if (Text50.Value.Trim() == "")
        {
            ld.Text = "<script>alert('您未点开始报价，请关闭该页面后重新打开报价页面!');window.close();</script>";
        }
        else if (string.IsNullOrEmpty(cpname_txt.Text.Trim()))
        {
            ld.Text = "<script>alert('产品名称不能存在空值。');</script>";
        }
        else
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            Button11.Visible = false;
            //string sql2 = "select * from BaoJiaChanPing where baojiaid='" + baojiaid + "'";
            //SqlCommand cmd2 = new SqlCommand(sql2, con);
            //SqlDataReader dr2 = cmd2.ExecuteReader();
            //if (dr2.Read())
            //{
            //    dr2.Close();
            //}
            //else
            //{

            //    dr2.Close();
            //    string sql = "insert into BaoJiaChanPing values('" + baojiaid + "','" + kehuid + "','" + cpname_txt.Text.Trim() + "','" + cpxinghao_txt.Text.Trim() + "','" + cpbeizhu_txt.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";
            //    SqlCommand cmd = new SqlCommand(sql, con);
            //    cmd.ExecuteNonQuery();
            //}
            string sql = "insert into BaoJiaChanPing values('" + baojiaid + "','" + kehuid + "','" + cpname_txt.Text.Trim() + "','" + cpxinghao_txt.Text.Trim() + "','" + cpbeizhu_txt.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            //upbaojia();
            Bind();

            BindDropCp();//绑定修改项目时的产品信息

            //this.cpbeizhu_txt.Text = string.Empty;
            //this.cpname_txt.Text = string.Empty;
            //this.cpxinghao_txt.Text = string.Empty;
            //Panel1.Visible = false;
        }
    }

    /// <summary>
    /// 查询产品用于修改项目信息
    /// </summary>
    private void BindDropCp()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select id,name from BaoJiaChanPing  where BaoJiaID='" + baojiaid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.dropUpdatecp.DataSource = ds.Tables[0];
            this.dropUpdatecp.DataTextField = "name";
            this.dropUpdatecp.DataValueField = "id";
            this.dropUpdatecp.DataBind();
        }
    }
    #endregion

    #region 绑定测试项目列表

    protected void BindXiangMu()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *, (feiyong*zhekou) as feiyong2 from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "' and baojiaid !='' order by id asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();

        con.Close();
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView2.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql3 = "select * from anjianxinxi3 where xiangmubianhao in (select id from BaoJiaCPXiangMu where id='" + id + "')";
        SqlCommand cmd3 = new SqlCommand(sql3, con);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {
            con.Close();
        }
        else
        {
            dr3.Close();
            string sql = "delete from BaoJiaCPXiangMu where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }


        con.Close();

        Calculate();
        //upbaojia();

        Bind();
        //   dcount();
    }

    #endregion

    #region 保存测试项目
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Calculate();
            Bind();
            csbz_txt.Text = "";
            csbeizhu_txt.Text = "";
            csyp_txt.Text = "";
            cszq_txt.Text = "";
            csnr_txt.Text = "";
            csjs_txt.Text = "";
            csfy_txt.Text = "";
            cszk_txt.Text = "1";
        }
        catch (Exception ex)
        {
            Console.WriteLine("Page:" + ex.Message);
        }
    }
    #endregion

    #region 保存报价单
    protected void Button3_Click(object sender, EventArgs e)
    {
        Calculate();
        //计算总价格
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        decimal price_sum = 0;
        string sql_sum = "select sum(total) as total from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' ";
        SqlCommand cmd_sum = new SqlCommand(sql_sum, con);
        SqlDataReader dr_sum = cmd_sum.ExecuteReader();
        if (dr_sum.Read())
        {
            if (dr_sum["total"] == DBNull.Value)
            {
                //当总价格为NULL、也就代表该报价没有测试项目
                ld.Text = "<script>alert('请增加测试项目')</script>";
                con.Close();
                return;
            }
            else
            {
                price_sum = Convert.ToDecimal(dr_sum["total"]);
            }
            dr_sum.Close();
        }
        else
        {
            //当总价格为空、也就代表该报价没有测试项目
            ld.Text = "<script>alert('请增加测试项目')</script>";
            con.Close();
            return;
        }

        if (Request.QueryString["baojiaid"] == null)
        {
            if (Text50.Value.Trim() != "")
            {
                //如果选择首款方式、则必须输入首款金额
                string bankaccout = "";
                if (drop_way.SelectedValue == "全款")
                {
                    bankaccout = "全款";
                }
                else
                {
                    bankaccout = "首款";
                    bool ifleagemoney = true;
                    try
                    {
                        Convert.ToDecimal(txt_drow.Text.Trim());
                    }
                    catch
                    {
                        ifleagemoney = false;
                    }
                    if ((string.IsNullOrEmpty(txt_drow.Text.Trim())) || (!ifleagemoney))
                    {
                        ld.Text = "<script>alert('首款金额不能为空,且数据需为数值')</script>";
                        return;
                    }
                }
                // string caiwu = ISWaibao();//标识该报价是否含有外包项目
                string sql2c = "select * from BaoJiaChanPing where baojiaid='" + baojiaid + "'";
                SqlCommand cmd2c = new SqlCommand(sql2c, con);
                SqlDataReader dr2c = cmd2c.ExecuteReader();
                if (dr2c.Read())
                {
                    dr2c.Close();
                    baojiaid = MakeBaojiaid();
                    string sql4 = "";
                    int xuhao = 0;
                    string tiaokuan = "";
                    for (int i = 1; i < CheckBoxList9.Items.Count + 1; i++)
                    {
                        if (CheckBoxList9.Items[i - 1].Selected)
                        {
                            xuhao++;

                            string xuhaos = xuhao.ToString();
                            string xuhaosh = xuhaos;

                            tiaokuan += CheckBoxList9.Items[i - 1].Text.ToString() + "|";
                            sql4 = "insert into Clause values('(" + xuhaosh + ")','" + CheckBoxList9.Items[i - 1].Text.ToString() + "','" + baojiaid + "')";
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

                    //如果是销售助理做合同responser则不取session
                    string dutyname = Dutyname();
                    string responer = "";
                    if (dutyname.Trim() == "销售助理")
                    {
                        string sql_select = "select responser from Customer_Sales where customerid='" + kehuid + "'";
                        SqlCommand cmd_select = new SqlCommand(sql_select, con);
                        SqlDataReader dr_select = cmd_select.ExecuteReader();
                        if (dr_select.Read())
                        {
                            responer = dr_select["responser"].ToString();
                        }
                        else
                        {
                            ld.Text = "<script>alert('保存失败、客户归属人存在异常请联系人管理员。')</script>";
                            return;
                        }
                        dr_select.Close();
                    }
                    else
                    {
                        responer = Session["Username"].ToString();
                    }

                    string sql = "";
                    //当项目全部为外包项目时，则不算则扣和标准价
                    decimal finalprice = 0.00m;

                    //扩展费
                    decimal kuozhanfei = 0;
                    if (string.IsNullOrEmpty(this.txt_kuozhanfei.Text))
                    {

                    }
                    else
                    {
                        kuozhanfei = Convert.ToDecimal(this.txt_kuozhanfei.Text);
                    }

                    if (!string.IsNullOrEmpty(TextBox_finalprice.Text))
                    {
                        finalprice = Convert.ToDecimal(TextBox_finalprice.Text);
                    }
                    if (string.IsNullOrEmpty(baojiazong_txt.Text.Trim()))
                    {
                        //判断是全款还是首款
                        if (bankaccout == "全款")
                        {
                            sql = "insert into BaoJiaBiao values('" + baojiaid + "','" + kehuid + "','" + Convert.ToDecimal(realzhekou_txt.Text) + "','" + price_sum + "','否','1900-1-1','否','否','1900-1-1','other','" + baojiabeizhu_txt.Text.Trim() + "','" + tiaokuan + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responer + "','" + weituo.Text.Trim() + "','" + Convert.ToDecimal(realzhekou_txt.Text.Trim()) + "','" + DropDownList2.SelectedValue + "','否','" + fast + "','" + this.TextBox_epibolypricetotal.Text + "','全款','0.00','" + drop_vat.SelectedValue + "','" + finalprice + "','" + drop_currency.SelectedValue + "','" + drop_language.SelectedValue + "','否',''," + kuozhanfei + ")";
                        }
                        else
                        {
                            sql = "insert into BaoJiaBiao values('" + baojiaid + "','" + kehuid + "','" + Convert.ToDecimal(realzhekou_txt.Text) + "','" + price_sum + "','否','1900-1-1','否','否','1900-1-1','other','" + baojiabeizhu_txt.Text.Trim() + "','" + tiaokuan + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responer + "','" + weituo.Text.Trim() + "','" + Convert.ToDecimal(realzhekou_txt.Text.Trim()) + "','" + DropDownList2.SelectedValue + "','否','" + fast + "','" + this.TextBox_epibolypricetotal.Text + "','首款','" + txt_drow.Text.Trim() + "','" + drop_vat.SelectedValue + "','" + finalprice + "','" + drop_currency.SelectedValue + "','" + drop_language.SelectedValue + "','否',''," + kuozhanfei + ")";
                        }
                    }
                    else
                    {
                        if (bankaccout == "全款")
                        {
                            sql = "insert into BaoJiaBiao values('" + baojiaid + "','" + kehuid + "','" + Convert.ToDecimal(realzhekou_txt.Text) + "','" + price_sum + "','否','1900-1-1','否','否','1900-1-1','other','" + baojiabeizhu_txt.Text.Trim() + "','" + tiaokuan + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responer + "','" + weituo.Text.Trim() + "','" + Convert.ToDecimal(realzhekou_txt.Text.Trim()) + "','" + DropDownList2.SelectedValue + "','否','" + fast + "','" + this.TextBox_epibolypricetotal.Text + "','全款','0.00','" + drop_vat.SelectedValue + "','" + finalprice + "','" + drop_currency.SelectedValue + "','" + drop_language.Text + "','否',''," + kuozhanfei + ")";
                        }
                        else
                        {
                            sql = "insert into BaoJiaBiao values('" + baojiaid + "','" + kehuid + "','" + Convert.ToDecimal(realzhekou_txt.Text) + "','" + price_sum + "','否','1900-1-1','否','否','1900-1-1','other','" + baojiabeizhu_txt.Text.Trim() + "','" + tiaokuan + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responer + "','" + weituo.Text.Trim() + "','" + Convert.ToDecimal(realzhekou_txt.Text.Trim()) + "','" + DropDownList2.SelectedValue + "','否','" + fast + "','" + this.TextBox_epibolypricetotal.Text + "','首款','" + txt_drow.Text.Trim() + "','" + drop_vat.SelectedValue + "','" + finalprice + "','" + drop_currency.SelectedValue + "','" + drop_language.SelectedValue + "','否',''," + kuozhanfei + ")";
                        }
                    }
                    string sql2 = "update BaoJiaChanPing set baojiaid ='" + baojiaid + "' where baojiaid='" + Text50.Value.Trim() + "'";
                    string sql3 = "update BaoJiaCPXiangMu set baojiaid ='" + baojiaid + "' where baojiaid='" + Text50.Value.Trim() + "'";
                    string sql44 = "insert into Provision2 values('" + baojiaid + "','" + TextBox1.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";
                    string sql5 = "insert into baojialink values('" + baojiaid + "','" + DropDownList1.SelectedValue + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";

                    SqlCommand myComm = new SqlCommand();
                    SqlTransaction myTran;
                    myTran = con.BeginTransaction();
                    try
                    {
                        myComm.Connection = con;
                        myComm.Transaction = myTran;

                        myComm.CommandText = sql;
                        myComm.ExecuteNonQuery();//更新数据

                        myComm.CommandText = sql2;
                        myComm.ExecuteNonQuery();

                        myComm.CommandText = sql3;
                        myComm.ExecuteNonQuery();

                        myComm.CommandText = sql44;
                        myComm.ExecuteNonQuery();

                        myComm.CommandText = sql5;
                        myComm.ExecuteNonQuery();
                        //****************
                        //提交事务
                        myTran.Commit();
                        //2019-7-25新增  给外包审核表添加报价单



                        string sql85 = "select hesuanname from baojiacpxiangmu where baojiaid='" + baojiaid + "' and hesuanname='是'";
                        SqlCommand cmd85 = new SqlCommand(sql85, con);
                        SqlDataReader dr85 = cmd85.ExecuteReader();
                        if (dr85.Read())
                        {
                            dr85.Close();
                            string sqlshepi = "update baojiabiao set weituo='是' where baojiaid='" + baojiaid + "'";
                            SqlCommand cmdshenpi = new SqlCommand(sqlshepi, con);
                            cmdshenpi.ExecuteNonQuery();

                        }
                        else
                        {
                            dr85.Close();
                            string sqlshepi = "update baojiabiao set weituo='' where baojiaid='" + baojiaid + "'";
                            SqlCommand cmdshenpi = new SqlCommand(sqlshepi, con);
                            cmdshenpi.ExecuteNonQuery();
                        }
                    }
                    catch (Exception err)
                    {
                        myTran.Rollback();
                        con.Close();

                        throw new ApplicationException("事务操作出错，系统信息：" + err.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                    //    Label3.ForeColor = Color.Red;
                    Button3.Enabled = false;
                    Panel1.Visible = false;
                    ld.Text = "<script>alert('保存成功!');window.close();</script>";
                }
                else
                {
                    con.Close();
                    ld.Text = "<script>alert('您没有保存产品，请关闭该页面后重新打开报价页面!');window.close();</script>";
                }
            }
            else
            {
                ld.Text = "<script>alert('您未点开始报价，请关闭该页面后重新打开报价页面!');window.close();</script>";
            }
            //Response.Write("<script>alert('保存成功'); window.close();</script>");
            //ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "msg1", "alert('保存成功');location.replace('QuotationAdd.aspx?baojiaid=" + baojiaid + "&kehuid=" + kehuid + "');", true);
        }
    }


    #region 计算项目金额，只用一个，其他计算函数去除
    protected void Calculate()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select SUM(total) as shijizong,SUM(shuliang*yuanshi) as yuanshizong,SUM(epiboly_price) as waibaozong,epiboly from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' group by epiboly";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        //汇率
        decimal exchange = 0m;
        if (drop_currency.SelectedValue == "美元")
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

        con.Close();
        if (ds.Tables[0].Rows.Count > 0)
        {
            decimal feiwaibaozong = 0m;//不含外包实际报价
            decimal feiwaibaobiaozhun = 0m;//非外包标准总价
            decimal hanwaibaozong = 0m;//含外包实际总价
            decimal waibaobufen = 0m;//外包总价
            decimal biaozhun = 0m;//标准总价
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string rowtype = ds.Tables[0].Rows[i]["epiboly"].ToString();
                    if (rowtype == "外包")
                    {
                        hanwaibaozong += Convert.ToDecimal(ds.Tables[0].Rows[i]["shijizong"].ToString());
                        waibaobufen += Convert.ToDecimal(ds.Tables[0].Rows[i]["waibaozong"].ToString());
                        biaozhun += Convert.ToDecimal(ds.Tables[0].Rows[i]["yuanshizong"].ToString());
                    }
                    else
                    {
                        feiwaibaobiaozhun += Convert.ToDecimal(ds.Tables[0].Rows[i]["yuanshizong"].ToString());
                        feiwaibaozong += Convert.ToDecimal(ds.Tables[0].Rows[i]["shijizong"].ToString());
                        biaozhun += Convert.ToDecimal(ds.Tables[0].Rows[i]["yuanshizong"].ToString());
                    }
                }
            }
            this.baojiazhehou_txt.Text = feiwaibaozong.ToString("f2");
            this.baojiazong_txt.Text = biaozhun.ToString("f2");
            this.epiboly_price.Text = hanwaibaozong.ToString("f2");
            this.TextBox_epibolypricetotal.Text = waibaobufen.ToString("f2");
            decimal zhengdanzhekou = 0m;
            decimal totalmoney = Convert.ToDecimal(baojiazhehou_txt.Text) + Convert.ToDecimal(epiboly_price.Text);//报价总金额
            this.totalmoney_txt.Text = totalmoney.ToString();
            //无扩展费
            //1.有优惠金额
            //整单折扣=优惠后金额/标准总价
            //外包比例=外包总金额/优惠后金额
            //2.没有优惠金额
            //整单折扣=报价总金额/标准总价
            //外包比例=外包总金额/报价总金额

            //有扩展费
            //1.有优惠金额
            //整单折扣=（优惠后金额-扩展费）/标准总价
            //外包比例=外包总金额/（优惠后金额-扩展费）
            //2.没有优惠金额
            //整单折扣=（报价总金额-扩展费）/标准总价
            //外包比例=外包总金额/（报价总金额-扩展费）


            if (biaozhun == 0m || string.IsNullOrEmpty(biaozhun.ToString("f2")))
            {
                //无项目 
                zhengdanzhekou = 1m;
            }
            else if (!string.IsNullOrEmpty(TextBox_finalprice.Text) && TextBox_finalprice.Text != "0.00")
            {
                //存在优惠后金额

                //判断是否存在扩展费
                if (!string.IsNullOrEmpty(this.txt_kuozhanfei.Text) && this.txt_kuozhanfei.Text != "0.00")
                {
                    if (drop_currency.SelectedValue == "美元")
                    {
                        decimal fenmu = biaozhun / exchange;
                        zhengdanzhekou = (Convert.ToDecimal(TextBox_finalprice.Text) - Convert.ToDecimal(this.txt_kuozhanfei.Text)) / fenmu;
                    }
                    else
                    {
                        zhengdanzhekou = (Convert.ToDecimal(TextBox_finalprice.Text) - Convert.ToDecimal(this.txt_kuozhanfei.Text)) / biaozhun;
                    }
                }
                else
                {
                    if (drop_currency.SelectedValue == "美元")
                    {
                        decimal fenmu = biaozhun / exchange;
                        zhengdanzhekou = Convert.ToDecimal(TextBox_finalprice.Text) / fenmu;
                    }
                    else
                    {
                        zhengdanzhekou = Convert.ToDecimal(TextBox_finalprice.Text) / biaozhun;
                    }
                }
            }
            else
            {
                //不存在优惠后金额

                //是否存在扩展费
                if (!string.IsNullOrEmpty(this.txt_kuozhanfei.Text) && this.txt_kuozhanfei.Text != "0.00")
                {
                    if (drop_currency.SelectedValue == "美元")
                    {
                        decimal fenmu = biaozhun / exchange;
                        zhengdanzhekou = (Convert.ToDecimal(totalmoney_txt.Text) - Convert.ToDecimal(this.txt_kuozhanfei.Text)) / fenmu;
                    }
                    else
                    {
                        zhengdanzhekou = (Convert.ToDecimal(totalmoney_txt.Text) - Convert.ToDecimal(this.txt_kuozhanfei.Text)) / biaozhun;
                    }
                }
                else
                {
                    if (drop_currency.SelectedValue == "美元")
                    {
                        decimal fenmu = biaozhun / exchange;
                        zhengdanzhekou = Convert.ToDecimal(totalmoney_txt.Text) / fenmu;
                    }
                    else
                    {
                        zhengdanzhekou = Convert.ToDecimal(totalmoney_txt.Text) / biaozhun;
                    }
                }
            }
            this.realzhekou_txt.Text = zhengdanzhekou.ToString("f2");
        }
        else
        {
            baojiazhehou_txt.Text = string.Empty;
            baojiazong_txt.Text = string.Empty;
            epiboly_price.Text = string.Empty;
            TextBox_epibolypricetotal.Text = string.Empty;
            realzhekou_txt.Text = string.Empty;
            totalmoney_txt.Text = string.Empty;
        }
    }
    #endregion



    #region 产品项目编辑
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        Bind();
    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView2.EditIndex = e.NewEditIndex;
        Bind();
    }
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string KeyId = GridView2.DataKeys[e.RowIndex].Value.ToString();


        GridViewRow gvr = GridView2.Rows[e.RowIndex];
        string stranswer = ((TextBox)gvr.FindControl("TextBox1")).Text;


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string uuname1 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());
        // string uuname2 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());

        string uuname2 = stranswer;
        string uuname3 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
        string uuname4 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
        string uuname5 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
        string uuname6 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[6].Controls[0]).Text.ToString());
        // string uuname7 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[7].Controls[0]).Text.ToString());
        string uuname8 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[8].Controls[0]).Text.ToString());
        string uuname9 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[9].Controls[0]).Text.ToString());
        string uuname10 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[10].Controls[0]).Text.ToString());

        string sql2 = "select top 1 leibiename from Product2 where leibieid in (select daid from BaoJiaCPXiangMu where id='" + KeyId + "') and leibiename like 'ccc%'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            dr2.Close();
            //string sql = "update BaoJiaCPXiangMu set ceshiname='" + uuname1 + "',biaozhun='" + uuname2 + "',neirong='" + uuname3 + "',yp='" + uuname4 + "',zhouqi='" + uuname5 + "',shuliang='" + Convert.ToDecimal(uuname8) + "',beizhu='" + uuname9 + "',jishuyaoqiu='" + uuname10 + "' where id='" + KeyId + "'";
            //SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.ExecuteNonQuery();

            string sql = "update BaoJiaCPXiangMu set ceshiname='" + uuname1 + "',biaozhun='" + uuname2 + "',neirong='" + uuname3 + "',yp='" + uuname4 + "',zhouqi='" + uuname5 + "',feiyong='" + Convert.ToDecimal(uuname6) + "',shuliang='" + Convert.ToDecimal(uuname8) + "',beizhu='" + uuname9 + "',jishuyaoqiu='" + uuname10 + "' where id='" + KeyId + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

        }
        else
        {
            dr2.Close();
            string sql = "update BaoJiaCPXiangMu set ceshiname='" + uuname1 + "',biaozhun='" + uuname2 + "',neirong='" + uuname3 + "',yp='" + uuname4 + "',zhouqi='" + uuname5 + "',feiyong='" + Convert.ToDecimal(uuname6) + "',shuliang='" + Convert.ToDecimal(uuname8) + "',beizhu='" + uuname9 + "',jishuyaoqiu='" + uuname10 + "' where id='" + KeyId + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }


        decimal ticheng = 0;

        decimal fenmu = 0;
        decimal fenzi = 0;


        string sql3 = "select * from BaoJiaCPXiangMu where id='" + KeyId + "'";
        SqlCommand cmd3 = new SqlCommand(sql3, con);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {
            fenmu = Convert.ToDecimal(dr3["yuanshi"]);
            fenzi = Convert.ToDecimal(dr3["feiyong"]);
        }
        if (fenmu != 0)
        {
            ticheng = fenzi / fenmu;
        }
        dr3.Close();

        if (ticheng <= Convert.ToDecimal(0.85))
        {
            string sql4 = "update BaoJiaCPXiangMu set hesuanname='是' where id='" + KeyId + "'";
            SqlCommand cmd4 = new SqlCommand(sql4, con);
            cmd4.ExecuteNonQuery();

        }
        else
        {
            string sql4 = "update BaoJiaCPXiangMu set hesuanname='' where id='" + KeyId + "'";
            SqlCommand cmd4 = new SqlCommand(sql4, con);
            cmd4.ExecuteNonQuery();
        }
        string sqltic = "update baojiacpxiangmu set zhekou='" + Convert.ToDecimal(ticheng) + "' where id='" + KeyId + "'";
        SqlCommand cmdti = new SqlCommand(sqltic, con);
        cmdti.ExecuteNonQuery();
        con.Close();
        GridView2.EditIndex = -1;
        Bind();

        upbaojia();
        Calculate();
        //dcount();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Bind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView1.EditIndex = e.NewEditIndex;
        Panel1.Visible = false;
        Bind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string KeyId = GridView1.DataKeys[e.RowIndex].Value.ToString();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string uuname1 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
        string uuname2 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
        string uuname3 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
        if (string.IsNullOrEmpty(uuname1))
        {
            ld.Text = "<script>alert('产品名称不能为空')</script>";
        }
        else
        {
            string sql = "update BaoJiaChanPing set name='" + uuname1 + "',type='" + uuname2 + "',beizhu='" + uuname3 + "' where id='" + KeyId + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            // upbaojia();
            this.cpbeizhu_txt.Text = string.Empty;
            this.cpname_txt.Text = string.Empty;
            this.cpxinghao_txt.Text = string.Empty;
            Bind();
        }
    }
    #endregion

    #region 后续判定控件是否应该删除
    protected void Button6_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        DataSet ds = new DataSet();

        string sql = "";

        sql = "select *  from product2 where neirong like '" + cs_txt.Text.Replace(",", "").Trim() + "%' order by name";




        SqlDataAdapter da = new SqlDataAdapter(sql, con);

        da.Fill(ds);

        csbz_txt.Text = ds.Tables[0].Rows[0]["biaozhun"].ToString();
        csfy_txt.Text = ds.Tables[0].Rows[0]["shoufei"].ToString();
        cszq_txt.Text = ds.Tables[0].Rows[0]["zhouqi"].ToString();
        csyp_txt.Text = ds.Tables[0].Rows[0]["yp"].ToString();
        csbeizhu_txt.Text = ds.Tables[0].Rows[0]["beizhu"].ToString();
        con.Close();
    }
    #endregion


    protected void upbaojia()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql3 = "";



        string sqlb21 = "update Customer set bianhao='" + DateTime.Now.ToShortDateString() + "' where kehuid='" + kehuid + "'";
        SqlCommand cmdb21 = new SqlCommand(sqlb21, con);
        cmdb21.ExecuteNonQuery();


        if (idf == "")
        {
            sql3 = "update baojiabiao set shenpibiaozhi='否',tijiaobiaozhi='否',tijiaotime='',huiqianbiaozhi='否',huiqiantime='' where  baojiaid ='" + baojiaid + "'";
            SqlCommand cmd = new SqlCommand(sql3, con);
            cmd.ExecuteNonQuery();




            string sql85 = "select hesuanname from baojiacpxiangmu where baojiaid='" + baojiaid + "' and hesuanname='是'";
            SqlCommand cmd85 = new SqlCommand(sql85, con);
            SqlDataReader dr85 = cmd85.ExecuteReader();
            if (dr85.Read())
            {
                dr85.Close();
                string sqlshepi = "update baojiabiao set weituo='是' where baojiaid='" + baojiaid + "'";
                SqlCommand cmdshenpi = new SqlCommand(sqlshepi, con);
                cmdshenpi.ExecuteNonQuery();

            }
            else
            {
                dr85.Close();
                string sqlshepi = "update baojiabiao set weituo='' where baojiaid='" + baojiaid + "'";
                SqlCommand cmdshenpi = new SqlCommand(sqlshepi, con);
                cmdshenpi.ExecuteNonQuery();
            }


            con.Close();
        }
        else
        {

            con.Close();
        }
    }

    Dictionary<int, string> dic = new Dictionary<int, string>();
    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            dic.Add(e.Row.RowIndex, e.Row.Cells[1].Text);
            Session["Dictionary"] = dic;
        }
    }

    /// <summary>
    /// 动态注册的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Btnproject_Click(object sender, EventArgs e)
    {
        //int i = Convert.ToInt32(((Button)sender).ID);
        //Button box = this.GridView1.Rows[i].Cells[6].FindControl(i.ToString()) as Button;
        //box.Enabled = true;
        try
        {
            Calculate();
            Bind();
            csbz_txt.Text = "";
            csbeizhu_txt.Text = "";
            csyp_txt.Text = "";
            cszq_txt.Text = "";
            csnr_txt.Text = "";
            csjs_txt.Text = "";
            csfy_txt.Text = "";
            cszk_txt.Text = "1";
        }
        catch (Exception ex)
        {
            Console.WriteLine("Page:" + ex.Message);
        }
    }

    protected void GridView2_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string cpid = e.Row.Cells[11].Text;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = "select name,type from BaoJiaChanPing where id=" + cpid + " and KeHuId='" + kehuid + "' and BaoJiaID='" + baojiaid + "'";
                SqlCommand com = new SqlCommand(sql, con);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    e.Row.Cells[12].Text = dr["name"].ToString();
                    e.Row.Cells[13].Text = dr["type"].ToString();
                }
                dr.Close();
            }
        }
    }
    #region 修改测试项目信息
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        #region
        //数量价格不能为空   外包价格不能高于实际费用
        if (txt_nums.Text.Trim() == "0" || string.IsNullOrEmpty(txt_nums.Text) || txt_nums.Text.Trim() == "0.00")
        {
            ld.Text = "<script>alert('数量输入不合法,请重新修改！')</script>";
        }
        else if (txt_price.Text.Trim() == "0" || string.IsNullOrEmpty(txt_price.Text))
        {
            ld.Text = "<script>alert('价格输入不合法,请重新修改！')</script>";
        }
        else if (Convert.ToDecimal(TextBox_epiboly_price.Text) > Convert.ToDecimal(txt_price.Text))
        {
            ld.Text = "<script>alert('外包价格不能高于实际费用,请重新修改！')</script>";
        }
        else
        {

            string KeyId = this.txt_idupdate.Text.Trim().ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            //单个项目于的折扣
            decimal zhekou = 0m;
            string sql_zhekou = "select yuanshi from BaoJiaCPXiangMu where id='" + KeyId + "'";
            SqlCommand cmd_zhekou = new SqlCommand(sql_zhekou, con);
            SqlDataReader dr_zhekou = cmd_zhekou.ExecuteReader();
            if (dr_zhekou.Read())
            {
                zhekou = Convert.ToDecimal(txt_price.Text) / Convert.ToDecimal(dr_zhekou[0]);
            }
            dr_zhekou.Close();

            string sql = "update BaoJiaCPXiangMu set zhekou='" + zhekou + "',yp='" + this.txt_specimen.Text.ToString() + "',zhouqi='" + this.txt_period.Text.ToString() + "',feiyong='" + Convert.ToDecimal(this.txt_price.Text.Trim()) + "',shuliang='" + Convert.ToDecimal(this.txt_nums.Text.Trim()) + "',beizhu='" + this.txt_remark.Text.ToString() + "',epiboly='" + DropDownList_epiboly.SelectedValue + "',epiboly_price='" + Convert.ToDecimal(this.TextBox_epiboly_price.Text.Trim()) + "',cpid='" + dropUpdatecp.SelectedValue + "' where id='" + KeyId + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            con.Close();
            GridView2.EditIndex = -1;
            Bind();
            // upbaojia();
            Calculate();
        }
        #endregion
    }
    #endregion



    /// <summary>
    ///删除测试项目
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region 删除测试项目
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();

        //删除
        if (e.CommandName == "btn_Delete")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql3 = "select * from anjianxinxi3 where xiangmubianhao in (select id from BaoJiaCPXiangMu where id='" + id + "')";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            if (dr3.Read())
            {
                con.Close();
            }
            else
            {
                dr3.Close();
                string sql = "delete from BaoJiaCPXiangMu where id='" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            Calculate();
            upbaojia();
            Bind();
        }

        if (e.CommandName == "copy")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = @"insert into BaoJiaCPXiangMu([baojiaid],[kehuid],[cpid],[ceshiname],[biaozhun],[neirong],[yp],[feiyong],[zhekou],[shuliang],
                                [beizhu],[fillname],[filltime],[responser],[zhouqi],[tijiaobiaozhi],[tijiaoname],[tijiaotime],[tijiaohaoma],[jishuyaoqiu],
                                [daid],[zhongid],[xiaoid],[yuanshi],[hesuanbiaozhi],[hesuantime],[hesuanname],[epiboly],[jigou],[bumen],[epiboly_Price]) 
                                select [baojiaid],[kehuid],[cpid],[ceshiname],[biaozhun],[neirong],[yp],[feiyong],[zhekou],[shuliang],
                                [beizhu],[fillname],[filltime],[responser],[zhouqi],[tijiaobiaozhi],[tijiaoname],[tijiaotime],[tijiaohaoma],[jishuyaoqiu],
                                [daid],[zhongid],[xiaoid],[yuanshi],[hesuanbiaozhi],[hesuantime],[hesuanname],[epiboly],[jigou],[bumen],[epiboly_Price]
                                 from BaoJiaCPXiangMu where id=" + id + "";
                SqlCommand command = new SqlCommand(sql, con);
                int number = command.ExecuteNonQuery();
                if (number > 0)
                {
                    Calculate();
                    upbaojia();
                    Bind();
                }
            }
        }

    }
    #endregion

    /// <summary>
    /// 插入外包项目,暂时不用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_action_Click(object sender, EventArgs e)
    {
        //测试项目名称和费用不能为空
        if (!string.IsNullOrEmpty(txt_csxiangmu.Text.Trim()) && !string.IsNullOrEmpty(txt_csprice.Text.Trim()) && !string.IsNullOrEmpty(txt_num.Text.Trim()))
        {
            string csname = txt_csxiangmu.Text.Trim();
            string csprice = txt_csprice.Text.Trim();
            string jishu = txt_jishu.Text.Trim();
            string num = txt_num.Text.Trim();
            string beizhu = txt_beizhu.Text;
            string zhouqi = txt_zhouqi.Text.Trim();
            string biaozhun = txt_biaozhun.Text;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = @"insert into BaoJiaCPXiangMu([baojiaid],[kehuid],[cpid],[ceshiname],[biaozhun],[neirong],[yp],[feiyong],[zhekou],[shuliang],
                             [beizhu],[fillname],[filltime],[responser],[zhouqi],[tijiaobiaozhi],[tijiaoname],[tijiaotime],[tijiaohaoma],[jishuyaoqiu],[daid],[zhongid],[xiaoid],[yuanshi],
                             [hesuanbiaozhi],[hesuantime],[hesuanname],[epiboly],[jigou],[bumen])
                             values('" + baojiaid + "', '" + kehuid + "', '" + drop_CP.SelectedValue + "', '" + csname + "', '" + biaozhun + "', '', '', '" + csprice + "', '1', '" + num + "', '" + beizhu + "', '" + Session["Username"].ToString() + "', '" + DateTime.Now + "', '" + Session["Username"].ToString() + "', '" + zhouqi + "', '否', '', '1900-01-01 00:00:00.000', '', '', '', '', '', '" + csprice + "', '否', '', '', '外包', '" + drop_Waibu.SelectedValue + "','" + this.DropDownList_xiangmubumen.SelectedValue + "')";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            try
            {
                Calculate();
                //    WaiBaoPrice();
                Bind();
            }
            catch (Exception ex)
            {
                ld.Text = ex.Message;
            }
        }
        else
        {
            ld.Text = "<script>alert('测试项目、费用、数量不能为空')</script>";
        }
    }

    /// <summary>
    /// 加载银行账户下拉框
    /// </summary>
    private void BankAccount()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from [dbo].[Bankaccount]";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DropDownList2.DataSource = ds.Tables[0];
            DropDownList2.DataTextField = "openaccout";
            DropDownList2.DataValueField = "id";
            DropDownList2.DataBind();

            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                if (DropDownList2.Items[i].Value == "9")
                {
                    DropDownList2.Items[i].Text = "深圳农村商业银行和平支行(标源)";
                }
            }
        }
    }

    protected void CheckBoxList9_DataBound(object sender, EventArgs e)
    {
        //默认选中电子电器的条款
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sqltiaokuan = "select * from Clause2 where banben='" + drop_language.SelectedValue + "'";
            SqlDataAdapter ad2 = new SqlDataAdapter(sqltiaokuan, con);
            DataSet ds2 = new DataSet();
            ad2.Fill(ds2);
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                if (ds2.Tables[0].Rows[i]["bumen"].ToString() == "电子电器")
                {
                    CheckBoxList9.Items[i].Selected = true;
                }
                if (ds2.Tables[0].Rows[i]["bumen"].ToString() == "默认")
                {
                    CheckBoxList9.Items[i].Selected = true;
                    CheckBoxList9.Items[i].Enabled = false;
                }
            }
        }
    }

    protected void TextBox_finalprice_TextChanged(object sender, EventArgs e)
    {
        Regex reg = new Regex("(?!^0*(\\.0{1,2})?$)^\\d{1,13}(\\.\\d{1,2})?$"); //第一个可以输入0
        if (reg.IsMatch(TextBox_finalprice.Text) || string.IsNullOrEmpty(TextBox_finalprice.Text))
        {
            Calculate();
        }
        else
        {
            TextBox_finalprice.Text = string.Empty;
            ld.Text = "<script>alert('优惠后金额输入不合法、请重新输入。');</script>";
        }
    }

    protected void drop_language_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindClause();
    }

    protected void drop_currency_SelectedIndexChanged(object sender, EventArgs e)
    {
        Calculate();
    }
    /// <summary>
    /// 获取当前登录进来人的职位
    /// </summary>
    protected string Dutyname()
    {
        using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string dutyname = "";
            con1.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con1);
            SqlDataReader dr = cmdstate.ExecuteReader();
            if (dr.Read())
            {
                dutyname = dr["dutyname"].ToString();
            }
            dr.Close();
            return dutyname;
        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            Button btnproject = new Button();
            btnproject.ID = e.Row.RowIndex.ToString();
            btnproject.Text = "添加项目";
            btnproject.Click += Btnproject_Click;
            Random rd = new Random();
            string qz = DateTime.Now.ToString() + rd.Next(1000);
            btnproject.Attributes["onclick"] = "window.showModalDialog('Default2.aspx?kehuid=" + kehuid + "&baojiaid=" + baojiaid + "&dd=" + qz + "&cp=" + DropDownList3.SelectedValue + "&cpid=" + e.Row.RowIndex.ToString() + "','test','dialogWidth=1400px;help:no;resizable:yes; dialogTop:100px;');";
            e.Row.Cells[6].Controls.Add(btnproject);
        }
    }
}
#endregion