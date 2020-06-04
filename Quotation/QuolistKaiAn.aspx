<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuolistKaiAn.aspx.cs" ValidateRequest="false" MaintainScrollPositionOnPostback="true"
    Inherits="Quotation_QuolistKaiAn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>填单</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <script type="text/javascript" src="../js/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_All">
            <div class="Body_Title">
                <asp:Label ID="Label1" runat="server" Text="业务管理"></asp:Label>
                》》<asp:Label ID="Label2" runat="server" Text="填单"></asp:Label>
            </div>
            <fieldset>
                <legend style="color: Red">客户信息</legend>
                <table class="Admin_Table" style="width: 100%">
                    <tr>
                        <td>实际客户：</td>
                        <td>
                            <asp:TextBox ID="kh" runat="server" Width="240" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>委托单位：
                        </td>
                        <td>
                            <asp:TextBox ID="wt" runat="server" Width="240"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" Text="必填" ForeColor="Red"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="wt" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>委托单位英文名:</td>
                        <td>
                            <asp:TextBox ID="txt_ENweituo" runat="server" Width="240"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>委托单位地址:</td>
                        <td>
                            <asp:TextBox ID="txt_addressWei" runat="server" Width="240"></asp:TextBox></td>
                        <td>委托单位英文地址:
                        </td>
                        <td>
                            <asp:TextBox ID="txt_ENaddressWei" runat="server" Width="240"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td>制造单位：</td>
                        <td>
                            <asp:TextBox ID="zz" runat="server" Width="240"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" Text="必填" ForeColor="Red"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                ControlToValidate="zz" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>制造单位英文名：</td>
                        <td>
                            <asp:TextBox ID="txt_ENzhizao" runat="server" Width="240"></asp:TextBox>
                            <%-- 付款单位文本框 --%>
                            <asp:TextBox ID="fk" runat="server" Width="240" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>制造单位地址：</td>
                        <td>
                            <asp:TextBox ID="txt_addresZhizao" runat="server" Width="240"></asp:TextBox></td>
                        <td>制造单位英文地址:</td>
                        <td>
                            <asp:TextBox ID="txt_ENaddresZhizao" runat="server" Width="240"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>生产单位：
                        </td>
                        <td>
                            <asp:TextBox ID="sc" runat="server" Width="240"></asp:TextBox>
                            <%--                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                ControlToValidate="sc" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td>生产单位英文名：</td>
                        <td>
                            <asp:TextBox ID="txt_ENshengchang" runat="server" Width="240"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>生产单位地址：</td>
                        <td>
                            <asp:TextBox ID="txt_addersshengchang" runat="server" Width="240"></asp:TextBox></td>
                        <td>生产单位英文地址：</td>
                        <td>
                            <asp:TextBox ID="txt_ENaddressshengchang" runat="server" Width="240"></asp:TextBox></td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend style="color: Red">联系人</legend>
                <table class="Admin_Table" style="width: 100%">
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;联系人：
                            <asp:TextBox ID="txt_linaxiren" runat="server" ReadOnly="true"></asp:TextBox></td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend style="color: Red">产品信息</legend>
                <table class="Admin_Table" style="width: 100%">
                    <tr>
                        <td>产品选择:</td>
                        <td>
                            <asp:DropDownList ID="drop_CP" runat="server" Width="230px" AutoPostBack="True" OnSelectedIndexChanged="drop_CP_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>产品名称：
                        </td>
                        <td>
                            <asp:TextBox ID="cp" runat="server" Width="240" OnTextChanged="cp_TextChanged"></asp:TextBox>
                            <asp:Label ID="Label6" runat="server" Text="必填" ForeColor="Red"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ControlToValidate="cp" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>产品英文名称：</td>
                        <td>
                            <asp:TextBox ID="txt_ENcpname" runat="server" Width="240px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>型号规格：</td>
                        <td>
                            <asp:TextBox ID="guige" runat="server" Width="240"></asp:TextBox>
                            <%--                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                ControlToValidate="guige" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td>产品类型：</td>
                        <td>
                            <asp:DropDownList ID="DropDownList3" runat="server" Width="240px">
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
                        <td>产品商标：</td>
                        <td>
                            <asp:TextBox ID="sb" runat="server" Width="240"></asp:TextBox>
                        </td>
                        <td>主测型号：</td>
                        <td>
                            <asp:TextBox ID="txt_cpzhuce" runat="server" Width="240px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>样品数量：
                        </td>
                        <td>
                            <asp:TextBox ID="yp" runat="server" Width="240" Text="0" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>
                            <asp:Label ID="Label7" runat="server" Text="必填" ForeColor="Red"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                ControlToValidate="yp" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>附加型号：</td>
                        <td>
                            <asp:TextBox ID="txt_cpfujia" runat="server" Width="240px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>送样方式：</td>
                        <td>
                            <asp:DropDownList ID="quyang" runat="server"
                                Width="230">
                                <asp:ListItem>委托单位送样</asp:ListItem>
                                <asp:ListItem>制造厂商送样</asp:ListItem>
                                <asp:ListItem>自备样品</asp:ListItem>
                                <asp:ListItem>抽样</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>产品描述：</td>
                        <td>
                            <asp:TextBox ID="txt_cpmiaoshu" runat="server" Width="240px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>提供资料：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox7" runat="server" Width="240"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>


                </table>

                <table class="Admin_Table" style="display: none">
                    <tr>
                        <td>样品名称：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" Width="240"></asp:TextBox>
                        </td>
                        <td>英文名称：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" Width="240"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>样品型号：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server" Width="240"></asp:TextBox>
                        </td>
                        <td>英文型号：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server" Width="240"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>送样日期：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="240" onclick="popUpCalendar(this,document.forms[0].TextBox1,'yyyy-mm-dd')"></asp:TextBox>
                        </td>
                        <td>取样方式：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" Width="240"></asp:TextBox>
                        </td>
                    </tr>



                </table>

            </fieldset>

            <fieldset>
                <legend style="color: Red">请选择相关项目</legend>

                <table class="Admin_Table" style="display: none;">
                    <tr>
                        <%--<td>检测类别： 
                        </td>
                        <td align="left">
                            <asp:CheckBoxList ID="CheckBoxList1" runat="server"
                                RepeatDirection="Horizontal" Width="100%" RepeatColumns="5" RepeatLayout="Flow" Visible="False">
                            </asp:CheckBoxList>
                        </td>--%>
                        <td>申请编号： 
                        </td>
                        <td>
                            <asp:TextBox ID="sqbianhao" runat="server" Width="157px"></asp:TextBox><span style="color: Red">(发证机构申请号)</span>
                        </td>
                    </tr>
                </table>
                <asp:CheckBox ID="chk_all" runat="server" Text="全选" AutoPostBack="True" OnCheckedChanged="chk_all_CheckedChanged" />
                <table class="Admin_Table" style="width: 100%">
                    <tr>
                        <td align="center" colspan="4">
                            <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="id" CssClass="Admin_Table">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            请选择
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                                CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Green" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ceshiname" HeaderText="测试项目" />
                                    <asp:BoundField DataField="biaozhun" HeaderText="标准" />
                                    <asp:BoundField DataField="neirong" HeaderText="内容" Visible="false" />
                                    <asp:BoundField DataField="yp" HeaderText="样品" />
                                    <asp:BoundField DataField="zhouqi" HeaderText="周期" />
                                    <asp:BoundField DataField="shuliang" HeaderText="数量" />
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                    <asp:BoundField DataField="cpname" HeaderText="产品名称" />
                                    <asp:BoundField DataField="cptype" HeaderText="产品类型" />
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
                <table class="Admin_Table" style="width: 100%">
                    <tr>
                        <td>周期要求：
                        </td>
                        <td>
                            <asp:DropDownList ID="zq" runat="server" Width="230">
                                <asp:ListItem>Regular 正常</asp:ListItem>
                                <asp:ListItem>Express 加急(40% surchargc 加收40%费用)</asp:ListItem>
                                <asp:ListItem>Emergency 特急(100% surcharge加收100%费用)</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                ControlToValidate="yp" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>报告形式：
                        </td>
                        <td>
                            <asp:DropDownList ID="bgxs" runat="server" Width="230">
                                <asp:ListItem>英文/English</asp:ListItem>
                                <asp:ListItem>中文/Chinese</asp:ListItem>
                                <asp:ListItem>数据/Test Data</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>报告领取：
                        </td>
                        <td>
                            <asp:DropDownList ID="bglq" runat="server" Width="230">
                                <asp:ListItem>自取/Self-Pick up</asp:ListItem>
                                <asp:ListItem>快递/Express</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>样品处理：
                        </td>
                        <td>
                            <asp:DropDownList ID="ypcl" runat="server" Width="230"
                                OnSelectedIndexChanged="ypcl_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>自取/Self-Pick up</asp:ListItem>
                                <asp:ListItem>请选择</asp:ListItem>

                                <asp:ListItem>无样品/Not Have</asp:ListItem>
                                <asp:ListItem Enabled="False">快递/Express(到付)</asp:ListItem>
                                <asp:ListItem>由实验室销毁/Disposed by Lab</asp:ListItem>
                            </asp:DropDownList>(选择快递时必填写快递接收人的名字，电话信息)
                            <asp:Label ID="Label8" runat="server" Text="必填" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>同意外包：</td>
                        <td>
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="230">

                                <asp:ListItem>否</asp:ListItem>
                                <asp:ListItem>是</asp:ListItem>

                            </asp:DropDownList>
                        </td>
                        <td>预计完成时间：</td>
                        <td>
                            <asp:TextBox ID="TextBox8" runat="server" Width="240" onclick="new Calendar().show(this.form.TextBox8);"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="TextBox8"></asp:RequiredFieldValidator>
                            <asp:Label ID="Label9" runat="server" Text="必填" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <%-- <td>预计费用：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                        </td>--%>
                        <td>客户等级：</td>
                        <td>
                            <asp:DropDownList ID="rwyouxian" runat="server" Width="80px">
                                <asp:ListItem>VIP客户</asp:ListItem>
                                <asp:ListItem Selected="True">普通客户</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>其他要求：
                        </td>
                        <td>
                            <asp:TextBox ID="qita" runat="server" Width="240"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>备注：
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="beizhu" runat="server" Width="400"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>

                        <td colspan="4">
                            <asp:Panel ID="Panel1" runat="server">




                                <fieldset>
                                    <legend style="color: Red">选择邮寄联系人
                 <asp:DropDownList ID="DropDownList6"
                     runat="server" AutoPostBack="true"
                     OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged">
                 </asp:DropDownList>
                                        <asp:TextBox ID="rwkehu"
                                            runat="server" Width="568px" Visible="False"></asp:TextBox>
                                        &nbsp;</legend>




                                    <table class="Admin_Table" style="width: 100%">
                                        <tr>
                                            <td align="center" colspan="4">
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

                                        <tr>
                                            <td>联系人：</td>
                                            <td>
                                                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                                            </td>

                                            <td>电话：</td>
                                            <td>
                                                <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>公司：</td>
                                            <td>
                                                <asp:TextBox ID="TextBox13" runat="server" Width="300px"></asp:TextBox>
                                            </td>

                                            <td>地址：</td>
                                            <td>
                                                <asp:TextBox ID="TextBox14" runat="server" Width="313px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>


                            </asp:Panel>

                        </td>

                    </tr>



                </table>
            </fieldset>
            <fieldset style="display: none">
                <legend style="color: Red">联系人
                <asp:DropDownList ID="DropDownList1" runat="server" Width="240" Enabled="false">
                </asp:DropDownList>
                </legend>
                <table class="Admin_Table">
                    <tr>
                        <td colspan="7" align="center">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
            <div style="text-align: center">
                <asp:Button ID="Button5" runat="server" Text="保存" Width="120" ForeColor="Red" OnClick="Button5_Click" Style="height: 27px" />
            </div>
            <asp:Literal ID="ld" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
