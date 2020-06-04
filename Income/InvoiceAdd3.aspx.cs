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
using System.Web.Services;
using Common;
using System.IO;
using System.Text;
using System.Drawing;

public partial class Income_InvoiceAdd3 : System.Web.UI.Page
{
    protected string ran = "";
    protected string kehuid = "";
    protected string baojiaid = "";
    protected string shoufeiid = "";

    protected string hesuan = "";
    protected string bianhao = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        ran = Request.QueryString["invoiceid"].ToString();

        //if (Request.QueryString["bianhao"] != "")
        //{
        //    bianhao = Request.QueryString["bianhao"].ToString();
        //}

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();



        string sql2 = "select inid,hesuanbiaozhi,kehuid from Invoice where inid='" + ran + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            kehuid = dr2["kehuid"].ToString();
            hesuan = dr2["hesuanbiaozhi"].ToString();
        }
        dr2.Close();


        string sql211 = "select * from cashin2 where dengji2='" + ran + "'";
        SqlCommand cmd211 = new SqlCommand(sql211, con);
        SqlDataReader dr211 = cmd211.ExecuteReader();
        if (dr211.Read())
        {
            Button1.Visible = false;
            Button3.Enabled = false;
            Button3.Text = "该收费单已到账，不允许追加费用";
        }
        dr211.Close();



      


        string sql = "  select * from Anjianxinxi2 where bianhaotwo='" + ran + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            baojiaid = dr["baojiaid"].ToString();
            kehuid = dr["kehuid"].ToString();
        }
        con.Close();

        if (Session["UserName"] == null)
        {
            Response.Write("<script>alert('请先登录!');window.location.href='../Login.aspx'</script>");
        }
        else
        {


            if (!IsPostBack)
            {
                Bind();
                Bind2();
                GridView7.DataSource = table1();
                GridView7.DataBind();

                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con1.Open();



                string sql21 = "select beizhu from Invoice where inid='" + ran + "'";
                SqlCommand cmd21 = new SqlCommand(sql21, con1);
                SqlDataReader dr21 = cmd21.ExecuteReader();
                if (dr21.Read())
                {
                    TextBox2.Text = dr21["beizhu"].ToString();

                }
                dr21.Close();
                con1.Close();


            }




        }
    }

    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CeShiFeiKf  where kehuid='" + kehuid + "' and (shoufeibianhao='" + ran + "') order by filltime desc";
        //string sql = "select * from studentInfo";




        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);



        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();


        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");



            string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();



            string sqlx = "select * from CeShiFeiKf where id='" + sid + "' and shoufeibianhao='" + ran + "'";
            SqlCommand cmdx = new SqlCommand(sqlx, con);
            SqlDataReader drx = cmdx.ExecuteReader();
            if (drx.Read())
            {
                hzf.Checked = true;
                drx.Close();

                //string sql2 = "insert into CheckHeSuan4 values('CeShiFei','" + sid + "','" + ran + "')";
                //SqlCommand cmd2 = new SqlCommand(sql2, con);
                //cmd2.ExecuteNonQuery();

            }
            else
            {
                //hzf.Checked = false;
            }

            drx.Close();


        }
     

        string sql21 = "select * from CustomerLinkMan where  customerid='" + kehuid + "' ";
        SqlDataAdapter ad21 = new SqlDataAdapter(sql21, con);
        DataSet ds21 = new DataSet();
        ad21.Fill(ds21);
        DropDownList2.DataSource = ds21.Tables[0];
        DropDownList2.DataTextField = "name";
        DropDownList2.DataValueField = "name";
        DropDownList2.DataBind();

        string linkman = "";


        string sqllink = " select * from CustomerLinkMan where id=(select top 1 linkid from baojialink where baojiaid='" + baojiaid + "')";
        SqlCommand cmdlink = new SqlCommand(sqllink, con);
        SqlDataReader drlink = cmdlink.ExecuteReader();
        if (drlink.Read())
        {
            linkman = drlink["name"].ToString();
        }
        drlink.Close();
        DropDownList2.SelectedValue = linkman;

        con.Close();
        con.Dispose();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {






    }

    public void Bind2()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        //string sql = "select * from invoice where inid='" + ran + "'";


        string sql = "select sum(feiyong) as zongjia from CeShiFeiKf where shoufeibianhao='" + ran + "'";


        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (dr["zongjia"] == DBNull.Value)
            {
                TextBox1.Text = "0.00";
                Label1.Text = "0.00";
            }
            else
            {
                TextBox1.Text = Math.Round(Convert.ToDecimal(dr["zongjia"]), 2).ToString();
                Label1.Text = Math.Round(Convert.ToDecimal(dr["zongjia"]), 2).ToString();
            }
        }

        con.Close();
        con.Dispose();
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {




            //CheckBox LinkBtn_DetailInfo2 = (CheckBox)e.Row.FindControl("CheckBox1");
            //if (e.Row.Cells[9].Text == ran)
            //{
            //    LinkBtn_DetailInfo2.Enabled = false;
            //    LinkBtn_DetailInfo2.ForeColor = Color.Black;

            //}
            //else
            //{
            //    LinkBtn_DetailInfo2.Enabled = true;

            //}






        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {


    }


    protected void Button1_Click(object sender, EventArgs e)
    {


        decimal jine = 0;

        foreach (GridViewRow gr in GridView1.Rows)
        {


            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            if (hzf.Checked)
            {
                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();

                jine = jine + Convert.ToDecimal(gr.Cells[2].Text);

            }



        }


        Label1.Text = Math.Round(Convert.ToDecimal(jine), 2).ToString();
        TextBox1.Text = Math.Round(Convert.ToDecimal(jine), 2).ToString();


    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con5.Open();






        //string sql2 = "update CeShiFeiKf set shoufeibianhao='' where shoufeibianhao='" + ran + "'";
        //SqlCommand cmd2 = new SqlCommand(sql2, con5);
        //cmd2.ExecuteNonQuery();









        foreach (GridViewRow gr in GridView1.Rows)
        {


            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            if (hzf.Checked)
            {
                //string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();


                //string sqlshoufei = "update CeShiFeiKf set shoufeibianhao='" + ran + "' where id='" + sid + "'";
                //SqlCommand cmdshoufei = new SqlCommand(sqlshoufei, con5);
                //cmdshoufei.ExecuteNonQuery();







            }
            else
            {
                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();


                string sqlshoufei = "update CeShiFeiKf set shoufeibianhao='' where id='" + sid + "'";
                SqlCommand cmdshoufei = new SqlCommand(sqlshoufei, con5);
                cmdshoufei.ExecuteNonQuery();
            }







        }


        string sql4 = "update Invoice set beizhu='" + TextBox2.Text.Trim() + "', name='" + DropDownList2.SelectedValue + "', zongjia='" + Convert.ToDecimal(TextBox1.Text) + "',feiyong='" + Convert.ToDecimal(TextBox1.Text) + "' where inid='" + ran + "'";
        SqlCommand cmd4 = new SqlCommand(sql4, con5);
        cmd4.ExecuteNonQuery();



        con5.Close();
        con5.Dispose();
        Response.Write("<script>alert('保存成功');window.location.href='../Case/CeShiFeiKfM.aspx'</script>");

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected DataTable table1()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        DataTable dt = new DataTable();
        dt.Columns.Add("测试项目", Type.GetType("System.String"));
        dt.Columns.Add("小计金额", Type.GetType(" System.Decimal"));



        dt.Columns.Add("折扣", Type.GetType("System.Decimal"));
        dt.Columns.Add("折后价", Type.GetType("System.Decimal"));

        string sql = "select  sum(convert(decimal,feiyong)) as feiyong  from CeShiFeiKf  where bianhaotwo='" + ran + "'  group by bianhaotwo order by bianhaotwo";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        int a = ds.Tables[0].Rows.Count;

        for (int i = 0; i <= a - 1; i++)
        {








            DataRow dr;
            dr = dt.NewRow();
            dr["测试项目"] = ds.Tables[0].Rows[i]["feiyong"].ToString();
            dr["小计金额"] = ds.Tables[0].Rows[i]["feiyong"].ToString();


            //  dr["当月目标进度"] = ds.Tables[0].Rows[0]["mubiao"].ToString();
            dr["折扣"] = ds.Tables[0].Rows[i]["feiyong"].ToString();

            dr["折后价"] = Convert.ToDecimal(dr["折扣"]) * Convert.ToDecimal(dr["小计金额"]);


            dt.Rows.Add(dr);
            //}




        }
        con.Close();









        return dt;
    }





    protected void GridView7_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


        string uuname1 = Server.HtmlEncode(((TextBox)this.GridView7.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());

        string uuname2 = Server.HtmlEncode(((TextBox)this.GridView7.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());

        string uuname3 = Server.HtmlEncode(((TextBox)this.GridView7.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());

        string uuname4 = Server.HtmlEncode(((TextBox)this.GridView7.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        decimal zk = 1;
        if (uuname3 != "")
        {
            zk = Convert.ToDecimal(uuname3);
        }


        decimal j = Convert.ToDecimal(uuname4) * zk;
        string sql2 = "update emcshiji set zhekou3='" + zk + "' where ceshiname='" + uuname1 + "' and biaoji='" + ran + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        cmd2.ExecuteNonQuery();


        con.Close();




        GridView7.EditIndex = -1;


        GridView7.DataSource = table1();
        GridView7.DataBind();





        con.Close();


    }
    protected void GridView7_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView7.EditIndex = e.NewEditIndex;
        GridView7.DataSource = table1();
        GridView7.DataBind();
    }
    protected void GridView7_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView7.EditIndex = -1;
        GridView7.DataSource = table1();
        GridView7.DataBind();
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {


        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        bool ch = ((CheckBox)sender).Checked;



        if (ch)
        {

            GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;

            string tableid = this.GridView1.Rows[gvr.RowIndex].Cells[1].Text.ToString();


            string sql = "select * from CheckHeSuan4 where tableid='" + tableid + "' and bianhao='" + ran + "' and tablename='CeShiFei'";
            SqlCommand cmd = new SqlCommand(sql, con1);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
            }
            else
            {
                dr.Close();
                string sql2 = "insert into CheckHeSuan4 values('CeShiFei','" + tableid + "','" + ran + "')";
                SqlCommand cmd2 = new SqlCommand(sql2, con1);
                cmd2.ExecuteNonQuery();
            }
        }

        else
        {

            GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;


            string tableid = this.GridView1.Rows[gvr.RowIndex].Cells[1].Text.ToString();


            string sql = "select * from CheckHeSuan4 where tableid='" + tableid + "' and bianhao='" + ran + "'  and tablename='CeShiFei'";
            SqlCommand cmd = new SqlCommand(sql, con1);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                string sql2 = "delete from  CheckHeSuan4 where tableid='" + tableid + "' and bianhao='" + ran + "'  and tablename='CeShiFei'";
                SqlCommand cmd2 = new SqlCommand(sql2, con1);
                cmd2.ExecuteNonQuery();
            }
            else
            {
                dr.Close();
            }
        }

        con1.Close();
        TextBox1.Text = hesuan2();
        Label1.Text = hesuan2();


    }

    protected string hesuan2()
    {
        string feiyong = "0";
        decimal a = 0;
        decimal b = 0;
        decimal c = 0;
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string sql4 = "select (select sum(feiyong) from CeShiFeiKf where id=CheckHeSuan4.tableid) as dd from CheckHeSuan4 where bianhao='" + ran + "' ";
        SqlCommand cmd4 = new SqlCommand(sql4, con1);
        SqlDataReader dr4 = cmd4.ExecuteReader();
        while (dr4.Read())
        {
            if (dr4["dd"] == DBNull.Value)
            {

            }
            else
            {
                //baojiazong_txt.Text = Convert.ToDecimal(drsum["total"]).ToString().Trim();
                a = a + Convert.ToDecimal(dr4["dd"]);
            }
        }
        dr4.Close();






        c = a + b;
        feiyong = Math.Round(Convert.ToDecimal(c), 2).ToString();
        con1.Close();
        return feiyong;

    }
}