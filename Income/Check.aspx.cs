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

public partial class Income_Check : System.Web.UI.Page
{
    private int _i = 0;
    public  string liushuihao = "";
    protected string kehuname = "";
    protected string queren = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        liushuihao = Request.QueryString["liushuihao"].ToString();
        kehuname = Request.QueryString["fukuanren"].ToString();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        string shui="";
       
        string sqls = "select  * from shuipiao where liushuihao='" + liushuihao + "'";
        SqlCommand cmds = new SqlCommand(sqls, con);
        SqlDataReader drs = cmds.ExecuteReader();
        if (drs.Read())
        {
            shui = drs["daoruren"].ToString();

           
        }
        drs.Close();


        //if (shui == Session["UserName"].ToString())
        //{
        //    con.Close();
        //    Button3.Text = "不能由同一到账登记人结算费用";
        //    Button3.Enabled = false;


        //}
        //else
        {










            string sql = "select  * from shuipiao where liushuihao='" + liushuihao + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Label1.Text = dr["fukuanren"].ToString();
                Label2.Text = dr["fukuanjine"].ToString();
                Label4.Text = dr["fukuanriqi"].ToString();
                Label1.ForeColor = Color.Red;
                Label2.ForeColor = Color.Red;
                Label4.ForeColor = Color.Red;
                queren = dr["queren"].ToString();
            }
            dr.Close();

            string sql2 = "select  sum(xiaojine) as yifen from cashin2 where daid='" + liushuihao + "' group by daid";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                Label3.Text = dr2["yifen"].ToString();

