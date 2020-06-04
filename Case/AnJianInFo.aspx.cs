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
using System.Collections.Generic;

public partial class Case_anjianinfo : System.Web.UI.Page
{

    private const int _firstEditCellIndex = 2;
    protected string xiangmuid = "";
    protected string shenpibiaozhi = "";
    protected string bumen = "";
    protected string st = "进行中";
    protected string baojiaid = "";
    protected string kehuid = "";
    protected string anjianinfoid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.Attributes.Add("style", "table-layout:fixed");
        xiangmuid = Request.QueryString["xiangmuid"].ToString();
        anjianinfoid = Request.QueryString["anjianinfoid"].ToString();

        //Button7.Attributes.Add("onclick", "return confirm('您确定需要修改该任务为完成状态吗，目前仅针对EMC任务有效？')");
        baojiaid = Request.QueryString["id"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select bumen from anjianinfo where id='" + baojiaid + "'";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            bumen = dr["bumen"].ToString();
        }
        dr.Close();
        con.Close();
        {
            Literal1.Text = "";
            if (!IsPostBack)
            {
                BindDep();
                BindAnJianInfo2();
                BindZhujianengineer();
                BindXiangmu();
                ProjectItem();
            }
            if (this.GridView1.SelectedIndex > -1)
            {
                {
                    this.GridView1.UpdateRow(this.GridView1.SelectedIndex, false);
                }
            }
            Bindbeizhu();
        }
        con.Close();
        if (bumen == "")
        {
            Button1.Visible = false;
            Button2.Visible = false;
            Button6.Visible = false;

        }
    }

    //protected void BindDep1()
    //{
    //    SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
    //    con3.Open();
    //    string sql = "select * from chaoqi ";


    //    SqlDataAdapter ad = new SqlDataAdapter(sql, con3);


    //    DataSet ds = new DataSet();


    //    ad.Fill(ds);





    //    DropDownList3.DataSource = ds.Tables[0];
    //    DropDownList3.DataValueField = "name";
    //    DropDownList3.DataTextField = "name";
    //    DropDownList3.DataBind();
    //    con3.Close();
    //    DropDownList3.Items.Insert(0, new ListItem("", ""));//

    //}



    protected void BindDep()
    {
        //**************2019-8-23修改
        //SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con3.Open();
        //string sql = "select * from UserDepa";
        //SqlDataAdapter ad = new SqlDataAdapter(sql, con3);
        //DataSet ds = new DataSet();
        //ad.Fill(ds);
        //DropDownList1.DataSource = ds.Tables[0];
        //DropDownList1.DataValueField = "name";
        //DropDownList1.DataTextField = "name";
        //DropDownList1.DataBind();
        //DropDownList1.SelectedValue = bumen;
        //con3.Close();
        txt_deap.Text = bumen;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql_test = "select UserName from UserInfo where departmentid in(select DepartmentId from UserDepa where Name='" + txt_deap.Text + "') and dutyname like '工程%'";
            SqlDataAdapter da = new SqlDataAdapter(sql_test, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DropDownList2.DataSource = ds;
            DropDownList2.DataTextField = "UserName";
            DropDownList2.DataValueField = "UserName";
            DropDownList2.DataBind();
        }
    }
    protected void BindAnJianInfo2()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *  from AnJianInfo2  where rwbianhao='" + xiangmuid + "' order  by id desc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView3.DataSource = ds.Tables[0];
        GridView3.DataBind();
        con.Close();
    }

    protected void BindZhujianengineer()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select *  from Zhujianengineer  where bianhao='" + xiangmuid + "' and  bumen='" + txt_deap.Text.Trim() + "' order  by id desc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView4.DataSource = ds.Tables[0];
        GridView4.DataBind();
        con.Close();
    }
    public void Bind()
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();
        //string sql = "select * from anjianxinxi where xiangmuid='" + xiangmuid + "' ";

        //SqlDataAdapter da = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //con.Close();
        //con.Dispose();
        //GridView1.DataSource = ds.Tables[0];
        //GridView1.DataBind();

    }
    public void Bindbeizhu()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from anjianbeizhu where xiangmuid='" + xiangmuid + "'";
        //string sql = "select * from studentInfo";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the LinkButton control in the first cell
            LinkButton _singleClickButton = (LinkButton)e.Row.Cells[0].Controls[0];
            // Get the javascript which is assigned to this LinkButton
            string _jsSingle = ClientScript.GetPostBackClientHyperlink(_singleClickButton, "");

            // Add events to each editable cell
            for (int columnIndex = _firstEditCellIndex; columnIndex < e.Row.Cells.Count - 1; columnIndex++)
            {
                // Add the column index as the event argument parameter
                string js = _jsSingle.Insert(_jsSingle.Length - 2, columnIndex.ToString());
                // Add this javascript to the onclick Attribute of the cell
                e.Row.Cells[columnIndex].Attributes["onclick"] = js;
                // Add a cursor style to the cells
                e.Row.Cells[columnIndex].Attributes["style"] += "cursor:pointer;cursor:hand;";
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();

            DropDownList goodsType = (DropDownList)e.Row.FindControl("gongchengshi");
            if (goodsType != null)
            {

                DataSet ds = new DataSet();

                string sql = "";
                //if (DropDownList2.SelectedItem.Text == "化学")
                //{
                //    sql = "select * from userinfo where departmentname='化学'";
                //}
                //else if (DropDownList2.SelectedItem.Text == "安规")
                //{
                //    sql = "select * from userinfo where departmentname='安规'";
                //}
                //else if (DropDownList2.SelectedItem.Text == "EMC")
                //{
                //    sql = "select * from userinfo where departmentname='EMC'";
                //}

                sql = "select * from userinfo";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(ds, "gt");

                goodsType.DataSource = ds.Tables["gt"];
                goodsType.DataTextField = "username";
                goodsType.DataValueField = "username";
                goodsType.DataBind();
            }

            DropDownList goodsType1 = (DropDownList)e.Row.FindControl("renwuname");
            if (goodsType1 != null)
            {

                DataSet ds1 = new DataSet();
                SqlDataAdapter da1 = new SqlDataAdapter("select * from anjiandropdown", con);
                da1.Fill(ds1, "gt1");

                goodsType1.DataSource = ds1.Tables["gt1"];
                goodsType1.DataTextField = "name";
                goodsType1.DataValueField = "value";
                goodsType1.DataBind();
            }
            DropDownList goodsType2 = (DropDownList)e.Row.FindControl("zhuangtai");
            if (goodsType2 != null)
            {

                DataSet ds2 = new DataSet();
                SqlDataAdapter da2 = new SqlDataAdapter("select * from state", con);
                da2.Fill(ds2, "gt2");

                goodsType2.DataSource = ds2.Tables["gt2"];
                goodsType2.DataTextField = "name";
                goodsType2.DataValueField = "name";
                goodsType2.DataBind();
            }

            con.Close();

            //((TextBox)e.Row.Cells[3].FindControl("shebei2")).Attributes.Add("onclick", "return Show('" + ((TextBox)e.Row.Cells[2].FindControl("shebei2")).ClientID + "')");
            ((TextBox)e.Row.Cells[3].FindControl("shebei2")).Attributes.Add("onclick", "popUpCalendar(this," + ((TextBox)e.Row.Cells[3].FindControl("shebei2")).ClientID + ",'yyyy-mm-dd')");
            ((TextBox)e.Row.Cells[4].FindControl("shebei3")).Attributes.Add("onclick", "popUpCalendar(this," + ((TextBox)e.Row.Cells[4].FindControl("shebei3")).ClientID + ",'yyyy-mm-dd')");
            ((TextBox)e.Row.Cells[5].FindControl("beizhu4")).Attributes.Add("onclick", "popUpCalendar(this," + ((TextBox)e.Row.Cells[5].FindControl("beizhu4")).ClientID + ",'yyyy-mm-dd')");
            ((TextBox)e.Row.Cells[6].FindControl("shebei4")).Attributes.Add("onclick", "popUpCalendar(this," + ((TextBox)e.Row.Cells[6].FindControl("shebei4")).ClientID + ",'yyyy-mm-dd')");
        }
    }


    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            if (((DropDownList)e.Row.FindControl("ddlXL")) != null)
            {
                DropDownList goodsType = (DropDownList)e.Row.FindControl("ddlXL");
                if (goodsType != null)
                {
                    DataSet ds = new DataSet();
                    string sql_test = "select realname,UserName from UserInfo where departmentid in(select DepartmentId from UserDepa where Name='" + txt_deap.Text + "') and dutyname like '工程%'";
                    SqlDataAdapter da = new SqlDataAdapter(sql_test, con);
                    da.Fill(ds, "gt");
                    goodsType.DataSource = ds.Tables["gt"];
                    goodsType.DataTextField = "realname";
                    goodsType.DataValueField = "UserName";
                    goodsType.DataBind();
                    goodsType.Items.Insert(0, new ListItem("", ""));//
                }
            }
            con.Close();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;

        switch (e.CommandName)
        {
            case ("SingleClick"):

                int _rowIndex = int.Parse(e.CommandArgument.ToString());

                int a = _gridView.Rows.Count;

                int _columnIndex = int.Parse(Request.Form["__EVENTARGUMENT"]);
                // Set the Gridview selected index
                _gridView.SelectedIndex = _rowIndex;

                _gridView.DataBind();

                Control _displayControl = _gridView.Rows[_rowIndex].Cells[_columnIndex].Controls[1];
                _displayControl.Visible = false;
                // Get the edit control for the selected cell and make it visible
                Control _editControl = _gridView.Rows[_rowIndex].Cells[_columnIndex].Controls[3];
                _editControl.Visible = true;
                // Clear the attributes from the selected cell to remove the click event
                _gridView.Rows[_rowIndex].Cells[_columnIndex].Attributes.Clear();

                // Set focus on the selected edit control
                ClientScript.RegisterStartupScript(GetType(), "SetFocus",
                    "<script>document.getElementById('" + _editControl.ClientID + "').focus();</script>");
                // If the edit control is a dropdownlist set the
                // SelectedValue to the value of the display control
                if (_editControl is DropDownList && _displayControl is Label)
                {
                    ((DropDownList)_editControl).SelectedValue = ((Label)_displayControl).Text;
                }
                // If the edit control is a textbox then select the text
                if (_editControl is TextBox)
                {
                    ((TextBox)_editControl).Attributes.Add("onfocus", "this.select()");
                    //((TextBox)_editControl).Attributes.Add("onclick", "popUpCalendar('" + ((TextBox)e.Row.Cells[2].FindControl("shebei2")).ClientID + "','yyyy-mm-dd')");

                }
                //_gridView.DataBind();
                break;
        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        string shenpibiaozhi1 = "";
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //con.Open();
        //string sql = "select * from anjianinfo where xiangmuid='" + xiangmuid + "'";
        ////string sql = "select * from studentInfo";
        //SqlDataAdapter da = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //da.Fill(ds);

        //if (ds.Tables[0].Rows.Count > 0)
        //{

        //    shenpibiaozhi1 = ds.Tables[0].Rows[0]["beizhu2"].ToString();

        //}
        //else
        //{

        //    shenpibiaozhi1 = "";

        //}
        //con.Close();


        //if (shenpibiaozhi1 == "已提交")
        //{

        //}
        //else
        {



            GridView _gridView = (GridView)sender;
            string key = "";
            string value = "";

            // The keys for the NewValues collection
            string[] _columnKeys = new string[] { "renwuname", "gongchengshi", "kaishiriqi", "jihuajieshu", "beizhu4", "shijijieshu", "zhuangtai", "beizhu", "beizhu3" };

            if (e.RowIndex > -1)
            {
                // Loop though the columns
                for (int i = _firstEditCellIndex; i < _gridView.Columns.Count - 1; i++)
                {
                    // Get the controls in the cell
                    Control _displayControl = _gridView.Rows[e.RowIndex].Cells[i].Controls[1];
                    Control _editControl = _gridView.Rows[e.RowIndex].Cells[i].Controls[3];

                    // Get the column key
                    key = _columnKeys[i - _firstEditCellIndex];

                    // If a cell in edit mode get the value of the edit control
                    if (_editControl.Visible)
                    {
                        if (_editControl is TextBox)
                        {
                            value = ((TextBox)_editControl).Text;
                        }
                        else if (_editControl is DropDownList)
                        {
                            value = ((DropDownList)_editControl).SelectedValue;
                        }

                        // Add the key/value pair to the NewValues collection
                        e.NewValues.Add(key, value);
                    }
                    // else get the value of the display control
                    else
                    {
                        value = ((Label)_displayControl).Text.ToString();

                        // Add the key/value pair to the NewValues collection
                        e.NewValues.Add(key, value);
                    }
                }
                // Clear the selected index to prevent 
                // another update on the next postback
                _gridView.SelectedIndex = -1;
            }
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        // The client events for GridView1 were created in GridView1_RowDataBound
        //foreach (GridViewRow r in GridView1.Rows)
        //{
        //    if (r.RowType == DataControlRowType.DataRow)
        //    {
        //        for (int columnIndex = _firstEditCellIndex; columnIndex < r.Cells.Count; columnIndex++)
        //        {
        //            Page.ClientScript.RegisterForEventValidation(r.UniqueID + "$ctl00", columnIndex.ToString());
        //        }
        //    }
        //}

        base.Render(writer);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "insert into anjianxinxi values('" + xiangmuid + "','','','','','','','','','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','','','','')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            GridView1.DataBind();
            con.Close();
            con.Dispose();

            BindZhujianengineer();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }


    protected void Button6_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            con.Open();
            string sql = "insert into anjianbeizhu values('" + xiangmuid + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + TextBox7.Text + "','分派工程师')";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            GridView2.DataSource = dr;
            GridView2.DataBind();
            con.Close();
            con.Dispose();
            Bindbeizhu();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        Response.Redirect("baogaoshouye.aspx?xiangmuid=" + xiangmuid);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //判断是否选中了测试项目
        int projectitems = 0;
        foreach (GridViewRow item in GridView5.Rows)
        {
            CheckBox chk = (CheckBox)item.Cells[5].FindControl("CheckBox1");
            if (chk.Checked)
            {
                projectitems++;
            }
        }

        if (projectitems > 0)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                con.Open();
                //判断该测试项目是否经过了前处理
                bool bu = false;//判断是否需前处理
                string sql_bumne = "select * from DepartmentType where department='" + txt_deap.Text + "' and Type='需前处理'";
                SqlCommand cmd_bumen = new SqlCommand(sql_bumne, con);
                SqlDataReader dr_bumen = cmd_bumen.ExecuteReader();
                if (dr_bumen.Read())
                {
                    dr_bumen.Close();
                    string sql_dispose = "select * from [dbo].[pretreatment] where anjianinfoid='" + anjianinfoid + "'";
                    SqlCommand cmd_dispose = new SqlCommand(sql_dispose, con);
                    SqlDataReader dr_dispose = cmd_dispose.ExecuteReader();
                    if (dr_dispose.Read())
                    {
                        dr_dispose.Close();
                    }
                    else
                    {
                        dr_dispose.Close();
                        bu = true;
                    }
                }
                else
                {
                    dr_bumen.Close();
                }
                if (bu == false)
                {
                    string sql = "insert into ZhuJianEngineer values('" + baojiaid + "','" + kehuid + "','" + xiangmuid + "','','" + txt_deap.Text + "','" + DropDownList2.SelectedValue + "','','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','否')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    GridView2.DataSource = dr;
                    GridView2.DataBind();
                    dr.Close();

                    string sqlstate = "insert into  TaskState values ('" + xiangmuid + "','" + xiangmuid + "','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','分派工程师','客服受理任务生成案件号')";
                    SqlCommand cmdstate = new SqlCommand(sqlstate, con);
                    cmdstate.ExecuteNonQuery();

                    //向工程师项目表中插数据
                    foreach (GridViewRow item in GridView5.Rows)
                    {
                        CheckBox cb = (CheckBox)item.Cells[5].FindControl("CheckBox1");
                        if (cb.Checked)
                        {
                            int index = item.RowIndex;
                            string xmid = GridView5.Rows[index].Cells[0].Text.ToString();
                            string xmname = GridView5.Rows[index].Cells[2].Text.ToString();
                            string bumen = GridView5.Rows[index].Cells[5].Text.ToString();
                            string sql_ProjectItem = "insert into ProjectItem values('" + xmid + "','" + xmname + "','" + xiangmuid + "','" + DropDownList2.SelectedValue + "','进行中','" + Session["Username"].ToString() + "','" + DateTime.Now + "','" + bumen + "','','')";
                            SqlCommand com_projectitem = new SqlCommand(sql_ProjectItem, con);
                            int i = com_projectitem.ExecuteNonQuery();
                            if (i > 0)
                            {
                                BindXiangmu();
                                ProjectItem();
                            }
                        }
                    }
                    con.Close();
                    con.Dispose();

                    MyExcutSql my1 = new MyExcutSql();
                    my1.ExtTaskone(xiangmuid, xiangmuid, "分派工程师", "手工提交", Session["UserName"].ToString(), "ZhuJianEngineer", DateTime.Now, st);
                    BindZhujianengineer();
                }
                else
                {
                    Literal1.Text = "<script>alert('请先完成前处理，再分派工程师')</script>";
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        else
        {
            Literal1.Text = "<script>alert('请先选择测试项目，再分派工程师')</script>";
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from userinfo where department='" + txt_deap.Text + "' and (jiaosename='工程师' or jiaosename='工程经理') order by username asc ";
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataTextField = "username";
        DropDownList2.DataValueField = "username";
        DropDownList2.DataBind();
        con.Close();
    }
    protected void GridView4_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        //判断该工程师是否存在测试项目，若存在测试项目将不能删除
        string engineer = GridView4.Rows[e.RowIndex].Cells[2].Text.ToString();
        string sql_projectitem = "select * from ProjectItem where taskid='" + xiangmuid + "' and engineer='" + engineer + "'";
        SqlCommand com_projectitem = new SqlCommand(sql_projectitem, con);
        SqlDataReader dr_prijectitme = com_projectitem.ExecuteReader();
        if (dr_prijectitme.Read())
        {
            dr_prijectitme.Close();
            Literal1.Text = "<script>alert('请先删除该工程师的测试项目')</script>";
        }
        else
        {
            dr_prijectitme.Close();
            string id = GridView4.DataKeys[e.RowIndex].Value.ToString();
            string sql = "delete from ZhuJianEngineer where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MyExcutSql my1 = new MyExcutSql();
            my1.ExtTaskone(xiangmuid, xiangmuid, "删除了工程师", "手工提交", Session["UserName"].ToString(), "ZhuJianEngineer", DateTime.Now, st);
            BindZhujianengineer();
        }
    }

    protected void GridView4_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView4.EditIndex = -1;
        BindZhujianengineer();
    }

    protected void GridView4_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView4.EditIndex = e.NewEditIndex;
        BindZhujianengineer();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        DropDownList ddl = GridView4.Rows[e.NewEditIndex].FindControl("ddlXL") as DropDownList;

        string sql = "select * from userinfo where jiaosename='工程师' or jiaosename='工程经理' order by username";
        SqlDataAdapter dap = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        dap.Fill(ds, "username");
        ddl.DataSource = ds.Tables["username"].DefaultView;
        HiddenField hf = GridView4.Rows[e.NewEditIndex].FindControl("hf") as HiddenField;
        con.Close();
        ddl.DataTextField = "username";
        ddl.DataValueField = "username";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("", ""));//

        ddl.SelectedValue = (GridView4.Rows[e.NewEditIndex].FindControl("hf") as HiddenField).Value;


    }
    protected void GridView4_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string KeyId = GridView4.DataKeys[e.RowIndex].Value.ToString();

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con1.Open();
        string uuname1 = Server.HtmlEncode(((TextBox)this.GridView4.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());
        // string uuname2 = Server.HtmlEncode(((TextBox)this.GridView4.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
        string xueli = ((DropDownList)GridView4.Rows[e.RowIndex].FindControl("ddlXL")).SelectedValue;

        string sql = "update zhujianengineer set beizhu=beizhu+name,bumen='" + uuname1 + "', name='" + xueli + "' where id='" + KeyId + "'";

        SqlCommand cmd = new SqlCommand(sql, con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        GridView4.EditIndex = -1;
        BindZhujianengineer();
    }
    /// <summary>
    /// 测试项目
    /// </summary>
    protected void BindXiangmu()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select *  from BaoJiaCPXiangMu  where id in (select xiangmubianhao from anjianxinxi3 where bianhao='" + anjianinfoid + "')";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView5.DataSource = ds.Tables[0];
            GridView5.DataBind();

            DataTable dt = Department();
            if ((dt.Rows[0]["dutyname"].ToString().Contains("工程") || dt.Rows[0]["dutyname"].ToString() == "测试员") && dt.Rows[0]["departmentname"].ToString() != "认证部")
            {
                string bumen = dt.Rows[0]["departmentname"].ToString();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)GridView5.Rows[i].FindControl("CheckBox1");
                    if (bumen != GridView5.Rows[i].Cells[5].Text)
                    {
                        chk.Enabled = false;
                    }
                }
            }
        }
    }

    protected void ProjectItem()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select * from [dbo].[ProjectItem] where taskid='" + xiangmuid + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView6.DataSource = ds.Tables[0];
            GridView6.DataBind();
        }
    }

    protected void GridView6_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //状态已完成的任务不能删除
        string state = GridView6.Rows[e.RowIndex].Cells[5].Text.ToString();
        string id = GridView6.DataKeys[e.RowIndex].Value.ToString();
        if (state != "已完成")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                string sql = "delete ProjectItem where id='" + id + "'";
                SqlCommand com = new SqlCommand(sql, con);
                int i = com.ExecuteNonQuery();
                if (i > 0)
                {
                    ProjectItem();
                }
            }
        }
        else
        {
            Literal1.Text = "<script>alert('已完成的项目不能删除。')</script>";
        }
    }

    private DataTable Department()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
        {
            con.Open();
            string sql = "select top 1 dutyname,departmentname from UserInfo where UserName='" + Session["Username"].ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }

}