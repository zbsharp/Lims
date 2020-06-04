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

public partial class Customer_CustomerAssign1 : System.Web.UI.Page
{
    #region 初始绑定
    protected DateTime FD;
    protected DateTime TD = DateTime.Now;
    protected string[] ch1 = new string[40];
    protected string[] ch2 = new string[40];
    protected string[] ch3 = new string[40];
    protected string departmentid = "";
    protected string departmentname = "";
    protected void Page_Load(object sender, EventArgs e)
    {





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
        string sql;


        sql = "select *   from Customer where customname !='' and (customname  like('%" + TextBox1.Text + "%') or responser like('%" + TextBox1.Text + "%'))  and (customlevel !='' or b='')  order by filltime desc";




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

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e) //经理可删除客户
    {
        if (Session["jiaose"].ToString() == "1" || Session["jiaose"].ToString() == "2")
        {
            try
            {
                string sid = GridView1.DataKeys[e.RowIndex].Value.ToString();
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con1.Open();
                string sql1 = "delete from Customer where kehuid=" + sid + "";
                SqlCommand com1 = new SqlCommand(sql1, con1);
                com1.ExecuteNonQuery();
                string sql2 = "delete from Customerfenpai where kehuid=" + sid + "";
                SqlCommand com2 = new SqlCommand(sql2, con1);
                com2.ExecuteNonQuery();

                con1.Close();
                //string WorkInfo = "删除了" + sid + "客户";
                //SqlInsert ql = new SqlInsert();
                //ql.InsertWorkLog(Session["UserName"].ToString(), DateTime.Now.ToString(), WorkInfo);
                Bind();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

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



    #endregion

    #region 分派业务
    protected void Button1_Click1(object sender, EventArgs e)
    {




        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        try
        {


            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");

                if (hzf.Checked)
                {

                    if (DropDownList3.SelectedValue != "")
                    {
                        string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
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

                        SqlCommand cmdb = new SqlCommand(sql, con);
                        cmdb.ExecuteNonQuery();




                        string sqlcf = "select * from Customered where kehuid='" + sid + "' and responser='" + DropDownList3.SelectedValue + "'";
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
                                string sqlinsert = " insert into Customered  select * from Customer  where kehuid='" + sid + "'";
                                cmdinsert.CommandText = sqlinsert;
                                cmdinsert.ExecuteNonQuery();
                                string sqlinsert1 = "update Customered set id='" + dataName + "',customlevel='' where kehuid='" + sid + "' and id ='" + id + "'";
                                cmdinsert.CommandText = sqlinsert1;
                                cmdinsert.ExecuteNonQuery();
                                string sqlinsert2 = "update Customered set responser ='" + DropDownList3.SelectedValue + "',pubtime2='" + DateTime.Now + "' ,customlevel='' where id='" + dataName + "'";
                                cmdinsert.CommandText = sqlinsert2;
                                cmdinsert.ExecuteNonQuery();
                                trs.Commit();
                                con.Close();
                                //string WorkInfo = "分派了" + sid + "客户给" + DropDownList3.SelectedValue + "业务员";
                                //searchwhere.InsertWorkLog(Session["UserName"].ToString(), DateTime.Now.ToString(), WorkInfo);
                            }
                            catch (SqlException kk)
                            {

                                trs.Rollback();
                            }

                        }



                    }

                    //ld.Text = "<script>alert('客户分派成功')</script>";

                }
                else
                {
                    //ld.Text = "<script>alert('请选择客户')</script>";
                }


            }



            con.Close();




            Bind();
        }
        catch (Exception ex)
        {

            con.Close();
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
        try
        {
            AspNetPager1.Visible = false;
            Bind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    #endregion
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();




        if (e.CommandName == "xiada")
        {

            string kn = "";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string yewu = "";

              
                string id = "";
                string sql = "update Customer set b='是', pubtime2='" + DateTime.Now + "',pubtime3='" + DateTime.Now.AddDays(7) + "',customlevel='' where kehuid='" + sid + "'";
                string sqlrenwu = "insert into renwu values('客户分派','" + DateTime.Now + "','否','业务员','" + sid + "','" + Session["UserName"].ToString() + "','','否','','','')";
                string sqlid = "select id,customlevel from Customer where kehuid='" + sid + "'";
                SqlCommand cmdid = new SqlCommand(sqlid, con);
                SqlDataReader drid = cmdid.ExecuteReader();
                if (drid.Read())
                {
                    id = drid["id"].ToString();
                    yewu = drid["customlevel"].ToString();
                }
                drid.Close();

                SqlCommand cmdb = new SqlCommand(sql, con);
                cmdb.ExecuteNonQuery();




                string sqlcf = "select * from Customered where kehuid='" + sid + "' and responser='" + yewu  + "'";
                SqlCommand cmdcf = new SqlCommand(sqlcf, con);
                SqlDataReader drcf = cmdcf.ExecuteReader();
                if (drcf.Read())
                {
                    kn = drcf["customname"].ToString();
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
                        string sqlinsert = " insert into Customered  select * from Customer  where kehuid='" + sid + "'";
                        cmdinsert.CommandText = sqlinsert;
                        cmdinsert.ExecuteNonQuery();
                        string sqlinsert1 = "update Customered set id='" + dataName + "',customlevel='' where kehuid='" + sid + "' and id ='" + id + "'";
                        cmdinsert.CommandText = sqlinsert1;
                        cmdinsert.ExecuteNonQuery();
                        string sqlinsert2 = "update Customered set responser ='" + yewu + "',pubtime2='" + DateTime.Now + "' ,customlevel='' where id='" + dataName + "'";
                        cmdinsert.CommandText = sqlinsert2;
                        cmdinsert.ExecuteNonQuery();
                        trs.Commit();
                       
                        string WorkInfo = "分派了" + sid + "客户给" + yewu + "业务员";
                        searchwhere.InsertWorkLog(Session["UserName"].ToString(), DateTime.Now.ToString(), WorkInfo);
                    }
                    catch (SqlException kk)
                    {

                        trs.Rollback();
                    }

                }

                string sqlf = "insert into CustomerRequest values('" + sid + "','" + kn + "','" + TextBox2.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','同意跟进')";
                SqlCommand cmdf = new SqlCommand(sqlf, con);
                cmdf.ExecuteNonQuery();

                con.Close();

                TextBox2.Text = "";

                Bind();
          
        }
        else if (e.CommandName == "xiada1")
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sqlf = "insert into CustomerRequest values('" + sid + "','','" + TextBox2.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','拒绝申请')";
            SqlCommand cmdf = new SqlCommand(sqlf, con);
            cmdf.ExecuteNonQuery();


            string sql2 = "update customer set customlevel ='' where kehuid='"+sid+"'";
            SqlCommand cmd2 = new SqlCommand(sql2,con);
            cmd2.ExecuteNonQuery();
            con.Close();
            TextBox2.Text = "";
            Bind();
        }
        else
        {


            Response.Redirect("~/Customer/CustomerSee.aspx?kehuid=" + sid + "");
        }

        

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo where department='" + DropDownList2.SelectedValue + "' order by username asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        DropDownList3.DataSource = ds.Tables[0];
        DropDownList3.DataTextField = "username";
        DropDownList3.DataValueField = "username";
        DropDownList3.DataBind();
        
    }

    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select * from UserDepa";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


        DataSet ds = new DataSet();


        ad.Fill(ds);

        con3.Close();



        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataValueField = "name";
        DropDownList2.DataTextField = "name";
        DropDownList2.DataBind();
        
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
}