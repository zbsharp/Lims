<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PDF.aspx.cs" Inherits="EMCmate_PDF" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;文档生成过程中如遇比较慢，请等待     &nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="生成" Width="114px" OnClick="Button1_Click" />
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" CssClass="Admin_Table" Width="100%" AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="GridView1_RowCommand">
                <Columns>
                    <asp:BoundField DataField="chargeid" HeaderText="收费单编号" />
                    <asp:BoundField DataField="filltime" HeaderText="生成时间" />
                    <asp:BoundField DataField="fillname" HeaderText="生成人" />
                    <asp:TemplateField HeaderText="下载">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtndownload" runat="server" CommandArgument="chargeid" CommandName="download">下载</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Admin_Table_Title" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
