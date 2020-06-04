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
using System.Drawing;
using System.Text;
using System.IO;
using Common;
using DBBLL;
using DBTable;
public partial class YangPinManage1 : System.Web.UI.Page
{
    private int _i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            //DateTime dt = DateTime.Now.AddMonths(-6);
            //int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            //int dayspan = (-1) * weeknow + 1;
            //DateTime dt2 = DateTime.Now.AddMonths(+6);
            ////本月第一天
            //txFDate.Value = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");

            ////本月最后一天
            //txTDate.Value = dt2.AddDays(-dt.Day).ToString("yyyy-MM-dd");
            // limit("样品管理");
            txFDate.Value = DateTime.Now.AddMonths(-12).ToString("yyyy-MM-01");


            DateTime lastDay = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddHours(23);

            txTDate.Value = lastDay.ToString("yyyy-MM-dd").ToString();

            Bind1();

            TextBox1.Focus();

        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string begintime;
        string endtime;
        int ChooseNo = int.Parse(DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;
        begintime = txFDate.Value;
        endtime = Convert.ToDateTime(txTDate.Value).AddDays(1).ToShortDateString();

        TimeBind(begintime, endtime, ChooseNo, ChooseValue);

        AspNetPager2.Visible = false;
    }
    protected void TimeBind(string a, string b, int c, string d)
    {
        string sqlL = "(select TOP 1 name  from YangPin2Detail where sampleid=YangPin2.sampleid and state='借出' order by id desc) as dd";
        string ds1 = a;
        string ds2 = b;
        int ChooseID = c;
        string ChooseValue = d;
        string wher = "";
        string sql4 = "(select top 1 yangpinchuli from anjianxinxi2 where taskno=anjianid) as rr";

        if (DropDownList3.SelectedValue == "全部")
        {
            wher = " 1=1 ";
        }
        else
        {
            wher = " anjianid in (select rwbianhao from anjianinfo2 where state='" + DropDownList3.SelectedValue + "')";
        }
        string sqlstr;

        if (DropDownList2.SelectedValue == "全部")
        {

            switch (ChooseID)
            {
                case 0:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "' and " + wher + " order by id desc";
                    break;
                case 1:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "' and ( kehuname like '%" + ChooseValue + "%' or position like '%" + ChooseValue + "%') and " + wher + " order by id desc";
                    break;
                case 2:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "' and name like '%" + ChooseValue + "%' and " + wher + " order by id desc";
                    break;
                case 3:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "' and sampleid ='" + ChooseValue + "' and " + wher + " order by id desc";
                    break;
                case 5:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "' and anjianid like '%" + ChooseValue + "%' and " + wher + " order by id desc";
                    break;
                case 6:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "'  and anjianid in(select rwbianhao from  anjianinfo2 where shenqingbianhao like '%" + ChooseValue + "%') and " + wher + " order by id desc";
                    break;
                case 7:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "'  and sampleid in(select sampleid from  YangPin2Detail where pub_field3 like '%" + ChooseValue + "%') and " + wher + " order by id desc";
                    break;





                case 8:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "'  and (beizhu4  like '%" + ChooseValue + "%') and " + wher + " order by id desc";
                    break;

                default:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "' and " + wher + " order by id desc";
                    break;
            }
        }
        else
        {
            switch (ChooseID)
            {
                case 0:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "' and " + wher + " and state ='" + DropDownList2.SelectedValue + "' order by id desc";
                    break;
                case 1:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "' and ( kehuname like '%" + ChooseValue + "%' or position like '%" + ChooseValue + "%') and " + wher + " and state ='" + DropDownList2.SelectedValue + "' order by id desc";
                    break;
                case 2:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "' and name like '%" + ChooseValue + "%' and " + wher + " and state ='" + DropDownList2.SelectedValue + "' order by id desc";
                    break;
                case 3:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "' and sampleid ='" + ChooseValue + "' and " + wher + " and state ='" + DropDownList2.SelectedValue + "'  order by id desc";
                    break;
                case 5:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "' and anjianid like '%" + ChooseValue + "%' and " + wher + " and state ='" + DropDownList2.SelectedValue + "' order by id desc";
                    break;

                case 6:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "'  and anjianid in (select rwbianhao from  anjianinfo2 where shenqingbianhao like '%" + ChooseValue + "%') and " + wher + " and state ='" + DropDownList2.SelectedValue + "' order by id desc";
                    break;
                case 7:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "'  and sampleid in(select sampleid from  YangPin2Detail where pub_field3 like '%" + ChooseValue + "%') and " + wher + " and state ='" + DropDownList2.SelectedValue + "' order by id desc";
                    break;
                case 8:
                    sqlstr = "select  *," + sql4 + ",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "'  and beizhu4 like '%" + ChooseValue + "%' and " + wher + " and state ='" + DropDownList2.SelectedValue + "' order by id desc";
                    break;

                default:
                    sqlstr = "select  *," + sql4 + ",(select top 1  state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 responser from BaoJiaBiao where baojiaid=(select baojiaid from AnJianInFo2 where rwbianhao=anjianid )) as kf from YangPin2 where   " + DropDownList4.SelectedValue + " between '" + ds1 + "' and '" + ds2 + "' and " + wher + " and state ='" + DropDownList2.SelectedValue + "' order by id desc";
                    break;
            }
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;
            e.Row.Cells[2].Text = SubStr(e.Row.Cells[2].Text, 5);
            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 5);
            e.Row.Cells[4].Text = SubStr(e.Row.Cells[4].Text, 5);
            //e.Row.Cells[17].Text = SubStr(e.Row.Cells[17].Text, 2);

            //MyExcutSql ext = new MyExcutSql();
            //e.Row.Cells[7].Text = ext.Eng(e.Row.Cells[1].Text);



        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "chakan")
        {
            Response.Redirect("YangPinSee.aspx?yangpinid=" + sid);
        }
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {

        Bind1();

    }

