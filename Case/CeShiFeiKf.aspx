<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CeShiFeiKf.aspx.cs" EnableViewState="true" Inherits="CeShiFeiKf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客服核算费用</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <style type="text/css">
        .print {
            display: none;
        }

        .auto-style1 {
            height: 34px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
            EnableScriptLocalization="false">
        </asp:ScriptManager>
        <div>
            <fieldset>
                <legend>客服核算费用</legend>
                <table class="Admin_Table">
                    <tr>
                        <td colspan="4">
                            <fieldset>
                                <legend>原始报价信息</legend>
                                <asp:GridView ID="GridView4" runat="server" Width="100%" CssClass="Admin_Table"
                                    AutoGenerateColumns="False" OnRowDataBound="GridView4_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="baojiaid" HeaderText="报价编号" />
                                        <asp:BoundField DataField="kehuname" HeaderText="客户" />
                                        <asp:BoundField DataField="realdiscount" HeaderText="折扣" />
                                        <asp:BoundField DataField="zhehoujia" HeaderText="报价" />
                                        <asp:BoundField DataField="coupon" HeaderText="优惠后金额" />
                                        <asp:BoundField DataField="isVAT" HeaderText="是否含税" />
                                        <asp:BoundField DataField="kuozhanfei" HeaderText="扩展费" />
                                        <asp:BoundField HeaderText="报价人" DataField="fillname" />
                                        <asp:BoundField HeaderText="报价日期" DataField="filltime" DataFormatString="{0:d}" />
                                        <asp:HyperLinkField HeaderText="查看" Text="查看" DataNavigateUrlFormatString="~/Print/QuoPrint.aspx?baojiaid={0}&&customerid={1}"
                                            DataNavigateUrlFields="baojiaid,kehuid" Target="_blank" />
                                    </Columns>
                                    <HeaderStyle CssClass="Admin_Table_Title " />
                                </asp:GridView>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <fieldset>
                                <legend>业务员报价费用<asp:Button ID="Button5" runat="server" Text="处理不能勾选问题"
                                    OnClick="Button5_Click" Visible="False" /></legend>
                                <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False"
                                    DataKeyNames="id" OnRowCreated="GridView2_RowCreated">
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="项目编号" />
                                        <asp:BoundField DataField="ceshiname" HeaderText="测试项目" />
                                        <asp:BoundField DataField="biaozhun" HeaderText="标准" />
                                        <asp:BoundField DataField="neirong" HeaderText="内容" />
                                        <asp:BoundField DataField="yp" HeaderText="样品" />
                                        <asp:BoundField DataField="zhouqi" HeaderText="周期" />
                                        <asp:BoundField DataField="total" HeaderText="费用" />
                                        <asp:BoundField DataField="zhekou" HeaderText="折扣" />
                                        <asp:BoundField DataField="shuliang" HeaderText="数量" />

                                        <asp:BoundField DataField="hesuanbiaozhi" HeaderText="计算标志" />
                                        <asp:BoundField DataField="bumen" HeaderText="项目部门" />
                                        <asp:BoundField DataField="epiboly" HeaderText="外包" />
                                        <asp:BoundField DataField="epiboly_price" HeaderText="外包金额" />
                                        <asp:HyperLinkField DataTextField="id" HeaderText="附件" Target="_blank" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?id={0}"
                                            DataNavigateUrlFields="id" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox2_CheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="Admin_Table_Title " />
                                </asp:GridView>
                            </fieldset>
                            <fieldset style="display: none;">
                                <legend>工程师上报费用合计:<asp:Label ID="Label1" runat="server" Text=""></asp:Label></legend>
                                <asp:GridView ID="GridView3" DataKeyNames="id" Width="100%" runat="server" class="Admin_Table"
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

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="bumen" HeaderText="部门" />

                                    </Columns>
                                    <HeaderStyle CssClass="Admin_Table_Title " />
                                </asp:GridView>
                            </fieldset>
                        </td>
                    </tr>

                </table>

                <table class="Admin_Table">
                    <tr>
                        <td align="left" class="auto-style1">项目：
                        </td>
                        <td align="left" class="auto-style1">
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                        <td align="left" class="auto-style1">检测部门：
                        </td>
                        <td align="left" class="auto-style1">
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="160">
                                <asp:ListItem>新能源部</asp:ListItem>
                                <asp:ListItem>国际认证部</asp:ListItem>
                                <asp:ListItem>化学部</asp:ListItem>
                                <asp:ListItem>安规部</asp:ListItem>
                                <asp:ListItem>代付</asp:ListItem>
                                <asp:ListItem>其他</asp:ListItem>
                            </asp:DropDownList>
                            （承检部门提示：<asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                            ）类别非检测费和规费，部门请选空</td>
                    </tr>
                    <tr>
                        <td align="left">费用：</td>
                        <td align="left">
                            <asp:TextBox ID="TextBox7" runat="server" Text="0" ReadOnly="false"></asp:TextBox>
                        </td>
                        <td align="left">金额类别：
                            <asp:Label ID="Label4" runat="server" Text="折扣：" Visible="false"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="DropDownList3" runat="server" Width="160" Visible="false">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>0.9</asp:ListItem>
                                <asp:ListItem>0.8</asp:ListItem>
                                <asp:ListItem>0.7</asp:ListItem>
                                <asp:ListItem>0.6</asp:ListItem>
                                <asp:ListItem>0.5</asp:ListItem>
                            </asp:DropDownList>

                            <asp:DropDownList ID="DropDownList_project" runat="server">
                                <asp:ListItem Value="检测费">检测费</asp:ListItem>
                                <asp:ListItem Value="规费">规费</asp:ListItem>
                                <asp:ListItem Value="扩展费">扩展费</asp:ListItem>
                                <asp:ListItem Value="加减项">加减项</asp:ListItem>
                                <asp:ListItem Value="其他">其他</asp:ListItem>
                            </asp:DropDownList>
                            加减项和其他类别，如果是减少，请使用负数（扩展费不需要）
                        </td>
                    </tr>
                    <tr>

                        <td align="left">备注：
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="TextBox5" runat="server" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="TextBox6" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4" style="text-align: center">
                            <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                                OnClick="Button1_Click" Text="保存核算费用" />
                            <asp:Button ID="btn_affirm" runat="server" Text="客服确认" OnClick="btn_affirm_Click" />
                            <asp:Button ID="btn_account" runat="server" Text="调账" OnClick="btn_account_Click" />
                            <asp:Button ID="Button2" runat="server" Text="完成核算" OnClick="Button2_Click1" Visible="false" />
                            <asp:Button ID="Button3" runat="server" Text="取消核算" OnClick="Button3_Click" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView ID="GridView1" ShowFooter="True" class="Admin_Table"
                                Width="100%" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                                OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="调账">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_account" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                                CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle ForeColor="Green" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="type" HeaderText="项目" ReadOnly="true" />
                                    <asp:BoundField DataField="zhekou" HeaderText="折扣" ReadOnly="true" />
                                    <asp:BoundField DataField="feiyong" HeaderText="应收" />
                                    <asp:BoundField DataField="project" HeaderText="类别" ReadOnly="true" />
                                    <asp:BoundField DataField="beizhu2" HeaderText="备注" />
                                    <asp:BoundField DataField="beizhu3" HeaderText="检测部门" ReadOnly="true" />
                                    <asp:BoundField DataField="fillname" HeaderText="录入人" ReadOnly="true" />
                                    <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="时间" ReadOnly="true" />
                                    <asp:BoundField DataField="shoufeibianhao" DataFormatString="{0:d}" HeaderText="收费编号" ReadOnly="true" />
                                    <asp:HyperLinkField HeaderText="打印" Text="打印" DataNavigateUrlFormatString="~/Print/InvoicePrint.aspx?baojiaid={0}&&customerid={1}&&inid={2}"
                                        DataNavigateUrlFields="baojiaid,kehuid,shoufeibianhao" Target="_blank" HeaderStyle-CssClass="print" ItemStyle-CssClass="print">
                                        <HeaderStyle CssClass="print"></HeaderStyle>
                                        <ItemStyle CssClass="print"></ItemStyle>
                                    </asp:HyperLinkField>
                                    <asp:BoundField DataField="xmid" HeaderText="项目编号" />
                                    <asp:CommandField HeaderText="取消" ShowDeleteButton="True" ShowEditButton="true">
                                        <ItemStyle ForeColor="Blue" />
                                    </asp:CommandField>
                                </Columns>
                                <HeaderStyle CssClass="Admin_Table_Title " />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td colspan="4" align="center">
                            <asp:DropDownList ID="DropDownList4" runat="server">
                                <asp:ListItem Value="1">新增收费单</asp:ListItem>
                                <asp:ListItem Value="2">追加到已有收费单</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="Button4" runat="server" Text="生成收费单" OnClick="Button4_Click" /></td>
                    </tr>
                    <tr style="display: none;">
                        <td colspan="4" align="center">如果修改或者删除已有收费编号的项目，在修改或删除后系统自动跳入相关收费单处，请点击"修改保存"以便更改相应费用</td>
                    </tr>
                    <tr style="display: none;">
                        <td colspan="4" align="center">以admin用户可修改已到账后费用所在的部门，修改部门时请确保输入的部门名称和该页面“部门”下拉框中的名称一致</td>
                    </tr>


                </table>
            </fieldset>
            <asp:Literal ID="ld" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
