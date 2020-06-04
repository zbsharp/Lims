<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" CodeFile="YangPinAdd.aspx.cs" Inherits="YangPinAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>样品录入
    </title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js">
<script language="javascript" src="popcalendar.js"></script>
    <script language="javascript" src="../../Js/calendar.js" type="text/javascript">
function bnQuery_onclick() {

}

    </script>
    <script type="text/javascript" src="../Celend/popcalendar.js"></script>

    <script type="text/javascript">
        function add() {
            var a = parseFloat(document.getElementById("TextBox8").value) * parseFloat(document.getElementById("TextBox9").value);

            if (a != null) {
                document.getElementById("TextBox10").value = a;
            }
        }
        function open1() {
            window.open('../Case/TaskSee.aspx?tijiaobianhao=<%=bianhao%>&&chakan=1');
        }


    </script>

</head>
<body>
    <form name="form1" runat="server" id="form1">
        <div class="Body_Title">
            业务受理 》》样品录入<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>

        <div>

            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
            </asp:ScriptManager>



            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                width="100%">
                <tr bgcolor="#f4faff" style="display: none;">
                    <td style="text-align: left; width: 110px;">厂商名称：</td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="TextBox1" runat="server" Width="90%"></asp:TextBox>
                        <asp:TextBox ID="TextBox12" runat="server"
                            ReadOnly="True"></asp:TextBox>申请编号：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        测试项目报告种类<asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="text-align: left; width: 110px;">任务编号：</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="查询" />
                        <input id="Button10" type="button" value="查看任务" runat="server" onclick="open1()" />

                    </td>
                    <td style="text-align: left; width: 90px;">收到日期：</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="TextBox11" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox11,'yyyy-mm-dd')"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="text-align: left; width: 110px;">样品名称：</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                    <td style="text-align: left; width: 90px;">数量：</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="TextBox7" runat="server" Text="1" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>否</asp:ListItem>
                            <asp:ListItem>是</asp:ListItem>
                        </asp:DropDownList>
                        (否表示一个数量占一行、是表示数量值有多少就有多少行)</td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="text-align: left; width: 110px;">型号：</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                    <td style="text-align: left; width: 90px;">单位：</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="TextBox8" runat="server" Text="台"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 110px; text-align: left">制造厂家：</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="TextBox9" runat="server" Width="250px"></asp:TextBox></td>
                    <td style="width: 110px; text-align: left">价值类别：</td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>A1</asp:ListItem>
                            <asp:ListItem>A2</asp:ListItem>
                            <asp:ListItem>A3</asp:ListItem>
                            <asp:ListItem>B1</asp:ListItem>
                            <asp:ListItem>B2</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp; 保存区：<asp:DropDownList ID="DropDownList3" runat="server">
                            <asp:ListItem>A2-2</asp:ListItem>
                            <asp:ListItem>A2-3</asp:ListItem>
                            <asp:ListItem>A3-2</asp:ListItem>
                            <asp:ListItem>A3-3</asp:ListItem>
                            <asp:ListItem>B1-1</asp:ListItem>
                            <asp:ListItem>B1-2</asp:ListItem>
                            <asp:ListItem>B1-3</asp:ListItem>
                            <asp:ListItem>C2-1</asp:ListItem>
                            <asp:ListItem>C2-2</asp:ListItem>
                            <asp:ListItem>C2-3</asp:ListItem>
                            <asp:ListItem>C3-1</asp:ListItem>
                            <asp:ListItem>C3-2</asp:ListItem>
                            <asp:ListItem Value="C3-3">C3-3</asp:ListItem>
                            <asp:ListItem>L1-1</asp:ListItem>
                            <asp:ListItem>L1-2</asp:ListItem>
                            <asp:ListItem>L1-3</asp:ListItem>
                            <asp:ListItem>L2-1</asp:ListItem>
                            <asp:ListItem>L2-2</asp:ListItem>
                            <asp:ListItem>L2-3</asp:ListItem>
                            <asp:ListItem>K1</asp:ListItem>
                            <asp:ListItem>龙华待测</asp:ListItem>
                            <asp:ListItem>龙华已测</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr bgcolor="#f4faff">
                    <%--  <td style="width: 110px; text-align: left">
                                保管要求：</td>
            <td style="text-align: left">
                               <asp:CheckBoxList ID="CheckBoxList2" runat="server" 
              RepeatDirection="Horizontal" Width="100%" RepeatColumns="5" RepeatLayout="Flow">
                                    <asp:ListItem>外观无损</asp:ListItem>
                                    <asp:ListItem>配件齐全</asp:ListItem>
                                    <asp:ListItem>装配还原</asp:ListItem>
                                    <asp:ListItem>包装完好</asp:ListItem>
                                </asp:CheckBoxList> 
            </td>--%>
                    <td style="width: 90px; text-align: left">最后处置：</td>
                    <td style="text-align: left">
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server"
                            RepeatDirection="Horizontal" Width="100%" RepeatColumns="5" RepeatLayout="Flow">
                            <asp:ListItem>退回客户</asp:ListItem>
                            <asp:ListItem>实验室封存</asp:ListItem>
                            <asp:ListItem>实验室销毁</asp:ListItem>
                            <asp:ListItem>待确认</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td colspan="2">A1:客户要求保密样品 &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;A2：价值1000元以上样品&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;A3:其他&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;B1:检后有价值样品&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; B2:检后无价值样品</td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="text-align: left; width: 110px;">备注：</td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="TextBox10" runat="server" Width="90%"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td>标签备注</td>
                    <td colspan="3">
                        <asp:TextBox ID="txbiaoqianremork" runat="server"></asp:TextBox>最好不超过6个字
                    </td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td colspan="4" style="text-align: center">
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                            Text="保 存" OnClick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" Text="找样品关联" OnClick="Button2_Click" />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        &nbsp;
                    </td>
                </tr>

                <tr bgcolor="#f4faff">
                    <td colspan="4" style="text-align: left">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" CssClass="Admin_Table"
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

                                        <asp:BoundField DataField="remark" HeaderText="备注" />
                                        <asp:BoundField DataField="state" HeaderText="状态" />
                                        <asp:BoundField DataField="anjianid" HeaderText="任务编号" />


                                        <asp:TemplateField HeaderText="编辑">
                                            <ItemTemplate>
                                                <span style="cursor: hand; color: Blue;" onclick="window.open('YangPinSee.aspx?yangpinid=<%#Eval("id") %>')">
                                                    <asp:Label ID="seeLB" runat="server" Text="编辑"></asp:Label></span>|| <span style="cursor: hand; color: Blue;"
                                                        onclick="window.open('../Print/YangPinFen.aspx?sampleid=<%#Eval("sampleid") %>')">
                                                        <asp:Label ID="Label3" runat="server" Text="标签"></asp:Label></span>|| <span style="cursor: hand; color: Blue;"
                                                            onclick="window.open('YangPinSee2.aspx?yangpinid=<%#Eval("id") %>')">
                                                            <asp:Label ID="Label7" runat="server" Text="查看"></asp:Label></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:HyperLinkField HeaderText="配件" Text="配件" Target="_blank" DataNavigateUrlFormatString="~/YangPin/YanPinManage.aspx?baojiaid={0}&&kehuid={1}&&sampleid={2}&&bianhao={3}"
                                            DataNavigateUrlFields="baojiaid,kehuid,sampleid,bianhao" />


                                        <asp:HyperLinkField HeaderText="附件" Text="附件" Target="_blank" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?baojiaid={0}&amp;&amp;id={1}"
                                            DataNavigateUrlFields="baojiaid,sampleid" />
                                    </Columns>
                                    <HeaderStyle CssClass="Admin_Table_Title " />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时没有数据"></asp:Label>
                                    </EmptyDataTemplate>

                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <a href="http://www.woying.net/" target="_blank"><span style="font-size: 13pt; vertical-align: middle; color: red">

                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="0"
                                CompletionSetCount="12" EnableViewState="true" MinimumPrefixLength="2" ServiceMethod="Getsuggestion"
                                ServicePath="~/Customer/WebService.asmx" TargetControlID="TextBox9">
                            </cc1:AutoCompleteExtender>
                        </span><span id="RequiredFieldValidator1" style="display: none; color: red"></span>
                        </a>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
