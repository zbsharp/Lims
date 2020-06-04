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
using Common;
using DBBLL;
using DBTable;

public partial class CCSZJiaoZhun_htw_ChanpinAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='价格增改'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
            if (!IsPostBack)
            {
                //ShenheItemsList();
                Bumen_list();
            }
        }
        else
        {
            con.Close();
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');top.main.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_chanpin.Text) || string.IsNullOrEmpty(txt_biaozhun.Text) || string.IsNullOrEmpty(txt_price.Text) || string.IsNullOrEmpty(txt_xm.Text))
        {
            Literal1.Text = "<script>alert('产品、项目、标准、价格不能存在空值')</script>";
        }
        else
        {
            string bumen = DropDownList1.SelectedValue;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                //生成类别编号、产品编号、项目编号
                //1.通过部门查询出类别编号和最大项目编号
                //2.通过部门和产品名称查询出产品id、如果没有查到则使用部门查处最大产品编号   
                string leibieid = "", chanpinid = "", neirongid = "";
                string sql_leibieid = "select top 1 leibieid,neirongid from Product2 where leibiename='" + bumen + "' order by neirongid desc";
                SqlCommand cmd_leibieid = new SqlCommand(sql_leibieid, con);
                SqlDataReader dr_leibieid = cmd_leibieid.ExecuteReader();
                if (dr_leibieid.Read())
                {
                    leibieid = dr_leibieid["leibieid"].ToString();
                    string i_tostring = dr_leibieid["neirongid"].ToString();
                    int i = Convert.ToInt32(i_tostring);
                    i++;
                    neirongid = i.ToString();
                }
                dr_leibieid.Close();

                string sql_chanpin = " select top 1 chanpinid from Product2 where leibiename='" + bumen + "' and chanpinname='" + txt_chanpin.Text + "'";
                SqlCommand cmd_chanpin = new SqlCommand(sql_chanpin, con);
                SqlDataReader dr_chanpin = cmd_chanpin.ExecuteReader();
                if (dr_chanpin.Read())
                {
                    chanpinid = dr_chanpin["chanpinid"].ToString();
                    dr_chanpin.Close();
                }
                else
                {
                    dr_chanpin.Close();
                    string sql_cp = "select top 1 chanpinid from Product2 where  leibiename='" + bumen + "' order by chanpinid desc";
                    SqlCommand cmd_cp = new SqlCommand(sql_cp, con);
                    SqlDataReader dr_cp = cmd_cp.ExecuteReader();
                    if (dr_cp.Read())
                    {
                        string i_tostring = dr_cp["chanpinid"].ToString();
                        int i = Convert.ToInt32(i_tostring);
                        i++;
                        chanpinid = i.ToString();
                    }
                    dr_cp.Close();
                }


                //插入数据
                string sql_add = @"insert into Product2(leibieid,leibiename,chanpinid,chanpinname,neirongid,neirong,biaozhun,shoufei,yp,zhouqi,beizhu,danwei)
                                    values('" + leibieid + "','" + bumen + "','" + chanpinid + "','" + txt_chanpin.Text + "','" + neirongid + "','" + txt_xm.Text + "','" + txt_biaozhun.Text + "','" + txt_price.Text + "','" + txt_yp.Text + "','" + txt_zhouqi.Text + "','" + txt_beizhu.Text + "','" + txt_danwei.Text + "')";
                SqlCommand cmd_add = new SqlCommand(sql_add, con);
                int rows = cmd_add.ExecuteNonQuery();
                if (rows > 0)
                {
                    ShenheItemsList();
                }
            }
        }
    }

    protected void ShenheItemsList()
    {
        Literal1.Text = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select top 1 * from Product2 order by id desc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager2.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "delete from Product2 where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();
        GridView1.DataSource = "";
        GridView1.DataBind();
        //ShenheItemsList();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        //ShenheItemsList();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Oldquatation_In.aspx");
    }
    private void Bumen_list()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from UserDepa where departmentid='10' or departmentid='11'  or  departmentid='12' or departmentid='13' or departmentid='14' or departmentid='15' or departmentid='1018' ";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DropDownList1.DataSource = ds.Tables[0];
            DropDownList1.DataValueField = "name";
            DropDownList1.DataTextField = "name";
            DropDownList1.DataBind();
        }
    }
}
