<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Check.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Income_Check" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>请款单列表</title>
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
        <div class="Body_Title">
            业务管理 》》请款单列表
        </div>

        <div>
            <table class="Admin_Table" width="100%">
                <tr>
                    <td align="left" colspan="4">付款客户：<asp:Label ID="Label1" runat="server" Text=""></asp:Label>--付款金额：<asp:Label ID="Label2"
                        runat="server" Text=""></asp:Label>--<asp:Label ID="Label3" Visible="false" runat="server" Text=""></asp:Label>--付款日期：<asp:Label
                            ID="Label4" runat="server" Text=""></asp:Label>
                        <asp:Button ID="Button3" runat="server" Text="完成分款" OnClick="Button3_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4">客户名称或任务号或申请编号或联系人或付款单位或委托单位
                    <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True"></asp:TextBox><asp:DropDownList ID="DropDownList1"
                        runat="server" Visible="false">
                        <asp:ListItem>单选</asp:ListItem>
                        <asp:ListItem>多选</asp:ListItem>
                    </asp:DropDownList>
                        <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" /><asp:TextBox ID="TextBox3" runat="server" Enabled="false"></asp:TextBox>
                        <asp:DropDownList ID="DropDownList2" Visible="false" runat="server">
                            <asp:ListItem>否</asp:ListItem>
                            <asp:ListItem>是</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <tr>
                        <td align="left" colspan="4" style="text-align: center">
                            <asp:GridView ID="GridView1" ShowFooter="true" runat="server" class="Admin_Table" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="inid" HeaderText="收费编号" />
                                    <asp:BoundField DataField="taskno" HeaderText="任务编号" />

                                    <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号" />
                                    <asp:BoundField DataField="kehuname" HeaderText="客户" />


                                    <asp:BoundField DataField="feiyong1" HeaderText="请款金额" />
                                    <asp:BoundField DataField="name" HeaderText="客户联系人" />
                                    <asp:BoundField DataField="fillname" HeaderText="开单人" />


                                    <asp:BoundField DataField="hesuanbiaozhi" HeaderText="核算标志" />
                                    <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="开单日期" />
                                    <asp:TemplateField HeaderText="明细" Visible="true" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='' Text="明细"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>


                                    <asp:HyperLinkField HeaderText="核算" Text="核算" Target="_blank" DataNavigateUrlFormatString="~/Case/CeShiFeiKf.aspx?bianhao={0}&&baojiaid={1}&&kehuid={2}"
                                        DataNavigateUrlFields="bianhao2,baojiaid,kehuid" />

                                </Columns>
                                <HeaderStyle CssClass="Admin_Table_Title " />
                            </asp:GridView>
                        </td>
                    </tr>
            </table>
        </div>
        <asp:Literal ID="ld" runat="server"></asp:Literal>

    </form>
</body>
</html>
