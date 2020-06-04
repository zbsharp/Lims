using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

public partial class Service : System.Web.UI.Page
{
    private String mCommand;
    private String mDocumentID;
    private String mSignatureID;
    private String mSignature;
    private String mSignatures;
    private String strSql;
    private String mUserName;
    private String mExtParam;
    private string mError;
    private string mDateTime;

    private bool mResult = false;
    private String mSignatureName;			  //印章名称
    private String mSignatureUnit;			  //签章单位
    private String mSignatureUser;			  //持章人
    private String mSignatureSN;			  //签章SN
    private String mSignatureGUID;			  //全球唯一标识符

    private String mMACHIP;			  //机器IP
    private String OPType;			  //操作标志
    private String mKeySn;       //KEY序列号
    /*	
        private string mCommand;
        private SqlCommand nSqlCommand;
        private string mDocumentID;
        private string mSignatureID;
        private string mSignature;
        private string mSignatures;
        private string strSql;
        private string strUpdateCmd;
        private string strInsertCmd;
		
        private bool mResult=false;

        private string mSignatureName;
        private string mSignatureUnit;
        private string mSignatureUser;
        private string mSignatureSN;  
        private string mSignatureGUID;
        private string mMACHIP;
        private string OPType;
        private string mKeySn;

*/

    

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面

       
        mCommand = Request.Params["COMMAND"];            //取得客户端操作命令
        mUserName = Request.Params["USERNAME"];
        mExtParam = Request.Params["EXTPARAM"];

        if (mCommand == "SAVESIGNATURE")
        {                 //保存签章数据信息
            //String mDocumentUser;
            mDocumentID = Request.Params["DOCUMENTID"];
            mSignatureID = Request.Params["SIGNATUREID"];
            mSignature = Request.Params["SIGNATURE"];

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //打开数据库
            conn.Open();
            strSql = "SELECT * FROM HTMLSignature WHERE SignatureID='" + mSignatureID + "' AND DocumentID='" + mDocumentID + "'";
            SqlCommand mSqlCommand = new SqlCommand(strSql, conn);
            SqlDataReader mReader = mSqlCommand.ExecuteReader();
            if (mReader.Read())
            {
                strSql = "UPDATE HTMLSignature set DocumentID='" + mDocumentID + "',SignatureID='" + mSignatureID + "',Signature='" + mSignature + "' where SignatureID='" + mSignatureID + "' AND DocumentID='" + mDocumentID + "'";
                mSqlCommand = new SqlCommand(strSql, conn);
            }
            else
            {
                System.DateTime SystemTime;
                SystemTime = DateTime.Now;
                mSignatureID = SystemTime.ToString("yyyyMMddhhmmss");    //取得唯一值(mSignature)					                     
                strSql = "INSERT INTO HTMLSignature (DocumentID,SignatureID,Signature) VALUES ('" + mDocumentID + "','" + mSignatureID + "','" + mSignature + "')";
                mSqlCommand = new SqlCommand(strSql, conn);
            }
            mReader.Close();
            try
            {

                mSqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                mError = ex.ToString();
            }
            conn.Close();
            Response.Clear();
            Response.Write("SIGNATUREID=" + mSignatureID + "\r\n");
            Response.Write("RESULT=OK");
            Response.End();

        }

        if (mCommand == "DELESIGNATURE")
        {                    //删除签章数据信息

         
             SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //打开数据库
            conn.Open();
            
            mDocumentID = Request.Params["DOCUMENTID"];
            mSignatureID = Request.Params["SIGNATUREID"];

            strSql = "SELECT * FROM HTMLSignature WHERE SignatureID='" + mSignatureID + "' AND DocumentID='" + mDocumentID + "'";
            SqlCommand mSqlCommand = new SqlCommand(strSql, conn);
            SqlDataReader mReader = mSqlCommand.ExecuteReader();
            if (mReader.Read())
            {
                strSql = "DELETE FROM HTMLSignature WHERE SignatureID='" + mSignatureID + "' AND DocumentID='" + mDocumentID + "'";
                mSqlCommand = new SqlCommand(strSql, conn);
            }
            mReader.Close();
            try
            {

                mSqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                mError = ex.ToString();
            }

            conn.Close();
            Response.Clear();
            Response.Write("RESULT=OK");
            Response.End();
        }


