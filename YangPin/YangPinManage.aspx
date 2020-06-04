﻿<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" CodeFile="YangPinManage.aspx.cs"
    Inherits="YangPinManage" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js">
  
    <script type="text/javascript" src="popcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
        业务受理 》》样品查询</div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
    <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
        <tr>
            <td align="left" style="height: 25px">
                &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 收到日期：<input id="txFDate" name="txFDate" class="TxCss"
                    type="text" value="" onclick="popUpCalendar(this,document.forms[0].txFDate,'yyyy-mm-dd')"
                    readonly runat="server" style="width: 111px" />
                到&nbsp;<input id="txTDate" name="txTDate" class="TxCss" type="text" value="" onclick="popUpCalendar(this,document.forms[0].txTDate,'yyyy-mm-dd')"
                    readonly runat="server" style="width: 111px" />
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="0">全部</asp:ListItem>
                    <asp:ListItem Value="1">厂商名称</asp:ListItem>
                    <asp:ListItem Value="2">样品名称</asp:ListItem>
                    <asp:ListItem Value="3">样品编号</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList2" runat="server">
                    <asp:ListItem>入库</asp:ListItem>
                    <asp:ListItem>借出</asp:ListItem>
                    <asp:ListItem>归还</asp:ListItem>
                    <asp:ListItem>封存</asp:ListItem>
                    <asp:ListItem>清退</asp:ListItem>
                    <asp:ListItem>销毁</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
                <asp:Button ID="Button2" CssClass="BnCss" runat="server" OnClick="Button2_Click"
                    Text="查询" />
                <asp:Button ID="Button1" runat="server" CssClass="BnCss" OnClick="Button1_Click"
                    Text="导出excel" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                            CommandArgument='<%# Eval("id") %>' CommandName="BussinessNeeds" ForeColor="Green"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="sampleid" HeaderText="样品编号" />
                                <asp:BoundField DataField="receivetime" DataFormatString="{0:d}" HeaderText="收到日期" />
                                <asp:BoundField DataField="kehuname" HeaderText="厂商名称" Visible="false" />
                                <asp:BoundField DataField="name" HeaderText="样品名称" />
                                <asp:BoundField DataField="model" HeaderText="型号" />
                                <asp:BoundField DataField="position" HeaderText="制造厂商" />
                                <asp:BoundField DataField="type" HeaderText="报告种类" />
                                <asp:BoundField DataField="remark" HeaderText="备注" />
                                <asp:BoundField DataField="state" HeaderText="当前状态" />
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <span style="cursor: hand; color: Blue;" onclick="window.open('YangPin_Jiechu.aspx?sampleid=<%#Eval("sampleid") %>')">
                                            <asp:Label ID="seeLB" runat="server" Text="借出"></asp:Label></span> || <span style="cursor: hand;
                                                color: Blue;" onclick="window.open('YangPin_FanHuan.aspx?sampleid=<%#Eval("sampleid") %>')">
                                                <asp:Label ID="Label1" runat="server" Text="返还"></asp:Label></span> ||
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="处置">
                                    <ItemTemplate>
                                        <span style="cursor: hand; color: Blue;" onclick="window.open('YangPinFenCun.aspx?sampleid=<%#Eval("sampleid") %>')">
                                            <asp:Label ID="seeLB" runat="server" Text="封存"></asp:Label></span>|| <span style="cursor: hand;
                                                color: Blue;" onclick="window.open('YangPinPiLiang.aspx?sampleid=<%#Eval("sampleid") %>')">
                                                <asp:Label ID="Label4" runat="server" Text="清退"></asp:Label></span>|| <span style="cursor: hand;
                                                    color: Blue;" onclick="window.open('YangPinPiLiang.aspx?sampleid=<%#Eval("sampleid") %>')">
                                                    <asp:Label ID="Label5" runat="server" Text="销毁"></asp:Label></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="编辑">
                                    <ItemTemplate>
                                        <span style="cursor: hand; color: Blue;" onclick="window.open('YangPinSee.aspx?yangpinid=<%#Eval("id") %>')">
                                            <asp:Label ID="seeLB" runat="server" Text="编辑"></asp:Label></span>|| <span style="cursor: hand;
                                                color: Blue;" onclick="window.open('../Print/YangPinPrint.aspx?sampleid=<%#Eval("sampleid") %>')">
                                                <asp:Label ID="Label3" runat="server" Text="标签"></asp:Label></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="附件" Text="附件" Target="_blank" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?baojiaid={0}&amp;&amp;id={1}"
                                    DataNavigateUrlFields="baojiaid,kehuid" />
                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                            <EmptyDataTemplate>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时没有数据"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第%StartRecordIndex%-%EndRecordIndex%"
                            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager2_PageChanged"
                            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                            Style="font-size: 9pt" UrlPaging="True" Width="682px">
                        </webdiyer:AspNetPager>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
