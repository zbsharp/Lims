<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkLogList.aspx.cs" Inherits="SysManage_WorkLogList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
</head>
<body>
   <form id="form1"  runat="server">
	
								<div class="Body_Title">
            综合管理 》》用户操作日志</div>							

																
                                                          <asp:GridView ID="GridView1"  runat="server" CssClass="Admin_Table"  AutoGenerateColumns="False"  DataKeyNames="worklogid" 
                                                                                          >
                                                       
                                                        <Columns>
                                                       
                                                      <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDo" runat="server" Style="left: 2px; position: relative; top: 2px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:BoundField DataField="WorkLogId" HeaderText="用户编号" />
                            <asp:BoundField DataField="UserName" HeaderText="用户姓名" />
                            <asp:BoundField DataField="WorkTime" HeaderText="操作时间" />
                            <asp:BoundField DataField="WorkInfo" HeaderText="操作描述" />
                                                        </Columns>
                                                              
                                                    </asp:GridView>

                                                     <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                                    CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                                    InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager2_PageChanged"
                                    PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                                    Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="15">
                                </webdiyer:AspNetPager>
                                                   
		</form>
</body>
</html>

