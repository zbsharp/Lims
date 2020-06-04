using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Income_daozhangrenling : System.Web.UI.Page
{
    string fukuanid;
    string fukuanren;
    bool ifenoughcash;
    decimal pandingjine;

    protected void Page_Load(object sender, EventArgs e)
    {
        ld.Text = "";
        //fukuanid = Request.QueryString["id"].ToString();
        //fukuanren = Request.QueryString["fukuanren"].ToString();
        //lbcurrency.Text = Request.QueryString["currency"].ToString();
        fukuanid = HttpUtility.UrlDecode(HttpContext.Current.Request["id"].ToString());
        //fukuanren = HttpUtility.UrlDecode(HttpContext.Current.Request["fukuanren"].ToString());
        //lbcurrency.Text = HttpUtility.UrlDecode(HttpContext.Current.Request["currency"].ToString());

        if (!IsPostBack)
        {
            //*使用fukuanid读取shuipiao对应的信息，
            //*select fukuanriqi,fukuanjine from shuipiao where id=fukuanid
            //*填到页面页面赋值如下：
            //*label2:fukuanriqi
            //*label3:fukuanjine
            //*读取该流水的已经认领金额
            //*select sum(money) as money from claim where cacellation=否 and shuipiaoid=fukuanid
            //*赋值到label5,如果金额为空，则置为0；label4值等于label3金额-label5金额;
            //*读取该流水已经认领并确认的金额：select sum(money) as money from claim where isaffirm=是 and cacellation=否 and shuipiaoid=fukuanid
            //*赋值到label6
            //*textbox1赋值fukuanren

            BindShuipiap();
            this.lbCusetomername.Text = fukuanren;
            BindLable();
            BindBaseSerch();  //绑定Gridview1
            BindClaimAdd();
        }
    }

    private void BindShuipiap()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select fukuanren,danwei from shuipiao where id='" + fukuanid + "'";
            SqlCommand command = new SqlCommand(sql, con);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                fukuanren = dataReader["fukuanren"].ToString();
                lbcurrency.Text = dataReader["danwei"].ToString();
            }
            dataReader.Close();
        }
    }

    /// <summary>
    /// 加载时给label赋值
    /// </summary>
    private void BindLable()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sqlshuipiao = "select fukuanriqi,fukuanjine from shuipiao where id='" + fukuanid + "'";
            SqlCommand cmdshuipiao = new SqlCommand(sqlshuipiao, con);
            SqlDataReader drshuipiao = cmdshuipiao.ExecuteReader();
            if (drshuipiao.Read())
            {
                lbpaymoney.Text = Convert.ToDecimal(drshuipiao["fukuanjine"]).ToString("f2");
                lbpaytime.Text = Convert.ToDateTime(drshuipiao["fukuanriqi"]).ToString("yyyy-MM-dd");
            }
            drshuipiao.Close();

            string sqlclaim = @"select sum(money) as money from claim where cancellation='否' and shuipiaoid='" + fukuanid + "' and ceshifeikfid "
                                + "in(select id from ceshifeikf where project = '检测费' or feiyong> 0)";
            SqlCommand cmdclaim = new SqlCommand(sqlclaim, con);
            SqlDataReader drclaim = cmdclaim.ExecuteReader();
            if (drclaim.Read())
            {
                //已分金额
                if (string.IsNullOrEmpty(drclaim["money"].ToString()) || drclaim["money"].ToString() == null)
                {
                    lballocatedmoney.Text = "0";
                }
                else
                {
                    lballocatedmoney.Text = Convert.ToDecimal(drclaim["money"].ToString()).ToString("f2");
                }
            }
            drclaim.Close();

            //未分金额
            lbnotpay.Text = (Convert.ToDecimal(lbpaymoney.Text) - Convert.ToDecimal(lballocatedmoney.Text)).ToString("f2");

            //已确认金额
            string sqlokmoney = @"select sum(money) as money from claim where isaffirm='是' and cancellation='否' and shuipiaoid='" + fukuanid + "'"
                                + "and ceshifeikfid in(select id from ceshifeikf where project = '检测费' or feiyong> 0)";
            SqlCommand cmdokmoney = new SqlCommand(sqlokmoney, con);
            SqlDataReader drokmoney = cmdokmoney.ExecuteReader();
            if (drokmoney.Read())
            {
                string a = drokmoney["money"].ToString();
                if (string.IsNullOrEmpty(drokmoney["money"].ToString()) || drokmoney["money"].ToString() == null)
                {
                    lbokmoney.Text = "0";
                }
                else
                {
                    lbokmoney.Text = Convert.ToDecimal(drokmoney["money"]).ToString("f2");
                }
            }
            drokmoney.Close();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //语句解析：显示为名下客户名/付款人/委托方/任务号/报价单号和输入框的任务信息
        //*string sql=select *,(selcct 客户名 from Customer where kehuid=anjinainfo2.kehuid) as kehuname,(select sum(feiyong) from ceshifeikf where taskid=anjianinfo2.rwbianhao) as jiancefeiyong from anjianinfo2 where (kehuid in(select kehuid from Customer where 
        //*customname like textbox1.text) or weituodanwei like textbox1.tex or fukuandanwei like textbox1.text or rwbianhao like textbox1.text or baojiaid like textbox1.text) and baojiaid in(select baojiaid from baojiabiao where resopnser=session[username])
        //*绑定到gridview1
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string customername = TextBox1.Text.Replace('\'', ' ').Trim();
            string sql = @"select *,(select currency from BaoJiaBiao where baojiaid=AnJianInFo2.baojiaid) as currency,
                        (select CustomName from Customer where kehuid=AnJianInFo2.kehuid) as kehuname,
                        (select SUM(feiyong) from CeShiFeiKf where taskid=AnJianInFo2.rwbianhao and project='检测费') as jiancefeiyong
                        from AnJianInFo2 where baojiaid in (select baojiaid from BaoJiaBiao where responser='" + Session["Username"].ToString() + "' and baojiaid in (select baojiaid from CeShiFeiKf where CeShiFeiKf.baojiaid=AnJianInFo2.baojiaid))"
                        + "and  (kehuid in (select kehuid from Customer where CustomName like '%" + this.TextBox1.Text.Trim() + "%') or (weituodanwei like '%" + this.TextBox1.Text.Trim() + "%' or fukuandanwei like '%" + this.TextBox1.Text.Trim() + "%') or rwbianhao like '%" + this.TextBox1.Text.Trim() + "%' or baojiaid like '%" + this.TextBox1.Text.Trim() + "%')"
                        + "and(baojiaid like 'FY%' or baojiaid like 'LH%') and rwbianhao like 'P%'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
    }

    protected void Button_addcash_Click(object sender, EventArgs e)
    {
        #region 陈工
        //*对gridview2中的每个输入框（本次分配金额）数值之和（保存到spitcashtotal）未分配金额对比，如果未分配金额为负数，则本次分配金额不能小于未分配金额且必须为负数；如果未分配金额为大于等于0，则本次分配金额不能大于未分配金额且必须为正数
        //*如果上述不满足，return，不继续往下执行，提醒输入分配金额有误，不能超过对应项目的未对金额
        //*判定gridview2中输入框列（本次分配金额）的金额之和(赋值到新变量inputtotalcash)是否小于或等于label4未分金额
        //*如果本次分配金额之和大于label4未分金额，则return，不往下执行，提示输入分配金额总和不能大于剩余金额；


        //注意上面几个renturn的判定，如果需要return ，本函数下面所有的代码都不需要执行了，下面两个foreach里的操作要使用事务进行提交，不能只执行一个，另一个不执行

        //*如果本次分配金额之和小于等于label4未分金额，可以继续往下执行

        //*将分配金额按照负数方式分配到自动项上
        //*如果spitcashtotal小于等于0，核算的项目金额之和为负数，不进行自动项的匹配
        //*如果spitcashtotal大于0，自动项要检查是否需要进行相应扣减；
        //*自动项操作开始
        //*foreach(row in gridview4)
        //*{
        //*if(row.未分配金额的绝对值<spitcashtotal)
        //*{
        //*insert claim:fukuanid,未分配金额(注意是负数)，datarowid，用户，当前时间,否，空值，空值，否，空值，空值，否，空值，自动项，空值
        //*spitcashtotal=spitcashtotal-rwo.未分配金额的绝对值
        //*}
        //*else
        //*{
        //*insert claim:fukuanid,spitcashtotal的负数(注意是负数)，datarowid，用户，当前时间,否，空值，空值，否，空值，空值，否，空值，自动项，空值
        //*break;这里是如果金额分配金额剩余已经不足自动项的分配了，在分配完最后一个的时候，就跳出foreach循环
        //*}
        //*}
        //*自动项操作结束

        //以下内容部不管是否有自动项操作，都要执行
        //*将数据插入到claim
        //*foreach(row in gridview2)
        //*{
        //*if(selected)
        //*插入claim表：fukuanid,分配金额，datarowid，用户，当前时间,否，空值，空值，否，空值，空值，否，空值，用户录入，空值
        //*}
        //*刷新gridveiw4、gridivew2（按照GridView1_RowCommand中的方法）
        //*刷新label4、label5的值（按照Page_Load中的方法）
        //*舒心gridviw3，调用bindclaimadd()方法
        #endregion
        int number = this.GridView2.Rows.Count;
        if (number == 0)
        {
            ld.Text = "<script>alert('请先按报价号获取详细信息')</script>";
            return;
        }

        //1.判断Gridview2的未分款金额和本次分款金额是否相匹配（本次分款金额只能小于或等于）
        //2.比较Gridview2的本次分款金额总和与lbnotpay的大小（本次分款金额只能小于或等于）
        bool b1 = true;
        decimal sum = 0;
        for (int i = 0; i < this.GridView2.Rows.Count; i++)
        {
            TextBox text = (TextBox)this.GridView2.Rows[i].FindControl("fenpeijine");
            if (string.IsNullOrEmpty(text.Text))
            {
                text.Text = "0";
            }
            sum += Convert.ToDecimal(text.Text);
            string weifen = this.GridView2.Rows[i].Cells[10].Text;
            if (Convert.ToDecimal(text.Text) > Convert.ToDecimal(weifen))
            {
                b1 = false;
                text.Text = string.Empty;
                break;
            }
        }
        if (sum > Convert.ToDecimal(lbnotpay.Text))
        {
            ld.Text = "<script>alert('分款金额之和不能大于本次到账未分款金额')</script>";
        }
        else if (b1 == false)
        {
            ld.Text = "<script>alert('存在分款金额大于未分金额')</script>";
        }
        else if (sum == 0)
        {
            ld.Text = "<script>alert('本次分款金额不能为0')</script>";
        }
        else
        {
            //添加认领记录  检测费
            string ceshifeikefuid = "";
            for (int i = 0; i < this.GridView2.Rows.Count; i++)
            {
                TextBox text = (TextBox)this.GridView2.Rows[i].FindControl("fenpeijine");
                if (string.IsNullOrEmpty(text.Text) || text.Text == "0")
                {
                    continue;
                }
                ceshifeikefuid = this.GridView2.DataKeys[i].Value.ToString();
                ClaimAdd(fukuanid, Convert.ToDecimal(text.Text), ceshifeikefuid);
            }

            //添加认领记录  扣减项
            decimal money = 0;
            for (int j = 0; j < this.GridView4.Rows.Count; j++)
            {
                decimal weifen = Convert.ToDecimal(this.GridView4.Rows[j].Cells[10].Text);

                if (Math.Abs(weifen) == 0)
                {
                    continue;
                }
                if (Math.Abs(weifen) >= sum)
                {
                    money = sum;
                    ceshifeikefuid = this.GridView4.DataKeys[j].Value.ToString();
                    ClaimAdd(fukuanid, -money, ceshifeikefuid);
                    break;//本次分款金额之和小于未分款金额则不要再计算下面的了
                }
                else
                {
                    money = weifen;

                    ceshifeikefuid = this.GridView4.DataKeys[j].Value.ToString();
                    ClaimAdd(fukuanid, money, ceshifeikefuid);

                    sum -= Math.Abs(money);
                    if (sum <= 0)
                    {
                        break;//表示该次分款金额已经对完
                    }
                }
            }

            BindLable();
            BindClaimAdd();
            ViewDateiled(this.Label1.Text);
            //ld.Text = "<script>alert('分款成功')</script>";

        }
    }

    private void ClaimAdd(string shuipiaoid, decimal money, string ceshifeikehuid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "insert into Claim values('" + shuipiaoid + "','" + money + "','" + ceshifeikehuid + "','" + Session["Username"].ToString() + "','" + DateTime.Now + "','否','','1900-1-1','否','','1900-1-1','否','','','')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region 不用了
        if (e.CommandName == "showtaskdetail")
        {
            //按任务号
            string selectcedtaskid = e.CommandArgument.ToString();
            //把非检测费扣减项用放到GridView4
            //*sql=select *,(select sum(money) from claim where ceshifeikfid=ceshifeikf.id and cancellation=否) as yifenkuan from ceshifeikf where taskid=selectedtaskid and (project=规费 or (project!=检测费 and feiyong<0))
            //*绑定gridview4

            //判定剩余金额是否足够本任务的对账，如果够，则直接冲账，如果不够，则要分摊
            //*先读取任务总应收
            //*judgesql=select sum(feiyong) as yingshoujine from ceshifeikf where taskid=selectedtaskid and (project=检测费 or feiyong>=0)
            //*select sum(moeny) as yirenlingjine from claim where cancallation=否 and ceshifeikfid in(select id from ceshifeikf where taskid=selectedtaskid and (project=检测费 or feiyong>=0)）
            //*shengyuweidui=yingshoujine-yirenlingjine
            //*shengyuweidui和label4金额比较，如果shengyuweidui大，表示不够，需要分摊，isenough的值设置为false；如果shengyuweidui小，表示足够对，可以冲账，isenough设置为true;
            //*pandingjine=lable4金额

            //pandingjine要用于gridview2的databound方法中，因此必须先赋值后再绑定gridview2;
            //isenough值要用到gridview2的databound方法中，因此必须先判定后再绑定gridview2;

            //把检测费用和增加项放到GridView2
            //*sql=select *,(select sum(money) from claim where ceshifeikfif=ceshifeikf.id and cancellation=否) as yifenkuan from ceshifeikf where taskid=selectedtaskid and (project=检测费 or feiyong>=0)
            //*绑定GridView2
        }
        #endregion
        if (e.CommandName == "showquotationdetail")
        {
            //按报价号selectedbaojaiid
            int index = int.Parse(e.CommandArgument.ToString());
            string selectedbaojaiid = this.GridView1.Rows[index].Cells[1].Text.ToString();
            string currency = this.GridView1.Rows[index].Cells[5].Text.ToString();
            //把非检测费扣减项用放到GridView4
            //*sql=select *,(select sum(money) from claim where ceshifeikfif=ceshifeikf.id and cancellation=否) as yifenkuan from ceshifeikf where baojiaid=selectedbaojiaid and (project=规费 or (project!=检测费 and feiyong<0))
            //*绑定gridview4
            //*判定剩余金额是否足够本报价的对账，如果够，则直接冲账，如果不够，则要分摊
            //*先读取任务总应收
            //*judgesql=select sum(feiyong) as yingshoujine from ceshifeikf where baojiaid=selectedbaojiaid and (project=检测费 or feiyong>=0)
            //*select sum(moeny) as yirenlingjine from claim where cancallation=否 and ceshifeikfid in(select id from ceshifeikf where baojiaid=selectedbaojiaid and (project=检测费 or feiyong>=0)）
            //*shengyuweidui=yingshoujine-yirenlingjine
            //*shengyuweidui和label4金额比较，如果shengyuweidui大，表示不够，需要分摊，isenough的值设置为false；如果shengyuweidui小，表示足够对，可以冲账，isenough设置为true;
            //*pandingjine=lable4金额
            //pandingjine要用于gridview2的databound方法中，因此必须先赋值后再绑定gridview2;
            //isenough值要用到gridview2的databound方法中，因此必须先判定后再绑定gridview2
            //把检测费用和增加项放到GridView2
            //*sql=select *,(select sum(money) from claim where ceshifeikfif=ceshifeikf.id and cancellation=否) as yifenkuan from ceshifeikf where baojiaid=selectedbaojiaid and (project=检测费 or feiyong>=0)
            //*绑定GridView2
            if (currency == lbcurrency.Text)
            {
                ViewDateiled(selectedbaojaiid);
                Label1.Text = selectedbaojaiid;
            }
            else
            {
                ld.Text = "<script>alert('报价币种与财务到款币种不一致')</script>";
            }
        }
    }

    private void ViewDateiled(string baojiaid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql4 = @"select *,
                                (select SUM(money) from Claim where isaffirm='是' and cancellation='否' and ceshifeikfid=CeShiFeiKf.id) as okmoney,
                                (select SUM(money) from Claim where cancellation='否' and ceshifeikfid=CeShiFeiKf.id) as yifenkuan
                                from CeShiFeiKf where baojiaid='" + baojiaid + "' and (project='规费' or (project!='检测费' and feiyong<0))";
            SqlDataAdapter da4 = new SqlDataAdapter(sql4, con);
            DataSet ds4 = new DataSet();
            da4.Fill(ds4);
            this.GridView4.DataSource = ds4.Tables[0];
            this.GridView4.DataBind();
            //查询出该任务下的应收金额总和
            decimal yingshou = 0;
            string sqlyingshou = "select sum(feiyong) as yingshoujine from ceshifeikf where baojiaid='" + baojiaid + "' and (project='检测费' or feiyong>=0)";
            SqlCommand cmdyingshou = new SqlCommand(sqlyingshou, con);
            SqlDataReader dryingshou = cmdyingshou.ExecuteReader();
            if (dryingshou.Read())
            {
                if (dryingshou["yingshoujine"] == DBNull.Value)
                {
                    yingshou = 0;
                }
                else
                {
                    yingshou = Convert.ToDecimal(dryingshou["yingshoujine"]);
                }
            }
            dryingshou.Close();
            //查询出该任务下的已分金额总和
            decimal yifen = 0;
            string sqlyifen = "select sum(money) as yirenlingjine from claim where cancellation='否' and ceshifeikfid in(select id from ceshifeikf where baojiaid='" + baojiaid + "' and (project='检测费' or feiyong>=0))";
            SqlCommand cmdyifen = new SqlCommand(sqlyifen, con);
            SqlDataReader dryifen = cmdyifen.ExecuteReader();
            if (dryifen.Read())
            {
                if (dryifen["yirenlingjine"] == DBNull.Value)
                {
                    yifen = 0;
                }
                else
                {
                    yifen = Convert.ToDecimal(dryifen["yirenlingjine"]);
                }
            }
            dryifen.Close();

            decimal weifen = yingshou - yifen;
            if (weifen <= Convert.ToDecimal(lbnotpay.Text))
            {
                ifenoughcash = true;
            }
            else
            {
                ifenoughcash = false;
            }

            string sql2 = @"select *,
                                (select SUM(money) from Claim where isaffirm='是' and cancellation='否' and ceshifeikfid=CeShiFeiKf.id) as okmoney,
                                (select SUM(money) from Claim where cancellation='否' and ceshifeikfid=CeShiFeiKf.id) as yifenkuan
                                from CeShiFeiKf where baojiaid='" + baojiaid + "' and (project='检测费' or feiyong>=0)";
            SqlDataAdapter da2 = new SqlDataAdapter(sql2, con);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            this.GridView2.DataSource = ds2.Tables[0];
            this.GridView2.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //暂时不需要代码
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //*来源列为自动项时，删除按钮不可操作，为灰色
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string project = e.Row.Cells[4].Text.ToString();
            if (project == "规费" || e.Row.Cells[8].Text != "否")
            {
                e.Row.Cells[this.GridView3.Columns.Count - 1].Visible = false;
            }
            if (e.Row.Cells[10].Text == "1900-01-01" || e.Row.Cells[10].Text.Trim() == "1900/1/1")
            {
                e.Row.Cells[10].Text = "";
            }
            if (e.Row.Cells[13].Text == "1900-01-01" || e.Row.Cells[13].Text.Trim() == "1900/1/1")
            {
                e.Row.Cells[13].Text = "";
            }
        }
    }
    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        #region  陈工

        //*如果该项目的claim表中的issubmit为是，即已经提交了，则不能删除，否则可以删除名下的认领
        //以下所有删除都要在同一个事务里执行


        //需要将自动项同步更新
        //*将要删除的项目对应流水的自动项查出
        //先找到该项目对应的baojiaid：select baojiaid as baojiaidget from ceshifeikf where id=(select ceshifeikfid from claim where id=数据id)
        //*sql=select * from claim where beizhu1=自动项 and issubmit=否 and cancallation=否 and shuipiaoid=fukuanid and ceshifeikfid in(select id from ceshifeikf where baojiaid=baojiaidget) order by id desc
        //*将以上查询结果存到datatable:zidongxiangliebiao中
        //*查出要删除项的金额：select money as itemmoney from claim where id=数据id



        //*如果itemmonye大于0，则进行数据判定，按照以下执行：
        //*for(int i=0;i<zidongxiangleibiao.row.cout;i++)
        //*{
        //*if(row.money绝对值<itemmoney)
        //*{
        //*delete from claim where id=row.id
        //*itemmoney=itemmoney-row.money绝对值
        //*}
        //*else
        //*{
        //*update claim set moeny=(money绝对值-itemmony)的负数 where id=row.id
        //*break
        //*}
        //*delete from calim where id=数据id and issubmit=否 and fillname=用户 and beizhu1!=自动项 and cancallation=否



        //*如果itemmonye小于0,则只删除非自动项,按照以下执行
        //*delete from calim where id=数据id and issubmit=否 and fillname=用户 and beizhu1!=自动项 and cancallation=否


        //以下不管哪种方式，都要执行
        //*刷新gridveiw4、gridivew2（按照GridView1_RowCommand中的方法）
        //*刷新label4、label5的值（按照Page_Load中的方法）
        //*舒心gridviw3，调用bindclaimadd()方法
        #endregion
        //已提交的不能删除
        string state = this.GridView3.Rows[e.RowIndex].Cells[8].Text.ToString();

        Label1.Text = this.GridView3.Rows[e.RowIndex].Cells[1].Text;//用于保存当前操作的报价编号，用于加载ViewDateiled方法

        if (state == "是")
        {
            ld.Text = "<script>alert('已提交的不能删除')</script>";
        }
        else
        {
            int id = Convert.ToInt32(this.GridView3.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = " delete Claim where id=" + id + "";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                //查询该报价下是否有未提交的规费记录
                string baojiaid = this.GridView3.Rows[e.RowIndex].Cells[1].Text.ToString();
                decimal money = Convert.ToDecimal(this.GridView3.Rows[e.RowIndex].Cells[14].Text.ToString());
                string sqlselect = @"select * from Claim where ceshifeikfid in (select id from CeShiFeiKf where baojiaid='" + baojiaid + "'  and project='规费') "
                                     + "and issubmit = '否' and cancellation = '否'";
                SqlCommand cmdselect = new SqlCommand(sqlselect, con);
                SqlDataReader drselect = cmdselect.ExecuteReader();
                while (drselect.Read())
                {
                    string claimid = drselect["id"].ToString();
                    decimal claimmoney = Convert.ToDecimal(drselect["money"].ToString());
                    if (Math.Abs(claimmoney) > money)
                    {
                        //修改Claim表的money
                        decimal money1 = Math.Abs(claimmoney) - money;
                        ClaimUpdate(claimid, -money1);
                        break;
                    }
                    else
                    {
                        //删除
                        money -= Math.Abs(claimmoney);
                        ClaimDelete(claimid);
                    }
                }
                drselect.Close();
            }

            BindClaimAdd();
            BindLable();

            ViewDateiled(this.Label1.Text);

        }
    }

    /// <summary>
    /// 删除认领记录时需要删除对应认领记录
    /// </summary>
    /// <param name="claimid"></param>
    private void ClaimDelete(string claimid)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "delete Claim where id=" + claimid + " and issubmit='否' and cancellation='否'";
            SqlCommand command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// 删除认领记录时需要修改对应认领记录
    /// </summary>
    /// <param name="claimid"></param>
    /// <param name="money"></param>
    private void ClaimUpdate(string claimid, decimal money)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = " update Claim set money=" + money + " where id=" + claimid + " and issubmit= '否' and cancellation= '否'";
            SqlCommand command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// 绑定Gridview1
    /// </summary>
    protected void BindBaseSerch()
    {
        //语句解析：默认显示为名下客户名/付款人/委托方的名称和实际付款人一样，且未完成对账的任务信息
        //*string sql=select *,(selcct 客户名 from Customer where kehuid=anjinainfo2.kehuid) as kehuname,(select sum(feiyong) from ceshifeikf where taskid=anjianinfo2.rwbianhao and (project=检测费 or feiyong>0)) as jiancefeiyong from anjianinfo2 where (kehuid in(select kehuid from Customer where 
        //*customname=textbox1.text) or weituodanwei=textbox1.tex or fukuandanwei=textbox1.text) and baojiaid in(select baojiaid from baojiabiao where resopnser=session[username])
        //*and rwbianhao in(select taskid from ceshifeikf where feiyong!=shishou)
        //*绑定到gridview1
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = @"select *,(select currency from BaoJiaBiao where baojiaid=AnJianInFo2.baojiaid) as currency,
                            (select CustomName from Customer where kehuid=AnJianInFo2.kehuid) as kehuname,
                            (select SUM(feiyong) from CeShiFeiKf where taskid=AnJianInFo2.rwbianhao and project='检测费') as jiancefeiyong
                            from AnJianInFo2 where  baojiaid in (select baojiaid from BaoJiaBiao where responser='" + Session["Username"].ToString() + "') "
                           + " and  (kehuid in (select kehuid from Customer where CustomName='" + lbCusetomername.Text + "') or (weituodanwei='" + lbCusetomername.Text + "' or fukuandanwei='" + lbCusetomername.Text + "'))"
                           + " and (baojiaid like 'FY%' or baojiaid like 'LH%') and rwbianhao like 'P%'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
    }

    protected void BindClaimAdd()
    {
        //将本到款的本人认领的金额显示到GridView3列表中
        //select * from claim left join ceshifeikf on claim.ceshifeikfid=ceshifeikf.id where shuipiao=fukuanid and claim.fillname=用户 and cancellation=否
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = @"select * from claim left join ceshifeikf on claim.ceshifeikfid=ceshifeikf.id where claim.shuipiaoid='" + fukuanid + "'"
                            + "and claim.fillname = '" + Session["Username"].ToString() + "' and cancellation = '否'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.GridView3.DataSource = ds.Tables[0];
            this.GridView3.DataBind();
        }
    }

    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //*如果e.row.cells[7]、e.row.cells[8]、e.rows.cells[9]即应收，已对金额，已分款金额为空时，用0代替；
            // e.Row.Cells[10].Text = ((Convert.ToDecimal(e.Row.Cells[7].Text) - Convert.ToDecimal(e.Row.Cells[9].Text)).ToString("f2"));
            if (e.Row.Cells[8].Text == null || string.IsNullOrEmpty(e.Row.Cells[8].Text) || e.Row.Cells[8].Text == "&nbsp;")
            {
                e.Row.Cells[8].Text = "0";
            }

            if (e.Row.Cells[9].Text == null || string.IsNullOrEmpty(e.Row.Cells[9].Text) || e.Row.Cells[9].Text == "&nbsp;")
            {
                e.Row.Cells[9].Text = "0";
            }
            e.Row.Cells[10].Text = (Convert.ToDecimal(e.Row.Cells[7].Text) - Convert.ToDecimal(e.Row.Cells[9].Text)).ToString("f2");
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //*如果e.row.cells[7]、e.row.cells[8]、e.rows.cells[9]即应收，已对金额，已分款金额为空时，用0代替；
        //    //*e.row.cells[10]的值等于e.row.cells[7]金额-e.row.cells[9]金额
        //    //如果金额足够该列表扣减的时候，把每项的剩余款默认写上
        //    if (ifenoughcash)
        //    {
        //        TextBox textboxinputcash = (TextBox)e.Row.Cells[11].FindControl("fenpeijine");
        //        textboxinputcash.Text = ((Convert.ToDecimal(e.Row.Cells[7].Text) - Convert.ToDecimal(e.Row.Cells[9].Text)).ToString("f2"));
        //    }
        //    else
        //    {
        //        if (pandingjine > 0m)
        //        {
        //            //如果金额不够该列表扣减的时候，默认按照从第一个项目分开始往后分派金额，到金额分派完不再分派
        //            if (pandingjine > (Convert.ToDecimal(e.Row.Cells[7].Text) - Convert.ToDecimal(e.Row.Cells[9].Text)))
        //            {
        //                TextBox textboxinputcash = (TextBox)e.Row.Cells[11].FindControl("fenpeijine");
        //                textboxinputcash.Text = ((Convert.ToDecimal(e.Row.Cells[7].Text) - Convert.ToDecimal(e.Row.Cells[9].Text)).ToString("f2"));
        //                pandingjine = pandingjine - Convert.ToDecimal(textboxinputcash.Text);
        //            }
        //            else
        //            {
        //                TextBox textboxinputcash = (TextBox)e.Row.Cells[11].FindControl("fenpeijine");
        //                textboxinputcash.Text = pandingjine.ToString("f2");
        //                pandingjine = 0m;
        //            }
        //        }
        //    }
        //}
        #endregion

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[9].Text == null || string.IsNullOrEmpty(e.Row.Cells[9].Text) || e.Row.Cells[9].Text == "&nbsp;")
            {
                e.Row.Cells[9].Text = "0";
            }

            if (e.Row.Cells[8].Text == null || string.IsNullOrEmpty(e.Row.Cells[8].Text) || e.Row.Cells[8].Text == "&nbsp;")
            {
                e.Row.Cells[8].Text = "0";
            }

            e.Row.Cells[10].Text = (Convert.ToDecimal(e.Row.Cells[7].Text) - Convert.ToDecimal(e.Row.Cells[9].Text)).ToString("f2");

            if (ifenoughcash == true)
            {
                TextBox textBox = (TextBox)e.Row.Cells[11].FindControl("fenpeijine");
                textBox.Text = e.Row.Cells[10].Text;
            }
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        #region 陈工
        //*随机生成一个批次号（根据日期、fukuanid，随机数联合生成）
        //*执行sql:update claim set issubmit=是,submittime=当前,submitren=用户,batch=批次号 where shuipiaoid=fukuanid and fillname=用户 and issubmit=否 and cancellation=否
        //*刷新gridveiw4、gridivew2（按照GridView1_RowCommand中的方法）
        //*刷新label4、label5、label6的值（按照Page_Load中的方法）
        //*舒心gridviw3，调用bindclaimadd()方法
        #endregion
        int number = this.GridView3.Rows.Count;
        if (number == 0)
        {
            return;
        }
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();

            string submit = "";
            string claimid = "";
            Random random = new Random();
            string batch = DateTime.Now.ToString("yyyy_MM_dd hms") + fukuanid + random.Next(1000 + DateTime.Now.Second);
            int i = 0;
            for (; i < this.GridView3.Rows.Count; i++)
            {
                submit = this.GridView3.Rows[i].Cells[8].Text.ToString();
                if (submit == "是")
                {
                    continue;
                }
                claimid = this.GridView3.DataKeys[i].Value.ToString();
                string sql = " update claim set issubmit = '是', submittime = '" + DateTime.Now + "', submitren = '" + Session["Username"].ToString() + "', batch = '" + batch + "' where id = '" + claimid + "' and shuipiaoid = '" + fukuanid + "' and fillname = '" + Session["Username"].ToString() + "' and issubmit = '否' and cancellation = '否'";
                SqlCommand command = new SqlCommand(sql, con);
                command.ExecuteNonQuery();
            }

            BindLable();
            BindClaimAdd();

            if (string.IsNullOrEmpty(this.Label1.Text))
            {
                string baojiaid = this.GridView3.Rows[i - 1].Cells[1].Text;
                ViewDateiled(baojiaid);
            }
            else
            {
                ViewDateiled(this.Label1.Text);
            }

        }
    }
}