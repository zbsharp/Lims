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
using System.Collections.Generic;
using System.Linq;

public partial class Default2 : System.Web.UI.Page
{

    public string kehuid = "";
    public string baojiaid = "";
    public string cp = "";
    public string kehuidfront = "";
    public int rowindex = 0;
    public string cpid = "";
    // public string baojiaidfront = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        string d = "";
        string mm = Request.QueryString["dd"].ToString();
        Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (Request.QueryString["kehuid"] != null && Request.QueryString["baojiaid"] != null)
        {
            kehuid = Request.QueryString["kehuid"].ToString();
            baojiaid = Request.QueryString["baojiaid"].ToString();
            d = Request.QueryString["kehuid"].ToString();
            rowindex = Convert.ToInt32(Request.QueryString["cpid"]);
            Dictionary<int, string> dic = Session["Dictionary"] as Dictionary<int, string>;
            //使用linq通过key查出value
            //var changpinid = dic.Where(p => p.Key == rowindex).Select(p => p.Value);
            //使用lamada通过key查出value
            List<string> list = (from q in dic
                                 where q.Key == rowindex
                                 select q.Value).ToList<string>();
            cpid = list[0].ToString();
            cp = "0";
            Button1.Visible = false;
        }
        else
        {
            Button1.Visible = false;
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

        sql = "select distinct leibieid,leibiename  from product2 order by leibieid";


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
        // string sql = "select distinct chanpinid,chanpinname  from product2 where leibieid='"+node.Value+"' order by chanpinid";
        string sql = "select leibieid,neirong from Product2 where leibieid=" + node.Value + " group by neirong,leibieid order by neirong asc";
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        da.Fill(ds);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            TreeNode tn = new TreeNode();
            // tn.Text = SubStr(dr["chanpinname"].ToString(), 12);
            tn.Text = SubStr(dr["neirong"].ToString(), 12);
            //   tn.Value = dr["chanpinid"].ToString();
            tn.Value = dr["leibieid"].ToString();
            node.ChildNodes.Add(tn);//在这个地方我一开始遇到了一个问题，就是之前写的是 TreeView1.Nodes//[0].ChildNodes.Add(tn); 而这样出现的结果是，当我点击第一级，也就是根的时候，本来要绑定的第二级也成了第一级，当我多次点击的时候，就会循环的绑定。
            tn.Expanded = false;
            tn.Target = "content3";
            Random rd = new Random();
            string mq = DateTime.Now.ToString() + rd.Next(1000);
            // tn.NavigateUrl = "Default3.aspx?leibieid=" + node.Value + "&&chanpinid=" + tn.Value + "&&baojiaid=" + baojiaid + "&&kehuid=" + kehuid + "&&cp=" + cp + "&&zz=" + mq;
            tn.NavigateUrl = "Default3.aspx?leibieid=" + node.Value + "&&xiangmuname=" + tn.Text.ToString().Replace("+", "%2B") + "&&baojiaid=" + baojiaid + "&&kehuid=" + kehuid + "&&cp=" + cp + "&&zz=" + mq + "&&cpid=" + cpid + "";
            tn.ImageUrl = "../Images/icon/folder.gif";
        }
        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();


        //DataSet ds = new DataSet();

        //string sql = "";

        //sql = "select distinct leibieid,leibiename  from product2 where leibieid like '%"+TextBox1.Text.Trim()+"%' or leibiename like '%"+TextBox1.Text.Trim()+"%' order by leibieid";


        //SqlDataAdapter da = new SqlDataAdapter(sql, con);
        //TreeView1.Nodes.Clear();
        //da.Fill(ds);
        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{

        //    TreeNode tn = new TreeNode();
        //    tn.Text = dr["leibiename"].ToString(); //根据需求要帮你数据库中的两列
        //    tn.Value = dr["leibieid"].ToString();
        //    TreeView1.Nodes.Add(tn);
        //    BandingAddressOne(tn);  //第二级的方法
        //    tn.Expanded = false;

        //}

        //con.Close();
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

    protected void Button_search_Click(object sender, EventArgs e)
    {
        TreeView1.Nodes[0].Checked = true;
        //return;
        if (this.TextBox1.Text.Trim() != "")
        {
            Random rd = new Random();
            string mq = DateTime.Now.ToString() + rd.Next(1000);

            detailm.Attributes.Add("src", "Default3.aspx?messagetype=search&&searchmessage=" + this.TextBox1.Text + "&&baojiaid=" + baojiaid + "&&kehuid=" + kehuid + "&&cp=" + cp + "&&zz=" + mq + "&&cpid=" + cpid + "");


        }
    }
}
