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

public partial class Case_CeShiFeiKfM : System.Web.UI.Page
{
    private int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {



            limit("查看收费");


            //DateTime dt = DateTime.Now.AddMonths(-6);
            //int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            //int dayspan = (-1) * weeknow + 1;
            //DateTime dt2 = DateTime.Now.AddMonths(+6);
            ////本月第一天
            //txFDate.Value = dt.AddMonths(-6).AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");


            //DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(6).ToString("yyyy-MM-01")).AddDays(-1).AddHours(23);

            //txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();

            txFDate.Value = DateTime.Now.AddMonths(-12).ToString("yyyy-MM-01");


            DateTime lastDay = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddHours(23);

            txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();

            
            bind();
            GridView1.ShowFooter = false;
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
    protected void bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql2 = "delete from invoice where inid not in (select shoufeibianhao from ceshifeikf)";
        SqlCommand cmd2 = new SqlCommand(sql2,con);
        cmd2.ExecuteNonQuery();

      //  string sql = "select   *,(select fukuandanwei from anjianinfo2 where rwbianhao=(select top 1 taskid from CeShiFeiKf where shoufeibianhao=invoice.inid)) as fk,(select weituodanwei from anjianinfo2 where rwbianhao=(select top 1 taskid from CeShiFeiKf where shoufeibianhao=invoice.inid)) as weituo,(select shenqingbianhao from anjianinfo2 where rwbianhao=(select top 1 taskid from CeShiFeiKf where shoufeibianhao=invoice.inid)) as shenqingbianhao,(select kf from anjianinfo2 where rwbianhao=(select top 1 taskid from CeShiFeiKf where shoufeibianhao=invoice.inid)) as kf,(select sum(feiyong) from CeShiFeiKf where shoufeibianhao=invoice.inid) as feiyong1,(select count(*) from CeShiFeiKf where shoufeibianhao=invoice.inid) as shu,(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname ,(select top 1 taskid from CeShiFeiKf where shoufeibianhao=invoice.inid) as taskno from Invoice where hesuanbiaozhi='" + DropDownList1.SelectedValue + "' order by id desc";

        string sql = "select  top 200  *,(select top 1 beizhu3 from anjianinfo2 where rwbianhao=invoice.rwbh) as beizhu3,(select top 1 responser from anjianinfo2 where rwbianhao=invoice.rwbh) as res,(select top 1 id from anjianinfo2 where rwbianhao=invoice.rwbh) as idd,name1 as fk,name2 as weituo,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=invoice.rwbh) as shenqingbianhao,(select top 1 kf from anjianinfo2 where rwbianhao=invoice.rwbh) as kf,(select sum(feiyong) from CeShiFeiKf where shoufeibianhao=invoice.inid) as feiyong1,(select count(*) from CeShiFeiKf where shoufeibianhao=invoice.inid) as shu,(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname ,rwbh as taskno from Invoice where hesuanbiaozhi='" + DropDownList1.SelectedValue + "' order by id desc";
        
        
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
        strSql.Append("select *, (select top 1 id from anjianinfo2 where rwbianhao=invoice.rwbh) as idd,");
        strSql.Append("(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=invoice.rwbh)  as shenqingbianhao,");
        strSql.Append("(select top 1 kf from anjianinfo2 where rwbianhao=rwbh) as kf,");
        strSql.Append("(name1) as fk,");
        strSql.Append("(name2) as weituo,");
        strSql.Append("(select top 1 responser from anjianinfo2 where rwbianhao=invoice.rwbh) as res,");
        strSql.Append("(select top 1 beizhu3 from anjianinfo2 where rwbianhao=invoice.rwbh) as beizhu3,");
        
        strSql.Append("(select sum(feiyong) from CeShiFeiKf where shoufeibianhao=invoice.inid) as feiyong1,");
        strSql.Append("(select count(*) from CeShiFeiKf where shoufeibianhao=invoice.inid) as shu,");
        strSql.Append("(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname ,");
        strSql.Append("(rwbh) as taskno ");
      
        strSql.Append("from Invoice ");
        strSql.Append(" where (");

        strSql.Append("(inid like '%" + TextBox1.Text.Trim() + "%' or ");

        strSql.Append(" name like '%" + TextBox1.Text.Trim() + "%' or ");

        strSql.Append(" zongjia like '%" + TextBox1.Text.Trim() + "%' or ");
        strSql.Append(" name1 like '%" + TextBox1.Text.Trim() + "%' or  ");
        strSql.Append(" name2 like '%" + TextBox1.Text.Trim() + "%' or  ");

        strSql.Append(" kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text.Trim() + "%') or ");
       // strSql.Append(" kehuid in (select customerid from customerlinkman where name like '%" + TextBox1.Text.Trim() + "%') or ");
        strSql.Append(" sqbianhao like '%" + TextBox1.Text.Trim() + "%' or  ");
        strSql.Append(" inid in (select shoufeibianhao from CeShiFeiKf where fillname like '%" + TextBox1.Text.Trim() + "%') or ");
        

        strSql.Append(" rwbh  like '%" + TextBox1.Text.Trim() + "%'");

        

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = strSql + ")) and  hesuanbiaozhi='" + DropDownList1.SelectedValue + "' and filltime between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by filltime desc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
      
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

       
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string shoufeiid = "";
        string hesuan = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql2 = "select inid,hesuanbiaozhi from Invoice where id='" + id + "' and fillname='"+Session["UserName"].ToString()+"'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            shoufeiid = dr2["inid"].ToString();
            hesuan = dr2["hesuanbiaozhi"].ToString();
        }
        dr2.Close();

        if (hesuan != "是" && shoufeiid !="")
        {

            string sql3 = "update Anjianxinxi2 set shoufeibiaozhi='否' where bianhaotwo='" + shoufeiid + "' ";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            cmd3.ExecuteNonQuery();

            string sql4 = "update CeShiFeiKf set shoufeibianhao='' where shoufeibianhao='" + shoufeiid + "' ";
            SqlCommand cmd4 = new SqlCommand(sql4, con);
            cmd4.ExecuteNonQuery();

            string sql = "delete from Invoice where id='" + id + "' and fillname='" + Session["UserName"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        con.Close();
        bind();
        
    }
    private decimal sum1 = 0;
    private decimal sum2 = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");
            e.Row.Cells[5].Text = SubStr(e.Row.Cells[5].Text, 6);
            e.Row.Cells[6].Text = SubStr(e.Row.Cells[6].Text, 5);
            e.Row.Cells[7].Text = SubStr(e.Row.Cells[7].Text, 5);


            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
        }

        if (e.Row.RowIndex >= 0)
        {

           
            if (e.Row.Cells[4].Text == "" || e.Row.Cells[4].Text == "&nbsp;")
            {
                e.Row.Cells[4].Text = "0";
            }
         
            sum2 += Convert.ToDecimal(e.Row.Cells[4].Text);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "小计：";

            e.Row.Cells[4].Text = sum2.ToString();
            e.Row.Cells[1].ForeColor = Color.Blue;
            e.Row.Cells[4].ForeColor = Color.Blue;

          

        }


    }
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




                //string sql3 = "delete from semcshiji where jiludanhao='" + sid + "'and biaoji!='" + shoufeiid + "'";
                //SqlCommand com3 = new SqlCommand(sql3, con);
                //com3.ExecuteNonQuery();

                //string sql = "insert into semcshiji select * from emcshiji where jiludanhao='" + sid + "'";
                //string sql1 = "update semcshiji set biaoji='" + shoufeiid + "' where jiludanhao='" + sid + "'";
                string sql2 = "update Invoice set dayinid='" + shoufeiid + "' where id='" + sid + "'";



                //SqlCommand com1 = new SqlCommand(sql1, con);


                //SqlCommand com = new SqlCommand(sql, con);
                SqlCommand com2 = new SqlCommand(sql2, con);

                //com.ExecuteNonQuery();
                //com1.ExecuteNonQuery();
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


}