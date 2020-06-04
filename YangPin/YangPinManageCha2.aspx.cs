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

public partial class YangPin_YangPinManageCha2 : System.Web.UI.Page
{
    private int _i = 0;
    string kehuname = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        Label6.Text = Request.QueryString["taskno"].ToString();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sqlstr = "select kehuname from yangpin2 where sampleid='" + Label6.Text + "'";
        SqlCommand cmd = new SqlCommand(sqlstr, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            kehuname = dr["kehuname"].ToString();
        }

        con.Close();
        con.Dispose();



        if (!IsPostBack)
        {
            Bind1();
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //string begintime;
        //string endtime;
        //int ChooseNo = int.Parse(DropDownList1.SelectedValue);
        //string ChooseValue = TextBox1.Text;
        //begintime = txFDate.Value;
        //endtime = Convert.ToDateTime(txTDate.Value).AddDays(1).ToShortDateString();


        TimeBind();

        AspNetPager2.Visible = false;
    }
    protected void TimeBind()
    {
        string sql = "select * from anjianinfo2 where (fukuandanwei like '%" + TextBox1.Text.Trim() + "%' or weituodanwei like '%" + TextBox1.Text.Trim() + "%' or zhizaodanwei like '%" + TextBox1.Text.Trim() + "%' or shengchandanwei like '%" + TextBox1.Text.Trim() + "%' or rwbianhao like '%" + TextBox1.Text.Trim() + "%' or shenqingbianhao like '%" + TextBox1.Text.Trim() + "%' or kf like '%" + TextBox1.Text.Trim() + "%') and rwbianhao not  like 'D%' order by id desc";

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
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
            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");


            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

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

        string sql = "select * from anjianinfo2 where (fukuandanwei like '%" + kehuname + "%' or weituodanwei like '%" + kehuname + "%' or zhizaodanwei like '%" + kehuname + "%' or shengchandanwei like '%" + kehuname + "%') and rwbianhao not  like 'D%' order by id desc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
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
    protected void Button5_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            if (hzf.Checked)
            {
                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString().Trim();
                string bianhao = "";
                string sql = "select bianhao from anjianinfo2 where rwbianhao='" + sid + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    bianhao = dr["bianhao"].ToString();
                }
                dr.Close();
                string sql2 = "update yangpin2 set anjianid='" + sid + "',bianhao='" + bianhao + "' where sampleid='" + Label6.Text.Trim() + "'";
                SqlCommand com2 = new SqlCommand(sql2, con);
                com2.ExecuteNonQuery();
            }
        }

        con.Close();

        Response.Write("<script>alert('关联成功')</script>");
    }
}