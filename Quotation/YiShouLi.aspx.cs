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


public partial class Quotation_YiShouLi : System.Web.UI.Page
{

    private int _i = 0;
    protected string ys = "";
    protected StringBuilder strSql = new StringBuilder();
    protected StringBuilder strSql2 = new StringBuilder();
    private string minId = "0";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["ys"] != null)
        {
            ys = Request.QueryString["ys"].ToString();
        }
        Label3.Visible = true;

        strSql.Append("select state  as state1,");
        strSql.Append("(select top 1 customtype from customer where kehuid=anjianinfo2.kehuid ) as ct,");
        strSql.Append("(select top 1 name from ZhuJianEngineer where bianhao=anjianinfo2.rwbianhao ) as name1,");
        strSql.Append("(select top 1 fenpainame from anjianinfo where tijiaobianhao=Anjianinfo2.bianhao and type='是') as fenpainame,");
        strSql.Append("(select responser from BaoJiaBiao where BaoJiaBiao.BaoJiaId=AnJianInFo2.baojiaid) as yewu,");
        strSql.Append("(select top 1 baogaoid from baogao2 where tjid=AnJianInFo2.rwbianhao and beizhu1!='是') as baogaoid,");
        strSql.Append("* from Anjianinfo2 ");

        if (!IsPostBack)
        {
            txFDate.Value = DateTime.Now.AddMonths(-12).ToString("yyyy-MM-01");
            DateTime lastDay = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddHours(23);
            txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();
            string dutyname = DutyName();
            if (dutyname.Trim() != "系统管理员" && dutyname.Trim() != "客户经理" && dutyname.Trim() != "客服经理" && dutyname.Trim() != "客服人员" && dutyname.Trim() != "销售助理")
            {
                GridView1.Columns[17].Visible = false;
            }
            TimeBind();
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


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string sqlstr = "";
        string dutyname = "";//职位
        string dn = "";//部门
                       //获取当前登录进来的人的职位和部门
        using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con1.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con1);
            SqlDataReader dr = cmdstate.ExecuteReader();
            if (dr.Read())
            {
                dn = dr["departmentname"].ToString();
                dutyname = dr["dutyname"].ToString();
            }
            dr.Close();
        }
        string tiaojian = "";
        if (DropDownList1.SelectedValue == "全选")
        {
            tiaojian = "1=1 and";
        }
        else if (DropDownList1.SelectedValue == "weituodanwei")
        {
            tiaojian = "weituodanwei like '%" + TextBox1.Text + "%' and ";
        }
        else
        {
            tiaojian = "" + DropDownList1.SelectedValue + " like '%" + TextBox1.Text.Trim().ToString() + "%' and";
        }
        if (dutyname.Trim() == "系统管理员" || dutyname.Trim() == "客服经理" || (dutyname.Trim() == "总经理" && dn == "总经办") || dutyname.Trim() == "董事长" || dutyname.Trim() == "样品管理员" || dutyname.Trim() == "客服人员")
        {
            sqlstr = strSql + "where  " + tiaojian + " convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and rwbianhao not like 'D%' order by id  desc ";
        }
        else if (dutyname.Trim() == "客户经理")
        {
            sqlstr = strSql + "where " + tiaojian + " convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'and baojiaid in  (select baojiaid from baojiabiao where (responser  in (select name2 from PersonConfig where name1='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "'))   and rwbianhao not like 'D%' order by id  desc ";
        }
        else if (dutyname.Trim() == "业务员")
        {
            sqlstr = strSql + "where " + tiaojian + " convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'and baojiaid in  (select baojiaid from baojiabiao where responser='" + Session["Username"].ToString() + "')  and rwbianhao not like 'D%' order by id  desc ";
        }
        else if (dutyname.Trim() == "销售助理")
        {
            sqlstr = strSql + "where " + tiaojian + " convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'and baojiaid in  (select baojiaid from baojiabiao where (responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "'))  and rwbianhao not like 'D%' order by id  desc ";
        }
        else if (dutyname.Trim() == "总经理")
        {
            sqlstr = strSql + "where " + tiaojian + " convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'and baojiaid in (select baojiaid from baojiabiao where responser in (select username from userinfo where departmentname='" + dn + "')) and rwbianhao not like 'D%' order by id  desc ";
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
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

    protected void TimeBind(string a, string b)
    {
        string ChooseID = a;
        string ChooseValue = b;
        string sqlstr = "";

        if (DropDownList1.SelectedValue == "kehuname")
        {
            sqlstr = strSql2 + " where  ( fukuan like '%" + ChooseValue + "%' or weituo like '%" + ChooseValue + "%' or shenchan like '%" + ChooseValue + "%' or kehuid in (select kehuid from customer where customname like '%" + ChooseValue + "%')) and   convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'  and rwbianhao not like 'D%' order by substring(rwbianhao,4,5) desc ";
        }
        else if (DropDownList1.SelectedValue == "b3")
        {
            sqlstr = strSql2 + " where  ((shenqingbianhao like '%ccc10%' or shenqingbianhao like '%-1001-%' or shenqingbianhao like '%CQC004014%')) and   convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'  and rwbianhao not like 'D%' order by substring(rwbianhao,4,5) desc ";
        }
        else
        {
            sqlstr = strSql2 + " where " + DropDownList1.SelectedValue + " like '%" + ChooseValue + "%' and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'  and rwbianhao not like 'D%' order by substring(rwbianhao,4,5) desc";
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

    }

    protected void TimeBind()
    {
        string sqlstr = "";
        string dutyname = "";//职位
        string dn = "";//部门
                       //获取当前登录进来的人的职位和部门
        using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con1.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con1);
            SqlDataReader dr = cmdstate.ExecuteReader();
            if (dr.Read())
            {
                dn = dr["departmentname"].ToString();
                dutyname = dr["dutyname"].ToString();
            }
            dr.Close();
        }
        //1.系统管理员，客服人员查看所有任务
        //2.销售经理查看自己部门的任务
        //3.业务员查看自己的任务
        if (dutyname.Trim() == "系统管理员" || dutyname.Trim() == "客服经理" || (dutyname.Trim() == "总经理" && dn == "总经办") || dutyname.Trim() == "董事长" || dutyname.Trim() == "客服人员" || dutyname.Trim() == "样品管理员")
        {
            sqlstr = strSql + "where  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and rwbianhao not like 'D%' order by id  desc ";
        }
        else if (dutyname.Trim() == "客户经理")
        {
            sqlstr = strSql + "where  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'and  baojiaid in  (select baojiaid from baojiabiao where (responser  in (select name2 from PersonConfig where name1='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "')) and rwbianhao not like 'D%' order by id  desc ";
        }
        else if (dutyname.Trim() == "总经理")
        {
            sqlstr = strSql + "where  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'and baojiaid in  (select baojiaid from baojiabiao where responser  in (select username from userinfo where departmentname='" + dn + "')) and rwbianhao not like 'D%' order by id  desc ";
        }
        else if (dutyname.Trim() == "销售助理")
        {
            sqlstr = strSql + "where  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'and baojiaid in  (select baojiaid from baojiabiao where (responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "')) and rwbianhao not like 'D%' order by id  desc ";
        }
        else
        {
            //业务员
            sqlstr = strSql + "where  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'and baojiaid in  (select baojiaid from baojiabiao where responser='" + Session["Username"].ToString() + "') and rwbianhao not like 'D%' order by id  desc ";
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
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


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

            e.Row.Cells[5].Text = SubStr(e.Row.Cells[5].Text, 10);
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 10);
            e.Row.Cells[6].Text = SubStr(e.Row.Cells[6].Text, 10);

            e.Row.Cells[3].Text = Eng(e.Row.Cells[1].Text.ToString());//报告号
            e.Row.Cells[24].Text = Zhuli(e.Row.Cells[25].Text.ToString());
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        //Random seed = new Random();
        //Random randomNum = new Random(seed.Next());
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();

        //string shoufeiid = randomNum.Next().ToString() + DateTime.Now.ToString("yyyyMMdd_hhmmss");
        //foreach (GridViewRow gr in GridView1.Rows)
        //{
        //    CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
        //    if (hzf.Checked)
        //    {
        //        string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
        //        string sql2 = "update Anjianxinxi2 set bianhaoone='" + shoufeiid + "' where id='" + sid + "'";
        //        SqlCommand com2 = new SqlCommand(sql2, con);
        //        com2.ExecuteNonQuery();
        //    }
        //}
        //con.Close();
        //Response.Redirect("~/Income/InvoiceAdd.aspx?ran=" + shoufeiid);
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        if (ys == "1")
        {
            //Button3.Visible = false;
        }
        else
        {
            TimeBind();
        }
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
    protected void Button4_Click(object sender, EventArgs e)
    {
        StringBuilder strSql6 = new StringBuilder();

        strSql6.Append("select state as state1,");
        strSql6.Append("(select top 1 customtype from customer where kehuid=anjianinfo2.kehuid ) as ct,");
        strSql6.Append("(select top 1 name from ZhuJianEngineer where bianhao=anjianinfo2.rwbianhao ) as name1,");
        strSql6.Append("(select top 1 fenpainame from anjianinfo where tijiaobianhao=Anjianinfo2.bianhao and type='是') as fenpainame,");
        strSql6.Append("* from Anjianinfo2 ");


        string sqlstr;

        sqlstr = strSql6 + "where  (shenqingbianhao like '%" + TextBox1.Text + "%' or fukuandanwei like '%" + TextBox1.Text + "%' or weituodanwei like '%" + TextBox1.Text + "%' or shengchandanwei like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%'))  and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and rwbianhao not like 'D%' order by substring(rwbianhao,4,5) desc ";



        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


        DisableControls(GridView1);

        Response.ClearContent();

        Response.AddHeader("content-disposition", "attachment; filename=TaskCount" + DateTime.Now.ToShortDateString() + ".xls");

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
    protected string DutyName()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlDataAdapter da_dutyname = new SqlDataAdapter(sql_dutyname, con);
            DataSet ds_dutyname = new DataSet();
            da_dutyname.Fill(ds_dutyname);
            string dutyname = ds_dutyname.Tables[0].Rows[0]["dutyname"].ToString();
            return dutyname;
        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string tiaojian = "";
        if (DropDownList2.SelectedValue == "")
        {
            tiaojian = "1=1 and";
        }
        else if (DropDownList2.SelectedValue == "中止")
        {
            tiaojian = "[state]='中止' and";
        }
        else if (DropDownList2.SelectedValue == "进行中")
        {
            tiaojian = "[state]='进行中' and";
        }
        else if (DropDownList2.SelectedValue == "暂停")
        {
            tiaojian = "[state]='暂停' and";
        }
        else if (DropDownList2.SelectedValue == "完成")
        {
            tiaojian = "[state]='完成' and";
        }

        string sqlstr = "";
        string dutyname = "";//职位
        string dn = "";//部门
                       //获取当前登录进来的人的职位和部门
        using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con1.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con1);
            SqlDataReader dr = cmdstate.ExecuteReader();
            if (dr.Read())
            {
                dn = dr["departmentname"].ToString();
                dutyname = dr["dutyname"].ToString();
            }
            dr.Close();
        }
        if (dutyname.Trim() == "系统管理员" || dutyname.Trim() == "客服经理" || dutyname.Trim() == "总经理" || dutyname.Trim() == "董事长")
        {
            sqlstr = strSql + "where  " + tiaojian + " convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and rwbianhao not like 'D%' order by id  desc ";
        }
        else if (dutyname.Trim() == "客户经理")
        {
            sqlstr = strSql + "where " + tiaojian + " convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'and baojiaid in  (select baojiaid from baojiabiao where responser  in (select username from userinfo where departmentname='" + dn + "')) and rwbianhao not like 'D%' order by id  desc ";
        }
        else if (dutyname.Trim() == "业务员")
        {
            sqlstr = strSql + "where " + tiaojian + " convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'and baojiaid in  (select baojiaid from baojiabiao where responser='" + Session["Username"].ToString() + "') and rwbianhao not like 'D%' order by id  desc ";
        }
        else if (dutyname.Trim() == "客服人员")
        {
            sqlstr = strSql + "where " + tiaojian + " convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'and  baojiaid in (select baojiaid from BaoJiaBiao where responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "')) and rwbianhao not like 'D%' order by id  desc ";
        }
        else if (dutyname.Trim() == "销售助理")
        {
            //sqlstr = strSql + "where  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' ";
            sqlstr = strSql + "where " + tiaojian + " convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and baojiaid in  (select baojiaid from baojiabiao where responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "')) and rwbianhao not like 'D%' order by id  desc";
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
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

    protected void btn_npoi_Click(object sender, EventArgs e)
    {
        //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        //{
        //    con.Open();
        //    string sql = "";
        //    if (!string.IsNullOrEmpty(txFDate.Value) && !string.IsNullOrEmpty(txTDate.Value))
        //    {
        //        sql = @"select an.xiadariqi as 下达日期,xm.feiyong as 费用,an.yaoqiuwanchengriqi as 要求完成日期,xm.baojiaid as 报价编号,(select responser from BaoJiaBiao where baojiaid=xm.baojiaid) as 业务员,
        //                (select departmentname from UserInfo where UserName=(select responser from BaoJiaBiao where baojiaid=xm.baojiaid)) as 销售团队,
        //                (select top 1 UserName from CustomerServer where marketid=(select responser from BaoJiaBiao where baojiaid=xm.baojiaid)) as 销售助理,
        //                an.fillname as 排单客服,(select CustomName from Customer where kehuid=xm.kehuid) as 公司名称,
        //                (select top 1 email from CustomerLinkMan where customerid=xm.kehuid) as 客户邮箱,
        //                an.chanpinname as 产品名称,an.b5 as 主测型号,xm.ceshiname as 案件项目,epiboly as 是否外发,(select top 1 baogaoid from ItemBaogao where xmid=xm.id) as 报告号,
        //                (select top 1 fillname from ZhuJianEngineer where bianhao=an.rwbianhao) as 负责人,(select top 1 name from ZhuJianEngineer where bianhao=an.rwbianhao) as 工程师,
        //                (select CustomType from Customer where kehuid=xm.kehuid) as 客户类型,xm.yp as 样品信息,
        //                (select Name from Bankaccount where id=(select zhangdan from BaoJiaBiao where BaoJiaId=an.baojiaid and (baojiaid like 'FY%' or baojiaid like 'LH%') and  LEN(BaoJiaBiao.BaoJiaId)<13)) as 收款抬头,
        //                an.beizhu as 案件备注,an.beizhu3 as 结案日期,(select top 1 fafangdate from baogao2 where rwid=an.rwbianhao) as 报告发放日期
        //                from BaoJiaCPXiangMu xm,AnJianInFo2 an where xm.baojiaid=an.baojiaid and (an.baojiaid  like 'FY%' or an.baojiaid like 'LH') and LEN(an.baojiaid)<13 and an.baojiaid not in(select baojiaid from BaoJiaBiao where responser='admin')
        //                and CONVERT(datetime,an.xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value) + "'";
        //    }
        //    else
        //    {
        //        sql = @"select an.xiadariqi as 下达日期,xm.feiyong as 费用,an.yaoqiuwanchengriqi as 要求完成日期,xm.baojiaid as 报价编号,(select responser from BaoJiaBiao where baojiaid=xm.baojiaid) as 业务员,
        //                (select departmentname from UserInfo where UserName = (select responser from BaoJiaBiao where baojiaid = xm.baojiaid)) as 销售团队,
        //                (select top 1 UserName from CustomerServer where marketid = (select responser from BaoJiaBiao where baojiaid = xm.baojiaid)) as 销售助理,
        //                an.fillname as 排单客服,(select CustomName from Customer where kehuid = xm.kehuid) as 公司名称,
        //                (select top 1 email from CustomerLinkMan where customerid = xm.kehuid) as 客户邮箱,
        //                an.chanpinname as 产品名称,an.b5 as 主测型号,xm.ceshiname as 案件项目,epiboly as 是否外发,(select top 1 baogaoid from ItemBaogao where xmid = xm.id) as 报告号,
        //                (select top 1 fillname from ZhuJianEngineer where bianhao = an.rwbianhao) as 负责人,(select top 1 name from ZhuJianEngineer where bianhao = an.rwbianhao) as 工程师,
        //                (select CustomType from Customer where kehuid = xm.kehuid) as 客户类型,xm.yp as 样品信息,
        //                (select Name from Bankaccount where id = (select zhangdan from BaoJiaBiao where BaoJiaId = an.baojiaid and(baojiaid like 'FY%' or baojiaid like 'LH%') and LEN(BaoJiaBiao.BaoJiaId)< 13)) as 收款抬头,
        //                an.beizhu as 案件备注,an.beizhu3 as 结案日期,(select top 1 fafangdate from baogao2 where rwid = an.rwbianhao) as 报告发放日期
        //                from BaoJiaCPXiangMu xm,AnJianInFo2 an where xm.baojiaid = an.baojiaid and(an.baojiaid  like 'FY%' or an.baojiaid like 'LH') and LEN(an.baojiaid)< 13 and an.baojiaid not in(select baojiaid from BaoJiaBiao where responser = 'admin')";
        //    }
        //    SqlDataAdapter da = new SqlDataAdapter(sql, con);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    Myoffice.Class1.DatatoExcel(ds.Tables[0]);
        //}
    }

    public string Eng(string rwid)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql112 = "select baogaoid from baogao2 where rwid='" + rwid + "'";

        SqlDataAdapter ad112 = new SqlDataAdapter(sql112, con);
        DataSet ds112 = new DataSet();
        ad112.Fill(ds112);
        con.Close();
        DataTable dt112 = ds112.Tables[0];
        string zhujian = "";
        for (int z = 0; z < dt112.Rows.Count; z++)
        {
            zhujian = zhujian + dt112.Rows[z]["baogaoid"].ToString() + ",";
        }
        if (zhujian.Contains(","))
        {
            zhujian = zhujian.Substring(0, zhujian.Length - 1);
        }

        if (zhujian.Length > 20)
        {
            string str = zhujian.Substring(0, 18) + "...";
            return str;
        }
        else
        {
            return zhujian;
        }
    }

    public string Zhuli(string name)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql112 = "select UserName from CustomerServer where marketid='" + name + "'";

        SqlDataAdapter ad112 = new SqlDataAdapter(sql112, con);
        DataSet ds112 = new DataSet();
        ad112.Fill(ds112);
        con.Close();
        DataTable dt112 = ds112.Tables[0];
        string zhujian = "";
        for (int z = 0; z < dt112.Rows.Count; z++)
        {
            zhujian = zhujian + dt112.Rows[z]["UserName"].ToString() + ",";
        }
        if (zhujian.Contains(","))
        {
            zhujian = zhujian.Substring(0, zhujian.Length - 1);
        }
        return zhujian;
    }
}