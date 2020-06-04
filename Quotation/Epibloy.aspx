<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Epibloy.aspx.cs" Inherits="Quotation_Epibloy" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript">
        function WB_infomation() {
            window.open('WBinfomation.aspx?=<%#Eval("id")%>'
                , 'test', 'dialogWidth=600px;DialogHeight=200;status:no;help:no;resizable:yes;dialogTop:100px;edge:raised;');

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            报价管理 》》外包项目
        </div>
        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table"
            AutoGenerateColumns="False" DataKeyNames="id" OnRowCreated="GridView1_RowCreated" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="baojiaid" HeaderText="报价编号" />
                <asp:BoundField DataField="kehuid" HeaderText="客户编号" />
                <asp:BoundField DataField="neirong" HeaderText="测试项目" />
                <asp:BoundField DataField="shoufei" HeaderText="价格" />
                <asp:BoundField DataField="yp" HeaderText="样品" />
                <asp:BoundField DataField="zhouqi" HeaderText="周期" />
                <asp:BoundField DataField="beizhu" HeaderText="备注" />
                <asp:BoundField DataField="fillname" HeaderText="录入人" />
                <asp:BoundField DataField="filltime" HeaderText="录入时间" />
                <asp:BoundField DataField="danwei" HeaderText="单位" />
                <asp:BoundField DataField="state" HeaderText="审核状态" />
                <asp:BoundField DataField="verifier" HeaderText="审核人" />
                <asp:HyperLinkField HeaderText="审批操作" DataNavigateUrlFields="id" DataNavigateUrlFormatString="~/Quotation/ShenPi.aspx?id={0}" Target="button" Text="审批" />
                <asp:TemplateField HeaderText="报价单">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" Text="查看报价单" OnClick="Button1_Click" />
                    </ItemTemplate>

                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <input type="button" value="外包信息"  onclick="window.open('WBinfomation.aspx?id=<%#Eval("id") %> ','test','dialogWidth=600px;DialogHeight=200;status:no;help:no;resizable:yes;dialogTop:100px;edge:raised;')"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
        </asp:GridView>
    </form>
</body>
</html>
