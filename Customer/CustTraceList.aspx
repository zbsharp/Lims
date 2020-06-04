<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustTraceList.aspx.cs" Inherits="Customer_CustTraceList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
   
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="div_All">
        <div class="Body_Title">
            销售管理 》》联系日志</div>
        <div>
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Value="CustomName">客户名称</asp:ListItem>
                <asp:ListItem Value ="responser">业务人员</asp:ListItem>
               
            </asp:DropDownList>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1"
                runat="server" Text="查询" onclick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="全部" onclick="Button2_Click" />
        </div>
        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                            CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>

                 <asp:BoundField DataField="kehuname" HeaderText="客户">
                                 
                                </asp:BoundField>
               <asp:BoundField DataField="neirong" HeaderText="内容">
                                 
                                </asp:BoundField>
                                  <asp:BoundField DataField="style" HeaderText="方式">
                                   
                                </asp:BoundField>
                                  <asp:BoundField DataField="result" HeaderText="结果">
                                   
                                </asp:BoundField>
                                <asp:BoundField DataField="filltime" HeaderText="填写时间" DataFormatString="{0:d}">
                                  
                                </asp:BoundField>
                                 <asp:BoundField DataField="responser" HeaderText="填写人">
                                    
                                </asp:BoundField>
                                <asp:BoundField DataField="zhongyao" HeaderText="联系人" />


                                <asp:BoundField DataField="xiacitime" HeaderText="下次跟踪日期" Visible ="false"  DataFormatString="{0:d}" />
                                          <asp:HyperLinkField DataNavigateUrlFields="genzongid" DataNavigateUrlFormatString="~/Customer/Update.aspx?kehuid={0}"
                    HeaderText="" Text="修改日期"  Target ="button" Visible="False"/>                   
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
        </asp:GridView>
          <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                                    CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                                    InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
                                    PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                                    Style="font-size: 9pt" UrlPaging="True" PageSize="15">
                                </webdiyer:AspNetPager> 
    </div>
    </form>
</body>
</html>