        if (mCommand == "LOADSIGNATURE")
        {                         //显示签章数据信息
            mDocumentID = Request.Params["DOCUMENTID"];
            mSignatureID = Request.Params["SIGNATUREID"];
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //打开数据库
            conn.Open();
            strSql = "SELECT * FROM HTMLSignature WHERE SignatureID='" + mSignatureID + "' AND DocumentID='" + mDocumentID + "'";
            SqlCommand mSqlCommand = new SqlCommand(strSql, conn);
            SqlDataReader mReader = mSqlCommand.ExecuteReader();
            while (mReader.Read())
            {
                mSignature = mReader["Signature"].ToString();
            }
            mReader.Close();
            conn.Close();
            Response.Clear();
            Response.Write(mSignature + "\r\n");
            Response.Write("RESULT=OK");
            Response.End();
        }


        if (mCommand == "SHOWSIGNATURE")
        {                           //获取当前页面签章记录号
            mDocumentID = Request.Params["DOCUMENTID"];
            mSignatures = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //打开数据库
            conn.Open();
            strSql = "SELECT * FROM HTMLSignature WHERE DocumentID='" + mDocumentID + "'";
            SqlCommand mSqlCommand = new SqlCommand(strSql, conn);
            SqlDataReader mReader = mSqlCommand.ExecuteReader();
            while (mReader.Read())
            {
                mSignatures = mSignatures + mReader["SignatureID"].ToString() + ";";
            }
            mReader.Close();
            conn.Close();
            Response.Clear();
            Response.Write("SIGNATURES=" + mSignatures + "\r\n");
            Response.Write("RESULT=OK");
            Response.End();
        }
        if (mCommand == "GETNOWTIME")
        {         //获取服务器时间
            System.DateTime SystemTime;
            SystemTime = DateTime.Now;
            mDateTime = SystemTime.ToString("yyyy-MM-dd hh:mm:ss");
            Response.Clear();
            Response.Write("NOWTIME=" + mDateTime + "\r\n");
            Response.Write("RESULT=OK");
            Response.End();
        }

        //---------------------------------------------------------------------------------------
        if (mCommand == "GETSIGNATUREDATA")
        {           //批量签章时，获取所要保护的数据
            String mSignatureData = "";
            mDocumentID = Request.Params["DOCUMENTID"];
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //打开数据库
            conn.Open();
            strSql = "SELECT XYBH,BMJH,JF,YF,HZNR,QLZR,CPMC,DGSL,DGRQ  from HTMLDocument Where DocumentID='" + mDocumentID + "'";
            SqlCommand mSqlCommand = new SqlCommand(strSql, conn);
            SqlDataReader mReader = mSqlCommand.ExecuteReader();

            while (mReader.Read())
            {
                mSignatureData = mSignatureData + "XYBH=" + (mReader["XYBH"].ToString()) + "\r\n";
                mSignatureData = mSignatureData + "BMJH=" + (mReader["BMJH"].ToString()) + "\r\n";
                mSignatureData = mSignatureData + "JF=" + (mReader["JF"].ToString()) + "\r\n";
                mSignatureData = mSignatureData + "YF=" + (mReader["YF"].ToString()) + "\r\n";
                mSignatureData = mSignatureData + "HZNR=" + (mReader["HZNR"].ToString()) + "\r\n";
                mSignatureData = mSignatureData + "QLZR=" + (mReader["QLZR"].ToString()) + "\r\n";
                mSignatureData = mSignatureData + "CPMC=" + (mReader["CPMC"].ToString()) + "\r\n";
                mSignatureData = mSignatureData + "DGSL=" + (mReader["DGSL"].ToString()) + "\r\n";
                mSignatureData = mSignatureData + "DGRQ=" + (mReader["DGRQ"].ToString()) + "\r\n";
            }
            mReader.Close();
            conn.Close();
            Response.Clear();
            mSignatureData = Server.UrlEncode(mSignatureData);
            Response.Write("SIGNATUREDATA=" + mSignatureData + "\r\n");
            Response.Write("RESULT=OK");
            Response.End();
        }

