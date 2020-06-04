<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerSee.aspx.cs" Inherits="Customer_CustomerSee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户编辑</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/popcalendar.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>



    <style type="text/css">
        .BnCss {
            height: 21px;
        }

        .auto-style1 {
            height: 28px;
        }
    </style>



</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="Body_Title">
            <asp:Label ID="Label1" runat="server"></asp:Label>
            》》<asp:Label ID="Label2" runat="server"></asp:Label>
        </div>
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Height="500"
            Width="100%">
            <cc1:TabPanel runat="server" HeaderText="标题" ID="TabPanel1">
                <HeaderTemplate>
                    基本信息
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table class="Admin_Table">
                                <tr>
                                    <td>客户名称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="kehuname" runat="server" CssClass="txtHInput"></asp:TextBox><span style="font-size: 13pt; vertical-align: middle; color: red">*</span>
                                    </td>

                                </tr>

                                <tr>
                                    <td>中文地址：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="txtHInput"></asp:TextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td>网址：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="txtHInput"></asp:TextBox>
                                    </td>

                                </tr>

                                <tr>
                                    <td>产品类别：
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem>IT</asp:ListItem>
                                            <asp:ListItem>AV</asp:ListItem>
                                            <asp:ListItem>灯具</asp:ListItem>
                                            <asp:ListItem>家电</asp:ListItem>
                                            <asp:ListItem>电动工具</asp:ListItem>
                                            <asp:ListItem>汽车电子</asp:ListItem>
                                            <asp:ListItem>电池</asp:ListItem>
                                            <asp:ListItem>玩具</asp:ListItem>
                                            <asp:ListItem>其他</asp:ListItem>
                                        </asp:CheckBoxList>

                                    </td>

                                </tr>
                                <tr>
                                    <td class="auto-style1">客户类型：
                                    </td>
                                    <td class="auto-style1">
                                        <asp:DropDownList ID="DropDownList5" runat="server" Width="80">

                                            <asp:ListItem>代理机构</asp:ListItem>
                                            <asp:ListItem>认证机构</asp:ListItem>
                                            <asp:ListItem>政府机构</asp:ListItem>
                                            <asp:ListItem>国外机构</asp:ListItem>
                                            <asp:ListItem>企业客户</asp:ListItem>

                                            <asp:ListItem Value="市场客户">市场客户</asp:ListItem>
                                            <asp:ListItem Value="3C客户">3C客户</asp:ListItem>
                                            <asp:ListItem Value="CQC客户">CQC客户</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td>客户行业：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList2" runat="server" Width="80">
                                            <asp:ListItem Value="ODM">ODM</asp:ListItem>
                                            <asp:ListItem Value="OEM">OEM</asp:ListItem>
                                            <asp:ListItem Value="贸易商">贸易商</asp:ListItem>
                                            <asp:ListItem Value="认证检测">认证检测</asp:ListItem>
                                            <asp:ListItem Value="其它">其它</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td nowrap="nowrap">来源途径：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList3" runat="server" Width="80">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="公司内部">公司内部</asp:ListItem>
                                            <asp:ListItem Value="自行开发">自行开发</asp:ListItem>
                                            <asp:ListItem Value="客户推荐">客户推荐</asp:ListItem>
                                            <asp:ListItem Value="展会">展会</asp:ListItem>
                                            <asp:ListItem Value="培训研讨会">培训研讨会</asp:ListItem>
                                            <asp:ListItem Value="网络推广">网络推广</asp:ListItem>
                                            <asp:ListItem Value="平面媒体">平面媒体</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td>信用情况：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList1" runat="server" Width="80">
                                            <asp:ListItem Value="正常">正常</asp:ListItem>
                                            <asp:ListItem Value="月结">月结</asp:ListItem>
                                            <asp:ListItem Value="欠款">欠款</asp:ListItem>
                                            <asp:ListItem Value="黑名单">黑名单</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td>客户等级：</td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList8" runat="server" Width="140px" Height="18px">
                                            <asp:ListItem Value="普通客户">普通客户</asp:ListItem>
                                            <asp:ListItem Value="VIP客户">VIP客户</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Button ID="Button1" runat="server" Text="修改等级" OnClick="Button1_Click1" Visible="false"/>
                                        <asp:Label ID="Label5" runat="server"></asp:Label>

                                    </td>

                                </tr>
                                <tr>
                                    <td>备注：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Intro" runat="server" ToolTip="备注" Width="403px"></asp:TextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="4" align="center"></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div style="text-align: center">
                        <asp:Button ID="Button6" runat="server" CssClass="BnCss" Text="修改保存" OnClick="Button6_Click" />
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="图片" ID="TabPanel2">
                <HeaderTemplate>
                    联系人员
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
  
                        <ContentTemplate>
                            <table class="Admin_Table">
                                <tr bgcolor="#f4faff">
                                    <td align="left" style="width: 84px">客户编号:
                                    </td>
                                    <td align="left" style="width: 272px">
                                        <asp:TextBox ID="TextBox2" runat="server" ReadOnly="True" Width="120px"></asp:TextBox>
                                    </td>
                                    <td align="left">QQ：</td>
                                    <td align="left" style="width: 223px">
                                        <asp:TextBox ID="txtQQ" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr bgcolor="#f4faff">
                                    <td align="left" style="width: 84px">客户联系人:
                                    </td>
                                    <td align="left" style="width: 272px">
                                        <asp:TextBox ID="linkman" runat="server" Width="120px"></asp:TextBox>
                                        <span style="color: Red">*
                                        </span>
                                    </td>
                                    <td align="left">职务：
                                    </td>
                                    <td align="left" style="width: 223px">
                                        <asp:DropDownList ID="DropDownList12" runat="server" Width="122px">
                                            <asp:ListItem>普通职工</asp:ListItem>
                                            <asp:ListItem>经理</asp:ListItem>
                                            <asp:ListItem>主管</asp:ListItem>
                                            <asp:ListItem>业务员</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr bgcolor="#f4faff">
                                    <td align="left" style="width: 84px">所在部门:
                                    </td>
                                    <td align="left" style="width: 272px">
                                        <asp:TextBox ID="TextBox6" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                    <td align="left">微信： <%--                                    传真：--%>
                                    </td>
                                    <td align="left" style="width: 223px">
                                        <asp:TextBox ID="txtWeixin" runat="server" Width="120px"></asp:TextBox>
                                        <%--                                    <asp:TextBox ID="fax" runat="server" Width="120px"></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr bgcolor="#f4faff">
                                    <td align="left" class="style3" style="width: 84px; height: 30px;">电话：
                                    </td>
                                    <td align="left" class="style1" style="width: 272px; height: 30px;">
                                        <asp:TextBox ID="telephone" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                    <td align="left" class="style2" style="height: 30px">手机：
                                    </td>
                                    <td align="left" style="width: 223px; height: 30px;">
                                        <asp:TextBox ID="mobile" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr bgcolor="#f4faff">
                                    <td align="left" class="style3" style="width: 84px">Email：
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:TextBox ID="email" runat="server" Width="503px"></asp:TextBox>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr bgcolor="#f4faff">
                                    <td align="left" class="style3" style="width: 84px">备注：
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:TextBox ID="TextBox7" runat="server" Width="503px" TextMode="MultiLine"></asp:TextBox>
                                        <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="电话与手机信息必须填其一！" Width="247px"
                                            Visible="False"></asp:Label>

                                    </td>
                                </tr>



                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <div style="text-align: center">
                        <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                            Text="保存联系人" CausesValidation="False" />
                    </div>



                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="Admin_Table" OnRowDeleting="GridView1_RowDeleting"
                                OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="id" OnRowEditing="GridView1_RowEditing"
                                OnRowUpdating="GridView1_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                                CommandArgument='<%# Eval("customerid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Green" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="customerid" HeaderText="客户编号" ReadOnly="True" />
                                    <asp:BoundField DataField="name" HeaderText="姓名" ReadOnly="true" />
                                    <asp:BoundField DataField="department" HeaderText="部门" />
                                    <asp:BoundField DataField="rode" HeaderText="角色" />
                                    <asp:BoundField DataField="telephone" HeaderText="电话" />
                                    <asp:BoundField DataField="mobile" HeaderText="手机" />
                                    <asp:BoundField DataField="email" HeaderText="邮箱" />
                                    <%--                                <asp:BoundField DataField="fax" HeaderText="传真" />--%>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                    <asp:BoundField DataField="QQ" HeaderText="QQ" />
                                    <asp:BoundField DataField="Weixin" HeaderText="微信" />
                                    <asp:CommandField HeaderText="取消" ShowDeleteButton="True" ShowEditButton="true" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="Admin_Table_Title " />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="电话" ID="TabPanel3">
                <HeaderTemplate>
                    查看联系日志
                </HeaderTemplate>
                <ContentTemplate>





                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="false">

                                <Columns>
                                    <asp:BoundField DataField="neirong" HeaderText="内容">
                                        <ItemStyle Width="30%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="style" HeaderText="方式">
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="result" HeaderText="结果">
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="filltime" HeaderText="填写时间" DataFormatString="{0:D}">
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="responser" HeaderText="填写人">
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="zhongyao" HeaderText="联系人" />

                                    <asp:BoundField DataField="xiacitime" HeaderText="下次跟踪日期" DataFormatString="{0:d}" />
                                </Columns>
                                <HeaderStyle CssClass="Admin_Table_Title " />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>

                    </asp:UpdatePanel>

                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="语言" ID="TabPanel4">
                <HeaderTemplate>
                    报价信息
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:GridView ID="GridView3" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                        CommandArgument='<%# Eval("baojiaid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:HyperLinkField HeaderText="报价单" Text="查看" DataNavigateUrlFormatString="~/Print/QuoPrint.aspx?baojiaid={0}&&customerid={1}"
                                DataNavigateUrlFields="baojiaid,kehuid" Target="_blank" />

                            <asp:BoundField DataField="kehuid" HeaderText="客户编号" />
                            <asp:BoundField DataField="discount" HeaderText="折扣" />
                            <asp:BoundField DataField="zhehoujia" HeaderText="折后价格" />
                            <asp:BoundField HeaderText="填写人" DataField="fillname" ReadOnly="True" />
                            <asp:BoundField HeaderText="审批状态" DataField="shenpibiaozhi" ReadOnly="True" />



                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="语言" ID="TabPanel5" Visible="false">
                <HeaderTemplate>
                    客户申请
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:GridView ID="GridView4" AutoGenerateColumns="False" runat="server" Width="100%" CssClass="Admin_Table">
                        <Columns>
                            <asp:BoundField DataField="kehuname" HeaderText="客户名称" />
                            <asp:BoundField DataField="beizhu" HeaderText="申请理由" />
                            <asp:BoundField DataField="biaoji" HeaderText="处理标记" />
                            <asp:BoundField DataField="fillname" HeaderText="填写人" />

                            <asp:BoundField DataField="filltime" HeaderText="录入时间" DataFormatString="{0:d}" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>
