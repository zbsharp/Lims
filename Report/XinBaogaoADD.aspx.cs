using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_XinBaogaoADD : System.Web.UI.Page
{
    protected string bianhao = "";
    protected string rwid = "";
    protected string shenqingbianhao = "";
    protected string tijiaobianhao = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //没有指派工程师的任务不能获取报告号，已经中止的任务不能获取报告号
        string state = "";

        bianhao = Request.QueryString["renwuid"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql3 = "select taskid,tijiaobianhao from anjianinfo where taskid='" + bianhao + "'";
        SqlCommand cmd3 = new SqlCommand(sql3, con);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {
            rwid = dr3["taskid"].ToString();
            tijiaobianhao = dr3["tijiaobianhao"].ToString();
        }
        dr3.Close();

        string sqlb = "select state from anjianinfo2 where rwbianhao='" + rwid + "'";
        SqlCommand cmdb = new SqlCommand(sqlb, con);
        SqlDataReader drb = cmdb.ExecuteReader();
        if (drb.Read())
        {
            state = drb["state"].ToString();
        }
        drb.Close();
        con.Close();

        if (rwid == "")
        {
            con.Close();
            Literal1.Text = "<script>alert('该任务没有指定工程部门');window.location.href='../Customer/CustomerManage.aspx'</script>";
            return;
        }
        if (state == "中止")
        {
            Literal1.Text = "<script>alert('该任务已被中止，不能获取报告');</script>";
            return;
        }
        if (!IsPostBack)
        {
            DataTable tb = Department();
            if (tb.Rows[0]["dutyname"].ToString().Contains("工程") || tb.Rows[0]["dutyname"].ToString() == "测试员")
            {
                DropDownList1.SelectedValue = tb.Rows[0]["departmentname"].ToString();
            }
            Bind();
            BindXiangmu();
            ItemBaogao();
        }
    }
    protected void Bind()
    {
        Literal1.Text = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from baogao2 where (rwid='" + bianhao + "' or tjid='" + rwid + "') and tjid !='' and beizhu1='否' order by id asc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
        con.Dispose();
    }
    protected void BindXiangmu()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select *  from BaoJiaCPXiangMu  where id in (select xiangmubianhao from anjianxinxi3 where bianhao='" + tijiaobianhao + "')";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView5.DataSource = ds.Tables[0];
            GridView5.DataBind();

            DataTable dt = Department();
            if ((dt.Rows[0]["dutyname"].ToString().Contains("工程") || dt.Rows[0]["dutyname"].ToString() == "测试员") && dt.Rows[0]["departmentname"].ToString() != "认证部")
            {
                string bumen = dt.Rows[0]["departmentname"].ToString();
                for (int i = 0; i < GridView5.Rows.Count; i++)
                {
                    CheckBox c = (CheckBox)GridView5.Rows[i].FindControl("CheckBox1");
                    if (GridView5.Rows[i].Cells[5].Text != bumen)
                    {
                        c.Enabled = false;
                    }
                }
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (CheckedProject() > 0)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string rand1 = "";
            string sql = "";
            string sqlran = "select top 1 b4 from anjianinfo2 where rwbianhao='" + rwid + "' order by id desc";
            SqlCommand cmdrand = new SqlCommand(sqlran, con);
            SqlDataReader drrand = cmdrand.ExecuteReader();
            if (drrand.Read())
            {
                rand1 = drrand["b4"].ToString();
            }
            drrand.Close();

            string tijianid = "";
            if (drop_y.SelectedValue == "是")
            {
                tijianid = Ybaogao();
            }
            else
            {
                tijianid = TiJian_id2();
            }

            //防止并发、插入的时候判断一下该报告号是否存在于数据库
            string sql_select = "select * from baogao2 where baogaoid='" + tijianid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql_select, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Literal1.Text = "<script>alert('该报告号已存在请重新获取')</script>";
            }
            else
            {
                sql = @"insert into [dbo].[baogao2](tjid,rwid,baogaoid,leibie,dengjiby,beizhu2,fillname,filltime,beizhu1,beizhu3)
                    values('" + rwid + "','" + bianhao + "','" + tijianid + "','" + DropDownList1.SelectedItem.Text + "','" + shenqingbianhao + "','" + rand1 + "','" + Session["username"].ToString() + "','" + DateTime.Now.ToString() + "','否','" + drop_y.SelectedValue + "')";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                Insert_itembaogao(tijianid);
                Bind();
                ItemBaogao();
            }
            con.Close();
        }
        else
        {
            Literal1.Text = "<script>alert('请先选择测试项目')</script>";
        }
    }
    protected string TiJian_id2()
    {
        ////////////////////*************2019-8-17修改
        string laboratory = this.DropDownList1.SelectedItem.Text;
        string type;
        if (laboratory == "安规部" || laboratory == "龙华安规部" || laboratory == "标源安规部")
        {
            type = "S";
        }
        else if (laboratory == "EMC部" || laboratory == "龙华EMC部" || laboratory == "标源EMC部")
        {
            type = "E";
        }
        else if (laboratory == "化学部")
        {
            type = "R";
        }
        else
        {
            type = "B";
        }
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string str = "";
            string year = DateTime.Now.Year.ToString().Substring(2, 2);
            string month = DateTime.Now.Month.ToString().PadLeft(2, '0');
            string sql_count = "select  top 1 baogaoid from baogao2 where hand is null and (beizhu3!='是' or beizhu3 is null) and rwid not like 'D%' order by id desc";
            SqlCommand com = new SqlCommand(sql_count, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                string baogaoid = dr["baogaoid"].ToString();
                dr.Close();
                //判断是否为当前年月
                string b_year = baogaoid.Substring(4, 2);
                string b_month = baogaoid.Substring(6, 2);
                if (b_year == year && b_month == month)
                {
                    string str_bg = baogaoid.Substring(8, 6);
                    int i = Convert.ToInt32(str_bg);
                    i++;
                    str = "BCTC" + b_year + b_month + i.ToString().PadLeft(6, '0') + type;
                    return str;
                }
                else
                {
                    str = "BCTC" + year + month + "000001" + type;
                    return str;
                }
            }
            else
            {
                dr.Close();
                str = "BCTC" + year + month + "000001" + type;
                return str;
            }
        }
    }



    protected void btn_hand_Click(object sender, EventArgs e)
    {
        if (CheckedProject() > 0)
        {
            if (txt_baogaoid.Text.Trim().Length == 15 || string.IsNullOrEmpty(txt_baogaoid.Text.Trim()))
            {
                txt_baogaoid.Text = string.Empty;
                Literal1.Text = "<script>alert('报告号格式不正确请重新输入')</script>";
            }
            else
            {
                string baogaoid = this.txt_baogaoid.Text.Trim();
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
                {
                    con.Open();
                    //判断该任务编号是否已存在
                    string sql_baogaoid = "select * from baogao2  where baogaoid='" + txt_baogaoid.Text.Trim() + "'";
                    SqlCommand com_boagaoid = new SqlCommand(sql_baogaoid, con);
                    SqlDataReader dr_baogaoid = com_boagaoid.ExecuteReader();
                    if (dr_baogaoid.Read())
                    {
                        dr_baogaoid.Close();
                        Literal1.Text = "<script>alert('您输入的报告已存在，请重新输入')</script>";
                    }
                    else
                    {
                        dr_baogaoid.Close();
                        string rand1 = "";
                        string sqlran = "select top 1 b4 from anjianinfo2 where rwbianhao='" + rwid + "' order by id desc";
                        SqlCommand cmdrand = new SqlCommand(sqlran, con);
                        SqlDataReader drrand = cmdrand.ExecuteReader();
                        if (drrand.Read())
                        {
                            rand1 = drrand["b4"].ToString();
                        }
                        drrand.Close();
                        string sql = @"insert into[dbo].[baogao2](tjid,rwid,baogaoid,leibie,dengjiby,beizhu2,fillname,filltime,hand,beizhu1)
                          values('" + rwid + "','" + bianhao + "','" + baogaoid + "','" + DropDownList1.SelectedItem.Text + "','" + shenqingbianhao + "','" + rand1 + "','" + Session["username"].ToString() + "','" + DateTime.Now.ToString() + "','是','否')";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                        Insert_itembaogao(baogaoid);
                        Bind();
                        ItemBaogao();
                    }
                }
            }
        }
        else
        {
            Literal1.Text = "<script>alert('请先选择测试项目')</script>";
        }
    }
    /// <summary>
    /// 已关联报告的项目
    /// </summary>
    private void ItemBaogao()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from ItemBaogao where taskid='" + rwid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView2.DataSource = ds.Tables[0];
            GridView2.DataBind();
        }
    }
    /// <summary>
    /// 验证是否选中了测试项目
    /// </summary>
    /// <returns></returns>
    private int CheckedProject()
    {
        int projects = 0;
        foreach (GridViewRow item in GridView5.Rows)
        {
            CheckBox chk = (CheckBox)item.Cells[6].FindControl("CheckBox1");
            if (chk.Checked)
            {
                projects++;
            }
        }
        return projects;
    }
    /// <summary>
    /// 向报告关联项目表插入数据
    /// </summary>
    /// <param name="baogaoid"></param>
    private void Insert_itembaogao(string baogaoid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            foreach (GridViewRow item in GridView5.Rows)
            {
                CheckBox chk = (CheckBox)item.Cells[6].FindControl("CheckBox1");
                if (chk.Checked)
                {
                    string sql_itembaogao = "insert ItemBaogao values('" + baogaoid + "','" + item.Cells[0].Text.ToString() + "','" + item.Cells[2].Text.ToString() + "','" + rwid + "','" + Session["Username"].ToString() + "','" + DateTime.Now + "')";
                    SqlCommand cmd_itembaogao = new SqlCommand(sql_itembaogao, con);
                    cmd_itembaogao.ExecuteNonQuery();
                }
            }
        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView2.DataKeys[e.RowIndex].Value.ToString();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = " delete ItemBaogao where id='" + id + "'";
            SqlCommand com = new SqlCommand(sql, con);
            com.ExecuteNonQuery();
            ItemBaogao();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)); //此得出的值是表示那行被选中的索引值
        string baogaoid = GridView1.Rows[drv.RowIndex].Cells[2].Text.ToString();
        if (e.CommandName == "delete_baogao")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql2 = "select * from baogao2 where id='" + id + "' and (pizhunby !='')";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                dr2.Close();
                Literal1.Text = "<script>alert('该报告已出草稿、不能删除')</script>";
            }
            else
            {
                dr2.Close();
                string sql = "update baogao2 set beizhu1='是' where id='" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                //删除报告项目表
                string sql_baogao = " delete ItemBaogao where baogaoid='" + baogaoid + "'";
                SqlCommand com_baogao = new SqlCommand(sql_baogao, con);
                com_baogao.ExecuteNonQuery();
                Bind();
                ItemBaogao();
            }
            con.Close();
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

    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
    private string Ybaogao()
    {
        string laboratory = this.DropDownList1.SelectedItem.Text;
        string type;
        if (laboratory == "安规部" || laboratory == "龙华安规部" || laboratory == "标源安规部")
        {
            type = "S";
        }
        else if (laboratory == "EMC部" || laboratory == "龙华EMC部" || laboratory == "标源EMC部")
        {
            type = "E";
        }
        else if (laboratory == "化学部")
        {
            type = "R";
        }
        else
        {
            type = "B";
        }

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select top 1 baogaoid from baogao2 where beizhu3='是' and rwid not like 'D%' order by id desc";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            string baogaoid = "";
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string year1 = year.Substring(2, 2);
            string month1 = month.PadLeft(2, '0');
            string bg = "";
            if (dr.Read())
            {
                baogaoid = dr["baogaoid"].ToString();
                string nian = baogaoid.Substring(5, 2);
                string yue = baogaoid.Substring(7, 2);
                if (year1 == nian && month1 == yue)
                {
                    string str = baogaoid.Substring(9, 6);
                    int num = Convert.ToInt32(str);
                    num++;
                    bg = "BCTCY" + nian + yue + num.ToString().PadLeft(6, '0') + type;
                }
                else
                {
                    bg = "BCTCY" + year1 + month1 + "000001" + type;
                }
            }
            else
            {
                bg = "BCTCY" + year1 + month1 + "000001" + type;
            }
            return bg;
        }
    }
}