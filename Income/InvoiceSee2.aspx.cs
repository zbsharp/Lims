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


public partial class Income_InvoiceSee2  : System.Web.UI.Page

{
    protected string invoiceid = "";
    protected string kehuid = "";
    protected string baojiaid = "";
    protected string liushuihao = "";
    string pinzheng = "";
    protected string queren = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        invoiceid = Request.QueryString["invoiceid"].ToString();

        liushuihao = Request.QueryString["liushuihao"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select  * from shuipiao where liushuihao='" + liushuihao + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
         
            queren = dr["queren"].ToString();
        }
        dr.Close();
        con.Close();
       
        if (!IsPostBack)
        {
            Bind();


            Bind2();

            Bind3();

        }


        if (queren == "已确认")
        {
            Button1.Enabled = false;
        }


    }

    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CeShiFeiKf where shoufeibianhao ='" + invoiceid + "'   order by filltime desc";


        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        con.Close();
        con.Dispose();
    }

    public void Bind3()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select (select shenqingbianhao from anjianinfo2 where rwbianhao=cashin2.taskid) as shenqing,(select type from ceshifeikf where id=cashin2.xiangmuid2) as na, (select top 1 fukuanriqi from shuipiao where liushuihao=cashin2.daid) as fukuanriqi,(select top 1 fukuanjine from shuipiao where liushuihao=cashin2.daid) as fukuanjine, * from cashin2 where daid='" + liushuihao + "' order by id desc";


        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();

        con.Close();
        con.Dispose();
    }



    public void Bind2()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from invoice where inid='" + invoiceid + "'";


        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            TextBox1.Text = dr["zongjia"].ToString();
            TextBox2.Text = dr["feiyong"].ToString();
        }

        con.Close();
        con.Dispose();
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {






    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            for (int j = 0; j < GridView1.Columns.Count; j++)
            {
                if (GridView1.Rows[i].Cells[j].Text.ToString() == "否" || GridView1.Rows[i].Cells[j].Text.ToString() == "未提交")
                {
                    GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.Red;
                }
            }

        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {


    }


    protected void Button1_Click(object sender, EventArgs e)
    {




    }
 


    protected void Button1_Click1(object sender, EventArgs e)
    {
        decimal daojine1 = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sql6 = "select shoufeiid from shuipiao where liushuihao='" + liushuihao + "' and shoufeiid !=''";
        SqlCommand cmd6 = new SqlCommand(sql6, con);
        SqlDataReader dr6 = cmd6.ExecuteReader();
        if (dr6.Read())
        {
            dr6.Close();
        }
        else
        {
            dr6.Close();
           
        }



        string sqlp = "select * from shuipiao where liushuihao='" + liushuihao + "'";
        SqlCommand cmdp = new SqlCommand(sqlp, con);
        SqlDataReader drp = cmdp.ExecuteReader();
        if (drp.Read())
        {
            pinzheng = drp["shoufeiid"].ToString();

            daojine1 = Math.Round(Convert.ToDecimal(drp["fukuanjine"]), 2);

        }
        drp.Close();


        string taskid1 = "";

        
        foreach (GridViewRow gr in GridView1.Rows)
        {

            
            decimal daojine = 0;
            string bumen = "";
            string taskid = "";
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");
            
            
                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();


                string sqlzhehou = "select feiyong,beizhu3,kehuid,taskid from CeShiFeiKf where id='" + sid + "'";
                SqlCommand cmdzhehou = new SqlCommand(sqlzhehou, con);
                SqlDataReader drzhehou = cmdzhehou.ExecuteReader();
                if (drzhehou.Read())
                {
                    if (drzhehou["feiyong"] == DBNull.Value)
                    {
                        daojine = 0;
                        kehuid = drzhehou["kehuid"].ToString();
                        taskid1 = drzhehou["taskid"].ToString();
                    }
                    else
                    {
                        //baojiazong_txt.Text = Convert.ToDecimal(drsum["total"]).ToString().Trim();
                        daojine = Math.Round(Convert.ToDecimal(drzhehou["feiyong"]), 2);
                        kehuid = drzhehou["kehuid"].ToString();
                        taskid1 = drzhehou["taskid"].ToString();
                    }
                    bumen = drzhehou["beizhu3"].ToString();
                }



                drzhehou.Close();


                string sql5 = "update CeShiFeikf set heduibiaozhi='是' where id='"+sid+"'";
                SqlCommand cmd5 = new SqlCommand(sql5 ,con);
                cmd5.ExecuteNonQuery();



                string sqlx = "select * from cashin2 where xiangmuid2='"+sid+"'";
                SqlCommand cmdx = new SqlCommand(sqlx,con);
                SqlDataReader drx = cmdx.ExecuteReader();
                if (drx.Read())
                {
                    drx.Close();
                    //string sql4 = "insert into cashin2 values('" + liushuihao + "','1','" + sid + "','" + invoiceid + "','" + daojine + "','" + DateTime.Now + "','" + Session["UserName"].ToString() + "','','" + kehuid + "','','','','','','','" + bumen + "','0','" + pinzheng + "','" + taskid + "')";
                    //SqlCommand com4 = new SqlCommand(sql4, con);
                    //com4.ExecuteNonQuery();
                }
                else
                {
                    drx.Close();
                    //string sql4 = "insert into cashin2 values('" + liushuihao + "','1','" + sid + "','"+invoiceid+"','" + daojine + "','" + DateTime.Now + "','" + Session["UserName"].ToString() + "','','" + kehuid + "','','','','','','','" + bumen + "','0','" + pinzheng + "','" + taskid + "')";
                    //SqlCommand com4 = new SqlCommand(sql4, con);
                    //com4.ExecuteNonQuery();
                }


            


            

          
        }


        string sql3 = "update invoice set hesuanbiaozhi='是',hesuanriqi='"+DateTime.Now.ToShortDateString()+"',hesuanname='"+Session["UserName"].ToString()+"' where inid='"+ invoiceid+"'";
        SqlCommand cmd3 = new SqlCommand(sql3,con);
        cmd3.ExecuteNonQuery();


        string sql4 = "insert into cashin2 values('" + liushuihao + "','1','','" + invoiceid + "','" + daojine1 + "','" + DateTime.Now + "','" + Session["UserName"].ToString() + "','','" + kehuid + "','','','','','','','检测部','0','" + pinzheng + "','" + taskid1 + "')";
        SqlCommand com4 = new SqlCommand(sql4, con);
        com4.ExecuteNonQuery();

        con.Close();

        Bind();


        Bind2();

        Bind3();
       //Response.Write("<script>alert('保存成功！');top.main.location.href='../Quotation/QuotationAppFra.aspx?id=shenpi'</script>");
      //  Response.Write("<script>alert('保存成功！');top.main.location.href='../Income/CheckFra.aspx?liushuihao="+liushuihao+"'</script>");

    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (queren != "已确认")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sid = GridView2.DataKeys[e.RowIndex].Value.ToString();
            string xiangmuid = "";
            string shoufeiid = "";

            string sql2 = "select xiangmuid2,dengji2 from  cashin2 where id='" + sid + "' ";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                xiangmuid = dr2["xiangmuid2"].ToString();
                shoufeiid = dr2["dengji2"].ToString();
            }
            dr2.Close();

            string sql3 = "update CeShiFeikf set heduibiaozhi='否' where id='" + xiangmuid + "'";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            cmd3.ExecuteNonQuery();


            string sql4 = "delete from cashin2 where id='" + sid + "'";
            SqlCommand com4 = new SqlCommand(sql4, con);
            com4.ExecuteNonQuery();

            string sql5 = "update invoice set hesuanbiaozhi='否' where inid='" + shoufeiid + "'";
            SqlCommand cmd5 = new SqlCommand(sql5, con);
            cmd5.ExecuteNonQuery();


            con.Close();
            Bind();


            Bind2();

            Bind3();
        }
        //Response.Write("<script>alert('保存成功！');top.main.location.href='../Quotation/QuotationAppFra.aspx?id=shenpi'</script>");

    }


    protected string hesuan()
    {
        string feiyong = "0";
        decimal a = 0;
        decimal b = 0;
        decimal c = 0;
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string sql4 = "select (select sum(feiyong) from CeShiFeiKf where id=CheckHeSuan2.tableid) as dd from CheckHeSuan2 where bianhao='" + invoiceid + "' ";
        SqlCommand cmd4 = new SqlCommand(sql4, con1);
        SqlDataReader dr4 = cmd4.ExecuteReader();
        while (dr4.Read())
        {
            if (dr4["dd"] == DBNull.Value)
            {

            }
            else
            {
                //baojiazong_txt.Text = Convert.ToDecimal(drsum["total"]).ToString().Trim();
                a = a + Convert.ToDecimal(dr4["dd"]);
            }
        }
        dr4.Close();




       

        c = a + b;
        feiyong = Math.Round(Convert.ToDecimal(c), 2).ToString();
        con1.Close();
        return feiyong;

    }


    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
      

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        bool ch = ((CheckBox)sender).Checked;
       


        if (ch)
        {

            GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;
            
            string tableid = this.GridView1.Rows[gvr.RowIndex].Cells[1].Text.ToString();


            string sql = "select * from CheckHeSuan2 where tableid='" + tableid + "' and bianhao='" + invoiceid + "' and tablename='CeShiFei'";
            SqlCommand cmd = new SqlCommand(sql, con1);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
            }
            else
            {
                dr.Close();
                string sql2 = "insert into CheckHeSuan2 values('CeShiFei','" + tableid + "','" + invoiceid + "')";
                SqlCommand cmd2 = new SqlCommand(sql2, con1);
                cmd2.ExecuteNonQuery();
            }
        }

        else
        {

            GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;
          

            string tableid = this.GridView1.Rows[gvr.RowIndex].Cells[1].Text.ToString();


            string sql = "select * from CheckHeSuan2 where tableid='" + tableid + "' and bianhao='" + invoiceid + "'  and tablename='CeShiFei'";
            SqlCommand cmd = new SqlCommand(sql, con1);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                string sql2 = "delete from  CheckHeSuan2 where tableid='" + tableid + "' and bianhao='" + invoiceid + "'  and tablename='CeShiFei'";
                SqlCommand cmd2 = new SqlCommand(sql2, con1);
                cmd2.ExecuteNonQuery();
            }
            else
            {
                dr.Close();
            }
        }
        con1.Close();
        TextBox3.Text = hesuan();

    }


}