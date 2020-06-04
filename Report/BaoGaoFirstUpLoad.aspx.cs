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

public partial class Report_BaoGaoFirstUpLoad : System.Web.UI.Page
{
    protected string xiangmuid = "";
    // protected string src = "";
    protected string baojiaid = "";
    protected string leibie = "";
    protected string sp = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        xiangmuid = Request.QueryString["baogaoid"].ToString();
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select leibie from baogao2  where baogaoid='" + xiangmuid + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                leibie = dr["leibie"].ToString();
            }
            con.Close();
            con.Dispose();
            Bindfuwufujian();
            BindDep();
            //dinge();
            //dinge2();
            //SFUPC();
        }
    }


    protected void BindDep()
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();
        string sql = "select * from UserDepa where name=(select departmentname from userinfo where username='" + Session["UserName"].ToString() + "')";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataValueField = "name";
        DropDownList1.DataTextField = "name";
        DropDownList1.DataBind();
        con3.Close();
    }

    //protected void dinge()
    //{

    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
    //    con.Open();
    //    string sql = "select *  from baogaoshenhe where shenhebianhao='" + xiangmuid + "' order by shenhetime1 desc";
    //    SqlCommand com = new SqlCommand(sql, con);
    //    SqlDataReader dr = com.ExecuteReader();
    //    GridView1.DataSource = dr;
    //    GridView1.DataBind();

    //    con.Close();
    //    con.Dispose();

    //}

    //protected void dinge2()
    //{

    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
    //    con.Open();
    //    string sql = "select *  from baogaoshenhe2 where shenhebianhao='" + xiangmuid + "' order by shenhetime1 desc";
    //    SqlCommand com = new SqlCommand(sql, con);
    //    SqlDataReader dr = com.ExecuteReader();
    //    GridView2.DataSource = dr;
    //    GridView2.DataBind();

    //    con.Close();
    //    con.Dispose();

    //}

    protected void Button5_Click(object sender, EventArgs e)
    {
        SqlConnection con12 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con12.Open();
        string sqlx = "select *,(select top 1 shenheren from baogaoshenhe2 where shenhebianhao=baogao2.baogaoid) as shenheby1 from baogao2 where baogaoid='" + xiangmuid + "'";
        SqlCommand comx = new SqlCommand(sqlx, con12);
        SqlDataReader drx = comx.ExecuteReader();
        if (drx.Read())
        {
            sp = drx["pizhunby"].ToString();
        }
        drx.Close();
        con12.Close();
        if (sp == "")
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con1.Open();

            string appl = "";
            string tjid = "";

            string sqla = "select dengjiby,tjid  from baogao2 where baogaoid='" + xiangmuid + "' order by id desc";
            SqlCommand coma = new SqlCommand(sqla, con1);
            SqlDataReader dra = coma.ExecuteReader();
            if (dra.Read())
            {
                appl = dra["dengjiby"].ToString();
                tjid = dra["tjid"].ToString();
            }

            dra.Close();
            string cc = "";


            string sqlb = "";

            if (FileUpload1.PostedFile.ContentLength == 0)
            {
                //  src = "";
            }
            else
            {
                Random rd = new Random();
                string rand = rd.Next(0, 99999).ToString();
                string fileFullname = DateTime.Now.ToString("yyyy-MM-dd") + rand + "..." + this.FileUpload1.FileName;
                string fileName = fileFullname.Substring(fileFullname.LastIndexOf("\\") + 1);
                string type = fileFullname.Substring(fileFullname.LastIndexOf(".") + 1);
                string path = Server.MapPath("~/BaoGaoFirst\\");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                this.FileUpload1.PostedFile.SaveAs(path + "\\" + fileFullname);
                string ProImg = "BaoGaoFirst/" + fileFullname;
                string ImageUrl = "~/BaoGaoFirst/" + fileFullname;


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sql = "insert BaoGaoFuJian values('" + xiangmuid + "','" + ImageUrl + "','" + fileName + "','" + type + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + FileUpload1.PostedFile.ContentLength / (1024 * 1024) + "','" + this.FileUpload1.FileName + "','','')";
                SqlCommand com = new SqlCommand(sql, con);
                com.ExecuteNonQuery();
                string sqlstate = "insert into  TaskState values ('" + xiangmuid + "','" + xiangmuid + "','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','上传报告','客服受理任务生成案件号')";
                SqlCommand cmdstate = new SqlCommand(sqlstate, con);
                cmdstate.ExecuteNonQuery();
                con.Close();
                Bindfuwufujian();
                this.Label2.Text = "上传附件成功";
            }
        }
        else
        {
            this.Label2.Text = "报告已批准，不能再次上传";
        }
    }

    public void Bindfuwufujian()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql1 = "select * from BaoGaoFuJian where caseid='" + xiangmuid + "'";
        SqlDataAdapter da = new SqlDataAdapter(sql1, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        this.GridView5.DataSource = ds;
        this.GridView5.DataBind();
        con.Close();
        con.Dispose();
    }

    protected void GridView5_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string src2 = "";
        int Appurtenanceid = Convert.ToInt32(GridView5.DataKeys[e.RowIndex].Value.ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql4 = "select * from baogao2 where baogaoid='" + xiangmuid + "' and pizhunby is null";
        SqlCommand cmd4 = new SqlCommand(sql4, con);
        SqlDataReader dr4 = cmd4.ExecuteReader();
        if (dr4.Read())
        {
            dr4.Close();
            string sql2 = "select * from BaoGaoFuJian where id=" + Appurtenanceid + "";
            SqlCommand cmd = new SqlCommand(sql2, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                src2 = dr["filename"].ToString();
            }
            dr.Close();

            string sql = "delete from BaoGaoFuJian where id=" + Appurtenanceid + "";
            SqlCommand com = new SqlCommand(sql, con);
            com.ExecuteNonQuery();

            string url = Server.MapPath("~") + "\\BaoGaoFirst\\" + src2;
            if (File.Exists(url))
            {
                File.Delete(url);
            }
            this.Label2.Text = "删除成功";
            string sql3 = "update baogao2 set statebumen1='',statebumen2='' where baogaoid='" + xiangmuid + "'";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            cmd3.ExecuteNonQuery();
        }
        else
        {
            this.Label2.Text = "报告已批准，不准删除";
        }
        con.Close();
        Bindfuwufujian();
    }


    //#region 该方法用于添加一个上传文件的控件
    //private void InsertC()
    //{
    //    //实例化ArrayList
    //    ArrayList AL = new ArrayList();
    //   // this.Tab_UpDownFile.Rows.Clear(); //清除id为F表格里的所有行
    //    GetInfo();
    //    //表示 HtmlTable 控件中的 <tr> HTML 元素
    //    HtmlTableRow HTR = new HtmlTableRow();
    //    //表示 HtmlTableRow 对象中的 <td> 和 <th> HTML 元素
    //    HtmlTableCell HTC = new HtmlTableCell();
    //    //在单元格中添加一个FileUpload控件
    //    HTC.Controls.Add(new FileUpload());
    //    //在行中添加单元格
    //    HTR.Controls.Add(HTC);
    //    //在表中添加行
    //    //Tab_UpDownFile.Rows.Add(HTR);
    //    SFUPC();
    //}
    //#endregion
    //#region 该方法用于将保存在Session中的上传文件控件集添加到表格中
    //private void GetInfo()
    //{
    //    ArrayList AL = new ArrayList();
    //    if (Session["FilesControls"] != null)
    //    {
    //        AL = (ArrayList)Session["FilesControls"];
    //        for (int i = 0; i < AL.Count; i++)
    //        {
    //            HtmlTableRow HTR = new HtmlTableRow();
    //            HtmlTableCell HTC = new HtmlTableCell();
    //            HTC.Controls.Add((System.Web.UI.WebControls.FileUpload)AL[i]);
    //            HTR.Controls.Add(HTC);
    //           // Tab_UpDownFile.Rows.Add(HTR);
    //        }
    //    }
    //}
    //#endregion
    //#region 调用InsertC方法，实现添加FileUpLoad控件的功能
    //protected void BtnAdd_Click(object sender, EventArgs e)
    //{
    //    InsertC();//执行添加控件方法
    //    LblMessage.Text = "";
    //}
    //#endregion
    //#region 实现文件上传的功能
    //protected void BtnUpFile_Click(object sender, EventArgs e)
    //{
    //    if (this.FileUpload1.PostedFile.FileName != "")
    //    {
    //        UpFile();//执行上传文件
    //        SFUPC();
    //    }
    //    else
    //    {
    //        LblMessage.Text = "对不起，上传文件为空，请选择上传文件！";
    //    }

    //}
    //#endregion

    //#region 该方法用于执行文件上传操作
    //private void UpFile()
    //{

    //    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
    //    con1.Open();
    //    string sqlb = "select * from ceshifei where beizhu5='" + xiangmuid + "' and beizhu5 !='' and fwz !='是'";
    //    SqlCommand cmdb = new SqlCommand(sqlb, con1);
    //    SqlDataReader drb = cmdb.ExecuteReader();
    //    if (drb.Read())
    //    {
    //        drb.Close();
    //        con1.Close();



    //        //获取文件夹路径
    //        string FilePath = Server.MapPath("../") + "BaoGaoFirst";

    //        // 获取客户端上载文件的集合
    //        HttpFileCollection HFC = Request.Files;
    //        for (int i = 0; i < HFC.Count; i++)
    //        {
    //            //访问指定的文件
    //            HttpPostedFile UserHPF = HFC[i];
    //            try
    //            {
    //                //判断文件是否为空
    //                if (UserHPF.ContentLength > 0)
    //                {
    //                    //将上传的文件存储在指定目录下


    //                    Random rd = new Random();

    //                    string rand = rd.Next(0, 99999).ToString();
    //                    string fileFullname = DateTime.Now.ToShortDateString() + rand + "..." + UserHPF.FileName;
    //                    string fileName = fileFullname.Substring(fileFullname.LastIndexOf("\\") + 1);
    //                    string type = fileFullname.Substring(fileFullname.LastIndexOf(".") + 1);
    //                    //UserHPF.SaveAs(Server.MapPath("../BaoGaoFirst") + "\\" + fileFullname);

    //                    UserHPF.SaveAs(FilePath + "\\" + System.IO.Path.GetFileName(UserHPF.FileName));

    //                    string ProImg = "BaoGaoFirst/" + fileFullname;
    //                    string ImageUrl = "~/BaoGaoFirst/" + UserHPF.FileName;

    //                    string ImageUrl1 = "~/BaoGaoFirst/" + UserHPF.FileName;

    //                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
    //                    con.Open();
    //                    string sql = "insert BaoGaoFuJian values('" + xiangmuid + "','" + ImageUrl1 + "','" + fileName + "','" + type + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','" + DropDownList1.SelectedValue + "','" + baojiaid + "','" + UserHPF.FileName + "','','')";
    //                    SqlCommand com = new SqlCommand(sql, con);
    //                    com.ExecuteNonQuery();



    //                    string sqlstate = "insert into  TaskState values ('" + xiangmuid + "','" + xiangmuid + "','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','上传报告','客服受理任务生成案件号')";
    //                    SqlCommand cmdstate = new SqlCommand(sqlstate, con);
    //                    cmdstate.ExecuteNonQuery();



    //                    con.Close();

    //                    Bindfuwufujian();


    //                }
    //            }
    //            catch
    //            {
    //                LblMessage.Text = "上传失败！";
    //            }
    //        }
    //        if (Session["FilesControls"] != null)
    //        {
    //            Session.Remove("FilesControls");
    //        }
    //        LblMessage.Text = "上传成功！";
    //    }
    //    else
    //    {
    //        drb.Close();
    //        con1.Close();
    //        this.Label2.Text = "您未填写上报费用，请上报费用后再上传报告";
    //    }

    //}
    //#endregion

    //private void SFUPC()
    //{
    //    ArrayList AL = new ArrayList();//动态增加数组
    //    foreach (Control C in Tab_UpDownFile.Controls)
    //    {
    //        //在表格中查找出FileUpload控件添加到ArrayList中
    //        if (C.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlTableRow")
    //        {
    //            HtmlTableCell HTC = (HtmlTableCell)C.Controls[0];
    //            foreach (Control FUC in HTC.Controls)
    //            {
    //                if (FUC.GetType().ToString() == "System.Web.UI.WebControls.FileUpload")
    //                {
    //                    FileUpload FU = (FileUpload)FUC;
    //                    //设置FileUpload的样式
    //                    FU.BorderColor = System.Drawing.Color.DimGray;
    //                    FU.BorderWidth = 1;
    //                    //添加FileUpload控件
    //                    AL.Add(FU);
    //                }
    //            }
    //        }
    //    }
    //    //把ArrayList添加到Session中
    //    Session.Add("FilesControls", AL);
    //}
}