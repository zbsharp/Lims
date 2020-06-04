<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskIn1.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Case_TaskIn1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务通知书(任务查看)</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>

    <script type="text/javascript" src="../js/calendar.js"></script>
    <style type="text/css">
        .yinc {
            display: none;
        }

        .tijiaohao {
            display: none;
        }

        .auto-style1 {
            width: 123px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="div_All">
            <div class="Body_Title">
                业务管理 》》任务查看
            </div>
            <fieldset>
                <legend style="color: Red">任务信息</legend>
                <table align="center" class="Admin_Table" width="100%">





                    <tr>
                        <td>任务编号：
                        </td>
                        <td>
                            <asp:TextBox ID="rwbianhao" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="auto-style1">当前状态：
                        </td>
                        <td>

                            <asp:DropDownList ID="rwstate" runat="server" Width="80px" Enabled="false">
                                <asp:ListItem>受理</asp:ListItem>
                                <asp:ListItem>下达</asp:ListItem>
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
                            <asp:TextBox ID="rwshenqingbianhao" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style1">需要报告：
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
                            <asp:TextBox ID="rwxiadariqi" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="auto-style1">客户等级：</td>
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
                            <asp:TextBox ID="rwwancheng" runat="server" onclick="new Calendar().show(this.form.rwwancheng);"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ErrorMessage="请输入要求完成日期" ControlToValidate="rwwancheng"></asp:RequiredFieldValidator>
                            <asp:Button ID="Button9" runat="server" OnClick="Button9_Click" Text="修改要求完成日期" />
                        </td>
                        <td class="auto-style1">备注：</td>
                        <td>

                            <asp:TextBox ID="rwbeizhu" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>客户时限(天)：</td>
                        <td>

                            <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style1">&nbsp;周期：</td>
                        <td>
                            <asp:TextBox ID="txt_zhouqi" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>




                    <tr>
                        <td>考核时限(天)：</td>
                        <td>
                            <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style1">财务备注信息:</td>
                        <td>
                            <asp:TextBox ID="txt_financebeizhu" runat="server" Width="305px" ReadOnly="true"></asp:TextBox></td>
                    </tr>


                    <%--   <tr>
                <td>
                    检验类别：</td>
                <td colspan="3">
                    
                    <asp:CheckBoxList ID="CheckBoxList2" runat="server" 
                RepeatDirection="Horizontal" Width="100%"  RepeatLayout="Flow"></asp:CheckBoxList></td>
            </tr>--%>


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

                            <asp:TextBox ID="TextBox1" Enabled="false" Visible="false" runat="server"></asp:TextBox>

                            <asp:DropDownList ID="DropDownList5" Visible="false" runat="server">
                                <asp:ListItem>否</asp:ListItem>
                                <asp:ListItem>是</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btn_copy" runat="server" Text="复制" Width="83px" OnClick="btn_copy_Click" /></td>
                    </tr>

                    <tr>
                        <td colspan="4" align="center">
                            <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False"
                                OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound">
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
                                    <asp:BoundField DataField="shuliang" HeaderText="数量" ReadOnly="true" />
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
                                    <asp:CommandField HeaderText="操作" ShowDeleteButton="True" ShowEditButton="True" CausesValidation="False" />
                                    <asp:BoundField DataField="tijiaohaoma" HeaderText="tijiaohaoma" HeaderStyle-CssClass="tijiaohao" ItemStyle-CssClass="tijiaohao">
                                        <HeaderStyle CssClass="tijiaohao"></HeaderStyle>

                                        <ItemStyle CssClass="tijiaohao"></ItemStyle>
                                    </asp:BoundField>
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
                <legend style="color: Red">样品信息</legend>
                <table align="center" class="Admin_Table" width="100%" style="display: none">

                    <tr>
                        <td>样品名称：
                        </td>
                        <td>
                            <asp:TextBox ID="ypbianhao" runat="server"></asp:TextBox>
                        </td>
                        <td>样品英文名称：
                        </td>
                        <td>
                            <asp:TextBox ID="ypbianhaoy" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>型号：
                        </td>
                        <td>
                            <asp:TextBox ID="ypxinghao" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                        <td>型号英文：
                        </td>
                        <td>
                            <asp:TextBox ID="ypxinghaoy" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table align="center" class="Admin_Table" width="100%">
                    <tr>
                        <td>产品名称：
                        </td>
                        <td>
                            <asp:TextBox ID="cp" runat="server"></asp:TextBox>
                        </td>
                        <td>生产日期：</td>
                        <td>
                            <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
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
                        <td>产品类型：</td>
                        <td>
                            <asp:DropDownList ID="DropDownList6" runat="server">
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
                    <tr>
                        <td>其他要求：</td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr style="display: none;">
                        <td>预计完成时间：</td>
                        <td>
                            <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr style="display: none;">
                        <td>是否外包：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox9" Visible="false" runat="server" onclick="popUpCalendar(this,document.forms[0].ypsongjianriqi,'yyyy-mm-dd')"
                                Width="137px"></asp:TextBox><asp:DropDownList ID="DropDownList3"
                                    runat="server" Width="240">
                                    <asp:ListItem>是</asp:ListItem>
                                    <asp:ListItem>否</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                        <td>提供资料：
                        </td>
                        <td>
                            <asp:TextBox ID="ypshengchanriqi" runat="server"></asp:TextBox>
                        </td>
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
                        <td style="text-align: left; width: 110px;">&nbsp;付款单位：
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="TextBox4" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 90px;">制造单位：</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="TextBox6" runat="server" Width="300"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 110px;">客户名称：</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="kh" runat="server" Enabled="false" Width="300"></asp:TextBox>
                            <asp:TextBox ID="TextBox3" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 90px;">生产单位：
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="TextBox7" runat="server" Width="300"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 110px;">报价编号：</td>
                        <td style="text-align: left">

                            <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox><asp:Label ID="Label1" runat="server"
                                Text=""></asp:Label>
                            <asp:Button ID="Button7" runat="server" Text="查看报价单" OnClick="Button7_Click" />

                        </td>
                    </tr>
                </table>
            </fieldset>



            <fieldset>
                <legend style="color: Red">联系信息</legend>
                <table align="center" class="Admin_Table" width="100%">
                    <tr>
                        <td>

                            <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="Admin_Table">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                                CommandArgument='<%# Eval("customerid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Green" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="customerid" HeaderText="客户编号" ReadOnly="True" />
                                    <asp:BoundField DataField="name" HeaderText="姓名" />
                                    <asp:BoundField DataField="department" HeaderText="部门" />
                                    <asp:BoundField DataField="rode" HeaderText="角色" />
                                    <asp:BoundField DataField="telephone" HeaderText="电话" />
                                    <asp:BoundField DataField="mobile" HeaderText="手机" />
                                    <asp:BoundField DataField="email" HeaderText="邮箱" />
                                    <asp:BoundField DataField="fax" HeaderText="传真" />
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                    <asp:BoundField DataField="lururen" HeaderText="业务员" />

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
                <legend style="color: Red">备注信息</legend>
                <table align="center" class="Admin_Table" width="100%">
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TextBox15" runat="server" Width="88%"></asp:TextBox>

                            <asp:Button
                                ID="Button8" runat="server" Width="10%" Text="保存备注"
                                OnClick="Button1_Click1" />
                        </td>
                    </tr>



                    <tr>
                        <td>
                            <asp:GridView ID="GridView3" Width="100%" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                                CssClass="Admin_Table">
                                <Columns>
                                    <asp:BoundField DataField="xiangmuid" HeaderText="项目编号" />
                                    <asp:BoundField DataField="neirong" HeaderText="内容" />
                                    <asp:BoundField DataField="name" HeaderText="填写人" />
                                    <asp:BoundField DataField="time" HeaderText="填写时间" />
                                </Columns>
                                <HeaderStyle CssClass="Admin_Table_Title " />
                            </asp:GridView>

                        </td>
                    </tr>


                </table>
            </fieldset>

            <fieldset>
                <legend style="color: Red">
                    <asp:DropDownList ID="DropDownList7" Visible="false"
                        runat="server"
                        OnSelectedIndexChanged="DropDownList7_SelectedIndexChanged">
                    </asp:DropDownList>
                    邮寄联系人信息：                   
                    <asp:TextBox ID="rwkehu" runat="server" Width="640px"></asp:TextBox>




                </legend>




                <table class="Admin_Table">
                    <tr>
                        <td colspan="7" align="center">
                            <asp:GridView ID="GridView5" Visible="false" runat="server" Width="100%" AutoGenerateColumns="false"
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

            <div style="text-align: center">
                归属销售人员：<asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                <asp:Button ID="Button10" runat="server" OnClick="Button10_Click"
                    Text="修改归属" Visible="False" />
                <asp:Button ID="Button6" runat="server" Text="结束任务" OnClick="Button6_Click" Enabled="false" Visible="False" />
                <asp:Button ID="Button2" runat="server" Text="修改保存" OnClick="Button2_Click" Visible="False" />
                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="受理" Visible="false" />
                <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="下达到科室" Visible="false" />
                <asp:Button ID="Button5" runat="server" Text="打印任务标签" OnClick="Button5_Click" Visible="False" />
                <asp:TextBox ID="ypsongjianriqi" Visible="false" runat="server" onclick="popUpCalendar(this,document.forms[0].ypsongjianriqi,'yyyy-mm-dd')"
                    Width="138px"></asp:TextBox>
            </div>



            <asp:Literal ID="ld" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>


