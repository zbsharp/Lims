<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustTraceAdd.aspx.cs" Inherits="Customer_CustTraceAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="Body_Title">
            销售管理 》》
            <asp:Label ID="Label1" runat="server" Text="<%#name %>"></asp:Label><input name="txTitle" type="text" id="txTitle" visible="false" value="<%#username%>" class="TxCss"
                style="width: 147px;" runat="server" />
        </div>



        <table class="Admin_Table">


            <tr>
                <td>联系方式  
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server">
                        <asp:ListItem>电话沟通</asp:ListItem>
                        <asp:ListItem>登门拜访</asp:ListItem>
                        <asp:ListItem>茶亭闲谈</asp:ListItem>
                        <asp:ListItem>其他方式</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>联系结果</td>
                <td>
                    <asp:DropDownList ID="DropDownList3" runat="server">
                        <asp:ListItem>报价</asp:ListItem>
                        <asp:ListItem>签单</asp:ListItem>
                        <asp:ListItem>待跟进</asp:ListItem>
                        <asp:ListItem>无果</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>发生日期：
                </td>
                <td>
                    <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="popUpCalendar(this, document.forms[0].txFDate, 'yyyy-mm-dd')" value="<%#dt%>"
                        readonly="readonly" type="text" style="width: 148px" />
                    <input id="Text1" runat="server" class="TxCss" visible="false" name="txFDate" onclick="popUpCalendar(this, document.forms[0].Text1, 'yyyy-mm-dd')"
                        type="text" style="width: 148px" />&nbsp;<asp:DropDownList ID="DropDownList1"
                            runat="server">
                            <asp:ListItem>重要</asp:ListItem>
                            <asp:ListItem>一般</asp:ListItem>
                            <asp:ListItem>可删</asp:ListItem>
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>内容：
                </td>
                <td>
                    <textarea name="txContent" cols="20" id="txContent" class="TxCss" style="height: 138px; width: 510px;"
                        runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="Button1" runat="server" Text="保  存" class="BnCss" OnClick="Button1_Click" />
                    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                    <input id="Reset1" type="reset" value="重 置" class="BnCss" runat="server" />
                </td>
            </tr>
        </table>



        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False" OnRowEditing="GridView2_RowEditing" DataKeyNames="genzongid" OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowDeleting="GridView2_RowDeleting" OnRowUpdating="GridView2_RowUpdating">

                    <Columns>
                        <asp:BoundField DataField="genzongid" HeaderText="日志编号" Visible="False" />
                        <asp:BoundField DataField="neirong" HeaderText="内容">
                            <ItemStyle Width="30%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="方式">
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="funtion">
                                    <asp:ListItem>电话沟通</asp:ListItem>
                                    <asp:ListItem>登门拜访</asp:ListItem>
                                    <asp:ListItem>茶亭闲谈</asp:ListItem>
                                    <asp:ListItem>其他方式</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("style") %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("style") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="result" HeaderText="结果" ReadOnly="True">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="filltime" HeaderText="填写时间" DataFormatString="{0:D}" ReadOnly="True">
                            <ItemStyle Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="responser" HeaderText="填写人"  ReadOnly="True">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="zhongyao" HeaderText="联系人" ReadOnly="True" />
                        <asp:CommandField HeaderText="操作" ShowDeleteButton="True" ShowEditButton="True" />
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title " />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>


    </form>
</body>
</html>
