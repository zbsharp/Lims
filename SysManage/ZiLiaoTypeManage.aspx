﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZiLiaoTypeManage.aspx.cs"
    Inherits="SysManage_ZiLiaoTypeManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>校准测量中心 </title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <style type="text/css">
        .BnCss
        {}
    </style>
</head>
<body>
    <form name="form1" runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
    <div>
        <div class="Body_Title">
            系统管理 》》资料列表</div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
            width="100%">
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    录入时间：<input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="popUpCalendar(this,document.forms[0].txFDate,'yyyy-mm-dd')"
                        style="width: 90px" type="text" visible="true" />
                    到
                    <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="popUpCalendar(this,document.forms[0].txTDate,'yyyy-mm-dd')"
                        style="width: 90px" type="text" visible="true" />
                    &nbsp; 查询条件：<asp:DropDownList ID="DropDownList1" runat="server" Width="77px">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="1">名称</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                        Text="查询" />
                    <asp:Button ID="Button1" runat="server" CssClass="BnCss" OnClick="Button1_Click"
                        Text="导出Excel" Width="152px" />
                </td>
            </tr>
            <tr bgcolor="#f4faff">
                <td style="height: 5px">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="id" CssClass="Admin_Table" OnRowCommand="GridView1_RowCommand"
                                OnRowDataBound="GridView1_RowDataBound">
                               <HeaderStyle CssClass="Admin_Table_Title " />
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                                CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Green" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="name" HeaderText="名称" />
                                    <asp:BoundField DataField="leibie1" HeaderText="检测类别" />
                                    <asp:BoundField DataField="bianhao" HeaderText="编号" />
                                    <asp:BoundField DataField="ybyq" HeaderText="一般要求" />
                                    <asp:BoundField DataField="syqk" HeaderText="适应情况" />
                                    <asp:BoundField DataField="bumen" HeaderText="适应部门" />
                                    <asp:BoundField DataField="jingji" HeaderText="紧急情况" />
                                    <asp:BoundField DataField="beizhu" HeaderText="顺序" />
                                    <asp:BoundField DataField="fillname" HeaderText="录入人" />
                                    <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="录入时间" />
                                    <asp:TemplateField HeaderText="明细">
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
