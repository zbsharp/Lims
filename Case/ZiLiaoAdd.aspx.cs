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

public partial class Case_ZiLiaoAdd : System.Web.UI.Page
{
    private const int _firstEditCellIndex = 2;
    protected string xiangmuid = "";
    protected string shenpibiaozhi = "";
    protected string bumen = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.Attributes.Add("style", "table-layout:fixed");
        xiangmuid = Request.QueryString["xiangmuid"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        
            if (!IsPostBack)
            {


              
                GridView1.DataBind();

            }
            if (this.GridView1.SelectedIndex > -1)
            {


                
                    this.GridView1.UpdateRow(this.GridView1.SelectedIndex, false);
                

            }
           


        
        con.Close();

    }




  

    public void Bind()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        string sql = "select * from anjianxinxi where xiangmuid='" + xiangmuid + "' order by convert(int,beizhu3) asc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        con.Dispose();
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

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
        foreach (GridViewRow r in GridView1.Rows)
        {
            if (r.RowType == DataControlRowType.DataRow)
            {
                for (int columnIndex = _firstEditCellIndex; columnIndex < r.Cells.Count; columnIndex++)
                {
                    Page.ClientScript.RegisterForEventValidation(r.UniqueID + "$ctl00", columnIndex.ToString());
                }
            }
        }

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
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }
  
}