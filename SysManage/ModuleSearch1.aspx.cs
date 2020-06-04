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

public partial class SysManage_ModuleSearch1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }
    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select top 10 * from ModuleDuty  where dutyname !=''  order by name";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);



        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        //this.My.Text = "现共有" + pds.DataSourceCount.ToString() + "条记录";

        con.Close();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {

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
                    int sid = Convert.ToInt32(GridView1.DataKeys[gr.RowIndex].Value.ToString());
                    string sqlx = "delete from  ModuleDuty where id='" + sid + "'";
                    SqlCommand cmdx = new SqlCommand(sqlx, con);
                    cmdx.ExecuteNonQuery();
                }
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "msg1", "alert('OK！');", true);
            con.Close();
            Bind();
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
    protected void Button2_Click(object sender, EventArgs e)
    {


        string sqlstr;

        if (TextBox1.Text.Trim() == "")
        {

            sqlstr = "select * from ModuleDuty where  modulename like '%" + DropDownList2.SelectedItem.Text + "%' order by name asc ";

        }
        else
        {
            sqlstr = "select * from ModuleDuty where name like '%" + TextBox1.Text + "%' or modulename like '%" + TextBox1.Text + "%' or dutyname like '%" + TextBox1.Text + "%'  order by name asc ";
        }

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
}