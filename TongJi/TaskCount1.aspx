<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskCount1.aspx.cs" Inherits="TongJi_TaskCount1" %>
<%--<%@ outputcache duration="100" varybyparam="none"  %>--%>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="EeekSoft.Web.PopupWin" Namespace="EeekSoft.Web" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title>统计管理</title>
    <style  type ="text/css" >
    .page_loading
{
position: absolute;
top: 90%;
left: 50%;
margin-top: -40px;
margin-left: -110px;
width: 40px;
height: 20px;
text-align: center;
background-color: #F7F7F7;
/*filter: alpha(opacity=60);
-moz-opacity: 0.6;*/
}
</style>


    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
      <script type="text/javascript" src="../js/calendar.js"></script>


      <script type ="text/javascript">


          var currentRowId = 0;
          function SelectRow() {
              if (event.keyCode == 40)
                  MarkRow(currentRowId + 1);
              else if (event.keyCode == 38)
                  MarkRow(currentRowId - 1);
          }

          function MarkRow(rowId) {
              if (document.getElementById(rowId) == null)
                  return;

              if (document.getElementById(currentRowId) != null)
                  document.getElementById(currentRowId).style.backgroundColor = '#ffffff';

              currentRowId = rowId;
              document.getElementById(rowId).style.backgroundColor = '#FFE0C0';
          }
          function text() {
              document.getElementById("bnClick").click();
          }
   
    
  
    
    </script>

</head>
<body>
    <form id="form1" runat="server">

 
 <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upLoading" runat="server">
    <ContentTemplate>


 <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID ="UpdatePanel2" runat="server">
<ProgressTemplate>
<div id="iLoading1" style="color: #ff0000; font-family: 隶书; background-color: #99ccff;">Loading1......</div>
</ProgressTemplate>
</asp:UpdateProgress>

</ContentTemplate>

<Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                                    
                                </Triggers>


 </asp:UpdatePanel> 







    <div class="Body_Title">
        统计管理 》》按工程师显示结算金额</div>


         结算日期： <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />


      
        <asp:TextBox ID="TextBox1" runat="server" Visible ="false"  Width="111px"></asp:TextBox>
      
       <asp:DropDownList ID="DropDownList1" runat="server"   
            onselectedindexchanged="DropDownList1_SelectedIndexChanged" >
        </asp:DropDownList>
      
        <asp:Button ID="Button2"  CssClass ="BnCss"    runat="server" 
             Text="查询" onclick="Button2_Click" /><asp:Button ID="Button1"    runat="server" 
            onclick="Button1_Click" Text="导出excel" />

 
                            <table width="100%" class="Admin_Table" 
                                border="1">
                                <tr>
                                    <td style="width: 100%";  valign="top">

                                      <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                                      <ContentTemplate >
                                        <asp:GridView ID="GridView1" runat="server" ShowFooter ="true"  Width="100%" 
                                            OnRowDataBound="GridView1_RowDataBound" 
                                          
                                            CssClass="Admin_Table">
                                          <Columns >
                                          
                                            
                                          </Columns>
                                          
                                          <HeaderStyle CssClass="Admin_Table_Title " />
                                        </asp:GridView>

                                    


                                      </ContentTemplate>

                                       <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                                    
                                </Triggers>
                                      </asp:UpdatePanel> 

                                    </td>
                                </tr>
                                 <tr><td align ="left" >

                                当一个任务由两个及以上工程师完成时，则表格中横向和纵向加总与表格边缘的汇总会有数据差别。
                           </td></tr>
                           
                            </table>
                        
                             
    </form>

     <%--<cc1:PopupWin ID="PopupWin1"  ShowAfter ="0"  DragDrop ="true"  Width ="200px" Height ="120px"  WindowScroll="true"  ActionType ="MessageWindow"      runat="server"  ColorStyle="Custom" 
            DockMode="BottomRight"     HideAfter ="500000"   Message ="该页面比较慢，建议打开一次导出EXCEL本地查看，列表纵向数据准确，横向数据待观察"  Link=""  LinkTarget="main" 
             BackColor="Red"     Title="请关注您将超期的任务"  />--%>

</body>
</html>



