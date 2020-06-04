using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.Data.SqlClient;
using Common;
public partial class Customer_LinkManAdd : System.Web.UI.Page
{
    #region 初始绑定
    public static string CustomID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"] == null)
        {
            Response.Write("<script>alert('请先登录!');window.location.href='../Login.aspx'</script>");
        }

        else
        {

            try
            {
                CustomID = Request.QueryString["kehuid"].ToString();
            }
            catch
            {
                Response.Redirect("ReLogin.aspx");
            }

            try
            {
                string CheckSessionStr = Session["role"].ToString();
            }
            catch
            {
                Response.Redirect("ReLogin.aspx");
            }

            if (!Page.IsPostBack)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sql = "select * from CustomerLinkMan where customerid='" + CustomID + "'";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);


                GridView2.DataSource = ds.Tables[0];


                GridView2.DataBind();
                con.Close();
            }
            DataBind();
        }
    }

    private void BindJiaoSe()
    {
        //string SqlStr = "select * from XiangMuJiaoSe order by id desc";
        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        //con.Open();


        //SqlCommand MyCommand = new SqlCommand(SqlStr, con);
        //SqlDataReader MyReader = MyCommand.ExecuteReader();
        //while (MyReader.Read())
        //{

        //    DropDownList1.Items.Add(new ListItem(MyReader["content"].ToString(), MyReader["Id"].ToString()));



        //}
        //con.Close();
    }

    private void BindAiHao()
    {
        //string SqlStr = "select * from GeRenAiHao";
        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        //con.Open();
        //SqlCommand MyCommand = new SqlCommand(SqlStr, con);
        //SqlDataReader MyReader = MyCommand.ExecuteReader();
        //while (MyReader.Read())
        //{
        //    DropDownList2.Items.Add(new ListItem(MyReader["content"].ToString(), MyReader["Id"].ToString()));

        //}
        //con.Close();
    }

    private void AddJiaoSe(string QuYUStr)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        //con.Open();
        //SqlCommand MyCommand = new SqlCommand("select * from XiangMuJiaoSe where Content='" + QuYUStr + "'", con);
        //SqlDataReader MyReade = MyCommand.ExecuteReader();
        //while (MyReade.Read())
        //{
        //    return;
        //}
        //string StrSTr = "insert into XiangMuJiaoSe(Content) values('" + QuYUStr + "')";
        //DB.RunSql(StrSTr);
        //MyReade.Close();
        //con.Close();
    }

    private void AddAiHao(string QuYUStr)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        //con.Open();
        //SqlCommand MyCommand = new SqlCommand("select * from GeRenAiHao where Content='" + QuYUStr + "'", con);
        //SqlDataReader MyReade = MyCommand.ExecuteReader();
        //while (MyReade.Read())
        //{
        //    return;
        //}
        //string StrSTr = "insert into GeRenAiHao(Content) values('" + QuYUStr + "')";
        //DB.RunSql(StrSTr);
        //MyReade.Close();
        //con.Close();
    }
    #endregion

    #region 保存联系人
    protected void Button4_Click(object sender, EventArgs e)
    {

        if (TextBox1.Text.Trim().ToString() == "")
        {
            Response.Write("<script>alert('联系人姓名不可以为空！');</script>");
            return;
        }
        else
        {
            try
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
                con1.Open();
                
                string SqlStr = "insert into CustomerLinkMan(kehuid,LinkManName,Department,ZhiWu,ZhiCheng,OfficeTel,HandTel,SmallTel,Email,fax,Msn,DateTimeStr,ManLike,HomeInformation,ItemJiaoSe,BackInformation) values(";
                SqlStr = SqlStr + "'" + CustomID + "','" + this.TextBox1.Text.Trim().ToString() + "','" + this.TextBox2.Text.Trim().ToString() + "','" + this.TextBox3.Text.Trim().ToString() + "','" + this.TextBox4.Text.Trim().ToString() + "','" + this.TextBox5.Text.Trim().ToString() + "','" + this.TextBox6.Text.Trim().ToString() + "','" + DropDownList3.SelectedValue + "','" + this.TextBox8.Text.Trim().ToString() + "','" + this.TextBox9.Text.Trim().ToString() + "','" + this.TextBox10.Text.Trim().ToString() + "','','" + this.DropDownList2.SelectedItem.Text.Trim().ToString() + "','" + this.TextBox14.Text.Trim().ToString() + "','" + this.DropDownList1.SelectedItem.Text + "','" + this.TextBox16.Text.Trim().ToString() + "','')";

                SqlCommand cmd = new SqlCommand(SqlStr,con1);
                cmd.ExecuteNonQuery();

               

                Response.Write("<script>alert('增加成功');</script>");

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");
    }
    #endregion
}