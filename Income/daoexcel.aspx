<%@ Page Language="C#" AutoEventWireup="true" CodeFile="daoexcel.aspx.cs" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" Inherits="shoufei_daoexcel" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body {
            font-size: 12px;
            cursor: default;
            font-family: 宋体;
        }
         
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="text" cellspacing="1" cellpadding="0" width="100%" bgcolor="#1d82d0"
                border="0">
                <tr bgcolor="#ffffff">
                    <td valign="top">
                        <table class="text" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td width="15">&nbsp;
                                </td>
                                <td valign="top" width="100%">
                                    <table class="text" cellspacing="1" cellpadding="0" width="100%" border="0">
                                        <tr height="30">
                                            <td style="width: 151px">
                                                <font face="宋体">请选择要导入的文件(6列)</font>
                                            </td>
                                            <td style="width: 350px" align="left" width="350">
                                                <input id="FileExcel" style="width: 300px" type="file" size="42" name="filephoto"
                                                    runat="server"><font color="red"></font>
                                            </td>
                                            <td class="hint">
                                                <font face="宋体">
                                                    <asp:Button ID="BtnImport" Text="导 入" CssClass="button" runat="server"
                                                        OnClick="BtnImport_Click1" Width="60px"></asp:Button>
                                                    <asp:Button ID="Button1" runat="server" Text="取消导入" Visible="false" OnClick="Button1_Click"
                                                        Width="78px" /><%--批此号：--%><asp:TextBox
                                                            ID="TextBox1" runat="server" Width="74px" Visible="false"></asp:TextBox></font>
                                                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="确认" Visible="False" />
                                            </td>
                                        </tr>
                                    </table>
                                    (付款日期,付款单位,付款金额,金额单位,备注)
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="LblMessage" runat="server" Font-Bold="True" ForeColor="Red" Width="224px"></asp:Label>
                        <br />
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="id" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound"
                            OnRowDeleting="GridView1_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="序 号" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>'
                                            CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="liushuihao" HeaderText="流水号" ReadOnly="True" />
                                <asp:BoundField DataField="fukuanren" HeaderText="付款人" />
                                <asp:BoundField DataField="fukuanriqi" HeaderText="付款日期" />
                                <asp:BoundField DataField="danwei" HeaderText="币种" />
                                <asp:BoundField DataField="fukuanjine" HeaderText="付款金额" />
                                <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                <asp:BoundField DataField="daoruren" HeaderText="导入人" />
                                <asp:BoundField DataField="daorutime" DataFormatString="{0:d}" HeaderText="导入时间" />
                                <asp:BoundField DataField="fapiaoleibie" HeaderText="批次" />
                                <asp:BoundField DataField="beizhu2" HeaderText="是否需要确认" Visible="False" />
                                <asp:HyperLinkField DataNavigateUrlFields="liushuihao" HeaderText="开票" Target="_blank" DataNavigateUrlFormatString="FapiaoAdd.aspx?liushuihao={0}"
                                    Text="开票" Visible="False" />
                                <asp:CommandField ShowDeleteButton="true" DeleteText="删除" Visible="false"/>
                                <%--   <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " PrevPageText="【前页】 "
                            ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                            Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="15" OnPageChanged="AspNetPager2_PageChanged">
                        </webdiyer:AspNetPager>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
