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
using Common;
public partial class Customer_CustomerAssigned : System.Web.UI.Page
{
    #region 初始绑定
    protected DateTime FD;
    protected DateTime TD = DateTime.Now;
    protected string departmentid = "";
    protected string departmentname = "";
    protected string[] ch1 = new string[40];
    protected string[] ch2 = new string[40];
    protected string[] ch3 = new string[40];
    protected void Page_PreRender(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"] == null)
        {
            Response.Write("<script>alert('请先登录!');window.location.href='../Login.aspx'</script>");
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='分派客户'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
            if (!IsPostBack)
            {



                Bind();
                BindDep();
            }

        }
        else
        {
            con.Close();
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');top.main.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
    }



    public void Bind()
    {

        string shijian = DropDownList1.SelectedValue;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {

            string sql = ""; ;

            if (txFDate.Value == "" || txTDate.Value == "")
            {

                sql = "select * from  Customered where " + searchwhere.search(Session["UserName"].ToString()) + "  order by pubtime2 desc";
            }
            else
            {
                sql = SeachTwo();
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

        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            con.Close();
            con.Dispose();

        }
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {




    }

    #endregion

    #region 查询条件
    protected string Seachone()
    {
        AspNetPager1.Visible = false;

        string sql = "";

        sql = " select * from  Customered  where   (customname like('%" + TextBox1.Text + "%') or responser like('%" + TextBox1.Text + "%'))";




        return sql;

    }

    protected string SeachTwo()
    {
        string sql = "";

        if (txFDate.Value.Trim() == "")
        {
            FD = DateTime.Now.AddDays(-360);
        }

        else
        {
            FD = Convert.ToDateTime(this.txFDate.Value);
        }
        //DateTime TD;

        if (txTDate.Value.Trim() == "")
        {
            TD = DateTime.Now;
        }

        else
        {
            TD = Convert.ToDateTime(this.txTDate.Value).AddDays(1);
        }



        string tiaojian = DropDownList1.SelectedValue;
        if (DropDownList3.SelectedValue == "分配时间")
        {



            sql = "select *   from " + DropDownList1.SelectedValue + " where  customname  like('%" + TextBox1.Text + "%') and pubtime2 between '" + FD + "' and '" + TD + "' and " + searchwhere.search(Session["UserName"].ToString()) + "";






        }
        else
        {



            sql = "select *   from Customered where kehuid not in( select kehuid from genzongjilu where  time between '" + FD + "' and '" + DateTime.Now + "' ) and " + DropDownList1.SelectedValue + "  like('%" + TextBox1.Text + "%') and where " + searchwhere.search(Session["UserName"].ToString()) + "";






        }
        return sql;

    }
    #endregion

    #region 取消分派
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Share")
        {
            this.form1.Target = "_blank";
            string sid = e.CommandArgument.ToString();
            string kehuid = "";

            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con1.Open();

            try
            {

                string sqlke = "select kehuid from Customered where id='" + sid + "'";
                SqlCommand cmdke = new SqlCommand(sqlke, con1);
                SqlDataReader drke = cmdke.ExecuteReader();
                if (drke.Read())
                {
                    kehuid = drke["kehuid"].ToString();
                }
                drke.Close();


                string sql1 = "delete from Customered where id='" + sid + "'";

                SqlCommand com1 = new SqlCommand(sql1, con1);
                com1.ExecuteNonQuery();


                string sql2 = "select kehuid from Customered where kehuid='" + kehuid + "' ";
                SqlCommand cmd2 = new SqlCommand(sql2, con1);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    dr2.Close();
                }
                else
                {

                    dr2.Close();

                    string sqlup = "update Customer set b='否' where kehuid='" + kehuid + "'";
                    SqlCommand cmdup = new SqlCommand(sqlup, con1);
                    cmdup.ExecuteNonQuery();
                }
                string WorkInfo = "取消了" + kehuid + "客户给";
                searchwhere.InsertWorkLog(Session["UserName"].ToString(), DateTime.Now.ToString(), WorkInfo);

                string sql = "";
                if (this.txFDate.Value == "" || this.txTDate.Value == "")
                {
                    sql = Seachone();
                }

                else
                {

                    sql = SeachTwo();

                }
                SqlDataAdapter da = new SqlDataAdapter(sql, con1);
                DataSet ds = new DataSet();
                da.Fill(ds);


                DataTable dt = ds.Tables[0];

                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                con1.Close();
            }


        }
        else if (e.CommandName == "Share1")
        {

            string sid = e.CommandArgument.ToString();
            Response.Redirect("~/Customer/CustomerSee.aspx?kehuid=" + sid + "");
        }
    }
    #endregion

    #region 分派客户
    protected void Button1_Click1(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            string a1 = "";
            string a2 = "";
            string a3 = "";
            string a4 = "";
            string a5 = "";
            string[] strtemp = new string[5];
            string text11 = Text1.Value.Trim() + ",";
            string[] strtemp1 = text11.Split(',');
            string strrighgs = "";
            for (int i = 0; i < strtemp1.Length - 1; i++)
            {
                if (strtemp1[i] != null)
                {
                    strtemp[i] = strtemp1[i];
                }
                else
                {
                    strtemp[i] = "0";
                }

            }

            for (int hh = 0; hh < strtemp.Length; hh++)
            {
                if (strtemp[hh] == null)
                {
                    strtemp[hh] = "0";
                }
            }



            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");

                if (hzf.Checked)
                {
                    if (DropDownList31.SelectedValue != "")
                    {
                        int sid = Convert.ToInt32(GridView1.DataKeys[gr.RowIndex].Value.ToString());
                        string id = "";


                        string sql = "update Customer set b='是', pubtime2='" + DateTime.Now + "',pubtime3='" + DateTime.Now.AddDays(7) + "',customlevel='' where kehuid='" + sid + "'";

                        string sqlrenwu = "insert into renwu values('客户分派','" + DateTime.Now + "','否','业务员','" + sid + "','" + Session["UserName"].ToString() + "','','否','','','')";


                        string sqlid = "select id from Customer where kehuid='" + sid + "'";
                        SqlCommand cmdid = new SqlCommand(sqlid, con);
                        SqlDataReader drid = cmdid.ExecuteReader();
                        if (drid.Read())
                        {
                            id = drid["id"].ToString();
                        }
                        drid.Close();




                        string sqlcf = "select * from Customered where kehuid='" + sid + "' and responser='" + DropDownList31.SelectedValue + "'";
                        SqlCommand cmdcf = new SqlCommand(sqlcf, con);
                        SqlDataReader drcf = cmdcf.ExecuteReader();
                        if (drcf.Read())
                        {
                            drcf.Close();
                        }
                        else
                        {

                            drcf.Close();

                            SqlTransaction trs = con.BeginTransaction();

                            SqlCommand cmdinsert = new SqlCommand();
                            cmdinsert.Connection = con;
                            cmdinsert.Transaction = trs;

                            try
                            {

                                Random rd = new Random();
                                string rd1 = rd.Next(1000).ToString();
                                string dataName = DateTime.Now.ToString("yyyyMMddhhmmss") + rd1;

                                if (DropDownList4.SelectedValue == "改派")
                                {
                                    string sqlinsert3 = " delete from  Customered  where kehuid='" + sid + "'";
                                    cmdinsert.CommandText = sqlinsert3;
                                    cmdinsert.ExecuteNonQuery();
                                }

                                string sqlinsert = " insert into Customered select * from Customer  where kehuid='" + sid + "'";
                                cmdinsert.CommandText = sqlinsert;
                                cmdinsert.ExecuteNonQuery();


                                string sqlinsert1 = "update Customered set id='" + dataName + "' where kehuid='" + sid + "' and id ='" + id + "'";
                                cmdinsert.CommandText = sqlinsert1;
                                cmdinsert.ExecuteNonQuery();

                                string sqlinsert2 = "update Customered set responser ='" + DropDownList31.SelectedValue + "',pubtime2='" + DateTime.Now + "',customlevel='' where id='" + dataName + "'";
                                cmdinsert.CommandText = sqlinsert2;
                                cmdinsert.ExecuteNonQuery();
                                trs.Commit();

                                string WorkInfo = "分派了" + sid + "客户给" + DropDownList31.SelectedValue + "业务员";
                                searchwhere.InsertWorkLog(Session["UserName"].ToString(), DateTime.Now.ToString(), WorkInfo);
                            }
                            catch (SqlException kk)
                            {

                                trs.Rollback();
                            }


                        }

                        SqlCommand com = new SqlCommand(sql, con);
                        SqlCommand comrenwu = new SqlCommand(sqlrenwu, con);
                        comrenwu.ExecuteNonQuery();
                        int i = com.ExecuteNonQuery();

                    }
                }

            }
            Bind();
            for (int j = 0; j < CheckBoxList7.Items.Count; j++)
            {
                if (CheckBoxList7.Items[j].Selected == true)
                {
                    CheckBoxList7.Items[j].Selected = false;
                }
            }
            // ld.Text ="<script>alert('任务分派成功')</script>";
        }

        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            con.Close();
            con.Dispose();

        }

    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

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
    #endregion

    #region 查询客户
    protected void Button2_Click(object sender, EventArgs e)
    {


        string sql = "";

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {
            sql = Seachone();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            con.Close();
            con.Dispose();

        }


    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)  //查看客户
    {



        int sid = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value.ToString());
        Response.Redirect("~/userwork/user1.aspx?kehuid=" + sid + "");


    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo where department='" + DropDownList2.SelectedValue + "' order by username asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList31.DataSource = ds.Tables[0];
        DropDownList31.DataTextField = "username";
        DropDownList31.DataValueField = "username";
        DropDownList31.DataBind();
        con.Close();
    }

    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select * from UserDepa";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


        DataSet ds = new DataSet();


        ad.Fill(ds);





        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataValueField = "name";
        DropDownList2.DataTextField = "name";
        DropDownList2.DataBind();
        con3.Close();
    }

    #endregion
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
}