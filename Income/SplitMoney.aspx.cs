using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Income_SplitMoney : System.Web.UI.Page
{
    private int id;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Convert.ToInt32(Request.QueryString["id"]);

        if (ViewState["CreatControl"] != null)
        {
            CreatControl(Convert.ToInt32(ViewState["CreatControl"]));
        }

        if (!IsPostBack)
        {
            bool b = IsClaim();
            if (b == true)
            {
                Bind();
            }
            else
            {
                Literal1.Text = "<script>alert('该款项业务已认领，不能再进行拆分');window.close();</script>";
            }
        }
    }

    /// <summary>
    /// 检查该款项是否已被认领
    /// </summary>
    /// <returns></returns>
    private bool IsClaim()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from Claim where shuipiaoid='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                return false;
            }
            else
            {
                dr.Close();
                return true;
            }
        }
    }

    private void Bind()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select  liushuihao,fukuanren,fukuanriqi,fukuanjine,danwei,beizhu,fukuanfangshi from shuipiao where id=" + id + "";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lbserialnumber.Text = dr["liushuihao"].ToString();
                lbpayer.Text = dr["fukuanren"].ToString();
                lbpaytime.Text = dr["fukuanriqi"].ToString();
                lbmoney.Text = dr["fukuanjine"].ToString();
                lbcurrency.Text = dr["danwei"].ToString();
                lbremark.Text = dr["beizhu"].ToString();
                lbpaymethod.Text = dr["fukuanfangshi"].ToString();
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Literal1.Text = string.Empty;
        int count = Convert.ToInt32(txcount.Text);
        if (count > 1)
        {
            PlaceHolder1.Controls.Clear();//清空控件
            ViewState["CreatControl"] = count;//用ViewState保存、防止页面回发清空已生成的控件
            CreatControl(count);
            btnsave.Visible = true;
        }
        else
        {
            Literal1.Text = "<script>alert('至少拆分成两个款项')</script>";
        }
    }

    /// <summary>
    /// 动态生成服务器控件
    /// </summary>
    private void CreatControl(int count)
    {
        for (int i = 1; i <= count; i++)
        {
            PlaceHolder1.Controls.Add(new Label() { Text = "第" + i + "款：" });
            PlaceHolder1.Controls.Add(new TextBox() { CssClass = "txt" });
            PlaceHolder1.Controls.Add(new Literal() { Text = "<br />" });
        }
    }

    /// <summary>
    /// 遍历PlaceHolder1控件的文本款并取值
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    private List<decimal> GetControlText(Control control)
    {
        List<decimal> list = new List<decimal>();
        foreach (Control item in control.Controls)
        {
            if (item is TextBox)
            {
                if (((TextBox)item).Text == "")
                {
                }
                else
                {
                    list.Add(Convert.ToDecimal(((TextBox)item).Text));
                }
            }
        }
        return list;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        List<decimal> list = GetControlText(PlaceHolder1);
        if (list.Count < 2)
        {
            Literal1.Text = "<script>alert('至少拆分成两笔')</script>";
            return;
        }

        decimal summoney = 0;
        for (int i = 0; i < list.Count; i++)
        {
            summoney += Convert.ToDecimal(list[i]);
        }
        if (summoney == Convert.ToDecimal(lbmoney.Text))
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                Random seed = new Random();
                for (int i = 0; i < list.Count; i++)
                {
                    string shoufeiid = seed.Next().ToString() + DateTime.Now.ToString("yyyyMMdd_hhmmss");
                    string sql = "insert into shuipiao values('" + shoufeiid + "','" + lbpayer.Text + "','" + lbpaytime.Text + "','" + list[i].ToString() + "','" + lbcurrency.Text + "','" + lbremark.Text + "','" + lbpaymethod.Text + "','','','','','','','','','','','','否','" + lbpaytime.Text + "','" + Session["Username"].ToString() + "','" + DateTime.Now + "')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
                string sqldelete = "update shuipiao set beizhu2='是' where id=" + id + "";
                SqlCommand cmddelete = new SqlCommand(sqldelete, con);
                cmddelete.ExecuteNonQuery();
                PlaceHolder1.Controls.Clear();
                Literal1.Text = "<script>alert('拆分成功')</script>";
            }
        }
        else
        {
            Literal1.Text = "<script>alert('拆分之后的金额总和要与付款金额相等')</script>";
        }
    }
}