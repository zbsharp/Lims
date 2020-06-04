using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using ASP;

public partial class TongJi_TaskSearch : System.Web.UI.Page
{
    const string basesql = "select baojiacpxiangmu.id as dataid,anjianinfo2.filltime as shijishijian,BaoJiaCPXiangMu.baojiaid, baojiacpxiangmu.bumen as shiyanshi,anjianinfo2.rwbianhao, xiadariqi, (select sum(feiyong) from ceshifeikf where xmid=baojiacpxiangmu.id and taskid=anjianinfo2.rwbianhao and "
            + " project='检测费') as feiyong,(select realdiscount from BaoJiaBiao where baojiaid=anjianinfo2.baojiaid) as zhekou,yaoqiuwanchengriqi,"
            + " (select responser from BaoJiaBiao where baojiaid=anjianinfo2.baojiaid) as yewuyuan,(select departmentname from UserInfo where UserName=(select responser"
            + " from BaoJiaBiao where baojiaid=anjianinfo2.baojiaid)) as xiaoshoutuandui,(select top 1 UserName from CustomerServer where marketid = "
            + " (select responser from BaoJiaBiao where baojiaid = anjianinfo2.baojiaid)) as xiaoshouzhuli,anjianinfo2.fillname,(select customname from "
            + " Customer where kehuid=anjianinfo2.kehuid) as kehuname,anjianinfo2.fukuandanwei as fukuandanwei,(select telephone from CustomerLinkMan where id in(select linkid from BaoJiaLink where "
            + " baojiaid=anjianinfo2.baojiaid)) as telephone,  "
            + "(select name from CustomerLinkMan where id in(select linkid from BaoJiaLink where  baojiaid=anjianinfo2.baojiaid)) as linkmanname,"
            + " (select mobile from CustomerLinkMan where id in(select linkid from BaoJiaLink where  baojiaid=anjianinfo2.baojiaid)) as mobile,"
            + " AnJianInFo2.name as chanpin,anjianinfo2.xinghao as xinghao,anjianinfo2.state as xmstate,"
            + " (select chanpinname from Product2 where id=BaoJiaCPXiangMu.xiaoid) as chanpinname,BaoJiaCPXiangMu.ceshiname,BaoJiaCPXiangMu.epiboly,"
            + " (select SUM(feiyong) from CeShiFeiKf where xmid=baojiacpxiangmu.id and taskid=anjianinfo2.rwbianhao and project='规费') as waibaofeiyong,"
            + "AnJianInFo2.youxian as kehuleixing,(select Name from Bankaccount where id = (select zhangdan from BaoJiaBiao "
            + "where BaoJiaId = AnJianInFo2.baojiaid)) as fukuantaitou,anjianinfo2.beizhu3 as wanchengriqi from BaoJiaCPXiangMu left join AnjianXinXi3 on "
            + "BaoJiaCPXiangMu.id=anjianxinxi3.xiangmubianhao left join AnJianInFo2 on anjianxinxi3.bianhao=anjianinfo2.bianhao   where anjianxinxi3.bianhao!='' and anjianinfo2.id!='' and AnJianInFo2.rwbianhao not like 'D%'  ";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!base.IsPostBack)
        {
            txFDate.Value = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");
            txTDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
            BindData();
        }
    }

    protected void BindDataWithoutPaging()
    {
        string dutyname = Dutyname();
        string sql = string.Empty;
        if (dutyname == "销售助理")
        {
            sql = basesql + "and (BaoJiaCPXiangMu.baojiaid in (select baojiaid from BaoJiaBiao where responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') and (baojiaid like 'FY%' or baojiaid like 'LH%')))"
                  + " and convert(datetime,anjianinfo2.xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by baojiacpxiangmu.id asc";
        }
        else
        {
            sql = basesql + " and convert(datetime,anjianinfo2.xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by baojiacpxiangmu.id asc";
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        da.SelectCommand.CommandTimeout = 120;
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        this.GridView1.DataSource = ds.Tables[0];
        this.GridView1.DataBind();
    }

    protected void BindData()
    {
        string dutyname = Dutyname();
        string sql = string.Empty;
        if (dutyname == "销售助理")
        {
            sql = basesql + "and (BaoJiaCPXiangMu.baojiaid in (select baojiaid from BaoJiaBiao where responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') and (baojiaid like 'FY%' or baojiaid like 'LH%')))"
                  + " and convert(datetime,anjianinfo2.xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by baojiacpxiangmu.id asc";
        }
        else
        {
            sql = basesql + " and convert(datetime,anjianinfo2.xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by baojiacpxiangmu.id asc";
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        da.SelectCommand.CommandTimeout = 120;
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

    protected void Button_search_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string taskid = e.Row.Cells[3].Text;
            string xiangmuid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            string xiadashijian = e.Row.Cells[1].Text;
            try
            {
                e.Row.Cells[1].Text = xiadashijian.Substring(xiadashijian.IndexOf(' ') + 1);
            }
            catch
            {
            }
            string getreportidsql = "select baogaoid,fafangdate from baogao2 where baogaoid in(select baogaoid from itembaogao where xmid='" + xiangmuid + "' and taskid='" + taskid + "')";
            string getengineersql = "select name,fillname from ZhuJianEngineer where name in(select engineer from projectitem where xmid='" + xiangmuid + "' and taskid='" + taskid + "') and bianhao='" + taskid + "'";
            string getsamplesql = "select top(5) name from yangpin2 where anjianid='" + taskid + "' order by id asc";

            string baogaoid = concatetable(getreportidsql, 0);
            string fafangdate = concatetable(getreportidsql, 1);
            string gognchengshi = concatetable(getengineersql, 0);
            string gognchengjingli = concatetable(getengineersql, 1);
            string yangpinxinxi = concatetable(getsamplesql, 0);
            e.Row.Cells[23].Text = baogaoid;
            e.Row.Cells[31].Text = fafangdate;
            e.Row.Cells[25].Text = gognchengjingli;
            e.Row.Cells[26].Text = gognchengshi;
            e.Row.Cells[28].Text = yangpinxinxi;
            if (e.Row.Cells[4].Text == null || e.Row.Cells[4].Text == "" || e.Row.Cells[4].Text == "&nbsp;")
            {
                e.Row.Cells[4].Text = "0.00";
            }
            if (e.Row.Cells[21].Text == null || e.Row.Cells[21].Text == "" || e.Row.Cells[21].Text == "&nbsp;")
            {
                e.Row.Cells[21].Text = "0.00";
            }
            try
            {
                decimal jiancefei = Convert.ToDecimal(e.Row.Cells[4].Text);
                decimal guifei = Convert.ToDecimal(e.Row.Cells[21].Text);
                e.Row.Cells[22].Text = (jiancefei + guifei).ToString("f2");
            }
            catch
            {
            }


        }
    }

    protected string concatetable(string sql, int rowid)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        DataTable dt = ds.Tables[0];
        string result = "";
        if (dt.Rows.Count != 0)
        {
            if (dt.Rows.Count == 1)
            {
                result = dt.Rows[0][rowid].ToString();
            }
            else
            {
                result = dt.Rows[0][rowid].ToString();
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    result = result + "/" + dt.Rows[i][rowid].ToString();
                    if (result == "/")
                    {
                        result = string.Empty;
                    }
                }
            }
        }

        return result;
    }

    private void DisableControls(Control gv)
    {
        new LinkButton();
        Literal literal = new Literal();
        string arg_11_0 = string.Empty;
        for (int i = 0; i < gv.Controls.Count; i++)
        {
            if (gv.Controls[i].GetType() == typeof(LinkButton))
            {
                literal.Text = (gv.Controls[i] as LinkButton).Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, literal);
            }
            else
            {
                if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    literal.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, literal);
                }
            }
            if (gv.Controls[i].HasControls())
            {
                this.DisableControls(gv.Controls[i]);
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void Button_outport_Click(object sender, EventArgs e)
    {
        GridView1.AllowPaging = false; //清除分页
        GridView1.AllowSorting = false; //清除排序     
        BindDataWithoutPaging();
        this.DisableControls(this.GridView1);
        base.Response.ClearContent();
        base.Response.AddHeader("content-disposition", "attachment; filename=TaskMessage" + DateTime.Now.ToShortDateString() + ".xls");
        base.Response.ContentType = "application/ms-excel";
        base.Response.Charset = "UTF-8";
        base.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
        this.GridView1.RenderControl(writer);
        base.Response.Write(stringWriter.ToString());
        base.Response.End();
        GridView1.AllowSorting = true; //恢复分页          
        BindData(); //再次绑定
    }

    protected string Dutyname()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmd = new SqlCommand(sql_dutyname, con);
            SqlDataReader dr = cmd.ExecuteReader();
            string str = string.Empty;
            if (dr.Read())
            {
                str = dr["dutyname"].ToString();
            }
            return str;
        }
    }
}