                Label3.ForeColor = Color.Red;

            }
            dr2.Close();


            string sql3 = "update invoice set feiyong =(select sum(feiyong) from ceshifeikf where shoufeibianhao=invoice.inid group by shoufeibianhao) ";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            cmd3.ExecuteNonQuery();

            con.Close();


            if (queren == "已确认")
            {
                Button3.Enabled = false;
            }



            if (!IsPostBack)
            {
                TextBox1.Text = Label1.Text;

                bind();

            }
        }

    }



    protected void bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select top 50 *,(select top 1 bianhao from anjianinfo2 where rwbianhao=invoice.rwbh) as bianhao2,sqbianhao as shenqingbianhao,rwbh as taskno,(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname ,(select sum(feiyong) from CeShiFeiKf where shoufeibianhao=invoice.inid) as feiyong1 from Invoice where hesuanbiaozhi='" + DropDownList2.SelectedValue + "' and kehuid in (select kehuid customname from customer where customname like '%" + TextBox1.Text.Trim() + "%') order by id desc";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        GridView1.DataSource = dr;
        GridView1.DataBind();

        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");


            if (queren!="已确认")
            {
                if (gr.Cells[8].Text == "否")
                {

                    hzf.Enabled = true;
                }
                else
                {
                    hzf.Enabled = false;
                }
              

                //string sql2 = "insert into CheckHeSuan4 values('CeShiFei','" + sid + "','" + ran + "')";
                //SqlCommand cmd2 = new SqlCommand(sql2, con);
                //cmd2.ExecuteNonQuery();

            }
            else
            {
                


            
                    hzf.Enabled = false;
                
              


            }



        }

        dr.Close();

        string sql2 = "delete from checkhesuan2 where bianhao='"+liushuihao+"'";
        SqlCommand cmd2 = new SqlCommand(sql2,con);
        cmd2.ExecuteNonQuery();



        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *,(select top 1 bianhao from anjianinfo2 where rwbianhao=invoice.rwbh) as bianhao2,sqbianhao as shenqingbianhao,rwbh as taskno ,(select sum(feiyong) from CeShiFeiKf where shoufeibianhao=invoice.inid) as feiyong1,(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname  from Invoice where    ((  rwbh like '%" + TextBox1.Text.Trim() + "%') or kehuid in (select kehuid  from customer where customname like '%" + TextBox1.Text.Trim() + "%') or  ( name like '%" + TextBox1.Text.Trim() + "%') or ( name1 like '%" + TextBox1.Text.Trim() + "%') or ( name2 like '%" + TextBox1.Text.Trim() + "%') or ((sqbianhao like '%" + TextBox1.Text.Trim() + "%')))";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        GridView1.DataSource = dr;
        GridView1.DataBind();
        GridView1.ShowFooter = true;
        con.Close();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
      
        
        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con3.Open();

        decimal dd = 0;

        string sql31 = "select sum(xiaojine) as jin from cashin2 where daid='"+liushuihao+"' group by daid";
        SqlCommand cmd31 = new SqlCommand(sql31,con3);
        SqlDataReader dr31 = cmd31.ExecuteReader();
        if (dr31.Read())
        {
            if (dr31["jin"] != DBNull.Value)
            {
                dd = Convert.ToDecimal(dr31["jin"]);
            }
        }
        dr31.Close();


        if (1 == 1)
        {

            string shoufeiid = "";



            string sql6 = "select shoufeiid from shuipiao where liushuihao='" + liushuihao + "' and shoufeiid !=''";
            SqlCommand cmd6 = new SqlCommand(sql6, con3);
            SqlDataReader dr6 = cmd6.ExecuteReader();
            if (dr6.Read())
            {


                shoufeiid = dr6["shoufeiid"].ToString();
                dr6.Close();


            }
            else
            {
                dr6.Close();







                string sql1 = "select shoufeiid from shuipiao where shoufeiid !=''   order by shoufeiid";

                string id = "";
                SqlDataAdapter adpter = new SqlDataAdapter(sql1, con3);
                DataSet ds = new DataSet();
                adpter.Fill(ds);
                string date1 = "";
                string day = "";
                string houzhui = "";
                string two = "";
                string yue = "";
                string tian = "";
                string yue1 = "";
                string tian1 = "";
                if (ds.Tables[0].Rows.Count == 0)
                {



                    shoufeiid = "P" + DateTime.Now.Year.ToString().Substring(2, 2) + "-" + "00001";
                }
                else
                {
                    houzhui = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["shoufeiid"].ToString();
                    yue = houzhui.Substring(4, 5);

                    yue1 = houzhui.Substring(1, 2);

                    tian1 = DateTime.Now.Year.ToString().Substring(2, 2);
                    if (yue1 == tian1)
                    {

                        int a = Convert.ToInt32(yue) + 1;

                        shoufeiid = "P" + DateTime.Now.Year.ToString().Substring(2, 2) + "-" + String.Format("{0:D5}", a);
                    }
                    else
                    {
                        shoufeiid = "P" + DateTime.Now.Year.ToString().Substring(2, 2) + "-" +"00001";
 
                    }
                }

            }

            string sql = "update shuipiao set  queren='已确认',querenren='" + Session["UserName"].ToString() + "',querenriqi='" + DateTime.Now + "',shoufeiid='" + shoufeiid + "' where liushuihao='" + liushuihao + "'";


            SqlCommand cmd3 = new SqlCommand(sql, con3);


            cmd3.ExecuteNonQuery();


            string sql2 = "update cashin2 set pinzheng='" + shoufeiid + "' where daid='" + liushuihao + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, con3);
            cmd2.ExecuteNonQuery();


            string sql41 = "update cashin2 set tichenriqi=shuipiao.querenriqi from shuipiao where pinzheng=shuipiao.shoufeiid  and daid='"+liushuihao+"'";
            SqlCommand cmd41 = new SqlCommand(sql41,con3);
            cmd41.ExecuteNonQuery();


            con3.Close();
            Button3.Enabled = false;
            ld.Text = "<script>alert('完成分款记录成功');</script>";


          
        }
        else
        {
            con3.Close();
            ld.Text = "<script>alert('分款记录不匹配');</script>";
        }
        
    }
    private decimal sum1 = 0;
    private decimal sum2 = 0;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            HyperLink hlf = (HyperLink)e.Row.FindControl("HyperLink1");



            hlf.NavigateUrl = "~/Income/InvoiceSee2.aspx?invoiceid="+e.Row.Cells[1].Text.Trim()+"&&liushuihao="+liushuihao;
            hlf.Target = "button";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("id", _i.ToString());
                e.Row.Attributes.Add("onKeyDown", "SelectRow();");
                //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");


                e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
                _i++;
            }



        }

        if (e.Row.RowIndex >= 0)
        {


            if (e.Row.Cells[5].Text == "" || e.Row.Cells[5].Text == "&nbsp;")
            {
                e.Row.Cells[5].Text = "0";
            }

            sum2 += Convert.ToDecimal(e.Row.Cells[5].Text);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "小计：";

            e.Row.Cells[5].Text = sum2.ToString();
            e.Row.Cells[1].ForeColor = Color.Blue;
            e.Row.Cells[5].ForeColor = Color.Blue;



        }

    }



    protected string hesuan()
    {
        string feiyong = "0";
        decimal a = 0;
        decimal b = 0;
        decimal c = 0;
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();

        string sql4 = "select (select sum(feiyong) from invoice where inid=CheckHeSuan2.tableid) as dd from CheckHeSuan2 where bianhao='" + liushuihao + "' ";
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


            string sql = "select * from CheckHeSuan2 where tableid='" + tableid + "' and bianhao='" + liushuihao + "' and tablename='CeShiFei'";
            SqlCommand cmd = new SqlCommand(sql, con1);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
            }
            else
            {
                dr.Close();
                string sql2 = "insert into CheckHeSuan2 values('CeShiFei','" + tableid + "','" + liushuihao + "')";
                SqlCommand cmd2 = new SqlCommand(sql2, con1);
                cmd2.ExecuteNonQuery();
            }
        }

        else
        {

            GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;


            string tableid = this.GridView1.Rows[gvr.RowIndex].Cells[1].Text.ToString();


            string sql = "select * from CheckHeSuan2 where tableid='" + tableid + "' and bianhao='" + liushuihao + "'  and tablename='CeShiFei'";
            SqlCommand cmd = new SqlCommand(sql, con1);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                string sql2 = "delete from  CheckHeSuan2 where tableid='" + tableid + "' and bianhao='" + liushuihao + "'  and tablename='CeShiFei'";
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