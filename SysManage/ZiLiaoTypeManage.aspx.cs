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
using Common;
using DBTable;
using System.Data.Common;
using System.Text;
using System.IO;

public partial class SysManage_ZiLiaoTypeManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now;
            int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            int dayspan = (-1) * weeknow + 1;
            DateTime dt2 = dt.AddMonths(1);
            //本月第一天
            txFDate.Value = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");

            //本月最后一天
            txTDate.Value = dt2.AddDays(-dt.Day).ToString("yyyy-MM-dd");

            Bind(txFDate.Value, txTDate.Value, int.Parse(DropDownList1.SelectedValue), TextBox1.Text);
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "chakan")
        {
            Response.Redirect("ZiLiaoTypeSee.aspx?id=" + sid);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string begintime;
        string endtime;
        string onlytime;
        begintime = txFDate.Value;
        endtime = txTDate.Value;
        int ChooseNo = int.Parse(DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text;

        Bind(begintime, endtime, ChooseNo, ChooseValue);
    }

    protected void Bind(string a, string b, int c, string d)
    {
        string ds1 = a;
        string ds2 = b;
        int ChooseID = c;
        string ChooseValue = d;

        string sqlstr;
        switch (ChooseID)
        {
            case 0:
                sqlstr = "select * from ZiLiaoType where filltime between '" + ds1 + "' and '" + ds2 + "' order by id asc  ";
                break;
            case 1:
                sqlstr = "select * from ZiLiaoType where filltime between '" + ds1 + "' and '" + ds2 + "' and name like'%" + ChooseValue + "%' order by id asc  ";
                break;
            default:
                sqlstr = "select * from ZiLiaoType where filltime between '" + ds1 + "' and '" + ds2 + "' order by id asc  ";
                break;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        DisableControls(GridView1);
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=TrainList" + DateTime.Now.ToShortDateString() + ".xls");
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
}