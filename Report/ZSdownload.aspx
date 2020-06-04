<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZSdownload.aspx.cs" Inherits="Report_ZSdownload" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>报告批准
    </title>

    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>

</head>
<body>
    <form name="form1" runat="server" id="form1">
        <div>

            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
            </asp:ScriptManager>

            <div class="Body_Title">
                报告管理 》》正式报告下载
            </div>
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                width="100%">
                <tr bgcolor="#f4faff">
                    <td colspan="4">
                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" CaptionAlign="Left"
                            DataKeyNames="id" OnRowCommand="GridView5_RowCommand" OnRowDataBound="GridView5_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="caseid" HeaderText="报告编号" />
                                <asp:HyperLinkField DataNavigateUrlFields="urltext" DataTextField="filename" HeaderText="文件名称">
                                    <ItemStyle ForeColor="Green" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="leibie" HeaderText="附件类型" />
                                <asp:BoundField DataField="typ" HeaderText="文件类型" />
                                <asp:TemplateField HeaderText="正式报告">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="dowtxt">下载</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />

                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
