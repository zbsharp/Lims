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

public partial class Income_CaseIncome4 : System.Web.UI.Page
{
    private int _i = 0;
    protected string str = "";
    private string minId = "0";
    protected void Page_Load(object sender, EventArgs e)
    {

        //  str = "select  rw sum(ceshifeikf.feiyong) as feiyong,taskid,kehuid from CeShiFeiKf where kehuid !='' group by taskid,kehuid";


        str = "select baojiaid,weituodanwei, bianhao, shiyanleibie,shenqingbianhao, state, kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2 ";
        if (!IsPostBack)
        {

            txFDate.Value = DateTime.Now.AddMonths(-12).ToString("yyyy-MM-01");
            DateTime lastDay = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddHours(23);
            txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();

            string cmd = "select  count(*) as d from Anjianinfo2 where  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "'";

            if (Request.QueryString["minid"] != null)
            {
                minId = Request.QueryString["minid"].ToString();
            }

            if (minId != "0")
            {
                DropDownList1.SelectedValue = Request.QueryString["ti5"].ToString();
                DropDownList3.SelectedValue = Request.QueryString["ti1"].ToString();
                TextBox1.Text = Request.QueryString["ti2"].ToString();
                txFDate.Value = Request.QueryString["ti3"].ToString();
                txTDate.Value = Request.QueryString["ti4"].ToString();
                cmd = "select count(*) as d from Anjianinfo2 where " + Server.UrlDecode(minId) + "";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                SqlCommand cmd1 = new SqlCommand(cmd, con);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    AspNetPager2.RecordCount = Convert.ToInt32(dr["d"].ToString());


                }
                con.Close();


            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                SqlCommand cmd1 = new SqlCommand(cmd, con);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    AspNetPager2.RecordCount = Convert.ToInt32(dr["d"].ToString());
                    // Label3.Text = "当前结果记录数:200";
                }
                con.Close();
            }

