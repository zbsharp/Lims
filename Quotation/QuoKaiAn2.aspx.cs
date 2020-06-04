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

public partial class Quotation_QuoKaiAn2 : System.Web.UI.Page
{
    protected string shwhere = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        shwhere = "tijiaobiaozhi ='是' and (tijiaohaoma is null or tijiaohaoma='')";
        if (!IsPostBack)
        {
            Bind();
        }

    }

    protected void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *,(select top 1 customname from customer where kehuid =baojiacpxiangmu.kehuid) as kehuname  from baojiacpxiangmu where " + shwhere + " and  " + searchwhere.search(Session["UserName"].ToString()) + "  order by id desc";

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
        con.Close();
        con.Dispose();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string ChooseID = DropDownList1.SelectedValue.Trim();
        string value = TextBox1.Text.Trim();
        string sql = "";

        if (DropDownList1.SelectedValue != "kehuname")
        {
            sql = "select *," + searchwhere.searchcustomer("BaoJiaCPXiangMu", "") + " from baojiacpxiangmu where " + searchwhere.search(Session["UserName"].ToString(), ChooseID, value) + " and " + shwhere + " order by id desc";
        }
        else
        {
            sql = "select *," + searchwhere.searchcustomer("BaoJiaCPXiangMu", "") + " from baojiacpxiangmu where " + searchwhere.search(Session["UserName"].ToString()) + " and " + searchwhere.searchcustomer(TextBox1.Text.Trim()) + " and  " + shwhere + "  order by id desc";

        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
        con.Dispose();
        AspNetPager1.Visible = false;
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        
        string baojiaid = "";
        string kehuid = "";
        string taskid="";
        string responser = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        int c = 0;
        for (int j = 0; j < CheckBoxList1.Items.Count; j++)
        {
            if (CheckBoxList1.Items[j].Selected == true)
            {
                c = c + 1 ;
            }
        }


        if (c == 1)
        {

            Random rd = new Random();
            string rd1 = rd.Next(1000).ToString();
            string dataName = DateTime.Now.ToString("yyyyMMddhhmmss") + rd1;
            try
            {

                foreach (GridViewRow gr in GridView1.Rows)
                {
                    CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
                    if (hzf.Checked)
                    {
                        string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                        string sql = "select baojiaid,kehuid,responser from BaoJiaCPXiangMu where id='" + sid + "'";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            baojiaid = dr["baojiaid"].ToString();
                            kehuid = dr["kehuid"].ToString();
                            responser = dr["responser"].ToString();
                        }

                        dr.Close();


                        SqlTransaction trs = con.BeginTransaction();
                        SqlCommand cmdinsert = new SqlCommand();
                        cmdinsert.Connection = con;
                        cmdinsert.Transaction = trs;

                        string sqlinsert = "delete from BaoJiaCPXiangMu2 where id='" + sid + "'and tijiaohaoma!='" + dataName + "'";
                        cmdinsert.CommandText = sqlinsert;
                        cmdinsert.ExecuteNonQuery();

                        string sqlinsert1 = "insert into BaoJiaCPXiangMu2 select * from BaoJiaCPXiangMu where id='" + sid + "'";
                        cmdinsert.CommandText = sqlinsert1;
                        cmdinsert.ExecuteNonQuery();

                        string sqlinsert2 = "update BaoJiaCPXiangMu2 set tijiaohaoma='" + dataName + "' where id='" + sid + "'";
                        cmdinsert.CommandText = sqlinsert2;
                        cmdinsert.ExecuteNonQuery();

                        string sqlinsert3 = "update BaoJiaCPXiangMu set tijiaohaoma='" + dataName + "' where id='" + sid + "'";
                        cmdinsert.CommandText = sqlinsert3;
                        cmdinsert.ExecuteNonQuery();



                        trs.Commit();


                    }
                }

                string sqlinsert4 = "insert into anjianinfo values('" + baojiaid + "','" + kehuid + "','" + taskid + "','" + dataName + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "','" + CheckBoxList1.SelectedValue + "','否','','','未开始')";
                SqlCommand cmd4 = new SqlCommand(sqlinsert4, con);
                cmd4.ExecuteNonQuery();

               
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
            }
            
        }
        else
        {


            string sid = "";
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
                if (hzf.Checked)
                {
                    sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                  
                }
            }
            string sql = "select baojiaid,kehuid,responser from BaoJiaCPXiangMu where id='" + sid + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                baojiaid = dr["baojiaid"].ToString();
                kehuid = dr["kehuid"].ToString();
                responser = dr["responser"].ToString();
            }

            dr.Close();

            for (int j = 0; j < CheckBoxList1.Items.Count; j++)
            {
                if (CheckBoxList1.Items[j].Selected == true)
                {

                    Random rd = new Random();
                    string rd1 = rd.Next(1000).ToString();
                    string dataName = DateTime.Now.ToString("yyyyMMddhhmmss") + rd1;
                    
                    
                    
                    SqlTransaction trs = con.BeginTransaction();
                    SqlCommand cmdinsert = new SqlCommand();
                    cmdinsert.Connection = con;
                    cmdinsert.Transaction = trs;

                    string sqlinsert = "delete from BaoJiaCPXiangMu2 where id='" + sid + "'and tijiaohaoma!='" + dataName + "'";
                    cmdinsert.CommandText = sqlinsert;
                    cmdinsert.ExecuteNonQuery();

                    string sqlinsert1 = "insert into BaoJiaCPXiangMu2 select * from BaoJiaCPXiangMu where id='" + sid + "'";
                    cmdinsert.CommandText = sqlinsert1;
                    cmdinsert.ExecuteNonQuery();

                    string sqlinsert2 = "update BaoJiaCPXiangMu2 set tijiaohaoma='" + dataName + "' where id='" + sid + "'";
                    cmdinsert.CommandText = sqlinsert2;
                    cmdinsert.ExecuteNonQuery();

                    string sqlinsert3 = "update BaoJiaCPXiangMu set tijiaohaoma='" + dataName + "' where id='" + sid + "'";
                    cmdinsert.CommandText = sqlinsert3;
                    cmdinsert.ExecuteNonQuery();

                    string sqlinsert4 = "insert into anjianinfo values('" + baojiaid + "','" + kehuid + "','" + taskid + "','" + dataName + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + responser + "','" + CheckBoxList1.SelectedValue + "','否','','','未开始')";
                    cmdinsert.CommandText = sqlinsert4;
                    cmdinsert.ExecuteNonQuery();

                    trs.Commit();
                }
            }

        }

        con.Close();
        con.Dispose();
       // ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('OK！');", true);
        Response.Write("<script>alert('保存成功！');top.main.location.href='../Case/CaseFra.aspx?id=1'</script>");
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        //全选或全不选
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
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }
}