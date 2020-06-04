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

public partial class Case_CeShiFeiKfMYW : System.Web.UI.Page
{
    private int _i = 0;
    protected string Rolename = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            limit("财务对账");
            txFDate.Value = DateTime.Now.AddMonths(-12).ToString("yyyy-MM-01");
            DateTime lastDay = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddHours(23);
            txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();
            bind();
            GridView1.ShowFooter = false;
        }
    }

    protected void bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select *,(select top 1 weituodanwei from anjianinfo2 where rwbianhao=invoice.rwbh) as weituo,(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname,(select sum(feiyong) from CeShiFeiKf where shoufeibianhao=invoice.inid) as yingshoujine,(select sum(xiaojine) from cashin2 where dengji2=invoice.inid) as yiduijine  from Invoice where (hesuanbiaozhi='否' or(hesuanbiaozhi='是' and inid in(select shoufeibianhao from ceshifeikf where (feiyong-shishou)>1 or (shishou-feiyong)>1)))";

        //  string sql = "select   *,(select fukuandanwei from anjianinfo2 where rwbianhao=(select top 1 taskid from CeShiFeiKf where shoufeibianhao=invoice.inid)) as fk,(select weituodanwei from anjianinfo2 where rwbianhao=(select top 1 taskid from CeShiFeiKf where shoufeibianhao=invoice.inid)) as weituo,(select shenqingbianhao from anjianinfo2 where rwbianhao=(select top 1 taskid from CeShiFeiKf where shoufeibianhao=invoice.inid)) as shenqingbianhao,(select kf from anjianinfo2 where rwbianhao=(select top 1 taskid from CeShiFeiKf where shoufeibianhao=invoice.inid)) as kf,(select sum(feiyong) from CeShiFeiKf where shoufeibianhao=invoice.inid) as feiyong1,(select count(*) from CeShiFeiKf where shoufeibianhao=invoice.inid) as shu,(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname ,(select top 1 taskid from CeShiFeiKf where shoufeibianhao=invoice.inid) as taskno from Invoice where hesuanbiaozhi='" + DropDownList1.SelectedValue + "' order by id desc";

        // string sql = "select  top 200  *,(select top 1 id from anjianinfo2 where rwbianhao=invoice.rwbh) as idd,name1 as fk,name2 as weituo,sqbianhao as shenqingbianhao,(select top 1 kf from anjianinfo2 where rwbianhao=invoice.rwbh) as kf,(select sum(feiyong) from CeShiFeiKf where shoufeibianhao=invoice.inid) as feiyong1,(select count(*) from CeShiFeiKf where shoufeibianhao=invoice.inid) as shu,(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname ,rwbh as taskno from Invoice where hesuanbiaozhi='" + DropDownList1.SelectedValue + "' and rwbh in (select rwbianhao from anjianinfo2 where responser='"+Session["UserName"].ToString()+"') order by id desc";
        string loginusername = this.Session["UserName"].ToString();
        string loginuserdepsql = "select dutyname from userinfo where username='" + this.Session["UserName"].ToString() + "'";
        SqlCommand logoinuserdepcmd = new SqlCommand(loginuserdepsql, con);
        SqlDataReader loginuserdepad = logoinuserdepcmd.ExecuteReader();
        if (loginuserdepad.Read())
        {
            Rolename = loginuserdepad["dutyname"].ToString();
        }
        loginuserdepad.Close();
        if (Rolename.Trim() == "业务员")
        {
            sql += " and rwbh in(select rwbianhao from anjianinfo2 where baojiaid in(select baojiaid from baojiabiao where responser='" + this.Session["UserName"].ToString() + "'))";
        }
        else if (Rolename.Trim() == "销售助理")
        {
            sql += " and rwbh in(select rwbianhao from anjianinfo2 where baojiaid in(select baojiaid from baojiabiao where responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + ")))";
        }
        else
        {
            if (Rolename.Trim() == "客户经理")
            {
                sql += " and rwbh in(select rwbianhao from anjianinfo2 where baojiaid in(select baojiaid from baojiabiao where responser in(select username from userinfo where departmentname=(select departmentname from userinfo where username='" + this.Session["UserName"].ToString() + "'))))";
            }
            else
            {
                if (Rolename.Trim() == "系统管理员" || Rolename.Trim() == "总经理" || Rolename.Trim() == "客服经理")
                {

                }
                else
                {
                    sql += " and rwbh in(select rwbianhao from anjianinfo2 where baojiaid in(select baojiaid from baojiabiao where responser='" + this.Session["UserName"].ToString() + "'))";
                }
            }
        }
        sql += " order by id asc";



        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        DataView dv = ds.Tables[0].DefaultView;
        PagedDataSource pds = new PagedDataSource();
        AspNetPager1.RecordCount = dv.Count;
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager1.PageSize;
        GridView1.DataSource = pds;
        GridView1.DataBind();

        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        AspNetPager1.Visible = false;
        GridView1.ShowFooter = true;
        StringBuilder strSql = new StringBuilder();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string loginuserdepsql = "select dutyname from userinfo where username='" + this.Session["UserName"].ToString() + "'";
        SqlCommand logoinuserdepcmd = new SqlCommand(loginuserdepsql, con);
        SqlDataReader loginuserdepad = logoinuserdepcmd.ExecuteReader();
        if (loginuserdepad.Read())
        {
            Rolename = loginuserdepad["dutyname"].ToString();
        }
        loginuserdepad.Close();
        strSql.Append("select *,(select top 1 weituodanwei from anjianinfo2 where rwbianhao=invoice.rwbh) as weituo,(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname,(select sum(feiyong) from CeShiFeiKf where shoufeibianhao=invoice.inid) as yingshoujine,(select sum(xiaojine) from cashin2 where dengji2=invoice.inid) as yiduijine  from Invoice");
        strSql.Append(" where convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'");
        if (this.DropDownList1.SelectedValue == "否")
        {
            strSql.Append(" and (hesuanbiaozhi='否' or(hesuanbiaozhi='是' and inid in(select shoufeibianhao from ceshifeikf where (feiyong-shishou)>1 or (shishou-feiyong)>1)))");
        }
        else
        {
            strSql.Append(" and (hesuanbiaozhi='是' and inid not in(select shoufeibianhao from ceshifeikf where (feiyong-shishou)>1 or (shishou-feiyong)>1))");
        }
        if (Rolename.Trim() == "业务员")
        {
            strSql.Append(" and rwbh in(select rwbianhao from anjianinfo2 where baojiaid in(select baojiaid from baojiabiao where responser='" + this.Session["UserName"].ToString() + "'))");
        }
        else if (Rolename.Trim() == "销售助理")
        {
            strSql.Append(" and rwbh in(select rwbianhao from anjianinfo2 where baojiaid in(select baojiaid from baojiabiao where responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "')))");
        }
        else
        {
            if (Rolename.Trim() == "客户经理")
            {
                strSql.Append(" and rwbh in(select rwbianhao from anjianinfo2 where baojiaid in(select baojiaid from baojiabiao where responser in(select username from userinfo where departmentname=(select departmentname from userinfo where username='" + this.Session["UserName"].ToString() + "'))))");
            }
            else
            {
                if (Rolename.Trim() == "系统管理员" || Rolename.Trim() == "总经理" || Rolename.Trim() == "客服经理")
                {

                }
                else
                {
                    strSql.Append(" and rwbh in(select rwbianhao from anjianinfo2 where baojiaid in(select baojiaid from baojiabiao where responser='" + this.Session["UserName"].ToString() + "'))");
                }
            }
        }

        strSql.Append(" and (inid like '%" + TextBox1.Text.Trim() + "%' or ");
        strSql.Append(" rwbh  like '%" + TextBox1.Text.Trim() + "%' or");
        strSql.Append(" kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text.Trim() + "%'))");
        strSql.Append(" order by id desc");

        //strSql.Append("select *, (select top 1 id from anjianinfo2 where rwbianhao=invoice.rwbh) as idd,");
        //strSql.Append("sqbianhao as shenqingbianhao,");
        //strSql.Append("(select top 1 kf from anjianinfo2 where rwbianhao=rwbh) as kf,");
        //strSql.Append("(name1) as fk,");
        //strSql.Append("(name2) as weituo,");



        //strSql.Append("(select sum(feiyong) from CeShiFeiKf where shoufeibianhao=invoice.inid) as feiyong1,");
        //strSql.Append("(select count(*) from CeShiFeiKf where shoufeibianhao=invoice.inid) as shu,");
        //strSql.Append("(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname ,");
        //strSql.Append("(rwbh) as taskno ");

        //strSql.Append("from Invoice ");
        //strSql.Append(" where (");

        //strSql.Append("(inid like '%" + TextBox1.Text.Trim() + "%' or ");

        //strSql.Append(" name like '%" + TextBox1.Text.Trim() + "%' or ");


        //strSql.Append(" name1 like '%" + TextBox1.Text.Trim() + "%' or  ");
        //strSql.Append(" name2 like '%" + TextBox1.Text.Trim() + "%' or  ");

        //strSql.Append(" kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text.Trim() + "%') or ");
        //strSql.Append(" sqbianhao like '%" + TextBox1.Text.Trim() + "%' or  ");
        //strSql.Append(" inid in (select shoufeibianhao from CeShiFeiKf where fillname like '%" + TextBox1.Text.Trim() + "%') or ");


        //strSql.Append(" rwbh  like '%" + TextBox1.Text.Trim() + "%'");

        string sql = strSql.ToString();

        // string sql = strSql + ")) and  hesuanbiaozhi='" + DropDownList1.SelectedValue + "' and filltime between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and rwbh in (select rwbianhao from anjianinfo2 where responser='" + Session["UserName"].ToString() + "') order by filltime desc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


    }

    //protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
    //    string shoufeiid = "";
    //    string hesuan = "";
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
    //    con.Open();

    //    string sql2 = "select inid,hesuanbiaozhi from Invoice where id='" + id + "' and fillname='" + Session["UserName"].ToString() + "'";
    //    SqlCommand cmd2 = new SqlCommand(sql2, con);
    //    SqlDataReader dr2 = cmd2.ExecuteReader();
    //    if (dr2.Read())
    //    {
    //        shoufeiid = dr2["inid"].ToString();
    //        hesuan = dr2["hesuanbiaozhi"].ToString();
    //    }
    //    dr2.Close();

    //    if (hesuan != "是" && shoufeiid != "")
    //    {

    //        string sql3 = "update Anjianxinxi2 set shoufeibiaozhi='否' where bianhaotwo='" + shoufeiid + "' ";
    //        SqlCommand cmd3 = new SqlCommand(sql3, con);
    //        cmd3.ExecuteNonQuery();

    //        string sql4 = "update CeShiFeiKf set shoufeibianhao='' where shoufeibianhao='" + shoufeiid + "' ";
    //        SqlCommand cmd4 = new SqlCommand(sql4, con);
    //        cmd4.ExecuteNonQuery();

    //        string sql = "delete from Invoice where id='" + id + "' and fillname='" + Session["UserName"].ToString() + "'";
    //        SqlCommand cmd = new SqlCommand(sql, con);
    //        cmd.ExecuteNonQuery();
    //    }

    //    con.Close();
    //    bind();

    //}
    private decimal sum1 = 0;
    private decimal sum2 = 0;
    private decimal sum3 = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 8);
            e.Row.Cells[5].Text = SubStr(e.Row.Cells[5].Text, 8);
            //e.Row.Cells[7].Text = SubStr(e.Row.Cells[7].Text, 5);


            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
        }

        if (e.Row.RowIndex >= 0)
        {


            if (e.Row.Cells[3].Text == "" || e.Row.Cells[8].Text == "&nbsp;")
            {
                e.Row.Cells[3].Text = "0";
            }
            if (e.Row.Cells[8].Text == "" || e.Row.Cells[8].Text == "&nbsp;")
            {
                e.Row.Cells[8].Text = "0";
            }

            sum2 += Convert.ToDecimal(e.Row.Cells[3].Text);
            sum3 += Convert.ToDecimal(e.Row.Cells[8].Text);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "小计：";

            e.Row.Cells[3].Text = sum2.ToString();
            e.Row.Cells[1].ForeColor = Color.Blue;
            e.Row.Cells[3].ForeColor = Color.Blue;

            e.Row.Cells[8].Text = sum3.ToString();
            e.Row.Cells[8].ForeColor = Color.Blue;
        }
    }
    //合并打印按钮
    protected void Button2_Click(object sender, EventArgs e)
    {
        Random seed = new Random();
        Random randomNum = new Random(seed.Next());


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string shoufeiid = randomNum.Next().ToString() + DateTime.Now.ToString("yyyyMMdd_hhmmss");

        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");

            if (hzf.Checked)
            {

                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();




                string sql2 = "update Invoice set dayinid='" + shoufeiid + "' where id='" + sid + "'";



                SqlCommand com2 = new SqlCommand(sql2, con);


                com2.ExecuteNonQuery();
            }
        }


        con.Close();



        Response.Redirect("~/Print/InvoicePrint2.aspx?ran=" + shoufeiid);
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        bind();
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


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        if (e.CommandName == "delete")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = "select * from CeShiFeiKf c,Invoice i where c.shoufeibianhao=i.inid and(c.heduibiaozhi = '是' or i.hesuanbiaozhi = '是') and(c.shoufeibianhao = '" + id + "' and i.inid = '" + id + "')";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //表示该收费单已收费 则不能删除
                    Literal1.Text = "<script>alert('该收费单已收费，不能删除')</script>";
                }
                else
                {
                    string sql_ceshifeikf = "delete  CeShiFeiKf where shoufeibianhao='" + id + "'";
                    string sql_invoice = "delete  Invoice where inid='" + id + "'";
                    SqlCommand cmd_ceshifeikf = new SqlCommand(sql_ceshifeikf, con);
                    cmd_ceshifeikf.ExecuteNonQuery();
                    SqlCommand cmd_invoice = new SqlCommand(sql_invoice, con);
                    cmd_invoice.ExecuteNonQuery();
                    bind();
                }
            }
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
}