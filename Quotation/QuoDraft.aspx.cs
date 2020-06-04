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

public partial class Quotation_QuoDraft : System.Web.UI.Page
{
    private int _i = 0;
    protected string shwhere = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string dutyname = "";//职位
        string dn = "";//部门
        string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
        SqlCommand cmdstate = new SqlCommand(sql_dutyname, con);
        SqlDataReader dr = cmdstate.ExecuteReader();
        if (dr.Read())
        {
            dn = dr["departmentname"].ToString();
            dutyname = dr["dutyname"].ToString();
        }
        dr.Close();
        con.Close();
        con.Dispose();
        if (dutyname.Trim() == "系统管理员" || (dutyname.Trim() == "总经理" && dn == "总经办") || (dutyname.Trim() == "董事长" && Session["Username"].ToString() != "蒋娜"))
        {
            shwhere = "1=1";
        }
        else if (dutyname.Trim() == "客户经理")
        {
            shwhere = "(responser in (select name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or responser = '" + Session["Username"].ToString() + "')";
        }
        else if (dutyname.Trim() == "业务员")
        {
            shwhere = "responser = '" + Session["Username"].ToString() + "'";
        }
        else if (dutyname.Trim() == "销售助理" || dutyname.Trim().Contains("客服"))
        {
            shwhere = "(responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "')or responser = '" + Session["Username"].ToString() + "')";
        }
        else
        {
            shwhere = "(responser in (select name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or responser = '" + Session["Username"].ToString() + "')";
        }
        if (!IsPostBack)
        {
            Bind();
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Bind()
    {
        //1.系统管理员和总经理查看所有人的草稿报价、2.客户经理查看自己部门的草稿报价、3.业务员查看自己的草稿报价、4.销售助理产看她负责的销售人员
        //大客户部情况特殊
        ld.Text = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = @"select *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,
                    (select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname  from baojiabiao
                    where TiJiaoBiaozhi = '否' and " + shwhere + " and isdelete='否' order by id desc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        ld.Text = string.Empty;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseID = DropDownList1.SelectedValue.Trim();
        string value = TextBox1.Text.Trim();
        string sql = "";
        if (DropDownList1.SelectedValue != "kehuname")
        {
            //sql = "select *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,(select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname from baojiabiao where " + ChooseID + " like '%" + value + "%' and  ((responser = '" + Session["Username"].ToString() + "') or (responser in (select name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "'))) order by id desc";
            sql = @"select *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,
                    (select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname  from baojiabiao
                    where TiJiaoBiaozhi = '否' and " + shwhere + " and " + ChooseID + " like '%" + value + "%' and isdelete='否' order by id desc";
        }
        else
        {
            // sql = " select *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,(select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname from baojiabiao  where kehuid in (select kehuid from customer where customname like '%" + value + "%')  and((responser = '" + Session["Username"].ToString() + "') or(responser in (select name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "'))) order by id desc";
            sql = @"select *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,
                    (select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname  from baojiabiao
                    where TiJiaoBiaozhi = '否' and " + shwhere + " and kehuid in (select kehuid from customer where customname like '%" + value + "%') and isdelete='否' order by id desc";
        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
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
            LinkButton LinkBtn_DetailInfo2 = (LinkButton)e.Row.FindControl("LinkButton5");
            if (e.Row.Cells[7].Text == "是")
            {
                LinkBtn_DetailInfo2.Enabled = false;
                LinkBtn_DetailInfo2.ForeColor = Color.Black;
                LinkBtn_DetailInfo2.Text = "已提交";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("id", _i.ToString());
                e.Row.Attributes.Add("onKeyDown", "SelectRow();");
                //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");


                e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
                _i++;
            }
            e.Row.Cells[2].Text = SubStr(e.Row.Cells[2].Text, 12);
            e.Row.Cells[3].Text = SubStr(e.Row.Cells[3].Text, 10);
            string coupon = e.Row.Cells[18].Text;
            if (coupon == "0.00" || coupon == "&nbsp;" || coupon == "NULL" || coupon == "null")
            {
                e.Row.Cells[4].Text = e.Row.Cells[17].Text;
            }
            else
            {
                e.Row.Cells[4].Text = coupon;
            }

            //外包比列
            string epiboly_Price = e.Row.Cells[19].Text.ToString();
            if (epiboly_Price == "&nbsp;" || string.IsNullOrEmpty(epiboly_Price) || epiboly_Price == "0.00")
            {
                e.Row.Cells[10].Text = "0%";
            }
            else
            {
                string price = e.Row.Cells[4].Text;
                if (!string.IsNullOrEmpty(price) && price != "0.00" && price != "&nbsp;")
                {
                    decimal waibao = Convert.ToDecimal(epiboly_Price) / Convert.ToDecimal(price);
                    e.Row.Cells[10].Text = (waibao * 100).ToString("f2") + "%";
                }
            }

            //扩展费
            if (e.Row.Cells[12].Text.ToLower() == "null" || string.IsNullOrEmpty(e.Row.Cells[12].Text) || e.Row.Cells[12].Text == "&nbsp;")
            {
                e.Row.Cells[12].Text = "0";
            }

        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string baojiaid = e.CommandArgument.ToString();
        GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        int index = gvrow.RowIndex;
        if (e.CommandName == "cancel1")
        {
            //如果报价没有联系人则不能提交
            bool bbaojialink = IsBaoJiaLink(baojiaid);
            if (bbaojialink == true)
            {
                string sql3 = "";
                string sql_approval = "";
                string sql_baojiaobiao = "";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                //查看当前的账户的职位
                string shenfen = "";
                string sqlshenfen = "select dutyname from userinfo where username='" + Session["UserName"].ToString() + "'";
                SqlCommand cmdshenfen = new SqlCommand(sqlshenfen, con);
                SqlDataReader drshenfen = cmdshenfen.ExecuteReader();
                if (drshenfen.Read())
                {
                    shenfen = drshenfen["dutyname"].ToString();
                }
                drshenfen.Close();

                decimal discount = Convert.ToDecimal(GridView1.Rows[index].Cells[9].Text);
                string waibao = GridView1.Rows[index].Cells[10].Text.ToString();
                decimal b = 0m;
                if (string.IsNullOrEmpty(waibao) || waibao == "0%")
                {

                }
                else
                {
                    string zhi = waibao.Substring(0, waibao.Length - 1);
                    b = Convert.ToDecimal(zhi) / 100;
                }
                if (discount >= (decimal)0.8 && b <= (decimal)0.8 && (this.GridView1.Rows[index].Cells[12].Text == "0" || this.GridView1.Rows[index].Cells[12].Text == "0.00"))
                {
                    sql_approval = string.Format(@"insert into  [dbo].[Approval]([bianhao],[leibie],[result],[yijian],[fillname],[filltime])values('{0}','{1}','{2}','{3}','{4}','{5}')",
                                                baojiaid, shenfen, "二级通过", "", Session["UserName"].ToString(), DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    sql_baojiaobiao = string.Format("update BaoJiaBiao set tijiaobiaozhi='是',other='other',tijiaotime='{0}',filltime='{0}', weituo='',shenpibiaozhi ='二级通过' where baojiaid='{1}'", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), baojiaid);
                    SqlCommand myComm = new SqlCommand();
                    SqlTransaction myTran;
                    myTran = con.BeginTransaction();
                    try
                    {
                        myComm.Connection = con;
                        myComm.Transaction = myTran;
                        myComm.CommandText = sql_approval;
                        myComm.ExecuteNonQuery();//更新数据
                        myComm.CommandText = sql_baojiaobiao;
                        myComm.ExecuteNonQuery();
                        //****************
                        //提交事务
                        myTran.Commit();
                    }
                    catch (Exception err)
                    {
                        myTran.Rollback();
                        throw new ApplicationException("事务操作出错，系统信息：" + err.Message);
                    }
                }
                else
                {
                    sql3 = "update baojiabiao set tijiaobiaozhi='是',other='other',tijiaotime='" + DateTime.Now + "' where  baojiaid ='" + baojiaid + "'";
                    SqlCommand cmd = new SqlCommand(sql3, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                con.Dispose();
                ld.Text = "<script>alert('提交成功!')</script>";
                Bind();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('未填写联系人不能提交报价')</script>");
            }
        }
        else if (e.CommandName == "cancel2")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sqlan = "select * from anjianxinxi2 where baojiaid='" + baojiaid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlan, con);
            SqlCommand coman = new SqlCommand(sqlan, con);
            SqlDataReader dran = coman.ExecuteReader();
            if (dran.Read())
            {
                ld.Text = "<script>alert('已填单的报价不能删除!');</script>";
            }
            else
            {
                dran.Close();
                //查询这条报价是否为本人创建的
                string sql_my = "select BaoJiaId from BaoJiaBiao where BaoJiaId='" + baojiaid + "' and responser='" + Session["Username"].ToString() + "'";
                SqlCommand cmd_my = new SqlCommand(sql_my, con);
                SqlDataReader dr_my = cmd_my.ExecuteReader();
                if (dr_my.Read())
                {
                    dr_my.Close();
                    string sql3 = "update BaoJiaBiao set isdelete='是' where BaoJiaId='" + baojiaid + "'";
                    SqlCommand cmd = new SqlCommand(sql3, con);
                    cmd.ExecuteNonQuery();
                    string sql4 = "delete from BaoJiaChanPing where baojiaid='" + baojiaid + "'";
                    SqlCommand cmd4 = new SqlCommand(sql4, con);
                    cmd4.ExecuteNonQuery();
                    string sql5 = "delete from BaoJiaCPXiangMu where baojiaid='" + baojiaid + "'";
                    SqlCommand cmd5 = new SqlCommand(sql5, con);
                    cmd5.ExecuteNonQuery();
                    string sql_Clause = "delete from Clause where baojiaid='" + baojiaid + "'";
                    SqlCommand cmd_clause = new SqlCommand(sql_Clause, con);
                    string sql_Link = "delete BaoJiaLink where baojiaid='" + baojiaid + "'";
                    SqlCommand cmd_link = new SqlCommand(sql_Link, con);
                    cmd_link.ExecuteNonQuery();
                    con.Close();
                    Bind();
                }
                else
                {
                    dr_my.Close();
                    ld.Text = "<script>alert('不能删除其他人的报价!');</script>";
                }
            }
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

    /// <summary>
    /// 判断该报价是否有联系人
    /// </summary>
    /// <param name="baojiaid"></param>
    /// <returns></returns>
    public bool IsBaoJiaLink(string baojiaid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from BaoJiaLink where baojiaid='" + baojiaid + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                return true;
            }
            else
            {
                dr.Close();
                return false;
            }
        }
    }
}