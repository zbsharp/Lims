using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Text;

public partial class Case_Test : System.Web.UI.Page
{
    string renwuid = "";
    string id = "";
    string anjianinfoid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        renwuid = Request.QueryString["xiangmuid"].ToString();
        id = Request.QueryString["id"].ToString();
        anjianinfoid = Request.QueryString["anjianinfoid"].ToString();
        if (!IsPostBack)
        {
            Depa();
            Bind();
            BindAnJianInfo2();
            BindXiangmu();
        }
    }

    private void Bind()
    {
        Literal1.Text = "";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from DaiFenTest where renwuid='" + renwuid + "' and bumen='" + txt_depa.Text.Trim() + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }

    /// <summary>
    /// 加载部门和测试员
    /// </summary>
    public void Depa()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = " select bumen from AnJianInFo where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_depa.Text = dr["bumen"].ToString();
            }
            dr.Close();

            string sql_test = "select UserName from UserInfo where departmentid in(select DepartmentId from UserDepa where Name='" + txt_depa.Text + "') and (dutyname='测试员' or dutyname like '工程%')";
            SqlDataAdapter da = new SqlDataAdapter(sql_test, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            drop_Test.DataSource = ds;
            drop_Test.DataTextField = "UserName";
            drop_Test.DataValueField = "UserName";
            drop_Test.DataBind();
        }
    }

    protected void btn_yes_Click(object sender, EventArgs e)
    {
        //判断是否选中了测试项目
        int projectitems = 0;
        foreach (GridViewRow item in gdvprojectitem.Rows)
        {
            CheckBox chk = (CheckBox)item.Cells[5].FindControl("CheckBox1");
            if (chk.Checked)
            {
                projectitems++;
            }
        }

        if (projectitems > 0)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                //判断该测试项目是否经过了前处理
                bool bu = false;//判断是否需前处理
                string sql_bumne = "select * from DepartmentType where department='" + txt_depa.Text + "' and Type='需前处理'";
                SqlCommand cmd_bumen = new SqlCommand(sql_bumne, con);
                SqlDataReader dr_bumen = cmd_bumen.ExecuteReader();
                if (dr_bumen.Read())
                {
                    dr_bumen.Close();
                    string sql_dispose = "select * from [dbo].[pretreatment] where anjianinfoid='" + anjianinfoid + "'";
                    SqlCommand cmd_dispose = new SqlCommand(sql_dispose, con);
                    SqlDataReader dr_dispose = cmd_dispose.ExecuteReader();
                    if (dr_dispose.Read())
                    {
                        dr_dispose.Close();
                    }
                    else
                    {
                        dr_dispose.Close();
                        bu = true;
                    }
                }
                dr_bumen.Close();


                if (bu == false)
                {
                    foreach (GridViewRow item in gdvprojectitem.Rows)
                    {
                        CheckBox chk = (CheckBox)item.Cells[5].FindControl("CheckBox1");
                        if (chk.Checked)
                        {
                            int index = item.RowIndex;
                            string xmid = gdvprojectitem.Rows[index].Cells[0].Text.ToString();
                            string xmname = gdvprojectitem.Rows[index].Cells[2].Text.ToString();
                            string sql = @"insert into [dbo].[DaiFenTest] values('" + renwuid + "','" + xmid + "','" + xmname + "','" + drop_Test.SelectedValue + "','1900-01-01 00:00:00.000','1900-01-01 00:00:00.000','" + Session["Username"].ToString() + "','" + DateTime.Now + "','" + txt_depa.Text + "','')";
                            SqlCommand com = new SqlCommand(sql, con);
                            com.ExecuteNonQuery();
                        }
                    }
                    Bind();
                }
                else
                {
                    Literal1.Text = "<script>alert('请先完成前处理，再分派测试员')</script>";
                }
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请选择项目')</script>");
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //1.已有结论的测试不能删除
        //2.管理员直接删除测试员
        //3.工程经理只能删除自己部门的测试员
        //4.工程师只能删除自己所分配的测试员
        string fillname = GridView1.Rows[e.RowIndex].Cells[5].Text.ToString();
        string jielun = GridView1.Rows[e.RowIndex].Cells[10].Text.ToString();
        string bm = GridView1.Rows[e.RowIndex].Cells[7].Text.ToString();
        string position = Zhiwei();
        //string bumen = Bumen();
        DataTable dt = Department();
        string bumen = dt.Rows[0]["departmentname"].ToString();
        int id = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[0].Text);
        if (jielun != "&nbsp;")
        {
            Literal1.Text = "<script>alert('该测试项目已完成，不能删除')</script>";
            return;
        }
        if (position == "系统管理员")
        {
            Delete(id);
        }
        else if (position == "工程经理")
        {
            if (bumen == bm)
            {
                Delete(id);
            }
            else
            {
                Literal1.Text = "<script>alert('该测试员不是你部门的，不能删除')</script>";
                return;
            }
        }
        else if (position == "测试工程师")
        {
            if (fillname == Session["Username"].ToString())
            {
                Delete(id);
            }
            else
            {
                Literal1.Text = "<script>alert('只能删除自己所分配的测试员')</script>";
                return;
            }
        }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow item in GridView1.Rows)
        {
            if (item.Cells[8].Text == "1900/1/1 0:00:00" || item.Cells[8].Text == "1900-01-01 00:00:00")
            {
                item.Cells[8].Text = string.Empty;
            }
            if (item.Cells[9].Text == "1900/1/1 0:00:00" || item.Cells[9].Text == "1900-01-01 00:00:00")
            {
                item.Cells[9].Text = string.Empty;
            }
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
    private string Bumen()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            string bumen = "";
            con.Open();
            string sql = " select departmentname from UserInfo  where UserName='" + Session["Username"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                bumen = dr["departmentname"].ToString();
            }
            return bumen;
        }
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    private void Delete(int id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "delete DaiFenTest where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            Bind();
        }
    }

    /// <summary>
    /// 查询任务信息
    /// </summary>
    protected void BindAnJianInfo2()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *  from AnJianInfo2  where rwbianhao='" + renwuid + "' order  by id desc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        gdvtaskon.DataSource = ds.Tables[0];
        gdvtaskon.DataBind();
        con.Close();
    }

    /// <summary>
    /// 测试项目
    /// </summary>
    protected void BindXiangmu()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select *  from BaoJiaCPXiangMu  where id in (select xiangmubianhao from anjianxinxi3 where bianhao='" + anjianinfoid + "')";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            gdvprojectitem.DataSource = ds.Tables[0];
            gdvprojectitem.DataBind();

            DataTable dt = Department();
            if ((dt.Rows[0]["dutyname"].ToString().Contains("工程") || dt.Rows[0]["dutyname"].ToString() == "测试员") && dt.Rows[0]["departmentname"].ToString() != "认证部")
            {
                string bumen = dt.Rows[0]["departmentname"].ToString();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)gdvprojectitem.Rows[i].FindControl("CheckBox1");
                    if (bumen != gdvprojectitem.Rows[i].Cells[5].Text)
                    {
                        chk.Enabled = false;
                    }
                }
            }
        }
    }

    private DataTable Department()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select top 1 dutyname,departmentname from UserInfo where UserName='" + Session["Username"].ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }

}