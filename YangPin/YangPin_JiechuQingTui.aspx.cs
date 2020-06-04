﻿using System;
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
using DBBLL;
using DBTable;

public partial class YangPin_YangPin_JiechuQingTui : System.Web.UI.Page
{
    protected string sampleid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        sampleid = "";

        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();

        TextBox1.Focus();
        if (!IsPostBack)
        {
            Bind1();
            //TextBox3.Text = Session["UserName"].ToString();
            TextBox4.Text = DateTime.Now.ToShortDateString();
            TextBox1.Text = sampleid;
            Label3.Text = "封存编号";
            // TextBox3.Text = Session["username"].ToString();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select * from YangPin2 where sampleid='" + sampleid + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                TextBox2.Text = dr["name"].ToString();
                TextBox6.Text = dr["state"].ToString();
            }
            dr.Close();


            string sql2 = "select kf from anjianinfo2 where rwbianhao=(select anjianid from YangPin2 where sampleid='" + sampleid + "')";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                TextBox3.Text = dr2["kf"].ToString();
            }

            dr2.Close();


            string sql3 = "delete from YangPinChaXun where name='" + Session["UserName"].ToString() + "'";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            cmd3.ExecuteNonQuery();

            con.Close();

            //if (TextBox6.Text == "已还"||TextBox6.Text == "借出")
            //{
            //    Button1.Visible = false;
            //}
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql10 = "select *,(select top 1 name from yangpin2 where sampleid=yangpinchaxun.sid) as spname from YangPinChaXun where  biaozhi='否' and name='" + Session["UserName"].ToString() + "' order by id desc";

        SqlDataAdapter ad10 = new SqlDataAdapter(sql10, con);
        DataSet ds10 = new DataSet();
        ad10.Fill(ds10);
        DataTable dt10 = ds10.Tables[0];
        int qz10 = dt10.Rows.Count;

        for (int i = 0; i < qz10; i++)
        {

            int fc = 0;

              string sqlc = "select * from YangPin2Detail where sampleid='" + ds10.Tables[0].Rows[i]["sid"].ToString() + "' and state='封存'";
                SqlCommand cmdc = new SqlCommand(sqlc,con);
                SqlDataReader drc = cmdc.ExecuteReader();
                if (drc.Read())
                {
                    drc.Close();
                    
                }
                else
                {
                    drc.Close();



                    string sqlfc = "select pub_field3 from YangPin2Detail where state='封存' and pub_field3 !='' order by id desc";
                    SqlCommand cmdfc = new SqlCommand(sqlfc,con);
                    SqlDataReader drfc = cmdfc.ExecuteReader();
                    if (drfc.Read())
                    {
                        fc = Convert.ToInt32(drfc["pub_field3"]);
                    }
                    drfc.Close();

                    int fc1 = fc + 1;

                    string sql = "insert into YangPin2Detail values('" + ds10.Tables[0].Rows[i]["sid"].ToString() + "','" + ds10.Tables[0].Rows[i]["spname"].ToString() + "','','" + ds10.Tables[0].Rows[i]["beizhu"].ToString() + "','" + DropDownList1.SelectedValue + "','" + TextBox7.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','','','" + fc1 + "','','')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();

                    string sqlx = "update YangPin2 set state='" + DropDownList1.SelectedValue + "',pub_field1='" + TextBox3.Text + "' where sampleid='" + ds10.Tables[0].Rows[i]["sid"].ToString() + "'";
                    SqlCommand cmdx = new SqlCommand(sqlx, con);
                    cmdx.ExecuteNonQuery();


                    string sqlx1 = "update YangPinChaXun set biaozhi='是' where id='" + ds10.Tables[0].Rows[i]["id"].ToString() + "'";
                    SqlCommand cmdx1 = new SqlCommand(sqlx1, con);
                    cmdx1.ExecuteNonQuery();
                }

            //ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('提交成功！');", true);

        }
        con.Close();
        //Button1.Enabled = false;

        //TextBox2.Text = "";
        //TextBox3.Text = "";
        Bind1();
        Bind2();

        TextBox3.Focus();
    }
    protected void Bind1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select top 10 *,(select top 1 kehuname from yangpin2 where sampleid=YangPin2Detail.sampleid) as kh,(select top 1 model from yangpin2 where sampleid=YangPin2Detail.sampleid) as model,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=(select top 1 anjianid from yangpin2 where sampleid=YangPin2Detail.sampleid)) as sq from YangPin2Detail where state='" + DropDownList1.SelectedValue + "' order by id desc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }
    protected void Bind2()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select *,(select top 1 shenqingbianhao from anjianinfo2 where rwbianhao=(select top 1 anjianid from yangpin2 where sampleid=YangPinChaXun.sid)) as sq,(select top 1 name from YangPin2Detail where sampleid=yangpinchaxun.sid order by id desc) as name1,(select top 1 time from YangPin2Detail where sampleid=yangpinchaxun.sid order by id desc) as beizhu1,(select top 1 name from yangpin2 where sampleid=yangpinchaxun.sid) as spname,(select top 1 state from yangpin2 where sampleid=yangpinchaxun.sid) as state from YangPinChaXun where  biaozhi='否' and name='" + Session["UserName"].ToString() + "' order by id desc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();

        con.Close();
        con.Dispose();
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //if (TextBox1.Text.Trim().Contains("SN"))
        //{
            if (exist(TextBox1.Text.Trim()))
            {
                if (TextBox1.Text.Contains("-"))
                {
                    TextBox1.Text = TextBox1.Text.Substring(0, 10);
                }

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();

                try
                {
                    string sql = "insert into YangPinChaXun values('" + TextBox1.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now + "','否','')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();



                    // ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('提交成功！');", true);
                }
                catch (Exception ex)
                {
                    //ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
                }
                finally
                {
                    con.Close();
                    Bind2();
                    TextBox1.Text = "";
                }
            }
            else
            {
                Label8.Text = "已扫描该样品或该样品不在库中";
                TextBox1.Text = "";
            }
        //}
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView2.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "delete from YangPinChaXun where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();
        Bind2();
    }

    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        Bind2();
    }

    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        Bind2();
    }
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string KeyId = GridView2.DataKeys[e.RowIndex].Value.ToString();

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();


        string uuname3 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
        string uuname4 = Server.HtmlEncode(((TextBox)this.GridView2.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());


        string sql = "update YangPinChaXun set lingyong='" + uuname3 + "',beizhu='" + uuname4 + "' where id='" + KeyId + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        GridView2.EditIndex = -1;
        Bind2();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "封存")
        {
            Label3.Text = "封存编号";
        }
        else
        {
            Label3.Text = "交接人";
        }
    }
    public bool exist(string sid)
    {

        bool A = false;
        bool B = false;






        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();


        string sql = "select * from yangpin2 where sampleid='" + sid + "' ";


        SqlCommand cmd = new SqlCommand(sql, con1);
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            A = true;
        }
        else
        {
            A = false;
        }

        dr.Close();
        string sql2 = "select * from yangpinchaxun where sid='" + sid + "'  and name='" + Session["UserName"].ToString() + "'";

        SqlCommand cmd2 = new SqlCommand(sql2, con1);
        SqlDataReader dr2 = cmd2.ExecuteReader();

        if (dr2.Read())
        {
            B = false;
        }
        else
        {
            B = true;
        }
        bool C = false;

        if (A == true && B == true)
        {
            C = true;
        }


        con1.Close();
        return C;

    }

}