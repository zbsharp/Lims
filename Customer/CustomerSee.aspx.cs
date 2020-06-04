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
using System.Text;
using System.IO;
using Common;
public partial class Customer_CustomerSee : System.Web.UI.Page
{

    protected string CustomerId = "";
    protected string CustomerName = "";
    protected string dt = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.Attributes.Add("style", "table-layout:fixed");
        GridView2.Attributes.Add("style", "table-layout:fixed");

        Label1.Text = Request.QueryString["kehuid"].ToString();
        CustomerId = Request.QueryString["kehuid"].ToString();
        TextBox2.Text = Request.QueryString["kehuid"].ToString();
        dt = DateTime.Now.ToShortDateString();

        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sql1 = "select * from Customer where kehuid='" + CustomerId + "' order by kehuid";
            SqlCommand cmd = new SqlCommand(sql1, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                kehuname.Text = dr["CustomName"].ToString();
                Label2.Text = dr["CustomName"].ToString();
                //drop_yewuleibie.SelectedValue = dr["WeiTuoName"].ToString();


                string safty1 = dr["WeiTuoName"].ToString();
                string[] safty2 = safty1.Split('|');
                foreach (string str in safty2)
                {
                    for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                    {
                        if (this.CheckBoxList1.Items[i].Text == str)
                        {
                            this.CheckBoxList1.Items[i].Selected = true;
                        }
                    }
                }



                TextBox4.Text = dr["Address"].ToString();
                TextBox5.Text = dr["url"].ToString();
                DropDownList5.SelectedValue = dr["CustomType"].ToString();
                DropDownList2.SelectedValue = dr["trade"].ToString();
                DropDownList3.SelectedValue = dr["CustomSouce"].ToString();
                DropDownList1.SelectedValue = dr["xingyongdengji"].ToString();
                DropDownList8.SelectedValue = dr["class"].ToString();
                Intro.Text = dr["Remark"].ToString();

            }
            dr.Close();

