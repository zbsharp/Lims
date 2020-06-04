<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustManage3.aspx.cs" Inherits="Customer_CustManage3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <style type="text/css">
        .kehuid {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_All">
            <div class="Body_Title">
                销售管理 》》客户信息
            </div>
            <div>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="customname">客户名称</asp:ListItem>
                    <asp:ListItem Value="responser">业务人员</asp:ListItem>

                    <asp:ListItem Value="contact">联系人</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:Button ID="Button1"
                    runat="server" Text="查询" OnClick="Button1_Click" /><asp:Button ID="Button2" runat="server"
                        OnClick="Button2_Click" Text="导出excel" Visible="False" />
            </div>




            <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False">
                <Columns>
                    <%--    <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Quotation/QuotationAdd.aspx?kehuid={0}"
                    HeaderText="" Text="客户报价" Target="_blank" />--%>

                    <asp:TemplateField HeaderText="序 号">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("0000") %>'
                                CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle ForeColor="Green" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编号" SortExpression="kehuid" HeaderStyle-CssClass="kehuid" ItemStyle-CssClass="kehuid">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("kehuid") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle CssClass="kehuid"></HeaderStyle>

                        <ItemStyle CssClass="kehuid"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="customname" HeaderText="客户名称" />
                    <asp:BoundField DataField="CustomType" HeaderText="客户类型" />
                    <asp:BoundField DataField="CustomSouce" HeaderText="客户来源" />
                    <asp:BoundField DataField="class" HeaderText="等级" Visible="False" />
                    <asp:BoundField DataField="Responser" HeaderText="归属人" />

                    <asp:BoundField DataField="filltime" HeaderText="录入时间" DataFormatString="{0:d}" />
                    <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Customer/CustomerSee.aspx?kehuid={0}"
                        HeaderText="" Text="查看客户" Target="button" />
                    <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Customer/Turncustomer.aspx?kehuid={0}"
                        HeaderText="操作" Text="分派" />

                </Columns>
                <HeaderStyle CssClass="Admin_Table_Title " />
            </asp:GridView>
        </div>

        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
            Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="15">
        </webdiyer:AspNetPager>





    </form>
</body>
</html>

