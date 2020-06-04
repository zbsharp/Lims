<%@ Page Language="C#" AutoEventWireup="true" CodeFile="daozhangrenling.aspx.cs" Inherits="Income_daozhangrenling" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>财务对账</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
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

    <style type="text/css">
        .style1 {
            width: 25%;
        }

        .auto-style1 {
            height: 40px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            费用管理 》》到账认领
        </div>
        <div>
            <table class="Admin_Table" width="100%">
                <tr>
                    <td>付款客户：</td>
                    <td>
                        <asp:Label ID="lbCusetomername" runat="server"></asp:Label></td>
                    <td class="style1">付款日期：</td>
                    <td>
                        <asp:Label ID="lbpaytime"
                            runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">付款金额：</td>
                    <td style="width: 25%">
                        <asp:Label ID="lbpaymoney" runat="server"></asp:Label>
                    </td>
                    <td class="style1">未分金额：</td>
                    <td style="width: 25%">
                        <asp:Label ID="lbnotpay" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">已分金额：</td>
                    <td style="width: 25%">
                        <asp:Label ID="lballocatedmoney" runat="server"></asp:Label>
                    </td>
                    <td class="style1">其中已经确认金额：</td>
                    <td style="width: 25%">
                        <asp:Label ID="lbokmoney" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        币种：<asp:Label ID="lbcurrency" runat="server" Text=""></asp:Label>
                        <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4">客户名称或任务号或报价单号或付款单位或委托单位
                    <asp:TextBox ID="TextBox1" runat="server" placeholder="搜索查询框"></asp:TextBox>

                        <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />
                        检测金额指检测费用</td>
                </tr>

                <tr>
                    <td align="left" colspan="4" style="text-align: center">
                        <asp:GridView ID="GridView1" ShowFooter="True" runat="server"
                            class="Admin_Table" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound"
                            OnRowCommand="GridView1_RowCommand">
                            <Columns>


                                <asp:BoundField DataField="rwbianhao" HeaderText="任务编号" />


                                <asp:BoundField DataField="baojiaid" HeaderText="报价单号" />
                                <asp:BoundField DataField="kehuname" HeaderText="客户" />


                                <asp:BoundField DataField="weituodanwei" HeaderText="委托单位" />


                                <asp:BoundField DataField="jiancefeiyong" HeaderText="检测金额" />
                                <asp:BoundField DataField="currency" HeaderText="币种" />
                                <asp:BoundField DataField="xiadariqi" HeaderText="任务受理日期" DataFormatString="{0:d}" />

                                <%--  <asp:TemplateField HeaderText="收费项明细">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="按任务号" CommandArgument='<%# Eval("rwbianhao") %>'
                                            CommandName="showtaskdetail"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="收费项明细">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="按报价号" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                            CommandName="showquotationdetail"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4">展开明细，非检测费减扣项（即核算金额为负数的项），系统会自动进行关联分配:<br />
                        自动关联项目：
                    <asp:GridView ID="GridView4" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table"
                        OnRowDataBound="GridView4_RowDataBound">

                        <Columns>

                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                        CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="baojiaid" HeaderText="报价号" />

                            <asp:BoundField DataField="taskid" HeaderText="任务号" />
                            <asp:BoundField DataField="type" HeaderText="项目" />
                            <asp:BoundField DataField="beizhu2" HeaderText="备注" />
                            <asp:BoundField DataField="beizhu3" HeaderText="部门" />
                            <asp:BoundField DataField="project" HeaderText="类别" />
                            <asp:BoundField DataField="feiyong" HeaderText="应收" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="okmoney" HeaderText="已确认金额" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="yifenkuan" HeaderText="已分款金额" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="" HeaderText="未分款金额" DataFormatString="{0:N2}" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                        金额分配项目：
                    <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table"
                        OnRowDataBound="GridView2_RowDataBound">
                        <Columns>
                            <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                        CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="baojiaid" HeaderText="报价号" />
                            <asp:BoundField DataField="taskid" HeaderText="任务号" />
                            <asp:BoundField DataField="type" HeaderText="项目" />
                            <asp:BoundField DataField="beizhu2" HeaderText="备注" />
                            <asp:BoundField DataField="beizhu3" HeaderText="部门" />
                            <asp:BoundField DataField="type" HeaderText="类别" />
                            <asp:BoundField DataField="feiyong" HeaderText="应收" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="okmoney" HeaderText="已确认金额" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="yifenkuan" HeaderText="已分款金额" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="" HeaderText="未分款金额" DataFormatString="{0:N2}" />
                            <asp:TemplateField HeaderText="本次分款金额" ItemStyle-BackColor="AliceBlue">
                                <ItemTemplate>
                                    <asp:TextBox ID="fenpeijine" runat="server" Visible="true"
                                        Width="100px" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="Button_addcash" runat="server" Text="项目分款"
                            OnClick="Button_addcash_Click" />

                    </td>
                </tr>
                <tr>
                    <td colspan="4">已分款金额（自动匹配的分款不能删除，系统会根据删除其他项操作同步执行变更）：
                    <asp:GridView ID="GridView3" runat="server" CssClass="Admin_Table"
                        AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="id" OnRowDataBound="GridView3_RowDataBound"
                        OnRowDeleting="GridView3_RowDeleting">

                        <Columns>
                            <asp:TemplateField HeaderText="序 号" Visible="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("0000") %>'
                                        CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="baojiaid" HeaderText="报价单号" />
                            <asp:BoundField DataField="taskid" HeaderText="任务编号" />
                            <asp:BoundField DataField="type" HeaderText="分款项目" />
                            <asp:BoundField DataField="project" HeaderText="类别" />
                            <asp:BoundField DataField="beizhu3" HeaderText="部门" />

                            <asp:BoundField DataField="fillname" HeaderText="分款人" />

                            <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="分款日期" />
                            <asp:BoundField DataField="issubmit" HeaderText="提交状态" />
                            <asp:BoundField DataField="submitren" HeaderText="提交人" />
                            <asp:BoundField DataField="submittime" HeaderText="提交时间" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="isaffirm" HeaderText="确认状态" />
                            <asp:BoundField DataField="affirmren" HeaderText="确认人" />
                            <asp:BoundField DataField="affirmtime" HeaderText="确认日期" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="money" HeaderText="分款金额" DataFormatString="{0:N2}" />

                            <asp:CommandField ShowDeleteButton="true" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />

                        <EmptyDataTemplate>
                            <div style="color: Red;">
                                无到账信息
                            </div>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>

                    </td>

                </tr>

                <tr>
                    <td colspan="4" class="auto-style1">
                        <asp:Button ID="Button3" runat="server" Text="提交分款" OnClick="Button3_Click" OnClientClick="return confirm('确认要提交吗？提交后不能删除');" />
                        已经提交的分款不能再提交，提交只针对未提交的分款，分款提交后不能删除，只能由财务退回或者财务确认后反结算
                    </td>
                </tr>

            </table>
        </div>
        <asp:Literal ID="ld" runat="server"></asp:Literal>

    </form>
</body>
</html>
