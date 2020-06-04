<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OneDispose.aspx.cs" Inherits="Case_OneDispose" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>前处理操作</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../js/calendar.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 32px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            工程管理 》》前处理操作
        </div>
        <table class="Admin_Table">
            <tr>
                <td>任务号：</td>
                <td>
                    <asp:TextBox ID="txt_taskid" runat="server" Width="196px" ReadOnly="true"></asp:TextBox>
                </td>
                <td>部门：</td>
                <td>
                    <asp:TextBox ID="txt_deparment" runat="server" Width="192px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>开始时间</td>
                <td>
                    <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
                        style="width: 194px" type="text" visible="true" /></td>
                <td>结束时间</td>
                <td>
                    <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
                        style="width: 193px" type="text" visible="true" /></td>
            </tr>
            <tr>
                <td>结果：</td>
                <td colspan="3">
                    <asp:TextBox ID="txt_result" runat="server" Width="733px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" align="center" class="auto-style1">
                    <asp:Button ID="btn_yes" runat="server" Text="确定" Width="101px" OnClientClick="javascript:return confirm('提交后不可删改');" OnClick="btn_yes_Click" />
                </td>
            </tr>
        </table>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
