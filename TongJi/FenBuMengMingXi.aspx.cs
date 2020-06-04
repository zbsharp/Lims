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

public partial class TongJi_FenBuMengMingXi : System.Web.UI.Page
{
    private int _i = 0;

    //protected string str = "select sum(jine),kehuid from cashin left join anjianinfo2 on cashin.taskid=anjianinfo2.rwbianhao  where cashin.beizhu3='EMC射频部' and  pinzheng in (select shoufeiid from shuipiao where querenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "') group by kehuid";
    protected void Page_Load(object sender, EventArgs e)
    {
       // if (Session["UserName"].ToString() == "admin" || Session["UserName"].ToString() == "王鹏" || Session["UserName"].ToString() == "林幸笋" || Session["UserName"].ToString() == "许芳" || Session["UserName"].ToString() == "于敦贞" || Session["UserName"].ToString() == "徐毅敏" || Session["UserName"].ToString() == "李思雄" || Session["UserName"].ToString() == "宣水根" || Session["UserName"].ToString() == "孙山" || Session["UserName"].ToString() == "谢玉章" || Session["UserName"].ToString() == "邓春涛")
        {

            if (!IsPostBack)
            {
                DateTime dt = DateTime.Now;
                int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
                int dayspan = (-1) * weeknow + 1;
                DateTime dt2 = dt.AddMonths(1);
                //本月第一天
                txFDate.Value = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");

                DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).AddHours(23);

                txTDate.Value = lastDay.ToShortDateString();
                TimeBind();
                BindDep();
                GridView1.ShowFooter = false;
            }
        }
        //else
        //{
        //    Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");

        //}
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        string sqlstr;
        GridView1.ShowFooter = true;
        if (DropDownList2.SelectedValue == "")
        {
            sqlstr = "select cashin.*,bianhao,kehuid from cashin left join anjianinfo2 on cashin.taskid=anjianinfo2.rwbianhao  where cashin.beizhu3='" + DropDownList1.SelectedValue + "' and  pinzheng in (select shoufeiid from shuipiao where querenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "') and pinzheng !=''  order by tichenriqi desc";
        }
        else
        {
            sqlstr = "select cashin.*,bianhao,kehuid from cashin left join anjianinfo2 on cashin.taskid=anjianinfo2.rwbianhao  where cashin.beizhu3='" + DropDownList1.SelectedValue + "' and  pinzheng in (select shoufeiid from shuipiao where querenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "') and pinzheng !='' and cashin.taskid =(select top 1 bianhao from zhujianengineer where bianhao=cashin.taskid and name ='"+DropDownList2.SelectedValue+"') order by tichenriqi desc";
 
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        AspNetPager1.Visible = false;
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }
    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();

        string sql = "";

        if (Session["UserName"].ToString() == "admin" || Session["UserName"].ToString() == "王鹏" || Session["UserName"].ToString() == "林幸笋" || Session["UserName"].ToString() == "许芳" || Session["UserName"].ToString() == "于敦贞")
        {
            sql = "select * from UserDepa where departmentid='12' or departmentid='13' or departmentid='15' or departmentid='22' or departmentid='16' or departmentid='17' or departmentid='9' or departmentid='19'";
        }
        else if (Session["UserName"].ToString() == "李思雄")
        {
            sql = "select * from UserDepa where departmentid='13'";

        }
        else if (Session["UserName"].ToString() == "孙山" || Session["UserName"].ToString() == "宣水根")
        {

            if (Session["UserName"].ToString() == "孙山")
            {
                sql = "select * from UserDepa where departmentid='12'";
            }
            else
            {
                sql = "select * from UserDepa where departmentid='22' or departmentid='12'";
            }

        }
        else if (Session["UserName"].ToString() == "谢玉章" || Session["UserName"].ToString() == "徐毅敏")
        {
            sql = "select * from UserDepa where departmentid='15'";

        }
        else if (Session["UserName"].ToString() == "邹林华" || Session["UserName"].ToString() == "吴立安")
        {
            sql = "select * from UserDepa where departmentid='16'";

        }
        else if (Session["UserName"].ToString() == "邓春涛" || Session["UserName"].ToString() == "易嗣宣")
        {
            sql = "select * from UserDepa where departmentid='17'";

        }
        else if (Session["UserName"].ToString() == "王克勤")
        {
            sql = "select * from UserDepa where departmentid='12' or departmentid='13'";

        }
        else if (Session["UserName"].ToString() == "姚成宗")
        {
            sql = "select * from UserDepa where departmentid='22'";

        }
        //else if (Session["UserName"].ToString() == "王克勤")
        //{
        //    sql = "select * from UserDepa where departmentid='12' or departmentid='13'";

        //}
        else
        {
            sql = "select * from UserDepa where name=(select departmentname from userinfo where username='" + Session["UserName"].ToString() + "')";
 
        }

        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


        DataSet ds = new DataSet();


        ad.Fill(ds);


        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataValueField = "name";
        DropDownList1.DataTextField = "name";
        DropDownList1.DataBind();

        DropDownList1.Items.Insert(0, new ListItem("", ""));//

        con3.Close();
    }


    protected void TimeBind()
    {

        string sqlstr;

       // string wh = "pinzheng in (select shoufeiid from shuipiao where querenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "')";


        sqlstr = "select cashin.*,bianhao,kehuid from cashin left join anjianinfo2 on cashin.taskid=anjianinfo2.rwbianhao  where cashin.beizhu3='" + DropDownList1.SelectedValue + "' and  pinzheng in (select shoufeiid from shuipiao where querenriqi between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "') and pinzheng !=''";




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

    private decimal sum1 = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {



          
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

            MyExcutSql my = new MyExcutSql();
          

         

            string sql4 = "select customname from customer where kehuid='" + e.Row.Cells[0].Text + "'";
            e.Row.Cells[1].Text = my.ExcutSql2(sql4, 0);


            sum1 += Convert.ToDecimal(e.Row.Cells[4].Text);

            MyExcutSql ext = new MyExcutSql();
            e.Row.Cells[8].Text = ext.Eng(e.Row.Cells[6].Text);




            string sql41 = "select top 1 liushuihao from shuipiao where shoufeiid='" + e.Row.Cells[2].Text + "'";
            e.Row.Cells[9].Text = my.ExcutSql2(sql41, 0);


            string sql411 = "select top 1 beizhu from shuipiao where shoufeiid='" + e.Row.Cells[2].Text + "'";
            e.Row.Cells[10].Text = my.ExcutSql2(sql411, 0);

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "合计：";

            e.Row.Cells[4].Text = sum1.ToString();
        }

    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        TimeBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {




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

    public string SubStr(string sString, int nLeng)
    {
        if (sString.Length <= nLeng)
        {
            return sString;
        }
        string sNewStr = sString.Substring(0, nLeng);

        return sNewStr;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo where department='" + DropDownList1.SelectedValue + "' order by username asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataTextField = "username";
        DropDownList2.DataValueField = "username";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("", ""));//

        con.Close();
    }
}