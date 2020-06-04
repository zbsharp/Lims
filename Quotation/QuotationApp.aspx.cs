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

public partial class Quotation_QuotationApp : System.Web.UI.Page
{
    protected string kehuid = "";
    protected string baojiaid = "";
    protected string chanpingid = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        Label1.Text = Request.QueryString["kehuid"].ToString();
        kehuid = Request.QueryString["kehuid"].ToString();

        if (Request.QueryString["baojiaid"] != null)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            //判断登录进来人的部门
            string dn = "";
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con);
            SqlDataReader dr_bumen = cmdstate.ExecuteReader();
            if (dr_bumen.Read())
            {
                dn = dr_bumen["departmentname"].ToString();
                if (dn == "客服部")
                {
                    TextBox2.Visible = false;
                    DropDownList1.Visible = false;
                    Button3.Visible = false;
                }
            }
            dr_bumen.Close();

            baojiaid = Request.QueryString["baojiaid"].ToString();
            Label3.Text = baojiaid;
            string shenpi = "";


            string sql = "select * from baojiabiao where baojiaid='" + baojiaid + "' order by baojiaid";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                shenpi = dr["weituo"].ToString();
            }
            dr.Close();


            string sql2 = "select * from baojiacpxiangmu where baojiaid='" + baojiaid + "' and daid not in ( select leibieid from product2 where (leibieid='1002' or leibieid='1001' or leibieid='0900' or leibieid='0300')) order by baojiaid";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {

            }
            else
            {
                shenpi = "";
            }
            dr2.Close();

            con.Close();

            //if (shenpi == "是")
            //{
            //    Button3.Enabled = false;
            //    Button3.Text = "有项目低于85折需要领导审批";
            //}
            //if (limit1("审批低折报价"))
            //{
            //    Button3.Enabled = true;
            //    Button3.Text = "确定保存";
            //}


        }

        if (!IsPostBack)
        {
            Clear();
            BindBankCount();
            Bind();
            BindClause();
            BindBaoJiaBiao();
            //DataBind();
        }
    }

    protected void BindBankCount()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from Bankaccount ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        this.DropDownList3.DataSource = ds.Tables[0];
        this.DropDownList3.DataTextField = "name";
        this.DropDownList3.DataValueField = "id";
        this.DropDownList3.DataBind();
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
        DropDownList2.DataSource = ds21.Tables[0];
        DropDownList2.DataTextField = "name";
        DropDownList2.DataValueField = "id";
        DropDownList2.DataBind();

        GridView5.DataSource = ds21.Tables[0];
        GridView5.DataBind();


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
            DropDownList2.SelectedValue = linkman;

        }
        string sql = "select * from baojiabiao where baojiaid='" + baojiaid + "'";
        SqlCommand com = new SqlCommand(sql, con);
        SqlDataReader dr = com.ExecuteReader();
        if (dr.Read())
        {
            strs = dr["clause"].ToString();
            baojiazhehou_txt.Text = Math.Round(Convert.ToDecimal(dr["zhehoujia"]), 2).ToString();
            baojiabeizhu_txt.Text = dr["beizhu"].ToString();
            baojiazhekou_txt.Text = dr["discount"].ToString();
            weituo.Text = dr["weituo"].ToString();
            realzhekou_txt.Text = dr["realdiscount"].ToString();
            //优惠后金额
            string coupon = dr["coupon"].ToString();
            if (coupon == "0.00" || coupon == "&nbsp;")
            {
                txt_coupon.Text = string.Empty;
            }
            else
            {
                txt_coupon.Text = coupon;
            }
            txt_currency.Text = dr["currency"].ToString();//币种
            txt_vat.Text = dr["isVAT"].ToString();//含税标志
            //税金
            if (txt_vat.Text == "否")
            {
                if (string.IsNullOrEmpty(txt_coupon.Text))
                {
                    decimal vat = Convert.ToDecimal(baojiazhehou_txt.Text) * 0.06m;
                    txt_taxes.Text = Math.Round(vat, 2).ToString();
                }
                else
                {
                    decimal vat = Convert.ToDecimal(txt_coupon.Text) * 0.06m;
                    txt_taxes.Text = Math.Round(vat, 2).ToString();
                }
            }
            string str = dr["kuozhanfei"].ToString();
            //扩展费
            if (dr["kuozhanfei"].ToString() == "&nbsp;" || dr["kuozhanfei"].ToString() == "" || dr["kuozhanfei"].ToString() == "0.00" || dr["kuozhanfei"] == null)
            {
                this.txt_kuozhanfei.Text = "0";
            }
            else
            {
                this.txt_kuozhanfei.Text = dr["kuozhanfei"].ToString();
            }

            //报价时选的银行账户
            string Bankaccount = dr["zhangdan"].ToString();
            dr.Close();
            string sql_bankaccount = "select * from Bankaccount where id='" + Bankaccount + "'";
            SqlCommand cmd_bankaccount = new SqlCommand(sql_bankaccount, con);
            SqlDataReader dr_bankaccount = cmd_bankaccount.ExecuteReader();
            if (dr_bankaccount.Read())
            {
                DropDownList3.SelectedValue = dr_bankaccount["Name"].ToString();
            }
            dr_bankaccount.Close();
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




        string su = "";
        string yuanzhi = "0";



        string zhe = "0";





        string sqlsum = "select sum(total) as total,sum(yuanshi*shuliang) as total1 from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' group by baojiaid";
        SqlCommand cmdsum = new SqlCommand(sqlsum, con);
        SqlDataReader drsum = cmdsum.ExecuteReader();
        if (drsum.Read())
        {
            if (drsum["total1"] == DBNull.Value)
            {
                yuanzhi = "0";
            }
            else
            {
                //baojiazong_txt.Text = Convert.ToDecimal(drsum["total"]).ToString().Trim();
                yuanzhi = Math.Round(Convert.ToDecimal(drsum["total1"]), 2).ToString();
            }

            if (drsum["total"] == DBNull.Value)
            {
                zhe = "0";
            }
            else
            {
                //baojiazong_txt.Text = Convert.ToDecimal(drsum["total"]).ToString().Trim();
                zhe = Math.Round(Convert.ToDecimal(drsum["total"]), 2).ToString();
            }

        }

        drsum.Close();

        decimal dis = 1;
        if (yuanzhi != "0" && yuanzhi != "0.00")
        {
            dis = Convert.ToDecimal(baojiazhehou_txt.Text.Trim()) / Convert.ToDecimal(yuanzhi);
        }
        baojiazhekou_txt.Text = Math.Round(dis, 2).ToString();




        con.Close();
        Calculate();
        string[] strtemp = strs.Split('|');
        foreach (string str in strtemp)
        {
            for (int i = 0; i < CheckBoxList9.Items.Count; i++)
            {
                if (this.CheckBoxList9.Items[i].Text == str)
                {
                    this.CheckBoxList9.Items[i].Selected = true;
                }
            }
        }
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
            Label2.Text = dr["CustomName"].ToString();
        }
        dr.Close();

        string sql2 = "delete from BaoJiaBiao where baojiaid='" + Session["UserName"].ToString() + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        cmd2.ExecuteNonQuery();

        string sql3 = "delete from BaoJiaChanPing where baojiaid='" + Session["UserName"].ToString() + "'";
        SqlCommand cmd3 = new SqlCommand(sql3, con);
        cmd3.ExecuteNonQuery();

        string sql4 = "delete from BaoJiaCPXiangMu where baojiaid='" + Session["UserName"].ToString() + "'";
        SqlCommand cmd4 = new SqlCommand(sql4, con);
        cmd4.ExecuteNonQuery();

        con.Close();
    }

    protected void Bind()
    {
        BindChanPin();
        BindXiangMu();
        BindProduct();
        BindShenPi();
        BindContract();
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


    #region 绑定条款
    protected void BindClause()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sqltiaokuan = "select * from Clause2";
        SqlDataAdapter ad2 = new SqlDataAdapter(sqltiaokuan, con);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);


        CheckBoxList9.DataSource = ds2.Tables[0];

        CheckBoxList9.DataTextField = "neirong";
        CheckBoxList9.DataValueField = "neirong"; ;
        CheckBoxList9.DataBind();
        con.Close();
    }
    #endregion

    #region 绑定产品列表

    protected void BindChanPin()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from BaoJiaChanPing where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
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




    #region 绑定测试项目列表

    protected void BindXiangMu()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();

        con.Close();
    }



    #endregion


    #region 绑定原始项目列表

    protected void BindProduct()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from product2 where id in(select xiaoid from baojiacpxiangmu where baojiaid='" + baojiaid + "' and kehuid='" + kehuid + "') ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView6.DataSource = ds.Tables[0];
        GridView6.DataBind();

        con.Close();
    }



    #endregion



    #region 绑定测试项目列表
    protected void Button3_Click(object sender, EventArgs e)
    {
        string shenfen = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sqlshenfen = "select dutyname from userinfo where username='" + Session["UserName"].ToString() + "'";
        SqlCommand cmdshenfen = new SqlCommand(sqlshenfen, con);
        SqlDataReader drshenfen = cmdshenfen.ExecuteReader();
        if (drshenfen.Read())
        {
            shenfen = drshenfen["dutyname"].ToString();
        }
        drshenfen.Close();

        string sql_zhekou = string.Format("select zhehoujia,coupon,Discount,epiboly_Price from BaoJiaBiao where BaoJiaId = '{0}'", baojiaid);
        SqlCommand cmd_bj = new SqlCommand(sql_zhekou, con);
        SqlDataReader dr_bj = cmd_bj.ExecuteReader();
        decimal price = 0m;
        decimal discount = 0m;
        string waibao = "";
        if (dr_bj.Read())
        {
            string coupon = dr_bj["coupon"].ToString();
            string zhehoujia = dr_bj["zhehoujia"].ToString();
            discount = Convert.ToDecimal(dr_bj["Discount"]);
            waibao = dr_bj["epiboly_Price"].ToString();

            if (string.IsNullOrEmpty(coupon) || coupon == "&nbsp;" || coupon == "0.00")
            {
                price = Convert.ToDecimal(zhehoujia);
            }
            else
            {
                price = Convert.ToDecimal(coupon);
            }
        }
        dr_bj.Close();

        decimal b = 0m;
        if (string.IsNullOrEmpty(waibao))
        {

        }
        else
        {
            b = Convert.ToDecimal(waibao) / price;
        }

        string sql = "";
        string sql2 = "";
        decimal kuozhanfei = Convert.ToDecimal(this.txt_kuozhanfei.Text);//扩展费
        if (DropDownList1.SelectedValue == "通过")
        {
            if (shenfen.Trim() == "系统管理员" || shenfen.Trim() == "总经理" || shenfen.Trim() == "董事长")
            {
                sql2 = string.Format("update BaoJiaBiao set filltime='{0}',other='other', weituo='',shenpibiaozhi ='二级通过' where baojiaid='{1}'", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), baojiaid);
                sql = string.Format(@"insert into Approval([bianhao],[leibie],[result],[yijian],[fillname],[filltime])values('" + baojiaid + "','" + shenfen.Trim() + "','二级通过','" + this.TextBox2.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')");
            }
            else
            {
                if (discount >= (decimal)0.8 && b <= (decimal)0.8 && kuozhanfei < 500)
                {
                    sql2 = string.Format("update BaoJiaBiao set filltime='{0}',other='other', weituo='',shenpibiaozhi ='二级通过' where baojiaid='{1}'", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), baojiaid);
                    sql = string.Format(@"insert into Approval([bianhao],[leibie],[result],[yijian],[fillname],[filltime])values('" + baojiaid + "','" + shenfen.Trim() + "','二级通过','" + this.TextBox2.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')");
                }
                else if (((discount >= (decimal)0.55 && discount < (decimal)0.8) || (b <= (decimal)0.9 && b > (decimal)0.8)) && kuozhanfei < 500)
                {
                    sql2 = string.Format("update BaoJiaBiao set filltime='{0}',other='other', weituo='',shenpibiaozhi ='二级通过' where baojiaid='{1}'", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), baojiaid);
                    sql = string.Format(@"insert into Approval([bianhao],[leibie],[result],[yijian],[fillname],[filltime])values('" + baojiaid + "','" + shenfen.Trim() + "','二级通过','" + this.TextBox2.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')");
                }
                else
                {
                    sql2 = string.Format("update BaoJiaBiao set filltime='{0}',other='other', weituo='',shenpibiaozhi ='一级通过' where baojiaid='{1}'", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), baojiaid);
                    sql = string.Format(@"insert into Approval([bianhao],[leibie],[result],[yijian],[fillname],[filltime])values('" + baojiaid + "','" + shenfen.Trim() + "','一级通过','" + this.TextBox2.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')");
                }
            }
        }
        else
        {
            sql2 = "update BaoJiaBiao set filltime='" + DateTime.Now + "', shenpibiaozhi ='否',tijiaobiaozhi='',other='other',tijiaotime='' where baojiaid='" + baojiaid + "'";
            sql = string.Format(@"insert into Approval([bianhao],[leibie],[result],[yijian],[fillname],[filltime])values('" + baojiaid + "','" + shenfen.Trim() + "','" + DropDownList1.SelectedValue + "','" + this.TextBox2.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')");
        }

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
            //****************
            //提交事务
            myTran.Commit();
        }
        catch (Exception err)
        {
            myTran.Rollback();
            throw new ApplicationException("事务操作出错，系统信息：" + err.Message);
        }
        finally
        {
            con.Close();
        }
        //Response.Write("<script>alert('保存成功！');top.main.location.href='../Quotation/QuotationAppFra.aspx?id=shenpi'</script>");
        BindShenPi();
        TextBox2.Text = string.Empty;
        Response.Write("<script>alert('保存成功！')</script>");
        Response.Write("<script>document.location=document.location;</script>");//防止页面样式会变  Response.write输出脚本代码到顶部，会打乱了文档模型
    }
    /// <summary>
    /// 计算折扣
    /// </summary>
    private decimal DC(SqlConnection con)
    {
        string sql_zhekou = string.Format("select * from BaoJiaBiao where BaoJiaId = '{0}'", baojiaid);
        SqlDataAdapter da = new SqlDataAdapter(sql_zhekou, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        decimal discount = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["Discount"]), 2);
        return discount;
    }

    #endregion

    #region 计算项目金额

    protected void Calculate()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sqlsum = "select sum(total) as total from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' group by baojiaid";
        SqlCommand cmdsum = new SqlCommand(sqlsum, con);
        SqlDataReader drsum = cmdsum.ExecuteReader();
        if (drsum.Read())
        {
            if (drsum["total"] == DBNull.Value)
            {
                baojiazong_txt.Text = "0";
            }
            else
            {
                baojiazong_txt.Text = Convert.ToDecimal(drsum["total"]).ToString().Trim();
                baojiazong_txt.Text = Math.Round(Convert.ToDecimal(drsum["total"]), 2).ToString();
            }
        }

        con.Close();

        if (baojiazhekou_txt.Text == "")
        {
            baojiazhekou_txt.Text = "0";
        }

        if (baojiazong_txt.Text == "")
        {
            baojiazong_txt.Text = "0";
        }


        decimal dis = 0;
        if (baojiazong_txt.Text != "0" && baojiazong_txt.Text != "0.00")
        {
            dis = Convert.ToDecimal(baojiazhehou_txt.Text.Trim()) / Convert.ToDecimal(baojiazong_txt.Text.Trim());
        }
        realzhekou_txt.Text = Math.Round(dis, 2).ToString();
    }

    #endregion
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
}