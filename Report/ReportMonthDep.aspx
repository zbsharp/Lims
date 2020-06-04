<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportMonthDep.aspx.cs" Inherits="Report_ReportMonthDep" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title>质量管理</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
      <script type="text/javascript" src="../js/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
        质量管理 》》部门月度差错</div>


         按报告退回年份： <input id="txFDate"  visible ="false"  runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text"  />
        
        <input id="txTDate"  visible ="false" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text"  />

            年份:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
      
        <asp:TextBox ID="TextBox1" runat="server" Visible ="false"  Width="111px"></asp:TextBox>
        
        <asp:DropDownList ID="DropDownList1" runat="server" 
            >
        </asp:DropDownList>
        
        
        
        (手工填写年份，默认当前年)
        <asp:Button ID="Button2"  CssClass ="BnCss"  runat="server" 
             Text="查询" onclick="Button2_Click" /><asp:Button ID="Button1"    runat="server" 
            onclick="Button1_Click" Text="导出excel" />

 
                            <table width="100%" class="Admin_Table" 
                                border="1">
                                <tr>
                                    <td style="width: 100%";  valign="top">
                                        <asp:GridView ID="GridView1" runat="server" ShowFooter ="true"  Width="100%" 
                                            OnRowDataBound="GridView1_RowDataBound" 
                                          
                                            CssClass="Admin_Table">
                                          <Columns >
                                          
                                            
                                          </Columns>
                                          
                                          <HeaderStyle CssClass="Admin_Table_Title " />
                                        </asp:GridView>
                                      


                                    </td>
                                </tr>
                               
                            </table>
                        
                             
    </form>
</body>
</html>