            Bind3();

        }
        if (limit1("案件关闭"))
        {
            Button3.Enabled = true;
        }
        else
        {
            Button3.Enabled = false;
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

    public void Bind3()
    {

        if (TextBox1.Text == "")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sqlstr;



            sqlstr = "select baojiaid, weituodanwei, bianhao, state,shenqingbianhao,shiyanleibie,(select top 1 kf from anjianxinxi2 where taskno=anjianinfo2.rwbianhao) as kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2 where state='" + DropDownList3.SelectedValue + "'  and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by substring(rwbianhao,4,5) desc ";
            if (DropDownList3.SelectedValue == "全部")
            {
                sqlstr = "select baojiaid, weituodanwei, bianhao, state,shenqingbianhao,shiyanleibie,(select top 1 kf from anjianxinxi2 where taskno=anjianinfo2.rwbianhao) as kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2 where  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by substring(rwbianhao,4,5) desc ";

            }
            if (minId != "0")
            {

                if (DropDownList3.SelectedValue == "全部")
                {
                    sqlstr = "select baojiaid, weituodanwei, bianhao, state,shenqingbianhao,shiyanleibie,(select kf from anjianxinxi2 where taskno=anjianinfo2.rwbianhao) as kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2  where  " + minId + " and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by substring(rwbianhao,4,5) desc ";

                }
                else
                {

                    sqlstr = "select baojiaid, weituodanwei, bianhao, state,shenqingbianhao,shiyanleibie,(select kf from anjianxinxi2 where taskno=anjianinfo2.rwbianhao) as kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2  where  " + minId + " and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by substring(rwbianhao,4,5) desc ";
                }
            }



            //string sql = "";


            SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
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
        else
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();


            string sqlstr;

            sqlstr = "select baojiaid, weituodanwei, bianhao, state,shenqingbianhao,shiyanleibie,(select kf from anjianxinxi2 where taskno=anjianinfo2.rwbianhao) as kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2 where state='" + DropDownList3.SelectedValue + "' and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and (state like '%" + TextBox1.Text + "%' or rwbianhao like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%') or rwbianhao in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text + "%')) order by substring(rwbianhao,4,5) desc ";

            if (DropDownList3.SelectedValue == "全部")
            {
                sqlstr = "select baojiaid, weituodanwei, bianhao, state,shenqingbianhao,shiyanleibie,(select kf from anjianxinxi2 where taskno=anjianinfo2.rwbianhao) as kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2 where  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and (state like '%" + TextBox1.Text + "%' or rwbianhao like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%') or rwbianhao in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text + "%')) order by substring(rwbianhao,4,5) desc ";

            }



            if (minId != "0")
            {

                if (DropDownList3.SelectedValue == "全部")
                {
                    sqlstr = "select baojiaid, weituodanwei, bianhao, state,shenqingbianhao,shiyanleibie,(select kf from anjianxinxi2 where taskno=anjianinfo2.rwbianhao) as kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2  where  " + minId + " and   convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and (state like '%" + TextBox1.Text + "%' or rwbianhao like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%') or rwbianhao in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text + "%')) order by substring(rwbianhao,4,5) desc ";

                }
                else
                {

                    sqlstr = "select baojiaid, weituodanwei, bianhao, state,shenqingbianhao,shiyanleibie,(select kf from anjianxinxi2 where taskno=anjianinfo2.rwbianhao) as kf ,rwbianhao,kehuid,chanpinname,xinghaoguige,(select customname from customer where kehuid=anjianinfo2.kehuid) as kehuname from anjianinfo2  where  " + minId + " and  state='" + DropDownList3.SelectedValue + "' and  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and (state like '%" + TextBox1.Text + "%' or rwbianhao like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%') or rwbianhao in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text + "%')) order by substring(rwbianhao,4,5) desc ";
                }
            }

            SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            con.Close();
            con.Dispose();
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();


            AspNetPager2.Visible = false;
        }


    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind3();
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");


            //e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();






            string sqlk3 = "select sum(feiyong)as feiyong,taskid  from CeShiFeiKf where taskid='" + e.Row.Cells[0].Text + "' group by taskid";
            SqlCommand cmdk3 = new SqlCommand(sqlk3, con);
            SqlDataReader drk3 = cmdk3.ExecuteReader();
            if (drk3.Read())
            {
                e.Row.Cells[7].Text = Math.Round(Convert.ToDecimal(drk3["feiyong"]), 2).ToString();
            }
            drk3.Close();

            string sqlk1 = "select sum(xiaojine) as xiaojine from cashin2 where taskid='" + e.Row.Cells[0].Text + "' group by taskid";
            SqlCommand cmdk1 = new SqlCommand(sqlk1, con);
            SqlDataReader drk1 = cmdk1.ExecuteReader();
            if (drk1.Read())
            {
                e.Row.Cells[8].Text = Math.Round(Convert.ToDecimal(drk1["xiaojine"]), 2).ToString();
            }
            con.Close();


            bool d = false;
            d = limit1("案件关闭");

            //LinkButton LinkBtn_DetailInfo2 = (LinkButton)e.Row.FindControl("LinkButton5");

            LinkButton LinkBtn_DetailInfo21 = (LinkButton)e.Row.FindControl("LinkButton1");


            //LinkButton LinkBtn_DetailInfo23 = (LinkButton)e.Row.FindControl("LinkButton3");


            //LinkButton LinkBtn_DetailInfo22 = (LinkButton)e.Row.FindControl("LinkButton2");
            LinkButton LinkBtn_DetailInfo24 = (LinkButton)e.Row.FindControl("LinkButton4");

            if (d == true)
            {




            }
            else
            {



                LinkBtn_DetailInfo21.Enabled = false;
                LinkBtn_DetailInfo21.ForeColor = Color.DarkGray;

                LinkBtn_DetailInfo24.Enabled = false;
                LinkBtn_DetailInfo24.ForeColor = Color.DarkGray;
            }


            //if (limit1("修改案件状态")) { }
            //else
            //{
            //    LinkBtn_DetailInfo2.Enabled = false;
            //    LinkBtn_DetailInfo2.ForeColor = Color.DarkGray;

            //    LinkBtn_DetailInfo23.Enabled = false;
            //    LinkBtn_DetailInfo23.ForeColor = Color.DarkGray;

            //    LinkBtn_DetailInfo22.Enabled = false;
            //    LinkBtn_DetailInfo22.ForeColor = Color.DarkGray;

            //    LinkBtn_DetailInfo24.Enabled = false;
            //    LinkBtn_DetailInfo24.ForeColor = Color.DarkGray;
            //}



            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 6);
            e.Row.Cells[2].Text = SubStr(e.Row.Cells[2].Text, 6);
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 6);
            e.Row.Cells[10].Text = SubStr(e.Row.Cells[10].Text, 6);


        }


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();

        //string sql = str + "where state='" + DropDownList3.SelectedValue + "' and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and (state like '%" + TextBox1.Text + "%' or rwbianhao like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%') or rwbianhao in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text + "%'))";


        //SqlDataAdapter da = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //da.Fill(ds);

        //con.Close();
        //con.Dispose();
        //GridView1.DataSource = ds.Tables[0];
        //GridView1.DataBind();


        //AspNetPager2.Visible = false;

        string dd = "";
        if (DropDownList3.SelectedValue == "全部")
        {
            dd = "  convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and (state like '%" + TextBox1.Text + "%' or rwbianhao like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%') or rwbianhao in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text + "%'))";
            Response.Redirect("CaseIncome4.aspx?minid=" + Server.UrlEncode(dd) + "&&ti1=" + DropDownList3.SelectedValue + "&&ti2=" + TextBox1.Text.Trim() + "&&ti3=" + txFDate.Value + "&&ti4=" + txTDate.Value + "&&ti5=" + DropDownList1.SelectedValue);

        }
        else
        {

            dd = " state='" + DropDownList3.SelectedValue + "' and convert(datetime,xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' and (state like '%" + TextBox1.Text + "%' or rwbianhao like '%" + TextBox1.Text + "%' or kehuid in (select kehuid from customer where customname like '%" + TextBox1.Text + "%') or rwbianhao in (select taskno from anjianxinxi2 where shenqingbianhao like '%" + TextBox1.Text + "%'))";
            Response.Redirect("CaseIncome4.aspx?minid=" + Server.UrlEncode(dd) + "&&ti1=" + DropDownList3.SelectedValue + "&&ti2=" + TextBox1.Text.Trim() + "&&ti3=" + txFDate.Value + "&&ti4=" + txTDate.Value + "&&ti5=" + DropDownList1.SelectedValue);

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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();

        if (e.CommandName == "xiada")
        {



            Random seed = new Random();
            Random randomNum = new Random(seed.Next());
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string baojiaid = "";
            string kehuid = "";
            string bianhao = "";
            string shoufeiid = randomNum.Next().ToString() + DateTime.Now.ToString("yyyyMMdd_hhmmss");


            string sql2 = "select baojiaid,bianhao,kehuid from  Anjianxinxi2  where taskno='" + sid + "'";
            SqlCommand com2 = new SqlCommand(sql2, con);
            SqlDataReader dr2 = com2.ExecuteReader();
            if (dr2.Read())
            {
                baojiaid = dr2["baojiaid"].ToString();
                kehuid = dr2["kehuid"].ToString();
                bianhao = dr2["bianhao"].ToString();
            }


            con.Close();
            // Response.Redirect("~/Income/InvoiceAdd.aspx?ran=" + shoufeiid);

            Response.Redirect("~/Case/CeShiFeiKf.aspx?baojiaid=" + baojiaid + "&kehuid=" + kehuid + "&bianhao=" + bianhao);




        }



        else if (e.CommandName == "xiada1")
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

         

            string sqlstate4 = "update anjianinfo2 set state='关闭' where rwbianhao='" + sid + "' and state='完成'";
            SqlCommand cmdstate4 = new SqlCommand(sqlstate4, con);
            cmdstate4.ExecuteNonQuery();


            con.Close();


            MyExcutSql my = new MyExcutSql();
            my.ExtTaskone(sid, sid, "关闭", "手工提交", Session["UserName"].ToString(), "关闭任务", DateTime.Now, "关闭");
            Bind3();

        }
        else if (e.CommandName == "xiada2")
        {



            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();




            string sqlstate = "insert into  zanting values ('" + sid + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','','','','')";
            SqlCommand cmdstate = new SqlCommand(sqlstate, con);
            cmdstate.ExecuteNonQuery();


            string sqlstate2 = "insert into  zanting2 values ('" + sid + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','','','中止','','')";
            SqlCommand cmdstate2 = new SqlCommand(sqlstate2, con);
            cmdstate2.ExecuteNonQuery();

            string sqlstate4 = "update anjianinfo2 set state='中止' where rwbianhao='" + sid + "'";
            SqlCommand cmdstate4 = new SqlCommand(sqlstate4, con);
            cmdstate4.ExecuteNonQuery();


            con.Close();


            MyExcutSql my = new MyExcutSql();
            my.ExtTaskone(sid, sid, "中止任务", "手工提交", Session["UserName"].ToString(), "中止任务", DateTime.Now, "中止");






            Bind3();

        }
        else if (e.CommandName == "xiada3")
        {



            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();



            string sqlr = "select beizhu3 from anjianinfo2 where rwbianhao=('" + sid + "') and beizhu3=''";
            SqlCommand cmdr = new SqlCommand(sqlr, con);
            SqlDataReader drr = cmdr.ExecuteReader();
            if (drr.Read())
            {
                drr.Close();


                string sqlstate4 = "update anjianinfo2 set state='完成',beizhu3='" + DateTime.Now.ToShortDateString() + "' where rwbianhao='" + sid + "'";
                SqlCommand cmdstate4 = new SqlCommand(sqlstate4, con);
                cmdstate4.ExecuteNonQuery();
            }
            else
            {
                drr.Close();


                string sqlstate4 = "update anjianinfo2 set state='完成' where rwbianhao='" + sid + "'";
                SqlCommand cmdstate4 = new SqlCommand(sqlstate4, con);
                cmdstate4.ExecuteNonQuery();
            }


            con.Close();


            MyExcutSql my = new MyExcutSql();
            my.ExtTaskone(sid, sid, "完成任务", "手工提交", Session["UserName"].ToString(), "完成任务", DateTime.Now, "完成");






            Bind3();

        }

        else if (e.CommandName == "xiada4")
        {


            string state = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sqlguanbi = "select state from anjianinfo2 where rwbianhao='" + sid + "'";
            SqlCommand cmdgb = new SqlCommand(sqlguanbi, con);
            SqlDataReader drgb = cmdgb.ExecuteReader();
            if (drgb.Read())
            {
                state = drgb["state"].ToString();
            }
            drgb.Close();

            if (state == "关闭")
            {
                if (limit1("案件恢复"))
                {
                    string sqlstate = "delete from zanting where rwbianhao='" + sid + "'";
                    SqlCommand cmdstate = new SqlCommand(sqlstate, con);
                    cmdstate.ExecuteNonQuery();



                    string sqlstate4 = "update anjianinfo2 set state='完成',beizhu3='' where rwbianhao='" + sid + "' and state='关闭'";
                    SqlCommand cmdstate4 = new SqlCommand(sqlstate4, con);
                    cmdstate4.ExecuteNonQuery();


                    con.Close();


                    MyExcutSql my = new MyExcutSql();
                    my.ExtTaskone(sid, sid, "恢复正常", "手工提交", Session["UserName"].ToString(), "恢复正常", DateTime.Now, "恢复正常");


                }


                con.Close();

            }
            else
            {

                con.Close();

            }



            Bind3();

        }


    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[12].FindControl("CheckBox1");
            if (hzf.Checked)
            {
                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();

                string sqlstate4 = "update anjianinfo2 set state='关闭' where rwbianhao='" + sid + "'";
                SqlCommand cmdstate4 = new SqlCommand(sqlstate4, con);
                cmdstate4.ExecuteNonQuery();






            }
        }
        con.Close();
        Bind3();
    }
}