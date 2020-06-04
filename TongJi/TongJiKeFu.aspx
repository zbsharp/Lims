<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TongJiKeFu.aspx.cs" Inherits="TongJi_TongJiKeFu" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title>统计管理</title>
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
        统计管理 》》客服业绩</div>


         任务完成日期： <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />


      
        <asp:TextBox ID="TextBox1" runat="server" Visible ="false"  Width="111px"></asp:TextBox>
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
                               <tr><td align ="left" >
该页面统计的数量是在以完成日期为准一段时间内所有的任务数量。<br />
1、CCC任务指类别为“CCC”的任务，其他均算“市场任务”<br />
2、未完成包括任务状态为“进行”和“暂停”的(N表示)<br />
3、已完成包括任务状态为“完成”和“关闭”的，不包括“中止”的(Y表示)<br />
4、查询条件为已完成任务的完成日期<br />
5、其他行列出客服字段为空的相应任务数量<br />
6、本页面授权给：客服主管、客服部经理、以及更高阶领导
                               </td></tr>
                            </table>
                        
                             
    </form>
</body>
</html>
