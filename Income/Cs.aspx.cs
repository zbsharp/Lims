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

public partial class Income_Cs : System.Web.UI.Page
{
    string kfs = "";
    string dutyname = "";
    string dn = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string posistion = Position();
        if (posistion.Trim() == "测试员")
        {
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sqldn = "select * from userinfo where username='" + Session["UserName"].ToString() + "'";


        SqlCommand cmdstate = new SqlCommand(sqldn, con);
        SqlDataReader dr = cmdstate.ExecuteReader();
        if (dr.Read())
        {
            dn = dr["departmentname"].ToString();

            dutyname = dr["dutyname"].ToString();
        }

        dr.Close();
        con.Close();
        
        if (dutyname.Trim() == "客户经理")
        {
            kfs = " baojiaid in  (select baojiaid from baojiabiao where responser  in (select username from userinfo where departmentname='" + dn + "'))";
        }
        else if (dutyname == "系统管理员")
        {
            kfs = " 1=1 ";
        }
        else if (dutyname == "业务员")
        {
            kfs = " baojiaid in  (select baojiaid from baojiabiao where responser='" + Session["UserName"].ToString() + "')  order by id desc  ";
        }
        else if (dutyname == "客服人员")
        {
            kfs = "baojiaid in (select baojiaid from BaoJiaBiao where responser in (select marketid from CustomerServer where UserName='"+Session["Username"].ToString()+"'))";
        }
        else if (dutyname == "客服经理")
        {
            kfs = " 1=1  ";
        }
        else
        {
            kfs = " baojiaid in  (select baojiaid from baojiabiao where responser='" + Session["UserName"].ToString() + "') ";
        }
        if (!IsPostBack)
        {
            Bind3();
        }
    }

    public void Bind3()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select  *,(select customname from customer where kehuid=anjianinfo2.kehuid) as kh  from anjianinfo2 where convert(datetime,yuji) < '" + DateTime.Today + "' and " + kfs + " order by id desc";


        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();

        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager2.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();


    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";


        if (DropDownList1.SelectedValue == "全部")
        {
            sql = "select  *,(select customname from customer where kehuid=anjianinfo2.kehuid) as kh  from anjianinfo2 where convert(datetime,yuji) < '" + DateTime.Now + "' order by convert(datetime,xiadariqi) desc";
        }
        else if (DropDownList1.SelectedValue == "完成")
        {
            sql = "select  *,(select customname from customer where kehuid=anjianinfo2.kehuid) as kh  from anjianinfo2 where convert(datetime,yuji) < '" + DateTime.Now + "' and state='" + DropDownList1.SelectedValue + "' order by convert(datetime,xiadariqi) desc";
 
        }
        else if (DropDownList1.SelectedValue == "未完成")
        {
            sql = "select  *,(select customname from customer where kehuid=anjianinfo2.kehuid) as kh  from anjianinfo2 where convert(datetime,yuji) < '" + DateTime.Now + "' and state !='完成' order by convert(datetime,xiadariqi) desc";

        }
        else if (DropDownList1.SelectedValue == "客户名称")
        {
            sql = "select  *,(select customname from customer where kehuid=anjianinfo2.kehuid) as kh  from anjianinfo2 where convert(datetime,yuji) < '" + DateTime.Now + "' and kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text.Trim() + "%') order by convert(datetime,xiadariqi) desc";

        }

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();

        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager2.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager2.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager2.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {


        string sqlstr;




        //sqlstr = "select * from customer order by fillname";

        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();
        //SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        //DataSet ds = new DataSet();
        //da.Fill(ds);

        //GridView1.DataSource = ds.Tables[0];
        //GridView1.DataBind();
        //con.Close();
        //con.Dispose();


        DisableControls(GridView1);

        Response.ClearContent();

        Response.AddHeader("content-disposition", "attachment; filename=IncomeList" + DateTime.Now.ToShortDateString() + ".xls");

        Response.ContentType = "application/ms-excel";

        Response.Charset = "UTF-8";

        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");


        StringWriter sw = new StringWriter();

        HtmlTextWriter htw = new HtmlTextWriter(sw);

        GridView1.RenderControl(htw);

        Response.Write(sw.ToString());

        Response.End();
    }

    private void DisableControls(Control gv)
    {

        LinkButton lb = new LinkButton();

        Literal l = new Literal();

        string name = String.Empty;

        for (int i = 0; i < gv.Controls.Count; i++)
        {

            if (gv.Controls[i].GetType() == typeof(LinkButton))
            {

                l.Text = (gv.Controls[i] as LinkButton).Text;

                gv.Controls.Remove(gv.Controls[i]);

                gv.Controls.AddAt(i, l);

            }

            else if (gv.Controls[i].GetType() == typeof(DropDownList))
            {

                l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;

                gv.Controls.Remove(gv.Controls[i]);

                gv.Controls.AddAt(i, l);

            }



            if (gv.Controls[i].HasControls())
            {

                DisableControls(gv.Controls[i]);

            }

        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind3();
    }
    /// <summary>
    /// 查看职位
    /// </summary>
    /// <returns></returns>
    protected string Position()
    {
        string position = "";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sqldd = "select dutyname from UserInfo where UserName='" + Session["Username"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sqldd, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                position = dr["dutyname"].ToString();
            }
            dr.Close();
        }
        return position;
    }
}