        if (mCommand == "PUTSIGNATUREDATA")
        {            //批量签章时，写入签章数据
            mDocumentID = Request.Params["DOCUMENTID"];
            mSignature = Request.Params["SIGNATURE"];
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //打开数据库
            conn.Open();
            //取得唯一值(mSignature)
            System.DateTime SystemTime;
            SystemTime = DateTime.Now;
            mSignatureID = SystemTime.ToString("yyyyMMddhhmmss");    //取得唯一值(mSignature)					                     
            strSql = "INSERT INTO HTMLSignature (DocumentID,SignatureID,Signature) VALUES ('" + mDocumentID + "','" + mSignatureID + "','" + mSignature + "')";
            SqlCommand mSqlCommand = new SqlCommand(strSql, conn);
            try
            {
                mSqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                mError = ex.ToString();
            }
            Response.Clear();
            conn.Close();
            Response.Write("SIGNATUREID=" + mSignatureID + "\r\n");
            Response.Write("RESULT=OK");
            Response.End();
        }

        if (mCommand == "SAVEHISTORY")
        {                                    //保存签章历史信息

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //打开数据库
            conn.Open();
            
            mSignatureName = Request.Params["SIGNATURENAME"];           //印章名称
            mSignatureUnit = Request.Params["SIGNATUREUNIT"];           //印章单位
            mSignatureUser = Request.Params["SIGNATUREUSER"];           //印章用户名
            mSignatureSN = Request.Params["SIGNATURESN"];             //印章序列号
            mSignatureGUID = Request.Params["SIGNATUREGUID"];           //全球唯一标识
            mDocumentID = Request.Params["DOCUMENTID"];              //页面ID
            mSignatureID = Request.Params["SIGNATUREID"];             //签章序列号
            mMACHIP = Request.Params["MACHIP"];                  //签章机器IP
            OPType = Request.Params["LOGTYPE"];                 //日志标志
            mKeySn = Request.Params["KEYSN"];                   //KEY序列号
            strSql = "";
            strSql = strSql + " INSERT INTO HTMLHistory(SignatureName,SignatureUnit,SignatureUser,SignatureSN,";
            strSql = strSql + " SignatureGUID,DocumentID,SignatureID,IP,LogType,KeySN)";
            strSql = strSql + " VALUES('" + mSignatureName + "','" + mSignatureUnit + "','" + mSignatureUser + "','" + mSignatureSN + "','" + mSignatureGUID + "','" + mDocumentID + "','" + mSignatureID + "','" + mMACHIP + "','" + OPType + "','" + mKeySn + "')";
            SqlCommand mSqlCommand = new SqlCommand(strSql, conn);
            try
            {
                mSqlCommand.ExecuteNonQuery();
                mResult = true;
            }
            catch (SqlException ex)
            {
                mError = ex.ToString();
                mResult = false;
            }
            conn.Close();
            Response.Clear();
            Response.Write("SIGNATUREID=" + mSignatureID + "\r\n");
            Response.Write("RESULT=OK");
        }
        if (mCommand == "SIGNATUREKEY")
        {
            string sLine = "";

            string KeyName = Server.MapPath(".") + "\\" + mUserName + "\\" + mUserName + ".key";
            System.IO.StreamReader mStream = new System.IO.StreamReader(KeyName, System.Text.Encoding.ASCII);
            sLine = mStream.ReadToEnd();
            mStream.Close();
            Response.Clear();
            Response.Write(sLine + "\r\n");
            Response.Write("RESULT=OK");
            Response.End();
        }

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
}