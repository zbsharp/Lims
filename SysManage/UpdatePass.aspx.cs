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
using DBTable;
using System.Data.Common;
public partial class SysManage_UpdatePass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // Common.CheckMes.CheckState("User_ID", "Index.aspx", "用户登录信息丢失，青重新登录...");
    }

    protected void BtnSaveUserInfo_Click(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();
        //string sql = "update userinfo set jiaose='" + jiaose + "',password='" + TextBox1.Text + "',npassword='" + TextBox1.Text + "',jiaosename='" + jiaoname + "',dutyid='" + jiaose + "',dutyname='" + jiaoname + "',youxiang='" + youxiang + "',banggongdianhua='" + dianhua + "',yidong='" + yidong + "',departmentid='" + departmentid + "',departmentname='" + department + "',department='" + department + "',fax='" + TextBox2.Text.Trim() + "',shortphone='" + TextBox3.Text.Trim() + "' where id='" + id + "' ";
        //SqlCommand com1 = new SqlCommand(sql, con);
        //com1.ExecuteNonQuery();
        //Response.Write("<script>alert('修改成功!')</script>");
        //con.Close();
    }
}