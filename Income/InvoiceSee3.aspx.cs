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

public partial class Income_InvoiceSee3 : System.Web.UI.Page
{
    protected string invoiceid = "";
    protected string kehuid = "";
    protected string baojiaid = "";
    protected string liushuihao = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        kehuid = Request.QueryString["kehuid"].ToString();




        if (!IsPostBack)
        {
            Bind();


       

            Bind3();

        }





    }

    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CeShiFeiKf where kehuid='"+kehuid+"' order by filltime desc";


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
        string sql = "select (select top 1 fukuanriqi from shuipiao where liushuihao=cashin2.daid) as fukuanriqi,(select top 1 fukuanjine from shuipiao where liushuihao=cashin2.daid) as fukuanjine, * from cashin2 where kehuid='" + kehuid  + "' order by id desc";


        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();

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
        foreach (GridViewRow gr in GridView1.Rows)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            decimal daojine = 0;
            string bumen = "";
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");

            if (hzf.Checked)
            {
                string sid = GridView1.DataKeys[gr.RowIndex].Value.ToString();


                string sqlzhehou = "select feiyong,beizhu3 from CeShiFeiKf where id='" + sid + "'";
                SqlCommand cmdzhehou = new SqlCommand(sqlzhehou, con);
                SqlDataReader drzhehou = cmdzhehou.ExecuteReader();
                if (drzhehou.Read())
                {
                    if (drzhehou["feiyong"] == DBNull.Value)
                    {
                        daojine = 0;
                    }
                    else
                    {
                        //baojiazong_txt.Text = Convert.ToDecimal(drsum["total"]).ToString().Trim();
                        daojine = Math.Round(Convert.ToDecimal(drzhehou["feiyong"]), 2);
                    }
                    bumen = drzhehou["beizhu3"].ToString();
                }



                drzhehou.Close();


                string sql5 = "update CeShiFeikf set heduibiaozhi='是' where id='" + sid + "'";
                SqlCommand cmd5 = new SqlCommand(sql5, con);
                cmd5.ExecuteNonQuery();



                string sql4 = "insert into cashin2 values('现金','"+Convert.ToDecimal(TextBox3.Text)+"','" + sid + "','','" + daojine + "','" + Convert .ToDateTime(TextBox5.Text.Trim()) + "','" + Session["UserName"].ToString() + "','','" + kehuid + "','','','','','','','" + bumen + "','0')";
                SqlCommand com4 = new SqlCommand(sql4, con);
                com4.ExecuteNonQuery();


            }

            con.Close();

            Bind();


          

            Bind3();
        }
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sid = GridView2.DataKeys[e.RowIndex].Value.ToString();
        string xiangmuid = "";


        string sql2 = "select xiangmuid2 from  cashin2 where id='" + sid + "' ";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            xiangmuid = dr2["xiangmuid2"].ToString();
        }
        dr2.Close();

        string sql3 = "update CeShiFeikf set heduibiaozhi='否' where id='" + xiangmuid + "'";
        SqlCommand cmd3 = new SqlCommand(sql3, con);
        cmd3.ExecuteNonQuery();


        string sql4 = "delete from cashin2 where id='" + sid + "'";
        SqlCommand com4 = new SqlCommand(sql4, con);
        com4.ExecuteNonQuery();

        con.Close();
        Bind();


    

        Bind3();
    }
}