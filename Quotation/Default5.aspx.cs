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

public partial class Quotation_Default5 : System.Web.UI.Page
{
    protected string name = "";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string leibieid = "0";
    protected string chanpinid = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["leibieid"] != null)
        {
            leibieid = Request.QueryString["leibieid"].ToString();
        }

        if (Request.QueryString["chanpinid"] != null)
        {
            chanpinid = Request.QueryString["chanpinid"].ToString();
        }
        if (Request.QueryString["kehuid"] != null)
        {
            kehuid = Request.QueryString["kehuid"].ToString();
        }
        if (Request.QueryString["baojiaid"] != null)
        {

            baojiaid = Server.UrlDecode(Request.QueryString["baojiaid"].ToString());
        }
        if (!IsPostBack)
        {



            BandingAddressOne1(name);




        }
    }

    public void BandingAddressOne1(string name)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        DataSet ds = new DataSet();

        string sql = "";

        sql = "select *  from product2 where leibieid='" + leibieid + "' and chanpinid='" + chanpinid + "' order by convert(int, neirongid) asc";




        SqlDataAdapter da = new SqlDataAdapter(sql, con);

        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        if (ds.Tables[0].Rows.Count > 0)
        {
            Label1.Text = ds.Tables[0].Rows[0]["chanpinname"].ToString();
        }
        con.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string chanpingid = "";
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
                {

                    //drcf.Close();


                    string cs_txt = "";
                    string csbz_txt = "";
                    string csnr_txt = "";
                    string csyp_txt = "";
                    string csfy_txt = "";
                    string cszq_txt = "";
                    string neirongid = "";
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
                    }
                    dr3.Close();
                    string maxid = "select top 1 id from BaoJiaChanPing where baojiaid='" + baojiaid + "' order by id desc";
                    SqlCommand cmdmax = new SqlCommand(maxid, con);
                    SqlDataReader drmax = cmdmax.ExecuteReader();
                    if (drmax.Read())
                    {
                        chanpingid = drmax["id"].ToString();
                    }
                    drmax.Close();
                   


                    string sql = "insert into BaoJiaCPXiangMu values('" + baojiaid + "','" + kehuid + "','" + chanpingid + "','" + cs_txt + "','" + csbz_txt + "','','" + csyp_txt + "','" + Convert.ToDecimal(csfy_txt) + "','1','1','" + csnr_txt + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + Session["UserName"].ToString() + "','" + cszq_txt + "','否','','1900-1-1','','','" + leibieid + "','" + chanpinid + "','" + sid + "','" + Convert.ToDecimal(csfy_txt) + "','否','','')";
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

                    if (ticheng <= Convert.ToDecimal(0.85))
                    {
                        string sql4 = "update BaoJiaCPXiangMu set hesuanname='是' where baojiaid='" + baojiaid + "'";
                        SqlCommand cmd4 = new SqlCommand(sql4, con);
                        cmd4.ExecuteNonQuery();

                    }
                    else
                    {
                        string sql4 = "update BaoJiaCPXiangMu set hesuanname='' where baojiaid='" + baojiaid + "'";
                        SqlCommand cmd4 = new SqlCommand(sql4, con);
                        cmd4.ExecuteNonQuery();
                    }

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

                        Response.Write("<script>window.close();window.location.reload();</script>");
                        return;
                    }
                }
            }
        }
        Response.Write("<script>window.close();window.location.reload();</script>");
        con.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();



        string sql = "";

        sql = "delete  from product2 where leibieid='" + leibieid + "' and chanpinid='" + chanpinid + "' ";

        SqlCommand cmd = new SqlCommand(sql,con);
        cmd.ExecuteNonQuery();
        con.Close();

        Response.Write("<script>alert('删除成功');</script>");

    }
}