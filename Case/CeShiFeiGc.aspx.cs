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

public partial class Case_CeShiFeiGc : System.Web.UI.Page
{
    protected string bianhao = "";
    protected string shenpi = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        GridView1.Attributes.Add("style", "table-layout:fixed");
        bianhao = Request.QueryString["bianhao"].ToString();
        if (!IsPostBack)
        {
            BindDep();
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();


            string sqlh = "select top 1 baogaoid,pizhunby from baogao2 where tjid=(select rwbianhao from anjianinfo2 where bianhao='" + bianhao + "') order by id desc";
            SqlCommand cmdh = new SqlCommand(sqlh, con);
            SqlDataReader drh = cmdh.ExecuteReader();
            if (drh.Read())
            {
                TextBox8.Text = drh["baogaoid"].ToString();
                
            }
            drh.Close();



            string sql = "select top 1 baogaoid,pizhunby from baogao2 where tjid=(select rwbianhao from anjianinfo2 where bianhao='" + bianhao + "' and (shiyanleibie='CCC' or shiyanleibie='cqc' or shiyanleibie='cb')) order by id desc";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
              
                shenpi = dr["pizhunby"].ToString();
            }
            dr.Close();


            if (shenpi != "")
            {
                con.Close();
                Response.Redirect("~/Print/PrintShangBao.aspx?bianhao=" + bianhao + "&&xianshi=" + DropDownList2.SelectedValue);
            }

            string sql13 = "select top 1 fw from CeShiFei where bianhao='" + bianhao + "' ";

            SqlDataAdapter ad13 = new SqlDataAdapter(sql13, con);
            DataSet ds13 = new DataSet();
            ad13.Fill(ds13);
            DataTable dt13 = ds13.Tables[0];
            if (dt13.Rows.Count > 0)
            {
                TextBox10.Text = dt13.Rows[0]["fw"].ToString();
            }
            else
            {
                TextBox10.Text = "0.00";
            }


            con.Close();


