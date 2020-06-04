using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Data;

public partial class Income_CashReconciliation : System.Web.UI.Page
{
    private int _i = 0;
    public string liushuihao = "";
    protected string kehuname = "";
    protected string queren = "";
    protected string selectedinvioce = "";
    string pingzhengid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        liushuihao = Request.QueryString["liushuihao"].ToString();
        kehuname = Request.QueryString["fukuanren"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();


        string shui = "";

        string sqls = "select  * from shuipiao where liushuihao='" + liushuihao + "'";
        SqlCommand cmds = new SqlCommand(sqls, con);
        SqlDataReader drs = cmds.ExecuteReader();
        if (drs.Read())
        {
            shui = drs["daoruren"].ToString();
            Label1.Text = drs["fukuanren"].ToString();
            Label2.Text = drs["fukuanjine"].ToString();
            Label4.Text = drs["fukuanriqi"].ToString();
            Label1.ForeColor = Color.Red;
            Label2.ForeColor = Color.Red;
            Label4.ForeColor = Color.Red;
            queren = drs["queren"].ToString();
            pingzhengid = drs["shoufeiid"].ToString();
        }
        drs.Close();
        //if (shui == Session["UserName"].ToString())
        //{
        //    con.Close();
        //    Label5.Text = "不能由同一用户执行导入到账和对账";
        //    Button3.Enabled = false;
        //    Button1.Enabled = false;
        //    Button_addcash.Enabled = false;
        //}
        //else
        //{
        if (queren == "已确认")
        {
            Button3.Enabled = false;
            Button1.Enabled = false;
            Button_addcash.Enabled = false;
        }
        string sql2 = "select  sum(xiaojine) as yifen from cashin2 where daid='" + liushuihao + "'";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            Label3.Text = dr2["yifen"].ToString();

            Label3.ForeColor = Color.Red;
        }
        dr2.Close();
        con.Close();
        if (!IsPostBack)
        {
            TextBox1.Text = Label1.Text;

            bind();
            BindCashin();
            BindClaim();
        }
        //}
    }



    protected void bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select top 50 *,(select top 1 bianhao from anjianinfo2 where rwbianhao=invoice.rwbh) as bianhao2,sqbianhao as shenqingbianhao,rwbh as taskno,(select top 1 customname from customer where kehuid =invoice.kehuid) as kehuname ,(select sum(feiyong) from CeShiFeiKf where shoufeibianhao=invoice.inid) as feiyong1 from Invoice where hesuanbiaozhi='否' and kehuid in (select kehuid customname from customer where customname like '%" + TextBox1.Text.Trim() + "%') order by id desc";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        GridView1.DataSource = dr;
        GridView1.DataBind();
        dr.Close();
        con.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
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
    }

    protected void Button3_Click(object sender, EventArgs e)
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string pingzhengid = "";
        //string querenzhuangtai = "";
        //string sql = "select queren from shuipiao where liushuihao='" + liushuihao + "'";
        //SqlCommand cmd = new SqlCommand(sql, con);
        //SqlDataReader dr = cmd.ExecuteReader();
        //if (dr.Read())
        //{

        //    querenzhuangtai = dr["queren"].ToString();
        //}
        //dr.Close();
        //if (pingzhengid == "")
        //{
        string sql1 = "select pinzheng from cashin2 where pinzheng !=''   order by pinzheng asc";
        string id = "";
        SqlDataAdapter adpter = new SqlDataAdapter(sql1, con);
        DataSet ds = new DataSet();
        adpter.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            pingzhengid = "P" + DateTime.Now.Year.ToString().Substring(2, 2) + "-" + "00001";
        }
        else
        {
            string houzhui = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["pinzheng"].ToString();
            string yue = houzhui.Substring(4, 5);
            string yue1 = houzhui.Substring(1, 2);

            string tian1 = DateTime.Now.Year.ToString().Substring(2, 2);
            if (yue1 == tian1)
            {

                int a = Convert.ToInt32(yue) + 1;

                pingzhengid = "P" + DateTime.Now.Year.ToString().Substring(2, 2) + "-" + String.Format("{0:D5}", a);
            }
            else
            {
                pingzhengid = "P" + DateTime.Now.Year.ToString().Substring(2, 2) + "-" + "00001";

            }
        }
        //}

        string judgemoneysql = "select fukuanjine,(select sum(xiaojine) from cashin2 where daid=shuipiao.liushuihao) as yiduijine from shuipiao where liushuihao='" + this.liushuihao + "'";
        SqlCommand judgemoneycmd = new SqlCommand(judgemoneysql, con);
        SqlDataReader judgemoneyad = judgemoneycmd.ExecuteReader();
        decimal fukuanjine = 0m;
        decimal yiduijine = 0m;
        if (judgemoneyad.Read())
        {
            fukuanjine = Convert.ToDecimal(judgemoneyad["fukuanjine"].ToString());
            yiduijine = Convert.ToDecimal(judgemoneyad["yiduijine"].ToString());
        }
        judgemoneyad.Close();
        if (fukuanjine == yiduijine)
        {
            string sql3 = "update shuipiao set  queren='已确认',querenren='" + Session["UserName"].ToString() + "',querenriqi=getdate() where liushuihao='" + liushuihao + "'";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            int num = cmd3.ExecuteNonQuery();
            if (num > 0)
            {
                Button3.Enabled = false;
                Button1.Enabled = false;
                Button_addcash.Enabled = false;
            }
        }
        else
        {
            string sql3 = "update shuipiao set  queren='半确认',querenren='" + Session["UserName"].ToString() + "',querenriqi=getdate() where liushuihao='" + liushuihao + "'";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            cmd3.ExecuteNonQuery();
        }


        string sql2 = "update cashin2 set pinzheng='" + pingzhengid + "',tichenriqi=getdate(),tichenbiaozhi='是' where daid='" + liushuihao + "' and tichenbiaozhi=''";
        SqlCommand cmd2 = new SqlCommand(sql2, con);
        cmd2.ExecuteNonQuery();




        con.Close();



        BindCashin();
    }
    private decimal sum1 = 0;
    private decimal sum2 = 0;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            //HyperLink hlf = (HyperLink)e.Row.FindControl("HyperLink1");



            //hlf.NavigateUrl = "~/Income/InvoiceSee2.aspx?invoiceid=" + e.Row.Cells[1].Text.Trim() + "&&liushuihao=" + liushuihao;
            //hlf.Target = "button";

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


            if (e.Row.Cells[3].Text == "" || e.Row.Cells[3].Text == "&nbsp;")
            {
                e.Row.Cells[3].Text = "0";
            }

            sum2 += Convert.ToDecimal(e.Row.Cells[3].Text);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "小计：";

            e.Row.Cells[3].Text = sum2.ToString();
            e.Row.Cells[1].ForeColor = Color.Blue;
            e.Row.Cells[3].ForeColor = Color.Blue;



        }

    }


    //暂保留，可不需要
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

    //暂保留，可不需要
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


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string inid = e.CommandArgument.ToString();
        if (e.CommandName == "showDetail")
        {
            selectedinvioce = inid;
            BindHesuanmingxi(selectedinvioce);
        }
    }

    protected void BindHesuanmingxi(string inid)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from CeShiFeiKf where shoufeibianhao ='" + inid + "'   order by filltime desc";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        GridView2.DataSource = dr;
        GridView2.DataBind();
        con.Close();
    }
    protected void Button_addcash_Click(object sender, EventArgs e)
    {
        string pingzhenghao = "";
        decimal daojine = 0m;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sqlp = "select * from shuipiao where liushuihao='" + liushuihao + "'";
        SqlCommand cmdp = new SqlCommand(sqlp, con);
        SqlDataReader drp = cmdp.ExecuteReader();
        if (drp.Read())
        {
            pingzhenghao = drp["shoufeiid"].ToString();

            daojine = Math.Round(Convert.ToDecimal(drp["fukuanjine"]), 2);

        }
        drp.Close();

        string sqlyifen = "select sum(xiaojine) as jine from cashin2 where daid='" + liushuihao + "'";
        SqlCommand sqlyifencmd = new SqlCommand(sqlyifen, con);
        SqlDataReader sqlyifendr = sqlyifencmd.ExecuteReader();
        decimal yifenjine = 0m;
        if (sqlyifendr.Read())
        {
            try
            {
                yifenjine = Convert.ToDecimal(sqlyifendr["jine"].ToString());
            }
            catch
            {
                yifenjine = 0m;
            }
        }
        sqlyifendr.Close();

        decimal shengyujine = daojine - yifenjine;

        string taskid = "";
        string kehuid = "";
        decimal fenxiangjine = 0m;
        decimal fenxiangshishoujine = 0m;
        string inid = "";
        foreach (GridViewRow gr in GridView2.Rows)
        {
            string bumen = "";
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");


            string sid = GridView2.DataKeys[gr.RowIndex].Value.ToString();

            if (hzf.Checked)
            {

                string sqlzhehou = "select feiyong,beizhu3,kehuid,taskid,shishou,shoufeibianhao from CeShiFeiKf where id='" + sid + "'";
                SqlCommand cmdzhehou = new SqlCommand(sqlzhehou, con);
                SqlDataReader drzhehou = cmdzhehou.ExecuteReader();
                if (drzhehou.Read())
                {
                    fenxiangjine = Convert.ToDecimal(drzhehou["feiyong"].ToString());
                    fenxiangshishoujine = Convert.ToDecimal(drzhehou["shishou"].ToString());
                    kehuid = drzhehou["kehuid"].ToString();
                    taskid = drzhehou["taskid"].ToString();
                    bumen = drzhehou["beizhu3"].ToString();
                    inid = drzhehou["shoufeibianhao"].ToString();
                }
                drzhehou.Close();
                decimal fenxiangshengyujine = fenxiangjine - fenxiangshishoujine;
                if (fenxiangshengyujine >= shengyujine)
                {
                    if (shengyujine > 0m)
                    {
                        fenxiangshishoujine = shengyujine + fenxiangshishoujine;

                        string ceshifekfsql = "update CeShiFeikf set heduibiaozhi='是',shishou='" + fenxiangshishoujine + "' where id='" + sid + "'";
                        string invoicesql = "update invoice set hesuanbiaozhi='是',hesuanriqi='" + DateTime.Now.ToShortDateString() + "',hesuanname='" + Session["UserName"].ToString() + "' where inid='" + inid + "'";
                        string cashinsql = "insert into cashin2 values('" + liushuihao + "','1','" + sid + "','" + inid + "','" + shengyujine + "',getdate(),'" + Session["UserName"].ToString() + "','','" + kehuid + "','','','','','','','" + bumen + "','0','" + pingzhenghao + "','" + taskid + "')";
                        SqlCommand addcashcmd = new SqlCommand(ceshifekfsql, con);
                        addcashcmd.ExecuteNonQuery();
                        addcashcmd = new SqlCommand(invoicesql, con);
                        addcashcmd.ExecuteNonQuery();
                        addcashcmd = new SqlCommand(cashinsql, con);
                        addcashcmd.ExecuteNonQuery();
                        shengyujine = 0m;
                    }
                    break;
                }
                else
                {
                    if (fenxiangshengyujine > 0m)
                    {
                        shengyujine = shengyujine - fenxiangshengyujine;
                        fenxiangshishoujine = fenxiangjine;
                        string ceshifekfsql = "update CeShiFeikf set heduibiaozhi='是',shishou='" + fenxiangshishoujine + "' where id='" + sid + "'";
                        string invoicesql = "update invoice set hesuanbiaozhi='是',hesuanriqi='" + DateTime.Now.ToShortDateString() + "',hesuanname='" + Session["UserName"].ToString() + "' where inid='" + inid + "'";
                        string cashinsql = "insert into cashin2 values('" + liushuihao + "','1','" + sid + "','" + inid + "','" + fenxiangshengyujine + "',getdate(),'" + Session["UserName"].ToString() + "','','" + kehuid + "','','','','','','','" + bumen + "','0','" + pingzhenghao + "','" + taskid + "')";
                        SqlCommand addcashcmd = new SqlCommand(ceshifekfsql, con);
                        addcashcmd.ExecuteNonQuery();
                        addcashcmd = new SqlCommand(invoicesql, con);
                        addcashcmd.ExecuteNonQuery();
                        addcashcmd = new SqlCommand(cashinsql, con);
                        addcashcmd.ExecuteNonQuery();
                    }
                }



            }
        }
        con.Close();
        BindCashin();


    }

    protected void BindCashin()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from cashin2 where daid ='" + liushuihao + "'   order by convert(datetime,riqi) desc";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        GridView3.DataSource = dr;
        GridView3.DataBind();
        con.Close();
    }
    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sid = GridView3.DataKeys[e.RowIndex].Value.ToString();
        string judgesql = "select * from cashin2 where id='" + sid + "' and tichenbiaozhi=''";
        SqlCommand judgecmd = new SqlCommand(judgesql, con);
        SqlDataReader judgead = judgecmd.ExecuteReader();
        if (judgead.Read())
        {
            string ceshifeikfid = judgead["xiangmuid2"].ToString();
            decimal jiesuanfeiyong = Convert.ToDecimal(judgead["xiaojine"].ToString());
            string inid = judgead["dengji2"].ToString();
            judgead.Close();
            string getceshifeikfsql = "select feiyong,shishou from ceshifeikf where id='" + ceshifeikfid + "'";
            SqlCommand getceshifeikfcmd = new SqlCommand(getceshifeikfsql, con);
            SqlDataReader getceshifeikfad = getceshifeikfcmd.ExecuteReader();
            decimal yingshoujine = 0m;
            decimal shishoujine = 0m;
            if (getceshifeikfad.Read())
            {
                yingshoujine = Convert.ToDecimal(getceshifeikfad["feiyong"].ToString());
                shishoujine = Convert.ToDecimal(getceshifeikfad["shishou"].ToString());
            }
            getceshifeikfad.Close();
            if (shishoujine == jiesuanfeiyong)
            {
                string sql1 = "delete from cashin2 where id='" + sid + "'";
                string sql2 = "update ceshifeikf set shishou=0.00,heduibiaozhi='否' where id='" + ceshifeikfid + "'";
                SqlCommand dealcomd = new SqlCommand(sql1, con);
                dealcomd.ExecuteNonQuery();
                dealcomd = new SqlCommand(sql2, con);
                dealcomd.ExecuteNonQuery();
                string judgeinvoicesql = "select * from ceshifeikf where shoufeibianhao='" + inid + "' and heduibiaozhi='是'";
                SqlCommand judgeinvoicecmd = new SqlCommand(judgeinvoicesql, con);
                SqlDataReader judgeinvoicead = judgeinvoicecmd.ExecuteReader();
                bool hadhedui = judgeinvoicead.Read();
                judgeinvoicead.Close();
                if (!hadhedui)
                {
                    string sql3 = "update invoice set hesuanbiaozhi='否' where inid='" + inid + "'";
                    dealcomd = new SqlCommand(sql3, con);
                    dealcomd.ExecuteNonQuery();
                }
            }
            else
            {
                decimal jianhoujine = shishoujine - jiesuanfeiyong;
                string sql1 = "delete from cashin2 where id='" + sid + "'";
                string sql2 = "update ceshifeikf set shishou=" + jianhoujine + " where id='" + ceshifeikfid + "'";
                SqlCommand dealcomd = new SqlCommand(sql1, con);
                dealcomd.ExecuteNonQuery();
                dealcomd = new SqlCommand(sql2, con);
                dealcomd.ExecuteNonQuery();
            }

            con.Close();
        }
        else
        {
            judgead.Close();
            con.Close();
            return;
        }
        if (selectedinvioce != "")
        {
            BindHesuanmingxi(selectedinvioce);
        }
        BindCashin();


    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text != "是")
            {
                e.Row.Cells[6].Text = "";

            }
        }
    }
    public void BindClaim()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from [dbo].[Claim] where liushuihao='" + liushuihao + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView4.DataSource = ds.Tables[0];
            GridView4.DataBind();
        }
    }
}