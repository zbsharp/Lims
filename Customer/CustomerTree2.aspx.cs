using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class Customer_CustomerTree2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void PopulateNode(Object sender, TreeNodeEventArgs e)
    {
        PopulateMe(e.Node, e.Node.Depth);
    }


    public void PopulateMe(TreeNode node, int Depth)
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        DataSet ds = new DataSet();

        string sql = "";



        DataTable dt;
        if (Depth == 0)
            sql = "select * from UserDepa order by name";
        else if (Depth == 1)
            sql = "select username as name from userinfo where departmentname ='" + node.Value + "' order by username";

        else if (Depth == 2)
            sql = "select customname as name,kehuid from Customer where responser='" + node.Value + "' order by filltime desc";






        SqlDataAdapter da = new SqlDataAdapter(sql, con);

        da.Fill(ds);


        con.Close();

        dt = ds.Tables[0];


        if (dt.Rows.Count > 0)
        {

            int vv = 1;
            foreach (DataRow row in dt.Rows)
            {


                if (Depth == 2)
                {
                    TreeNode NewNode = new TreeNode();
                    
                    NewNode.Text = row["name"].ToString(); //根据需求要帮你数据库中的两列

                    int dd = dt.Rows.Count + 1;
                    NewNode.Text = "<span style=''>" + vv + "..." + row["name"].ToString() + "<span style='color:green'>";

                    NewNode.NavigateUrl = "~/Customer/CustomerSee.aspx?kehuid=" + row["kehuid"].ToString() + "";
                    NewNode.SelectAction = TreeNodeSelectAction.Select  ;
                    NewNode.Target = "_blank";
                    node.ChildNodes.Add(NewNode);
                   
                }
                else
                {
                    TreeNode NewNode = new TreeNode(row["name"].ToString(), row["name"].ToString());
                    NewNode.PopulateOnDemand = (Depth == 100) ? false : true;
                    NewNode.SelectAction = TreeNodeSelectAction.Expand;
                    node.ChildNodes.Add(NewNode);
                }

                vv++;
            }
        }
    }
}