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
using System.ServiceModel.Channels;

public partial class TongJi_LaboratoryData : System.Web.UI.Page
{
    const string basesql = @"select BaoJiaCPXiangMu.id as xmid,anjianinfo2.xiadariqi as 受理日期,yaoqiuwanchengriqi as 要求完成时间,  
                             (select Name from Bankaccount where id = (select zhangdan from BaoJiaBiao where BaoJiaId = AnJianInFo2.baojiaid)) as 收款抬头,
                             BaoJiaCPXiangMu.baojiaid as 报价编号, anjianinfo2.rwbianhao as 任务编号, 
                             (select responser from BaoJiaBiao where baojiaid=anjianinfo2.baojiaid) as 业务员,
                             (select departmentname from UserInfo where UserName=(select responser from BaoJiaBiao where baojiaid=anjianinfo2.baojiaid)) as 所属部门,
                             (select top 1 UserName from CustomerServer where marketid = (select responser from BaoJiaBiao where baojiaid = anjianinfo2.baojiaid)) as 销售助理,
                             (select customname from  Customer where kehuid=anjianinfo2.kehuid) as 公司名称,AnJianInFo2.name as 产品名称,
                             anjianinfo2.xinghao as 主测型号,BaoJiaCPXiangMu.ceshiname as 测试项目,BaoJiaCPXiangMu.biaozhun as 项目标准,AnJianInFo2.youxian as 客户类型,
                             (select top 1 zhouqi from AnJianInFo where AnJianInFo.tijiaobianhao=AnJianInFo2.bianhao) as 是否加急,
                             baojiacpxiangmu.bumen as 实验室,
                             anjianinfo2.state as 案件状态,anjianinfo2.beizhu3 as  结案日期,
                             (select sum(feiyong) from ceshifeikf where xmid=baojiacpxiangmu.id and taskid=anjianinfo2.rwbianhao and  project='检测费') as feiyong,
                             BaoJiaCPXiangMu.epiboly as 是否外包,
                            (select SUM(feiyong) from CeShiFeiKf where xmid=baojiacpxiangmu.id and taskid=anjianinfo2.rwbianhao and project='规费') as waibaofeiyong
                              from BaoJiaCPXiangMu left join AnjianXinXi3 on BaoJiaCPXiangMu.id=anjianxinxi3.xiangmubianhao 
                             left join AnJianInFo2 on anjianxinxi3.bianhao=anjianinfo2.bianhao 
                             where anjianxinxi3.bianhao!='' and anjianinfo2.id!=''";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!base.IsPostBack)
        {
            bool bu = limit1("工程明细统计");
            if (bu == true)
            {
                txFDate.Value = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");
                txTDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
                BindData();
            }
            else
            {
                Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
            }
        }
    }

    protected void BindDataWithoutPaging()
    {
        DataTable da_user = Dutyname();
        string dutyname = da_user.Rows[0]["dutyname"].ToString();//职位
        string dn = da_user.Rows[0]["departmentname"].ToString();//部门
        string sql = "";
        if (dutyname == "总经理" || dutyname == "系统管理员" || dutyname.Contains("客服"))
        {
            sql = basesql + " and  convert(datetime,anjianinfo2.xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by baojiacpxiangmu.id asc";
        }
        else if (dn == "认证部")
        {
            sql = basesql + " and BaoJiaCPXiangMu.epiboly='外包' and convert(datetime,anjianinfo2.xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by baojiacpxiangmu.id asc";
        }
        else
        {
            sql = basesql + " and BaoJiaCPXiangMu.bumen=(select departmentname from userinfo where username='" + Session["Username"].ToString() + "') and convert(datetime,anjianinfo2.xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by baojiacpxiangmu.id asc";
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        this.GridView1.DataSource = ds.Tables[0];
        this.GridView1.DataBind();
    }

    protected void BindData()
    {
        DataTable da_user = Dutyname();
        string dutyname = da_user.Rows[0]["dutyname"].ToString();//职位
        string dn = da_user.Rows[0]["departmentname"].ToString();//部门
        string sql = "";
        if (dutyname == "总经理" || dutyname == "系统管理员" || dutyname.Contains("客服"))
        {
            sql = basesql + " and  convert(datetime,anjianinfo2.xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by baojiacpxiangmu.id asc";
        }
        else if (dn == "认证部")
        {
            sql = basesql + " and BaoJiaCPXiangMu.epiboly='外包' and convert(datetime,anjianinfo2.xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by baojiacpxiangmu.id asc";
        }
        else
        {
            sql = basesql + " and BaoJiaCPXiangMu.bumen=(select departmentname from userinfo where username='" + Session["Username"].ToString() + "') and convert(datetime,anjianinfo2.xiadariqi) between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' order by baojiacpxiangmu.id asc";
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
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
            string taskid = e.Row.Cells[4].Text;
            string xiangmuid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();

            string getreportidsql = "select baogaoid,fafangdate from baogao2 where baogaoid in(select baogaoid from itembaogao where xmid='" + xiangmuid + "' and taskid='" + taskid + "')";
            string getengineersql = "select name,fillname from ZhuJianEngineer where name in(select engineer from projectitem where xmid='" + xiangmuid + "' and taskid='" + taskid + "') and bianhao='" + taskid + "'";
            string getceshiyuan = "select ceshiyuan from DaiFenTest where renwuid='" + taskid + "' and xmid='" + xiangmuid + "' ";

            string baogaoid = concatetable(getreportidsql, 0);
            string gognchengshi = concatetable(getengineersql, 0);
            string gognchengjingli = concatetable(getengineersql, 1);
            string ceshiyuan = concatetable(getceshiyuan, 0);

            e.Row.Cells[15].Text = baogaoid;
            e.Row.Cells[17].Text = gognchengjingli;//分派人-工程经理
            e.Row.Cells[18].Text = gognchengshi;//工程师
            e.Row.Cells[19].Text = ceshiyuan;//测试员

            if (e.Row.Cells[22].Text == null || e.Row.Cells[22].Text == "" || e.Row.Cells[22].Text == "&nbsp;")
            {
                e.Row.Cells[22].Text = "0.00";
            }
            if (e.Row.Cells[24].Text == null || e.Row.Cells[24].Text == "" || e.Row.Cells[24].Text == "&nbsp;")
            {
                e.Row.Cells[24].Text = "0.00";
            }

            try
            {
                decimal jiancefei = Convert.ToDecimal(e.Row.Cells[22].Text);
                decimal guifei = Convert.ToDecimal(e.Row.Cells[24].Text);
                e.Row.Cells[25].Text = (jiancefei + guifei).ToString("f2");
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
        base.Response.AddHeader("content-disposition", "attachment; filename=Laboratory" + DateTime.Now.ToShortDateString() + ".xls");
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

    protected DataTable Dutyname()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlDataAdapter dr = new SqlDataAdapter(sql_dutyname, con);
            DataSet ds = new DataSet();
            dr.Fill(ds);
            return ds.Tables[0];
        }
    }
}