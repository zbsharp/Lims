<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Deleterecord.aspx.cs" Inherits="Case_Deleterecord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客服删除项目记录</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            客服管理 》》客服删除项目记录
        </div>
        <table style="width: 100%" class="Admin_Table">
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="Admin_Table">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="编号" />
                            <asp:BoundField DataField="baojiaid" HeaderText="报价编号" />
                            <asp:BoundField DataField="weituodanwei" HeaderText="委托单位" />
                            <asp:BoundField DataField="xmname" HeaderText="项目名称" />
                            <asp:BoundField DataField="fillname" HeaderText="删除人" />
                            <asp:BoundField DataField="filltime" HeaderText="删除时间" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title"/>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
