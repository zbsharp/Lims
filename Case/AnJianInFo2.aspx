<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnJianInFo2.aspx.cs" Inherits="Case_AnJianInFo2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>案件信息</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
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
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
    <div id="con_one_1">
        <table cellpadding="2" cellspacing="1" class="Admin_Table" style="width: 99%; margin: 5px auto;">
            <tr style="display: none;">
                <td>
                    当前状态:
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>未开始</asp:ListItem>
                        <asp:ListItem>进行中</asp:ListItem>
                        <asp:ListItem>已结束</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    转工程师:
                </td>
                <td>
                    部门
                    <asp:DropDownList ID="DropDownList2" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                    </asp:DropDownList>
                    人员
                    <asp:DropDownList ID="DropDownList3" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <table cellpadding="2" cellspacing="1" class="Admin_Table" style="width: 99%; margin: 5px auto;">
                <thead>
                    <tr class="Admin_Table_Title">
                        <th colspan="4">
                            基本信息
                        </th>
                    </tr>
                </thead>
                <tr>
                    <td colspan="4">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView3" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="rwbianhao" HeaderText="任务编号"></asp:BoundField>
                                        <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号" Visible="false"></asp:BoundField>
                                        <asp:BoundField DataField="state" HeaderText="状态"></asp:BoundField>
                                        <asp:BoundField DataField="shiyanleibie" HeaderText="试验类别"></asp:BoundField>
                                        <asp:BoundField DataField="xiadariqi" HeaderText="下单日期"></asp:BoundField>
                                        <asp:BoundField DataField="yaoqiuwanchengriqi" HeaderText="要求完成日期"></asp:BoundField>
                                        <asp:BoundField DataField="baogao" HeaderText="是否出报告"></asp:BoundField>
                                        <asp:BoundField DataField="kehuyaoqiu" HeaderText="客户要求"></asp:BoundField>
                                        <asp:BoundField DataField="youxian" HeaderText="优先级别"></asp:BoundField>
                                        <asp:BoundField DataField="beizhu" HeaderText="备注"></asp:BoundField>
                                        <asp:BoundField DataField="chenjieren" HeaderText="承接人" Visible ="false" ></asp:BoundField>
                                        <asp:BoundField DataField="fillname" HeaderText="填写人"></asp:BoundField>
                                        <asp:BoundField DataField="filltime" HeaderText="填写日期"></asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="Admin_Table_Title " />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <hr />
            <table cellpadding="2" cellspacing="1" class="Admin_Table" style="width: 99%; margin: 5px auto;">
                <thead>
                    <tr class="Admin_Table_Title">
                        <th colspan="4">
                            主检工程师列表
                        </th>
                    </tr>
                </thead>
                <tr>
                    <td colspan="4">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView4" runat="server" Width="100%" CssClass="Admin_Table"  DataKeyNames ="id"  onrowdeleting="GridView4_RowDeleting" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="bianhao" HeaderText="任务编号"></asp:BoundField>
                                        <asp:BoundField DataField="bumen" HeaderText="部门" ></asp:BoundField>
                                        <asp:BoundField DataField="name" HeaderText="工程师"></asp:BoundField>


                                        
                                        <asp:BoundField DataField="fillname" HeaderText="填写人"></asp:BoundField>
                                        <asp:BoundField DataField="filltime" HeaderText="填写日期"></asp:BoundField>
                                        <asp:CommandField ShowEditButton="false" ShowDeleteButton="true" CausesValidation="False" />
                                    </Columns>
                                    <HeaderStyle CssClass="Admin_Table_Title " />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <hr />
            <table align="center" width="99%">
                <thead>
                    <tr class="Admin_Table_Title">
                        <th colspan="4">
                            工作任务列表
                        </th>
                    </tr>
                </thead>
                <tr>
                    <td class="usertablerow2" colspan="4">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                                    CssClass="Admin_Table" DataKeyNames="Id" OnRowDataBound="GridView1_RowDataBound"
                                    OnRowCommand="GridView1_RowCommand" OnRowUpdating="GridView1_RowUpdating" Width="100%">
                                    <Columns>
                                        <asp:ButtonField CommandName="SingleClick" Text="SingleClick" Visible="False" />
                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                <asp:TextBox ID="Id" runat="server" Text='<%# Eval("Id") %>' Visible="false" Width="30px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="任务名称">
                                            <ItemTemplate>
                                                <asp:Label ID="timeLabel" runat="server" Text='<%# Eval("renwuname") %>'></asp:Label>
                                                <asp:DropDownList ID="renwuname" runat="server" Visible="false" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工程师">
                                            <ItemTemplate>
                                                <asp:Label ID="shebei1Label" runat="server" Text='<%# Eval("gongchengshi") %>'></asp:Label>
                                                <asp:DropDownList ID="gongchengshi" runat="server" Visible="false" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="计划开始">
                                            <ItemTemplate>
                                                <asp:Label ID="shebei2Label" runat="server" Text='<%# Eval("kaishiriqi") %>'></asp:Label>
                                                <asp:TextBox ID="shebei2" runat="server" Text='<%# Eval("kaishiriqi") %>' Visible="false"
                                                    Width="175px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="计划结束">
                                            <ItemTemplate>
                                                <asp:Label ID="shebei3Label" runat="server" Text='<%# Eval("jihuajieshu") %>'></asp:Label>
                                                <asp:TextBox ID="shebei3" runat="server" Text='<%# Eval("jihuajieshu") %>' Visible="false"
                                                    Width="175px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="实际开始">
                                            <ItemTemplate>
                                                <asp:Label ID="beizhu4Label" runat="server" Text='<%# Eval("beizhu4") %>'></asp:Label>
                                                <asp:TextBox ID="beizhu4" runat="server" Text='<%# Eval("beizhu4") %>' Visible="false"
                                                    Width="175px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="实际结束">
                                            <ItemTemplate>
                                                <asp:Label ID="shebei4Label" runat="server" Text='<%# Eval("shijijieshu") %>'></asp:Label>
                                                <asp:TextBox ID="shebei4" runat="server" Text='<%# Eval("shijijieshu") %>' Visible="false"
                                                    Width="175px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="任务状态">
                                            <ItemTemplate>
                                                <asp:Label ID="shebei5Label" runat="server" Text='<%# Eval("zhuangtai") %>'></asp:Label>
                                                <asp:DropDownList ID="zhuangtai" runat="server" Visible="false" AutoPostBack="true">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>未开始</asp:ListItem>
                                                    <asp:ListItem>正在进行</asp:ListItem>
                                                    <asp:ListItem>已完成</asp:ListItem>
                                                    <asp:ListItem>已延期</asp:ListItem>
                                                    <asp:ListItem>等待其他人</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="具体情况">
                                            <ItemTemplate>
                                                <asp:Label ID="shebei6Label" runat="server" Text='<%# Eval("beizhu") %>'></asp:Label>
                                                <asp:TextBox ID="shebei6" runat="server" Text='<%# Eval("beizhu") %>' Visible="false"
                                                    Width="175px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="步骤顺序">
                                            <ItemTemplate>
                                                <asp:Label ID="paixu1" runat="server" Text='<%# Eval("beizhu3") %>'></asp:Label>
                                                <asp:DropDownList ID="paixu" runat="server" Visible="false" AutoPostBack="true">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                    <asp:ListItem>9</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                    </Columns>
                                    <HeaderStyle CssClass="Admin_Table_Title " />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="usertablerow2" colspan="4" align="center">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" CssClass="BnCss"
                            Text="增加任务" />
                    </td>
                </tr>
                <tr>
                    <td align="center" class="usertablerow2" colspan="4">
                        <asp:TextBox ID="TextBox7" runat="server" Width="100%" TextMode="MultiLine" Height="37px"></asp:TextBox><br />
                        <asp:Button ID="Button6" runat="server" CssClass="BnCss" Text="增加备注" OnClick="Button6_Click" /><br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="GridView2" Width="100%" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                                    CssClass="Admin_Table">
                                    <Columns>
                                        <asp:BoundField DataField="xiangmuid" HeaderText="项目编号" />
                                        <asp:BoundField DataField="neirong" HeaderText="内容" />
                                        <asp:BoundField DataField="name" HeaderText="填写人" />
                                        <asp:BoundField DataField="time" HeaderText="填写时间" />
                                    </Columns>
                                    <HeaderStyle CssClass="Admin_Table_Title " />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button6" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="usertablerow2" colspan="4">
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DB %>"
                            DeleteCommand="DELETE FROM [anjianxinxi] WHERE [id] = @id" InsertCommand="INSERT INTO [anjianxinxi] ([xiangmuid], [kehuname], [renwuname], [gongchengshi], [kaishiriqi], [jihuajieshu], [shijijieshu], [zhuangtai], [beizhu], [tianxieren], [tianxietime], [beizhu1], [beizhu2], [beizhu3], [beizhu4]) VALUES (@xiangmuid, @kehuname, @renwuname, @gongchengshi, @kaishiriqi, @jihuajieshu, @shijijieshu, @zhuangtai, @beizhu, @tianxieren, @tianxietime, @beizhu1, @beizhu2, @beizhu3, @beizhu4)"
                            SelectCommand="SELECT * FROM [anjianxinxi] WHERE ([xiangmuid] = @xiangmuid) and leibie='工程' order by convert(int,beizhu3) asc "
                            UpdateCommand="UPDATE [anjianxinxi] SET   [renwuname] = @renwuname, [gongchengshi] = @gongchengshi, [kaishiriqi] = @kaishiriqi, [jihuajieshu] = @jihuajieshu, [shijijieshu] = @shijijieshu, [zhuangtai] = @zhuangtai, [beizhu] = @beizhu,[beizhu4] = @beizhu4,[beizhu3] = @beizhu3   WHERE [id] = @id">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="xiangmuid" QueryStringField="xiangmuid" Type="String" />
                            </SelectParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="id" Type="Int32" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="xiangmuid" Type="String" />
                                <asp:Parameter Name="kehuname" Type="String" />
                                <asp:Parameter Name="renwuname" Type="String" />
                                <asp:Parameter Name="gongchengshi" Type="String" />
                                <asp:Parameter Name="kaishiriqi" Type="String" />
                                <asp:Parameter Name="jihuajieshu" Type="String" />
                                <asp:Parameter Name="shijijieshu" Type="String" />
                                <asp:Parameter Name="zhuangtai" Type="String" />
                                <asp:Parameter Name="beizhu" Type="String" />
                                <asp:Parameter Name="tianxieren" Type="String" />
                                <asp:Parameter Name="tianxietime" Type="DateTime" />
                                <asp:Parameter Name="beizhu1" Type="String" />
                                <asp:Parameter Name="beizhu2" Type="String" />
                                <asp:Parameter Name="beizhu3" Type="String" />
                                <asp:Parameter Name="beizhu4" Type="String" />
                                <asp:Parameter Name="id" Type="Int32" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="xiangmuid" Type="String" />
                                <asp:Parameter Name="kehuname" Type="String" />
                                <asp:Parameter Name="renwuname" Type="String" />
                                <asp:Parameter Name="gongchengshi" Type="String" />
                                <asp:Parameter Name="kaishiriqi" Type="String" />
                                <asp:Parameter Name="jihuajieshu" Type="String" />
                                <asp:Parameter Name="shijijieshu" Type="String" />
                                <asp:Parameter Name="zhuangtai" Type="String" />
                                <asp:Parameter Name="beizhu" Type="String" />
                                <asp:Parameter Name="tianxieren" Type="String" />
                                <asp:Parameter Name="tianxietime" Type="DateTime" />
                                <asp:Parameter Name="beizhu1" Type="String" />
                                <asp:Parameter Name="beizhu2" Type="String" />
                                <asp:Parameter Name="beizhu3" Type="String" />
                                <asp:Parameter Name="beizhu4" Type="String" />
                            </InsertParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
