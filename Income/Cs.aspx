<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cs.aspx.cs" Inherits="Income_Cs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

     <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem>全部</asp:ListItem>
        <asp:ListItem>完成</asp:ListItem>
        <asp:ListItem>未完成</asp:ListItem>

         <asp:ListItem>客户名称</asp:ListItem>

    </asp:DropDownList>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="查询" onclick="Button1_Click" />
    <asp:Button ID="Button2"
        runat="server" Text="EXCEL" onclick="Button2_Click" />
    <div>
     <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False"
                        DataKeyNames="id" 
                       >

                       <HeaderStyle CssClass="Admin_Table_Title " />


                        <Columns>
                           

                            <asp:BoundField DataField="rwbianhao" HeaderText="任务号" />
                                              
                                                <asp:BoundField DataField="chanpinname" HeaderText="产品名称" />
                                                
                                                 <asp:BoundField DataField="xiadariqi" HeaderText="下达日期" />
                                                <asp:BoundField DataField="state" HeaderText="当前状态" />
                                                <asp:BoundField DataField="kh" HeaderText="客户名称" />
                           <asp:BoundField DataField="yuji" HeaderText="预计完成" />
                              <asp:HyperLinkField HeaderText="查看" Text="查看" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=1"
                    DataNavigateUrlFields="bianhao" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>


                    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                    CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                    InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " PrevPageText="【前页】 "
                    ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                    Width="682px" Style="font-size: 9pt" PageSize="15" OnPageChanged="AspNetPager2_PageChanged">
                </webdiyer:AspNetPager>


    </div>
    </form>
</body>
</html>

