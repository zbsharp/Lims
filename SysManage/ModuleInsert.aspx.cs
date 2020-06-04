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

public partial class SysManage_ModuleInsert : System.Web.UI.Page
{
    protected string username = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["username"] != null)
        {
            username = Request.QueryString["username"].ToString();
            TextBox1.Text = username;
        }
        
        if (!IsPostBack)
        {
            TimeBind(0, TextBox1.Text);
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        int ChooseNo = 0;
        string ChooseValue = TextBox1.Text;

        AspNetPager1.Visible = false;
        TimeBind(ChooseNo, ChooseValue);
    }

    protected void TimeBind(int a, string b)
    {
        int ChooseID = a;
        string ChooseValue = b;
        string sqlstr;

        sqlstr = "select * from userinfo where username like '%" + TextBox1.Text + "%' or departmentname like '%" + TextBox1.Text + "%' or dutyname like '%" + TextBox1.Text + "%' order by jiaosename,departmentname asc ";
       
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
       
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
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

    protected void Button1_Click(object sender, EventArgs e)
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
                    for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                    {
                        if (this.CheckBoxList1.Items[i].Selected == true)
                        {
                            
                            string duty = "";
                            string username = "";
                            string dutyname = "";
                            int sid = Convert.ToInt32(GridView1.DataKeys[gr.RowIndex].Value.ToString());
                            string sql = "select dutyid,username,dutyname from userinfo where id='" + sid + "'";
                            SqlCommand cmd = new SqlCommand(sql, con);
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                duty = dr["dutyid"].ToString();
                                username = dr["username"].ToString();
                                dutyname = dr["dutyname"].ToString();
                            }
                            dr.Close();

                            string sqlx = "insert into ModuleDuty values('" + CheckBoxList1.Items[i].Value + "','" + CheckBoxList1.Items[i].Text + "','" + duty + "','" + dutyname + "','" + username + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";
                            SqlCommand cmdx = new SqlCommand(sqlx, con);
                            cmdx.ExecuteNonQuery();
                        }
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('OK！');", true);
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
        }
        finally
        {
            con.Close();
        }
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
        
    }
}