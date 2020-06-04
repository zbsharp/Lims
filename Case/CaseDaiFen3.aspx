﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseDaiFen3.aspx.cs" Inherits="Case_CaseDaiFen3" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
    <div class="Body_Title">
        工程管理 》》重新分派工程师</div>
    <asp:DropDownList ID="DropDownList1" runat="server" Width="77px" Visible="false">
        <asp:ListItem Value="kehuname">客户名称</asp:ListItem>
        <asp:ListItem Value="fillname">业务人员</asp:ListItem>
        <asp:ListItem Value="baojiaid">报价编号</asp:ListItem>
        <asp:ListItem Value="taskid">任务编号</asp:ListItem>

        <asp:ListItem Value="shenqingbianhao">申请编号</asp:ListItem>

    </asp:DropDownList>
    任务编号或工程师或部门或客户或申请编号或生产企业：
    <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
    <asp:Button ID="Button2" CssClass="BnCss" runat="server" OnClick="Button2_Click"
        Text="查询" />
    <asp:DropDownList ID="DropDownList3" runat="server" Visible ="false" >
        <asp:ListItem Value="jieshoutime desc">完成日期逆序</asp:ListItem>
        <asp:ListItem Value="jieshoutime asc">完成日期顺序</asp:ListItem>
    </asp:DropDownList>
    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False"
        DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="序号" Visible ="false" >
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                        CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle ForeColor="Green" />
            </asp:TemplateField>
               <asp:BoundField DataField="taskid" HeaderText="任务编号" />
            <asp:BoundField DataField="shenqinghao" HeaderText="申请号" />
            <asp:BoundField DataField="kehuname" HeaderText="委托方" />
            <asp:BoundField DataField="bumen" HeaderText="承接部门" />
            <asp:BoundField DataField="kf" HeaderText="项目经理" />

          
            <asp:BoundField DataField="gc" HeaderText="工程师" />

            <asp:BoundField DataField="" HeaderText="状态" />
           
          <asp:BoundField DataField="ziliao" HeaderText="资料状态"   />
            <asp:BoundField DataField="xiadariqi" HeaderText="下达日期"   />
            <asp:BoundField DataField="yaoqiuwanchengriqi" HeaderText="要求完成日期" />
         
            <asp:HyperLinkField Text="工程师" HeaderText="修改分派" Target="button" DataNavigateUrlFormatString="~/Case/AnJianInFo.aspx?xiangmuid={0}&&bumen={1}&&id={2}"
                DataNavigateUrlFields="taskid,bumen,id" />

                 

        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
        </EmptyDataTemplate>
        <HeaderStyle CssClass="Admin_Table_Title " />
    </asp:GridView>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
        CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
        InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
        PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
        Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="15">
    </webdiyer:AspNetPager>

    <asp:Literal ID="ld" runat="server"></asp:Literal>


    </form>
</body>
</html>
