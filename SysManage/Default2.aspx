<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="SysManage_Default2" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
  
      <div class="Body_Title">
            系统管理 》》用户信息</div>	

    <asp:Label ID="My" runat="server" Text=""></asp:Label>
    &nbsp;&nbsp; 用户名称或职位或部门:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;
    <asp:DropDownList ID="DropDownList1" Visible ="false"  runat="server">

    <asp:ListItem Value="">全部</asp:ListItem>
        <asp:ListItem Value="username">用户名称</asp:ListItem>
        <asp:ListItem Value="jiaosename">职位名称</asp:ListItem>
        <asp:ListItem Value="departmentname">所在部门</asp:ListItem>
    </asp:DropDownList>
    &nbsp;
    <asp:Button ID="Button1" class="BnCss" runat="server" Text="查 询" OnClick="Button1_Click1"
        Width="60px" />
    <asp:Button ID="Button2" class="BnCss" runat="server" Text="增加用户"  />
    <asp:GridView ID="GridView1" runat="server" CssClass="Admin_Table" AutoGenerateColumns="False"
        DataKeyNames="id" 
        >
        <Columns>
            <asp:TemplateField HeaderText="序号">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%#(Container.DisplayIndex+1).ToString("000")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="username" HeaderText="用户编号" ReadOnly="True" />
           
            <asp:BoundField DataField="departmentname" HeaderText="所在部门" />
            <asp:BoundField DataField="jiaosename" HeaderText="职位名称" />
            <asp:BoundField DataField="youxiang" HeaderText="电子邮箱" />
            <asp:BoundField DataField="banggongdianhua" HeaderText="办公电话" />
            <asp:BoundField DataField="yidong" HeaderText="移动电话" />
            <asp:BoundField DataField="fax" HeaderText="传真" />
         <asp:BoundField DataField="shortphone" HeaderText="短号" />
            <asp:TemplateField HeaderText="修改">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton4" runat="server" Text="修改" CommandArgument='<%# Eval("id") %>'
                        CommandName="log"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle ForeColor="Green" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="冻结">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton5" runat="server" Text="冻结" CommandArgument='<%# Eval("id") %>'
                        CommandName="cancel1"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle ForeColor="Green" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="解冻">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton6" runat="server" Text="解冻" CommandArgument='<%# Eval("id") %>'
                        CommandName="cancel2"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle ForeColor="Green" />
            </asp:TemplateField>
           
            <asp:BoundField DataField="flag" HeaderText="是否被取消" />
        </Columns>

           <HeaderStyle CssClass="Admin_Table_Title " />
    </asp:GridView>

    <webdiyer:aspnetpager id="AspNetPager2" runat="server" PageSize="12" AlwaysShow="True" ShowCustomInfoSection="Left" ShowDisabledButtons="false" ShowPageIndexBox="always" PageIndexBoxType="DropDownList"
    CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;%PageSize%/Page&nbsp;&nbsp;Order:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%" UrlPaging="true" OnPageChanged="AspNetPager2_PageChanged"></webdiyer:aspnetpager>
    <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>
