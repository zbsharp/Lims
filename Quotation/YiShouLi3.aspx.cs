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

public partial class Quotation_YiShouLi3 : System.Web.UI.Page
{
    private int _i = 0;
    protected string st = "进行中";
    protected StringBuilder strSql = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        strSql.Append("select ");
        strSql.Append("(select fillname from baojiabiao where baojiaid=anjianxinxi2.baojiaid) as fn,");
        strSql.Append("(select state from anjianinfo2 where rwbianhao=anjianxinxi2.taskno) as state1,");
        strSql.Append("(select kf from anjianinfo2 where rwbianhao=anjianxinxi2.taskno) as kf,");
        strSql.Append("(select top 1 name from ZhuJianEngineer where bianhao=anjianxinxi2.taskno ) as name1,");
        strSql.Append("(select top 1 renwu from anjianinfo2 where bianhao=Anjianxinxi2.bianhao) as renwu,");
        strSql.Append("(select top 1 state from anjianinfo2 where bianhao=Anjianxinxi2.bianhao) as state1,");
        strSql.Append("(select top 1 xiadariqi from anjianinfo2 where bianhao=Anjianxinxi2.bianhao) as xiadariqi,");
        strSql.Append("(select top 1 fenpainame from anjianinfo where tijiaobianhao=Anjianxinxi2.bianhao and type='是') as fenpainame,");
        strSql.Append("* from Anjianxinxi2 ");

        if (!IsPostBack)
        {
            TimeBind();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        string tid = "";
        bool B = false;
        bool C = limit1("案件下达");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sqlkf = "select kf,rwbianhao from anjianinfo2 where bianhao='" + sid + "' and kf='"+Session["UserName"].ToString()+"'";
        SqlCommand cmdkf = new SqlCommand(sqlkf,con);
        SqlDataReader drkf = cmdkf.ExecuteReader();
        if (drkf.Read())
        {
            B = true;
            tid = drkf["rwbianhao"].ToString();
        }
        else
        {
            B = false;
        }
        drkf.Close();
        
        if (B == true || C == true)
        {



          

            
            
            if (e.CommandName == "xiada")
            {


                string sqli = "update Anjianinfo2 set renwu='是',state='进行中' ,xiadariqi='" + DateTime.Now.ToShortDateString() + "' where bianhao='" + sid + "'";
                SqlCommand cmdii = new SqlCommand(sqli, con);
                cmdii.ExecuteNonQuery();

                DateTime d1 = DateTime.Now;


                string sqlstate = "insert into  TaskState values ('" + sid + "','" + tid + "','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + d1 + "','下达任务','客服下达任务到科室')";
                SqlCommand cmdstate = new SqlCommand(sqlstate, con);
                cmdstate.ExecuteNonQuery();


                con.Close();
                MyExcutSql my1 = new MyExcutSql();
                my1.ExtTaskone(sid, sid, "下达", "手工提交", Session["UserName"].ToString(), "修改了anjianinfo2的renwu和state记录下达", DateTime.Now,st);

                searchwhere sx42 = new searchwhere();
                string sjt1 = sx42.ShiXiao3(tid);


                ld.Text = "<script>alert('下达成功!');</script>";


                TimeBind((DropDownList1.SelectedValue), TextBox1.Text);
            }
        }
        else
        {
            con.Close();
        }

        con.Close();
        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string ChooseNo =(DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text.Trim();
        AspNetPager1.Visible = false;
        TimeBind(ChooseNo, ChooseValue);
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

    protected void TimeBind(string a, string b)
    {



        string ChooseID = a.Trim();
        string ChooseValue = b.Trim();
        string sqlstr;

        if (DropDownList1.SelectedValue == "kehuname")
        {
            sqlstr = strSql + " where " + searchwhere.searchcustomer(ChooseValue) + " and shoulibiaozhi='是' and taskno not in (select rwbianhao from anjianinfo2 where (state='下达' or state='完成' or state='中止' or state='暂停' or state='关闭' or state='进行中' or state='已完成打印') ) and taskno in (select rwbianhao from anjianinfo2 where kf !='') order by substring(taskno,4,5) desc ";

        }
        else if (DropDownList1.SelectedValue == "kf")
        {
            sqlstr = strSql + " where   shoulibiaozhi='是' and taskno not in (select rwbianhao from anjianinfo2 where (state='下达' or state='完成' or state='中止' or state='暂停' or state='关闭' or state='进行中' or state='已完成打印')) and taskno in (select rwbianhao from anjianinfo2 where kf like '%" + TextBox1.Text.Trim() + "%') order by substring(taskno,4,5) desc";

        }
         else
        {
            sqlstr = strSql + " where " + DropDownList1.SelectedValue + " like '%" + ChooseValue + "%' and shoulibiaozhi='是' and taskno not in (select rwbianhao from anjianinfo2 where (state='下达' or state='完成' or state='中止' or state='暂停' or state='关闭' or state='进行中' or state='已完成打印')) and taskno in (select rwbianhao from anjianinfo2 where kf !='') order by substring(taskno,4,5) desc";

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

        string sqlstr;

        sqlstr = strSql + " where shoulibiaozhi='是'  and taskno not in (select rwbianhao from anjianinfo2 where (state='下达' or state='完成' or state='中止' or state='暂停' or state='关闭' or state='进行中' or state='已完成打印')) and taskno in (select rwbianhao from anjianinfo2 where kf !='') order by id desc  ";

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


            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 10);
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 10);
            e.Row.Cells[5].Text = SubStr(e.Row.Cells[5].Text, 10);


            string sid = e.Row.Cells[1].ToString();
            bool B = false;
            bool C = limit1("案件下达");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sqlkf = "select kf from anjianinfo2 where bianhao='" + sid + "' and kf='" + Session["UserName"].ToString() + "'";
            SqlCommand cmdkf = new SqlCommand(sqlkf, con);
            SqlDataReader drkf = cmdkf.ExecuteReader();
            if (drkf.Read())
            {
                B = true;
            }
            else
            {
                B = false;
            }
            drkf.Close();
            con.Close();
            if (B == true || C == true)
            {
                


            }
            else
            {
                LinkButton LinkBtn_DetailInfo2 = (LinkButton)e.Row.FindControl("LinkButton5");

                LinkBtn_DetailInfo2.Enabled = false;
                LinkBtn_DetailInfo2.ForeColor = Color.DarkGray;

            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        TimeBind();
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