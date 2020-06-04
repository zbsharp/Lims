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
using System.Collections.Generic;
using System.Linq;

public partial class Customer_CustomerAdd : System.Web.UI.Page
{
    #region 初始绑定

    protected string kehuid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_dutyname = string.Format("select dutyname,departmentname from UserInfo where UserName='{0}'", Session["UserName"].ToString());
            SqlCommand cmdstate = new SqlCommand(sql_dutyname, con);
            SqlDataReader dr = cmdstate.ExecuteReader();
            string dutyname = "";
            if (dr.Read())
            {
                dutyname = dr["dutyname"].ToString();
            }

            if (dutyname=="销售助理")
            {
                Button3.Visible = false;
            }

        }
    }


    //认证类型 AV
    private void Bindleixing()
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        //con.Open();
        //string sql = "select * from SendUnit";
        //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //ad.Fill(ds);
        //CheckBoxList1.DataSource = ds.Tables[0];
        //CheckBoxList1.DataTextField = "name";
        //CheckBoxList1.DataValueField = "name"; ;
        //CheckBoxList1.DataBind();
        //con.Close();
    }

    //信用等级
    protected void BindDengji()
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        //con.Open();
        //string sql = "select * from DengJi order by id asc ";
        //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //ad.Fill(ds);
        //DropDownList1.DataSource = ds.Tables[0];
        //DropDownList1.DataTextField = "dengji";
        //DropDownList1.DataValueField = "dengji";
        //DropDownList1.DataBind();
        //con.Close();
    }

    //客户类型  报价客户
    protected void BindCustomleixing()
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        //con.Open();
        //string sql = "select * from Customleixing order by id asc ";
        //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //ad.Fill(ds);
        //DropDownList4.DataSource = ds.Tables[0];
        //DropDownList4.DataTextField = "name";
        //DropDownList4.DataValueField = "name";
        //DropDownList4.DataBind();
        //con.Close();
    }

    //客户行业  贸易商
    protected void BindCustomhangye()
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        //con.Open();
        //string sql = "select * from Customhangye order by id asc ";
        //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //ad.Fill(ds);
        //DropDownList2.DataSource = ds.Tables[0];
        //DropDownList2.DataTextField = "name";
        //DropDownList2.DataValueField = "name";
        //DropDownList2.DataBind();
        //con.Close();
    }

    //服务类型  认证+EMC
    protected void BindFuwuleixing()
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        //con.Open();
        //string sql = "select * from Fuwuleixing order by id asc ";
        //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //ad.Fill(ds);
        //DropDownList5.DataSource = ds.Tables[0];
        //DropDownList5.DataTextField = "name";
        //DropDownList5.DataValueField = "name";
        //DropDownList5.DataBind();
        //con.Close();
    }

    //来源途径  自行开发
    protected void BindCustomSource()
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        //con.Open();
        //string sql = "select * from CustomSource2 order by id asc ";
        //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //ad.Fill(ds);
        //DropDownList3.DataSource = ds.Tables[0];
        //DropDownList3.DataTextField = "name";
        //DropDownList3.DataValueField = "name";
        //DropDownList3.DataBind();
        //con.Close();
    }

    //绩效计算  
    protected void BindCustomjisuanleixing()
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        //con.Open();
        //string sql = "select * from Customjisuanleixing order by id asc ";
        //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //ad.Fill(ds);
        //DropDownList6.DataSource = ds.Tables[0];
        //DropDownList6.DataTextField = "name";
        //DropDownList6.DataValueField = "name";
        //DropDownList6.DataBind();
        //con.Close();
    }






    #endregion

    #region 保存客户，如果是新客户则1000000，如果是老客户则判断部门1000001/1000002


    protected void Button4_Click(object sender, EventArgs e)
    {


    }

    #endregion

    #region 绑定联系人
    protected void Button1_Click(object sender, EventArgs e)
    {
        //string BindStr = string.Empty;
        //BindStr = "select * from LinkManInformation where kehuid='" + IDStr.ToString() + "'";
        //DB.BindGridView(this.GridView1, BindStr);

        //string BindStr1 = string.Empty;
        //BindStr1 = "select * from teshuyaoqiu where kehuid='" + IDStr.ToString() + "'";
        //DB.BindGridView(this.GridView4, BindStr1);
    }

    #endregion

    #region 删除联系人
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Delete")
        //{
        //    try
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);

        //        string SqlStr = "delete from LinkManInformation where ID=" + ID.ToString();
        //        DB.RunSql(SqlStr);

        //        string BindStr = string.Empty;
        //        BindStr = "select * from LinkManInformation where kehuid='" + IDStr.ToString() + "'";
        //        DB.BindGridView(this.GridView1, BindStr);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //}
    }

    #endregion

    #region 绑定特殊要求
    protected void Button6_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");
    }

    protected void Button6_Click1(object sender, EventArgs e)
    {
        //string BindStr1 = string.Empty;
        //BindStr1 = "select * from teshuyaoqiu where kehuid='" + IDStr.ToString() + "'";
        //DB.BindGridView(this.GridView4, BindStr1);
    }
    #endregion

    #region 如果要求跟进客户则要求分派

    protected void Button5_Click1(object sender, EventArgs e)
    {

        if (TextBox1.Text == "")
        {



            ld.Text = "<script>alert('请填写申请理由');</script>";
        }
        else
        {


            string sql4 = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            try
            {
                con.Open();

                string kehuid1 = "";
                string sql2 = "select kehuid from Customer where customname='" + kehuname.Text.Trim() + "' ";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    kehuid1 = dr2["kehuid"].ToString();
                    sql4 = "update Customered set customlevel='" + Session["UserName"].ToString() + "',requestdate='" + DateTime.Now.ToShortDateString() + "',pubtime2='" + DateTime.Now + "' where customname='" + kehuname.Text.Trim() + "'";

                    string sql5 = "update Customer set customlevel='" + Session["UserName"].ToString() + "',requestdate='" + DateTime.Now.ToShortDateString() + "' where customname='" + kehuname.Text.Trim() + "'";
                    dr2.Close();
                    SqlCommand cmd5 = new SqlCommand(sql5, con);

                    cmd5.ExecuteNonQuery();
                    dr2.Close();
                    SqlCommand com4 = new SqlCommand(sql4, con);
                    com4.ExecuteNonQuery();

                    string sql = "insert into CustomerRequest values('','" + kehuname.Text.Trim() + "','" + TextBox1.Text.Trim() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','否')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ld.Text = "<script>alert('申请提交成功，请等待分派');</script>";

                }
                else
                {
                    ld.Text = "<script>alert('该客户还没有被录入在系统中，请先保存再申请分派');</script>";
                }









            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {

                con.Close();
            }
        }

    }

    #endregion
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string sql1 = "select top 1 Kehuid from Customer where Kehuid not like 'D%' order by id desc";
        SqlDataAdapter adpter = new SqlDataAdapter(sql1, con1);
        DataSet ds = new DataSet();
        adpter.Fill(ds);

        string sql3 = "select customname,kehuid,Responser from Customer where customname= '" + kehuname.Text.Trim() + "' and  Kehuid not like 'D%'";
        SqlCommand cmd = new SqlCommand(sql3, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            string responser = dr["Responser"].ToString();
            dr.Close();
            con1.Close();
            con1.Dispose();
            ld.Text = "<script>alert('此客户已存在系统中,业务员为: " + responser + "');</script>";
        }
        else if (!kehuname.Text.Trim().Contains("业务员"))
        {
            dr.Close();
            //当客户表中没有客户时，获取客户id
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows == null)
            {
                int h4 = 1;
                string h5 = h4.ToString("1000000");
                kehuid = h5;
            }
            else
            {
                string haoma = ds.Tables[0].Rows[0]["kehuid"].ToString();
                string houzhui = haoma.Substring(0, 7);
                int a = Convert.ToInt32(houzhui);
                int b = a + 1;
                kehuid = b.ToString();
            }
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
            string SqlStr = "insert Customer values('" + kehuid + "','" + kehuname.Text.Trim() + "','" + safty1 + "','" + TextBox2.Text.Trim() + "'," + "'" + txt_address_en.Text.Trim() + "'," +
            "'" + TextBox3.Text.Trim() + "'," +
            "'" + Session["UserName"].ToString() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','否','" + DateTime.Now + "','" + DateTime.Now + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + DropDownList3.SelectedValue + "','" + DropDownList5.SelectedValue + "','" +/* DropDownList2.SelectedValue*/' ' + "','','" + Intro.Text.Trim() + "','否','','" + DropDownList6.SelectedValue + "','','','')";
            //DB.RunSql(SqlStr);

            SqlCommand cmdi = new SqlCommand(SqlStr, con1);
            cmdi.ExecuteNonQuery();

            string sql_customerSalse = "insert Customer_Sales (customerid,responser,fillname,filltime)values('" + kehuid + "','" + Session["Username"].ToString() + "','" + Session["Username"].ToString() + "','" + DateTime.Now.ToString() + "')";
            SqlCommand cmd_customerSalse = new SqlCommand(sql_customerSalse, con1);
            cmd_customerSalse.ExecuteNonQuery();

            con1.Close();
            con1.Dispose();
            // ld.Text = "<script>alert('客户添加完成，请继续增加联系人信息！');top.main.yujijun.location.href='../Customer/CustManage.aspx'</script>";
            Label1.Text = kehuid;
            //Response.Redirect("~/Customer/CustomerSee.aspx?kehuid=" + Label1.Text);
            Response.Redirect("~/Customer/CustomerSee.aspx?kehuid=" + Label1.Text.ToString());
        }
        else
        {
            dr.Close();
            con1.Close();
            con1.Dispose();
            ld.Text = "<script>alert('请输入有效的客户名称');</script>";
        }
    }

    protected void kehuname_TextChanged(object sender, EventArgs e)
    {
        string prefixText = kehuname.Text.ToString();
        List<string> suggestions = new List<string>();
        if (prefixText == "公司" || prefixText == "深圳" || prefixText == "有限公司" || prefixText == "有限" || prefixText == "有限公" || prefixText == "深圳市")
        {
        }
        else
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //con.Open();
            //string getcustomersql = "select customname,fillname from customer where customname like '%" + prefixText + "%'";
            //SqlDataAdapter getcustomerad = new SqlDataAdapter(getcustomersql, con);

            //DataSet getcustomerds = new DataSet();
            //getcustomerad.Fill(getcustomerds);
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<ul class=\"kehu\">");
            //for (int i = 0; i < getcustomerds.Tables[0].Rows.Count; i++)
            //{
            //    string customername = getcustomerds.Tables[0].Rows[i]["customname"].ToString();
            //    string fillname = getcustomerds.Tables[0].Rows[i]["fillname"].ToString();
            //    string str = "  客户名称：" + customername + "--录入人：" + fillname + "";
            //    sb.Append("<li>" + str + "</li>");
            //}
            //sb.Append("</ul>");
            //con.Close();
            //Response.Write(sb);
        }
    }
}