    protected void Bind1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql2 = "update yangpin2 set bianhao=anjianinfo2.bianhao from anjianinfo2 where yangpin2.anjianid=anjianinfo2.rwbianhao";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        cmd2.ExecuteNonQuery();
        //string wher = " 1=1";
        //string sqlL = "(select TOP 1 name  from YangPin2Detail where sampleid=YangPin2.sampleid and state='借出' order by id desc) as dd,(select top 1 yangpinchuli from anjianxinxi2 where taskno=anjianid) as rr";
        //string sql4 = "(select top 1 kuaidiid from kuaidizibiao where leixing='样品' and neirong=YangPin2.sampleid order by id desc) as kd";
        //string sql = "select top 200 *,"+sql4+",(select top 1 state from anjianinfo2 where rwbianhao=anjianid) as zt,(select top 1 dajine from cashin2 where taskid=YangPin2.anjianid) as dajine," + sqlL + ",(select top 1 bianhao from anjianinfo2 where rwbianhao=anjianid) as bianhao,(select top 1 name from zhujianengineer where bianhao=anjianid) as gc,(select top 1 kf from anjianinfo2 where rwbianhao=anjianid) as kf from YangPin2 where  " + wher + "  order by id desc";
        SqlDataAdapter ad = new SqlDataAdapter("daozhang", con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;
        DataSet ds = new DataSet();
        ad.Fill(ds);
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
        DisableControls(GridView1);

        Response.ClearContent();

        //Response.AddHeader("content-disposition", "attachment; filename=qiandanlist" + DateTime.Now.ToShortDateString() + ".xls");

        Response.ContentType = "application/ms-excel";

        Response.Charset = "GB2312";
        Response.AddHeader("Content-Disposition", "attachment;   filename=" + System.Web.HttpUtility.UrlEncode("样品管理入库列表" + DateTime.Now, System.Text.Encoding.UTF8) + ".xls");//这样的话，可以设置文件名为中文，且文件名不会乱码。其实就是将汉字转换成UTF8      


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
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/YangPin/YangPinAdd.aspx?baojiaid=&&kehuid=&&bianhao=");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/YangPin/YangPinPiLiang.aspx?baojiaid=&&kehuid=");
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
        string ypid = GridView1.Rows[e.RowIndex].Cells[0].Text.ToString();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_YangPin2 = "delete YangPin2 where sampleid='" + ypid + "'";
            SqlCommand com_yangpin2 = new SqlCommand(sql_YangPin2, con);
            com_yangpin2.ExecuteNonQuery();
            string sql_YaoPinManage = "delete YaoPinManage where picihao='" + ypid + "'";
            SqlCommand com_yaopinmanage = new SqlCommand(sql_YaoPinManage, con);
            com_yaopinmanage.ExecuteNonQuery();
            Literal1.Text = "<script>alert('删除成功')</script>";
            Bind1();
        }
    }
}
