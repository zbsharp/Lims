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

public partial class Case_ZiLiaoAddM : System.Web.UI.Page
{
    private const int _firstEditCellIndex = 9;
    protected string xiangmuid = "";
    protected string shenpibiaozhi = "";
    protected string bumen = "";
    private int _i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.Attributes.Add("style", "table-layout:fixed");
        xiangmuid = Request.QueryString["xiangmuid"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();
        Label2.Text = xiangmuid;

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



            e.Row.Attributes.Add("id", _i.ToString());
            e.Row.Attributes.Add("onKeyDown", "SelectRow();");
            //e.Row.Attributes.Add("oncontextmenu", "SelectRow();");


            e.Row.Attributes.Add("onClick", "MarkRow(" + _i.ToString() + ");");
            _i++;









            // Get the LinkButton control in the first cell
            LinkButton _singleClickButton = (LinkButton)e.Row.Cells[7].Controls[0];
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

            Label lb1 = (Label)e.Row.Cells[6].FindControl("Label1");

            lb1.Text = "nihao";

            string index = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            string sql2 = "select * from fuwufujian where caseid ='" + index + "' and pub_field3='资料'";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                dr2.Close();
                lb1.Text = "查看";
            }
            else
            {
                dr2.Close();
                lb1.Text = "上传";
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

            // ((TextBox)e.Row.Cells[3].FindControl("shebei2")).Attributes.Add("onclick", "return Show('" + ((TextBox)e.Row.Cells[2].FindControl("shebei2")).ClientID + "')");
            // ((TextBox)e.Row.Cells[3].FindControl("shebei2")).Attributes.Add("onclick", "popUpCalendar(this," + ((TextBox)e.Row.Cells[3].FindControl("shebei2")).ClientID + ",'yyyy-mm-dd')");
            //  ((TextBox)e.Row.Cells[4].FindControl("shebei3")).Attributes.Add("onclick", "popUpCalendar(this," + ((TextBox)e.Row.Cells[4].FindControl("shebei3")).ClientID + ",'yyyy-mm-dd')");
            //((TextBox)e.Row.Cells[10].FindControl("beizhu4")).Attributes.Add("onclick", "popUpCalendar(this," + ((TextBox)e.Row.Cells[5].FindControl("beizhu4")).ClientID + ",'yyyy-mm-dd')");
           // ((TextBox)e.Row.Cells[11].FindControl("shebei5Label5")).Attributes.Add("onclick", "popUpCalendar(this," + ((TextBox)e.Row.Cells[6].FindControl("shebei5Label5")).ClientID + ",'yyyy-mm-dd')");

           // ((TextBox)e.Row.Cells[12].FindControl("paixutext")).Attributes.Add("onclick", "popUpCalendar(this," + ((TextBox)e.Row.Cells[7].FindControl("paixutext")).ClientID + ",'yyyy-mm-dd')");


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
            string[] _columnKeys = new string[] { "renwuname", "gongchengshi", "kaishiriqi", "jihuajieshu", "beizhu4", "shijijieshu", "zhuangtai", "beizhu", "beizhu1", "beizhu3", "beizhu2" };

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

            string sql2 = "select * from ZiLiaoType where id not in (select leibieid from anjianxinxi where xiangmuid='" + xiangmuid + "')";



            SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);
            DataSet ds2 = new DataSet();
            ad2.Fill(ds2);
            DataTable dt2 = ds2.Tables[0];
            for (int i = 0; i < dt2.Rows.Count; i++)
            {

                string sql = "insert into anjianxinxi values('" + xiangmuid + "','','','','','','','','','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','','','','','" + dt2.Rows[i]["id"].ToString() + "','资料')";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }

            con.Close();
            con.Dispose();
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Print/ZiLiaoPrint.aspx?bianhao=" + xiangmuid);
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sqlstate = "insert into  TaskState values ('" + xiangmuid + "','" + xiangmuid + "','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','初审资料','客服受理任务生成案件号')";
        SqlCommand cmdstate = new SqlCommand(sqlstate, con);
        cmdstate.ExecuteNonQuery();

        con.Close();
        con.Dispose();

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        con.Open();

        string sqlstate = "insert into  TaskState values ('" + xiangmuid + "','" + xiangmuid + "','(select max(id)) from Anjianxinxi2','" + Session["UserName"].ToString() + "','" + DateTime.Now + "','" + DateTime.Now + "','确定资料','客服受理任务生成案件号')";
        SqlCommand cmdstate = new SqlCommand(sqlstate, con);
        cmdstate.ExecuteNonQuery();

        con.Close();
        con.Dispose();
    }
}