<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskSee.aspx.cs" Inherits="Case_TaskSee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务通知书(任务查看)</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <script type="text/javascript">
        function open1() {
            <%--            window.open('../Print/PrintWeiTuo.aspx?bianhao=<%=tijiaobianhao%>');--%>
            window.open('../Print/PrintWeiTuo.aspx?bianhao=<%=tijiaobianhao%>&rwid=<%=rwid%>');
        }
        function open2() {
            window.open('../Print/TaskPrint.aspx?bianhao=<%=tijiaobianhao%>');
        }

        function open3() {
            window.open('../Case/CeShiFeiGc.aspx?bianhao=<%=tijiaobianhao%>');
        }

        function open4() {
            window.open('../Case/CeShiFeiKf.aspx?bianhao=<%=tijiaobianhao%>&&baojiaid=<%=baojiaid%>&&kehuid=<%=kehuid%>');
        }
        function open5() {
            window.open('../Report/XinBaogaoADD.aspx?renwuid=<%=task%>');
        }
        function open6() {
            window.open('../Case/ziliaoaddm.aspx?xiangmuid=<%=task%>');
        }

        function open7() {
            window.open('../YangPin/YangPinAdd.aspx?bianhao=<%=tijiaobianhao%>&&baojiaid=<%=baojiaid%>&&kehuid=<%=kehuid%>');
        }


        function open8() {
            window.open('../Case/TaskYanQi.aspx?renwuid=<%=tijiaobianhao%>');
        }

        function open9() {
            window.open('../Case/CeShi.aspx?id=<%=tijiaobianhao%>');
        }

        function open33() {
            window.open('../Case/DianZiShangBao.aspx?id=<%=task%>');
        }


    </script>
    <style type="text/css">
        .auto-style1 {
            width: 600px;
        }

        .xmid {
            display: none;
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
                <legend style="color: Red; text-align: center;">操作</legend>
                <asp:Panel ID="Panel1" runat="server">

                    <input id="Button16" type="button" runat="server" value="打印委托" onclick="open1()" />
                    <input id="Button17" type="button" runat="server" value="打印任务" onclick="open2()" />
                    <input id="Button1" type="button" runat="server" value="上报费用" onclick="open3()" visible="False" />

                    <input id="Button15" type="button" runat="server" value="电子上报" onclick="open33()" visible="False" />

                    <input id="Button9" type="button" runat="server" value="核算费用" onclick="open4()" />
                    <input id="Button10" type="button" value="获取报告" runat="server" onclick="open5()" />
                    <input id="Button11" type="button" value="客户资料" runat="server" onclick="open6()" />
                    <input id="Button12" type="button" value="关联样品" runat="server" onclick="open7()" />
                    <input id="Button13" type="button" value="延期处理" runat="server" visible="false" onclick="open8()" />

                    <input id="Button14" type="button" value="完成测试" visible="false" runat="server" onclick="open9()" />

                    <asp:Button ID="Button7" runat="server" Text="查看报价单" OnClick="Button7_Click" />




                </asp:Panel>
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
                        <td style="text-align: left; width: 90px;">制造单位：
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="TextBox6" runat="server" Width="300"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 110px;">客户名称：
                        </td>
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
                        <td style="text-align: left; width: 110px;">报价编号：
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>报价人:<asp:Label ID="Label1"
                                runat="server" Text=""></asp:Label>
                            &nbsp;&nbsp;
                            助理：<asp:Label ID="lb_zhuli" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend style="color: Red">联系信息</legend>
                <table align="center" class="Admin_Table" width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="false"
                                CssClass="Admin_Table">
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
                                    <asp:BoundField DataField="lururen" HeaderText="业务员" Visible="false" />
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
                <legend style="color: Red">任务信息</legend>
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
                                <asp:ListItem>受理</asp:ListItem>
                                <asp:ListItem>下达</asp:ListItem>
                                <asp:ListItem>进行中</asp:ListItem>
                                <asp:ListItem>在检</asp:ListItem>
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
                            <asp:TextBox ID="rwxiadariqi" runat="server" onclick="popUpCalendar(this,document.forms[0].rwxiadariqi,'yyyy-mm-dd')"></asp:TextBox>
                        </td>
                        <td>优先级别：
                        </td>
                        <td>
                            <asp:DropDownList ID="rwyouxian" runat="server" Width="80px">
                                <asp:ListItem>A</asp:ListItem>
                                <asp:ListItem>B</asp:ListItem>
                                <asp:ListItem>C</asp:ListItem>
                                <asp:ListItem>D</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>要求完成日期：
                        </td>
                        <td>
                            <asp:TextBox ID="rwwancheng" runat="server" onclick="popUpCalendar(this,document.forms[0].rwwancheng,'yyyy-mm-dd')"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入要求完成日期" ControlToValidate="rwwancheng"></asp:RequiredFieldValidator>
                        </td>
                        <td>备注：
                        </td>
                        <td>
                            <asp:TextBox ID="rwbeizhu" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>客户时限(天)：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                        </td>
                        <td>考核时限(天)：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <%--  <tr>
                        <td>检验类别：
                        </td>
                        <td colspan="3">
                            <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatDirection="Horizontal"
                                Width="100%" RepeatLayout="Flow">
                            </asp:CheckBoxList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>任务承接：
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="DropDownList2" Visible="false" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList1" Visible="false" runat="server">
                            </asp:DropDownList>
                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                            </asp:CheckBoxList>
                            <asp:TextBox ID="TextBox1" Enabled="false" Visible="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False"
                                OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                                OnRowUpdating="GridView1_RowUpdating" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound">
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
                                    <asp:BoundField DataField="feiyong" HeaderText="费用" />
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                    <asp:BoundField DataField="jishuyaoqiu" HeaderText="技术要求" Visible="false" />



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
                <legend style="color: Red">工程师测试项</legend>
                修改状态原因：<asp:TextBox ID="txcause" runat="server" Width="395px"></asp:TextBox>
                <br />
                <asp:GridView ID="gv_projectitem" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="gv_projectitem_RowCommand" OnRowDataBound="gv_projectitem_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="taskid" HeaderText="任务号" />
                        <asp:BoundField DataField="xmname" HeaderText="测试项目" />
                        <asp:BoundField DataField="engineer" HeaderText="承检工程师" />
                        <asp:BoundField DataField="fillname" HeaderText="分派人" />
                        <asp:BoundField DataField="filltime" HeaderText="分派时间" />
                        <asp:BoundField DataField="state" HeaderText="状态" />
                        <asp:BoundField DataField="cause" HeaderText="状态操作原因" />
                        <asp:BoundField DataField="id" HeaderText="id" HeaderStyle-CssClass="xmid" ItemStyle-CssClass="xmid" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="suspend" CommandArgument='<%#Eval("id") %>'>中止</asp:LinkButton>||
                                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="zt" CommandArgument='<%#Eval("id") %>'>暂停</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title" />
                </asp:GridView>
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
                        <td class="auto-style1">
                            <asp:TextBox ID="cp" runat="server"></asp:TextBox>
                        </td>
                        <td>生产日期：</td>
                        <td>
                            <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>型号规格：
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="guige" runat="server"></asp:TextBox>
                        </td>
                        <td>备注：
                        </td>
                        <td>
                            <asp:TextBox ID="ypbeizhu" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>产品商标：
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="ypshanbiao" runat="server"></asp:TextBox>
                        </td>
                        <td>产品类型：</td>
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
                        <td>样品数量：
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="ypshuliang" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>
                        </td>
                        <td>取样方式：
                        </td>
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
                        <td>其他要求：
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr style="display: none;">
                        <td>预计完成时间：
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr style="display: none;">
                        <td>是否外包：
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="TextBox9" Visible="false" runat="server" onclick="popUpCalendar(this,document.forms[0].ypsongjianriqi,'yyyy-mm-dd')"
                                Width="137px"></asp:TextBox><asp:DropDownList ID="DropDownList3" runat="server" Width="240">
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
                    <tr>
                        <td>样品快递接收人</td>
                        <td colspan="3">
                            <asp:TextBox ID="rwkehu" runat="server"
                                Width="774px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table"
                                Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                                CommandName="BussinessNeeds" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Green" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="sampleid" HeaderText="样品编号" />
                                    <asp:BoundField DataField="receivetime" DataFormatString="{0:d}" HeaderText="收到日期" />
                                    <asp:BoundField DataField="kehuname" HeaderText="客户名称" />
                                    <asp:BoundField DataField="name" HeaderText="样品名称" />
                                    <asp:BoundField DataField="model" HeaderText="型号" />
                                    <asp:BoundField DataField="position" HeaderText="制造厂家" />
                                    <asp:BoundField DataField="type" HeaderText="报告种类" />
                                    <asp:BoundField DataField="remark" HeaderText="备注" />
                                    <asp:BoundField DataField="state" HeaderText="状态" />
                                    <asp:BoundField DataField="anjianid" HeaderText="任务编号" />
                                    <asp:BoundField DataField="pub_field3" HeaderText="样品处置" />
                                    <asp:HyperLinkField HeaderText="附件" Text="附件" Target="_blank" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?baojiaid={0}&amp;&amp;id={1}"
                                        DataNavigateUrlFields="baojiaid,sampleid" />


                                    <asp:HyperLinkField HeaderText="配件" Text="配件" Target="_blank" DataNavigateUrlFormatString="~/YangPin/YanPinManage.aspx?baojiaid={0}&&kehuid={1}&&sampleid={2}&&bianhao={3}&&biaozhi=1"
                                        DataNavigateUrlFields="baojiaid,kehuid,sampleid,bianhao" />


                                    <asp:HyperLinkField HeaderText="流转" Text="流转" Target="_blank" DataNavigateUrlFormatString="~/Print/YangPinLiuZhuan.aspx?baojiaid={0}&&kehuid={1}&&sampleid={2}&&bianhao={3}"
                                        DataNavigateUrlFields="baojiaid,kehuid,sampleid,bianhao" />
                                </Columns>
                                <HeaderStyle CssClass="Admin_Table_Title " />

                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend style="color: Red">工程信息</legend>
                <asp:GridView ID="GridView6" runat="server" Width="100%" CssClass="Admin_Table" DataKeyNames="id"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="bianhao" HeaderText="任务编号"></asp:BoundField>
                        <asp:BoundField DataField="bumen" HeaderText="部门"></asp:BoundField>
                        <asp:BoundField DataField="name" HeaderText="工程师"></asp:BoundField>
                        <asp:BoundField DataField="fillname" HeaderText="填写人"></asp:BoundField>
                        <asp:BoundField DataField="filltime" HeaderText="填写日期"></asp:BoundField>
                        <asp:HyperLinkField Text="工程" HeaderText="修改" Target="button" DataNavigateUrlFormatString="~/Case/AnJianInFo.aspx?xiangmuid={0}&&bumen={1}&&id={2}"
                            DataNavigateUrlFields="bianhao,bumen,id" Visible="False" />
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title " />
                </asp:GridView>
            </fieldset>


            <fieldset style="display: none;">
                <legend style="color: Red">记录接收</legend>
                <asp:GridView ID="GridView8" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False"
                    DataKeyNames="id">
                    <Columns>


                        <asp:TemplateField Visible="false">
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
                                    CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle ForeColor="Green" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="rwid" HeaderText="任务编号" />
                        <asp:BoundField DataField="bumen" HeaderText="部门" />
                        <asp:BoundField DataField="beizhu2" HeaderText="是否参与" />
                        <asp:BoundField DataField="jieshouname" HeaderText="接收人" />
                        <asp:BoundField DataField="jieshoutime" HeaderText="接收日期" />
                        <asp:BoundField DataField="beizhu" HeaderText="备注" />

                    </Columns>

                    <HeaderStyle CssClass="Admin_Table_Title " />
                </asp:GridView>


            </fieldset>



            <fieldset>
                <legend style="color: Red">报告信息</legend>


                <asp:GridView ID="grid_itembaogao" runat="server" AutoGenerateColumns="False" Width="100%" class="Admin_Table">
                    <Columns>
                        <asp:BoundField DataField="baogaoid" HeaderText="报告编号" />
                        <asp:BoundField DataField="taskid" HeaderText="任务号" />
                        <asp:BoundField DataField="xmname" HeaderText="测试项目" />
                        <asp:BoundField DataField="biaozhun" HeaderText="标准" />
                        <asp:BoundField DataField="fillname" HeaderText="获取人" />
                        <asp:BoundField DataField="filltime" HeaderText="获取时间" />
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title" />
                </asp:GridView>
                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table"
                    DataKeyNames="id" Style="font-size: 9pt" Width="100%" OnRowCommand="GridView4_RowCommand" OnRowDataBound="GridView4_RowDataBound" Visible="False">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                    CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle ForeColor="Green" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="baogaoid" HeaderText="报告编号" ReadOnly="True" />
                        <asp:BoundField DataField="leibie" HeaderText="类别" />
                        <asp:BoundField DataField="dayintime" HeaderText="草稿" />
                        <asp:BoundField DataField="wanchengtime" HeaderText="实际完成" />
                        <asp:BoundField DataField="statebumen1" DataFormatString="{0:d}" HeaderText="签字" />
                        <asp:BoundField DataField="statebumen2" DataFormatString="{0:d}" HeaderText="审核" />
                        <asp:BoundField DataField="pizhunby" DataFormatString="{0:d}" HeaderText="缮制" />
                        <asp:BoundField DataField="pizhundate" DataFormatString="{0:d}" HeaderText="缮制日期" />
                        <asp:BoundField DataField="fafangdate" DataFormatString="{0:d}" HeaderText="发放日期" />
                        <asp:BoundField DataField="danganid" DataFormatString="{0:d}" HeaderText="归档编号" />
                        <asp:BoundField DataField="dangandate" DataFormatString="{0:d}" HeaderText="归档日期" />
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title " />
                </asp:GridView>
            </fieldset>


            <fieldset style="display: none;">
                <legend style="color: Red">上报费用</legend>

                <asp:GridView ID="GridView11" DataKeyNames="id" Width="100%" runat="server" class="Admin_Table"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="项目编号" />
                        <asp:BoundField DataField="type" HeaderText="类型" />
                        <asp:BoundField DataField="xiangmu" HeaderText="项目" />
                        <asp:BoundField DataField="shuliang" HeaderText="数量" />
                        <asp:BoundField DataField="xiaoji" HeaderText="费用" />
                        <asp:BoundField DataField="filltime" HeaderText="填写时间" />
                        <asp:BoundField DataField="fillname" HeaderText="填写人" />
                        <asp:BoundField DataField="beizhu3" HeaderText="备注" />
                        <asp:BoundField DataField="beizhu2" HeaderText="计算标志" />
                        <asp:BoundField DataField="fwz" HeaderText="是否被作废" />
                        <asp:BoundField DataField="sb" ReadOnly="true" HeaderText="是否为上报费用" />


                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title " />
                </asp:GridView>



            </fieldset>

            <fieldset>
                <legend style="color: Red; display: none;">核算信息</legend>
                <asp:GridView ID="GridView10" ShowFooter="true" Visible="false" class="Admin_Table"
                    Width="100%" runat="server" AutoGenerateColumns="false"
                    DataKeyNames="id">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                    CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                            <ItemStyle ForeColor="Green" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="type" HeaderText="项目" />
                        <asp:BoundField DataField="baojia" HeaderText="报价" ReadOnly="true" />
                        <asp:BoundField DataField="zhekou" HeaderText="折扣" ReadOnly="true" />
                        <asp:BoundField DataField="feiyong" HeaderText="应收" />

                        <asp:BoundField DataField="beizhu2" HeaderText="备注" />
                        <asp:BoundField DataField="beizhu3" HeaderText="类别" />
                        <asp:BoundField DataField="fillname" HeaderText="录入人" ReadOnly="true" />
                        <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="时间" ReadOnly="true" />

                        <asp:BoundField DataField="shoufeibianhao" DataFormatString="{0:d}" HeaderText="收费编号" ReadOnly="true" />

                        <asp:HyperLinkField HeaderText="打印" Text="打印" Visible="false" DataNavigateUrlFormatString="~/Print/InvoicePrint.aspx?baojiaid={0}&&customerid={1}&&inid={2}"
                            DataNavigateUrlFields="baojiaid,kehuid,shoufeibianhao" Target="_blank" />
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title " />
                </asp:GridView>

            </fieldset>


            <fieldset style="display: none;">
                <legend style="color: Red">请款信息</legend>
                <asp:GridView ID="GridView7" runat="server"
                    class="Admin_Table" Width="100%" AutoGenerateColumns="False"
                    DataKeyNames="id">
                    <Columns>



                        <asp:BoundField DataField="inid" HeaderText="收费编号" />


                        <asp:BoundField DataField="taskno" HeaderText="任务编号" />


                        <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号" />
                        <asp:BoundField DataField="feiyong1" HeaderText="请款金额" DataFormatString="{0:N2}" />

                        <asp:BoundField DataField="kehuname" HeaderText="客户名称" />


                        <asp:BoundField DataField="name" HeaderText="客户联系人" />

                        <asp:BoundField DataField="kf" HeaderText="项目经理" />
                        <asp:BoundField DataField="hesuanbiaozhi" HeaderText="对账记录" />
                        <asp:BoundField DataField="fillname" HeaderText="开单人" />
                        <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="开单日期" />


                        <asp:HyperLinkField HeaderText="下载" Text="下载" Visible="false" Target="_blank" DataNavigateUrlFormatString="~/Income/BaoGaoFirstUpLoad.aspx?inid={0}"
                            DataNavigateUrlFields="inid" />



                        <asp:HyperLinkField HeaderText="打印" Text="打印" DataNavigateUrlFormatString="~/Print/InvoicePrint.aspx?baojiaid={0}&&customerid={1}&&inid={2}"
                            DataNavigateUrlFields="baojiaid,kehuid,inid" Target="_blank" />



                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title " />
                </asp:GridView>


            </fieldset>






            <fieldset style="display: none;">
                <legend style="color: Red">暂停恢复</legend>
                <table align="center" class="Admin_Table" width="100%">

                    <tr>
                        <td>
                            <asp:GridView ID="GridView9" Width="100%" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                                CssClass="Admin_Table">
                                <Columns>
                                    <asp:BoundField DataField="rwbianhao" HeaderText="项目编号" />
                                    <asp:BoundField DataField="beizhu2" HeaderText="原因" />
                                    <asp:BoundField DataField="name" HeaderText="操作人" />
                                    <asp:BoundField DataField="time2" HeaderText="操作时间" />
                                    <asp:BoundField DataField="beizhu3" HeaderText="操作类别" />

                                    <asp:BoundField DataField="beizhu4" HeaderText="暂停类别" />
                                    <asp:BoundField DataField="beizhu5" HeaderText="暂停原因" />
                                </Columns>
                                <HeaderStyle CssClass="Admin_Table_Title " />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>


            <fieldset>
                <legend style="color: red">客服修改任务状态信息</legend>
                <asp:GridView ID="gv_zanting" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="rwbianhao" HeaderText="任务编号" />
                        <asp:BoundField DataField="beizhu3" HeaderText="任务状态" />
                        <asp:BoundField DataField="name" HeaderText="操作人" />
                        <asp:BoundField DataField="time1" HeaderText="操作时间" />
                        <asp:BoundField DataField="beizhu5" HeaderText="修改原因" />
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title" />
                </asp:GridView>

            </fieldset>


            <fieldset>
                <legend style="color: Red">备注信息</legend>
                <table align="center" class="Admin_Table" width="100%">
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TextBox15" runat="server" Width="63%"></asp:TextBox>
                            <asp:Button ID="Button8" runat="server" Width="9%" Text="保存备注"
                                OnClick="Button1_Click1" />
                            <asp:Button ID="Button18" runat="server" Text="申请暂停" OnClick="Button18_Click" Visible="False" />
                            <asp:Button ID="Button19" runat="server" Text="申请恢复" OnClick="Button19_Click" Visible="False" />
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

            <div style="text-align: center">
                <asp:Button ID="Button6" runat="server" Text="结束任务" OnClick="Button6_Click" Visible="false"
                    Enabled="false" />
                <asp:Button ID="Button2" runat="server" Text="修改保存" OnClick="Button2_Click" Visible="false" />
                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="受理" Visible="false" />
                <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="下达到科室" Visible="false" />
                <asp:Button ID="Button5" runat="server" Text="打印任务标签" Visible="false" OnClick="Button5_Click" />
                <asp:TextBox ID="ypsongjianriqi" Visible="false" runat="server" onclick="popUpCalendar(this,document.forms[0].ypsongjianriqi,'yyyy-mm-dd')"
                    Width="138px"></asp:TextBox>
            </div>
            <asp:Literal ID="ld" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
