using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Common;
public partial class Customer_CustTraceAdd : System.Web.UI.Page
{
    #region 初始绑定
    protected string name;
    protected string id;
    protected string username = "";
    protected string dt = "";
    protected string kehuid = "";
    protected string kehuleixing = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"] == null)
        {
            Response.Write("<script>alert('请先登录!');window.location.href='../Login.aspx'</script>");
        }

        else
        {

            id = Request.QueryString["kehuid"].ToString();
            username = Session["UserName"].ToString();
            dt = DateTime.Now.ToShortDateString();


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select customname,kehuid,CustomType from customer where kehuid='" + id + "' ";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                name = dr["customname"].ToString();
                kehuid = dr["kehuid"].ToString();
                kehuleixing = dr["CustomType"].ToString();

            }
            dr.Close();


            if (!IsPostBack)
            {
                Bind();

                txFDate.Value = DateTime.Now.ToShortDateString();
            }

            con.Close();
            con.Dispose();


            //DataBind();
        }

    }

    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CustomerTrace where  kehuid='" + id + "' order by genzongid desc ";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();


        string sql2 = "select * from CustomerLinkMan where  customerid='" + id + "' ";
        SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);
        DropDownList1.DataSource = ds2.Tables[0];
        DropDownList1.DataTextField = "name";
        DropDownList1.DataValueField = "name";
        DropDownList1.DataBind();


        con.Close();
        con.Dispose();

    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    #endregion

    #region 保存联系记录
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Text1.Value.Trim() == "")
        {
            Text1.Value = DateTime.Now.AddDays(7).ToShortDateString();
        }
        int days = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string sql4 = "select * from renwu where bianhao='" + id + "' and jieshouren='" + Session["UserName"].ToString() + "'";
            SqlCommand cmd4 = new SqlCommand(sql4, con);
            SqlDataReader dr = cmd4.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                string sql = "insert into CustomerTrace values('" + id + "','" + txTitle.Value.Trim() + "','" + txContent.Value.Trim() + "','" + DateTime.Now.Date + "','" + Session["UserName"].ToString() + "','" + Label1.Text + "','" + DropDownList1.SelectedValue + "','','" + Text1.Value.Trim() + "','" + DropDownList2.SelectedValue + "','" + DropDownList3.SelectedValue + "','" + Session["UserName"].ToString() + "')";
                string sql5 = "update renwu set renwutime='" + Convert.ToDateTime(Text1.Value.Trim()) + "' where bianhao='" + id + "' and jieshouren='" + Session["UserName"].ToString() + "'";
                SqlCommand com = new SqlCommand(sql, con);
                SqlCommand cmd5 = new SqlCommand(sql5, con);
                com.ExecuteNonQuery();
                cmd5.ExecuteNonQuery();
                string WorkInfo = "添加了" + name + "的" + txTitle.Value.Trim() + "跟踪记录";
                searchwhere.InsertWorkLog(Session["UserName"].ToString(), DateTime.Now.ToString(), WorkInfo);
                Bind();
            }
            else
            {
                dr.Close();
                string sql = "insert into CustomerTrace values('" + id + "','" + txTitle.Value.Trim() + "','" + txContent.Value.Trim() + "','" + DateTime.Now.Date + "','" + Session["UserName"].ToString() + "','" + Label1.Text + "','" + DropDownList1.SelectedValue + "','','" + Text1.Value.Trim() + "','" + DropDownList2.SelectedValue + "','" + DropDownList3.SelectedValue + "','" + Session["UserName"].ToString() + "')";
                string sql2 = "update customer set pubtime3='" + DateTime.Now.AddDays(days) + "' where kehuid='" + id + "'";
                string sql3 = "insert into renwu values('跟踪提醒','" + DateTime.Now + "','未处理','" + Session["UserName"].ToString() + "','" + id + "','" + Session["UserName"].ToString() + "','" + Convert.ToDateTime(Text1.Value.Trim()) + "','否','" + DateTime.Now.AddDays(days) + "','','')";
                SqlCommand com1 = new SqlCommand(sql2, con);
                SqlCommand com = new SqlCommand(sql, con);
                SqlCommand com3 = new SqlCommand(sql3, con);
                string WorkInfo = "添加了" + name + "的" + txContent.Value.Trim() + "跟踪记录";
                searchwhere.InsertWorkLog(Session["UserName"].ToString(), DateTime.Now.ToString(), WorkInfo);
                com.ExecuteNonQuery();
                com1.ExecuteNonQuery();
                com3.ExecuteNonQuery();
            }
            Bind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            con.Close();
            Bind();
        }
        Bind();
    }
    #endregion

    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView2.EditIndex = e.NewEditIndex;
        BindTrace();
    }
    protected void BindTrace()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CustomerTrace where kehuid='" + id + "'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();

        con.Close();
    }

    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //取消更新
        this.GridView2.EditIndex = -1;
        BindTrace();
    }

    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //更新
        string id = this.GridView2.DataKeys[e.RowIndex].Value.ToString();//获取客户日志表的id
        string neirong = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());//内容
        string style = ((DropDownList)GridView2.Rows[e.RowIndex].FindControl("funtion")).SelectedValue;//拜访方式
        string sql = string.Format("update [dbo].[CustomerTrace] set neirong='{0}',style='{1}' where genzongid={2}", neirong, style, id);
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            this.GridView2.EditIndex = -1;
            BindTrace();
        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = this.GridView2.DataKeys[e.RowIndex].Value.ToString();
        string sql = string.Format("delete from  [CustomerTrace] where genzongid='{0}'", id);
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            BindTrace();
        }
    }
}