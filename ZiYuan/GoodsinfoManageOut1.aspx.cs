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
using DBBLL;
using DBTable;
using System.Drawing;
using System.IO;

public partial class ZiYuan_GoodsinfoManageOut1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["username"] = "ccic";

        if (!IsPostBack)
        {
            txFDate.Value = "2010-01-01";
            txTDate.Value = DateTime.Now.AddDays(1).ToShortDateString();



            string begintime;
            string endtime;
            int ChooseNo = int.Parse(DropDownList1.SelectedValue);
            string ChooseValue = TextBox1.Text;
            begintime = txFDate.Value;
            endtime = Convert.ToDateTime(txTDate.Value).AddDays(1).ToShortDateString();

            TimeBind(begintime, endtime, ChooseNo, ChooseValue);


        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "chakan")
        {
            Response.Redirect("GoodsInfoUpdate.aspx?bianhao=" + sid);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
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
    }

    protected void TimeBind(string a, string b, int c, string d)
    {
        string ds1 = a;
        string ds2 = b;
        int ChooseID = c;
        string ChooseValue = d;
        string sqlstr;
        switch (ChooseID)
        {
            case 0:
                sqlstr = "select * from GoodsInfo where buydate between '" + ds1 + "' and '" + ds2 + "' order by id desc  ";
                break;
            case 1:
                sqlstr = "select * from GoodsInfo where buydate between '" + ds1 + "' and '" + ds2 + "' and jq_name like '%" + ChooseValue + "%' order by id desc  ";
                break;
            case 2:
                sqlstr = "select * from GoodsInfo where buydate between '" + ds1 + "' and '" + ds2 + "' and gys_linkman like '%" + ChooseValue + "%' order by id desc  ";
                break;
            case 3:
                sqlstr = "select * from GoodsInfo where buydate between '" + ds1 + "' and '" + ds2 + "' and gys_tel like '%" + ChooseValue + "%' order by id desc  ";
                break;
            case 4:
                sqlstr = "select * from GoodsInfo where buydate between '" + ds1 + "' and '" + ds2 + "' and gys_address like '%" + ChooseValue + "%' order by id desc  ";
                break;
            default:
                sqlstr = "select * from GoodsInfo where buydate between '" + ds1 + "' and '" + ds2 + "' order by id desc  ";
                break;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionrz"]);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        con.Close();
        con.Dispose();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DisableControls(GridView1);
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=statistics" + DateTime.Now.ToShortDateString() + ".xls");
        Response.ContentType = "application/ms-excel";
        Response.Charset = "UTF-8";
        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
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
}