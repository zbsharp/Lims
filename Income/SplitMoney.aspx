<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SplitMoney.aspx.cs" Inherits="Income_SplitMoney" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/Jquery.js"></script>
    <title>金额拆分</title>
    <style>
        .txt {
            margin-top: 10px;
            margin-bottom: 10px;
        }

        #btnSave {
            margin-left: 5.5%;
        }
    </style>
    <script>
        $(function () {
            $('.txt').keyup(function () {
                this.value = this.value.replace(/[^0-9.]/gi, "");
            });
        });
    </script>
</head>
<body>
    <%-- autocomplete="off"清除浏览器对文本框的缓存 --%>
    <form id="form1" runat="server" autocomplete="off">
        <div class="Body_Title">
            财务管理 》》金额拆分
        </div>
        <table class="Admin_Table">
            <tr>
                <td>流水号:</td>
                <td>
                    <asp:Label ID="lbserialnumber" runat="server" Text=""></asp:Label></td>
                <td>付款人:</td>
                <td>
                    <asp:Label ID="lbpayer" runat="server" Text=""></asp:Label></td>
                <td>付款日期:</td>
                <td>
                    <asp:Label ID="lbpaytime" runat="server" Text=""></asp:Label></td>
                <td>备注：
                </td>
                <td>
                    <asp:Label ID="lbremark" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>币种	:</td>
                <td>
                    <asp:Label ID="lbcurrency" runat="server" Text=""></asp:Label></td>
                <td>付款金额:</td>
                <td>
                    <asp:Label ID="lbmoney" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                <td>付款方式:</td>
                <td colspan="3">
                    <asp:Label ID="lbpaymethod" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td colspan="8">请输入拆分条数：<asp:TextBox ID="txcount" runat="server" Width="130px" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>
                    <asp:Button ID="btnAdd" runat="server" Text="确认" OnClick="btnAdd_Click" Height="26px" Width="66px" />
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    <asp:Button ID="btnsave" runat="server" Text="提交" Height="31px" OnClick="btnsave_Click" Visible="False" Width="103px" />
                </td>
            </tr>
        </table>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
