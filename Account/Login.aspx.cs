using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBTable;
using Common;
using System.Collections;
using DBBLL;
using System.Data.SqlClient;
using System.Configuration;
public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            //HttpCookie Cookie = CookiesHelper.GetCookie("UserInfo");
            //if (Cookie != null)
            //{
            //    this.UserName.Text = HttpUtility.UrlDecode(Cookie.Values["uName"]);
            //}


            



        }

    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        //bool A = false;

        //if (A == false)
        //{
        //    A = true;
        //    DateTime Time = DateTime.Now;
        //    string ip = Request.UserHostAddress.ToString();
        //    string UN = this.UserName.Text.Trim();
        //    string PW = this.Password.Text.Trim();
        //    string strMd5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PW, "md5");
        //    if (UN.Contains("or") || PW.Contains("=") || PW.Contains("<"))
        //    {
        //        ld.Text = "<script>alert('您的密码设置不大合理,请与系统员联系')</script>";
        //    }
        //    else
        //    {
        //        Common.LoginLogic LG = new LoginLogic();
        //        DBTable.DBLoginInfo LoginInfo = new DBLoginInfo();
        //        string i = LG.Login(UN, strMd5);
        //        string State = "";
        //        if (i == "0")
        //        {
        //            State = "登录失败";
        //            int m = LoginInfo.Add(UN, Time, ip, State);
        //            ld.Text = "<script>alert('帐号或密码错误！！')</script>";

        //        }

        //        else
        //        {
        //            //string ssid = "";
        //            //string sqlxiangtong = "select ip,username,state from KeLuRu where ip='" + ip + "'";
        //            //MyExcutSql Exts = new MyExcutSql();
        //            //ssid = Exts.ExcutSql2(sqlxiangtong, 2);
                    
        //            //if (ssid != "no")
        //            //{
        //            //    //ld.Text = "<script>alert('该用户在本机已在线！！')</script>";
        //            //}
        //            //else
        //            {

        //                State = "登录成功";

        //                string sqldelete = "delete from keluru where state='"+UserName.Text.Trim()+"'";
        //                MyExcutSql ext2 = new MyExcutSql();
        //                ext2.ExcutSql3(sqldelete);

        //                int n = LoginInfo.Add(UN, Time, ip, State);
        //                Hashtable hOnline = (Hashtable)Application["Online"];//读取全局变量
        //                if (hOnline != null)
        //                {
        //                    IDictionaryEnumerator idE = hOnline.GetEnumerator();
        //                    string strKey = "";
        //                    while (idE.MoveNext())
        //                    {
        //                        if (idE.Value != null && idE.Value.ToString().Equals(UserName.Text.Trim()))//如果当前用户已经登录，
        //                        {
        //                            //already　login　　　　　　　　　　　　
        //                            strKey = idE.Key.ToString();
        //                            hOnline[strKey] = "XX";//将当前用户已经在全局变量中的值设置为XX
        //                            break;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    hOnline = new Hashtable();
        //                }
        //                hOnline[Session.SessionID] = UserName.Text.Trim();//初始化当前用户的
        //                Application.Lock();
        //                Application["Online"] = hOnline;
        //                Application.UnLock();

        //                Session["UserName"] = (UserName.Text.Trim());
        //                Session["Role"] = i.ToString();
        //                string sql = "select id from userinfo where username='" + UserName.Text.Trim() + "' order by id asc ";
        //                MyExcutSql Exts2 = new MyExcutSql();
        //                Session["id"] = Exts2.ExcutSql2(sql, 0);
        //                if (CheckBox1.Checked)
        //                {

        //                    HttpCookie Cookie = CookiesHelper.GetCookie("UserInfo");
        //                    if (Cookie == null)
        //                    {
        //                        Cookie = new HttpCookie("UserInfo");
        //                        Cookie.Values.Add("uName", HttpUtility.UrlEncode(UserName.Text.Trim()));

        //                        Cookie.Expires = DateTime.Now.AddDays(365);
        //                        CookiesHelper.AddCookie(Cookie);
        //                    }
        //                    else if (!HttpUtility.UrlDecode(Cookie.Values["uName"]).Equals(UserName.Text.Trim()))
        //                        CookiesHelper.SetCookie("UserInfo", "uName", HttpUtility.UrlEncode(UserName.Text.Trim()));
        //                }

        //                Response.Redirect("Index.aspx");

        //            }
        //        }
        //    }
            
        //}
        //A = false;
    }

    //改了form认证
    //{
    //    System.Web.Security.FormsAuthentication.SetAuthCookie(this.UserName.Text.Trim(), false);

    //    Response.Redirect("Index.aspx");
    //}

    //dev.21tx.com/2010/01/20/13049.html
    //chenxiao154.blog.163.com/blog/static/529467022010112812818646/
    //blog.csdn.net/diligentcat/article/details/5460829 session
    //www.hbrc.com/rczx/shownews-733814-11.html session

  
}
