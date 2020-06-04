using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpCookie cookie = CookiesHelper.GetCookie("UserInfo");
            if (cookie != null)
            {
                this.userName.Text = HttpUtility.UrlDecode(cookie.Values["uName"]);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime time = DateTime.Now;
        string ip = Request.UserHostAddress.ToString();
        string username = this.userName.Text.Trim();
        string pwd = this.userPwd.Text;
        string pwdmd5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "md5");
        if (username.Contains("=") || username.Contains("or") || username.Contains("<") || username.Contains(">") || username.Contains("'") || pwd.Contains("=") || pwd.Contains("or") || pwd.Contains("<") || pwd.Contains(">") || pwd.Contains("'"))
        {
            Literal1.Text = "<script>alert('您的密码设置不大合理,请与系统员联系')</script>";
        }
        else
        {
            string receive = CommonLogin(username, pwdmd5);
            string state = "";
            if (receive == "0")
            {
                state = "登录失败";
                LoginAdd(username, time, ip, state);
                Literal1.Text = "<script>alert('帐号或密码错误！！')</script>";
            }
            else
            {
                state = "登录成功";
                string sqldelete = "delete from keluru where state='" + userName.Text.Trim() + "'";
                ExcutSql3(sqldelete);
                LoginAdd(username, time, ip, state);
                Hashtable hOnline = (Hashtable)Application["Online"];//读取全局变量
                if (hOnline != null)
                {
                    IDictionaryEnumerator idE = hOnline.GetEnumerator();
                    string strKey = "";
                    while (idE.MoveNext())
                    {
                        if (idE.Value != null && idE.Value.ToString().Equals(userName.Text.Trim()))//如果当前用户已经登录
                        {
                            //already　login　　　　　　　　　　　　
                            strKey = idE.Key.ToString();
                            hOnline[strKey] = "XX";//将当前用户已经在全局变量中的值设置为XX
                            break;
                        }
                    }
                }
                else
                {
                    hOnline = new Hashtable();
                }
                hOnline[Session.SessionID] = userName.Text.ToString();//初始化当前用户的
                Application.Lock();
                Application["Online"] = hOnline;
                Application.UnLock();
                Session["UserName"] = userName.Text.ToString();
                Session["Role"] = receive.ToString();

                string sql = "select id from userinfo where username='" + userName.Text.Trim() + "' order by id asc ";
                ExcutSql2(sql, 0);

                //保存cookie
                if (CheckBox1.Checked)
                {
                    HttpCookie cookie = CookiesHelper.GetCookie("UserInfo");
                    if (cookie == null)
                    {
                        cookie = new HttpCookie("UserInfo");
                        cookie.Values.Add("uName", HttpUtility.UrlEncode(userName.Text.Trim()));

                        cookie.Expires = DateTime.Now.AddDays(365);
                        CookiesHelper.AddCookie(cookie);
                    }
                    else if (!HttpUtility.UrlDecode(cookie.Values["uName"]).Equals(userName.Text.Trim()))
                    {
                        CookiesHelper.SetCookie("UserInfo", "uName", HttpUtility.UrlEncode(userName.Text.Trim()));
                    }
                }
                Response.Redirect("Account/Index.aspx");
            }
        }
    }



    public string CommonLogin(string UserName, string Password)
    {
        string result = null;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from userinfo where username='" + UserName + "' and pw='" + Password + "' and flag !='是'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            result = dr["jiaose"].ToString();
            dr.Close();
            dr.Dispose();
            con.Close();
            con.Dispose();
            return result;
        }
        else
        {
            con.Close();
            con.Dispose();
            return "0";
        }
    }

    /// <summary>
    /// 这段代码还不知道它是什么意思
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public string ExcutSql3(string sql)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string returnvalue = "no";
        SqlCommand myComm = new SqlCommand(sql, con);
        int i = myComm.ExecuteNonQuery();
        returnvalue = i.ToString();
        con.Close();
        return returnvalue;
    }

    /// <summary>
    /// 这段代码还不知道它是什么意思
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public string ExcutSql2(string sql, int i)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string returnvalue = "no";
        SqlCommand myComm = new SqlCommand(sql, con);
        SqlDataReader dr = myComm.ExecuteReader();
        if (dr.Read())
        {
            returnvalue = dr[i].ToString();
        }
        else
        {
            returnvalue = "no";
        }
        con.Close();
        return returnvalue;
    }

    public void LoginAdd(string UserName, DateTime LoginTime, string IP, string State)
    {
        //StringBuilder strSql = new StringBuilder();
        //strSql.Append("INSERT INTO Logininfo(");
        //strSql.Append("UserName,logintime,IP,State)");
        //strSql.Append(" VALUES (");
        //strSql.Append("@Un,@Lt,@ip,@st)");
        //DbParameter[] cmdParms = new DbParameter[]
        //{
        //DALHelper.DataBaseOAHelper.CreateInDbParameter("@Un", DbType.AnsiString, UserName),
        //DALHelper.DataBaseOAHelper.CreateInDbParameter("@Lt", DbType.DateTime, LoginTime),
        //DALHelper.DataBaseOAHelper.CreateInDbParameter("@ip", DbType.AnsiString, IP),
        //DALHelper.DataBaseOAHelper.CreateInDbParameter("@st", DbType.AnsiString, State)
        //};
        //return DALHelper.DataBaseOAHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "insert Logininfo values('" + UserName + "','" + LoginTime + "','" + IP + "','" + State + "')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
    }
}