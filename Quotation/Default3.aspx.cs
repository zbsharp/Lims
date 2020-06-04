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
using System.Data.SqlClient;

public partial class Default3 : System.Web.UI.Page
{
    protected string name = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string leibieid = "0";
    protected string chanpinid = "0";
    protected string mq = "";
    protected bool issearch = false;
    protected string searchmessage = "";
    protected string xiangmuname = "";
    protected string cpid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["messagetype"] != null)
        {
            string messagetype = Request.QueryString["messagetype"].ToString();
            if (messagetype == "search")
            {
                issearch = true;
            }
        }
        if (!issearch)
        {

            if (Request.QueryString["leibieid"] != null)
            {
                leibieid = Request.QueryString["leibieid"].ToString();
            }
            if (Request.QueryString["chanpinid"] != null)
            {
                chanpinid = Request.QueryString["chanpinid"].ToString();
            }

        }
        else
        {
            if (Request.QueryString["searchmessage"] != null)
            {
                searchmessage = Request.QueryString["searchmessage"].ToString();
            }
        }
        if (Request.QueryString["xiangmuname"] != null)
        {
            xiangmuname = Request.QueryString["xiangmuname"].ToString();
        }
        if (Request.QueryString["zz"] != null)
        {
            mq = Request.QueryString["zz"].ToString();
        }
        if (Request.QueryString["kehuid"] != null)
        {
            kehuid = Request.QueryString["kehuid"].ToString();
        }
        if (Request.QueryString["baojiaid"] != null)
        {
            baojiaid = Server.UrlDecode(Request.QueryString["baojiaid"].ToString());
        }
        //加载时传过来的产品ID
        if (Request.QueryString["cpid"] != null)
        {
            cpid = Request.QueryString["cpid"].ToString();
        }

        if (!IsPostBack)
        {
            CPload();
            BandingAddressOne1(name);
            Bind_cpxm();
        }
    }
    public void BandingAddressOne1(string name)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        DataSet ds = new DataSet();
        string sql = "";
        if (!issearch)
        {
            //sql = "select *  from product2 where leibieid='" + leibieid + "' and chanpinid='" + chanpinid + "' order by convert(int, neirongid) asc";
            sql = "select *  from product2 where leibieid='" + leibieid + "' and neirong like '%" + xiangmuname + "%' order by convert(int, neirongid) asc";
        }
        else
        {
            sql = "select *  from product2 where neirong like '%" + searchmessage + "%' or biaozhun like '%" + searchmessage + "%' or chanpinname like '%" + searchmessage + "%' order by convert(int, neirongid) asc";
        }

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    Label1.Text = ds.Tables[0].Rows[0]["chanpinname"].ToString();
        //}
        con.Close();
    }
    /// <summary>
    /// 加载所添加的产品信息
    /// </summary>
    public void CPload()
    {
        //-------------2019-8-16修改
        //string sql = "select * from BaoJiaChanPing where BaoJiaID='" + baojiaid + "' and KeHuId='" + kehuid + "' and BaoJiaID!=''";
        //SqlDataAdapter da = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //con.Close();
        //DropDownList1.DataSource = ds.Tables[0];
        //DropDownList1.DataTextField = "name";
        //DropDownList1.DataValueField = "id";
        //DropDownList1.DataBind();
        if (baojiaid != "<%=baojiaid)%>" && kehuid != "<%=kehuid %>" && !string.IsNullOrEmpty(cpid))
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select * from BaoJiaChanPing where id=" + cpid + " and BaoJiaID='" + baojiaid + "' and KeHuId='" + kehuid + "' and BaoJiaID!=''";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            if (dr.Read())
            {
                lie_name.Text = dr["name"].ToString();
                lie_type.Text = dr["type"].ToString();
                lbname.Visible = true;
                lbtype.Visible = true;
            }
            dr.Close();
            con.Close();
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(baojiaid))
        {
            Response.Write("<script>alert('必须先开始报价和保存产品，请关闭该页面后重新打开报价页面!');window.close();</script>");
            return;
        }
        Label_notice.Text = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string chanpingid = "";
        string ssss = cpid;
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            if (hzf.Checked)
            {
                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                //string sqlchongfu = "select * from BaoJiaCPXiangMu where baojiaid='"+baojiaid+"' and xiaoid='"+sid+"'";
                //SqlCommand cmdcf = new SqlCommand(sqlchongfu,con);
                //SqlDataReader drcf = cmdcf.ExecuteReader();
                //if (drcf.Read())
                //{
                //    drcf.Close();
                //}
                //else
                //drcf.Close();
                string cs_txt = "";
                string csbz_txt = "";
                string csnr_txt = "";
                string csyp_txt = "";
                string csfy_txt = "";
                string cszq_txt = "";
                string neirongid = "";
                string id = "";
                string depaname = "";
                string sql3 = "select * from product2 where id='" + sid + "'";
                SqlCommand cmd3 = new SqlCommand(sql3, con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                if (dr3.Read())
                {
                    cs_txt = dr3["neirong"].ToString();
                    csbz_txt = dr3["biaozhun"].ToString();
                    csnr_txt = dr3["beizhu"].ToString();
                    csyp_txt = dr3["yp"].ToString();
                    csfy_txt = dr3["shoufei"].ToString();
                    cszq_txt = dr3["zhouqi"].ToString();
                    neirongid = dr3["neirongid"].ToString();
                    depaname = dr3["leibiename"].ToString();
                    id = dr3["id"].ToString();
                }
                dr3.Close();
                //**************2019-8-16
                //string maxid = "select top 1 id from BaoJiaChanPing where baojiaid='" + baojiaid + "' order by id desc";
                //SqlCommand cmdmax = new SqlCommand(maxid, con);
                //SqlDataReader drmax = cmdmax.ExecuteReader();
                //if (drmax.Read())
                //{
                //    chanpingid = drmax["id"].ToString();
                //}
                //drmax.Close();

                //chanpingid = DropDownList1.SelectedValue;

                string sql = "insert into BaoJiaCPXiangMu values('" + baojiaid + "','" + kehuid + "','" + cpid + "','" + cs_txt + "','" + csbz_txt + "','','" + csyp_txt + "','" + Convert.ToDecimal(csfy_txt) + "','1','1','" + csnr_txt + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + Session["UserName"].ToString() + "','" + cszq_txt + "','否','','1900-1-1','','','" + leibieid + "','" + chanpinid + "','" + sid + "','" + Convert.ToDecimal(csfy_txt) + "','否','','','非外包','','" + depaname + "','0.00')";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                decimal ticheng = 0;

                decimal fenmu = 0;
                decimal fenzi = 0;

                string xid = "";
                string sql31 = "select top 1 * from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "' order by id desc";
                SqlCommand cmd31 = new SqlCommand(sql31, con);
                SqlDataReader dr31 = cmd31.ExecuteReader();
                if (dr31.Read())
                {
                    fenmu = Convert.ToDecimal(dr31["yuanshi"]);
                    fenzi = Convert.ToDecimal(dr31["feiyong"]);
                    xid = dr31["id"].ToString();
                }
                if (fenmu != 0)
                {
                    ticheng = fenzi / fenmu;
                }
                dr31.Close();


                string sqltic = "update baojiacpxiangmu set zhekou='" + Convert.ToDecimal(ticheng) + "' where id='" + xid + "'";
                SqlCommand cmdti = new SqlCommand(sqltic, con);
                cmdti.ExecuteNonQuery();

                string sql85 = "select top 1 hesuanname from baojiacpxiangmu where baojiaid='" + baojiaid + "' and hesuanname='是' order by id desc";
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


                if (neirongid == "0")
                {
                    return;
                }

            }
        }
        con.Close();
        Label_notice.Text = "添加成功";
        Bind_cpxm();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    /// <summary>
    /// 修改外包项目
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(this.epiboly_shoufei.Text.Trim().ToString()) || this.epiboly_shoufei.Text.Trim().ToString() == "0")
        //{
        //    Response.Write("<script>alert('价格不能为空')</script>");
        //    return;
        //}
        //else if (this.epiboly_shoufei.Text.Trim().ToString().Substring(0, 1) == "0")
        //{
        //    Response.Write("<script>alert('价格输入不合法')</script>");
        //    return;
        //}
        //else
        //{
        //    string id = this.epiboly_id.Text.Trim().ToString();
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        //    {
        //        con.Open();
        //        string sql = string.Format(@"update Product2 set shoufei='{0}' ,neirongid='{1}',neirong='{2}',
        //                                biaozhun='{3}',yp='{4}',zhouqi='{5}',beizhu='{6}',
        //                                danwei='{7}' where id={8}", this.epiboly_shoufei.Text.Trim().ToString(), this.epiboly_neirongid.Text.ToString(),
        //                                this.epiboly_neirong.Text.ToString(),
        //                                this.epiboly_biaozhun.Text.ToString(),
        //                                this.epiboly_yp.Text.ToString(), this.epiboly_zhouqi.Text.ToString(), this.epiboly_beizhu.Text.ToString(), this.epiboly_danwei.Text.ToString(), id);
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.ExecuteNonQuery();
        //        BandingAddressOne1(name);
        //    }
        //}
    }
    protected void Button_close_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();window.location.reload();</script>");
    }
    /// <summary>
    /// 加载已添加的测试项目
    /// </summary>
    protected void Bind_cpxm()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select bumen,ceshiname,biaozhun,feiyong from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView2.DataSource = ds.Tables[0];
            GridView2.DataBind();
        }
    }
}