            bind();
        }

    }

    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select distinct name from ShangBaoXiangMu ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList4.DataSource = ds.Tables[0];
        DropDownList4.DataValueField = "name";
        DropDownList4.DataTextField = "name";
        DropDownList4.DataBind();


        string sql2 = "select * from ShangBaoXiangMu where name='" + DropDownList4.SelectedValue + "'  order by wenyuan asc ";
        SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con3);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);
        DropDownList3.DataSource = ds2.Tables[0];
        DropDownList3.DataValueField = "wenyuan";
        DropDownList3.DataTextField = "wenyuan";
        DropDownList3.DataBind();


        con3.Close();
    }

    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ShangBaoXiangMu where name='" + DropDownList4.SelectedValue + "'  order by wenyuan asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList3.DataSource = ds.Tables[0];
        DropDownList3.DataTextField = "wenyuan";
        DropDownList3.DataValueField = "wenyuan";
        DropDownList3.DataBind();
        con.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "insert into CeShiFei values('" + bianhao + "','" + DropDownList1.SelectedValue + "','" + DropDownList3.SelectedValue + "','" +Convert.ToDecimal(TextBox3.Text) + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','','否','" + TextBox9.Text + "','" + DropDownList4.SelectedValue + "','" + TextBox8.Text + "','" + Convert.ToDecimal(TextBox10.Text) + "','','"+DropDownList5.SelectedValue+"','')";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();




        string sql13 = "select sum(xiaoji) as xiaoji from CeShiFei where bianhao='" + bianhao + "' and (fwz !='是' or fwz is null ) and sb ='是' group by bianhao";

        SqlDataAdapter ad13 = new SqlDataAdapter(sql13, con);
        DataSet ds13 = new DataSet();
        ad13.Fill(ds13);
        DataTable dt13 = ds13.Tables[0];
        TextBox10.Text = dt13.Rows[0]["xiaoji"].ToString();



        string sql2 = "update CeShiFei set fw='" + Convert.ToDecimal(TextBox10.Text.Trim()) + "' where  bianhao='" + bianhao + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        cmd2.ExecuteNonQuery();



        con.Close();
        bind();




    }

    protected void bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CeShiFei where bianhao='" + bianhao + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        GridView1.DataSource = dr;
        GridView1.DataBind();

        con.Close();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sqld = "select beizhu2 from ceshifei where id='"+id+"' and beizhu2='是'";
        SqlCommand cmdd = new SqlCommand(sqld,con);
        SqlDataReader drd = cmdd.ExecuteReader();
        if (drd.Read()) 
        {
            drd.Close();
            if (limit1("修改上报费用"))
            {
                string sql = "delete from CeShiFei where id='" + id + "' and fillname='" + Session["UserName"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();


                string sql13 = "select sum(xiaoji) as xiaoji from CeShiFei where bianhao='" + bianhao + "' and (fwz !='是' or fwz is null ) and sb ='是' group by bianhao";

                SqlDataAdapter ad13 = new SqlDataAdapter(sql13, con);
                DataSet ds13 = new DataSet();
                ad13.Fill(ds13);
                DataTable dt13 = ds13.Tables[0];

                if (dt13.Rows.Count > 0)
                {
                    TextBox10.Text = dt13.Rows[0]["xiaoji"].ToString();
                }



                string sql2 = "update CeShiFei set fw='" + Convert.ToDecimal(TextBox10.Text.Trim()) + "' where  bianhao='" + bianhao + "'";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.ExecuteNonQuery();
            }
        }
        else
        {
            drd.Close();
            string sql = "delete from CeShiFei where id='" + id + "' and fillname='" + Session["UserName"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            string sql13 = "select sum(xiaoji) as xiaoji from CeShiFei where bianhao='" + bianhao + "' and (fwz !='是' or fwz is null ) and sb ='是' group by bianhao";

            SqlDataAdapter ad13 = new SqlDataAdapter(sql13, con);
            DataSet ds13 = new DataSet();
            ad13.Fill(ds13);
            DataTable dt13 = ds13.Tables[0];

            if (dt13.Rows.Count > 0)
            {
                TextBox10.Text = dt13.Rows[0]["xiaoji"].ToString();
            }



            string sql2 = "update CeShiFei set fw='" + Convert.ToDecimal(TextBox10.Text.Trim()) + "' where  bianhao='" + bianhao + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.ExecuteNonQuery();
        }



        con.Close();
        bind();
    }

    protected void limit(string pagename1)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {


            con.Close();
        }
        else
        {
            con.Close();
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
    }

    protected bool limit1(string pagename1)
    {
        bool A = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {


            con.Close();
            A = true;
        }
        else
        {
            con.Close();
            A = false;
        }
        return A;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Print/PrintShangBao.aspx?bianhao=" + bianhao + "&&xianshi=" + DropDownList2.SelectedValue);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "update CeShiFei set fw='" + Convert.ToDecimal(TextBox10.Text.Trim()) + "' where  bianhao='" + bianhao + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();
        bind();
    }

    private decimal sum1 = 0;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex >= 0)
        {

            if (e.Row.Cells[7].Text == "" || e.Row.Cells[7].Text == "&nbsp;")
            {
                e.Row.Cells[7].Text = "0";
            }

            sum1 += Convert.ToDecimal(e.Row.Cells[7].Text);

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "合计：";

            e.Row.Cells[7].Text = sum1.ToString();


            e.Row.Cells[7].ForeColor = Color.Blue;

        }

    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        bind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView1.EditIndex = e.NewEditIndex;
        bind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string KeyId = GridView1.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string sqld = "select beizhu2 from ceshifei where id='"+KeyId+"' and beizhu2='是'";
        SqlCommand cmdd = new SqlCommand(sqld,con1);
        SqlDataReader drd = cmdd.ExecuteReader();
        if (drd.Read())
        {
            drd.Close();
            if (limit1("修改上报费用"))
            {
                string uuname2 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
                string uuname = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());
                string uuname4 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
                string uuname3 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
                string uuname5 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
                string uuname6 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text.ToString());
                //string uuname7 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[7].Controls[0]).Text.ToString());
                string uuname8 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[8].Controls[0]).Text.ToString());
                string sql = "update ceshifei set feiyong='" + Convert.ToDecimal(uuname6) + "',  type='" + uuname + "',xiangmu='" + uuname2 + "',beizhu4='" + uuname3 + "',beizhu5='" + uuname4 + "',shuliang='" + Convert.ToDecimal(uuname5) + "',beizhu3='" + uuname8 + "' where id='" + KeyId + "'";
                SqlCommand cmd = new SqlCommand(sql, con1);
                cmd.ExecuteNonQuery();
                string sql13 = "select sum(xiaoji) as xiaoji from CeShiFei where bianhao='" + bianhao + "' and (fwz !='是' or fwz is null ) and sb ='是' group by bianhao";
                SqlDataAdapter ad13 = new SqlDataAdapter(sql13, con1);
                DataSet ds13 = new DataSet();
                ad13.Fill(ds13);
                DataTable dt13 = ds13.Tables[0];
                if (dt13.Rows.Count > 0)
                {
                    TextBox10.Text = dt13.Rows[0]["xiaoji"].ToString();
                }
                string sql2 = "update CeShiFei set fw='" + Convert.ToDecimal(TextBox10.Text.Trim()) + "' where  bianhao='" + bianhao + "'";
                SqlCommand cmd2 = new SqlCommand(sql2, con1);
                cmd2.ExecuteNonQuery();
            }
        }
        else
        {
            drd.Close();
            string uuname2 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
            string uuname = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());
            string uuname4 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
            string uuname3 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
            string uuname5 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
            string uuname6 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text.ToString());
            //string uuname7 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[7].Controls[0]).Text.ToString());
            string uuname8 = Server.HtmlEncode(((TextBox)this.GridView1.Rows[e.RowIndex].Cells[8].Controls[0]).Text.ToString());
            string sql = "update ceshifei set feiyong='" + Convert.ToDecimal(uuname6) + "',  type='" + uuname + "',xiangmu='" + uuname2 + "',beizhu4='" + uuname3 + "',beizhu5='" + uuname4 + "',shuliang='" + Convert.ToDecimal(uuname5) + "',beizhu3='" + uuname8 + "' where id='" + KeyId + "'";
            SqlCommand cmd = new SqlCommand(sql, con1);
            cmd.ExecuteNonQuery();
            string sql13 = "select sum(xiaoji) as xiaoji from CeShiFei where bianhao='" + bianhao + "' and (fwz !='是' or fwz is null ) and sb ='是' group by bianhao";
            SqlDataAdapter ad13 = new SqlDataAdapter(sql13, con1);
            DataSet ds13 = new DataSet();
            ad13.Fill(ds13);
            DataTable dt13 = ds13.Tables[0];
            if (dt13.Rows.Count > 0)
            {
                TextBox10.Text = dt13.Rows[0]["xiaoji"].ToString();
            }
            string sql2 = "update CeShiFei set fw='" + Convert.ToDecimal(TextBox10.Text.Trim()) + "' where  bianhao='" + bianhao + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con1);
            cmd2.ExecuteNonQuery();

        }

        con1.Close();
        GridView1.EditIndex = -1;
        bind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();

        if (e.CommandName == "xiada")
        {
            Random seed = new Random();
            Random randomNum = new Random(seed.Next());
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            string sqld = "select beizhu2 from ceshifei where id='" + sid + "' and beizhu2='是'";
            SqlCommand cmdd = new SqlCommand(sqld, con);
            SqlDataReader drd = cmdd.ExecuteReader();
            if (drd.Read())
            {
                drd.Close();

            }
            else
            {

                string shoufeiid = randomNum.Next().ToString() + DateTime.Now.ToString("yyyyMMdd_hhmmss");
                string sql2 = "update CeShiFei set fwz='是'  where id='" + sid + "'";
                SqlCommand com2 = new SqlCommand(sql2, con);
                SqlDataReader dr2 = com2.ExecuteReader();
                dr2.Close();
            }
            con.Close();
            bind();

        }
    }


}