<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CountShiJi.aspx.cs" Inherits="ShiXiao_CountShiJi" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>时效监督 </title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <script type="text/javascript">


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
    <div>
    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand"  OnRowDataBound="GridView1_RowDataBound">
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <Columns>
                            <asp:BoundField DataField="rwbianhao" HeaderText="任务号" />
                            <asp:BoundField DataField="beizhu3" HeaderText="完成"  Visible ="false" />
                            <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号" />
                            <asp:BoundField DataField="na" HeaderText="名称" Visible ="false"/>
                            <asp:BoundField DataField="gg" HeaderText="型号" Visible ="false"/>
                            <asp:BoundField DataField="" HeaderText="实际" />
                            <asp:BoundField DataField="" HeaderText="总共" />
                            <asp:BoundField DataField="xiada" HeaderText="下达日期"  />
                            <asp:BoundField DataField="shixian" HeaderText="考核" />
                            <asp:BoundField DataField="beizhu3" HeaderText="完成" />
                            <asp:BoundField DataField="" HeaderText="报告状态"  Visible ="false"/>
                             <asp:BoundField DataField="state" HeaderText="案件状态" />

                            <asp:BoundField DataField="kf" HeaderText="客服" />
                            <asp:BoundField DataField="" HeaderText="工程师"  Visible ="false"/>
                           

                            <asp:HyperLinkField HeaderText="资料" Visible ="false" Text="资料" Target="_blank" DataNavigateUrlFormatString="~/Case/ziliaoaddM.aspx?xiangmuid={0}"
                                DataNavigateUrlFields="rwbianhao" />
                            <asp:HyperLinkField HeaderText="操作" Text="分派" Visible ="false" Target="_blank" DataNavigateUrlFormatString="~/Case/FenPaiKeFu.aspx?xiangmuid={0}"
                                DataNavigateUrlFields="rwbianhao" />
                            <asp:BoundField DataField="xiada" HeaderText="下达日期"  Visible ="false" />
                            
                              <asp:HyperLinkField HeaderText="明细" Text="明细"  Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=0"
                    DataNavigateUrlFields="bianhao" />

                        <asp:TemplateField HeaderText="操作"  Visible ="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton5" runat="server" Text="暂停" Visible ="false" CommandArgument='<%# Eval("rwbianhao") %>'
                            CommandName="xiada"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>

                  <asp:HyperLinkField HeaderText="附件" Text="上传" Visible ="false" Target="_blank" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?id={0}&&baojiaid={1}"
                    DataNavigateUrlFields="id,baojiaid" />

                    <asp:TemplateField HeaderText="序号" >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>'
                            CommandArgument='<%# Eval("id") %>' CommandName="BussinessNeeds" ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                    


                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>


                      <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第%StartRecordIndex%-%EndRecordIndex%"
                        CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                        InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager2_PageChanged"
                        PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                        Style="font-size: 9pt" UrlPaging="True" PageSize="15" Width="100%">
                    </webdiyer:AspNetPager>

    </div>
    </form>
</body>
</html>
