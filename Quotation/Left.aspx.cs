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

public partial class Quotation_Left : System.Web.UI.Page
{
    public string kehuid = "";
    public string baojiaid = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        string d = "";
        Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (Request.QueryString["kehuid"] != null && Request.QueryString["baojiaid"] != null)
        {
            kehuid = Request.QueryString["kehuid"].ToString();
            baojiaid = (Request.QueryString["baojiaid"].ToString());
            d = Request.QueryString["kehuid"].ToString();
          
        }
        else
        {
           
        }
        if (!IsPostBack)
        {
            BandingAddressOne1();


        }
    }

    public void BandingAddressOne1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        DataSet ds = new DataSet();

        string sql = "";

        sql = "select distinct leibieid,leibiename  from product2 order by leibiename";


        SqlDataAdapter da = new SqlDataAdapter(sql, con);

        da.Fill(ds);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            TreeNode tn = new TreeNode();
            tn.Text = dr["leibiename"].ToString(); //根据需求要帮你数据库中的两列
            tn.Value = dr["leibieid"].ToString();
            TreeView1.Nodes.Add(tn);
            BandingAddressOne(tn);  //第二级的方法
            tn.Expanded = false;
            tn.SelectAction = TreeNodeSelectAction.Expand;

        }

        con.Close();
    }

    public void BandingAddressOne(TreeNode node)
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select distinct chanpinid,chanpinname  from product2 where leibieid='" + node.Value + "' order by chanpinname";


        DataSet ds = new DataSet();

        SqlDataAdapter da = new SqlDataAdapter(sql, con);

        da.Fill(ds);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            TreeNode tn = new TreeNode();


            tn.Text = SubStr(dr["chanpinname"].ToString(), 12);
            tn.Value = dr["chanpinid"].ToString();
            node.ChildNodes.Add(tn);//在这个地方我一开始遇到了一个问题，就是之前写的是 TreeView1.Nodes//[0].ChildNodes.Add(tn); 而这样出现的结果是，当我点击第一级，也就是根的时候，本来要绑定的第二级也成了第一级，当我多次点击的时候，就会循环的绑定。

            tn.Expanded = false;

            tn.Target = "content3";
            tn.NavigateUrl = "Default5.aspx?leibieid=" + node.Value + "&&chanpinid=" + tn.Value + "&&baojiaid=" + baojiaid + "&&kehuid=" + kehuid;
            tn.ImageUrl = "../Images/icon/folder.gif";





        }
        con.Close();


    }
   

    public string SubStr(string sString, int nLeng)
    {
        if (sString.Length <= nLeng)
        {
            return sString;
        }
        string sNewStr = sString.Substring(0, nLeng);

        return sNewStr;
    }
}