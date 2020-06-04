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

public partial class Customer_CustomerTree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //con.Open();
            //string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='查看客户列表'";
            //SqlCommand cmd = new SqlCommand(sql, con);
            //SqlDataReader dr = cmd.ExecuteReader();
            //if (dr.Read())
            //{

            //    BindTree1();
            //    con.Close();
            //}
            //else
            //{
            //    con.Close();
            //    Response.Write("<script>alert('您没有权限，请与相关人员联系！');top.main.location.href='../Account/WelCome.aspx?MeId=2'</script>");
            //}

            limit("查看客户列表");
            BindTree1();

        }
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
      
        this.TreeView1.ExpandAll(); // 展开子节点

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
      
        this.TreeView1.CollapseAll();// 折叠所有节点

    }

    private void BindTree1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        DataSet ds = new DataSet();

        string sql = "";





        sql = "select * from UserDepa order by name";




        SqlDataAdapter da = new SqlDataAdapter(sql, con);

        da.Fill(ds);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            TreeNode tn = new TreeNode();
            tn.Text = dr["name"].ToString(); //根据需求要帮你数据库中的两列
            tn.Value = dr["name"].ToString();
            TreeView1.Nodes.Add(tn);
            //BandingAddressOne(tn);  //第二级的方法
            BindTree(tn);
            tn.SelectAction = TreeNodeSelectAction.None;
            tn.Expanded = false;
        }

        con.Close();


    }





    private void BindTree(TreeNode node)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        DataSet ds = new DataSet();

        string sql = "";


      


            sql = "select * from userinfo where departmentname ='"+node.Value+"' order by username";
        
      


        SqlDataAdapter da = new SqlDataAdapter(sql, con);

        da.Fill(ds);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            TreeNode tn = new TreeNode();
            tn.Text = dr["username"].ToString(); //根据需求要帮你数据库中的两列
            tn.Value = dr["username"].ToString();
            node.ChildNodes.Add(tn);
            BandingAddressOne(tn);  //第二级的方法

            tn.SelectAction = TreeNodeSelectAction.None;
            tn.Expanded = false;
        }

        con.Close();


    }


    public void BandingAddressOne(TreeNode node)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        DataSet ds = new DataSet();

        string sql = "";
        if (TextBox1.Text == "")
        {
            sql = "select * from Customer where responser='" + node.Value + "' order by filltime desc";
        }
        else
        {
            sql = "select * from Customer where  customname like '%" + TextBox1.Text + "%' and responser='" + node.Value + "' order by filltime desc";

        }
        SqlDataAdapter da = new SqlDataAdapter(sql, con);

        da.Fill(ds);
        int vv = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

           

            TreeNode tn = new TreeNode();
            tn.Text = dr["customname"].ToString(); //根据需求要帮你数据库中的两列


            tn.Text = "<span style=''>" + vv + "..." + dr["customname"].ToString() + "<span style='color:green'>";

            tn.NavigateUrl = "~/Customer/CustomerSee.aspx?kehuid=" + dr["kehuid"].ToString() + "";

            tn.Value = dr["kehuid"].ToString();
            node.ChildNodes.Add(tn);
         
            tn.Expanded = false;

            tn.Target = "_blank";
            vv++;

        }

        con.Close();
    }



    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {

    }
    protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        AddChild(e.Node);
    }

    private void AddChild(TreeNode tnd)
    {



    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        DataSet ds = new DataSet();
        string sql = "select * from Customer where  customname like '%" + TextBox1.Text + "%'";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);

        da.Fill(ds);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            TreeNode tn = new TreeNode();
            tn.Text = dr["customname"].ToString(); //根据需求要帮你数据库中的两列
            tn.Value = dr["kehuid"].ToString();

            TreeView1.Nodes[0].ChildNodes.Clear();
            TreeView1.Nodes[0].ChildNodes.Add(tn);
            BandingAddressOne(tn);  //第二级的方法
            tn.Expanded = false;
        }

        con.Close();
    }
}