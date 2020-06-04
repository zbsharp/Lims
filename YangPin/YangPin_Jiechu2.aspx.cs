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
using DBBLL;
using DBTable;
using System.Drawing;

public partial class YangPin_YangPin_Jiechu2 : System.Web.UI.Page
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
            txTDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            TextBox1.Text = sampleid;
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


            //string sql2 = "select kf from anjianinfo2 where rwbianhao=(select anjianid from YangPin2 where sampleid='" + sampleid + "')";
            //SqlCommand cmd2 = new SqlCommand(sql2, con);
            //SqlDataReader dr2 = cmd2.ExecuteReader();
            //if (dr2.Read())
            //{
            //    TextBox3.Text = dr2["kf"].ToString();
            //}

            //dr2.Close();


            string sql3 = "delete from YangPinChaXun where name='" + Session["UserName"].ToString() + "'";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            cmd3.ExecuteNonQuery();

            con.Close();

            //if (TextBox6.Text == "已还"||TextBox6.Text == "借出")
            //{
            //    Button1.Visible = false;
            //}
            Bind();

        }
    }

    protected void Bind()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select * from UserDepa where departmentid='10' or departmentid='11'  or  departmentid='12' or departmentid='13' or departmentid='14' or departmentid='15' or departmentid='1018'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataValueField = "name";
        DropDownList2.DataTextField = "name";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("", ""));
        con3.Close();
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo where department='" + DropDownList2.SelectedValue + "' order by username desc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList3.DataSource = ds.Tables[0];
        DropDownList3.DataTextField = "username";
        DropDownList3.DataValueField = "username";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("", ""));//

        con.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {


        if (DropDownList3.SelectedValue == "")
        {
            Label8.Text = "请指定借出人";
            Label8.ForeColor = Color.Red;
        }
        else
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();




            string sql10 = "select *,(select name from yangpin2 where sampleid=yangpinchaxun.sid) as spname from YangPinChaXun where  biaozhi='否' and name='" + Session["UserName"].ToString() + "' order by id desc";

            SqlDataAdapter ad10 = new SqlDataAdapter(sql10, con);
            DataSet ds10 = new DataSet();
            ad10.Fill(ds10);
            DataTable dt10 = ds10.Tables[0];
            int qz10 = dt10.Rows.Count;

            for (int i = 0; i < qz10; i++)
            {

                //string sqlc = "select * from YangPin2Detail where sampleid='" + ds10.Tables[0].Rows[i]["sid"].ToString() + "' and state='借出'";
                //SqlCommand cmdc = new SqlCommand(sqlc,con);
                //SqlDataReader drc = cmdc.ExecuteReader();
                //if (drc.Read()) 
                //{
                //    drc.Close(); 
                //}
                //else
                {
                    //drc.Close();
                    string sql = "insert into YangPin2Detail values('" + ds10.Tables[0].Rows[i]["sid"].ToString() + "','" + ds10.Tables[0].Rows[i]["spname"].ToString() + "','" + DropDownList3.SelectedValue + "','" + txTDate.Value + "','" + DropDownList1.SelectedValue + "','" + TextBox5.Text.Trim() + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','','','','','')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();

                    string sqlx = "update YangPin2 set  beizhu2='" + DropDownList3.SelectedValue + "', state='" + DropDownList1.SelectedValue + "' where sampleid='" + ds10.Tables[0].Rows[i]["sid"].ToString() + "'";
                    SqlCommand cmdx = new SqlCommand(sqlx, con);
                    cmdx.ExecuteNonQuery();


                    string sqlx1 = "update YangPinChaXun set biaozhi='是',time='" + txTDate.Value + "',lingyong='" + DropDownList3.SelectedValue + "' where id='" + ds10.Tables[0].Rows[i]["id"].ToString() + "'";
                    SqlCommand cmdx1 = new SqlCommand(sqlx1, con);
                    cmdx1.ExecuteNonQuery();
                }

                //ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('提交成功！');", true);

            }
            con.Close();
            //Button1.Enabled = false;

            //TextBox2.Text = "";
            DropDownList2.SelectedValue = "";
            DropDownList3.SelectedValue = "";
            TextBox1.Focus();
            Bind1();
            Bind2();
            Label8.Text = "";
        }

    }
    protected void Bind1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select top 10 * from YangPin2Detail where state='" + DropDownList1.SelectedValue + "' order by id desc";
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

        string ml = "(select TOP 1 name  from YangPin2Detail where sampleid=yangpinchaxun.sid and state='借出' order by id desc) as dd";

        string sql = "select *," + ml + ",(select model from yangpin2 where sampleid=yangpinchaxun.sid) as model,(select anjianid from yangpin2 where sampleid=yangpinchaxun.sid) as anjianid,(select name from yangpin2 where sampleid=yangpinchaxun.sid) as spname,(select state from yangpin2 where sampleid=yangpinchaxun.sid) as state from YangPinChaXun where  biaozhi='否' and name='" + Session["UserName"].ToString() + "' order by id desc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();

        con.Close();
        con.Dispose();
    }


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");


            MyExcutSql ext = new MyExcutSql();
            e.Row.Cells[5].Text = ext.Eng(e.Row.Cells[4].Text);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //if (TextBox1.Text.Trim().Contains("SN"))
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        if (exist(TextBox1.Text.Trim()))
        {
            if (TextBox1.Text.Contains("-"))
            {
                TextBox1.Text = TextBox1.Text.Substring(0, 10);
            }
            string sql = "insert into YangPinChaXun values('" + TextBox1.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now + "','否','')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            Bind2();
            TextBox1.Text = "";

        }
        else
        {
            string sql = "select * from yangpin2 where sampleid='" + TextBox1.Text.Trim() + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
            }
            else
            {
                Label8.Text = "不存在该样品编号";
                Label8.ForeColor = Color.Red;
            }

            dr.Close();

            string sql2 = "select * from yangpinchaxun where sid='" + TextBox1.Text + "'  and name='" + Session["UserName"].ToString() + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                Label8.Text = "已扫描该样品";
                Label8.ForeColor = Color.Red;
            }
            else
            {

            }
            TextBox1.Text = "";

        }
        con.Close();
        Bind();
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
        string sql2 = "select * from yangpinchaxun where sid='" + sid + "' and name='" + Session["UserName"].ToString() + "'";


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