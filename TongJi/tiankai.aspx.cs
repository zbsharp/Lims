using System;
using System.Data;
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
using System.Drawing;


public partial class shoufei_tiankai : System.Web.UI.Page
{
    protected string xunzhengid = "";
    protected string kehuname = "";
    protected string yewuyuan = "";
    protected string shifouxieyi = "";
    protected string kuanleibie = "";
    protected string kehuid = "";
    DateTime shengqingtime = DateTime.Now;
    DateTime diyitime = DateTime.Now;
    #region Variables
    string gvUniqueID = String.Empty;
    int gvNewPageIndex = 0;
    int gvEditIndex = -1;
    string gvSortExpr = String.Empty;

    protected string liushuihao = "";

    protected string baojiaid = "";
    private string gvSortDir
    {

        get { return ViewState["SortDirection"] as string ?? "ASC"; }

        set { ViewState["SortDirection"] = value; }

    }
    #endregion

    //This procedure returns the Sort Direction
    private string GetSortDirection()
    {
        switch (gvSortDir)
        {
            case "ASC":
                gvSortDir = "DESC";
                break;

            case "DESC":
                gvSortDir = "ASC";
                break;
        }
        return gvSortDir;
    }

    //This procedure prepares the query to bind the child GridView
    private SqlDataSource ChildDataSource(string strCustometId,string kuanlb ,string shoufeiid,string strSort)
    {
      //  kuanleibie = kuanlb;
        string strQRY = "";
        SqlDataSource dsTemp = new SqlDataSource();
        //AccessDataSource dsTemp = new AccessDataSource();
        dsTemp.ConnectionString = ConfigurationManager.AppSettings["Connection"];
        //dsTemp.DataFile = "App_Data/Northwind.mdb";
        if (strCustometId != "月结")
        {



            strQRY = "SELECT * FROM [casean] WHERE [beizhu2] = '" + strCustometId + "'";
        }
        else
        {
            strQRY = "SELECT * FROM [casean] WHERE [beizhu2] = '" + shoufeiid + "'";
        }
                             

        dsTemp.SelectCommand = strQRY;
        return dsTemp;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        Button1.Attributes.Add("onclick", "javascript:return confirm('您确认要修改到帐情况吗?,此操作执行后不能再次修改')");
        TextBox12.Attributes.Add("onkeyup", "add()");
        TextBox1.Text = Request.QueryString["liushuihao"].ToString();
        TextBox2.Text = Server.UrlDecode(Request.QueryString["kehuname"].ToString());

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        con.Open();


        string sql1 = "select * from shuipiao where liushuihao='" + TextBox1.Text + "'";

        SqlCommand cmd = new SqlCommand(sql1, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {


           // TextBox4.Text = dr["danwei"].ToString();
            TextBox5.Text = dr["fukuanren"].ToString();
            TextBox6.Text = dr["fukuanriqi"].ToString();
           // TextBox7.Text = dr["fukuanjine"].ToString();
            CheckBoxList1.Text = dr["fukuanfangshi"].ToString();
            TextBox10.Text = dr["fukuanjine"].ToString() ;
            TextBox11.Text = dr["danwei"].ToString();
            TextBox8.Text = dr["beizhu2"].ToString();
            baojiaid = dr["beizhu2"].ToString();
        }
        else
        {
            //TextBox4.Text = "0";
        }

        dr.Close();





        if (!IsPostBack)
        {

           
            dinge();
            TextBox7.Text = TextBox10.Text;
            Bindceshi();


        }
        btnFirst.Text = "最首页";
        btnPrev.Text = "前一页";
        btnNext.Text = "下一页";
        btnLast.Text = "最后页";
        ShowPageChangedStatus();
        con.Close();


    }
    public void PagerButtonClick(object sender, EventArgs e)
    {
        string arg = ((LinkButton)sender).CommandArgument.ToString();
        switch (arg)
        {
            case "next":
                if (GridView1.PageIndex < (GridView1.PageCount - 1))
                {
                    GridView1.PageIndex += 1;
                }
                break;
            case "prev":
                if (GridView1.PageIndex > 0)
                {
                    GridView1.PageIndex -= 1;
                }
                break;
            case "last":
                GridView1.PageIndex = (GridView1.PageCount - 1);
                break;
            default:
                GridView1.PageIndex = System.Convert.ToInt32(arg);
                break;
        }
        dinge();
        ShowPageChangedStatus();
    }
  


    private void ShowPageChangedStatus()
    {
        Label1.Text = "第" + (GridView1.PageIndex + 1).ToString() + "页";
        Label2.Text = "总共 " + GridView1.PageCount.ToString() + " 页";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        con.Open();
        string sql = "insert into shuipiao values()";


    }
    protected void Button2_Click(object sender, EventArgs e)
    {


    }

    public void Bindceshi()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        con.Open();
        string sql = "select * from emcshiji  where daozhang='否' and kehuname like '%" + TextBox5.Text.Trim() + "%' order by lurutime desc";
        //string sql = "select * from studentInfo";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        con.Close();
        con.Dispose();


        GridView6.DataSource = ds.Tables[0];
        GridView6.DataBind();
        // this.My.Text = "现共有" + pds.DataSourceCount.ToString() + "条记录";
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        int i;
        if (((CheckBox)sender).Checked)
        {
            for (i = 0; i < GridView6.Rows.Count; i++)
            {
                ((CheckBox)GridView6.Rows[i].FindControl("CheckBox1")).Checked = true;
            }
        }
        else
        {
            for (i = 0; i < GridView6.Rows.Count; i++)
            {
                ((CheckBox)GridView6.Rows[i].FindControl("CheckBox1")).Checked = false;
            }
        }

        
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void dinge()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        con.Open();
        string sql = "select *  from baojiabiao where (kehuname like '%" + TextBox5.Text + "%' or kehuname like '%" + TextBox5.Text + "' or kehuname like '" + TextBox5.Text + "%' or liushuihao='"+TextBox1.Text.Trim()+"') and shifoudaozhang !='全额到帐' and huiqianbiaozhi='是'";
        //string sql = "select *  from baojiabiao where (kehuname like '%" + TextBox5.Text + "%' or kehuname like '%" + TextBox5.Text + "' or kehuname like '" + TextBox5.Text + "%') and shifoudaozhang !='全额到帐'";
       
        
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        DropDownList1.DataSource = ds.Tables[0];
        DropDownList1.DataTextField = "baojiaid";
        DropDownList1.DataValueField = "baojiaid";
        DropDownList1.DataBind();



        string sql2 = "select *  from casean where beizhu2='" + DropDownList1.SelectedValue + "'";


        SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);
        DataSet ds2 = new DataSet();
        ad2.Fill(ds2);


