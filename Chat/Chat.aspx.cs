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


public partial class Chat_Chat : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        //注册Ajax类型

        Ajax.Utility.RegisterTypeForAjax(typeof(Chat_Chat));
    }

    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// 设计器支持所需的方法 - 不要使用代码编辑器修改
    /// 此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion

    public string UserName
    {
        get
        {
            return User.Identity.Name + ":" + Session["UserName"].ToString();
        }
    }

    /// <summary>
    /// 获取新消息的html字符串
    /// </summary>
    /// <returns>客户端输出的html字符串</returns>
    [Ajax.AjaxMethod()]
    public string GetNewMsgString()
    {
        string strMsgHTML = "";

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            

        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetNewMsg";
        cmd.Parameters.Add("@username", UserName);

        conn.Open();
        using (SqlDataReader dr = cmd.ExecuteReader())
        {
            while (dr.Read())
            {
                if (dr.GetString(1) != "")
                {
                    strMsgHTML += string.Format(
                        "<span class='chatmsg' style='COLOR: #{0}'>{1}&nbsp;{2}&nbsp;{3}&nbsp;{4}&nbsp;>>&nbsp;{5}</span><br>",
                        dr.GetString(5),
                        dr.GetString(1),
                        TestIsPublic(dr.GetBoolean(6)),
                        TestYourself(dr.GetString(2)),
                        dr.GetString(4),
                        Replace_GTLT(dr.GetString(3)));
                }
                else
                {
                    strMsgHTML += string.Format(
                        "<span class='chatmsg' style='COLOR: #{0}'>{1}</span><br>",
                        dr.GetString(5),
                        dr.GetString(3));
                }
            }
        }
        conn.Close();

        SetMsgPos();

        return strMsgHTML;
    }

    /// <summary>
    /// 替换字符串中的'<','>'字符
    /// </summary>
    /// <param name="strInput">输入字符串</param>
    /// <returns>替换后的字符串</returns>
    private string Replace_GTLT(string strInput)
    {
        string strOutput = strInput.Replace("<", "&lt;");
        strOutput = strOutput.Replace(">", "&gt;");
        return strOutput;
    }

    /// <summary>
    /// 检查用户名是否是当前登录的用户名
    /// </summary>
    /// <param name="strInput">用户名</param>
    /// <returns>经过替换的用户名</returns>
    private string TestYourself(string strInput)
    {
        if (strInput == UserName)
            return "你";
        else
            return strInput;
    }

    private string TestIsPublic(bool IsPublic)
    {
        if (IsPublic)
            return "对";
        else
            return "悄悄地对";
    }

    /// <summary>
    /// 记录已经阅读过的消息id
    /// </summary>
    private void SetMsgPos()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SetMsgPos";
        cmd.Parameters.Add("@username", UserName);

        conn.Open();

        cmd.ExecuteNonQuery();

        conn.Close();
    }

    [Ajax.AjaxMethod()]
    public void SendMsg(string strMsg, string strUserTo, string strColor, string strExpression, bool bIsPublic)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            

        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SendMsg";
        cmd.Parameters.Add("@user_from", UserName);
        cmd.Parameters.Add("@user_to", strUserTo);
        cmd.Parameters.Add("@content", strMsg);
        cmd.Parameters.Add("@expression", strExpression);
        cmd.Parameters.Add("@color", strColor);
        cmd.Parameters.Add("@ispublic", bIsPublic);

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    [Ajax.AjaxMethod()]
    public string GetOnlineUserString()
    {
        string strUserlist = "";

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
           

        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetOnlineUsers";

        conn.Open();
        using (SqlDataReader dr = cmd.ExecuteReader())
        {
            while (dr.Read())
            {
                strUserlist += dr.GetString(1) + ",";
            }
        }
        conn.Close();
        return strUserlist.TrimEnd(',');
    }

    [Ajax.AjaxMethod()]
    public void Logout()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            

        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "UserLogout";
        cmd.Parameters.Add("@username", UserName);

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        //FormsAuthentication.SignOut();

    }
}