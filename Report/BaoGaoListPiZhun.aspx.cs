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

public partial class Report_BaoGaoListPiZhun : System.Web.UI.Page
{
    protected string shwhere = "1=1";
    private int _i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        shwhere = " 1=1 ";
        Button3.Attributes.Add("onclick", "return confirm('您确定需要修改选中报告为完成状态吗？')");
        if (!IsPostBack)
        {
            txFDate.Value = DateTime.Now.AddYears(-1).AddDays(-7).ToString("yyyy-MM-dd");
            txTDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            shangzhi_time.Value = DateTime.Now.ToString("yyyy-MM-dd");
            Bind();
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


    public void Bind()
    {
        Literal1.Text = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "";
        string position = Position();
        string bumen = BuMen();
        if (position == "系统管理员" || position == "总经理" || position == "董事长")
        {
            sql = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and beizhu1='否' and rwid not like 'D%' order by id desc";
        }
        else if (bumen == "龙华EMC部")
        {
            sql = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源EMC部') and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and beizhu1='否' and rwid not like 'D%' order by id desc";
        }
        else if (bumen == "龙华安规部")
        {
            sql = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源安规部') and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and beizhu1='否' and rwid not like 'D%' order by id desc";
        }
        else
        {
            sql = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  leibie like '%" + bumen + "%' and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and beizhu1='否' and rwid not like 'D%' order by id desc";
        }
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        AspNetPager2.Visible = false;
        Literal1.Text = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseNo = (DropDownList1.SelectedValue);
        string ChooseValue = TextBox1.Text.Trim();
        string position = Position();
        string shwere = DropDownList2.SelectedValue;
        string bumen = BuMen();
        string sqlstr;
        if (position.Trim() == "系统管理员" || position == "总经理" || position == "董事长")
        {
            if (string.IsNullOrEmpty(shwere))
            {
                if (DropDownList1.SelectedValue == "0")
                {
                    sqlstr = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
                else
                {
                    sqlstr = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and  " + ChooseNo + " like '%" + ChooseValue + "%' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
            }
            else
            {
                if (DropDownList1.SelectedValue == "0")
                {
                    sqlstr = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and [state]='" + shwere + "' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
                else
                {
                    sqlstr = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and [state]='" + shwere + "' and  " + ChooseNo + " like '%" + ChooseValue + "%' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
            }
        }
        else if (bumen == "龙华EMC部")
        {
            if (string.IsNullOrEmpty(shwere))
            {
                if (DropDownList1.SelectedValue == "0")
                {
                    sqlstr = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源EMC部') and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
                else
                {
                    sqlstr = " select top 200 *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源EMC部') and  " + ChooseNo + " like  '%" + ChooseValue + "%' and [state]!='' and  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
            }
            else
            {
                if (DropDownList1.SelectedValue == "0")
                {
                    sqlstr = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源EMC部') and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and [state]='" + shwere + "' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
                else
                {
                    sqlstr = " select top 200 *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源EMC部') and  " + ChooseNo + " like  '%" + ChooseValue + "%' and [state]!='' and [state]='" + shwere + "' and  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
            }
        }
        else if (bumen == "龙华安规部")
        {
            if (string.IsNullOrEmpty(shwere))
            {
                if (DropDownList1.SelectedValue == "0")
                {
                    sqlstr = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源安规部') and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
                else
                {
                    sqlstr = " select top 200 *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源安规部') and  " + ChooseNo + " like  '%" + ChooseValue + "%' and [state]!='' and  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
            }
            else
            {
                if (DropDownList1.SelectedValue == "0")
                {
                    sqlstr = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源安规部') and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and [state]='" + shwere + "' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
                else
                {
                    sqlstr = " select top 200 *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  (leibie like '%" + bumen + "%' or leibie='标源安规部') and  " + ChooseNo + " like  '%" + ChooseValue + "%' and [state]!='' and [state]='" + shwere + "' and  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
            }
        }
        else
        {
            if (string.IsNullOrEmpty(shwere))
            {
                if (DropDownList1.SelectedValue == "0")
                {
                    sqlstr = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  leibie like '%" + bumen + "%' and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
                else
                {
                    sqlstr = " select top 200 *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  leibie like '%" + bumen + "%' and  " + ChooseNo + " like  '%" + ChooseValue + "%' and [state]!='' and  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
            }
            else
            {
                if (DropDownList1.SelectedValue == "0")
                {
                    sqlstr = "select top 200  *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  leibie like '%" + bumen + "%' and convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and [state]!='' and [state]='" + shwere + "' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
                else
                {
                    sqlstr = " select top 200 *,(select top 1 bianhao from anjianinfo2 where rwbianhao =baogao2.tjid) as bianhao2,(select top 1 name from anjianinfo2 where rwbianhao =baogao2.tjid) as name,(select top 1 shenqingbianhao from anjianxinxi2 where taskno=baogao2.tjid) as shenqingbianhao,(select top 1 baojiaid from AnJianInFo2 where rwid=rwbianhao ) as baojiaid from BaoGao2 where  leibie like '%" + bumen + "%' and  " + ChooseNo + " like  '%" + ChooseValue + "%' and [state]!='' and [state]='" + shwere + "' and  convert(datetime,filltime) between '" + Convert.ToDateTime(txFDate.Value.Trim()) + "' and '" + Convert.ToDateTime(txTDate.Value.Trim()).AddHours(23) + "' and beizhu1='否' and rwid not like 'D%' order by id desc";
                }
            }
        }
        SqlDataAdapter ad = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
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

            MyExcutSql ext = new MyExcutSql();
            e.Row.Cells[3].Text = ext.Eng(e.Row.Cells[0].Text);
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;


        }

    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        //全选或全不选
        int i;
        if (((CheckBox)sender).Checked)
        {
            for (i = 0; i < GridView1.Rows.Count; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked = true;
            }
        }
        else
        {
            for (i = 0; i < GridView1.Rows.Count; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked = false;
            }
        }

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox hzf = (CheckBox)gr.Cells[8].FindControl("CheckBox1");
                if (hzf.Checked)
                {
                    string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                    string state = gr.Cells[4].Text.ToString();
                    string rwid = gr.Cells[0].Text.ToString();
                    string baogaoid = gr.Cells[1].Text.ToString();

                    if (state == "已出草稿")
                    {
                        string sql = "update baogao2 set statebumen1='合格',statebumen2='合格',state='已缮制', shenheby='" + Session["UserName"].ToString() + "',pizhunby='" + Session["Username"].ToString() + "',pizhundate='" + DateTime.Now.ToString() + "',CMA='" + CMA.SelectedValue + "',CNAS='" + CNAS.SelectedValue + "',AtwolA='" + AtwolA.SelectedValue + "'  where id='" + sid + "' and state='已出草稿'";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();

                        //读取需要缮制的报告，对应获取报告号的工程师所在部门userdeapa
                        string sql_depart = "select departmentname from UserInfo where UserName=(select fillname from baogao2 where baogaoid='" + baogaoid + "')";
                        SqlCommand cmd_depart = new SqlCommand(sql_depart, con);
                        SqlDataReader dr_depart = cmd_depart.ExecuteReader();
                        string bumen = "";
                        if (dr_depart.Read())
                        {
                            bumen = dr_depart["departmentname"].ToString();
                            dr_depart.Close();
                        }
                        else
                        {
                            dr_depart.Close();
                        }

                        //修改任务状态
                        string sql_itembaogao = "update ProjectItem set state='完成',finishdate='" + DateTime.Now + "' where xmid in(select xmid from ItemBaogao where baogaoid='" + baogaoid + "') and bumen='" + bumen + "'";
                        SqlCommand cmd_itembaogao = new SqlCommand(sql_itembaogao, con);
                        cmd_itembaogao.ExecuteNonQuery();

                        //读取报告对应的任务号taskno
                        string sql_projectitem = "select * from ProjectItem where engineer in(select username from userinfo where departmentname='" + bumen + "') and taskid='" + rwid + "' and state='进行中'";
                        SqlDataAdapter da_projectitem = new SqlDataAdapter(sql_projectitem, con);
                        DataSet ds_projectitem = new DataSet();
                        da_projectitem.Fill(ds_projectitem);
                        if (ds_projectitem.Tables[0].Rows.Count > 0)
                        {

                        }
                        else
                        {
                            string sql_anjianinfo = "update anjianinfo set state = '完成' where taskid = '" + rwid + "' and bumen = '" + bumen + "'";
                            SqlCommand com_anjianinfo = new SqlCommand(sql_anjianinfo, con);
                            com_anjianinfo.ExecuteNonQuery();
                            string sql_aj = "select * from anjianinfo where state='进行中' and taskid='" + rwid + "'";
                            SqlCommand com = new SqlCommand(sql_aj, con);
                            SqlDataReader dr = com.ExecuteReader();
                            if (dr.Read())
                            {
                                dr.Close();
                            }
                            else
                            {
                                dr.Close();
                                string sql_updatean = "update anjianinfo2 set state='完成',beizhu3='" + DateTime.Now.ToString() + "' where rwbianhao='" + rwid + "'";
                                SqlCommand com_update = new SqlCommand(sql_updatean, con);
                                com_update.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        Literal1.Text = "<script>alert('该报告还未出草稿')</script>";
                    }
                }
            }
        }
        Bind();
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
        GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        int index = gvrow.RowIndex;
        if (e.CommandName == "shanzhi")
        {
            string rwid = GridView1.Rows[index].Cells[0].Text.ToString();
            string state = GridView1.Rows[index].Cells[4].Text.ToString();
            string baogaoid = GridView1.Rows[index].Cells[1].Text.ToString();
            if (state == "已出草稿")
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
                {
                    con.Open();
                    string sql = "update baogao2 set statebumen1='合格',statebumen2='合格',state='已缮制', shenheby='" + Session["UserName"].ToString() + "',pizhunby='" + Session["Username"].ToString() + "',pizhundate='" + shangzhi_time.Value + "',CMA='" + CMA.SelectedValue + "',CNAS='" + CNAS.SelectedValue + "',AtwolA='" + AtwolA.SelectedValue + "'  where id='" + sid + "' and state='已出草稿'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();

                    //读取需要缮制的报告，对应获取报告号的工程师所在部门userdeapa
                    string sql_depart = "select departmentname from UserInfo where UserName=(select fillname from baogao2 where baogaoid='" + baogaoid + "')";
                    SqlCommand cmd_depart = new SqlCommand(sql_depart, con);
                    SqlDataReader dr_depart = cmd_depart.ExecuteReader();
                    string bumen = "";
                    if (dr_depart.Read())
                    {
                        bumen = dr_depart["departmentname"].ToString();
                    }
                    dr_depart.Close();


                    //修改任务状态
                    string sql_itembaogao = "update ProjectItem set state='完成' ,finishdate='" + DateTime.Now + "' where xmid in(select xmid from ItemBaogao where baogaoid='" + baogaoid + "') and bumen='" + bumen + "'";
                    SqlCommand cmd_itembaogao = new SqlCommand(sql_itembaogao, con);
                    cmd_itembaogao.ExecuteNonQuery();

                    string sql_projectitem = "select * from ProjectItem where bumen='" + bumen + "' and taskid='" + rwid + "' and state='进行中'";
                    SqlDataAdapter da_projectitem = new SqlDataAdapter(sql_projectitem, con);
                    DataSet ds_projectitem = new DataSet();
                    da_projectitem.Fill(ds_projectitem);
                    if (ds_projectitem.Tables[0].Rows.Count > 0)
                    {

                    }
                    else
                    {
                        string sql_anjianinfo = "update anjianinfo set state = '完成' where taskid = '" + rwid + "' and bumen = '" + bumen + "'";
                        SqlCommand com_anjianinfo = new SqlCommand(sql_anjianinfo, con);
                        com_anjianinfo.ExecuteNonQuery();
                        string sql_aj = "select * from anjianinfo where state='进行中' and taskid='" + rwid + "'";
                        SqlCommand com = new SqlCommand(sql_aj, con);
                        SqlDataReader dr = com.ExecuteReader();
                        if (dr.Read())
                        {
                            dr.Close();
                        }
                        else
                        {
                            dr.Close();
                            string sql_updatean = "update anjianinfo2 set state='完成',beizhu3='" + DateTime.Now.ToString() + "' where rwbianhao='" + rwid + "'";
                            SqlCommand com_update = new SqlCommand(sql_updatean, con);
                            com_update.ExecuteNonQuery();
                        }
                    }
                }
                Bind();
            }
            else
            {
                Literal1.Text = "<script>alert('该报告已缮制')</script>";
            }
        }
    }
    /// <summary>
    /// 查询当前登录进来人的部门
    /// </summary>
    /// <returns></returns>
    private string BuMen()
    {
        string name = "";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select department from UserInfo where UserName='" + Session["Username"].ToString() + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                name = dr["department"].ToString();
            }
            return name;
        }
    }
    /// <summary>
    /// 当前登录进来人的职位
    /// </summary>
    /// <returns></returns>
    protected string Position()
    {
        string position = "";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sqldd = "select dutyname from UserInfo where UserName='" + Session["Username"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sqldd, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                position = dr["dutyname"].ToString();
            }
            dr.Close();
        }
        return position;
    }
}