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

public partial class Quotation_QuotationList : System.Web.UI.Page
{
    protected string shwhere = "";
    private int _i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        shwhere = "(tijiaobiaozhi='是' or tijiaobiaozhi='否' or tijiaobiaozhi='')";
        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now;
            int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            int dayspan = (-1) * weeknow + 1;
            DateTime dt2 = dt.AddMonths(1);
            //本月第一天
            txFDate.Value = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");

            DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).AddHours(23);

            txTDate.Value = lastDay.ToString("yyyy-MM-dd");
            Bind();
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Bind()
    {
        string dutyname = "";//职位
        string dn = "";//部门
        //获取当前登录进来的人的职位和部门
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "";
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con);
            SqlDataReader dr = cmdstate.ExecuteReader();
            if (dr.Read())
            {
                dn = dr["departmentname"].ToString();
                dutyname = dr["dutyname"].ToString();
                dr.Close();
            }
            if (dutyname.Trim() == "系统管理员" || (dutyname.Trim() == "总经理" && dn == "总经办") || (dutyname.Trim() == "董事长" && Session["Username"].ToString() != "蒋娜"))
            {
                sql = @" select top 200 *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,
                        (select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname
                        from baojiabiao where (tijiaobiaozhi = '是' or tijiaobiaozhi = '否' or tijiaobiaozhi = '') and isdelete='否'
                        and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13
                        order by id desc";
            }
            else if (dutyname.Trim() == "客户经理")
            {
                sql = @"select top 200 *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,
                        (select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname
                        from baojiabiao where (tijiaobiaozhi = '是' or tijiaobiaozhi = '否' or tijiaobiaozhi = '') 
                        and (responser in (select name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or responser = '" + Session["Username"].ToString() + "') and isdelete='否' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by id desc";
            }
            else if (dutyname.Trim() == "销售助理" || dutyname.Trim().Contains("客服"))
            {
                sql = @" select top 200 *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,
                        (select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname 
                        from baojiabiao where (tijiaobiaozhi='是' or tijiaobiaozhi='否' or tijiaobiaozhi='') 
                         and (responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "') and isdelete='否'   and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by id desc";
            }
            else if (dutyname.Trim() == "总经理" && (dn == "标源销售部" || dn == "销售龙华部"))
            {
                sql = @"select top 200 *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,
                        (select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname
                        from baojiabiao where (tijiaobiaozhi = '是' or tijiaobiaozhi = '否' or tijiaobiaozhi = '') 
                        and (responser in (select name2 from PersonConfig where name1 = '" + Session["Username"].ToString() + "') or responser = '" + Session["Username"].ToString() + "') and isdelete='否' and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by id desc";
            }
            else
            {
                //业务员和销售大客户部的人
                sql = @" select top 200 *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,
                        (select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname 
                        from baojiabiao where (tijiaobiaozhi='是' or tijiaobiaozhi='否' or tijiaobiaozhi='') 
                         and responser='" + Session["Username"].ToString() + "' and isdelete='否'  and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by id desc";
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
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        AspNetPager1.Visible = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseID = DropDownList1.SelectedValue.Trim();
        string value = TextBox1.Text.Trim();
        string sql = "";
        string wh2 = "";
        string dt = "filltime between '" + Convert.ToDateTime(txFDate.Value) + "' and '" + Convert.ToDateTime(txTDate.Value).AddHours(23) + "' ";
        if (DropDownList2.SelectedValue == "否")
        {
            wh2 = "baojiaid not in (select baojiaid from anjianinfo2)";
        }
        else
        {
            wh2 = "baojiaid  in (select baojiaid from anjianinfo2)";
        }
        //查询条件       
        string sql_where = "";
        if (DropDownList1.SelectedValue == "全部")
        {
            sql_where = "and 1=1";
        }
        else if (DropDownList1.SelectedValue == "bumen")
        {
            sql_where = "and responser in (select username from userinfo where departmentname like '%" + TextBox1.Text + "%')";
        }
        else if (DropDownList1.SelectedValue == "kehuname")
        {
            sql_where = "and KeHuId in (select Kehuid from Customer where CustomName like '%" + TextBox1.Text + "%')";
        }
        else
        {
            //报价编号和业务员
            sql_where = "and " + ChooseID + " like '%" + value + "%'";
        }
        string dutyname = "";//职位
        string dn = "";//部门
        string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
        SqlCommand cmdstate = new SqlCommand(sql_dutyname, con);
        SqlDataReader dr = cmdstate.ExecuteReader();
        if (dr.Read())
        {
            dn = dr["departmentname"].ToString();
            dutyname = dr["dutyname"].ToString();
            dr.Close();
        }
        if (dutyname.Trim() == "系统管理员" || (dutyname.Trim() == "总经理" && dn.Trim() == "总经办") || (dutyname.Trim() == "董事长" && Session["Username"].ToString() != "蒋娜"))
        {
            sql = @" select top 200 *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,
                        (select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname
                        from baojiabiao where (tijiaobiaozhi = '是' or tijiaobiaozhi = '否' or tijiaobiaozhi = '') 
                        and " + dt + " " + sql_where + " and " + wh2 + " and isdelete='否'  and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by id desc";
        }
        else if (dutyname.Trim() == "客户经理" && dn != "销售大客户部")
        {
            sql = @"select top 200 *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,
                        (select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname
                        from baojiabiao where (tijiaobiaozhi = '是' or tijiaobiaozhi = '否' or tijiaobiaozhi = '') 
                        and KeHuId in (select Kehuid from Customer where responser in(select UserName from UserInfo where department = '" + dn + "'))  and " + dt + "  " + sql_where + " and " + wh2 + " and isdelete='否'  and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by id desc";
        }
        else if (dutyname.Trim() == "销售助理" || dutyname.Trim().Contains("客服"))
        {
            sql = @" select top 200 *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,
                        (select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname 
                        from baojiabiao where (tijiaobiaozhi='是' or tijiaobiaozhi='否' or tijiaobiaozhi='') 
                         and (responser in (select marketid from CustomerServer where UserName='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "') and " + dt + " " + sql_where + " and " + wh2 + " and isdelete='否'  and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by id desc";
        }
        else if (dutyname.Trim() == "总经理" && (dn.Trim() == "标源销售部" || dn.Trim() == "销售龙华部"))
        {
            sql = @"select top 200 *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,
                        (select top 1 customname from customer where kehuid = baojiabiao.kehuid) as kehuname
                        from baojiabiao where (tijiaobiaozhi = '是' or tijiaobiaozhi = '否' or tijiaobiaozhi = '') 
                        and KeHuId in (select Kehuid from Customer where responser in(select UserName from UserInfo where department = '" + dn + "'))  and " + dt + "  " + sql_where + " and " + wh2 + " and isdelete='否'  and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%')  and LEN(BaoJiaId)<13 order by id desc";
        }
        else
        {
            //业务员
            sql = @" select top 200 *,(select top 1 name from BaoJiaChanPing where baojiaid=baojiabiao.baojiaid) as name,
                        (select top 1 customname from customer where kehuid =baojiabiao.kehuid) as kehuname 
                        from baojiabiao where (tijiaobiaozhi='是' or tijiaobiaozhi='否' or tijiaobiaozhi='') 
                         and responser='" + Session["Username"].ToString() + "' and " + dt + " " + sql_where + " and " + wh2 + " and isdelete='否'  and (BaoJiaId like 'FY%' or BaoJiaId like 'LH%') and LEN(BaoJiaId)<13 order by id desc";
        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }

    private decimal sumprice = 0m;//总金额
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");
            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;

            string coupon = e.Row.Cells[21].Text;
            if (coupon == "0.00" || coupon == "&nbsp;" || coupon == "NULL" || coupon == "null")
            {
                e.Row.Cells[4].Text = e.Row.Cells[20].Text;
            }
            else
            {
                e.Row.Cells[4].Text = coupon;
            }

            if (e.Row.RowIndex >= 0)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[11].Text.Trim().ToString().Substring(0, 4) == "1900")
                    {
                        e.Row.Cells[11].Text = "";
                    }
                    if (e.Row.Cells[14].Text.Trim().ToString().Substring(0, 4) == "1900")
                    {
                        e.Row.Cells[14].Text = "";
                    }
                }
            }
            //外包比列
            string epiboly_Price = e.Row.Cells[22].Text.ToString();
            if (epiboly_Price == "&nbsp;" || string.IsNullOrEmpty(epiboly_Price) || epiboly_Price == "0.00")
            {
                e.Row.Cells[5].Text = "0%";
            }
            else
            {
                string price = e.Row.Cells[4].Text;
                if (!string.IsNullOrEmpty(price) && price != "0.00" && price != "&nbsp;")
                {
                    decimal waibao = Convert.ToDecimal(epiboly_Price) / Convert.ToDecimal(price);
                    e.Row.Cells[5].Text = (waibao * 100).ToString("f2") + "%";
                }
            }
            ////扩展费
            if (e.Row.Cells[16].Text == "&nbsp;" || string.IsNullOrEmpty(e.Row.Cells[16].Text) || e.Row.Cells[16].Text == null)
            {
                e.Row.Cells[16].Text = "0";
            }

            string sum = e.Row.Cells[4].Text;
            if (string.IsNullOrEmpty(sum) || sum == "0" || sum == "&nbsp;")
            {
                sum = "0";
            }
            sumprice += Convert.ToDecimal(sum);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "总金额";
            e.Row.Cells[4].Text = sumprice.ToString("f2");
            e.Row.Cells[3].ForeColor = Color.Blue;
            e.Row.Cells[4].ForeColor = Color.Blue;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
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
}