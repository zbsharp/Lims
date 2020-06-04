<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerExcel.aspx.cs" Inherits="Customer_CustomerExcel" %>

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
    <form id="form1" runat="server">
    <div>
        <div class="Body_Title">
            销售管理 》》客户信息（通过Excel导入客户资料）</div>
        <div>
            <table cellspacing="1" cellpadding="0" width="100%" border="0">
                <tr height="30">
                    <td style="width: 151px">
                        <font face="宋体">请选择要导入的文件</font>
                    </td>
                    <td style="width: 350px" align="left" width="350">
                        <input id="FileExcel" style="width: 300px" type="file" size="42" name="filephoto"
                            runat="server"><font color="red"></font>
                    </td>
                    <td class="hint">
                        <font face="宋体">
                            <asp:Button ID="BtnImport" Text="导 入" CssClass="button" runat="server"></asp:Button>
                        </font>
                    </td>
                </tr>
            </table>
            <asp:Label ID="LblMessage" runat="server" Font-Bold="True" ForeColor="Red" Width="224px"></asp:Label>
            <br />
            <asp:GridView ID="GridView1" runat="server" CssClass="Admin_Table" AutoGenerateColumns="False" Width="100%"
               OnRowDataBound="GridView1_RowDataBound"
               DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting">
                
                <Columns>
                     <asp:TemplateField HeaderText="编号" SortExpression="kehuid">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("kehuid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="customname"  HeaderText="客户名称" />
                <asp:BoundField DataField="CustomType"  HeaderText="客户类型" />
                <asp:BoundField DataField="CustomSouce"  HeaderText="客户来源" />
                <asp:BoundField DataField="fillname"  HeaderText="填写人" />
               
                <asp:BoundField DataField="filltime" HeaderText="录入时间" DataFormatString="{0:d}"  />
                <asp:BoundField DataField="b" HeaderText="是否分派" />
                <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Customer/CustomerSee.aspx?kehuid={0}"
                    HeaderText="" Text="查看客户"  />
                    <asp:TemplateField HeaderText="联系人" Visible="False">
                        <ItemTemplate>
                            <a href="#" class="BnCss" onclick="window.open('addlianxiren.aspx?kehuid=<%#Eval("kehuid")%>','','width=350,height=200,top=220,left=300')">
                                <span style="">增加</span></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
                <HeaderStyle CssClass="Admin_Table_Title " />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
