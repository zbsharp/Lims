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

public partial class Case_TreeEngineer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           

           
            BindTree1();

        }
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





        sql = "select * from UserDepa where departmentid='12' or departmentid='13' or departmentid='15' or departmentid='16' or departmentid='17' or departmentid='9' order by name";




        SqlDataAdapter da = new SqlDataAdapter(sql, con);

        da.Fill(ds);
        con.Close();
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

        


    }





    private void BindTree(TreeNode node)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        DataSet ds = new DataSet();

        string sql = "";





        sql = "select * from userinfo where departmentname ='" + node.Value + "' order by username";




        SqlDataAdapter da = new SqlDataAdapter(sql, con);

        da.Fill(ds);
        con.Close();
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

       


    }


    public void BandingAddressOne(TreeNode node)
    {



      

        string tab = xianshi( node.Value);




        TreeNode tn = new TreeNode();
        tn.Text = tab;
        tn.Value = tab;
        node.ChildNodes.Add(tn);

        tn.Expanded = false;

        tn.Target = "_blank";
        tn.SelectAction = TreeNodeSelectAction.None;

        
        
  


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
        con.Close();
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

        
    }

    public string xianshi(string leixing)
    {
        string strTable = "";

        SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con4.Open();
        string sql= "";

     
            sql = "select *,(select state from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as state,(select kf from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as kf,(select xiadariqi from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as xiadariqi,(select yaoqiuwanchengriqi from anjianinfo2 where rwbianhao=ZhuJianEngineer.bianhao) as yaoqiuwanchengriqi from ZhuJianEngineer where name='" + leixing + "' and bianhao not in (select rwbianhao from anjianinfo2 where state='完成' or state='已完成打印') order by filltime desc";
        
      

        SqlDataAdapter ad4 = new SqlDataAdapter(sql, con4);



        DataSet ds4 = new DataSet();
        ad4.Fill(ds4);

        DataTable dt4 = new DataTable();
        dt4 = ds4.Tables[0];
        con4.Close();





        strTable += "<table border=\"1\"  id=mytable   ><tr><td align=\"center\">任务编号</td><td align=\"center\">状态（下达等同进行中）</td><td align=\"center\">下达日期</td><td align=\"center\">要求完成日期</td><td align=\"center\">项目经理</td><td align=\"center\">序号</td></tr>";


        for (int i = 0; i < dt4.Rows.Count; i++)
        {
            int j = i + 1;

            strTable += "<tr class=b  onclick =window.open('../Case/tasksee.aspx?tijiaobianhao=" + dt4.Rows[i]["bianhao"].ToString() + "&&chakan=1')><td align=\"center\" color=\"green\">" + dt4.Rows[i]["bianhao"].ToString() + "</td><td align=\"center\">" + dt4.Rows[i]["state"].ToString() + "</td><td align=\"center\" color=\"green\">" + dt4.Rows[i]["xiadariqi"].ToString() + "</td><td align=\"center\">" + dt4.Rows[i]["yaoqiuwanchengriqi"].ToString() + "</td><td align=\"center\">" + dt4.Rows[i]["kf"].ToString() + "</td><td align=\"center\">" + j + "</td></tr>";




        }
        strTable += "</table>";




      
        return strTable;
    }

}