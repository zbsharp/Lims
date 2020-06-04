using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Case_OneDispose : System.Web.UI.Page
{
    string taskid = "";
    string tijiaobianhao = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        taskid = Request.QueryString["xiangmuid"].ToString();
        tijiaobianhao = Request.QueryString["tijiaobianhao"].ToString();
        string id = Request.QueryString["id"].ToString();
        txt_taskid.Text = taskid;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            //查询该任务是哪个部门
            con.Open();
            string sql = "select bumen from AnJianInFo where id='" + id + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                txt_deparment.Text = dr["bumen"].ToString();
            }
            dr.Close();
            //判断该案件是否已经前处理了
            string sql_dispose = " select * from pretreatment where [anjianinfoid]='" + tijiaobianhao + "'";
            SqlCommand com_dispose = new SqlCommand(sql_dispose, con);
            SqlDataReader dr_dispose = com_dispose.ExecuteReader();
            if (dr_dispose.Read())
            {
                txFDate.Value = dr_dispose["begintime"].ToString();
                txTDate.Value = dr_dispose["endtime"].ToString();
                txt_result.Text = dr_dispose["result"].ToString();
                btn_yes.Visible = false;
            }
            dr_dispose.Close();
        }
    }

    protected void btn_yes_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txFDate.Value) || string.IsNullOrEmpty(txTDate.Value) || string.IsNullOrEmpty(txt_result.Text))
        {
            Literal1.Text = "<script>alert('不能存在空值')</script>";
        }
        //else if ()
        //{
        //    //时间不能早于任务下达时间



        //}
        else
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                DateTime begin = Convert.ToDateTime(txFDate.Value);
                DateTime end = Convert.ToDateTime(txTDate.Value);
                string result = txt_result.Text;
                string sql = "insert into pretreatment values('" + taskid + "','" + txt_deparment.Text + "','" + tijiaobianhao + "','" + begin + "','" + end + "','" + result + "','" + Session["Username"].ToString() + "','" + DateTime.Now + "')";
                SqlCommand cmd = new SqlCommand(sql, con);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    txFDate.Value = begin.ToString();
                    txTDate.Value = end.ToString();
                    txt_result.Text = result;
                    btn_yes.Visible = false;
                    Literal1.Text = "<script>alert('提交成功')</script>";
                }
            }
        }
    }
}