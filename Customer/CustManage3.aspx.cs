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

public partial class Customer_CustManage3 : System.Web.UI.Page
{
    private int _i = 0;
    private int _i2 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            limit("客户列表");
            BindMyFill();
        }
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
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {

        BindMyFill();

    }

    protected void BindMyFill()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "";
        DataTable da_user = Dutyname();
        string dutyname = da_user.Rows[0]["dutyname"].ToString();//职位
        string dn = da_user.Rows[0]["departmentname"].ToString();//部门

        if (dutyname.Trim() == "系统管理员" || (dutyname.Trim() == "总经理" && dn == "总经办") || dutyname.Trim() == "董事长" || dutyname.Trim().Contains("财务") || Session["Username"].ToString() == "周琴")
        {
            sql = "select  * from Customer where Kehuid not like 'D%' order by id desc";
        }
        else
        {
            //销售经理
            sql = "select * from Customer where Kehuid in  (select customerid from Customer_Sales where (responser in (select  name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or responser = '" + Session["Username"].ToString() + "'))  and  Kehuid not like 'D%'";
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

        //if (dutyname.Trim() == "系统管理员" || (dutyname.Trim() == "总经理" && dn == "总经办") || dutyname.Trim() == "董事长" || dutyname.Trim().Contains("财务"))
        //{
        //    GridView1.Columns[8].Visible = true;
        //}
        //else
        //{
        //    GridView1.Columns[8].Visible = false;
        //}
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        AspNetPager1.Visible = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseID = DropDownList1.SelectedValue.Trim();
        string value = TextBox1.Text.Trim();
        string sql = "";

        DataTable da_user = Dutyname();
        string dutyname = da_user.Rows[0]["dutyname"].ToString();//职位
        string dn = da_user.Rows[0]["departmentname"].ToString();//部门

        if (DropDownList1.SelectedValue != "contact")
        {
            if (dutyname.Trim() == "系统管理员" || (dutyname.Trim() == "总经理" && dn == "总经办") || dutyname.Trim() == "董事长" || dutyname.Trim().Contains("财务"))
            {
                sql = "select * from Customer where  " + ChooseID + " like '%" + value + "%'  and  Kehuid not like 'D%'";
            }
            else
            {
                sql = "select * from Customer where Kehuid in  (select customerid from Customer_Sales where (responser in (select  name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or responser = '" + Session["Username"].ToString() + "')) and " + ChooseID + " like '%" + value + "%'  and  Kehuid not like 'D%'";
            }
        }
        else
        {
            if (dutyname.Trim() == "系统管理员" || (dutyname.Trim() == "总经理" && dn == "总经办") || dutyname.Trim() == "董事长" || dutyname.Trim().Contains("财务"))
            {
                sql = "select * from Customer where  kehuid in (select customerid from CustomerLinkMan where name like '%" + TextBox1.Text.Trim() + "%')  and  Kehuid not like 'D%'";
            }
            else
            {
                sql = "select * from Customer where kehuid in (select customerid from CustomerLinkMan where name like '%" + TextBox1.Text.Trim() + "%') and Kehuid in (select customerid from Customer_Sales where (responser in (select  name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or responser = '" + Session["Username"].ToString() + "'))  and  Kehuid not like 'D%'";
            }
        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        //if (dutyname.Trim() == "系统管理员" || (dutyname.Trim() == "总经理" && dn == "总经办") || dutyname.Trim() == "董事长" || dutyname.Trim().Contains("财务"))
        //{
        //    GridView1.Columns[8].Visible = true;
        //}
        //else
        //{
        //    GridView1.Columns[8].Visible = false;
        //}
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
        }
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
    /// <summary>
    /// 返回登录进来人的职位
    /// </summary>
    /// <returns></returns>
    protected DataTable Dutyname()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlDataAdapter dr = new SqlDataAdapter(sql_dutyname, con);
            DataSet ds = new DataSet();
            dr.Fill(ds);
            return ds.Tables[0];
        }
    }
}