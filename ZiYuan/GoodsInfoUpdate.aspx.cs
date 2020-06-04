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
using Common;
using DBBLL;
using DBTable;
using System.Drawing;
using System.IO;

public partial class ZiYuan_GoodsInfoUpdate : System.Web.UI.Page
{
    protected string bianhao = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        bianhao = Request.QueryString["bianhao"].ToString();
        Button2.Attributes.Add("onclick", "javascript:return confirm('是否删除？其结果不可逆！')");
        if (!IsPostBack)
        {
            UserDepa();
            TextBox14.Text = DateTime.Now.ToShortDateString();
            //TextBox23.Text = Session["UserName"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "select * from GoodsInfo where bianhao='" + bianhao + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (dr["sqdate"].ToString().Substring(0, 4) == "1900")
                {
                    TextBox1.Text = "";
                }
                else
                {
                    TextBox1.Text = Convert.ToDateTime(dr["sqdate"].ToString()).ToShortDateString();
                }
                TextBox2.Text = dr["sqnum"].ToString();
                TextBox3.Text = dr["yusuanid"].ToString();
                TextBox4.Text = dr["yusuanfee"].ToString();
                TextBox5.Text = dr["jingbanby"].ToString();
                TextBox6.Text = dr["jq_name"].ToString();
                TextBox7.Text = dr["jq_type"].ToString();
                TextBox8.Text = dr["jq_id"].ToString();
                TextBox9.Text = dr["jq_bianhao"].ToString();
                TextBox10.Text = dr["jq_danwei"].ToString();
                TextBox11.Text = dr["gongyingshang"].ToString();
                TextBox12.Text = dr["realfee"].ToString();
                if (dr["buydate"].ToString().Substring(0, 4) == "1900")
                {
                    TextBox13.Text = "";
                }
                else
                {
                    TextBox13.Text = Convert.ToDateTime(dr["buydate"].ToString()).ToShortDateString();
                }
                if (dr["ruzhangdate"].ToString().Substring(0, 4) == "1900")
                {
                    TextBox14.Text = "";
                }
                else
                {
                    TextBox14.Text = Convert.ToDateTime(dr["ruzhangdate"].ToString()).ToShortDateString();
                }
                if (dr["shiyongdate"].ToString().Substring(0, 4) == "1900")
                {
                    TextBox15.Text = "";
                }
                else
                {
                    TextBox15.Text = Convert.ToDateTime(dr["shiyongdate"].ToString()).ToShortDateString();
                }
                if (dr["youxiaodate"].ToString().Substring(0, 4) == "1900")
                {
                    TextBox16.Text = "";
                }
                else
                {
                    TextBox16.Text = Convert.ToDateTime(dr["youxiaodate"].ToString()).ToShortDateString();
                }
                TextBox17.Text = dr["testdanwei"].ToString();
                if (dr["testdate"].ToString().Substring(0, 4) == "1900")
                {
                    TextBox18.Text = "";
                }
                else
                {
                    TextBox18.Text = Convert.ToDateTime(dr["testdate"].ToString()).ToShortDateString();
                }
                TextBox19.Text = dr["testzhouqi"].ToString();
                TextBox20.Text = dr["shiyongqingkuang"].ToString();
                TextBox21.Text = dr["yichangqingkuang"].ToString();
                TextBox22.Text = dr["danganid"].ToString();
                TextBox23.Text = dr["yanshouby"].ToString();
                TextBox24.Text = dr["remark"].ToString();
                TextBox26.Text = dr["gys_address"].ToString();
                TextBox27.Text = dr["gys_linkman"].ToString();
                TextBox28.Text = dr["gys_tel"].ToString();
                TextBox29.Text = dr["gys_zizhi"].ToString();
                TextBox30.Text = dr["youhuifee"].ToString();
                TextBox31.Text = dr["fukuanfee"].ToString();
                DropDownList11.SelectedValue  = dr["daohuoflag"].ToString();
                TextBox33.Text = dr["zhizaoshang"].ToString();
                TextBox34.Text = dr["beizhu1"].ToString();
                TextBox35.Text = dr["beizhu2"].ToString();

                DropDownList1.SelectedValue = dr["leibie"].ToString();
                DropDownList2.SelectedValue = dr["sqbumen"].ToString();
                Userinfo();
                DropDownList3.SelectedValue = dr["sqby"].ToString();
                DropDownList4.SelectedValue = dr["biandongtype"].ToString();
                DropDownList5.SelectedValue = dr["youhuiflag"].ToString();
                DropDownList6.SelectedValue = dr["fapiaoflag"].ToString();
                DropDownList7.SelectedValue = dr["testflag"].ToString();
                DropDownList8.SelectedValue = dr["state1"].ToString();
                DropDownList9.SelectedValue = dr["state2"].ToString();
                DropDownList10.SelectedValue = dr["jihua"].ToString();
            }
            dr.Close();
            con.Close();
            Bind();
        }

        if (Session["UserName"].ToString() == "admin") { }
        else if (limit1("资产采购"))
        {
            TextBox13.Enabled = false;
            TextBox33.Enabled = false;
            TextBox23.Enabled = false;
            TextBox15.Enabled = false;
            TextBox7.Enabled = false;
            TextBox8.Enabled = false;

            TextBox9.Enabled = false;
            DropDownList7.Enabled = false;
            TextBox35.Enabled = false;
            TextBox8.Enabled = false;
            TextBox20.Enabled = false;
            TextBox17.Enabled = false;
            TextBox18.Enabled = false;
            TextBox19.Enabled = false;
            TextBox22.Enabled = false;
            TextBox21.Enabled = false;
            TextBox24.Enabled = false;
            TextBox16.Enabled = false;
            DropDownList9.Enabled = false;
            DropDownList8.Enabled = false;
            TextBox7.Enabled = false;
        }
        else if (limit1("资产验收"))
        {
            TextBox20.Enabled = false;
            TextBox17.Enabled = false;
            TextBox18.Enabled = false;
            TextBox19.Enabled = false;
            TextBox22.Enabled = false;
            TextBox21.Enabled = false;
            TextBox24.Enabled = false;
            TextBox16.Enabled = false;
            DropDownList9.Enabled = false;
            DropDownList8.Enabled = false;



            TextBox1.Enabled = false;
            TextBox2.Enabled = false;
            TextBox3.Enabled = false;
            TextBox4.Enabled = false;
            TextBox5.Enabled = false;
            TextBox6.Enabled = false;
            TextBox7.Enabled = true;
            TextBox8.Enabled = true;
            TextBox10.Enabled = false;
            TextBox11.Enabled = false;
            TextBox12.Enabled = false;
            TextBox14.Enabled = false;
            
            TextBox25.Enabled = false;
            TextBox26.Enabled = false;
            TextBox27.Enabled = false;
            TextBox28.Enabled = false;
            TextBox29.Enabled = false;
            TextBox30.Enabled = false;
            TextBox31.Enabled = false;
            
            DropDownList11.Enabled = false;
            TextBox34.Enabled = false;
        
            DropDownList1.Enabled = false;
            DropDownList2.Enabled = false;
            DropDownList3.Enabled = false;
            DropDownList4.Enabled = false;
            DropDownList5.Enabled = false;
            DropDownList6.Enabled = false;
            DropDownList10.Enabled = false;
          
        }
        else if (limit1("资产校准"))
        {
            TextBox13.Enabled = false;
            TextBox33.Enabled = false;
            TextBox23.Enabled = false;
            TextBox15.Enabled = false;
            TextBox9.Enabled = false;
            DropDownList7.Enabled = false;
            TextBox35.Enabled = false;


            TextBox7.Enabled = false;
            TextBox1.Enabled = false;
            TextBox2.Enabled = false;
            TextBox3.Enabled = false;
            TextBox4.Enabled = false;
            TextBox5.Enabled = false;
            TextBox6.Enabled = false;
            TextBox7.Enabled = false;
            TextBox8.Enabled = false;
            TextBox10.Enabled = false;
            TextBox11.Enabled = false;
            TextBox12.Enabled = false;
            TextBox14.Enabled = false;
            TextBox23.Enabled = false;
            TextBox25.Enabled = false;
            TextBox26.Enabled = false;
            TextBox27.Enabled = false;
            TextBox28.Enabled = false;
            TextBox29.Enabled = false;
            TextBox30.Enabled = false;
            TextBox31.Enabled = false;
            DropDownList11.Enabled = false;
            TextBox34.Enabled = false;
            TextBox35.Enabled = false;
            DropDownList1.Enabled = false;
            DropDownList2.Enabled = false;
            DropDownList3.Enabled = false;
            DropDownList4.Enabled = false;
            DropDownList5.Enabled = false;
            DropDownList6.Enabled = false;
            DropDownList10.Enabled = false;

        }
        else
        {
            Button1.Enabled = false;
            Button2.Enabled = false;
        }
    }

    protected void limit(string pagename1)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {


            con.Close();
        }
        else
        {
            con.Close();
            Response.Write("<script>alert('您没有权限，请与相关人员联系！');this.location.href='../Account/WelCome.aspx?MeId=2'</script>");
        }
    }

    protected bool limit1(string pagename1)
    {
        bool A = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from ModuleDuty where name='" + Session["UserName"].ToString() + "' and modulename='" + pagename1 + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {


            con.Close();
            A = true;
        }
        else
        {
            con.Close();
            A = false;
        }
        return A;
    }

    protected void UserDepa()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from UserDepa";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        con.Close();
        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataTextField = "name";
        DropDownList2.DataValueField = "name";
        DropDownList2.DataBind();
        
    }
    protected void Userinfo()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from Userinfo where departmentname='" + DropDownList2.SelectedValue + "'";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        con.Close();
        DropDownList3.DataSource = ds.Tables[0];
        DropDownList3.DataTextField = "username";
        DropDownList3.DataValueField = "username";
        DropDownList3.DataBind();
      
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Userinfo();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

       

            //string dd="";
            //string sql2="select youxiaodate from goodsinfo where bianhao='" + bianhao + "'";
            //SqlCommand cmd2=new SqlCommand(sql2,con);
            //SqlDataReader dr2=cmd2.ExecuteReader();
            //if(dr2.Read())
            //{
            //    dd=Convert.ToDateTime(dr2["youxiaodate"]).ToShortDateString();
            //}
            //dr2.Close();
        string sql = "";


        string dd = "";
        string sql4 = "select bianhao from GoodsInfo where bianhao='" + bianhao + "'";
        SqlCommand cmd4 = new SqlCommand(sql4,con);
        SqlDataReader dr4 = cmd4.ExecuteReader();
        if (dr4.Read())
        {
            dd = dr4["bianhao"].ToString();
        }
        dr4.Close();
        if (Session["UserName"].ToString() == "刘洁")
        {
            if (dd.Contains("a") || dd.Contains("b") || dd.Contains("r"))
            {
                sql = "update GoodsInfo set  leibie='" + DropDownList1.SelectedValue + "',sqbumen='" + DropDownList2.SelectedValue + "',sqby='" + DropDownList3.SelectedValue + "',biandongtype='" + DropDownList4.SelectedValue + "',youhuiflag='" + DropDownList5.SelectedValue + "',fapiaoflag='" + DropDownList6.SelectedValue + "',testflag='" + DropDownList7.SelectedValue + "',state1='" + DropDownList8.SelectedValue + "',state2='" + DropDownList9.SelectedValue + "',sqdate='" + TextBox1.Text + "',sqnum='" + TextBox2.Text + "',yusuanid='" + TextBox3.Text + "',yusuanfee='" + TextBox4.Text + "',jingbanby='" + TextBox5.Text + "',jq_name='" + TextBox6.Text + "',jq_type='" + TextBox7.Text + "',jq_id='" + TextBox8.Text + "',jq_bianhao='" + TextBox9.Text + "',jq_danwei='" + TextBox10.Text + "',gongyingshang='" + TextBox11.Text + "',realfee='" + TextBox12.Text + "',buydate='" + TextBox13.Text + "',ruzhangdate='" + TextBox14.Text + "',shiyongdate='" + TextBox15.Text + "',youxiaodate='" + TextBox16.Text + "',testdanwei='" + TextBox17.Text + "',testdate='" + TextBox18.Text + "',testzhouqi='" + TextBox19.Text + "',shiyongqingkuang='" + TextBox20.Text + "',yichangqingkuang='" + TextBox21.Text + "',danganid='" + TextBox22.Text + "',yanshouby='" + TextBox23.Text + "',remark='" + TextBox24.Text + "',gys_address='" + TextBox26.Text + "',gys_linkman='" + TextBox27.Text + "',gys_tel='" + TextBox28.Text + "',gys_zizhi='" + TextBox29.Text + "',youhuifee='" + TextBox30.Text + "',fukuanfee='" + TextBox31.Text + "',daohuoflag='" + DropDownList11.SelectedValue + "',zhizaoshang='" + TextBox33.Text + "',beizhu1='" + TextBox34.Text + "',beizhu2='" + TextBox35.Text + "',jihua='" + DropDownList10.SelectedValue + "' where bianhao='" + bianhao + "' ";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            else
            {

                string bianhao3 = bianhao_id();
                sql = "update GoodsInfo set bianhao='" + bianhao3 + "', leibie='" + DropDownList1.SelectedValue + "',sqbumen='" + DropDownList2.SelectedValue + "',sqby='" + DropDownList3.SelectedValue + "',biandongtype='" + DropDownList4.SelectedValue + "',youhuiflag='" + DropDownList5.SelectedValue + "',fapiaoflag='" + DropDownList6.SelectedValue + "',testflag='" + DropDownList7.SelectedValue + "',state1='" + DropDownList8.SelectedValue + "',state2='" + DropDownList9.SelectedValue + "',sqdate='" + TextBox1.Text + "',sqnum='" + TextBox2.Text + "',yusuanid='" + TextBox3.Text + "',yusuanfee='" + TextBox4.Text + "',jingbanby='" + TextBox5.Text + "',jq_name='" + TextBox6.Text + "',jq_type='" + TextBox7.Text + "',jq_id='" + TextBox8.Text + "',jq_bianhao='" + TextBox9.Text + "',jq_danwei='" + TextBox10.Text + "',gongyingshang='" + TextBox11.Text + "',realfee='" + TextBox12.Text + "',buydate='" + TextBox13.Text + "',ruzhangdate='" + TextBox14.Text + "',shiyongdate='" + TextBox15.Text + "',youxiaodate='" + TextBox16.Text + "',testdanwei='" + TextBox17.Text + "',testdate='" + TextBox18.Text + "',testzhouqi='" + TextBox19.Text + "',shiyongqingkuang='" + TextBox20.Text + "',yichangqingkuang='" + TextBox21.Text + "',danganid='" + TextBox22.Text + "',yanshouby='" + TextBox23.Text + "',remark='" + TextBox24.Text + "',gys_address='" + TextBox26.Text + "',gys_linkman='" + TextBox27.Text + "',gys_tel='" + TextBox28.Text + "',gys_zizhi='" + TextBox29.Text + "',youhuifee='" + TextBox30.Text + "',fukuanfee='" + TextBox31.Text + "',daohuoflag='" + DropDownList11.SelectedValue + "',zhizaoshang='" + TextBox33.Text + "',beizhu1='" + TextBox34.Text + "',beizhu2='" + TextBox35.Text + "',jihua='" + DropDownList10.SelectedValue + "' where bianhao='" + bianhao + "' ";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

            }
        }
        else
        {
            sql = "update GoodsInfo set  leibie='" + DropDownList1.SelectedValue + "',sqbumen='" + DropDownList2.SelectedValue + "',sqby='" + DropDownList3.SelectedValue + "',biandongtype='" + DropDownList4.SelectedValue + "',youhuiflag='" + DropDownList5.SelectedValue + "',fapiaoflag='" + DropDownList6.SelectedValue + "',testflag='" + DropDownList7.SelectedValue + "',state1='" + DropDownList8.SelectedValue + "',state2='" + DropDownList9.SelectedValue + "',sqdate='" + TextBox1.Text + "',sqnum='" + TextBox2.Text + "',yusuanid='" + TextBox3.Text + "',yusuanfee='" + TextBox4.Text + "',jingbanby='" + TextBox5.Text + "',jq_name='" + TextBox6.Text + "',jq_type='" + TextBox7.Text + "',jq_id='" + TextBox8.Text + "',jq_bianhao='" + TextBox9.Text + "',jq_danwei='" + TextBox10.Text + "',gongyingshang='" + TextBox11.Text + "',realfee='" + TextBox12.Text + "',buydate='" + TextBox13.Text + "',ruzhangdate='" + TextBox14.Text + "',shiyongdate='" + TextBox15.Text + "',youxiaodate='" + TextBox16.Text + "',testdanwei='" + TextBox17.Text + "',testdate='" + TextBox18.Text + "',testzhouqi='" + TextBox19.Text + "',shiyongqingkuang='" + TextBox20.Text + "',yichangqingkuang='" + TextBox21.Text + "',danganid='" + TextBox22.Text + "',yanshouby='" + TextBox23.Text + "',remark='" + TextBox24.Text + "',gys_address='" + TextBox26.Text + "',gys_linkman='" + TextBox27.Text + "',gys_tel='" + TextBox28.Text + "',gys_zizhi='" + TextBox29.Text + "',youhuifee='" + TextBox30.Text + "',fukuanfee='" + TextBox31.Text + "',daohuoflag='" + DropDownList11.SelectedValue + "',zhizaoshang='" + TextBox33.Text + "',beizhu1='" + TextBox34.Text + "',beizhu2='" + TextBox35.Text + "',jihua='" + DropDownList10.SelectedValue + "' where bianhao='" + bianhao + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }


            //if (dd != Convert.ToDateTime(TextBox16.Text.Trim()).ToShortDateString())
            //{
            //    string sql3 = "update goodsinfo set pub_field2='是' where bianhao='" + bianhao + "' ";
            //    SqlCommand cmd3 = new SqlCommand(sql3, con);
            //    cmd3.ExecuteNonQuery();
            //}

            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('更新成功');", true);
            //ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('提交成功');window.navigate('GoodsInfoManage.aspx','_self');", true);
   

      

            con.Close();
       

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        try
        {
            string sql = "delete from GoodsInfo where bianhao='" + bianhao + "' and  fillname='"+Session["UserName"].ToString()+"'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            //ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('更新成功');", true);
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('已删除成功！！');window.navigate('GoodsInfoManage.aspx','_self');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('" + ex.Message.ToString() + "请重新检查输入是否规范，如有不明与开发人员联系！');", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
       
            if (FileUpload1.PostedFile.ContentLength == 0)
            {
                this.Label2.Text = "请重新检查！！";
            }
            else
            {
                //在上传文件时经常要判断文件夹是否存在，如果存在就上传文件，否则新建文件夹再上传文件
                //判断语句为
                string mapfilestring = "~/Upfiles";
                if (System.IO.Directory.Exists(Server.MapPath(mapfilestring)) == false)//如果不存在就创建file文件夹
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(mapfilestring));
                }

                string fileFullname = this.FileUpload1.FileName;
                string fileName = fileFullname.Substring(fileFullname.LastIndexOf("\\") + 1);
                string type = fileFullname.Substring(fileFullname.LastIndexOf(".") + 1);
                fileFullname = bianhao + "_" + fileName;

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sqlx = "select * from GoodsInfo_LoadList where fileFullname='" + fileFullname + "'";
                SqlCommand comx = new SqlCommand(sqlx, con);
                SqlDataReader drx = comx.ExecuteReader();
                if (drx.Read())
                {
                    drx.Close();
                    con.Close();
                    this.Label2.Text = "此文件已存在，请重新检查！！";
                }
                else
                {
                    drx.Close();
                    this.FileUpload1.SaveAs(Server.MapPath(mapfilestring) + "\\" + fileFullname);

                    string ImageUrl = mapfilestring + "/" + fileFullname;
                    string ip = Request.UserHostAddress.ToString();
                    string sql = "insert GoodsInfo_LoadList values('" + ImageUrl + "','" + fileName + "','" + type + "','" + fileFullname + "','" + bianhao + "','" + ip + "','" + Session["username"].ToString() + "','" + DateTime.Now + "','','','')";
                    SqlCommand com = new SqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();
                    this.Label2.Text = "上传成功！！";
                }
            }
      
            this.TextBox25.Text = "";
            Bind();
        
    }
    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql = "select * from GoodsInfo_LoadList where bianhao='" + bianhao + "' order by id asc";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }
    //下载函数   
    private void DownLoadFile(string fileurl, string filefullname)
    {
        string filePath = Server.MapPath(fileurl) + "\\" + filefullname;
        if (File.Exists(filePath))
        {
            FileInfo file = new FileInfo(filePath);
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8"); //解决中文乱码       
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name)); //解决中文文件名乱码        
            Response.AddHeader("Content-length", file.Length.ToString());
            Response.ContentType = "appliction/octet-stream";
            Response.WriteFile(file.FullName);
            Response.End();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sid = e.CommandArgument.ToString();
        if (e.CommandName == "shanchu")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string mapfilestring = "";
            string sql = "select * from GoodsInfo_LoadList where id='" + sid + "'";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                mapfilestring = dr["url"].ToString();
            }
            dr.Close();

            System.IO.File.Delete(MapPath(mapfilestring));

            string sqlx = "delete from GoodsInfo_LoadList where id='" + sid + "'";
            SqlCommand cmdx = new SqlCommand(sqlx, con);
            cmdx.ExecuteNonQuery();

            con.Close();

            Bind();
        }
        else if (e.CommandName == "open")
        {
            string FullFileName = "";

            try
            {
                string url = "";
                string filefullname = "";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                string sql = "select * from GoodsInfo_LoadList where id='" + sid + "'";
                SqlCommand com = new SqlCommand(sql, con);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    url = dr["url"].ToString();
                    filefullname = dr["filefullname"].ToString();
                }
                dr.Close();
                con.Close();

                string FileName = ".//路径//书名.pdf";
                FileName = url;
                FullFileName = Server.MapPath(FileName);
                //FileName--要下载的文件名 
                FileInfo DownloadFile = new FileInfo(FullFileName);
                if (DownloadFile.Exists)
                {
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.Buffer = false;
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(DownloadFile.FullName, System.Text.Encoding.ASCII));
                    Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
                    Response.WriteFile(DownloadFile.FullName);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    //文件不存在
                }
            }
            catch (Exception ex)
            {
                //打开时异常了
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
        }
    }

    protected string bianhao_id()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string bianhao1 = "";
        string bh_start = "";
        string bh_middle = "";

        if (DropDownList1.SelectedValue == "中检")
        {
            bh_start = "A";
        }
        else if (DropDownList1.SelectedValue == "中认")
        {
            bh_start = "R";
        }
        else if (DropDownList1.SelectedValue == "佛山")
        {
            bh_start = "B";
        }
        bh_middle = DateTime.Now.Year.ToString().Substring(2, 2) + Convert.ToInt32(DateTime.Now.Month.ToString()).ToString("00");

        string sql = "select bianhao from GoodsInfo where bianhao like '" + bh_start + "%' order by convert(int,substring(bianhao,2,9)) asc";
        SqlDataAdapter adpter = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        adpter.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            bianhao1 = bh_start + bh_middle + "00001";
        }
        else
        {
            string lastid = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["bianhao"].ToString();
            string lastid_end = lastid.Substring(5, 5);
            int aa = Convert.ToInt32(lastid_end) + 1;
            bianhao1 = bh_start + bh_middle + aa.ToString("00000");
        }
        con.Close();
        return bianhao1;
    }

}