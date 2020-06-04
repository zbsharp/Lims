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

public partial class Quotation_QuotationAdd : System.Web.UI.Page
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
    protected string wt = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        name = Session["UserName"].ToString();

        baojiaid = Text50.Value.Trim();

        if (Request.QueryString["idf"] != null)
        {
            idf = Request.QueryString["idf"].ToString();
        }


        Label1.Text = Request.QueryString["kehuid"].ToString();
        kehuid = Request.QueryString["kehuid"].ToString();
        GridView1.Attributes.Add("style", "table-layout:fixed");
        GridView2.Attributes.Add("style", "table-layout:fixed");

        // GridView2.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        if (Request.QueryString["baojiaid"] != null)
        {
            baojiaid = Request.QueryString["baojiaid"].ToString();
        }
        else
        {
            //baojiaid = Session["id"].ToString();
            Button4.Enabled = false;
            Button5.Enabled = false;
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
            wt = dr["weituo"].ToString();
            //Calculate();
        }
        dr.Close();


        con.Close();
        if (tijiaobiaozhi == "是" && shenpibiaozhi == "否")
        {
            Response.Redirect("~/Customer/Welcome.aspx?id=rrt");
        }



        if (!IsPostBack)
        {

            using (SqlConnection con_language = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con_language.Open();
                string sql_language = "select * from baojiabiao where baojiaid='" + baojiaid + "'";
                SqlCommand com_language = new SqlCommand(sql_language, con_language);
                SqlDataReader dr_language = com_language.ExecuteReader();
                if (dr_language.Read())
                {
                    drop_language.SelectedValue = dr_language["Clause_language"].ToString();
                }
                dr_language.Close();
            }
            BindDep();
            Bind();
            BindClause();
            BankAccount();
            BindBaoJiaBiao();

        }

        if (tijiaobiaozhi == "是")
        {


            Button1.Enabled = false;
            Button2.Enabled = false;


            if (shenpibiaozhi != "通过")
            {
                Button3.Enabled = false;

                Button10.Visible = false;
                btn_action.Visible = false;
                //this.GridView1.Columns[5].Visible = false;
                //this.GridView2.Columns[20].Visible = false;
            }
            Button4.Enabled = false;
            if (huiqianbiaozhi == "否" && shenpibiaozhi == "通过")
            {
                Button5.Enabled = true;
            }
            else
            {
                Button5.Enabled = false;
            }
        }
        else
        {
            Button5.Enabled = false;
            Button3.Enabled = true;
            // Button10.Visible = true;
        }

        Calculate();
        //this.Button10.Attributes["onclick"] = "window.showModalDialog('Default2.aspx?kehuid=" + kehuid + "&baojiaid=" + baojiaid + "&dd=" + DateTime.Now + "','test','dialogWidth=1400px;help:no;resizable:yes; dialogTop:100px;');";


        //string sql4 = "select * from anjianxinxi2 where baojiaid='" + baojiaid + "'";
        //SqlCommand cmd4 = new SqlCommand(sql4, con);
        //SqlDataReader dr4 = cmd4.ExecuteReader();
        //if (dr4.Read())
        //{
        //    Button10.Visible = false;
        //}
        //else
        //{
        //    Button10.Visible = true;

        //}




        //this.Button10.Attributes["onclick"] = "javascript:GetMyValue('cs_txt',window.showModalDialog('Default2.aspx?kehuid=" + kehuid + "&baojiaid=" + baojiaid + "&dd=" + DateTime.Now + "','window','dialogWidth=900px;status:no;help:no;resizable:yes; dialogTop:100px;'));";
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


    protected void Bind()
    {
        ld.Text = "";
        BindChanPin();
        BindXiangMu();
        BindShenPi();
        BindContract();
        BindDropCp();//查询产品、用于修改项目
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

    protected void BindBaoJiaBiao()
    {

        string strs = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql21 = "select * from CustomerLinkMan where  customerid='" + kehuid + "' and delete_biaozhi !='是' ";
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


        string sql = "select * from baojiabiao where baojiaid='" + baojiaid + "'";
        SqlCommand com = new SqlCommand(sql, con);
        SqlDataReader dr = com.ExecuteReader();
        if (dr.Read())
        {
            strs = dr["clause"].ToString();
            string zhangdan = dr["zhangdan"].ToString();
            Calculate();
            //  baojiazhehou_txt.Text = Math.Round(Convert.ToDecimal(dr["zhehoujia"]), 2).ToString();
            baojiabeizhu_txt.Text = dr["beizhu"].ToString();
            baojiazhekou_txt.Text = dr["discount"].ToString();
            //realzhekou_txt.Text= dr["discount"].ToString();
            //  epiboly_price.Text = dr["epiboly_Price"].ToString();
            drop_way.SelectedValue = dr["paymentmethod"].ToString();
            drop_vat.SelectedValue = dr["isVAT"].ToString();
            TextBox_finalprice.Text = dr["coupon"].ToString();
            drop_currency.SelectedValue = dr["currency"].ToString();
            txt_drow.Text = dr["thefirst"].ToString();
            drop_language.SelectedValue = dr["Clause_language"].ToString();
            this.txt_kuozhanfei.Text = dr["kuozhanfei"].ToString();

            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                if (DropDownList2.Items[i].Value == zhangdan)
                {
                    if (zhangdan == "9")
                    {
                        DropDownList2.Items[i].Text = "深圳农村商业银行和平支行(标源)";
                    }
                    DropDownList2.Items[i].Selected = true;
                }
            }
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
        CheckBoxList9.DataValueField = "neirong";
        CheckBoxList9.DataBind();
        con.Close();
        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
        {
            //if (ds2.Tables[0].Rows[i]["bumen"].ToString() == "电子电器")
            //{
            //    CheckBoxList9.Items[i].Selected = true;
            //}
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
        string sql = "select * from BaoJiaChanPing where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "' order by id asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        //绑定外包项目中的产品下拉框    2019-8-16添加
        this.drop_CP.DataSource = ds.Tables[0];
        this.drop_CP.DataTextField = "name";
        this.drop_CP.DataValueField = "id";
        this.drop_CP.DataBind();


        string sql2 = "select * from dd";
        SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);
        con.Close();
    }

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
        //upbaojia();
        Calculate();
        Bind();
    }

    #endregion

    #region 产生报价编号
    protected string MakeBaojiaid()
    {
        //string baojiaid = "";
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();
        //try
        //{
        //    string sql1 = "select baojiaid from baojiabiao order by id asc";
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
        //        if (DateTime.Today.Month < 10)
        //        {
        //            date1 = "0" + DateTime.Today.Month;
        //        }
        //        else
        //        {
        //            date1 = DateTime.Today.Month.ToString();
        //        }
        //        baojiaid = "SQ" + DateTime.Now.Year.ToString() + date1 + "0001";
        //    }
        //    else
        //    {
        //        houzhui = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["baojiaid"].ToString();
        //        yue = houzhui.Substring(6, 2);
        //        tian = houzhui.Substring(8, 4);
        //        if (DateTime.Today.Month < 10)
        //        {
        //            yue1 = "0" + DateTime.Today.Month;
        //        }
        //        else
        //        {
        //            yue1 = Convert.ToString(DateTime.Today.Month);

        //        }

        //        if (yue == yue1)
        //        {
        //            int a = Convert.ToInt32(tian) + 1;
        //            baojiaid = "SQ" + DateTime.Now.Year.ToString() + yue1 + String.Format("{0:D4}", a);
        //        }
        //        else
        //        {

        //            baojiaid = "SQ" + DateTime.Now.Year.ToString() + yue1 + "0001";

        //        }

        //    }
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message);
        //}
        //con.Close();
        //return baojiaid;
        //**********////////////2019-8-8修改
        string sql = "select top 1 BaoJiaId from [dbo].[BaoJiaBiao] order by id desc";
        string str = "";
        string year;
        //判断用户是福永还是龙华地区的人--后续开发
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                //当数据库里还没有编号时
                year = DateTime.Now.Year.ToString();
                str = "FY" + year.Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + "000001";
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
                    str = "FY" + year.Substring(2, 2) + yue + i.ToString().PadLeft(6, '0');
                }
                else
                {
                    str = "FY" + year.Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + "000001";
                }
            }
        }
        return str;
    }
    #endregion

    #region 保存产品

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cpname_txt.Text.Trim()))
        {
            ld.Text = "<script>alert('产品名称不能存在空值。');</script>";
        }
        else
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "insert into BaoJiaChanPing values('" + baojiaid + "','" + kehuid + "','" + cpname_txt.Text.Trim() + "','" + cpxinghao_txt.Text.Trim() + "','" + cpbeizhu_txt.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            //upbaojia();
            Bind();
        }
    }

    #endregion

    #region 绑定测试项目列表

    protected void BindXiangMu()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *, (feiyong*zhekou) as feiyong2 from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "' order by id asc";
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

        string sql4 = "select * from anjianxinxi2 where baojiaid='" + baojiaid + "'";
        SqlCommand cmd4 = new SqlCommand(sql4, con);
        SqlDataReader dr4 = cmd4.ExecuteReader();
        if (dr4.Read())
        {
            dr4.Close();
        }
        else
        {
            dr4.Close();


            string sql = "delete from BaoJiaCPXiangMu where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        if (limit1("修改开案后报价"))
        {
            string sql = "delete from BaoJiaCPXiangMu where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        con.Close();

        Calculate();
        //upbaojia();

        Bind();
        //dcount();
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
            Response.Write(ex.Message);
        }

    }
    #endregion

    #region 保存报价单

    protected void Button3_Click(object sender, EventArgs e)
    {
        Calculate();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        //计算总价格
        decimal price_sum = 0;
        string sql_sum = "select sum(total) as total from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "'";
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
            ld.Text = "<script>alert('请增加测试项目')</script>";
            con.Close();
            return;
        }

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
            if ((string.IsNullOrEmpty(txt_drow.Text.Trim())) || (!ifleagemoney) || txt_drow.Text == "0.00")
            {
                ld.Text = "<script>alert('首款金额不能为空,且数据需为数值')</script>";
                return;
            }
        }

        string sqlclause = "delete from Clause where baojiaid='" + baojiaid + "'";
        SqlCommand cmdclause = new SqlCommand(sqlclause, con);
        cmdclause.ExecuteNonQuery();


        string sql2 = "update Provision2 set remark='" + TextBox1.Text.Trim() + "' where  baojiaid ='" + baojiaid + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        cmd2.ExecuteNonQuery();

        string sql4 = "";

        int xuhao = 0;
        string tiaokuan = "";
        for (int i = 1; i < CheckBoxList9.Items.Count + 1; i++)
        {
            if (CheckBoxList9.Items[i - 1].Selected)
            {
                xuhao++;

                string xuhaos = xuhao.ToString();
                string xuhaosh = xuhaos + ",";

                tiaokuan += CheckBoxList9.Items[i - 1].Text.ToString() + "|";
                sql4 = "insert into Clause values('" + xuhaosh + "','" + CheckBoxList9.Items[i - 1].Text.ToString() + "','" + baojiaid + "')";
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


        string sqlbao = "";
        decimal finalprice = 0.00m;
        if (!string.IsNullOrEmpty(TextBox_finalprice.Text))
        {
            finalprice = Convert.ToDecimal(TextBox_finalprice.Text);
        }

        //扩展费
        decimal kuozhanfei = 0m;
        if (!string.IsNullOrEmpty(this.txt_kuozhanfei.Text.Trim()))
        {
            kuozhanfei = Convert.ToDecimal(this.txt_kuozhanfei.Text);
        }


        if (bankaccout == "全款")
        {
            sqlbao = "update baojiabiao set realdiscount='" + Convert.ToDecimal(realzhekou_txt.Text.Trim()) + "' ,discount='" + Convert.ToDecimal(realzhekou_txt.Text.Trim()) + "',zhehoujia='" + price_sum + "',clause='" + tiaokuan + "',beizhu='" + baojiabeizhu_txt.Text.Trim() + "',zhangdan='" + DropDownList2.SelectedValue + "',[paymentmethod]='" + bankaccout + "', thefirst='0.00',isVAT='" + drop_vat.SelectedValue + "',coupon='" + finalprice + "',currency='" + drop_currency.SelectedValue + "',Clause_language='" + drop_language.SelectedValue + "',epiboly_Price='" + TextBox_epibolypricetotal.Text.ToString() + "',kuozhanfei=" + kuozhanfei + " where baojiaid='" + baojiaid + "'";
        }
        else
        {
            sqlbao = "update baojiabiao set realdiscount='" + Convert.ToDecimal(realzhekou_txt.Text.Trim()) + "' ,discount='" + Convert.ToDecimal(realzhekou_txt.Text.Trim()) + "',zhehoujia='" + price_sum + "',clause='" + tiaokuan + "',beizhu='" + baojiabeizhu_txt.Text.Trim() + "',zhangdan='" + DropDownList2.SelectedValue + "',[paymentmethod]='" + bankaccout + "', thefirst='" + txt_drow.Text.Trim() + "',isVAT='" + drop_vat.SelectedValue + "',coupon='" + finalprice + "',currency='" + drop_currency.SelectedValue + "',Clause_language='" + drop_language.SelectedValue + "',epiboly_Price='" + TextBox_epibolypricetotal.Text.ToString() + "',kuozhanfei=" + kuozhanfei + " where baojiaid='" + baojiaid + "'";
        }

        SqlCommand cmdbao = new SqlCommand(sqlbao, con);
        cmdbao.ExecuteNonQuery();

        string sql51 = "delete from  baojialink where baojiaid='" + baojiaid + "'";
        SqlCommand cmdbao11 = new SqlCommand(sql51, con);
        cmdbao11.ExecuteNonQuery();
        string sql5 = "insert into baojialink values('" + baojiaid + "','" + DropDownList1.SelectedValue + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";

        SqlCommand cmdbao1 = new SqlCommand(sql5, con);
        cmdbao1.ExecuteNonQuery();
        upbaojia();
        con.Close();
        ld.Text = "<script>alert('保存成功!');</script>";
        //ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "msg1", "alert('保存成功');location.replace('QuotationAdd.aspx?baojiaid=" + baojiaid + "&kehuid=" + kehuid + "');", true);
        //}
    }

    #endregion
    #region 计算项目金额
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


    protected void Button4_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql3 = "update baojiabiao set tijiaobiaozhi='是',tijiaotime='" + DateTime.Now + "' where  baojiaid ='" + baojiaid + "'";
        SqlCommand cmd = new SqlCommand(sql3, con);
        cmd.ExecuteNonQuery();
        con.Close();
        Label3.Text = "提交成功";
        Label3.ForeColor = Color.Red;
        //Response.Write("<script>alert('客户添加完成，可继续添加客户联系人信息！');top.main.yujijun.location.href=''</script>");
        //ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "msg1", "alert('提交成功');location.replace('QuotationAdd.aspx?baojiaid="+baojiaid+"&kehuid="+kehuid+"');", true);

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

    protected void Button5_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql3 = "update baojiabiao set huiqianbiaozhi='是',huiqiantime='" + DateTime.Now + "' where  baojiaid ='" + baojiaid + "'";
        SqlCommand cmd = new SqlCommand(sql3, con);
        cmd.ExecuteNonQuery();
        con.Close();
        Label3.Text = "提交成功";
        Label3.ForeColor = Color.Red;
        //Response.Write("<script>alert('客户添加完成，可继续添加客户联系人信息！');top.main.yujijun.location.href=''</script>");
        //ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "msg1", "alert('提交成功');location.replace('QuotationAdd.aspx?baojiaid=" + baojiaid + "&kehuid=" + kehuid + "');", true);

    }
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
    #region  以前的修改测试项目
    //protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    string KeyId = GridView2.DataKeys[e.RowIndex].Value.ToString();

    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
    //    con.Open();

    //    string sql5 = "select * from anjianxinxi2 where baojiaid='" + baojiaid + "'";
    //    SqlCommand cmd5 = new SqlCommand(sql5, con);
    //    SqlDataReader dr5 = cmd5.ExecuteReader();
    //    if (dr5.Read())
    //    {
    //        dr5.Close();
    //    }
    //    else
    //    {
    //        dr5.Close();







    //        GridViewRow gvr = GridView2.Rows[e.RowIndex];
    //        string stranswer = ((TextBox)gvr.FindControl("TextBox1")).Text;


    //        string uuname1 = ((TextBox)gvr.FindControl("TextBoxxc")).Text;
    //        // string uuname2 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());

    //        string uuname2 = stranswer;

    //        string uuname3 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
    //        string uuname4 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
    //        string uuname5 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
    //        string uuname6 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[6].Controls[0]).Text.ToString());
    //        // string uuname7 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[7].Controls[0]).Text.ToString());
    //        string uuname8 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[8].Controls[0]).Text.ToString());
    //        string uuname9 = ((TextBox)gvr.FindControl("TextBoxxb")).Text;
    //        string uuname10 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[10].Controls[0]).Text.ToString());

    //        string sql2 = "select top 1 leibiename from Product2 where leibieid in (select daid from BaoJiaCPXiangMu where id='" + KeyId + "') and leibiename like 'ccc%'";
    //        SqlCommand cmd2 = new SqlCommand(sql2, con);
    //        SqlDataReader dr2 = cmd2.ExecuteReader();
    //        if (dr2.Read())
    //        {
    //            dr2.Close();
    //            string sql = "update BaoJiaCPXiangMu set ceshiname='" + uuname1 + "',biaozhun='" + uuname2 + "',neirong='" + uuname3 + "',yp='" + uuname4 + "',zhouqi='" + uuname5 + "',shuliang='" + Convert.ToDecimal(uuname8) + "',beizhu='" + uuname9 + "',jishuyaoqiu='" + uuname10 + "' where id='" + KeyId + "'";
    //            SqlCommand cmd = new SqlCommand(sql, con);
    //            cmd.ExecuteNonQuery();
    //        }
    //        else
    //        {
    //            dr2.Close();
    //            string sql = "update BaoJiaCPXiangMu set ceshiname='" + uuname1 + "',biaozhun='" + uuname2 + "',neirong='" + uuname3 + "',yp='" + uuname4 + "',zhouqi='" + uuname5 + "',feiyong='" + Convert.ToDecimal(uuname6) + "',shuliang='" + Convert.ToDecimal(uuname8) + "',beizhu='" + uuname9 + "',jishuyaoqiu='" + uuname10 + "' where id='" + KeyId + "'";
    //            SqlCommand cmd = new SqlCommand(sql, con);
    //            cmd.ExecuteNonQuery();
    //        }


    //        decimal ticheng = 0;
    //        decimal fenmu = 0;
    //        decimal fenzi = 0;


    //        string sql3 = "select * from BaoJiaCPXiangMu where id='" + KeyId + "'";
    //        SqlCommand cmd3 = new SqlCommand(sql3, con);
    //        SqlDataReader dr3 = cmd3.ExecuteReader();
    //        if (dr3.Read())
    //        {
    //            fenmu = Convert.ToDecimal(dr3["yuanshi"]);
    //            fenzi = Convert.ToDecimal(dr3["feiyong"]);
    //        }
    //        if (fenmu != 0)
    //        {
    //            ticheng = fenzi / fenmu;
    //        }
    //        dr3.Close();

    //        if (ticheng <= Convert.ToDecimal(0.85))
    //        {
    //            string sql4 = "update BaoJiaCPXiangMu set hesuanname='是' where id='" + KeyId + "'";
    //            SqlCommand cmd4 = new SqlCommand(sql4, con);
    //            cmd4.ExecuteNonQuery();

    //        }
    //        else
    //        {
    //            string sql4 = "update BaoJiaCPXiangMu set hesuanname='' where id='" + KeyId + "'";
    //            SqlCommand cmd4 = new SqlCommand(sql4, con);
    //            cmd4.ExecuteNonQuery();
    //        }
    //    }

    //    GridViewRow gvr = GridView2.Rows[e.RowIndex];
    //    string stranswer = ((TextBox)gvr.FindControl("TextBox1")).Text;


    //    string uuname1 = ((TextBox)gvr.FindControl("TextBoxxc")).Text;
    //    // string uuname2 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());

    //    string uuname2 = stranswer;

    //    string uuname3 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
    //    string uuname4 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
    //    string uuname5 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
    //    string uuname6 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[6].Controls[0]).Text.ToString());
    //    // string uuname7 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[7].Controls[0]).Text.ToString());
    //    string uuname8 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[8].Controls[0]).Text.ToString());
    //    string uuname9 = ((TextBox)gvr.FindControl("TextBoxxb")).Text;
    //    string uuname10 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[10].Controls[0]).Text.ToString());

    //    string sql2 = "select top 1 leibiename from Product2 where leibieid in (select daid from BaoJiaCPXiangMu where id='" + KeyId + "') and leibiename like 'ccc%'";
    //    SqlCommand cmd2 = new SqlCommand(sql2, con);
    //    SqlDataReader dr2 = cmd2.ExecuteReader();
    //    if (dr2.Read())
    //    {
    //        dr2.Close();
    //        string sql = "update BaoJiaCPXiangMu set ceshiname='" + uuname1 + "',biaozhun='" + uuname2 + "',neirong='" + uuname3 + "',yp='" + uuname4 + "',zhouqi='" + uuname5 + "',shuliang='" + Convert.ToDecimal(uuname8) + "',beizhu='" + uuname9 + "',jishuyaoqiu='" + uuname10 + "' where id='" + KeyId + "'";
    //        SqlCommand cmd = new SqlCommand(sql, con);
    //        cmd.ExecuteNonQuery();
    //    }
    //    else
    //    {
    //        dr2.Close();
    //        string sql = "update BaoJiaCPXiangMu set ceshiname='" + uuname1 + "',biaozhun='" + uuname2 + "',neirong='" + uuname3 + "',yp='" + uuname4 + "',zhouqi='" + uuname5 + "',feiyong='" + Convert.ToDecimal(uuname6) + "',shuliang='" + Convert.ToDecimal(uuname8) + "',beizhu='" + uuname9 + "',jishuyaoqiu='" + uuname10 + "' where id='" + KeyId + "'";
    //        SqlCommand cmd = new SqlCommand(sql, con);
    //        cmd.ExecuteNonQuery();
    //    }

    //    decimal ticheng = 0;

    //    decimal fenmu = 0;
    //    decimal fenzi = 0;


    //    string sql3 = "select * from BaoJiaCPXiangMu where id='" + KeyId + "'";
    //    SqlCommand cmd3 = new SqlCommand(sql3, con);
    //    SqlDataReader dr3 = cmd3.ExecuteReader();
    //    if (dr3.Read())
    //    {
    //        fenmu = Convert.ToDecimal(dr3["yuanshi"]);
    //        fenzi = Convert.ToDecimal(dr3["feiyong"]);
    //    }
    //    if (fenmu != 0)
    //    {
    //        ticheng = fenzi / fenmu;
    //    }
    //    dr3.Close();

    //    if (ticheng <= Convert.ToDecimal(0.85))
    //    {
    //        string sql4 = "update BaoJiaCPXiangMu set hesuanname='是' where id='" + KeyId + "'";
    //        SqlCommand cmd4 = new SqlCommand(sql4, con);
    //        cmd4.ExecuteNonQuery();
    //    }
    //    else
    //    {
    //        string sql4 = "update BaoJiaCPXiangMu set hesuanname='' where id='" + KeyId + "'";
    //        SqlCommand cmd4 = new SqlCommand(sql4, con);
    //        cmd4.ExecuteNonQuery();
    //    }
    //    string sqltic = "update baojiacpxiangmu set zhekou='" + Convert.ToDecimal(ticheng) + "' where id='" + KeyId + "'";
    //    SqlCommand cmdti = new SqlCommand(sqltic, con);
    //    cmdti.ExecuteNonQuery();
    //    con.Close();
    //    GridView2.EditIndex = -1;
    //    Bind();
    //    upbaojia();
    //    Calculate();
    //}
    #endregion

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
        Bind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string KeyId = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        GridViewRow gvr = GridView1.Rows[e.RowIndex];
        string uuname1 = ((TextBox)gvr.FindControl("TextBoxcn")).Text;
        string uuname2 = ((TextBox)gvr.FindControl("TextBoxct")).Text;
        string uuname3 = ((TextBox)gvr.FindControl("TextBoxcb")).Text;
        //string uuname1 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
        //string uuname2 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
        //string uuname3 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
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
            //upbaojia();
            Bind();
        }
    }
    #endregion


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
    protected void Button7_Click(object sender, EventArgs e)
    {
        Response.Redirect("QuoOther.aspx?baojiaid=" + baojiaid);
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        Response.Redirect("QuoLink.aspx?baojiaid=" + baojiaid);
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Case/UploadFile.aspx?baojiaid=" + baojiaid + "&&id=" + baojiaid);
    }
    protected void upbaojia()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql3 = "";
        if (idf == "")
        {

            string sqlb = "select baojiaid from anjianxinxi2 where baojiaid='" + baojiaid + "'";
            SqlCommand cmdb = new SqlCommand(sqlb, con);
            SqlDataReader drb = cmdb.ExecuteReader();
            if (drb.Read())
            {
                drb.Close();
                sql3 = "update baojiabiao set shenpibiaozhi='否',tijiaobiaozhi='否',tijiaotime='1900-01-01',huiqianbiaozhi='否',huiqiantime='1900-01-01' where  baojiaid ='" + baojiaid + "'";
                SqlCommand cmd = new SqlCommand(sql3, con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                drb.Close();
                sql3 = "update baojiabiao set shenpibiaozhi='否',tijiaobiaozhi='否',tijiaotime='1900-01-01',huiqianbiaozhi='否',huiqiantime='1900-01-01' where  baojiaid ='" + baojiaid + "'";
                SqlCommand cmd = new SqlCommand(sql3, con);
                cmd.ExecuteNonQuery();
            }


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

    protected void Button12_Click(object sender, EventArgs e)
    {
        string baoid = MakeBaojiaid();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "Addnewbaojia";
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter Para1 = new SqlParameter("@newbaojiaid", SqlDbType.NVarChar);
        Para1.Value = baoid;
        cmd.Parameters.Add(Para1);

        SqlParameter Para2 = new SqlParameter("@oldbaojiaid", SqlDbType.NVarChar);
        Para2.Value = baojiaid;
        cmd.Parameters.Add(Para2);

        int result = cmd.ExecuteNonQuery();
        con.Close();
        Response.Redirect("QuotationAdd.aspx?baojiaid=" + baoid + "&kehuid=" + kehuid);
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
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
            //  WaiBaoPrice();
            Bind();
            //  dcount();
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
                    Bind();
                }
            }
        }
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        //数量价格不能为空  外包价格不能高于实际费用
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
            int columns = this.GridView2.Columns.Count;
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


            string sql = "update BaoJiaCPXiangMu set  zhekou='" + zhekou + "',yp='" + this.txt_specimen.Text.ToString() + "',zhouqi='" + this.txt_period.Text.ToString() + "',feiyong='" + Convert.ToDecimal(this.txt_price.Text.Trim()) + "',shuliang='" + Convert.ToDecimal(this.txt_nums.Text.Trim()) + "',beizhu='" + this.txt_remark.Text.ToString() + "',jishuyaoqiu='" + this.txt_technology.Text.ToString() + "',epiboly='" + DropDownList_epiboly.SelectedValue + "',epiboly_price='" + Convert.ToDecimal(this.TextBox_epiboly_price.Text.Trim()) + "',cpid='" + this.dropUpdatecp.SelectedValue + "' where id='" + KeyId + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            con.Close();
            GridView2.EditIndex = -1;
            Bind();
            // upbaojia();
            Calculate();
        }
    }

    protected void btn_action_Click(object sender, EventArgs e)
    {
        //测试项目名称和费用不能为空
        if (!string.IsNullOrEmpty(txt_csxiangmu.Text.Trim()) && !string.IsNullOrEmpty(txt_csprice.Text.Trim()))
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
                             values('" + baojiaid + "', '" + kehuid + "', '" + drop_CP.SelectedValue + "', '" + csname + "', '" + biaozhun + "', '', '', '" + csprice + "', '1', '" + num + "', '" + beizhu + "', '" + Session["Username"].ToString() + "', '" + DateTime.Now + "', '" + Session["Username"].ToString() + "', '" + zhouqi + "', '否', '', '1900-01-01 00:00:00.000', '', '', '', '', '', '" + csprice + "', '否', '','','外包','" + drop_Waibu.SelectedValue + "','" + this.DropDownList_xiangmubumen.SelectedValue + "')";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            try
            {
                WaiBaoPrice();
                Bind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        else
        {
            Response.Write("<script>alert('测试项目和费用不能为空')</script>");
        }
    }
    /// <summary>
    /// 计算外包总价
    /// </summary>
    private void WaiBaoPrice()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            //计算外包项目的总价格
            string sql_epiboly = "select sum(epiboly_price) as total from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' group by baojiaid";
            SqlCommand cmd_epiboly = new SqlCommand(sql_epiboly, con);
            SqlDataReader dr_epiboly = cmd_epiboly.ExecuteReader();
            if (dr_epiboly.Read())
            {
                if (dr_epiboly["total"] == DBNull.Value)
                {
                    this.epiboly_price.Text = "0";
                }
                else
                {
                    this.epiboly_price.Text = Convert.ToDecimal(dr_epiboly["total"]).ToString("f2");
                }
            }
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
            //Response.Write("优惠后金额输入不合法");报错
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
            btnproject.Attributes["onclick"] = "window.showModalDialog('Default2.aspx?kehuid=" + kehuid + "&baojiaid=" + baojiaid + "&dd=" + qz + "&cpid=" + e.Row.RowIndex.ToString() + "','test','dialogWidth=1400px;help:no;resizable:yes; dialogTop:100px;');";
            e.Row.Cells[6].Controls.Add(btnproject);
        }
    }

    private void Btnproject_Click(object sender, EventArgs e)
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
            Response.Write(ex.Message);
        }
    }
}