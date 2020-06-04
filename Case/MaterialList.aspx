<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialList.aspx.cs" Inherits="Case_MaterialList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
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
    <form id="form2" runat="server">
    <div class="Body_Title">
        业务管理 》》资料清单</div>
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Value="kehuname">客户名称</asp:ListItem>
        <asp:ListItem Value="baojiaid">报价编号</asp:ListItem>
        <asp:ListItem Value="renwuhao">任务编号</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />
    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="false"
        OnRowDataBound="GridView1_RowDataBound" 
        onrowcommand="GridView1_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="序号">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                        CommandArgument='<%# Eval("baojiaid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle ForeColor="Green" />
            </asp:TemplateField>
            <asp:BoundField DataField="baojiaid" HeaderText="报价编号" />
            <asp:BoundField DataField="kehuid" HeaderText="客户" />
            <asp:BoundField DataField="renwuhao" HeaderText="任务号" />
            <asp:BoundField DataField="name1" HeaderText="资料及配件名称" />
            <asp:BoundField DataField="name2" HeaderText="描述" />
            <asp:BoundField DataField="name3" HeaderText="数量" />
            <asp:BoundField DataField="name4" HeaderText="备注" />
            <asp:BoundField HeaderText="录入人" DataField="fillname" />
            <asp:BoundField HeaderText="录入日期" DataField="filltime" DataFormatString="{0:d}" />
            <asp:BoundField HeaderText="确认标志" DataField="biaozhi" ReadOnly="True" />
            <asp:BoundField HeaderText="确认人" DataField="querenname" ReadOnly="True" />
            <asp:BoundField HeaderText="确认日期" DataField="querentime" ReadOnly="True" DataFormatString="{0:d}"/>
             <asp:TemplateField HeaderText="确认">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton5" runat="server" Text="确认" CommandArgument='<%# Eval("id") %>'
                        CommandName="cancel1"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle ForeColor="Green" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Admin_Table_Title " />
    </asp:GridView>
    <webdiyer:aspnetpager id="AspNetPager1" runat="server" custominfohtml="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
        custominfotextalign="Center" firstpagetext="【首页】" height="25px" horizontalalign="Center"
        inputboxstyle="width:19px" lastpagetext="【尾页】" nextpagetext="【下页】 " onpagechanged="AspNetPager1_PageChanged"
        prevpagetext="【前页】 " showcustominfosection="Left" showinputbox="Never" shownavigationtooltip="True"
        width="682px" style="font-size: 9pt" urlpaging="True" pagesize="12">
                                </webdiyer:aspnetpager>
    </form>
</body>
</html> 