        DropDownList2.DataSource = ds2.Tables[0];
        DropDownList2.DataTextField = "xiaolei";
        DropDownList2.DataValueField = "id";
        DropDownList2.DataBind();


        con.Close();
        con.Dispose();

    }

    #region GridView1 Event Handlers

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        string strSort = string.Empty;

        // Make sure we aren't in header/footer rows

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#FFE0C0'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");



            //Find Child GridView control
            GridView gv = new GridView();
            gv = (GridView)row.FindControl("GridView2");

            //Check if any additional conditions (Paging, Sorting, Editing, etc) to be applied on child GridView
            if (gv.UniqueID == gv.UniqueID)
            {
                gv.PageIndex = gvNewPageIndex;
                gv.EditIndex = gvEditIndex;
                //Check if Sorting used
                if (gvSortExpr != string.Empty)
                {
                    GetSortDirection();
                    strSort = " ORDER BY " + string.Format("{0} {1}", gvSortExpr, gvSortDir);
                }
                //Expand the Child grid
               // ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["id"].ToString() + "','two');</script>");
            }

            //Prepare the query for Child GridView by passing the Customer ID of the parent row
            gv.DataSource = ChildDataSource(DataBinder.Eval(e.Row.DataItem, "baojiaid").ToString(), DataBinder.Eval(e.Row.DataItem, "baojiaid").ToString(), DataBinder.Eval(e.Row.DataItem, "baojiaid").ToString(), strSort);
            gv.DataBind();

            //Add delete confirmation message for Customer
            e.Row.Cells[2].Attributes.Add("onclick", "javascript:window.showModalDialog('cashin.aspx?shoufeiid=" + DataBinder.Eval(e.Row.DataItem, "baojiaid").ToString() + "&&kehuname=" + DataBinder.Eval(e.Row.DataItem, "kehuname").ToString() + "&&zongfeiyong=" + DataBinder.Eval(e.Row.DataItem, "baojiaid").ToString() + "&&rand=" + DateTime.Now + "','window','dialogWidth=800px;DialogHeight=500px;status:no;help:no;resizable:yes;')");
             
        }


        //for (int i = 0; i < GridView1.Rows.Count; i++)
        //{
        //    for (int j = 0; j < GridView1.Columns.Count; j++)
        //    {



        //        string qq = TextBox5.Text;



        //        //GridView1.Rows[i].Cells[1].Attributes.Add("onclick", "javascript:window.showModalDialog('cashin.aspx?shoufeiid=" + Server.UrlEncode(GridView1.Rows[i].Cells[2].Text) + "&&kehuname=" + GridView1.Rows[i].Cells[4].Text + "&&zongfeiyong=" + Server.UrlEncode(GridView1.Rows[i].Cells[2].Text) + "&&rand=" + DateTime.Now + "','window','dialogWidth=800px;DialogHeight=500px;status:no;help:no;resizable:yes;')");
        //        string a = GridView1.Rows[i].Cells[0].Text;
        //        string b = GridView1.Rows[i].Cells[1].Text;
        //        string c = GridView1.Rows[i].Cells[2].Text;
        //        GridView1.Rows[i].Cells[3].ForeColor = Color.Red;
        //       string q = GridView1.Rows[i].Cells[3].Text;

        //    }



        //}

    }

    //This event occurs for any operation on the row of the grid
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Check if Add button clicked

    }

    //This event occurs on click of the Update button
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //Get the values stored in the text boxes

    }

    //This event occurs after RowUpdating to catch any constraints while updating
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        //Check if there is any exception while deleting

    }

    //This event occurs on click of the Delete button
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


    }

    //This event occurs after RowDeleting to catch any constraints while deleting
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    #endregion

    #region GridView2 Event Handlers
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gvTemp = (GridView)sender;
        gvUniqueID = gvTemp.UniqueID;
        gvNewPageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddOrder")
        {
            try
            {
                GridView gvTemp = (GridView)sender;
                gvUniqueID = gvTemp.UniqueID;

                //Get the values stored in the text boxes
                string strCustomerID = gvTemp.DataKeys[0].Value.ToString();  //Customer ID is stored as DataKeyNames
                string strFreight = ((TextBox)gvTemp.FooterRow.FindControl("txtFreight")).Text;
                string strShipperName = ((TextBox)gvTemp.FooterRow.FindControl("txtShipperName")).Text;
                string strShipAdress = ((TextBox)gvTemp.FooterRow.FindControl("txtShipAdress")).Text;

                //Prepare the Insert Command of the DataSource control
                string strSQL = "";
                strSQL = "INSERT INTO Orders (CustomerID, Freight, ShipName, " +
                        "ShipAddress) VALUES ('" + strCustomerID + "'," + float.Parse(strFreight) + ",'" +
                        strShipperName + "','" + strShipAdress + "')";

                SqlDataSource1.InsertCommand = strSQL;
                SqlDataSource1.Insert();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Order added successfully');</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message.ToString().Replace("'", "") + "');</script>");
            }
        }
        else if (e.CommandName == "chakan")
        {
            string sid = e.CommandArgument.ToString();
            Response.Redirect("baojia2.aspx?baojiaid=" + sid);
        }




    }

    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        string baojiaid="";
        SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        con3.Open();

        GridView gvTemp1 = (GridView)sender;

        string sid = gvTemp1.DataKeys[e.NewEditIndex].Value.ToString();


        string sqlbao = "select beizhu2 from casean where id='" + sid + "'";
        SqlCommand cmdbao = new SqlCommand(sqlbao, con3);
        SqlDataReader drbao = cmdbao.ExecuteReader();
        if (drbao.Read())
        {
            baojiaid = drbao["beizhu2"].ToString();
        }
        drbao.Close();






        string nandian = "全款";
     
    
      
        string sql3 = "";

      
         if (nandian == "全款")
        {

            sql3 = "update casean set qkfwqk=case qkfwqk when  '否' then '已到全款' when '已到全款' then '否' end,qkrzqk='" + TextBox6.Text + "' ,liushuihao='" + TextBox1.Text + "',shoukuan=ren  where id='" + sid + "'";
        }

        SqlCommand cmd3 = new SqlCommand(sql3, con3);
        cmd3.ExecuteNonQuery();


     




        con3.Close();
        dinge();
    }

    protected void GridView2_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void GridView2_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

        if (e.Exception != null)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
            e.ExceptionHandled = true;
        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GridView2_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        //if (kuanleibie == "全款")
        //{
        //    e.Row.Cells[3].Visible = false;
        //    e.Row.Cells[4].Visible = false;
        //    e.Row.Cells[5].Visible = false;
        //    e.Row.Cells[6].Visible = true;

        //    e.Row.Cells[8].Visible = false;
        //    e.Row.Cells[9].Visible = false;


        //    e.Row.Cells[10].Visible = false;
        //    e.Row.Cells[11].Visible = false;
        //    e.Row.Cells[12].Visible = false;

        //    e.Row.Cells[13].Visible = false;
        //    e.Row.Cells[14].Visible = true;
        //    e.Row.Cells[15].Visible = true;
        //}
    }
    #endregion
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string sid = GridView1.DataKeys[e.NewEditIndex].Value.ToString();
        string baojiaid = "";
        string shoufeiid = "";
        
        SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        con3.Open();


        string sqlbao = "select baojiaid from baojiabiao where id='" + sid + "'";

        SqlCommand cmdbao = new SqlCommand(sqlbao, con3);
        SqlDataReader drbao = cmdbao.ExecuteReader();
        if (drbao.Read())
        {
            baojiaid = drbao["baojiaid"].ToString();
            // kuanleibie = drbao["kuanleibie"].ToString();
            //shoufeiid = drbao["shoufeiid"].ToString();

        }
        drbao.Close();
        
        
       kuanleibie="全款";
        
        
        
        
        
        string sql3 = "";
      
         if (kuanleibie == "全款")
        {

            sql3 = "update casean set qkfwqk=case qkfwqk when  '否' then '已到全款' when '已到全款' then '否' end ,qkrzqk=case qkrzqk when  '' then '" + TextBox6.Text + "' when '" + TextBox6.Text + "' then '' end ,liushuihao='" + TextBox1.Text + "',shoukuan=ren  where beizhu2='" + baojiaid + "'";
        }

        SqlCommand cmd3 = new SqlCommand(sql3, con3);
        cmd3.ExecuteNonQuery();

        string sqlf = "update baojiabiao set  shifoudaozhang=case shifoudaozhang when  '未到帐' then '全额到帐' when '全额到帐' then '未到帐' end ,liushuihao='" + TextBox1.Text + "',fapiaotaitou='" + TextBox3.Text + "',fapiaoneirong='" + DropDownList3.SelectedItem.Text + "',daozhangtime='" + TextBox6.Text + "' where id='" + sid + "'";
        SqlCommand cmddd = new SqlCommand(sqlf, con3);
        cmddd.ExecuteNonQuery();

        



        con3.Close();
        dinge();
        
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int startIndex;
        startIndex = GridView1.PageIndex * GridView1.PageSize;
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        dinge();
        
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        con3.Open();
        string sql = "update shuipiao set fapiaohao='"+TextBox10.Text+"',fapiaojine='"+TextBox11.Text+"',fapiaoneirong='"+TextBox12.Text+"', queren='已确认',querenren='" + Session["UserName"].ToString() + "',querenriqi='" + DateTime.Now + "'where liushuihao='" + TextBox1.Text + "'";
       //string  sql2 = "insert into fapiao values('','','','','','','" + DropDownList3.SelectedItem.Text + "','" + TextBox3.Text + "','','','','','','')";

        
        SqlCommand cmd3 = new SqlCommand(sql, con3);
        //SqlCommand cmd2 = new SqlCommand(sql2, con3);

        cmd3.ExecuteNonQuery();
        //cmd2.ExecuteNonQuery();

        con3.Close();
        dinge();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        con3.Open();

        string sql = "select *  from casean where beizhu2='"+DropDownList1.SelectedValue+"'";


        SqlDataAdapter ad = new SqlDataAdapter(sql, con3);
        DataSet ds = new DataSet();
        ad.Fill(ds);
      

        DropDownList2.DataSource = ds.Tables[0];
        DropDownList2.DataTextField = "xiaolei";
        DropDownList2.DataValueField = "id";
        DropDownList2.DataBind();

        con3.Close();
        con3.Dispose();
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        con3.Open();

        int a = Convert.ToInt32(TextBox9.Text);
        string sql = "update casean set  shoukuan=shoukuan+'" + a + "',qkrzqk='"+TextBox6.Text+"',liushuihao=liushuihao+'"+TextBox1.Text+"' where beizhu2='" + DropDownList1.SelectedValue + "' and id='" + DropDownList2.SelectedValue + "'";


        SqlCommand cmd = new SqlCommand(sql, con3);
        cmd.ExecuteNonQuery();
        string quan = "";

        string sql4 = "select sum(convert(decimal(18,2),shoukuan)) as jine from casean where beizhu2='" + DropDownList1.SelectedValue + "' group by beizhu2";
        SqlCommand cmd4 = new SqlCommand(sql4, con3);
        SqlDataReader dr4 = cmd4.ExecuteReader();
        if (dr4.Read())
        {
            quan =  dr4["jine"].ToString();
        }
        dr4.Close();


        string sql7 = "select kehuid from baojiabiao where baojiaid='" + DropDownList1.SelectedValue + "'";
        SqlCommand cmd7 = new SqlCommand(sql7, con3);
        SqlDataReader dr7 = cmd7.ExecuteReader();
        if (dr7.Read())
        {
            kehuid = dr7["kehuid"].ToString();
        }
        dr7.Close();


        string sql5 = "update baojiabiao set name6='" + quan + "',liushuihao=liushuihao+'"+TextBox1.Text+"' where baojiaid='" + DropDownList1.SelectedValue + "'";
        SqlCommand cmd5 = new SqlCommand(sql5, con3);
        cmd5.ExecuteNonQuery();


        string sql6 = "insert into cashin2 values('" + TextBox6.Text + "','" + Convert.ToDecimal(TextBox7.Text) + "','" + DropDownList2.SelectedValue + "','" + DropDownList1.SelectedValue + "','" + Convert.ToDecimal(TextBox9.Text) + "','" + DateTime.Now + "','" + Session["UserName"].ToString() + "','" + TextBox1.Text + "','" + kehuid + "','否','" + DateTime.Now + "','否','" + DateTime.Now + "','','','')";
        SqlCommand cmd6 = new SqlCommand(sql6, con3);
        cmd6.ExecuteNonQuery();



        con3.Close();
        con3.Dispose();

        TextBox9.Text = "0";

        if (TextBox13.Text == "")
        {
            dinge();
        }
        else
        {
            find();
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        find();
    }

    protected void find()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        con.Open();

        string sql = "";
        if (DropDownList4.SelectedValue == "报价单号")
        {
            sql = "select *  from baojiabiao where (baojiaid='" + TextBox13.Text.Trim() + "' and shifoudaozhang !='全额到帐'  and huiqianbiaozhi='是') or  liushuihao='" + TextBox1.Text.Trim() + "'";
            //string sql = "select *  from baojiabiao where (kehuname like '%" + TextBox5.Text + "%' or kehuname like '%" + TextBox5.Text + "' or kehuname like '" + TextBox5.Text + "%') and shifoudaozhang !='全额到帐'";


            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();

            DropDownList1.DataSource = ds.Tables[0];
            DropDownList1.DataTextField = "baojiaid";
            DropDownList1.DataValueField = "baojiaid";
            DropDownList1.DataBind();



            string sql2 = "select *  from casean where beizhu2='" + DropDownList1.SelectedValue + "'";


            SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);
            DataSet ds2 = new DataSet();
            ad2.Fill(ds2);


            DropDownList2.DataSource = ds2.Tables[0];
            DropDownList2.DataTextField = "xiaolei";
            DropDownList2.DataValueField = "id";
            DropDownList2.DataBind();
        }
           
        else
        {
           // sql = "select *  from baojiabiao where (baojiaid='" + TextBox13.Text.Trim() + "' and shifoudaozhang !='全额到帐' and huiqianbiaozhi='是') or  liushuihao='" + TextBox1.Text.Trim() + "'";

            sql = "select * from emcshiji  where daozhang='否' and ceshihao like '%" + TextBox13.Text.Trim() + "%'  order by lurutime desc";
            //string sql = "select * from studentInfo";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

          


            GridView6.DataSource = ds.Tables[0];
            GridView6.DataBind();
        }


        con.Close();
        con.Dispose();
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        Random seed = new Random();
        Random randomNum = new Random(seed.Next());

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        con.Open();


        string shoufeiid = randomNum.Next().ToString() + DateTime.Now;

        foreach (GridViewRow gr in GridView6.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");

            if (hzf.Checked)
            {
                //int sid = Convert.ToInt32(gr.Cells[9].Text);
                string sid = GridView6.DataKeys[gr.RowIndex].Value.ToString();




                //string sql3 = "delete from semcshiji where jiludanhao='" + sid + "'and biaoji!='" + shoufeiid + "'";
                //SqlCommand com3 = new SqlCommand(sql3, con);
                //com3.ExecuteNonQuery();

                //string sql = "insert into semcshiji select * from emcshiji where jiludanhao='" + sid + "'";
                //string sql1 = "update semcshiji set biaoji='" + shoufeiid + "' where jiludanhao='" + sid + "'";
                string sql2 = "update emcshiji set daojine=hfeiyong,daozhang='是',queren='" + Session["UserName"].ToString() + "',querenriqi='" + DateTime.Now.ToShortDateString() + "',liushuihao='"+TextBox1.Text+"' where jiludanhao='" + sid + "'";



                //SqlCommand com1 = new SqlCommand(sql1, con);


                //SqlCommand com = new SqlCommand(sql, con);
                SqlCommand com2 = new SqlCommand(sql2, con);

                //com.ExecuteNonQuery();
                //com1.ExecuteNonQuery();
                com2.ExecuteNonQuery();
            }
        }

        con.Close();

        Bindceshi();
        
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Random seed = new Random();
        Random randomNum = new Random(seed.Next());

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        con.Open();


        string shoufeiid = randomNum.Next().ToString() + DateTime.Now;

        foreach (GridViewRow gr in GridView6.Rows)
        {
            CheckBox hzf = (CheckBox)gr.Cells[0].FindControl("CheckBox1");

            if (hzf.Checked)
            {
                string sid = GridView6.DataKeys[gr.RowIndex].Value.ToString();











                string sql2 = "update emcshiji set daojine=hfeiyong,daozhang='是',remark3='" + Session["UserName"].ToString() + "',remark4='" + DateTime.Now.ToShortDateString() + "' where jiludanhao='" + sid + "'";




                SqlCommand com2 = new SqlCommand(sql2, con);


                com2.ExecuteNonQuery();


                string sql3 = "update anjianinfo  set chongcebiaozhi='已收' where xiangmuid='" + sid + "'";

                SqlCommand com3 = new SqlCommand(sql3, con);


                com3.ExecuteNonQuery();
                decimal daojine = 0;
                string idd = "";
                string baojiaid = "";
                string kehuid = "";
                string sql5 = "select hfeiyong from emcshiji  where jiludanhao='" + sid + "'";
                SqlCommand cmd5 = new SqlCommand(sql5, con);
                SqlDataReader dr5 = cmd5.ExecuteReader();
                if (dr5.Read())
                {

                    if (dr5["hfeiyong"] != DBNull.Value)
                    {

                        daojine = Convert.ToDecimal(dr5["hfeiyong"]);
                    }
                }
                dr5.Close();

                // string sql8 = "insert into casean values('','','','周期检验','','','','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','HQ205001001','','','否','','0','','','0','RMB','','0','RMB','','','','','','','','"+DateTime.Now.ToShortDateString()+"','','','无','','','" + sid + "','','','','','0','','0','','0','','0','','0','','0','','0','','0','','','','','','否','否','否','否','否','否','否','否','','','','')";
                // SqlCommand cmd8 = new SqlCommand(sql8, con);
                //cmd8.ExecuteNonQuery();
                string sql6 = "select id,beizhu2 from casean where dengji='" + sid + "'";
                SqlCommand cmd6 = new SqlCommand(sql6, con);
                SqlDataReader dr6 = cmd6.ExecuteReader();
                if (dr6.Read())
                {



                    idd = dr6["id"].ToString();
                    baojiaid = dr6["beizhu2"].ToString();

                }
                dr6.Close();


                string sql7 = "select kehuid from baojiabiao where baojiaid='" + baojiaid + "'";
                SqlCommand cmd7 = new SqlCommand(sql7, con);
                SqlDataReader dr7 = cmd7.ExecuteReader();
                if (dr7.Read())
                {




                    kehuid = dr7["kehuid"].ToString();

                }
                dr7.Close();







                string sql4 = "insert into cashin2 values('','" + daojine + "','" + idd + "','','" + daojine + "','" + DateTime.Now + "','" + Session["UserName"].ToString() + "','','" + kehuid + "','','','','','','','')";




                SqlCommand com4 = new SqlCommand(sql4, con);


                com4.ExecuteNonQuery();

            }
        }

        con.Close();

        Bindceshi();
    }
}

