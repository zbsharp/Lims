﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KuaiDiManage.aspx.cs" Inherits="Case_KuaiDiManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <script type="text/javascript" src="../js/calendar.js"></script>

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
    <form name="form1" runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
    <div>
        <div class="Body_Title">
            业务管理 》》快递列表</div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    &nbsp;&nbsp; 寄件日期： <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />
             查询条件：<asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="bianhao">编号</asp:ListItem>
                        <asp:ListItem Value="jijianren">寄件人</asp:ListItem>
                        <asp:ListItem Value="shoujianren">收件人</asp:ListItem>
                        <asp:ListItem Value="shoujiandizhi">收件地址</asp:ListItem>
                        <asp:ListItem Value="shoujiandanwei">收件单位</asp:ListItem>

                    </asp:DropDownList>
                    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                        Text="查询" />
                </td>
            </tr>
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="id" class="Admin_Table" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                               <HeaderStyle CssClass="Admin_Table_Title " />
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                                CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Green" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="bianhao" HeaderText="编号" />
                                    <asp:BoundField DataField="jijianren" HeaderText="寄件人" />
                                    <asp:BoundField DataField="jijianriqi" DataFormatString="{0:d}" HeaderText="寄件日期" />
                                    <asp:BoundField DataField="shoujiandanwei" HeaderText="收件单位" />
                                    <asp:HyperLinkField HeaderText="回执上传" Text="回执上传" Target="_blank" DataNavigateUrlFormatString="~/Case/KuaiDiUpload.aspx?id={0}"
                                        DataNavigateUrlFields="id" />
                                    <asp:HyperLinkField HeaderText="顺打印" Text="顺打印" Target="_blank" DataNavigateUrlFormatString="~/Print/KuaiDi.aspx?bianhao={0}"
                                        DataNavigateUrlFields="bianhao" />
                                         <asp:HyperLinkField HeaderText="德打印" Text="德打印" Target="_blank" DataNavigateUrlFormatString="~/Print/KuaiDi2.aspx?bianhao={0}"
                                        DataNavigateUrlFields="bianhao" />

                                        <asp:HyperLinkField HeaderText="明细" Text="明细" Target="_blank" DataNavigateUrlFormatString="KuaiDiSee.aspx?id={0}"
                                        DataNavigateUrlFields="id" />

                                    <asp:TemplateField HeaderText="明细" Visible ="false" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton8" runat="server" Text="查看" ForeColor="blue" CommandArgument='<%# Eval("id") %>'
                                                CommandName="chakan"></asp:LinkButton>
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
                                Style="font-size: 9pt" UrlPaging="True" PageSize ="15" Width="682px">
                            </webdiyer:AspNetPager>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
