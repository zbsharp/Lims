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

public partial class Quotation_QuotationApp2 : System.Web.UI.Page
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
            baojiaid = Request.QueryString["baojiaid"].ToString();
            Label3.Text = baojiaid;
            string shenpi = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

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
            Bind();
            BindClause();
            BindBaoJiaBiao();
            //DataBind();
        }
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
            DropDownList3.SelectedValue = dr["zhangdan"].ToString();
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
        string sql = "select * from Approval where bianhao='" + baojiaid + "'";
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


    #region 绑定测试项目列表
    protected void Button3_Click(object sender, EventArgs e)
    {

        decimal admit = 0;
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

        string weituo = "";

        string sqlshenfen1 = "select weituo from baojiabiao where baojiaid='" + baojiaid + "'";
        SqlCommand cmdshenfen1 = new SqlCommand(sqlshenfen1, con);
        SqlDataReader drshenfen1 = cmdshenfen1.ExecuteReader();
        if (drshenfen1.Read())
        {
            weituo = drshenfen1["weituo"].ToString();
        }
        drshenfen1.Close();

        string sql = "";
        string sql2 = "";

      
      
            sql = "insert into Approval values('" + baojiaid + "','领导级别','" + DropDownList1.SelectedValue + "','" + TextBox2.Text + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";

            if (DropDownList1.SelectedValue == "不通过" || DropDownList1.SelectedValue == "待定")
            {
                sql2 = "update BaoJiaBiao set shenpibiaozhi ='" + DropDownList1.SelectedValue + "',other='other',weituo='',tijiaobiaozhi='',tijiaotime='' where baojiaid='" + baojiaid + "'";
            }
            else
            {
                sql2 = "update BaoJiaBiao set shenpibiaozhi ='" + DropDownList1.SelectedValue + "',other='other',weituo='' where baojiaid='" + baojiaid + "'";
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


        Response.Write("<script>alert('保存成功！');top.main.location.href='../Quotation/QuotationAppFra.aspx?id=shenpi2'</script>");


    }

    #endregion

    #region 计算项目金额

    protected void Calculate()
    {

        string su = "";
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

        //  baojiazhehou_txt.Text = (Convert.ToDecimal(baojiazhekou_txt.Text) * Convert.ToDecimal(baojiazong_txt.Text)).ToString();

        //  baojiazhehou_txt.Text  = Math.Round(Convert.ToDecimal(baojiazhehou_txt.Text), 2).ToString();

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
            string xy6 = e.Row.Cells[6].Text.Trim().ToString();
            if (xy6 != "1.00")
            {
                e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
            }

            decimal a = Convert.ToDecimal(e.Row.Cells[5].Text);
            decimal b = Convert.ToDecimal(e.Row.Cells[6].Text);

            decimal dis = 0;
            if (e.Row.Cells[6].Text != "0.00")
            {
                dis = a / b;
            }
            e.Row.Cells[7].Text = Math.Round(dis, 2).ToString();

        }
    }
}