using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Case_Test : System.Web.UI.Page
{
    private string rwbianhao = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        rwbianhao = Request.QueryString["rwbianhao"].ToString();
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
            string sql = "";
            string position = Zhiwei();
            string bumen = BuMen();
            if (position.Trim() == "系统管理员" || position.Trim() == "总经理" || position.Trim() == "董事长")
            {
                sql = "select * from DaiFenTest where renwuid='" + rwbianhao + "'";
            }
            else if (position.Trim() == "工程经理")
            {
                sql = "select * from DaiFenTest where renwuid='" + rwbianhao + "' and bumen='" + bumen + "'";
            }
            else if (position.Trim() == "工程师")
            {
                sql = "select * from DaiFenTest where renwuid='" + rwbianhao + "' and fillname='" + Session["username"].ToString() + "'";
            }
            else
            {
                //测试员
                sql = "select * from DaiFenTest where renwuid='" + rwbianhao + "' and ceshiyuan='" + Session["Username"].ToString() + "'";
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
            //foreach (GridViewRow gr in GridView1.Rows)
            //{
            //    CheckBox hzf = (CheckBox)gr.Cells[11].FindControl("CheckBox1");
            //    if (gr.Cells[9].Text != "是")
            //    {
            //        hzf.Enabled = true;
            //    }
            //    else
            //    {
            //        hzf.Enabled = false;
            //    }
            //}
        }
    }
    /// <summary>
    /// 职位
    /// </summary>
    /// <returns></returns>
    private string Zhiwei()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string position = "";
            con.Open();
            string sql = "select dutyname from UserInfo where UserName='" + Session["Username"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader da = cmd.ExecuteReader();
            if (da.Read())
            {
                position = da["dutyname"].ToString();
            }
            return position;
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        bool ch = ((CheckBox)sender).Checked;
        if (ch)
        {
            GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;
            string actiontime = this.GridView1.Rows[gvr.RowIndex].Cells[7].Text.ToString();
            string endtime = this.GridView1.Rows[gvr.RowIndex].Cells[8].Text.ToString();
            string jielun = this.GridView1.Rows[gvr.RowIndex].Cells[9].Text.ToString();
            if (actiontime == "1900/1/1 0:00:00")
            {
                txFDate.Value = string.Empty;
            }
            else
            {
                txFDate.Value = actiontime;
            }
            if (endtime == "1900/1/1 0:00:00")
            {
                txTDate.Value = string.Empty;
            }
            else
            {
                txTDate.Value = endtime;
            }
            if (jielun == "&nbsp;")
            {
            }
            else
            {
                DropDownList1.SelectedValue = jielun;
            }
        }
    }

    protected void btn_action_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            bool bu = true;//记录用户是否选中行
            con.Open();
            foreach (GridViewRow item in GridView1.Rows)
            {
                CheckBox hzf = (CheckBox)item.Cells[0].FindControl("CheckBox1");
                if (hzf.Checked)
                {
                    bu = false;
                    //判断该任务是否完成
                    string renwuid = this.GridView1.Rows[item.RowIndex].Cells[2].Text.ToString();
                    string sql_baoga = "select * from baogao2 where rwid='" + renwuid + "' and pizhunby='是'";
                    SqlDataAdapter da = new SqlDataAdapter(sql_baoga, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Response.Write("已完成的任务不能再次提交");
                        return;
                    }
                    else if (string.IsNullOrEmpty(txFDate.Value) || string.IsNullOrEmpty(txTDate.Value))
                    {
                        Response.Write("<script>alert('不能存在空值')</script>");
                        return;
                    }
                    else
                    {
                        string id = GridView1.DataKeys[item.RowIndex].Value.ToString();
                        string sql = "update DaiFenTest set actiontime='" + txFDate.Value + "' ,endtime='" + txTDate.Value + "',jielun='" + DropDownList1.SelectedValue + "' where id='" + id + "'";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            if (bu == true)
            {
                Response.Write("<script>alert('请勾选需要提交的测试项目')</script>");
            }
            else
            {
                Bind();
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow item in GridView1.Rows)
        {
            if (item.Cells[7].Text == "1900/1/1 0:00:00")
            {
                item.Cells[7].Text = string.Empty;
            }
            if (item.Cells[8].Text == "1900/1/1 0:00:00")
            {
                item.Cells[8].Text = string.Empty;
            }
            if (item.Cells[9].Text == "&nbsp;")
            {
                item.Cells[9].Text = string.Empty;
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
}