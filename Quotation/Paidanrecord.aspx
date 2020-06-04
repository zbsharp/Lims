﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paidanrecord.aspx.cs" Inherits="Quotation_QuotationHuiqian" %>

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
        .no {
            display: none;
        }
    </style>
</head>
<body>

    <form id="form2" runat="server">
        <div class="Body_Title">
            客户管理 》》排单记录</div>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="kehuname">客户名称</asp:ListItem>
            <asp:ListItem Value="bj.BaoJiaId">报价编号</asp:ListItem>
            <asp:ListItem Value="bj.responser">报价人</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1"
            runat="server" Text="查询" OnClick="Button1_Click" />

        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table"
            AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" DataKeyNames="id">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>' ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                <asp:BoundField DataField="bjid" HeaderText="报价编号" />

                <asp:BoundField HeaderText="任务编号" DataField="rwid" />
                <asp:BoundField DataField="weituo" HeaderText="客户" />
                <asp:BoundField HeaderText="报价人" DataField="baojiaren" />
<asp:BoundField DataField="tiandanren" HeaderText="填单人"></asp:BoundField>
                <asp:BoundField HeaderText="报价日期" DataField="baojiariqi" DataFormatString="{0:d}" />

                <asp:BoundField DataField="tiandanriqi" HeaderText="填单日期" DataFormatString="{0:d}" />
                <asp:BoundField DataField="id" HeaderText="id" DataFormatString="id" HeaderStyle-CssClass="no" ItemStyle-CssClass="no"/>

                <asp:TemplateField HeaderText="预览">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_yulan" runat="server" CommandName="yulan" CommandArgument='<%#Eval("id") %>'>预览</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
        </asp:GridView>



        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
            Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="12">
        </webdiyer:AspNetPager>

        <asp:Literal ID="Literal1" runat="server"></asp:Literal>

    </form>
</body>
</html>

