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

public partial class Case_CaseDaiFen3 : System.Web.UI.Page
{
    protected string shwhere = "";
    private int _i = 0;

    private string minId = "0";

    protected void Page_Load(object sender, EventArgs e)
    {


        string quan = "0";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sqldd = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='分派工程师'";

        SqlCommand cmddd = new SqlCommand(sqldd, con);
        SqlDataReader drdd = cmddd.ExecuteReader();
        if (drdd.Read())
        {
            quan = "1";
        }
        drdd.Close();
       

        if (Session["role"].ToString() == "8")
        {

            shwhere = "((fenpaibiaozhi !='是' or fenpaibiaozhi is null) and taskid in (select rwbianhao from anjianinfo2 where renwu='是') and id  in (select baojiaid from ZhuJianEngineer))";
        }
        else if (Session["role"].ToString() == "1" || quan == "1" || Session["role"].ToString() == "3")
        {
            shwhere = "((fenpaibiaozhi !='是' or fenpaibiaozhi is null) and taskid in (select rwbianhao from anjianinfo2 where renwu='是') and id  in (select baojiaid from ZhuJianEngineer) and  bumen =(select departmentname from userinfo where username='" + Session["UserName"].ToString() + "'))";

        }
        else
        {
            shwhere = "((fenpaibiaozhi !='是' or fenpaibiaozhi is null) and taskid in (select rwbianhao from anjianinfo2 where renwu='是') and id  in (select baojiaid from ZhuJianEngineer) and (taskid in (select bianhao from zhujianengineer where name='" + Session["UserName"].ToString() + "')))";

        }


        if (!IsPostBack)
        {

            string cmd = "select count(*) from anjianinfo where " + shwhere;

            if (Request.QueryString["minid"] != null)
            {
                minId = Request.QueryString["minid"].ToString();
            }

            if (minId != "0")
            {
                cmd = "select count(*) from anjianinfo where " + Server.UrlDecode(minId) + " and " + minId + "";

            }

            SqlDataAdapter da = new SqlDataAdapter(cmd, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataView dv = ds.Tables[0].DefaultView;
            AspNetPager1.RecordCount = ds.Tables[0].Rows.Count;
           

            Bind();

        }
        con.Close();
    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        string sql = "select  *,(select ziliaostate from anjianinfo2 where rwbianhao=anjianinfo.taskid) as ziliao,(select yaoqiuwanchengriqi from anjianinfo2 where rwbianhao=anjianinfo.taskid) as yaoqiuwanchengriqi,(select xiadariqi from anjianinfo2 where rwbianhao=anjianinfo.taskid) as xiadariqi,(select kf from anjianinfo2 where rwbianhao=anjianinfo.taskid) as kf,(select shenqingbianhao from anjianinfo2 where rwbianhao=anjianinfo.taskid) as shenqinghao,(select top 1 name from zhujianengineer where baojiaid=anjianinfo.id) as gc,(select top 1 weituodanwei from anjianinfo2 where rwbianhao =anjianinfo.taskid) as kehuname  from anjianinfo where " + shwhere + "  order by "+DropDownList3.SelectedValue+"";

        if (minId != "0")
        {
            sql = "select  *,(select ziliaostate from anjianinfo2 where rwbianhao=anjianinfo.taskid) as ziliao,(select yaoqiuwanchengriqi from anjianinfo2 where rwbianhao=anjianinfo.taskid) as yaoqiuwanchengriqi,(select xiadariqi from anjianinfo2 where rwbianhao=anjianinfo.taskid) as xiadariqi,(select kf from anjianinfo2 where rwbianhao=anjianinfo.taskid) as kf,(select shenqingbianhao from anjianinfo2 where rwbianhao=anjianinfo.taskid) as shenqinghao,(select top 1 name from zhujianengineer where baojiaid=anjianinfo.id) as gc,(select top 1 weituodanwei from anjianinfo2 where rwbianhao =anjianinfo.taskid) as kehuname  from anjianinfo where " + shwhere + " and " + minId + "  order by " + DropDownList3.SelectedValue + "";
                
            
        }


     
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        con.Close();
        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager1.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager1.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();
        
    }

    protected void Button2_Click(object sender, EventArgs e)
    {



        if (this.TextBox1.Text.Trim() == "")
        {
            

        }
        else
        {
            string dd = "(taskid in (select rwbianhao from anjianinfo2 where shengchandanwei like '%"+TextBox1.Text.Trim()+"%') or  bumen like '%" + TextBox1.Text.Trim() + "%' or taskid like '%" + TextBox1.Text.Trim() + "%' or id in (select baojiaid from zhujianengineer where name like '%" + TextBox1.Text.Trim() + "%') or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text.Trim() + "%') or taskid in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text.Trim() + "%'))";
            Response.Redirect("CaseDaiFen3.aspx?minid=" + Server.UrlEncode(dd));


        }


    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");

            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 8);
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;


          

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select state from anjianinfo2 where rwbianhao='" + e.Row.Cells[1].Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                e.Row.Cells[7].Text = dr["state"].ToString();
            }


            con.Close();

        }
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    public string SubStr(string sString, int nLeng)
    {
        if (sString.Length <= nLeng)
        {
            return sString;
        }
        string sNewStr = sString.Substring(0, nLeng);

        return sNewStr;
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();


      
            if (e.CommandName == "cancel1")
            {



                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sqly = "";
                string sqlx = "";
                string zhi = "";
                string sql = "select canyu from anjianinfo where id='" + sid + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    zhi = dr["canyu"].ToString();
                }
                dr.Close();

                if (zhi == "" || zhi == "是")
                {
                    sqly = "update anjianinfo set canyu='否' where id='" + sid + "'";

                    sqlx = "update baogaobumen set beizhu2='否' where rwid=(select top 1 taskid from anjianinfo where id='" + sid + "') and bumen=(select top 1 bumen from anjianinfo where id='" + sid + "')";
                }
                else
                {
                    sqly = "update anjianinfo set canyu='是' where id='" + sid + "'";

                    sqlx = "update baogaobumen set beizhu2='是' where rwid=(select top 1 taskid from anjianinfo where id='" + sid + "') and bumen=(select top 1 bumen from anjianinfo where id='" + sid + "')";


                }
                SqlCommand cmdy = new SqlCommand(sqly, con);
                cmdy.ExecuteNonQuery();


                SqlCommand cmdx = new SqlCommand(sqlx, con);
                cmdx.ExecuteNonQuery();

                con.Close();

                ld.Text = "<script>alert('取消成功!');</script>";
                Bind();
            }
        
    }
}