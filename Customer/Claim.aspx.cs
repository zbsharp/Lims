using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Customer_Claim : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con);
            SqlDataReader dr = cmdstate.ExecuteReader();
            string dutyname = "";
            string bumen = "";
            if (dr.Read())
            {
                bumen = dr["departmentname"].ToString();
                dutyname = dr["dutyname"].ToString();
            }
            dr.Close();
            string sql = "";
            if ((dutyname == "总经理" && bumen == "总经办") || dutyname == "系统管理员" || dutyname == "董事长")
            {
                sql = "select * from shuipiao where ((queren != '确认' and queren != '半确认')or queren is null) and (beizhu2 !='是' or beizhu2 is null) order by id desc";
            }
            else
            {
                sql = @"select * from shuipiao where fukuanren in (select CustomName from Customer where Responser='" + Session["Username"] + "') or not exists(select* from Customer where CustomName= shuipiao.fukuanren) and ((queren != '确认' and queren != '半确认') or queren is null) and (beizhu2 !='是' or beizhu2 is null) order by id desc";
            }
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
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
        AspNetPager1.Visible = false;
        string value = TextBox1.Text.ToString().Replace('\'', ' ');
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con);
            SqlDataReader dr = cmdstate.ExecuteReader();
            string dutyname = "";
            string bumen = "";
            if (dr.Read())
            {
                bumen = dr["departmentname"].ToString();
                dutyname = dr["dutyname"].ToString();
            }
            dr.Close();
            string sql = "";
            if ((dutyname == "总经理" && bumen == "总经办") || dutyname == "系统管理员" || dutyname == "董事长")
            {
                sql = "select * from shuipiao where fukuanren like '%" + value.Trim() + "%' and ((queren != '确认' and queren != '半确认') or queren is null) and (beizhu2 !='是' or beizhu2 is null) order by id desc";
            }
            else
            {
                sql = @"select * from shuipiao where fukuanren in (select CustomName from Customer where Responser='" + Session["Username"] + "') or not exists(select * from Customer where CustomName= shuipiao.fukuanren) and ((queren != '确认' and queren != '半确认') or queren is null) and (beizhu2 !='是' or beizhu2 is null) and fukuanren like '%" + value.Trim() + "%' order by id desc";
            }
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //string type = e.CommandName.ToString();
        //int index = Convert.ToInt32(e.CommandArgument.ToString());
        //int id = (int)(this.GridView1.DataKeys[index].Value);
        //string fukuanren = this.GridView1.Rows[index].Cells[2].Text;
        //string currency = this.GridView1.Rows[index].Cells[5].Text;

        //if (type == "action")
        //{
            //Response.Redirect("~/Income/daozhangrenling.aspx?id=" + HttpUtility.UrlEncode(id.ToString()) + "&fukuanren=" + HttpUtility.UrlEncode(fukuanren) + "&currency=" + HttpUtility.UrlEncode(currency) + "", true);
            //Server.Transfer("~/Income/daozhangrenling.aspx?id=" + HttpUtility.UrlEncode(id.ToString()) + "&fukuanren=" + HttpUtility.UrlEncode(fukuanren) + "&currency=" + HttpUtility.UrlEncode(currency) + "");
         //}
    }
}