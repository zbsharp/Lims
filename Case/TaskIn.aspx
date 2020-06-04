<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskIn.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="Case_TaskIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务通知书(业务受理)</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <script type="text/javascript" src="../js/calendar.js"></script>

    <style type="text/css">
        .yinc {
            display: none;
        }

        body {
            width: 100%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="div_All">
            <div class="Body_Title">
                业务管理 》》录入任务书(业务受理)
            </div>
            <fieldset>
                <legend style="color: Red">下达任务</legend>
                <table align="center" class="Admin_Table" width="100%">
                    <tr>
                        <td>任务编号：
                        </td>
                        <td>
                            <asp:TextBox ID="rwbianhao" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>当前状态：
                        </td>
                        <td>

                            <asp:DropDownList ID="rwstate" runat="server" Width="80px" Enabled="false">
                                <asp:ListItem>进行中</asp:ListItem>

                                <asp:ListItem>暂停</asp:ListItem>
                                <asp:ListItem>完成</asp:ListItem>
                                <asp:ListItem>中止</asp:ListItem>
                                <asp:ListItem>关闭</asp:ListItem>
                            </asp:DropDownList>


                        </td>
                    </tr>
                    <tr>
                        <td>申请编号：
                        </td>
                        <td>
                            <asp:TextBox ID="rwshenqingbianhao" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>需要报告：
                        
                        </td>
                        <td>

                            <asp:DropDownList ID="rwbaogao" runat="server" Width="80px">
                                <asp:ListItem>是</asp:ListItem>
                                <asp:ListItem>否</asp:ListItem>
                                <asp:ListItem>待确定</asp:ListItem>
                            </asp:DropDownList>


                        </td>
                    </tr>
                    <tr>
                        <td>任务受理日期：
                        </td>
                        <td>
                            <asp:TextBox ID="rwxiadariqi" runat="server" onclick="new Calendar().show(this.form.rwxiadariqi);"></asp:TextBox>
                        </td>
                        <td>客户等级：</td>
                        <td>

                            <asp:DropDownList ID="rwyouxian" runat="server" Width="80px">
                                <asp:ListItem>VIP客户</asp:ListItem>
                                <asp:ListItem Selected="True">普通客户</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>要求完成日期：
                        </td>
                        <td>
                            <asp:TextBox ID="rwwancheng" runat="server" onclick="new Calendar().show(this.form.rwwancheng);"></asp:TextBox>
                        </td>
                        <td>备注：</td>
                        <td>

                            <asp:TextBox ID="rwbeizhu" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>客户时限(天)：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox11" runat="server" Text="10"></asp:TextBox>

                        </td>
                        <td>&nbsp;周期：</td>
                        <td>
                            <asp:TextBox ID="txt_zhouqi" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
                        <td>考核时限(天)：</td>
                        <td>
                            <asp:TextBox ID="TextBox10" runat="server" Text="10"></asp:TextBox>
                        </td>
                        <td>财务备注信息:</td>
                        <td>
                            <asp:TextBox ID="txt_financebeizhu" runat="server" Width="305px" ReadOnly="true"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td>任务承接：
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="DropDownList2" Visible="false" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList1" Visible="false" runat="server">
                            </asp:DropDownList>

                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                            </asp:CheckBoxList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ErrorMessage="*" ControlToValidate="DropDownList1" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Button ID="btn_copy" runat="server" Text="复制" OnClick="btn_copy_Click" Width="83px" /></td>
                    </tr>

                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False"
                                DataKeyNames="id" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            是否受理
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            是否复制
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_copy" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="序号" ItemStyle-CssClass="yinc" HeaderStyle-CssClass="yinc">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                                CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                        </ItemTemplate>

                                        <HeaderStyle CssClass="yinc"></HeaderStyle>

                                        <ItemStyle ForeColor="Green" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ceshiname" HeaderText="测试项目" />
                                    <asp:BoundField DataField="biaozhun" HeaderText="标准" />
                                    <asp:BoundField DataField="yp" HeaderText="样品" />
                                    <asp:BoundField DataField="zhouqi" HeaderText="周期" />
                                    <asp:BoundField DataField="feiyong" HeaderText="费用" />
                                    <asp:BoundField DataField="shuliang" HeaderText="数量"/>
                                    <asp:BoundField DataField="total" HeaderText="合计" DataFormatString="{0:N2}" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="是否外包">
                                        <EditItemTemplate>
                                            <asp:DropDownList runat="server" ID="epiboly">
                                                <asp:ListItem Text="是" Value="外包"></asp:ListItem>
                                                <asp:ListItem Text="否" Value="非外包" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Bind("epiboly") %>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("epiboly") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="epiboly_Price" HeaderText="外包金额" />
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                    <asp:TemplateField HeaderText="部门">
                                        <EditItemTemplate>
                                            <asp:DropDownList runat="server" ID="funtion">
                                                <asp:ListItem Text="电池部" Value="电池部"></asp:ListItem>
                                                <asp:ListItem Text="化学部" Value="化学部"></asp:ListItem>
                                                <asp:ListItem Text="EMC部" Value="EMC部"></asp:ListItem>
                                                <asp:ListItem Text="安规部" Value="安规部"></asp:ListItem>
                                                <asp:ListItem Text="龙华EMC部" Value="龙华EMC部"></asp:ListItem>
                                                <asp:ListItem Text="龙华安规部" Value="龙华安规部"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("bumen") %>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("bumen") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField HeaderText="操作" ShowEditButton="True" CausesValidation="False" ShowDeleteButton="True" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="Admin_Table_Title " />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend style="color: Red">服务要求</legend>
                <table align="center" class="Admin_Table" width="100%" style="display: none">

                    <tr>
                        <td>样品名称：
                        </td>
                        <td>
                            <asp:TextBox ID="ypbianhao" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>样品英文名称：
                        </td>
                        <td>
                            <asp:TextBox ID="ypbianhaoy" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>型号：
                        </td>
                        <td>
                            <asp:TextBox ID="ypxinghao" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </td>
                        <td>型号英文：
                        </td>
                        <td>
                            <asp:TextBox ID="ypxinghaoy" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table align="center" class="Admin_Table" width="100%">

                    <tr>
                        <td class="auto-style1">产品名称：
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="cp" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style1">其他要求：</td>
                        <td class="auto-style1">
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                    </tr>



                    <tr>
                        <td>型号规格：</td>
                        <td>
                            <asp:TextBox ID="guige" runat="server"></asp:TextBox>
                        </td>
                        <td>备注：</td>
                        <td>
                            <asp:TextBox ID="ypbeizhu" runat="server"></asp:TextBox>
                        </td>
                    </tr>



                    <tr>
                        <td>产品商标：
                        </td>
                        <td>
                            <asp:TextBox ID="ypshanbiao" runat="server"></asp:TextBox>
                        </td>
                        <td>产品类型:</td>
                        <td>
                            <asp:DropDownList ID="DropDownList5" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>元器件或材料</asp:ListItem>
                                <asp:ListItem>部件</asp:ListItem>
                                <asp:ListItem>家用电器</asp:ListItem>
                                <asp:ListItem>商用机械</asp:ListItem>
                                <asp:ListItem>实验用测控仪器设备</asp:ListItem>
                                <asp:ListItem>AV</asp:ListItem>
                                <asp:ListItem>IT</asp:ListItem>
                                <asp:ListItem>照明电器</asp:ListItem>
                                <asp:ListItem>电信终端</asp:ListItem>
                                <asp:ListItem>逆变器</asp:ListItem>
                                <asp:ListItem>光伏组件</asp:ListItem>
                                <asp:ListItem>玩具</asp:ListItem>
                                <asp:ListItem>电玩具</asp:ListItem>
                                <asp:ListItem>汇流箱</asp:ListItem>
                                <asp:ListItem>无线产品</asp:ListItem>
                                <asp:ListItem>医疗器械</asp:ListItem>
                                <asp:ListItem>汽车电子</asp:ListItem>
                                <asp:ListItem>节能</asp:ListItem>
                                <asp:ListItem>信息设备</asp:ListItem>
                                <asp:ListItem>音视频设备</asp:ListItem>
                                <asp:ListItem>灯具</asp:ListItem>
                                <asp:ListItem>ROHS</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>



                    <tr>
                        <td>样品数量：</td>
                        <td>
                            <asp:TextBox ID="ypshuliang" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>
                        </td>
                        <td>取样方式：</td>
                        <td>
                            <asp:TextBox ID="ypchouyangfangshi" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>主测型号：</td>
                        <td>
                            <asp:TextBox ID="txt_zhuce" runat="server"></asp:TextBox></td>
                        <td>附加型号：</td>
                        <td>
                            <asp:TextBox ID="txt_fujia" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr style="display: none;">
                        <td>是否外包：
                        </td>
                        <td>
                            <asp:TextBox ID="ypsongjianriqi" Visible="false" runat="server" onclick="popUpCalendar(this,document.forms[0].ypsongjianriqi,'yyyy-mm-dd')"
                                Width="137px"></asp:TextBox><asp:DropDownList ID="DropDownList3"
                                    runat="server" Width="140px">
                                    <asp:ListItem>是</asp:ListItem>
                                    <asp:ListItem>否</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                        <td>取样方式：
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr style="display: none;">
                        <td>生产日期：</td>
                        <td>&nbsp;</td>
                        <td>预计费用：</td>
                        <td>
                            <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr style="display: none;">
                        <td>提供资料：
                        </td>
                        <td>
                            <asp:TextBox ID="ypshengchanriqi" runat="server"></asp:TextBox>
                        </td>
                        <td>备注;
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
                        </td>
                    </tr>




                    <tr style="display: none;">
                        <td>其他要求：</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>




                </table>
            </fieldset>
            <fieldset>
                <legend style="color: Red">客户信息</legend>
                <table align="center" class="Admin_Table" width="100%">
                    <tr>
                        <td style="text-align: left; width: 90px;">委托单位：
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="TextBox5" runat="server" Width="300"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 110px;">客户名称：</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="kh" runat="server" Enabled="false" Width="300"></asp:TextBox>
                            <asp:TextBox ID="TextBox3" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 90px;">制造单位：
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="TextBox6" runat="server" Width="300"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 110px;">付款单位：</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="TextBox4" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 90px;">生产单位：
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="TextBox7" runat="server" Width="300"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 110px;">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend style="color: Red">
                    <asp:DropDownList ID="DropDownList6" Visible="false"
                        runat="server"
                        OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged">
                    </asp:DropDownList>
                    邮寄联系人信息：<asp:TextBox ID="rwkehu"
                        runat="server" Width="568px"></asp:TextBox>



                </legend>




                <table class="Admin_Table">
                    <tr>
                        <td colspan="7" align="center">
                            <asp:GridView ID="GridView5" runat="server" Width="100%" AutoGenerateColumns="false"
                                DataKeyNames="id"
                                CssClass="Admin_Table">
                                <Columns>
                                    <asp:BoundField DataField="name" HeaderText="姓名" />
                                    <asp:BoundField DataField="department" HeaderText="部门" />
                                    <asp:BoundField DataField="rode" HeaderText="角色" />
                                    <asp:BoundField DataField="telephone" HeaderText="电话" />
                                    <asp:BoundField DataField="mobile" HeaderText="手机" />
                                    <asp:BoundField DataField="email" HeaderText="邮箱" />
                                    <asp:BoundField DataField="fax" HeaderText="传真" />
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="Admin_Table_Title " />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <asp:Literal ID="ld" runat="server"></asp:Literal>
        </div>

        <div style="text-align: center;">
            <asp:Button ID="Button2" runat="server" Text="受理仅保存" OnClick="Button2_Click" Visible="False" />
            <asp:Button ID="Button3" runat="server" Text="受理并下达"
                OnClick="Button3_Click" />
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