            DropDownList8.Enabled = false;
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con);
            SqlDataReader drdutyname = cmdstate.ExecuteReader();
            string dutyname = "";
            string bumen = "";
            if (drdutyname.Read())
            {
                bumen = drdutyname["departmentname"].ToString();
                dutyname = drdutyname["dutyname"].ToString();
            }
            drdutyname.Close();
            if (bumen== "财务部"||dutyname=="销售助理"||dutyname=="总经理"||dutyname=="董事长"||dutyname=="系统管理员")
            {
                DropDownList8.Enabled = true;
                Button1.Visible = true;
            }

            con.Close();
            BindLinkMan();
            BindTrace();
            BindQuo();
            BindMyFill();

        }




    }

    protected void BindMyFill()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CustomerRequest where ( kehuname like '%" + kehuname.Text.Trim() + "%' or kehuid ='" + CustomerId + "') order by id desc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView4.DataSource = ds.Tables[0];
        GridView4.DataBind();
        con.Close();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (linkman.Text.Trim().ToString() == "")
        {
            ld.Text = "<script>alert('联系人姓名不可以为空！');</script>";
            return;
        }
        else
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                if (linkman.Text == "")
                {
                    Label4.Visible = true;
                    Label4.Text = "联系人名称为必填";
                }
                else if (telephone.Text == "" && mobile.Text == "")
                {
                    Label4.Visible = true;
                    Label4.Text = "电话与手机信息必须填其一!";
                }
                else
                {
                    Label4.Visible = false;
                    //fax.Text 传真
                    string sql = "insert into CustomerLinkMan values('" + CustomerId + "','" + linkman.Text + "','" + TextBox6.Text + "','" + telephone.Text + "','" + mobile.Text + "','" + email.Text + "','" + /*fax.Text*/txtQQ.Text.Trim() + "','" + txtWeixin.Text.Trim() + "','" + " " + "','" + TextBox7.Text + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DropDownList12.SelectedValue + "','','','','','')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    BindLinkMan();
                    linkman.Text = "";
                    TextBox6.Text = "";
                    //fax.Text = "";
                    mobile.Text = "";
                    telephone.Text = "";
                    email.Text = "";
                    TextBox7.Text = "";
                    txtQQ.Text = string.Empty;
                    txtWeixin.Text = string.Empty;
                }
            }

            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    protected void BindQuo()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from baojiabiao where kehuid='" + CustomerId + "' and (responser in (select name2 from PersonConfig where name1='" + Session["Username"].ToString() + "') or responser='" + Session["Username"].ToString() + "')";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);


        GridView3.DataSource = ds.Tables[0];


        GridView3.DataBind();
        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void Button4_Click(object sender, EventArgs e)
    {


    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
    
        string safty1 = "";
        for (int i = 1; i < CheckBoxList1.Items.Count + 1; i++)
        {
            if (CheckBoxList1.Items[i - 1].Selected)
            {
                safty1 += CheckBoxList1.Items[i - 1].Text.ToString() + "|";
            }
        }
        if (safty1.Length > 0)
        {
            safty1 = safty1.Substring(0, safty1.LastIndexOf('|'));
        }
        else
        {
            safty1 = "0";
        }
        string sql1 = "";
        string sql2 = "";
        string sqlyy = "select * from anjianxinxi2 where kehuid='" + CustomerId + "'";
        SqlCommand cmdyy = new SqlCommand(sqlyy, con);
        SqlDataReader dryy = cmdyy.ExecuteReader();
        if (dryy.Read())
        {
            dryy.Close();
            sql1 = "update Customer set  weituoname='" + safty1 + "',address='" + TextBox4.Text + "',url='" + TextBox5.Text + "',customtype='" + DropDownList5.SelectedValue + "',trade='" + DropDownList2.SelectedValue + "',customsouce='" + DropDownList3.SelectedValue + "',xingyongdengji='" + DropDownList1.SelectedValue + "',remark='" + Intro.Text + "' where kehuid='" + CustomerId + "'";
            sql2 = "update Customered set  weituoname='" + safty1 + "',address='" + TextBox4.Text + "',url='" + TextBox5.Text + "',customtype='" + DropDownList5.SelectedValue + "',trade='" + DropDownList2.SelectedValue + "',customsouce='" + DropDownList3.SelectedValue + "',xingyongdengji='" + DropDownList1.SelectedValue + "',remark='" + Intro.Text + "' where kehuid='" + CustomerId + "'";
            SqlCommand cmd = new SqlCommand(sql1, con);
            cmd.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.ExecuteNonQuery();
            con.Close();
            ld.Text = "<script>alert('修改成功,但因已开案不能修改客户名称')</script>";
        }
        else
        {
            dryy.Close();
            //string sqlshifoushenhe = "select * from customered where kehuid='" + CustomerId + "'";
            //SqlCommand cmdshifoushenhe = new SqlCommand(sqlshifoushenhe, con);
            //SqlDataReader drshenhe = cmdshifoushenhe.ExecuteReader();
            //if (drshenhe.Read())
            //{
            //    drshenhe.Close();
            //    string sqldd = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='修改名称'";
            //    SqlCommand cmddd = new SqlCommand(sqldd, con);
            //    SqlDataReader drdd = cmddd.ExecuteReader();
            //    if (drdd.Read())
            //    {
            //        drdd.Close();
            //        sql1 = "update Customer set  CustomName='" + kehuname.Text + "',weituoname='" + safty1 + "',address='" + TextBox4.Text + "',url='" + TextBox5.Text + "',customtype='" + DropDownList5.SelectedValue + "',trade='" + DropDownList2.SelectedValue + "',customsouce='" + DropDownList3.SelectedValue + "',xingyongdengji='" + DropDownList1.SelectedValue + "',remark='" + Intro.Text + "' where kehuid='" + CustomerId + "'";
            //        sql2 = "update Customered set  CustomName='" + kehuname.Text + "',weituoname='" + safty1 + "',address='" + TextBox4.Text + "',url='" + TextBox5.Text + "',customtype='" + DropDownList5.SelectedValue + "',trade='" + DropDownList2.SelectedValue + "',customsouce='" + DropDownList3.SelectedValue + "',xingyongdengji='" + DropDownList1.SelectedValue + "',remark='" + Intro.Text + "' where kehuid='" + CustomerId + "'";

            //        SqlCommand cmd = new SqlCommand(sql1, con);
            //        cmd.ExecuteNonQuery();

            //        SqlCommand cmd2 = new SqlCommand(sql2, con);
            //        cmd2.ExecuteNonQuery();

            //        con.Close();
            //        ld.Text = "<script>alert('修改成功')</script>";

            //    }
            //    else
            //    {
            //        drdd.Close();
            //        sql1 = "update Customer set  weituoname='" + safty1 + "',address='" + TextBox4.Text + "',url='" + TextBox5.Text + "',customtype='" + DropDownList5.SelectedValue + "',trade='" + DropDownList2.SelectedValue + "',customsouce='" + DropDownList3.SelectedValue + "',xingyongdengji='" + DropDownList1.SelectedValue + "',remark='" + Intro.Text + "' where kehuid='" + CustomerId + "'";
            //        sql2 = "update Customered set  weituoname='" + safty1 + "',address='" + TextBox4.Text + "',url='" + TextBox5.Text + "',customtype='" + DropDownList5.SelectedValue + "',trade='" + DropDownList2.SelectedValue + "',customsouce='" + DropDownList3.SelectedValue + "',xingyongdengji='" + DropDownList1.SelectedValue + "',remark='" + Intro.Text + "' where kehuid='" + CustomerId + "'";

            //        SqlCommand cmd = new SqlCommand(sql1, con);
            //        cmd.ExecuteNonQuery();

            //        SqlCommand cmd2 = new SqlCommand(sql2, con);
            //        cmd2.ExecuteNonQuery();
            //        con.Close();
            //    }

            //}
            //else
            //{
            //    drshenhe.Close();
            //    sql1 = "update Customer set  CustomName='" + kehuname.Text + "',weituoname='" + safty1 + "',address='" + TextBox4.Text + "',url='" + TextBox5.Text + "',customtype='" + DropDownList5.SelectedValue + "',trade='" + DropDownList2.SelectedValue + "',customsouce='" + DropDownList3.SelectedValue + "',xingyongdengji='" + DropDownList1.SelectedValue + "',remark='" + Intro.Text + "' where kehuid='" + CustomerId + "'";
            //    sql2 = "update Customered set  CustomName='" + kehuname.Text + "',weituoname='" + safty1 + "',address='" + TextBox4.Text + "',url='" + TextBox5.Text + "',customtype='" + DropDownList5.SelectedValue + "',trade='" + DropDownList2.SelectedValue + "',customsouce='" + DropDownList3.SelectedValue + "',xingyongdengji='" + DropDownList1.SelectedValue + "',remark='" + Intro.Text + "' where kehuid='" + CustomerId + "'";
            //    SqlCommand cmd = new SqlCommand(sql1, con);
            //    cmd.ExecuteNonQuery();
            //    SqlCommand cmd2 = new SqlCommand(sql2, con);
            //    cmd2.ExecuteNonQuery();
            //    con.Close();
            //    ld.Text = "<script>alert('修改成功')</script>";
            //}
            string sql3 = "select customname,kehuid from Customer where customname like '%" + kehuname.Text.Trim() + "%'";
            SqlDataAdapter da = new SqlDataAdapter(sql3, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count >= 2)
            {
                ld.Text = "<script>alert('此客户已存在系统中');</script>";
            }
            else if (!kehuname.Text.Trim().Contains("业务员"))
            {
                sql1 = "update Customer set  CustomName='" + kehuname.Text + "',weituoname='" + safty1 + "',address='" + TextBox4.Text + "',url='" + TextBox5.Text + "',customtype='" + DropDownList5.SelectedValue + "',trade='" + DropDownList2.SelectedValue + "',customsouce='" + DropDownList3.SelectedValue + "',xingyongdengji='" + DropDownList1.SelectedValue + "',remark='" + Intro.Text + "' where kehuid='" + CustomerId + "'";
                sql2 = "update Customered set  CustomName='" + kehuname.Text + "',weituoname='" + safty1 + "',address='" + TextBox4.Text + "',url='" + TextBox5.Text + "',customtype='" + DropDownList5.SelectedValue + "',trade='" + DropDownList2.SelectedValue + "',customsouce='" + DropDownList3.SelectedValue + "',xingyongdengji='" + DropDownList1.SelectedValue + "',remark='" + Intro.Text + "' where kehuid='" + CustomerId + "'";
                SqlCommand cmd1 = new SqlCommand(sql1, con);
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.ExecuteNonQuery();
                con.Close();
                ld.Text = "<script>alert('修改成功')</script>";
            }
            else
            {
                ld.Text = "<script>alert('请输入有效的客户名称');</script>";
            }
        }
        con.Close();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindLinkMan();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string sql_see = "select * from BaoJiaLink where linkid in (select id from CustomerLinkMan where id=" + id + ")";
        SqlCommand cmd_see = new SqlCommand(sql_see, con);
        SqlDataReader dr_see = cmd_see.ExecuteReader();
        if (dr_see.Read())
        {
            dr_see.Close();
            //ld.Text = "<scrip type='text/javascript'>alert('该联系人已有开案信息不能删除')</script>";
            //正常情况下在UpdatePanel容器中js返回提示信息会失效
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.GetType(), "提示", "alert(' 该联系人已有报价不能删除!')", true);
        }
        else
        {
            dr_see.Close();
            //string sql = "update  CustomerLinkMan set customerid='admin' where id='" + id + "' and lururen='" + Session["UserName"].ToString() + "'";
            string sql = " update CustomerLinkMan set delete_biaozhi='是', delete_name='" + Session["Username"].ToString() + "' where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            BindLinkMan();
        }
        con.Close();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        BindLinkMan();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string KeyId = GridView1.DataKeys[e.RowIndex].Value.ToString();


        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string uuname2 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
        string uuname3 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
        string uuname4 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
        string uuname5 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text.ToString());
        string uuname6 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[7].Controls[0]).Text.ToString());
        string uuname7 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[8].Controls[0]).Text.ToString());
        string uuname8 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[9].Controls[0]).Text.ToString());
        string uuname9 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[10].Controls[0]).Text.ToString());
        //string uuname9 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[9].Controls[0]).Text.ToString());
        //string sql = "update CustomerLinkMan set name='" + uuname2 + "',department='" + uuname3 + "',rode='" + uuname4 + "',telephone='" + uuname5 + "',mobile='" + uuname6 + "',email='" + uuname7 + "',beizhu='" + uuname8 + "',QQ='"+uuname9' where id='" + KeyId + "'";
        //string sql_see = " select* from Anjianxinxi2 where lianxiren='" + uuname2 + "'";
        //SqlCommand cmd_see = new SqlCommand(sql_see, con1);
        //SqlDataReader dr_see = cmd_see.ExecuteReader();
        //if (dr_see.Read())
        //{
        //    dr_see.Close();
        //    string sql = string.Format("update CustomerLinkMan set department='{1}',rode='{2}',telephone='{3}',mobile='{4}',email='{5}',beizhu='{6}',QQ='{7}',Weixin='{8}' where id='{9}'", uuname3, uuname4, uuname5, uuname6, uuname7, uuname8, uuname9, uuname10, KeyId);
        //    SqlCommand cmd = new SqlCommand(sql, con1);
        //    cmd.ExecuteNonQuery();
        //    con1.Close();
        //    GridView1.EditIndex = -1;
        //    BindLinkMan();
        //    ld.Text = "<script>alert('修改完成！由于该联系人已开案所以姓名不能修改')</script>";
        //}
        //else
        //{
        //dr_see.Close();
        string sql = string.Format("update CustomerLinkMan set department='{0}',rode='{1}',telephone='{2}',mobile='{3}',email='{4}',beizhu='{5}',QQ='{6}',Weixin='{7}' where id='{8}'", uuname2, uuname3, uuname4, uuname5, uuname6, uuname7, uuname8, uuname9, KeyId);
        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        GridView1.EditIndex = -1;
        BindLinkMan();
        //}
    }

    protected void BindLinkMan()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CustomerLinkMan where customerid='" + CustomerId + "' and delete_biaozhi !='是'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
    }

    protected void BindTrace()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CustomerTrace where kehuid='" + CustomerId + "' order by genzongid desc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);


        GridView2.DataSource = ds.Tables[0];


        GridView2.DataBind();
        con.Close();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string sql1 = "update Customer set class='" + DropDownList8.SelectedValue + "' where kehuid='" + CustomerId + "'";
        //string sql2 = "update Customered set class='" + DropDownList8.SelectedValue + "' where kehuid='" + CustomerId + "'";
        SqlCommand cmd = new SqlCommand(sql1, con1);
        int i = cmd.ExecuteNonQuery();
        if (i > 0)
        {
            Label5.Text = "修改成功";
        }
    }
}